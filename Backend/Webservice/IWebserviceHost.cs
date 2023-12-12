namespace Backend.Webservice
{
    using Backend.Utilities;

    public interface IWebserviceHost
    {
        public event Action<bool>? Connected;

        public int HttpPort { get; set; }

        public bool IsConnected { get; set; }

        public ISystemUtilities SystemUtilities { get; set; }

        public void StartListening();

        public void StopListening();
    }
    
}
