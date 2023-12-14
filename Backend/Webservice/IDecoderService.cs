using Backend.Types;

namespace Backend.Webservice
{
    public interface IDecoderService
    {
        Dictionary<string, string> DeserializeParameterData(string parameterData);

        DecodedRequestData DecodeRequestData(string rawUrl, string postParameterData);
    }
}