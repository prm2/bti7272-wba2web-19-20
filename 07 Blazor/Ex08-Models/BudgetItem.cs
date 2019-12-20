using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class BudgetItem
    {
        public AccountGroup AccountGroup { get; set; }
        public decimal Amount { get; set; }
    }
}
