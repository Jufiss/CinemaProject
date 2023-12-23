using LoginForm.Interfaces;
using LoginForm.Model;
using LoginForm.Models;
using LoginForm.Repositories;
using LoginForm.View;
using Microsoft.VisualBasic.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace LoginForm.ViewModel
{
    public class FilmsViewModel : ViewModelBase
    {
        private FilmModel _currentFilmModel;
        List<FilmModel> allFilms;
        List<GenresModel> _genres;
        private IFilmsRepository filmRepository;
        private string _text;

        private string _name;
        private byte[]? _poster;
        private DateTime _startDate;
        private DateTime _endDate;
        private int _duration;
        private int _genreId;
        private string _director;
        private string _country;
        private string _plot;

        public List<FilmModel> AllFilms
        {
            get => allFilms;
            set
            {
                allFilms = value;
                OnPropertyChanged(nameof(AllFilms));
            }
        }

        public FilmModel CurrentFilmModel
        {
            get => _currentFilmModel;
            set
            {
                _currentFilmModel = value;
                OnPropertyChanged(nameof(CurrentFilmModel));
            }
        }
        public string TextBox
        {
            get => _text;
            set
            {
                _text = value;
                OnPropertyChanged(nameof(TextBox));
            }
        }
        public List<GenresModel> Genres
        {
            get => _genres;
            set
            {
                _genres = value;
                OnPropertyChanged(nameof(Genres));
            }
        }

        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }
        public byte[]? Poster { get => _poster; set { _poster = value; OnPropertyChanged(nameof(Poster)); } }
        public DateTime StartDate { get => _startDate; set { _startDate = value; OnPropertyChanged(nameof(StartDate)); } }
        public DateTime EndDate { get => _endDate; set { _endDate = value; OnPropertyChanged(nameof(EndDate)); } }
        public int Duration { get => _duration; set { _duration = value; OnPropertyChanged(nameof(Duration)); } }
        public int GenreId { get => _genreId; set { _genreId = value; OnPropertyChanged(nameof(GenreId)); } }

        public ICommand SerchFilmViewCommand { get; }
        public ICommand UpdateFilmViewCommand { get; }
        public ICommand AddFilmViewCommand { get; }
        public ICommand DeleteFilmViewCommand { get; }
        public ICommand UploadImageCommand { get; }
        public string Director { get => _director; set { _director = value; OnPropertyChanged(nameof(Director));} }
        public string Country { get => _country; set { _country = value; OnPropertyChanged(nameof(Country)); }}
        public string Plot { get => _plot; set { _plot = value; OnPropertyChanged(nameof(Plot)); }}

        public FilmsViewModel()
        {
            filmRepository = new FilmsRepository();
            CurrentFilmModel = new FilmModel();
            loadData();
            SerchFilmViewCommand = new ViewModelCommand(ExecuteSerchFilmViewCommand);
            AddFilmViewCommand = new ViewModelCommand(ExecuteAddFilmViewCommand);
            UpdateFilmViewCommand = new ViewModelCommand(ExecuteUpdateFilmViewCommand);
            DeleteFilmViewCommand = new ViewModelCommand(ExecuteDeleteFilmViewCommand);
            UploadImageCommand = new ViewModelCommand(ExecuteUploadImageCommand);
        }

        private void ExecuteUploadImageCommand(object obj)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "Image files|*.bmp;*.jpg;*.png;*.jpeg;";
            fileDialog.FilterIndex = 1;

            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                Poster = File.ReadAllBytes(fileDialog.FileName);
            }
        }

        private void ExecuteDeleteFilmViewCommand(object obj)
        {
            filmRepository.Delete(CurrentFilmModel.Id);
            loadData();
        }

        private void ExecuteUpdateFilmViewCommand(object obj)
        {
            AddFilmView edit = new AddFilmView();
            edit.DataContext = this;
            Name = CurrentFilmModel.Name;
            Duration = CurrentFilmModel.Duration;
            StartDate = CurrentFilmModel.DateStart;
            EndDate = CurrentFilmModel.DateEnd;
            GenreId = CurrentFilmModel.GenreId;
            Director = CurrentFilmModel.Director;
            Country = CurrentFilmModel.Country;
            Plot = CurrentFilmModel.Plot;
            if (CurrentFilmModel.Poster != null)
            {
                Poster = CurrentFilmModel.Poster;
            }
            else
                Poster = null;

            if (edit.ShowDialog() == true) // if ok
            {
                FilmModel u = new FilmModel();
                u.Id = CurrentFilmModel.Id;
                u.Name = Name;
                u.Duration = Duration;
                u.DateStart = StartDate;
                u.DateEnd = EndDate;
                u.GenreId = GenreId;
                u.Director = Director;
                u.Country = Country;
                u.Plot = Plot;
                u.Poster = Poster;
                if (Name != null && Duration != 0 && Director != null && Country != null && Plot != null)
                    filmRepository.Update(u);
                else
                    System.Windows.MessageBox.Show("Не все поля заполнены. Пожалуйста, заполните все поля о фильме.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            loadData();
        }

        private void ExecuteAddFilmViewCommand(object obj)
        {
            Name = null;
            Duration = 0;
            Poster = null;
            Director = null;
            Country = null;
            Plot = null;
            StartDate = DateTime.Now;
            EndDate = DateTime.Now;

            AddFilmView edit = new AddFilmView();
            edit.DataContext = this;

            if (edit.ShowDialog() == true) // if ok
            {
                FilmModel u = new FilmModel();
                u.Name = Name;
                u.Duration = Duration;
                u.DateStart = StartDate;
                u.DateEnd = EndDate;
                u.GenreId = GenreId;
                u.Poster = Poster;
                u.Director = Director;
                u.Country = Country;
                u.Plot = Plot;

                if (Name != null && Duration != 0 && Director != null && Country != null && Plot != null)
                    filmRepository.Add(u);
                else
                    System.Windows.MessageBox.Show("Не все поля заполнены. Пожалуйста, заполните все поля о фильме.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ExecuteSerchFilmViewCommand(object obj)
        {
            AllFilms = filmRepository.Search(TextBox);
        }

        private void loadData()
        {
            AllFilms = filmRepository.GetAllFilms();
            Genres = filmRepository.GetAllGenres();
        }
    }
}
