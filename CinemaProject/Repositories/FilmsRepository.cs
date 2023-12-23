using LoginForm.Interfaces;
using LoginForm.Model;
using LoginForm.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace LoginForm.Repositories
{
    public class FilmsRepository : RepositoryBase, IFilmsRepository
    {
        public void Add(FilmModel filmModel)
        {
            db.Films.Add(new Film()
            {
                Id = filmModel.Id,
                Name = filmModel.Name,
                Duration = filmModel.Duration,
                GenreId = filmModel.GenreId,
                EndDate = filmModel.DateEnd,
                StartDate = filmModel.DateStart,
                Poster = filmModel.Poster,
            });
            Save();
        }

        public void Delete(int Id)
        {
            Film f = db.Films.Find(Id);
            if (f != null)
            {
                db.Films.Remove(f);
                Save();
            }
        }

        public List<FilmModel> GetAllFilms()
        {
            return db.Films.Include(u => u.Genre).ToList().Select(i => new FilmModel(i)).ToList();
        }

        public List<FilmModel> GetSoonFilms(DateTime date)
        {
            date = date.AddDays(5);
            DateTime dateEnd = date.AddDays(35);
            return db.Films.Include(u => u.Genre).ToList().Where(f => f.StartDate >= date && f.StartDate <= dateEnd).Select(i => new FilmModel(i)).ToList();
        }

        public List<GenresModel> GetAllGenres()
        {
            return db.Genres.ToList().Select(i => new GenresModel(i)).ToList();
        }

        public FilmModel GetByName(string name)
        {
            FilmModel film = db.Films
                .Where(filmModel => filmModel.Name == name)
                .Select(filmModel => new FilmModel
                {
                    Id = filmModel.Id,
                    Name = filmModel.Name,
                    Duration = filmModel.Duration,
                    GenreId = filmModel.GenreId,
                    DateEnd = filmModel.EndDate,
                    DateStart = filmModel.StartDate,
                    Poster = filmModel.Poster,
                    Director = filmModel.Director,
                    Country = filmModel.Country,
                    Plot = filmModel.Plot,
                    Genres = filmModel.Genre.Name,
                    DurationTime = TimeSpan.FromMinutes(filmModel.Duration),
        })
                .FirstOrDefault();
            return film;
        }

        public List<FilmModel> Search(string name)
        {
            var request = db.Films
                .Where(filmModel => filmModel.Name.ToUpper().Contains(name.ToUpper()))
                .Select(filmModel => new FilmModel
                {
                    Id = filmModel.Id,
                    Name = filmModel.Name,
                    Duration = filmModel.Duration,
                    GenreId = filmModel.GenreId,
                    DateEnd = filmModel.EndDate,
                    DateStart = filmModel.StartDate,
                    Poster = filmModel.Poster,
                })
                .ToList();
            return request;
        }

        public void Update(FilmModel filmModel)
        {
            Film f = db.Films.Find(filmModel.Id);
            f.Id = filmModel.Id;
            f.Name = filmModel.Name;
            f.Duration = filmModel.Duration;
            f.GenreId = filmModel.GenreId;
            f.EndDate = filmModel.DateEnd;
            f.StartDate = filmModel.DateStart;
            f.Poster = filmModel.Poster;
            Save();
        }

        public bool Save()
        {
            if (db.SaveChanges() > 0)
                return true;
            return false;
        }
    }
}
