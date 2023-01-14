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
        internal class Realm
        {
            public const string SERVER_IP = "127.0.0.1";
            public const int REALM_PORT = 9100;
            public const int PROXY_PORT = 9090;
            public const int WORLD_PORT = 8100;
            public const int CLIENT_VERSION = 3368;
            public const string REALM_NAME = "|c0000FF00AlphaCore Sharp";
        }

        internal class Settings
        {
            // TODO: Remove this and replace with db-stored accounts.
            public const string TEMP_ACCOUNT_USER = "admin";
            // TEMP_ACCOUNT_PASS = "admin".
            public const string TEMP_ACCOUNT_PASS = "8c6976e5b5410415bde908bd4dee15dfb167a9c873fc4bb8a81f6f2ab448a918";
        }

        internal class Logging
        {
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
}
