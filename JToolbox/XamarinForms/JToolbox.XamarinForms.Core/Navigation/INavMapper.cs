using JToolbox.XamarinForms.Core.Base;
using Prism.Ioc;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace JToolbox.XamarinForms.Core.Navigation
{
    public interface INavMapper
    {
        Dictionary<Type, Type> ViewModelsMapping { get; }
        string ViewModelSuffix { get; }
        List<string> ViewsSuffixes { get; }

        string GetPageName(Type pageType);

        bool IsValidViewName(string typeName);

        void Register(IContainerRegistry containerRegistry, Assembly assembly);

        void Register(IContainerRegistry containerRegistry, Type pageType, Type viewModelType);

        void Register<TPage, TViewModel>(IContainerRegistry containerRegistry)
            where TPage : PageBase
            where TViewModel : ViewModelBase;

        Type GetPageForViewModel(Type viewModelType);
    }
}