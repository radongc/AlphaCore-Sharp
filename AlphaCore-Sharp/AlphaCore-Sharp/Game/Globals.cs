namespace AlphaCore_Sharp.Game
{
    // TODO: Move all of this to a config file outside the core code base.
    internal class Globals
    {
        internal class Database
        {
            public const string MYSQL_IP = "127.0.0.1";
            public const string MYSQL_USER = "root";
            public const string MYSQL_PASS = "root";

            public const string DBC_DB_NAME = "alpha_dbc";
            public const string REALM_DB_NAME = "alpha_realm";
            public const string WORLD_DB_NAME = "alpha_world";
        }
        internal class Realm
        {
            public const string SERVER_IP = "127.0.0.1";
            public const int REALM_PORT = 9100;
            public const int PROXY_PORT = 9090;
            public const int WORLD_PORT = 8100;
            public const int CLIENT_VERSION = 3368;
            public const string REALM_NAME = "|c0000FF00AlphaCore Sharp";
        }

        internal class World
        {
            internal class Gameplay
            {
                public const float GAME_SPEED = 0.016666668f;
            }
        }

        internal class Unit
        {
            public const float BOUNDING_RADIUS = 0.388999998569489f;
            public const float COMBAT_REACH = 1.5f;
            public const int BASE_ATTACK_TIME = 2000;
            public const int OFFHAND_ATTACK_TIME = 1000;
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
