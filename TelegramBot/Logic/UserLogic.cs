using DataLayer.EfClasses;
using DataLayer.EfCode;
using ServiceLayer.BuildingServices;
using ServiceLayer.UserServices;
using Telegram.Bot;
using Telegram.Bot.Types;
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
    }
}
