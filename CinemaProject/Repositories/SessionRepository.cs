using LoginForm.Interfaces;
using LoginForm.Model;
using LoginForm.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Windows;

namespace LoginForm.Repositories
{
    public class SessionRepository : RepositoryBase, ISessionRepository
    {
        public void Add(SessionModel sessionModel)
        {
            db.Sessions.Add(new Session()
            {
                Id = sessionModel.Id,
                Date = sessionModel.Date,
                HallId = sessionModel.HallId,
                FilmId = sessionModel.FilmId,
            });
            Save();
        }

        public void Delete(int Id)
        {
            Session f = db.Sessions.Find(Id);
            if (f != null)
            {
                db.Sessions.Remove(f);
                Save();
            }
        }

        public ObservableCollection<SessionModel> GetAllSessions()
        {
            List<SessionModel> sessionList = db.Sessions.Include(u => u.Hall).Include(u => u.Film).Include(u => u.Film.Genre).ToList().Select(i => new SessionModel(i)).ToList();
            ObservableCollection<SessionModel> sessions = new ObservableCollection<SessionModel>(sessionList);
            return sessions;
        }

        public List<HallModel> GetAllHalls()
        {
            return db.Halls.ToList().Select(i => new HallModel(i)).ToList();
        }
        public List<FilmModel> GetAllFilms()
        {
            return db.Films.Include(u => u.Genre).ToList().Select(i => new FilmModel(i)).ToList();
        }

        public ObservableCollection<SessionModel> GetAllByDate(DateTime date)
        {
            DateTime endDate = date.Date.AddDays(1).AddTicks(-1);
            List<SessionModel> session = db.Sessions
                .Where(i => i.Date >= date && i.Date <= endDate)
                .Select(i => new SessionModel
                {
                    Id = i.Id,
                    Date = i.Date,
                    Name = i.Film.Name,
                    Duration = i.Film.Duration,
                    DurationTime = TimeSpan.FromMinutes(i.Film.Duration),
                    Genres = i.Film.Genre.Name,
                    HallId = i.HallId,
                    FilmId = i.FilmId,
                    Poster = i.Film.Poster,
                    Halls = i.Hall.Number,
                })
                 .ToList();
            ObservableCollection<SessionModel> sessions = new ObservableCollection<SessionModel>(session);
            return sessions;
        }

        public ObservableCollection<SessionModel> GetByDate(DateTime date)
        {
            DateTime endDate = date.Date.AddDays(1).AddTicks(-1);
            List<SessionModel> session = db.Sessions
                .Where(i => i.Date >= date && i.Date <= endDate)
                .Select(i => new SessionModel
                {
                    Id = i.Id,
                    Date = i.Date,
                    Name = i.Film.Name,
                    Duration = i.Film.Duration,
                    DurationTime = TimeSpan.FromMinutes(i.Film.Duration),
                    Genres = i.Film.Genre.Name,
                    HallId = i.HallId,
                    FilmId = i.FilmId,
                    Poster = i.Film.Poster,
                    Halls = i.Hall.Number,
                })
                .GroupBy(s => s.Name)
                 .Select(g => new SessionModel
                 {
                     Id = g.First().Id,
                     Date = g.First().Date,
                     Name = g.Key, // Вместо g.First().Name
                     Duration = g.First().Duration,
                     DurationTime = TimeSpan.FromMinutes(g.First().Duration),
                     Genres = g.First().Genres,
                     HallId = g.First().HallId,
                     FilmId = g.First().FilmId,
                     Poster = g.First().Poster,
                     Halls = g.First().Halls,
                     HallList = new ObservableCollection<int>(g.Select(s => s.Halls)),
                     SessionTimes = new ObservableCollection<DateTime>(g.Select(s => s.Date))
                 })
                .ToList();
            ObservableCollection<SessionModel> sessions = new ObservableCollection<SessionModel>(session);
            return sessions;
        }
        public ObservableCollection<SessionModel> GetByName(string name)
        {
            List<SessionModel> sessionsList = db.Sessions
                .Where(i => i.Film.Name == name)
                .Select(i => new SessionModel
                {
                    Id = i.Id,
                    Date = i.Date,
                    Name = i.Film.Name,
                    Duration = i.Film.Duration,
                    HallId = i.HallId,
                    FilmId = i.FilmId,
                    Halls = i.Hall.Number,
                    DurationTime = TimeSpan.FromMinutes(i.Film.Duration),
                })
                .ToList();
            ObservableCollection<SessionModel> sessions = new ObservableCollection<SessionModel>(sessionsList);
            return sessions;
        }
        public SessionModel SearchSession(string name, DateTime date) 
        {
            if (name == null)
                return null;
            SessionModel session = db.Sessions
                .Where(i => i.Film.Name == name && i.Date == date)
                .Select(i => new SessionModel
                {
                    Id = i.Id,
                    Date = i.Date,
                    Name = i.Film.Name,
                    Poster = i.Film.Poster,
                    Duration = i.Film.Duration,
                    HallId = i.HallId,
                    FilmId = i.FilmId,
                    Halls = i.Hall.Number,
                    DurationTime = TimeSpan.FromMinutes(i.Film.Duration),
                })
                .FirstOrDefault();
                return session;
        }

        public List<SessionModel> Search(DateTime date)
        {
            var request = db.Sessions
                .Where(i => i.Date == date)
                .Select(i => new SessionModel
                {
                    Id = i.Id,
                    Date = i.Date,
                    Name = i.Film.Name,
                    Duration = i.Film.Duration,
                    Genres = i.Film.Genre.Name,
                    HallId = i.HallId,
                    FilmId = i.FilmId,
                    Poster = i.Film.Poster,
                    Halls = i.Hall.Number,
                    DurationTime = TimeSpan.FromMinutes(i.Film.Duration),
                    Director = i.Film.Director,
                    Plot = i.Film.Plot,
                    Country = i.Film.Country,
                })
                .ToList();
            return request;
        }

        public bool IsFree(DateTime date, int filmId, int hall)
        {
            DateTime startDate = date;
            Film f = db.Films.Find(filmId);
            DateTime endDate = date.AddMinutes(f.Duration);
            if (date > f.EndDate || date < f.StartDate)
                return false;
            var request = db.Sessions
                .Where(i => i.HallId == hall && i.Date < endDate && i.Date.AddMinutes(i.Film.Duration) > startDate).FirstOrDefault();
            if (request == null)
                return true;
            else
            return false;
        }

        public void Update(SessionModel sessionModel)
        {
            Session f = db.Sessions.Find(sessionModel.Id);
            f.Id = sessionModel.Id;
            f.Date = sessionModel.Date;
            f.HallId = sessionModel.HallId;
            f.FilmId = sessionModel.FilmId;
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
