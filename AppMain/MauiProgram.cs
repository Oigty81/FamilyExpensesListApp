namespace AppMain;

using Backend;
using Backend.Types;
using Microsoft.Maui.LifecycleEvents;
using System.Reflection;

#if WINDOWS
    using Microsoft.UI;
    using Microsoft.UI.Windowing;
    using Windows.Graphics;
#endif

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        builder.ConfigureLifecycleEvents(appLifecycle => {
#if ANDROID
            appLifecycle.AddAndroid(android => android
                .OnStop((activity) =>
                    {
                        //Console.WriteLine("Android Stop");
                    })
                    .OnCreate((activity, bundle) =>
                    {
                        //Console.WriteLine("Android Create");
                    })
                    .OnBackPressed((activity) =>
                    {
                        //Console.WriteLine("Android OnBackPressed");
                        return false;
                    })
                );
#endif

#if WINDOWS
            appLifecycle.AddWindows(windows => windows
                .OnActivated((window, args) => 
                    {
                    Console.WriteLine("Windows activated");
                    })
                    .OnClosed((window, args) => 
                    {
                    Console.WriteLine("Windows closed");
                    })
                    .OnWindowCreated(win1 => 
                    {
                    IntPtr nativeWindowHandle = WinRT.Interop.WindowNative.GetWindowHandle(win1);
					WindowId win32WindowsId = Win32Interop.GetWindowIdFromWindow(nativeWindowHandle);
					AppWindow winuiAppWindow = AppWindow.GetFromWindowId(win32WindowsId);

					win1.ExtendsContentIntoTitleBar = false; // must be false or else you see some of the buttons
					winuiAppWindow.SetPresenter(AppWindowPresenterKind.FullScreen);
                                                    
                    })
                );
#endif

        });

        var appDirectory = string.Empty;

#if ANDROID
            appDirectory = FileSystem.AppDataDirectory;
#endif

#if WINDOWS
            string appPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase).Substring(6);
            appDirectory = appPath;

#endif

        OsSelectorOptions osSelector = OsSelectorOptions.Android;

#if WINDOWS
            osSelector = OsSelectorOptions.Windows;
#endif
#if ANDROID
            osSelector = OsSelectorOptions.Android;
#endif

#if IOS
            osSelector = OsSelectorOptions.Windows;
#endif

        BackendMain.StartUp("Web.Resources", new SystemUtilities(osSelector, appDirectory));

        return builder.Build();
    }
}
