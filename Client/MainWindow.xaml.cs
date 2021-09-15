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
        private void Set_map()
        {

        }
        private void Update_map_state()
        {

        }
    }
}
