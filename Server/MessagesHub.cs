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
        public async Task SendMessage(string user, string message)
        {
            Console.WriteLine(user + ": " + message);
            await Clients.All.SendAsync("RecieveMessage", user, message);
        }

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
                maps[i] = String.Format("Map: {0}, playesr online: {1}, map size: {2}, state: {3}",
                    Program.rooms[i].Id, Program.rooms[i].current_players, Program.rooms[i].map_size, Program.rooms[i].state);
            }
            this.Clients.Caller.SendAsync("Show_maps_options", maps);
        }
        public async Task Create_map(Int32 players_count, Int32 map_size)
        {
            Room r1 = new Room(Program.rooms.Count + 1, players_count, map_size);
            Program.rooms.Add(r1);
            this.Connect();
        }
        public async Task Join_map(Int32 id)
        {
            int index = Program.rooms.FindIndex(x => x.Id == id);
            Program.rooms[index].Add_player(Context.ConnectionId.ToString());
            this.Groups.AddToGroupAsync(Context.ConnectionId, id.ToString());
            string json_map = Program.rooms[index].To_Json();
            this.Clients.Caller.SendAsync("Set_map", json_map);
        }
        // map id = hub group name
        public async Task Move()
        {

        }
    }
}
