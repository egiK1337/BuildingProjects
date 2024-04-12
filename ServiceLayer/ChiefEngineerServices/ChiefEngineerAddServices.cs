using DataLayer.EfClasses;
using DataLayer.EfCode;


namespace ServiceLayer.ChiefEngineerServices
{
    public class ChiefEngineerAddServices
    {
        private readonly EfCoreContext _context;

        public ChiefEngineerAddServices(EfCoreContext context)
        {
            _context = context;
        }

        public string Add(string enterChiefEngineer)
        {
            if (enterChiefEngineer != null)
            {
                var check = _context.ChiefEngineers
                    .Where(b => b.Name.ToUpper().Trim() == enterChiefEngineer.ToUpper().Trim())
                    .FirstOrDefault();
                if (check != null)
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
