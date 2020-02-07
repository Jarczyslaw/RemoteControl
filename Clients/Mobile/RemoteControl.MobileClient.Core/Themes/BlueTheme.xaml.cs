using JToolbox.XamarinForms.Themes;
using Xamarin.Forms;

namespace RemoteControl.MobileClient.Core.Themes
{
    public partial class BlueTheme : ResourceDictionary, IThemeResourceDictionary
    {
        public BlueTheme()
        {
            InitializeComponent();
            ThemeColorExtractor = new ThemeColorExtractor(this);
        }

        public ThemeColorExtractor ThemeColorExtractor { get; }
    }
}