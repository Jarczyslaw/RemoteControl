using JToolbox.XamarinForms.Settings;

namespace RemoteControl.MobileClient.Core.Services
{
    public class AppSettings : ApplicationSettings, IAppSettings
    {
        public string Name
        {
            get => GetString(nameof(Name), string.Empty);
            set => AddString(nameof(Name), value);
        }

        public string RemoteAddress
        {
            get => GetString(nameof(RemoteAddress), string.Empty);
            set => AddString(nameof(RemoteAddress), value);
        }

        public int Port
        {
            get => GetInt(nameof(Port), 1);
            set => AddInt(nameof(Port), value);
        }
    }
}