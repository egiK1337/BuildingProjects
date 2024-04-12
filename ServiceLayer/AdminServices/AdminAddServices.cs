using DataLayer.EfClasses;
using DataLayer.EfCode;


namespace ServiceLayer.AdminServices
{
    public class AdminAddServices
    {
        private readonly EfCoreContext _context;

        public AdminAddServices(EfCoreContext context)
        {
            _context = context;
        }

        public string Add(String enterAdminName)
        {
            if (enterAdminName != null)
            {
                var check = _context.Admins
                    .Where(b => b.Name.ToUpper().Trim() == enterAdminName.ToUpper().Trim())
                    .FirstOrDefault();

                if (check != null)
                {
                    return "Такой админ уже есть в базе";
                }
                var newAdmin = new Admin()
                {
                    Name = enterAdminName,
                    Password = enterAdminName,
                    User = new User() { Roles = Roles.Admin }
                };
                _context.Add(newAdmin);
                _context.SaveChanges();
                return $"Админ {enterAdminName} добавлено в базу";
            }
            return "Вы не ввели имя админа";
        }


        //public (Admin, string) Add(string enterAdmin)
        //{
        //    if (enterAdmin != null)
        //    {
        //        var check = _context.Admins
        //            .Where(b => b.Name.ToUpper().Trim() == enterAdmin.ToUpper().Trim())
        //            .FirstOrDefault();

        //        if (check != null)
        //        {
        //            return (check, "Такой администратор уже есть в базе");
        //        }
        //        else
        //        {
        //            var newAdmin = new Admin()
        //            {
        //                Name = enterAdmin,
        //                Password = enterAdmin,
        //                User = new User() { Roles = Roles.Admin }
        //            };

        //            _context.Add(newAdmin);
        //            _context.SaveChanges();
        //            return (newAdmin, $"Администратор {enterAdmin} добавлено в базу");
        //        }
        //    }
        //    var a = new Admin();
        //    a = null;
        //    return (a, "Вы не ввели имя администратора");
        //}

        //public string Login(Admin admin, string login)
        //{
        //    if (admin == null)
        //    {
        //        return "Нельзя добавить логин к не существуещему администартору";
        //    }
        //    if (login != null)
        //    {
        //        var adminsCheck = _context.Admins
        //            .Where(i => i.User.Login == login);
        //        var chiefEngineerCheck = _context.ChiefEngineers
        //            .Where(i => i.User.Login == login);
        //        var engineerCheck = _context.Engineers
        //            .Where(i => i.User.Login == login);
        //        var projectManagerCheck = _context.ProjectManagers
        //            .Where(i => i.User.Login == login);

        //        if (adminsCheck == null &&
        //            chiefEngineerCheck == null &&
        //            engineerCheck == null &&
        //            projectManagerCheck == null)
        //        {
        //            admin.User.Login = login;
        //            _context.SaveChanges();
        //        }
        //    }

        //    return $"К администратору: {admin.Name} добавлен логин: {admin.User.Login}";
        //}

        //public string Password(Admin admin, string password)
        //{
        //    if (admin == null)
        //    {
        //        return "Нельзя добавить пароль к не существуещего администартора";
        //    }
        //    if (password != null)
        //    {
        //        admin.User.Password = password;
        //        _context.SaveChanges();
        //    }
        //    return admin.Name;
        //}
    }
}
