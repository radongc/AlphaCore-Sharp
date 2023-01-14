using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlphaCore_Sharp.Game
{
    // TODO: Move all of this to a config file outside the core code base.
    internal class Globals
    {
        public const string SERVER_IP = "127.0.0.1";
        public const int REALM_PORT = 9100;
        public const int PROXY_PORT = 9090;
        public const int WORLD_PORT = 8100;

        /*  
            Add mask values together to customize which logs to show; ex. to only show SUCCESS (0x01) + INFO (0x02), mask should be 0x03. 
            Logging mask values:
            NONE = 0x00,
            SUCCESS = 0x01,
            INFO = 0x02,
            ANTICHEAT = 0x04,
            WARNING = 0x08,
            ERROR = 0x10,
            DEBUG = 0x20,
        */
        // 0x3f: Show all logs.
        public const int LOGGING_MASK = 0x3f;
    }
}
