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

        public string Add(string enterProjectManager)
        {
            if (enterProjectManager != null)
            {
                var check = _context.ProjectManagers
                    .Where(b => b.Name.ToUpper().Trim() == enterProjectManager.ToUpper().Trim())
                    .FirstOrDefault();
                if (check != null)
                {
                    return "Такой руководитель проекта уже есть в базе";
                }
                var newProjectManager = new ProjectManager()
                {
                    Name = enterProjectManager,
                    User = new User() { Roles = Roles.ProjectManager }
                };
                _context.Add(newProjectManager);
                _context.SaveChanges();
                return $"Руководитель проекта {enterProjectManager} добавлен в базу";
            }
            return "Вы не ввели имя руководителя проекта";
        }

        //public void Add()
        //{
        //var newProjectManager = new ProjectManager()
        //{
        //    Name = "Василий Васильевич Васильев",
        //    Building = new Building
        //    {
        //        Name = "Карамельный домик",
        //        ChiefEngineer = new ChiefEngineer
        //        {
        //            Name = "Пётр Петрович Петров",
        //            User = new User
        //            {
        //                Login = "2",
        //                Password = "2",
        //                Roles = Roles.ChiefEnginer
        //            }
        //        },
        //        Engineer = new List<Engineer>()
        //        {
        //            new Engineer
        //            {
        //                Name = "Игорь Игоревич Игорев",
        //                User = new User
        //                {
        //                    Login = "3",
        //                    Password = "3",
        //                    Roles= Roles.Engineer
        //                }
        //            }
        //        }
        //    },
        //    User = new User
        //    {
        //        Login = "1",
        //        Password = "1",
        //        Roles = Roles.ProjectManager
        //    }
        //};

        //_context.Add(newProjectManager);
        //_context.SaveChanges();
        //}

    }
}
