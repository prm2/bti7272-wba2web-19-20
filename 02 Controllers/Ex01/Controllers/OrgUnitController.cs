using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ex01.Controllers
{
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