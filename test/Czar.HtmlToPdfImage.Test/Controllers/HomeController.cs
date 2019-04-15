using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Czar.HtmlToPdfImage.Test.Models;
using System.IO;
using Czar.HtmlToPdfImage.AspNetCore;

namespace Czar.HtmlToPdfImage.Test.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
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


        /// <summary>
        /// save image on server and response as image
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public IActionResult Image(string url = "http://news.baidu.com")
        {
            // way 1:
            //ViewData["Message"] = "Your application description page.";
            //var model = new TestModel { Name = "Giorgio" };
            //var image= new ViewAsImage("About",model);
            //image.PageWidth = 800;
            //image.PageHeight = 600;
            //return image;

            //way 2:
            var image = new UrlAsImage(url);
            image.SaveOnServerPath = Path.Combine("wwwroot/files/image", DateTime.Now.ToString("yyyyMMddhhmmss") + ".png");
            return image;
        }

        /// <summary>
        /// save image to server and return savePath not image
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public async Task<object> ImageFile(string url = "http://news.baidu.com")
        {
            var image = new UrlAsImage(url);
            var path = Path.Combine("wwwroot/files/image", DateTime.Now.ToString("yyyyMMddhhmmss") + ".png");
            image.SaveOnServerPath = path;
            await image.BuildFile(url);
            return new { FileName = path };

        }

        /// <summary>
        /// return pdf without saving to server
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public IActionResult Pdf(string url = "http://news.baidu.com")
        {
            // way 1:
            //ViewData["Message"] = "Your application description page.";
            //var model = new TestModel { Name = "Giorgio" };
            //var pdf = new ViewAsPdf("About", model);
            //return pdf;

            //way 2:
            var pdf = new UrlAsPdf(url);
            pdf.SaveOnServerPath = Path.Combine("wwwroot/files/pdf", DateTime.Now.ToString("yyyyMMddhhmmss") + ".pdf");
            return pdf;
        }

        /// <summary>
        /// save image to server and return savePath not image
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public async Task<object> PdfFile(string url = "http://news.baidu.com")
        {
            var pdf = new UrlAsPdf(url);
            var path = Path.Combine("wwwroot/files/pdf", DateTime.Now.ToString("yyyyMMddhhmmss") + ".pdf");
            pdf.SaveOnServerPath = path;
            await pdf.BuildFile(url);
            return new { FileName = path };

        }

        public async Task<IActionResult> ToPdf(string url = "/Home/Index")
        {
            url = string.Format("{0}://{1}{2}", HttpContext.Request.Scheme, HttpContext.Request.Host, url);
            var pdf = new UrlToPdf(url);
            await pdf.SaveFileAsync(Path.Combine("wwwroot/files/pdf", DateTime.Now.ToString("yyyyMMddhhmmss") + ".pdf"));
            return Content("ok");
        }

        public async Task<IActionResult> ToImage(string url = "/Home/Index")
        {
            url = string.Format("{0}://{1}{2}", HttpContext.Request.Scheme, HttpContext.Request.Host, url);
            var image = new UrlToImage(url);
            await image.SaveFileAsync(Path.Combine("wwwroot/files/image", DateTime.Now.ToString("yyyyMMddhhmmss") + ".png"));
            return Content("ok");
        }
    }
}
