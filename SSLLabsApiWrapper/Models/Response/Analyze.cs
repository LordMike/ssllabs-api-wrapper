using System.Collections.Generic;

namespace SSLLabsApiWrapper.Models.Response
{
    public class Analyze : BaseModel
    {
        public string Host { get; set; }

        public int Port { get; set; }

        public string Protocol { get; set; }

        public bool IsPublic { get; set; }

        public string Status { get; set; }

        public string StatusMessage { get; set; }

        public long StartTime { get; set; }

        public long TestTime { get; set; }

        public string EngineVersion { get; set; }

        public string CriteriaVersion { get; set; }

        public List<Endpoint> Endpoints { get; set; }
    }
}