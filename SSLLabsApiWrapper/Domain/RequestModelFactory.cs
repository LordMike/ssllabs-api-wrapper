using SSLLabsApiWrapper.Models;
using SSLLabsApiWrapper.Utilities;

namespace SSLLabsApiWrapper.Domain
{
    internal class RequestModelFactory
    {
        public RequestModel NewInfoRequestModel(string apiBaseUrl, string action)
        {
            return new RequestModel() {ApiBaseUrl = apiBaseUrl, Action = action};
        }

        public RequestModel NewAnalyzeRequestModel(string apiBaseUrl, string action, string host, Publish publish,
            StartNew startNew,
            FromCache fromCache, int? maxHours, All all, IgnoreMismatch ignoreMismatch)
        {
            RequestModel requestModel = new RequestModel() {ApiBaseUrl = apiBaseUrl, Action = action};

            requestModel.Parameters.Add("host", host);
            requestModel.Parameters.Add("publish", publish.GetDescription());

            if (all != All.Ignore)
                requestModel.Parameters.Add("all", all.GetDescription());

            if (startNew != StartNew.Ignore)
                requestModel.Parameters.Add("startNew", startNew.GetDescription());

            if (fromCache != FromCache.Ignore)
                requestModel.Parameters.Add("fromCache", fromCache.GetDescription());

            if (!maxHours.HasValue)
                requestModel.Parameters.Add("maxHours", maxHours.ToString());

            if (ignoreMismatch != IgnoreMismatch.Off)
                requestModel.Parameters.Add("ignoreMismatch", ignoreMismatch.GetDescription());

            return requestModel;
        }

        public RequestModel NewEndpointDataRequestModel(string apiBaseUrl, string action, string host, string s,
            string fromCache)
        {
            RequestModel requestModel = new RequestModel() {ApiBaseUrl = apiBaseUrl, Action = action};

            requestModel.Parameters.Add("host", host);
            requestModel.Parameters.Add("s", s);
            requestModel.Parameters.Add("fromCache", fromCache);

            return requestModel;
        }

        public RequestModel NewStatusCodesRequestModel(string apiBaseUrl, string action)
        {
            return new RequestModel() {ApiBaseUrl = apiBaseUrl, Action = action};
        }
    }
}