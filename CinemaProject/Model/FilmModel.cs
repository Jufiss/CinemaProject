using LoginForm.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace LoginForm.Model
{
    public class FilmModel
    {
        public int Id { get; set; }
        [DisplayName ("Название")]
        public string Name { get; set; }
        [DisplayName("Длительность")]
        public int Duration { get; set; }
        public TimeSpan DurationTime { get; set; }
        public int GenreId { get; set; }
        [DisplayName("Начало проката")]
        public DateTime DateStart { get; set; }
        [DisplayName("Конец проката")]
        public DateTime DateEnd { get; set; }
        [DisplayName("Постер")]
        public byte[]? Poster { get; set; }
        [DisplayName("Жанр")]
        public string Genres { get; set; }
        [DisplayName("Режиссер")]
        public string? Director { get; set; }
        [DisplayName("Описание")]
        public string? Plot { get; set; }
        [DisplayName("Страна")]
        public string? Country { get; set; }
        public FilmModel() { }

        public FilmModel(Film u)
        {
            Id = u.Id;
            Name = u.Name;
            Duration = u.Duration;
            DurationTime = TimeSpan.FromMinutes(u.Duration);
            GenreId = u.GenreId;
            DateStart = u.StartDate;
            DateEnd = u.EndDate;
            Genres = u.Genre.Name;
            Poster = u.Poster;
            Director = u.Director;
            Plot = u.Plot;
            Country = u.Country;
        }
    }
}
