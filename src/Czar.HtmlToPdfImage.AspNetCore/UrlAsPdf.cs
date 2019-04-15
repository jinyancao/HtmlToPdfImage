using System;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace Czar.HtmlToPdfImage.AspNetCore
{
    public class UrlAsPdf : AsPdfResultBase
    {
        private readonly string _url;

        public UrlAsPdf(string url)
        {
            _url = url ?? string.Empty;
        }

        protected override string GetUrl(ActionContext context)
        {
            string urlLower = _url.ToLower();
            if (urlLower.StartsWith("http://") || urlLower.StartsWith("https://"))
            {
                return _url;
            }
            var currentUri = new Uri(context.HttpContext.Request.GetDisplayUrl());
            var authority = currentUri.GetComponents(UriComponents.StrongAuthority, UriFormat.Unescaped);
            var url = string.Format("{0}://{1}{2}", context.HttpContext.Request.Scheme, authority, _url);
            return url;
        }
    }
}