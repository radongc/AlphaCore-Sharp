// See https://aka.ms/new-console-template for more information
// dotnet publish -r win-x64 -c Release in powershell to build (native).
using AlphaCore_Sharp.Network.Packet;
using AlphaCore_Sharp.Utils;

namespace AlphaCore_Sharp
{
    class WorldServer
    {
        static void Main()
        {
            ulong a = 25362;
            string b = "hellurr";

            PacketWriter writer = new PacketWriter(OpCodes.OpCode.CMSG_BOOTME);
            writer += a;
            writer += b;

            PacketReader reader = new PacketReader(writer.ReadDataToSend());

            Console.WriteLine("Hello, World!");
            Console.WriteLine(reader.Opcode);
            Console.WriteLine(reader.Size);
            Console.WriteLine(reader.ReadInt32());
            Console.WriteLine(reader.ReadString());
            Console.ReadLine();
        }
    }
}