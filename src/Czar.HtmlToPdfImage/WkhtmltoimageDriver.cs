
namespace Czar.HtmlToPdfImage
{
    public class WkhtmltoimageDriver : WkhtmlDriver
    {
        private const string _wkhtmlExe = "wkhtmltoimage.exe";

        /// <summary>
        /// Converts given HTML string to Image.
        /// </summary>
        /// <param name="wkhtmltopdfPath">Path to wkthmltopdf.</param>
        /// <param name="switches">Switches that will be passed to wkhtmltopdf binary.</param>
        /// <param name="html">String containing HTML code that should be converted to Image.</param>
        /// <returns>PDF as byte array.</returns>
        public static byte[] ConvertHtml(string wkhtmltopdfPath, string switches, string html)
        {
            return Convert(wkhtmltopdfPath, switches, html, _wkhtmlExe);
        }

        /// <summary>
        /// Converts given URL to Image.
        /// </summary>
        /// <param name="wkhtmltopdfPath">Path to wkthmltoimage.</param>
        /// <param name="switches">Switches that will be passed to wkhtmltopdf binary.</param>
        /// <returns>PDF as byte array.</returns>
        public static byte[] Convert(string wkhtmltopdfPath, string switches)
        {
            return Convert(wkhtmltopdfPath, switches, null, _wkhtmlExe);
        }
    }
}
