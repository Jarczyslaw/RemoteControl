using JToolbox.XamarinForms.Core.Base;
using JToolbox.XamarinForms.Core.Navigation;
using Prism.Navigation;

namespace RemoteControl.MobileClient.Core.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public MainViewModel(INavService navService, INavigationService navigationService)
            : base(navService, navigationService)
        {
        }
    }
}