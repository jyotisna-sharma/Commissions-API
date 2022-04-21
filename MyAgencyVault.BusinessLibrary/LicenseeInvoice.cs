using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using MyAgencyVault.BusinessLibrary.Base;
using System.Runtime.Serialization;
using DLinq = DataAccessLayer.LinqtoEntity;
using MyAgencyVault.BusinessLibrary.Masters;
using DataAccessLayer.LinqtoEntity;

namespace MyAgencyVault.BusinessLibrary
{
    [DataContract]
    public class LicenseeInvoice
    {
        [DataMember]
        public long InvoiceId { get; set; }
        [DataMember]
        public Guid? LicenseeId { get; set; }
        //[DataMember]
        //public DateTime? BillingStartDate { get; set; }
        DateTime? _billingStartDate;
        [DataMember]
        public DateTime? BillingStartDate
        {
            get
            {
                return _billingStartDate;
            }
            set
            {
                _billingStartDate = value;
                if (value != null && string.IsNullOrEmpty(billingStartDatestring))
                {
                    billingStartDatestring = value.ToString();
                }
            }
        }
        string _billingStartDateString;
        [DataMember]
        public string billingStartDatestring
        {
            get
            {
                return _billingStartDateString;
            }
            set
            {
                _billingStartDateString = value;
                if (BillingStartDate == null && !string.IsNullOrEmpty(_billingStartDateString))
                {
                    DateTime dt;
                    DateTime.TryParse(_billingStartDateString, out dt);
                    BillingStartDate = dt;
                }
            }
        }

        //[DataMember]
        //public DateTime? BillingEndDate { get; set; }
        DateTime? _billingEndDate;
        [DataMember]
        public DateTime? BillingEndDate
        {
            get
            {
                return _billingEndDate;
            }
            set
            {
                _billingEndDate = value;
                if (value != null && string.IsNullOrEmpty(billingEndDatestring))
                {
                    billingEndDatestring = value.ToString();
                }
            }
        }
        string _billingEndDateString;
        [DataMember]
        public string billingEndDatestring
        {
            get
            {
                return _billingEndDateString;
            }
            set
            {
                _billingEndDateString = value;
                if (BillingEndDate == null && !string.IsNullOrEmpty(_billingEndDateString))
                {
                    DateTime dt;
                    DateTime.TryParse(_billingEndDateString, out dt);
                    BillingEndDate = dt;
                }
            }
        }

        //[DataMember]
        //public DateTime? BillingDate { get; set; }
        DateTime? _billingDate;
        [DataMember]
        public DateTime? BillingDate
        {
            get
            {
                return _billingDate;
            }
            set
            {
                _billingDate = value;
                if (value != null && string.IsNullOrEmpty(billingDatestring))
                {
                    billingDatestring = value.ToString();
                }
            }
        }
        string _billingDateString;
        [DataMember]
        public string billingDatestring
        {
            get
            {
                return _billingDateString;
            }
            set
            {
                _billingDateString = value;
                if (BillingDate == null && !string.IsNullOrEmpty(_billingDateString))
                {
                    DateTime dt;
                    DateTime.TryParse(_billingDateString, out dt);
                    BillingDate = dt;
                }
            }
        }

        [DataMember]
        public decimal? InvoiceAmount { get; set; }
        [DataMember]
        public decimal? DueBalance { get; set; }

        //[DataMember]
        //public DateTime? InvoiceGeneratedOn { get; set; }
        DateTime? _InvoiceGeneratedOn;
        [DataMember]
        public DateTime? InvoiceGeneratedOn
        {
            get
            {
                return _InvoiceGeneratedOn;
            }
            set
            {
                _InvoiceGeneratedOn = value;
                if (value != null && string.IsNullOrEmpty(InvoiceGeneratedOnstring))
                {
                    InvoiceGeneratedOnstring = value.ToString();
                }
            }
        }
        string _InvoiceGeneratedOnString;
        [DataMember]
        public string InvoiceGeneratedOnstring
        {
            get
            {
                return _InvoiceGeneratedOnString;
            }
            set
            {
                _InvoiceGeneratedOnString = value;
                if (InvoiceGeneratedOn == null && !string.IsNullOrEmpty(_InvoiceGeneratedOnString))
                {
                    DateTime dt;
                    DateTime.TryParse(_InvoiceGeneratedOnString, out dt);
                    InvoiceGeneratedOn = dt;
                }
            }
        }

        [DataMember]
        public Guid? ExportedBatchId { get; set; }
        [DataMember]
        public string BillingPeriod { get; set; }
    }

    public class LicenseeInvoiceHelper
    {
        public static List<LicenseeInvoice> getAllInvoice()
        {
            using (DLinq.CommissionDepartmentEntities DataModel = Entity.DataModel)
            {
                var products = (from se in DataModel.Invoices
                                where se.BillingStartDate != null && se.BillingEndDate != null
                                select new LicenseeInvoice
                                {
                                    InvoiceId = se.InvoiceId,
                                    LicenseeId = se.Licensee.LicenseeId,
                                    BillingStartDate = se.BillingStartDate,
                                    BillingEndDate = se.BillingEndDate,
                                    BillingDate = se.BillingDate,
                                    InvoiceAmount = se.InvoiceAmount,
                                    DueBalance = se.DueBalance,
                                    InvoiceGeneratedOn = se.InvoiceGeneratedOn,
                                    ExportedBatchId = se.ExportBatchFile.ExportBatchId
                                }).ToList();
                products.ForEach(s => s.BillingPeriod = s.BillingStartDate.Value.ToShortDateString() + "-" + s.BillingEndDate.Value.ToShortDateString());
                return products;
            }
        }

        public static LicenseeInvoice getInvoiceByID(long Id)
        {
            using (DLinq.CommissionDepartmentEntities DataModel = Entity.DataModel)
            {
                var invoice = (from se in DataModel.Invoices
                               where se.InvoiceId == Id
                               select new LicenseeInvoice
                               {
                                   InvoiceId = se.InvoiceId,
                                   LicenseeId = se.Licensee.LicenseeId,
                                   BillingStartDate = se.BillingStartDate,
                                   BillingEndDate = se.BillingEndDate,
                                   BillingDate = se.BillingDate,
                                   InvoiceAmount = se.InvoiceAmount,
                                   DueBalance = se.DueBalance,
                                   InvoiceGeneratedOn = se.InvoiceGeneratedOn,
                                   ExportedBatchId = se.ExportBatchFile.ExportBatchId
                                   //BillingPeriod = se.BillingStartDate.ToString() + "-" + se.BillingEndDate.ToString()
                               }).FirstOrDefault();

                return invoice;
            }
        }

        public static string getExportBatchName(Guid? ExportedBatchId)
        {
            string fileName = string.Empty;
            if (ExportedBatchId != null && ExportedBatchId.Value != Guid.Empty)
            {
                using (DLinq.CommissionDepartmentEntities DataModel = Entity.DataModel)
                {
                    fileName = (from se in DataModel.ExportBatchFiles
                                    where se.ExportBatchId == ExportedBatchId.Value
                                    select se.FileName).FirstOrDefault();

                }
            }
            return fileName;
        }

        public static DateTime? getLatestBillingDate()
        {
            using (DLinq.CommissionDepartmentEntities DataModel = Entity.DataModel)
            {
                DateTime? maxBillingDate = null;
                
                if(DataModel.Invoices.Count() != 0)
                    maxBillingDate = DataModel.Invoices.Max(s => s.BillingDate);

                return maxBillingDate;
            }
        }
    }
}
