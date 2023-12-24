using LoginForm.Interfaces;
using LoginForm.Model;
using LoginForm.ViewModel;
using LoginForm.Repositories;
using LoginForm.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Documents.DocumentStructures;
using System.Threading;
using System.Windows;

namespace LoginForm.ViewModel
{
    public class OrderViewModel : ViewModelBase, IOrderService
    {
        private ObservableCollection<SeatModel> _seats;
        private ObservableCollection<SeatModel> _selectedSeats;
        private SessionModel _session;
        private UserModel _user;
        private double _sum;
        private long _number;
        private bool popupIsOpen;
        private string _promocode;
        private bool IsActivePromocode = false;
        private IOrderRepository orderRepository;
        private ISessionRepository sessionRepository;
        private IUserRepository userRepository;
        public ObservableCollection<SeatModel> Seats { get => _seats; set { _seats = value; OnPropertyChanged(nameof(Seats)); } }
        public SessionModel Session { get => _session; set { _session = value; OnPropertyChanged(nameof(Session)); } }
        public ObservableCollection<SeatModel> SelectedSeats { get => _selectedSeats; set { _selectedSeats = value; OnPropertyChanged(nameof(SelectedSeats)); } }
        public double Sum { get => _sum; set { _sum = value; OnPropertyChanged(nameof(Sum)); } }
        public bool PopupIsOpen
        {
            get => popupIsOpen;
            set
            {
                    popupIsOpen = value;
                    OnPropertyChanged(nameof(PopupIsOpen));
            }
        }
        public ICommand MakeOrderCommand { get; }
        public ICommand OpenPopUpCommand { get; }
        public ICommand ClosePopUpCommand { get; }
        public ICommand SelectSeatCommand { get; }
        public ICommand ApplyPromocodeCommand { get; }
        public UserModel User { get => _user; set { _user = value; OnPropertyChanged(nameof(User)); } }

        public long Number { get => _number; set { _number = value; OnPropertyChanged(nameof(Number)); } }

        public string Promocode { get => _promocode; set { _promocode = value; OnPropertyChanged(nameof(Promocode)); } }

        public OrderViewModel() 
        {
            orderRepository = new OrderRepository();
            sessionRepository = new SessionRepository();
            userRepository = new UserRepository();
            SelectedSeats = new ObservableCollection<SeatModel>();

            MakeOrderCommand = new ViewModelCommand(ExecuteMakeOrderCommand);
            OpenPopUpCommand = new ViewModelCommand(ExecuteOpenPopUpCommand);
            ClosePopUpCommand = new ViewModelCommand(ExecuteClosePopUpCommand);
            SelectSeatCommand = new ViewModelCommand(ExecuteSelectSeatCommand);
            ApplyPromocodeCommand = new ViewModelCommand(ExecuteApplyPromocodeCommand);

            User = userRepository.GetByUsername(Thread.CurrentPrincipal.Identity.Name);
        }

        private void ExecuteApplyPromocodeCommand(object obj)
        {
            if (Promocode != null && IsActivePromocode == false)
            {
                PromocodeModel promocode = orderRepository.GetPromocode(Promocode);
                int discount = promocode.Discount;
                double discountValue = 1 - discount / 100.0;
                Sum *= discountValue;
                IsActivePromocode = true;
            }
        }

        private void ExecuteSelectSeatCommand(object obj)
        {
            SeatModel seat = obj as SeatModel;
            if (SelectedSeats.Contains(seat))
            {
                SelectedSeats.Remove(seat);
                Sum -= seat.Cost;
            }
            else
            {
                SelectedSeats.Add(seat);
                Sum += seat.Cost;
            }
        }

        private void ExecuteOpenPopUpCommand(object obj)
        {
            PopupIsOpen = true;
        }

        private void ExecuteClosePopUpCommand(object obj)
        {
            PopupIsOpen = false;
        }

        public void loadData()
        {
            int hall = Session.HallId;
            Seats = orderRepository.GetSeats(hall);
        }
        private void ExecuteMakeOrderCommand(object obj)
        {
            Session = obj as SessionModel;
            
            if (Session == null)
            {
                dynamic data = obj;
                string filmName = data.FilmName;
                DateTime sessionTime = data.SessionTime;
                Session = sessionRepository.SearchSession(filmName, sessionTime);
            }
            else 
            {
                Session = sessionRepository.SearchSession(Session.Name, Session.Date);
            }
            loadData();

            OrderView edit = new OrderView();
            edit.DataContext = this;

            if (edit.ShowDialog() == true) // if ok
            {
                OrderModel u = new OrderModel();
                u.UserId = User.Id;
                u.SessionId = Session.Id;
                u.Sum = Sum;
                const string chars = "0123456789";
                const int length = 10;
                Number = Convert.ToInt64(new string(Enumerable.Repeat(chars, length).Select(s => s[new Random().Next(s.Length)]).ToArray()));
                u.Number = Number;
                u.OrderDate = DateTime.Now;
                if (Promocode != null)
                {
                    PromocodeModel promocode = orderRepository.GetPromocode(Promocode);
                    u.PromocodeId = promocode.Id;
                }
                orderRepository.Add(u);
                
                for (int i = 0; i < SelectedSeats.Count; i++) 
                {
                    OrderSeatsModel m = new OrderSeatsModel();
                    List<OrderModel> order = orderRepository.SearchOrder(Number);
                    m.OrderId = order.FirstOrDefault().Id;
                    m.SeatId = SelectedSeats[i].Id;
                    orderRepository.AddSeats(m);
                }

                Sum = 0;
                Promocode = string.Empty;
                SelectedSeats.Clear();
                MessageBox.Show("Билет успешно куплен!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                Sum = 0;
                Promocode = string.Empty;
                SelectedSeats.Clear();
            }
        }
    }
}
