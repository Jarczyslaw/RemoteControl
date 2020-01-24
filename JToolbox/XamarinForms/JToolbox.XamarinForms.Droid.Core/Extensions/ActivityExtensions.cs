using Android.App;
using Android.Views;

namespace JToolbox.XamarinForms.Droid.Core.Extensions
{
    public static class ActivityExtensions
    {
        public static void SetLightStatusBar(this Activity activity, bool light)
        {
            if (light)
            {
                activity.Window.DecorView.SystemUiVisibility |= (StatusBarVisibility)SystemUiFlags.LightStatusBar;
            }
            else
            {
                activity.Window.DecorView.SystemUiVisibility &= ~(StatusBarVisibility)SystemUiFlags.LightStatusBar;
            }
        }
    }
}