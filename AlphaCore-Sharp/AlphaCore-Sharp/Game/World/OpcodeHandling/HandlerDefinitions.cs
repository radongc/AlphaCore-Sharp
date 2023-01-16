using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AlphaCore_Sharp.Game.World.OpcodeHandling.Handlers;
using static AlphaCore_Sharp.Utils.Constants.OpCodes;
using static AlphaCore_Sharp.Game.World.OpcodeHandling.PacketManager;

namespace AlphaCore_Sharp.Game.World.OpcodeHandling
{
    // TODO: Finish character enum packet (pet and gear info).
    internal class HandlerDefinitions
    {
        public static void InitializePacketHandlers()
        {
            // Authentication OpCodes.
            StoreOpCode(OpCode.CMSG_AUTH_SESSION, AuthenticationHandler.HandleAuthSession);

            // Network OpCodes.
            StoreOpCode(OpCode.CMSG_PING, NetworkHandler.HandlePing);

            // Character OpCodes.
            StoreOpCode(OpCode.CMSG_CHAR_ENUM, CharacterHandler.HandleCharEnum);
        }
    }
}
