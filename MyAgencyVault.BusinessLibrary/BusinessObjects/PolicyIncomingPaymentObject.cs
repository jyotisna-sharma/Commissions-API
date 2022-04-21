using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace MyAgencyVault.BusinessLibrary.BusinessObjects
{
    public class PolicyIncomingPaymentObject
    {
        [DataMember]
        public Guid PaymentEntryID { get; set; }
        [DataMember]
        public Guid StmtID { get; set; }
        [DataMember]
        public Guid PolicyID { get; set; }

        DateTime? _InvoiceDate;
        [DataMember]
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
                    if (_InvoiceDate != null)
                        InvoiceDateString = _InvoiceDate.GetValueOrDefault().ToString("MMM dd, yyyy");
                    else
                        InvoiceDateString = "";
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
        DateTime? _StatementDate;
        [DataMember]
        public DateTime? StatementDate
        {
            get
            {
                return _StatementDate;
            }
            set
            {
                _StatementDate = value;
                if (value != null && string.IsNullOrEmpty(StatementDateString))
                {
                    StatementDateString = Convert.ToDateTime(value).ToString("MMM dd, yyyy");
                }
            }
        }
        string _StatementDateString;
        [DataMember]
        public string StatementDateString
        {
            get
            {
                return _StatementDateString;
            }
            set
            {
                _StatementDateString = value;
                if (StatementDate == null && !string.IsNullOrEmpty(_StatementDateString))
                {
                    DateTime dt;
                    DateTime.TryParse(_StatementDateString, out dt);
                    StatementDate = dt;
                }
            }
        }

        [DataMember]
        public string PaymentRecived { get; set; }
        [DataMember]
        public string CommissionPercentage { get; set; }
        [DataMember]
        public string NumberOfUnits { get; set; }
        [DataMember]
        public string DollerPerUnit { get; set; }
        [DataMember]
        public string Fee { get; set; }
        [DataMember]
        public string SplitPer { get; set; }
        [DataMember]
        public string TotalPayment { get; set; }

        [DataMember]
        public Guid? DEUEntryId { get; set; }
        [DataMember]
        public string StmtNumber { get; set; }
        [DataMember]
        public String BatchNumber { get; set; }

        [DataMember]
        public string Pageno { get; set; }
        [DataMember]
        public Guid? FollowUpVarIssueId { get; set; }

        [DataMember]
        public bool? IsLinkPayment { get; set; }


        [DataMember]
        public bool? IsEntrybyCommissiondashBoard { get; set; }

        [DataMember]
        public string FileName { get; set; }

        [DataMember]
        public Guid? StatementId { get; set; }
        [DataMember]
        public Guid? BatchId { get; set; }
        //Newly added 29102013
        //[DataMember]
        //public int? FollowUpIssueResolveOrClosed { get; set; }

        //Newly added 21012014 for unlink payment from commision dashboard
        //[DataMember]
        //public bool? IsLinkPayment { get; set; }

        //private static decimal? expectedpayment;

        //public static decimal? Expectedpayment
        //{
        //    get { return PolicyPaymentEntriesPost.expectedpayment; }
        //    set { PolicyPaymentEntriesPost.expectedpayment = value; }
        //}
    }
}
