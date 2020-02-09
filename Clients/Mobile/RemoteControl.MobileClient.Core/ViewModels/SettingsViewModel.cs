using JToolbox.XamarinForms.Core.Base;
using JToolbox.XamarinForms.Core.Navigation;
using Prism.Commands;

namespace RemoteControl.MobileClient.Core.ViewModels
{
    public class SettingsViewModel : ViewModelBase
    {
        public SettingsViewModel(INavService navService)
            : base(navService)
        {
        }

        public DelegateCommand SaveCommand => new DelegateCommand(async () =>
        {
        });
    }
}