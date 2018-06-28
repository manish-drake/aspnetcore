using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Communique.DAL;
using Communique.Models;
using Microsoft.AspNetCore.Mvc;

namespace Communique.Controllers.Admin
{
    public class AssessmentTypeController : Controller
    {
        string conStr = "Data Source=DESKTOP-HT6095K\\" +
                    "SQLEXPRESS;Initial Catalog=Communique;User ID=sa;Password=dspl;";

        [HttpGet]
        [Route("api/AssessmentType/Index")]
        public IEnumerable<AssessmentTypeModel> Index()
        {
            AssessmentType assessmentType = new AssessmentType(conStr);
            var data = assessmentType.Items();
            return data;           
        }
    }
}