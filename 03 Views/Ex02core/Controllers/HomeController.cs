using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Ex02core.Models;
using System.IO;

namespace Ex02core.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            var path = AppDomain.CurrentDomain.GetData("App_Data").ToString();
            var files = Directory.GetFiles(path, "*.jpg");

            ViewData["ImageCount"] = files.Length;

            return View();
        }

        public IActionResult Image(int? id)
        {
            var path = AppDomain.CurrentDomain.GetData("App_Data").ToString();
            var files = Directory.GetFiles(path, "*.jpg");
            var filename = files[(id ?? 0) % files.Length];

            ViewData["ImageIndex"] = (id ?? 0) % files.Length;
            ViewData["ImageName"] = (new FileInfo(filename)).Name;

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
