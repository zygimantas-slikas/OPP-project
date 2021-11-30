using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Interpreter
{
    public class Context
    {
        public List<int> Data { get; set; }
        public Context()
        {
            Data = new List<int>();
        }
    }
    public class Node
    {
        public virtual string Interpret(Context con)
        {
            return "";
        }
    }
    public class DataNode : Node
    {
        public int Data { get; set; }
        public DataNode()
        { 
        }
        public DataNode (int d)
        {
            Data = d;
        }
        public override string Interpret(Context con)
        {
            con.Data.Add(Data);
            return "";
        }
    }
    public class ListCommandNode : Node
    {
        public override string Interpret(Context con)
        {
            var res = from map in Program.rooms
                      select String.Format("Id {0}, players {1}, size {2}, level {3}",
                map.Id, map.current_players, map.map_size, map.level);
            var res2 = res.ToArray();
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Maps list: \n");
            for (int i = 0; i < res2.GetLength(0); i++)
            {
                sb.AppendLine(res2[i]);
            }
            return sb.ToString();
        }
    }
    public class AddCommandNode: Node
    {
        public DataNode Players { get; set; }
        public DataNode Map {get;set;}
        public DataNode Level {get;set;}
        public DataNode Type { get; set; }
        public AddCommandNode(DataNode p, DataNode m, DataNode l, DataNode t)
        {
            Players = p;
            Map = m;
            Level = l;
            Type = t;
        }
        public override string Interpret(Context con)
        {
            Players.Interpret(con);
            Map.Interpret(con);
            Level.Interpret(con);
            Type.Interpret(con);
            Program.rooms.Add(new Room(Program.rooms.Count +1,con.Data[0],
                con.Data[1], con.Data[2], con.Data[3]));
            return String.Format("Map created: Id {0}, players {1}, size {2}, level {3}, type {4}",
                Program.rooms.Count + 1, con.Data[0], con.Data[1], con.Data[2], con.Data[3]);
        }
    }
    public class TestSetNode : Node
    {
        public Node Create1 { get; set; }
        public Node List1 { get; set; }
        public TestSetNode()
        {
            Create1 = new Node();
            List1 = new Node();
        }
        public override string Interpret(Context con)
        {
            Create1.Interpret(con);
            return List1.Interpret(con);
        }
    }
}
