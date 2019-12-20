using System;
using System.Collections.Generic;

namespace Ex08_API.Models
{
    public partial class AccountYear
    {
        public string Type { get; set; }
        public string SubjectId { get; set; }
        public string FunctionType { get; set; }
        public string FunctionId { get; set; }
        public int Number { get; set; }
        public int Year { get; set; }
        public decimal? ExpensesBudget { get; set; }
        public decimal? IncomeBudget { get; set; }
        public decimal? ExpensesEffective { get; set; }
        public decimal? IncomeEffective { get; set; }

        public virtual Account Account { get; set; }
    }
}
