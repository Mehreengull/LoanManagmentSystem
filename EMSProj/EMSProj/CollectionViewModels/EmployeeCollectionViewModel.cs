using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EMSProj.Models;

namespace EMSProj.CollectionViewModels
{
    public class EmployeeCollectionViewModel
    {
        public RegisterViewModel ApplicationUser { get; set; }
        public Employee Employee { get; set; }

        public int DepartmentId { get; set; }
        public List<Department> Departments { get; set; }

        public int RankId { get; set; }
        public List<Rank> Ranks { get; set; }

        public int TypeId { get; set; }
        public List<Lookup> Lookups { get; set; }
    }
}