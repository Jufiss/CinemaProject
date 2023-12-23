using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using LoginForm.Interfaces;
using LoginForm.Model;
using Moq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;


namespace LoginForm.Tests
{
    public class UserTest
    {
        Mock<IUserRepository> context = new Mock<IUserRepository>();

        private UserModel[] users =
        {
            new UserModel {Id = 1, Login = "anya", Name = "Anya", CategoryId = 1, Email = "dddd", Image = null, Password = "123"},
            new UserModel {Id = 2, Login = "sasha", Name = "Sasha", CategoryId = 2, Email = "ssss", Image = null, Password = "456"}
        };

        [Test]
        public void UserGetByUsername_Null()
        {
            context.Setup(m => m.GetByUsername("")).Returns((string username) => users.FirstOrDefault(u => u.Login == username));

            var result = context.Object.GetByUsername("");

            Assert.IsNull(result);
        }

        [Test]
        public void UserGetByUsername_Correct()
        {
            context.Setup(m => m.GetByUsername(users[1].Login)).Returns((string username) => users.FirstOrDefault(u => u.Login == username));

            var result = context.Object.GetByUsername(users[1].Login);

            Assert.AreEqual(users[1], result);
        }

        [Test]
        public void IncorrectUserLogin()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            const int length = 6;
            string Username = new string(Enumerable.Repeat(chars, length).Select(s => s[new Random().Next(s.Length)]).ToArray());
            string Password = new string(Enumerable.Repeat(chars, length).Select(s => s[new Random().Next(s.Length)]).ToArray());

            context.Setup(m => m.AuthenticateUser(It.IsAny<NetworkCredential>())).Returns<NetworkCredential>(credentials =>
            {
                return users.Any(u => u.Login == credentials.UserName && u.Password == credentials.Password);
            });

            bool result = context.Object.AuthenticateUser(new NetworkCredential(Username, Password));

            Assert.IsFalse(result);
        }
        [Test]
        public void CorrectUserLogin()
        {
            context.Setup(m => m.AuthenticateUser(It.IsAny<NetworkCredential>())).Returns<NetworkCredential>(credentials =>
            {
                return users.Any(u => u.Login == credentials.UserName && u.Password == credentials.Password);
            });

            bool result = context.Object.AuthenticateUser(new NetworkCredential(users[0].Login, users[0].Password));

            Assert.IsTrue(result);
        }
    }
}
