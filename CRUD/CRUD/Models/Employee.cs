using System;
using System.Collections.Generic;

namespace Communique.Models
{
    public partial class Employee
    {
        public int EmployeeId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Number { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string StateName { get; set; }
    }
}
