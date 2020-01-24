using JToolbox.XamarinForms.Core.Extensions;
using JToolbox.XamarinForms.Droid.Core.Extensions;
using JToolbox.XamarinForms.Themes;
using Plugin.CurrentActivity;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

namespace RemoteControl.MobileClient.Droid
{
    public class StatusBarColorManager : IStatusBarColorManager
    {
        public void SetColor(Color color)
        {
            var activity = CrossCurrentActivity.Current.Activity as MainActivity;
            activity.SetStatusBarColor(color.ToAndroid());
            activity.SetLightStatusBar(color.IsLight());
        }
    }
}