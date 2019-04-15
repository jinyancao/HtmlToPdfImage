using System;
using System.IO;

namespace Czar.HtmlToPdfImage
{
    public static class WkhtmltoxConfig
    {
        public static string WkhtmltoxPath { get; private set; }

        /// <summary>
        /// Setup wkhtmltox library
        /// </summary>
        /// <param name="env">The IHostingEnvironment object</param>
        /// <param name="wkhtmltopdfRelativePath">Optional. Relative path to the directory containing wkhtmltopdf.exe. Default is "Rotativa". Download at https://wkhtmltopdf.org/downloads.html</param>
        public static void Setup(string WebRootPath, string wkhtmltopdfRelativePath = "Wkhtmltox") 
        {
            var wkhtmltoxPath = Path.Combine(WebRootPath, wkhtmltopdfRelativePath);
            if (!Directory.Exists(wkhtmltoxPath))
            {
                throw new ApplicationException("Folder containing wkhtmltopdf.exe not found, searched for " + wkhtmltoxPath);
            }
            WkhtmltoxPath = wkhtmltoxPath;
        }
    }
}
