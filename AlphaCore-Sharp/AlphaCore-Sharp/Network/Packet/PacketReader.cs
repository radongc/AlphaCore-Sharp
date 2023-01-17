using System.IO;
using System.Text;
using static AlphaCore_Sharp.Utils.Constants.OpCodes;

namespace AlphaCore_Sharp.Network.Packet
{
    /* PacketReader
     * Based on barncastle/Alpha-WoW's PacketReader class.
     * POCO representation of incoming game packet.
     */

    // TODO: Add comments explaining this class.
    internal class PacketReader : BinaryReader
    {
        public OpCode Opcode { get; set; }
        public ushort Size { get; set; }

        public PacketReader(byte[] data, bool worldPacket = true) : base(new MemoryStream(data))
        {
            // 0.5.3 3368 Packet header: Size: 2 bytes + Cmd(opcode): 4 bytes
            if (worldPacket)
            {
                Size = (ushort)((this.ReadUInt16() / 0x100) - 4);
                Opcode = (OpCode)this.ReadUInt32();
            }
        }

        public sbyte ReadInt8()
        {
            return base.ReadSByte();
        }

        public new short ReadInt16()
        {
            return base.ReadInt16();
        }

        public new int ReadInt32()
        {
            return base.ReadInt32();
        }

        public new long ReadInt64() 
        {
            return base.ReadInt64();
        }

        public byte ReadUInt8()
        {
            return base.ReadByte();
        }

        public new ushort ReadUInt16()
        {
            return base.ReadUInt16();
        }

        public new uint ReadUInt32()
        {
            return base.ReadUInt32();
        }

        public new ulong ReadUInt64()
        {
            return base.ReadUInt64();
        }

        public float ReadFloat()
        {
            return base.ReadSingle();
        }

        public new double ReadDouble()
        {
            return base.ReadDouble();
        }

        public string ReadString(byte terminator = 0)
        {
            StringBuilder tempString = new StringBuilder();
            char tempChar = base.ReadChar();
            char tempEndChar = Convert.ToChar(Encoding.UTF8.GetString(new byte[] { terminator }));

            while (tempChar != tempEndChar)
            {
                tempString.Append(tempChar);
                tempChar = base.ReadChar();
            }

            return tempString.ToString();
        }

        public new string ReadString()
        {
            return ReadString(0);
        }

        public new byte[] ReadBytes(int count)
        {
            return base.ReadBytes(count);
        }

        public string ReadStringFromBytes(int count)
        {
            byte[] stringArray = base.ReadBytes(count);
            Array.Reverse(stringArray);

            return Encoding.ASCII.GetString(stringArray);
        }

        public string ReadIPAddress()
        {
            byte[] ip = new byte[4];

            for (int i = 0; i < 4; ++i)
            {
                ip[i] = ReadUInt8();
            }

            return $"{ip[0]}.{ip[1]}.{ip[2]}.{ip[3]}";
        }

        public void SkipBytes(int count)
        {
            base.BaseStream.Position += count;
        }
    }
}
