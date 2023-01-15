using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionString = $"server={Globals.Database.MYSQL_IP};user={Globals.Database.MYSQL_USER};password={Globals.Database.MYSQL_PASS};database={Globals.Database.REALM_DB_NAME}";
            optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        }
    }

    // TODO: Rewrite this, we don't want to have the account Model be the same object as the account manager.
    internal class Account
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Ip { get; set;}
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
}
