using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace MyAgencyVault.BusinessLibrary
{
    [DataContract]
    public class ClientCountObject
    {
        [DataMember]
        public int ActivePolicyClientCount { get; set; }

        [DataMember]
        public int TotalClientCount { get; set; }
        [DataMember]
        public int PendingPolicyClientCount { get; set; }
        [DataMember]
        public int TerminatePolicyClientCount { get; set; }
        [DataMember]
        public int WithoutPolicyClientCount { get; set; }
        [DataMember]
        public int SelectedClientCount { get; set; }

    }
}
