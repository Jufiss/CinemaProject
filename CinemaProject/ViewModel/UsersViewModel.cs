using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LoginForm.Model;
using LoginForm.Repositories;
using System.Threading.Tasks;
using System.Net;
using System.Windows.Input;
using System.Threading;
using System.Windows.Controls;
using LoginForm.View;
using LoginForm.Models;
using System.Windows.Forms;
using System.Windows.Data;
using System.Windows;
using System.Windows.Media;
using System.Drawing;
using System.Windows.Media.Imaging;
using System.IO;
using LoginForm.Interfaces;
using System.ComponentModel;

namespace LoginForm.ViewModel
{
    public class UsersViewModel : ViewModelBase
    {
        List<UserModel> allUsers;
        private IUserRepository userRepository;
        private string _text;

        private byte[]? _image;
        private string _login;
        private string _password;
        private string _email;
        private string _name;
        private int _categoryId;
        List<UserCategoryModel> _category;

        private UserModel _selectedUser;

        public List<UserModel> AllUsers
        {
            get => allUsers;
            set
            {
                allUsers = value;
                OnPropertyChanged(nameof(AllUsers));
            }
        }

        public UserModel SelectedUser
        {
            get => _selectedUser;
            set
            {
                _selectedUser = value;
                OnPropertyChanged(nameof(SelectedUser));
            }
        }
        public string TextBox
        {
            get => _text;
            set
            {
                _text = value;
                OnPropertyChanged(nameof(TextBox));
            }
        }

        public string Login
        {
            get => _login;
            set
            {
                _login = value;
                OnPropertyChanged(nameof(Login));
            }
        }
            
        public string Password 
        { get => _password;
            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
            }
        }
        public string Email 
        { get => _email;
            set
            {
                _email = value;
                OnPropertyChanged(nameof(Email));
            }
        }
        public string Name 
        { get => _name;
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        public int CategoryId
        {
            get => _categoryId;
            set
            {
                _categoryId = value;
                OnPropertyChanged(nameof(CategoryId));
            }
        }

        public List<UserCategoryModel> Categories
        { get => _category;
            set
            {
                _category = value;
                OnPropertyChanged(nameof(Categories));
            }
        }

        public byte[]? Image
        {
            get => _image;
            set
            {
                _image = value;
                OnPropertyChanged(nameof(Image));
            }
        }

        public ICommand SerchUserViewCommand { get; }
        public ICommand UpdateUserViewCommand { get; }
        public ICommand AddUserViewCommand { get; }
        public ICommand DeleteUserViewCommand { get; }
        public ICommand UploadImageCommand { get; }
        public ICommand DeleteUserImageCommand { get; }

        public UsersViewModel() 
        {
            userRepository = new UserRepository();
            SelectedUser = new UserModel();
            loadData();
            SerchUserViewCommand = new ViewModelCommand(ExecuteSerchUserViewCommand);
            AddUserViewCommand = new ViewModelCommand(ExecuteAddUserViewCommand);
            UpdateUserViewCommand = new ViewModelCommand(ExecuteUpdateUserViewCommand);
            DeleteUserViewCommand = new ViewModelCommand(ExecuteDeleteUserViewCommand);
            UploadImageCommand = new ViewModelCommand(ExecuteUploadImageCommand);
            DeleteUserImageCommand = new ViewModelCommand(ExecuteDeleteUserImageCommand);
        }

        private void ExecuteDeleteUserImageCommand(object obj)
        {
            Image = null;
        }

        private void ExecuteUploadImageCommand(object obj)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "Image files|*.bmp;*.jpg;*.png;*.jpeg;";
            fileDialog.FilterIndex = 1;

            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                Image = File.ReadAllBytes(fileDialog.FileName);
            }
        }

        private void ExecuteDeleteUserViewCommand(object obj)
        {
            userRepository.Delete(SelectedUser.Id);
            loadData();
        }

        private void ExecuteUpdateUserViewCommand(object obj)
        {
            AddUserView edit = new AddUserView();
            edit.DataContext = this;
            Name = SelectedUser.Name;
            Login = SelectedUser.Login;
            Password = SelectedUser.Password;
            Email = SelectedUser.Email;
            CategoryId = SelectedUser.CategoryId;
            if (SelectedUser.Image != null)
                Image = SelectedUser.Image;
            else
                Image = null;

            if (edit.ShowDialog() == true) // if ok
            {
                UserModel u = new UserModel();
                u.Id = SelectedUser.Id;
                u.Name = Name;
                u.Login = Login;
                u.Password = Password;
                u.Email = Email;
                u.CategoryId = CategoryId;
                u.Image = Image;
                if (Name != null && Login != null && Password != null && Email != null)
                    userRepository.Update(u);
                else
                    System.Windows.MessageBox.Show("Не все поля заполнены. Пожалуйста, заполните все поля о пользователе.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            loadData();
        }

        private void ExecuteAddUserViewCommand(object obj)
        {
            Name = null;
            Login = null;
            Password = null;
            Email = null;
            Image = null;

            AddUserView edit = new AddUserView();
            edit.DataContext = this;

            if (edit.ShowDialog() == true) // if ok
            {
                UserModel u = new UserModel();
                u.Name = Name;
                u.Login = Login;
                u.Password = Password;
                u.Email = Email;
                u.CategoryId = CategoryId;
                u.Image = Image;

                if (Name != null && Login != null && Password != null && Email != null)
                    userRepository.Add(u);
                else
                    System.Windows.MessageBox.Show("Не все поля заполнены. Пожалуйста, заполните все поля о пользователе.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);

            }
        }

        private void ExecuteSerchUserViewCommand(object obj)
        {
            AllUsers = userRepository.Search(TextBox);
        }

        private void loadData()
        {
            AllUsers = userRepository.GetAllUsers();
            Categories = userRepository.GetUserCategories();
        }
    }
}
