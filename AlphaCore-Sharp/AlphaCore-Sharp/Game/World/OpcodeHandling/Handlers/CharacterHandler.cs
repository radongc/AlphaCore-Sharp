using AlphaCore_Sharp.Network.Packet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static AlphaCore_Sharp.Utils.Constants.OpCodes;

namespace AlphaCore_Sharp.Game.World.OpcodeHandling.Handlers
{
    internal class CharacterHandler
    {
        // TODO: Rewrite this to actually fetch real characters instead of sending mock data.
        public static bool HandleCharEnum(ref PacketReader packet, ref WorldManager worldSession)
        {
            bool result = false;

            PacketWriter charPacket = new PacketWriter(OpCode.SMSG_CHAR_ENUM);
            // Write number of characters found.
            charPacket += (byte)1;

            // GUID.
            charPacket += (ulong)1;
            // Name.
            charPacket += "Arthas";

            // Race.
            charPacket += (byte)1;
            // Class.
            charPacket += (byte)3;
            // Gender.
            charPacket += (byte)0;
            // Skin.
            charPacket += (byte)1;
            // Face
            charPacket += (byte)1;
            // Hair style.
            charPacket += (byte)3;
            // Hair color.
            charPacket += (byte)2;
            // Facial hair.
            charPacket += (byte)0;
            // Level.
            charPacket += (byte)23;

            // Zone.
            charPacket += (uint)37;
            // Map.
            charPacket += (uint)0;

            // Location: X
            charPacket += (float)509.15f;
            // Location: Y
            charPacket += (float)172.36f;
            // Location: Z
            charPacket += (float)42.90f;

            // Guild GUID.
            charPacket += (uint)0;
            // Pet Display Info.
            charPacket += (uint)0;
            // Pet level.
            charPacket += (uint)0;
            // Pet family.
            charPacket += (uint)0;

            // Send mock gear data.
            for (byte i = 0; i < 22; i++)
            {
                if (i == 1 || i == 2)
                    continue;

                // Gear display ID.
                charPacket += (uint)0;
                // Gear inventory type.
                charPacket += (byte)0;
            }

            // Send character packet to client and return success.
            worldSession.Send(charPacket);
            result = true;
            return result;
        }
    }
}
