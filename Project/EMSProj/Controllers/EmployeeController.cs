using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EMSProj.Controllers
{
    public class EmployeeController : Controller
    {
        // GET: Employee
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Report()
        {
            DB51Entities1 db = new DB51Entities1();
            var c = db.EmployeeReports.ToList();
            EmpReport rept = new EmpReport();
            rept.Load();
            rept.SetDataSource(c);
            Stream s = rept.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            return File(s, "application/pdf");
        }

        public ActionResult LoanStatus()
        {
            return View();
        }
    }
}