using Xamarin.Forms;

namespace JToolbox.XamarinForms.Themes
{
    public interface IThemeManager
    {
        event ThemeChanged OnThemeChanged;

        IThemeResourceDictionary CurrentTheme { get; }

        void SetTheme<T>() where T : ResourceDictionary, IThemeResourceDictionary;

        void SetTheme(ResourceDictionary resourceDictionary);

        void ReloadTheme();
    }
}