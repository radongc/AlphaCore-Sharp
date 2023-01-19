using static AlphaCore_Sharp.Utils.Constants.ObjectCodes;

namespace AlphaCore_Sharp.Game.World.Managers.Objects
{
    // TODO: Major refactor of this class, this is solely to get enter world working.
    internal class UnitManager : ObjectManager
    {
        public int ChannelSpell { get; set; }
        public int ChannelObject { get; set; }
        public int Health { get; set; }
        public int Power1 { get; set; }
        public int Power2 { get; set; }
        public int Power3 { get; set; }
        public int Power4 { get; set; }
        public int MaxHealth { get; set; }
        public int MaxPower1 { get; set; }
        public int MaxPower2 { get; set; }
        public int MaxPower3 { get; set; }
        public int MaxPower4 { get; set; }
        public int Level { get; set; }
        public int Faction { get; set; }
        public int Bytes0 { get; set; }
        public int Stat0 { get; set; }
        public int Stat1 { get; set; }
        public int Stat2 { get; set; }
        public int Stat3 { get; set; }
        public int Stat4 { get; set; }
        public int BaseStat0 { get; set; }
        public int BaseStat1 { get; set; }
        public int BaseStat2 { get; set; }
        public int BaseStat3 { get; set; }
        public int BaseStat4 { get; set; }
        public int Flags { get; set; }
        public int Coinage { get; set; }
        public float CombatReach { get; set; }
        public int MountDisplayID { get; set; }
        public int ResistanceBuffModsPositive0 { get; set; }
        public int ResistanceBuffModsPositive1 { get; set; }
        public int ResistanceBuffModsPositive2 { get; set; }
        public int ResistanceBuffModsPositive3 { get; set; }
        public int ResistanceBuffModsPositive4 { get; set; }
        public int ResistanceBuffModsPositive5 { get; set; }
        public int ResistanceBuffModsPegative0 { get; set; }
        public int ResistanceBuffModsPegative1 { get; set; }
        public int ResistanceBuffModsPegative2 { get; set; }
        public int ResistanceBuffModsPegative3 { get; set; }
        public int ResistanceBuffModsPegative4 { get; set; }
        public int ResistanceBuffModsPegative5 { get; set; }
        public int BaseAttackTime { get; set; }
        public int OffhandAttackTime { get; set; }
        public int Resistance0 { get; set; }
        public int Resistance1 { get; set; }
        public int Resistance2 { get; set; }
        public int Resistance3 { get; set; }
        public int Resistance4 { get; set; }
        public int Resistance5 { get; set; }
        public int Standstate { get; set; }
        public int Bytes1 { get; set; }
        public int ModCastSpeed { get; set; }
        public int DynamicFlags { get; set; }
        public int Damage { get; set; }
        public int Bytes2 { get; set; }
        public int CombatTarget { get; set; }

        public UnitManager(int channelSpell = 0,
            int channelObject = 0,
            int health = 0,
            int power1 = 0,
            int power2 = 0,
            int power3 = 0,
            int power4 = 0,
            int maxHealth = 0,
            int maxPower1 = 0,
            int maxPower2 = 0,
            int maxPower3 = 0,
            int maxPower4 = 0,
            int level = 0,
            int faction = 0,
            int bytes0 = 0,
            int stat0 = 0,
            int stat1 = 0,
            int stat2 = 0,
            int stat3 = 0,
            int stat4 = 0,
            int baseStat0 = 0,
            int baseStat1 = 0,
            int baseStat2 = 0,
            int baseStat3 = 0,
            int baseStat4 = 0,
            int flags = 0,
            int coinage = 0,
            float combatReach = Globals.Unit.COMBAT_REACH,
            int mountDisplayID = 0,
            int resistanceBuffModsPositive0 = 0,
            int resistanceBuffModsPositive1 = 0,
            int resistanceBuffModsPositive2 = 0,
            int resistanceBuffModsPositive3 = 0,
            int resistanceBuffModsPositive4 = 0,
            int resistanceBuffModsPositive5 = 0,
            int resistanceBuffModsPegative0 = 0,
            int resistanceBuffModsPegative1 = 0,
            int resistanceBuffModsPegative2 = 0,
            int resistanceBuffModsPegative3 = 0,
            int resistanceBuffModsPegative4 = 0,
            int resistanceBuffModsPegative5 = 0,
            int baseAttackTime = Globals.Unit.BASE_ATTACK_TIME,
            int offhandAttackTime = Globals.Unit.OFFHAND_ATTACK_TIME,
            int resistance0 = 0,
            int resistance1 = 0,
            int resistance2 = 0,
            int resistance3 = 0,
            int resistance4 = 0,
            int resistance5 = 0,
            int standstate = 0,
            int bytes1 = 0,
            int modCastSpeed = 0,
            int dynamicFlags = 0,
            int damage = 0,
            int bytes2 = 0,
            int combatTarget = 0)
        {
            ChannelSpell = channelSpell;
            ChannelObject = channelObject;
            Health = health;
            Power1 = power1;
            Power2 = power2;
            Power3 = power3;
            Power4 = power4;
            MaxHealth = maxHealth;
            MaxPower1 = maxPower1;
            MaxPower2 = maxPower2;
            MaxPower3 = maxPower3;
            MaxPower4 = maxPower4;
            Level = level;
            Faction = faction;
            Bytes0 = bytes0;
            Stat0 = stat0;
            Stat1 = stat1;
            Stat2 = stat2;
            Stat3 = stat3;
            Stat4 = stat4;
            BaseStat0 = baseStat0;
            BaseStat1 = baseStat1;
            BaseStat2 = baseStat2;
            BaseStat3 = baseStat3;
            BaseStat4 = baseStat4;
            Flags = flags;
            Coinage = coinage;
            CombatReach = combatReach;
            MountDisplayID = mountDisplayID;
            ResistanceBuffModsPositive0 = resistanceBuffModsPositive0;
            ResistanceBuffModsPositive1 = resistanceBuffModsPositive1;
            ResistanceBuffModsPositive2 = resistanceBuffModsPositive2;
            ResistanceBuffModsPositive3 = resistanceBuffModsPositive3;
            ResistanceBuffModsPositive4 = resistanceBuffModsPositive4;
            ResistanceBuffModsPositive5 = resistanceBuffModsPositive5;
            ResistanceBuffModsPegative0 = resistanceBuffModsPegative0;
            ResistanceBuffModsPegative1 = resistanceBuffModsPegative1;
            ResistanceBuffModsPegative2 = resistanceBuffModsPegative2;
            ResistanceBuffModsPegative3 = resistanceBuffModsPegative3;
            ResistanceBuffModsPegative4 = resistanceBuffModsPegative4;
            ResistanceBuffModsPegative5 = resistanceBuffModsPegative5;
            BaseAttackTime = baseAttackTime;
            OffhandAttackTime = offhandAttackTime;
            Resistance0 = resistance0;
            Resistance1 = resistance1;
            Resistance2 = resistance2;
            Resistance3 = resistance3;
            Resistance4 = resistance4;
            Resistance5 = resistance5;
            Standstate = standstate;
            Bytes1 = bytes1;
            ModCastSpeed = modCastSpeed;
            DynamicFlags = dynamicFlags;
            Damage = damage;
            Bytes2 = bytes2;
            CombatTarget = combatTarget;

            ObjectType.Append(ObjectTypes.TYPE_UNIT);
        }
    }
}
