using System;
using Xamarin.Forms;

namespace RemoteControl.MobileClient.Core.Controls
{
    public class StatusBarMessage
    {
        public string Content { get; set; }
        public Color Color { get; protected set; }
        public TimeSpan Duration { get; set; }
    }
}