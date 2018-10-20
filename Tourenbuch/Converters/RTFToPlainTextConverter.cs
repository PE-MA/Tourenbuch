using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Documents;
using Xceed.Wpf.Toolkit;

namespace Tourenbuch.Converters
{
    public class RTFToPlainTextConverter : BaseConverter, IValueConverter
    {

        public object Convert(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            // for information : default Xceed.Wpf.Toolkit.RichTextBox formatter is RtfFormatter 
            RichTextBox rtBox = new RichTextBox(new FlowDocument());
            rtBox.Text = (string)value;
            rtBox.TextFormatter = new PlainTextFormatter();
            return rtBox.Text;
        }

        public object ConvertBack(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            // for information : default Xceed.Wpf.Toolkit.RichTextBox formatter is RtfFormatter 
            RichTextBox rtBox = new RichTextBox(new FlowDocument());
            rtBox.Text = (string)value;
            rtBox.TextFormatter = new PlainTextFormatter();
            return rtBox.Text;
        }
    }
}