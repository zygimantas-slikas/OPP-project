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
using Microsoft.AspNetCore.SignalR.Client;
using System.Text.RegularExpressions;

namespace Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Map map1;
        private List<Player> players1;
        public MainWindow()
        {
            InitializeComponent();
        }

        private HubConnection connection;

        private void Connect_to_server(object sender, RoutedEventArgs e)
        {
            if (this.connection != null)
            {
                this.connection.DisposeAsync();
            }
            debug_list.Items.Add(server_addr.Text + "/messageHub");
            this.connection = new HubConnectionBuilder().WithUrl(server_addr.Text + "/messageHub").Build();
            //add functions==================
            connection.On<string[]>("Show_maps_options", this.Show_maps_options);
            connection.On<string>("Set_map", this.Set_map);
            connection.On<string>("Set_players", this.Set_players);
            //===============================
            connection.StartAsync();
            connection.SendAsync("Connect");
        }

        private void Create_new_room(object sender, RoutedEventArgs e)
        {
            Object[] args = new Object[2] 
            { Convert.ToInt32(this.players_count.Text), Convert.ToInt32(this.map_size.Text)};
            this.connection.SendCoreAsync("Create_map", args);
        }

        private void Join_room(object sender, RoutedEventArgs e)
        {
            string room_text = rooms_select.SelectedItem.ToString();
            Regex re = new Regex(@"\d+");
            Match match = re.Match(room_text);
            Int32 id = Convert.ToInt32(match.ToString());
            Object[] args = new Object[1]{ id};
            this.connection.SendCoreAsync("Join_map", args);
            this.Tabs_control.SelectedItem = this.Game;
        }
        
        private void Show_maps_options (string[] list)
        {
            this.rooms_select.Items.Clear();
            foreach (string line1 in list)
            {
                this.rooms_select.Items.Add(line1);
            }
            this.rooms_select.IsDropDownOpen = true;
        }
        private async Task Set_map(string json_text)
        {
            this.map1 = new Map();
            var t1 = Task.Run(() => map1.From_json(json_text));
            await t1;

            this.debug_list.Items.Add(json_text);
            //example
            int pos_x = 0, pos_y = 0;
            canvas1.Height = Convert.ToInt32(json_text) * 50 + 20;
            canvas1.Width = Convert.ToInt32(json_text) * 50 + 20;
            for (int i =0; i < Convert.ToInt32(json_text); i++)
            {
                pos_x = 0;
                for (int j = 0; j < Convert.ToInt32(json_text); j++)
                {
                    Rectangle myRect = new System.Windows.Shapes.Rectangle();
                    myRect.Stroke = System.Windows.Media.Brushes.Black;
                    myRect.Fill = System.Windows.Media.Brushes.LightGreen;
                    myRect.Height = 50;
                    myRect.Width = 50;
                    this.canvas1.Children.Add(myRect);
                    Canvas.SetTop(myRect, pos_y);
                    Canvas.SetLeft(myRect, pos_x);
                    pos_x += 50;
                }
                pos_y += 50;
            }
        }
        private void Set_players(string json_text)
        {

        }
        private void Update_map_state()
        {

        }

        private void RecieveMessage(string msg)
        {
            debug_list.Items.Add(msg);
        }

        private void Test_Serialize(object sender, RoutedEventArgs e)
        {
            Player p = new Player("1");
            DataProcessor dp = new DataProcessor();
            //debug_list.Items.Add(dp.SerializeJson(p));

            Object[] args = new Object[1]
            { dp.SerializeJson(p)};
            this.connection.SendCoreAsync("SendMessage", args);
        }
    }
}
