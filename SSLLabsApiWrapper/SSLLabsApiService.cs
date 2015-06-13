using System;
using System.Threading;
using SSLLabsApiWrapper.Domain;
using SSLLabsApiWrapper.External;
using SSLLabsApiWrapper.Interfaces;
using SSLLabsApiWrapper.Models;
using SSLLabsApiWrapper.Models.Response;
using SSLLabsApiWrapper.Models.Response.BaseSubModels;

namespace SSLLabsApiWrapper
{
    public class SSLLabsApiService
    {
        #region construction

        private readonly IApiProvider _apiProvider;
        private readonly RequestModelFactory _requestModelFactory;
        private readonly ResponsePopulation _responsePopulation;
        private readonly UrlValidation _urlValidation;
        private string ApiUrl { get; set; }

        public SSLLabsApiService(string apiUrl = "https://api.ssllabs.com/api/v2/")
            : this(apiUrl, new SSLLabsApi())
        {
        }

        internal SSLLabsApiService(string apiUrl, IApiProvider apiProvider)
        {
            _apiProvider = apiProvider;
            _requestModelFactory = new RequestModelFactory();
            _urlValidation = new UrlValidation();
            _responsePopulation = new ResponsePopulation();
            ApiUrl = _urlValidation.Format(apiUrl);
        }

        #endregion

        public Info Info()
        {
            Info infoModel = new Info() {};
            RequestModel requestModel = _requestModelFactory.NewInfoRequestModel(ApiUrl, "info");

            try
            {
                // Making Api request and binding result to model
                WebResponseModel webResponse = _apiProvider.MakeGetRequest(requestModel);
                infoModel = _responsePopulation.InfoModel(webResponse, infoModel);

                if (infoModel.EngineVersion != null)
                {
                    infoModel.Online = true;
                }
            }
            catch (Exception ex)
            {
                infoModel.HasErrorOccurred = true;
                infoModel.Errors.Add(new Error {message = ex.ToString()});
            }

            if (infoModel.Errors.Count != 0 && !infoModel.HasErrorOccurred)
            {
                infoModel.HasErrorOccurred = true;
            }

            return infoModel;
        }

        public Analyze Analyze(string host, Publish publish = Publish.Off, StartNew startNew = StartNew.On,
            FromCache fromCache = FromCache.Ignore, int? maxHours = null, All all = All.On,
            IgnoreMismatch ignoreMismatch = IgnoreMismatch.Off)
        {
            Analyze analyzeModel = new Analyze();

            // Checking host is valid before continuing
            if (!UrlValidation.IsValidHostname(host))
                throw new ArgumentException("Hostname is not valid: " + host);

            // Building request model
            RequestModel requestModel = _requestModelFactory.NewAnalyzeRequestModel(ApiUrl, "analyze", host, publish,
                startNew,
                fromCache, maxHours, all, ignoreMismatch);

            try
            {
                WebResponseModel webResponse = _apiProvider.MakeGetRequest(requestModel);
                analyzeModel = _responsePopulation.AnalyzeModel(webResponse, analyzeModel);
            }
            catch (Exception ex)
            {
                analyzeModel.HasErrorOccurred = true;
                analyzeModel.Errors.Add(new Error {message = ex.ToString()});
            }

            // Checking if errors have occoured either from ethier api or wrapper
            if (analyzeModel.Errors.Count != 0 && !analyzeModel.HasErrorOccurred)
            {
                analyzeModel.HasErrorOccurred = true;
            }

            return analyzeModel;
        }

        public Analyze AutomaticAnalyze(string host, int maxWaitInterval = 300, int sleepInterval = 15)
        {
            return AutomaticAnalyze(host, Publish.Off, StartNew.Ignore, FromCache.Ignore, null, All.Done,
                IgnoreMismatch.Off, maxWaitInterval, sleepInterval);
        }

        public Analyze AutomaticAnalyze(string host, Publish publish, StartNew startNew, FromCache fromCache,
            int? maxHours, All all, IgnoreMismatch ignoreMismatch,
            int maxWaitInterval, int sleepInterval)
        {
            DateTime startTime = DateTime.UtcNow;
            int sleepIntervalMilliseconds = sleepInterval*1000;
            int apiPassCount = 1;
            Analyze analyzeModel = Analyze(host, publish, startNew, fromCache, maxHours, all, ignoreMismatch);

            // Ignoring cache settings after first request to prevent loop
            startNew = StartNew.Ignore;

            // Shouldn't have to check status header as HasErrorOccurred should be enough
            while (analyzeModel.HasErrorOccurred == false && analyzeModel.Status != "READY" &&
                   (DateTime.UtcNow - startTime).TotalSeconds < maxWaitInterval)
            {
                Thread.Sleep(sleepIntervalMilliseconds);
                apiPassCount++;
                analyzeModel = Analyze(host, publish, startNew, fromCache, null, all, ignoreMismatch);
            }

            analyzeModel.Wrapper.ApiPassCount = apiPassCount;

            return analyzeModel;
        }

        public Endpoint GetEndpointData(string host, string server, FromCache fromCache = FromCache.Off)
        {
            Endpoint endpointModel = new Endpoint();

            // Checking host is valid before continuing
            if (!UrlValidation.IsValidHostname(host))
                throw new ArgumentException("Hostname is not valid: " + host);

            // Building request model
            RequestModel requestModel = _requestModelFactory.NewEndpointDataRequestModel(ApiUrl, "getEndpointData", host,
                server, fromCache.ToString());

            try
            {
                WebResponseModel webResponse = _apiProvider.MakeGetRequest(requestModel);
                endpointModel = _responsePopulation.EndpointModel(webResponse, endpointModel);
            }
            catch (Exception ex)
            {
                endpointModel.HasErrorOccurred = true;
                endpointModel.Errors.Add(new Error {message = ex.ToString()});
            }

            // Checking if errors have occoured either from ethier api or wrapper
            if (endpointModel.Errors.Count != 0 && !endpointModel.HasErrorOccurred)
            {
                endpointModel.HasErrorOccurred = true;
            }

            return endpointModel;
        }

        public StatusCodes GetStatusCodes()
        {
            StatusCodes statusCodesModel = new StatusCodes();
            RequestModel requestModel = _requestModelFactory.NewStatusCodesRequestModel(ApiUrl, "getStatusCodes");

            try
            {
                WebResponseModel webResponse = _apiProvider.MakeGetRequest(requestModel);
                statusCodesModel = _responsePopulation.StatusCodesModel(webResponse, statusCodesModel);
            }
            catch (Exception ex)
            {
                statusCodesModel.HasErrorOccurred = true;
                statusCodesModel.Errors.Add(new Error {message = ex.ToString()});
            }

            return statusCodesModel;
        }
    }
}