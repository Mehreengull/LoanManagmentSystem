using Microsoft.AspNet.Identity.Owin;
using EMSProj.CollectionViewModels;
using EMSProj.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace EMSProj.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        DB51Entities db = new DB51Entities();
        private ApplicationUserManager _userManager;
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        public ActionResult Index()
        {
            return View();
        }


        // *************************************
        // COMPANY INFORMATION, EDIT
        // *************************************
        // Doing Nothing
        public ActionResult UpdateCompanyInfo()
        {
            return View();
        }
        [HttpGet]
        // Doing Nothing
        public ActionResult CreateCompany()
        {
            return View();
        }
        [HttpPost]
        // Doing Nothing
        public ActionResult CreateCompany(Company model)
        {
            db.Companies.Add(model);
            db.SaveChanges();
            return RedirectToAction("CompaniesList");
        }

        [HttpGet]
        // Showing Companies Information
        public ActionResult CompaniesList()
        {
            var model = db.Companies.ToList();
            return View(model);
        }

        //Edit Company Data
        public ActionResult EditCompany(int id)
        {
            var model = db.Companies.SingleOrDefault(c => c.Code == id);
            return View(model);
        }

        // Saving to Model
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditCompany(int id, Company model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var com = db.Companies.Single(c => c.Code == id);
            com.Name = model.Name;
            com.Email = model.Email;
            com.PhoneNo = model.PhoneNo;
            com.PostalCode = model.PostalCode;
            com.Url = model.Url;
            com.Fax = model.Fax;
            db.SaveChanges();
            return RedirectToAction("CompaniesList");
        }


        // *************************************
        // DEPARTMENT INFORMATION, ADD, DELETE, EDIT
        // *************************************
        public ActionResult AddDepartment()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddDepartment(Department model)
        {
            if (db.Departments.Any(c => c.Name == model.Name))
            {
                ModelState.AddModelError("Name", "Name already present!");
                return View(model);
            }

            db.Departments.Add(model);
            db.SaveChanges();
            return RedirectToAction("DepartmentList");
        }

        public ActionResult DepartmentList()
        {
            var model = db.Departments.ToList();
            return View(model);
        }

        public ActionResult EditDepartment(int id)
        {
            var model = db.Departments.SingleOrDefault(c => c.depId == id);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditDepartment(int id, Department model)
        {
            var department = db.Departments.Single(c => c.depId == id);
            department.Name = model.Name;
            db.SaveChanges();
            return RedirectToAction("DepartmentList");
        }

        [HttpGet]
        public ActionResult DeleteDepartment(int id)
        {
            var model = db.Departments.SingleOrDefault(c => c.depId == id);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteDepartment(int id, Department model)
        {
            var department = db.Departments.SingleOrDefault(c => c.depId == id);
            db.Departments.Remove(department);
            db.SaveChanges();
            return RedirectToAction("DepartmentList");
        }


        // *************************************
        // RANKS INFORMATION, ADD, DELETE, EDIT 
        // *************************************

        public ActionResult AddRank()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddRank(Rank model)
        {
            db.Ranks.Add(model);
            db.SaveChanges();
            return RedirectToAction("RankList");
        }

        public ActionResult RankList()
        {
            var model = db.Ranks.ToList();
            return View(model);
        }

        public ActionResult EditRank(int id)
        {
            var model = db.Ranks.SingleOrDefault(c => c.rankId == id);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditRank(int id, Rank model)
        {
            var rank = db.Ranks.Single(c => c.rankId == id);
            rank.Name = model.Name;
            db.SaveChanges();
            return RedirectToAction("RankList");
        }

        public ActionResult DeleteRank(int? id)
        {
            var model = db.Ranks.SingleOrDefault(c => c.rankId == id);
            return View(model);
        }

        [HttpPost, ActionName("DeleteRank")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteRank(int id, Rank model)
        {
            var rank = db.Ranks.SingleOrDefault(c => c.rankId == id);
            db.Ranks.Remove(rank);
            db.SaveChanges();
            return RedirectToAction("RankList");
        }

        // *************************************
        // EMPLOYEE INFORMATION, ADD, DELETE, EDIT 
        // *************************************

        public ActionResult AddEmployee()
        {
            var collection = new EmployeeCollectionViewModel
            {
                ApplicationUser = new RegisterViewModel(),
                Employee = new Employee(),
                Departments = db.Departments.ToList(),
                Ranks = db.Ranks.ToList(),
                Lookups = db.Lookups.ToList()

            };
            return View(collection);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddEmployee(EmployeeCollectionViewModel model)
        {
            var user = new ApplicationUser
            {
                UserName = model.ApplicationUser.UserName,
                Email = model.ApplicationUser.Email,
                UserRole = "Employee",
            };
            var result = await UserManager.CreateAsync(user, model.ApplicationUser.Password).ConfigureAwait(false);
            if (result.Succeeded)
            {
                await UserManager.AddToRoleAsync(user.Id, "Employee").ConfigureAwait(false);
                var rank = db.Ranks.Where(c => c.rankId == model.RankId).SingleOrDefault();
                var depart = db.Departments.Where(c => c.depId == model.DepartmentId).SingleOrDefault();
                var employee = new Employee
                {
                    FirstName = model.Employee.FirstName,
                    LastName = model.Employee.LastName,
                    Cnic = model.Employee.Cnic,
                    Status = model.Employee.Status,
                    DateofJoining = DateTime.Now,
                    Email = model.ApplicationUser.Email,
                    DepId = model.DepartmentId,
                    Rank = model.DepartmentId,
                    UserId = user.Id
                    
                };
                
                
                db.Employees.Add(employee);
                db.SaveChanges();
                return RedirectToAction("EmployeeList");
            }


            return HttpNotFound();
        }

        public ActionResult EmployeeList()
        {
            var model = db.Employees.ToList();
            return View(model);
        }

        public ActionResult EditEmployee(int id)
        {


            var Employee = db.Employees.SingleOrDefault(c => c.empId == id);
            
            return View(Employee);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditEmployee(int id, Employee model)
        {
            var emp = db.Employees.Single(c => c.empId == id);
            emp.FirstName = model.FirstName;
            emp.LastName = model.LastName;
            emp.State = model.State;
            emp.City = model.City;
            emp.DateofBirth = (model.DateofBirth);
            emp.Salary = model.Salary;
            emp.DepId = model.DepId;
            emp.Rank = model.Rank;

            emp.Province = model.Province;
            emp.Cnic = model.Cnic;

            db.SaveChanges();
            return RedirectToAction("EmployeeList");
        }

        public ActionResult DeleteEmployee(int? id)
        {
            var model = db.Employees.SingleOrDefault(c => c.empId == id);
            return View(model);
        }

        [HttpPost, ActionName("DeleteEmployee")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteEmployee(int id, Rank model)
        {
            var emp = db.Employees.SingleOrDefault(c => c.empId == id);
            db.Employees.Remove(emp);
            db.SaveChanges();
            return RedirectToAction("EmployeeList");
        }

        // *************************************
        // LOAN INFORMATION, ADD, DELETE, EDIT 
        // *************************************
        //For Employee
        

        //For Admin
        public ActionResult LoanList()
        {
            var model = db.Loans.ToList();
            return View(model);
        }

        //For Admin
        public ActionResult EditLoan(int id)
        {
            var model = db.Loans.SingleOrDefault(c => c.loanId == id);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditLoan(int id, Loan model)
        {
            var loan = db.Loans.Single(c => c.loanId == id);
            loan.approvalDate = DateTime.Now.Date;

            DateTime date = DateTime.Now;
            date = date.AddMonths(1);
            date = date.AddDays(-DateTime.Now.Day+1);

            loan.loanStartDate = date;
            loan.loanAmount = model.loanAmount;
            loan.noOfInsatllments = model.noOfInsatllments;
            loan.loanStatus = model.loanStatus;
            loan.remarks = model.remarks;

            db.SaveChanges();
            return RedirectToAction("LoanList");
        }

        // For Admin
        public ActionResult DeleteLoan(int? id)
        {
            var model = db.Loans.SingleOrDefault(c => c.loanId == id);
            return View(model);
        }

        [HttpPost, ActionName("DeleteLoan")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteLoan(int id, Loan model)
        {
            var loan = db.Loans.SingleOrDefault(c => c.loanId == id);
            db.Loans.Remove(loan);
            db.SaveChanges();
            return RedirectToAction("LoanList");
        }
        public ActionResult DepartmentReport()
        {
            DB51Entities db = new DB51Entities();
            var c = db.DepartmentViews.ToList();
            DepartmentReport rept = new DepartmentReport();
            rept.Load();
            rept.SetDataSource(c);
            Stream s = rept.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            return File(s, "application/pdf");
        }
        public ActionResult RankReport()
        {
            DB51Entities db = new DB51Entities();
            var c = db.rankInfoes.ToList();
            RankReport rept = new RankReport();
            rept.Load();
            rept.SetDataSource(c);
            Stream s = rept.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            return File(s, "application/pdf");
        }
        public ActionResult InsallmentReport()
        {
            DB51Entities db = new DB51Entities();
            var c = db.InstalmentDetails.ToList();
            InstalmentReport rept = new InstalmentReport();
            rept.Load();
            rept.SetDataSource(c);
            Stream s = rept.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            return File(s, "application/pdf");
        }
    }
}