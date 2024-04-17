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
                    botClient.SendTextMessageAsync(chatId, "Выберите действие:", replyMarkup: guestKeyboard);
                    break;

                case Roles.Engineer:
                    var engineerKeyboard = new ReplyKeyboardMarkup(new[]
                    {
                     new[] { new KeyboardButton( "/list" ), new KeyboardButton("/exit") }
                    });
                    botClient.SendTextMessageAsync(chatId, "Выберите действие:", replyMarkup: engineerKeyboard);
                    break;

                case Roles.Admin:
                    var adminKeyboard = new ReplyKeyboardMarkup(new[]
                    {
                    new[]
                    {
                        new KeyboardButton( "/userList" ),
                        new KeyboardButton( "/adminAdd" ),
                        new KeyboardButton( "/projectManagerAdd" ),
                        new KeyboardButton( "/adminDel" ),
                        new KeyboardButton( "/projectManagerDel" ),
                        new KeyboardButton("/exit")
                    }
                });
                    botClient.SendTextMessageAsync(chatId, "Выберите действие:", replyMarkup: adminKeyboard);
                    break;

                case Roles.ChiefEngineer:
                    var chiefEngineerKeyboard = new ReplyKeyboardMarkup(new[]
                    {
                    new[]
                    {
                        new KeyboardButton( "/buildList" ),
                        new KeyboardButton( "/engineerAdd" ),
                        new KeyboardButton( "/engineerDelBase" ),
                        new KeyboardButton( "/engineerDelBuild" ),
                        new KeyboardButton( "/engineerList" ),
                        new KeyboardButton("/exit")
                    }
                });
                    botClient.SendTextMessageAsync(chatId, "Выберите действие:", replyMarkup: chiefEngineerKeyboard);
                    break;

                case Roles.ProjectManager:
                    var projectManagerKeyboard = new ReplyKeyboardMarkup(new[]
                    {
                    new[]
                    {
                        new KeyboardButton( "/buildList" ),
                        new KeyboardButton( "/chiefEngineerAdd" ),
                        new KeyboardButton( "/buildAdd" ),
                        new KeyboardButton( "/chiefEngineerDelBase" ),
                        new KeyboardButton( "/chiefEngineerDelBuild" ),
                        new KeyboardButton( "/chiefEngineerList" ),
                        new KeyboardButton("/exit")
                    }
                });
                    botClient.SendTextMessageAsync(chatId, "Выберите действие:", replyMarkup: projectManagerKeyboard);
                    break;

            }

        }
    }
}
