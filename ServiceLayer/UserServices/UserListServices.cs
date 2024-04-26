
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
        public List<string> List()
        {
            var listEngineers = _context.Engineers.ToList();
            var listChiefEngineers = _context.ChiefEngineers.ToList();
            var listProjectManagers = _context.ProjectManagers.ToList();
            var listAdmin = _context.Admins.ToList();

            var users = new List<string>();

            foreach (var item in listEngineers)
            {
                users.Add("Id: " + item.Id + "; Имя: " + item.Name + "; Статус - " + Roles.Engineer);
            }
            foreach (var item in listChiefEngineers)
            {
                users.Add("Id: " + item.Id + "; Имя: " + item.Name + "; Статус - " + Roles.ChiefEngineer);
            }
            foreach (var item in listProjectManagers)
            {
                users.Add("Id: " + item.Id + "; Имя: " + item.Name + " Статус - " + Roles.ProjectManager);
            }
            foreach (var item in listAdmin)
            {
                users.Add("Id: " + item.Id + "; Имя: " + item.Name + " Статус - " + Roles.Admin);
            }

            return users;
        }
    }
}
