using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using LoginForm.Model;
using LoginForm.Repositories;
using LoginForm.View;
using LoginForm.ViewModel;
using Ninject;

namespace CinemaProject
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected void Application_Startup(object sender, StartupEventArgs e)
        {
            var loginView = new LoginView();
            loginView.Show();

            //loginView.IsVisibleChanged += (s, ev) =>
            //{
            //    if (loginView.IsVisible == false && loginView.IsLoaded)
            //    {
            //        //var mainView = new MainWindow();
            //        //mainView.Show();
            //        loginView.Close();
            //    }
            //};
        }
    }
}
