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
    public class DepartmentsController : ApiController
    {

        private Info_Department departments = new Info_Department();

        /// <summary>
        /// Получить список отделов
        /// </summary>
        /// <returns></returns>
        [Route("getdepartments")]
        public ObservableCollection<Department> Get() => departments.GetDepartments();

        /// <summary>
        /// Добавить отдел в коллекцию
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [Route("adddepartments")]
        public HttpResponseMessage POST([FromBody]Department value)
        {
            if (departments.AddDepartment(value)) return Request.CreateResponse(HttpStatusCode.Created);
            return Request.CreateResponse(HttpStatusCode.BadRequest);
        }

        /// <summary>
        /// Удалить отдел
        /// </summary>
        /// <param name="id">ид отдела</param>
        /// <returns></returns>
        [Route("deletedepartment/{id}")]
        public IHttpActionResult DELETE(int id)
        {
            var department = departments.GetDepartments().FirstOrDefault((p) => p.Id == id);
            if (departments.DeleteDepartment(id)) return Ok(department);
            return NotFound();
        }
    }
}
