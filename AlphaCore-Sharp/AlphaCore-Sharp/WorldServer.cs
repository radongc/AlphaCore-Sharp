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
            Logger.Message("Based on The-Alpha-Project/alpha-core (Python) by Grender & others.\n");

            // TODO: Add detailed comments explaning step by step the realm, proxy, and world socket connection process.
            // TODO: Review boilerplate socket code and make recommended changes (to start off, we are nearly 1:1 with Alpha-WoW's base server code.)
            RealmManager.RealmSession = new RealmSocket();
            WorldManager.WorldSession = new WorldSocket();
            if (WorldManager.WorldSession.Start() && RealmManager.RealmSession.Start())
            {
                RealmManager.RealmSession.StartRealmThread();
                RealmManager.RealmSession.StartProxyThread();
                Logger.Info($"Realm Proxy listening on {Globals.SERVER_IP} port {Globals.REALM_PORT}/{Globals.PROXY_PORT}");
                Logger.Info("Realm Proxy successfully started!");

                WorldManager.WorldSession.StartConnectionThread();
                Logger.Info($"World Server listening on {Globals.SERVER_IP} port {Globals.WORLD_PORT}");
                Logger.Info("World Server successfully started!\n");

                // Setup packet handlers.
                HandlerDefinitions.InitializePacketHandlers();
            }
            else
            {
                Logger.Error("World Server couldn't start.");
            }

            // Free memory.
            GC.Collect();
            Logger.Info($"Total memory usage: {Convert.ToSingle(GC.GetTotalMemory(false) / 1024 / 1024)}MB.");

            // TODO: Initialize commands.
        }
    }
}

// TODO: Choose a good ORM (or write your own, not recommended) and start adding database models.
// TODO: Start loading data from database after ORM is chosen and implemented.
// TODO: Make sure account auth process is working properly and start writing opcode handlers.