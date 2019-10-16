using System;

namespace JToolbox.XamarinForms.Core.Navigation.Exceptions
{
    public class InvalidTypeException : Exception
    {
        public InvalidTypeException(Type type, Type constraint)
            : base($"Invalid requested type. {type.Name} is not a subclass of {constraint.Name}")
        {
        }
    }
}