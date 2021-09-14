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
            debug_list.Items.Add(connection.State.ToString());
            connection.StartAsync();
            debug_list.Items.Add(connection.State.ToString());
            connection.InvokeCoreAsync("SendMessage", args: new[] { "user1", "hello" });
        }

        private void Join_room(object sender, RoutedEventArgs e)
        {

        }

        private void Create_new_room(object sender, RoutedEventArgs e)
        {

        }
    }
}
