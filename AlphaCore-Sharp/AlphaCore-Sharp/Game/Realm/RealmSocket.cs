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
    // TODO: Add comments explaining this class.
    internal class RealmSocket
    {
        public bool ListenRealmSocket = true;
        public bool ListenProxySocket = true;
        private TcpListener _realmListener;
        private TcpListener _proxyListener;

        public bool Start()
        {
            try
            {
                _realmListener = new TcpListener(IPAddress.Parse(Globals.Realm.SERVER_IP), Globals.Realm.REALM_PORT);
                _realmListener.Start();

                _proxyListener = new TcpListener(IPAddress.Parse(Globals.Realm.SERVER_IP), Globals.Realm.PROXY_PORT);
                _proxyListener.Start();

                return true;
            }
            catch (Exception ex)
            {
                Logger.Error($"{ex.Message}\n");

                return false;
            }
        }

        public void StartRealmThread()
        {
            new Thread(AcceptRealmConnection).Start();
        }

        public void StartProxyThread()
        {
            new Thread(AcceptProxyConnection).Start();
        }

        protected void AcceptRealmConnection()
        {
            while (ListenRealmSocket)
            {
                Thread.Sleep(1);
                if (_realmListener.Pending())
                {
                    RealmManager realm = new RealmManager();
                    realm.RealmSocket = _realmListener.AcceptSocket();

                    Thread recvThread = new Thread(realm.ReceiveRealm);
                    recvThread.Start();
                }
            }
        }

        protected void AcceptProxyConnection()
        {
            while (ListenProxySocket)
            {
                Thread.Sleep(1);
                if (_proxyListener.Pending())
                {
                    RealmManager proxy = new RealmManager();
                    proxy.ProxySocket = _proxyListener.AcceptSocket();

                    Thread recvThread = new Thread(proxy.ReceiveProxy);
                    recvThread.Start();
                }
            }
        }
        
        public void Dispose()
        {
            ListenRealmSocket = false;
            ListenProxySocket = false;
            _realmListener.Stop();
            _proxyListener.Stop();
        }
    }
}
