using AlphaCore_Sharp.Network.Packet;
using AlphaCore_Sharp.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static AlphaCore_Sharp.Utils.Constants.OpCodes;
using static AlphaCore_Sharp.Utils.Constants.AuthCodes;
using AlphaCore_Sharp.Utils.Constants;
using AlphaCore_Sharp.Game.World.Managers.Objects;

namespace AlphaCore_Sharp.Game.World.OpcodeHandling.Handlers
{
    internal class AuthenticationHandler
    {
        // TODO: Implement account login (after ORM implemented with models and data loading.)
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

            // If name and password size is not exactly 2, return unknown account.
            if (nameAndPass.Length != 2)
                responsePacket += (byte)AuthCode.AUTH_UNKNOWN_ACCOUNT;
            else
            {
                string accountName = nameAndPass[0];
                string accountPass = nameAndPass[1];

                // TODO: Implement database account checking. For now, use static user/pass.
                AccountManager account = new AccountManager();

                if (version != Globals.Realm.CLIENT_VERSION)
                    // If client version does not match, reject login attempt.
                    authResult = AuthCode.AUTH_VERSION_MISMATCH;
                else if (accountName != Globals.Settings.TEMP_ACCOUNT_USER)
                    // If account name is not recognized, reject login attempt.
                    authResult = AuthCode.AUTH_UNKNOWN_ACCOUNT;
                else if (!account.CheckPassword(accountPass))
                    // If password does not match account, reject login attempt.
                    authResult = AuthCode.AUTH_INCORRECT_PASSWORD;
                else
                    // If no issues encountered, accept login attempt.
                    authResult = AuthCode.AUTH_OK;
            }

            // result = true keeps the socket connection open. If handlers do not return true, the client connection is closed.
            if (authResult == AuthCode.AUTH_OK)
                result = true;

            // Write result to the response packet.
            responsePacket += (byte)authResult;

            // Send response packet to client and return the result to WorldManager.OnData().
            worldSession.Send(responsePacket);
            return result;
        }
    }
}
