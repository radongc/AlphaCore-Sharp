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
    internal class WorldSocket
    {
        public bool ListenWorldSocket = true;
        private TcpListener _worldListener;

        public bool Start()
        {
            try
            {
                // Create new TcpListener which waits for socket connection attempts to world server.
                _worldListener = new TcpListener(IPAddress.Parse(Globals.Realm.SERVER_IP), Globals.Realm.WORLD_PORT);
                _worldListener.Start();

                return true;
            }
            catch (Exception ex)
            {
                Logger.Error($"{ex.Message}\n");

                return false;
            }
        }

        public void StartConnectionThread() => new Thread(AcceptConnection).Start();

        protected void AcceptConnection()
        {
            // Continuously accept world server socket connections.
            while (ListenWorldSocket)
            {
                // If there is a pending connection, create a world manager (game session) and accept it.
                Thread.Sleep(1);
                if (_worldListener.Pending())
                {
                    WorldManager world = new WorldManager();
                    world.Socket = _worldListener.AcceptSocket();

                    // Start a new thread and begin main world packet receiving loop.
                    Thread recvThread = new Thread(world.HandleSession);
                    recvThread.Start();
                }
            }
        }

        public void Dispose()
        {
            ListenWorldSocket= false;
            _worldListener.Stop();
        }
    }
}
