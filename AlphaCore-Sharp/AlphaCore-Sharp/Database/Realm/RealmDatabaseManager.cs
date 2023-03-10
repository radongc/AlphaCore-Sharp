using static AlphaCore_Sharp.Utils.Constants.CustomCodes;
using static AlphaCore_Sharp.Utils.Constants.MiscCodes;

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
            using (RealmModels models = new RealmModels())
            {
                // Best solution so far to first query of table taking extra time. However, this makes first query happen on startup, making startup take longer.
                models.Accounts.FirstOrDefault();
                models.Characters.FirstOrDefault();
                models.CharacterButtons.FirstOrDefault();
            }
        }

        // ** Account ** //

        public static AccountInfo AccountTryLogin(string username, string password)
        {
            using (RealmModels models = new RealmModels())
            {
                Account account = models.Accounts.Where(acc => acc.Name == username).FirstOrDefault();
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
            using (RealmModels models = new RealmModels())
            {
                List<Character> chars = models.Characters.Where(ch => ch.Account == accountId).ToList();

                return chars ?? new List<Character> { };
            }
        }

        // ** Character ** //

        public static int CharacterGetOnlineCount()
        {
            using (RealmModels models = new RealmModels())
            {
                int onlineCount = models.Characters.Where(c => c.Online == 1).ToList().Count();

                return onlineCount;
            }
        }

        public static Character CharacterGetByGUID(ulong guid)
        {
            using (RealmModels models = new RealmModels())
            {
                Character character = models.Characters.Where(c => c.GUID == (guid & (~(ulong)HighGuid.HIGHGUID_PLAYER))).FirstOrDefault();

                return character;
            }
        }
    }
}
