using CsvHelper.Configuration;
using CsvHelper;
using LiveCharts;
using LiveCharts.Wpf;
using LoginForm.Interfaces;
using LoginForm.Model;
using LoginForm.Repositories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Printing;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;

namespace LoginForm.ViewModel
{
    public class ReportMoneyViewModel : ViewModelBase
    {
        private DateTime _startDate;
        private DateTime _endDate;
        private List<OrderModel> _orders;
        private List<OrderSeatsModel> _orderSeats;
        private Dictionary<string, ObservableCollection<SessionModel>> _sessions;
        private ObservableCollection<SessionModel> _allSessions;
        private Dictionary<string, double> _ordersBySessions;
        private Dictionary<string, double> _seatsBySessions;
        private IOrderRepository orderRepository;
        private ISessionRepository sessionRepository;
        private SeriesCollection _seriesCollection;
        private SeriesCollection _seriesCollection2;
        private string _filePath;

        public DateTime StartDate { get => _startDate; set { _startDate = value; OnPropertyChanged(nameof(StartDate)); } }
        public DateTime EndDate { get => _endDate; set { _endDate = value; OnPropertyChanged(nameof(EndDate)); } }
        public List<OrderModel> Orders { get => _orders; set { _orders = value; OnPropertyChanged(nameof(Orders)); } }

        public SeriesCollection SeriesCollection { get => _seriesCollection; set { _seriesCollection = value; OnPropertyChanged(nameof(SeriesCollection)); } }
        public SeriesCollection SeriesCollection2 { get => _seriesCollection2; set { _seriesCollection2 = value; OnPropertyChanged(nameof(SeriesCollection2)); } }

        public Dictionary<string, ObservableCollection<SessionModel>> Sessions { get => _sessions; set { _sessions = value; OnPropertyChanged(nameof(Sessions)); } }

        public ObservableCollection<SessionModel> AllSessions { get => _allSessions; set { _allSessions = value; } }

        public Dictionary<string, double> SeatsBySessions { get => _seatsBySessions; set { _seatsBySessions = value; OnPropertyChanged(nameof(SeatsBySessions)); } }

        public List<OrderSeatsModel> OrderSeats { get => _orderSeats; set { _orderSeats = value; OnPropertyChanged(nameof(OrderSeats)); } }
        public ICommand ShowReportMoneyCommand { get; private set; }
        public ICommand SaveReportMoneyCommand { get; private set; }
        public ICommand ShowReportSeatsCommand { get; private set; }
        public ICommand SaveReportSeatsCommand { get; private set; }
        public ReportMoneyViewModel()
        {
            orderRepository = new OrderRepository();
            sessionRepository = new SessionRepository();

            ShowReportMoneyCommand = new ViewModelCommand(ExecuteShowReportMoneyCommand);
            SaveReportMoneyCommand = new ViewModelCommand(ExecuteSaveReportMoneyCommand);
            ShowReportSeatsCommand = new ViewModelCommand(ExecuteShowReportSeatsCommand);
            SaveReportSeatsCommand = new ViewModelCommand(ExecuteSaveReportSeatsCommand);
        }

        private void ExecuteSaveReportMoneyCommand(object obj)
        {
            _filePath = OpenFolderDialog();

            if (!string.IsNullOrEmpty(_filePath))
            {
                GenerateCsvReport(_ordersBySessions, _filePath);
            }
        }

        private void ExecuteShowReportMoneyCommand(object obj)
        {
            loadData();
        }

        private void ExecuteSaveReportSeatsCommand(object obj)
        {
            _filePath = OpenFolderDialog();

            if (!string.IsNullOrEmpty(_filePath))
            {
                GenerateCsvReport(_seatsBySessions, _filePath);
            }
        }

        private void ExecuteShowReportSeatsCommand(object obj)
        {
            loadData2();
        }
        public void loadData()
        {
            var allSessions = sessionRepository.GetAllSessions();
            var sessionsGroupedByFilm = allSessions.GroupBy(session => session.Name)
                .ToDictionary(group => group.Key, group => new ObservableCollection<SessionModel>(group));

            Sessions = new Dictionary<string, ObservableCollection<SessionModel>>(sessionsGroupedByFilm);
            
            Orders = orderRepository.OrdersByTwoDates(StartDate, EndDate);

            _ordersBySessions = new Dictionary<string, double>();

            foreach (var order in Orders)
            {
                var sessionName = order.Sessions;

                if (!string.IsNullOrEmpty(sessionName))
                {
                    if (_ordersBySessions.ContainsKey(sessionName))
                        _ordersBySessions[sessionName] += order.Sum;
                    else
                        _ordersBySessions[sessionName] = order.Sum;
                }
            }
            SeriesCollection = new SeriesCollection();
            foreach (var session in Sessions)
            {
                if (_ordersBySessions.TryGetValue(session.Key, out var count))
                {
                    SeriesCollection.Add(new PieSeries
                    {
                        Title = session.Key,
                        Values = new ChartValues<double> { count }
                    });
                }
            }
        }

        public void loadData2()
        {
            AllSessions = sessionRepository.GetAllSessions();
            Orders = orderRepository.OrdersByTwoDates(StartDate, EndDate);
            OrderSeats = orderRepository.GetOrderSeats();

            _seatsBySessions = new Dictionary<string, double>();
            foreach (var session in AllSessions)
            {
                int totalSeats = 0;
                switch (session.HallId)
                {
                    case 1: totalSeats = 12; break;
                    case 2: totalSeats = 15; break;
                    case 3: totalSeats = 20; break;
                }
                double soldSeats = 0;

                foreach (var order in Orders.Where(o => o.SessionId == session.Id))
                {
                    var seatsInOrder = OrderSeats.Where(o => o.OrderId == order.Id);
                        soldSeats += seatsInOrder.Count();
                }
                double occupancyPercentage = (soldSeats / totalSeats) * 100;

                _seatsBySessions.Add(session.Name+" "+session.Date, occupancyPercentage);
            }

            SeriesCollection2 = new SeriesCollection();
            foreach (var session in AllSessions)
            {
                if (_seatsBySessions.TryGetValue(session.Name + " " + session.Date, out var count))
                {
                    if (count != 0)
                    {
                        SeriesCollection2.Add(new ColumnSeries
                        {
                            Title = session.Name + " " + session.Date,
                            Values = new ChartValues<double> {count},
                        });
                    }
                }
            }
        }
        public string OpenFolderDialog()
        {
            var saveFileDialog = new SaveFileDialog
            {
                Title = "Сохранить файл как",
                Filter = "Все файлы (*.*)|*.*",
                FileName = "Report.csv",
                CheckPathExists = true
            };

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                return saveFileDialog.FileName;
            }

            return null;
        }
        public static void GenerateCsvReport(Dictionary<string, double> data, string filePath)
        {
            var csvConfig = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                Delimiter = ";",
                ShouldUseConstructorParameters = _ => false
            };

            using (var writer = new StreamWriter(filePath, false, Encoding.UTF8))
            using (var csv = new CsvWriter(writer, csvConfig))
            {
                csv.WriteRecords(data.Select(item => new { Name = item.Key, Count = item.Value }));
            }
        }
    }
}
