﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using System.Text.Json;

namespace Server
{
    class MessagesHub : Hub
    {

        public async Task Connect()
        {
            //Console.WriteLine("Connected");
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
                maps[i] = String.Format("Map: {0}, playesr online: {1}, map size: {2}, state: {3}",
                    Program.rooms[i].Id, Program.rooms[i].current_players, Program.rooms[i].map_size, Program.rooms[i].state);
            }
            await this.Clients.Caller.SendAsync("Show_maps_options", maps);
        }
        public async Task Create_map(Int32 players_count, Int32 map_size)
        {
            Room r1 = new Room(Program.rooms.Count + 1, players_count, map_size);
            Program.rooms.Add(r1);
            await this.Connect();
            //test
            //Console.WriteLine(JsonSerializer.Serialize(r1));
        }
        public async Task Join_map(Int32 id, string name)
        {
            // 1 check if map isn't full already
            int index = Program.rooms.FindIndex(x => x.Id == id);
            Program.rooms[index].Add_player(Context.ConnectionId.ToString(), name);
            Task t1 = this.Groups.AddToGroupAsync(Context.ConnectionId, id.ToString());

            string json_map = Program.rooms[index].To_Json();
            string json_players = Program.rooms[index].Players_to_Json();
            
            await this.Clients.Group(id.ToString()).SendAsync("Set_players", json_players);
            await Clients.All.SendAsync("Update_map_state", json_map);
            await this.Clients.Caller.SendAsync("Set_map", json_map);
            // 2 check if map is full already strat the game
        }

        // map id = hub group name
        public async Task Move()
        {

        }
    }
}
