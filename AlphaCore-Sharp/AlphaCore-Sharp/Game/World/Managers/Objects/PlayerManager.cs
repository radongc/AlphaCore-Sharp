using AlphaCore_Sharp.Database.Realm;
using AlphaCore_Sharp.Network.Packet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static AlphaCore_Sharp.Utils.Constants.ObjectCodes;

namespace AlphaCore_Sharp.Game.World.Managers.Objects
{
    // TODO: Refactor this class, this is just to get world enter working.
    internal class PlayerManager : UnitManager
    {
        public Character Player { get; set; }
        public int NumInvSlots { get; set; }
        public int PlayerBytes { get; set; }
        public int XP { get; set; }
        public int NextLevelXP { get; set; }
        public int PlayerBytes2 { get; set; }
        public int TalentPoints { get; set; }
        public int SkillPoints { get; set; }
        public float BlockPercentage { get; set; }
        public float DodgePercentage { get; set; }
        public float ParryPercentage { get; set; }
        public int BaseMana { get; set; }
        public byte SheathState { get; set; }
        public byte ComboPoints { get; set; }

        public PlayerManager(Character player = null, int numInvSlots = 0x89, int playerBytes = 0, int xp = 0, int nextLevelXP = 0, int playerBytes2 = 0, int talentPoints = 0 , int skillPoints = 0, float blockPercentage = 0, float dodgePercentage = 0, float parryPercentage = 0, int baseMana = 0, byte sheathState = 0, byte comboPoints = 0)
        {
            Player = player;
            NumInvSlots = numInvSlots;
            PlayerBytes = playerBytes;
            XP = xp;
            NextLevelXP = nextLevelXP;
            PlayerBytes2 = playerBytes2;
            TalentPoints = talentPoints;
            SkillPoints = skillPoints;
            BlockPercentage = blockPercentage;
            DodgePercentage = dodgePercentage;
            ParryPercentage = parryPercentage;
            BaseMana = baseMana;
            SheathState = sheathState;
            ComboPoints = comboPoints;

            if (Player != null)
            {
                GUID = Player.GUID;
                Level = Player.Level;
                ObjectType.Append(ObjectTypes.TYPE_PLAYER);

                PacketWriter byteWriter1 = new PacketWriter();
                byteWriter1 += (byte)Player.Race;
                byteWriter1 += (byte)Player.Class;
                byteWriter1 += (byte)Player.Gender;
                byteWriter1 += (byte)1; // Power type

                PacketWriter byteWriter2 = new PacketWriter();
                byteWriter2 += (byte)Standstate;
                byteWriter2 += (byte)0;
                byteWriter2 += (byte)ShapeshiftForm;
                byteWriter2 += (byte)sheathState;

                PacketWriter byteWriter3 = new PacketWriter();
                byteWriter3 += (byte)comboPoints;
                byteWriter3 += (byte)0;
                byteWriter3 += (byte)0;
                byteWriter3 += (byte)0;

                PacketWriter playerByteWriter1 = new PacketWriter();
                playerByteWriter1 += (byte)Player.Skin;
                playerByteWriter1 += (byte)Player.Face;
                playerByteWriter1 += (byte)Player.HairStyle;
                playerByteWriter1 += (byte)Player.HairColour;

                PacketWriter playerByteWriter2 = new PacketWriter();
                playerByteWriter2 += (byte)Player.ExtraFlags;
                playerByteWriter2 += (byte)Player.BankSlots;
                playerByteWriter2 += (byte)Player.FacialHair;
                playerByteWriter2 += (byte)0;

                PacketReader byteReader1 = new PacketReader(byteWriter1.ReadDataToSend());
                PacketReader byteReader2 = new PacketReader(byteWriter2.ReadDataToSend());
                PacketReader byteReader3 = new PacketReader(byteWriter3.ReadDataToSend());

                PacketReader playerByteReader1 = new PacketReader(playerByteWriter1.ReadDataToSend());
                PacketReader playerByteReader2 = new PacketReader(playerByteWriter2.ReadDataToSend());

                Bytes0 = byteReader1.ReadInt32();
                Bytes1 = byteReader2.ReadInt32();
                Bytes2 = byteReader3.ReadInt32();

                PlayerBytes = playerByteReader1.ReadInt32();
                PlayerBytes2 = playerByteReader2.ReadInt32();

                Map = Player.Map;
                Zone = Player.Zone;
                Location.X = Player.PositionX;
                Location.Y = Player.PositionY;
                Location.Z = Player.PositionZ;
                Orientation = Player.Orientation;

                // Testing
                Health = 1;
                MaxHealth = 1;
                DisplayID = 278;
                MovementFlags = 0x08000000;
            }
        }

        // TODO: GetInitialSpells(), GetQueryDetails(), GetUpdatePacket() and UpdatePacketFactory.
        // After we get enter world working, refactor this class in line with latest alpha-core changes.
    }
}
