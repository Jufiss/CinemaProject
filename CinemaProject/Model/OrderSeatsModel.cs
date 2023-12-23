using LoginForm.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginForm.Model
{
    public class OrderSeatsModel
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int SeatId { get; set; }

        public OrderSeatsModel() { }
        public OrderSeatsModel(OrderSeat o)
        {
            Id = o.Id;
            OrderId = o.OrderId;
            SeatId = o.SeatId;
        }
    }
}
