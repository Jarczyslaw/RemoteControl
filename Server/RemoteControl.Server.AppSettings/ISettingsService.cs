namespace RemoteControl.Server.AppSettings
{
    public interface ISettingsService
    {
        Settings Settings { get; set; }
        string SettingsPath { get; }

        void Load();

        void Save();
    }
}