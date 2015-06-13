using System.IO;
using System.Linq;
using System.Net;
using SSLLabsApiWrapper.Interfaces;
using SSLLabsApiWrapper.Models;

namespace SSLLabsApiWrapper.External
{
    internal class SSLLabsApi : IApiProvider
    {
        public WebResponseModel MakeGetRequest(RequestModel requestModel)
        {
            string url = requestModel.ApiBaseUrl + "/" + requestModel.Action;

            if (requestModel.Parameters.Count >= 1)
            {
                url = string.Format("{0}{1}{2}", url, "?", string.Join("&", (from parameter in requestModel.Parameters
                    where parameter.Value != null
                    select string.Format("{0}={1}", parameter.Key, parameter.Value))));
            }

            WebResponseModel webResponseModel = new WebResponseModel() {Url = url};

            HttpWebRequest request = (HttpWebRequest) WebRequest.Create(url);
            request.Method = "GET";

            HttpWebResponse response = (HttpWebResponse) request.GetResponse();
            StreamReader streamReader = new StreamReader(response.GetResponseStream());

            webResponseModel.Payloay = streamReader.ReadToEnd();
            webResponseModel.StatusCode = (int) response.StatusCode;
            webResponseModel.StatusDescription = response.StatusDescription;
            webResponseModel.Url = url;

            return webResponseModel;
        }
    }
}