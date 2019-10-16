using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Media.Imaging;

namespace JToolbox.Desktop.Core.Services
{
    public class SystemService : ISystemService
    {
        public void CopyToClipboard(BitmapSource bitmapSource)
        {
            Clipboard.SetImage(bitmapSource);
        }

        public void OpenAppLocation()
        {
            var appLocation = AppDomain.CurrentDomain.BaseDirectory;
            StartProcess(appLocation);
        }

        public void OpenFileLocation(string filePath)
        {
            var argument = "/select, \"" + filePath + "\"";
            StartProcess("explorer.exe", argument);
        }

        public void StartProcess(string process, string arguments = null)
        {
            Process.Start(process, arguments);
        }
    }
}