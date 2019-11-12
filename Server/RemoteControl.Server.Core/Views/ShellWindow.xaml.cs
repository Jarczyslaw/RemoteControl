using JToolbox.WPF.UI;
using MahApps.Metro.Controls;
using System;
using System.Windows;

namespace RemoteControl.Server.Core.Views
{
    public partial class ShellWindow : MetroWindow
    {
        private readonly WindowEvents windowEvents;
        private WindowState windowState;

        public ShellWindow()
        {
            InitializeComponent();
            windowEvents = new WindowEvents(this);
            windowEvents.Attach();
        }

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
    }
}