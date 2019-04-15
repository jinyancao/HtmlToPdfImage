using System;
using System.Collections.Generic;
using System.Text;

namespace Czar.HtmlToPdfImage
{
    public class UrlToPdf : ToPdfResultBase
    {
        public UrlToPdf(string url)
        {
            Url = url ?? string.Empty;
        }
    }
}
