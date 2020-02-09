using JToolbox.XamarinForms.Core.Base;
using JToolbox.XamarinForms.Core.Navigation.Exceptions;
using Prism.Navigation;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace JToolbox.XamarinForms.Core.Navigation
{
    public class NavService : INavService
    {
        public NavService()
        {
        }

        public NavService(INavigationService navigationService, INavMapper navMapper)
        {
            NavigationService = navigationService;
            NavMapper = navMapper;
        }

        public INavigationService NavigationService { get; set; }
        public INavMapper NavMapper { get; set; }

        private void CheckType(Type type, Type constraint)
        {
            if (type.IsAssignableFrom(constraint))
            {
                throw new InvalidTypeException(type, constraint);
            }
        }

        public Page GetCurrentPage()
        {
            var mainPage = Application.Current.MainPage;
            if (mainPage == null)
            {
                return null;
            }
            return mainPage.Navigation.NavigationStack.LastOrDefault();
        }

        public bool IsCurrentPage<T>()
            where T : PageBase
        {
            return IsCurrentPage(typeof(T));
        }

        public bool IsCurrentPage(Type pageType)
        {
            CheckType(pageType, typeof(PageBase));
            var currentPage = GetCurrentPage();
            if (currentPage == null)
            {
                return false;
            }
            return currentPage.GetType() == pageType;
        }

        public bool IsCurrentViewModel<T>()
            where T : ViewModelBase
        {
            return IsCurrentViewModel(typeof(T));
        }

        public bool IsCurrentViewModel(Type viewModelType)
        {
            CheckType(viewModelType, typeof(ViewModelBase));
            var targetPageType = NavMapper.GetPageForViewModel(viewModelType);
            return IsCurrentPage(targetPageType);
        }

        public bool IsPageOpened<T>()
            where T : PageBase
        {
            return IsPageOpened(typeof(T));
        }

        public bool IsPageOpened(Type pageType)
        {
            CheckType(pageType, typeof(PageBase));
            var mainPage = Application.Current.MainPage;
            if (mainPage == null)
            {
                return false;
            }
            return mainPage.Navigation.NavigationStack.Any(p => p.GetType() == pageType);
        }

        public bool IsViewModelOpened<T>()
            where T : ViewModelBase
        {
            return IsViewModelOpened(typeof(T));
        }

        public bool IsViewModelOpened(Type viewModelType)
        {
            CheckType(viewModelType, typeof(ViewModelBase));
            var targetPageType = NavMapper.GetPageForViewModel(viewModelType);
            return IsPageOpened(targetPageType);
        }

        public Task<INavigationResult> StartNavigationViewModel<T>(Parameters parameters = null)
            where T : ViewModelBase
        {
            return NavigateToViewModel(typeof(T), parameters, true);
        }

        public Task<INavigationResult> NavigateToViewModel<T>(Parameters parameters = null)
            where T : ViewModelBase
        {
            return NavigateToViewModel(typeof(T), parameters, false);
        }

        public Task<INavigationResult> NavigateToViewModel(Type viewModelType, Parameters parameters = null)
        {
            return NavigateToViewModel(viewModelType, parameters, false);
        }

        private Task<INavigationResult> NavigateToViewModel(Type viewModelType, Parameters parameters = null, bool isNavigationPage = false)
        {
            CheckType(viewModelType, typeof(ViewModelBase));
            if (NavMapper.ViewModelsMapping.TryGetValue(viewModelType, out var pageType))
            {
                var pageUri = pageType.Name;
                if (isNavigationPage)
                {
                    pageUri = $"NavigationPage/{pageUri}";
                }

                var navigationParams = parameters == null
                    ? new NavigationParameters() : parameters.CreateNavigationParameters();
                return NavigationService.NavigateAsync(pageUri, navigationParams);
            }
            else
            {
                throw new NoPageException(viewModelType.Name);
            }
        }

        public Task<INavigationResult> Close(Parameters parameters = null)
        {
            var navigationParams = parameters == null
                ? new NavigationParameters() : parameters.CreateNavigationParameters();
            return NavigationService.GoBackAsync(navigationParams);
        }

        public Task<INavigationResult> ReturnToRoot(Parameters parameters = null)
        {
            var navigationParams = parameters == null
                ? new NavigationParameters() : parameters.CreateNavigationParameters();
            return NavigationService.GoBackToRootAsync(navigationParams);
        }
    }
}