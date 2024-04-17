using DataLayer.EfClasses;
using DataLayer.EfCode;
using User = DataLayer.EfClasses.User;

namespace ServiceLayer.ChiefEngineerServices
{
    public class ChiefEngineerAddServices
    {
        private readonly EfCoreContext _context;

        public ChiefEngineerAddServices(EfCoreContext context)
        {
            _context = context;
        }

        public string Add(string enterChiefEngineer, User user)
        {
            if (enterChiefEngineer != null)
            {
                var сhiefEngineerNamecheck = _context.ChiefEngineers
                    .Where(b => b.Name.ToUpper().Trim() == enterChiefEngineer.ToUpper().Trim())
                    .FirstOrDefault();
                var UserLoginCheck = _context.ChiefEngineers
                    .Where(u => u.User.Login.ToUpper().Trim() == user.Login.ToUpper().Trim())
                    .FirstOrDefault();

                if (сhiefEngineerNamecheck != null || UserLoginCheck != null)
                {
                    return "Такой главный инженер уже есть в базе";
                }
                var newChiefEngineer = new ChiefEngineer()
                {
                    Name = enterChiefEngineer,
                    User = new User() { Roles = Roles.ChiefEngineer }
                };
                _context.Add(newChiefEngineer);
                _context.SaveChanges();
                return $"Главный инженер {enterChiefEngineer} добавлен в базу";
            }
            return "Вы не ввели имя главного инженера";
        }
    }
}
