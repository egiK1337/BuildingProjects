
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


    }
}
