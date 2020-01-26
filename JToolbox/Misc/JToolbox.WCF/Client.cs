using JToolbox.WCF.BindingConfigurations;
using System;
using System.ServiceModel;

namespace JToolbox.WCF
{
    public class Client<TProxy> : IDisposable
            where TProxy : class
    {
        public Client(BindingConfiguration bindingConfiguration)
        {
            ChannelFactory = new ChannelFactory<TProxy>(bindingConfiguration.Binding, new EndpointAddress(bindingConfiguration.ServiceAddress));
        }

        public TProxy Proxy { get; private set; }
        public ChannelFactory<TProxy> ChannelFactory { get; }
        public bool IsConnected => ChannelFactory?.State == CommunicationState.Opened;

        public void Start()
        {
            Stop();
            Proxy = ChannelFactory.CreateChannel();
        }

        public void Stop()
        {
            if (ChannelFactory != null)
            {
                try
                {
                    ChannelFactory.Close();
                }
                catch
                {
                    ChannelFactory.Abort();
                }
            }
        }

        public void Dispose()
        {
            Stop();
        }
    }
}