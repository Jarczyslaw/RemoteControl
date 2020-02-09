using JToolbox.XamarinForms.Core.Base;
using Prism.Commands;
using Prism.Navigation;

namespace RemoteControl.MobileClient.Core.ViewModels
{
    public class SettingsViewModel : ViewModelBase
    {
        public SettingsViewModel(INavigationService navigationService)
            : base(navigationService)
        {
        }

        public DelegateCommand SaveCommand => new DelegateCommand(async () =>
        {
        });
    }
}