using JToolbox.WCF.BindingConfigurations;
using System;
using System.ServiceModel;

namespace JToolbox.WCF
{
    public class Server<TProxy> : IDisposable
            where TProxy : class
    {
        public Server(BindingConfiguration bindingConfiguration, Type serviceType)
        {
            Host = new ServiceHost(serviceType, new Uri(bindingConfiguration.ApplicationAddress));
            Initialize(bindingConfiguration);
        }

        public Server(BindingConfiguration bindingConfiguration, TProxy proxyInstance)
        {
            Host = new ServiceHost(proxyInstance, new Uri(bindingConfiguration.ApplicationAddress));
            Initialize(bindingConfiguration);
        }

        public ServiceHost Host { get; }
        public bool IsListening => Host?.State == CommunicationState.Opened;

        public void Start()
        {
            Stop();
            Host.Open();
        }

        public void Stop()
        {
            if (Host != null)
            {
                try
                {
                    Host.Close();
                }
                catch
                {
                    Host.Abort();
                }
            }
        }

        public void Dispose()
        {
            Stop();
        }

        private void Initialize(BindingConfiguration bindingConfiguration)
        {
            Host.AddServiceEndpoint(typeof(TProxy), bindingConfiguration.Binding, bindingConfiguration.ServiceName);
        }
    }
}