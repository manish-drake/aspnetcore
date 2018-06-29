using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Communique.Models
{
    public class EmployeeModel
    {
        public int EmployeeId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int Number { get; set; }

        public string Email { get; set; }

        public string Address { get; set; }

        //  public int StateId { get; set; }

        public string StateName { get; set; }

    }
}
