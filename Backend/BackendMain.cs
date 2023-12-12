namespace Backend
{
    using Backend.Utilities;
    using Backend.Webservice;

    public class BackendMain
    {
        public static IWebserviceHost? WebServiceHostInstance { get; private set; }

        public static ISystemUtilities? SystemUtilitiesInstance { get; private set; }

        public static IResourceLoader? RessourceLoader { get; private set; }

        public static void StartUp(IResourceLoader ressourceLoader, ISystemUtilities systemUtilities)
        {
            IWebserviceHost host = new WebserviceHost(ressourceLoader, systemUtilities);

            WebServiceHostInstance = host;
            SystemUtilitiesInstance = systemUtilities;
            RessourceLoader = ressourceLoader;

            host.StartListening();
        }
    }
}
