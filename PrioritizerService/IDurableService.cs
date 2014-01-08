using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Online
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IDurableService" in both code and config file together.
    //[ServiceContract(SessionMode = SessionMode.NotAllowed)]
    [ServiceContract(SessionMode = SessionMode.Allowed)]
    public interface IDurableService
    {
        [OperationContract]
        void DoWork();

        [OperationContract]
        void EndPersistence();
    }
}
