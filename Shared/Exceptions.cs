using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Prioritizer.Shared.Model;
using System.Runtime.Serialization;

namespace Prioritizer.Shared
{
    public class PrioritizerExceptionBase : Exception
    {
        public PrioritizerExceptionBase(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }

    public class PrioritizerDisconnectException : PrioritizerExceptionBase
    {
        const string message = "Client is disconnected currently";
        public PrioritizerDisconnectException(Exception innerException)
            : base(String.Format("{0}", string.Format(message, Environment.NewLine)), innerException)
        {
        }

    }
    public class PrioritizerExceptionPrivateTaskInMeeting : PrioritizerExceptionBase
    {
        const string message = "Private task can't be linked to meeting{0}Task '{1}'";
        public PrioritizerExceptionPrivateTaskInMeeting(Tasks t, Exception innerException)
            : base(String.Format("{0}", string.Format(message, Environment.NewLine, t.taskName)), innerException)
        {
        }

    }

    [DataContract]
    public class TaskSaveFailure
    {
        [DataMember]
        public Tasks Task { set; get; }
        [DataMember]
        public string ExceptionMessage { set; get; }
        [DataMember]
        public string ExceptionType { set; get; }
    }


}
