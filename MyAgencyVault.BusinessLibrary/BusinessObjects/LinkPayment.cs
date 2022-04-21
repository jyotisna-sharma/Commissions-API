using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace MyAgencyVault.BusinessLibrary.BusinessObjects
{
    [DataContract]
    public class LinkPayment
    {
        [DataMember]
        public Guid PaymentEntryID { get; set; }
        [DataMember]
        public Guid PolicyId { get; set; }

        [DataMember]
        public string PolicyNumber { get; set; }

        DateTime? invoiceDate;
        [DataMember]
        public DateTime? InvoiceDate
        {
            get
            {
                return invoiceDate;
            }
            set
            {
                invoiceDate = value;
                if (value != null && string.IsNullOrEmpty(InvoiceDateString))
                {
                    InvoiceDateString = Convert.ToDateTime(value).ToString("MMM dd, yyyy");
                }
            }
        }
        string invoiceDateString;
        [DataMember]
        public string InvoiceDateString
        {
            get
            {
                return invoiceDateString;
            }
            set
            {
                invoiceDateString = value;
                if (InvoiceDate == null && !string.IsNullOrEmpty(invoiceDateString))
                {
                    DateTime dt;
                    DateTime.TryParse(invoiceDateString, out dt);
                    InvoiceDate = dt;
                }
            }
        }
        [DataMember]
        public string PaymentRecived { get; set; }
        [DataMember]
        public string CommissionPercentage { get; set; }//incoming Percentage
        [DataMember]
        public string NumberOfUnits { get; set; }
        [DataMember]
        public string DollerPerUnit { get; set; }
        [DataMember]
        public decimal Fee { get; set; }
        [DataMember]
        public string SplitPer { get; set; }
        [DataMember]
        public string TotalPayment { get; set; }
    }
}
