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
using Client.Strategy;
using System.Windows.Media.Animation;
using Client.Observer;
using Client.Decorator;
using Client.Command;
using Client.Facade;
using Newtonsoft.Json.Linq;

namespace Client.Facade
{
    class MainFacade
    {
        public async void Move_player(Map map1, Player current_Player, GameSettings settings, ScrollViewer canvas_scrollbar, KeyEventArgs e, HubConnection connection, Int32 mapId)
        {
            bool changed = false;
            if (e.Key == Key.W)
            {
                if (!settings.Delay.IsCompleted) return;
                else if (current_Player.Y > 0 && map1.map[current_Player.Y - 1, current_Player.X].Surface != Tile.Tile_type.wall)
                {
                    current_Player.Y -= 1;
                    changed = true;
                    canvas_scrollbar.ScrollToVerticalOffset(current_Player.Y * 50 + 5 - canvas_scrollbar.ActualHeight / 2);
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
                    canvas_scrollbar.ScrollToVerticalOffset(current_Player.Y * 50 + 5 - canvas_scrollbar.ActualHeight / 2);
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
                await connection.SendCoreAsync("Move", args);
            }
        }
        public async void Actions_with_items(Map map1, Player current_Player, HubConnection connection, Int32 mapId, KeyEventArgs e, List<Border> inventory_items_gui)
        {
            string action = "";
            string info = "";
            Invoker invoker = new Invoker();
            if (e.Key == Key.U)
            {
                invoker.undo(current_Player.Name);
            }
            if (e.Key == Key.L)
            {
                BrushConverter bc;
                Brush brush;
                if (current_Player.currentItem >= 0)
                {
                    bc = new BrushConverter();
                    brush = (Brush)bc.ConvertFrom("#FF333337");
                    brush.Freeze();
                    inventory_items_gui[current_Player.currentItem].BorderBrush = brush;
                }
                invoker.SetCommand(new SwitchCommand(current_Player));
                invoker.InvokeSwitch();
                if (current_Player.currentItem >= 0)
                {
                    bc = new BrushConverter();
                    brush = (Brush)bc.ConvertFrom("#FF18E79D");
                    brush.Freeze();
                    inventory_items_gui[current_Player.currentItem].BorderBrush = brush;
                }
            }
            else if (e.Key == Key.J)
            {
                BrushConverter bc;
                Brush brush;
                if (current_Player.currentItem >= 0)
                {
                    bc = new BrushConverter();
                    brush = (Brush)bc.ConvertFrom("#FF333337");
                    brush.Freeze();
                    inventory_items_gui[current_Player.currentItem].BorderBrush = brush;
                }
                invoker.SetCommand(new SwitchCommand(current_Player));
                invoker.InvokeUnSwitch();
                if (current_Player.currentItem >= 0)
                {
                    bc = new BrushConverter();
                    brush = (Brush)bc.ConvertFrom("#FF18E79D");
                    brush.Freeze();
                    inventory_items_gui[current_Player.currentItem].BorderBrush = brush;
                }
            }
            if (e.Key == Key.E)
            {
                if (map1.map[current_Player.Y, current_Player.X].Loot != null)
                {
                    invoker.SetCommand(new TakeDropCommand(action = "take"));
                    invoker.InvokeTake();
                }
            }
            else if (e.Key == Key.Q)
            {
                if (map1.map[current_Player.Y, current_Player.X].Loot == null)
                {
                    invoker.SetCommand(new TakeDropCommand(action = "drop"));
                    invoker.InvokeDrop();
                    info = current_Player.Inventory[current_Player.currentItem].Type;
                }
            }
            else if (e.Key == Key.K)
            {
                if (current_Player.currentItem >= 0)
                {
                    action = "use_item";
                    info = current_Player.Inventory[current_Player.currentItem].Type;
                }
            }
            if (action != "")
            {
                Object[] args = new Object[5] { mapId, current_Player.Y, current_Player.X, action, info };
                await connection.SendCoreAsync("Action", args);
            }
        }
        public async void Drop_trap(Player current_Player, HubConnection connection, Int32 mapId, KeyEventArgs e)
        {
            if (e.Key == Key.B)
            {
                //TODO: drop a trap
                Object[] args = new Object[3] { mapId, current_Player.X, current_Player.Y };
                await connection.SendCoreAsync("DropTrap", args);
            }
        }

        public void Check_if_steped_on_trap(Map map1, Player current_Player, HubConnection connection, Int32 mapId, KeyEventArgs e)
        {
            if (e.Key == Key.W || e.Key == Key.A || e.Key == Key.S || e.Key == Key.D)
            {
                Check_step_on_fire(map1, current_Player, connection, mapId);
                Check_step_on_trap(map1, current_Player, connection, mapId);
            }
        }
        public async void Check_step_on_trap(Map map1, Player current_Player, HubConnection connection, Int32 mapId)
        {
            if (map1.map[current_Player.Y, current_Player.X].Loot != null)
            {
                if (map1.map[current_Player.Y, current_Player.X].Loot.Type == new VisibleTrap().Type ||
                    map1.map[current_Player.Y, current_Player.X].Loot.Type == new InVisibleTrap().Type)
                {
                    Object[] args = new Object[3] { mapId, current_Player.X, current_Player.Y };
                    await connection.SendCoreAsync("ActivateTrap", args);
                }
            }
        }

        public async void Check_step_on_fire(Map map1, Player current_Player, HubConnection connection, Int32 mapId)
        {
            if (map1.map[current_Player.Y, current_Player.X].Loot != null)
            {
                int stepsCount = 0;
                if (map1.map[current_Player.Y, current_Player.X].Loot.Type == new HighFireDecorator(new Fire(0)).Type)
                {
                    stepsCount = (map1.map[current_Player.Y, current_Player.X].Loot as HighFireDecorator).GetNumber();
                    Object[] args = new Object[4] { mapId, current_Player.X, current_Player.Y, stepsCount };
                    await connection.SendCoreAsync("StepOnFire", args);
                }
                else if (map1.map[current_Player.Y, current_Player.X].Loot.Type == new MediumFireDecorator(new Fire(0)).Type)
                {
                    stepsCount = (map1.map[current_Player.Y, current_Player.X].Loot as MediumFireDecorator).GetNumber();
                    Object[] args = new Object[4] { mapId, current_Player.X, current_Player.Y, stepsCount };
                    await connection.SendCoreAsync("StepOnFire", args);
                }
                else if (map1.map[current_Player.Y, current_Player.X].Loot.Type == new LowFireDecorator(new Fire(0)).Type)
                {
                    stepsCount = (map1.map[current_Player.Y, current_Player.X].Loot as LowFireDecorator).GetNumber();
                    Object[] args = new Object[4] { mapId, current_Player.X, current_Player.Y, stepsCount };
                    await connection.SendCoreAsync("StepOnFire", args);
                }
            }
        }
        
        public void DrawMap(Map map1, Dictionary<String, Shape> players_gui, Canvas canvas1)
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
                    canvas1.Children.Add(myRect);
                    Canvas.SetTop(myRect, pos_y);
                    Canvas.SetLeft(myRect, pos_x);
                    Canvas.SetZIndex(myRect, 0);
                    if (map1.map[i, j].Loot != null)
                    {
                        map1.map[i, j].Icon = map1.map[i, j].Loot.get_view();
                        canvas1.Children.Add(map1.map[i, j].Icon);
                        Canvas.SetTop(map1.map[i, j].Icon, pos_y + 5);
                        Canvas.SetLeft(map1.map[i, j].Icon, pos_x + 5);
                        Canvas.SetZIndex(map1.map[i, j].Icon, 1);
                    }
                    pos_x += 50;
                }
                pos_y += 50;
            }
            foreach (var el in players_gui.Values)
            {
                canvas1.Children.Remove(el);
                canvas1.Children.Add(el);
                Canvas.SetZIndex(el, 10);
            }
        }
        public void Create_new_room(HubConnection connection, TextBox players_count, RadioButton game_mode_easy, RadioButton map_type_1, TextBox map_size, RadioButton game_mode_hard, RadioButton map_type_2)
        {
            if (connection == null || connection.State != HubConnectionState.Connected)
            {
                MessageBox.Show("Connect to server first!");
                return;
            }
            int p_c, m_s;
            if (!int.TryParse(players_count.Text, out p_c) || p_c > 4 || p_c < 2)
            {
                MessageBox.Show("Players count must be between 2 and 4!");
                return;
            }
            if (!int.TryParse(map_size.Text, out m_s) || m_s < 10)
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
            Object[] args = new Object[4]{ Convert.ToInt32(players_count.Text),
                Convert.ToInt32(map_size.Text), level, map_type};
            connection.SendCoreAsync("Create_map", args);
        }
        public void Update_players_objects(List<Player> players1, Dictionary<String, Shape> players_gui, Canvas canvas1)
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
            foreach (var new1 in names)
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
                    Canvas.SetTop(playerSprite, players1.Find(x => x.Name == new1).Y * 50 + 5);
                    Canvas.SetLeft(playerSprite, players1.Find(x => x.Name == new1).X * 50 + 5);
                }
                else
                {
                    Canvas.SetTop(players_gui[new1], players1.Find(x => x.Name == new1).Y * 50 + 5);
                    Canvas.SetLeft(players_gui[new1], players1.Find(x => x.Name == new1).X * 50 + 5);
                }
            }
        }
    }
}
