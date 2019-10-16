namespace JToolbox.XamarinForms.Core.Abstraction
{
    public interface IAppCore
    {
        string DeviceId { get; }
        string LogPath { get; }
        void Kill();
    }
}