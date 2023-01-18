using AlphaCore_Sharp.Game;
using MySqlConnector;
using System.ComponentModel.DataAnnotations.Schema;

namespace AlphaCore_Sharp.Database.Dbc.Models
{
    internal class DbcModels
    {
        public static MySqlConnection GetDbcConnection()
        {
            string connectionString = @$"Server={Globals.Database.MYSQL_IP};Database={Globals.Database.DBC_DB_NAME}; User ID={Globals.Database.MYSQL_USER};Password={Globals.Database.MYSQL_PASS};";
            return new MySqlConnection(connectionString);
        }
    }

    internal class AreaTable
    {
        public const string TABLENAME = "AreaTable";

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

    internal class SkillLine
    {
        public const string TABLENAME = "SkillLine";

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
