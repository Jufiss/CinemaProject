using LoginForm.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginForm.Model
{
    public class SeatCategoryModel
    {
        public int Id { get; set; }
        [DisplayName("Наименование")]
        public string Name { get; set; } = null!;
        [DisplayName("Стоимость")]
        public int Cost { get; set; }

        public SeatCategoryModel() { }

        public SeatCategoryModel(Category u)
        {
            Id = u.Id;
            Name = u.Name;
            Cost = u.Cost;
        }
    }
}
