﻿using LoginForm.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LoginForm.View
{
    /// <summary>
    /// Логика взаимодействия для CheckTicketsView.xaml
    /// </summary>
    public partial class CheckTicketsView : UserControl
    {
        public CheckTicketsView()
        {
            InitializeComponent();
            //OrdersList.AutoGeneratingColumn += AutoGenerateHandler.HideIdOnAutogeneration;
            //OrdersList.AutoGeneratingColumn += AutoGenerateHandler.RenameColumnsOnAutogeneration;
        }
    }
}
