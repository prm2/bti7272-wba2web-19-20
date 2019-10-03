using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ex01.Controllers
{
    public class ImageController : Controller
    {
        // GET: Image
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Show(int? id)
        {
            var path = Server.MapPath("~/App_Data/");
            var files = Directory.GetFiles(path, "*.jpg");

            return File(files[(id ?? 0) % files.Length], "image/jpeg");
        }

        public ActionResult Thumbnail(int? id, int size = 400)
        {
            var path = Server.MapPath("~/App_Data/");
            var files = Directory.GetFiles(path, "*.jpg");

            var res = new ThumbnailResult();
            res.Path = files[(id ?? 0) % files.Length];
            res.Size = size;

            return res;
        }
    }
}