using DataLayer.EfCode;


namespace ServiceLayer.EngineerServices
{
    public class EngineerEditServices
    {
        private readonly EfCoreContext _context;

        public EngineerEditServices(EfCoreContext context)
        {
            _context = context;
        }
        public string Edit(string name)
        {
            if (name != null)
            {
                var check = _context.Engineers
                       .Where(i => i.Name.ToUpper().Trim() == name.ToUpper().Trim())
                       .FirstOrDefault();
                if (check != null)
                {
                    check.Name = name;
                    _context.SaveChanges();
                    return $"Инженер {check.Name} обновлёно";
                }
            }
            return "Такого инженера нет";
        }
    }
}
