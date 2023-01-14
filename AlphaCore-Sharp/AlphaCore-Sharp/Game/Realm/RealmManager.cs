using AlphaCore_Sharp.Network.Packet;
using AlphaCore_Sharp.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace AlphaCore_Sharp.Game.Realm
{
    // RealmManager
    // Based on Alpha-WoW's RealmManager class.
    internal class RealmManager
    {
        public static RealmSocket RealmSession;
        public Socket RealmSocket;
        public Socket ProxySocket;

        public void HandleProxyConnection(RealmManager session)
        {
            Logger.Info("Begin redirection to World Server.");

            PacketWriter proxyWriter = new PacketWriter();
            proxyWriter += $"{Globals.SERVER_IP}:{Globals.WORLD_PORT}";

            session.Send(proxyWriter, ProxySocket);
            ProxySocket.Close();

            Logger.Info("Successfully redirected to World Server.\n");
        }

        public void HandleRealmList(RealmManager session)
        {
            PacketWriter realmWriter = new PacketWriter();
            realmWriter += (byte)1;
            realmWriter += "|cFF00FFFFAlphaCore-Sharp Development";
            realmWriter += $"{Globals.SERVER_IP}:{Globals.PROXY_PORT}";
            realmWriter += (uint)100; // TODO: Number of online players.

            session.Send(realmWriter, RealmSocket);
            RealmSocket.Close();
        }

        public void ReceiveRealm()
        {
            HandleRealmList(this);
        }

        public void ReceiveProxy()
        {
            HandleProxyConnection(this);
        }

        public void Send(PacketWriter writer, Socket socket)
        {
            byte[] buffer = writer.ReadDataToSend(isAuthPacket: true);

            try
            {
                socket.Send(buffer, 0, buffer.Length, SocketFlags.None);
            }
            catch (Exception ex)
            {
                Logger.Error($"{ex.Message}\n");
                socket.Close();
            }
        }
    }
}
