using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DB = Ex08_API.Models;
using Shared = Models;

namespace Ex08_API
{
    public class DataService
    {
        Shared.Account Cast(DB.Account a)
        {
            return new Shared.Account
            {
                Id = $"{a.FunctionId}.{a.SubjectId}.{a.Number:D2}",
                Name = a.Name
            };
        }

        Shared.Account Cast(DB.AccountYear y)
        {
            var account = Cast(y.Account);

            account.ExpensesBudget = y.ExpensesBudget;
            account.IncomeBudget = y.IncomeBudget;
            account.ExpensesEffective = y.ExpensesEffective;
            account.IncomeEffective = y.IncomeEffective;

            return account;
        }

        Shared.AccountGroup Cast(DB.AccountGroup g, List<DB.AccountYear> accounts)
        {
            var group = new Shared.AccountGroup
            {
                Id = g.Id,
                Name = g.Name,
                SubGroups = g.SubGroups
                    .Select(g => Cast(g, accounts))
                    .OrderBy(g => g.Id)
                    .ToList(),
                Accounts = accounts
                    .Where(a => a.FunctionId == g.Id)
                    .Select(a => Cast(a))
                    .OrderBy(a => a.Id)
                    .ToList()
            };

            group.ExpensesBudget = 
                group.Accounts.Sum(a => a.ExpensesBudget) + 
                group.SubGroups.Sum(g => g.ExpensesBudget);
            group.IncomeBudget = 
                group.Accounts.Sum(a => a.IncomeBudget) + 
                group.SubGroups.Sum(g => g.IncomeBudget);
            group.ExpensesEffective = 
                group.Accounts.Sum(a => a.ExpensesEffective) + 
                group.SubGroups.Sum(g => g.ExpensesEffective);
            group.IncomeEffective = 
                group.Accounts.Sum(a => a.IncomeEffective) + 
                group.SubGroups.Sum(g => g.IncomeEffective);

            return group;
        }

        public async Task<List<Shared.Account>> AccountsAsync(int? year = null, string id = null)
        {
            using (var db = new DB.WBA2WebContext())
            {
                if (year == null)
                {
                    if (id == null)
                        return (await db.Account.ToListAsync())
                            .Select(a => Cast(a))
                            .OrderBy(a => a.Id)
                            .ToList();
                    else
                        return (await db.Account
                            .Where(a => a.FunctionId == id).ToListAsync())
                            .Select(a => Cast(a))
                            .OrderBy(a => a.Id)
                            .ToList();
                }
                else
                {
                    if (id == null)
                        return (await db.AccountYear.Include(y => y.Account)
                            .Where(y => y.Year == (year ?? 0)).ToListAsync())
                            .Select(a => Cast(a))
                            .OrderBy(a => a.Id)
                            .ToList();
                    else
                        return (await db.AccountYear.Include(y => y.Account)
                            .Where(y => y.Year == (year ?? 0) && y.FunctionId == id).ToListAsync())
                            .Select(a => Cast(a))
                            .OrderBy(a => a.Id)
                            .ToList();
                }
            }
        }

        public async Task<IEnumerable<Shared.AccountGroup>> ER(int? year)
        {
            using (var db = new DB.WBA2WebContext())
            {
                var accounts = await db.AccountYear
                    .Where(a => a.Year == (year ?? 0))
                    .ToListAsync();

                var groups =  await db.AccountGroup
                    .Include(a => a.SubGroups)
                    .Include(a => a.FunctionAccounts)
                    .Where(a => a.Level > 0 && a.Type == "FG")
                    .OrderBy(a => a.Id)
                    .ToListAsync();

                return groups
                    .Where(g => g.Level == 1)
                    .Select(g => Cast(g, accounts))
                    .OrderBy(g => g.Id);
            }
        }

        public async Task<Shared.Budget> Budget(int? year)
        {
            var er = await ER(year);

            return new Shared.Budget
            {
                Year = year ?? 0,
                Items = er.Select(g => new Shared.BudgetItem {
                    AccountGroup = g,
                    Amount = (g.ExpensesBudget ?? 0.00m) - (g.IncomeBudget ?? 0.00m)
                })
                .Where(b => b.Amount > 0.00m)
                .OrderBy(b => b.AccountGroup.Id)
                .ToList()
            };
        }

    }
}
