// See https://aka.ms/new-console-template for more information
// dotnet publish -r win-x64 -c Release in powershell to build (native).
using AlphaCore_Sharp.Game;
using AlphaCore_Sharp.Game.Realm;
using AlphaCore_Sharp.Game.World;
using AlphaCore_Sharp.Game.World.OpcodeHandling;
using AlphaCore_Sharp.Network.Packet;
using AlphaCore_Sharp.Utils;
using AlphaCore_Sharp.Utils.Constants;
using System.Net;

// AlphaCore# (Sharp)
// WoW 0.5.3 3368 Emulator. Boilerplate socket/network implementation based closely on Barncastle's Alpha-WoW, the rest of emulator based closely on The-Alpha-Project/alpha-core.
namespace AlphaCore_Sharp
{
    class WorldServer
    {
        static void Main()
        {
            Logger.Message("AlphaCore#");
            Logger.Message("WoW 0.5.3 (3368) - Alpha Emulator");
            Logger.Message("Written in Native C# with NativeAOT.");
            Logger.Message("Based on The-Alpha-Project/alpha-core (Python) and barncastle/Alpha-WoW (.NET IL).\n");

            // TODO: Add detailed comments explaning step by step the realm, proxy, and world socket connection process.
            // TODO: Review boilerplate socket code and make recommended changes (to start off, we are nearly 1:1 with Alpha-WoW's base server code.)
            RealmManager.RealmSocketSession = new RealmSocket();
            WorldManager.WorldSocketSession = new WorldSocket();
            if (WorldManager.WorldSocketSession.Start() && RealmManager.RealmSocketSession.Start())
            {
                RealmManager.RealmSocketSession.StartRealmThread();
                RealmManager.RealmSocketSession.StartProxyThread();
                Logger.Success($"Realm Proxy listening on {Globals.Realm.SERVER_IP} port {Globals.Realm.REALM_PORT}/{Globals.Realm.PROXY_PORT}");

                WorldManager.WorldSocketSession.StartConnectionThread();
                Logger.Success($"World Server listening on {Globals.Realm.SERVER_IP} port {Globals.Realm.WORLD_PORT}\n");

                // Setup packet handlers.
                HandlerDefinitions.InitializePacketHandlers();
            }
            else
            {
                Logger.Error("World Server couldn't start.");
            }

            // Free memory.
            GC.Collect();
            Logger.Info($"Total memory usage: {Convert.ToSingle(GC.GetTotalMemory(false) / 1024 / 1024)}MB.\n");

            // TODO: Initialize commands.
        }
    }
}

// TODO: Add comments explaining the full process of client connection in RealmSocket, WorldSocket, RealmManager, and WorldManager.
// TODO: Choose a good ORM (or write your own, not recommended) and start adding database models.
// TODO: Start loading data from database after ORM is chosen and implemented.
// TODO: Make sure account auth process is working properly and start writing opcode handlers.