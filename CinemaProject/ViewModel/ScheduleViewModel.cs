using FontAwesome.Sharp;
using LoginForm.Interfaces;
using LoginForm.Model;
using LoginForm.Models;
using LoginForm.Repositories;
using LoginForm.View;
using Microsoft.VisualBasic.Logging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Input.Manipulations;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace LoginForm.ViewModel
{
    public class ScheduleViewModel : ViewModelBase
    {
        private string _todayDate;
        private string _todayDateWeek;
        private string _secondDate;
        private string _secondDateWeek;
        private string _thirdDate;
        private string _thirdDateWeek;
        private string _fourthDate;
        private string _fourthDateWeek;

        private ISessionRepository sessionRepository;
        private IFilmsRepository filmRepository;
        private ObservableCollection<SessionModel> _sessions;
        private int _selectedSession;
        private int _selectedDate;
        private List<GenresModel> _genres;
        private bool _isEnabled;

        private readonly IOrderService _orderService;
        private GenresModel _selectedGenre;
        private int _selectedSessionTime;
        private string _error;
        public string TodayDate 
        { get => _todayDate;
            set
            {
                _todayDate = value;
                OnPropertyChanged(nameof(TodayDate));
            }
        }
        public string SecondDate 
        { get => _secondDate;
            set
            {
                _secondDate = value;
                OnPropertyChanged(nameof(SecondDate));
            }
        }
        public string ThirdDate 
        { get => _thirdDate;
            set
            {
                _thirdDate = value;
                OnPropertyChanged(nameof(ThirdDate));
            }
        }
        public string TodayDateWeek
        {
            get => _todayDateWeek;
            set
            {
                _todayDateWeek = value;
                OnPropertyChanged(nameof(TodayDateWeek));
            }
        }
        public string SecondDateWeek
        {
            get => _secondDateWeek;
            set
            {
                _secondDateWeek = value;
                OnPropertyChanged(nameof(SecondDateWeek));
            }
        }
        public string ThirdDateWeek
        {
            get => _thirdDateWeek;
            set
            {
                _thirdDateWeek = value;
                OnPropertyChanged(nameof(ThirdDateWeek));
            }
        }

        public ObservableCollection<SessionModel> Sessions { get => _sessions; set { _sessions = value; OnPropertyChanged(nameof(Sessions)); } }
        public int SelectedSession { get => _selectedSession; set { _selectedSession = value; OnPropertyChanged(nameof(SelectedSession)); } }
        public int SelectedDate { get => _selectedDate; set { _selectedDate = value; OnPropertyChanged(nameof(SelectedDate)); } }
        public int SelectedSessionTime { get => _selectedSessionTime; set { _selectedSessionTime = value; OnPropertyChanged(nameof(SelectedSessionTime)); } }

        public ICommand ShowFilmsOnDateCommand {  get; set; }
        public ICommand CanselFilterGenreCommand { get; set; }
        public string FourthDate { get => _fourthDate; set { _fourthDate = value; OnPropertyChanged(nameof(FourthDateWeek)); } }
        public string FourthDateWeek { get => _fourthDateWeek; set { _fourthDateWeek = value; OnPropertyChanged(nameof(FourthDateWeek)); } }

        public bool IsEnabled { get => _isEnabled; set { _isEnabled = value; OnPropertyChanged(nameof(IsEnabled)); } }

        public List<GenresModel> Genres { get => _genres; set { _genres = value; OnPropertyChanged(nameof(Genres)); } }
        public string Error { get => _error; set { _error = value; OnPropertyChanged(nameof(Error)); } }

        public GenresModel SelectedGenre { get => _selectedGenre; set { _selectedGenre = value; OnPropertyChanged(nameof(SelectedGenre)); SelectGenre(); } }

        public ScheduleViewModel() 
        {
            sessionRepository = new SessionRepository();
            filmRepository = new FilmsRepository();
            _orderService = new OrderViewModel();

            TodayDate = DateTime.Today.ToShortDateString();
            TodayDateWeek = " (" + DateTime.Today.ToString("dddd", new System.Globalization.CultureInfo("ru-RU")) + ")";
            SecondDate = DateTime.Today.AddDays(1).ToShortDateString();
            SecondDateWeek = " (" + DateTime.Today.AddDays(1).ToString("dddd", new System.Globalization.CultureInfo("ru-RU")) + ")";
            ThirdDate = DateTime.Today.AddDays(2).ToShortDateString();
            ThirdDateWeek = " (" + DateTime.Today.AddDays(2).ToString("dddd", new System.Globalization.CultureInfo("ru-RU")) + ")";
            FourthDate = DateTime.Today.AddDays(3).ToShortDateString();
            FourthDateWeek = " (" + DateTime.Today.AddDays(3).ToString("dddd", new System.Globalization.CultureInfo("ru-RU")) + ")";

            ShowFilmsOnDateCommand = new ViewModelCommand(ExecuteShowFilmsOnDateCommand);
            CanselFilterGenreCommand = new ViewModelCommand(ExecuteCanselFilterGenreCommand);

            Genres = filmRepository.GetAllGenres();

            ExecuteShowFilmsOnDateCommand(DateTime.Parse(TodayDate));
        }

        private void ExecuteCanselFilterGenreCommand(object obj)
        {
            var date = Sessions[0].Date.Date;
            Sessions = sessionRepository.GetByDate(date);
            Error = string.Empty;
        }

        private void ExecuteShowFilmsOnDateCommand(object obj)
        {
            DateTime date = DateTime.Parse(obj.ToString());
            ObservableCollection<SessionModel> sessionsByDate = sessionRepository.GetByDate(date);
            Sessions = sessionsByDate;
            Error = string.Empty;
        }


        private void SelectGenre()
        {
            var date = Sessions[0].Date.Date;
            var filteredSessions = sessionRepository.GetByDate(date).Where(u => u.Genres == SelectedGenre.Name).ToList();
            if (filteredSessions.Count > 0)
            {
                Sessions = new ObservableCollection<SessionModel>(filteredSessions);
                Error = string.Empty;
            }
            else
                Error = "Фильмы не найдены";
        }
    }
}
