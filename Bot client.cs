using System;
using Telegram.Bot;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bot.Types;
using Telegram.Bot.Exceptions;
using System.Threading.Tasks;
using System.Threading;

namespace Telegram_Bot_Visual
{
    class BotClient
    {
        private MainWindow w;
        //"5547982888:AAFN0hoWwHf07vmtqV72Sho8d5rgd3uR8XY"
        private TelegramBotClient bot;
        public ObservableCollection<Message> BotMessage { get; set; }

        static async Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            Console.WriteLine(JsonConvert.SerializeObject(exception));
        }

        async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            MessageListener(botClient,update);
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

        public void ITelegramBotClient (MainWindow W, string PathToken = "5547982888:AAFN0hoWwHf07vmtqV72Sho8d5rgd3uR8XY")
        {
            this.BotMessage = new ObservableCollection<Message>();
            this.w = W;

            bot = new TelegramBotClient(PathToken);

            var cansellationToken = new CancellationTokenSource();

            var receiverOptions = new ReceiverOptions
            {
                AllowedUpdates = { },
            };

            bot.StartReceiving(updateHandler: HandleUpdateAsync, errorHandler: HandleErrorAsync, receiverOptions: receiverOptions, cansellationToken.Token);

        }

        public void SendMessage(string Text, string Id)
        {
            long id = Convert.ToInt64(Id);
            bot.SendTextMessageAsync(id, Text);
        }

    }
}
