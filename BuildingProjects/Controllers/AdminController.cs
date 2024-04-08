using DataLayer.EfCode;
using ServiceLayer.AdminServices;

namespace BuildingProjects.Controllers
{
    public class AdminController
    {
        private readonly AdminAddServices _adminAddServices;
        private readonly AdminDeleteServices _adminDeleteServices;
        private readonly AdminEditServices _adminEditServices;
        private readonly AdminListServices _adminListServices;

        public AdminController(EfCoreContext context) : base()
        {
            _adminAddServices = new AdminAddServices(context);
            _adminDeleteServices = new AdminDeleteServices(context);
            _adminEditServices = new AdminEditServices(context);
            _adminListServices = new AdminListServices(context);
        }
    }
}
