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
                _worldListener = new TcpListener(IPAddress.Parse(Globals.SERVER_IP), Globals.WORLD_PORT);
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
            while (ListenWorldSocket)
            {
                Thread.Sleep(1);
                if (_worldListener.Pending())
                {
                    WorldManager world = new WorldManager();
                    world.Socket = _worldListener.AcceptSocket();

                    Thread recvThread = new Thread(world.Receive);
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
