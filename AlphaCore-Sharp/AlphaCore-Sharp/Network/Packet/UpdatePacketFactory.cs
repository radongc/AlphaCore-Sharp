using System;
using static AlphaCore_Sharp.Utils.Constants.ObjectCodes;
using static AlphaCore_Sharp.Utils.Constants.UpdateFields;

namespace AlphaCore_Sharp.Network.Packet
{
    internal class UpdatePacketFactory
    {
        public ObjectTypes[] TypesList;

        public List<byte> ObjectValues;
        public List<byte> ItemValues;
        public List<byte> ContainerValues;
        public List<byte> UnitValues;
        public List<byte> PlayerValues;
        public List<byte> GameobjectValues;
        public List<byte> DynamicObjectValues;

        public List<byte> UpdatePacket;

        public UpdatePacketFactory(ObjectTypes[] typesList)
        {
            TypesList = typesList;

            ObjectValues = new List<byte>();
            ItemValues = new List<byte>();
            ContainerValues = new List<byte>();
            UnitValues = new List<byte>();
            PlayerValues = new List<byte>();
            GameobjectValues = new List<byte>();
            DynamicObjectValues = new List<byte>();
        }

        // TODO: Fix this. Stack overflow.
        public void Update<T>(ref List<byte> valueList, int position, T value)
        {
            if (value is Int64)
            {
                Update(ref valueList, position, Convert.ToInt64(value) & 0xFFFFFFFF);
                Update(ref valueList, position, Convert.ToInt64(value) & 0xFFFFFFFF);
            }
            else
            {
                PacketWriter bytes = new PacketWriter();

                if (value is byte)
                    bytes += Convert.ToByte(value);
                else if (value is sbyte)
                    bytes += Convert.ToSByte(value);
                else if (value is short)
                    bytes += Convert.ToInt16(value);
                else if (value is ushort)
                    bytes += Convert.ToUInt16(value);
                else if (value is int)
                    bytes += Convert.ToInt32(value);
                else if (value is uint)
                    bytes += Convert.ToUInt32(value);
                else if (value is long)
                    bytes += Convert.ToInt64(value);
                else if (value is ulong)
                    bytes += Convert.ToUInt64(value);
                else if (value is string)
                    bytes += Convert.ToString(value);
                else
                    return;

                int length = bytes.ReadDataToSend().Length;
                PacketReader reader = new PacketReader(bytes.ReadDataToSend(), worldPacket: false);

                for (int i = 0; i < length; i++)
                {
                    valueList.Insert(position, bytes.ReadDataToSend()[i]);
                }
            }
        }

        public void InitLists()
        {
            ObjectValues.Add((byte)(Convert.ToByte(0) * (int)ObjectFields.OBJECT_END));
            ItemValues.Add((byte)(Convert.ToByte(0) * (int)ItemFields.ITEM_END));
            ContainerValues.Add((byte)(Convert.ToByte(0) * (int)ContainerFields.CONTAINER_END));
            UnitValues.Add((byte)(Convert.ToByte(0) * (int)UnitFields.UNIT_END));
            PlayerValues.Add((byte)(Convert.ToByte(0) * (int)PlayerFields.PLAYER_END));
            GameobjectValues.Add((byte)(Convert.ToByte(0) * (int)GameObjectFields.GAMEOBJECT_END));
            DynamicObjectValues.Add((byte)(Convert.ToByte(0) * (int)DynamicObjectFields.DYNAMICOBJECT_END));
        }

        public PacketWriter BuildPacket()
        {
            PacketWriter pkt = new PacketWriter();

            if (TypesList.Contains(ObjectTypes.TYPE_OBJECT))
                pkt += ObjectValues;
            if (TypesList.Contains(ObjectTypes.TYPE_UNIT))
                pkt += UnitValues;
            if (TypesList.Contains(ObjectTypes.TYPE_PLAYER))
                pkt += PlayerValues;
            if (TypesList.Contains(ObjectTypes.TYPE_ITEM))
                pkt += ItemValues;
            if (TypesList.Contains(ObjectTypes.TYPE_CONTAINER))
                pkt += ContainerValues;
            if (TypesList.Contains(ObjectTypes.TYPE_GAMEOBJECT))
                pkt += GameobjectValues;
            if (TypesList.Contains(ObjectTypes.TYPE_DYNAMICOBJECT))
                pkt += DynamicObjectValues;

            InitLists();
            return pkt;
        }
    }
}
