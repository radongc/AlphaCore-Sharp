using AlphaCore_Sharp.Game;
using System.Security.Cryptography;
using System.Text;
using LinqToDB.Mapping;
using LinqToDB;

// TODO: Investigate replacing EF Core with Dapper.
// EF Core does not work with Native AOT. Supposedly, that will be a part of EF Core 8.0
// Also supposedly, Dapper does work with Native AOT.
// Although I prefer the syntax of EF Core to Dapper (and the use of LINQ/IQueryable objects), building with Native AOT is a priority.
// Maybe in the future we can go back to EF Core once it supports Native AOT.
namespace AlphaCore_Sharp.Database.Realm.Models
{
    internal class RealmModels : LinqToDB.Data.DataConnection
    {
        public RealmModels() : base(LinqToDB.ProviderName.MySql, @$"Server={Globals.Database.MYSQL_IP};Database={Globals.Database.REALM_DB_NAME}; User ID={Globals.Database.MYSQL_USER};Password={Globals.Database.MYSQL_PASS};") { }

        public ITable<Account> Accounts => this.GetTable<Account>();
        public ITable<Character> Characters => this.GetTable<Character>();
    }

    // ** Models ** //
    // Columns with "Column" attribute have a different in-database name than their model property name.
    // Keys named 'ID' do not need to be marked by FluentAPI (in OnModelCreating); otherwise they must be marked.

    // TODO: Possibly rewrite this, we don't want to have the account Model be the same object as the account manager.
    [Table(Name = "accounts")]
    internal class Account
    {
        [PrimaryKey]
        public int Id { get; set; }
        [Column]
        public string Name { get; set; }
        [Column]
        public string Password { get; set; }
        [Column]
        public string Ip { get; set; }
        [Column]
        public int Gmlevel { get; set; }

        public void SetPassword(string plaintextPassword)
        {
            Password = HashPassword(plaintextPassword);
        }

        public bool CheckPassword(string plaintextPassword) => Password == HashPassword(plaintextPassword);

        public string HashPassword(string plaintextPassword)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(plaintextPassword));
                string stringHash = BitConverter.ToString(bytes).Replace("-", "").ToLower();
                return stringHash;
            }
        }
    }

    internal class AppliedUpdate
    {
        public int ID { get; set; }
    }

    [Table(Name = "characters")]
    internal class Character
    {
        [PrimaryKey]
        public int GUID { get; set; }
        [PrimaryKey]
        public int Account { get; set; }
        [PrimaryKey]
        public string Name { get; set; }
        [Column]
        public int Race { get; set; }
        [Column]
        public int Class { get; set; }
        [Column]
        public int Gender { get; set; }
        [Column]
        public int Level { get; set; }
        [Column]
        public int XP { get; set; }
        [Column]
        public int Money { get; set; }
        [Column]
        public int Skin { get; set; }
        [Column]
        public int Face { get; set; }
        [Column]
        public int HairStyle { get; set; }
        [Column]
        public int HairColour { get; set; }
        [Column]
        public int FacialHair { get; set; }
        [Column]
        public int BankSlots { get; set; }
        [Column]
        public int TalentPoints { get; set; }
        [Column]
        public int SkillPoints { get; set; }
        [Column(Name = "position_x")]
        public float PositionX { get; set; }
        [Column(Name = "position_y")]
        public float PositionY { get; set; }
        [Column(Name = "position_z")]
        public float PositionZ { get; set; }
        [Column]
        public int Map { get; set; }
        [Column]
        public float Orientation { get; set; }
        [Column]
        public string TaxiMask { get; set; }
        [Column(Name = "explored_areas")]
        public string ExploredAreas { get; set; }
        [PrimaryKey]
        public int Online { get; set; }
        [Column]
        public int TotalTime { get; set; }
        [Column]
        public int LevelTime { get; set; }
        [Column(Name = "extra_flags")]
        public int ExtraFlags { get; set; }
        [Column]
        public int Zone { get; set; }
        [Column(Name = "taxi_path")]
        public string TaxiPath { get; set; }
        [Column]
        public int Drunk { get; set; }
        [Column]
        public int Health { get; set; }
        [Column]
        public int Power1 { get; set; }
        [Column]
        public int Power2 { get; set; }
        [Column]
        public int Power3 { get; set; }
        [Column]
        public int Power4 { get; set; }
        [Column]
        public int Power5 { get; set; }
    }

    [Table(Name = "character_buttons")]
    internal class CharacterButton
    {
        public const string TABLENAME = "character_buttons";

        public int Owner { get; set; }
        public int Index { get; set; }
        public int Action { get; set; }
    }

    [Table(Name = "character_deathbind")]
    internal class CharacterDeathbind
    {
        public const string TABLENAME = "character_deathbind";

        [Column(Name ="deathbind_id")]
        public int ID { get; set; }
        [Column(Name ="player_guid")]
        public int PlayerGUID { get; set; }
        [Column(Name ="creature_binder_guid")]
        public int CreatureBinderGUID { get; set; }
        [Column(Name ="deathbind_map")]
        public int DeathbindMap { get; set; }
        [Column(Name ="deathbind_zone")]
        public int DeathbindZone { get; set; }
        [Column(Name ="deathbind_position_x")]
        public float DeathbindPositionX { get; set; }
        [Column(Name ="deathbind_position_y")]
        public float DeathbindPositionY { get; set; }
        [Column(Name ="deathbind_position_z")]
        public float DeathbindPositionZ { get; set; }
    }

    [Table(Name = "character_gifts")]
    internal class CharacterGifts
    {

    }

    [Table(Name = "character_inventory")]
    internal class CharacterInventory
    {

    }

    [Table(Name = "character_pets")]
    internal class CharacterPet
    {

    }

    [Table(Name = "character_pet_spells")]
    internal class CharacterPetSpell
    {

    }

    [Table(Name = "character_quest_state")]
    internal class CharacterQuestState
    {

    }

    [Table(Name = "character_reputation")]
    internal class CharacterReputation
    {

    }

    [Table(Name = "character_skills")]
    internal class CharacterSkill
    {

    }

    [Table(Name = "character_social")]
    internal class CharacterSocial
    {

    }

    [Table(Name = "character_spells")]
    internal class CharacterSpell
    {

    }

    [Table(Name = "character_spell_book")]
    internal class CharacterSpellBook
    {

    }

    [Table(Name = "character_spell_cooldown")]
    internal class CharacterSpellCooldown
    {

    }

    [Table(Name = "group")]
    internal class Group
    {

    }

    [Table(Name = "group_member")]
    internal class GroupMember
    {

    }

    [Table(Name = "guild")]
    internal class Guild
    {

    }

    [Table(Name = "guild_member")]
    internal class GuildMember
    {

    }

    [Table(Name = "petition")]
    internal class Petition
    {

    }

    [Table(Name = "petition_sign")]
    internal class PetitionSign
    {

    }

    [Table(Name = "tickets")]
    internal class Ticket
    {

    }
}
