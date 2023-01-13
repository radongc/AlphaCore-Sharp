using AlphaCore_Sharp.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace AlphaCore_Sharp.Game.World
{
    internal class WorldManager
    {
        public static void Start()
        {
            IPAddress ipAddress = IPAddress.Parse(Globals.SERVER_IP);
            IPEndPoint localEndpoint = new IPEndPoint(ipAddress, Globals.WORLD_PORT);

            Socket serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            serverSocket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);

            serverSocket.Bind(localEndpoint);
            serverSocket.Listen();

            Logger.Success($"World server started, listening on {localEndpoint.Address}:{localEndpoint.Port}");
            Logger.Info("Info test");
            Logger.Anticheat("AC test");
            Logger.Warning("Warning test");
            Logger.Error("Error test");
            Logger.Debug("Debug test");

            while (true)
            {
                try
                {
                    Socket clientSocket = serverSocket.Accept();
                    // Session handling goes here
                }
                catch
                {
                    break;
                }
            }
        }
    }
}
