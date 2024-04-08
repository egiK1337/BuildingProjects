
using DataLayer.EfClasses;
using DataLayer.EfCode;

namespace ServiceLayer.UserServices
{
    public class UserListServices 
    {
        private readonly EfCoreContext _context;

        public UserListServices(EfCoreContext context)
        {
            _context = context;
        }

        public List<object> List()
        {
            var users = new List<object>();

            var admins = _context.ProjectManagers.ToList();
            foreach (var admin in admins)
            {
                users.Add(admin);
            }
            var ChiefEngineers = _context.ChiefEngineers.ToList();
            foreach (var chiefEngineer in ChiefEngineers)
            {
                users.Add(chiefEngineer);
            }
            var Engineers = _context.Engineers.ToList();
            foreach (var engineer in Engineers)
            {
                users.Add(engineer);
            }
            var ProjectManagers = _context.ProjectManagers.ToList();
            foreach (var projectManager in ProjectManagers)
            {
                users.Add(projectManager);
            }

            return new List<object>();
        }

    }
}
