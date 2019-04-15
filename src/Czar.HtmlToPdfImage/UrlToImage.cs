using System;
using System.Collections.Generic;
using System.Text;

namespace Czar.HtmlToPdfImage
{
    public class UrlToImage : ToImageResultBase
    {
        public UrlToImage(string url)
        {
            Url = url ?? string.Empty;
        }
    }
}
