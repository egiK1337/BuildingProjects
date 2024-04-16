using Telegram.Bot.Types;
using TelegramBot.Enum;
using User = DataLayer.EfClasses.User;

namespace TelegramBot.Handlers
{
    internal class UserHandler
    {
        public static void FillingUser(Message message, StateAdd stateAdd, User currentUser)
        {
            //TODO auth logic goes here
            //just for show case
            stateAdd = !string.IsNullOrWhiteSpace(message.Text) && message.Text.Split(" ").Length > 1
                ? StateAdd.InProgress
                : StateAdd.Finish;
            if (stateAdd == StateAdd.Finish)
            {
                var loginPassword = message.Text.Split();

                currentUser.Login = loginPassword[0];
                currentUser.Password = loginPassword[1];
            }
        }
    }
}
