
using DataLayer.EfCode;
using System.Data.Entity;

namespace ServiceLayer.EngineerServices
{
    public class EngineerEditServices
    {
        private readonly EfCoreContext _context;

        public EngineerEditServices(EfCoreContext context)
        {
            _context = context;
        }

        public string Edit(string _userId, string _buildId)
        {
            int.TryParse(_userId, out var userId);
            var engineer = _context.Engineers.FirstOrDefault(e => e.Id == userId);

            if (engineer != null)
            {
                int.TryParse(_buildId, out var buildId);
                var building = _context.Buildings.Where(b => b.Id == buildId).Include(x => engineer).FirstOrDefault();

                if (building != null)
                {
                    building.Engineer = engineer;
                    _context.SaveChanges();
                    return $"Инженер: {engineer.Name} назначен на объект: {building.Name}";
                }
                else
                {
                    return $"Такого строенния нет";
                }
            }
            return "Такого инженера нет";
        }
    }
}
