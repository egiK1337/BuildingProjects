using DataLayer.EfClasses;
using DataLayer.EfCode;
using ServiceLayer.UserServices;
using Telegram.Bot;

using User = DataLayer.EfClasses.User;

namespace TelegramBot.TelegramServices
{
    internal class UserLogic
    {
        private readonly UserAuthorizationServices _userAuthorizationServices;
        private readonly UserListServices _userListServices;

        public UserLogic(EfCoreContext context)
        {
            _userAuthorizationServices = new UserAuthorizationServices(context);
            _userListServices = new UserListServices(context);
        }

        public Roles RoleFinder(User user)
        {
            return _userAuthorizationServices.RoleFinder(user);
        }

        public static async Task RequestId(long chatId, ITelegramBotClient botClient)
        {
            await botClient.SendTextMessageAsync(chatId,
                "Введите Id пользователя (пример: 1):");
        }

        public static async Task RequestBuildName(long chatId, ITelegramBotClient botClient)
        {
            await botClient.SendTextMessageAsync(chatId,
                "Введите Id строения (пример: 3):");
        }

        public List<string> List()
        {
           return _userListServices.List();
        }
    }
}
