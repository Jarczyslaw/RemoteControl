using RemoteControl.Proxy;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using static RemoteControl.Proxy.ConnectionRequest.Types;

namespace RemoteControl.DesktopClient.Core
{
    public partial class MainForm : Form
    {
        private LazyProxyClient lazyProxyClient = new LazyProxyClient();

        public MainForm()
        {
            InitializeComponent();
            InitializeClientData();
        }

        public string DeviceName
        {
            get => tbDeviceName.Text;
            set => tbDeviceName.Text = value;
        }

        public DeviceType DeviceType
        {
            set => tbDeviceType.Text = value.ToString();
        }

        public string LocalAddress
        {
            get => tbLocalAddress.Text;
            set => tbLocalAddress.Text = value;
        }

        public string RemoteAddress
        {
            get => tbRemoteAddress.Text;
            set => tbRemoteAddress.Text = value;
        }

        public int Port
        {
            get => Convert.ToInt32(nudRemotePort.Value);
            set => nudRemotePort.Value = value;
        }

        private void InitializeClientData()
        {
            tbDeviceName.Text = Environment.MachineName;
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