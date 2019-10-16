using Acr.UserDialogs;
using System;
using System.Threading.Tasks;

namespace JToolbox.XamarinForms.Dialogs
{
    public interface IDialogsService
    {
        IUserDialogs UserDialogs { get; }

        Task<bool> QuestionYesNo(string message, string title = "Question");

        Task Warning(string message);

        Task Information(string message);

        Task Error(string message);

        Task Error(Exception exc, string message);

        void Toast(string message, TimeSpan? duration = null);

        Task ShowLoading(string message, Func<Task> loadingAction, Action cancelAction = null);

        Task Busy(string message, Func<Task> busyAction);
    }
}