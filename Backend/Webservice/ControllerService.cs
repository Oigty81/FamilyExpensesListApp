namespace Backend.Webservice
{
    using Microsoft.AspNetCore.Mvc;
    using System.Reflection;
    using Backend.Types;

    public class ControllerService : IControllerService
    {
        public Dictionary<string, RouteMapping> InitControllers()
        {
            var typeList = Assembly.GetExecutingAssembly().GetTypes();
            Dictionary<string, RouteMapping> routeMapping = new Dictionary<string, RouteMapping>();

            foreach (Type type in typeList
                         .Where(t => t.IsClass && !t.IsAbstract && t.IsSubclassOf(typeof(ControllerBase)) &&
                                     t.CustomAttributes.Count(x => x.AttributeType == typeof(RouteAttribute)) > 0 &&
                                     t.CustomAttributes.First(x => x.AttributeType == typeof(RouteAttribute)).ConstructorArguments.Count > 0))
            {
                string routeBase = type.CustomAttributes.First(x => x.AttributeType == typeof(RouteAttribute)).ConstructorArguments[0].Value.ToString();

                foreach (MethodInfo method in type.GetRuntimeMethods().Where(m => m.CustomAttributes.Count(x => x.AttributeType == typeof(RouteAttribute)) > 0 &&
                             m.CustomAttributes.First(x => x.AttributeType == typeof(RouteAttribute)).ConstructorArguments.Count > 0))
                {
                    var route = routeBase + "/" + method.CustomAttributes.First(x => x.AttributeType == typeof(RouteAttribute)).ConstructorArguments[0].Value;
                    var allowedMethods = new List<string>();

                    var resultContentType = "application/json";

                    if (method.CustomAttributes.Count(x => x.AttributeType == typeof(HttpGetAttribute)) > 0)
                    {
                        allowedMethods.Add("GET");
                    }

                    if (method.CustomAttributes.Count(x => x.AttributeType == typeof(HttpPostAttribute)) > 0)
                    {
                        allowedMethods.Add("POST");
                    }

                    if (method.CustomAttributes.Count(x => x.AttributeType == typeof(HttpPutAttribute)) > 0)
                    {
                        allowedMethods.Add("PUT");
                    }

                    if (method.CustomAttributes.Count(x => x.AttributeType == typeof(ProducesAttribute)) > 0)
                    {
                       resultContentType = method.CustomAttributes.First(x => x.AttributeType == typeof(ProducesAttribute)).ConstructorArguments[0].Value.ToString();
                    }

                    routeMapping.Add(route, new RouteMapping(route, type, method, method.GetParameters().ToList(), allowedMethods, resultContentType));
                }
            }

            return routeMapping;
        }
    }
}
