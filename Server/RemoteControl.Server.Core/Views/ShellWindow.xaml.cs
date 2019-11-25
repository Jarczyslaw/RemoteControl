using JToolbox.Desktop.Core.Services;
using JToolbox.WPF.UI;
using MahApps.Metro.Controls;
using System;
using System.IO;
using System.Windows;

namespace RemoteControl.Server.Core.Views
{
    public partial class ShellWindow : MetroWindow
    {
        private readonly WindowEvents windowEvents;
        private WindowState windowState;
        private readonly ISystemService systemService;

        public ShellWindow(ISystemService systemService)
        {
            this.systemService = systemService;

            Instance = this;
            InitializeComponent();
            Width = MinWidth = 800;
            Height = MinHeight = 600;
            windowEvents = new WindowEvents(this);
            windowEvents.Attach();
        }

        public static ShellWindow Instance { get; private set; }

        protected override void OnStateChanged(EventArgs e)
        {
            if (WindowState == WindowState.Minimized)
            {
                Hide();
            }
            else
            {
                windowState = WindowState;
            }
            base.OnStateChanged(e);
        }

        private void miRestore_Click(object sender, RoutedEventArgs e)
        {
            WindowRestore();
        }

        private void miClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void taskBarIcon_TrayMouseDoubleClick(object sender, RoutedEventArgs e)
        {
            WindowRestore();
        }

        private void WindowRestore()
        {
            Show();
            WindowState = windowState;
        }

        private void miAppLocation_Click(object sender, RoutedEventArgs e)
        {
            systemService.OpenAppLocation();
        }

        private void miAppLogsLocation_Click(object sender, RoutedEventArgs e)
        {
            systemService.OpenFolderLocation(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "logs"));
        }
    }
}