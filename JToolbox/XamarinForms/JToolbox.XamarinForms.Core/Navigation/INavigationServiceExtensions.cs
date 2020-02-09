using JToolbox.XamarinForms.Core.Base;
using JToolbox.XamarinForms.Core.Navigation.Exceptions;
using Prism.Navigation;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace JToolbox.XamarinForms.Core.Navigation
{
    public static class INavigationServiceExtensions
    {
        private static void CheckType(Type type, Type constraint)
        {
            if (type.IsAssignableFrom(constraint))
            {
                throw new InvalidTypeException(type, constraint);
            }
        }

        public static Page GetCurrentPage(this INavigationService navigationService)
        {
            var mainPage = Application.Current.MainPage;
            if (mainPage == null)
            {
                return null;
            }
            return mainPage.Navigation.NavigationStack.LastOrDefault();
        }

        public static bool IsCurrentPage<T>(this INavigationService navigationService)
            where T : PageBase
        {
            return navigationService.IsCurrentPage(typeof(T));
        }

        public static bool IsCurrentPage(this INavigationService navigationService, Type pageType)
        {
            CheckType(pageType, typeof(PageBase));
            var currentPage = navigationService.GetCurrentPage();
            if (currentPage == null)
            {
                return false;
            }
            return currentPage.GetType() == pageType;
        }

        public static bool IsCurrentViewModel<T>(this INavigationService navigationService)
            where T : ViewModelBase
        {
            return navigationService.IsCurrentViewModel(typeof(T));
        }

        public static bool IsCurrentViewModel(this INavigationService navigationService, Type viewModelType)
        {
            CheckType(viewModelType, typeof(ViewModelBase));
            var targetPageType = NavigationMapper.Instance.GetPageForViewModel(viewModelType);
            return navigationService.IsCurrentPage(targetPageType);
        }

        public static bool IsPageOpened<T>(this INavigationService navigationService)
            where T : PageBase
        {
            return navigationService.IsPageOpened(typeof(T));
        }

        public static bool IsPageOpened(this INavigationService navigationService, Type pageType)
        {
            CheckType(pageType, typeof(PageBase));
            var mainPage = Application.Current.MainPage;
            if (mainPage == null)
            {
                return false;
            }
            return mainPage.Navigation.NavigationStack.Any(p => p.GetType() == pageType);
        }

        public static bool IsViewModelOpened<T>(this INavigationService navigationService)
            where T : ViewModelBase
        {
            return navigationService.IsViewModelOpened(typeof(T));
        }

        public static bool IsViewModelOpened(this INavigationService navigationService, Type viewModelType)
        {
            CheckType(viewModelType, typeof(ViewModelBase));
            var targetPageType = NavigationMapper.Instance.GetPageForViewModel(viewModelType);
            return navigationService.IsPageOpened(targetPageType);
        }

        public static Task<INavigationResult> StartNavigationViewModel<T>(this INavigationService navigationService, Parameters parameters = null)
            where T : ViewModelBase
        {
            return navigationService.NavigateToViewModel(typeof(T), parameters, true);
        }

        public static Task<INavigationResult> NavigateToViewModel<T>(this INavigationService navigationService, Parameters parameters = null)
            where T : ViewModelBase
        {
            return navigationService.NavigateToViewModel(typeof(T), parameters, false);
        }

        public static Task<INavigationResult> NavigateToViewModel(this INavigationService navigationService, Type viewModelType, Parameters parameters = null)
        {
            return navigationService.NavigateToViewModel(viewModelType, parameters, false);
        }

        private static Task<INavigationResult> NavigateToViewModel(this INavigationService navigationService, Type viewModelType, Parameters parameters = null, bool isNavigationPage = false)
        {
            CheckType(viewModelType, typeof(ViewModelBase));
            if (NavigationMapper.Instance.ViewModelsMapping.TryGetValue(viewModelType, out var pageType))
            {
                var pageUri = pageType.Name;
                if (isNavigationPage)
                {
                    pageUri = $"NavigationPage/{pageUri}";
                }

                var navigationParams = parameters == null
                    ? new NavigationParameters() : parameters.CreateNavigationParameters();
                return navigationService.NavigateAsync(pageUri, navigationParams);
            }
            else
            {
                throw new NoPageException(viewModelType.Name);
            }
        }

        public static Task<INavigationResult> Close(this INavigationService navigationService, Parameters parameters = null)
        {
            var navigationParams = parameters == null
                ? new NavigationParameters() : parameters.CreateNavigationParameters();
            return navigationService.GoBackAsync(navigationParams);
        }

        public static Task<INavigationResult> ReturnToRoot(this INavigationService navigationService, Parameters parameters = null)
        {
            var navigationParams = parameters == null
                ? new NavigationParameters() : parameters.CreateNavigationParameters();
            return navigationService.GoBackToRootAsync(navigationParams);
        }
    }
}