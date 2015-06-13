using System.Collections.Generic;

namespace SSLLabsApiWrapper.Models.Response
{
    public class Info : BaseModel
    {
        public string EngineVersion { get; set; }

        public string CriteriaVersion { get; set; }

        public int ClientMaxAssessments { get; set; }

        public int CurrentAssessments { get; set; }

        public List<string> Messages { get; set; }

        public bool Online { get; set; }

        public Info()
        {
            // Assigning default online status
            this.Online = false;
        }
    }
}
