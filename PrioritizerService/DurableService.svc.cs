using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.ServiceModel.Description;

namespace Online
{
    [Serializable]
    [DurableService]
    public class DurableService : IDurableService
    {
        static int currentValue = default(int);
        [DurableOperation(CanCreateInstance = true)]
        public void DoWork()
        {
            currentValue++;
        }

        [DurableOperation(CompletesInstance = true)]
        public void EndPersistence()
        {
        }
    }
}
