using DataLayer.EfClasses;
using DataLayer.EfCode;


namespace ServiceLayer.EngineerServices
{
    public class EngineerAddServices
    {
        private readonly EfCoreContext _context;

        public EngineerAddServices(EfCoreContext context)
        {
            _context = context;
        }

        public string Add(string enterEngineer, User user)
        {
            if (enterEngineer != null)
            {
                var check = _context.Engineers
                    .Where(b => b.Name.ToUpper().Trim() == enterEngineer.ToUpper().Trim())
                    .FirstOrDefault();
                if (check != null)
                {
                    return "Такой инженер уже есть в базе";
                }
                var newEngineer = new Engineer()
                {
                    Name = enterEngineer,
                    User = new User()
                    {
                        Login = user.Login,
                        Password = user.Password,
                        Roles = user.Roles
                    }

                };
                _context.Add(newEngineer);
                _context.SaveChanges();
                return $"Инженер {enterEngineer} добавлен в базу";
            }
            return "Вы не ввели имя инженера";
        }
    }
}
