using JToolbox.XamarinForms.Core.Base;
using Xamarin.Forms;

namespace RemoteControl.MobileClient.Core.Views
{
    public partial class SettingsPage : PageBase
    {
        public SettingsPage()
        {
            InitializeComponent();
            NavigationPage.SetHasBackButton(this, true);
        }
    }
}