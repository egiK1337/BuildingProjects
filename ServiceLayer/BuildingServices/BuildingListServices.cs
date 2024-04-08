using DataLayer.EfClasses;
using DataLayer.EfCode;


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
            return _context.Buildings.ToList();
        }
    }
}
