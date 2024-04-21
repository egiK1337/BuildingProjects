using DataLayer.EfClasses;
using DataLayer.EfCode;


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
