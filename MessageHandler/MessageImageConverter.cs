using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace MessageHandler
{
    /// <summary>
    /// Convert a message type to an image
    /// </summary>
    public class MessageImageConverter : IValueConverter
    {
        public object? Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is not MessageTypes messageType || !(parameter is string uri))
                return null;

            // Convert the message type to an image
            return messageType switch
            {
                MessageTypes.Error => new BitmapImage(new Uri(string.Format(uri, "outlineWarning.png"))),
                MessageTypes.Success => new BitmapImage(new Uri(string.Format(uri, "checkMark.png"))),
                MessageTypes.Information => new BitmapImage(new Uri(string.Format(uri, "information.png"))),
                _ => null
            };
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

}
