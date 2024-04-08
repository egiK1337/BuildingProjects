using DataLayer.EfClasses;
using DataLayer.EfCode;

namespace ServiceLayer.ChiefEngineerServices
{
    public class ChiefEngineerListServices
    {
        private readonly EfCoreContext _context;

        public ChiefEngineerListServices(EfCoreContext context)
        {
            _context = context;
        }

        public List<ChiefEngineer> List()
        {
            return _context.ChiefEngineers.ToList();
        }
    }
}
