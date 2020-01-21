using System;
using Xamarin.Forms;

namespace RemoteControl.MobileClient.Core.Themes
{
    public class ThemeManager : IThemeManager
    {
        public void SetTheme<T>()
            where T : ResourceDictionary
        {
            var mergedDictionaries = Application.Current.Resources.MergedDictionaries;
            if (mergedDictionaries != null)
            {
                mergedDictionaries.Clear();
                mergedDictionaries.Add(Activator.CreateInstance<T>());
            }
        }
    }
}