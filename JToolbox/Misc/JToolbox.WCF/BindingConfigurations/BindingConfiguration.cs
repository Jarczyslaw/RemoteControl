using System.ServiceModel.Channels;

namespace JToolbox.WCF.BindingConfigurations
{
    public abstract class BindingConfiguration
    {
        public Binding Binding { get; protected set; }
        public string ApplicationName { get; set; }
        public string ServiceName { get; set; }
        public virtual string MachineAddress { get; protected set; }

        public abstract string BindingAddress { get; }

        public string ApplicationAddress => $"{BindingAddress}{MachineAddress}/{ApplicationName}";
        public string ServiceAddress => $"{ApplicationAddress}/{ServiceName}";
    }
}