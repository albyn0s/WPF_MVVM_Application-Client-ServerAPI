using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace WebApi1.Models
{
    public class Info_Employee
    {
        private SqlConnection sqlConnection;

        public Info_Employee()
        {
            /// <summary>
            /// Строка подключения к БД
            /// </summary>
            string connectionDb =
    ConfigurationManager.ConnectionStrings["connectionDb"].ConnectionString;

            sqlConnection = new SqlConnection(connectionDb);
            sqlConnection.Open();
        }

        /// <summary>
        /// Коллекция сотрудников
        /// </summary>
        private ObservableCollection<Employee> employees;

        /// <summary>
        /// Заполнение коллекции из БД
        /// </summary>
        /// <returns>заполненная коллекция из БД</returns>
        public ObservableCollection<Employee> GetEmployees()
        {
            employees = new ObservableCollection<Employee>();
            string Query = "SELECT * FROM Employee";

            using (SqlCommand command = new SqlCommand(Query, sqlConnection))
            {
                using (SqlDataReader rd = command.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        employees.Add(
                            new Employee()
                            {
                                Name = rd["Name"].ToString(),
                                SurName = rd["SurName"].ToString(),
                                Age = rd["Age"].ToString(),
                                Department = rd["Department"].ToString(),
                                Id = (int)rd["Id"]
                            });
                    }
                }
            }
            return employees;
        }

        /// <summary>
        /// Создание нового сотрудника в БД
        /// </summary>
        /// <param name="employee">Новый сотрудник</param>
        /// <returns></returns>
        public bool AddEmployee(Employee employee)
        {
            try
            {
                string QueryAdd = $@"INSERT INTO Employee(Name, SurName, Age, Department)
                VALUES (N'{employee.Name}',
                        N'{employee.SurName}',
                        N'{employee.Age}',
                        N'{employee.Department}')";

                using (var command = new SqlCommand(QueryAdd, sqlConnection))
                {
                    command.ExecuteNonQuery();
                }
            }
            catch
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Удаление выбранного сотрудника
        /// </summary>
        /// <param name="employee">Переданный сотрудник</param>
        /// <returns></returns>
        public bool DeleteEmployee(int id)
        {
            try
            {
                string QueryDelete = $@"DELETE FROM Employee WHERE ID = {id}";
                using (var command = new SqlCommand(QueryDelete, sqlConnection))
                {
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            return true;
        }

        /// <summary>
        /// Редактирование сотрудника
        /// </summary>
        /// <param name="employee">Переданный сотрудник</param>
        /// <returns></returns>
        public bool UpdateEmployee(Employee employee)
        {
            try
            {
                string QueryUpdate = $@"UPDATE Employee SET 
                Name = N'{employee.Name}', 
                SurName = N'{employee.SurName}', 
                Age = N'{employee.Age}', 
                Department = N'{employee.Department}' 
                WHERE ID = {employee.Id}";

                using (var command = new SqlCommand(QueryUpdate, sqlConnection))
                {
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            return true;
        }

       
        
    }
}