using AlphaCore_Sharp.Game.World.Abstractions;
using AlphaCore_Sharp.Network.Packet;
using static AlphaCore_Sharp.Utils.Constants.ObjectCodes;
using static AlphaCore_Sharp.Utils.Constants.UpdateFields;

namespace AlphaCore_Sharp.Game.World.Managers.Objects
{
    // TODO: Major refactor of this class, this is solely to get enter world working.
    internal class ObjectManager
    {
        public ulong GUID { get; set; }
        public int Entry { get; set; }
        public ObjectTypes[] ObjectType { get; set; }
        public float WalkSpeed { get; set; }
        public float RunningSpeed { get; set; }
        public float SwimSpeed { get; set; }
        public float TurnRate { get; set; }
        public int MovementFlags { get; set; }
        public int UnitFlags { get; set; }
        public int DynamicFlags { get; set; }
        public int ShapeshiftForm { get; set; }
        public int DisplayID { get; set; }
        public float Scale { get; set; }
        public float BoundingRadius { get; set; }
        public Vector Location { get; set; }
        public int TransportID { get; set; }
        public Vector Transport { get; set; }
        public float TransportOrientation { get; set; }
        public float Orientation { get; set; }
        public float Pitch { get; set; }
        public int Zone { get; set; }
        public int Map { get; set; }

        public ObjectManager(
            ulong guid = 0, 
            int entry = 0, 
            float walkSpeed = 2.5f, 
            float runningSpeed = 7.0f, 
            float swimSpeed = 4.72222223f, 
            float turnRate = (float)Math.PI, 
            int movementFlags = 0, 
            int unitFlags = 0, 
            int dynamicFlags = 0, 
            int shapeshiftForm = 0, 
            int displayID = 0, 
            float scale = 1, 
            float boundingRadius = Globals.Unit.BOUNDING_RADIUS, 
            Vector location = null, 
            int transportID = 0, 
            Vector transport = null, 
            float transportOrientation = 0, 
            float orientation = 0, 
            float pitch = 0, 
            int zone = 0, 
            int map = 0)
        {
            GUID = guid;
            Entry = entry;
            ObjectType = new ObjectTypes[] { ObjectTypes.TYPE_OBJECT };
            WalkSpeed = walkSpeed;
            RunningSpeed = runningSpeed;
            SwimSpeed = swimSpeed;
            TurnRate = turnRate;
            MovementFlags = movementFlags;
            UnitFlags = unitFlags;
            DynamicFlags = dynamicFlags;
            ShapeshiftForm = shapeshiftForm;
            DisplayID = displayID;
            Scale = scale;
            BoundingRadius = boundingRadius;
            Location = location ?? new Vector();
            TransportID = transportID;
            Transport = transport ?? new Vector();
            TransportOrientation = transportOrientation;
            Orientation = orientation;
            Pitch = pitch;
            Zone = zone;
            Map = map;
        }

        public int GetObjectTypeValue()
        {
            int typeValue = 0;
            foreach (ObjectTypes type_ in ObjectType)
            {
                typeValue |= (int)type_;
            }
            return typeValue;
        }

        public int GetUpdateMask()
        {
            int mask = 0;

            if (ObjectType.Contains(ObjectTypes.TYPE_CONTAINER))
                mask += (int)ContainerFields.CONTAINER_END;
            if (ObjectType.Contains(ObjectTypes.TYPE_ITEM))
                mask += (int)ItemFields.ITEM_END;
            if (ObjectType.Contains(ObjectTypes.TYPE_PLAYER))
                mask += (int)PlayerFields.PLAYER_END;
            if (ObjectType.Contains(ObjectTypes.TYPE_UNIT))
                mask += (int)UnitFields.UNIT_END;
            if (ObjectType.Contains(ObjectTypes.TYPE_OBJECT))
                mask += (int)ObjectFields.OBJECT_END;
            if (ObjectType.Contains(ObjectTypes.TYPE_GAMEOBJECT))
                mask += (int)GameObjectFields.GAMEOBJECT_END;

            return (mask + 31) / 32;
        }

        public PacketWriter CreateUpdatePacket(UpdateTypes updateType)
        {
            int updateMask = GetUpdateMask();

            PacketWriter packet = new PacketWriter();
            packet += 1;
            packet += 2;
            packet += GUID;
            packet += (byte)updateType;
            packet += TransportID;
            packet += Transport.X;
            packet += Transport.Y;
            packet += Transport.Z;
            packet += TransportOrientation;
            packet += Location.X;
            packet += Location.Y;
            packet += Location.Z;
            packet += Orientation;
            packet += Pitch;
            packet += MovementFlags;
            packet += 0;
            packet += WalkSpeed;
            packet += RunningSpeed;
            packet += SwimSpeed;
            packet += TurnRate;
            packet += 1;
            packet += ObjectType.Contains(ObjectTypes.TYPE_PLAYER) ? 1 : 0;
            packet += 0;
            packet += 0; // Victim GUID, add later
            packet += updateMask;

            for (int i = 0; i < updateMask; i++)
            {
                packet += 0xFFFFFFFF;
            }

            return packet;
        }
    }
}
