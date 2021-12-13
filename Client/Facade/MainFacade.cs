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
using Client.Observer;
using Client.Decorator;
using Client.Command;
using Client.Facade;
using Newtonsoft.Json.Linq;
using Client.Adapter;
using Client.Proxy;
using Client.State;
using Client.Composite;
using Client.Memento;

namespace Client.Facade
{
    class MainFacade
    {
        public async void Move_player(Map map1, Player current_Player, GameSettings settings, IMapControl map_controler, KeyEventArgs e, HubConnection connection, Int32 mapId, /*StackPanel players_scrollbar,*/ ActionState gamestate/*, Orginator orginator, Caretaker caretaker*/)
        {
            bool changed = false;
            //if (e.Key == Key.P)
            //{
            //    if (gamestate.message == null)
            //    {
            //        gamestate.setState(new StartMovingState(gamestate));
            //        Label state = new Label();
            //        state.Content = "State: " + gamestate.message;
            //        players_scrollbar.Children.Add(state);
            //        current_Player.State = gamestate.message;
            //    }
            //    else
            //    {
            //        gamestate.NextState();
            //        Label state = new Label();
            //        state.Content = "State: " + gamestate.message;
            //        players_scrollbar.Children.Add(state);
            //        current_Player.State = gamestate.message;
            //    }
            //}
            //if (e.Key == Key.R)
            //{
            //    action = "eik";
            //    //if (!settings.Delay.IsCompleted) return;
            //    orginator.RestoreMemento(caretaker.Memento);
            //    Label state = new Label();
            //    state.Content = "r: " + orginator.Points;
            //    players_scrollbar.Children.Add(state);
            //    current_Player.Points = orginator.Points;
            //    //current_Player.X -= 2;
            //    current_Player.Y -= 1;
            //    changed = true;
            //    settings.moved();
            //    //current_Player.move();
            //}

            if (gamestate.message == "Start moving" || gamestate.message == "Picking items" || gamestate.message == "Using inventory" || gamestate.message == "Dropping traps")
            {
                if (e.Key == GameSettings.GetInstance().Move_up_key)
            {
                if (!settings.Delay.IsCompleted) return;
                else if (current_Player.Y > 0 && map1.map[current_Player.Y - 1, current_Player.X].Surface != Tile.Tile_type.wall)
                {
                    current_Player.Y -= 1;
                    changed = true;
                    //canvas_scrollbar.ScrollToVerticalOffset(current_Player.Y * 50 + 5 - canvas_scrollbar.ActualHeight / 2);
                    map_controler.SetScrollBar();
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
            else if (e.Key == GameSettings.GetInstance().Move_left_key)
            {
                if (!settings.Delay.IsCompleted) return;
                else if (current_Player.X > 0 && map1.map[current_Player.Y, current_Player.X - 1].Surface != Tile.Tile_type.wall)
                {
                    current_Player.X -= 1;
                    changed = true;
                    //canvas_scrollbar.ScrollToHorizontalOffset(current_Player.X * 50 + 5 - canvas_scrollbar.ActualWidth / 2);
                    map_controler.SetScrollBar();
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
            else if (e.Key == GameSettings.GetInstance().Move_down_key)
            {
                if (!settings.Delay.IsCompleted) return;
                else if (current_Player.Y < map1.map_size - 1 && map1.map[current_Player.Y + 1, current_Player.X].Surface != Tile.Tile_type.wall)
                {
                    current_Player.Y += 1;
                    changed = true;
                    //canvas_scrollbar.ScrollToVerticalOffset(current_Player.Y * 50 + 5 - canvas_scrollbar.ActualHeight / 2);
                    map_controler.SetScrollBar();
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
            else if (e.Key == GameSettings.GetInstance().Move_right_key)
            {
                if (!settings.Delay.IsCompleted) return;
                else if (current_Player.X < map1.map_size - 1 && map1.map[current_Player.Y, current_Player.X + 1].Surface != Tile.Tile_type.wall)
                {
                    current_Player.X += 1;
                    changed = true;
                    //canvas_scrollbar.ScrollToHorizontalOffset(current_Player.X * 50 + 5 - canvas_scrollbar.ActualWidth / 2);
                    map_controler.SetScrollBar();
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
        }
        public async void Actions_with_items(Map map1, Player current_Player, HubConnection connection, Int32 mapId, KeyEventArgs e, List<Border> inventory_items_gui, ActionState actionState, Orginator orginator, Caretaker caretaker)
        {
            string action = "";
            string info = "";
            Invoker invoker = new Invoker();
            if (actionState.message == "Start moving" || actionState.message == "Picking items" || actionState.message == "Using inventory" || actionState.message == "Dropping traps")
            {
                if (e.Key == Key.W)
                {
                    action = "add_comment";
                }
                if (e.Key == Key.S)
                {
                    action = "add_comment2";
                }
                if (e.Key == Key.A)
                {
                    action = "add_comment3";
                }
                if (e.Key == Key.D)
                {
                    action = "add_comment4";
                }
            }

            if (e.Key == Key.P)
            {
                if (actionState.message == null)
                {
                    action = "change_state";
                    actionState.setState(new StartMovingState(actionState));
                    current_Player.State = actionState.message;
                }
                else
                {
                    action = "change_state";
                    actionState.NextState();
                    current_Player.State = actionState.message;
                }
            }

            if (e.Key == Key.R)
            {
                action = "reset";
                orginator.RestoreMemento(caretaker.Memento);
                current_Player.X = orginator.X;
                current_Player.Y = orginator.Y;
                current_Player.Points = orginator.Points;
            }

            if (e.Key == Key.O)
            {
                action = "add_comment5";
            }
            if (e.Key == Key.I)
            {
                action = "add_comment6";
            }
            if (e.Key == Key.U)
            {
                invoker.undo(current_Player.Name);
            }


            if (e.Key == Key.U)
            {
                invoker.undo(current_Player.Name);
            }
            if (actionState.message == "Using inventory")
            {
                if (e.Key == GameSettings.GetInstance().Switch_right_key)
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
                else if (e.Key == GameSettings.GetInstance().Switch_left_key)
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
                else if (e.Key == GameSettings.GetInstance().Use_item_key)
                {
                    if (current_Player.currentItem >= 0)
                    {
                        action = "use_item";
                        info = current_Player.Inventory[current_Player.currentItem].Type;
                    }
                }
            }
            //if (e.Key == GameSettings.GetInstance().Switch_right_key)
            //{
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
            //else if (e.Key == GameSettings.GetInstance().Switch_left_key)
            //{
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
            //    invoker.InvokeUnSwitch();
            //    if (current_Player.currentItem >= 0)
            //    {
            //        bc = new BrushConverter();
            //        brush = (Brush)bc.ConvertFrom("#FF18E79D");
            //        brush.Freeze();
            //        inventory_items_gui[current_Player.currentItem].BorderBrush = brush;
            //    }
            //}
            if (actionState.message == "Picking items")
            {
                if (e.Key == GameSettings.GetInstance().Take_item_key)
                {
                    if (map1.map[current_Player.Y, current_Player.X].Loot != null)
                    {
                        invoker.SetCommand(new TakeDropCommand(action = "take"));
                        invoker.InvokeTake();
                    }

                }
                else if (e.Key == GameSettings.GetInstance().Drop_item_key)
                {
                    if (map1.map[current_Player.Y, current_Player.X].Loot == null)
                    {
                        if (current_Player.Inventory.Count > current_Player.currentItem)
                        {

                            invoker.SetCommand(new TakeDropCommand(action = "drop"));
                            invoker.InvokeDrop();
                            info = current_Player.Inventory[current_Player.currentItem].Type;
                        }
                    }
                    else if (map1.map[current_Player.Y, current_Player.X].Loot != null)
                    {
                        if (map1.map[current_Player.Y, current_Player.X].Loot.IsCrate() && current_Player.Inventory.Count > 0)
                        {
                            //map1.map[current_Player.Y, current_Player.X].Loot.Add(current_Player.Inventory[current_Player.currentItem]);
                            invoker.SetCommand(new TakeDropCommand(action = "drop"));
                            invoker.InvokeDrop();
                            info = current_Player.Inventory[current_Player.currentItem].Type;
                        }
                    }
                }
            }
            //else if (e.Key == GameSettings.GetInstance().Use_item_key)
            //{
            //    if (current_Player.currentItem >= 0)
            //    {
            //        action = "use_item";
            //        info = current_Player.Inventory[current_Player.currentItem].Type;
            //    }
            //}
            if (action != "")
            {
                Object[] args = new Object[7] { mapId, current_Player.Y, current_Player.X, current_Player.Points, action, info, current_Player.State };
                await connection.SendCoreAsync("Action", args);
            }
        }
        public async void Drop_trap(Player current_Player, HubConnection connection, Int32 mapId, KeyEventArgs e, ActionState actionState)
        {
            if (actionState.message == "Dropping traps")
            {
                if (e.Key == GameSettings.GetInstance().Put_bomb_key)
                {
                    //TODO: drop a trap
                    Object[] args = new Object[3] { mapId, current_Player.X, current_Player.Y };
                    await connection.SendCoreAsync("DropTrap", args);
                }
            }

        }

        public void Check_if_steped_on_trap(Map map1, Player current_Player, HubConnection connection, Int32 mapId, KeyEventArgs e)
        {
            if (e.Key == GameSettings.GetInstance().Move_up_key ||
                e.Key == GameSettings.GetInstance().Move_left_key ||
                e.Key == GameSettings.GetInstance().Move_down_key ||
                e.Key == GameSettings.GetInstance().Move_right_key)
            {
                Check_step_on_fire(map1, current_Player, connection, mapId);
                Check_step_on_trap(map1, current_Player, connection, mapId);
            }
        }
        public async void Check_step_on_trap(Map map1, Player current_Player, HubConnection connection, Int32 mapId)
        {
            if (map1.map[current_Player.Y, current_Player.X].Loot != null)
            {
                if (map1.map[current_Player.Y, current_Player.X].Loot.Type == new VisibleTrapAdapter(new VisibleTrap()).Type ||
                    map1.map[current_Player.Y, current_Player.X].Loot.Type == new InvisibleTrapAdapter(new InvisibleTrap()).Type)
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

        //public void DrawMap(Map map1, Dictionary<String, Shape> players_gui, Canvas canvas1)
        //{
        //    int pos_x = 0, pos_y = 0;
        //    canvas1.Children.Clear();
        //    canvas1.Height = Convert.ToInt32(map1.map_size) * 50 + 20;
        //    canvas1.Width = Convert.ToInt32(map1.map_size) * 50 + 20;
        //    for (int i = 0; i < Convert.ToInt32(map1.map_size); i++)
        //    {
        //        pos_x = 0;
        //        for (int j = 0; j < Convert.ToInt32(map1.map_size); j++)
        //        {
        //            Rectangle myRect = new System.Windows.Shapes.Rectangle();
        //            myRect.Stroke = System.Windows.Media.Brushes.Black;
        //            if (map1.map[i, j].Surface == Tile.Tile_type.grass)
        //            {
        //                myRect.Fill = System.Windows.Media.Brushes.LightGreen;
        //            }
        //            else if (map1.map[i, j].Surface == Tile.Tile_type.wall)
        //            {
        //                myRect.Fill = System.Windows.Media.Brushes.Gray;
        //            }
        //            else if (map1.map[i, j].Surface == Tile.Tile_type.water)
        //            {
        //                myRect.Fill = System.Windows.Media.Brushes.Blue;
        //            }
        //            else if (map1.map[i, j].Surface == Tile.Tile_type.lava)
        //            {
        //                myRect.Fill = System.Windows.Media.Brushes.OrangeRed;
        //            }
        //            else if (map1.map[i, j].Surface == Tile.Tile_type.bush)
        //            {
        //                myRect.Fill = System.Windows.Media.Brushes.DarkGreen;
        //            }
        //            myRect.Height = 50;
        //            myRect.Width = 50;
        //            canvas1.Children.Add(myRect);
        //            Canvas.SetTop(myRect, pos_y);
        //            Canvas.SetLeft(myRect, pos_x);
        //            Canvas.SetZIndex(myRect, 0);
        //            if(map1.map[i,j].Surface == Tile.Tile_type.bush)
        //            {
        //                Canvas.SetZIndex(myRect, 4);
        //            }
        //            if (map1.map[i, j].Loot != null)
        //            {
        //                map1.map[i, j].Icon = map1.map[i, j].Loot.get_view();
        //                canvas1.Children.Add(map1.map[i, j].Icon);
        //                Canvas.SetTop(map1.map[i, j].Icon, pos_y + 5);
        //                Canvas.SetLeft(map1.map[i, j].Icon, pos_x + 5);
        //                Canvas.SetZIndex(map1.map[i, j].Icon, 2);
        //            }
        //            pos_x += 50;
        //        }
        //        pos_y += 50;
        //    }
        //    foreach (var el in players_gui.Values)
        //    {
        //        canvas1.Children.Remove(el);
        //        canvas1.Children.Add(el);
        //        Canvas.SetZIndex(el, 3);
        //    }
        //}
        public void Create_new_room(HubConnection connection, TextBox players_count, RadioButton game_mode_easy, RadioButton map_type_1, TextBox map_size, RadioButton game_mode_hard, RadioButton map_type_2, MainWindow window)
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
            window.new_map_notification.Visibility = Visibility.Visible;
        }
        public void Update_players_objects(List<Player> players1, Dictionary<String, Shape> players_gui, Canvas canvas1, Player current_Player)
        {
            IEnumerable<string> names = from Player p in players1 select p.Name;
            Score ScoreTracking = new Score();
            IEnumerable<string> created = players_gui.Keys.ToList();
            if (current_Player.getObserverCount() == 0)
                current_Player.Attach(ScoreTracking);
            current_Player.Notify(names, created, players1, players_gui, canvas1, current_Player);
        }
    }
}
