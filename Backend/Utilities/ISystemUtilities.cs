namespace Backend.Utilities
{
    using Backend.Types;

    public interface ISystemUtilities
    {
        public OsSelectorOptions OsSelector { get; }

        public string AppDirectory { get; }

        public int GetBatteryState();

        public void MinimizeApplication();

        public void QuitApplication();
    }
}
