using AlphaCore_Sharp.Database.Realm;
using AlphaCore_Sharp.Game.World.OpcodeHandling;
using AlphaCore_Sharp.Network.Packet;
using AlphaCore_Sharp.Utils;
using System.Collections.Concurrent;
using System.Net.Sockets;
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

        private BlockingCollection<PacketWriter> _outgoingPending;
        private BlockingCollection<PacketReader> _incomingPending;

        private bool _keepAlive = false;

        byte[] buffer = null;

        public void HandleSession()
        {
            try
            {
                // Packet processing and other loops execute while _keepAlive is true.
                _keepAlive = true;

                // Perform initial auth exchange.
                if (AuthChallenge())
                {
                    Socket.ReceiveTimeout = 0;

                    // Initialize packet queues.
                    _outgoingPending = new BlockingCollection<PacketWriter>();
                    _incomingPending = new BlockingCollection<PacketReader>();

                    // Start processing outgoing packets.
                    new Thread(ProcessOutgoing).Start();
                    // Start processing incoming packets.
                    new Thread(ProcessIncoming).Start();

                    // Wait for subsequent packets sent by the client.
                    while (Receive() && _keepAlive)
                        continue;
                }
            }
            finally
            {
                CloseSocket();
            }
        }

        public bool Receive()
        {
            Thread.Sleep(1);
            if (Socket.Connected && Socket.Available > 0)
            {
                // Receive data from client.
                buffer = new byte[Socket.Available];
                Socket.Receive(buffer, buffer.Length, SocketFlags.None);

                // Serialize packet bytes.
                PacketReader pkt = new PacketReader(buffer);

                // Check if we have defined this packet.
                if (Enum.IsDefined(typeof(OpCode), pkt.Opcode))
                    Logger.Debug($"Received OpCode {pkt.Opcode}, Length: {pkt.Size}");
                else
                    Logger.Debug($"Unknown data received: {pkt.Opcode}, Length: {pkt.Size}");

                // Add packet to incoming queue.
                if (pkt != null)
                    _incomingPending.Add(pkt);
                else
                    return false;
            }

            return true;
        }

        public void EnqueuePacket(PacketWriter packet)
        {
            // Add packet to outgoing queue.
            if (_keepAlive)
                _outgoingPending.Add(packet);
        }

        public void ProcessOutgoing()
        {
            while(_keepAlive)
            {
                try
                {
                    // Get next packet from queue and send.
                    PacketWriter packet = _outgoingPending.Take();

                    if (packet != null)
                    {
                        Send(packet);
                    }
                }
                catch (Exception)
                {
                    // Disconnect client if exception occurs.
                    CloseSocket();
                }
            }
        }

        public void ProcessIncoming()
        {
            try
            {
                while (_keepAlive)
                {
                    // Get next incoming packet from queue.
                    PacketReader packet = _incomingPending.Take();

                    if (packet != null && _keepAlive)
                    {
                        if (packet.Opcode != null)
                        {
                            // Invoke the handler for this packet.
                            bool handlerResult = PacketManager.Handle(packet, this, packet.Opcode);
                            
                            // Disconnect if handler returns bad result.
                            if (!handlerResult)
                                break;
                        }
                    }
                }

                CloseSocket();
            }
            catch(Exception e)
            {
                Logger.Error(e.Message);
                CloseSocket();
            }
        }

        // Send packet directly.
        public void Send(PacketWriter packet)
        {
            if (packet == null)
                return;

            // Get raw packet bytes.
            byte[] buffer = packet.ReadDataToSend();

            try
            {
                // Try to send the raw packet bytes.
                Socket.Send(buffer, 0, buffer.Length, SocketFlags.None);

                Logger.Debug($"Sending {packet.Opcode}");
            }
            catch (Exception ex)
            {
                Logger.Error($"{ex.Message}\n");

                // Close world socket if error caught.
                CloseSocket();
            }
        }

        public bool AuthChallenge()
        {
            try
            {
                // Send AUTH_CHALLENGE packet before anything else.
                PacketWriter challengePkt = new PacketWriter(OpCode.SMSG_AUTH_CHALLENGE);
                challengePkt += (byte)0;
                challengePkt += (byte)0;
                challengePkt += (byte)0;
                challengePkt += (byte)0;
                challengePkt += (byte)0;
                challengePkt += (byte)0;
                Send(challengePkt);

                bool keepAliveAuth = true;
                
                // Start temporary receive loop before authentication is complete, end session if any other packets are received.
                while (keepAliveAuth)
                {
                    if (Socket.Connected && Socket.Available > 0)
                    {
                        Socket.ReceiveTimeout = 10000;
                        buffer = new byte[Socket.Available];
                        Socket.Receive(buffer, buffer.Length, SocketFlags.None);

                        PacketReader pkt = new PacketReader(buffer);

                        if (pkt.Opcode == OpCode.CMSG_AUTH_SESSION)
                        {
                            keepAliveAuth = false;

                            bool handlerResult = PacketManager.Handle(pkt, this, pkt.Opcode);

                            return handlerResult;
                        }
                    }
                }
            }
            catch(Exception e) 
            {
                CloseSocket();
                return false;
            }

            return false;
        }

        public void CloseSocket()
        {
            // TODO: Log character out.
            Socket.Close();
            _keepAlive = false;
        }
    }
}
