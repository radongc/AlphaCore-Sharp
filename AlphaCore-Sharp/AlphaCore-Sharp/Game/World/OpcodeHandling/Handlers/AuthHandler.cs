using AlphaCore_Sharp.Network.Packet;
using AlphaCore_Sharp.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlphaCore_Sharp.Game.World.OpcodeHandling.Handlers
{
    internal class AuthHandler
    {
        // TODO: Implement account login (after ORM implemented with models and data loading.)
        public static void HandleAuthSession(ref PacketReader packet, ref WorldManager worldManager)
        {
            Logger.Debug("Auth session being handled!!");
        }
    }
}
