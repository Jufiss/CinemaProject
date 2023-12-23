using LoginForm.Interfaces;
using LoginForm.Model;
using LoginForm.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;

namespace LoginForm.Repositories
{
    public class UserRepository : RepositoryBase, IUserRepository
    {
        public void Add(UserModel userModel)
        {
            if (userModel == default(UserModel))
            {
                 throw new ArgumentNullException(nameof(userModel));
            }

            db.Users.Add(new User()
            {
                Id = userModel.Id,
                Name = userModel.Name,
                Email = userModel.Email,
                Password = userModel.Password,
                Login = userModel.Login,
                CategoryId = userModel.CategoryId,
                Image = userModel.Image,
            });
            Save();
        }

        public bool AuthenticateUser(NetworkCredential credential)
        {
            bool validUser;
            string username = credential.UserName;
            string password = credential.Password;
            validUser = db.Users.Any(user => user.Login == username && user.Password == password);
            return validUser;
        }

        public void Delete(int Id)
        {
            User u = db.Users.Find(Id);
            if (u != null) 
            {
                db.Users.Remove(u);
                Save();
            }
        }

        public List<UserModel> GetAllUsers()
        {
            return db.Users.Include(u => u.Category).ToList().Select(i => new UserModel(i)).ToList();
        }

        public List<UserCategoryModel> GetUserCategories() 
        {
            return db.UserCategories.ToList().Select(i => new UserCategoryModel(i)).ToList();
        }

        public UserModel GetByUsername(string username)
        {
            if (username == null)
                return null;
            UserModel user = db.Users
                .Where(u => u.Login == username)
                .Select(u => new UserModel
                {
                    Id = u.Id,
                    Login = u.Login,
                    Password = u.Password,
                    Name = u.Name,
                    Email = u.Email,
                    CategoryId = u.CategoryId,
                    Image = u.Image
                })
                .FirstOrDefault();
            return user;
        }

        public List<UserModel> Search(string username)
        {
            var request = db.Users
                .Where(u => u.Login.ToUpper().Contains(username.ToUpper()))
                .Select(u => new UserModel()
                {
                    Id = u.Id,
                    Login = u.Login,
                    Password = u.Password,
                    Name = u.Name,
                    Email = u.Email,
                    CategoryId = u.CategoryId,
                    Categories = u.Category.Name,
                    Image = u.Image
                })
                .ToList();
            return request;
        }

        public void Update(UserModel userModel)
        {
            if (userModel.Id != 0)
            {
                User u = db.Users.Find(userModel.Id);
                u.Login = userModel.Login;
                u.Password = userModel.Password;
                u.Email = userModel.Email;
                u.Name = userModel.Name;
                u.CategoryId = userModel.CategoryId;
                u.Image = userModel.Image;
                Save();
            }
        }

        public bool Save()
        {
            if (db.SaveChanges() > 0)
                return true;
            return false;
        }
    }
}
