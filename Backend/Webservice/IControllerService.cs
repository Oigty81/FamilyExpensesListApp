using Backend.Types;

namespace Backend.Webservice
{
    public interface IControllerService
    {
        Dictionary<string, RouteMapping> InitControllers();
    }
}