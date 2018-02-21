using BeardedManStudios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterServerUDP
{
    class Program
    {
        static void Main(string[] args)
        {
            string host = "0.0.0.0";
            ushort port = 15942;
            string read = string.Empty;
            int eloRange = 0;

            Dictionary<string, string> arguments = ArgumentParser.Parse(args);

            if (args.Length > 0)
            {
                if (arguments.ContainsKey("host"))
                    host = arguments["host"];

                if (arguments.ContainsKey("port"))
                    ushort.TryParse(arguments["port"], out port);

                if (arguments.ContainsKey("elorange"))
                    int.TryParse(arguments["elorange"], out eloRange);
            }
            else
            {
                Console.WriteLine("Entering nothing will choose defaults.");
                Console.WriteLine("Enter Host IP (Default: 0.0.0.0):");
                read = Console.ReadLine();
                if (string.IsNullOrEmpty(read))
                    host = "0.0.0.0";
                else
                    host = read;

                Console.WriteLine("Enter Port (Default: 15942):");
                read = Console.ReadLine();
                if (string.IsNullOrEmpty(read))
                    port = 15942;
                else
                {
                    ushort.TryParse(read, out port);
                }
            }

            Console.WriteLine(string.Format("Hosting ip [{0}] on port [{1}]", host, port));
           
            MasterServerUDP server = new MasterServerUDP(host, port);

 
            while (true)
            {
                read = Console.ReadLine().ToLower();
                if (read == "s" || read == "stop")
                {
                    lock (server)
                    {
                        Console.WriteLine("Server stopped.");
                        server.Dispose();
                    }
                }
                else if (read == "c" || read == "connections")
                {
                    server.ListHosts();
                }
                else if (read == "r" || read == "restart")
                {
                    lock (server)
                    {
                        if (server.IsRunning)
                        {
                            Console.WriteLine("Server stopped.");
                            server.Dispose();
                        }
                    }

                    Console.WriteLine("Restarting...");
                    Console.WriteLine(string.Format("Hosting ip [{0}] on port [{1}]", host, port));
                    server = new MasterServerUDP(host, port);
                }
                else if (read == "q" || read == "quit")
                {
                    lock (server)
                    {
                        Console.WriteLine("Quitting...");
                        server.Dispose();
                    }
                    break;
                }
                else if (read == "h" || read == "help")
                    PrintHelp();

                else
                    Console.WriteLine("Command not recognized, please try again");
            }


        }

        private static void PrintHelp()
        {
            Console.WriteLine(@"Commands Available
(s)top - Stops hosting
(r)estart - Restarts the hosting service even when stopped
(q)uit - Quits the application
(c)onnections - Shows list of hosts
(h)elp - Get a full list of comands");
        }

    }
}
