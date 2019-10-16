using Acr.UserDialogs;
using System;

namespace JToolbox.XamarinForms.Dialogs
{
    public class BusyIndicator : IDisposable
    {
        public BusyIndicator(string message)
        {
            if (string.IsNullOrEmpty(message))
            {
                message = "Please wait...";
            }

            UserDialogs.Instance.ShowLoading(message, MaskType.Gradient);
        }

        public void Dispose()
        {
            UserDialogs.Instance.HideLoading();
        }
    }
}