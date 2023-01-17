using ICSharpCode.SharpZipLib.Zip.Compression.Streams;
using System.IO;
using System.Text;
using System.Xml.Xsl;
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

        public void WriteInt8(sbyte data)
        {
            base.Write(data);
        }

        // WriteInt8
        public static PacketWriter operator +(PacketWriter a, sbyte b)
        {
            PacketWriter message = a;

            message.Write(b);

            return message;
        }

        public void WriteInt16(short data)
        {
            base.Write(data);
        }

        // WriteInt16
        public static PacketWriter operator +(PacketWriter a, short b)
        {
            PacketWriter message = a;

            message.Write(b);

            return message;
        }

        public void WriteInt32(int data)
        {
            base.Write(data);
        }

        // WriteInt32
        public static PacketWriter operator +(PacketWriter a, int b)
        {
            PacketWriter message = a;

            message.Write(b);

            return message;
        }

        public void WriteInt64(long data)
        {
            base.Write(data);
        }


        /// WriteInt64
        public static PacketWriter operator +(PacketWriter a, long b)
        {
            PacketWriter message = a;

            message.Write(b);

            return message;
        }

        public void WriteUInt8(byte data)
        {
            base.Write(data);
        }

        // WriteUInt8
        public static PacketWriter operator +(PacketWriter a, byte b)
        {
            PacketWriter message = a;

            message.Write(b);

            return message;
        }

        public void WriteUInt16(ushort data)
        {
            base.Write(data);
        }

        // WriteUInt16
        public static PacketWriter operator +(PacketWriter a, ushort b)
        {
            PacketWriter message = a;

            message.Write(b);

            return message;
        }

        public void WriteUInt32(uint data)
        {
            base.Write(data);
        }

        // WriteUInt32
        public static PacketWriter operator +(PacketWriter a, uint b)
        {
            PacketWriter message = a;

            message.Write(b);

            return message;
        }

        public void WriteUInt64(ulong data)
        {
            base.Write(data);
        }

        // WriteUInt64
        public static PacketWriter operator +(PacketWriter a, ulong b)
        {
            PacketWriter message = a;

            message.Write(b);

            return message;
        }

        public void WriteFloat(float data)
        {
            base.Write(data);
        }

        // WriteFloat
        public static PacketWriter operator +(PacketWriter a, float b)
        {
            PacketWriter message = a;

            message.Write(b);

            return message;
        }

        public void WriteDouble(double data)
        {
            base.Write(data);
        }

        // WriteDouble
        public static PacketWriter operator +(PacketWriter a, double b)
        {
            PacketWriter message = a;

            message.Write(b);

            return message;
        }

        public void WriteString(string data)
        {
            byte[] sBytes = Encoding.ASCII.GetBytes(data);
            this.WriteBytes(sBytes);
            base.Write((byte)0);    // String null terminated
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

        public void WriteBytes(byte[] data)
        {
            base.Write(data);
        }

        // WriteBytes
        public static PacketWriter operator +(PacketWriter a, byte[] b)
        {
            PacketWriter message = a;

            message.Write(b);

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
