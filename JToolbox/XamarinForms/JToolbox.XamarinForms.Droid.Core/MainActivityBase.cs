using Acr.UserDialogs;
using Android.OS;
using Android.Runtime;
using Plugin.Permissions;

namespace JToolbox.XamarinForms.Droid.Core
{
    public class MainActivityBase : Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected void Initialize(Bundle bundle)
        {
            Xamarin.Forms.Forms.Init(this, bundle);
            Xamarin.Essentials.Platform.Init(this, bundle);
            Plugin.CurrentActivity.CrossCurrentActivity.Current.Init(this, bundle);
            UserDialogs.Init(this);
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}