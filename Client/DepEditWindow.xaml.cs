using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
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
using System.Windows.Shapes;

namespace WPF_application
{
    /// <summary>
    /// Логика взаимодействия для DepEditWindow.xaml
    /// </summary>
    public partial class DepEditWindow : Window
    {

        public static string newDepName;
        public static string oldDepName;

        public DepEditWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            oldDepName = DepNamesBox.SelectedItem.ToString(); //Отдел, который удаляем записываем в переменную
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            newDepName = Textbox_dep.Text; //Отдел, который создаем, записываем в переменную
        }
    }
}
