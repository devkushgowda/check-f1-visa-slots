using System;
using Telegram.Bot;

namespace ConsoleApp4.Bots
{
    public class TelegramBot : IBot
    {
        private static readonly Lazy<TelegramBotClient> _lazyBot =
            new Lazy<TelegramBotClient>(() => new TelegramBotClient(Token));

        private const string Token = "5340067593:AAGgS0HDGvNdt-RUAGKnu0dpe1A9E-8jxuo";
        private readonly string _id;
        public TelegramBot(string id)
        {
            _id = id;
        }
        public void Send(string message)
        {
            var s = _lazyBot.Value.SendTextMessageAsync(_id, message).Result;
        }

        public void Send(string message, string subject = null)
        {
            Send(message);
        }
    }
}
