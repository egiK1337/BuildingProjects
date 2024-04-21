using DataLayer.EfClasses;
using Telegram.Bot.Types;
using TelegramBot.Enum;
using User = DataLayer.EfClasses.User;

namespace TelegramBot.Handlers
{
    internal class RegistrationHandler
    {
        public static void Registration(Message message, State currentState, User currentUser)
        {

            currentState = !string.IsNullOrWhiteSpace(message.Text) && message.Text.Split(" ").Length > 1
                ? State.Registered
                : State.RegInProgress;
            if (currentState == State.Registered)
            {
                var loginPassword = message.Text.Split();

                currentUser.Login = loginPassword[0];
                currentUser.Password = loginPassword[1];
                currentUser.Roles = Roles.Guest;
            }
        }
    }
}
