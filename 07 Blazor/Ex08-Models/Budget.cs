using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class Budget
    {
        public int Year { get; set; }
        public List<BudgetItem> Items { get; set; }
    }
}
