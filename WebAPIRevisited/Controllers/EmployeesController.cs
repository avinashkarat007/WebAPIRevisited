using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Web.Http;
using EmployeeDataAccess;

namespace WebAPIRevisited.Controllers
{
    public class EmployeesController : ApiController
    {
        [HttpGet]
        [BasicAuthentication]
        public IEnumerable<Employee> LoadEmployees()
        {
            string username = Thread.CurrentPrincipal.Identity.Name;
            
            using (var entities = new EmployeesDBEntities())
            {
                return entities.Employees.ToList();
            }
        }

        public Employee GetEmployee(int id)
        {
            using (var entities = new EmployeesDBEntities())
            {
                return entities.Employees.FirstOrDefault(x => x.ID == id);
            }
        }

        public HttpResponseMessage Post([FromBody] Employee employee)
        {
            try
            {
                using (var entities = new EmployeesDBEntities())
                {
                    entities.Employees.Add(employee);
                    entities.SaveChanges();

                    var message = Request.CreateResponse(HttpStatusCode.Created, employee);
                    message.Headers.Location = new Uri(Request.RequestUri +
                                                       employee.ID.ToString());

                    return message;
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
    }
}
