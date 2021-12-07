using Client.Facade;
using Client.Observer;
using Client.Proxy;
using Client.State;
using Microsoft.AspNetCore.SignalR.Client;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

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
        protected List<Border> inventory_items_gui;
        protected Int32 mapId;
        protected GameSettings settings = GameSettings.GetInstance();
        protected HubConnection connection;
        protected Score ScoreTracking = new Score();
        private MainFacade facade = new MainFacade();
        protected string[] rooms_data_from_server;
        protected IMapControl map_drawer;
        ActionState actionState = new ActionState();
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
            this.map_drawer = new MapDrawProxy(this.canvas_scrollbar, current_Player);
        }
        protected void Connect_to_server(object sender, RoutedEventArgs e)
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

            this.connection = new HubConnectionBuilder().WithUrl(server_addr.Text + "/messageHub").Build();
            //add functions==================
            connection.On<string[]>("Show_maps_options", this.Show_maps_options);
            connection.On<string>("Set_map", this.Set_map);
            connection.On<string>("Set_players", this.Set_players);
            connection.On<Int32, Int32, string>("Update_map_state", this.Update_map_state);
            //===============================
            connection.StartAsync();
            connection.SendAsync("Connect");
        }
        protected void Create_new_room(object sender, RoutedEventArgs e)
        {
            facade.Create_new_room(connection, players_count, game_mode_easy, map_type_1, map_size, game_mode_hard, map_type_2, this);
            //facade.Show_settings_menu(sender, e, Login, Setting_menu);
            //facade.DataWindow_Closing(connection);
            //if (connection == null || connection.State != HubConnectionState.Connected)
            //{
            //    MessageBox.Show("Connect to server first!");
            //    return;
            //}
            //int p_c, m_s;
            //if (!int.TryParse(this.players_count.Text, out p_c) || p_c > 4 || p_c < 2)
            //{
            //    MessageBox.Show("Players count must be between 2 and 4!");
            //    return;
            //}
            //if (!int.TryParse(this.map_size.Text, out m_s) || m_s < 10)
            //{
            //    MessageBox.Show("Map size must be bigger than 10!");
            //    return;
            //}
            //Int32 level = 0;
            //if (game_mode_easy.IsChecked == true) level = 1;
            //else if (game_mode_hard.IsChecked == true) level = 2;
            //Int32 map_type = 0;
            //if (map_type_1.IsChecked == true) map_type = 1;
            //else if (map_type_2.IsChecked == true) map_type = 2;
            //Object[] args = new Object[4]{ Convert.ToInt32(this.players_count.Text), 
            //    Convert.ToInt32(this.map_size.Text), level, map_type};
            //this.connection.SendCoreAsync("Create_map", args);
        }
        protected void Join_room(object sender, RoutedEventArgs e)
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
            }
            mapId = id;
            current_player_name = player_name.Text;
            Object[] args = new Object[2] { id, player_name.Text };
            this.connection.SendCoreAsync("Join_map", args);
            this.Show_game_tab(sender, e);
            this.players_gui = new Dictionary<String, Shape>();
            this.inventory_items_gui = new List<Border>();
        }
        private void Show_maps_options(string[] list)
        {
            if (list[0][0] == 'N')
            {
                return;
            }
            this.rooms_for_join.Items.Clear();
            this.rooms_data_from_server = list;
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
            this.map_drawer.DrawMap(map1, players_gui, canvas1);
        }
        private void Update_map_state(Int32 y, Int32 x, string text)
        {
            string[] parts1 = text.Split(';');
            if (parts1[0] == "remove")
            {
                canvas1.Children.Remove(this.map1.map[y, x].Icon);
                this.map1.map[y, x].Icon = null;
                this.map1.map[y, x].Loot = null;
            }
            else if (parts1[0] == "create")
            {
                Item it1;
                switch (parts1[1])
                {
                    case "BlueGun":
                        it1 = new BlueGun();
                        break;
                    case "RedGun":
                        it1 = new RedGun();
                        break;
                    case "BlueMedicKit":
                        it1 = new BlueMedicKit();
                        break;
                    case "RedMedicKit":
                        it1 = new RedMedicKit();
                        break;
                    default:
                        it1 = null;
                        break;
                }
                this.map1.map[y, x].Loot = it1;
                this.map1.map[y, x].Icon = it1.get_view();
                canvas1.Children.Add(this.map1.map[y, x].Icon);
                Canvas.SetTop   (map1.map[y, x].Icon, y * 50 + 5);
                Canvas.SetLeft  (map1.map[y, x].Icon, x * 50 + 5);
                Canvas.SetZIndex(map1.map[y, x].Icon, 1);
            }
        }

        //private void DrawMap()
        //{
        //    facade.DrawMap(map1, players_gui, canvas1);
        //    //int pos_x = 0, pos_y = 0;
        //    //canvas1.Children.Clear();
        //    //canvas1.Height = Convert.ToInt32(map1.map_size) * 50 + 20;
        //    //canvas1.Width = Convert.ToInt32(map1.map_size) * 50 + 20;
        //    //for (int i = 0; i < Convert.ToInt32(map1.map_size); i++)
        //    //{
        //    //    pos_x = 0;
        //    //    for (int j = 0; j < Convert.ToInt32(map1.map_size); j++)
        //    //    {
        //    //        Rectangle myRect = new System.Windows.Shapes.Rectangle();
        //    //        myRect.Stroke = System.Windows.Media.Brushes.Black;
        //    //        if (map1.map[i, j].Surface == Tile.Tile_type.grass)
        //    //        {
        //    //            myRect.Fill = System.Windows.Media.Brushes.LightGreen;
        //    //        }
        //    //        else if (map1.map[i, j].Surface == Tile.Tile_type.wall)
        //    //        {
        //    //            myRect.Fill = System.Windows.Media.Brushes.Gray;
        //    //        }
        //    //        else if (map1.map[i, j].Surface == Tile.Tile_type.water)
        //    //        {
        //    //            myRect.Fill = System.Windows.Media.Brushes.Blue;
        //    //        }
        //    //        else if (map1.map[i, j].Surface == Tile.Tile_type.lava)
        //    //        {
        //    //            myRect.Fill = System.Windows.Media.Brushes.OrangeRed;
        //    //        }
        //    //        else if (map1.map[i, j].Surface == Tile.Tile_type.bush)
        //    //        {
        //    //            myRect.Fill = System.Windows.Media.Brushes.DarkGreen;
        //    //        }
        //    //        myRect.Height = 50;
        //    //        myRect.Width = 50;
        //    //        this.canvas1.Children.Add(myRect);
        //    //        Canvas.SetTop(myRect, pos_y);
        //    //        Canvas.SetLeft(myRect, pos_x);
        //    //        Canvas.SetZIndex(myRect, 0);
        //    //        if (map1.map[i, j].Loot != null)
        //    //        {
        //    //            map1.map[i, j].Icon = map1.map[i, j].Loot.get_view();
        //    //            this.canvas1.Children.Add(map1.map[i, j].Icon);
        //    //            Canvas.SetTop(map1.map[i, j].Icon, pos_y +5);
        //    //            Canvas.SetLeft(map1.map[i, j].Icon, pos_x+5);
        //    //            Canvas.SetZIndex(map1.map[i, j].Icon, 1);
        //    //        }
        //    //        pos_x += 50;
        //    //    }
        //    //    pos_y += 50;
        //    //}
        //    //foreach(var el in players_gui.Values)
        //    //{
        //    //    canvas1.Children.Remove(el);
        //    //    canvas1.Children.Add(el);
        //    //    Canvas.SetZIndex(el, 10);
        //    //}
        //}
        private void Set_players(string json_text)
        {
            //gamestate.setState(new PreparationState(gamestate));
            JArray json_players = JArray.Parse(json_text);
            players1 = new List<Player>();
            for (int i = 0; i < json_players.Count; i++)
            {
                List<Item> item_list = new List<Item>(); 
                JArray loot_items= (JArray)json_players[i]["Inventory"];
                for (int j = 0; j < loot_items.Count; j++)
                {
                    switch ((string)loot_items[j]["Type"])
                    {
                        case "BlueBerry":
                            item_list.Add(new BlueBerry()); 
                            break;
                        case "RedBerry":
                            item_list.Add(new RedBerry());
                            break;
                        case "BlueGun":
                            item_list.Add(new BlueGun());
                            break;
                        case "RedGun":
                            item_list.Add(new RedGun());
                            break;
                        case "BlueMedicKit":
                            item_list.Add(new BlueMedicKit());
                            break;
                        case "RedMedicKit":
                            item_list.Add(new RedMedicKit());
                            break;
                        default:
                            break;
                    }
                }
                players1.Add(new Player((int)json_players[i]["Health"], (string)json_players[i]["Name"],
                    (int)json_players[i]["X"], (int)json_players[i]["Y"],(int)json_players[i]["Points"], item_list, (string)json_players[i]["Comment"]));
            }
            current_Player = players1.Find(x => x.Name == current_player_name);

            this.map_drawer.current = current_Player;
            items_scrollbar.Children.Clear();
            inventory_items_gui.Clear();
            for (int i = 0; i < current_Player.Inventory.Count; i++)
            {
                Border b = new Border();
                b.Child = current_Player.Inventory[i].get_view();
                b.Margin = new Thickness(5);
                b.BorderThickness = new Thickness(3);
                b.Width = 50;
                if (this.current_Player.currentItem == i)
                {
                    BrushConverter bc = new BrushConverter();
                    Brush brush = (Brush)bc.ConvertFrom("#FF18E79D");
                    brush.Freeze();
                    b.BorderBrush= brush;
                }
                this.inventory_items_gui.Add(b);
                items_scrollbar.Children.Add(b);
            }
            facade.Update_players_objects(players1, players_gui, canvas1, current_Player);
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
                Label position = new Label();
                position.Content = String.Format("X:{0}, Y:{1}", players1[i].X, players1[i].Y);
                Label log = new Label();
                log.Content = "Log: " + players1[i].Comment;
                p1.Children.Add(name);
                p1.Children.Add(health);
                p1.Children.Add(points);
                p1.Children.Add(position);
                p1.Children.Add(log);
                players_scrollbar.Children.Add(p1);
            }
        }
        //private void Update_players_objects()
        //{
        //    IEnumerable<string> names = from Player p in players1 select p.Name;
        //    IEnumerable<string> created = players_gui.Keys.ToList();
        //    foreach (var existing in created)
        //    {
        //        if (!names.Contains(existing))//delete old
        //        {
        //            canvas1.Children.Remove(players_gui[existing]);
        //            players_gui.Remove(existing);
        //        }
        //    }
        //    foreach (var new1 in names)
        //    {
        //        if (!created.Contains(new1))
        //        {
        //            Ellipse playerSprite = new Ellipse();
        //            playerSprite.Width = 40;
        //            playerSprite.Height = 40;
        //            ImageBrush myBrush = new ImageBrush();
        //            myBrush.ImageSource = new BitmapImage(new Uri(@"..\..\..\..\Sprites\png-clipart-knight-free-content-school-uniform-cartoon-fictional-character.png", UriKind.RelativeOrAbsolute));
        //            playerSprite.Fill = myBrush;
        //            players_gui.Add(new1, playerSprite);
        //            canvas1.Children.Add(playerSprite);
        //            Canvas.SetZIndex(playerSprite, 10);
        //            Canvas.SetTop(playerSprite, players1.Find(x => x.Name == new1).Y * 50 + 5);
        //            Canvas.SetLeft(playerSprite, players1.Find(x => x.Name == new1).X * 50 + 5);
        //        }
        //        else
        //        {
        //            Canvas.SetTop(players_gui[new1], players1.Find(x => x.Name == new1).Y * 50 + 5);
        //            Canvas.SetLeft(players_gui[new1], players1.Find(x => x.Name == new1).X * 50 + 5);
        //        }
        //    }
        //}
        private void DataWindow_Closing(object sender, EventArgs e)
        {
            if (this.connection != null)
            {
                this.connection.DisposeAsync();
            }
        }
        protected void Key_pressed(object sender, KeyEventArgs e)
        {
            facade.Move_player(map1, current_Player, settings, map_drawer, e, connection, mapId, players_scrollbar, actionState);
            facade.Check_if_steped_on_trap(map1, current_Player, connection, mapId, e);
            facade.Drop_trap(current_Player, connection, mapId, e, actionState);
            facade.Actions_with_items(map1, current_Player, connection, mapId, e, inventory_items_gui, actionState);
            //string action = "";
            //string info = "";
            //Invoker invoker = new Invoker();
            //if (e.Key == Key.U)
            //{
            //    invoker.undo(current_Player.Name);
            //}
            //if (e.Key == Key.L)
            //{
            //    facade.NextItem(invoker, inventory_items_gui, current_Player);
            //    BrushConverter bc;
            //    Brush brush;
            //    if (current_Player.currentItem >= 0)
            //    {
            //        bc = new BrushConverter();
            //        brush = (Brush)bc.ConvertFrom("#FF333337");
            //        brush.Freeze();
            //        inventory_items_gui[current_Player.currentItem].BorderBrush = brush;
            //    }
            //    invoker.SetCommand(new SwitchCommand(current_Player));
            //    invoker.InvokeSwitch();
            //    if (current_Player.currentItem >= 0)
            //    {
            //        bc = new BrushConverter();
            //        brush = (Brush)bc.ConvertFrom("#FF18E79D");
            //        brush.Freeze();
            //        inventory_items_gui[current_Player.currentItem].BorderBrush = brush;
            //    }
            //}
            //else if (e.Key == Key.J)
            //{
            //    facade.NextItem(invoker, inventory_items_gui, current_Player);
            //BrushConverter bc;
            //Brush brush;
            //if (current_Player.currentItem >= 0)
            //{
            //    bc = new BrushConverter();
            //    brush = (Brush)bc.ConvertFrom("#FF333337");
            //    brush.Freeze();
            //    inventory_items_gui[current_Player.currentItem].BorderBrush = brush;
            //}
            //invoker.SetCommand(new SwitchCommand(current_Player));
            //invoker.InvokeUnSwitch();
            //if (current_Player.currentItem >= 0)
            //{
            //    bc = new BrushConverter();
            //    brush = (Brush)bc.ConvertFrom("#FF18E79D");
            //    brush.Freeze();
            //    inventory_items_gui[current_Player.currentItem].BorderBrush = brush;
            //}
            //}
            //if (e.Key == Key.E)
            //{
            //    if (map1.map[current_Player.Y, current_Player.X].Loot != null)
            //    {
            //        invoker.SetCommand(new TakeDropCommand(action = "take"));
            //        invoker.InvokeTake();
            //    }
            //}
            //else if (e.Key == Key.Q)
            //{
            //    if (map1.map[current_Player.Y, current_Player.X].Loot == null)
            //    {
            //        invoker.SetCommand(new TakeDropCommand(action = "drop"));
            //        invoker.InvokeDrop();
            //        info = current_Player.Inventory[current_Player.currentItem].Type;
            //    }
            //}
            //else if (e.Key == Key.K)
            //{
            //    if (current_Player.currentItem >= 0)
            //    {
            //        action = "use_item";
            //        info = current_Player.Inventory[current_Player.currentItem].Type;
            //    }
            //}
            //else if (e.Key == Key.B)
            //{
            //    
            //    Object[] args = new Object[3] { mapId, current_Player.X, current_Player.Y };
            //    await this.connection.SendCoreAsync("DropTrap", args);
            //}

            //if (action != "")
            //{
            //    Object[] args = new Object[5] { mapId, current_Player.Y, current_Player.X, action, info};
            //    await this.connection.SendCoreAsync("Action", args);
            //}

            //if (e.Key == Key.W || e.Key == Key.A || e.Key == Key.S || e.Key == Key.D)
            //{
            //    facade.Check_step_on_fire(map1, current_Player, connection, mapId);
            //    facade.Check_step_on_trap(map1, current_Player, connection, mapId);

            //}
            //bool changed = false;
            //if (e.Key == Key.W)
            //{
            //    if (!settings.Delay.IsCompleted) return;
            //    else if (current_Player.Y > 0 && map1.map[current_Player.Y - 1, current_Player.X].Surface != Tile.Tile_type.wall)
            //    {
            //        current_Player.Y -= 1;
            //        changed = true;
            //        canvas_scrollbar.ScrollToVerticalOffset(current_Player.Y*50+5 - canvas_scrollbar.ActualHeight/2);
            //        settings.moved();
            //        if (map1.map[current_Player.Y, current_Player.X].Surface == Tile.Tile_type.water)
            //        {
            //            current_Player.setStrategy(new MoveOnWater());
            //        }
            //        else if (map1.map[current_Player.Y, current_Player.X].Surface == Tile.Tile_type.bush)
            //        {
            //            current_Player.setStrategy(new MoveOnBush());
            //        }
            //        else if (map1.map[current_Player.Y, current_Player.X].Surface == Tile.Tile_type.lava)
            //        {
            //            current_Player.setStrategy(new MoveOnLava());
            //        }
            //        else if (map1.map[current_Player.Y, current_Player.X].Surface == Tile.Tile_type.grass)
            //        {
            //            current_Player.setStrategy(new MoveOnGrass());
            //        }
            //        current_Player.move();
            //    }
            //}
            //else if (e.Key == Key.A)
            //{
            //    if (!settings.Delay.IsCompleted) return;
            //    else if (current_Player.X > 0 && map1.map[current_Player.Y, current_Player.X - 1].Surface != Tile.Tile_type.wall)
            //    {
            //        current_Player.X -= 1;
            //        changed = true;
            //        canvas_scrollbar.ScrollToHorizontalOffset(current_Player.X * 50 + 5 - canvas_scrollbar.ActualWidth / 2);
            //        settings.moved();
            //        if (map1.map[current_Player.Y, current_Player.X].Surface == Tile.Tile_type.water)
            //        {
            //            current_Player.setStrategy(new MoveOnWater());
            //        }
            //        else if (map1.map[current_Player.Y, current_Player.X].Surface == Tile.Tile_type.bush)
            //        {
            //            current_Player.setStrategy(new MoveOnBush());
            //        }
            //        else if (map1.map[current_Player.Y, current_Player.X].Surface == Tile.Tile_type.lava)
            //        {
            //            current_Player.setStrategy(new MoveOnLava());
            //        }
            //        else if (map1.map[current_Player.Y, current_Player.X].Surface == Tile.Tile_type.grass)
            //        {
            //            current_Player.setStrategy(new MoveOnGrass());
            //        }
            //        current_Player.move();
            //    }
            //}
            //else if (e.Key == Key.S)
            //{
            //    if (!settings.Delay.IsCompleted) return;
            //    else if (current_Player.Y < map1.map_size - 1 && map1.map[current_Player.Y + 1, current_Player.X].Surface != Tile.Tile_type.wall)
            //    {
            //        current_Player.Y += 1;
            //        changed = true;
            //        canvas_scrollbar.ScrollToVerticalOffset(current_Player.Y * 50 + 5 - canvas_scrollbar.ActualHeight/2);
            //        settings.moved();
            //        if (map1.map[current_Player.Y, current_Player.X].Surface == Tile.Tile_type.water)
            //        {
            //            current_Player.setStrategy(new MoveOnWater());
            //        }
            //        else if (map1.map[current_Player.Y, current_Player.X].Surface == Tile.Tile_type.bush)
            //        {
            //            current_Player.setStrategy(new MoveOnBush());
            //        }
            //        else if (map1.map[current_Player.Y, current_Player.X].Surface == Tile.Tile_type.lava)
            //        {
            //            current_Player.setStrategy(new MoveOnLava());
            //        }
            //        else if (map1.map[current_Player.Y, current_Player.X].Surface == Tile.Tile_type.grass)
            //        {
            //            current_Player.setStrategy(new MoveOnGrass());
            //        }
            //        current_Player.move();
            //    }
            //}
            //else if (e.Key == Key.D)
            //{
            //    if (!settings.Delay.IsCompleted) return;
            //    else if (current_Player.X < map1.map_size - 1 && map1.map[current_Player.Y, current_Player.X + 1].Surface != Tile.Tile_type.wall)
            //    {
            //        current_Player.X += 1;
            //        changed = true;
            //        canvas_scrollbar.ScrollToHorizontalOffset(current_Player.X * 50 + 5 - canvas_scrollbar.ActualWidth / 2);
            //        settings.moved();
            //        if (map1.map[current_Player.Y, current_Player.X].Surface == Tile.Tile_type.water)
            //        {
            //            current_Player.setStrategy(new MoveOnWater());
            //        }
            //        else if (map1.map[current_Player.Y, current_Player.X].Surface == Tile.Tile_type.bush)
            //        {
            //            current_Player.setStrategy(new MoveOnBush());
            //        }
            //        else if (map1.map[current_Player.Y, current_Player.X].Surface == Tile.Tile_type.lava)
            //        {
            //            current_Player.setStrategy(new MoveOnLava());
            //        }
            //        else if (map1.map[current_Player.Y, current_Player.X].Surface == Tile.Tile_type.grass)
            //        {
            //            current_Player.setStrategy(new MoveOnGrass());
            //        }
            //        current_Player.move();
            //    }
            //}
            //if (changed == true)
            //{
            //    Object[] args = new Object[3] { mapId, current_Player.X, current_Player.Y };
            //    await this.connection.SendCoreAsync("Move", args);
            //}

        }

        //private async void Check_step_on_trap()
        //{
        //    if (map1.map[current_Player.Y, current_Player.X].Loot != null)
        //    {
        //        if (map1.map[current_Player.Y, current_Player.X].Loot.Type == new VisibleTrap().Type ||
        //            map1.map[current_Player.Y, current_Player.X].Loot.Type == new InVisibleTrap().Type)
        //        {
        //            Object[] args = new Object[3] { mapId, current_Player.X, current_Player.Y };
        //            await this.connection.SendCoreAsync("ActivateTrap", args);
        //        }
        //    }
        //}
        //private async void Check_step_on_fire()
        //{
        //    if (map1.map[current_Player.Y, current_Player.X].Loot != null)
        //    {
        //        int stepsCount = 0;
        //        if (map1.map[current_Player.Y, current_Player.X].Loot.Type == new HighFireDecorator(new Fire(0)).Type)
        //        {
        //            stepsCount = (map1.map[current_Player.Y, current_Player.X].Loot as HighFireDecorator).GetNumber();
        //            Object[] args = new Object[4] { mapId, current_Player.X, current_Player.Y, stepsCount };
        //            await this.connection.SendCoreAsync("StepOnFire", args);
        //        }
        //        else if (map1.map[current_Player.Y, current_Player.X].Loot.Type == new MediumFireDecorator(new Fire(0)).Type) {
        //            stepsCount = (map1.map[current_Player.Y, current_Player.X].Loot as MediumFireDecorator).GetNumber();
        //            Object[] args = new Object[4] { mapId, current_Player.X, current_Player.Y, stepsCount };
        //            await this.connection.SendCoreAsync("StepOnFire", args);
        //        }
        //        else if (map1.map[current_Player.Y, current_Player.X].Loot.Type == new LowFireDecorator(new Fire(0)).Type)
        //        {
        //            stepsCount = (map1.map[current_Player.Y, current_Player.X].Loot as LowFireDecorator).GetNumber();
        //            Object[] args = new Object[4] { mapId, current_Player.X, current_Player.Y, stepsCount };
        //            await this.connection.SendCoreAsync("StepOnFire", args);
        //        }
        //    }
        //}

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
            settings_tab_button.BorderBrush = brush2;
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
            new_map_notification.Visibility = Visibility.Hidden;
            BrushConverter bc = new BrushConverter();
            Brush brush1 = (Brush)bc.ConvertFrom("#FF18E79D");
            brush1.Freeze();
            Brush brush2 = (Brush)bc.ConvertFrom("#FF131B3C");
            brush2.Freeze();
            login_tab_button.BorderBrush = brush2;
            join_tab_button.BorderBrush = brush1;
            game_tab_button.BorderBrush = brush2;
            settings_tab_button.BorderBrush = brush2;
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
            settings_tab_button.BorderBrush = brush2;
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
            Join.Visibility = Visibility.Hidden;
            create_map_panel.Visibility = Visibility.Hidden;
            Game.Visibility = Visibility.Hidden;
            BrushConverter bc = new BrushConverter();
            Brush brush1 = (Brush)bc.ConvertFrom("#FF18E79D");
            brush1.Freeze();
            Brush brush2 = (Brush)bc.ConvertFrom("#FF131B3C");
            brush2.Freeze();
            login_tab_button.BorderBrush = brush2;
            join_tab_button.BorderBrush = brush2;
            game_tab_button.BorderBrush = brush2;
            settings_tab_button.BorderBrush = brush1;
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
            settings_tab_button.BorderBrush = brush2;
            Login.Visibility = Visibility.Hidden;
            Join.Visibility = Visibility.Hidden;
            create_map_panel.Visibility = Visibility.Hidden;
            Game.Visibility = Visibility.Visible;
            Setting_menu.Visibility = Visibility.Hidden;
        }
        unsafe protected void Settings_changed(object sender, TextChangedEventArgs e)
        {
            List<TextBox> fields = new List<TextBox> { this.Move_up_key, this.Move_down_key,
                this.Move_left_key, this.Move_right_key, this.Take_item_key, this.Drop_item_key,
                this.Switch_left_key, this.Switch_right_key, this.Use_item_key, this.Put_bomb_key};
            if (fields.Contains(null))
            {
                return;
            }
            fixed (Key* k1 = &GameSettings.GetInstance().Move_up_key)
            {
                [DllImport("user32.dll", CharSet = CharSet.Unicode)]
                static extern short VkKeyScan(char ch);
                for (int i = 0; i < fields.Count; i++)
                {
                    if (fields[i].Text.Length > 0)
                    {
                        char ch = fields[i].Text[0];
                        int v_key = VkKeyScan(ch) & 0xff;
                        *(k1 + i) = KeyInterop.KeyFromVirtualKey(v_key);
                    }
                }
            }
        }
        protected void Map_selected(object sender, SelectionChangedEventArgs e)
        {
            if (rooms_for_join.SelectedItem == null)
            {
                return;
            }
            string map_id_1 = ((Rooms_list_view_obj)rooms_for_join.SelectedItem).map_id;
            string line1 = rooms_data_from_server.ToList<string>().Find(x => x.Split(',')[0].Split(':')[1] == map_id_1);
            string[] data = line1.Split(',');
            map_id_text_box.Text =         data[0].Split(new char[] { ':' })[1];
            players_online_text_box.Text = data[1].Split(new char[] { ':' })[1];
            map_size_text_box.Text =       data[2].Split(new char[] { ':' })[1];
            level_text_box.Text =          data[3].Split(new char[] { ':' })[1];
            map_state_text_box.Text =      data[4].Split(new char[] { ':' })[1];
        }
    }
}
