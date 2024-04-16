using DataLayer.EfClasses;
using TelegramBot.Enum;

namespace TelegramBot.Handlers
{
    internal class ExitHandler
    {
        public static void Exit(StateAction stateAction, State currentState, StateAdd stateAdd, User user)
        {
            stateAction = StateAction.Start;
            currentState = State.Start;
            stateAdd = StateAdd.Start;
            user.Roles = Roles.Guest;
        }
    }
}
