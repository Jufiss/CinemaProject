using LoginForm.Model;
using LoginForm.Models;
using LoginForm.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace LoginForm.ViewModel
{
    public class SeatAvailabilityConverter : RepositoryBase, IMultiValueConverter
    {
        public object Convert(object[] value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value.Length == 2 && value[0] is SeatModel seat && value[1] is SessionModel session)
            {
                if (IsSeatTaken(seat.Id, session.Id))
                    return false;
            }

            return true;
        }

        public object[] ConvertBack(object value, Type[] targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        private bool IsSeatTaken(int seatId, int sessionId)
        {
            return db.OrderSeats.Any(orderSeat => orderSeat.SeatId == seatId && orderSeat.Order.SessionId == sessionId);
        }
    }
}
