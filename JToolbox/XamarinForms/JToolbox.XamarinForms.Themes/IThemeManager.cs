using Xamarin.Forms;

namespace JToolbox.XamarinForms.Themes
{
    public interface IThemeManager
    {
        event ThemeChanged OnThemeChanged;
        void SetTheme<T>() where T : ResourceDictionary, IThemeResourceDictionary;

        void SetTheme(ResourceDictionary resourceDictionary);
    }
}