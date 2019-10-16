using System;

namespace JToolbox.XamarinForms.Settings
{
    public interface IApplicationSettings
    {
        void AddBool(string key, bool value);

        void AddDateTime(string key, DateTime value);

        void AddDouble(string key, double value);

        void AddFloat(string key, float value);

        void AddInt(string key, int value);

        void AddLong(string key, long value);

        void AddString(string key, string value);

        void AddValue<T>(string key, T value);

        void Clear();

        bool GetBool(string key, bool defaultValue = false);

        DateTime GetDateTime(string key, DateTime defaultValue = default(DateTime));

        double GetDouble(string key, double defaultValue = 0);

        float GetFloat(string key, float defaultValue = 0);

        int GetInt(string key, int defaultValue = 0);

        long GetLong(string key, long defaultValue = 0);

        string GetString(string key, string defaultValue = null);

        T GetValue<T>(string key, T defaultValue = default(T));

        void Remove(string key);
    }
}