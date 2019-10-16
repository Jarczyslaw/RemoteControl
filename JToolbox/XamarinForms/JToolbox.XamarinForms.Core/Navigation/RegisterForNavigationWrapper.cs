using Prism.Ioc;
using System;
using System.Reflection;
using Xamarin.Forms;

namespace JToolbox.XamarinForms.Core.Navigation
{
    public class RegisterForNavigationWrapper
    {
        private readonly MethodInfo register = typeof(RegisterForNavigationWrapper).GetMethod(nameof(RegisterForNavigation), new[] { typeof(IContainerRegistry) });

        public void RegisterForNavigation<TView, TViewModel>(IContainerRegistry containerRegistry)
            where TView : Page
            where TViewModel : class
        {
            containerRegistry.RegisterForNavigation<TView, TViewModel>();
        }

        public void RegisterTypes(IContainerRegistry containerRegistry, Type page, Type viewModel)
        {
            var methodInfo = register.MakeGenericMethod(page, viewModel);
            methodInfo.Invoke(this, new object[] { containerRegistry });
        }
    }
}