using System.Windows.Media.Imaging;

namespace JToolbox.Desktop.Core.Services
{
    public interface ISystemService
    {
        void CopyToClipboard(BitmapSource bitmapSource);
        void OpenAppLocation();
        void OpenFileLocation(string filePath);
        void StartProcess(string process, string arguments = null);
    }
}