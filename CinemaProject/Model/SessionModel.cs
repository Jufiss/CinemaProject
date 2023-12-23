using LoginForm.Models;
using Microsoft.VisualBasic.Logging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace LoginForm.Model
{
    public class SessionModel
    {
        public int Id { get; set; }
        [DisplayName("Дата и время")]
        public DateTime Date { get; set; }

        public int FilmId { get; set; }

        public int HallId { get; set; }
        [DisplayName("Фильм")]
        public string Name { get; set; } = null!;
        [DisplayName("Зал")]
        public int Halls { get; set; }
        [DisplayName("Длительность")]
        public int Duration { get; set; }
        public TimeSpan DurationTime { get; set; }
        [DisplayName("Жанр")]
        public string Genres { get; set; }
        [DisplayName("Постер")]
        public byte[]? Poster { get; set; }
        public ObservableCollection<int> HallList { get; set; }
        public ObservableCollection<DateTime> SessionTimes { get; set; }
        public string? Director { get; set; }
        public string? Plot { get; set; }
        public string? Country { get; set; }
        public List<int> HallsList { get; set; }
        public SessionModel() { }
        public SessionModel(Session u)
        {
            Id = u.Id;
            Date = u.Date;
            FilmId = u.FilmId;
            HallId = u.HallId;
            Name = u.Film.Name;
            Duration = u.Film.Duration;
            DurationTime = TimeSpan.FromMinutes(u.Film.Duration);
            Genres = u.Film.Genre.Name;
            Poster = u.Film.Poster;
            Halls = u.Hall.Number;
            Director = u.Film.Director;
            Plot = u.Film.Plot;
            Country = u.Film.Country;
        }
    }
}
