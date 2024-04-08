using DataLayer.EfCode;
using Telegram.Bot;
using Telegram.Bot.Types;
using TelegramBot.TelegramServices;

namespace TelegramBot.Handlers
{
    public static class AdminHandler
    {

        public static async Task Add(ITelegramBotClient client, Update update, CancellationToken ct)
        {
            var message = update.Message.Text;

            var efCoreContext = new EfCoreContext();

            var adminLogic = new AdminLogic(efCoreContext);

            if (message != null)
            {
                if (!message.Equals("/addAdmin"))
                {
                    var mes = adminLogic.Add(message);
                    await client.SendTextMessageAsync(chatId: update.Message!.Chat.Id,
                            text: mes.Item2);

                    //await client.SendTextMessageAsync(chatId: update.Message!.Chat.Id,
                    //        text: "Введите логин");
                    //var login = adminLogic.Login(mes.Item1, message.Text);
                    //await client.SendTextMessageAsync(chatId: update.Message!.Chat.Id,
                    //       text: login);

                    //await client.SendTextMessageAsync(chatId: update.Message!.Chat.Id,
                    //      text: "Введите пароль");
                    //var password = adminLogic.Password(mes.Item1, message.Text);
                    //await client.SendTextMessageAsync(chatId: update.Message!.Chat.Id,
                    //       text: password);
                }
                else
                {
                    await client.SendTextMessageAsync(chatId: update.Message!.Chat.Id,
                            text: "Вы не ввели имя администратора");
                }
            }
            else
            {
                await client.SendTextMessageAsync(chatId: update.Message!.Chat.Id,
                            text: "Введите имя администратора");
            }
        }


        //public static async Task Add(ITelegramBotClient client, Update update, CancellationToken ct)
        //{
        //    var message = update.Message.Text;

        //    var efCoreContext = new EfCoreContext();

        //    var adminLogic = new AdminLogic(efCoreContext);

        //    if (message != null)
        //    {
        //        var mes = adminLogic.Add(message);
        //        await client.SendTextMessageAsync(chatId: update.Message!.Chat.Id,
        //                text: mes.Item2);

        //        //await client.SendTextMessageAsync(chatId: update.Message!.Chat.Id,
        //        //        text: "Введите логин");
        //        //var login = adminLogic.Login(mes.Item1, message.Text);
        //        //await client.SendTextMessageAsync(chatId: update.Message!.Chat.Id,
        //        //       text: login);

        //        //await client.SendTextMessageAsync(chatId: update.Message!.Chat.Id,
        //        //      text: "Введите пароль");
        //        //var password = adminLogic.Password(mes.Item1, message.Text);
        //        //await client.SendTextMessageAsync(chatId: update.Message!.Chat.Id,
        //        //       text: password);            
        //    }
        //    else
        //    {
        //        await client.SendTextMessageAsync(chatId: update.Message!.Chat.Id,
        //                    text: "Введите имя администратора");
        //    }
        //}

    }
}
