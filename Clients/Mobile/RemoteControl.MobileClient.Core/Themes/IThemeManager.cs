using Xamarin.Forms;

namespace RemoteControl.MobileClient.Core.Themes
{
    public interface IThemeManager
    {
        void SetTheme<T>() where T : ResourceDictionary;
    }
}