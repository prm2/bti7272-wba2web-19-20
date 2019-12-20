using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class AccountGroup
    {
        public String Id { get; set; }
        public String Name { get; set; }
        public List<AccountGroup> SubGroups { get; set; }
        public List<Account> Accounts { get; set; }
        public decimal? ExpensesBudget { get; set; }
        public decimal? IncomeBudget { get; set; }
        public decimal? ExpensesEffective { get; set; }
        public decimal? IncomeEffective { get; set; }
    }
}
