using System.ServiceModel;

namespace JToolbox.WCF.BindingConfigurations
{
    public class NamedPipeConfiguration : BindingConfiguration
    {
        public NamedPipeConfiguration()
        {
            MachineAddress = "localhost";
            Binding = new NetNamedPipeBinding();
        }

        public override string BindingAddress => "net.pipe://";
    }
}