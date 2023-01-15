using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static AlphaCore_Sharp.Utils.Constants.CustomCodes;

namespace AlphaCore_Sharp.Database.Realm
{
    internal class RealmDatabaseManager
    {
        private static RealmModels? _realmModels;

        public static void InitializeRealmDatabase()
        {
            _realmModels = new RealmModels();
        }

        public static LoginStatus TryLoginAccount(string username, string password)
        {
            Account account = _realmModels.Accounts.Where(acc => acc.Name == username).FirstOrDefault();
            if (account == null)
                return LoginStatus.UNKNOWN_ACCOUNT;
            else
            {
                if (!account.CheckPassword(password))
                    return LoginStatus.INVALID_PASSWORD;
                else
                    return LoginStatus.SUCCESS;
            }
        }
    }
}
