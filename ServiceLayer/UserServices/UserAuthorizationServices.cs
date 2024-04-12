using DataLayer.EfClasses;
using DataLayer.EfCode;


namespace ServiceLayer.UserServices
{
    public class UserAuthorizationServices
    {
        private readonly EfCoreContext _context;

        public UserAuthorizationServices(EfCoreContext context)
        {
            _context = context;
        }

        public Roles RoleFinder(User user)
        {
            var admins = _context.Admins
                .Where(i => i.User.Password == user.Password && i.User.Login == user.Login)
                .FirstOrDefault();
            if(admins != null)
            {
                return Roles.Admin;
            }

            var chiefEngineers = _context.ChiefEngineers
                .Where(i => i.User.Password == user.Password && i.User.Login == user.Login)
                .FirstOrDefault();
            if(chiefEngineers != null)
            {
                return Roles.ChiefEngineer;
            }

            var engineers = _context.Engineers
                .Where(i => i.User.Password == user.Password && i.User.Login == user.Login)
                .FirstOrDefault();
            if(engineers != null)
            {
                return Roles.Engineer;
            }

            var projectManagers = _context.ProjectManagers
                .Where(i => i.User.Password == user.Password && i.User.Login == user.Login)
                .FirstOrDefault();
            if(projectManagers != null)
            {
                return Roles.ProjectManager;
            }

            return Roles.Guest;
        }

    }
}
