
using TelegramBot.Enum;

namespace TelegramBot.Handlers
{
    internal class StatusSelect
    {
        public static StateAction StatusSelector(string comand)
        {
            var dict = new Dictionary<string, StateAction>()
            {
                {"/start", StateAction.Start},
                {"/adminAdd", StateAction.AdminAdd},
                {"/engineerAdd", StateAction.EngineerAdd},
                {"/chiefEngineerAdd", StateAction.ChiefEngineerAdd},
                {"/projectManagerAdd", StateAction.ProjectManagerAdd},
                {"/buildAdd", StateAction.BuildAdd},
                {"/engineerAddToBuild", StateAction.EngineerAddToBuild},
                {"/chiefEngineerAddToBuild", StateAction.ChiefEngineerAddToBuild},
                {"/projectManagerAddToBuild", StateAction.ProjectManagerAddToBuild},
                {"/adminDel", StateAction.AdminDel},
                {"/engineerDel", StateAction.EngineerDel},
                {"/chiefEngineerDel", StateAction.ChiefEngineerDel},
                {"/projectManagerDel", StateAction.ProjectManagerDel},
                {"/engineerDelBuild", StateAction.EngineerDelBuild},
                {"/chiefEngineerDelBuild", StateAction.ChiefEngineerDelBuild},
                {"/projectManagerDelBuild", StateAction.ProjectManagerDelBuild},
                {"/adminList", StateAction.AdminList},
                {"/engineerList", StateAction.EngineerList},
                {"/chiefEngineerList", StateAction.ChiefEngineerList},
                {"/projectManagerList", StateAction.ProjectManagerList},
                {"/buildList", StateAction.BuildList},
                {"/userList", StateAction.UserList}
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
