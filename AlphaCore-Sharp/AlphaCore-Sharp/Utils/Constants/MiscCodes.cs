using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlphaCore_Sharp.Utils.Constants
{
    internal class MiscCodes
    {
        public enum HighGuid : int
        {
            HIGHGUID_PLAYER = 0x0000 << 48,
            HIGHGUID_ITEM = 0x4000 << 48,
            HIGHGUID_CONTAINER = 0x4000 << 48,
            HIGHGUID_GAMEOBJECT = 0xF110 << 48,
            HIGHGUID_TRANSPORT = 0xF120 << 48,
            HIGHGUID_UNIT = 0xF130 << 48,
            HIGHGUID_PET = 0xF140 << 48,
            HIGHGUID_VEHICLE = 0xF150 << 48,
            HIGHGUID_DYNAMICOBJECT = 0xF100 << 48,
            HIGHGUID_CORPSE = 0xF101 << 48,
            HIGHGUID_MO_TRANSPORT = 0x1FC0 << 48,
            HIGHGUID_GROUP = 0x1F50 << 48,
            HIGHGUID_GUILD = 0x1FF6 << 48
        }
    }
}
