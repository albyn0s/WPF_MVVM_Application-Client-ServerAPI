<<<<<<< HEAD
﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WPF_application
{
    /// <summary>
    /// Сотрудник
    /// </summary>
    public class Employee : INotifyPropertyChanged //интерфейс для проверки изменения
    {
        private string _name; //Имя
        private string _surName; //Фамилия
        private string _age; //Возраст
        private string _department; //Отдел
        private int _id; //идентификатор

        /// <summary>
        /// Имя сотрудника
        /// </summary>
        public string Name
        {
            get => this._name;
            set
            {
                this._name = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs
                    (nameof(this.Name)));//проверка на изменения
            }
        }

        /// <summary>
        /// Фамилия сотрудника
        /// </summary>
        public string SurName
        {
            get => this._surName;
            set
            {
                this._surName = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs
                    (nameof(this.SurName)));//проверка на изменения
            }
        }

        /// <summary>
        /// Возраст сотрудника
        /// </summary>
        public string Age
        {
            get => this._age;
            set
            {
                this._age = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs
                    (nameof(this.Age)));//проверка на изменения
            }
        }

        /// <summary>
        /// Отдел сотрудника
        /// </summary>
        public string Department
        {
            get => this._department;
            set
            {
                this._department = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs
                    (nameof(this.Department)));//проверка на изменения
            }
        }

        /// <summary>
        /// идентификатор сотрудника
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
    /// Свойство на "Изменение"
    /// </summary>
    public event PropertyChangedEventHandler PropertyChanged;
    }
}
=======
﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WPF_application
{
    /// <summary>
    /// Сотрудник
    /// </summary>
    public class Employee : INotifyPropertyChanged //интерфейс для проверки изменения
    {
        private string _name; //Имя
        private string _surName; //Фамилия
        private string _age; //Возраст
        private string _department; //Отдел
        private int _id; //идентификатор

        /// <summary>
        /// Имя сотрудника
        /// </summary>
        public string Name
        {
            get => this._name;
            set
            {
                this._name = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs
                    (nameof(this.Name)));//проверка на изменения
            }
        }

        /// <summary>
        /// Фамилия сотрудника
        /// </summary>
        public string SurName
        {
            get => this._surName;
            set
            {
                this._surName = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs
                    (nameof(this.SurName)));//проверка на изменения
            }
        }

        /// <summary>
        /// Возраст сотрудника
        /// </summary>
        public string Age
        {
            get => this._age;
            set
            {
                this._age = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs
                    (nameof(this.Age)));//проверка на изменения
            }
        }

        /// <summary>
        /// Отдел сотрудника
        /// </summary>
        public string Department
        {
            get => this._department;
            set
            {
                this._department = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs
                    (nameof(this.Department)));//проверка на изменения
            }
        }

        /// <summary>
        /// идентификатор сотрудника
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
    /// Свойство на "Изменение"
    /// </summary>
    public event PropertyChangedEventHandler PropertyChanged;
    }
}
>>>>>>> 5bd1de91cf308b3afa5db67b230b886b6fce0121
