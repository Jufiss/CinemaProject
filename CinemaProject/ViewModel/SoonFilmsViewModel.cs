using LoginForm.Interfaces;
using LoginForm.Model;
using LoginForm.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginForm.ViewModel
{
    public class SoonFilmsViewModel : ViewModelBase
    {
        List<FilmModel> allFilms;
        private IFilmsRepository filmRepository;

        public List<FilmModel> AllFilms
        {
            get => allFilms;
            set
            {
                allFilms = value;
                OnPropertyChanged(nameof(AllFilms));
            }
        }

        public SoonFilmsViewModel()
        {
            filmRepository = new FilmsRepository();
            loadData();
        }
        private void loadData()
        {
            DateTime date = DateTime.Now;
            AllFilms = filmRepository.GetSoonFilms(date);
        }
    }
}
