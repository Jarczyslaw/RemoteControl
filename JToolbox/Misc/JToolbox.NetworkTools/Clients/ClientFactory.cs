namespace JToolbox.NetworkTools.Clients
{
    public class ClientFactory
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