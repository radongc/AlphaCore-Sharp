using AlphaCore_Sharp.Database.Realm;
using AlphaCore_Sharp.Game.World.OpcodeHandling;
using AlphaCore_Sharp.Network.Packet;
using AlphaCore_Sharp.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using static AlphaCore_Sharp.Utils.Constants.OpCodes;

namespace AlphaCore_Sharp.Game.World
{
    internal class WorldManager
    {
        // TODO: Store Character data.
        public ulong Id;
        public Account Account;
        public Socket Socket;
        public static WorldSocket WorldSocketSession;

        byte[] buffer = null;

        public bool OnPacketReceived()
        {
            // Serialize packet bytes.
            PacketReader pkt = new PacketReader(buffer);

            // Check if we have defined this packet.
            if (Enum.IsDefined(typeof(OpCode), pkt.Opcode))
                Logger.Debug($"Received OpCode {pkt.Opcode}, Length: {pkt.Size}");
            else
                Logger.Debug($"Unknown data received: {pkt.Opcode}, Length: {pkt.Size}");

            // Invoke the handler for this packet.
            bool handlerResult = PacketManager.Invoke(pkt, this, pkt.Opcode);

            // Return result to Receive() loop.
            return handlerResult;
        }

        public void Receive()
        {
            // Send AUTH_CHALLENGE packet before anything else. SMSG_AUTH_CHALLENGE contains 6 zeros.
            PacketWriter challengePkt = new PacketWriter(OpCode.SMSG_AUTH_CHALLENGE);
            challengePkt += 0;
            challengePkt += 0;
            challengePkt += 0;
            challengePkt += 0;
            challengePkt += 0;
            challengePkt += 0;
            Send(challengePkt);

            // Wait for subsequent packets sent by the client.
            while(WorldSocketSession.ListenWorldSocket)
            {
                Thread.Sleep(1);
                if (Socket.Connected && Socket.Available > 0)
                {
                    buffer = new byte[Socket.Available];
                    Socket.Receive(buffer, buffer.Length, SocketFlags.None);

                    // When packet received, handle packets via OnPacketReceived. Break receive loop if handler returns bad result.
                    if (!OnPacketReceived())
                        break;
                }
            }

            // Close socket connection if packet loop breaks.
            CloseSocket();
        }

        public void Send(PacketWriter packet, bool suppressLog = false)
        {
            if (packet == null)
                return;

            // Get raw packet bytes.
            byte[] buffer = packet.ReadDataToSend();

            try
            {
                // Try to asynchronously send the raw packet bytes.
                Socket.BeginSend(buffer, 0, buffer.Length, SocketFlags.None, new AsyncCallback(FinishSend), Socket);

                if (!suppressLog)
                {
                    Logger.Debug($"Sending {packet.Opcode}");
                }
            }
            catch (Exception ex)
            {
                Logger.Error($"{ex.Message}\n");

                // Close world socket if error caught.
                CloseSocket();
            }
        }

        public void CloseSocket()
        {
            // TODO: Log character out.

            Socket.Close();
        }

        public void FinishSend(IAsyncResult result)
        {
            // Finish asynchronous packet send on socket.
            Socket.EndSend(result);
        }
    }
}
