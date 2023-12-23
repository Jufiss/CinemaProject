using LoginForm.Interfaces;
using LoginForm.Model;
using LoginForm.Repositories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace LoginForm.ViewModel
{
    public class AccountViewModel : ViewModelBase
    {
        private UserModel _currentUserModel;
        private IUserRepository userRepository;
        private IOrderRepository orderRepository;
        private byte[]? _image;
        private ObservableCollection<OrderModel> _orders;

        public UserModel CurrentUserModel
        {
            get => _currentUserModel;
            set
            {
                _currentUserModel = value;
                OnPropertyChanged(nameof(CurrentUserModel));
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

        public ICommand ShowDataAccountViewCommand { get; }
        public ICommand ChangeDataAccountViewCommand { get; }
        public ICommand UploadImageAccountCommand { get; }
        public ICommand DeleteUserImageCommand { get; }
        public ObservableCollection<OrderModel> Orders { get => _orders; set { _orders = value; OnPropertyChanged(nameof(Orders)); } }

        public AccountViewModel()
        {
            userRepository = new UserRepository();
            orderRepository = new OrderRepository();
            CurrentUserModel = new UserModel();
            ShowDataAccountViewCommand = new ViewModelCommand(ExecuteShowDataAccountViewCommand);
            ChangeDataAccountViewCommand = new ViewModelCommand(ExecuteChangeDataAccountViewCommand);
            UploadImageAccountCommand = new ViewModelCommand(ExecuteUploadImageAccountCommand);
            DeleteUserImageCommand = new ViewModelCommand(ExecuteDeleteUserImageCommand);

            ExecuteShowDataAccountViewCommand(null);
        }

        private void ExecuteDeleteUserImageCommand(object obj)
        {
            Image = null;
            CurrentUserModel.Image = null;
        }

        private void ExecuteUploadImageAccountCommand(object obj)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "Image files|*.bmp;*.jpg;*.png;*.jpeg;";
            fileDialog.FilterIndex = 1;

            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                Image = File.ReadAllBytes(fileDialog.FileName);
                CurrentUserModel.Image = File.ReadAllBytes(fileDialog.FileName);
            }
        }

        private void ExecuteChangeDataAccountViewCommand(object obj)
        {
            userRepository.Update(CurrentUserModel);
        }

        private void ExecuteShowDataAccountViewCommand(object obj)
        {
            var user = userRepository.GetByUsername(Thread.CurrentPrincipal.Identity.Name);
            if (user != null)
            {
                CurrentUserModel.Name = user.Name;
                CurrentUserModel.Email = user.Email;
                CurrentUserModel.Login = user.Login;
                CurrentUserModel.Password = user.Password;
                CurrentUserModel.Id = user.Id;
                CurrentUserModel.CategoryId = user.CategoryId;
                if (user.Image != null)
                {
                    Image = user.Image;
                    CurrentUserModel.Image = user.Image;
                }

                Orders = orderRepository.OrdersByUserId(user.Id);
            }
        }
    }
}
