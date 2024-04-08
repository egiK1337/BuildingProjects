using DataLayer.EfCode;


namespace ServiceLayer.BuildingServices
{
    public class BuildingDeleteServices
    {
        private readonly EfCoreContext _context;

        public BuildingDeleteServices(EfCoreContext context)
        {
            _context = context;
        }
        public string Delete(int id)
        {
            var searchBuilding = _context.Buildings.Where(i => i.Id == id).FirstOrDefault();

            if (searchBuilding != null)
            {
                _context.Buildings.Remove(searchBuilding);
                _context.SaveChanges();
                return $"Постройка {searchBuilding.Name} удалёна";
            }

            return $"Постройка с {id} нет в базе";
        }
    }
}
