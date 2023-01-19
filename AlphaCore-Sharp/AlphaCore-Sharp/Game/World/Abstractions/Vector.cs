using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlphaCore_Sharp.Game.World.Abstractions
{
    internal class Vector
    {
        public float X { get; set; } 
        public float Y { get; set; } 
        public float Z { get; set; }

        public Vector()
        {
            this.X = 0;
            this.Y = 0;
            this.Z = 0;
        }

        public Vector(float x, float y, float z)
        {
            this.X= x;
            this.Y= y;
            this.Z= z;
        }
    }
}
