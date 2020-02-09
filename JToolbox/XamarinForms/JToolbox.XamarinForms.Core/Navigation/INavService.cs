using JToolbox.XamarinForms.Core.Base;
using Prism.Navigation;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace JToolbox.XamarinForms.Core.Navigation
{
    public interface INavService
    {
        INavigationService NavigationService { get; set; }
        INavMapper NavMapper { get; set; }

        Task<INavigationResult> Close(Parameters parameters = null);
        Page GetCurrentPage();
        bool IsCurrentPage(Type pageType);
        bool IsCurrentPage<T>() where T : PageBase;
        bool IsCurrentViewModel(Type viewModelType);
        bool IsCurrentViewModel<T>() where T : ViewModelBase;
        bool IsPageOpened(Type pageType);
        bool IsPageOpened<T>() where T : PageBase;
        bool IsViewModelOpened(Type viewModelType);
        bool IsViewModelOpened<T>() where T : ViewModelBase;
        Task<INavigationResult> NavigateToViewModel(Type viewModelType, Parameters parameters = null);
        Task<INavigationResult> NavigateToViewModel<T>(Parameters parameters = null) where T : ViewModelBase;
        Task<INavigationResult> ReturnToRoot(Parameters parameters = null);
        Task<INavigationResult> StartNavigationViewModel<T>(Parameters parameters = null) where T : ViewModelBase;
    }
}