using JToolbox.XamarinForms.Core.Base;
using Prism.Navigation;
using System;

namespace JToolbox.XamarinForms.Core.Navigation
{
    public class Parameters
    {
        public static string SourceViewModelKey => nameof(SourceViewModelKey);
        public static string ValueKey => nameof(ValueKey);
        public static string CallbackKey => nameof(CallbackKey);

        public ViewModelBase SourceViewModel { get; set; }
        public object Value { get; set; }
        public Func<object> Callback { get; set; }

        public INavigationParameters CreateNavigationParameters()
        {
            return new NavigationParameters
            {
                { SourceViewModelKey, SourceViewModel },
                { ValueKey, Value },
                { CallbackKey, Callback },
            };
        }

        public static Parameters CreateFromNavigationParameters(INavigationParameters navigationParameters)
        {
            if (navigationParameters.ContainsKey(SourceViewModelKey)
                && navigationParameters.ContainsKey(ValueKey))
            {
                return new Parameters
                {
                    SourceViewModel = (ViewModelBase)navigationParameters[SourceViewModelKey],
                    Value = navigationParameters[ValueKey],
                    Callback = (Func<object>)navigationParameters[CallbackKey],
                };
            }
            return null;
        }
    }
}