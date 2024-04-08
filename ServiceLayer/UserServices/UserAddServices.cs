using DataLayer.EfCode;
using Microsoft.EntityFrameworkCore;
using Telegram.Bot;
using Telegram.Bot.Types;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using Update = Telegram.Bot.Types.Update;

namespace ServiceLayer.UserServices
{
    public class UserAddServices
    {
        private readonly EfCoreContext _context;

        public UserAddServices(EfCoreContext context)
        {
            _context = context;
        }

        public async Task Add(ITelegramBotClient client, Update update, CancellationToken ct)
        {

        }

        public void Add()
        {
            var newUser = new User()
            { 

            };
        }

    }
}
