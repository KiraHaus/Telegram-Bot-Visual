using System;
using Telegram.Bot;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bot.Types;
using Telegram.Bot.Exceptions;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Threading;
using Telegram.Bot.Types.Enums;
using System.Collections.Generic;
using System.Text;

namespace Telegram_Bot_Visual
{
    class BotClient
    {
        private MainWindow w;
        //"5547982888:AAFN0hoWwHf07vmtqV72Sho8d5rgd3uR8XY"
        private TelegramBotClient bot;
        public ObservableCollection<Message> BotMessage { get; set; }

        public BotClient(MainWindow window, string PathToken)
        {
            BotMessage = new ObservableCollection<Message>();
            w = window;
            bot = new TelegramBotClient(PathToken);
            var cansellationToken = new CancellationTokenSource();

            var receiverOptions = new ReceiverOptions
            {
                AllowedUpdates = Array.Empty<UpdateType>()
            };

            bot.StartReceiving(updateHandler: HandleUpdateAsync, errorHandler: HandleErrorAsync, receiverOptions: receiverOptions, cansellationToken.Token);
        }

        static async Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            Console.WriteLine(JsonConvert.SerializeObject(exception));
        }

        async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            if (update.Type != UpdateType.Message)
            {
                return;
            }

            if (update.Message.Type != MessageType.Text)
            {
                return;
            }

            Debug.WriteLine($"{DateTime.Now.ToLongTimeString()} : {Convert.ToString(update.Message.From.Id)} {update.Message.Text} {update.Message.From.FirstName}");

            w.Dispatcher.Invoke(() =>
            {
                BotMessage.Add(new Message(DateTime.Now.ToLongTimeString(), update.Message.Text,update.Message.From.FirstName, update.Message.From.Id));
            });
        }

        private void MessageListener(object sender, Update e)
        {

            string text = $"{DateTime.Now.ToLongTimeString()}: {e.Message.Chat.FirstName} {e.Message.Chat.Id} {e.Message.Text}";

            if (e.Message.Text == null) return;

            var messageText = e.Message.Text;

            w.Dispatcher.Invoke(() =>
            {
                BotMessage.Add(
                new Message(
                    DateTime.Now.ToLongTimeString(), messageText, e.Message.Chat.FirstName, e.Message.Chat.Id));
            });
        }

        public void SendMessage(string Text, string Id)
        {
            bot.SendTextMessageAsync(Id, Text);
        }

    }
}
