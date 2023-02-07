using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace MessageHandler
{
    internal class MessageTemplateSelector : DataTemplateSelector
    {
        public override DataTemplate? SelectTemplate(object item, DependencyObject container)
        {
            if (container is not FrameworkElement element)
                return null;

            if (item is not Message message)
                return null;

            return message.MessageTypes switch
            {
                MessageTypes.Error => element.FindResource("ErrorTemplate") as DataTemplate,
                MessageTypes.Success => element.FindResource("SuccessTemplate") as DataTemplate,
                MessageTypes.Information => element.FindResource("InformationTemplate") as DataTemplate,
                _ => null
            };
        }
    }
}
