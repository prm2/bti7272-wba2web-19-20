using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace Ex02core.Controllers
{
    public class ImageController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Show(int? id)
        {
            var path = AppDomain.CurrentDomain.GetData("App_Data").ToString();
            var files = Directory.GetFiles(path, "*.jpg");

            return PhysicalFile(files[(id ?? 0) % files.Length], "image/jpeg");
        }

        public IActionResult Thumbnail(int? id, int size = 400)
        {
            var path = AppDomain.CurrentDomain.GetData("App_Data").ToString();
            var files = Directory.GetFiles(path, "*.jpg");

            var tn = new Thumbnail();
            tn.Path = files[(id ?? 0) % files.Length];
            tn.Size = size;

            return File(tn.ToBytes(), "image/png");
        }
    }
}