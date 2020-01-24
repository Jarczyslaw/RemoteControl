using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace JToolbox.XamarinForms.Themes
{
    public class ThemeManager : IThemeManager
    {
        private readonly IStatusBarColorManager statusBarColorManager;

        public ThemeManager(IStatusBarColorManager statusBarColorManager)
        {
            this.statusBarColorManager = statusBarColorManager;
        }

        public void SetTheme<T>()
            where T : ResourceDictionary, IThemeResourceDictionary
        {
            SetTheme(Activator.CreateInstance<T>());
        }

        public void SetTheme(ResourceDictionary resourceDictionary)
        {
            var mergedDictionaries = Application.Current.Resources.MergedDictionaries;
            if (mergedDictionaries != null)
            {
                var dictionaries = new List<ResourceDictionary>();
                foreach (var dictionary in mergedDictionaries)
                {
                    if (!(dictionary is IThemeResourceDictionary))
                    {
                        dictionaries.Add(dictionary);
                    }
                }

                mergedDictionaries.Clear();
                mergedDictionaries.Add(resourceDictionary);
                foreach (var dictionary in dictionaries)
                {
                    mergedDictionaries.Add(dictionary);
                }

                var themeResourceDictionary = resourceDictionary as IThemeResourceDictionary;
                statusBarColorManager.SetColor(themeResourceDictionary.NotificationBarColor);
            }
        }
    }
}