using DataLayer.EfClasses;
using DataLayer.EfCode;
using ServiceLayer.ProjectManagerServices;

namespace TelegramBot.TelegramServices
{
    public class ProjectManagerLogic
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

        //public static void ProjectManagerAdd(string str)
        //{
        //    using var dbContext = new EfCoreContext();

        //    var projectManagerAddServices = new ProjectManagerAddServices(dbContext);

        //    projectManagerAddServices.Add(str);
        //}

        public string Add(string enterString)
        {
            return _projectManagerAddServices.Add(enterString);
        }

        public string Delete(int id)
        {
            return _projectManagerDeleteServices.Delete(id);
        }

        public string Edit(string name)
        {
            return _projectManagerEditServices.Edit(name);
        }

        public List<ProjectManager> List()
        {
            return _projectManagerListServices.List();
        }
    }
}
