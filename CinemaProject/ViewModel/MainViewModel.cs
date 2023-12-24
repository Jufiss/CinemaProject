using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LoginForm.Repositories;
using LoginForm.Model;
using System.Threading;
using System.Windows;
using FontAwesome.Sharp;
using System.Windows.Input;
using System.Security;
using LoginForm.Interfaces;
using LoginForm.View;
using CinemaProject;

namespace LoginForm.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private UserModel _currentUserModel;
        private ViewModelBase _currentChildView;
        private string _caption;
        private IconChar _icon;

        private IUserRepository userRepository;

        private Visibility _visibilityAdmin;
        private Visibility _visibilityWorker;

        public UserModel CurrentUserModel 
        { get => _currentUserModel;
            set
            {
                _currentUserModel = value;
                OnPropertyChanged(nameof(CurrentUserModel));
            }
        }

        public ViewModelBase CurrentChildView 
        { 
            get => _currentChildView;
            set
            {
                _currentChildView = value;
                OnPropertyChanged(nameof(CurrentChildView));
            }
        }
        public string Caption 
        { 
            get => _caption; 
            set
            {
                _caption = value;
                OnPropertyChanged(nameof(Caption));
            }
        }
        public IconChar Icon 
        { 
            get => _icon; 
            set
            {
                _icon = value;
                OnPropertyChanged(nameof(Icon));
            }
        }
        public ICommand ShowUsersViewCommand { get; }
        public ICommand ShowScheduleViewCommand { get; }
        public ICommand ShowFilmsViewCommand { get; }
        public ICommand ShowAccountViewCommand { get; }
        public ICommand ShowSessionsViewCommand { get; }
        public ICommand ShowCheckTicketsViewCommand { get; }
        public ICommand ShowFilmPageViewCommand { get; }
        public ICommand ShowSoonFilmsViewCommand { get; }
        public ICommand ShowReportMoneyViewCommand { get; }
        public ICommand LogOutViewCommand { get; }
        public Visibility VisibilityAdmin { get => _visibilityAdmin; set { _visibilityAdmin = value; OnPropertyChanged(nameof(VisibilityAdmin)); } }

        public Visibility VisibilityWorker { get => _visibilityWorker; set { _visibilityWorker = value; OnPropertyChanged(nameof(VisibilityWorker)); } }

        public MainViewModel()
        {
            userRepository = new UserRepository();
            CurrentUserModel = new UserModel();

            ShowUsersViewCommand = new ViewModelCommand(ExecuteShowUsersViewCommand);
            ShowFilmsViewCommand = new ViewModelCommand(ExecuteShowFilmsViewCommand);
            ShowScheduleViewCommand = new ViewModelCommand(ExecuteShowScheduleViewCommand);
            ShowAccountViewCommand = new ViewModelCommand(ExecuteShowAccountViewCommand);
            ShowSessionsViewCommand = new ViewModelCommand(ExecuteShowSessionsViewCommand);
            ShowCheckTicketsViewCommand = new ViewModelCommand(ExecuteShowCheckTicketsViewCommand);
            ShowFilmPageViewCommand = new ViewModelCommand(ExecuteShowFilmPageViewCommand);
            ShowSoonFilmsViewCommand = new ViewModelCommand(ExecuteSoonFilmsViewCommand);
            ShowReportMoneyViewCommand = new ViewModelCommand(ExecuteShowReportMoneyViewCommand);
            LogOutViewCommand = new ViewModelCommand(ExecuteLogOutViewCommand);

            ExecuteShowScheduleViewCommand(null);

            loadMenu();
        }

        private void ExecuteLogOutViewCommand(object obj)
        {
            var loginView = new LoginView();
            loginView.Show();

            foreach (System.Windows.Window window in Application.Current.Windows)
            {
                if (window.GetType() == typeof(MainWindow))
                {
                    window.Close();
                    break;
                }
            }
        }

        public void loadMenu()
        {
            CurrentUserModel = userRepository.GetByUsername(Thread.CurrentPrincipal.Identity.Name);

            if (CurrentUserModel.CategoryId == 1) 
            {
                VisibilityAdmin = Visibility.Hidden;
                VisibilityWorker = Visibility.Hidden;
            }
            else if (CurrentUserModel.CategoryId == 2)
            {
                VisibilityAdmin = Visibility.Hidden;
                VisibilityWorker = Visibility.Visible;
            }
            else if (CurrentUserModel.CategoryId == 3)
            {
                VisibilityAdmin = Visibility.Visible;
                VisibilityWorker = Visibility.Visible;
            }
        }
        private void ExecuteShowReportMoneyViewCommand(object obj)
        {
            CurrentChildView = new ReportMoneyViewModel();
            Caption = "Отчеты";
            Icon = IconChar.ChartSimple;
        }

        private void ExecuteSoonFilmsViewCommand(object obj)
        {
            CurrentChildView = new SoonFilmsViewModel();
            Caption = "Скоро в кино";
            Icon = IconChar.Clapperboard;
        }

        private void ExecuteShowFilmPageViewCommand(object obj)
        {
            string name = obj as string;
            CurrentChildView = new FilmPageViewModel(name);
            Caption = "Выбор сеанса";
            Icon = IconChar.PhotoFilm;
        }

        private void ExecuteShowCheckTicketsViewCommand(object obj)
        {
            CurrentChildView = new CheckTicketsViewModel();
            Caption = "Проверить билет";
            Icon = IconChar.Ticket;
        }

        private void ExecuteShowSessionsViewCommand(object obj)
        {
            CurrentChildView = new SessionsViewModel();
            Caption = "Управление сеансами";
            Icon = IconChar.Film;
        }

        private void ExecuteShowAccountViewCommand(object obj)
        {
            CurrentChildView = new AccountViewModel();
            Caption = "Мой аккаунт";
            Icon = IconChar.UserCircle;
        }

        private void ExecuteShowScheduleViewCommand(object obj)
        {
            CurrentChildView = new ScheduleViewModel();
            Caption = "Расписание";
            Icon = IconChar.List;
        }

        private void ExecuteShowFilmsViewCommand(object obj)
        {
            CurrentChildView = new FilmsViewModel();
            Caption = "Управление фильмами";
            Icon = IconChar.Video;
        }

        private void ExecuteShowUsersViewCommand(object obj)
        {
            CurrentChildView = new UsersViewModel();
            Caption = "Управление пользователями";
            Icon = IconChar.User;
        }
    }
}
