using DataLayer.EfClasses;
using DataLayer.EfCode;

namespace ServiceLayer.BuildingServices
{
    public class BuildingListServices
    {
        private readonly EfCoreContext _context;

        public BuildingListServices(EfCoreContext context)
        {
            _context = context;
        }

        public List<string> ListWithoutEmployees()
        {
            var Employeelist = _context.Buildings.ToList();

            var list = new List<string>();

            foreach (var employee in Employeelist)
            {
                list.Add("Id: " + employee.Id + " " + employee.Name);
            }

            return list;
        }

        public List<string> List()
        {
            var engineers = _context.Engineers
                .ToList();
            var engineersBuildingsSelect = engineers
                .Select(x => x.BuildingId)
                .ToList();
            var buildingsEngineers = _context.Buildings
                .Where(x => engineersBuildingsSelect
                .Contains(x.Id))
                .ToList();

            var chiefEngineers = _context.ChiefEngineers
                .ToList();
            var chiefEngineersBuildingsSelect = chiefEngineers
                .Select(x => x.BuildingId).ToList();
            var buildingsChiefEngineers = _context.Buildings
                .Where(x => chiefEngineersBuildingsSelect
                .Contains(x.Id))
                .ToList();

            var projectManagers = _context.ProjectManagers
                .ToList();
            var projectManagersBuildingsSelect = projectManagers
                .Select(x => x.BuildingId).ToList();
            var buildingsProjectManagers = _context.Buildings
                .Where(x => projectManagersBuildingsSelect
                .Contains(x.Id))
                .ToList();

            var data = "";

            var EmployeeListToString = new List<string>();

            foreach (var item in engineers)
            {
                if (item.Name != null)
                {
                    data = data + "Id:" + item.Id + "; " + "Инженер: " 
                        + item.Name + "; Должность: " + Roles.Engineer + "; ";
                }
                if (item.BuildingId != null)
                {
                    var bE = buildingsEngineers
                        .FirstOrDefault(x => x.Id == item.BuildingId);
                    data = data + "Работает на объекте: " 
                        + "Id:" + bE.Id + "; " + " Здание - " + bE.Name + " ";
                }

                EmployeeListToString.Add(data);
                data = "";
            }

            foreach (var item in chiefEngineers)
            {
                if (item.Name != null)
                {
                    data = data + "Id:" + item.Id + "; " 
                        + " Главный инженер -  " + item.Name 
                        + "; Должность: " + Roles.ChiefEngineer + "; ";
                }
                if (item.BuildingId != null)
                {
                    var bE = buildingsChiefEngineers
                        .FirstOrDefault(x => x.Id == item.BuildingId);
                    data = data + "Работает на объекте: " + "Id:" 
                        + bE.Id + "; " + " Здание - " + bE.Name + " ";
                }
                EmployeeListToString.Add(data);
                data = "";
            }

            foreach (var item in projectManagers)
            {
                if (item.Name != null)
                {
                    data = data + "Id:" + item.Id + "; " 
                        + " Руководитель проекта -  " + item.Name 
                        + "; Должность: " + Roles.ProjectManager + "; ";
                }
                if (item.BuildingId != null)
                {
                    var bE = buildingsProjectManagers
                        .FirstOrDefault(x => x.Id == item.BuildingId);
                    data = data + "Работает на объекте: " 
                        + "Id:" + bE.Id + "; " + " Здание - " + bE.Name + " ";
                } 

                EmployeeListToString.Add(data);
                data = "";
            }


            return EmployeeListToString;
        }
    }
}
