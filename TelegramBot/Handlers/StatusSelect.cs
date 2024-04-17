

using ServiceLayer.Abstractions.DTO.Admin;
using TelegramBot.Enum;

namespace TelegramBot.Handlers
{
    internal class StatusSelect
    {
        public static StateAction StatusSelector(string comand)
        {
            var dict = new Dictionary<string, StateAction>()
            {
                {"/Start", StateAction.Start},
                {"/adminAdd", StateAction.AdminAdd},
                {"/engineerAdd", StateAction.EngineerAdd},
                {"/chiefEngineerAdd", StateAction.ChiefEngineerAdd},
                {"/projectManagerAdd", StateAction.ProjectManagerAdd},
                {"/adminDel", StateAction.AdminDel},
                {"/engineerDel", StateAction.EngineerDel},
                {"/chiefEngineerDel", StateAction.ChiefEngineerDel},
                {"/projectManagerDel", StateAction.ProjectManagerDel},
                {"/adminList", StateAction.AdminList},
                {"/engineerList", StateAction.EngineerList},
                {"/chiefEngineerList", StateAction.ChiefEngineerList},
                {"/projectManagerList", StateAction.ProjectManagerList},
                {"/BuildList", StateAction.BuildList}
            };

            StateAction stateAction = new StateAction();

            if (dict.ContainsKey(comand))
            {
                stateAction = dict[comand];
                return stateAction;
            }
            else
            {
                return StateAction.Error;
            }

           
        }

    }
}
