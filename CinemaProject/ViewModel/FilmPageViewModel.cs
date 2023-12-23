using LoginForm.Interfaces;
using LoginForm.Model;
using LoginForm.Models;
using LoginForm.Repositories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace LoginForm.ViewModel
{
    public class FilmPageViewModel: ViewModelBase
    {
        private string _name;
        private FilmModel _film;
        private ObservableCollection<SessionModel> _session;
        private string _error;
        private string _selectedSessionTime;
        private ISessionRepository sessionRepository;
        private IFilmsRepository filmsRepository;
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        public FilmModel Film { get => _film; set { _film = value; OnPropertyChanged(nameof(Film)); } }

        public string SelectedSessionTime { get => _selectedSessionTime; set { _selectedSessionTime = value; OnPropertyChanged(nameof(SelectedSessionTime)); } }

        public string Error { get => _error; set { _error = value; OnPropertyChanged(nameof(Error)); } }

        public ObservableCollection<SessionModel> Session { get => _session; set { _session = value; OnPropertyChanged(nameof(Session)); } }

        public FilmPageViewModel(string name) 
        {
            Name = name;
            sessionRepository = new SessionRepository();
            filmsRepository = new FilmsRepository();

            loadData();
        }

        public void loadData()
        {
            FilmModel filmByName = filmsRepository.GetByName(Name);
            ObservableCollection<SessionModel> sessionByName = sessionRepository.GetByName(Name);
            Film = filmByName;
            if (sessionByName.Count != 0)
            {
                Session = sessionByName;
                Error = string.Empty;
            }
            else Error = "В кино с " + Film.DateStart.ToString("dd.MM.yyyy");
        }
    }
}
