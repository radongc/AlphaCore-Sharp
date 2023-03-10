using AlphaCore_Sharp.Network.Packet;
using static AlphaCore_Sharp.Utils.Constants.OpCodes;
using static AlphaCore_Sharp.Utils.Constants.AuthCodes;
using static AlphaCore_Sharp.Utils.Constants.CustomCodes;
using AlphaCore_Sharp.Database.Realm;

namespace AlphaCore_Sharp.Game.World.OpcodeHandling.Handlers
{
    internal class AuthenticationHandler
    {
        public static bool HandleAuthSession(ref PacketReader packet, ref WorldManager worldSession)
        {
            bool result = false;

            // AuthCode result of login attempt.
            AuthCode authResult = AuthCode.AUTH_FAILED;

            // Create SMSG_AUTH_RESPONSE packet.
            PacketWriter responsePacket = new PacketWriter(OpCode.SMSG_AUTH_RESPONSE);

            // Read version and empty Uint from CMSG_AUTH_SESSION.
            uint version = packet.ReadUInt32();
            packet.ReadUInt32();

            // Read account name and password from CMSG_AUTH_SESSION.
            string[] nameAndPass = packet.ReadString().Split(new string[] { "\r\n", "\r", "\n" }, 
                                                             StringSplitOptions.RemoveEmptyEntries);
            AccountInfo loginAttempt = new AccountInfo();

            // If name and password size is not exactly 2, return unknown account.
            if (nameAndPass.Length != 2)
                authResult = AuthCode.AUTH_UNKNOWN_ACCOUNT;
            else
            {
                string accountName = nameAndPass[0];
                string accountPass = nameAndPass[1];

                loginAttempt = RealmDatabaseManager.AccountTryLogin(accountName, accountPass);

                if (version != Globals.Realm.CLIENT_VERSION)
                    // If client version does not match, reject login attempt.
                    authResult = AuthCode.AUTH_VERSION_MISMATCH;
                else if (loginAttempt.status == LoginStatus.INVALID_PASSWORD)
                    // If password does not match account, reject login attempt.
                    authResult = AuthCode.AUTH_INCORRECT_PASSWORD;
                else if (loginAttempt.status == LoginStatus.UNKNOWN_ACCOUNT)
                    // If account doesn't exist, reject login attempt.
                    authResult = AuthCode.AUTH_UNKNOWN_ACCOUNT;
                else
                    // If no issues encountered, accept login attempt.
                    authResult = AuthCode.AUTH_OK;
            }

            // result = true keeps the socket connection open. If handlers do not return true, the client connection is closed.
            if (authResult == AuthCode.AUTH_OK)
            {
                result = true;

                worldSession.Account = loginAttempt.account!;
            }

            // Write result to the response packet.
            responsePacket += (byte)authResult;

            // Send response packet to client and return the result to WorldManager.AuthChallenge().
            // We do not use EnqueuePacket here because the queue has not been initialized at this point.
            worldSession.Send(responsePacket);
            return result;
        }
    }
}
