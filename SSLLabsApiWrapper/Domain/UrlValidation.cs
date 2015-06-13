using System;

namespace SSLLabsApiWrapper.Domain
{
    internal class UrlValidation
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

        public static bool IsValidHostname(string hostname)
        {
            if (Uri.CheckHostName(hostname) == UriHostNameType.Dns && hostname.Contains("."))
                return true;

            Uri uri;
            if (Uri.TryCreate(hostname, UriKind.Absolute, out uri) && (uri.Scheme == "https" || uri.Scheme == "http"))
                return true;

            return false;
        }
    }
}