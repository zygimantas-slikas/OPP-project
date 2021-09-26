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
using Newtonsoft.Json;

namespace Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Map map1;
        private List<Player> players1;
        private Player current_Player;
        private Int32 mapId;
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
            connection.On<string>("Update_map_state", this.Update_map_state);
            connection.On<string>("ReceiveMessage", this.ReceiveMessage);
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
            mapId = id;
            Object[] args = new Object[1]{ id};
            this.connection.SendCoreAsync("Join_map", args);
            this.Tabs_control.SelectedItem = this.Game;
        }
        
        private void Show_maps_options (string[] list)
        {
            debug_list.Items.Add("show maps options");
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
            var serializerSettings = new JsonSerializerSettings { PreserveReferencesHandling = PreserveReferencesHandling.Objects };
            this.map1 = JsonConvert.DeserializeObject<Map>(json_text, serializerSettings);
            current_Player = this.map1.players.Last();
            //var t1 = Task.Run(() => map1.From_json(json_text));
            //await t1;

            this.debug_list.Items.Add(json_text);
            //example
            DrawMap();
        }
        private void Set_players(string json_text)
        {

        }
        private void Update_map_state(string json_text)
        {
            if (this.map1 != null && this.current_Player != null)
            {
                var serializerSettings = new JsonSerializerSettings { PreserveReferencesHandling = PreserveReferencesHandling.Objects };
                this.map1 = JsonConvert.DeserializeObject<Map>(json_text, serializerSettings);
                this.debug_list.Items.Add(json_text);

                DrawMap();
            }
        }

        private void DrawMap()
        {
            int pos_x = 0, pos_y = 0;
            canvas1.Height = Convert.ToInt32(map1.map_size) * 50 + 20;
            canvas1.Width = Convert.ToInt32(map1.map_size) * 50 + 20;
            debug_list.Items.Add(map1.map_size);
            for (int i = 0; i < Convert.ToInt32(map1.map_size); i++)
            {
                pos_x = 0;
                for (int j = 0; j < Convert.ToInt32(map1.map_size); j++)
                {
                    Rectangle myRect = new System.Windows.Shapes.Rectangle();
                    myRect.Stroke = System.Windows.Media.Brushes.Black;
                    if (!map1.map[i, j].Player_Standing)
                            myRect.Fill = System.Windows.Media.Brushes.LightGreen;
                    else
                        if (i == current_Player.x && j == current_Player.y)
                        myRect.Fill = System.Windows.Media.Brushes.Black;
                    else
                        myRect.Fill = System.Windows.Media.Brushes.Red;
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

        private void ReceiveMessage(string msg)
        {
            debug_list.Items.Add(msg);
        }
    }
}
