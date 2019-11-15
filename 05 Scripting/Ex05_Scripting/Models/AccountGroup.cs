using System;
using System.Collections.Generic;

namespace Ex05_Scripting.Models
{
    public partial class AccountGroup
    {
        public AccountGroup()
        {
            SubjectAccounts = new HashSet<Account>();
            FunctionAccounts = new HashSet<Account>();
            SubGroups = new HashSet<AccountGroup>();
        }

        public string Type { get; set; }
        public string Id { get; set; }
        public string ParentId { get; set; }
        public int Level { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual AccountGroup ParentGroup { get; set; }
        public virtual ICollection<Account> SubjectAccounts { get; set; }
        public virtual ICollection<Account> FunctionAccounts { get; set; }
        public virtual ICollection<AccountGroup> SubGroups { get; set; }
    }
}
