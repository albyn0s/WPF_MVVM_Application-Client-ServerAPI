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
using WPF_application.ViewModel;

namespace WPF_application
{
    /// <summary>
    /// Логика взаимодействия для EditWindow.xaml
    /// </summary>
    public partial class EditWindow : Window
    {
        static public string Name, SurName, Age, Department;

        public EditWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Записываем данные из TextBox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Сохранить_Click(object sender, RoutedEventArgs e)
        {
                Name = textBox_name.Text;
                SurName = textBox_surname.Text;
                Age = textBox_age.Text;
                Department = DepNamesBox.SelectedItem.ToString();
        }
    }
}
