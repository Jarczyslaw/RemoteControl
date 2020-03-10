using System;
using System.Collections.Generic;
using Acr.UserDialogs;

namespace JToolbox.XamarinForms.Dialogs
{
    public class ActionSheet<T> : ActionSheetConfig
    {
        public Action<T> ActionSheetSelected { get; set; }

        public ActionSheet()
        {
            Options = new List<ActionSheetOption>();
        }

        public void AddOption(string title, T value)
        {
            Options.Add(new ActionSheetOption(title, () => ActionSheetSelected(value)));
        }

        public void AddCancel(string title)
        {
            Cancel = new ActionSheetOption(title, () => ActionSheetSelected(default(T)));
        }
    }
}