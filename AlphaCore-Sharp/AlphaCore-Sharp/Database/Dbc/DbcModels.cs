using AlphaCore_Sharp.Game;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlphaCore_Sharp.Database.Dbc
{
    internal class DbcModels : DbContext
    {
        public DbSet<AreaTable> AreaTables { get; set; }
        public DbSet<SkillLine> SkillLines { get; set; }

        // Set up MySQL connection to Dbc database.
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionString = $"server={Globals.Database.MYSQL_IP};user={Globals.Database.MYSQL_USER};password={Globals.Database.MYSQL_PASS};database={Globals.Database.DBC_DB_NAME}";
            optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        }
    }

    [Table("applied_updates")]
    internal class AppliedUpdates
    {
        public int ID { get; set; }
    }

    [Table("AreaTable")]
    internal class AreaTable
    {
        public int ID { get; set; }
        public int AreaNumber { get; set; }
        public int ContinentID { get; set; }
        public int ParentAreaNum { get; set; }
        public int AreaBit { get; set; }
        public int Flags { get; set; }
        public int SoundProviderPref { get; set; }
        public int SoundProviderPrefUnderwater { get; set; }
        public int MIDIAmbience { get; set; }
        public int MIDIAmbienceUnderwater { get; set; }
        public int ZoneMusic { get; set; }
        public int IntroSound { get; set; }
        public int IntroPriority { get; set; }
        public string AreaName_enUS { get; set; }
        public string AreaName_enGB { get; set; }
        public string AreaName_koKR { get; set; }
        public string AreaName_frFR { get; set; }
        public string AreaName_deDE { get; set; }
        public string AreaName_enCN { get; set; }
        public string AreaName_zhCN { get; set; }
        public string AreaName_enTW { get; set; }
        public int AreaName_Mask { get; set; }
    }

    [Table("AreaTrigger")]
    internal class AreaTrigger
    {
        public int ID { get; set; }
        public int ContinentID { get; set; }
        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }
        public float Radius { get; set; }
    }

    [Table("BankBagSlotPrices")]
    internal class BankBagSlotPrice
    {
        public int ID { get; set; }
        public int Cost { get; set; }
    }

    [Table("CharacterCreateCameras")]
    internal class CharacterCreateCamera
    {
        public int ID { get; set; }
        public int Race { get; set; }
        public int Sex { get; set; }
        public int Camera { get; set; }
        public float Height { get; set; }
        public float Radius { get; set; }
        public float Target { get; set; }
    }

    [Table("CharacterFacialHairStyles")]
    internal class CharacterFacialHairStyle
    {
        public int ID { get; set; }
        public int RaceID { get; set; }
        public int GenderID { get; set; }
        public int VariationID { get; set; }
        public int BeardGeoset { get; set; }
        public int MoustacheGeoset { get; set; }
        public int SideburnGeoset { get; set; }
    }

    [Table("CharBaseInfo")]
    internal class CharBaseInfo
    {
        public int ID { get; set; }
        public int RaceID { get; set; }
        public int ClassID { get; set; }
        public int Proficiency { get; set; }
    }

    [Table("CharStartOutfit")]
    internal class CharStartOutfit
    {
        public int ID { get; set; }
        public int RaceID { get; set; }
        public int ClassID { get; set; }
        public int GenderID { get; set; }
        public int OutfitID { get; set; }
        public int ItemID_1 { get; set; }
        public int ItemID_2 { get; set; }
        public int ItemID_3 { get; set; }
        public int ItemID_4 { get; set; }
        public int ItemID_5 { get; set; }
        public int ItemID_6 { get; set; }
        public int ItemID_7 { get; set; }
        public int ItemID_8 { get; set; }
        public int ItemID_9 { get; set; }
        public int ItemID_10 { get; set; }
        public int ItemID_11 { get; set; }
        public int ItemID_12 { get; set; }
        public int DisplayItemID_1 { get; set; }
        public int DisplayItemID_2 { get; set; }
        public int DisplayItemID_3 { get; set; }
        public int DisplayItemID_4 { get; set; }
        public int DisplayItemID_5 { get; set; }
        public int DisplayItemID_6 { get; set; }
        public int DisplayItemID_7 { get; set; }
        public int DisplayItemID_8 { get; set; }
        public int DisplayItemID_9 { get; set; }
        public int DisplayItemID_10 { get; set; }
        public int DisplayItemID_11 { get; set; }
        public int DisplayItemID_12 { get; set; }
        public int InventoryType_1 { get; set; }
        public int InventoryType_2 { get; set; }
        public int InventoryType_3 { get; set; }
        public int InventoryType_4 { get; set; }
        public int InventoryType_5 { get; set; }
        public int InventoryType_6 { get; set; }
        public int InventoryType_7 { get; set; }
        public int InventoryType_8 { get; set; }
        public int InventoryType_9 { get; set; }
        public int InventoryType_10 { get; set; }
        public int InventoryType_11 { get; set; }
        public int InventoryType_12 { get; set; }
    }

    // I think we can omit the other name localization columns? We will never use them.
    // Localizations koKR, frFR, deDE, enCN, zhCN, enTW omitted.
    [Table("ChrClasses")]
    internal class ChrClass
    {
        public int ID { get; set; }
        public int PlayerClass { get; set; }
        public int DamageBonusStat { get; set; }
        public int DisplayPower { get; set; }
        public string PetNameToken { get; set; }
        public string Name_enUS { get; set; }
        public string Name_enGB { get; set; }
        public int Name_Mask { get; set; }
    }

    [Table("ChrProficiency")]
    internal class ChrProficiency
    {
        public int ID { get; set; }
        public int Proficiency_MinLevel_1 { get; set; }
        public int Proficiency_MinLevel_2 { get; set; }
        public int Proficiency_MinLevel_3 { get; set; }
        public int Proficiency_MinLevel_4 { get; set; }
        public int Proficiency_MinLevel_5 { get; set; }
        public int Proficiency_MinLevel_6 { get; set; }
        public int Proficiency_MinLevel_7 { get; set; }
        public int Proficiency_MinLevel_8 { get; set; }
        public int Proficiency_MinLevel_9 { get; set; }
        public int Proficiency_MinLevel_10 { get; set; }
        public int Proficiency_MinLevel_11 { get; set; }
        public int Proficiency_MinLevel_12 { get; set; }
        public int Proficiency_MinLevel_13 { get; set; }
        public int Proficiency_MinLevel_14 { get; set; }
        public int Proficiency_MinLevel_15 { get; set; }
        public int Proficiency_MinLevel_16 { get; set; }
        public int Proficiency_AcquireMethod_1 { get; set; }
        public int Proficiency_AcquireMethod_2 { get; set; }
        public int Proficiency_AcquireMethod_3 { get; set; }
        public int Proficiency_AcquireMethod_4 { get; set; }
        public int Proficiency_AcquireMethod_5 { get; set; }
        public int Proficiency_AcquireMethod_6 { get; set; }
        public int Proficiency_AcquireMethod_7 { get; set; }
        public int Proficiency_AcquireMethod_8 { get; set; }
        public int Proficiency_AcquireMethod_9 { get; set; }
        public int Proficiency_AcquireMethod_10 { get; set; }
        public int Proficiency_AcquireMethod_11 { get; set; }
        public int Proficiency_AcquireMethod_12 { get; set; }
        public int Proficiency_AcquireMethod_13 { get; set; }
        public int Proficiency_AcquireMethod_14 { get; set; }
        public int Proficiency_AcquireMethod_15 { get; set; }
        public int Proficiency_AcquireMethod_16 { get; set; }
        public int Proficiency_ItemClass_1 { get; set; }
        public int Proficiency_ItemClass_2 { get; set; }
        public int Proficiency_ItemClass_3 { get; set; }
        public int Proficiency_ItemClass_4 { get; set; }
        public int Proficiency_ItemClass_5 { get; set; }
        public int Proficiency_ItemClass_6 { get; set; }
        public int Proficiency_ItemClass_7 { get; set; }
        public int Proficiency_ItemClass_8 { get; set; }
        public int Proficiency_ItemClass_9 { get; set; }
        public int Proficiency_ItemClass_10 { get; set; }
        public int Proficiency_ItemClass_11 { get; set; }
        public int Proficiency_ItemClass_12 { get; set; }
        public int Proficiency_ItemClass_13 { get; set; }
        public int Proficiency_ItemClass_14 { get; set; }
        public int Proficiency_ItemClass_15 { get; set; }
        public int Proficiency_ItemClass_16 { get; set; }
        public int Proficiency_ItemSubClassMask_1 { get; set; }
        public int Proficiency_ItemSubClassMask_2 { get; set; }
        public int Proficiency_ItemSubClassMask_3 { get; set; }
        public int Proficiency_ItemSubClassMask_4 { get; set; }
        public int Proficiency_ItemSubClassMask_5 { get; set; }
        public int Proficiency_ItemSubClassMask_6 { get; set; }
        public int Proficiency_ItemSubClassMask_7 { get; set; }
        public int Proficiency_ItemSubClassMask_8 { get; set; }
        public int Proficiency_ItemSubClassMask_9 { get; set; }
        public int Proficiency_ItemSubClassMask_10 { get; set; }
        public int Proficiency_ItemSubClassMask_11 { get; set; }
        public int Proficiency_ItemSubClassMask_12 { get; set; }
        public int Proficiency_ItemSubClassMask_13 { get; set; }
        public int Proficiency_ItemSubClassMask_14 { get; set; }
        public int Proficiency_ItemSubClassMask_15 { get; set; }
        public int Proficiency_ItemSubClassMask_16 { get; set; }
    }

    // Localizations koKR, frFR, deDE, enCN, zhCN, enTW omitted.
    [Table("ChrRaces")]
    internal class ChrRace
    {
        public int ID { get; set; }
        public int Flags { get; set; }
        public int FactionID { get; set; }
        public int MaleDisplayId { get; set; }
        public int FemaleDisplayId { get; set; }
        public string ClientPrefix { get; set; }
        public float MountScale { get; set; }
        public int BaseLanguage { get; set; }
        public int CreatureType { get; set; }
        public int LoginEffectSpellID { get; set; }
        public int CombatStunSpellID { get; set; }
        public int ResSicknessSpellID { get; set; }
        public int SplashSoundID { get; set; }
        public int StartingTaxiNodes { get; set; }
        public string ClientFileString { get; set; }
        public int CinematicSequenceID { get; set; }
        public string Name_enUS { get; set; }
        public string Name_enGB { get; set; }
        public int Name_Mask { get; set; }
    }

    [Table("CinematicCamera")]
    internal class CinematicCamera
    {
        public int ID { get; set; }
        public string Model { get; set; }
        public int SoundID { get; set; }
        public float OriginX { get; set; }
        public float OriginY { get; set; }
        public float OriginZ { get; set; }
        public float OriginFacing { get; set; }
    }

    [Table("CinematicSequences")]
    internal class CinematicSequence
    {
        public int ID { get; set; }
        public int SoundID { get; set; }
        public int Camera_1 { get; set; }
        public int Camera_2 { get; set; }
        public int Camera_3 { get; set; }
        public int Camera_4 { get; set; }
        public int Camera_5 { get; set; }
        public int Camera_6 { get; set; }
        public int Camera_7 { get; set; }
        public int Camera_8 { get; set; }
    }

    [Table("CreatureDisplayInfo")]
    internal class CreatureDisplayInfo
    {
        public int ID { get; set; }
        public int ModelID { get; set; }
        public int SoundID { get; set; }
        public int ExtendedDisplayInfoID { get; set; }
        public float CreatureModelScale { get; set; }
        public int CreatureModelAlpha { get; set; }
        public string TextureVariation_1 { get; set; }
        public string TextureVariation_2 { get; set; }
        public string TextureVariation_3 { get; set; }
        public int BloodID { get; set; }
    }

    [Table("CreatureDisplayInfoExtra")]
    internal class CreatureDisplayInfoExtra
    {
        public int ID { get; set; }
        public int DisplayRaceID { get; set; }
        public int DisplayGenderID { get; set; }
        public int SkinID { get; set; }
        public int FaceID { get; set; }
        public int HairStyleID { get; set; }
        public int HairColorID { get; set; }
        public int FacialHairID { get; set; }
        public int NPCItemDisplay_1 { get; set; }
        public int NPCItemDisplay_2 { get; set; }
        public int NPCItemDisplay_3 { get; set; }
        public int NPCItemDisplay_4 { get; set; }
        public int NPCItemDisplay_5 { get; set; }
        public int NPCItemDisplay_6 { get; set; }
        public int NPCItemDisplay_7 { get; set; }
        public int NPCItemDisplay_8 { get; set; }
        public int NPCItemDisplay_9 { get; set; }
        public int NPCItemDisplay_10 { get; set; }
        public string BakeName { get; set; }
    }

    [Table("CreatureFamily")]
    internal class CreatureFamily
    {
        public int ID { get; set; }
        public float MinScale { get; set; }
        public int MinScaleLevel { get; set; }
        public float MaxScale { get; set; }
        public float MaxScaleLevel { get; set; }
        public int SkillLine_1 { get; set; }
        public int SkillLine_2 { get; set; }
    }

    [Table("Emotes")]
    internal class Emote
    {
        public int ID { get; set; }
        public int EmoteAnimID { get; set; }
        public int EmoteFlags { get; set; }
        public int EmoteSpecProc { get; set; }
        public int EmoteSpecProcParam { get; set; }
    }

    [Table("EmotesText")]
    internal class EmoteText
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int EmoteID { get; set; }
        public int EmoteText_1 { get; set; }
        public int EmoteText_2 { get; set; }
        public int EmoteText_3 { get; set; }
        public int EmoteText_4 { get; set; }
        public int EmoteText_5 { get; set; }
        public int EmoteText_6 { get; set; }
        public int EmoteText_7 { get; set; }
        public int EmoteText_8 { get; set; }
        public int EmoteText_9 { get; set; }
        public int EmoteText_10 { get; set; }
        public int EmoteText_11 { get; set; }
        public int EmoteText_12 { get; set; }
        public int EmoteText_13 { get; set; }
        public int EmoteText_14 { get; set; }
        public int EmoteText_15 { get; set; }
        public int EmoteText_16 { get; set; }
    }

    // Localizations koKR, frFR, deDE, enCN, zhCN, enTW omitted.
    [Table("EmotesTextData")]
    internal class EmoteTextData
    {
        public int ID { get; set; }
        public string Text_enUS { get; set; }
        public string Text_enGB { get; set; }
        public int Text_Mask { get; set; }
    }

    // Localizations koKR, frFR, deDE, enCN, zhCN, enTW omitted.
    [Table("Faction")]
    internal class Faction
    {
        public int ID { get; set; }
        public int ReputationIndex { get; set; }
        public int ReputationRaceMask_1 { get; set; }
        public int ReputationRaceMask_2 { get; set; }
        public int ReputationRaceMask_3 { get; set; }
        public int ReputationRaceMask_4 { get; set; }
        public int ReputationClassMask_1 { get; set; }
        public int ReputationClassMask_2 { get; set; }
        public int ReputationClassMask_3 { get; set; }
        public int ReputationClassMask_4 { get; set; }
        public int ReputationBase_1 { get; set; }
        public int ReputationBase_2 { get; set; }
        public int ReputationBase_3 { get; set; }
        public int ReputationBase_4 { get; set; }
        public string Name_enUS { get; set; }
        public string Name_enGB { get; set; }
        public int Name_Mask { get; set; }
    }

    // Localizations koKR, frFR, deDE, enCN, zhCN, enTW omitted.
    [Table("FactionGroup")]
    internal class FactionGroup
    {
        public int ID { get; set; }
        public int MaskID { get; set; }
        public string InternalName { get; set; }
        public string Name_enUS { get; set; }
        public string Name_enGB { get; set; }
        public int Name_Mask { get; set; }
    }

    [Table("SkillLine")]
    internal class SkillLine
    {
        public int ID { get; set; }
        public int RaceMask { get; set; }
        public int ClassMask { get; set; }
        public int ExcludeRace { get; set; }
        public int ExcludeClass { get; set; }
        public int CategoryID { get; set; }
        public int SkillType { get; set; }
        public int MinCharLevel { get; set; }
        public int MaxRank { get; set; }
        public int Abandonable { get; set; }
        public string DisplayName_enUS { get; set; }
        public string DisplayName_enGB { get; set; }
        public string DisplayName_koKR { get; set; }
        public string DisplayName_frFR { get; set; }
        public string DisplayName_deDE { get; set; }
        public string DisplayName_enCN { get; set; }
        public string DisplayName_zhCN { get; set; }
        public string DisplayName_enTW { get; set; }
        public int DisplayName_Mask { get; set; }
    }
}
