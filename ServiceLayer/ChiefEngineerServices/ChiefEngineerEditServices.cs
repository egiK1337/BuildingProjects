using DataLayer.EfCode;


namespace ServiceLayer.ChiefEngineerServices
{
    public class ChiefEngineerEditServices
    {
        private readonly EfCoreContext _context;

        public ChiefEngineerEditServices(EfCoreContext context)
        {
            _context = context;
        }
        public string Edit(string name)
        {
            if (name != null)
            {
                var check = _context.ChiefEngineers
                       .Where(i => i.Name.ToUpper().Trim() == name.ToUpper().Trim())
                       .FirstOrDefault();
                if (check != null)
                {
                    check.Name = name;
                    _context.SaveChanges();
                    return $"Главный инженер {check.Name} обновлёно";
                }
            }
            return "Такого главного инженера нет";
        }
    }
}
