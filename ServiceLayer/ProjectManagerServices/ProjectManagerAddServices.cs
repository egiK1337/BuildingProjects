using DataLayer.EfClasses;
using DataLayer.EfCode;


namespace ServiceLayer.ProjectManagerServices
{
    public class ProjectManagerAddServices
    {
        private readonly EfCoreContext _context;

        public ProjectManagerAddServices(EfCoreContext context)
        {
            _context = context;
        }

        public string Add(string enterProjectManager, User user)
        {
            if (enterProjectManager != null)
            {
                var projectManagerNamecheck = _context.ProjectManagers
                    .Where(b => b.Name.ToUpper().Trim() == enterProjectManager.ToUpper().Trim())
                    .FirstOrDefault();
                var UserLoginCheck = _context.ProjectManagers
                    .Where(u => u.User.Login.ToUpper().Trim() == user.Login.ToUpper().Trim())
                    .FirstOrDefault();

                if (projectManagerNamecheck != null || UserLoginCheck != null)
                {
                    return "Такой руководитель проекта уже есть в базе";
                }
                var newProjectManager = new ProjectManager()
                {
                    Name = enterProjectManager,
                    User = new User() 
                    {
                        Login = user.Login,
                        Password = user.Password,
                        Roles = Roles.ProjectManager 
                    }
                };
                _context.Add(newProjectManager);
                _context.SaveChanges();
                return $"Руководитель проекта {enterProjectManager} добавлен в базу";
            }
            return "Вы не ввели имя руководителя проекта";
        }
    }
}
