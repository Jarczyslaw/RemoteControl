using Xamarin.Forms;

namespace JToolbox.XamarinForms.Themes
{
    public interface IThemeManager
    {
        void SetTheme<T>() where T : ResourceDictionary, IThemeResourceDictionary;

        void SetTheme(ResourceDictionary resourceDictionary);
    }
}