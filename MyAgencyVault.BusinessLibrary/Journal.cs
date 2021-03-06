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
    public class LicenseeInvoiceJournal
    {
        [DataMember]
        public long? JournalId { get; set; }
        [DataMember]
        public long? lnvoiceId { get; set; }
        [DataMember]
        public Guid? ImportedBatchID { get; set; }
        [DataMember]
        public string TransactionID { get; set; }
        
        //[DataMember]
        //public DateTime? ReceivedDate { get; set; }
        DateTime? _ReceivedDate;
        [DataMember]
        public DateTime? ReceivedDate
        {
            get
            {
                return _ReceivedDate;
            }
            set
            {
                _ReceivedDate = value;
                if (value != null && string.IsNullOrEmpty(ReceivedDatestring))
                {
                    ReceivedDatestring = value.ToString();
                }
            }
        }
        string _ReceivedDateString;
        [DataMember]
        public string ReceivedDatestring
        {
            get
            {
                return _ReceivedDateString;
            }
            set
            {
                _ReceivedDateString = value;
                if (ReceivedDate == null && !string.IsNullOrEmpty(_ReceivedDateString))
                {
                    DateTime dt;
                    DateTime.TryParse(_ReceivedDateString, out dt);
                    ReceivedDate = dt;
                }
            }
        }
        
        
        [DataMember]
        public string TypeOfEntry { get; set; }
        [DataMember]
        public decimal? JournalAmount { get; set; }

        //[DataMember]
        //public DateTime? TransactionDateTime { get; set; }
        DateTime? _TransactionDateTime;
        [DataMember]
        public DateTime? TransactionDateTime
        {
            get
            {
                return _TransactionDateTime;
            }
            set
            {
                _TransactionDateTime = value;
                if (value != null && string.IsNullOrEmpty(TransactionDateTimestring))
                {
                    TransactionDateTimestring = value.ToString();
                }
            }
        }
        string _TransactionDateTimeString;
        [DataMember]
        public string TransactionDateTimestring
        {
            get
            {
                return _TransactionDateTimeString;
            }
            set
            {
                _TransactionDateTimeString = value;
                if (TransactionDateTime == null && !string.IsNullOrEmpty(_TransactionDateTimeString))
                {
                    DateTime dt;
                    DateTime.TryParse(_TransactionDateTimeString, out dt);
                    TransactionDateTime = dt;
                }
            }
        }

        //[DataMember]
        //public DateTime? CreatedOn { get; set; }
        DateTime? _CreatedOn;
        [DataMember]
        public DateTime? CreatedOn
        {
            get
            {
                return _CreatedOn;
            }
            set
            {
                _CreatedOn = value;
                if (value != null && string.IsNullOrEmpty(CreatedOnstring))
                {
                    CreatedOnstring = value.ToString();
                }
            }
        }
        string _CreatedOnString;
        [DataMember]
        public string CreatedOnstring
        {
            get
            {
                return _CreatedOnString;
            }
            set
            {
                _CreatedOnString = value;
                if (CreatedOn == null && !string.IsNullOrEmpty(_CreatedOnString))
                {
                    DateTime dt;
                    DateTime.TryParse(_CreatedOnString, out dt);
                    CreatedOn = dt;
                }
            }
        }

        //[DataMember]
        //public DateTime? ModifiedOn { get; set; }
        DateTime? _ModifiedOn;
        [DataMember]
        public DateTime? ModifiedOn
        {
            get
            {
                return _ModifiedOn;
            }
            set
            {
                _ModifiedOn = value;
                if (value != null && string.IsNullOrEmpty(ModifiedOnstring))
                {
                    ModifiedOnstring = value.ToString();
                }
            }
        }
        string _ModifiedOnString;
        [DataMember]
        public string ModifiedOnstring
        {
            get
            {
                return _ModifiedOnString;
            }
            set
            {
                _ModifiedOnString = value;
                if (ModifiedOn == null && !string.IsNullOrEmpty(_ModifiedOnString))
                {
                    DateTime dt;
                    DateTime.TryParse(_ModifiedOnString, out dt);
                    ModifiedOn = dt;
                }
            }
        }

        [DataMember]
        public bool IsManualEntry { get; set; }
        [DataMember]
        public Guid? LicenseeID { get; set; }
        [DataMember]
        public string PaymentType { get; set; }
    }

    public class JournalHelper
    {
        public List<LicenseeInvoiceJournal> getJournalEntries(Guid LicenseeID)
        {
            using (DLinq.CommissionDepartmentEntities DataModel = Entity.DataModel)
            {
                var journals = (from se in DataModel.Journals
                                where se.Licensee.LicenseeId == LicenseeID
                                select new LicenseeInvoiceJournal
                                {
                                 JournalId = se.JournalId,
                                 JournalAmount = se.JournalAmount,
                                 CreatedOn =DateTime.Today,
                                 ReceivedDate = se.ReceivedDate,
                                 lnvoiceId = se.Invoice.InvoiceId,
                                 ImportedBatchID = se.ImportBatchFile.ImportBatchId,
                                 TransactionID = se.TransactionId,
                                 TypeOfEntry = se.TypeOfEntry,
                                 TransactionDateTime = se.TransactionDateTime,
                                 ModifiedOn = se.ModifiedOn,
                                 IsManualEntry = se.IsManuallEntry,
                                 PaymentType = se.PaymentStatus,
                                 LicenseeID = se.Licensee.LicenseeId
                                }).ToList();
                return journals;
            }
        }

        public List<LicenseeInvoiceJournal> getJournalEntries(long InvoiceId)
        {
            using (DLinq.CommissionDepartmentEntities DataModel = Entity.DataModel)
            {
                var journals = (from se in DataModel.Journals
                                where se.Invoice.InvoiceId == InvoiceId
                                select new LicenseeInvoiceJournal
                                {
                                    JournalId = se.JournalId,
                                    JournalAmount = se.JournalAmount,
                                    CreatedOn = DateTime.Today,
                                    ReceivedDate = se.ReceivedDate,
                                    lnvoiceId = se.Invoice.InvoiceId,
                                    ImportedBatchID = se.ImportBatchFile.ImportBatchId,
                                    TransactionID = se.TransactionId,
                                    TypeOfEntry = se.TypeOfEntry,
                                    TransactionDateTime = se.TransactionDateTime,
                                    ModifiedOn = se.ModifiedOn,
                                    IsManualEntry = se.IsManuallEntry,
                                    LicenseeID = se.Licensee.LicenseeId,
                                    PaymentType = se.PaymentStatus
                                }).ToList();
                return journals;
            }
        }

        public List<LicenseeInvoiceJournal> getJournalEntries(Guid LicenseeID, long InvoiceId)
        {
            using (DLinq.CommissionDepartmentEntities DataModel = Entity.DataModel)
            {
                var journals = (from se in DataModel.Journals
                                where se.Invoice.InvoiceId == InvoiceId && se.Licensee.LicenseeId == LicenseeID
                                select new LicenseeInvoiceJournal
                                {
                                    JournalId = se.JournalId,
                                    JournalAmount = se.JournalAmount,
                                    CreatedOn = DateTime.Today,
                                    ReceivedDate = se.ReceivedDate,
                                    lnvoiceId = se.Invoice.InvoiceId,
                                    ImportedBatchID = se.ImportBatchFile.ImportBatchId,
                                    TransactionID = se.TransactionId,
                                    TypeOfEntry = se.TypeOfEntry,
                                    TransactionDateTime = se.TransactionDateTime,
                                    ModifiedOn = se.ModifiedOn,
                                    IsManualEntry = se.IsManuallEntry,
                                    LicenseeID = se.Licensee.LicenseeId,
                                    PaymentType = se.PaymentStatus
                                }).ToList();
                return journals;
            }
        }

        public List<LicenseeInvoiceJournal> getAllJournalEntries()
        {
            using (DLinq.CommissionDepartmentEntities DataModel = Entity.DataModel)
            {
                var journals = (from se in DataModel.Journals
                                select new LicenseeInvoiceJournal
                                {
                                    JournalId = se.JournalId,
                                    JournalAmount = se.JournalAmount,
                                    CreatedOn = DateTime.Today,
                                    ReceivedDate = se.ReceivedDate,
                                    lnvoiceId = se.Invoice.InvoiceId,
                                    ImportedBatchID = se.ImportBatchFile.ImportBatchId,
                                    TransactionID = se.TransactionId,
                                    TypeOfEntry = se.TypeOfEntry,
                                    TransactionDateTime = se.TransactionDateTime,
                                    ModifiedOn = se.ModifiedOn,
                                    IsManualEntry = se.IsManuallEntry,
                                    LicenseeID = se.Licensee.LicenseeId,
                                    PaymentType = se.PaymentStatus
                                }).ToList();
                return journals;
            }
        }

        public bool DeleteJournalEntry(LicenseeInvoiceJournal journalEntry)
        {
            using (DLinq.CommissionDepartmentEntities DataModel = Entity.DataModel)
            {
                var jouEntry = (from m in DataModel.Journals where m.JournalId == journalEntry.JournalId select m).First();
                bool retVal = false;

                if (jouEntry != null)
                {
                    if (!jouEntry.IsManuallEntry)
                        retVal = false;
                    else
                    {
                        if (jouEntry.Invoice != null && jouEntry.InvoiceId != 0)
                            jouEntry.Invoice.DueBalance += jouEntry.JournalAmount;

                        if(jouEntry.Licensee != null && jouEntry.LicenseeId != Guid.Empty)
                            jouEntry.Licensee.DueBalance += jouEntry.JournalAmount;

                        DataModel.DeleteObject(jouEntry);
                        retVal = true;
                    }
                }
                else
                {
                    retVal = false;
                }

                if (retVal)
                    DataModel.SaveChanges();

                return retVal;
            }
        }

        public long InsertJournalEntry(LicenseeInvoiceJournal journalEntry)
        {
            using (DLinq.CommissionDepartmentEntities DataModel = Entity.DataModel)
            {
                long retVal = 0;
                try
                {
                    DLinq.Journal journal = new Journal
                    {
                        CreatedOn = DateTime.Today,
                        ModifiedOn = DateTime.Today,
                        JournalAmount = journalEntry.JournalAmount,
                        TransactionId = journalEntry.TransactionID,
                        TypeOfEntry = journalEntry.TypeOfEntry,
                        IsManuallEntry = true,
                        ReceivedDate = journalEntry.ReceivedDate,
                        TransactionDateTime = journalEntry.TransactionDateTime,
                        PaymentStatus = journalEntry.PaymentType
                    };

                    var InvoiceRow = (from m in DataModel.Invoices where m.InvoiceId == journalEntry.lnvoiceId select m).FirstOrDefault();
                    if (InvoiceRow != null)
                    {
                        InvoiceRow.DueBalance -= journalEntry.JournalAmount;
                        journal.Invoice = InvoiceRow;
                    }
                  
                    var LicenseeRow = (from m in DataModel.Licensees where m.LicenseeId == journalEntry.LicenseeID select m).First();
                    LicenseeRow.DueBalance -= journalEntry.JournalAmount;
                    journal.Licensee = LicenseeRow;

                    DataModel.AddToJournals(journal);
                    DataModel.SaveChanges();
                    retVal = journal.JournalId;
                }
                catch(Exception)
                {
                    retVal = 0;
                }

                return retVal;
            }
        }

        public bool UpdateJournalEntry(LicenseeInvoiceJournal journalEntry)
        {
            using (DLinq.CommissionDepartmentEntities DataModel = Entity.DataModel)
            {
                bool retVal = false;
                decimal? prevJournalAmount = 0;
                try
                {
                    var jouEntry = (from m in DataModel.Journals where m.JournalId == journalEntry.JournalId select m).First();
                    prevJournalAmount = jouEntry.JournalAmount;

                    jouEntry.ModifiedOn = DateTime.Today;
                    jouEntry.JournalAmount = journalEntry.JournalAmount;
                    jouEntry.TransactionId = journalEntry.TransactionID;
                    jouEntry.TypeOfEntry = journalEntry.TypeOfEntry;
                    jouEntry.IsManuallEntry = true;
                    jouEntry.ReceivedDate = journalEntry.ReceivedDate;
                    jouEntry.TransactionDateTime = journalEntry.TransactionDateTime;
                    jouEntry.PaymentStatus = journalEntry.PaymentType;

                    var InvoiceRow = (from m in DataModel.Invoices where m.InvoiceId == journalEntry.lnvoiceId select m).FirstOrDefault();
                    if (InvoiceRow != null)
                    {
                        InvoiceRow.DueBalance -= (journalEntry.JournalAmount - prevJournalAmount);
                        jouEntry.Invoice = InvoiceRow;
                    }

                    var LicenseeRow = (from m in DataModel.Licensees where m.LicenseeId == journalEntry.LicenseeID select m).First();
                    LicenseeRow.DueBalance -= (journalEntry.JournalAmount - prevJournalAmount);
                    jouEntry.Licensee = LicenseeRow;

                    DataModel.SaveChanges();
                    retVal = true;
                }
                catch
                {
                    retVal = false;
                }

                return retVal;
            }
        }
    }
}
