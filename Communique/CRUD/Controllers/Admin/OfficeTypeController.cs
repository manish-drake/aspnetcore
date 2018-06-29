using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Communique.DAL;
using Communique.Models;
using Microsoft.AspNetCore.Mvc;

namespace Communique.Controllers.Admin
{
    public class OfficeTypeController : Controller
    {
        string conStr = "Data Source=DESKTOP-HT6095K\\" +
                   "SQLEXPRESS;Initial Catalog=Communique;User ID=sa;Password=dspl;";

        [HttpGet]
        [Route("api/OfficeType/Index")]
        public IEnumerable<OfficeTypeModel> Index()
        {
            OfficeType officeType = new OfficeType(conStr);
            var data = officeType.Items();
            return data;
        }
    }
}