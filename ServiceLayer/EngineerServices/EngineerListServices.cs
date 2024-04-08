
using DataLayer.EfClasses;
using DataLayer.EfCode;

namespace ServiceLayer.EngineerServices
{
    public class EngineerListServices
    {
        private readonly EfCoreContext _context;

        public EngineerListServices(EfCoreContext context)
        {
            _context = context;
        }

        public List<Engineer> List()
        {
            return _context.Engineers.ToList();
        }
    }
}
