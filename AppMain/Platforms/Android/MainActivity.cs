using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Views;
using AndroidX.Core.App;
using AndroidX.Core.Content;

namespace AppMain;

[Activity(
    Theme = "@style/Maui.SplashTheme",
    MainLauncher = true,
    ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density,
    ScreenOrientation = ScreenOrientation.Landscape)]
public class MainActivity : MauiAppCompatActivity
{
    static readonly int REQUEST_STORAGE = 0;
    protected override void OnCreate(Bundle bSavedInstanceState)
    {
        base.OnCreate(bSavedInstanceState);

        this.SetWindowLayout();

        if (ContextCompat.CheckSelfPermission(this, Android.Manifest.Permission.WriteExternalStorage) !=
                (int)Permission.Granted)
        {
            this.RequestPermissionWriteExternalStorage();
        }
    }

    // Force Fullscreen Android, call in OnCreate
    private void SetWindowLayout()
    {
        if (Window != null)
        {
            if (Build.VERSION.SdkInt >= BuildVersionCodes.R)
            {
#pragma warning disable CA1416

                IWindowInsetsController wicController = Window.InsetsController;


                this.Window.SetDecorFitsSystemWindows(false);
                this.Window.SetFlags(WindowManagerFlags.Fullscreen, WindowManagerFlags.Fullscreen);

                if (wicController != null)
                {
                    wicController.Hide(WindowInsets.Type.Ime());
                    wicController.Hide(WindowInsets.Type.NavigationBars());
                }
#pragma warning restore CA1416
            }
            else
            {
#pragma warning disable CS0618

                Window.SetFlags(WindowManagerFlags.Fullscreen, WindowManagerFlags.Fullscreen);

                Window.DecorView.SystemUiVisibility = (StatusBarVisibility)(SystemUiFlags.Fullscreen |
                                                                             SystemUiFlags.HideNavigation |
                                                                             SystemUiFlags.Immersive |
                                                                             SystemUiFlags.ImmersiveSticky |
                                                                             SystemUiFlags.LayoutHideNavigation |
                                                                             SystemUiFlags.LayoutStable |
                                                                             SystemUiFlags.LowProfile);
#pragma warning restore CS0618
            }
        }
    }

    //// implemented only for debug breakpoint
    public override void OnBackPressed(
        )
    {
        base.OnBackPressed();
    }

    //// implemented only for debug breakpoint
    protected override void OnPause()
    {
        base.OnPause();

    }

    private void RequestPermissionWriteExternalStorage()
    {
        if (ActivityCompat.ShouldShowRequestPermissionRationale(this, Android.Manifest.Permission.WriteExternalStorage))
        {
            ActivityCompat.RequestPermissions(this, new String[] { Android.Manifest.Permission.WriteExternalStorage }, REQUEST_STORAGE);
        }
        else
        {
            ActivityCompat.RequestPermissions(this, new String[] { Android.Manifest.Permission.WriteExternalStorage }, REQUEST_STORAGE);
        }
    }

    private void CheckPermissions()
    {

        var pExtStrg = ContextCompat.CheckSelfPermission(this, Android.Manifest.Permission.WriteExternalStorage);
        var pReadContact = ContextCompat.CheckSelfPermission(this, Android.Manifest.Permission.ReadContacts);

        if (pExtStrg != (int)Permission.Granted)
        {
            if (ActivityCompat.ShouldShowRequestPermissionRationale(this, Android.Manifest.Permission.WriteExternalStorage))
            {
                ActivityCompat.RequestPermissions(this, new String[] { Android.Manifest.Permission.WriteExternalStorage }, REQUEST_STORAGE);
            }
            else
            {
                ActivityCompat.RequestPermissions(this, new String[] { Android.Manifest.Permission.WriteExternalStorage }, REQUEST_STORAGE);
            }
        }
    }
}
