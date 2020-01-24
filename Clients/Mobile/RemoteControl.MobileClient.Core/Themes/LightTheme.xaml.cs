using JToolbox.XamarinForms.Themes;
using Xamarin.Forms;

namespace RemoteControl.MobileClient.Core.Themes
{
    public partial class LightTheme : ResourceDictionary, IThemeResourceDictionary
    {
        public LightTheme()
        {
            InitializeComponent();
        }

        public Color NotificationBarColor => (Color)this["NavigationBarColor"];
    }
}