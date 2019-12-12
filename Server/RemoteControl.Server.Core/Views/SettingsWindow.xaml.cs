using JToolbox.WPF.Core.Awareness;
using MahApps.Metro.SimpleChildWindow;

namespace RemoteControl.Server.Core.Views
{
    public partial class SettingsWindow : ChildWindow
    {
        public SettingsWindow()
        {
            InitializeComponent();
            Loaded += SettingsWindow_Loaded;
        }

        private void SettingsWindow_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            if (!WindowInitialized)
            {
                if (DataContext is ICloseSource closeAware)
                {
                    closeAware.OnClose += () => Close();
                }
                WindowInitialized = true;
            }
        }

        public bool WindowInitialized { get; private set; }
    }
}