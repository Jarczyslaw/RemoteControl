using JToolbox.XamarinForms.Settings;
using Xamarin.Essentials;

namespace RemoteControl.MobileClient.Core.Services
{
    public class AppSettings : ApplicationSettings, IAppSettings
    {
        public string Name
        {
            get => GetString(nameof(Name), DeviceInfo.Name);
            set => AddString(nameof(Name), value);
        }

        public string RemoteAddress
        {
            get => GetString(nameof(RemoteAddress), string.Empty);
            set => AddString(nameof(RemoteAddress), value);
        }

        public int Port
        {
            get => GetInt(nameof(Port), 9977);
            set => AddInt(nameof(Port), value);
        }

        public string Validate()
        {
            if (string.IsNullOrEmpty(RemoteAddress))
            {
                return "Invalid remote address";
            }

            return null;
        }
    }
}