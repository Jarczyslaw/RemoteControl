using JToolbox.XamarinForms.Core.Awareness;
using Xamarin.Forms;

namespace JToolbox.XamarinForms.Core.Base
{
    public class PageBase : ContentPage
    {
        private bool shown;

        public ViewModelBase Context => BindingContext as ViewModelBase;

        public bool ViewVisible { get; private set; }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            ViewVisible = true;
            if (Context is IOnAppearingAware onAppearingAware)
            {
                await onAppearingAware.OnAppearing();
            }
            if (!shown && Context is IOnShownAware onShownAware)
            {
                await onShownAware.OnShown();
                shown = true;
            }
        }

        protected override async void OnDisappearing()
        {
            base.OnDisappearing();
            ViewVisible = false;
            if (Context is IOnDisappearingAware onDisappearingAware)
            {
                await onDisappearingAware.OnDisappearing();
            }
        }

        protected override bool OnBackButtonPressed()
        {
            base.OnBackButtonPressed();
            Device.BeginInvokeOnMainThread(async () =>
            {
                var @return = true;
                if (Context is IOnBackButtonAware onBackButtonAware)
                {
                    @return = await onBackButtonAware.OnBackButton();
                }
                if (@return)
                {
                    await Navigation.PopAsync();
                }
            });
            return true;
        }
    }
}