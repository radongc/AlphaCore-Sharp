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

    // TODO: Add comments explaining this class.
    internal class RealmManager
    {
        public static RealmSocket RealmSession;
        public Socket RealmSocket;
        public Socket ProxySocket;

        public void HandleProxyConnection(RealmManager session)
        {
            Logger.Info("Redirecting to World Server from Proxy Server...");

            PacketWriter proxyWriter = new PacketWriter();
            proxyWriter += $"{Globals.Realm.SERVER_IP}:{Globals.Realm.WORLD_PORT}";

            session.Send(proxyWriter, ProxySocket);
            ProxySocket.Close();
        }

        public void HandleRealmList(RealmManager session)
        {
            PacketWriter realmWriter = new PacketWriter();
            realmWriter += (byte)1;
            realmWriter += $"{Globals.Realm.REALM_NAME}";
            realmWriter += $"{Globals.Realm.SERVER_IP}:{Globals.Realm.PROXY_PORT}";
            realmWriter += (uint)4916; // TODO: Number of online players.

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
