
namespace TelegramBot.Enum
{
    internal enum StateAction
    {
        Start,
        AdminAdd,
        EngineerAdd,
        ProjectManagerAdd,       
        BuildAdd,
        ChiefEngineerAdd,
        EngineerAddToBuild,
        ChiefEngineerAddToBuild,
        ProjectManagerAddToBuild,
        AdminDel,
        EngineerDel,
        ChiefEngineerDel,
        ProjectManagerDel,
        EngineerDelBuild,
        ChiefEngineerDelBuild,
        ProjectManagerDelBuild,
        ChiefEngineerList,
        AdminList,
        EngineerList,
        ProjectManagerList,
        BuildList,
        UserList,
        Error
    }
}
