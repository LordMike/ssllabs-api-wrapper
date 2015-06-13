using System.Net;
using SSLLabsApiWrapper.Models;

namespace SSLLabsApiWrapper.Interfaces
{
    internal interface IApiProvider
    {
        WebResponseModel MakeGetRequest(RequestModel requestModel);
    }
}