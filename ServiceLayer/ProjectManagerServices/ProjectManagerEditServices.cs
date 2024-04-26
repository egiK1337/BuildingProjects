
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
                    building.ProjectManager = projectManager;
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
