using JToolbox.XamarinForms.Core.Navigation;
using Prism.Mvvm;
using Prism.Navigation;
using System.Threading.Tasks;

namespace JToolbox.XamarinForms.Core.Base
{
    public class ViewModelBase : BindableBase, INavigationAware, IDestructible
    {
        protected readonly INavService navService;
        protected Parameters Parameters { get; private set; }

        private string title;

        public ViewModelBase(INavService navService)
        {
            this.navService = navService;
        }

        public string Title
        {
            get => title;
            set => SetProperty(ref title, value);
        }

        #region Navigation

        protected Task<INavigationResult> Navigate<T>(Parameters parameters = null)
            where T : ViewModelBase
        {
            return navService.NavigateToViewModel<T>(parameters);
        }

        protected Task<INavigationResult> Close(Parameters parameters = null)
        {
            return navService.Close(parameters);
        }

        protected Task<INavigationResult> ReturnToRoot(Parameters parameters = null)
        {
            return navService.ReturnToRoot(parameters);
        }

        public virtual void OnNavigatedFrom(INavigationParameters parameters)
        {
        }

        public virtual void OnNavigatedTo(INavigationParameters parameters)
        {
            Parameters = Parameters.CreateFromNavigationParameters(parameters);
        }

        #endregion Navigation

        public virtual void Destroy()
        {
        }
    }
}