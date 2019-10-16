using System;

namespace JToolbox.XamarinForms.Core.Navigation.Exceptions
{
    public class NoViewModelException : Exception
    {
        public NoViewModelException(string message)
            : base(message)
        {
        }
    }
}