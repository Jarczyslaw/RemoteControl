using JToolbox.WPF.Core.Awareness;
using System;
using System.ComponentModel;
using System.Windows;

namespace JToolbox.WPF.UI
{
    public class WindowBase : Window
    {
        private bool windowRendered;
        private bool windowInitialized;

        public WindowBase()
        {
        }

        public WindowBase(object dataContext)
            : this()
        {
            DataContext = dataContext;
        }

        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);
            if (!windowInitialized)
            {
                OnViewInitialized();
                windowInitialized = true;
            }
        }

        protected override void OnContentRendered(EventArgs e)
        {
            base.OnContentRendered(e);
            if (!windowRendered)
            {
                OnViewShown();
                windowRendered = true;
            }
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            if (DataContext is IOnClosingAware closingAware)
            {
                e.Cancel = closingAware.OnClosing();
            }
        }

        protected virtual void OnViewInitialized()
        {
            if (DataContext is ICloseSource closeAware)
            {
                closeAware.OnClose += Close;
            }
        }

        protected virtual void OnViewShown()
        {
            (DataContext as IOnShowAware)?.OnShow();
        }
    }
}