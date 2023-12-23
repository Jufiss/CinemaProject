using LoginForm.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace LoginForm.ViewModel
{
    public class AddViewModel : ViewModelBase
    {
        public ICommand OkCommand { get; }
        public AddViewModel()
        {
            OkCommand = new ViewModelCommand(ExecuteOkCommand);
        }

        private void ExecuteOkCommand(object obj)
        {
            Window window = obj as Window;
            window.DialogResult = true;
        }
    }
}
