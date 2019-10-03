using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Web.Mvc;

namespace Ex01
{
    public class ThumbnailResult : ActionResult
    {
        public int Size { get; set; } = 200;

        public string Path { get; set; } = "/notfound.jpg";

        public override void ExecuteResult(ControllerContext context)
        {
            context.HttpContext.Response.Clear();
            context.HttpContext.Response.ContentType = "image/png";

            var img = System.Drawing.Image.FromFile(this.Path);

            int w = img.Width;
            int h = img.Height;
            double factor = (double)this.Size / ((w > h) ? (double)w : (double)h);
            w = (int)(w * factor);
            h = (int)(h * factor);

            var tn = new Bitmap(img, new Size(w, h));
            tn.Save(context.HttpContext.Response.OutputStream, ImageFormat.Png);
        }
    }
}