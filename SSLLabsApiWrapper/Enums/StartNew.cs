using System.ComponentModel;

namespace SSLLabsApiWrapper
{
    /// <summary>
    /// If set to "on" then cached assessment results are ignored and a new assessment is started. 
    /// However, if there's already an assessment in progress, its status is delivered instead.
    /// </summary>
    public enum StartNew
    {
        [Description("on")] On,
        Ignore
    }
}