using System.ComponentModel;

namespace SSLLabsApiWrapper
{
    /// <summary>
    /// Always deliver cached assessment reports if available; optional, defaults to "off". 
    /// This parameter is intended for API consumers that don't want to wait for assessment results. Can't be used at the same time as the startNew parameter.
    /// </summary>
    public enum FromCache
    {
        [Description("on")] On,
        [Description("off")] Off,
        Ignore
    }
}