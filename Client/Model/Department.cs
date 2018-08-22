using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_application
{
    /// <summary>
    /// Отдел
    /// </summary>
    public class Department: INotifyPropertyChanged //интерфейс проверки на изменение
    {
        private string _depname; //Название отдела
        private int _id; //идентификатор отдела

        /// <summary>
        /// Отдел
        /// </summary>
        public string DepName
        {
            get => this._depname;
            set
            {
                this._depname = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs
                    (nameof(this.DepName)));//проверка на изменения
            }
        }

        /// <summary>
        /// идентификатор отдела
        /// </summary>
        public int Id
        {
            get => this._id;
            set
            {
                this._id = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs
                    (nameof(this.Id)));//проверка на изменения
            }
        }

        /// <summary>
        /// Переопределение отображения для combobox
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"{DepName}";
        }

        /// <summary>
        /// Свойство на "Изменение"
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
