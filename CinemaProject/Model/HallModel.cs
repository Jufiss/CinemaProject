using LoginForm.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginForm.Model
{
    public class HallModel
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public int NumberSeats { get; set; }

        public int NumberRows { get; set; }
        public HallModel() { }

        public HallModel(Hall u)
        {
            Id = u.Id;
            Number = u.Number;
            NumberSeats = u.NumberSeats;
            NumberRows = u.NumberRows;
        }
    }
}
