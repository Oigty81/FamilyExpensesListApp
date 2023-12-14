namespace Backend.Webservice
{
    public interface IMimeTypeService
    {
        Dictionary<string, string> MimeTypeMapping { get; }
    }
}