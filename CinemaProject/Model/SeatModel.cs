using LoginForm.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginForm.Model
{
    public class SeatModel
    {
        public int Id { get; set; }
        [DisplayName("Номер")]
        public int Number { get; set; }
        [DisplayName("Статус")]

        public int CategoryId { get; set; }
        public int Cost { get; set; }
        public string CategoryName { get; set; }

        public int HallId { get; set; }
        [DisplayName("Ряд")]
        public int Row { get; set; }

        public SeatModel() { }

        public SeatModel(Seat u)
        {
            Id = u.Id;
            Number = u.Number;
            CategoryId = u.CategoryId;
            HallId = u.HallId;
            Row = u.Row;
            Cost = u.Category.Cost;
            CategoryName = u.Category.Name;

        }
    }
}
