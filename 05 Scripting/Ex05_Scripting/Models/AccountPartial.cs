using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ex05_Scripting.Models
{
    public partial class Account
    {
        public string Id
        {
            get
            {
                return $"{FunctionId}.{SubjectId}.{Number}";
            }
        }
    }
}
