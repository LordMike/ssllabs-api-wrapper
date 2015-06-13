using SSLLabsApiWrapper.Models.Response.EndpointSubModels;

namespace SSLLabsApiWrapper.Models.Response
{
    public class Endpoint : BaseModel
    {
        public string IpAddress { get; set; }

        public string ServerName { get; set; }

        public string StatusMessage { get; set; }

        public string StatusDetails { get; set; }

        public string StatusDetailsMessage { get; set; }

        public int Progress { get; set; }

        public int Eta { get; set; }

        public int Delegation { get; set; }

        // Two groups of poperities can be returned. Just seperating them out for my own reference.
        public int Duration { get; set; }

        public string Grade { get; set; }

        public string GradeTrustIgnored { get; set; }

        public bool HasWarnings { get; set; }

        public bool IsExceptional { get; set; }

        public Details Details { get; set; }

        public Endpoint()
        {
            Details = new Details();
        }
    }
}