namespace Backend.Webservice
{
    using Backend.Types;
    using Backend.Utilities;
    using WatsonWebserver;

    public class WebserviceHost : IWebserviceHost
    {
        public event Action<bool>? Connected;

        public int HttpPort { get; set; }

        public bool IsConnected { get; set; }

        public ISystemUtilities SystemUtilities { get; set; }

        public Dictionary<string, RouteMapping> AvailableRoutes { get; set; }

        private string _hostname = "127.0.0.1";
        private bool _ssl = false;
        private Server _server = null;

        private string _basicAuthorizationString;

        public IResourceLoader RessourceLoader { get; set; }
        

        public Dictionary<string, string> MimeTypeMapping = new Dictionary<string, string>()
            {
                { "gif", "image/gif" },
                { "jpeg", "image/jpeg" },
                { "jpg", "image/jpeg" },
                { "png", "image/png" },
                { "pdf", "application/pdf" },
                { "xml", "text/xml" },
                { "js", "application/javascript" },
                { "zip", "application/zip" },
                { "html", "text/html" },
                { "css", "text/css" },
                { "svg", "image/svg+xml" },
                { "woff", "application/font-woff" },
                { "woff2", "application/font-woff" },
                { "ttf", "application/x-font-ttf" },
            };

        private static string __basicAuthorizationString = string.Empty;
        private static string __httpUser = string.Empty;
        private static string __httpPass = string.Empty;
        private static Dictionary<string, RouteMapping> __availableRoutes;

        public WebserviceHost(IResourceLoader ressourceLoader, ISystemUtilities systemUtilities)
        {
            try
            {
                RessourceLoader = ressourceLoader;
                SystemUtilities = systemUtilities;

             
                _basicAuthorizationString = string.Empty;

                //AvailableRoutes = WebserviceUtilities.InitControllers();

               
            }
            catch (Exception e)
            {
                ////TODO: implement logservice
                Console.WriteLine(e.Message + "\n" + e.StackTrace);
            }

        }

        public void StartListening()
        {

        }

        public void StopListening()
        {

        }
    }
}
