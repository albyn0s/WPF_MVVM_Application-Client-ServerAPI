using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using WPF_application;

namespace WPF_application.ViewModel
{
    ///<summary>
    ///Основная модель(ViewModel)
    ///</summary>
    public class MainWindowViewModel : INotifyPropertyChanged //интерфейс на изменение
    {
        static HttpClient Client = new HttpClient(); //клиент

        /// <summary>
        /// Выбранный сотрудник
        /// </summary>
        Employee selectedEmployee;
        /// <summary>
        /// Выбранный отдел
        /// </summary>
        Department selectedDepartment;

        /// <summary>
        /// Доп окно для "редактирования сотрудника"
        /// </summary>
        EditWindow editWindow = new EditWindow();
        /// <summary>
        /// Доп окно для редактирования отдела
        /// </summary>
        DepEditWindow depEditWindow = new DepEditWindow();

        /// <summary>
        /// Флаг проверки
        /// </summary>
        public static bool flag = true;

        private ObservableCollection<Employee> _employees;
        private ObservableCollection<Department> _departments;

        /// <summary>
        /// Коллекция сотрудников
        /// </summary>
        public ObservableCollection<Employee> employees
        {
            get => this._employees;
            set
            {
                this._employees = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs
                    (nameof(this.employees)));//проверка на изменения
            }
        } //свойство сотрудников.

