using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using AlphaCore_Sharp.Game;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql;

// TODO: EF Core does not support Native AOT. Must find an ORM that does, or write your own.
// Furthermore, there are issues with IQueryable objects and Native AOT as well.
namespace AlphaCore_Sharp.Database.Realm
{
    internal class RealmModels : DbContext
    {
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Character> Characters { get; set; }

        // Set up MySQL connection to Realm database.
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionString = $"server={Globals.Database.MYSQL_IP};user={Globals.Database.MYSQL_USER};password={Globals.Database.MYSQL_PASS};database={Globals.Database.REALM_DB_NAME}";
            optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Define primary keys (FluentAPI, EF < 7.0); Pomelo
            modelBuilder.Entity<Account>()
                .HasKey(a => new { a.Id });
            
            modelBuilder.Entity<Character>()
                .HasKey(c => new { c.GUID, c.Account, c.Name, c.Online });
        }
    }

    // TODO: Possibly rewrite this, we don't want to have the account Model be the same object as the account manager.
    internal class Account
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Ip { get; set; }
        public int Gmlevel { get; set; }

        public void SetPassword(string plaintextPassword)
        {
            this.Password = HashPassword(plaintextPassword);
        }

        // TODO: Replace placeholder account.
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

    internal class Character
    {
        public int GUID { get; set; }
        public int Account { get; set; }
        public string Name { get; set; }
        public int Race { get; set; }
        public int Class { get; set; }
        public int Gender { get; set; }
        public int Level { get; set; }
        public int XP { get; set; }
        public int Money { get; set; }
        public int Skin { get; set; }
        public int Face { get; set; }
        public int HairStyle { get; set; }
        public int HairColour { get; set; }
        public int FacialHair { get; set; }
        public int BankSlots { get; set; }
        public int TalentPoints { get; set; }
        public int SkillPoints { get; set; }
        public float Position_X { get; set; }
        public float Position_Y { get; set; }
        public float Position_Z { get; set; }
        public int Map { get; set; }
        public float Orientation { get; set; }
        public string TaxiMask { get; set; }
        public string Explored_Areas { get; set; }
        public int Online { get; set; }
        public int TotalTime { get; set; }
        public int LevelTime { get; set; }
        public int Extra_Flags { get; set; }
        public int Zone { get; set; }
        public string Taxi_Path { get; set; }
        public int Drunk { get; set; }
        public int Health { get; set; }
        public int Power1 { get; set; }
        public int Power2 { get; set; }
        public int Power3 { get; set; }
        public int Power4 { get; set; }
        public int Power5 { get; set; }
    }
}
