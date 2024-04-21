using DataLayer.EfClasses;
using DataLayer.EfCode;
using System.Data.Entity;

namespace ServiceLayer.ProjectManagerServices
{
    public class ProjectManagerEditServices
    {
        private readonly EfCoreContext _context;

        public ProjectManagerEditServices(EfCoreContext context)
        {
            _context = context;
        }
        public string Edit(string _userId, string _buildId)
        {
            int.TryParse(_userId, out var userId);
            var projectManager = _context.ProjectManagers.FirstOrDefault(e => e.Id == userId);

            if (projectManager != null)
            {
                int.TryParse(_buildId, out var buildId);
                var building = _context.Buildings.Where(b => b.Id == buildId).Include(x => projectManager).FirstOrDefault();

                if (building != null)
                {
                    if (building.ProjectManager != null)
                    {
                        building.ProjectManager.Name = projectManager.Name;
                        building.ProjectManager.Id = projectManager.Id;
                        building.ProjectManager.BuildingId = building.Id;
                        building.ProjectManager.User = projectManager.User;
                    }
                    else
                    {
                        building.ProjectManager = new ProjectManager
                        {
                            Id = projectManager.Id,
                            Name = projectManager.Name,
                            BuildingId = building.Id,
                            User = projectManager.User 
                        };
                    }

                    _context.SaveChanges();
                    return $"Руководитель проекта: {projectManager.Name} назначен на объект: {building.Name}";
                }
                else
                {
                    return $"Такого руководителя проекта нет";
                }
            }
            return "Такого руководителя проекта нет";
        }
    }
}
