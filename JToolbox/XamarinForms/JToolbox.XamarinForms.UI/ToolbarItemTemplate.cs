using Xamarin.Forms;

namespace JToolbox.XamarinForms.UI
{
    public class ToolbarItemTemplate
    {
        public Color Color { get; set; }
        public string FontFamily { get; set; }
        public string Glyph { get; set; }
        public string CommandBinding { get; set; }

        public ToolbarItem GetItem()
        {
            var item = new ToolbarItem
            {
                IconImageSource = new FontImageSource
                {
                    Color = Color,
                    FontFamily = FontFamily,
                    Glyph = Glyph
                },
            };
            item.SetBinding(MenuItem.CommandProperty, new Binding(CommandBinding));
            return item;
        }
    }
}