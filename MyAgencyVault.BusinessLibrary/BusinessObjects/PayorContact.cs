using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace MyAgencyVault.BusinessLibrary.BusinessObjects
{
    [DataContract]
    public class PayorContact
    {
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public Guid PayorContactId { get; set; }
        [DataMember]
        public Guid GlobalPayorId { get; set; }
        [DataMember]
        public string FirstName { get; set; }

        [DataMember]
        public string Email { get; set; }

        [DataMember]
        public string LastName { get; set; }

        [DataMember]
        public int? Priority { get; set; }
        [DataMember]
        public string ContactPref { get; set; }
        [DataMember]
        public bool IsDeleted { get; set; }

        [DataMember]
        public ContactDetails OfficePhone { get; set; }

        [DataMember]
        public ContactDetails Fax { get; set; }
       // [DataMember]
        //public string OfficePhone_DialCode { get; set; }
        //[DataMember]
        //public string Fax_DialCode { get; set; }
        //[DataMember]
        //public string OfficePhone_CountryCode { get; set; }
        //[DataMember]
        //public string Fax_CountryCode { get; set; }
        [DataMember]
        public string FormattedAddress { get; set; }
    }
}
