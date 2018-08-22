using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi1.Models
{
    public class Employee
    {
        /// <summary>
        /// Имя сотрудника
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// Фамилия сотрудника
        /// </summary>
        public string SurName { get; set; }

        /// <summary>
        /// Возраст сотрудника
        /// </summary>
        public string Age { get; set; }

        /// <summary>
        /// Отдел сотрудника
        /// </summary>
        public string Department { get; set; }

        /// <summary>
        /// Идентификатор сотрудника
        /// </summary>
        public int Id { get; set; }
    }
}