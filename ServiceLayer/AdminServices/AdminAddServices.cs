using DataLayer.EfClasses;
using DataLayer.EfCode;
using Telegram.Bot.Types;
using User = DataLayer.EfClasses.User;

namespace ServiceLayer.AdminServices
{
    public class AdminAddServices
    {
        private readonly EfCoreContext _context;

        public AdminAddServices(EfCoreContext context)
        {
            _context = context;
        }

        public void AddAdmin()
        {           
            var newAdmin = new Admin()
            {
                Name = "Admin",
                User = new User()
                {
                    Login = "Admin",
                    Password = "123",
                    Roles = Roles.Admin
                }
            };
            _context.Add(newAdmin);
            _context.SaveChanges();
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
