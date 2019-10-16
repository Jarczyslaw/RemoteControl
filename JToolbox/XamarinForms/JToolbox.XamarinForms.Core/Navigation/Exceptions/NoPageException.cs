using System;

namespace JToolbox.XamarinForms.Core.Navigation.Exceptions
{
    public class NoPageException : Exception
    {
        public NoPageException(string viewModelName)
            : base($"No page for {viewModelName} found")
        {
        }
    }
}