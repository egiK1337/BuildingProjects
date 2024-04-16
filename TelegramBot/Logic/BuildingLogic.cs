using DataLayer.EfClasses;
using DataLayer.EfCode;
using ServiceLayer.AdminServices;
using ServiceLayer.BuildingServices;


namespace TelegramBot.TelegramServices
{
    internal class BuildingLogic
    {
        private readonly BuildingAddServices _buldingAddServices;
        private readonly BuildingDeleteServices _buldingDeleteServices;
        private readonly BuildingEditServices _buldingEditServices;
        private readonly BuildingListServices _buldingListServices;

        public BuildingLogic(EfCoreContext context)
        {
            _buldingAddServices = new BuildingAddServices(context);
            _buldingDeleteServices = new BuildingDeleteServices(context);
            _buldingEditServices = new BuildingEditServices(context);
            _buldingListServices = new BuildingListServices(context);
        }

        public string Add(string enterString)
        {
            return _buldingAddServices.Add(enterString);
        }

        public string Delete(int id)
        {
            return _buldingDeleteServices.Delete(id);
        }

        public string Edit(string name)
        {
            return _buldingEditServices.Edit(name);
        }

        public List<Building> List()
        {
            return _buldingListServices.List();
        }
    }
}
