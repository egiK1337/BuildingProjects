using DataLayer.EfCode;
using ServiceLayer.UserServices;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegramBot.TelegramServices
{
    public class UserLogic
    {
        private readonly UserAuthorizationServices _userAuthorizationServices;

        public UserLogic()
        {
        }

        public UserLogic(EfCoreContext context) : base()
        {
            _userAuthorizationServices = new UserAuthorizationServices(context);
        }

        //public async Task Authorization(ITelegramBotClient client, Update update, CancellationToken ct)
        //{
        //   await _userAuthorizationServices.Authorization(client, update, ct);
        //}
    }
}
