using JToolbox.XamarinForms.Core.Abstraction;
using JToolbox.XamarinForms.Droid.Core;
using Prism;
using Prism.Ioc;

namespace RemoteControl.MobileClient.Droid
{
    public class AndroidInitializer : IPlatformInitializer
    {
        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IPaths, Paths>();
            containerRegistry.RegisterSingleton<IAppCore, AppCore>();
        }
    }
}