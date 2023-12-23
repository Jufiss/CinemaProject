using LoginForm.Interfaces;
using LoginForm.Model;
using LoginForm.Models;
using LoginForm.Repositories;
using LoginForm.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace LoginForm.ViewModel
{
    public class SessionsViewModel : ViewModelBase
    {
        private SessionModel _selectedSession;
        ObservableCollection<SessionModel> allSessions;
        private ISessionRepository sessionRepository;
        private DateTime _searchDate;
        List<FilmModel> films;
        List<HallModel> halls;

        private string _date;
        private int _hallId;
        private int _filmId;
        public SessionModel SelectedSession
        {
            get => _selectedSession;
            set
            {
                _selectedSession = value;
                OnPropertyChanged(nameof(SelectedSession));
            }
        }
        public ObservableCollection<SessionModel> AllSessions
        {
            get => allSessions;
            set
            {
                allSessions = value;
                OnPropertyChanged(nameof(AllSessions));
            }
        }
        public DateTime SearchDate
        {
            get => _searchDate;
            set
            {
                _searchDate = value;
                OnPropertyChanged(nameof(SearchDate));
            }
        }
        public List<FilmModel> Films { get => films; set { films = value; OnPropertyChanged(nameof(Films)); } }
        public List<HallModel> Halls { get => halls; set { halls = value; OnPropertyChanged(nameof(Halls)); } }
        public string Date { get => _date; set { _date = value; OnPropertyChanged(nameof(Date)); } }
        public int HallId { get => _hallId; set { _hallId = value; OnPropertyChanged(nameof(HallId)); } }
        public int FilmId { get => _filmId; set { _filmId = value; OnPropertyChanged(nameof(FilmId)); } }
        public ICommand SearchSessionViewCommand { get; }
        public ICommand UpdateSessionViewCommand { get; }
        public ICommand AddSessionViewCommand { get; }
        public ICommand DeleteSessionViewCommand { get; }

        public SessionsViewModel()
        {
            sessionRepository = new SessionRepository();
            SelectedSession = new SessionModel();
            loadData();
            SearchSessionViewCommand = new ViewModelCommand(ExecuteSearchSessionViewCommand);
            AddSessionViewCommand = new ViewModelCommand(ExecuteAddSessionViewCommand);
            UpdateSessionViewCommand = new ViewModelCommand(ExecuteUpdateSessionViewCommand);
            DeleteSessionViewCommand = new ViewModelCommand(ExecuteDeleteSessionViewCommand);
        }

        private void ExecuteDeleteSessionViewCommand(object obj)
        {
            sessionRepository.Delete(SelectedSession.Id);
            loadData();
        }

        private void ExecuteUpdateSessionViewCommand(object obj)
        {
            AddSessionView edit = new AddSessionView();
            edit.DataContext = this;
            Date = SelectedSession.Date.ToString("g");
            HallId = SelectedSession.HallId;
            FilmId = SelectedSession.FilmId;

            if (edit.ShowDialog() == true) // if ok
            {
                SessionModel u = new SessionModel();
                u.Id = SelectedSession.Id;
                u.Date = DateTime.Parse(Date);
                u.HallId = HallId;
                u.FilmId = FilmId;
                sessionRepository.Update(u);
            }
            loadData();
        }

        private void ExecuteAddSessionViewCommand(object obj)
        {
            AddSessionView edit = new AddSessionView();
            edit.DataContext = this;

            if (edit.ShowDialog() == true) // if ok
            {
                SessionModel u = new SessionModel();
                u.Date = DateTime.Parse(Date);
                u.HallId = HallId;
                u.FilmId = FilmId;

                var isFree = sessionRepository.IsFree(DateTime.Parse(Date), FilmId, HallId);
                if (isFree == true)
                {
                    if (HallId != 0 && FilmId != 0)
                        sessionRepository.Add(u);
                    else
                        System.Windows.MessageBox.Show("Не все поля заполнены. Пожалуйста, заполните все поля о сеансе.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                    System.Windows.MessageBox.Show("Этот зал и время уже заняты другим сеансом или дата показа фильма не совпадает.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);

            }
        }

        private void ExecuteSearchSessionViewCommand(object obj)
        {
            AllSessions = sessionRepository.GetAllByDate(SearchDate);
        }

        private void loadData()
        {
            AllSessions = sessionRepository.GetAllSessions();
            Halls = sessionRepository.GetAllHalls();
            Films = sessionRepository.GetAllFilms();
        }
    }
}
