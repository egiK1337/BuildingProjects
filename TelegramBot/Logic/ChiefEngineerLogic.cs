using DataLayer.EfClasses;
using DataLayer.EfCode;
using ServiceLayer.ChiefEngineerServices;

namespace TelegramBot.TelegramServices
{
    internal class ChiefEngineerLogic
    {
        private readonly ChiefEngineerAddServices _chiefEngineerAddServices;
        private readonly ChiefEngineerDeleteServices _chiefEngineerDeleteServices;
        private readonly ChiefEngineerEditServices _chiefEngineerEditServices;
        private readonly ChiefEngineerListServices _chiefEngineerListServices;

        public ChiefEngineerLogic(EfCoreContext context)
        {
            _chiefEngineerAddServices = new ChiefEngineerAddServices(context);
            _chiefEngineerDeleteServices = new ChiefEngineerDeleteServices(context);
            _chiefEngineerEditServices = new ChiefEngineerEditServices(context);
            _chiefEngineerListServices = new ChiefEngineerListServices(context);
        }

        public string Add(string enterString, User user)
        {
            return _chiefEngineerAddServices.Add(enterString, user);
        }

        public string Delete(int id)
        {
            return _chiefEngineerDeleteServices.Delete(id);
        }

        public string Edit(string _userId, string _buildId)
        {
            return _chiefEngineerEditServices.Edit(_userId, _buildId);
        }

        public List<ChiefEngineer> List()
        {
            return _chiefEngineerListServices.List();
        }
    }
}
