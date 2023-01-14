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
    // TODO: Start adding handlers for packets.
    internal class HandlerDefinitions
    {
        public static void InitializePacketHandlers()
        {
            StoreOpCode(OpCode.CMSG_AUTH_SESSION, AuthenticationHandler.HandleAuthSession);
        }
    }
}
