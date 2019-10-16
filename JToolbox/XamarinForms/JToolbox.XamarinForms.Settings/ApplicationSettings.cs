using Newtonsoft.Json;
using System;
using Xamarin.Essentials;

namespace JToolbox.XamarinForms.Settings
{
    public abstract class ApplicationSettings : IApplicationSettings
    {
        public DateTime GetDateTime(string key, DateTime defaultValue = default(DateTime))
        {
            return Preferences.Get(key, defaultValue);
        }

        public float GetFloat(string key, float defaultValue = default(float))
        {
            return Preferences.Get(key, defaultValue);
        }

        public int GetInt(string key, int defaultValue = default(int))
        {
            return Preferences.Get(key, defaultValue);
        }

        public string GetString(string key, string defaultValue = default(string))
        {
            return Preferences.Get(key, defaultValue);
        }

        public long GetLong(string key, long defaultValue = default(long))
        {
            return Preferences.Get(key, defaultValue);
        }

        public bool GetBool(string key, bool defaultValue = default(bool))
        {
            return Preferences.Get(key, defaultValue);
        }

        public double GetDouble(string key, double defaultValue = default(double))
        {
            return Preferences.Get(key, defaultValue);
        }

        public T GetValue<T>(string key, T defaultValue = default(T))
        {
            var serialized = GetString(key, string.Empty);
            if (string.IsNullOrEmpty(serialized))
            {
                return defaultValue;
            }
            else
            {
                return JsonConvert.DeserializeObject<T>(serialized);
            }
        }

        public void AddDateTime(string key, DateTime value)
        {
            Preferences.Set(key, value);
        }

        public void AddFloat(string key, float value)
        {
            Preferences.Set(key, value);
        }

        public void AddInt(string key, int value)
        {
            Preferences.Set(key, value);
        }

        public void AddString(string key, string value)
        {
            Preferences.Set(key, value);
        }

        public void AddLong(string key, long value)
        {
            Preferences.Set(key, value);
        }

        public void AddBool(string key, bool value)
        {
            Preferences.Set(key, value);
        }

        public void AddDouble(string key, double value)
        {
            Preferences.Set(key, value);
        }

        public void AddValue<T>(string key, T value)
        {
            var serialized = JsonConvert.SerializeObject(value);
            AddString(key, serialized);
        }

        public void Clear()
        {
            Preferences.Clear();
        }

        public void Remove(string key)
        {
            Preferences.Remove(key);
        }
    }
}