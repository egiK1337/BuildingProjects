using DataLayer.EfClasses;
using DataLayer.EfCode;


namespace ServiceLayer.AdminServices
{
    public class AdminListServices
    {
        private readonly EfCoreContext _context;

        public AdminListServices(EfCoreContext context)
        {
            _context = context;
        }

        public List<Admin> List()
        {
            return _context.Admins.ToList();
        }
    }
}
