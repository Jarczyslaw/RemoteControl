using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace JToolbox.XamarinForms.Themes
{
    public class ThemeColorExtractor
    {
        private readonly ResourceDictionary resourceDictionary;

        public ThemeColorExtractor(ResourceDictionary resourceDictionary)
        {
            this.resourceDictionary = resourceDictionary;
        }

        private Color GetColor([CallerMemberName] string colorName = null)
        {
            return (Color)resourceDictionary[colorName];
        }

        public Color PageBackgroundColor => GetColor();
        public Color NavigationBarColor => GetColor();
        public Color PrimaryColor => GetColor();
        public Color SecondaryColor => GetColor();
        public Color PrimaryTextColor => GetColor();
        public Color SecondaryTextColor => GetColor();
        public Color TertiaryTextColor => GetColor();
        public Color BorderColor => GetColor();
        public Color TransparentColor => GetColor();
    }
}