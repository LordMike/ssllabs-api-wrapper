using System.ComponentModel;

namespace SSLLabsApiWrapper
{
    /// <summary>
    /// Set to "on" to proceed with assessments even when the server certificate doesn't match the assessment hostname. 
    /// Set to off by default. Please note that this parameter is ignored if a cached report is returned.
    /// </summary>
    public enum IgnoreMismatch
    {
        [Description("on")] On,
        [Description("off")] Off
    }
}