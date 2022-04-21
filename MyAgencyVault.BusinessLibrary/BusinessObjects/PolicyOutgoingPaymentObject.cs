using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace MyAgencyVault.BusinessLibrary.BusinessObjects
{
    public class PolicyOutgoingPaymentObject
    {

        [DataMember]
        public Guid OutgoingPaymentId { get; set; }
        [DataMember]

        public Guid? PaymentEntryId { get; set; }
        [DataMember]

        public Guid? RecipientUserCredentialId { get; set; }
        [DataMember]

        public string PaidAmount { get; set; } //it is TotalDueToPayee
                                               //[DataMember]

        //public DateTime? CreatedOn { get; set; }
        DateTime? _CreatedDate;
        [DataMember]
        public DateTime? CreatedOn
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
                    CreatedDateString = value.ToString();
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
                if (CreatedOn == null && !string.IsNullOrEmpty(_CreatedDateString))
                {
                    DateTime dt;
                    DateTime.TryParse(_CreatedDateString, out dt);
                    CreatedOn = dt;
                }
            }
        }


        // [DataMember]

        // public Guid? ReferencedOutgoingScheduleId { get; set; }
        // [DataMember]

        //  public Guid? ReferencedOutgoingAdvancedScheduleId { get; set; }

        [DataMember]
        public bool? IsPaid { get; set; }

        [DataMember]
        public string PayeeName { get; set; }
        [DataMember]
        public string Status { get; set; }

        [DataMember]
        public string Premium { get; set; }  //It is %of Premium

        [DataMember]
        public string OutgoingPremium { get; set; }  //It is %of Premium
        [DataMember]
        public string OutGoingPerUnit { get; set; } //it is OutgoingPerunit

        [DataMember]
        public string Payment { get; set; }//It is % of commission
        [DataMember]
        public bool IsEntrybyCommissiondashBoard { get; set; }//It is % of commission
    }
}