        /// <summary>
        /// Коллекция отделов
        /// </summary>
        public ObservableCollection<Department> departaments
        {
            get => this._departments;
            set
            {
                this._departments = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs
                    (nameof(this.departaments)));//проверка на изменения
            }
        } //свойство отделов.

        /// <summary>
        /// Выбранный сотрудник
        /// </summary>
        public Employee SelectedEmployee
        {
            get => this.selectedEmployee;
            set
            {
                this.selectedEmployee = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs
                    (nameof(this.SelectedEmployee)));//проверка на изменения
            }
        }

        /// <summary>
        /// Выбранный отдел
        /// </summary>
        public Department SelectedDepartment
        {
            get => this.selectedDepartment;
            set
            {
                this.selectedDepartment = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs
                    (nameof(this.SelectedDepartment)));//проверка на изменения
            }
        }

        #region Команды
        /// <summary>
        /// Добавить сотрудника
        /// </summary>
        public ICommand OpenAddWindow { get; private set; }
        /// <summary>
        /// Редактировать сотрудника
        /// </summary>
        public ICommand OpenEditWindow { get; private set; }
        /// <summary>
        /// Удалить сотрудника
        /// </summary>
        public ICommand Del { get; private set; }
        /// <summary>
        /// Добавить отдел
        /// </summary>
        public ICommand AddDep { get; private set; }
        /// <summary>
        /// Удалить отдел
        /// </summary>
        public ICommand DelDep { get; private set; }
        /// <summary>
        /// Сохранить
        /// </summary>
        public ICommand Save { get; private set; }
        /// <summary>
        /// Отменить
        /// </summary>
        public ICommand Cancel { get; private set; }
        /// <summary>
        /// Вернуться назад
        /// </summary>
        public ICommand CancelDep { get; private set; }
        /// <summary>
        /// Редактор отдела
        /// </summary>
        public ICommand windowEditDep { get; private set; }
        #endregion

        /// <summary>
        /// Команды 
        /// </summary>
        public void getCommands()
        {
            windowEditDep = new DelegateCommand(_windowEditDep); //Добавить отдел
            AddDep = new DelegateCommand(_AddDep); //Добавить отдел
            DelDep = new DelegateCommand(_DelDep, CanRemoveDep); //удалить отдел
            Save = new DelegateCommand(SaveEmp); //Сохранить
            Cancel = new DelegateCommand(CancelEmp); //Выйти из формы сотрудников
            OpenAddWindow = new DelegateCommand(_OpenAddWindow); //Добавить сотрудника
            OpenEditWindow = new DelegateCommand(_OpenEditWindow); //Редактировать сотрудника
            Del = new DelegateCommand(DelEmployee, CanRemoveEmployee); //Удалить сотрудника
            CancelDep = new DelegateCommand(_CancelDep); //выйти из формы отделов
        }

        /// <summary>
        /// Создание коллекций и команд.
        /// </summary>
        public MainWindowViewModel()
        {
            Client.BaseAddress = new Uri("http://localhost:58205/"); // Uri 
            Client.DefaultRequestHeaders.Accept.Clear(); //Очистка запроса
            Client.DefaultRequestHeaders.Accept.Add( //Заголовок для получения/отправки json
                new MediaTypeWithQualityHeaderValue("application/json")
                );

            getCommands(); //команды 
            employees = new ObservableCollection<Employee>(http_getEmployees()); //Коллекция сотрудников заполненная из API
            departaments = new ObservableCollection<Department>(http_getDepartments()); //Коллекция отделов заполненная из API
        }

        /// <summary>
        /// Get запрос с сервера
        /// </summary>
        /// <returns></returns>
        public static ObservableCollection<Employee> http_getEmployees() => 
            Client.GetAsync(Client.BaseAddress + "getemployees").Result.
            Content.ReadAsAsync<ObservableCollection<Employee>>().Result;

        /// <summary>
        /// Get запрос с сервера
        /// </summary>
        /// <returns></returns>
        public static ObservableCollection<Department> http_getDepartments() =>
            Client.GetAsync(Client.BaseAddress + "getdepartments").Result.
            Content.ReadAsAsync<ObservableCollection<Department>>().Result;


        /// <summary>
        /// Сохранить
        /// </summary>
        /// <param name="obj"></param>
        private void SaveEmp(object obj)
        {
            if (!flag) //Если flag = true, то делаем замену сотрудника, иначе создаем нового сотрудника
            {
                string Query = "{" + $@"'Name': '{EditWindow.Name}',
                                'SurName': '{EditWindow.SurName}',
                                'Age': '{EditWindow.Age}',
                                'Department': '{EditWindow.Department}'" + "}"; //json запрос
                Console.WriteLine(Query); //логирование
                string url = Client.BaseAddress + "addemployee"; // URL
                StringContent content = new StringContent(
                    Query,
                    Encoding.UTF8,
                    "application/json"
                    );

                var result = Client.PostAsync(url, content).Result; //запросы async перевожу в sync
                Console.WriteLine(result); //логирование
                refresh(); //обновление списка сотрудников/отделов
                editWindow.Hide(); //скрытие окна
            }
            else
            {
                string Query = "{" + $@"'Name': '{EditWindow.Name}',
                                'SurName': '{EditWindow.SurName}',
                                'Age': '{EditWindow.Age}',
                                'Department': '{EditWindow.Department}',
                                'Id': '{SelectedEmployee.Id}'" + "}"; //json запрос
                Console.WriteLine(Query); //логирование
                string url = Client.BaseAddress + "updateemployee/" + $"{SelectedEmployee.Id}"; //Url
                StringContent content = new StringContent(
                    Query,
                    Encoding.UTF8,
                    "application/json"
                    );

                var result = Client.PutAsync(url, content).Result; //async в sync
                Console.WriteLine(result); //логирование
                refresh(); //обновление списка сотрудников/отделов
                editWindow.Hide(); //скрытие окна

            }
        }

        /// <summary>
        /// Кнопка "Отмены"
        /// </summary>
        /// <param name="obj"></param>
        private void CancelEmp(object obj)
        {
            refresh(); //обновление списка сотрудников/отделов
            editWindow.Hide(); //скрытие окна
        }
        
        /// <summary>
        /// Кнопка "Отмены"
        /// </summary>
        /// <param name="obj"></param>
        private void _CancelDep(object obj)
        {
            refresh(); //обновление списка сотрудников/отделов
            depEditWindow.Hide(); //скрытие окна
        }

        /// <summary>
        /// Удалить отдел
        /// </summary>
        /// <param name="obj">Переданный отдел</param>
        private void _DelDep(object obj)
        {
            if (SelectedDepartment == null) Console.WriteLine("Не выбран отдел"); // а может прокнет?)
            else
            {
                string url = Client.BaseAddress + "deletedepartment/" + $"{SelectedDepartment.Id}"; //запрос
                Console.WriteLine(url); //логирование
                var result = Client.DeleteAsync(url).Result; //async в sync
                Console.WriteLine(result);//логирование
                refresh();//обновление списка сотрудников/отделов
            }
        }

        /// <summary>
        /// Добавить отдел
        /// </summary>
        /// <param name="obj"></param>
        private void _AddDep(object obj)
        {
            string Query = "{" + $"'DepName': '{DepEditWindow.newDepName}'" + "}"; // json запрос
            Console.WriteLine(Query); // логирование
            string url = Client.BaseAddress + "adddepartments"; //Url
            StringContent content = new StringContent(
                Query,
                Encoding.UTF8,
                "application/json"
                );

            var result = Client.PostAsync(url, content).Result; //async в sync
            Console.WriteLine(result); //логирование
            refresh();//обновление списка сотрудников/отделов
        }

        /// <summary>
        /// Окно редактирования для отдела.
        /// </summary>
        /// <param name="obj"></param>
        private void _windowEditDep(object obj)
        {
            depEditWindow.DataContext = MainWindow.ViewModel; //отправляю ViewModel для DataContext
            depEditWindow.DepNamesBox.DataContext = MainWindow.ViewModel; //здесь костыль, потому что DataContext дальше идет null

            depEditWindow.Show(); //показ окна
        }

        /// <summary>
        /// Удалить сотрудника
        /// </summary>
        /// <param name="obj">Переданный сотрудник</param>
        private void DelEmployee(object obj)
        {
            if (SelectedEmployee == null) Console.WriteLine("Не выбран сотрудник"); //проверка...
            else
            {
                string url = Client.BaseAddress + "deleteemployee/" + $"{SelectedEmployee.Id}"; //url
                Console.WriteLine(url); //логирование
                var result = Client.DeleteAsync(url).Result; //async в sync
                Console.WriteLine(result); //логирование

                refresh();//обновление списка сотрудников/отделов
            }
        }

        /// <summary>
        /// обновление списка сотрудников/отделов
        /// </summary>
        public void refresh()
        {
            employees = new ObservableCollection<Employee>(http_getEmployees()); // получаем коллекцию сотрудников
            departaments = new ObservableCollection<Department>(http_getDepartments()); // получаем коллекцию отделов
        }

        /// <summary>
        /// Открытие окна для редактирования
        /// </summary>
        /// <param name="obj"></param>
        private void _OpenEditWindow(object obj)
        {
            editWindow.DataContext = MainWindow.ViewModel; // передача DataContext
            flag = true; //Флаг, который, говорит что нажали на кнопку "Редактирование"
            getData_toEmp(); //данные для кнопок, на которых DataContext не должен влиять (костыль)
            editWindow.Show(); //показываем экран
        }

        /// <summary>
        /// Добавить сотрудника
        /// </summary>
        /// <param name="obj">Переданный сотрудник</param>
        public void _OpenAddWindow(object obj)
        {
            editWindow.DataContext = null; //обнуляем DataContext
            flag = false; //нажали на кнопку "Добавить"
            getData_toEmp();//данные для кнопок, на которых DataContext не должен влиять (костыль)
            editWindow.Show();//показываем экран
        }

        /// <summary>
        /// DataContext + кнопки, которые не должны выключаться при DataContext == null;
        /// </summary>
        private void getData_toEmp()
        {
            editWindow.DepNamesBox.DataContext = MainWindow.ViewModel; //combobox для отделов
            editWindow.Save_btn.Command = MainWindow.ViewModel.Save; //кнопка сохранить
            editWindow.Cancel_btn.Command = MainWindow.ViewModel.Cancel; //кнопка отмена
            editWindow.settings_deps.Command = MainWindow.ViewModel.windowEditDep; // кнопка редактора отделов
        }

        /// <summary>
        /// Проверка на удаление
        /// </summary>
        /// <param name="arg">передаваемый объект</param>
        /// <returns></returns>
        private bool CanRemoveDep(object arg)
        {
            return (arg as Department) != null;
        }

        /// <summary>
        /// Проверка на удаление сотрудника
        /// </summary>
        /// <param name="arg">Переданный сотрудник</param>
        /// <returns></returns>
        private bool CanRemoveEmployee(object arg)
        {
            return (arg as Employee) != null;
        }

        /// <summary>
        /// Событие на изменение
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
    }
}

>>>>>>> 5bd1de91cf308b3afa5db67b230b886b6fce0121
