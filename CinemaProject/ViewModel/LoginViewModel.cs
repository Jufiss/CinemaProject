using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Input;
using LoginForm.Repositories;
using System.Threading;
using System.Security.Principal;
using Microsoft.VisualBasic.Logging;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using CinemaProject;
using System.Windows;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using LoginForm.View;
using LoginForm.Interfaces;

namespace LoginForm.ViewModel
{
    public class LoginViewModel : ViewModelBase
    {
        private string _username;
        private string _password;
        private string _errorMessage;
        private string _caption;

        private ViewModelBase _currentChildView;
        private IUserRepository _userRepository;
        public string Username 
        { 
            get => _username;
            set
            {
                _username = value;
                OnPropertyChanged(nameof(Username));
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

        public ViewModelBase CurrentChildView
        {
            get => _currentChildView;
            set
            {
                _currentChildView = value;
                OnPropertyChanged(nameof(CurrentChildView));
            }
        }

        public ICommand LoginCommand { get; }
        public ICommand ShowRegistrationCommand { get; }
        public ICommand ShowLoginCommand { get; }
        public string Caption
        {
            get => _caption;
            set
            {
                _caption = value;
            }
        }

        public LoginViewModel()
        {
            _userRepository = new UserRepository();

            LoginCommand = new ViewModelCommand(ExecuteLoginCommand, CanExecuteLoginCommand);

            ShowRegistrationCommand = new ViewModelCommand(ExecuteShowRegistrationCommand);
            ShowLoginCommand = new ViewModelCommand(ExecuteShowLoginCommand);

            ExecuteShowLoginCommand(null);
        }

        private void ExecuteShowLoginCommand(object obj)
        {
            CurrentChildView = new DoLoginViewModel();
            Caption = "Авторизация";
        }

        private void ExecuteShowRegistrationCommand(object obj)
        {
            CurrentChildView = new RegistrationViewModel();
            Caption = "Регистрация";
        }

        private bool CanExecuteLoginCommand(object obj)
        {
            bool validDate;
            if (string.IsNullOrWhiteSpace(Username) || Password == null)
                validDate = false;
            else
                validDate = true;
            return validDate;
        }

        private void ExecuteLoginCommand(object obj)
        {
            var isValidUser = _userRepository.AuthenticateUser(new NetworkCredential(Username, Password));
            if(isValidUser) 
            {
                Thread.CurrentPrincipal = new GenericPrincipal(new GenericIdentity(Username), null);
                var mainView = new MainWindow();
                mainView.Show();
                foreach (System.Windows.Window window in Application.Current.Windows)
                {
                    if (window.GetType() == typeof(LoginView))
                    {
                        window.Close();
                        break;
                    }
                }
            }
            else
            {
                ErrorMessage = "Неверный логин или пароль";
            }
        }
    }
}
