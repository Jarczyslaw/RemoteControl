using JToolbox.XamarinForms.Themes;
using Xamarin.Forms;

namespace RemoteControl.MobileClient.Core.Themes
{
    public partial class DarkTheme : ResourceDictionary, IThemeResourceDictionary
    {
        public DarkTheme()
        {
            InitializeComponent();
        }

        public Color NotificationBarColor => (Color)this["NavigationBarColor"];
    }
}