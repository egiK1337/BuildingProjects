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

        public Roles Authorisation()
        {

            return new Roles { };
        }

    }
}
