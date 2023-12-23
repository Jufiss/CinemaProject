using LoginForm.Models;
using Microsoft.VisualBasic.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace LoginForm.Model
{
    public class OrderModel
    {
        public int Id { get; set; }

        public double Sum { get; set; }
        public int? PromocodeId { get; set; }

        public int UserId { get; set; }

        public int SessionId { get; set; }
        [DisplayName("Место")]
        public List<SeatModel> Seats { get; set; }
        [DisplayName("Сеанс")]
        public string Sessions { get; set; } = null!;
        [DisplayName("Пользователь")]
        public string Users { get; set; } = null!;
        [DisplayName("Номер билета")]
        public long Number {  get; set; }
        [DisplayName("Дата и время")]
        public DateTime Date { get; set; }
        public DateTime OrderDate { get; set; }
        public OrderModel() { }

        public OrderModel(Order u)
        {
            Id = u.Id;
            Sum = u.Sum;
            UserId = u.UserId;
            SessionId = u.SessionId;
            Sessions = u.Session.Film.Name;
            Users = u.User.Login;
            Number = u.Number;
            PromocodeId = u.PromocodeId;
            OrderDate = u.OrderDate;
            Date = u.Session.Date;
        }
    }
}
