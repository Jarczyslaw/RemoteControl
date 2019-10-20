using JToolbox.WPF.UI;
using MahApps.Metro.Controls;

namespace RemoteControl.Server.Core.Views
{
    public partial class ShellWindow : MetroWindow
    {
        private readonly WindowEvents windowEvents;

        public ShellWindow()
        {
            InitializeComponent();
            windowEvents = new WindowEvents(this);
            windowEvents.Attach();
        }
    }
}