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
