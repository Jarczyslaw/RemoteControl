using JToolbox.XamarinForms.Themes;
using Xamarin.Forms;

namespace RemoteControl.MobileClient.Core.Themes
{
    public partial class LightTheme : ResourceDictionary, IThemeResourceDictionary
    {
        public LightTheme()
        {
            InitializeComponent();
            ThemeColorExtractor = new ThemeColorExtractor(this);
        }

        public ThemeColorExtractor ThemeColorExtractor { get; }
    }
}