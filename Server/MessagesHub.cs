using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using System.Text.Json;
using Server.AbstractFactory;
using Server.Builder;
using Client.Composite;
using Server.Chain_of_responsability;

namespace Server
{
    public class MessagesHub : Hub
    {
        public async Task Connect()
        {
            string[] maps;
            if (Program.rooms.Count > 0)
            {
                maps = new string[Program.rooms.Count];
            }
            else
            {
                maps = new string[1];
                maps[0] = "No maps created on this server.";
            }
            for (int i = 0; i <Program.rooms.Count; i++)
            {
                maps[i] = String.Format("Map: {0}, playesr online: {1}, map size: {2}, level: {3}, state: {4}",
                    Program.rooms[i].Id, Program.rooms[i].current_players, Program.rooms[i].map_size,
                    Program.rooms[i].level, Program.rooms[i].state);
            }
            await this.Clients.Caller.SendAsync("Show_maps_options", maps);
        }
        public async Task Create_map(Int32 players_count, Int32 map_size, Int32 level, int type)
        {
            Room r1 = new Room(Program.rooms.Count + 1, players_count, map_size, level, type);
            Program.rooms.Add(r1);
            await this.Connect();
        }
        public async Task Join_map(Int32 id, string name)
        {
            var logger = new LoggerToGameWindow();
            int index = Program.rooms.FindIndex(x => x.Id == id);
            if (Program.rooms[index].current_players < Program.rooms[index].max_players &&
                Program.rooms[index].players.Find(x => x.Name == name) == null)
            {
                Program.rooms[index].Add_player(Context.ConnectionId.ToString(), name);
            }
            else
            {
                return;
                //TODO: notify client about error
            }
            await Groups.AddToGroupAsync(Context.ConnectionId, id.ToString());
            string json_map = Program.rooms[index].To_Json();
            string json_players = Program.rooms[index].Players_to_Json();
            await this.Clients.Group(id.ToString()).SendAsync("Set_players", json_players);
            await this.Clients.Caller.SendAsync("Set_map", json_map);
            await Clients.Group(id.ToString()).SendAsync("Update_map_state", json_map);
        }
        public override async Task OnDisconnectedAsync(Exception exception)
        {
            int index = -1;
            for (int i = 0; i < Program.rooms.Count; i++)
            {
                for (int j = 0; j < Program.rooms[i].current_players; j++)
                {
                    if (Program.rooms[i].players[j].Con_id == Context.ConnectionId)
                    {
                        index = i;
                    }
                }
            }
            if (index != -1)
            {
                await Groups.RemoveFromGroupAsync(Context.ConnectionId, Program.rooms[index].Id.ToString());
                Program.rooms[index].Remove_player(Context.ConnectionId);
                string json_players = Program.rooms[index].Players_to_Json();
                await this.Clients.Group(Program.rooms[index].Id.ToString()).SendAsync("Set_players", json_players);
            }
            await base.OnDisconnectedAsync(exception);
        }
        // map id = hub group name
        public async Task Move(Int32 map_id, Int32 x, Int32 y)
        {
            Room r = Program.rooms.Find(x => x.Id == map_id);
            Player p = r.players.Find(x => x.Con_id == Context.ConnectionId);
            if ((Math.Abs(p.X - x) + Math.Abs(p.Y - y) ) < 2)
            {
                r.map[p.Y, p.X].Player_Standing = null;
                p.X = x;
                p.Y = y;
                r.map[p.Y, p.X].Player_Standing = p.Name;
                string json_players = r.Players_to_Json();
                await this.Clients.Group(map_id.ToString()).SendAsync("Set_players", json_players);
            }
        }
        public async Task Action(Int32 map_id, Int32 y, Int32 x, Int32 points, string action, string info, string state)
        {
            Room r = Program.rooms.Find(x => x.Id == map_id);
            Player p = r.players.Find(x => x.Con_id == Context.ConnectionId);
            string map_change = "";
            var logger = new LoggerToGameWindow();
            var loggerChat = new LoggerToGameWindowChat();
            if (action == "take")
            {
                logger.Log("Took an item!", "Action", p);
                if (r.map[y, x].Loot is not Crate)
                {
                    map_change = "remove;";
                    r.map[y, x].Loot.PickupEffect(p);
                }
                if (r.map[y, x].Loot is not Berry && r.map[y, x].Loot is not Crate)
                {
                    p.Inventory.Add(r.map[y, x].Loot);
                    r.map[y, x].Loot = null;
                }
                else if(r.map[y, x].Loot is Crate)
                {
                    bool crateOfItems = false;
                    Item loot = r.map[y, x].Loot.Remove(new BlueGun(), p, out crateOfItems);
                    if (loot != null)
                    {
                        if (crateOfItems)
                        {
                            p.Inventory.Add(r.map[y, x].Loot);
                            map_change = "remove;";
                        }
                        else
                        {
                            p.Inventory.Add(loot);
                        }
                        
                    }
                }
            }
            else if (action == "drop")
            {
                logger.Log("Dropped an item!", "Action", p);
                Item it1 = p.Inventory.Find(x => x.Type == info);
                if (it1 != null)
                {
                    if (r.map[y, x].Loot is Crate)
                    {
                        if(r.map[y, x].Loot.Add(it1, p))
                            p.Inventory.Remove(it1);
                    }
                    else
                    {
                        if (it1 is not Crate)
                        {
                            map_change = "create;" + it1.Type;
                            r.map[y, x].Loot = it1;
                            p.Inventory.Remove(it1);
                        }
                    }
                }
            }
            else if (action == "use_item")
            {
            }
            else if (action == "add_comment")
            {
                logger.Log("Moved up!", "Movement", p);
            }
            else if (action == "add_comment2")
            {
                logger.Log("Moved down!", "Movement", p);
            }
            else if (action == "add_comment3")
            {
                logger.Log("Moved left!", "Movement", p);
            }
            else if (action == "add_comment4")
            {
                logger.Log("Moved right!", "Movement", p);
            }
            else if (action == "add_comment5")
            {
                loggerChat.Log("Hello!", "Chat", p);
            }
            else if (action == "add_comment6")
            {
                loggerChat.Log("I am going to get you!", "Taunt", p);
            }
            else if (action == "change_state")
            {
                p.State = state;
            }
            else if (action == "reset")
            {
                p.X = x;
                p.Y = y;
                p.Points = points;
            }
            string json_players = r.Players_to_Json();
            await this.Clients.Group(map_id.ToString()).SendAsync("Update_map_state", y, x, map_change);
            await this.Clients.Group(map_id.ToString()).SendAsync("Set_players", json_players);
        }
        //=============================================================
        public async Task DropTrap(Int32 map_id, Int32 x, Int32 y)
        {
            Room r = Program.rooms.Find(x => x.Id == map_id);
            Player p = r.players.Find(x => x.Con_id == Context.ConnectionId);
            ItemsFactory items_factory;
            if ((Math.Abs(p.X - x) + Math.Abs(p.Y - y)) < 2)
            {
                if (r.level == 1)
                {
                    items_factory = new Level1Factory();
                }
                else
                {
                    items_factory = new Level2Factory();
                }
                r.map[p.Y, p.X].Loot = items_factory.Create_trap();
                string json_map = r.To_Json();
                await this.Clients.Group(map_id.ToString()).SendAsync("Set_map", json_map);
            }
        }

