using JToolbox.XamarinForms.Core.Base;
using JToolbox.XamarinForms.Core.Navigation.Exceptions;
using Prism.Ioc;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace JToolbox.XamarinForms.Core.Navigation
{
    public class NavService : INavService
    {
        private readonly List<string> viewsSuffixes = new List<string>
        {
            "View",
            "Page",
            "Window"
        };

        private readonly string viewModelSuffix = "ViewModel";
        private readonly RegisterForNavigationWrapper wrapper = new RegisterForNavigationWrapper();

        public Dictionary<Type, Type> ViewModelsMapping { get; } = new Dictionary<Type, Type>();

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
            var targetPageType = GetPageForViewModel(viewModelType);
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
            var targetPageType = GetPageForViewModel(viewModelType);
            return IsPageOpened(targetPageType);
        }

        public Task<INavigationResult> StartNavigationViewModel<T>(INavigationService navigationService, Parameters parameters = null)
            where T : ViewModelBase
        {
            return NavigateToViewModel(navigationService, typeof(T), parameters, true);
        }

        public Task<INavigationResult> NavigateToViewModel<T>(INavigationService navigationService, Parameters parameters = null)
            where T : ViewModelBase
        {
            return NavigateToViewModel(navigationService, typeof(T), parameters, false);
        }

        public Task<INavigationResult> NavigateToViewModel(INavigationService navigationService, Type viewModelType, Parameters parameters = null)
        {
            return NavigateToViewModel(navigationService, viewModelType, parameters, false);
        }

        private Task<INavigationResult> NavigateToViewModel(INavigationService navigationService, Type viewModelType, Parameters parameters = null, bool isNavigationPage = false)
        {
            CheckType(viewModelType, typeof(ViewModelBase));
            if (ViewModelsMapping.TryGetValue(viewModelType, out var pageType))
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

        public Task<INavigationResult> Close(INavigationService navigationService, Parameters parameters = null)
        {
            var navigationParams = parameters == null
                ? new NavigationParameters() : parameters.CreateNavigationParameters();
            return navigationService.GoBackAsync(navigationParams);
        }

        public Task<INavigationResult> ReturnToRoot(INavigationService navigationService, Parameters parameters = null)
        {
            var navigationParams = parameters == null
                ? new NavigationParameters() : parameters.CreateNavigationParameters();
            return navigationService.GoBackToRootAsync(navigationParams);
        }

        private Type GetPageForViewModel(Type viewModelType)
        {
            CheckType(viewModelType, typeof(ViewModelBase));
            if (ViewModelsMapping.TryGetValue(viewModelType, out var type))
            {
                return type;
            }
            throw new NoPageException(viewModelType.Name);
        }

        public void Register<TPage, TViewModel>(IContainerRegistry containerRegistry)
            where TPage : PageBase
            where TViewModel : ViewModelBase
        {
            Register(containerRegistry, typeof(TPage), typeof(TViewModel));
        }

        public void Register(IContainerRegistry containerRegistry, Type pageType, Type viewModelType)
        {
            CheckType(pageType, typeof(PageBase));
            CheckType(viewModelType, typeof(ViewModelBase));
            wrapper.RegisterTypes(containerRegistry, pageType, viewModelType);
            ViewModelsMapping.Add(viewModelType, pageType);
        }

        public void Register(IContainerRegistry containerRegistry, Assembly assembly)
        {
            var types = assembly.GetTypes();
            var pages = types.Where(t => IsValidViewName(t.Name) && t.IsSubclassOf(typeof(Page)));
            var viewModels = types.Where(t => t.Name.EndsWith(viewModelSuffix));
            foreach (var page in pages)
            {
                var pageName = GetPageName(page);
                var targetViewModelName = pageName + viewModelSuffix;
                var pageViewModels = viewModels.Where(v => v.Name == targetViewModelName);
                if (!pageViewModels.Any())
                {
                    throw new NoViewModelException($"No view model found for: {page.Name}");
                }
                else if (pageViewModels.Count() > 1)
                {
                    var invalidViewModels = string.Join(",", pageViewModels.Select(p => p.FullName).ToArray());
                    throw new ToManyViewModelsException($"More than one view model found for: {page.Name}. Found view models: {invalidViewModels}");
                }
                else
                {
                    var viewModel = pageViewModels.First();
                    wrapper.RegisterTypes(containerRegistry, page, viewModel);
                    ViewModelsMapping.Add(viewModel, page);
                }
            }
        }

        public bool IsValidViewName(string typeName)
        {
            foreach (var suffix in viewsSuffixes)
            {
                if (typeName.EndsWith(suffix))
                {
                    return true;
                }
            }
            return false;
        }

        public string GetPageName(Type pageType)
        {
            CheckType(pageType, typeof(PageBase));
            foreach (var suffix in viewsSuffixes)
            {
                var tempName = pageType.Name.Replace(suffix, string.Empty);
                if (tempName.Length != pageType.Name.Length)
                {
                    return tempName;
                }
            }
            throw new Exception($"{pageType.Name} is not a valid page name");
        }
    }
}