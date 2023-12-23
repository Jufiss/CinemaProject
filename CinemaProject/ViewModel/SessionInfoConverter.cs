using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows;

namespace LoginForm.ViewModel
{
    public class SessionInfoConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Length >= 2)
            {
                string filmName = values[0] as string;
                DateTime sessionTime = (DateTime)values[1];

                // Ваши действия с filmName и sessionTime

                // Возвращаем необходимый объект, который будет передан в качестве параметра команды
                return new { FilmName = filmName, SessionTime = sessionTime };
            }

            return DependencyProperty.UnsetValue;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
