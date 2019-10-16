namespace JToolbox.XamarinForms.Core.Abstraction
{
    public interface IPaths
    {
        string InternalFolder { get; }
        string PublicExternalFolder { get; }
        string PrivateExternalFolder { get; }
    }
}