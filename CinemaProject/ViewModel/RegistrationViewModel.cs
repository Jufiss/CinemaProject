using LoginForm.Interfaces;
using LoginForm.Model;
using LoginForm.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace LoginForm.ViewModel
{
    public class RegistrationViewModel : ViewModelBase
    {
        private string _login;
        private string _name;
        private string _email;
        private string _password;
        private string _errorMessage;

        private IUserRepository _userRepository;
        public string Login
        {
            get => _login;
            set
            {
                _login = value;
                OnPropertyChanged(nameof(Login));
            }
        }
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }
        public string Email
        {
            get => _email;
            set
            {
                _email = value;
                OnPropertyChanged(nameof(Email));
            }
        }
        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
            }
        }
        public string ErrorMessage
        {
            get => _errorMessage;
            set
            {
                _errorMessage = value;
                OnPropertyChanged(nameof(ErrorMessage));
            }
        }
        public ICommand RegistrationCommand { get; }
        public RegistrationViewModel()
        {
            _userRepository = new UserRepository();

            RegistrationCommand = new ViewModelCommand(ExecuteRegistrationCommand);
        }
        private void ExecuteRegistrationCommand(object obj)
        {
            UserModel user = new UserModel();
            user.Name = Name;
            user.Email = Email;
            user.Login = Login;
            user.Password = Password;
            user.CategoryId = 1;

            _userRepository.Add(user);
            ErrorMessage = "Пользователь добавлен";
        }
    }
}
