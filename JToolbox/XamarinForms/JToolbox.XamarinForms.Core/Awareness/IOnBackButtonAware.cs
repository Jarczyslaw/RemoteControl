using System.Threading.Tasks;

namespace JToolbox.XamarinForms.Core.Awareness
{
    public interface IOnBackButtonAware
    {
        Task<bool> OnBackButton();
    }
}