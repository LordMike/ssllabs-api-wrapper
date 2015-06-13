using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SSLLabsApiWrapper;
using SSLLabsApiWrapper.Domain;

namespace given_that_I_make_a_analyze_request
{
    [TestClass]
    public class HostNameChecks
    {
        [TestMethod]
        public void HostNameCheck_Valid()
        {
            UrlValidation.IsValidHostname("adsfa3243124$@£$£@$").Should().BeFalse();
            UrlValidation.IsValidHostname("google.com").Should().BeTrue();
            UrlValidation.IsValidHostname("www.ashleypoole.co.uk").Should().BeTrue();
            UrlValidation.IsValidHostname("123431244123").Should().BeFalse();

            UrlValidation.IsValidHostname("ftp://asdfdasf.com").Should().BeFalse();
            UrlValidation.IsValidHostname("https://asdfdasf.com").Should().BeTrue();
        }
    }
}