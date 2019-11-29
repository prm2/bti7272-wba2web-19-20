using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Ex05_Scripting.Models
{
    public partial class Account
    {
        public string Type { get; set; }
        public string SubjectId { get; set; }
        public string FunctionType { get; set; }
        public string FunctionId { get; set; }
        public int Number { get; set; }
        [Required]
        public string Name { get; set; }

        public virtual AccountGroup AccountGroup { get; set; }
        public virtual AccountGroup Function { get; set; }
    }
}
