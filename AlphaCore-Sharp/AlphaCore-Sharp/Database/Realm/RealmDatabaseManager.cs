using AlphaCore_Sharp.Database.Realm.Models;
using static AlphaCore_Sharp.Utils.Constants.CustomCodes;

namespace AlphaCore_Sharp.Database.Realm
{
    // TODO: Refactor and/or move this struct somewhere else, verify that this is the best way to check login attempt.
    internal struct AccountInfo
    {
        internal AccountInfo(Account acc, LoginStatus status)
        {
            this.account = acc;
            this.status = status;
        }

        public Account? account { get; set; }
        public LoginStatus status { get; set; }
    }

    // TODO: Add comments explaining this class.
    internal class RealmDatabaseManager
    {
        public static void InitializeRealmDatabase()
        {
        }

        // ** Account ** //

        public static AccountInfo AccountTryLogin(string username, string password)
        {
            /*MySqlConnection models = RealmModels.GetRealmConnection();
            models.Open();

            Account account = models.Query<Account>($"SELECT * FROM accounts WHERE name=\"{username}\"").FirstOrDefault();
            models.Close();*/

            using (RealmModels db = new RealmModels())
            {
                Account account = ((Account)(from acc in db.Accounts
                            where acc.Name.Equals(username)
                            select acc));


                if (account == null)
                    return new AccountInfo(null, LoginStatus.UNKNOWN_ACCOUNT);
                else
                {
                    if (!account.CheckPassword(password))
                        return new AccountInfo(null, LoginStatus.INVALID_PASSWORD);
                    else
                        return new AccountInfo(account, LoginStatus.SUCCESS);
                }
            }
        }

        public static List<Character> AccountGetCharacters(int accountId)
        {
            /*MySqlConnection models = RealmModels.GetRealmConnection();
            models.Open();

            List<Character> chars = models.Query<Character>($"SELECT * FROM characters WHERE account = {accountId}").ToList();
            models.Close();

            return chars ?? new List<Character> { };*/
            return new List<Character> { };
        }

        // ** Character ** //

        public static int CharacterGetOnlineCount()
        {
            return 0;
/*
            MySqlConnection models = RealmModels.GetRealmConnection();
            models.Open();

            int onlineCount = models.Query<Character>($"SELECT * FROM {Character.TABLENAME} WHERE online = 1").ToList().Count();
            models.Close();

            return onlineCount;*/
        }
    }
}
