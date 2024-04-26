using DataLayer.EfClasses;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.EfCode
{
    public class EfCoreContext : DbContext
    {
        public EfCoreContext()
        {
        }

        public EfCoreContext(DbContextOptions<EfCoreContext> options)
            : base(options)
        {
        }

        public DbSet<Building> Buildings { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<ChiefEngineer> ChiefEngineers { get; set; }
        public DbSet<Engineer> Engineers { get; set; }
        public DbSet<ProjectManager> ProjectManagers { get; set; }
        public IEnumerable<object> ChiefEnginers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseNpgsql("Server=127.0.0.1;Port=5432;Database=BdProjects;User Id=postgres;Password=123;");
        }


    }
}
