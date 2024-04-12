using DataLayer.EfClasses;
using DataLayer.EfCode;
using Microsoft.IdentityModel.Tokens;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using TelegramBot.Enum;
using TelegramBot.TelegramServices;

class Program
{
    private static ITelegramBotClient _botClient;
    private static ReceiverOptions _receiverOptions;
    private static bool _isAuthorized = false;
    private static State _currentState = State.Start;
    private static DataLayer.EfClasses.User _currentUser = new DataLayer.EfClasses.User();


    //private static EngineerLogic _engineerLogic;


    static async Task Main()
    {
        _botClient =
            new TelegramBotClient(
                "6527030864:AAH1uKB7y9yTXg7DXZnAQo8fSQxRejrTSYQ"); // Присваиваем нашей переменной значение, в параметре передаем Token, полученный от BotFather
        _receiverOptions = new ReceiverOptions // Также присваем значение настройкам бота
        {
            AllowedUpdates =
                new
                    [] // Тут указываем типы получаемых Update`ов, о них подробнее расказано тут https://core.telegram.org/bots/api#update
                    {
                        UpdateType.Message, // Сообщения (текст, фото/видео, голосовые/видео сообщения и т.д.)
                        UpdateType.CallbackQuery // Inline кнопки
                    },
            // Параметр, отвечающий за обработку сообщений, пришедших за то время, когда бот был оффлайн
            // True - не обрабатывать, False (стоит по умолчанию) - обрабаывать
            ThrowPendingUpdates = true
        };

        using var cts = new CancellationTokenSource();

        // UpdateHander - обработчик приходящих Update`ов
        // ErrorHandler - обработчик ошибок, связанных с Bot API
        _botClient.StartReceiving(UpdateHandler, ErrorHandler, _receiverOptions, cts.Token); // Запускаем бота

        var me = await _botClient.GetMeAsync(); // Создаем переменную, в которую помещаем информацию о нашем боте.
        Console.WriteLine($"{me.FirstName} запущен!");

        await Task.Delay(-1); // Устанавливаем бесконечную задержку, чтобы наш бот работал постоянно
    }

    private static async Task UpdateHandler(ITelegramBotClient _botClient, Update update,
        CancellationToken cancellationToken)
    {
        var efCoreContext = new EfCoreContext();
        var engineerLogic = new EngineerLogic(efCoreContext);



        if (update.Type == UpdateType.Message && update.Message is { })
        {
            var message = update.Message;

            switch (_currentState)
            {
                case State.Start:
                    if (message.Text.StartsWith("/auth"))
                    {
                        _currentState = State.AuthInProgress;
                        await RequestLoginPassword(message.Chat.Id);
                    }
                    else if (message.Text.StartsWith("/reg"))
                    {
                        _currentState = State.RegInProgress;
                        await RequestLoginPassword(message.Chat.Id);
                    }
                    else
                    {
                        await _botClient.SendTextMessageAsync(message.Chat.Id,
                            "Пожалуйста начните работу с аунтификации используя команду /auth или зарегестрируйтесь в системе используя команду /reg.");
                    }

                    break;

                case State.RegInProgress:
                    // Регистрация пользователя
                    Registration(message);
                    if (_currentState == State.Registered)
                        ShowMainMenu(message.Chat.Id, _currentUser.Roles);
                    else await RequestLoginPassword(message.Chat.Id);
                    break;

                case State.Registered:
                    if (message.Text.StartsWith("/addName"))
                    {
                        _currentState = State.AddingUser;
                        await _botClient.SendTextMessageAsync(message.Chat.Id,
                            "Пожалуйста, введите фамилию и имя пользователя (например, Иванов Иван).:");
                    }
                    else
                    {
                        ShowMainMenu(message.Chat.Id, _currentUser.Roles);
                    }

                    break;

                case State.AuthInProgress:
                    // Аунтификация пользователя
                    var loginPassCheck = Authentication(message);
                    if (_currentState == State.Authenticated)
                    {
                        await _botClient.SendTextMessageAsync(message.Chat.Id, $"{loginPassCheck}. Вы авторизированы в статусе:{_currentUser.Roles}");
                        //_currentState = State.Start;
                        if (_currentUser.Roles != Roles.Guest)
                        {
                            ShowMainMenu(message.Chat.Id, _currentUser.Roles);
                        }
                        else
                        {
                            await _botClient.SendTextMessageAsync(message.Chat.Id,
                             "Пожалуйста начните работу с аунтификации используя команду /auth или зарегестрируйтесь в системе используя команду /reg.");
                        }

                    }
                    else await RequestLoginPassword(message.Chat.Id);
                    break;

                case State.Authenticated:

                    switch (_currentUser.Roles)
                    {
                        case Roles.Engineer:
                            if (message.Text.StartsWith("/list"))
                            {
                                var engineersList = engineerLogic.List();
                                var count = 0;
                                foreach (var item in engineersList)
                                {
                                    await _botClient.SendTextMessageAsync(message.Chat.Id, $"#{count}:{item.Name.ToString()}");
                                    count++;
                                }
                            }
                            else if (message.Text.StartsWith("/exit"))
                            {
                                _currentState = State.Start;
                                await _botClient.SendTextMessageAsync(message.Chat.Id,
                                "Пожалуйста начните работу с аунтификации используя команду /auth или зарегестрируйтесь в системе используя команду /reg.");
                            }
                            else
                            {
                                ShowMainMenu(message.Chat.Id, _currentUser.Roles);
                            }

                            break;
                    }
                    break;

                case State.AddingUser:
                    if (!message.Text.IsNullOrEmpty())
                    {
                        var newUser = engineerLogic.Add(message.Text, _currentUser);
                        _currentUser.Roles = Roles.Engineer;
                        await _botClient.SendTextMessageAsync(message.Chat.Id, newUser);
                    }
                    efCoreContext.Dispose();
                    _currentState = State.Start;

                    break;

                default:
                    break;
            }
        }
    }

