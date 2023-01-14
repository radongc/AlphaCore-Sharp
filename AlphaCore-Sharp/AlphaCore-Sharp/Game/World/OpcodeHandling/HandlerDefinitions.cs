using AlphaCore_Sharp.Game.World.OpcodeHandling.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static AlphaCore_Sharp.Utils.Constants.OpCodes;

namespace AlphaCore_Sharp.Game.World.OpcodeHandling
{
    // TODO: Start adding handlers for packets.
    internal class HandlerDefinitions
    {
        public static void InitializePacketHandlers()
        {
            PacketManager.Define(OpCode.CMSG_AUTH_SESSION, AuthHandler.HandleAuthSession);
        }
    }
}
