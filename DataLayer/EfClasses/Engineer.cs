

namespace DataLayer.EfClasses
{
    public class Engineer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int BuildingId { get; set; }
        public virtual User User { get; set; }
        public virtual List<Building>? Building { get; set; }
        //public virtual Building Building { get; set; }

        public Engineer()
        {

        }
    }
}
