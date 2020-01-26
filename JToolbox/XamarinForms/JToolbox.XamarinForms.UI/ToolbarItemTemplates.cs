using System.Collections.Generic;
using Xamarin.Forms;

namespace JToolbox.XamarinForms.UI
{
    public class ToolbarItemTemplates : List<ToolbarItemTemplate>
    {
        public void GenerateToolbarItems(IList<ToolbarItem> toolbarItems)
        {
            toolbarItems.Clear();
            foreach (var toolbarItem in this)
            {
                toolbarItems.Add(toolbarItem.GetItem());
            }
        }
    }
}