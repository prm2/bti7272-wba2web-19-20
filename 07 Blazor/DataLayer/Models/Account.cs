using System;
using System.Collections.Generic;

namespace DataLayer.Models
{
    public partial class Account
    {
        public string Type { get; set; }
        public string SubjectId { get; set; }
        public string FunctionType { get; set; }
        public string FunctionId { get; set; }
        public int Number { get; set; }
        public string Name { get; set; }

        public virtual AccountGroup AccountGroup { get; set; }
        public virtual AccountGroup Function { get; set; }

        public string Id
        {
            get
            {
                return $"{FunctionId}.{SubjectId}.{Number:D2}";
            }
        }
    }
}
