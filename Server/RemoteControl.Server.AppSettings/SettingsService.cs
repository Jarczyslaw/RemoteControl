using JToolbox.Serializers;
using System;
using System.IO;

namespace RemoteControl.Server.AppSettings
{
    public class SettingsService : ISettingsService
    {
        private readonly SerializerJson serializerJson = new SerializerJson();

        public string SettingsPath => Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "settings.set");

        public Settings Settings { get; set; }

        public void Load()
        {
            Settings = serializerJson.FromFile<Settings>(SettingsPath);
        }

        public void Save()
        {
            serializerJson.ToFile(Settings, SettingsPath);
        }
    }
}