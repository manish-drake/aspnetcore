using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CRUD.Models
{
    public static class EmployeeDataAccessLayer
    {
        static Angular6Context db = new Angular6Context();

        public static IEnumerable<EmployeeModel> GetAllEmployees()
        {
            Angular6Context db = new Angular6Context();

            List<EmployeeModel> obj = new List<EmployeeModel>();

            var item = (from Emp in db.Employee
                        select new { Emp }).ToList();

            foreach (var data in item)
            {
                EmployeeModel objevm = new EmployeeModel();

                objevm.EmployeeId = data.Emp.EmployeeId;
                objevm.FirstName = data.Emp.FirstName;
                objevm.Address = data.Emp.Address;
                objevm.Number = data.Emp.Number;
                objevm.LastName = data.Emp.LastName;
                objevm.Email = data.Emp.Email;
                objevm.StateName = data.Emp.StateName;
                obj.Add(objevm);
            }

            return obj.ToList();
        }

        public static bool SaveEmployee(EmployeeModel objEmployee)
        {

            try
            {
                Employee emp = new Employee();

                emp.FirstName = objEmployee.FirstName;
                emp.LastName = objEmployee.LastName;
                emp.Address = objEmployee.Address;
                emp.Email = objEmployee.Email;
                emp.Number = objEmployee.Number;
                emp.StateName = objEmployee.StateName;

                db.Employee.Add(emp);
                db.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static bool UpdateEmployee(EmployeeModel employeeModel)
        {
            try
            {
                if (employeeModel.EmployeeId != 0)
                {
                    var objEmployee = db.Employee.Where(x => x.EmployeeId == employeeModel.EmployeeId).FirstOrDefault();

                    objEmployee.FirstName = employeeModel.FirstName;
                    objEmployee.LastName = employeeModel.LastName;
                    objEmployee.Address = employeeModel.Address;
                    objEmployee.Email = employeeModel.Email;
                    objEmployee.Number = employeeModel.Number;
                    objEmployee.StateName = employeeModel.StateName;
                  
                    db.SaveChanges();
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static EmployeeModel GetEmployeeData(int id)
        {
            EmployeeModel emp = new EmployeeModel();

            try
            {
                if (id != 0)
                {
                    var objEmployee = db.Employee.Where(x => x.EmployeeId == id).FirstOrDefault();

                    emp.EmployeeId = objEmployee.EmployeeId;
                    emp.FirstName = objEmployee.FirstName;
                    emp.LastName = objEmployee.LastName;
                    emp.Address = objEmployee.Address;
                    emp.Email = objEmployee.Email;
                    emp.Number = objEmployee.Number;
                    emp.StateName = objEmployee.StateName;
                }
                return emp;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public static bool DeleteEmployee(int id)
        {
            try
            {
                Employee emp = db.Employee.Find(id);
                db.Employee.Remove(emp);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static List<State> GetStates()
        {
            List<State> lst = new List<State>();
            lst = (from List in db.State select List).ToList();

            return lst;
        }

    }
}
