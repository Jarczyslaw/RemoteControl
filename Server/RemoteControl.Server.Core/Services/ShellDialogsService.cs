using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using RemoteControl.Server.Core.Views;
using System;
using System.Threading.Tasks;

namespace RemoteControl.Server.Core.Services
{
    public class ShellDialogsService : IShellDialogsService
    {
        private readonly MetroWindow window;

        public ShellDialogsService()
        {
            window = ShellWindow.Instance;
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

        public async Task<bool> ShowYesNoQuestion(string question)
        {
            var result = await window.ShowMessageAsync("Question", question, MessageDialogStyle.AffirmativeAndNegative);
            return result == MessageDialogResult.Affirmative;
        }
    }
}