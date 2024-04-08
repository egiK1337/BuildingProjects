using DataLayer.EfCode;

namespace ServiceLayer.AdminServices
{
    public class AdminDeleteServices
    {
        private readonly EfCoreContext _context;

        public AdminDeleteServices(EfCoreContext context)
        {
            _context = context;
        }
        public string Delete(int id)
        {
            var searchAdmin = _context.Admins.Where(i => i.Id == id).FirstOrDefault();

            if (searchAdmin != null)
            {
                _context.Admins.Remove(searchAdmin);
                _context.SaveChanges();
                return $"Администратор {searchAdmin.Name} удалён";
            }

            return $"Администратора с {id} нет в базе";
        }
    }
}
