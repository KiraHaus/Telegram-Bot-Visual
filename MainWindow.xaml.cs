using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Telegram.Bot;
using Newtonsoft.Json;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bot.Types;

namespace Telegram_Bot_Visual
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        BotClient client;
        public MainWindow()
        {
            InitializeComponent();
            client = new BotClient(this);
            listMessage.ItemsSource = client.BotMessage;
        }

        private void SendMessage(object sender, RoutedEventArgs e)
        {
            client.SendMessage(admMessage.Text, TargetSend.Text);
        }

        private void LoadFile(object sender, RoutedEventArgs e)
        {

        }
    }
}
