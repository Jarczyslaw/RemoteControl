namespace RemoteControl.MobileClient.Core.Services
{
    public interface IAppSettings
    {
        string Name { get; set; }
        int Port { get; set; }
        string RemoteAddress { get; set; }
    }
}