    private static async Task RequestLoginPassword(long chatId)
    {
        await _botClient.SendTextMessageAsync(chatId,
            "Введите логин и пароль (пример: User123 Password123):"); // Логика получения логина
    }

    private static void Registration(Message message)
    {
        //TODO auth logic goes here
        //just for show case
        _currentState = !string.IsNullOrWhiteSpace(message.Text) && message.Text.Split(" ").Length > 1
            ? State.Registered
            : State.RegInProgress;
        if (_currentState == State.Registered)
        {
            var loginPassword = message.Text.Split();

            _currentUser.Login = loginPassword[0];
            _currentUser.Password = loginPassword[1];
            _currentUser.Roles = Roles.Guest;
        }
    }

    private static string Authentication(Message message)
    {
        //TODO auth logic goes here
        //just for show case
        _currentState = !string.IsNullOrWhiteSpace(message.Text) && message.Text.Split(" ").Length > 1
            ? State.Authenticated
            : State.AuthInProgress;
        if (_currentState == State.Authenticated)
        {
            var loginPassword = message.Text.Split();

            _currentUser.Login = loginPassword[0];
            _currentUser.Password = loginPassword[1];

            var efCoreContext = new EfCoreContext();
            var userLogic = new UserLogic(efCoreContext);
            _currentUser.Roles = userLogic.RoleFinder(_currentUser);
            efCoreContext.Dispose();
            if (_currentUser.Roles != Roles.Guest)
            {
                return "Логин и пароль приняты";
            }
        }
        return "Логин или пароль введены неправильно";
    }


    private static void ShowMainMenu(long chatId, Roles roles)
    {
        switch (_currentUser.Roles)
        {
            case Roles.Guest:
                var guestKeyboard = new ReplyKeyboardMarkup(new[]
{
                     new[] { new KeyboardButton("/addName"), new KeyboardButton("/exit") }
                });
                _botClient.SendTextMessageAsync(chatId, "Choose an action:", replyMarkup: guestKeyboard);
                break;

            case Roles.Engineer:
                var engineerKeyboard = new ReplyKeyboardMarkup(new[]
                {
                     new[] { new KeyboardButton( "/list" ), new KeyboardButton("/exit") }
                });
                _botClient.SendTextMessageAsync(chatId, "Choose an action:", replyMarkup: engineerKeyboard);
                break;

            case Roles.Admin:
                var adminKeyboard = new ReplyKeyboardMarkup(new[]
                {
                    new[] { new KeyboardButton( "/add" ), new KeyboardButton( "/delete" ), new KeyboardButton( "/update" ) }
                });
                _botClient.SendTextMessageAsync(chatId, "Choose an action:", replyMarkup: adminKeyboard);
                break;

            case Roles.ChiefEngineer:
                var chiefEngineerKeyboard = new ReplyKeyboardMarkup(new[]
                {
                    new[] { new KeyboardButton( "/add" ), new KeyboardButton( "/delete" ), new KeyboardButton( "/update" ) }
                });
                _botClient.SendTextMessageAsync(chatId, "Choose an action:", replyMarkup: chiefEngineerKeyboard);
                break;

            case Roles.ProjectManager:
                var projectManagerKeyboard = new ReplyKeyboardMarkup(new[]
                {
                     new[] { new KeyboardButton( "/add" ), new KeyboardButton( "/delete" ), new KeyboardButton( "/update" ) }
                });
                _botClient.SendTextMessageAsync(chatId, "Choose an action:", replyMarkup: projectManagerKeyboard);
                break;

        }

    }

