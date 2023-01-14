using AlphaCore_Sharp.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AlphaCore_Sharp.Game.World.Managers.Objects
{
    internal class AccountManager
    {
        // TODO: Replace placeholder account.
        public bool CheckPassword(string plaintextPassword) => Globals.Settings.TEMP_ACCOUNT_PASS == HashPassword(plaintextPassword);

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
