using LoginForm.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginForm.Model
{
    public class UserCategoryModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public UserCategoryModel() { }

        public UserCategoryModel(UserCategory u)
        {
            Id = u.Id;
            Name = u.Name;
        }
    }
}
