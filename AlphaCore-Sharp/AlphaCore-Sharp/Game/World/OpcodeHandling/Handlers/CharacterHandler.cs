using AlphaCore_Sharp.Database.Realm;
using AlphaCore_Sharp.Network.Packet;
using AlphaCore_Sharp.Utils;
using AlphaCore_Sharp.Game.World.Managers.Objects;
using static AlphaCore_Sharp.Utils.Constants.OpCodes;
using static AlphaCore_Sharp.Utils.Constants.ObjectCodes;

namespace AlphaCore_Sharp.Game.World.OpcodeHandling.Handlers
{
    internal class CharacterHandler
    {
        // TODO: Rewrite this to actually fetch real gear data instead of sending mock data.
        public static bool HandleCharEnum(ref PacketReader packet, ref WorldManager worldSession)
        {
            bool result = false;

            // Get character list for account.
            List<Character> chars = RealmDatabaseManager.AccountGetCharacters(worldSession.Account.Id);

            PacketWriter charPacket = new PacketWriter(OpCode.SMSG_CHAR_ENUM);
            // Write number of characters found.
            charPacket += (byte)chars.Count;

            // Add character data for each character to packet.
            foreach (Character c in chars)
                GetCharacterPacket(worldSession, c, ref charPacket);

            // Send character packet to client and return success.
            worldSession.EnqueuePacket(charPacket);
            result = true;
            return result;
        }

        // TODO: Finish and refactor this handler and add comments.
        public static bool HandlePlayerLogin(ref PacketReader packet, ref WorldManager worldSession)
        {
            // Get GUID.
            ulong guid = packet.ReadUInt64();
            Character character = RealmDatabaseManager.CharacterGetByGUID(guid);

            worldSession.Player = new PlayerManager(character);

            if (character == null)
            {
                Logger.Anticheat($"Character with wrong guid {guid} tried to login.");
                return false;
            }

            PacketWriter timePacket = new PacketWriter(OpCode.SMSG_LOGIN_SETTIMESPEED);

            timePacket += GetSecondsToTimeBitFields();
            timePacket += Globals.World.Gameplay.GAME_SPEED;

            worldSession.EnqueuePacket(timePacket);

            worldSession.EnqueuePacket(worldSession.Player.GetInitialSpells());
            worldSession.EnqueuePacket(worldSession.Player.GetQueryDetails());

            PacketWriter updatePacket = new PacketWriter(OpCode.SMSG_UPDATE_OBJECT);
            updatePacket += worldSession.Player.CreateUpdatePacket(UpdateTypes.UPDATE_IN_RANGE) +
                worldSession.Player.GetUpdatePacket();

            worldSession.EnqueuePacket(updatePacket);

            return true;
        }

        private static int GetSecondsToTimeBitFields()
        {
            DateTime localTime = DateTime.Now;

            int year = localTime.Year - 2000;
            int month = localTime.Month - 1;
            int day = localTime.Day - 1;
            int dayOfWeek = (int)localTime.DayOfWeek;
            int hour = localTime.Hour;
            int minute = localTime.Minute;

            return minute | (hour << 6) | (dayOfWeek << 11) | (day << 14) | (month << 20) | (year << 24);
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
