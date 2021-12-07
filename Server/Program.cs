using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading;
using Server.Interpreter;
using System.Text.RegularExpressions;
using Server.Chain_of_responsability;

namespace Server
{
    static class Program
    {
        public static List<Room> rooms;
        public static string ip;
        static void Main(string[] args)
        {
            var logger = new LoggerToConsole();
            var loggerToFile = new LoggerToFile();
            Player p = new Player();
            logger.Log("Server is starting", "Info", p);
            loggerToFile.Log("Server is starting", "Info", p);
            rooms = new List<Room>();
            if (args.GetLength(0) >= 1)
            {
                ip = args[0];
            }
            else
            {
                Console.Write("Type in your ip address: ");
                ip = Console.ReadLine();
            }
            ip = "http://" + ip + ":5000";
            Thread th1 = new Thread(new ThreadStart(()=>CreateHostBuilder(args).Build().Run()));
            th1.Start();

            Node root_node;
            while (true)
            {
                root_node = parse_to_tree(Console.ReadLine());
                if (root_node != null)
                {
                    Console.WriteLine(root_node.Interpret(new Context()));
                }
            }
        }
        public static Node parse_to_tree (string command)
        {
            Node result;
            MatchCollection matches = Regex.Matches(command, "([\\w]+)|(-[\\w]+ [\\d]+)");
            switch (matches[0].ToString().ToLower())
            {
                case "list":
                    result = new ListCommandNode();
                    break;
                case "add":
                    if (matches.Count == 5)
                    {
                        DataNode p1 = new DataNode();
                        DataNode p2 = new DataNode();
                        DataNode p3 = new DataNode();
                        DataNode p4 = new DataNode();
                        for (int i = 1; i< 5; i++)
                        {
                            string[] parts = matches[i].ToString().Split();
                            if (parts[0] == "-pc") //players count
                            {
                                p1.Data = Int32.Parse(parts[1].Trim());
                            }
                            else if (parts[0] == "-ms") //map size
                            {
                                p2.Data = Int32.Parse(parts[1].Trim());
                            }
                            else if (parts[0] == "-l") // level
                            {
                                p3.Data = Int32.Parse(parts[1].Trim());
                            }
                            else if (parts[0] == "-t") //type
                            {
                                p4.Data = Int32.Parse(parts[1].Trim());
                            }
                        }
                        result = new AddCommandNode(p1, p2, p3, p4);
                    }
                    else
                    {
                        result = null;
                    }
                    break;
                case "testset":
                    result = new TestSetNode();
                    (result as TestSetNode).List1 = new ListCommandNode();
                    (result as TestSetNode).Create1 = new AddCommandNode(
                        new DataNode(4), new DataNode(200), new DataNode(2), new DataNode(1));
                    break;
                default:
                    result = null;
                    break;
            }
            return result;
        }
        public static IWebHostBuilder CreateHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args).UseUrls(ip).UseStartup<Startup>();
    }
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSignalR();
        }
        public void Configure(IApplicationBuilder app)
        {
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<MessagesHub>("/messageHub");
            });
        }
    }
}
