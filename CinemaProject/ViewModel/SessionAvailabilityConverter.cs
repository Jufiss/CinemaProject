using LoginForm.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace LoginForm.ViewModel
{
    public class SessionAvailabilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is DateTime sessionTime)
            {
                DateTime currentTime = DateTime.Now;
                if (currentTime < sessionTime) 
                    return true;
            }
            else if (value is SessionModel session)
            {
                DateTime currentTime = DateTime.Now;
                if (currentTime < session.Date)
                    return true;
            }

            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
