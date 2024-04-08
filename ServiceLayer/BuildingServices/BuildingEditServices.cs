using DataLayer.EfCode;


namespace ServiceLayer.BuildingServices
{
    public class BuildingEditServices
    {
        private readonly EfCoreContext _context;

        public BuildingEditServices(EfCoreContext context)
        {
            _context = context;
        }
        public string Edit(string name)
        {
            if (name != null)
            {
                var check = _context.Buildings
                       .Where(i => i.Name.ToUpper().Trim() == name.ToUpper().Trim())
                       .FirstOrDefault();
                if (check != null)
                {
                    check.Name = name;
                    _context.SaveChanges();
                    return $"Строение {check.Name} обновлёно";
                }
            }
            return "Такого строения нет";
        }
    }
}
