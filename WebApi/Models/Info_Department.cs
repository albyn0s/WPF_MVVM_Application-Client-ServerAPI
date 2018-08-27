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
    public class Info_Department
    {
        private SqlConnection sqlConnection;

        public Info_Department()
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
        /// Коллекция отделов
        /// </summary>
        private ObservableCollection<Department> departments;

        /// <summary>
        /// Заполнение коллекции из БД
        /// </summary>
        /// <returns>заполненная коллекция из БД</returns>
        public ObservableCollection<Department> GetDepartments()
        {
            departments = new ObservableCollection<Department>();
            string Query = "SELECT * FROM Department";

            using (SqlCommand command = new SqlCommand(Query, sqlConnection))
            {
                using (SqlDataReader rd = command.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        departments.Add(
                            new Department()
                            {
                                DepName = rd["DepName"].ToString(),
                                Id = (int)rd["id"]
                            });
                    }
                }
            }
            return departments;
        }

        /// <summary>
        /// Создание нового отдела в БД
        /// </summary>
        /// <param name="department">Новый отдел</param>
        /// <returns></returns>
        public bool AddDepartment(Department department)
        {
            try
            {
                string QueryAdd = $@"INSERT INTO Department(DepName)
                VALUES (N'{department.DepName}')";

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
        /// Удаление выбранного отдела
        /// </summary>
        /// <param name="id">Переданный отдел</param>
        /// <returns></returns>
        public bool DeleteDepartment(int id)
        {
            try
            {
                string QueryDelete = $@"DELETE FROM Department WHERE ID = {id}";
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
    }
}