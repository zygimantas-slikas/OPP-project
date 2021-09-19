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
        private void Set_map(string t)
        {
            this.debug_list.Items.Add(t);
            //example
            int pos_x = 0, pos_y = 0;
            for (int i =0; i < Convert.ToInt32(t); i++)
            {
                pos_x = 0;
                for (int j = 0; j < Convert.ToInt32(t); j++)
                {
                    Rectangle myRect = new System.Windows.Shapes.Rectangle();
                    myRect.Stroke = System.Windows.Media.Brushes.Black;
                    myRect.Fill = System.Windows.Media.Brushes.LightGreen;
                    myRect.Height = 50;
                    myRect.Width = 50;
                    this.Canvas1.Children.Add(myRect);
                    Canvas.SetTop(myRect, pos_y);
                    Canvas.SetLeft(myRect, pos_x);
                    pos_x += 51;
                }
                pos_y += 51;
            }
        }
        private void Update_map_state()
        {

        }
    }
}
