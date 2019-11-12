using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using System;
using System.Threading.Tasks;

namespace RemoteControl.Server.Core.Services
{
    public class DialogsService : IDialogsService
    {
        private readonly MetroWindow window;

        public DialogsService(MetroWindow window)
        {
            this.window = window;
        }

        public Task ShowError(string message)
        {
            return window.ShowMessageAsync("Error", message);
        }

        public Task ShowException(Exception exc)
        {
            return window.ShowMessageAsync("Error", $"An exception occured: {exc.Message}");
        }

        public Task ShowInfo(string message)
        {
            return window.ShowMessageAsync("Information", message);
        }
    }
}