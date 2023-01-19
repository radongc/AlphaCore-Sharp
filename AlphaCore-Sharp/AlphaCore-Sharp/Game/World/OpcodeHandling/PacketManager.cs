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
        // Delegate function that will call Handler method when invoked.
        public delegate bool HandlerDelegate(ref PacketReader opcode, ref WorldManager worldSession);

        // Store OpCode and Handler method.
        public static void StoreOpCode(OpCode opcode, HandlerDelegate handler)
        {
            OpcodeHandlers[opcode] = handler;
        }

        // Invoke the Handler method. Returns a boolean, which may be false if the handler method returned a bad result, or if an opcode is unhandled.
        public static bool Invoke(PacketReader reader, WorldManager worldSession, OpCode opcode)
        {
            if (OpcodeHandlers.ContainsKey(opcode))
            {
                Logger.Debug($"Handling {opcode}");
                bool handleResult = OpcodeHandlers[opcode].Invoke(ref reader, ref worldSession);
                return handleResult;
            }
            else
            {
                // Return false and kill socket connection if OpCode unhandled (for now).
                byte[] opcodeArray = {(byte)opcode};

                Logger.Error($"Unhandled Opcode: {opcode} - ID 0x{Convert.ToHexString(opcodeArray)}, {(byte)opcode}");
                return true;
            }
        }
    }
}