        public async Task ActivateTrap(Int32 map_id, Int32 x, Int32 y)
        {
            Room r = Program.rooms.Find(x => x.Id == map_id);
            Player p = r.players.Find(x => x.Con_id == Context.ConnectionId);
            if ((Math.Abs(p.X - x) + Math.Abs(p.Y - y)) < 2)
            {
                if ((r.map[p.Y, p.X].Loot as Trap).Activated)
                {
                    r.map[p.Y, p.X].Loot = new Fire();
                }
                else
                {
                    r.map[p.Y, p.X].Loot.PickupEffect(p); //activate trap
                }
                string json_map = r.To_Json();
                await this.Clients.Group(map_id.ToString()).SendAsync("Set_map", json_map);
            }
        }

        public async Task StepOnFire(Int32 map_id, Int32 x, Int32 y, Int32 stepsCount)
        {
            Room r = Program.rooms.Find(x => x.Id == map_id);
            Player p = r.players.Find(x => x.Con_id == Context.ConnectionId);
            if ((Math.Abs(p.X - x) + Math.Abs(p.Y - y)) < 2)
            {
                Fire f = new Fire();
                stepsCount++;
                for (int i = 0; i < stepsCount; i++)
                {
                    f.PickupEffect(p);
                }
                r.map[p.Y, p.X].Loot = f;
                string json_map = r.To_Json();
                await this.Clients.Group(map_id.ToString()).SendAsync("Set_map", json_map);
            }
        }
    }
}
