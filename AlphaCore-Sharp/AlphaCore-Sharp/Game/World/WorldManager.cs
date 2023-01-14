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
        // TODO: Store Account and Character data.
        public ulong Id;
        public Socket Socket;
        public static WorldSocket WorldSession;

        byte[] buffer = null;

        public void OnData()
        {
            PacketReader pkt = new PacketReader(buffer);

            if (Enum.IsDefined(typeof(OpCode), pkt.Opcode))
                Logger.Debug($"Received OpCode {pkt.Opcode}, Length: {pkt.Size}\n");
            else
                Logger.Debug($"Unknown data received: {pkt.Opcode}, Length: {pkt.Size}\n");

            PacketManager.Invoke(pkt, this, pkt.Opcode);
        }

        public void Receive()
        {
            // Send AUTH_CHALLENGE packet before anything else.
            PacketWriter packet = new PacketWriter(OpCode.SMSG_AUTH_CHALLENGE);
            packet += 0;
            packet += 0;
            packet += 0;
            packet += 0;
            packet += 0;
            packet += 0;
            this.Send(packet);

            // Wait for subsequent packets sent by the client.
            while(WorldSession.ListenWorldSocket)
            {
                Thread.Sleep(1);
                if (Socket.Connected && Socket.Available > 0)
                {
                    buffer = new byte[Socket.Available];
                    Socket.Receive(buffer, buffer.Length, SocketFlags.None);

                    OnData();
                }
            }

            CloseSocket();
        }

        public void Send(PacketWriter packet, bool suppressLog = false)
        {
            if (packet == null)
                return;

            byte[] buffer = packet.ReadDataToSend();

            try
            {
                Socket.BeginSend(buffer, 0, buffer.Length, SocketFlags.None, new AsyncCallback(FinishSend), Socket);

                if (!suppressLog)
                {
                    Logger.Debug($"Sending {packet.Opcode}\n");
                }
            }
            catch (Exception ex)
            {
                Logger.Error($"{ex.Message}\n");

                CloseSocket();
            }
        }

        public void CloseSocket()
        {
            // TODO: Log character out.

            CloseSocket();
        }

        public void FinishSend(IAsyncResult result)
        {
            Socket.EndSend(result);
        }
    }
}
