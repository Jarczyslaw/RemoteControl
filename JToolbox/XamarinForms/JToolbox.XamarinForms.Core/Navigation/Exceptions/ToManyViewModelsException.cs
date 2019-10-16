using System;

namespace JToolbox.XamarinForms.Core.Navigation.Exceptions
{
    public class ToManyViewModelsException : Exception
    {
        public ToManyViewModelsException(string message)
            : base(message)
        {
        }
    }
}