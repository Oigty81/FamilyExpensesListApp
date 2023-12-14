namespace Backend.Webservice
{
    using Backend.Utilities;

    public interface IWebserviceHost
    {
        public event Action<bool>? Connected;

        public int HttpPort { get; }

        public bool IsConnected { get; }

        public void StartListening();

        public void StopListening();
    }
    
}
