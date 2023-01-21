using ICSharpCode.SharpZipLib.Zip.Compression.Streams;
using System.Text;
using static AlphaCore_Sharp.Utils.Constants.OpCodes;

/*
 * PacketWriter
 * Serialization class derived from barncastle/Alpha-WoW's PacketWriter.
 * Can use 'Write' methods to serialize, or use '+=' operators for convenience.
 * POCO representation of outgoing game packet.
 */

// TODO: Add comments explaining this class.
namespace AlphaCore_Sharp.Network.Packet
{
    internal class PacketWriter : BinaryWriter
    {
        public OpCode Opcode { get; set; }
        public ushort Size { get; set; }
        public int Length 
        { 
            get 
            { 
                return (int)BaseStream.Length; 
            } 
        }

        public PacketWriter() : base(new MemoryStream()) { }
        public PacketWriter(OpCode opcode) : base(new MemoryStream())
        {
            Opcode = opcode;
            WritePacketHeader();
        }

        protected void WritePacketHeader()
        {
            // Header for SMSG_AUTH_CHALLENGE: Size - 2 bytes + Cmd - 2bytes
            // Header: Size - 2 bytes + Cmd - 4 bytes
            WriteUInt8(0);
            WriteUInt8(0);
            WriteUInt8((byte)((uint)Opcode % 0x100));
            WriteUInt8((byte)((uint)Opcode / 0x100));

            if (Opcode != OpCode.SMSG_AUTH_CHALLENGE)
            {
                WriteUInt8(0);
                WriteUInt8(0);
            }
        }

        public byte[] ReadDataToSend(bool isAuthPacket = false)
        {
            byte[] data = new byte[BaseStream.Length];
            Seek(0, SeekOrigin.Begin);

            for (int i = 0; i < BaseStream.Length; i++)
                data[i] = (byte)BaseStream.ReadByte();

            Size = (ushort)(data.Length - 2);
            if (!isAuthPacket)
            {
                data[0] = (byte)(Size / 0x100);
                data[1] = (byte)(Size % 0x100);
            }

            return data;
        }

        public PacketWriter WriteInt8(sbyte data)
        {
            base.Write(data);
            return this;
        }

        // WriteInt8
        public static PacketWriter operator +(PacketWriter a, sbyte b)
        {
            PacketWriter message = a;

            message.Write(b);

            return message;
        }

        public PacketWriter WriteInt16(short data)
        {
            base.Write(data);
            return this;
        }

        // WriteInt16
        public static PacketWriter operator +(PacketWriter a, short b)
        {
            PacketWriter message = a;

            message.Write(b);

            return message;
        }

        public PacketWriter WriteInt32(int data)
        {
            base.Write(data);
            return this;
        }

        // WriteInt32
        public static PacketWriter operator +(PacketWriter a, int b)
        {
            PacketWriter message = a;

            message.Write(b);

            return message;
        }

        public PacketWriter WriteInt64(long data)
        {
            base.Write(data);
            return this;
        }


        /// WriteInt64
        public static PacketWriter operator +(PacketWriter a, long b)
        {
            PacketWriter message = a;

            message.Write(b);

            return message;
        }

        public PacketWriter WriteUInt8(byte data)
        {
            base.Write(data);
            return this;
        }

        // WriteUInt8
        public static PacketWriter operator +(PacketWriter a, byte b)
        {
            PacketWriter message = a;

            message.Write(b);

            return message;
        }

        public PacketWriter WriteUInt16(ushort data)
        {
            base.Write(data);
            return this;
        }

        // WriteUInt16
        public static PacketWriter operator +(PacketWriter a, ushort b)
        {
            PacketWriter message = a;

            message.Write(b);

            return message;
        }

        public PacketWriter WriteUInt32(uint data)
        {
            base.Write(data);
            return this;
        }

        // WriteUInt32
        public static PacketWriter operator +(PacketWriter a, uint b)
        {
            PacketWriter message = a;

            message.Write(b);

            return message;
        }

        public PacketWriter WriteUInt64(ulong data)
        {
            base.Write(data);
            return this;
        }

        // WriteUInt64
        public static PacketWriter operator +(PacketWriter a, ulong b)
        {
            PacketWriter message = a;

            message.Write(b);

            return message;
        }

        public PacketWriter WriteFloat(float data)
        {
            base.Write(data);
            return this;
        }

        // WriteFloat
        public static PacketWriter operator +(PacketWriter a, float b)
        {
            PacketWriter message = a;

            message.Write(b);

            return message;
        }

        public PacketWriter WriteDouble(double data)
        {
            base.Write(data);
            return this;
        }

        // WriteDouble
        public static PacketWriter operator +(PacketWriter a, double b)
        {
            PacketWriter message = a;

            message.Write(b);

            return message;
        }

        public PacketWriter WriteString(string data)
        {
            byte[] sBytes = Encoding.ASCII.GetBytes(data);
            this.WriteBytes(sBytes);
            base.Write((byte)0);    // String null terminated
            return this;
        }

        // WriteString
        public static PacketWriter operator +(PacketWriter a, string b)
        {
            PacketWriter message = a;

            byte[] stringBytes = Encoding.ASCII.GetBytes(b);
            message += stringBytes;
            message.Write((byte)0); // Null-terminate string

            return message;
        }

        public PacketWriter WriteBytes(byte[] data)
        {
            base.Write(data);
            return this;
        }

        // WriteBytes
        public static PacketWriter operator +(PacketWriter a, byte[] b)
        {
            PacketWriter message = a;

            message.Write(b);

            return message;
        }

        // WriteBytes
        public static PacketWriter operator +(PacketWriter a, List<byte> b)
        {
            PacketWriter message = a;

            message.WriteBytes(b.ToArray());

            return message;
        }

        public static PacketWriter operator +(PacketWriter a, PacketWriter b)
        {
            PacketWriter message = a;

            message.WriteBytes(b.ReadDataToSend());

            return message;
        }

        public void Compress()
        {
            if (Opcode != OpCode.SMSG_UPDATE_OBJECT || BaseStream.Length <= 100)
                return;

            byte[] outBuffer = null;
            int baseSize = (int)BaseStream.Length - 6; // subtract header size

            try
            {
                using (MemoryStream outputStream = new MemoryStream())
                {
                    using (DeflaterOutputStream compressedStream = new DeflaterOutputStream(outputStream))
                    {
                        byte[] data = ReadDataToSend();
                        compressedStream.Write(data, 6, data.Length - 6);
                        compressedStream.Flush();
                        compressedStream.Close();
                        outBuffer = outputStream.ToArray();
                    }
                }
            }
            catch { return; }

            if (outBuffer.Length >= baseSize)
                return;

            BaseStream.Seek(0, SeekOrigin.Begin); // Reset packet
            BaseStream.SetLength(0);

            Opcode = OpCode.SMSG_COMPRESSED_UPDATE_OBJECT;
            WritePacketHeader();
            WriteInt32(baseSize);
            WriteBytes(outBuffer);
        }
    }
}
