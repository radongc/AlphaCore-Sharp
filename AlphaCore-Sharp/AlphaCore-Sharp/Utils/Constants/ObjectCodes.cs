﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlphaCore_Sharp.Utils.Constants
{
    internal class ObjectCodes
    {
        public enum ObjectTypes
        {
            TYPE_OBJECT = 1,
            TYPE_ITEM = 2,
            TYPE_CONTAINER = 6,
            TYPE_UNIT = 8,
            TYPE_PLAYER = 16,
            TYPE_GAMEOBJECT = 32,
            TYPE_DYNAMICOBJECT = 64,
            TYPE_CORPSE = 128,
            TYPE_AIGROUP = 256,
            TYPE_AREATRIGGER = 512
        }
    }
}
