namespace JToolbox.NetworkTools
{
    public class PortScannerFactory
    {
        public IPortClient GetClient(PortType portType)
        {
            if (portType == PortType.TCP)
            {
                return new TCPPortClient();
            }
            else
            {
                return new UDPPortClient();
            }
        }
    }
}