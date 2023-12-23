using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;

namespace LoginForm.ViewModel
{
    //internal class AutoGenerateHandler
    //{
    //    public static void HideIdOnAutogeneration(object sender, DataGridAutoGeneratingColumnEventArgs e)
    //    {
    //        if (e.PropertyName.Contains("Id"))
    //        {
    //            e.Column.Visibility = Visibility.Hidden;
    //        }
    //    }
    //    public static void RenameColumnsOnAutogeneration(object sender, DataGridAutoGeneratingColumnEventArgs e)
    //    {
    //        PropertyDescriptor pd = e.PropertyDescriptor as PropertyDescriptor;
    //        PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(pd.ComponentType);
    //        PropertyDescriptor property = properties.Find(e.PropertyName, false);
    //        e.Column.Header = property.DisplayName;
    //    }

    //    public static void DataColumns(object sender, DataGridAutoGeneratingColumnEventArgs e)
    //    {
    //        if (e.PropertyName == "Date") 
    //        {
    //            var dataGridTextColumn = e.Column as DataGridTextColumn;
    //            if (dataGridTextColumn != null)
    //            {
    //                dataGridTextColumn.Binding.StringFormat = "dd-MM-yyyy HH:mm"; 
    //            }
    //        }
    //    }
    //}
}
