using DataLayer.EfCode;


namespace ServiceLayer.EngineerServices
{
    public class EngineerDeleteServices
    {
        private readonly EfCoreContext _context;

        public EngineerDeleteServices(EfCoreContext context)
        {
            _context = context;
        }
        public string Delete(int id)
        {
            var searchEngineer = _context.Engineers.Where(i => i.Id == id).FirstOrDefault();

            if (searchEngineer != null)
            {
                _context.Engineers.Remove(searchEngineer);
                _context.SaveChanges();
                return $"Инженер {searchEngineer.Name} удалён";
            }

            return $"Инженер с {id} нет в базе";
        }
    }
}
