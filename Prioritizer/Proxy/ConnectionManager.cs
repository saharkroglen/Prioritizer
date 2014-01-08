using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PrioritizerService.Model;
using System.Threading.Tasks;
using System.Configuration;
using Prioritizer.Shared;
using Shared;
using System.Timers;

namespace Prioritizer.Proxy
{
    public static class ConnectionManager
    {
        private static PrioritizerServiceClient _proxyClient;
        private static bool _isAlive = false;
        private static Int32 PING_TIMEOUT = Convert.ToInt32(ConfigurationManager.AppSettings["pingTimeout"]);
        private static Int32 PING_INTERVAL = Convert.ToInt32(ConfigurationManager.AppSettings["pingInterval"]);
        private static Guid _loggedInUser = Guid.Empty;
        private static Timer _heartbeatTimer = new Timer(PING_INTERVAL);

        public delegate void ConnectionStateChange(bool Connected);
        public static event ConnectionStateChange OnConnectionStateChange;

        public delegate void ServerMessageArrival(ClientMessage message);
        public static event ServerMessageArrival OnServerMessage;

        public static void SetConnectionIdentity(Guid loggedInUser)
        {
            if (_loggedInUser == Guid.Empty && loggedInUser != Guid.Empty)
                _loggedInUser = loggedInUser;
        }

        public static void init()
        {
            _heartbeatTimer.Elapsed += new ElapsedEventHandler(_heartbeatTimer_Elapsed);
            _heartbeatTimer.Start();
            initProxy(PING_TIMEOUT);
        }

        public static void CheckConnection()
        {
            initProxy(PING_TIMEOUT);
        }

        static void _heartbeatTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            initProxy(PING_TIMEOUT);
        }

        public static PrioritizerServiceClient Proxy
        {
            get
            {
                if (IsAlive)
                {
                    if (_proxyClient == null)
                        initProxy(PING_TIMEOUT);
                    return _proxyClient;
                }
                else
                {
                    throw new PrioritizerDisconnectException(null);
                }
            }
        }

        public static bool IsAlive
        {
            set
            {
                if (_isAlive != value)
                {
                    _isAlive = value;
                    Logger.Instance.Info(string.Format("Connection state changed to {0}", _isAlive));
                    if (OnConnectionStateChange != null)
                    {
                        OnConnectionStateChange(_isAlive);
                    }                    
                }
            }
            get
            {
                return _isAlive;
            }
        }

        private static void initProxy(int timeout)
        {
            if (!IsAlive)
            {
                _proxyClient = new PrioritizerServiceClient();
                _proxyClient.InnerChannel.Faulted += new EventHandler(InnerChannel_Faulted);
            }
            Task pingTask = Task.Factory.StartNew(() => pingAsync(_loggedInUser));
            bool succeed = false;
            try
            {
                succeed = pingTask.Wait(timeout);
            }
            catch (Exception ex)
            {
            }
            finally
            {
                if (succeed && pingTask.Exception == null)
                {
                    IsAlive = true;
                    setHeartbeatInterval(PING_INTERVAL);
                }
                else
                {
                    IsAlive = false;
                    Logger.Instance.Warn(string.Format("Ping timeout: {0} ms", PING_TIMEOUT));
                    setHeartbeatInterval(PING_INTERVAL / 2);
                }
            }
            
        }

        private static DateTime _lastUpdate ;
        private static void pingAsync(Guid loggedInUserID)
        {
            try
            {
                ClientMessage message = _proxyClient.ping(loggedInUserID, _lastUpdate);

                if (message != null && OnServerMessage != null)
                {
                    _lastUpdate = message.UpdatedOn;
                    OnServerMessage(message);
                }

            }
            catch (Exception ex)
            {
                Logger.Instance.Error(string.Format("ping exception"), ex);
                IsAlive = false;
                throw ex;
            }
        }

        static void InnerChannel_Faulted(object sender, EventArgs e)
        {
            IsAlive = false;
        }

        //private static Task _connectionChecker = null;
        //private static void startConnectionWatchdog()
        //{
        //    if (!_inRecoveryMode)
        //    {
        //        _inRecoveryMode = true;
        //        _proxyClient = new PrioritizerServiceClient();
        //        _connectionChecker = Task.Factory.StartNew(connectionWatchdog,TaskCreationOptions.LongRunning);
        //    }
        //}

        //private static void connectionWatchdog()
        //{
        //    Logger.Instance.Info(string.Format("Connection watchdog started"));
        //    int retryCounter = 0;
        //    while (true)
        //    {
        //        Console.WriteLine("watchdog retry");
        //        initProxy(5000);
        //        retryCounter++;
        //        Logger.Instance.Info(string.Format("Connection watchdog try #{0}",retryCounter));
        //        if (IsAlive)
        //        {
        //            Logger.Instance.Info(string.Format("Connection re-established"));
        //            _inRecoveryMode = false;
                    
        //            return;
        //        }
        //    }

        //}

        private static void setHeartbeatInterval(int interval)
        {
            _heartbeatTimer.Interval = interval;
        }

        public static bool IsConnectionRelatedException(Exception ex)
        {
            string message = ex.Message.ToLower();
            if (message.Contains("cannot be used for communication because it is in the faulted") || message.Contains("no connection") || message.Contains("no dns entries exist") || message.Contains("could not connect to") || message.Contains("the target machine actively refused") || message.Contains("the socket connection was aborted"))
            {
                IsAlive = false;
                setHeartbeatInterval(PING_INTERVAL / 2);
                return true;
            }
            return false;
        }

    }
}
