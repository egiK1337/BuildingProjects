using DataLayer.EfClasses;
using DataLayer.EfCode;
using ServiceLayer.AdminServices;


namespace TelegramBot.TelegramServices
{
    internal class AdminLogic
    {
        private readonly AdminAddServices _adminAddServices;
        private readonly AdminEditServices _adminEditServices;
        private readonly AdminDeleteServices _adminDeleteServices;
        private readonly AdminListServices _adminListServices;

        public AdminLogic(EfCoreContext context)
        {
            _adminAddServices = new AdminAddServices(context);
            _adminListServices = new AdminListServices(context);
            _adminEditServices = new AdminEditServices(context);
            _adminDeleteServices = new AdminDeleteServices(context);
        }

        public string Add(string enterString, User user)
        {
            return _adminAddServices.Add(enterString, user);
        }

        public string Delete(int id)
        {
            return _adminDeleteServices.Delete(id);
        }

        public string Edit(string name)
        {
            return _adminEditServices.Edit(name);
        }

        public List<Admin> List()
        {
            return _adminListServices.List();
        }

        public void AddAdmin()
        {
            _adminAddServices.AddAdmin();
        }
    }
}
