using LoginForm.Model;
using LoginForm.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginForm.Interfaces
{
    public interface IFilmsRepository
    {
        public void Add(FilmModel filmModel);

        public void Delete(int Id);

        public List<FilmModel> GetAllFilms();
        public List<FilmModel> GetSoonFilms(DateTime date);
        public List<GenresModel> GetAllGenres();
        public FilmModel GetByName(string name);

        public List<FilmModel> Search(string name);

        public void Update(FilmModel filmModel);

        public bool Save();
    }
}
