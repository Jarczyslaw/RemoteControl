using System;
using System.Threading.Tasks;

namespace RemoteControl.Server.Core.Services
{
    public interface IShellDialogsService
    {
        Task ShowInfo(string message);

        Task ShowError(string message);

        Task ShowException(Exception exc);
        Task ShowException(string message, Exception exc);

        Task<bool> ShowYesNoQuestion(string question);
    }
}