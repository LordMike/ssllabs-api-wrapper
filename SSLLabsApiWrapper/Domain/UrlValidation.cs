using System;

namespace SSLLabsApiWrapper.Domain
{
    class UrlValidation
    {
        public bool IsValid(string url)
        {
            bool valid = true;

            Uri uri = null;
            if (!Uri.TryCreate(url, UriKind.Absolute, out uri) || null == uri)
                valid = false;

            return valid;
        }

        public string Format(string url)
        {
            if (url.EndsWith("/"))
                url = url.Substring(0, url.Length - 1);

            return url;
        }
    }
}