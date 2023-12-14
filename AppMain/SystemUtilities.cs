namespace AppMain
{
    using System.Diagnostics;
    using Backend;
    using Backend.Types;
    using Backend.Utilities;

#if WINDOWS
    using Microsoft.UI;
    using Microsoft.UI.Windowing;
    using Windows.Graphics;
#endif

    public class SystemUtilities : ISystemUtilities
    {
        public SystemUtilities(OsSelectorOptions os, string appDirectory)
        {
            this.OsSelector = os;
            this.AppDirectory = appDirectory;
        }
        public OsSelectorOptions OsSelector { get; }

        public string AppDirectory { get; }

        public int GetBatteryState()
        {
            return (int)(Battery.ChargeLevel * 100.0);
        }

        public void MinimizeApplication()
        {
#if WINDOWS
            var mauiWindow = App.Current.Windows.First();
            var nativeWindow = mauiWindow.Handler.PlatformView;
            IntPtr windowHandle = WinRT.Interop.WindowNative.GetWindowHandle(nativeWindow);

            PInvoke.User32.ShowWindow(windowHandle, PInvoke.User32.WindowShowStyle.SW_MINIMIZE);
#endif
#if ANDROID
            var currentActivity = (MainActivity) Microsoft.Maui.ApplicationModel.Platform.CurrentActivity;
            currentActivity.MoveTaskToBack(false);         
#endif
        }

        public void QuitApplication()
        {
            try
            {
                Microsoft.Maui.ApplicationModel.MainThread.BeginInvokeOnMainThread(() =>
                {
                    Application.Current.Quit();
                });

                BackendMain.WebServiceHostInstance.StopListening();
            }
            catch (Exception e)
            {
                Debug.WriteLine("..." + e.Message);
            }
        }

    }
}
