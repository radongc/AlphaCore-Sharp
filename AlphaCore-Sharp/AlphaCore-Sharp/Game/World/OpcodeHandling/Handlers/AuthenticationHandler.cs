using AlphaCore_Sharp.Network.Packet;
using AlphaCore_Sharp.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static AlphaCore_Sharp.Utils.Constants.OpCodes;

namespace AlphaCore_Sharp.Game.World.OpcodeHandling.Handlers
{
    internal class AuthenticationHandler
    {
        // TODO: Implement account login (after ORM implemented with models and data loading.)
        public static void HandleAuthSession(ref PacketReader packet, ref WorldManager worldManager)
        {
            PacketWriter responsePacket = new PacketWriter(OpCode.SMSG_AUTH_RESPONSE);

            uint version = packet.ReadUInt32();
            packet.ReadUInt32();
            string[] nameAndPass = packet.ReadString().Split(new string[] { "\r\n", "\r", "\n" }, 
                                                             StringSplitOptions.RemoveEmptyEntries);

            if (nameAndPass.Length != 2)
                responsePacket += (byte)AuthCodes.AUTH_UNKNOWN_ACCOUNT;
        }
    }
}
