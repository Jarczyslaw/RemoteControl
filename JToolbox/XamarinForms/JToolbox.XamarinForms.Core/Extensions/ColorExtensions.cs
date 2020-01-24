using Xamarin.Forms;

namespace JToolbox.XamarinForms.Core.Extensions
{
    public static class ColorExtensions
    {
        public static double GetLuminance(this Color color)
        {
            return 0.3d * color.R + 0.59d * color.G + 0.11d * color.B;
        }

        public static bool IsLight(this Color color, double threshold = 0.5d)
        {
            return color.GetLuminance() > threshold;
        }
    }
}