using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace API.Controllers
{
    public class EmployeeController : ApiController
    {
        private EmployeeRecordsEntities db;
        public EmployeeController()
        {
            db = new EmployeeRecordsEntities();
        }
        [HttpGet]
        public HttpResponseMessage GetAllEmp()
        {

            return Request.CreateResponse(HttpStatusCode.OK, db.Employees);

        }
        [HttpGet]
        public HttpResponseMessage GetEmpByID([FromUri] int empid)
        {

            return Request.CreateResponse(HttpStatusCode.OK, 
                db.Employees.SingleOrDefault(model => model.EmpID == empid));
        }
        [HttpPost]
        public HttpResponseMessage AddEmployee([FromBody] EmpModel emp)
        {
            Employee employee = new Employee()
            {
                FirstName = emp.FirstName,
                MiddleName = emp.MiddleName,
                LastName = emp.LastName
            };
            db.Employees.Add(employee);
            db.SaveChanges();
            return Request.CreateResponse(HttpStatusCode.OK, "Employee Added");
        }
        [HttpPut]
        public HttpResponseMessage UpdateEmp([FromUri] int empid, [FromBody] EmpModel emp)
        {
            
            Employee employee = new Employee();
            employee = db.Employees.Single(model => model.EmpID == empid);
            employee.FirstName = emp.FirstName;
            employee.MiddleName = emp.MiddleName;
            employee.LastName = emp.LastName;
            db.SaveChanges();
            return Request.CreateResponse(HttpStatusCode.OK, "Employee Information Updated");
        }
        [HttpDelete]
        public HttpResponseMessage Delete([FromUri] int empid)
        {
            if (!db.Employees.Any(model => model.EmpID == empid))
            {
                return Request.CreateResponse(HttpStatusCode.OK, "Doesnot EXIST");
            }
            Employee emp = db.Employees.Single(model => model.EmpID == empid);
            db.Employees.Remove(emp);
            db.SaveChanges();
            return Request.CreateResponse(HttpStatusCode.OK, "Sucessfully Deleted");
        }
        
        
    }
}
