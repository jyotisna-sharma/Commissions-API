using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyAgencyVault.BusinessLibrary.Base;
using System.Runtime.Serialization;
using DLinq = DataAccessLayer.LinqtoEntity;
using DataAccessLayer.LinqtoEntity;
using MyAgencyVault.BusinessLibrary.Masters;
using System.Transactions;
using System.Threading;
using System.Data.SqlClient;
using System.Data;
using System.Threading.Tasks;
using System.Data.SqlTypes;
using OfficeOpenXml.FormulaParsing.Excel.Functions.DateTime;
using System.Globalization;

namespace MyAgencyVault.BusinessLibrary
{
    

    [DataContract]
    public class ModifiableStatementData
    {
        [DataMember]
        public Guid StatementId { get; set; }
        [DataMember]
        public Guid? TemplateId { get; set; }
        [DataMember]
        public Guid? PayorId { get; set; }
        [DataMember]
        public decimal EnteredAmount { get; set; }
        [DataMember]
        public double CompletePercentage { get; set; }
        [DataMember]
        public int Entries { get; set; }
        [DataMember]
        public int StatusId { get; set; }

        //[DataMember]
        //public DateTime LastModified { get; set; }
        DateTime? _LastModified;
        [DataMember]
        public DateTime? LastModified
        {
            get
            {
                return _LastModified;
            }
            set
            {
                _LastModified = value;
                if (value != null && string.IsNullOrEmpty(LastModifiedString))
                {
                    LastModifiedString = value.ToString();
                }
            }
        }
        string _LastModifiedString;
        [DataMember]
        public string LastModifiedString
        {
            get
            {
                return _LastModifiedString;
            }
            set
            {
                _LastModifiedString = value;
                if (LastModified == null && !string.IsNullOrEmpty(_LastModifiedString))
                {
                    DateTime dt;
                    DateTime.TryParse(_LastModifiedString, out dt);
                    LastModified = dt;
                }
            }
        }
    }


    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public class Statement
    {

        class StatementData
        {
            public List<Guid?> Policies { get; set; }
            public List<Guid?> Issues { get; set; }
        }

        #region "Data Members aka - public properties "
        [DataMember]
        public Guid StatementID { get; set; }
        [DataMember]
        public Guid BatchId { get; set; }
        [DataMember]
        public Guid? PayorId { get; set; }
        [DataMember]
        public int StatementNumber { get; set; }
        [DataMember]
        public int BatchNumber { get; set; }

        //[DataMember]
        //public DateTime? StatementDate { get; set; }
        DateTime? _statementDate;
        [DataMember]
        public DateTime? StatementDate
        {
            get
            {
                return _statementDate;
            }
            set
            {
                _statementDate = value;
                if (value != null && string.IsNullOrEmpty(StatementDateString))
                {
                    StatementDateString = Convert.ToDateTime(value).ToString("MMM dd, yyyy");
                }
            }
        }
        string _statementDateString;
        [DataMember]
        public string StatementDateString
        {
            get
            {
                return _statementDateString;
            }
            set
            {
                _statementDateString = value;
                if (StatementDate == null && !string.IsNullOrEmpty(_statementDateString))
                {
                    DateTime dt;
                    DateTime.TryParse(_statementDateString, out dt);
                    StatementDate = dt;
                }
            }
        }

        decimal? _checkAmount;

        [DataMember]
        public decimal? CheckAmount
        {
            get
            {
                return _checkAmount;
            }
            set
            {
                _checkAmount = value;
                if (string.IsNullOrEmpty(CheckAmountString))
                {
                    CheckAmountString = (value != null) ? Convert.ToDecimal(value).ToString("C") : 0.ToString("C");
                }
            }
        }

        [DataMember]
        public string CheckAmountString
        {
            get; set;
        }
        [DataMember]
        public decimal? BalanceForOrAdjustment { get; set; }

        decimal? _netAmount;

        [DataMember]
        public decimal? NetAmount
        {
            get
            {
                return _netAmount;
            }
            set
            {
                _netAmount = value;
                if (string.IsNullOrEmpty(NetAmountString))
                {
                    NetAmountString = (value != null) ? Convert.ToDecimal(value).ToString("N", new CultureInfo("en-US")) : 0.ToString("C");
                }
            }
        }
        [DataMember]
        public string NetAmountString
        {
            get; set;
        }
        [DataMember]
        public string EnteredAmountString
        {
            get;set;
        }
        decimal _enteredAmount;
        [DataMember]
        public decimal EnteredAmount
        {
            get
            {
                return _enteredAmount;
            }
            set
            {
                _enteredAmount = value;
                if (string.IsNullOrEmpty(EnteredAmountString))
                {
                    EnteredAmountString = Convert.ToDecimal(value).ToString("N", new CultureInfo("en-US"));// : 0.ToString("C");
                }
            }
        }
        double _completePercent;
        [DataMember]
        public double CompletePercentage
        {
            get
            {
                return _completePercent;
            }
            set
            {
                _completePercent = value;
                if (string.IsNullOrEmpty(CompletePercentageString))
                {
                    CompletePercentageString = Convert.ToDecimal(value).ToString("N", new CultureInfo("en-US"));//: 0.ToString("C");
                }
            }
        }
        [DataMember]
        public string CompletePercentageString { get; set; }
        [DataMember]
        public int Entries { get; set; }
        [DataMember]
        public string Payor { get; set; }
        [DataMember]
        public int? StatusId { get; set; }
        [DataMember]
        public string StatusName { get; set; }
        //[DataMember]
        //public DateTime CreatedDate { get; set; }
        DateTime _CreatedDate;
        [DataMember]
        public DateTime CreatedDate
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
                    CreatedDateString = value.ToString("MM/dd/yyyy");
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
        //[DataMember]
        //public DateTime LastModified { get; set; }

        DateTime _LastModifiedDate;
        [DataMember]
        public DateTime LastModified
        {
            get
            {
                return _LastModifiedDate;
            }
            set
            {
                _LastModifiedDate = value;
                if (value != null && string.IsNullOrEmpty(LastModifiedDateString))
                {
                    LastModifiedDateString = value.ToString("MM/dd/yyyy");
                }
            }
        }
        string _LastModifiedDateString;
        [DataMember]
        public string LastModifiedDateString
        {
            get
            {
                return _LastModifiedDateString;
            }
            set
            {
                _LastModifiedDateString = value;
                if (LastModified == null && !string.IsNullOrEmpty(_LastModifiedDateString))
                {
                    DateTime dt;
                    DateTime.TryParse(_LastModifiedDateString, out dt);
                    LastModified = dt;
                }
            }
        }
        [DataMember]
        public Guid CreatedBy { get; set; }
        [DataMember]
        public string CreatedByDEU { get; set; }
        [DataMember]
        public string CarrierName { get; set; }
        [DataMember]
        public string ProductName { get; set; }
        //[DataMember]
        //public DateTime? EntryDate { get; set; }
        DateTime? _entryDate;
        [DataMember]
        public DateTime? EntryDate
        {
            get
            {
                return _entryDate;
            }
            set
            {
                _entryDate = value;
                if (value != null && string.IsNullOrEmpty(EntryDateString))
                {
                    LastModifiedDateString = value.ToString();
                }
            }
        }
        string _entryDateString;
        [DataMember]
        public string EntryDateString
        {
            get
            {
                return _entryDateString;
            }
            set
            {
                _entryDateString = value;
                if (_entryDate == null && !string.IsNullOrEmpty(_entryDateString))
                {
                    DateTime dt;
                    DateTime.TryParse(_entryDateString, out dt);
                    _entryDate = dt;
                }
            }
        }

        [DataMember]
        public Guid? TemplateID { get; set; }

        [DataMember]
        public string FromPage { get; set; }

        [DataMember]
        public string ToPage { get; set; }

        #endregion
        #region  "functionss"
        /// <summary>
        /// hold all the commission entries made through this statement.
        /// </summary>
        [DataMember]
        public List<ExposedDEU> DeuEntries
        {
            get;
            set;
        }

        public static List<ExposedDEU> GetDeuEntriesforStatement(Guid StatementId)
        {
            ActionLogger.Logger.WriteLog("GetDeuEntriesforStatement:Processing begins with StatementId:"+ StatementId, true);
            using (DLinq.CommissionDepartmentEntities DataModel = Entity.DataModel)
            {
                List<ExposedDEU> exposedDeuEntries = new List<ExposedDEU>();

                try
                {
                    List<DLinq.EntriesByDEU> deuEntries = DataModel.EntriesByDEUs.Where(s => s.StatementID == StatementId).ToList();
                    DEU objdeu = new DEU();
                    foreach (DLinq.EntriesByDEU deu in deuEntries)
                    {
                        ExposedDEU exposedDeu = objdeu.CreateExposedDEU(deu);
                        exposedDeuEntries.Add(exposedDeu);
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                return exposedDeuEntries;


            }

        }
        /// <summary>
        /// Add/Update the statement.
        /// </summary>
        public int AddUpdate()
        {
            using (DLinq.CommissionDepartmentEntities DataModel = Entity.DataModel)
            {
                var _statement = (from pn in DataModel.Statements where pn.StatementId == this.StatementID select pn).FirstOrDefault();
                if (_statement == null)
                {
                    DLinq.Batch batch = DataModel.Batches.FirstOrDefault(s => s.BatchId == this.BatchId);
                    _statement = new DLinq.Statement
                    {
                        BatchId = this.BatchId,
                        Batch = batch,
                        PayorId = this.PayorId,
                        CheckAmount = this.CheckAmount,
                        CreatedBy = this.CreatedBy,
                        CreatedOn = DateTime.Now,
                        LastModified = DateTime.Now,
                        Entries = this.Entries,
                        EnteredAmount = this.EnteredAmount,
                        StatementId = this.StatementID,
                        StatementDate = this.StatementDate,
                        StatementStatusId = this.StatusId,
                        BalAdj = this.BalanceForOrAdjustment,
                        TemplateID = this.TemplateID,
                        FromPage = this.FromPage,
                        ToPage = this.ToPage,
                        IsCreatedFromWeb = true
                    };
                    DataModel.AddToStatements(_statement);

                    if (batch.Statements.Count == 1)
                        batch.AssignedUserCredentialId = this.CreatedBy;

                }
                else
                {
                    _statement.BatchId = this.BatchId;
                    _statement.CheckAmount = this.CheckAmount;
                    _statement.EnteredAmount = this.EnteredAmount;//For Commission board
                    _statement.Entries = this.Entries;
                    _statement.CreatedBy = this.CreatedBy;
                    _statement.PayorId = this.PayorId;
                    _statement.LastModified = DateTime.Now;
                    _statement.StatementDate = this.StatementDate;
                    _statement.StatementStatusId = this.StatusId;
                    _statement.BalAdj = this.BalanceForOrAdjustment;
                    _statement.StatementStatusId = this.StatusId;
                    _statement.CreatedOn = System.DateTime.Now;
                    _statement.TemplateID = this.TemplateID;
                    _statement.FromPage = this.FromPage;
                    _statement.ToPage = this.ToPage;
                }

                DataModel.SaveChanges();
                return _statement.StatementNumber;
            }
        }

        public int AddStatementNumber(Statement objStatement)
        {
            int intStatementNumber = 0;
            try
            {
                using (DLinq.CommissionDepartmentEntities DataModel = Entity.DataModel)
                {
                    var _statement = (from pn in DataModel.Statements where pn.StatementId == objStatement.StatementID select pn).FirstOrDefault();
                    if (_statement == null)
                    {
                        DLinq.Batch batch = DataModel.Batches.FirstOrDefault(s => s.BatchId == objStatement.BatchId);
                        _statement = new DLinq.Statement
                        {
                            StatementId = objStatement.StatementID,
                            BatchId = objStatement.BatchId,
                            Batch = batch,
                            PayorId = objStatement.PayorId,
                            CheckAmount = objStatement.CheckAmount,
                            CreatedBy = objStatement.CreatedBy,
                            CreatedOn = DateTime.Now,
                            LastModified = DateTime.Now,
                            Entries = objStatement.Entries,
                            EnteredAmount = objStatement.EnteredAmount,
                            StatementDate = objStatement.StatementDate,
                            StatementStatusId = objStatement.StatusId,
                            BalAdj = objStatement.BalanceForOrAdjustment,
                            TemplateID = objStatement.TemplateID,


                        };
                        DataModel.AddToStatements(_statement);

                        if (batch.Statements.Count == 1)
                            batch.AssignedUserCredentialId = this.CreatedBy;
                    }
                    else
                    {
                        _statement.BatchId = objStatement.BatchId;
                        _statement.CheckAmount = objStatement.CheckAmount;
                        _statement.EnteredAmount = objStatement.EnteredAmount;//For Commission board
                        _statement.Entries = objStatement.Entries;
                        _statement.CreatedBy = objStatement.CreatedBy;
                        _statement.PayorId = objStatement.PayorId;
                        _statement.LastModified = DateTime.Now;
                        _statement.StatementDate = objStatement.StatementDate;
                        _statement.BalAdj = objStatement.BalanceForOrAdjustment;
                        _statement.StatementStatusId = objStatement.StatusId;
                        _statement.CreatedOn = System.DateTime.Now;
                        _statement.TemplateID = objStatement.TemplateID;
                    }

                    DataModel.SaveChanges();
                    intStatementNumber = _statement.StatementNumber;
                }
            }
            catch (Exception ex)
            {
                //Acme aded  Mar 28, 2017
                ActionLogger.Logger.WriteLog("AddStatement number exception: " + ex.Message, true);
                if (ex.InnerException != null)
                {
                    ActionLogger.Logger.WriteLog("AddStatement number exception: " + ex.InnerException.Message, true);
                }
            }
            return intStatementNumber;
        }



        /*
        public static ModifiableStatementData UpdateCheckAmount(Guid statementId, decimal dcCheckAmount, decimal dcNetAmount, decimal dcAdjustment)
        {
            ActionLogger.Logger.WriteLog("UpdateCheckAmount1 statementId: " + statementId + ", dcCheckAmount: " + dcCheckAmount + ", dcNetAmount: " + dcNetAmount + ", dcAdjustment:" + dcAdjustment, true);

            ModifiableStatementData statementData = new ModifiableStatementData();
            try
            {
                using (DLinq.CommissionDepartmentEntities DataModel = Entity.DataModel)
                {
                    DLinq.Statement statement = DataModel.Statements.FirstOrDefault(s => s.StatementId == statementId);

                    if (statement != null)
                    {
                        statement.CheckAmount = dcCheckAmount;
                        statement.BalAdj = dcAdjustment;
                        DataModel.SaveChanges();

                        statementData.StatementId = statement.StatementId;
                        //statementData.CompletePercentage = (double)((statement.EnteredAmount ?? 0) / ((netAmount + statement.BalAdj ?? 0) == 0 ? int.MaxValue : (netAmount + statement.BalAdj ?? 0))) * 100;
                        if (dcNetAmount == 0)
                            statementData.CompletePercentage = 0;
                        else
                            statementData.CompletePercentage = ((double)(dcNetAmount - statement.EnteredAmount) * 100) / ((double)(dcNetAmount));

                        statementData.LastModified = statement.LastModified.Value;
                        statementData.Entries = statement.Entries.Value;
                        statementData.StatusId = statement.MasterStatementStatu.StatementStatusId;
                        statementData.EnteredAmount = statement.EnteredAmount.Value;
                    }
                    ActionLogger.Logger.WriteLog("UpdateCheckAmount1 success ", true);
                }
            }
            catch (Exception ex)
            {
                ActionLogger.Logger.WriteLog("UpdateCheckAmount1 exception: " + ex.Message, true);
            }
            return statementData;
        }*/

        public void UpdateImporttoolStatementData(int intStatementNumber, decimal? dbCheckAmount, decimal? dbBalAdj, decimal? enteredAmount, int? intEntries, DateTime? dtSatementDate)
        {
            using (DLinq.CommissionDepartmentEntities DataModel = Entity.DataModel)
            {
                try
                {
                    DLinq.Statement statement = DataModel.Statements.FirstOrDefault(s => s.StatementNumber == intStatementNumber);

                    if (statement != null)
                    {
                        statement.CheckAmount = dbCheckAmount;
                        statement.BalAdj = dbBalAdj;
                        //statement.EnteredAmount = enteredAmount;                  
                        statement.StatementDate = dtSatementDate;
                        DataModel.SaveChanges();
                    }
                }
                catch
                {
                }
            }
        }


        public static void UpdateStmtPages(Guid statementId, string fromPage, string toPage)
        {
            ActionLogger.Logger.WriteLog("UpdateStmtPages request: stmtID -  " + statementId + ", fromPage: " + fromPage + ", toPage: " + toPage, true);
            try
            {

                DateTime dt = DateTime.MinValue;

                using (DLinq.CommissionDepartmentEntities DataModel = Entity.DataModel)
                {
                    DLinq.Statement statement = DataModel.Statements.FirstOrDefault(s => s.StatementId == statementId);

                    if (statement != null)
                    {
                        statement.FromPage = fromPage;
                        statement.ToPage = toPage;

                        DataModel.SaveChanges();
                    }
                    ActionLogger.Logger.WriteLog("UpdateStmtPages success ", true);
                }
            }
            catch (Exception ex)
            {
                ActionLogger.Logger.WriteLog("UpdateStmtPages exception: " + ex.Message, true);
                throw ex;
            }
        }
        public static string UpdateCheckAmount(Guid statementId, decimal checkAmount, decimal adjustment, string statementDateString, out string completePercent)
        {
            string strNetCheck = "";completePercent = "";
            ActionLogger.Logger.WriteLog("UpdateCheckAmount request: stmtID -  " + statementId + ", checkAmt: " + checkAmount + ", adjustment: " + adjustment + ", stmtDate: " + statementDateString, true);
            try
            {
           
                DateTime dt = DateTime.MinValue;
               
                if (string.IsNullOrEmpty(statementDateString))
                {
                    ActionLogger.Logger.WriteLog("UpdateCheckAmount stmt date issue: blank date", true);
                    throw new Exception("Kindly enter valid statement date.");
                }
                else
                {
                    DateTime.TryParse(statementDateString, out dt);
                    DateTime? date = (dt >= (DateTime)SqlDateTime.MinValue) ? dt : (DateTime?)null;
                    if (dt == DateTime.MinValue|| date == null)
                    {
                        ActionLogger.Logger.WriteLog("UpdateCheckAmount stmt date issue: invalid date", true);
                        throw new Exception("Kindly enter valid statement date.");
                    }
                }

                using (DLinq.CommissionDepartmentEntities DataModel = Entity.DataModel)
                {
                    DLinq.Statement statement = DataModel.Statements.FirstOrDefault(s => s.StatementId == statementId);

                    if (statement != null)
                    {
                        statement.CheckAmount = checkAmount;
                        statement.BalAdj = adjustment;
                       
                        statement.StatementDate = dt;

                        DataModel.SaveChanges();

                        // double checkAmt = checkAmount ?? 0; // == null ? 0 : (decimal)checkAmount;

                        decimal netCheck = (decimal)checkAmount - (decimal)adjustment;
                        strNetCheck = Convert.ToDecimal(netCheck).ToString("N", new CultureInfo("en-US")); //netCheck.ToString("C");

                        double completePer = CalculateCompletePercent(checkAmount, adjustment, statement.EnteredAmount);
                        completePercent = completePer.ToString("N", new CultureInfo("en-US"));

                    }
                    ActionLogger.Logger.WriteLog("UpdateCheckAmount success ", true);
                }
            }
            catch (Exception ex)
            {
                ActionLogger.Logger.WriteLog("UpdateCheckAmount exception: " + ex.Message, true);
                throw ex;
            }
            return strNetCheck;
        }
        /* public void UpdateCheckAmount(int intStatementNumber, decimal? dbCheckAmount, decimal? dbBalAdj)
         {
             ActionLogger.Logger.WriteLog("UpdateCheckAmount intStatementNumber: " + intStatementNumber + ", dbCheckAmount: " + dbCheckAmount + ", dbBalAdj: " + dbBalAdj, true);
             try
             {
                 using (DLinq.CommissionDepartmentEntities DataModel = Entity.DataModel)
                 {
                     DLinq.Statement statement = DataModel.Statements.FirstOrDefault(s => s.StatementNumber == intStatementNumber);

                     if (statement != null)
                     {
                         statement.CheckAmount = dbCheckAmount;
                         statement.BalAdj = dbBalAdj;
                         DataModel.SaveChanges();
                     }
                     ActionLogger.Logger.WriteLog("UpdateCheckAmount success ", true);
                 }
             }
             catch (Exception ex)
             {
                 ActionLogger.Logger.WriteLog("UpdateCheckAmount exception: " + ex.Message, true);
             }
         }*/

        public static ModifiableStatementData CreateModifiableStatementData(DLinq.Statement statement)
        {
            ModifiableStatementData statementData = new ModifiableStatementData();

            try
            {
                if (statement != null)
                {
                    statementData.StatementId = statement.StatementId;
                    statementData.CompletePercentage = (double)((statement.EnteredAmount ?? 0) / ((statement.CheckAmount ?? 0 + statement.BalAdj ?? 0) == 0 ? int.MaxValue : (statement.CheckAmount ?? 0 + statement.BalAdj ?? 0))) * 100;
                    statementData.LastModified = statement.LastModified.Value;
                    statementData.Entries = statement.Entries.Value;
                    statementData.StatusId = statement.MasterStatementStatu.StatementStatusId;
                    statementData.EnteredAmount = statement.EnteredAmount.Value;
                    statementData.PayorId = statement.PayorId;
                    statement.TemplateID = statement.TemplateID;
                }

            }
            catch (Exception ex)
            {
                ActionLogger.Logger.WriteLog("CreateModifiableStatementData" + ex.Message.ToString(), true);
                ActionLogger.Logger.WriteLog("*****************", true);
                ActionLogger.Logger.WriteLog("CreateModifiableStatementData" + ex.InnerException.ToString(), true);
            }

            return statementData;
        }        /// <summary>
                 /// just insert the statument . call AddUpdate()
                 /// with all required properties filled.
                 /// </summary>
        public void PostStatement()
        {
            this.AddUpdate();
        }

        /// <summary>
        /// update the status of the statement to be closed.
        /// </summary>
        //public bool CloseStatement()
        //{
        //    using (DLinq.CommissionDepartmentEntities DataModel = Entity.DataModel)
        //    {
        //        bool StatementEntriesSuccessfull = false;
        //        DLinq.Statement currentStatement = (from p in DataModel.Statements where p.StatementId == this.StatementID select p).FirstOrDefault();
        //        if (currentStatement != null)
        //        {
        //            //List<DLinq.EntriesByDEU> DeuEntries = (from p in DataModel.EntriesByDEUs where p.StatementID == this.StatementID select p).ToList();

        //            //decimal TotalMoney = 0;
        //            //foreach (DLinq.EntriesByDEU entry in DeuEntries)
        //            //{
        //            //    TotalMoney += entry.CommissionPaid.Value + entry.CommissionTotal.Value;
        //            //}


        //            //Needs to be a +/- of $1.00 for right now to allow a statement to close "Eric requirement"
        //            //if (currentStatement.EnteredAmount == this.NetAmount)

        //            if (Math.Abs(Convert.ToDecimal(currentStatement.EnteredAmount) - Convert.ToDecimal(this.NetAmount)) <= 1)                    
        //                StatementEntriesSuccessfull = true;

        //            if (StatementEntriesSuccessfull)
        //            {
        //                currentStatement.StatementStatusId = 2;
        //                currentStatement.MasterStatementStatu = DataModel.MasterStatOrementStatus.FirstDefault(s => s.StatementStatusId == 2);
        //                DataModel.SaveChanges();
        //            }
        //        }
        //        return StatementEntriesSuccessfull;
        //    }
        //}

        public bool CloseStatement()
        {
            ActionLogger.Logger.WriteLog(DateTime.Now.ToString() + " CloseStatement request: NetAmount: " + this.NetAmount + ", StatementiD: " + this.StatementID, true);
            bool StatementEntriesSuccessfull = false;
            try
            {
                using (DLinq.CommissionDepartmentEntities DataModel = Entity.DataModel)
                {
                    DLinq.Statement currentStatement = (from p in DataModel.Statements where p.StatementId == this.StatementID select p).FirstOrDefault();
                    if (currentStatement != null)
                    {
                        ActionLogger.Logger.WriteLog(DateTime.Now.ToString() + " CloseStatement : EnteredAmount - " + currentStatement.EnteredAmount, true);
                        if (Math.Abs(Convert.ToDecimal(currentStatement.EnteredAmount) - Convert.ToDecimal(this.NetAmount)) <= 1)
                            StatementEntriesSuccessfull = true;

                        ActionLogger.Logger.WriteLog(DateTime.Now.ToString() + " CloseStatement : StatementEntriesSuccessfull - " + StatementEntriesSuccessfull, true);
                        if (StatementEntriesSuccessfull)
                        {
                            currentStatement.StatementStatusId = 2;
                            currentStatement.MasterStatementStatu = DataModel.MasterStatementStatus.FirstOrDefault(s => s.StatementStatusId == 2);
                            DataModel.SaveChanges();
                            ActionLogger.Logger.WriteLog(DateTime.Now.ToString() + " CloseStatement success", true);
                        }
                    }
                    else
                    {
                        ActionLogger.Logger.WriteLog(DateTime.Now.ToString() + " CloseStatement : current statement found null  ", true);
                    }
                }
            }
            catch (Exception ex)
            {
                ActionLogger.Logger.WriteLog(DateTime.Now.ToString() + " CloseStatement exception:  " + ex.Message, true);
            }
            return StatementEntriesSuccessfull;
        }

        public bool CloseStatementFromDeu(Statement objStatement)
        {
            ActionLogger.Logger.WriteLog(DateTime.Now.ToString() + " CloseStatementFromDeu request: ", true);
            using (DLinq.CommissionDepartmentEntities DataModel = Entity.DataModel)
            {
                bool StatementEntriesSuccessfull = false;

                try
                {

                    if (objStatement != null)
                    {
                        //List<DLinq.EntriesByDEU> DeuEntries = (from p in DataModel.EntriesByDEUs where p.StatementID == this.StatementID select p).ToList();

                        //decimal TotalMoney = 0;
                        //foreach (DLinq.EntriesByDEU entry in DeuEntries)
                        //{
                        //    TotalMoney += entry.CommissionPaid.Value + entry.CommissionTotal.Value;
                        //}


                        //Needs to be a +/- of $1.00 for right now to allow a statement to close "Eric requirement"
                        //if (currentStatement.EnteredAmount == this.NetAmount)

                        if (Math.Abs(Convert.ToDecimal(objStatement.EnteredAmount) - Convert.ToDecimal(objStatement.NetAmount)) <= 1)
                            StatementEntriesSuccessfull = true;

                        if (StatementEntriesSuccessfull)
                        {
                            ActionLogger.Logger.WriteLog(DateTime.Now.ToString() + " CloseStatementFromDeu success: ", true);
                            DLinq.Statement currentStatement = (from p in DataModel.Statements where p.StatementId == this.StatementID select p).FirstOrDefault();
                            if (currentStatement != null)
                            {
                                currentStatement.StatementStatusId = 2;
                                currentStatement.MasterStatementStatu = DataModel.MasterStatementStatus.FirstOrDefault(s => s.StatementStatusId == 2);
                                currentStatement.EnteredAmount = objStatement.EnteredAmount;
                                DataModel.SaveChanges();
                                StatementEntriesSuccessfull = true;
                            }
                            else
                            {
                                ActionLogger.Logger.WriteLog(DateTime.Now.ToString() + " CloseStatementFromDeu failure ", true);
                                StatementEntriesSuccessfull = false;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    ActionLogger.Logger.WriteLog("Issue while Closing StatementFromDeu", true);
                    ActionLogger.Logger.WriteLog(ex.StackTrace.ToString(), true);
                }
                return StatementEntriesSuccessfull;
            }
        }
        public static bool OpenStatement(Guid statementID)
        {
            using (DLinq.CommissionDepartmentEntities DataModel = Entity.DataModel)
            {
                bool StatementEntriesSuccessfull = false;
                try
                {
                    DLinq.Statement currentStatement = (from p in DataModel.Statements where p.StatementId == statementID select p).FirstOrDefault();
                    if (currentStatement != null)
                    {

                        currentStatement.StatementStatusId = 1;
                        currentStatement.MasterStatementStatu = DataModel.MasterStatementStatus.FirstOrDefault(s => s.StatementStatusId == 1);
                        DataModel.SaveChanges();
                        StatementEntriesSuccessfull = true;

                    }
                }
                catch (Exception ex)
                {
                    StatementEntriesSuccessfull = false;
                    ActionLogger.Logger.WriteLog(ex.StackTrace.ToString(), true);
                }
                return StatementEntriesSuccessfull;
            }
        }

        public static Statement GetFindStatement(int statementNumber)
        {

            using (DLinq.CommissionDepartmentEntities DataModel = Entity.DataModel)
            {
                Statement statement = (from dv in DataModel.Statements
                                       where dv.StatementNumber == statementNumber
                                       select new Statement
                                       {
                                           StatementID = dv.StatementId,
                                           StatementNumber = dv.StatementNumber,
                                           StatementDate = dv.StatementDate.Value,
                                           CheckAmount = dv.CheckAmount,
                                           BalanceForOrAdjustment = dv.BalAdj,
                                           NetAmount = dv.CheckAmount ?? 0 + dv.BalAdj ?? 0,
                                           CompletePercentage = (double)((dv.EnteredAmount ?? 0) / ((dv.CheckAmount ?? 0 + dv.BalAdj ?? 0) == 0 ? int.MaxValue : (dv.CheckAmount ?? 0 + dv.BalAdj ?? 0))) * 100,
                                           BatchId = dv.BatchId.Value,
                                           StatusId = dv.MasterStatementStatu.StatementStatusId,
                                           Entries = dv.Entries ?? 0,
                                           EnteredAmount = dv.EnteredAmount.Value,
                                           CreatedDate = dv.CreatedOn.Value,
                                           LastModified = dv.LastModified.Value,
                                           PayorId = dv.PayorId.Value,
                                           TemplateID = dv.TemplateID,
                                           CreatedBy = dv.CreatedBy.Value,
                                           FromPage = dv.FromPage,
                                           ToPage = dv.ToPage

                                       }).FirstOrDefault();

                return statement;
            }

        }

        /// <summary>
        /// Modified By:Ankit
        /// Modified On:09-05-2019
        /// </summary>
        /// <param name="BatchID"></param>
        /// <returns></returns>
        public List<Statement> GetStatementList(Guid BatchID)
        {
            using (DLinq.CommissionDepartmentEntities DataModel = Entity.DataModel)
            {
                List<Statement> statements = (from dv in DataModel.Statements
                                              where dv.Batch.BatchId == BatchID
                                              select new Statement
                                              {
                                                  StatementID = dv.StatementId,
                                                  StatementNumber = dv.StatementNumber,
                                                  StatementDate = dv.StatementDate.Value,
                                                  CheckAmount = dv.CheckAmount,
                                                  BalanceForOrAdjustment = dv.BalAdj,
                                                  NetAmount = ((dv.CheckAmount ?? 0) + (dv.BalAdj ?? 0)),
                                                  CompletePercentage = (double)((dv.EnteredAmount ?? 0) / ((dv.CheckAmount ?? 0 + dv.BalAdj ?? 0) == 0 ? int.MaxValue : (dv.CheckAmount ?? 0 + dv.BalAdj ?? 0))) * 100,
                                                  BatchId = dv.BatchId.Value,
                                                  StatusId = dv.MasterStatementStatu.StatementStatusId,
                                                  Entries = dv.Entries.Value,
                                                  EnteredAmount = dv.EnteredAmount.Value,
                                                  CreatedDate = dv.CreatedOn.Value,
                                                  LastModified = dv.LastModified.Value,
                                                  PayorId = dv.PayorId.Value,
                                                  CreatedBy = dv.CreatedBy.Value,
                                                  FromPage = dv.FromPage,
                                                  ToPage = dv.ToPage,
                                                  CreatedByDEU = DataModel.UserDetails.FirstOrDefault(p => p.UserCredentialId == dv.CreatedBy).LastName ?? "Super"
                                              }).ToList();

                DLinq.UserDetail ud = null;
                //statements.ForEach(s => s.CreatedByDEU = ((ud = DataModel.UserDetails.FirstOrDefault(p => p.UserCredentialId == s.CreatedBy)) == null ? "Super" : ud.LastName));
                return statements;
            }
        }


        public static bool UpdateStatementDate(Guid statementId, string statementDate)
        {
            bool isStamentDateUpdate = false;
            try
            {
                ActionLogger.Logger.WriteLog("UpdateStatementDate:Processing Start with statementId: " + statementId, true);
                using (DLinq.CommissionDepartmentEntities DataModel = Entity.DataModel)
                {
                    DLinq.Statement statement = DataModel.Statements.FirstOrDefault(s => s.StatementId == statementId);
                    BatchStatmentRecords newRecord = new BatchStatmentRecords();
                    newRecord.StatementDateString = statementDate;
                    if (statement != null)
                    {
                        isStamentDateUpdate = true;
                        statement.StatementDate = newRecord.StatementDate;
                        DataModel.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                ActionLogger.Logger.WriteLog("UpdateStatementDate:Exception occur with statementId:" + "StatementId:" + statementId + " " + ex.Message, true);
                throw ex;
            }
            return isStamentDateUpdate;
        }




        public static Guid GetbatchID(Guid StatementID)
        {
            Guid batchID = Guid.Empty;
            try
            {
                using (DLinq.CommissionDepartmentEntities DataModel = Entity.DataModel)
                {
                    var statement = (from dv in DataModel.Statements
                                     where dv.StatementId == StatementID
                                     select dv.BatchId).FirstOrDefault();

                    if (statement != null)
                    {
                        batchID = (Guid)statement.Value;
                    }
                }
            }
            catch (Exception ex)
            {
                ActionLogger.Logger.WriteLog("Exception in getting batch: " + ex.Message, true);
            }
            return batchID;
        }
        public static Statement GetStatement(Guid StatementID)
        {
            using (DLinq.CommissionDepartmentEntities DataModel = Entity.DataModel)
            {
                Statement statement = (from dv in DataModel.Statements
                                       where dv.StatementId == StatementID
                                       select new Statement
                                       {
                                           StatementID = dv.StatementId,
                                           StatementNumber = dv.StatementNumber,
                                           StatementDate = dv.StatementDate.Value,
                                           CheckAmount = dv.CheckAmount,
                                           BalanceForOrAdjustment = dv.BalAdj,
                                           NetAmount = dv.CheckAmount ?? 0 + dv.BalAdj ?? 0,
                                           CompletePercentage = (double)((dv.EnteredAmount ?? 0) / ((dv.CheckAmount ?? 0 + dv.BalAdj ?? 0) == 0 ? int.MaxValue : (dv.CheckAmount ?? 0 + dv.BalAdj ?? 0))) * 100,
                                           BatchId = dv.BatchId.Value,
                                           StatusId = dv.MasterStatementStatu.StatementStatusId,
                                           Entries = dv.Entries ?? 0,
                                           EnteredAmount = dv.EnteredAmount.Value,
                                           CreatedDate = dv.CreatedOn.Value,
                                           LastModified = dv.LastModified.Value,
                                           PayorId = dv.PayorId.Value,
                                           CreatedBy = dv.CreatedBy.Value,
                                           FromPage = dv.FromPage,
                                           ToPage = dv.ToPage

                                       }).FirstOrDefault();

                DLinq.UserDetail _UD = DataModel.UserDetails.FirstOrDefault(s => s.UserCredentialId == statement.CreatedBy);
                statement.CreatedByDEU = _UD == null ? "Super" : _UD.LastName;
                return statement;
            }
        }


        public static bool IsStatementClosed(Guid StatementID)
        {
            try
            {
                using (DLinq.CommissionDepartmentEntities DataModel = Entity.DataModel)
                {
                    Statement statement = (from dv in DataModel.Statements
                                           where dv.StatementId == StatementID
                                           select new Statement
                                           {
                                               StatusId = dv.StatementStatusId
                                           }).FirstOrDefault();

                    if (statement != null && statement.StatusId == 2)
                    {
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                ActionLogger.Logger.WriteLog("IsStatementClosed exception: " + StatementID + ", ex: " + ex.Message, true);
            }

            return false;
        }

        #endregion
        #region IEditable<Statement> Members

        static void ProcessDeletedStatementDone(Object data)
        {

        }

        /*
         * Note - Below process is same as the delete deu entry process.
         * 
         * This method is part of delete statement process
         * Called in a separate thread after stmt is removed
         * 
         * Steps:
         * Issues list  - Checks each IssueID, if not associated to any other payment, deletes from system.
         * Policies list - Checks each policy - If no payment existing and policy is pending, removed from the system
         * If has payments - then update learned fields with latest Invoice record.
         * 
         * One proces skipped - OpenMissingIssuesIfAny for the policy, as this will be taken care by follow up service in next run of this policy. 
         * */
        static void ProcessDeletedStatementData(Object data)
        {
            try
            {
                ActionLogger.Logger.WriteLog("ProcessDeletedStatementData data ", true);

                StatementData objStmt = data as StatementData;

                using (DLinq.CommissionDepartmentEntities DataModel = Entity.DataModel)
                {
                    for (int i = 0; i < objStmt.Issues.Count; i++)
                    {
                        ActionLogger.Logger.WriteLog("ProcessDeletedStatementData Issue: " + i, true);

                        Guid? FollowUpVarIssueId = objStmt.Issues[i];
                        if (FollowUpVarIssueId != null)
                        {
                            List<DLinq.PolicyPaymentEntry> _PolicyPaymentEntriesPostLst = DataModel.PolicyPaymentEntries.Where(p => p.FollowUpVarIssueId == FollowUpVarIssueId).ToList();
                            DLinq.FollowupIssue FollowupIssueRecord = DataModel.FollowupIssues.Where(p => p.IssueId == FollowUpVarIssueId).FirstOrDefault();
                            if (FollowupIssueRecord != null)
                            {
                                DataModel.DeleteObject(FollowupIssueRecord);
                                DataModel.SaveChanges();
                            }
                        }
                    }

                    //For policies
                    for (int i = 0; i < objStmt.Policies.Count; i++)
                    {
                        ActionLogger.Logger.WriteLog("ProcessDeletedStatementData Policy: " + i, true);
                        Guid PolicyID = (Guid)objStmt.Policies[i];
                        DEU objDEU = new DEU();
                        List<DEU> _DEULst = objDEU.GetDEUPolicyIdWise(PolicyID);

                        if (_DEULst != null && _DEULst.Count != 0)
                        {
                            ActionLogger.Logger.WriteLog("ProcessDeletedStatementData policy update: " + i, true);
                            try
                            {
                                DEU _DEU = _DEULst.Where(p => p.InvoiceDate == _DEULst.Max(p1 => p1.InvoiceDate)).FirstOrDefault();
                                DEUFields _DEUFields = PostUtill.FillDEUFields(_DEU.DEUENtryID);
                                DEU _LatestDEUrecord = objDEU.GetLatestInvoiceDateRecord(PolicyID);
                                if (_LatestDEUrecord != null)
                                {
                                    Guid PolicyId = DEULearnedPost.AddDataDeuToLearnedPost(_LatestDEUrecord);
                                    LearnedToPolicyPost.AddUpdateLearnedToPolicy(PolicyId);
                                    PolicyToLearnPost.AddUpdatPolicyToLearn(PolicyId);
                                }

                            }
                            catch (Exception ex)
                            {
                                //ActionLogger.Logger.WriteLog("Issue in AddDataDeuToLearnedPost,AddUpdateLearnedToPolicy,AddUpdatPolicyToLearn block in Poststart function", true);
                                ActionLogger.Logger.WriteLog(ex.StackTrace.ToString(), true);
                            }

                        }
                        else
                        {
                            ActionLogger.Logger.WriteLog("ProcessDeletedStatementData policy delete: " + i, true);
                            try
                            {
                                PolicyDetailsData ppolicy = PostUtill.GetPolicy(PolicyID);
                                //if (ppolicy == null)
                                //{
                                //    _PostProcessReturnStatus.IsClientDeleted = true;
                                //}
                                if (ppolicy.PolicyStatusId == (int)_PolicyStatus.Pending)
                                {
                                    try
                                    {
                                        Policy.DeletePolicyCascadeFromDBById(ppolicy.PolicyId);
                                    }
                                    catch (Exception ex)
                                    {
                                        ActionLogger.Logger.WriteLog("issue in function DeletePolicyCascadeFromDBById :" + ex.StackTrace.ToString(), true);
                                    }
                                    PostUtill.RemoveClient(ppolicy.ClientId ?? Guid.Empty, ppolicy.PolicyLicenseeId ?? Guid.Empty);
                                    //  _PostProcessReturnStatus.IsClientDeleted = true;

                                }
                                //else
                                //{
                                //    PolicyDetailsData Policyhistory = Policy.GetPolicyHistoryIdWise(PolicyID);
                                //    if (Policyhistory != null)
                                //        Policy.AddUpdatePolicy(Policyhistory);

                                //    PolicyLearnedFieldData _PolicyLearnedField = PolicyLearnedField.GetPolicyLearnedFieldsHistoryPolicyWise(PolicySelectedIncomingPaymenttemp.PolicyID);
                                //    if (_PolicyLearnedField != null)
                                //        PolicyLearnedField.AddUpdateLearned(_PolicyLearnedField, _PolicyLearnedField.ProductType);
                                //}
                            }
                            catch (Exception ex)
                            {
                                ActionLogger.Logger.WriteLog("1 :" + ex.StackTrace.ToString(), true);
                                ActionLogger.Logger.WriteLog("1 :" + ex.InnerException.ToString(), true);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        /*
         * Delete statement process:
         * Removes statement from DB - includes cascading deletion of all incoming/outgoing entries
         * Returns response to front end
         * In a separate thread - call for processing policies and issues related to statement payments
         * 
         * */
        public static bool DeleteStatement(Guid StatemetnId, UserRole _UserRole, string strOperation)
        {
            try
            {
                ActionLogger.Logger.WriteLog("DeleteStatement StmtID :" + StatemetnId, true);
                StatementData data = new StatementData();
                using (DLinq.CommissionDepartmentEntities DataModel = Entity.DataModel)
                {
                    DLinq.Statement statement = DataModel.Statements.FirstOrDefault(s => s.StatementId == StatemetnId);

                    List<Guid?> lstPolicies = statement.PolicyPaymentEntries.Select(x => x.PolicyId).Distinct().ToList();
                    List<Guid?> lstIssues = statement.PolicyPaymentEntries.Where(x => x.FollowUpVarIssueId != null).Select(x => x.FollowUpVarIssueId).Distinct().ToList();

                    data.Policies = lstPolicies;
                    data.Issues = lstIssues;
                }

                //Call Sp to delete statement 
                using (SqlConnection con = new SqlConnection(DBConnection.GetConnectionString()))
                {
                    using (SqlCommand cmd = new SqlCommand("usp_DeleteStatement", con))
                    {
                        con.Open();
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandTimeout = 0;
                        cmd.Parameters.AddWithValue("@statementID", StatemetnId);
                        cmd.ExecuteNonQuery();
                    }
                }


                ////Delete statement and the policy 
                //DLinq.Statement deletableStatement = DataModel.Statements.FirstOrDefault(s => s.StatementId == StatemetnId);
                //DataModel.DeleteObject(deletableStatement);
                //DataModel.SaveChanges();

                ParameterizedThreadStart starter = ProcessDeletedStatementData;
                //Task.Factory.StartNew(() => ProcessDeletedStatementData,data)

                //starter += () =>
                //{
                //    ProcessDeletedStatementDone(12);
                //};
                Thread th = new Thread(starter);
                th.Start(data);
                ActionLogger.Logger.WriteLog("DeleteStatement StmtID thread start: " + StatemetnId, true);

                return true;
            }
            catch (Exception ex)
            {
                ActionLogger.Logger.WriteLog("DeleteStatement StmtID exception: " + StatemetnId + ", " + ex.Message, true);
                throw ex;
            }
        }


        public static bool DeleteStatement_OLD(Guid StatemetnId, UserRole _UserRole, string strOperation)
        {
            using (DLinq.CommissionDepartmentEntities DataModel = Entity.DataModel)
            {
                DLinq.Statement statement = DataModel.Statements.FirstOrDefault(s => s.StatementId == StatemetnId);
                //bool DeleteStatementSuccessfull = true;
                bool DeleteStatementSuccessfull = false;

                if (statement != null && statement.EntriesByDEUs != null && statement.EntriesByDEUs.Count != 0)
                {
                    TransactionOptions options = new TransactionOptions
                    {
                        IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted,
                        //Timeout = TimeSpan.FromMinutes(statement.EntriesByDEUs.Count * 1)
                        Timeout = TimeSpan.FromMinutes(statement.EntriesByDEUs.Count * 60)
                    };
                    try
                    {
                        using (TransactionScope transaction = new TransactionScope(TransactionScopeOption.Required, options))
                        {
                            List<Guid> entryIds = statement.EntriesByDEUs.Select(s => s.DEUEntryID).ToList();

                            for (int index = 0; index < entryIds.Count; index++)
                            {
                                PostUtill.PostStart(PostEntryProcess.Delete, entryIds[index], Guid.Empty, Guid.Empty, _UserRole, PostEntryProcess.Delete, string.Empty, strOperation);
                            }

                            using (DLinq.CommissionDepartmentEntities InnerDataModel = Entity.DataModel)
                            {
                                DLinq.Statement deletableStatement = InnerDataModel.Statements.FirstOrDefault(s => s.StatementId == StatemetnId);
                                InnerDataModel.DeleteObject(deletableStatement);
                                InnerDataModel.SaveChanges();
                                DeleteStatementSuccessfull = true;
                            }
                            transaction.Complete();
                        }
                    }
                    catch (Exception ex)
                    {
                        DeleteStatementSuccessfull = false;
                        ActionLogger.Logger.WriteLog(ex, true);
                    }
                }
                else
                {
                    try
                    {
                        if (statement != null && statement.EntriesByDEUs != null && statement.EntriesByDEUs.Count == 0)
                        {
                            using (DLinq.CommissionDepartmentEntities InnerDataModel = Entity.DataModel)
                            {
                                DLinq.Statement deletableStatement = InnerDataModel.Statements.FirstOrDefault(s => s.StatementId == StatemetnId);
                                InnerDataModel.DeleteObject(deletableStatement);
                                InnerDataModel.SaveChanges();
                                DeleteStatementSuccessfull = true;
                            }
                        }
                    }
                    catch
                    {
                        DeleteStatementSuccessfull = false;
                    }

                }
                return DeleteStatementSuccessfull;
            }
        }

        public bool IsStatementPartiallyOrFullyPaid()
        {
            using (DLinq.CommissionDepartmentEntities DataModel = Entity.DataModel)
            {
                bool IsPaid = false;
                List<PolicyPaymentEntry> Entries = DataModel.PolicyPaymentEntries.Where(s => s.StatementId == this.StatementID).ToList();
                if (Entries != null)
                {

                    foreach (PolicyPaymentEntry Entry in Entries)
                    {
                        bool Flag = PolicyOutgoingDistribution.GetOutgoingPaymentByPoicyPaymentEntryId(Entry.PaymentEntryId).Any(p => p.IsPaid == true);//Acme changed from -> (p => p.IsPaid == false);
                        if (Flag)
                        {
                            IsPaid = true;
                            break;
                        }
                    }
                }
                return IsPaid;
            }
        }


        public static int GetErrorCount(Guid statementID)
        {
            int count = 0;
            try
            {
                string sql = "Select count(entry.DEUEntryID) from entriesbyDEU entry Left join policypaymententries payment on entry.DEUEntryID = payment.DEUEntryID " +
                             "where entry.statementID = '" + statementID + "' and paymentEntryID is null and IsNull(IsProcessing,0) = 0";
                using (SqlConnection con = new SqlConnection(DBConnection.GetConnectionString()))
                {
                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        con.Open();
                        cmd.CommandTimeout = 0;
                        var result = cmd.ExecuteScalar();
                        Int32.TryParse(Convert.ToString(result), out count);
                    }
                }
            }
            catch(Exception ex)
            {
                ActionLogger.Logger.WriteLog("GetErrorCount request: exception: " + ex.Message, true);
            }

            return count;

        }

        public static int GetEntriesCount(Guid statementID)
        {
            int count = 0;
            try
            {
                //get only those that have 
                string sql = "Select count(entry.DEUEntryID) from entriesbyDEU entry where entry.statementID = '" + statementID + "'";
                using (SqlConnection con = new SqlConnection(DBConnection.GetConnectionString()))
                {
                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        con.Open();
                        cmd.CommandTimeout = 0;
                        var result = cmd.ExecuteScalar();
                        Int32.TryParse(Convert.ToString(result), out count);
                    }
                }
            }
            catch (Exception ex)
            {
                ActionLogger.Logger.WriteLog("GetErrorCount request: exception: " + ex.Message, true);
            }

            return count;

        }
        public static double CalculateCompletePercent(decimal? CheckAmount, decimal AdjAmount, decimal? EnteredAmount)
        {
           // ActionLogger.Logger.WriteLog("CalculateCompletePercent request: checkamount- "+ CheckAmount + ", adjamt: " + AdjAmount + ", entrd: " + EnteredAmount, true);

            try
            {
                decimal dbNetAmount = Convert.ToDecimal(CheckAmount) - Convert.ToDecimal(AdjAmount);
                double CompletePercentage = 0;
                if (dbNetAmount != 0)
                {
                    //double dbValue = Convert.ToDouble(dbNetAmount - CurrentStatement.EnteredAmount) / Convert.ToDouble(dbNetAmount);
                    double dbValue = Convert.ToDouble(EnteredAmount) / Convert.ToDouble(dbNetAmount);
                    CompletePercentage = dbValue * 100;
                }
                else
                    CompletePercentage = 0;

                return CompletePercentage; //.ToString("N", new CultureInfo("en-US"));
            }
            catch (Exception ex)
            {
                ActionLogger.Logger.WriteLog("CalculateCompletePercent: ex: " + ex.Message, true);
                return 0; //.ToString("C");
            }

        }

        #endregion
    }
}
