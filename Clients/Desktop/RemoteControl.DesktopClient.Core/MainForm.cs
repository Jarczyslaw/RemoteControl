using System;
using System.Windows.Forms;

namespace RemoteControl.DesktopClient.Core
{
    public partial class MainForm : Form
    {
        private string remoteAddress;
        private int remotePort;

        public MainForm()
        {
            InitializeComponent();
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
        }

        private void btnDisconnect_Click(object sender, EventArgs e)
        {
        }

        private void btnShutdown_Click(object sender, EventArgs e)
        {
        }

        private void btnRestart_Click(object sender, EventArgs e)
        {
        }
    }
}