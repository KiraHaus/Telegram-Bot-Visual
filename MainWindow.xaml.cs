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
using System.Media;
using System.IO;
using Microsoft.Win32;
using Newtonsoft.Json;

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
            client = new BotClient(this, "5547982888:AAFN0hoWwHf07vmtqV72Sho8d5rgd3uR8XY");
            listMessage.ItemsSource = client.BotMessage;
        }

        private void SendMessage(object sender, RoutedEventArgs e)
        {
            client.SendMessage(admMessage.Text, TargetSend.Text);
            TargetSend.Text = "";
        }

        private void SaveDialog(object sender, RoutedEventArgs e)
        {
            var json = JsonConvert.SerializeObject(client.BotMessage);

            SaveFileDialog dialog = new SaveFileDialog();

            if (dialog.ShowDialog() == true)
            {
                File.WriteAllText($@"{dialog.FileName}", json);
            }
        }

        private void LoadDialog(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();

            if (dialog.ShowDialog() == true)
            {
                string message = File.ReadAllText(dialog.FileName);
                client.BotMessage.Clear();

                var json = JsonConvert.DeserializeObject<List<Message>>(message);

                foreach (dynamic item in json)
                {
                    client.BotMessage.Add(new Message(item.Time, item.Msg, item.FirstName, item.Id));
                }
            }
        }
    }
}
