namespace Backend.Utilities
{
    public class LoggingService : ILoggingService
    {
        public void LogConsole(string message)
        {
            Console.WriteLine(message);
        }
    }
}
