using DataLayer.EfCode;

namespace ServiceLayer.ChiefEngineerServices
{
    public class ChiefEngineerDeleteServices
    {
        private readonly EfCoreContext _context;

        public ChiefEngineerDeleteServices(EfCoreContext context)
        {
            _context = context;
        }
        public string Delete(int id)
        {
            var searchChiefEngineer = _context.ChiefEngineers.Where(i => i.Id == id).FirstOrDefault();

            if (searchChiefEngineer != null)
            {
                _context.ChiefEngineers.Remove(searchChiefEngineer);
                _context.SaveChanges();
                return $"Главный инженер {searchChiefEngineer.Name} удалён";
            }

            return $"Главного инженера с {id} нет в базе";
        }
    }
}
