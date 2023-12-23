using LoginForm.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginForm.Model
{
    public class PromocodeModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Discount { get; set; }
        public PromocodeModel() { }

        public PromocodeModel(Promocode u)
        {
            Id = u.Id;
            Name = u.Name;
            Discount = u.Discount;
        }
    }
}
