namespace Backend.Webservice
{
    public class MimeTypeService : IMimeTypeService
    {
        public MimeTypeService()
        {
            this.MimeTypeMapping = new Dictionary<string, string>()
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
        }

        public Dictionary<string, string> MimeTypeMapping { get; }
    }
}
