using DataLayer.EfClasses;
using DataLayer.EfCode;
using Telegram.Bot;
using Telegram.Bot.Types;
using TelegramBot.Enum;
using TelegramBot.TelegramServices;
using User = DataLayer.EfClasses.User;

namespace TelegramBot.Handlers
{
    internal class AuthenticationHandler
    {
        public static async Task RequestLoginPassword(long chatId, ITelegramBotClient botClient)
        {
            await botClient.SendTextMessageAsync(chatId,
                "Введите логин и пароль (пример: User123 Password123):"); // Логика получения логина
        }


        public static string Authentication(Message message, State currentState, User currentUser)
        {
            //TODO auth logic goes here
            //just for show case
            currentState = !string.IsNullOrWhiteSpace(message.Text) && message.Text.Split(" ").Length > 1
                ? State.Authenticated
                : State.AuthInProgress;
            if (currentState == State.Authenticated)
            {
                var loginPassword = message.Text.Split();

                currentUser.Login = loginPassword[0];
                currentUser.Password = loginPassword[1];

                var efCoreContext = new EfCoreContext();
                var userLogic = new UserLogic(efCoreContext);
                currentUser.Roles = userLogic.RoleFinder(currentUser);
                efCoreContext.Dispose();
                if (currentUser.Roles != Roles.Guest)
                {
                    return "Логин и пароль приняты";
                }
            }
            return "Логин или пароль введены неправильно";
        }
    }
}
