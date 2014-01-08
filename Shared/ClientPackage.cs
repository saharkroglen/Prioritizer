using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace Prioritizer.Shared
{
    [DataContract(IsReference = true)]
    public class ClientPackage
    {
        [DataMember]
        public byte[] bin
        {
            get;
            set;
        }

        [DataMember]
        public string binName
        {
            get;
            set;
        }
    }
}