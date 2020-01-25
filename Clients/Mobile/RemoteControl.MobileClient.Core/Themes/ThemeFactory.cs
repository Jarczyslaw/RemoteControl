using JToolbox.XamarinForms.Themes;

namespace RemoteControl.MobileClient.Core.Themes
{
    public class ThemeFactory
    {
        public IThemeResourceDictionary GetThemeResourceDictionary(Theme theme)
        {
            if (theme == Theme.Dark)
            {
                return new DarkTheme();
            }
            else if (theme == Theme.Light)
            {
                return new LightTheme();
            }
            else
            {
                return null;
            }
        }
    }
}