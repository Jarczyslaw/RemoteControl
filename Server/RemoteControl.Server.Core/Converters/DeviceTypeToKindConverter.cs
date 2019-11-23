using MahApps.Metro.IconPacks;
using System;
using System.Globalization;
using System.Windows.Data;

namespace RemoteControl.Server.Core.Converters
{
    public class DeviceTypeToKindConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Proxy.ConnectionRequest.Types.DeviceType deviceType)
            {
                if (deviceType == Proxy.ConnectionRequest.Types.DeviceType.Mobile)
                {
                    return PackIconFontAwesomeKind.MobileAltSolid;
                }
                else if (deviceType == Proxy.ConnectionRequest.Types.DeviceType.Desktop)
                {
                    return PackIconFontAwesomeKind.DesktopSolid;
                }
                else
                {
                    return PackIconFontAwesomeKind.QuestionCircleRegular;
                }
            }
            return PackIconFontAwesomeKind.ExclamationCircleSolid;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}