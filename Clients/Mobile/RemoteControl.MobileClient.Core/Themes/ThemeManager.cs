using System;
using System.Collections.Generic;
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
                foreach (var dictionary in mergedDictionaries)
                {
                    if (!(dictionary is ITheme))
                    {
                        mergedDictionaries.Add(dictionary);
                    }
                }
                mergedDictionaries.Add(Activator.CreateInstance<T>());
            }
        }
    }
}