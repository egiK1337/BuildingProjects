using DataLayer.EfCode;


namespace ServiceLayer.EngineerServices
{
    public class EngineerDeleteServices
    {
        private readonly EfCoreContext _context;

        public EngineerDeleteServices(EfCoreContext context)
        {
            _context = context;
        }
        public string Delete(int id)
        {
            var searchEngineer = _context.Engineers.Where(i => i.Id == id).FirstOrDefault();

            if (searchEngineer != null)
            {
                _context.Engineers.Remove(searchEngineer);
                _context.SaveChanges();
                return $"Инженер {searchEngineer.Name} удалён";
            }

            return $"Инженер с {id} нет в базе";
        }

        public string DeleteFromBuild(string _userId, string _buildId)
        {
            int.TryParse(_userId, out var userId);
            var engineer = _context.Engineers.FirstOrDefault(e => e.Id == userId);

            if (engineer != null)
            {
                int.TryParse(_buildId, out var buildId);
                var building = _context.Buildings.Where(b => b.Id == buildId).FirstOrDefault();

                if (building != null)
                {
                    building.ChiefEngineer = null;
                    _context.SaveChanges();
                    return $"Инженер удалён с объекта: {building.Name}";
                }
                else
                {
                    return $"Инженера нет";
                }
            }
            return "Такого инженера нет";
        }
    }
}
