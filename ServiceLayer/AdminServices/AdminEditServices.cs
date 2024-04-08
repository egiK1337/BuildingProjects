using DataLayer.EfCode;

namespace ServiceLayer.AdminServices
{
    public class AdminEditServices
    {
        private readonly EfCoreContext _context;

        public AdminEditServices(EfCoreContext context)
        {
            _context = context;
        }
        public string Edit(string name)
        {
            if (name != null)
            {
                var check = _context.Admins
                       .Where(i => i.Name.ToUpper().Trim() == name.ToUpper().Trim())
                       .FirstOrDefault();
                if (check != null)
                {
                    check.Name = name;
                    _context.SaveChanges();
                    return $"Администатор {check.Name} обновлён";
                }
            }
            return "Такого адмиистратора нет";
        }
    }
}
