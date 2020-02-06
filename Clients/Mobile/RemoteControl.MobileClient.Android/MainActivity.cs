using Android.App;
using Android.Content.PM;
using Android.OS;
using JToolbox.XamarinForms.Droid.Core;
using RemoteControl.MobileClient.Core;

namespace RemoteControl.MobileClient.Droid
{
    [Activity(Label = "RemoteControlClient", Icon = "@mipmap/ic_launcher", Theme = "@style/Splash", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : MainActivityBase
    {
        private readonly AppGlobalExceptionHandler globalExceptionHandler = new AppGlobalExceptionHandler();

        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;
            RequestedOrientation = ScreenOrientation.Portrait;
            SetTheme(Resource.Style.MainTheme);

            base.OnCreate(bundle);

            globalExceptionHandler.Attach();
            Initialize(bundle);

            LoadApplication(new App(new AndroidInitializer()));
        }
    }
}