using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RemoteControl.MobileClient.Core.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StatusBar : ContentView
    {
        public StatusBar()
        {
            InitializeComponent();
        }
    }
}