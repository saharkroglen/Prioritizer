using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading;
//using System.Web.Mail;
using System.Net.Mail;
using System.Net;
using System.Threading.Tasks;

namespace PrioritizerService
{
    public static class EmailManager
    {

        private static Queue<Email> _emailCollection = new Queue<Email>();
       

        public static AutoResetEvent FinishedWorkEvent = new AutoResetEvent(false);

        static EmailManager()
        {
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network; //??
            smtpClient.Host = "smtp.gmail.com";
            smtpClient.Port = 587; //25
            smtpClient.EnableSsl = true;
            smtpClient.Credentials = new NetworkCredential("priorimanager@gmail.com", "1qaz!@#$");

        }
        public static void Enqueue(Email image)
        {
            _emailCollection.Enqueue(image);
            _hasWorkToDoEvent.Set();
            Task.Factory.StartNew(() => SenderWorker());
        }

        #region persistence workers sending emails
        private static object sync = new object();
        private static object sync1 = new object();
        private static int NUM_OF_WORKERS = 1;
        private static int _workersCounter = 0;
        private static ManualResetEvent _hasWorkToDoEvent = new ManualResetEvent(false);

        private static void SenderWorker()
        {
            try
            {
                lock (sync1)
                {
                    _workersCounter++;

                    if (_workersCounter > NUM_OF_WORKERS)
                    {
                        return;
                    }
                }
                while (true)
                {
                    if (_emailCollection.Count == 0)
                        _hasWorkToDoEvent.WaitOne();

                    try
                    {
                        _hasWorkToDoEvent.Reset();
                        Email email;
                        lock (sync)
                        {
                            if (_emailCollection.Count == 0)
                                continue;
                            else
                                email = _emailCollection.Dequeue();
                        }
                        sendEmail(email);
                    }
                    catch (Exception e)
                    {
                        //Logger.Instance.Error(e.Message);
                        throw e;
                    }

                }
            }
            finally
            {
                _workersCounter--;
            }
        }
        #endregion

        private static SmtpClient smtpClient = new SmtpClient();
        private static bool sendEmail(Email email)
        {
            MailMessage message = new MailMessage();
            //message.From = "Alert";
            message.IsBodyHtml = true;
            message.Sender = new MailAddress("PrioriManager@No-Reply.com");
            message.To.Add(new MailAddress(email.To));
            message.Subject = email.subject;
            message.Body = string.Format("{0}<br><br><br><br><br><br><br>{1}", email.Body, "------Please don't replay to this email------");
            message.IsBodyHtml = true;
            
            smtpClient.Send(message);

            return false;
        }
    }
    public struct Email
    {
        public string To { set; get; }
        public string From { set; get; }
        public string subject { set; get; }
        public string Body { set; get; }
    }
}