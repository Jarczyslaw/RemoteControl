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

        public Color PrimaryBackgroundColor => GetColor();
        public Color SecondaryBackgroundColor => GetColor();
        public Color PrimaryColor => GetColor();
        public Color SecondaryColor => GetColor();
        public Color TertiaryColor => GetColor();
        public Color PrimaryTextColor => GetColor();
        public Color SecondaryTextColor => GetColor();
        public Color TertiaryTextColor => GetColor();
        public Color TransparentColor => GetColor();
    }
}