using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace MyAgencyVault.BusinessLibrary.BusinessObjects
{
    [DataContract]
    public class ComListObject
    {
        [DataMember]
        public string CheckAmountString { get; set; }
        [DataMember]
        public string HouseString { get; set; }
        [DataMember]
        public string RemainingString { get; set; }
        [DataMember]
        public string DonePerString { get; set; }
        [DataMember]
        public int? Entries { get; set; }
        [DataMember]
        public string PayorNickName { get; set; }
    }
}
