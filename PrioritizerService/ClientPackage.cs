using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace PrioritizerService
{
    [DataContract(IsReference = true)]
    public class ClientPackage
    {
        [DataMember]
        public byte[] zip
        {
            get;
            set;
        }
        
    }
}