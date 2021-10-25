﻿using System;
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
using Client.Strategy;
using System.Windows.Media.Animation;

namespace Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        protected Map map1;
        protected List<Player> players1;
        protected Dictionary<String, Shape> players_gui;
        protected String current_player_name;
        protected Player current_Player;
        protected Int32 mapId;
        protected GameSettings settings = GameSettings.GetInstance();
        protected HubConnection connection;
        public MainWindow()
        {
            InitializeComponent();
            Login.Visibility = Visibility.Visible;
            Join.Visibility = Visibility.Hidden;
            Setting_menu.Visibility = Visibility.Hidden;
            create_map_panel.Visibility = Visibility.Hidden;
            Game.Visibility = Visibility.Hidden;
            BrushConverter bc = new BrushConverter();
            Brush brush1 = (Brush)bc.ConvertFrom("#FF18E79D");
            brush1.Freeze();
            Brush brush2 = (Brush)bc.ConvertFrom("#FF131B3C");
            brush2.Freeze();
            login_tab_button.BorderBrush = brush1;
            join_tab_button.BorderBrush = brush2;
            game_tab_button.BorderBrush = brush2;
        }
        private void Connect_to_server(object sender, RoutedEventArgs e)
        {
            if (this.connection != null)
            {
                this.connection.DisposeAsync();
            }
            //BeginStoryboard()
            //transition_login_to_join.Begin();
            //var keys = main_window.Resources;
            //var story = main_window.Resources.Values.GetEnumerator();
            Join.Visibility = Visibility.Visible;
            Login.Visibility = Visibility.Hidden;
            create_map_panel.Visibility = Visibility.Hidden;
            maps_list_menu.Visibility = Visibility.Visible;
            BrushConverter bc = new BrushConverter();
            Brush brush1 = (Brush)bc.ConvertFrom("#FF18E79D");
            brush1.Freeze();
            Brush brush2 = (Brush)bc.ConvertFrom("#FF131B3C");
            brush2.Freeze();
            login_tab_button.BorderBrush = brush2;
            join_tab_button.BorderBrush = brush1;
            game_tab_button.BorderBrush = brush2;

            //debug_list.Items.Add(server_addr.Text + "/messageHub");
            this.connection = new HubConnectionBuilder().WithUrl(server_addr.Text + "/messageHub").Build();
            //add functions==================
            connection.On<string[]>("Show_maps_options", this.Show_maps_options);
            connection.On<string>("Set_map", this.Set_map);
            connection.On<string>("Set_players", this.Set_players);
            connection.On<string>("Update_map_state", this.Update_map_state);
            //===============================
            connection.StartAsync();
            connection.SendAsync("Connect");
        }
        private void Create_new_room(object sender, RoutedEventArgs e)
        {
            if (connection == null || connection.State != HubConnectionState.Connected)
            {
                MessageBox.Show("Connect to server first!");
                return;
            }
            int p_c, m_s;
            if (!int.TryParse(this.players_count.Text, out p_c) || p_c > 4 || p_c < 2)
            {
                MessageBox.Show("Players count must be between 2 and 4!");
                return;
            }
            if (!int.TryParse(this.map_size.Text, out m_s) || m_s < 10)
            {
                MessageBox.Show("Map size must be bigger than 10!");
                return;
            }
            Int32 level = 0;
            if (game_mode_easy.IsChecked == true) level = 1;
            else if (game_mode_hard.IsChecked == true) level = 2;
            Int32 map_type = 0;
            if (map_type_1.IsChecked == true) map_type = 1;
            else if (map_type_2.IsChecked == true) map_type = 2;
            Object[] args = new Object[4]{ Convert.ToInt32(this.players_count.Text), 
                Convert.ToInt32(this.map_size.Text), level, map_type};
            this.connection.SendCoreAsync("Create_map", args);
        }
        private void Join_room(object sender, RoutedEventArgs e)
        {
            if (connection == null || connection.State != HubConnectionState.Connected)
            {
                MessageBox.Show("Connect to server first!");
                return;
            }
            if (player_name.Text == "")
            {
                MessageBox.Show("Player name not set!");
                return;
            }
            int id;
            if (rooms_for_join.SelectedItem == null)
            {
                MessageBox.Show("Choose a map!");
                return;
            }
            else
            {
                Rooms_list_view_obj obj = (Rooms_list_view_obj)rooms_for_join.SelectedItem;
                id = int.Parse(obj.map_id);
                //string room_text = rooms_select.SelectedItem.ToString();
                //Regex re = new Regex(@"\d+");
                //Match match = re.Match(room_text);
                //if (!int.TryParse(match.ToString(), out id))
                //{
                //    MessageBox.Show("Choose a map!");
                //    return;
                //}
            }
            mapId = id;
            current_player_name = player_name.Text;
            Object[] args = new Object[2] { id, player_name.Text };
            this.connection.SendCoreAsync("Join_map", args);
            this.Show_game_tab(sender, e);
            this.players_gui = new Dictionary<String, Shape>();
        }
        private void Show_maps_options(string[] list)
        {
            if (list[0][0] == 'N') //TODO: finish
            {
                return;
            }
            this.rooms_for_join.Items.Clear();
            foreach (string line1 in list)
            {
                string[] data = line1.Split(',');
                string map_id =   data[0].Split(new char[] { ':'})[1];
                string players =  data[1].Split(new char[] { ':'})[1];
                string map_size = data[2].Split(new char[] { ':'})[1];
                rooms_for_join.Items.Add(new Rooms_list_view_obj( map_id, players, map_size ));
            }
        }
        private void Set_map(string json_text)
        {
            this.map1 = new Map();
            var t1 = Task.Run(() => map1.From_json(json_text));
            t1.Wait();
            DrawMap();
        }

        private void Update_map_state(string json_text)
        {//TODO: map update from short json
            //if (this.map1 != null && this.current_Player != null)
            //{
            //    var serializerSettings = new JsonSerializerSettings { PreserveReferencesHandling = PreserveReferencesHandling.Objects };
            //    this.map1 = JsonConvert.DeserializeObject<Map>(json_text, serializerSettings);
            //    this.debug_list.Items.Add(json_text);

            //    DrawMap();
            //}
        }
        private void DrawMap()
        {
            int pos_x = 0, pos_y = 0;
            canvas1.Children.Clear();
            canvas1.Height = Convert.ToInt32(map1.map_size) * 50 + 20;
            canvas1.Width = Convert.ToInt32(map1.map_size) * 50 + 20;
            for (int i = 0; i < Convert.ToInt32(map1.map_size); i++)
            {
                pos_x = 0;
                for (int j = 0; j < Convert.ToInt32(map1.map_size); j++)
                {
                    Rectangle myRect = new System.Windows.Shapes.Rectangle();
                    myRect.Stroke = System.Windows.Media.Brushes.Black;
                    if (map1.map[i, j].Surface == Tile.Tile_type.grass)
                    {
                        myRect.Fill = System.Windows.Media.Brushes.LightGreen;
                    }
                    else if (map1.map[i, j].Surface == Tile.Tile_type.wall)
                    {
                        myRect.Fill = System.Windows.Media.Brushes.Gray;
                    }
                    else if (map1.map[i, j].Surface == Tile.Tile_type.water)
                    {
                        myRect.Fill = System.Windows.Media.Brushes.Blue;
                    }
                    else if (map1.map[i, j].Surface == Tile.Tile_type.lava)
                    {
                        myRect.Fill = System.Windows.Media.Brushes.OrangeRed;
                    }
                    else if (map1.map[i, j].Surface == Tile.Tile_type.bush)
                    {
                        myRect.Fill = System.Windows.Media.Brushes.DarkGreen;
                    }
                    myRect.Height = 50;
                    myRect.Width = 50;
                    this.canvas1.Children.Add(myRect);
                    Canvas.SetTop(myRect, pos_y);
                    Canvas.SetLeft(myRect, pos_x);
                    Canvas.SetZIndex(myRect, 0);
                    if (map1.map[i, j].Loot != null)
                    {
                        map1.map[i, j].Icon = map1.map[i, j].Loot.get_view();
                        this.canvas1.Children.Add(map1.map[i, j].Icon);
                        Canvas.SetTop(map1.map[i, j].Icon, pos_y +5);
                        Canvas.SetLeft(map1.map[i, j].Icon, pos_x+5);
                        Canvas.SetZIndex(map1.map[i, j].Icon, 1);
                    }
                    pos_x += 50;
                }
                pos_y += 50;
            }
            foreach(var el in players_gui.Values)
            {
                canvas1.Children.Remove(el);
                canvas1.Children.Add(el);
                Canvas.SetZIndex(el, 10);
            } 
        }
        private void Set_players(string json_text)
        {
            players1 = System.Text.Json.JsonSerializer.Deserialize<List<Player>>(json_text);
            current_Player = players1.Find(x => x.Name == current_player_name);
            this.Update_players_objects();
            players_scrollbar.Children.Clear();
            for (int i = 0; i < players1.Count; i++)
            {
                StackPanel p1 = new StackPanel();
                p1.Margin = new Thickness(10);
                Border b1 = new Border();
                b1.Width = 1;
                BrushConverter bc = new BrushConverter();
                Brush brush = (Brush)bc.ConvertFrom("#FF131B3C");
                brush.Freeze();
                p1.Background = brush;
                Label name = new Label();
                name.Content = "Name: " + players1[i].Name;
                Label health = new Label();
                health.Content = "Health: " + players1[i].Health;
                Label points= new Label();
                points.Content = "Points: " + players1[i].Points;
                Label items = new Label();
                items.Content = "Items" + players1[i].Inventory ;
                Label position = new Label();
                position.Content = String.Format("X:{0}, Y:{1}", players1[i].X, players1[i].Y);
                p1.Children.Add(name);
                p1.Children.Add(health);
                p1.Children.Add(points);
                p1.Children.Add(position);
                p1.Children.Add(items);
                players_scrollbar.Children.Add(p1);
            }
        }
        private void Update_players_objects()
        {
            IEnumerable<string> names = from Player p in players1 select p.Name;
            IEnumerable<string> created = players_gui.Keys.ToList();
            foreach (var existing in created)
            {
                if (!names.Contains(existing))//delete old
                {
                    canvas1.Children.Remove(players_gui[existing]);
                    players_gui.Remove(existing);
                }
            }
            foreach(var new1 in names)
            {
                if (!created.Contains(new1))
                {
                    Ellipse playerSprite = new Ellipse();
                    playerSprite.Width = 40;
                    playerSprite.Height = 40;
                    ImageBrush myBrush = new ImageBrush();
                    myBrush.ImageSource = new BitmapImage(new Uri(@"..\..\..\..\Sprites\png-clipart-knight-free-content-school-uniform-cartoon-fictional-character.png", UriKind.RelativeOrAbsolute));
                    playerSprite.Fill = myBrush;
                    players_gui.Add(new1, playerSprite);
                    canvas1.Children.Add(playerSprite);
                    Canvas.SetZIndex(playerSprite, 10);
                    Canvas.SetTop(playerSprite, players1.Find(x => x.Name == new1).Y * 50 +5);
                    Canvas.SetLeft(playerSprite, players1.Find(x => x.Name == new1).X * 50 +5);
                }
                else
                {
                    Canvas.SetTop(players_gui[new1], players1.Find(x => x.Name == new1).Y * 50 +5);
                    Canvas.SetLeft(players_gui[new1], players1.Find(x => x.Name == new1).X * 50 +5);
                }
            }
        }
        private void DataWindow_Closing(object sender, EventArgs e)
        {
            if (this.connection != null)
            {
                this.connection.DisposeAsync();
            }
        }
        private async void Key_pressed(object sender, KeyEventArgs e)
        {
            bool changed = false;
            string action = "";
            if (e.Key == Key.L)
            {
                //TODO: switch to next item
                //switchCommand.undo();
            }
            else if (e.Key == Key.J)
            {
                //TODO: switch to previous item
                //switchCommand.execute();
            }
            if (e.Key == Key.E)
            {
                Object[] args = new Object[1] { mapId };
                await this.connection.SendCoreAsync("lootItem", args);
                //Item item = map1.map[current_Player.Y, current_Player.X].Loot;
                //if (item != null)
                //{
                //    current_Player.addItem(item);
                //    map1.map[current_Player.Y, current_Player.X].Loot = null;
                //    changed = true;
                //}
                //TODO: take
                //action = TakeCommand.Execute();
            }
            else if (e.Key == Key.Q)
            {
                //TODO: drop
                //action = TakeCommand.Undo();
            }
            else if (e.Key == Key.K)
            {
                //TODO: use item
                //action = item.use();
            }
            else if (e.Key == Key.B)
            {
                //TODO: drop a trap
                Object[] args = new Object[3] { mapId, current_Player.X, current_Player.Y };
                await this.connection.SendCoreAsync("DropTrap", args);
            }
            //if (action != true)
            //{
            //    Object[] args = new Object[3] { mapId, action};
            //    await this.connection.SendCoreAsync("Action", args);
            //}

            if (e.Key == Key.W)
            {
                if (!settings.Delay.IsCompleted) return;
                else if (current_Player.Y > 0 && map1.map[current_Player.Y - 1, current_Player.X].Surface != Tile.Tile_type.wall)
                {
                    current_Player.Y -= 1;
                    changed = true;
                    canvas_scrollbar.ScrollToVerticalOffset(current_Player.Y*50+5 - canvas_scrollbar.ActualHeight/2);
                    settings.moved();
                    if (map1.map[current_Player.Y, current_Player.X].Surface == Tile.Tile_type.water)
                    {
                        current_Player.setStrategy(new MoveOnWater());
                    }
                    else if (map1.map[current_Player.Y, current_Player.X].Surface == Tile.Tile_type.bush)
                    {
                        current_Player.setStrategy(new MoveOnBush());
                    }
                    else if (map1.map[current_Player.Y, current_Player.X].Surface == Tile.Tile_type.lava)
                    {
                        current_Player.setStrategy(new MoveOnLava());
                    }
                    else if (map1.map[current_Player.Y, current_Player.X].Surface == Tile.Tile_type.grass)
                    {
                        current_Player.setStrategy(new MoveOnGrass());
                    }
                    current_Player.move();
                }
            }
            else if (e.Key == Key.A)
            {
                if (!settings.Delay.IsCompleted) return;
                else if (current_Player.X > 0 && map1.map[current_Player.Y, current_Player.X - 1].Surface != Tile.Tile_type.wall)
                {
                    current_Player.X -= 1;
                    changed = true;
                    canvas_scrollbar.ScrollToHorizontalOffset(current_Player.X * 50 + 5 - canvas_scrollbar.ActualWidth / 2);
                    settings.moved();
                    if (map1.map[current_Player.Y, current_Player.X].Surface == Tile.Tile_type.water)
                    {
                        current_Player.setStrategy(new MoveOnWater());
                    }
                    else if (map1.map[current_Player.Y, current_Player.X].Surface == Tile.Tile_type.bush)
                    {
                        current_Player.setStrategy(new MoveOnBush());
                    }
                    else if (map1.map[current_Player.Y, current_Player.X].Surface == Tile.Tile_type.lava)
                    {
                        current_Player.setStrategy(new MoveOnLava());
                    }
                    else if (map1.map[current_Player.Y, current_Player.X].Surface == Tile.Tile_type.grass)
                    {
                        current_Player.setStrategy(new MoveOnGrass());
                    }
                    current_Player.move();
                }
            }
            else if (e.Key == Key.S)
            {
                if (!settings.Delay.IsCompleted) return;
                else if (current_Player.Y < map1.map_size - 1 && map1.map[current_Player.Y + 1, current_Player.X].Surface != Tile.Tile_type.wall)
                {
                    current_Player.Y += 1;
                    changed = true;
                    canvas_scrollbar.ScrollToVerticalOffset(current_Player.Y * 50 + 5 - canvas_scrollbar.ActualHeight/2);
                    settings.moved();
                    if (map1.map[current_Player.Y, current_Player.X].Surface == Tile.Tile_type.water)
                    {
                        current_Player.setStrategy(new MoveOnWater());
                    }
                    else if (map1.map[current_Player.Y, current_Player.X].Surface == Tile.Tile_type.bush)
                    {
                        current_Player.setStrategy(new MoveOnBush());
                    }
                    else if (map1.map[current_Player.Y, current_Player.X].Surface == Tile.Tile_type.lava)
                    {
                        current_Player.setStrategy(new MoveOnLava());
                    }
                    else if (map1.map[current_Player.Y, current_Player.X].Surface == Tile.Tile_type.grass)
                    {
                        current_Player.setStrategy(new MoveOnGrass());
                    }
                    current_Player.move();
                }
            }
            else if (e.Key == Key.D)
            {
                if (!settings.Delay.IsCompleted) return;
                else if (current_Player.X < map1.map_size - 1 && map1.map[current_Player.Y, current_Player.X + 1].Surface != Tile.Tile_type.wall)
                {
                    current_Player.X += 1;
                    changed = true;
                    canvas_scrollbar.ScrollToHorizontalOffset(current_Player.X * 50 + 5 - canvas_scrollbar.ActualWidth / 2);
                    settings.moved();
                    if (map1.map[current_Player.Y, current_Player.X].Surface == Tile.Tile_type.water)
                    {
                        current_Player.setStrategy(new MoveOnWater());
                    }
                    else if (map1.map[current_Player.Y, current_Player.X].Surface == Tile.Tile_type.bush)
                    {
                        current_Player.setStrategy(new MoveOnBush());
                    }
                    else if (map1.map[current_Player.Y, current_Player.X].Surface == Tile.Tile_type.lava)
                    {
                        current_Player.setStrategy(new MoveOnLava());
                    }
                    else if (map1.map[current_Player.Y, current_Player.X].Surface == Tile.Tile_type.grass)
                    {
                        current_Player.setStrategy(new MoveOnGrass());
                    }
                    current_Player.move();
                }
            }
            if (changed == true)
            {
                Object[] args = new Object[3] { mapId, current_Player.X, current_Player.Y };
                await this.connection.SendCoreAsync("Move", args);
            }
        }

        private void Show_maps_list(object sender, RoutedEventArgs e)
        {
            BrushConverter bc = new BrushConverter();
            Brush brush1 = (Brush)bc.ConvertFrom("#FF18E79D");
            brush1.Freeze();
            Brush brush2 = (Brush)bc.ConvertFrom("#FF131B3C");
            brush2.Freeze();
            login_tab_button.BorderBrush = brush2;
            join_tab_button.BorderBrush = brush1;
            game_tab_button.BorderBrush = brush2;
            Join.Visibility = Visibility.Visible;
            Login.Visibility = Visibility.Hidden;
            Setting_menu.Visibility = Visibility.Hidden;
            Game.Visibility = Visibility.Hidden;
            create_map_panel.Visibility = Visibility.Hidden;
            rooms_for_join.Visibility = Visibility.Visible;
            maps_list_menu.Visibility = Visibility.Visible;
        }

        private void Show_create_map_options(object sender, RoutedEventArgs e)
        {
            BrushConverter bc = new BrushConverter();
            Brush brush1 = (Brush)bc.ConvertFrom("#FF18E79D");
            brush1.Freeze();
            Brush brush2 = (Brush)bc.ConvertFrom("#FF131B3C");
            brush2.Freeze();
            login_tab_button.BorderBrush = brush2;
            join_tab_button.BorderBrush = brush1;
            game_tab_button.BorderBrush = brush2;
            Join.Visibility = Visibility.Visible;
            maps_list_menu.Visibility = Visibility.Hidden;
            create_map_panel.Visibility = Visibility.Visible;
        }

        private void Back_to_login(object sender, RoutedEventArgs e)
        {
            BrushConverter bc = new BrushConverter();
            Brush brush1 = (Brush)bc.ConvertFrom("#FF18E79D");
            brush1.Freeze();
            Brush brush2 = (Brush)bc.ConvertFrom("#FF131B3C");
            brush2.Freeze();
            login_tab_button.BorderBrush = brush1;
            join_tab_button.BorderBrush = brush2;
            game_tab_button.BorderBrush = brush2;
            Login.Visibility = Visibility.Visible;
            Join.Visibility = Visibility.Hidden;
            create_map_panel.Visibility = Visibility.Hidden;
            Game.Visibility = Visibility.Hidden;
            Setting_menu.Visibility = Visibility.Hidden;
        }

        private void Show_settings_menu(object sender, RoutedEventArgs e)
        {
            Login.Visibility = Visibility.Hidden;
            Setting_menu.Visibility = Visibility.Visible;
        }

        private void Show_game_tab(object sender, RoutedEventArgs e)
        {
            BrushConverter bc = new BrushConverter();
            Brush brush1 = (Brush)bc.ConvertFrom("#FF18E79D");
            brush1.Freeze();
            Brush brush2 = (Brush)bc.ConvertFrom("#FF131B3C");
            brush2.Freeze();
            login_tab_button.BorderBrush = brush2;
            join_tab_button.BorderBrush = brush2;
            game_tab_button.BorderBrush = brush1;
            Login.Visibility = Visibility.Hidden;
            Join.Visibility = Visibility.Hidden;
            create_map_panel.Visibility = Visibility.Hidden;
            Game.Visibility = Visibility.Visible;
            Setting_menu.Visibility = Visibility.Hidden;
        }
    }
}
