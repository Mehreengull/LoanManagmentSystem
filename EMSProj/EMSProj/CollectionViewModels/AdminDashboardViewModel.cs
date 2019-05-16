using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EMSProj.CollectionViewModels
{
    public class AdminDashboardViewModel
    {
        public List<Department> Departments { get; set; }
        public IEnumerable<Employee> Employees { get; set; }
        public List<Rank> Ranks { get; set; }
        public IEnumerable<Loan> Loans { get; set; }
        
        public Department Department { get; set; }
        public Employee Employee { get; set; }
        public Rank rank { get; set; }
        
   
    }
}