using DataLayer.EfClasses;
using DataLayer.EfCode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.ProjectManagerServices
{
    public class ProjectManagerListServices
    {
        private readonly EfCoreContext _context;

        public ProjectManagerListServices(EfCoreContext context)
        {
            _context = context;
        }

        public List<ProjectManager> List()
        {
            return _context.ProjectManagers.ToList();
        }
    }
}
