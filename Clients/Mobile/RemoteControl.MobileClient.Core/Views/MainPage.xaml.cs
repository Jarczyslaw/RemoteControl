using JToolbox.XamarinForms.Themes;
using Xamarin.Forms;

namespace RemoteControl.MobileClient.Core.Views
{
    public partial class MainPage : ContentPage
    {
        public MainPage(IThemeManager themeManager)
        {
            InitializeComponent();

            themeManager.OnThemeChanged += t =>
            {
                fisSettings.Color = t.ThemeColorExtractor.PrimaryTextColor;
            };
        }
    }
}