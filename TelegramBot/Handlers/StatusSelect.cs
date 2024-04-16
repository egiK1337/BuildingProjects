

using TelegramBot.Enum;

namespace TelegramBot.Handlers
{
    internal class StatusSelect
    {
        public static StateAction StatusSelector(string comand)
        {
            var dict = new Dictionary<string, StateAction>()
            {
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
                {"/projectManagerList", StateAction.ProjectManagerList}
            };

            var state = dict[comand];

            return state;
        }

    }
}
