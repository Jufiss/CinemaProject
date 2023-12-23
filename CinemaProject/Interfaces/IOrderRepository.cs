using LoginForm.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginForm.Interfaces
{
    public interface IOrderRepository
    {
        public void Add(OrderModel orderModel);
        public void AddSeats(OrderSeatsModel orderModel);
        public ObservableCollection<SeatModel> GetSeats(int hall);
        public List<OrderModel> GetAllOrders();
        public List<OrderSeatsModel> GetOrderSeats();
        public List<OrderModel> SearchOrder(long number);
        public List<OrderModel> OrdersByTwoDates(DateTime start, DateTime end);
        public ObservableCollection<OrderModel> OrdersByUserId(int userId);
        public PromocodeModel GetPromocode(string name);
        public bool Save();
    }
}
