using LoginForm.Model;
using LoginForm.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginForm.Interfaces
{
    public interface ISessionRepository
    {
        public void Add(SessionModel sessionModel);

        public void Delete(int Id);

        public ObservableCollection<SessionModel> GetAllSessions();
        public ObservableCollection<SessionModel> GetByDate(DateTime date);
        public ObservableCollection<SessionModel> GetByName(string name);
        public SessionModel SearchSession(string name, DateTime date);
        public ObservableCollection<SessionModel> GetAllByDate(DateTime date);
        public List<HallModel> GetAllHalls();
        public List<FilmModel> GetAllFilms();

        public List<SessionModel> Search(DateTime date);
        public bool IsFree(DateTime date, int filmId, int hall);

        public void Update(SessionModel sessionModel);

        public bool Save();
    }
}
