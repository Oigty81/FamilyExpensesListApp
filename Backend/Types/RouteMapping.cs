
namespace Backend.Types
{
    using System.Collections.Generic;
    using System.Reflection;

    public class RouteMapping
    {
        public RouteMapping(
            string route,
            Type controllerType,
            MethodInfo routeMethod,
            List<ParameterInfo> parameters,
            List<string> allowedMethods,
            string resultContentType)
        {
            this.Route = route;
            this.ControllerType = controllerType;
            this.RouteMethod = routeMethod;
            this.Parameters = parameters;
            this.AllowedMethods = allowedMethods;
            this.ResultContentType = resultContentType;
        }

        public string Route { get; }

        public Type ControllerType { get; }

        public MethodInfo RouteMethod { get; }

        public List<ParameterInfo> Parameters { get; }

        public List<string> AllowedMethods { get; }

        public string ResultContentType { get; }
    }
}
