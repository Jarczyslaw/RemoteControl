using System;
using System.Threading.Tasks;

namespace RemoteControl.Server.Core.Services
{
    public interface IDialogsService
    {
        Task ShowInfo(string message);

        Task ShowError(string message);

        Task ShowException(Exception exc);
    }
}