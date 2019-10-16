using Android.App;
using Android.OS;
using JToolbox.XamarinForms.Core.Abstraction;
using System;
using System.IO;
using Xamarin.Essentials;
using static Android.Provider.Settings;

namespace JToolbox.XamarinForms.Droid.Core
{
    public class AppCore : IAppCore
    {
        private readonly IPaths paths;
        private string deviceId;

        public AppCore(IPaths paths)
        {
            this.paths = paths;
        }

        public string DeviceId
        {
            get
            {
                if (deviceId == null)
                {
                    var id = Build.Serial;
                    if (string.IsNullOrWhiteSpace(id) || id == Build.Unknown || id == "0")
                    {
                        try
                        {
                            var context = Application.Context;
                            id = Secure.GetString(context.ContentResolver, Secure.AndroidId);
                        }
                        catch (Exception ex)
                        {
                            Android.Util.Log.Warn("DeviceInfo", "Unable to get id: " + ex.ToString());
                        }
                    }
                    deviceId = id;
                }
                return deviceId;
            }
        }

        public string LogPath => Path.Combine(paths.PublicExternalFolder, AppInfo.Name);

        public void Kill()
        {
            Process.KillProcess(Process.MyPid());
        }
    }
}