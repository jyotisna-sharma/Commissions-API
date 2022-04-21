using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace MyAgencyVault.BusinessLibrary
{
    [DataContract]
    public class UserListObject
    {
        [DataMember]
        public string NickName { get; set; }

        [DataMember]
        public string FirstName { get; set; }
        [DataMember]
        public string LastName { get; set; }
        [DataMember]
        public string UserName { get; set; }
        [DataMember]
        public string Email { get; set; }
        [DataMember]
        public bool IsHouseAccount { get; set; }
        [DataMember]
        public string CreatedDate { get; set; }
        public string action = "action";
        [DataMember]
        public string Action { get { return action; } set { action = value; } }
        [DataMember]
        public string UserCredentialID { get; set; }

        [DataMember]
        public bool? IsAdmin { get; set; }
        [DataMember]
        public decimal FirstYearDefault { get; set; }
        [DataMember]
        public decimal RenewalDefault { get; set; }

    }

    public class DataEntryUserListObject
    {
        [DataMember]
        public string UserName { get; set; }

        [DataMember]
        public string FirstName { get; set; }
        [DataMember]
        public string LastName { get; set; }


        [DataMember]
        public string CreatedDate { get; set; }
    }
   
}
