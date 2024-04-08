

namespace DataLayer.EfClasses
{
    public class Building
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ChiefEngineer ChiefEngineer { get; set; }
        public virtual List<Engineer> Engineer { get; set; }
        public virtual ProjectManager ProjectManager { get; set; }

        public Building()
        {

        }
    }
}
