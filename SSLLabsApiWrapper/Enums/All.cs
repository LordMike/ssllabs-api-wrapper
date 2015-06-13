using System.ComponentModel;

namespace SSLLabsApiWrapper
{
    /// <summary>
    /// By default this call results only summaries of individual endpoints. 
    /// If this parameter is set to "on", full information will be returned. 
    /// If set to "done", full information will be returned only if the assessment is complete (status is READY or ERROR).
    /// </summary>
    public enum All
    {
        [Description("on")] On,
        [Description("done")] Done,
        Ignore
    }
}