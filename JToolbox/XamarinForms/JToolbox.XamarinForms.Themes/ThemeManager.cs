using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace JToolbox.XamarinForms.Themes
{
    public delegate void ThemeChanged(IThemeResourceDictionary themeResourceDictionary);

    public class ThemeManager : IThemeManager
    {
        private readonly IPlatformThemeManager platformThemeManager;

        public event ThemeChanged OnThemeChanged = delegate { };

        public ThemeManager(IPlatformThemeManager platformThemeManager)
        {
            this.platformThemeManager = platformThemeManager;
        }

        public IThemeResourceDictionary CurrentTheme { get; private set; }

        public void SetTheme<T>()
            where T : ResourceDictionary, IThemeResourceDictionary
        {
            SetTheme(Activator.CreateInstance<T>());
        }

        public void SetTheme(ResourceDictionary resourceDictionary)
        {
            var themeResourceDictionary = resourceDictionary as IThemeResourceDictionary;
            if (themeResourceDictionary == null)
            {
                throw new Exception("Invalid resource dictionary type");
            }

            var mergedDictionaries = Application.Current.Resources.MergedDictionaries;
            if (mergedDictionaries != null)
            {
                CurrentTheme = themeResourceDictionary;
                platformThemeManager.SetTheme(themeResourceDictionary);
                ReplaceThemeResourceDictionaries(mergedDictionaries, resourceDictionary);
                OnThemeChanged(themeResourceDictionary);
            }
        }

        private void ReplaceThemeResourceDictionaries(ICollection<ResourceDictionary> mergedDictionaries, ResourceDictionary resourceDictionary)
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
        }
    }
}