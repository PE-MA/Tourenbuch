using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using TourenbuchDatentypen;

namespace Tourenbuch.Converters
{
    public class AssignedUsersToStringConverter : BaseConverter, IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values[0] == DependencyProperty.UnsetValue || values[1] == DependencyProperty.UnsetValue)
                return null;
            List<int> userList = (List<int>) values[0];
            ObservableCollection<User> users = (ObservableCollection <User>)values[1];

            StringBuilder returnStringBuilder = new StringBuilder();
            for(int i=0; i<userList.Count; i++)
            {
                User mate = users.FirstOrDefault(u => u.id == userList[i]);
                if(mate != null)
                {
                    if(i != 0)
                        returnStringBuilder.Append(" ");
                    returnStringBuilder.Append(mate.Forename.Substring(0,1));
                    returnStringBuilder.Append(".");
                    returnStringBuilder.Append(mate.Surname.Substring(0, 1));
                }
            }
            return returnStringBuilder.ToString();
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
