using DataLayer.EfClasses;
using Telegram.Bot;
using Telegram.Bot.Types.ReplyMarkups;
using User = DataLayer.EfClasses.User;

namespace TelegramBot.Handlers
{
    internal class MenuHandler
    {
        public static void ShowMainMenu(long chatId, ITelegramBotClient botClient, User currentUser)
        {
            switch (currentUser.Roles)
            {
                case Roles.Guest:
                    var guestKeyboard = new ReplyKeyboardMarkup(new[]
    {
                     new[] { new KeyboardButton("/addName"), new KeyboardButton("/exit") }
                });
                    botClient.SendTextMessageAsync(chatId, "Choose an action:", replyMarkup: guestKeyboard);
                    break;

                case Roles.Engineer:
                    var engineerKeyboard = new ReplyKeyboardMarkup(new[]
                    {
                     new[] { new KeyboardButton( "/list" ), new KeyboardButton("/exit") }
                });
                    botClient.SendTextMessageAsync(chatId, "Choose an action:", replyMarkup: engineerKeyboard);
                    break;

                case Roles.Admin:
                    var adminKeyboard = new ReplyKeyboardMarkup(new[]
                    {
                    new[] 
                    { 
                        new KeyboardButton( "/adminAdd" ), 
                        new KeyboardButton( "/delete" ), 
                        new KeyboardButton( "/update" ),
                        new KeyboardButton( "/exit" )
                    }
                });
                    botClient.SendTextMessageAsync(chatId, "Choose an action:", replyMarkup: adminKeyboard);
                    break;

                case Roles.ChiefEngineer:
                    var chiefEngineerKeyboard = new ReplyKeyboardMarkup(new[]
                    {
                    new[] { new KeyboardButton( "/add" ), new KeyboardButton( "/delete" ), new KeyboardButton( "/update" ) }
                });
                    botClient.SendTextMessageAsync(chatId, "Choose an action:", replyMarkup: chiefEngineerKeyboard);
                    break;

                case Roles.ProjectManager:
                    var projectManagerKeyboard = new ReplyKeyboardMarkup(new[]
                    {
                     new[] { new KeyboardButton( "/add" ), new KeyboardButton( "/delete" ), new KeyboardButton( "/update" ) }
                });
                    botClient.SendTextMessageAsync(chatId, "Choose an action:", replyMarkup: projectManagerKeyboard);
                    break;

            }

        }
    }
}
