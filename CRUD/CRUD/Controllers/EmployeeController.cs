using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using CRUD.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CRUD.Controllers
{
    public class EmployeeController : Controller
    {
        [HttpGet]
        [Route("api/Employee/Index")]
        public IEnumerable<EmployeeModel> Index()
        {
            var data = EmployeeDataAccessLayer.GetAllEmployees();
            return data;
        }

        [HttpPost]
        [Route("api/Employee/Create")]
        public bool Create([FromBody] EmployeeModel employee)
        {
            var data = EmployeeDataAccessLayer.SaveEmployee(employee);
            return data;
        }

        [HttpGet]
        [Route("api/Employee/Details/{empId}")]
        public EmployeeModel Details(int empId)
        {
            var data = EmployeeDataAccessLayer.GetEmployeeData(empId);
            return data;
        }


        [HttpPut]
        [Route("api/Employee/Edit")]
        public bool Edit([FromBody]EmployeeModel employee)
        {
            var data = EmployeeDataAccessLayer.UpdateEmployee(employee);
            return data;
        }

        [HttpDelete]
        [Route("api/Employee/Delete/{empId}")]
        public bool Delete(int empId)
        {
            var data = EmployeeDataAccessLayer.DeleteEmployee(empId);
            return data;
        }

        [HttpGet]
        [Route("api/Employee/GetStateList")]
        public IEnumerable<State> Details()
        {
            var data = EmployeeDataAccessLayer.GetStates();
            return data;
        }

        public ActionResult Error()
        {
            ViewData["RequestId"] = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
            return View();
        }
    }
}
