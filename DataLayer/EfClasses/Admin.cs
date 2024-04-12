
namespace DataLayer.EfClasses
{
    public class Admin
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public virtual User User { get; set; }

    }

}
