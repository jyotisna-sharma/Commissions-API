using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace MyAgencyVault.BusinessLibrary.BusinessObjects
{
    public class ReportObject
    {
    }
    [DataContract]
    public class ReportBatchDetails
    {
        [DataMember]
        public Guid BatchId { get; set; }

        [DataMember]
        public int BatchNumber { get; set; }

        DateTime? _CreatedDate;
        [DataMember]
        public DateTime? CreatedDate
        {
            get
            {
                return _CreatedDate;
            }
            set
            {
                _CreatedDate = value;
                if (value != null && string.IsNullOrEmpty(CreatedDateString))
                {
                    CreatedDateString = Convert.ToDateTime(value).ToString("MMM dd, yyyy");
                }
            }
        }
        string _CreatedDateString;
        [DataMember]
        public string CreatedDateString
        {
            get
            {
                return _CreatedDateString;
            }
            set
            {
                _CreatedDateString = value;
                if (CreatedDate == null && !string.IsNullOrEmpty(_CreatedDateString))
                {
                    DateTime dt;
                    DateTime.TryParse(_CreatedDateString, out dt);
                    CreatedDate = dt;
                }
            }
        }
        [DataMember]
        public EntryStatus EntryStatus
        {
            get; set;
        }
        [DataMember]
        public string Status { get; set; }

        [DataMember]
        public string Year { get; set; }
    }
    public class GetPayeeList
    {
        [DataMember]
        public Guid UserCredentialId { get; set; }
        [DataMember]
        public string PayeeName{get; set;}
        [DataMember]
        public string LastName { get; set; }
        [DataMember]
        public string FirstName { get; set; }
        [DataMember]
        public string UserName { get; set; }

    }
}
