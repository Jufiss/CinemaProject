using LoginForm.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginForm.Model
{
    public class GenresModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public GenresModel() { }

        public GenresModel(Genre u)
        {
            Id = u.Id;
            Name = u.Name;
        }
    }
}