    private static Task ErrorHandler(ITelegramBotClient botClient, Exception error,
        CancellationToken cancellationToken)
    {
        // Тут создадим переменную, в которую поместим код ошибки и её сообщение 
        var errorMessage = error switch
        {
            ApiRequestException apiRequestException
                => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
            _ => error.ToString()
        };

        Console.WriteLine(errorMessage);
        return Task.CompletedTask;
    }
}







//private static async Task RequestAuthorization(long chatId)
//{
//    await _botClient.SendTextMessageAsync(chatId,
//        "Введи те логин и пароль (пример: User123 Password123):"); // Логика получения логина
//}

//private static void Authorize(Message message)
//{
//    //TODO auth logic goes here
//    //just for show case
//    _currentState = !string.IsNullOrWhiteSpace(message.Text) && message.Text.Split(" ").Length > 1
//        ? State.Authenticated
//        : State.AuthInProgress;
//    if (_currentState == State.Authenticated)
//    {
//        var loginPassword = message.Text.Split();

//        _currentUser.Login = loginPassword[0];
//        _currentUser.Password = loginPassword[1];
//    }
//}










//using Telegram.Bot;
//using Telegram.Bot.Polling;
//using Telegram.Bot.Types;
//using TelegramBot.Handlers;


//namespace TelegramBot
//{
//    public class Program
//    {
//        static async Task SendMenu(ITelegramBotClient client, Update update)
//        {
//            await client.SendTextMessageAsync(chatId: update.Message!.Chat.Id,
//                  text: "Выберите меню:\n\n" +
//                  "/addBuild - добавить здание\n" +
//                  "/addAdmin - добавить админа\n" +
//                  "/exit - вернуться в главное меню");
//        }

//        enum ChatMode
//        {
//            Initial = 0,
//            AddBuild = 1,
//            AddAdmin = 2
//        }

//        public static async Task Main()
//        {
//            var botClient = new TelegramBotClient("6527030864:AAH1uKB7y9yTXg7DXZnAQo8fSQxRejrTSYQ");

//            var dict = new Dictionary<long, ChatMode>();

//            var ro = new ReceiverOptions
//            {
//                AllowedUpdates = new Telegram.Bot.Types.Enums.UpdateType[] { }
//            };

//            botClient.StartReceiving(updateHandler: Handler, pollingErrorHandler: ErrorHandler, receiverOptions: ro);

//            Console.WriteLine("Стартовали");

//            await Task.Delay(Timeout.Infinite);


//            async Task Handler(ITelegramBotClient client, Update update, CancellationToken ct)
//            {
//                //var message = update.Message;

//                //var adminHandler = new AdminHandler();

//                //var buildingHandler = new BuildingHandler();

//                if (!dict.TryGetValue(update.Message!.Chat.Id, out var state))
//                {
//                    dict.Add(update.Message!.Chat.Id, ChatMode.Initial);
//                }

//                state = dict[update.Message!.Chat.Id];


//                if (update.Message.Text == "/exit")
//                {
//                    dict[update.Message!.Chat.Id] = ChatMode.Initial;

//                    await SendMenu(client, update);
//                }

//                else
//                {
//                    switch (state)
//                    {
//                        case ChatMode.AddBuild:
//                            await BuildingHandler.Add(client, update, ct);
//                            break;
//                        //case ChatMode.AddAdmin:
//                        //    await AdminHandler.Add(client, update, ct);
//                        //    break;
//                        default:
//                            switch (update.Message.Text)
//                            {
//                                case "/addBuild":
//                                    await BuildingHandler.Add(client, update, ct);
//                                    dict[update.Message!.Chat.Id] = ChatMode.AddBuild;
//                                    break;
//                                //case "/addAdmin":
//                                //    await AdminHandler.Add(client, update, ct);
//                                //    dict[update.Message!.Chat.Id] = ChatMode.AddAdmin;
//                                //    break;
//                                default:
//                                    await SendMenu(client, update);
//                                    break;
//                            }
//                            break;
//                    }
//                }
//            }

//            async Task ErrorHandler(ITelegramBotClient client, Exception exception, CancellationToken ct)
//            {
//                Console.WriteLine("");
//            }
//        }
//    }

//}
