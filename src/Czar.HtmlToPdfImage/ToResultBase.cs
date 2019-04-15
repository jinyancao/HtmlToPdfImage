using Czar.HtmlToPdfImage.Options;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Czar.HtmlToPdfImage
{
    public abstract class ToResultBase
    {
        protected ToResultBase()
        {
            this.WkhtmlPath = string.Empty;
            this.FormsAuthenticationCookieName = ".ASPXAUTH";
        }

        /// <summary>
        /// This will be send to the browser as a name of the generated PDF file.
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// Path to wkhtmltopdf\wkhtmltoimage binary.
        /// </summary>
        public string WkhtmlPath { get; set; }

        /// <summary>
        /// Custom name of authentication cookie used by forms authentication.
        /// </summary>
        public string FormsAuthenticationCookieName { get; set; }

        /// <summary>
        /// Sets custom headers.
        /// </summary>
        [OptionFlag("--custom-header")]
        public Dictionary<string, string> CustomHeaders { get; set; }

        /// <summary>
        /// Sets cookies.
        /// </summary>
        [OptionFlag("--cookie")]
        public Dictionary<string, string> Cookies { get; set; }

        /// <summary>
        /// Sets post values.
        /// </summary>
        [OptionFlag("--post")]
        public Dictionary<string, string> Post { get; set; }

        /// <summary>
        /// Indicates whether the page can run JavaScript.
        /// </summary>
        [OptionFlag("-n")]
        public bool IsJavaScriptDisabled { get; set; }

        /// <summary>
        /// Minimum font size.
        /// </summary>
        [OptionFlag("--minimum-font-size")]
        public int? MinimumFontSize { get; set; }

        /// <summary>
        /// Sets proxy server.
        /// </summary>
        [OptionFlag("-p")]
        public string Proxy { get; set; }

        /// <summary>
        /// HTTP Authentication username.
        /// </summary>
        [OptionFlag("--username")]
        public string UserName { get; set; }

        /// <summary>
        /// HTTP Authentication password.
        /// </summary>
        [OptionFlag("--password")]
        public string Password { get; set; }

        /// <summary>
        /// Use this if you need another switches that are not currently supported by Rotativa.
        /// </summary>
        [OptionFlag("")]
        public string CustomSwitches { get; set; }

        public ContentDisposition ContentDisposition { get; set; }

        /// <summary>
        /// 待生成的地址
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Returns properties with OptionFlag attribute as one line that can be passed to wkhtmltopdf binary.
        /// </summary>
        /// <returns>Command line parameter that can be directly passed to wkhtmltopdf binary.</returns>
        protected virtual string GetConvertOptions()
        {
            var result = new StringBuilder();
            var fields = this.GetType().GetProperties();
            foreach (var fi in fields)
            {
                var of = fi.GetCustomAttributes(typeof(OptionFlag), true).FirstOrDefault() as OptionFlag;
                if (of == null)
                    continue;
                object value = fi.GetValue(this, null);
                if (value == null)
                    continue;
                if (fi.PropertyType == typeof(Dictionary<string, string>))
                {
                    var dictionary = (Dictionary<string, string>)value;
                    foreach (var d in dictionary)
                    {
                        result.AppendFormat(" {0} {1} {2}", of.Name, d.Key, d.Value);
                    }
                }
                else if (fi.PropertyType == typeof(bool))
                {
                    if ((bool)value)
                        result.AppendFormat(CultureInfo.InvariantCulture, " {0}", of.Name);
                }
                else
                {
                    result.AppendFormat(CultureInfo.InvariantCulture, " {0} {1}", of.Name, value);
                }
            }

            return result.ToString().Trim();
        }

        private string GetWkParams(string url)
        {
            var switches = string.Empty;
            switches += " " + this.GetConvertOptions();
            switches += " " + url;
            return switches;
        }

        protected virtual Task<byte[]> CallTheDriver(string url)
        {
            var switches = this.GetWkParams(url);
            var fileContent = this.WkhtmlConvert(switches);
            return Task.FromResult(fileContent);
        }

        protected abstract byte[] WkhtmlConvert(string switches);

        public async Task SaveFileAsync(string SaveOnServerPath)
        {
            if (string.IsNullOrEmpty(Url))
                throw new ArgumentNullException("url is null");
            this.WkhtmlPath = WkhtmltoxConfig.WkhtmltoxPath;
            var fileContent = await CallTheDriver(Url);
            var directoryName = Path.GetDirectoryName(SaveOnServerPath);
            if (Directory.Exists(directoryName) == false)
            {
                Directory.CreateDirectory(directoryName);
            }
            File.WriteAllBytes(SaveOnServerPath, fileContent);
        }
    }
}
