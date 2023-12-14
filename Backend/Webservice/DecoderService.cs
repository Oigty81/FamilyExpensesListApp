namespace Backend.Webservice
{
    using System;
    using System.Collections.Generic;
    using System.Web;

    using Backend.Types;
    using Backend.Utilities;

    public class DecoderService : IDecoderService
    {
        private readonly ILoggingService loggingService;

        public DecoderService(ILoggingService loggingService)
        {
            this.loggingService = loggingService;
        }
        public Dictionary<string, string> DeserializeParameterData(string parameterData)
        {
            Dictionary<string, string> parameterMap = new Dictionary<string, string>();

            try
            {
                string[] parameterList = parameterData.Split('&');

                foreach (var parameterPair in parameterList)
                {
                    if (parameterPair.Contains("=") && !parameterPair.StartsWith("{") && !parameterPair.StartsWith("["))
                    {
                        parameterMap[parameterPair.Substring(0, parameterPair.IndexOf("="))] = HttpUtility.UrlDecode(parameterPair.Substring(parameterPair.IndexOf("=") + 1));
                    }
                    else
                    {
                        parameterMap[parameterPair] = "";
                        parameterMap["body"] = parameterPair;
                    }
                }
            }
            catch (Exception ex)
            {
                this.loggingService.LogConsole(ex.Message);
            }

            return parameterMap;
        }

        public DecodedRequestData DecodeRequestData(string rawUrl, string postParameterData)
        {
            string url = rawUrl.Contains("?") ? rawUrl.Substring(0, rawUrl.IndexOf("?")) : rawUrl;
            string parameterData = rawUrl.Contains("?") ? rawUrl.Substring(rawUrl.IndexOf("?") + 1) + "&" + postParameterData : postParameterData;

            string[] urlParts = url.Split('/');
            string? requestIdentifier = null;
            string? reqestedMethod = null;

            if (urlParts.Length > 2)
            {
                requestIdentifier = urlParts[2];
            }

            if (urlParts.Length > 3)
            {
                reqestedMethod = urlParts[3];
            }

            if(requestIdentifier == null || reqestedMethod == null)
            {
                throw new Exception("Wrong request data");
            }

            return new DecodedRequestData(requestIdentifier, reqestedMethod, this.DeserializeParameterData(parameterData));
        }
    }
}
