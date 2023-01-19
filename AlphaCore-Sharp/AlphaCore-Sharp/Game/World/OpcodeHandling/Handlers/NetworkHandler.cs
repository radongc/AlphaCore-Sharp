using AlphaCore_Sharp.Network.Packet;
using static AlphaCore_Sharp.Utils.Constants.OpCodes;

namespace AlphaCore_Sharp.Game.World.OpcodeHandling.Handlers
{
    internal class NetworkHandler
    {
        public static bool HandlePing(ref PacketReader packet, ref WorldManager worldSession)
        {
            bool result = false;

            PacketWriter pongPacket = new PacketWriter(OpCode.SMSG_PONG);
            pongPacket += packet.ReadUInt32();

            result = true;
            worldSession.EnqueuePacket(pongPacket);

            return result;
        }
    }
}
