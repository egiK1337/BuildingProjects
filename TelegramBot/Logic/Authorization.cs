using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelegramBot.Enum;

namespace TelegramBot.Logic
{
    internal class Authorization
    {

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
    }
}
