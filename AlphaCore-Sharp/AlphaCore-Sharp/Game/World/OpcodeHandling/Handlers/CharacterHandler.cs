using AlphaCore_Sharp.Database.Realm;
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
        // TODO: Rewrite this to actually fetch real gear data instead of sending mock data.
        public static bool HandleCharEnum(ref PacketReader packet, ref WorldManager worldSession)
        {
            bool result = false;

            var chars = RealmDatabaseManager.AccountGetCharacters(worldSession.Account.Id);

            PacketWriter charPacket = new PacketWriter(OpCode.SMSG_CHAR_ENUM);
            // Write number of characters found.
            charPacket += (byte)chars.Count;

            // Add character data for each character to packet.
            foreach (Character c in chars)
                GetCharacterPacket(worldSession, c, ref charPacket);

            // Send character packet to client and return success.
            worldSession.Send(charPacket);
            result = true;
            return result;
        }

        public static void GetCharacterPacket(WorldManager worldSession, Character character, ref PacketWriter characterPacket)
        {
            // GUID.
            characterPacket += (ulong)character.GUID;
            // Name.
            characterPacket += character.Name;

            // Race.
            characterPacket += (byte)character.Race;
            // Class.
            characterPacket += (byte)character.Class;
            // Gender.
            characterPacket += (byte)character.Gender;
            // Skin.
            characterPacket += (byte)character.Skin;
            // Face
            characterPacket += (byte)character.Face;
            // Hair style.
            characterPacket += (byte)character.HairStyle;
            // Hair color.
            characterPacket += (byte)character.HairColour;
            // Facial hair.
            characterPacket += (byte)character.FacialHair;
            // Level.
            characterPacket += (byte)character.Level;

            // Zone.
            characterPacket += (uint)character.Zone;
            // Map.
            characterPacket += (uint)character.Map;

            // Location: X
            characterPacket += (float)character.PositionX;
            // Location: Y
            characterPacket += (float)character.PositionY;
            // Location: Z
            characterPacket += (float)character.PositionZ;

            // Guild GUID.
            characterPacket += (uint)0;
            // Pet Display Info.
            characterPacket += (uint)0;
            // Pet level.
            characterPacket += (uint)0;
            // Pet family.
            characterPacket += (uint)0;

            // Send mock gear data.
            for (byte i = 0; i < 22; i++)
            {
                if (i == 0 || i == 2)
                    continue;

                // Gear display ID.
                characterPacket += (uint)1965;
                // Gear inventory type.
                characterPacket += (byte)i;
            }
        }
    }
}
