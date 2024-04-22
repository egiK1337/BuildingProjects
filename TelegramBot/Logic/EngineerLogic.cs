using DataLayer.EfClasses;
using DataLayer.EfCode;
using ServiceLayer.EngineerServices;

namespace TelegramBot.TelegramServices
{
    internal class EngineerLogic
    {
        private readonly EngineerAddServices _engineerAddServices;
        private readonly EngineerDeleteServices _engineerDeleteServices;
        private readonly EngineerEditServices _engineerEditServices;
        private readonly EngineerListServices _engineerListServices;

        public EngineerLogic(EfCoreContext context)
        {
            _engineerAddServices = new EngineerAddServices(context);
            _engineerDeleteServices = new EngineerDeleteServices(context);
            _engineerEditServices = new EngineerEditServices(context);
            _engineerListServices = new EngineerListServices(context);
        }

        public string Add(string enterString, User user)
        {
            return _engineerAddServices.Add(enterString, user);
        }

        public string Delete(int id)
        {
            return _engineerDeleteServices.Delete(id);
        }

        public string Edit(string _userId,string _buildId)
        {
            return _engineerEditServices.Edit(_userId, _buildId);
        }

        public List<Engineer> List()
        {
            return _engineerListServices.List();
        }

        public string DeleteFromBuild(string _userId, string _buildId)
        {
            return _engineerDeleteServices.DeleteFromBuild(_userId, _buildId);
        }

    }
}
