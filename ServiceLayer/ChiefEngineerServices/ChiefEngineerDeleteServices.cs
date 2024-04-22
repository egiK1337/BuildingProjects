using DataLayer.EfCode;
using Microsoft.EntityFrameworkCore;

namespace ServiceLayer.ChiefEngineerServices
{
    public class ChiefEngineerDeleteServices
    {
        private readonly EfCoreContext _context;

        public ChiefEngineerDeleteServices(EfCoreContext context)
        {
            _context = context;
        }
        public string Delete(int id)
        {
            var searchChiefEngineer = _context.ChiefEngineers.Where(i => i.Id == id).FirstOrDefault();

            if (searchChiefEngineer != null)
            {
                _context.ChiefEngineers.Remove(searchChiefEngineer);
                _context.SaveChanges();
                return $"Главный инженер {searchChiefEngineer.Name} удалён";
            }

            return $"Главного инженера с {id} нет в базе";
        }

        public string DeleteFromBuild(string _userId, string _buildId)
        {
            int.TryParse(_userId, out var userId);
            var chiefEngineer = _context.ChiefEngineers.FirstOrDefault(e => e.Id == userId);

            if (chiefEngineer != null)
            {
                int.TryParse(_buildId, out var buildId);
                var building = _context.Buildings.Where(b => b.Id == buildId).FirstOrDefault();

                if (building != null)
                {
                    building.ChiefEngineer = null;
                    _context.SaveChanges();
                    return $"Главный инженер удалён с объекта: {building.Name}";
                }
                else
                {
                    return $"Такого главного инженера нет";
                }
            }
            return "Такого главного инженера нет";
        }
    }
}
