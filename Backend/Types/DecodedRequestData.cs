namespace Backend.Types
{
    using System.Collections.Generic;

    public class DecodedRequestData
    {
        public DecodedRequestData(string requestIdentifier, string requestedMethod, Dictionary<string, string> requestParameter)
        {
            this.RequestIdentifier = requestIdentifier;
            this.RequestedMethod = requestedMethod;
            this.RequestParameter = requestParameter;
        }

        public string RequestIdentifier { get; }

        public string RequestedMethod { get; }

        public Dictionary<string, string> RequestParameter { get; }
    }
}
