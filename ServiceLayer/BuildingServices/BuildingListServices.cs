using DataLayer.EfClasses;
using DataLayer.EfCode;
using System.Data.Entity;

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
            var Employeelist = _context.Buildings
                  .Include(e => e.Engineer)
                  .ToList();

            var data = "";

            var EmployeeListToString = new List<string>();

            foreach (var item in Employeelist)
            {
                if (item.Name != null)
                {
                    data = data + "Id:" + item.Id + "; " + "Строение: " + item.Name + "; ";
                }
                else if (item.Engineer != null)
                {
                    data = data + "Id:" + item.Id + "; " + " Инженер - " + item.Engineer[1].Name + "; Должность: " + Roles.Engineer + "; ";
                }
                else if (item.ChiefEngineer != null)
                {
                    data = data + "Id:" + item.Id + "; " + " Главный инженер -  " + item.ChiefEngineer.Name + "; Должность: " + Roles.ChiefEngineer + "; ";
                }
                else if (item.ProjectManager != null)
                {
                    data = data + "Id:" + item.Id + "; " + " Руководитель проекта -  " + item.ProjectManager.Name + "; Должность: " + Roles.ProjectManager;
                }

                EmployeeListToString.Add(data);
                data = "";
            }


            return EmployeeListToString;
        }
    }
}
