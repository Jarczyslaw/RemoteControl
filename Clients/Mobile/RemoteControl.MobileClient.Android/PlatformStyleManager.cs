using JToolbox.XamarinForms.Themes;
using Plugin.CurrentActivity;
using RemoteControl.MobileClient.Core.Themes;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

namespace RemoteControl.MobileClient.Droid
{
    public class PlatformStyleManager : IPlatformStyleManager
    {
        public void SetPlatformStyle(IThemeResourceDictionary themeResourceDictionary)
        {
            int themeId = 0;
            if (themeResourceDictionary is LightTheme)
            {
                themeId = Resource.Style.MainTheme_Light;
            }
            else if (themeResourceDictionary is DarkTheme)
            {
                themeId = Resource.Style.MainTheme_Dark;
            }

            var activity = CrossCurrentActivity.Current.Activity as FormsAppCompatActivity;
            activity.SetTheme(themeId);
            activity.Recreate();
        }
    }
}