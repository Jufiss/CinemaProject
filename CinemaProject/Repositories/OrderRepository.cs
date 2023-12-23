using LoginForm.Interfaces;
using LoginForm.Model;
using LoginForm.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace LoginForm.Repositories
{
    public class OrderRepository : RepositoryBase, IOrderRepository
    {
        public void Add(OrderModel orderModel)
        {
            db.Orders.Add(new Order()
            {
                //Id = orderModel.Id,
                Number = orderModel.Number,
                PromocodeId = orderModel.PromocodeId,
                SessionId = orderModel.SessionId,
                Sum = orderModel.Sum,
                UserId = orderModel.UserId,
                OrderDate = orderModel.OrderDate,
            });
            Save();
        }

        public void AddSeats(OrderSeatsModel orderModel)
        {
            db.OrderSeats.Add(new OrderSeat()
            {
                Id = orderModel.Id,
                OrderId = orderModel.OrderId,
                SeatId = orderModel.SeatId,
            });
            Save();
        }

        public List<OrderSeatsModel> GetOrderSeats()
        {
             return db.OrderSeats.Include(u => u.Order).Include(u=>u.Seat).ToList().Select(i => new OrderSeatsModel(i)).ToList();
        }
        public ObservableCollection<SeatModel> GetSeats(int hall)
        {
            List<SeatModel> seatList = db.Seats
                .Where(s => s.HallId == hall)
                .Select(s => new SeatModel
                {
                    Id = s.Id,
                    Row = s.Row,
                    CategoryId = s.CategoryId,
                    HallId = s.HallId,
                    Number = s.Number,
                    Cost = s.Category.Cost,
                    CategoryName = s.Category.Name,
                })
                .ToList();
            ObservableCollection<SeatModel> seats = new ObservableCollection<SeatModel>(seatList);
            return seats;
        }

        public ObservableCollection<OrderModel> OrdersByUserId(int userId)
        {
            List<OrderModel> orderList = db.Orders
                .Where(s => s.UserId == userId)
                .Select(s => new OrderModel
                {
                    Id = s.Id,
                    Date = s.Session.Date,
                    Number = s.Number,
                    Sum = s.Sum,
                    UserId = s.UserId,
                    SessionId = s.SessionId,
                    Sessions = s.Session.Film.Name,
                    Seats = s.OrderSeats.Select(os => new SeatModel
                    {
                        Id =os.Id,
                        Number = os.Seat.Number,
                    }).ToList(),
                })
                .ToList();
            ObservableCollection<OrderModel> orders = new ObservableCollection<OrderModel>(orderList);
            return orders;
        }

        public PromocodeModel GetPromocode(string name)
        {
            var request = db.Promocodes
                .Where(u => u.Name == name)
                .Select(u => new PromocodeModel()
                {
                    Id = u.Id,
                    Name = u.Name,
                    Discount = u.Discount,
                })
                .FirstOrDefault();
            return request;
        }

        public List<OrderModel> OrdersByTwoDates(DateTime start, DateTime end)
        {
            List<OrderModel> orderList = db.Orders.Where(s => s.OrderDate >= start && s.OrderDate <= end)
                                    .Include(s => s.User)
                                    .Include(s => s.Session)
                                    .Select(i => new OrderModel
                                    {
                                        Id =i.Id,
                                        OrderDate = i.OrderDate,
                                        Sum = i.Sum,
                                        SessionId = i.SessionId,
                                        Sessions = i.Session.Film.Name,
                                    })
                                    .ToList();

            //ObservableCollection<OrderModel> observableCollection = new ObservableCollection<OrderModel>(orderList);

            return orderList;
        }
        public List<OrderModel> GetAllOrders()
        {
            List<OrderModel> orderList = db.Orders
                                    .Include(s => s.User)
                                    .Include(s => s.Session)
                                    .Select(u => new OrderModel()
                                    {
                                        Id = u.Id,
                                        Number = u.Number,
                                        SessionId = u.SessionId,
                                        UserId = u.UserId,
                                        Sessions = u.Session.Film.Name,
                                        Users = u.User.Login,
                                        Date = u.Session.Date,
                                        Sum = u.Sum,
                                        Seats = u.OrderSeats.Select(os => new SeatModel
                                        {
                                            Id = os.Id,
                                            Number = os.Seat.Number,
                                        }).ToList(),
                                    })
                                    .ToList();

            return orderList;
        }
        public List<OrderModel> SearchOrder(long number)
        {
            var request = db.Orders
                .Where(u => u.Number == number)
                .Select(u => new OrderModel()
                {
                    Id = u.Id,
                    Number=u.Number,
                    //SeatId = u.SeatId,
                    SessionId = u.SessionId,
                    UserId  = u.UserId,
                    //Seats = u.Seat.Number,
                    Sessions = u.Session.Film.Name,
                    Users = u.User.Login,
                    Date = u.Session.Date,
                    Sum = u.Sum,
                    Seats = u.OrderSeats.Select(os => new SeatModel
                    {
                        Id = os.Id,
                        Number = os.Seat.Number,
                    }).ToList(),
                })
                .ToList();
            return request;
        }
        public bool Save()
        {
            if (db.SaveChanges() > 0)
                return true;
            return false;
        }
    }
}
