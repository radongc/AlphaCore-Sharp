using AlphaCore_Sharp.Network.Packet;
using AlphaCore_Sharp.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static AlphaCore_Sharp.Utils.Constants.OpCodes;

namespace AlphaCore_Sharp.Game.World.OpcodeHandling
{
    internal static class PacketManager
    {
        // Dictionary of all OpCodes and their Handler methods.
        public static Dictionary<OpCode, HandlerDelegate> OpcodeHandlers = new Dictionary<OpCode, HandlerDelegate>();
        // Delegate definition that will call Handler method when invoked.
        public delegate void HandlerDelegate(ref PacketReader opcode, ref WorldManager worldManager);

        // Define OpCode with Handler method.
        public static void Define(OpCode opcode, HandlerDelegate handler)
        {
            OpcodeHandlers[opcode] = handler;
        }

        // Call (invoke) the Handler method.
        public static bool Invoke(PacketReader reader, WorldManager worldManager, OpCode opcode)
        {
            if (OpcodeHandlers.ContainsKey(opcode))
            {
                OpcodeHandlers[opcode].Invoke(ref reader, ref worldManager);
                return true;
            }
            else
            {
                Logger.Error($"Unhandled Opcode: {opcode} - {(byte)opcode}");
                return false;
            }
        }
    }
}
