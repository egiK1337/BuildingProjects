using DataLayer.EfClasses;
using DataLayer.EfCode;
using ServiceLayer.ProjectManagerServices;

namespace TelegramBot.TelegramServices
{
    internal class ProjectManagerLogic
    {
        private readonly ProjectManagerAddServices _projectManagerAddServices;
        private readonly ProjectManagerDeleteServices _projectManagerDeleteServices;
        private readonly ProjectManagerEditServices _projectManagerEditServices;
        private readonly ProjectManagerListServices _projectManagerListServices;

        public ProjectManagerLogic(EfCoreContext context)
        {
            _projectManagerAddServices = new ProjectManagerAddServices(context);
            _projectManagerDeleteServices = new ProjectManagerDeleteServices(context);
            _projectManagerEditServices = new ProjectManagerEditServices(context);
            _projectManagerListServices = new ProjectManagerListServices(context);
        }

        public string Add(string enterString, User user)
        {
            return _projectManagerAddServices.Add(enterString, user);
        }

        public string Delete(int id)
        {
            return _projectManagerDeleteServices.Delete(id);
        }

        public string Edit(string _userId, string _buildId)
        {
            return _projectManagerEditServices.Edit(_userId, _buildId);
        }

        public List<ProjectManager> List()
        {
            return _projectManagerListServices.List();
        }
    }
}
