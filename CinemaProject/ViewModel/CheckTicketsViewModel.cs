using LoginForm.Interfaces;
using LoginForm.Model;
using LoginForm.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace LoginForm.ViewModel
{
    public class CheckTicketsViewModel : ViewModelBase
    {
        private List<OrderModel> allOrders;
        private string _text;
        private IOrderRepository orderRepository;

        public List<OrderModel> AllOrders { get => allOrders; set { allOrders = value; OnPropertyChanged(nameof(AllOrders)); } }
        public string Text { get => _text; set { _text = value; OnPropertyChanged(nameof(Text)); ExecuteSearchTicketViewCommand(null); } }
        public ICommand SearchTicketViewCommand { get; }
        public CheckTicketsViewModel() 
        {
            orderRepository = new OrderRepository();
            AllOrders = orderRepository.GetAllOrders();

            SearchTicketViewCommand = new ViewModelCommand(ExecuteSearchTicketViewCommand);
        }

        private void ExecuteSearchTicketViewCommand(object obj)
        {
            if (Text != null && Text != "")
                AllOrders = orderRepository.SearchOrder(Convert.ToInt32(Text));
        }
    }
}
