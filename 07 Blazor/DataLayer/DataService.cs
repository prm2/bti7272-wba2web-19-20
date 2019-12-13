using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace DataLayer
{
    public class DataService
    {
        public async Task<List<Account>> AccountsAsync(string id = null)
        {
            using (var db = new WBA2WebContext())
            {
                if (id == null)
                    return await db.Account.ToListAsync();
                else
                    return await db.Account
                        .Where(a => a.FunctionId == id)
                        .ToListAsync();
            }
        }

        public async Task<IEnumerable<AccountGroup>> ER()
        {
            using (var db = new WBA2WebContext())
            {
                return await db.AccountGroup
                    .Include(a => a.SubGroups)
                    .Include(a => a.FunctionAccounts)
                    .Where(a => a.Level > 0 && a.Type == "FG")
                    .OrderBy(a => a.Id)
                    .ToListAsync();
            }
        }

    }
}
