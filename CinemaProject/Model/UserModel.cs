using LoginForm.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Windows.Media;

namespace LoginForm.Model
{
    public class UserModel
    {
        public int Id { get; set; }
        [DisplayName("Имя")]
        public string Name { get; set; } = null!;
        [DisplayName("Почта")]
        public string Email { get; set; } = null!;
        [DisplayName("Логин")]
        public string Login { get; set; } = null!;
        [DisplayName("Пароль")]
        public string Password { get; set; } = null!;
        [DisplayName("Категория")]
        public string Categories { get; set; }
        public int CategoryId { get; set; }
        public byte[]? Image { get; set; }
        public UserModel() { }

        public UserModel(User u)
        {
            Id = u.Id;
            Name = u.Name;
            Email = u.Email;
            Login = u.Login;
            Password = u.Password;
            Categories = u.Category.Name;
            CategoryId = u.CategoryId;
            Image = u.Image;
        }
    }
}
