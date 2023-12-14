using Backend;

namespace AppMain;

public partial class MainPage : ContentPage
{
    ////TODO: move this options in config later
    private bool shouldOpenBrowser = false; ////set true to open frontend in browser optional (for debugging etc.)
    private bool useTestStaticWebResouces = false; //// set true to use "TestStaticWebResources" (for test Backend instead) "Frontend" (SPA Bundle)

    public MainPage()
    {
        this.InitializeComponent();

        //// enable maui WebView (Android) enable debugging with chrome://inspect/  (DEBUG)
#if ANDROID && DEBUG
            Android.Webkit.WebView.SetWebContentsDebuggingEnabled(true);
#endif

        // disable dev tools on windows (RELEASE)
#if WINDOWS && !DEBUG
            webView.Navigating += Wv_NavigatingForDisableDevTools;
#endif

        string staticWebResouceUrlPart = string.Empty;  // set to "/swr"

        if(this.useTestStaticWebResouces)
        {
            staticWebResouceUrlPart = "/swr";
        }


        this.LoadedUrl = "http://127.0.0.1:" + BackendMain.WebServiceHostInstance.HttpPort + staticWebResouceUrlPart + "/index.html";

        this.webView.Source = new HtmlWebViewSource
        {
            Html = @"<HTML><BODY><P style=""text-align: center; font-size: 48px;""> .... Loading WebApp to WebView ....</P></BODY></HTML>",
        };

        this.SetWebAppToWebview();
    }

    public string LoadedUrl { get; set; }

    private async void SetWebAppToWebview()
    {
        await Task.Delay(1100);

        if (!BackendMain.WebServiceHostInstance.IsConnected)
        {
            this.webView.IsVisible = false;
            this.errorMessage.IsVisible = true;
        }

        this.webView.Source = this.LoadedUrl;

        if (this.shouldOpenBrowser)
        {
            await Task.Delay(800);

            await Launcher.OpenAsync(this.LoadedUrl);

            await Task.Delay(400);

            string file = Path.Combine(Path.GetTempPath(), "appurl.html");

            string content = string.Empty;

            content += $@"<HTML><BODY>";

            content += $@"<p style = ""text-align: left; font-size: 24px; "" >{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff") + " | " + this.LoadedUrl}</p><br /> <hr />";
            content += $@"<a href=""{this.LoadedUrl}"" style = ""text-align: left; font-size: 16px; "" >{this.LoadedUrl}</a>";
            content += $@"</BODY ></HTML>";

            System.IO.File.WriteAllText(file, content);
            await Launcher.Default.OpenAsync(new OpenFileRequest("APP - URL", new ReadOnlyFile(file)));
        }
    }

#if WINDOWS
        // disable dev tools on windows (hack)
        private void Wv_NavigatingForDisableDevTools(object sender, WebNavigatingEventArgs e)
        {
            WebView webview = (WebView)sender;
            Microsoft.Web.WebView2.Core.CoreWebView2 coreWebView2 = (webview.Handler.PlatformView as Microsoft.UI.Xaml.Controls.WebView2).CoreWebView2;
            coreWebView2.Settings.AreDevToolsEnabled = false;
            webview.Navigating -= Wv_NavigatingForDisableDevTools;
        }
#endif
}
