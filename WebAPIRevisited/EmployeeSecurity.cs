using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using EmployeeDataAccess;

namespace WebAPIRevisited
{
    public class EmployeeSecurity
    {
        public static bool Login(string username, string password)
        {
            using (var entities = new EmployeesDBEntities())
            {
                return entities.Users.Any(x => x.Username == username && x.Password == password);
            }
        }
    }
}