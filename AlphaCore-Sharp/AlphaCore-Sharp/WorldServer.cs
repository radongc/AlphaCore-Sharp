// See https://aka.ms/new-console-template for more information
// dotnet publish -r win-x64 -c Release in powershell to build (native).
using AlphaCore_Sharp.Game.Realm;
using AlphaCore_Sharp.Game.World;
using AlphaCore_Sharp.Network.Packet;
using AlphaCore_Sharp.Utils.Constants;

namespace AlphaCore_Sharp
{
    class WorldServer
    {
        static void Main()
        {
            new Thread(LoginServerSessionHandler.Start).Start();
            new Thread(ProxyServerSessionHandler.Start).Start();
            new Thread(WorldManager.Start).Start();
        }
    }
}