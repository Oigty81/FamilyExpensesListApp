namespace Backend
{
    using Backend.Utilities;
    using Backend.Webservice;

    public class BackendMain
    {
        public static IWebserviceHost? WebServiceHostInstance { get; private set; }

        public static ISystemUtilities? SystemUtilitiesInstance { get; private set; }

        public static IResourceLoader? RessourceLoader { get; private set; }

        public static void StartUp(string webResourcesProject, ISystemUtilities systemUtilities)
        {
            ILoggingService loggingService = new LoggingService();
            IResourceLoader ressourceLoader = new DllResourceLoader(loggingService, webResourcesProject);
            IMimeTypeService mimeTypeService = new MimeTypeService();
            IControllerService controllerService = new ControllerService();
            IDecoderService decoderService = new DecoderService(loggingService);

            IWebserviceHost host = new WebserviceHost(
                loggingService,
                ressourceLoader,
                mimeTypeService,
                controllerService,
                decoderService,
                systemUtilities);

            WebServiceHostInstance = host;
            SystemUtilitiesInstance = systemUtilities;
            RessourceLoader = ressourceLoader;

            host.StartListening();
        }
    }
}
