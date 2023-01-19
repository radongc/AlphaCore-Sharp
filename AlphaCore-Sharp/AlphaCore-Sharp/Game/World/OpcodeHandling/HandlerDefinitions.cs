using AlphaCore_Sharp.Game.World.OpcodeHandling.Handlers;
using static AlphaCore_Sharp.Utils.Constants.OpCodes;
using static AlphaCore_Sharp.Game.World.OpcodeHandling.PacketManager;

namespace AlphaCore_Sharp.Game.World.OpcodeHandling
{
    internal class HandlerDefinitions
    {
        // TODO: Consider creating a separate file/class for each handler, rather than grouping them together.
        public static void InitializePacketHandlers()
        {
            // Authentication OpCodes.
            StoreOpCode(OpCode.CMSG_AUTH_SESSION, AuthenticationHandler.HandleAuthSession);

            // Network OpCodes.
            StoreOpCode(OpCode.CMSG_PING, NetworkHandler.HandlePing);

            // Character OpCodes.
            StoreOpCode(OpCode.CMSG_CHAR_ENUM, CharacterHandler.HandleCharEnum);
            StoreOpCode(OpCode.CMSG_PLAYER_LOGIN, CharacterHandler.HandlePlayerLogin);
        }
    }
}
