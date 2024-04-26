using DataLayer.EfClasses;
using DataLayer.EfCode;
using Microsoft.IdentityModel.Tokens;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using TelegramBot.Enum;
using TelegramBot.Handlers;
using TelegramBot.TelegramServices;


class Program
{
    private static ITelegramBotClient _botClient;
    private static ReceiverOptions _receiverOptions;

    private static StateAction _stateAction;
    private static State _currentState;
    private static StateAdd _stateAdd;

    private static DataLayer.EfClasses.User _currentUser = new DataLayer.EfClasses.User();
    private static string? _userId;
    private static string? _buildId;

    static async Task Main()
    {
        var path = Path.Combine("Secrets", "secret.txt");
        var token = System.IO.File.ReadAllText(path);
        _botClient = new TelegramBotClient(token); // Присваиваем нашей переменной значение, в параметре передаем Token, полученный от BotFather
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
        var adminLogic = new AdminLogic(efCoreContext);
        var chiefEngineerLogic = new ChiefEngineerLogic(efCoreContext);
        var projectManagerLogic = new ProjectManagerLogic(efCoreContext);
        var buildingLogic = new BuildingLogic(efCoreContext);
        var userLogic = new UserLogic(efCoreContext);

        var message = update.Message;

        switch (_currentState)
        {
            case State.Start:
                if (message.Text.StartsWith("/auth"))
                {
                    _currentState = State.AuthInProgress;
                    await AuthenticationHandler.RequestLoginPassword(message.Chat.Id, _botClient);
                }
                else if (message.Text.StartsWith("/reg"))
                {
                    _currentState = State.RegInProgress;
                    await AuthenticationHandler.RequestLoginPassword(message.Chat.Id, _botClient);
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
                    MenuHandler.ShowMainMenu(message.Chat.Id, _botClient, _currentUser);
                else await AuthenticationHandler.RequestLoginPassword(message.Chat.Id, _botClient);
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
                    MenuHandler.ShowMainMenu(message.Chat.Id, _botClient, _currentUser);
                }
                break;

            case State.AuthInProgress:
                // Аунтификация пользователя
                var loginPassCheck = Authentication(message);
                if (_currentState == State.Authenticated)
                {
                    await _botClient.SendTextMessageAsync(message.Chat.Id, $"{loginPassCheck}. Вы авторизированы в статусе:{_currentUser.Roles}");
                    if (_currentUser.Roles != Roles.Guest)
                    {
                        MenuHandler.ShowMainMenu(message.Chat.Id, _botClient, _currentUser);
                    }
                    else
                    {
                        MenuHandler.ShowMainMenu(message.Chat.Id, _botClient, _currentUser);
                        _currentState = State.Start;
                        await _botClient.SendTextMessageAsync(message.Chat.Id,
                         "Пожалуйста начните работу с аунтификации используя команду /auth или зарегестрируйтесь в системе используя команду /reg.");
                    }

                }
                else await AuthenticationHandler.RequestLoginPassword(message.Chat.Id, _botClient);

                break;

            case State.Authenticated:

                switch (_currentUser.Roles)
                {
                    case Roles.Engineer:
                        if (message.Text.StartsWith("/list"))
                        {
                            var engineersList = engineerLogic.List();
                            foreach (var item in engineersList)
                            {
                                await _botClient.SendTextMessageAsync(message.Chat.Id, $"Id:{item.Id}: {item.Name}");
                            }
                        }
                        else if (message.Text.StartsWith("/exit"))
                        {
                            _stateAction = StateAction.Start;
                            _currentState = State.Start;
                            _stateAdd = StateAdd.Start;
                            _currentUser.Roles = Roles.Guest;

                            await _botClient.SendTextMessageAsync(message.Chat.Id,
                            "Пожалуйста начните работу с аунтификации используя команду /auth или зарегестрируйтесь в системе используя команду /reg.");
                        }
                        else
                        {
                            MenuHandler.ShowMainMenu(message.Chat.Id, _botClient, _currentUser);
                        }

                        break;

                    case Roles.Admin:
                        if (!message.Text.StartsWith("/exit"))
                        {
                            var tempStateAction = StatusSelect.StatusSelector(message.Text);
                            if (tempStateAction != StateAction.Error)
                            {
                                _stateAction = StatusSelect.StatusSelector(message.Text);
                            }
                        }
                        else if (message.Text.StartsWith("/exit"))
                        {
                            _stateAction = StateAction.Start;
                            _currentState = State.Start;
                            _stateAdd = StateAdd.Start;
                            _currentUser.Roles = Roles.Guest;

                            MenuHandler.ShowMainMenu(message.Chat.Id, _botClient, _currentUser);

                            await _botClient.SendTextMessageAsync(message.Chat.Id,
                            "Пожалуйста начните работу с аунтификации используя команду /auth или зарегестрируйтесь в системе используя команду /reg.");
                        }
                        else
                        {
                            MenuHandler.ShowMainMenu(message.Chat.Id, _botClient, _currentUser);
                        }

                        switch (_stateAction)
                        {
                            case StateAction.UserList:
                                var userList = userLogic.List();
                                foreach (var item in userList)
                                {
                                    await _botClient.SendTextMessageAsync(message.Chat.Id, item);
                                }
                                break;

                            case StateAction.AdminAdd:
                                switch (_stateAdd)
                                {
                                    case StateAdd.Start:
                                        _stateAdd = StateAdd.InProgress;
                                        await AuthenticationHandler.RequestLoginPassword(message.Chat.Id, _botClient);
                                        break;

                                    case StateAdd.InProgress:
                                        FillingUser(message);
                                        if (_stateAdd == StateAdd.Finish)
                                        {
                                            await _botClient.SendTextMessageAsync(message.Chat.Id,
                                            "Пожалуйста, введите фамилию и имя пользователя (например, Иванов Иван).:");
                                        }
                                        else await AuthenticationHandler.RequestLoginPassword(message.Chat.Id, _botClient);
                                        break;

                                    case StateAdd.Finish:
                                        if (!message.Text.IsNullOrEmpty())
                                        {
                                            var newAdmin = adminLogic.Add(message.Text, _currentUser);
                                            await _botClient.SendTextMessageAsync(message.Chat.Id, newAdmin);
                                        }
                                        _stateAction = StateAction.Start;
                                        _stateAdd = StateAdd.Start;

                                        efCoreContext.Dispose();

                                        MenuHandler.ShowMainMenu(message.Chat.Id, _botClient, _currentUser);
                                        break;
                                }

                                break;

                            case StateAction.ProjectManagerAdd:
                                switch (_stateAdd)
                                {
                                    case StateAdd.Start:
                                        _stateAdd = StateAdd.InProgress;
                                        await AuthenticationHandler.RequestLoginPassword(message.Chat.Id, _botClient);
                                        break;

                                    case StateAdd.InProgress:
                                        FillingUser(message);
                                        if (_stateAdd == StateAdd.Finish)
                                        {
                                            await _botClient.SendTextMessageAsync(message.Chat.Id,
                                            "Пожалуйста, введите фамилию и имя пользователя (например, Иванов Иван).:");
                                        }
                                        else await AuthenticationHandler.RequestLoginPassword(message.Chat.Id, _botClient);
                                        break;

                                    case StateAdd.Finish:

                                        if (!message.Text.IsNullOrEmpty())
                                        {
                                            var newProjectManager = projectManagerLogic.Add(message.Text, _currentUser);
                                            await _botClient.SendTextMessageAsync(message.Chat.Id, newProjectManager);
                                        }
                                        _stateAction = StateAction.Start;
                                        _stateAdd = StateAdd.Start;

                                        efCoreContext.Dispose();

                                        MenuHandler.ShowMainMenu(message.Chat.Id, _botClient, _currentUser);
                                        break;
                                }
                                break;

                            case StateAction.ProjectManagerAddToBuild:
                                switch (_stateAdd)
                                {
                                    case StateAdd.Start:
                                        var projectManagerList = projectManagerLogic.List();
                                        foreach (var item in projectManagerList)
                                        {
                                            await _botClient.SendTextMessageAsync(message.Chat.Id, $" Id: {item.Id}; Имя : {item.Name}");
                                        }
                                        _stateAdd = StateAdd.InProgress;
                                        await UserLogic.RequestId(message.Chat.Id, _botClient);

                                        break;

                                    case StateAdd.InProgress:
                                        CheckId(message);
                                        if (_stateAdd == StateAdd.Finish)
                                        {
                                            _userId = message.Text;
                                            var projectList = buildingLogic.ListWithoutEmployees();
                                            foreach (var item in projectList)
                                            {
                                                await _botClient.SendTextMessageAsync(message.Chat.Id, $"{item};");
                                            }
                                            await UserLogic.RequestBuildName(message.Chat.Id, _botClient);
                                        }
                                        else await UserLogic.RequestId(message.Chat.Id, _botClient);

                                        break;

                                    case StateAdd.Finish:
                                        CheckId(message);
                                        if (_stateAdd == StateAdd.Finish)
                                        {
                                            _buildId = message.Text;
                                            var addProjectManagerToBuild = projectManagerLogic.Edit(_userId, _buildId);
                                            await _botClient.SendTextMessageAsync(message.Chat.Id, addProjectManagerToBuild);
                                        }
                                        _stateAction = StateAction.Start;
                                        _stateAdd = StateAdd.Start;

                                        efCoreContext.Dispose();

                                        MenuHandler.ShowMainMenu(message.Chat.Id, _botClient, _currentUser);

                                        break;
                                }
                                break;

                            case StateAction.AdminDel:
                                switch (_stateAdd)
                                {
                                    case StateAdd.Start:
                                        var adminList = adminLogic.List();
                                        foreach (var item in adminList)
                                        {
                                            await _botClient.SendTextMessageAsync(message.Chat.Id, $" Id: {item.Id}; Имя : {item.Name}");
                                        }
                                        _stateAdd = StateAdd.InProgress;
                                        await UserLogic.RequestId(message.Chat.Id, _botClient);

                                        break;

                                    case StateAdd.InProgress:
                                        CheckId(message);
                                        if (_stateAdd == StateAdd.Finish)
                                        {
                                            await _botClient.SendTextMessageAsync(message.Chat.Id,
                                            "Введите Id повторно");
                                        }
                                        else await UserLogic.RequestId(message.Chat.Id, _botClient);

                                        break;

                                    case StateAdd.Finish:

                                        if (!message.Text.IsNullOrEmpty())
                                        {
                                            var adminDel = adminLogic.Delete(int.Parse(message.Text));
                                            await _botClient.SendTextMessageAsync(message.Chat.Id, adminDel);
                                        }
                                        _stateAction = StateAction.Start;
                                        _stateAdd = StateAdd.Start;

                                        efCoreContext.Dispose();

                                        MenuHandler.ShowMainMenu(message.Chat.Id, _botClient, _currentUser);

                                        break;
                                }
                                break;

                            case StateAction.ProjectManagerDel:
                                switch (_stateAdd)
                                {
                                    case StateAdd.Start:
                                        var projectManagerList = projectManagerLogic.List();
                                        foreach (var item in projectManagerList)
                                        {
                                            await _botClient.SendTextMessageAsync(message.Chat.Id, $" Id: {item.Id}; Имя : {item.Name}");
                                        }
                                        _stateAdd = StateAdd.InProgress;
                                        await UserLogic.RequestId(message.Chat.Id, _botClient);

                                        break;

                                    case StateAdd.InProgress:
                                        CheckId(message);
                                        if (_stateAdd == StateAdd.Finish)
                                        {
                                            await _botClient.SendTextMessageAsync(message.Chat.Id,
                                            "Введите Id повторно");
                                        }
                                        else await UserLogic.RequestId(message.Chat.Id, _botClient);

                                        break;

                                    case StateAdd.Finish:

                                        if (!message.Text.IsNullOrEmpty())
                                        {
                                            var projectManagerDel = projectManagerLogic.Delete(int.Parse(message.Text));
                                            await _botClient.SendTextMessageAsync(message.Chat.Id, projectManagerDel);
                                        }
                                        _stateAction = StateAction.Start;
                                        _stateAdd = StateAdd.Start;

                                        efCoreContext.Dispose();

                                        MenuHandler.ShowMainMenu(message.Chat.Id, _botClient, _currentUser);

                                        break;
                                }
                                break;
                        }
                        break;

                    case Roles.ChiefEngineer:
                        if (!message.Text.StartsWith("/exit"))
                        {
                            var tempStateAction = StatusSelect.StatusSelector(message.Text);
                            if (tempStateAction != StateAction.Error)
                            {
                                _stateAction = StatusSelect.StatusSelector(message.Text);
                            }
                        }

                        else if (message.Text.StartsWith("/exit"))
                        {
                            _stateAction = StateAction.Start;
                            _currentState = State.Start;
                            _stateAdd = StateAdd.Start;
                            _currentUser.Roles = Roles.Guest;

                            MenuHandler.ShowMainMenu(message.Chat.Id, _botClient, _currentUser);

                            await _botClient.SendTextMessageAsync(message.Chat.Id,
                            "Пожалуйста начните работу с аунтификации используя команду /auth или зарегестрируйтесь в системе используя команду /reg.");
                        }
                        else
                        {
                            MenuHandler.ShowMainMenu(message.Chat.Id, _botClient, _currentUser);
                        }
                        switch (_stateAction)
                        {
                            case StateAction.EngineerList:
                                var engineersList = engineerLogic.List();
                                foreach (var item in engineersList)
                                {
                                    await _botClient.SendTextMessageAsync(message.Chat.Id, $"Id{item.Id}:{item.Name}");
                                }
                                break;
                            case StateAction.BuildList:
                                var buildList = buildingLogic.List();
                                foreach (var item in buildList)
                                {
                                    await _botClient.SendTextMessageAsync(message.Chat.Id, $"{item};");
                                }
                                break;

                            case StateAction.EngineerAddToBuild:
                                switch (_stateAdd)
                                {
                                    case StateAdd.Start:
                                        var engineerList = engineerLogic.List();
                                        foreach (var item in engineerList)
                                        {
                                            await _botClient.SendTextMessageAsync(message.Chat.Id, $" Id: {item.Id}; Имя : {item.Name}");
                                        }
                                        _stateAdd = StateAdd.InProgress;
                                        await UserLogic.RequestId(message.Chat.Id, _botClient);

                                        break;

                                    case StateAdd.InProgress:
                                        CheckId(message);
                                        if (_stateAdd == StateAdd.Finish)
                                        {
                                            _userId = message.Text;
                                            var projectList = buildingLogic.ListWithoutEmployees();
                                            foreach (var item in projectList)
                                            {
                                                await _botClient.SendTextMessageAsync(message.Chat.Id, $"{item};");
                                            }
                                            await UserLogic.RequestBuildName(message.Chat.Id, _botClient);
                                        }
                                        else await UserLogic.RequestId(message.Chat.Id, _botClient);

                                        break;

                                    case StateAdd.Finish:
                                        CheckId(message);
                                        if (_stateAdd == StateAdd.Finish)
                                        {
                                            _buildId = message.Text;
                                            var addEngineerToBuild = engineerLogic.Edit(_userId, _buildId);
                                            await _botClient.SendTextMessageAsync(message.Chat.Id, addEngineerToBuild);
                                        }
                                        _stateAction = StateAction.Start;
                                        _stateAdd = StateAdd.Start;

                                        efCoreContext.Dispose();

                                        MenuHandler.ShowMainMenu(message.Chat.Id, _botClient, _currentUser);

                                        break;
                                }
                                break;

                            case StateAction.EngineerDelBuild:
                                switch (_stateAdd)
                                {
                                    case StateAdd.Start:
                                        var fullList = buildingLogic.List();
                                        foreach (var item in fullList)
                                        {
                                            await _botClient.SendTextMessageAsync(message.Chat.Id, item);
                                        }
                                        _stateAdd = StateAdd.InProgress;
                                        await UserLogic.RequestId(message.Chat.Id, _botClient);

                                        break;

                                    case StateAdd.InProgress:
                                        CheckId(message);
                                        if (_stateAdd == StateAdd.Finish)
                                        {
                                            _userId = message.Text;
                                            await UserLogic.RequestBuildName(message.Chat.Id, _botClient);
                                        }
                                        else await UserLogic.RequestId(message.Chat.Id, _botClient);

                                        break;

                                    case StateAdd.Finish:
                                        CheckId(message);
                                        if (_stateAdd == StateAdd.Finish)
                                        {
                                            _buildId = message.Text;
                                            var engineerDelBuild = engineerLogic.DeleteFromBuild(_userId, _buildId);
                                            await _botClient.SendTextMessageAsync(message.Chat.Id, engineerDelBuild);
                                        }
                                        _stateAction = StateAction.Start;
                                        _stateAdd = StateAdd.Start;

                                        efCoreContext.Dispose();

                                        MenuHandler.ShowMainMenu(message.Chat.Id, _botClient, _currentUser);

                                        break;
                                }
                                break;

                            case StateAction.EngineerDel:
                                switch (_stateAdd)
                                {
                                    case StateAdd.Start:
                                        var engineerList = engineerLogic.List();
                                        foreach (var item in engineerList)
                                        {
                                            await _botClient.SendTextMessageAsync(message.Chat.Id, $" Id: {item.Id}; Имя : {item.Name}");
                                        }
                                        _stateAdd = StateAdd.InProgress;
                                        await UserLogic.RequestId(message.Chat.Id, _botClient);

                                        break;

                                    case StateAdd.InProgress:
                                        CheckId(message);
                                        if (_stateAdd == StateAdd.Finish)
                                        {
                                            await _botClient.SendTextMessageAsync(message.Chat.Id,
                                            "Введите Id повторно");
                                        }
                                        else await UserLogic.RequestId(message.Chat.Id, _botClient);

                                        break;

                                    case StateAdd.Finish:

                                        if (!message.Text.IsNullOrEmpty())
                                        {
                                            var engineerDel = engineerLogic.Delete(int.Parse(message.Text));
                                            await _botClient.SendTextMessageAsync(message.Chat.Id, engineerDel);
                                        }
                                        _stateAction = StateAction.Start;
                                        _stateAdd = StateAdd.Start;

                                        efCoreContext.Dispose();

                                        MenuHandler.ShowMainMenu(message.Chat.Id, _botClient, _currentUser);

                                        break;
                                }
                                break;
                        }
                        break;

                    case Roles.ProjectManager:
                        if (!message.Text.StartsWith("/exit"))
                        {
                            var tempStateAction = StatusSelect.StatusSelector(message.Text);
                            if (tempStateAction != StateAction.Error)
                            {
                                _stateAction = StatusSelect.StatusSelector(message.Text);
                            }
                        }

                        else if (message.Text.StartsWith("/exit"))
                        {
                            _stateAction = StateAction.Start;
                            _currentState = State.Start;
                            _stateAdd = StateAdd.Start;
                            _currentUser.Roles = Roles.Guest;

                            MenuHandler.ShowMainMenu(message.Chat.Id, _botClient, _currentUser);

                            await _botClient.SendTextMessageAsync(message.Chat.Id,
                            "Пожалуйста начните работу с аунтификации используя команду /auth или зарегестрируйтесь в системе используя команду /reg.");
                        }
                        else
                        {
                            MenuHandler.ShowMainMenu(message.Chat.Id, _botClient, _currentUser);
                        }
                        switch (_stateAction)
                        {
                            case StateAction.ChiefEngineerAdd:
                                switch (_stateAdd)
                                {
                                    case StateAdd.Start:
                                        _stateAdd = StateAdd.InProgress;
                                        await AuthenticationHandler.RequestLoginPassword(message.Chat.Id, _botClient);

                                        break;

                                    case StateAdd.InProgress:
                                        FillingUser(message);
                                        if (_stateAdd == StateAdd.Finish)
                                        {
                                            await _botClient.SendTextMessageAsync(message.Chat.Id,
                                            "Пожалуйста, введите фамилию и имя пользователя (например, Иванов Иван).:");
                                        }
                                        else await AuthenticationHandler.RequestLoginPassword(message.Chat.Id, _botClient);

                                        break;

                                    case StateAdd.Finish:

                                        if (!message.Text.IsNullOrEmpty())
                                        {
                                            var newChiefEngineer = chiefEngineerLogic.Add(message.Text, _currentUser);
                                            await _botClient.SendTextMessageAsync(message.Chat.Id, newChiefEngineer);
                                        }
                                        _stateAction = StateAction.Start;
                                        _stateAdd = StateAdd.Start;

                                        efCoreContext.Dispose();

                                        MenuHandler.ShowMainMenu(message.Chat.Id, _botClient, _currentUser);

                                        break;
                                }
                                break;

                            case StateAction.BuildAdd:
                                switch (_stateAdd)
                                {
                                    case StateAdd.Start:
                                        _stateAdd = StateAdd.InProgress;
                                        await AuthenticationHandler.RequestBuildingName(message.Chat.Id, _botClient);
                                        break;

                                    case StateAdd.InProgress:
                                        if (!message.Text.IsNullOrEmpty())
                                        {
                                            _stateAdd = StateAdd.Finish;
                                            await _botClient.SendTextMessageAsync(message.Chat.Id,
                                            "Введите название повторно");
                                        }
                                        else await AuthenticationHandler.RequestBuildingName(message.Chat.Id, _botClient);
                                        break;

                                    case StateAdd.Finish:
                                        if (!message.Text.IsNullOrEmpty())
                                        {
                                            var newBuild = buildingLogic.Add(message.Text);
                                            await _botClient.SendTextMessageAsync(message.Chat.Id, newBuild);
                                        }
                                        _stateAction = StateAction.Start;
                                        _stateAdd = StateAdd.Start;

                                        efCoreContext.Dispose();

                                        MenuHandler.ShowMainMenu(message.Chat.Id, _botClient, _currentUser);
                                        break;
                                }
                                break;

                            case StateAction.ChiefEngineerDel:
                                switch (_stateAdd)
                                {
                                    case StateAdd.Start:
                                        var chiefEngineerList = chiefEngineerLogic.List();
                                        foreach (var item in chiefEngineerList)
                                        {
                                            await _botClient.SendTextMessageAsync(message.Chat.Id, $" Id: {item.Id}; Имя : {item.Name}");
                                        }
                                        _stateAdd = StateAdd.InProgress;
                                        await UserLogic.RequestId(message.Chat.Id, _botClient);
                                        break;

                                    case StateAdd.InProgress:
                                        CheckId(message);
                                        if (_stateAdd == StateAdd.Finish)
                                        {
                                            await _botClient.SendTextMessageAsync(message.Chat.Id,
                                            "Введите Id повторно");
                                        }
                                        else await UserLogic.RequestId(message.Chat.Id, _botClient);
                                        break;

                                    case StateAdd.Finish:

                                        if (!message.Text.IsNullOrEmpty())
                                        {
                                            var chiefeEngineerDel = engineerLogic.Delete(int.Parse(message.Text));
                                            await _botClient.SendTextMessageAsync(message.Chat.Id, chiefeEngineerDel);
                                        }
                                        _stateAction = StateAction.Start;
                                        _stateAdd = StateAdd.Start;

                                        efCoreContext.Dispose();

                                        MenuHandler.ShowMainMenu(message.Chat.Id, _botClient, _currentUser);
                                        break;
                                }
                                break;

                            case StateAction.ChiefEngineerAddToBuild:
                                switch (_stateAdd)
                                {
                                    case StateAdd.Start:
                                        var chiefEngineerList = chiefEngineerLogic.List();
                                        foreach (var item in chiefEngineerList)
                                        {
                                            await _botClient.SendTextMessageAsync(message.Chat.Id, $" Id: {item.Id}; Имя : {item.Name}");
                                        }
                                        _stateAdd = StateAdd.InProgress;
                                        await UserLogic.RequestId(message.Chat.Id, _botClient);

                                        break;

                                    case StateAdd.InProgress:
                                        CheckId(message);
                                        if (_stateAdd == StateAdd.Finish)
                                        {
                                            _userId = message.Text;
                                            var projectList = buildingLogic.ListWithoutEmployees();
                                            foreach (var item in projectList)
                                            {
                                                await _botClient.SendTextMessageAsync(message.Chat.Id, $"{item};");
                                            }
                                            await UserLogic.RequestBuildName(message.Chat.Id, _botClient);
                                        }
                                        else await UserLogic.RequestId(message.Chat.Id, _botClient);

                                        break;

                                    case StateAdd.Finish:
                                        CheckId(message);
                                        if (_stateAdd == StateAdd.Finish)
                                        {
                                            _buildId = message.Text;
                                            var addChiefEngineerToBuild = chiefEngineerLogic.Edit(_userId, _buildId);
                                            await _botClient.SendTextMessageAsync(message.Chat.Id, addChiefEngineerToBuild);
                                        }
                                        _stateAction = StateAction.Start;
                                        _stateAdd = StateAdd.Start;

                                        efCoreContext.Dispose();

                                        MenuHandler.ShowMainMenu(message.Chat.Id, _botClient, _currentUser);

                                        break;
                                }
                                break;

                            case StateAction.ChiefEngineerDelBuild:
                                switch (_stateAdd)
                                {
                                    case StateAdd.Start:
                                        var fullList = buildingLogic.List();
                                        foreach (var item in fullList)
                                        {
                                            await _botClient.SendTextMessageAsync(message.Chat.Id, item);
                                        }
                                        _stateAdd = StateAdd.InProgress;
                                        await UserLogic.RequestId(message.Chat.Id, _botClient);

                                        break;

                                    case StateAdd.InProgress:
                                        CheckId(message);
                                        if (_stateAdd == StateAdd.Finish)
                                        {
                                            _userId = message.Text;
                                            await UserLogic.RequestBuildName(message.Chat.Id, _botClient);
                                        }
                                        else await UserLogic.RequestId(message.Chat.Id, _botClient);

                                        break;

                                    case StateAdd.Finish:
                                        CheckId(message);
                                        if (_stateAdd == StateAdd.Finish)
                                        {
                                            _buildId = message.Text;
                                            var chiefEngineerDelBuild = chiefEngineerLogic.DeleteFromBuild(_userId, _buildId);
                                            await _botClient.SendTextMessageAsync(message.Chat.Id, chiefEngineerDelBuild);
                                        }
                                        _stateAction = StateAction.Start;
                                        _stateAdd = StateAdd.Start;

                                        efCoreContext.Dispose();

                                        MenuHandler.ShowMainMenu(message.Chat.Id, _botClient, _currentUser);

                                        break;
                                }
                                break;


                            case StateAction.ChiefEngineerList:
                                var chiefEngineersList = engineerLogic.List();
                                foreach (var item in chiefEngineersList)
                                {
                                    await _botClient.SendTextMessageAsync(message.Chat.Id, $"Id{item.Id}:{item.Name}");
                                }
                                break;

                            case StateAction.BuildList:
                                var buildList = buildingLogic.List();
                                foreach (var item in buildList)
                                {
                                    await _botClient.SendTextMessageAsync(message.Chat.Id, $"{item};");
                                }
                                break;
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
                    MenuHandler.ShowMainMenu(message.Chat.Id, _botClient, _currentUser);
                }
                efCoreContext.Dispose();
                _currentState = State.Authenticated;

                break;

            default:
                break;
        }


        //if (update.Type == UpdateType.Message && update.Message is { })
        //{

        //}
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


    private static void Registration(Message message)
    {
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

    private static void FillingUser(Message message)
    {
        _stateAdd = !string.IsNullOrWhiteSpace(message.Text) && message.Text.Split(" ").Length > 1
            ? StateAdd.Finish
            : StateAdd.InProgress;
        if (_stateAdd == StateAdd.Finish)
        {
            var loginPassword = message.Text.Split();

            _currentUser.Login = loginPassword[0];
            _currentUser.Password = loginPassword[1];
        }
    }

    private static void CheckId(Message message)
    {
        var result = int.TryParse(message.Text, out int i);
        if (result)
        {
            _stateAdd = StateAdd.Finish;
        }
        else
        {
            _stateAdd = StateAdd.InProgress;
        }
    }

}

