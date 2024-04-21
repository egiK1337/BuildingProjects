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

        public string Add(String enterAdminName, User user)
        {
            if (enterAdminName != null)
            {
                var AdminNamecheck = _context.Admins
                    .Where(b => b.Name.ToUpper().Trim() == enterAdminName.ToUpper().Trim())
                    .FirstOrDefault();
                var UserLoginCheck = _context.Admins
                    .Where(u => u.User.Login.ToUpper().Trim() == user.Login.ToUpper().Trim())
                    .FirstOrDefault();

                if (AdminNamecheck != null || UserLoginCheck != null)
                {
                    return "Такой админ уже есть в базе";
                }
                var newAdmin = new Admin()
                {
                    Name = enterAdminName,
                    Password = enterAdminName,
                    User = new User()
                    {
                        Login = user.Login,
                        Password = user.Password,
                        Roles = Roles.Admin
                    }
                };
                _context.Add(newAdmin);
                _context.SaveChanges();
                return $"Админ {enterAdminName} добавлено в базу";
            }
            return "Вы не ввели имя админа";
        }

    }
}
