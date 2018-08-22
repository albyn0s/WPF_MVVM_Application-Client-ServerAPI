using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApi1.Models;

namespace WebApi1.Controllers
{
    public class EmployeesController : ApiController
    {
        private Info_Employee emp = new Info_Employee();

        /// <summary>
        /// Получить список сотрудников
        /// </summary>
        /// <returns></returns>
        [Route("getemployees")]
        public ObservableCollection<Employee> Get() => emp.GetEmployees();

        /// <summary>
        /// Добавить сотрудника
        /// </summary>
        /// <param name="value">запрос</param>
        /// <returns></returns>
        [Route("addemployee")]
        public HttpResponseMessage POST([FromBody]Employee value)
        {
            if (emp.AddEmployee(value)) return Request.CreateResponse(HttpStatusCode.Created);
            return Request.CreateResponse(HttpStatusCode.BadRequest);
        }

        /// <summary>
        /// удалить сотрудника
        /// </summary>
        /// <param name="id">ид сотрудника</param>
        /// <returns></returns>
        [Route("deleteemployee/{id}")]
        public IHttpActionResult DELETE(int id)
        {
            var employee = emp.GetEmployees().FirstOrDefault((p) => p.Id == id);
            if (emp.DeleteEmployee(id)) return Ok(employee);
            return NotFound();
        }

        /// <summary>
        /// Проверка на наличие сотрудника по ид
        /// </summary>
        /// <param name="id">ид сотрудника</param>
        /// <returns></returns>
        [Route("getemployees/{id}")]
        public IHttpActionResult GETEmployee(int id)
        {
            var employee = emp.GetEmployees().FirstOrDefault((p) => p.Id == id);
            if (employee == null) return NotFound();
            return Ok(employee);
        }

        /// <summary>
        /// Отредактировать сотрудника
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [Route("updateemployee/{id}")]
        public HttpResponseMessage PUT([FromBody]Employee value)
        {
            if (emp.UpdateEmployee(value)) return Request.CreateResponse(HttpStatusCode.Accepted);
            return Request.CreateResponse(HttpStatusCode.BadRequest);
        }
    }
}
