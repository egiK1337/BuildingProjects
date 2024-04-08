using DataLayer.EfCode;


namespace ServiceLayer.ProjectManagerServices
{
    public class ProjectManagerEditServices
    {
        private readonly EfCoreContext _context;

        public ProjectManagerEditServices(EfCoreContext context)
        {
            _context = context;
        }
        public string Edit(string name)
        {
            if (name != null)
            {
                var check = _context.ProjectManagers
                       .Where(i => i.Name.ToUpper().Trim() == name.ToUpper().Trim())
                       .FirstOrDefault();
                if (check != null)
                {
                    check.Name = name;
                    _context.SaveChanges();
                    return $"Руководитель проекта {check.Name} обновлён";
                }
            }
            return "Такого руководителя проекта нет";
        }
    }
}
