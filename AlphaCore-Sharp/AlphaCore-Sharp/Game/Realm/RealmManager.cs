using AlphaCore_Sharp.Database.Realm;
using AlphaCore_Sharp.Network.Packet;
using AlphaCore_Sharp.Utils;
using System.Net.Sockets;

namespace AlphaCore_Sharp.Game.Realm 
{
    // RealmManager
    // Based on Alpha-WoW's RealmManager class.

    internal class RealmManager
    {
        public static RealmSocket RealmSocketSession;
        public Socket RealmSocket;
        public Socket ProxySocket;

        public void HandleProxyConnection(RealmManager session)
        {
            Logger.Info("Redirecting to World Server from Proxy Server...");

            // Redirect client to world server by sending a packet with just the world server address on realm -> world proxy socket.
            PacketWriter proxyWriter = new PacketWriter();
            proxyWriter += $"{Globals.Realm.SERVER_IP}:{Globals.Realm.WORLD_PORT}";

            // Send the packet and close the proxy connection.
            session.Send(proxyWriter, ProxySocket);
            ProxySocket.Close();
        }

        public void HandleRealmList(RealmManager session)
        {
            // Create a new bare packet containing the realm ID (?), realm name, address, and population.
            PacketWriter realmPacket = new PacketWriter();
            realmPacket += (byte)1; // Realm ID? Seems that it must be '1' always.
            realmPacket += $"{Globals.Realm.REALM_NAME}"; // Realm name.
            realmPacket += $"{Globals.Realm.SERVER_IP}:{Globals.Realm.PROXY_PORT}"; // Realm IP/Port.
            realmPacket += (uint)RealmDatabaseManager.CharacterGetOnlineCount();

            // Send this packet on the realmlist socket.
            session.Send(realmPacket, RealmSocket);

            // Close realmlist connection as it is no longer needed.
            RealmSocket.Close();
        }

        public void ReceiveRealm()
        {
            // Handle realmlist packets.
            HandleRealmList(this);
        }

        public void ReceiveProxy()
        {
            // Handle realm -> world proxy connection.
            HandleProxyConnection(this);
        }

        public void Send(PacketWriter writer, Socket socket)
        {
            // Get raw auth packet data and send it on socket.
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
