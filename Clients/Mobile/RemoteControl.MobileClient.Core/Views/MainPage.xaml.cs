using JToolbox.XamarinForms.Themes;
using JToolbox.XamarinForms.UI;
using Xamarin.Forms;

namespace RemoteControl.MobileClient.Core.Views
{
    public partial class MainPage : ContentPage
    {
        private readonly ToolbarItemTemplates toolbarItemTemplates = new ToolbarItemTemplates();

        public MainPage(IThemeManager themeManager)
        {
            InitializeComponent();
            InitializeToolbarItems();

            themeManager.OnThemeChanged += _ => InitializeToolbarItems();
        }

        private void InitializeToolbarItems()
        {
            var color = (Color)Application.Current.Resources["PrimaryTextColor"];
            var fontFamily = ((OnPlatform<string>)Application.Current.Resources["FontAwesomeSolid"])
                .Platforms[0].Value.ToString();

            toolbarItemTemplates.Clear();
            toolbarItemTemplates.Add(new ToolbarItemTemplate
            {
                Color = color,
                FontFamily = fontFamily,
                Glyph = "\uf085",
                CommandBinding = "SettingsCommand"
            });
            toolbarItemTemplates.GenerateToolbarItems(ToolbarItems);
        }
    }
}