using DataLayer.EfClasses;
using DataLayer.EfCode;
using System.Data.Entity;

namespace ServiceLayer.ChiefEngineerServices
{
    public class ChiefEngineerEditServices
    {
        private readonly EfCoreContext _context;

        public ChiefEngineerEditServices(EfCoreContext context)
        {
            _context = context;
        }
        public string Edit(string _userId, string _buildId)
        {
            int.TryParse(_userId, out var userId);
            var chiefEngineer = _context.ChiefEngineers.FirstOrDefault(e => e.Id == userId);

            if (chiefEngineer != null)
            {
                int.TryParse(_buildId, out var buildId);
                var building = _context.Buildings.Where(b => b.Id == buildId).Include(x => chiefEngineer).FirstOrDefault();

                if (building != null)
                {
                    if (building.ProjectManager != null)
                    {
                        building.ProjectManager.Name = chiefEngineer.Name;
                        building.ProjectManager.Id = chiefEngineer.Id;
                        building.ProjectManager.BuildingId = building.Id;
                        building.ProjectManager.User = chiefEngineer.User;
                    }
                    else
                    {
                        building.ProjectManager = new ProjectManager
                        {
                            Id = chiefEngineer.Id,
                            Name = chiefEngineer.Name,
                            BuildingId = building.Id,
                            User = chiefEngineer.User
                        };
                    }

                    _context.SaveChanges();
                    return $"Главный инженер: {chiefEngineer.Name} назначен на объект: {building.Name}";
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
