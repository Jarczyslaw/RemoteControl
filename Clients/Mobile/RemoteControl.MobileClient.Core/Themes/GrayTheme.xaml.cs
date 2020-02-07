using JToolbox.XamarinForms.Themes;
using Xamarin.Forms;

namespace RemoteControl.MobileClient.Core.Themes
{
    public partial class GrayTheme : ResourceDictionary, IThemeResourceDictionary
    {
        public GrayTheme()
        {
            InitializeComponent();
            ThemeColorExtractor = new ThemeColorExtractor(this);
        }

        public ThemeColorExtractor ThemeColorExtractor { get; }
    }
}