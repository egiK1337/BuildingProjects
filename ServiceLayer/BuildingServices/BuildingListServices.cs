using DataLayer.EfClasses;
using DataLayer.EfCode;
using System.Data.Entity;

namespace ServiceLayer.BuildingServices
{
    public class BuildingListServices
    {
        private readonly EfCoreContext _context;

        public BuildingListServices(EfCoreContext context)
        {
            _context = context;
        }
        public List<Building> List()
        {
            return _context.Buildings
                  .Include(e => e.Engineer)
                  .Include(c => c.ChiefEngineer)
                  .Include(p => p.ProjectManager)
                  .ToList();
        }
    }
}
