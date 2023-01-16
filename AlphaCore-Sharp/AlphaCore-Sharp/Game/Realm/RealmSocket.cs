using AlphaCore_Sharp.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace AlphaCore_Sharp.Game.Realm
{
    internal class RealmSocket
    {
        public bool ListenRealmSocket = true;
        public bool ListenProxySocket = true;
        private TcpListener _realmListener;
        private TcpListener _proxyListener;

        public bool Start()
        {
            // Start TCP Listeners for realmlist and realm -> world proxy.
            try
            {
                // Create new TcpListeners which wait for socket connection attempts to realm and proxy servers.
                _realmListener = new TcpListener(IPAddress.Parse(Globals.Realm.SERVER_IP), Globals.Realm.REALM_PORT);
                _realmListener.Start();

                _proxyListener = new TcpListener(IPAddress.Parse(Globals.Realm.SERVER_IP), Globals.Realm.PROXY_PORT);
                _proxyListener.Start();

                return true;
            }
            catch (Exception ex)
            {
                // Abort server startup on error.
                Logger.Error($"{ex.Message}\n");

                return false;
            }
        }

        public void StartRealmThread()
        {
            // Start a new thread that waits for realmlist connections.
            new Thread(AcceptRealmConnection).Start();
        }

        public void StartProxyThread()
        {
            // Start a new thread that waits for realm (realm -> world) proxy connections.
            new Thread(AcceptProxyConnection).Start();
        }

        protected void AcceptRealmConnection()
        {
            // Continuously accept realmlist connections.
            while (ListenRealmSocket)
            {
                // If there is a pending connection, create a new realm manager and accept it.
                Thread.Sleep(1);
                if (_realmListener.Pending())
                {
                    RealmManager realm = new RealmManager();
                    realm.RealmSocket = _realmListener.AcceptSocket();

                    // Start a new thread and receive realmlist connections on the socket that was just opened.
                    Thread recvThread = new Thread(realm.ReceiveRealm);
                    recvThread.Start();
                }
            }
        }

        protected void AcceptProxyConnection()
        {
            // Continuously accept realm -> world proxy connections.
            while (ListenProxySocket)
            {
                // If there is a pending connection, create a new realm manager and accept it.
                Thread.Sleep(1);
                if (_proxyListener.Pending())
                {
                    RealmManager proxy = new RealmManager();
                    proxy.ProxySocket = _proxyListener.AcceptSocket();

                    // Start a new thread and receive realm -> world proxy connections on socket that was just opened.
                    Thread recvThread = new Thread(proxy.ReceiveProxy);
                    recvThread.Start();
                }
            }
        }
        
        public void Dispose()
        {
            // Stop all packet/socket listening.
            ListenRealmSocket = false;
            ListenProxySocket = false;
            _realmListener.Stop();
            _proxyListener.Stop();
        }
    }
}
