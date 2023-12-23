using LoginForm.Model;
using LoginForm.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace LoginForm.Interfaces
{
    public interface IUserRepository
    {
        bool AuthenticateUser(NetworkCredential credential);
        void Add(UserModel userModel);
        void Update(UserModel userModel);
        void Delete(int Id);
        UserModel GetByUsername(string username);
        public List<UserCategoryModel> GetUserCategories();
        List<UserModel> Search(string username);
        List<UserModel> GetAllUsers();
        bool Save();
    }
}
