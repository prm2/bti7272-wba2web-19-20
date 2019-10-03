using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Ex01core.Controllers
{
    [Route("OrgUnit/{action=show}/{*path=/}")]
    public class OrgUnitController : Controller
    {
        public string Show(string path)
        {
            return $"show org unit '{path}'";
        }

        public string Edit(string path)
        {
            return $"edit org unit '{path}'";
        }
    }
}