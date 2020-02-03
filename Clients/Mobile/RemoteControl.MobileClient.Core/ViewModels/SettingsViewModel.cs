using JToolbox.XamarinForms.Core.Base;
using JToolbox.XamarinForms.Core.Navigation;
using Prism.Navigation;

namespace RemoteControl.MobileClient.Core.ViewModels
{
    public class SettingsViewModel : ViewModelBase
    {
        public SettingsViewModel(INavService navService, INavigationService navigationService)
            : base(navService, navigationService)
        {
        }
    }
}