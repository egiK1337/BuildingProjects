using DataLayer.EfCode;
using Telegram.Bot;
using Telegram.Bot.Types;
using TelegramBot.TelegramServices;

namespace TelegramBot.Handlers
{
    public static class BuildingHandler
    {

        public static async Task Add(ITelegramBotClient client, Update update, CancellationToken ct)
        {
            var message = update.Message.Text;

            var efCoreContext = new EfCoreContext();

            var buildingLogic = new BuildingLogic(efCoreContext);

            if (message != null)
            {
                if (!message.Equals("/addBuild"))
                {
                    if (update.Message.Text != null)
                    {
                        var mes = buildingLogic.Add(message);
                        await client.SendTextMessageAsync(chatId: update.Message!.Chat.Id,
                                text: mes);
                    }
                    else
                    {
                        await client.SendTextMessageAsync(chatId: update.Message!.Chat.Id,
                                text: "Вы не ввели наименование строения");
                    }
                }
                else
                {
                    await client.SendTextMessageAsync(chatId: update.Message!.Chat.Id,
                    text: "Введите название строения");
                }
            }
        }


        //public static async Task Add(ITelegramBotClient client, Update update, CancellationToken ct)
        //{
        //    var efCoreContext = new EfCoreContext();

        //    var buildingLogic = new BuildingLogic(efCoreContext);

        //    if (!string.IsNullOrWhiteSpace(update?.Message?.Text))
        //    {
        //            var mes = buildingLogic.Add(update.Message.Text);
        //            await client.SendTextMessageAsync(chatId: update.Message!.Chat.Id,
        //                    text: mes);          
        //    }
        //    else
        //    {
        //        await client.SendTextMessageAsync(chatId: update.Message!.Chat.Id,
        //        text: "Введите название строения");
        //    }

        //}
    }
}
