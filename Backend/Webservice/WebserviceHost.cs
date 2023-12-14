namespace Backend.Webservice
{
    using System.Net;
    using System.Reflection;
    using System.Text;
    using Backend.Types;
    using Backend.Utilities;
    using Newtonsoft.Json;
    using WatsonWebserver;

    public class WebserviceHost : IWebserviceHost
    {
        //// this field "availableRoutesForWatson" is necessary for pass the property content to Methods, which call with Watson Web Server over Dynamic Route-Attributes
        private static Dictionary<string, RouteMapping>? availableRoutesForWatson;
        private static IDecoderService? decoderServiceForWatson;

        private readonly ILoggingService? loggingService;
        private readonly IResourceLoader? ressourceLoader;
        private readonly IMimeTypeService? mimeTypeService;
        private readonly IControllerService? controllerService;
        private readonly IDecoderService? decoderService;
        private readonly ISystemUtilities? systemUtilities;

        private Server? server = null;

        private Dictionary<string, RouteMapping>? availableRoutes;

        public WebserviceHost() { } ////  this parameterless constructor is necessary to start Watson Web Server

        public WebserviceHost(
            ILoggingService loggingService,
            IResourceLoader ressourceLoader,
            IMimeTypeService mimeTypeService,
            IControllerService controllerService,
            IDecoderService decoderService,
            ISystemUtilities systemUtilities)
        {
            this.loggingService = loggingService;
            this.ressourceLoader = ressourceLoader;
            this.mimeTypeService = mimeTypeService;
            this.controllerService = controllerService;
            this.decoderService = decoderService;
            this.systemUtilities = systemUtilities;

            try
            {
                this.availableRoutes = this.controllerService.InitControllers();
            }
            catch (Exception e)
            {
                this.loggingService.LogConsole(e.Message + "\n" + e.StackTrace);
            }
        }

        public event Action<bool>? Connected;

        public int HttpPort { get; private set; }

        public bool IsConnected { get; private set; }

        public void StartListening()
        {
            this.IsConnected = false;

            //// TODO: implement free port search
            string httpPortString = "8002";
            this.HttpPort = Convert.ToInt32(httpPortString);

            //// !!! this assignment is necessary for pass the property content to Methods, which call with Watson Web Server over Dynamic Route-Attributes
            availableRoutesForWatson = this.availableRoutes;
            decoderServiceForWatson = this.decoderService;

            try
            {
                string newPrefix = "http://127.0.0.1:" + httpPortString + "/"; //// valid urlacl
                var settings = new WatsonWebserverSettings();

                settings.Prefixes.Add(newPrefix);
                this.server = new Server(settings, this.DefaultRequest);
                this.server.Start();
                this.IsConnected = true;
                this.Connected?.Invoke(true);
            }
            catch (Exception ex)
            {
                this.loggingService.LogConsole("error start listening - " + ex.Message + "\n" + ex.StackTrace);
                if (this.server != null)
                {
                    this.server.Stop();
                    this.server.Dispose();
                }

                this.systemUtilities.QuitApplication();
            }
        }

        public void StopListening()
        {
            if (this.server != null)
            {
                this.server.Stop();
                this.server.Dispose();
            }
        }

        [DynamicRoute(WatsonWebserver.HttpMethod.GET, "^(/api/|/swr/api)")]
        public async Task GetApi(HttpContext context)
        {
            var responseValue = this.ProcessApiRequest(context);
            await context.Response.Send(responseValue);
        }

        [DynamicRoute(WatsonWebserver.HttpMethod.POST, "^(/api/|/swr/api)")]
        public async Task PostApi(HttpContext context)
        {
            var responseValue = this.ProcessApiRequest(context);
            await context.Response.Send(responseValue);
        }

        async Task DefaultRequest(HttpContext context)
        {
            var responseValue = this.ProcessDefautRequest(context);
            await context.Response.Send(responseValue);
        }

        byte[] ProcessDefautRequest(HttpContext context)
        {
            var requestUrl = context.Request.Url.RawWithoutQuery;

            bool isStaticWebResources = false;

            if (requestUrl.StartsWith("/swr"))
            {
                isStaticWebResources = true;
                requestUrl = requestUrl.Replace("/swr", "");
            }

            byte[] responseValue = Encoding.Default.GetBytes(context.Request.Url.RawWithoutQuery); ///???

            if (requestUrl.Contains("?"))
            {
                requestUrl = requestUrl.Substring(0, requestUrl.IndexOf("?"));
            }

            // ------
            if (isStaticWebResources)
            {
                responseValue = this.ressourceLoader.ReadRessource("TestStaticWebResources" + requestUrl);
            }
            else
            {
                responseValue = this.ressourceLoader.ReadRessource("Frontend" + requestUrl);
            }

            string extension = requestUrl.Substring(requestUrl.LastIndexOf(".") + 1).ToLower();

            if (this.mimeTypeService.MimeTypeMapping.ContainsKey(extension))
            {
                context.Response.ContentType = this.mimeTypeService.MimeTypeMapping[extension];
            }

            context.Response.StatusCode = (int)HttpStatusCode.OK;

            context.Response.Headers.Add("Access-Control-Allow-Origin", "*");

            return responseValue;
        }

        private byte[] ProcessApiRequest(HttpContext context)
        {
            var originalListenerRequest = context.Request.ListenerContext.Request;

            bool isValidRequest = true;
            byte[] responseValue = Encoding.Default.GetBytes(context.Request.Url.RawWithoutQuery);

            string postData = string.Empty;
            using (var reader = new StreamReader(originalListenerRequest.InputStream, originalListenerRequest.ContentEncoding))
            {
                postData = reader.ReadToEnd();
            }

            var localAvailableRoutes = availableRoutesForWatson;

            DecodedRequestData requestData = decoderServiceForWatson.DecodeRequestData(context.Request.Url.RawWithoutQuery, postData);
            string requestName = context.Request.Url.RawWithQuery.Substring(1);

            if (requestName.StartsWith("swr"))
            {
                requestName = requestName.Replace("swr/", "");
            }

            if (requestName.IndexOf("?") > 0)
            {
                requestName = requestName.Substring(0, requestName.IndexOf("?"));
            }

            if (!localAvailableRoutes.ContainsKey(requestName))
            {
                context.Response.StatusCode = (int)HttpStatusCode.NotFound;
            }
            else
            {
                RouteMapping route = localAvailableRoutes[requestName];
                List<object> constructorParameters = new List<object>();

                if (route.ControllerType.GetConstructors().Length > 0)
                {
                    foreach (ParameterInfo parameter in route.ControllerType.GetConstructors()[0]
                                 .GetParameters())
                    {
                        constructorParameters.Add(Activator.CreateInstance(parameter.ParameterType));
                    }
                }

                object instance = constructorParameters.Count > 0
                            ? Activator.CreateInstance(route.ControllerType, constructorParameters.ToArray())
                            : Activator.CreateInstance(route.ControllerType);

                List<object> methodParams = new List<object>();

                try
                {
                    foreach (var routeParameter in route.Parameters)
                    {
                        if (routeParameter.ParameterType.ToString().Contains("Dictionary"))
                        {
                            methodParams.Add(requestData.RequestParameter);
                        }
                        else if (requestData.RequestParameter.ContainsKey(routeParameter.Name))
                        {
                            switch (routeParameter.ParameterType.ToString())
                            {
                                case "System.Int32":
                                    methodParams.Add(Convert.ToInt32(requestData.RequestParameter[routeParameter.Name]));
                                    break;
                                default:
                                    methodParams.Add(requestData.RequestParameter[routeParameter.Name]);
                                    break;

                            }
                        }
                        else
                        {
                            isValidRequest = false;
                            responseValue = Encoding.UTF8.GetBytes("error");
                        }
                    }

                    if (!route.AllowedMethods.Contains(originalListenerRequest.HttpMethod.ToUpper()))
                    {
                        isValidRequest = false;
                        responseValue = Encoding.UTF8.GetBytes("error");
                    }


                    if (isValidRequest)
                    {
                        var result = route.RouteMethod.Invoke(instance, methodParams.Count == 0 ? null : methodParams.ToArray());

                        if (result != null && result is Task)
                        {
                            var taskResult = result.GetType().GetProperty("Result").GetValue(result);
                            var resultValue = (taskResult).GetType().GetProperty("Value").GetValue(taskResult);

                            context.Response.ContentType = route.ResultContentType;

                            switch (route.ResultContentType)
                            {
                                case "application/json":
                                    responseValue = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(resultValue));
                                    context.Response.StatusCode = (int)HttpStatusCode.OK;
                                    break;

                                case "application/pdf":
                                    responseValue = (byte[])resultValue;
                                    context.Response.StatusCode = (int)HttpStatusCode.OK;
                                    break;
                                default:
                                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                                    break;
                            }

                        }
                    }
                    else
                    {
                        context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    }
                }
                catch (Exception ex)
                {
                    this.loggingService.LogConsole("error process api request - " + ex.Message + "\n" + ex.StackTrace);
                }
            }

            context.Response.Headers.Add("Access-Control-Allow-Origin", "*");

            return responseValue;
        }
    }
}
