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

        public UserLogic(EfCoreContext context)
        {
            _userAuthorizationServices = new UserAuthorizationServices(context);
        }

        public Roles RoleFinder(User user)
        {
            return _userAuthorizationServices.RoleFinder(user);
        }

        public static async Task RequestId(long chatId, ITelegramBotClient botClient)
        {
            await botClient.SendTextMessageAsync(chatId,
                "Введите Id пользователя которого неообходимо удалить (пример: 1):");
        }


    }
}
