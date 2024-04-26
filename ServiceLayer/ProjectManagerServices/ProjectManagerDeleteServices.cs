using DataLayer.EfCode;

namespace ServiceLayer.ProjectManagerServices
{
    public class ProjectManagerDeleteServices
    {
        private readonly EfCoreContext _context;

        public ProjectManagerDeleteServices(EfCoreContext context)
        {
            _context = context;
        }

        public string Delete(int id)
        {
            var searchProjectManager = _context.ProjectManagers.Where(i => i.Id == id).FirstOrDefault();

            if (searchProjectManager != null)
            {
                _context.ProjectManagers.Remove(searchProjectManager);
                _context.SaveChanges();

                return $"Руководитель проекта {searchProjectManager.Name} удалён";
            }

            return $"Руководитель проекта с {id} нет в базе";
        }
    }
}
