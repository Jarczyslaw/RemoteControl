using JToolbox.XamarinForms.Core.Extensions;
using JToolbox.XamarinForms.Droid.Core.Extensions;
using JToolbox.XamarinForms.Themes;
using Plugin.CurrentActivity;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

namespace JToolbox.XamarinForms.Droid.Core
{
    public class StatusBarColorManager : IStatusBarColorManager
    {
        public void SetColor(Color color)
        {
            var activity = CrossCurrentActivity.Current.Activity as FormsAppCompatActivity;
            activity.SetStatusBarColor(color.ToAndroid());
            activity.SetLightStatusBar(color.IsLight());
        }
    }
}