using DataLayer.EfClasses;
using DataLayer.EfCode;


namespace ServiceLayer.BuildingServices
{
    public class BuildingAddServices
    {
        private readonly EfCoreContext _context;

        public BuildingAddServices(EfCoreContext context)
        {
            _context = context;
        }

        public string Add(String enterBuildingName)
        {
            if (enterBuildingName != null)
            {
                var check = _context.Buildings
                    .Where(b => b.Name.ToUpper().Trim() == enterBuildingName.ToUpper().Trim())
                    .FirstOrDefault();

                if (check != null)
                {
                    return "Такое строение уже есть в базе";                    
                }
                var newBuild = new Building()
                {
                    Name = enterBuildingName
                };

                _context.Add(newBuild);
                _context.SaveChanges();

                return $"Строение {enterBuildingName} добавлено в базу";
            }
            return "Вы не ввели название строения";
        }
    }
}
