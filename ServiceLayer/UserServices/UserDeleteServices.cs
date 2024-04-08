
using DataLayer.EfClasses;
using DataLayer.EfCode;

namespace ServiceLayer.UserServices
{
    public class UserDeleteServices
    {
        private readonly EfCoreContext _context;

        public UserDeleteServices(EfCoreContext context)
        {
            _context = context;
        }
        public string Delete(int id, Roles roles)
        {
            object? user = null;

            switch (roles)
            {
                case Roles.Engineer:
                    user = _context.Engineers.Where(i => i.Id == id).FirstOrDefault();
                    break;
                case Roles.Admin:
                    user = _context.Admins.Where(i => i.Id == id).FirstOrDefault();
                    break;
                case Roles.ChiefEnginer:
                    user = _context.ChiefEngineers.Where(i => i.Id == id).FirstOrDefault();
                    break;
                case Roles.ProjectManager:
                    user = _context.ProjectManagers.Where(i => i.Id == id).FirstOrDefault();
                    break;
            }

            if (user != null)
            {

                return $"Пользователь удалён";
            }

            return $"Такого пользователя нет в базе";
        }
    }
}
