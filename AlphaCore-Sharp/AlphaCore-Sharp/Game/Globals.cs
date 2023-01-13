using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlphaCore_Sharp.Game
{
    internal class Globals
    {
        // All should be moved to a Config file later.
        public const string SERVER_IP = "127.0.0.1";
        public const int REALM_PORT = 9100;
        public const int PROXY_PORT = 9090;
        public const int WORLD_PORT = 8100;

        /*
            Logging mask values:
            NONE = 0x00,
            SUCCESS = 0x01,
            INFO = 0x02,
            ANTICHEAT = 0x04,
            WARNING = 0x08,
            ERROR = 0x10,
            DEBUG = 0x20,
        */
        public const int LOGGING_MASK = 0x3f;
    }
}
