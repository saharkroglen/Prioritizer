using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Prioritizer.Shared;
using System.IO;
using Newtonsoft.Json;
using Prioritizer.Shared.Model;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Hosting;
using System.Collections.Concurrent;
using Shared;

namespace PrioritizerService.Class
{

    public class ServerMessagesManager
    {
        private static readonly string RECENTLY_POKED_USERS_FILE = "_RecentlyPokedUsers.txt";
        private Dictionary<Guid, ClientMessage> _clientMessages = new Dictionary<Guid, ClientMessage>();
        private Queue<Guid> _dirtyUsersList = new Queue<Guid>();
        public List<Guid> RecentlyPokedUsersList = new List<Guid>();
        private object syncModify = new object();
        private object syncAdd = new object();

        #region Singleton

        private static ServerMessagesManager _instance = null;

        private ServerMessagesManager() { }

        public static ServerMessagesManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    ServerMessagesManager instance = new ServerMessagesManager();
                    instance.Initialize();
                    _instance = instance;
                }
                return _instance;
            }
        }

        #endregion


        private void Initialize()
        {
            if (!Directory.Exists(CLIENT_MESSAGES_FOLDER))
            {
                Directory.CreateDirectory(CLIENT_MESSAGES_FOLDER);
            }
            loadRecentPokes();
            SingletonTimer st = SingletonTimer.Instance;
        }

        private static string CLIENT_MESSAGES_FOLDER = string.Format(@"{0}\{1}", HostingEnvironment.MapPath(@"/App_Data"), "PrioriMessages");


        public ClientMessage GetMessages(Guid userID, DateTime lastUpdate)
        {
            ClientMessage message = getClientMessage(userID);
            if (message != null)
            {
                if (message.UpdatedOn != lastUpdate)
                {
                    return message;
                }
                //else  //user got the last client message according to its last update date then reset it.
                //{
                //    ClearClientMessage(userID);
                //}
            }
            return null;
        }


        public static void SendPokeByMail(Poke p)
        {
            Email pokeEmail = new Email();
            Users u = ServerUtils._usersDict[p.To];
            pokeEmail.To = u.email;
            pokeEmail.From = ServerUtils._usersDict[p.From].email;

            StringBuilder sb = new StringBuilder();

            switch (p.Type)
            {
                case enPokeType.Invoker:
                    pokeEmail.subject = string.Format("You got {0} from {1}", Utils.getMoodName(p.PokeMood), ServerUtils._usersDict[p.From].userName);
                    break;
                case enPokeType.Reply:
                    pokeEmail.subject = string.Format("Reply From {0}", ServerUtils._usersDict[p.From].userName);
                    break;
                case enPokeType.PlainMessage:
                    pokeEmail.subject = string.Format("Message from {0}", ServerUtils._usersDict[p.From].userName);
                    break;
            }

            pokeEmail.Body = p.Comment;

            EmailManager.Enqueue(pokeEmail);
        }

        public void Poke(Poke p)
        {
            Guid userID = p.To;
            ClientMessage clientMsg = getClientMessage(userID);
            if (clientMsg == null)
            {
                clientMsg = new ClientMessage() { UserID = userID, UpdatedOn = DateTime.UtcNow };
                _clientMessages.Add(userID, clientMsg);
            }

            if (p.SendEmail)
            {
                SendPokeByMail(p);
            }
            clientMsg.AddPoke(p);
            addToRecentlyPokedList(userID);
            AddMessageToDirtyQueue(userID);
        }

        private void addToRecentlyPokedList(Guid userID)
        {
            RecentlyPokedUsersList.Add(userID);
            SaveRecentPokes();
        }

        public void PokeReply(Poke p)
        {
            Guid userID = p.To;
            ClientMessage clientMsg = getClientMessage(userID);

            if (clientMsg == null)
            {
                clientMsg = new ClientMessage() { UserID = userID, UpdatedOn = DateTime.UtcNow };
                _clientMessages.Add(userID, clientMsg);
            }


            clientMsg.AddPoke(p);
            addToRecentlyPokedList(userID);
            AddMessageToDirtyQueue(userID);
        }

        private ClientMessage getClientMessage(Guid userID)
        {
            ClientMessage clientMsg = null;
            if (!_clientMessages.ContainsKey(userID))
            {
                string fileName = string.Format(@"{0}\{1}", CLIENT_MESSAGES_FOLDER, userID.ToString());
                if (File.Exists(fileName))
                {
                    string fileContent = Prioritizer.Shared.Utils.LoadFile(fileName);
                    clientMsg = JsonConvert.DeserializeObject<ClientMessage>(fileContent);
                }
                else
                {
                    return null;
                }
                lock (syncAdd)
                {
                    _clientMessages.Add(userID, clientMsg);
                }
            }
            else
            {
                clientMsg = _clientMessages[userID];
            }
            return clientMsg;
        }
        public void ClearClientMessage(Guid userID)
        {
            ClientMessage cleanMessage = new ClientMessage() { UserID = userID, UpdatedOn = DateTime.UtcNow };

            lock (syncModify)
            {
                _clientMessages[userID] = cleanMessage;
            }

            //clear the message for that client by creating a brand new message object, and saving to persistence
            AddMessageToDirtyQueue(userID);
        }

        public void AddMessageToDirtyQueue(Guid userID)
        {
          
            _dirtyUsersList.Enqueue(userID);
            startPersistenceWorker();
        }

        #region persistence workers saving client messages to disk

        private void startPersistenceWorker()
        {
            Task.Factory.StartNew(() => PersistenceWorker());
            _hasWorkToDoEvent.Set();
        }


        private static object persistenceQueueSync = new object();
        private static object workersCounterSync = new object();
        private static int NUM_OF_WORKERS = 2;
        private static int _workersCounter = 0;
        private static ManualResetEvent _hasWorkToDoEvent = new ManualResetEvent(false);

        private void PersistenceWorker()
        {
            try
            {
                lock (workersCounterSync)
                {
                    _workersCounter++;

                    if (_workersCounter > NUM_OF_WORKERS)
                    {
                        return;
                    }
                }
                Guid userID = Guid.Empty;
                while (true)
                {
                    if (_dirtyUsersList.Count == 0)
                        _hasWorkToDoEvent.WaitOne();

                    try
                    {
                        _hasWorkToDoEvent.Reset();

                        while (_dirtyUsersList.Count > 0)
                        {
                            lock (persistenceQueueSync)
                            {
                                if (_dirtyUsersList.Count > 0)
                                {
                                    userID = _dirtyUsersList.Dequeue();
                                    saveMessage(userID);
                                }
                            }
                        }

                    }
                    catch (Exception e)
                    {
                        Logger.Instance.Error(e.Message);
                        //throw e;
                    }
                }
            }
            finally
            {
                _workersCounter--;
            }
        }
        private void saveMessage(Guid userID)
        {
            if (_clientMessages.ContainsKey(userID))
            {
                ClientMessage m = _clientMessages[userID];

                string fileName = string.Format(@"{0}\{1}", CLIENT_MESSAGES_FOLDER, m.UserID.ToString());
                string jsonFormat = JsonConvert.SerializeObject(m, Formatting.Indented);
                Prioritizer.Shared.Utils.SaveFileContent(fileName, jsonFormat);
            }
        }
        public void SaveRecentPokes()
        {
            string fileName = string.Format(@"{0}\{1}", CLIENT_MESSAGES_FOLDER, RECENTLY_POKED_USERS_FILE);
            string jsonFormat = JsonConvert.SerializeObject(RecentlyPokedUsersList, Formatting.Indented);
            Prioritizer.Shared.Utils.SaveFileContent(fileName, jsonFormat);
        }
        private void loadRecentPokes()
        {
            string fileName = string.Format(@"{0}\{1}", CLIENT_MESSAGES_FOLDER, RECENTLY_POKED_USERS_FILE);
            if (File.Exists(fileName))
            {
                string fileContent = Prioritizer.Shared.Utils.LoadFile(fileName);
                RecentlyPokedUsersList = JsonConvert.DeserializeObject<List<Guid>>(fileContent);
            }
        }
        #endregion
    }

    internal class SingletonTimer
    {

        private static Timer _timer = null;
        private static SingletonTimer _Instance = null;
        private SingletonTimer ()
        {
        }
        public static SingletonTimer Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new SingletonTimer();
                    _Instance.initialize();
                }
                return _Instance;
            }
        }

        public void timer_tick(Object stateInfo)
        {
            Guid userID = Guid.Empty;
            for (int i = ServerMessagesManager.Instance.RecentlyPokedUsersList.Count - 1; i >=0 ; i-- )
            {
                bool userHasUndeliveredPokes = false;
                bool errorsOccured = false;
                bool messageWasChanged = false;
                userID = ServerMessagesManager.Instance.RecentlyPokedUsersList[i];
                ClientMessage m = ServerMessagesManager.Instance.GetMessages(userID, DateTime.UtcNow);
                
                if (m!= null && m.PokeList.Count > 0)
                {
                    
                    foreach (var p in m.PokeList)
                    {

                        if (DateTime.UtcNow.Subtract(p.SentOn) > new TimeSpan(0, 5, 0) && !p.SendEmail)
                        {
                            try
                            {
                                ServerMessagesManager.SendPokeByMail(p);
                                p.DeliveredByMailAfterTimeout = true;
                                messageWasChanged = true;
                            }
                            catch (Exception ex)
                            {
                                Logger.Instance.Error(ex);
                                errorsOccured = true;
                            }
                        }
                        else
                        {
                            userHasUndeliveredPokes = true;
                        }
                    }
                    if (messageWasChanged)
                        ServerMessagesManager.Instance.AddMessageToDirtyQueue(userID);
                }
                if (!userHasUndeliveredPokes && !errorsOccured)
                {
                    ServerMessagesManager.Instance.RecentlyPokedUsersList.RemoveAt(i);
                } 
            }
            ServerMessagesManager.Instance.SaveRecentPokes();
        }

        private void initialize()
        {
            TimerCallback tcb = timer_tick;
            _timer = new Timer(new TimerCallback(tcb),null,0,30000);
        }
    }

}
