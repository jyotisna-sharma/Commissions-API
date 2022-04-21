using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyAgencyVault.BusinessLibrary.Base;
using System.Runtime.Serialization;

namespace MyAgencyVault.BusinessLibrary
{
    [DataContract]
   public class FollowupIncomingPament
    {

        #region "Datamembers aka public properties."

        DateTime? _InvoiceDate;
       
        public DateTime? InvoiceDate
        {
            get
            {
                return _InvoiceDate;
            }
            set
            {
                _InvoiceDate = value;
                if (value != null && string.IsNullOrEmpty(InvoiceDateString))
                {
                    InvoiceDateString = value.ToString();
                }
            }
        }
        string _InvoiceDateString;
        [DataMember]
        public string InvoiceDateString
        {
            get
            {
                return _InvoiceDateString;
            }
            set
            {
                _InvoiceDateString = value;
                if (InvoiceDate == null && !string.IsNullOrEmpty(_InvoiceDateString))
                {
                    DateTime dt;
                    DateTime.TryParse(_InvoiceDateString, out dt);
                    InvoiceDate = dt;
                }
            }
        }

        //[DataMember]
        //public DateTime? InvoiceDate { get; set; }

        [DataMember]
        public decimal? PaymentRecived { get; set; }

        [DataMember]
        public double? CommissionPercentage { get; set; }

        [DataMember]
        public int? NumberOfUnits { get; set; }

        [DataMember]
        public decimal? DollerPerUnit { get; set; }

        [DataMember]
        public decimal? Fee { get; set; }

        [DataMember]
        public double? SplitPer { get; set; }

        [DataMember]
        public decimal? TotalPayment { get; set; }

        [DataMember]
        public int Statement { get; set; }

        [DataMember]
        public int Batch { get; set; }

        #endregion
    }
}
