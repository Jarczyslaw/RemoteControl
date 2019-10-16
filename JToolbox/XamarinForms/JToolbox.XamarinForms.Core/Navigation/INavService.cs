using JToolbox.XamarinForms.Core.Base;
using Prism.Ioc;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace JToolbox.XamarinForms.Core.Navigation
{
    public interface INavService
    {
        Dictionary<Type, Type> ViewModelsMapping { get; }

        Task<INavigationResult> Close(INavigationService navigationService, Parameters parameters = null);

        Page GetCurrentPage();

        string GetPageName(Type pageType);

        bool IsCurrentPage(Type pageType);

        bool IsCurrentPage<T>() where T : PageBase;

        bool IsCurrentViewModel(Type viewModelType);

        bool IsCurrentViewModel<T>() where T : ViewModelBase;

        bool IsPageOpened(Type pageType);

        bool IsPageOpened<T>() where T : PageBase;

        bool IsValidViewName(string typeName);

        bool IsViewModelOpened(Type viewModelType);

        bool IsViewModelOpened<T>() where T : ViewModelBase;

        Task<INavigationResult> NavigateToViewModel(INavigationService navigationService, Type viewModelType, Parameters parameters = null);

        Task<INavigationResult> NavigateToViewModel<T>(INavigationService navigationService, Parameters parameters = null) where T : ViewModelBase;

        void Register(IContainerRegistry containerRegistry, Assembly assembly);

        void Register(IContainerRegistry containerRegistry, Type pageType, Type viewModelType);

        void Register<TPage, TViewModel>(IContainerRegistry containerRegistry)
            where TPage : PageBase
            where TViewModel : ViewModelBase;

        Task<INavigationResult> ReturnToRoot(INavigationService navigationService, Parameters parameters = null);

        Task<INavigationResult> StartNavigationViewModel<T>(INavigationService navigationService, Parameters parameters = null) where T : ViewModelBase;
    }
}