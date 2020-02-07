using JToolbox.XamarinForms.Themes;

namespace RemoteControl.MobileClient.Core.Themes
{
    public class ThemeFactory
    {
        public IThemeResourceDictionary GetThemeResourceDictionary(Theme theme)
        {
            if (theme == Theme.Blue)
            {
                return new BlueTheme();
            }
            else if (theme == Theme.Gray)
            {
                return new GrayTheme();
            }
            else
            {
                return null;
            }
        }
    }
}