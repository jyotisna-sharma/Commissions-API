using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyAgencyVault.BusinessLibrary.Base;
using MyAgencyVault.BusinessLibrary.Masters;
using System.Runtime.Serialization;
using DLinq = DataAccessLayer.LinqtoEntity;
using System.Data.Objects.SqlClient;
using MyAgencyVault.BusinessLibrary.BusinessObjects;
using System.Data.EntityClient;
using System.Data.SqlClient;
using System.Data;
using System.Globalization;

namespace MyAgencyVault.BusinessLibrary
{
    [DataContract]
    public class PolicyPaymentEntriesPost
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
                    InvoiceDateString = Convert.ToDateTime(value).ToString("MMM dd, yyyy");
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
                if (value != null && string.IsNullOrEmpty(CreatedOnString))
                {
                    CreatedOnString = Convert.ToDateTime(value).ToString("MMM dd, yyyy");
                }
            }
        }
        string _CreatedOnString;
        [DataMember]
        public string CreatedOnString
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

        [DataMember]
        public decimal PaymentRecived { get; set; }
        [DataMember]
        public double CommissionPercentage { get; set; }
        [DataMember]
        public int NumberOfUnits { get; set; }
        [DataMember]
        public decimal DollerPerUnit { get; set; }
        [DataMember]
        public decimal Fee { get; set; }
        [DataMember]
        public double? SplitPer { get; set; }
        [DataMember]
        public decimal TotalPayment { get; set; }
        //[DataMember]
        //public DateTime CreatedOn { get; set; }
        [DataMember]
        public Guid CreatedBy { get; set; }
        [DataMember]
        public int? PostStatusID { get; set; }
        [DataMember]
        public Guid ClientId { get; set; }
        [DataMember]
        public decimal Bonus { get; set; }
        [DataMember]
        public Guid? DEUEntryId { get; set; }
        [DataMember]
        public int StmtNumber { get; set; }
        [DataMember]
        public String BatchNumber { get; set; }
        [DataMember]
        public DateTime? EntryDate { get; set; }
        [DataMember]
        public string Pageno { get; set; }
        [DataMember]
        public Guid? FollowUpVarIssueId { get; set; }
        //Newly added 29102013
        [DataMember]
        public int? FollowUpIssueResolveOrClosed { get; set; }

        //Newly added 21012014 for unlink payment from commision dashboard
        [DataMember]
        public bool? IsLinkPayment { get; set; }

        private static decimal? expectedpayment;

        public static decimal? Expectedpayment
        {
            get { return PolicyPaymentEntriesPost.expectedpayment; }
            set { PolicyPaymentEntriesPost.expectedpayment = value; }
        }

        public static void AddUpadate(PolicyPaymentEntriesPost policypaymententriespost)
        {
            try
            {
                using (DLinq.CommissionDepartmentEntities DataModel = Entity.DataModel)
                {
                    var _popaenpost = (from u in DataModel.PolicyPaymentEntries
                                       where (u.PaymentEntryId == policypaymententriespost.PaymentEntryID)
                                       select u).FirstOrDefault();
                    if (_popaenpost == null)
                    {
                        _popaenpost = new DLinq.PolicyPaymentEntry
                        {
                            PaymentEntryId = ((policypaymententriespost.PaymentEntryID == Guid.Empty) ? Guid.NewGuid() : policypaymententriespost.PaymentEntryID),
                            InvoiceDate = policypaymententriespost.InvoiceDate,
                            PaymentRecived = policypaymententriespost.PaymentRecived,
                            //IncomigPercentage = policypaymententriespost.IncomingPercentage,
                            CommissionPercentage = policypaymententriespost.CommissionPercentage,
                            NumberOfUnits = policypaymententriespost.NumberOfUnits,
                            DollerPerUnit = policypaymententriespost.DollerPerUnit,
                            Fee = policypaymententriespost.Fee,
                            //SharePercentage = policypaymententriespost.SharePercentage,
                            SplitPercentage = policypaymententriespost.SplitPer,
                            TotalPayment = policypaymententriespost.TotalPayment,
                            CreatedOn = policypaymententriespost.CreatedOn,
                            CreatedBy = policypaymententriespost.CreatedBy,
                            //ClientId = policypaymententriespost.ClientId,
                            Bonus = policypaymententriespost.Bonus,
                            // IsPaid = policypaymententriespost.IsPaid,
                            FollowUpVarIssueId = policypaymententriespost.FollowUpVarIssueId,
                            IsLinkPayment = policypaymententriespost.IsLinkPayment,
                            //Added code 29102013
                            //To idenify issue is manaully closed or resolved
                            // FollowUpIssueResolveOrClosed = policypaymententriespost.FollowUpIssueResolveOrClosed,
                        };
                        //_dtUsers.MasterRole = _masterRole;
                        _popaenpost.StatementReference.Value = (from f in DataModel.Statements where f.StatementId == policypaymententriespost.StmtID select f).FirstOrDefault();
                        _popaenpost.PolicyReference.Value = (from f in DataModel.Policies where f.PolicyId == policypaymententriespost.PolicyID select f).FirstOrDefault();
                        // _popaenpost.FollowupIssueReference.Value = (from f in DataModel.FollowupIssues where f.IssueId == policypaymententriespost.IssueID select f).FirstOrDefault();
                        _popaenpost.MasterPostStatuReference.Value = (from f in DataModel.MasterPostStatus where f.PostStatusID == policypaymententriespost.PostStatusID select f).FirstOrDefault();
                        _popaenpost.EntriesByDEUReference.Value = (from f in DataModel.EntriesByDEUs where f.DEUEntryID == policypaymententriespost.DEUEntryId select f).FirstOrDefault();
                        DataModel.AddToPolicyPaymentEntries(_popaenpost);
                    }
                    else
                    {
                        _popaenpost.InvoiceDate = policypaymententriespost.InvoiceDate;
                        _popaenpost.PaymentRecived = policypaymententriespost.PaymentRecived;
                        _popaenpost.CommissionPercentage = policypaymententriespost.CommissionPercentage;
                        _popaenpost.NumberOfUnits = policypaymententriespost.NumberOfUnits;
                        _popaenpost.DollerPerUnit = policypaymententriespost.DollerPerUnit;
                        _popaenpost.Fee = policypaymententriespost.Fee;
                        _popaenpost.SplitPercentage = policypaymententriespost.SplitPer;
                        _popaenpost.TotalPayment = policypaymententriespost.TotalPayment;
                        _popaenpost.CreatedOn = policypaymententriespost.CreatedOn;
                        _popaenpost.CreatedBy = policypaymententriespost.CreatedBy;
                        //_popaenpost.ClientId = policypaymententriespost.ClientId;
                        _popaenpost.Bonus = policypaymententriespost.Bonus;
                        _popaenpost.IsLinkPayment = policypaymententriespost.IsLinkPayment;
                        // _popaenpost.IsPaid = policypaymententriespost.IsPaid;                    
                    }
                    _popaenpost.StatementReference.Value = (from f in DataModel.Statements where f.StatementId == policypaymententriespost.StmtID select f).FirstOrDefault();
                    _popaenpost.PolicyReference.Value = (from f in DataModel.Policies where f.PolicyId == policypaymententriespost.PolicyID select f).FirstOrDefault();
                    // _popaenpost.FollowupIssueReference.Value = (from f in DataModel.FollowupIssues where f.IssueId == policypaymententriespost.IssueID select f).FirstOrDefault();
                    _popaenpost.MasterPostStatuReference.Value = (from f in DataModel.MasterPostStatus where f.PostStatusID == policypaymententriespost.PostStatusID select f).FirstOrDefault();
                    _popaenpost.EntriesByDEUReference.Value = (from f in DataModel.EntriesByDEUs where f.DEUEntryID == policypaymententriespost.DEUEntryId select f).FirstOrDefault();

                    DataModel.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                ActionLogger.Logger.WriteLog("Issue to addupdate payment in PolicyPaymentEntry (AddUpadate)  :" + ex.StackTrace.ToString(), true);
            }
        }

        public static void AddUpadateResolvedorClosed(PolicyPaymentEntriesPost policypaymententriespost)
        {
            ActionLogger.Logger.WriteLog("AddUpadateResolvedorClosed:Processing begins with PolicyPaymnetEntryData  :" + policypaymententriespost.ToString(), true);
            try
            {
                using (DLinq.CommissionDepartmentEntities DataModel = Entity.DataModel)
                {
                    var _popaenpost = (from u in DataModel.PolicyPaymentEntries
                                       where (u.PaymentEntryId == policypaymententriespost.PaymentEntryID)
                                       select u).FirstOrDefault();
                    if (_popaenpost == null)
                    {
                        _popaenpost = new DLinq.PolicyPaymentEntry
                        {
                            PaymentEntryId = ((policypaymententriespost.PaymentEntryID == Guid.Empty) ? Guid.NewGuid() : policypaymententriespost.PaymentEntryID),
                            InvoiceDate = policypaymententriespost.InvoiceDate,
                            PolicyId = policypaymententriespost.PolicyID,
                            CreatedOn = System.DateTime.Now,
                            CreatedBy = policypaymententriespost.CreatedBy,
                            FollowUpVarIssueId = policypaymententriespost.FollowUpVarIssueId,
                            //Added code 29102013
                            //To idenify issue is manaully closed or resolved
                            FollowUpIssueResolveOrClosed = policypaymententriespost.FollowUpIssueResolveOrClosed,
                        };

                        DataModel.AddToPolicyPaymentEntries(_popaenpost);
                        DataModel.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                ActionLogger.Logger.WriteLog("AddUpadateResolvedorClosed:Exception occurs with policypaymententriespost:" + policypaymententriespost.ToStringDump() + "Exception:" + ex.StackTrace.ToString(), true);
                throw ex;
            }
        }

        public static List<PolicyPaymentEntriesPost> GetAllResolvedorClosedIssueId(Guid? objpolicyId)
        {
            List<PolicyPaymentEntriesPost> objAllResolvedorClosed = new List<PolicyPaymentEntriesPost>();

            using (DLinq.CommissionDepartmentEntities DataModel = Entity.DataModel)
            {
                try
                {
                    objAllResolvedorClosed = (from u in DataModel.PolicyPaymentEntries
                                              where (u.PolicyId == objpolicyId && u.FollowUpIssueResolveOrClosed != null)
                                              select new PolicyPaymentEntriesPost
                                              {
                                                  PaymentEntryID = u.PaymentEntryId,
                                                  StmtID = u.StatementId ?? Guid.Empty,
                                                  PolicyID = u.PolicyId ?? Guid.Empty,
                                                  InvoiceDate = u.InvoiceDate ?? null,
                                                  CreatedOn = u.CreatedOn.Value,
                                                  CreatedBy = u.CreatedBy ?? Guid.Empty,
                                                  FollowUpVarIssueId = u.FollowUpVarIssueId,
                                                  //Added code 29102013
                                                  //To idenify issue is manaully closed or resolved
                                                  FollowUpIssueResolveOrClosed = u.FollowUpIssueResolveOrClosed,

                                              }
                                      ).ToList();
                }
                catch
                {
                }
                return objAllResolvedorClosed;
            }
        }

        //Update status when manully resolved or closed the status
        //When 1 then resolved
        //When 2 then closed
        public static void UpadateResolvedOrClosedbyManualy(Guid PaymentEntryID, int intId)
        {
            using (DLinq.CommissionDepartmentEntities DataModel = Entity.DataModel)
            {
                var _popaenpost = (from u in DataModel.PolicyPaymentEntries
                                   where (u.PaymentEntryId == PaymentEntryID)
                                   select u).FirstOrDefault();
                if (_popaenpost == null)
                {
                    _popaenpost = new DLinq.PolicyPaymentEntry
                    {
                        //Added code 29102013
                        //To idenify issue is manaully closed or resolved
                        FollowUpIssueResolveOrClosed = intId,
                    };
                    DataModel.SaveChanges();
                }
            }
        }

        public static void DeletePolicyPayentIdWise(Guid PaymentEntryId)
        {
            using (DLinq.CommissionDepartmentEntities DataModel = Entity.DataModel)
            {
                DLinq.PolicyPaymentEntry pom = DataModel.PolicyPaymentEntries.FirstOrDefault(s => s.PaymentEntryId == PaymentEntryId);
                if (pom != null)
                {
                    DataModel.DeleteObject(pom);
                    DataModel.SaveChanges();
                }
            }
        }

        public static List<PolicyPaymentEntriesPost> GetAllPaymentEntriesOfRange(DateTime FromDate, DateTime ToDate, Guid PolicyID)
        {
            return GetPolicyPaymentEntryPolicyIDWise(PolicyID).Where(p => p.InvoiceDate >= FromDate).Where(p => p.InvoiceDate <= ToDate).ToList<PolicyPaymentEntriesPost>();
        }

        public static PolicyPaymentEntriesPost GetPolicyPaymentEntry(Guid PolicyPaymentEntryID)
        {
            using (DLinq.CommissionDepartmentEntities DataModel = Entity.DataModel)
            {
                PolicyPaymentEntriesPost _popaenpost = (from u in DataModel.PolicyPaymentEntries
                                                        where (u.PaymentEntryId == PolicyPaymentEntryID)
                                                        select new PolicyPaymentEntriesPost
                                                        {
                                                            PaymentEntryID = u.PaymentEntryId,
                                                            StmtID = u.StatementId ?? Guid.Empty,
                                                            PolicyID = u.PolicyId ?? Guid.Empty,
                                                            //IssueID = u.IssueID.Value,
                                                            InvoiceDate = u.InvoiceDate.Value,
                                                            PaymentRecived = u.PaymentRecived.Value,
                                                            CommissionPercentage = u.CommissionPercentage.Value,
                                                            NumberOfUnits = u.NumberOfUnits.Value,
                                                            DollerPerUnit = u.DollerPerUnit.Value,
                                                            Fee = u.Fee.Value,
                                                            SplitPer = u.SplitPercentage.Value,
                                                            TotalPayment = u.TotalPayment.Value,
                                                            CreatedOn = u.CreatedOn.Value,
                                                            CreatedBy = u.CreatedBy ?? Guid.Empty,
                                                            PostStatusID = u.PostStatusID.Value,
                                                            //ClientId = u.ClientId ?? Guid.Empty,
                                                            Bonus = u.Bonus.Value,
                                                            //  IsPaid = u.IsPaid,
                                                            // PercentageOfPremium=u.PerOfPremium.Value,
                                                            DEUEntryId = u.DEUEntryId ?? Guid.Empty,
                                                            StmtNumber = u.Statement.StatementNumber,
                                                            FollowUpVarIssueId = u.FollowUpVarIssueId,
                                                            //Added code 29102013
                                                            //To idenify issue is manaully closed or resolved
                                                            FollowUpIssueResolveOrClosed = u.FollowUpIssueResolveOrClosed,
                                                            //Check payment is link or not
                                                            IsLinkPayment = u.IsLinkPayment ?? null,

                                                        }
                                   ).FirstOrDefault();
                return _popaenpost;
            }
        }

        public static List<PolicyPaymentEntriesPost> GetPolicyPaymentEntryStatementWise(Guid StmtId)
        {
            var _popaenpost = new List<PolicyPaymentEntriesPost>();
            try
            {
                ActionLogger.Logger.WriteLog("GetPolicyPaymentEntryStatementWise:process begins with PolicyPaymentEntries " + " " + "StatementId:" + StmtId, true);
                using (DLinq.CommissionDepartmentEntities DataModel = Entity.DataModel)
                {
                    _popaenpost = (from u in DataModel.PolicyPaymentEntries
                                   where (u.StatementId == StmtId)
                                   select new PolicyPaymentEntriesPost
                                   {
                                       PaymentEntryID = u.PaymentEntryId,
                                       StmtID = u.StatementId.Value,
                                       PolicyID = u.PolicyId.Value,
                                       //IssueID = u.IssueID.Value,
                                       InvoiceDate = u.InvoiceDate.Value,
                                       PaymentRecived = u.PaymentRecived.Value,
                                       CommissionPercentage = u.CommissionPercentage.Value,
                                       NumberOfUnits = u.NumberOfUnits.Value,
                                       DollerPerUnit = u.DollerPerUnit.Value,
                                       Fee = u.Fee.Value,
                                       SplitPer = u.SplitPercentage.Value,
                                       TotalPayment = u.TotalPayment.Value,
                                       CreatedOn = u.CreatedOn.Value,
                                       CreatedBy = u.CreatedBy.Value,
                                       PostStatusID = u.PostStatusID.Value,
                                       //ClientId = u.ClientId ?? Guid.Empty,
                                       //IsPaid = u.IsPaid,
                                       DEUEntryId = u.DEUEntryId ?? Guid.Empty,
                                       StmtNumber = u.Statement.StatementNumber,
                                       FollowUpVarIssueId = u.FollowUpVarIssueId,
                                       //Added code 29102013
                                       //To idenify issue is manaully closed
                                       FollowUpIssueResolveOrClosed = u.FollowUpIssueResolveOrClosed,
                                       //Check payment is link or not
                                       IsLinkPayment = u.IsLinkPayment ?? null,
                                   }).ToList();
                }
            }
            catch (Exception ex)
            {
                ActionLogger.Logger.WriteLog("GetPolicyPaymentEntryStatementWise:Exception occur while fetching list of policyPaymentEntries " + " " + "StatementId:" + StmtId + " " + "Exception:" + ex.Message, true);
            }
            return _popaenpost;
        }

        /// <summary>
        /// Author: Jyotisna 
        /// Date: Jan 31, 2019
        /// Purpose: to get count of issue with a policy ID 
        /// </summary>
        /// <param name="policyID"></param>
        /// <returns></returns>
        public static bool HasPaymentEntries(Guid PolicyID)
        {
            int count = 0;
            try
            {
                using (DLinq.CommissionDepartmentEntities DataModel = Entity.DataModel)
                {
                    var result = DataModel.GetPolicyPaymentEntryPolicyIDWise(PolicyID).ToList();
                    count = (result != null) ? result.Count : 0;
                }
            }
            catch (Exception ex)
            {
                ActionLogger.Logger.WriteLog("exception in getting payment entries: " + ex.Message, true);
            }
            return count > 0;
        }

        public static bool IfPolicyHasPayments(Guid PolicyID)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(DBConnection.GetConnectionString()))
                {
                    using (System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("GetPolicyPaymentEntryPolicyIDWise", con))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.CommandTimeout = 0;
                        cmd.Parameters.AddWithValue("@PolicyID", PolicyID);
                        con.Open();
                        System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            ActionLogger.Logger.WriteLog("IfPolicyHasPayments rows found ", true);
                            return true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ActionLogger.Logger.WriteLog("IfPolicyHasPayments exception - " + ex.Message, true);
            }
            ActionLogger.Logger.WriteLog("IfPolicyHasPayments rows NOT found ", true);
            return false;
        }

        public static List<PolicyPaymentEntriesPost> GetPolicyPaymentEntryPolicyIDWise(Guid PolicyID)
        {
            List<PolicyPaymentEntriesPost> _popaenpost = new List<PolicyPaymentEntriesPost>();
            try
            {
                using (SqlConnection con = new SqlConnection(DBConnection.GetConnectionString()))
                {
                    using (System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("GetPolicyPaymentEntryPolicyIDWise", con))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.CommandTimeout = 0;
                        cmd.Parameters.AddWithValue("@PolicyID", PolicyID);
                        con.Open();
                        System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            PolicyPaymentEntriesPost payment = new PolicyPaymentEntriesPost();
                            payment.PaymentEntryID = (Guid)reader["PaymentEntryID"];
                            payment.StmtID = (Guid)reader["StatementId"];
                            payment.PolicyID = (Guid)reader["PolicyID"];

                            //string strInvoice = Convert.ToString(reader["InvoiceDate"]);
                            //DateTime dtInvoice = DateTime.MinValue;
                            //DateTime.TryParse(strInvoice, out dtInvoice);
                            payment.InvoiceDate = (reader["InvoiceDate"] == DBNull.Value) ? (DateTime?)null : (DateTime)reader["InvoiceDate"];

                            decimal paymnt = 0;
                            decimal.TryParse(Convert.ToString(reader["PaymentRecived"]), out paymnt);
                            payment.PaymentRecived = paymnt;

                            double dbl = 0;
                            double.TryParse(Convert.ToString(reader["CommissionPercentage"]), out dbl);
                            payment.CommissionPercentage = dbl;

                            int units = 0;
                            int.TryParse(Convert.ToString(reader["NumberOfUnits"]), out units);
                            payment.NumberOfUnits = units;

                            paymnt = 0;
                            decimal.TryParse(Convert.ToString(reader["DollerPerUnit"]), out paymnt);
                            payment.DollerPerUnit = paymnt;

                            paymnt = 0;
                            decimal.TryParse(Convert.ToString(reader["Fee"]), out paymnt);
                            payment.Fee = paymnt;

                            dbl = 0;
                            double.TryParse(Convert.ToString(reader["SplitPercentage"]), out dbl);
                            payment.SplitPer = dbl;

                            paymnt = 0;
                            decimal.TryParse(Convert.ToString(reader["TotalPayment"]), out paymnt);
                            payment.TotalPayment = paymnt;

                            payment.CreatedOn = (reader["CreatedOn"] == DBNull.Value) ? (DateTime?)null : (DateTime)reader["CreatedOn"];
                            payment.CreatedBy = (reader["CreatedBy"] == DBNull.Value) ? Guid.Empty : (Guid)reader["CreatedBy"];

                            units = 0;
                            int.TryParse(Convert.ToString(reader["PostStatusID"]), out units);
                            payment.PostStatusID = units;

                            payment.DEUEntryId = (Guid?)reader["DEUEntryId"];

                            units = 0;
                            int.TryParse(Convert.ToString(reader["StatementNumber"]), out units);
                            payment.StmtNumber = units;
                            payment.BatchNumber = Convert.ToString(reader["BatchNumber"]);
                            payment.EntryDate = (reader["EntryDate"] == DBNull.Value) ? (DateTime?)null : (DateTime?)reader["EntryDate"];
                            payment.StatementDate = (reader["StatementDate"] == DBNull.Value) ? (DateTime?)null : (DateTime?)reader["StatementDate"];
                            payment.IsLinkPayment = (reader["IsLinkPayment"] == DBNull.Value) ? (bool?)null : (bool?)reader["IsLinkPayment"];
                            payment.Pageno = Convert.ToString(reader["Pageno"]);
                            payment.FollowUpVarIssueId = (reader["FollowUpVarIssueId"] == DBNull.Value) ? (Guid?)null : (Guid?)reader["FollowUpVarIssueId"];

                            _popaenpost.Add(payment);

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ActionLogger.Logger.WriteLog("GetPolicyPaymentEntryPolicyIDWise exception - " + ex.Message, true);
            }
            /*  using (DLinq.CommissionDepartmentEntities DataModel = Entity.DataModel)
              {
                  var Result = DataModel.GetPolicyPaymentEntryPolicyIDWise(PolicyID);

                  _popaenpost = (from u in Result
                                 join st in DataModel.Statements on u.StatementId equals st.StatementId
                                 where (u.PolicyID == PolicyID)
                                 select new PolicyPaymentEntriesPost
                                 {

                                     PaymentEntryID = u.PaymentEntryID,
                                     StmtID = u.StatementId.Value,
                                     PolicyID = u.PolicyID.Value,
                                     // IssueID = u.IssueID.Value,
                                     InvoiceDate = u.InvoiceDate,
                                     PaymentRecived = u.PaymentRecived ?? 0,
                                     CommissionPercentage = u.CommissionPercentage ?? 0,
                                     NumberOfUnits = u.NumberOfUnits ?? 0,
                                     DollerPerUnit = u.DollerPerUnit ?? 0,
                                     Fee = u.Fee.Value,
                                     SplitPer = u.SplitPercentage ?? 0,
                                     TotalPayment = u.TotalPayment ?? 0,
                                     CreatedOn = u.CreatedOn.Value,
                                     CreatedBy = u.CreatedBy ?? Guid.Empty,
                                     PostStatusID = u.PostStatusID,
                                     //ClientId = u.ClientId ?? Guid.Empty,
                                     // IsPaid = u.IsPaid,
                                     DEUEntryId = u.DEUEntryId ?? Guid.Empty,
                                     StmtNumber = u.StatementNumber,
                                     FollowUpVarIssueId = u.FollowUpVarIssueId,
                                     BatchNumber = u.BatchNumber,
                                     EntryDate = u.EntryDate,
                                     Pageno = u.Pageno,
                                     //IsLinkPayment=
                                     IsLinkPayment = u.IsLinkPayment ?? null,
                                     StatementDate = st.StatementDate

                                 }
                                     ).ToList();
              }
        }
catch
{
}*/
            return _popaenpost;
        }


        public static List<PolicyIncomingPaymentObject> GetPolicyIncomingPaymentList(Guid policyId, ListParams listParams)
        {
            List<PolicyIncomingPaymentObject> _popaenpost = new List<PolicyIncomingPaymentObject>();
            try
            {
                ActionLogger.Logger.WriteLog(DateTime.Now.ToString() + "GetPolicyIncomingPaymentList:Incoming Payment request with PolicyId: " + policyId, true);
                List<PolicyIncomingPaymentObject> _PolicyOutgoingDistribution = new List<PolicyIncomingPaymentObject>();

                using (SqlConnection con = new SqlConnection(DBConnection.GetConnectionString()))
                {
                    using (SqlCommand cmd = new SqlCommand("Usp_GetIncomingPaymentList", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@policyId", policyId);
                        con.Open();
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            try
                            {
                                PolicyIncomingPaymentObject objPolicyDetailsData = new PolicyIncomingPaymentObject();
                                PolicyIncomingPaymentObject exportData = new PolicyIncomingPaymentObject();
                                if (!string.IsNullOrEmpty(Convert.ToString(reader["PaymentEntryId"])))
                                {
                                    objPolicyDetailsData.PaymentEntryID = (Guid)(reader["PaymentEntryId"]);
                                }
                                if (!string.IsNullOrEmpty(Convert.ToString(reader["StatementId"])))
                                {
                                    objPolicyDetailsData.StmtID = (Guid)(reader["StatementId"]);
                                }
                                if (!string.IsNullOrEmpty(Convert.ToString(reader["PolicyId"])))
                                {
                                    objPolicyDetailsData.PolicyID = (Guid)(reader["PolicyId"]);
                                }
                                if (!string.IsNullOrEmpty(Convert.ToString(reader["InvoiceDate"])))
                                {
                                    objPolicyDetailsData.InvoiceDate = Convert.ToDateTime(reader["InvoiceDate"]);
                                }
                                if (!string.IsNullOrEmpty(Convert.ToString(reader["PaymentRecived"])))
                                {
                                    objPolicyDetailsData.PaymentRecived = Convert.ToString(reader["PaymentRecived"]);
                                }
                                if (!string.IsNullOrEmpty(Convert.ToString(reader["CommissionPercentage"])))
                                {
                                    objPolicyDetailsData.CommissionPercentage = Convert.ToString(reader["CommissionPercentage"]);
                                }
                                if (!string.IsNullOrEmpty(Convert.ToString(reader["NumberOfUnits"])))
                                {
                                    objPolicyDetailsData.NumberOfUnits = Convert.ToString(reader["NumberOfUnits"]);
                                }
                                if (!string.IsNullOrEmpty(Convert.ToString(reader["DollerPerUnit"])))
                                {
                                    objPolicyDetailsData.DollerPerUnit = Convert.ToString(reader["DollerPerUnit"]);
                                }
                                if (!string.IsNullOrEmpty(Convert.ToString(reader["Fee"])))
                                {
                                    objPolicyDetailsData.Fee = Convert.ToString(reader["Fee"]);
                                }
                                if (!string.IsNullOrEmpty(Convert.ToString(reader["SplitPercentage"])))
                                {
                                    objPolicyDetailsData.SplitPer = Convert.ToString(reader["SplitPercentage"]);
                                }
                                if (!string.IsNullOrEmpty(Convert.ToString(reader["TotalPayment"])))
                                {
                                    objPolicyDetailsData.TotalPayment = Convert.ToString(reader["TotalPayment"]);
                                }
                                if (!string.IsNullOrEmpty(Convert.ToString(reader["DEUEntryId"])))
                                {
                                    objPolicyDetailsData.DEUEntryId = (Guid)(reader["DEUEntryId"]);
                                }
                                if (!string.IsNullOrEmpty(Convert.ToString(reader["StatementNumber"])))
                                {
                                    objPolicyDetailsData.StmtNumber = Convert.ToString(reader["StatementNumber"]);
                                }
                                if (!string.IsNullOrEmpty(Convert.ToString(reader["FollowUpVarIssueId"])))
                                {
                                    objPolicyDetailsData.FollowUpVarIssueId = (Guid)(reader["FollowUpVarIssueId"]);
                                }
                                if (!string.IsNullOrEmpty(Convert.ToString(reader["StatementNumber"])))
                                {
                                    objPolicyDetailsData.BatchNumber = Convert.ToString(reader["BatchNumber"]);
                                }
                                if (!string.IsNullOrEmpty(Convert.ToString(reader["FileName"])))
                                {
                                    objPolicyDetailsData.FileName = Convert.ToString(reader["FileName"]);
                                }
                                if (!string.IsNullOrEmpty(Convert.ToString(reader["StatementDate"])))
                                {
                                    objPolicyDetailsData.StatementDate = Convert.ToDateTime(reader["StatementDate"]);
                                }
                                if (!string.IsNullOrEmpty(Convert.ToString(reader["Pageno"])))
                                {
                                    objPolicyDetailsData.Pageno = Convert.ToString(reader["Pageno"]);
                                }
                                if (System.DBNull.Value != reader["IsEntrybyCommissiondashBoard"])
                                {
                                    objPolicyDetailsData.IsEntrybyCommissiondashBoard = Convert.ToBoolean(reader["IsEntrybyCommissiondashBoard"]);
                                }
                                if (System.DBNull.Value != reader["IsLinkPayment"])
                                {
                                    objPolicyDetailsData.IsLinkPayment = Convert.ToBoolean(reader["IsLinkPayment"]);

                                }
                                if (System.DBNull.Value != reader["StatementId"])
                                {
                                    objPolicyDetailsData.StatementId = (Guid)(reader["StatementId"]);

                                }
                                if (System.DBNull.Value != reader["BatchId"])
                                {
                                    objPolicyDetailsData.BatchId = (Guid)(reader["BatchId"]);

                                }

                                _PolicyOutgoingDistribution.Add(objPolicyDetailsData);
                            }
                            catch (Exception ex)
                            {
                                ActionLogger.Logger.WriteLog(DateTime.Now.ToString() + "GetOutgoingPaymentByPoicyPaymentEntryId:Exception occur while fetching list of OutgoingPayments with PolicyId: " + policyId + " " + "Exception:" + " " + ex.Message, true);
                            }

                        }
                        // Call Close when done reading.
                        reader.Close();
                    }

                }
                return _PolicyOutgoingDistribution;
            }

            catch (Exception ex)

            {
                ActionLogger.Logger.WriteLog(DateTime.Now.ToString() + "GetOutgoingPaymentByPoicyPaymentEntryId:Exception occur while fetching list of OutgoingPayments with PolicyId: " + policyId + " " + "Exception:" + " " + ex.Message, true);
              
            }
            return _popaenpost;
        }

        public static List<PolicyPaymentEntriesPost> GetPolicyGreatestInvoiceDate(Guid PolicyID)
        {
            List<PolicyPaymentEntriesPost> PaymentInvoiceDate = new List<PolicyPaymentEntriesPost>();
            try
            {
                using (DLinq.CommissionDepartmentEntities DataModel = Entity.DataModel)
                {
                    PaymentInvoiceDate = (from u in DataModel.PolicyPaymentEntries
                                          where (u.PolicyId == PolicyID)
                                          select new PolicyPaymentEntriesPost
                                          {
                                              PaymentEntryID = u.PaymentEntryId,
                                              InvoiceDate = u.InvoiceDate
                                          }
                                        ).ToList();
                }

            }
            catch
            {
            }
            return PaymentInvoiceDate;
        }

        //add on 10/01/2012 get total payment at that invoice
        public static decimal GetTotalpaymentOnInvoiceDate(Guid policyID, DateTime dtTime)
        {
            using (DLinq.CommissionDepartmentEntities DataModel = Entity.DataModel)
            {
                List<PolicyPaymentEntriesPost> PaymentInvoiceDate = (from u in DataModel.PolicyPaymentEntries
                                                                     where (u.PolicyId == policyID && u.InvoiceDate == dtTime)
                                                                     select new PolicyPaymentEntriesPost
                                                                     {
                                                                         //PaymentEntryID = u.PaymentEntryId,
                                                                         //InvoiceDate = u.InvoiceDate,
                                                                         TotalPayment = u.TotalPayment.Value,
                                                                     }

                                    ).ToList();


                Decimal D = PaymentInvoiceDate.Sum(P => P.TotalPayment);
                return D;
            }
        }


        public static bool IfPaymentExistsInPaymentEntries(Guid DeuEntryId, out Guid paymentEntryID,out Guid paymentPolicyID, out Guid stmtID)
        {
            paymentEntryID = Guid.Empty;
            paymentPolicyID = Guid.Empty;
            stmtID = Guid.Empty;

            using (DLinq.CommissionDepartmentEntities DataModel = Entity.DataModel)
            {
                PolicyPaymentEntriesPost record = (from u in DataModel.PolicyPaymentEntries
                              where (u.DEUEntryId == DeuEntryId)
                              select new PolicyPaymentEntriesPost { PaymentEntryID = u.PaymentEntryId , PolicyID = u.PolicyId ?? Guid.Empty, StmtID = u.StatementId ?? Guid.Empty }).FirstOrDefault();

                if (record != null)
                {
                    paymentEntryID = record.PaymentEntryID;
                    paymentPolicyID = record.PolicyID;
                    stmtID = record.StmtID;
                    return true;
                }

                return false;
            }
        }


        /// <summary>
        /// Method to delete payment entry
        /// From DEU
        /// </summary>
        /// <param name="PaymentEntryID"></param>
        /// <param name="UserID"></param>
        public static void DeletePaymentEntry(Guid PaymentEntryID, ref PostProcessWebStatus status /*out int entries, out decimal? entered, out int stmtStatus, out int batchStatus, out string completePer*/)
        {
            int entries = 0;
       //     int stmtStatus = 0;
         //   int batchStatus = 0;
        //    string completePer = "";
            try
            {
                using (SqlConnection con = new SqlConnection(DBConnection.GetConnectionString()))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_DeletePaymentEntry", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@PaymentEntryId", PaymentEntryID);

                       /* cmd.Parameters.Add("@entryCount", SqlDbType.Int);
                        cmd.Parameters["@entryCount"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add("@bal", SqlDbType.Money);
                        cmd.Parameters["@bal"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add("@checkAmount", SqlDbType.Money);
                        cmd.Parameters["@checkAmount"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add("@entered", SqlDbType.Money);
                        cmd.Parameters["@entered"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add("@statementStatus", SqlDbType.Int);
                        cmd.Parameters["@statementStatus"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add("@batchStatus", SqlDbType.Int);
                        cmd.Parameters["@batchStatus"].Direction = ParameterDirection.Output;*/

                        con.Open();
                        SqlDataReader dr =  cmd.ExecuteReader();

                        while (dr.Read())
                        {
                            entries = (int)dr["Entries"];
                            status.StatementStatusID = (int)dr["statementStatus"];
                            status.BatchStatusID = (int)dr["batchStatus"];
                            status.EnteredAmount = dr.IsDBNull("EnteredAmount") ? 0.ToString("C") : ((decimal)dr["EnteredAmount"]).ToString("N", new CultureInfo("en-US")); ; ;
                            decimal bal = dr.IsDBNull("balance") ? 0 : (decimal)dr["balance"];
                            decimal check = dr.IsDBNull("Checkamount") ? 0 : (decimal)dr["Checkamount"];
                            decimal entered = dr.IsDBNull("EnteredAmount") ? 0 : (decimal)dr["EnteredAmount"];
                            status.CompletePercent = Statement.CalculateCompletePercent(check, bal, entered).ToString("N", new CultureInfo("en-US"));
                            status.PayorId = (Guid)dr["PayorID"];
                            status.TemplateId = dr.IsDBNull("TemplateID") ? Guid.Empty : (Guid)dr["TemplateID"];
                            status.ErrorCount = dr.IsDBNull("ErrorCount") ? 0: (int)dr["ErrorCount"];
                        }
                     /*   entries = (int)cmd.Parameters["@entryCount"].Value;
                        stmtStatus = (int)cmd.Parameters["@statementStatus"].Value;
                        batchStatus = (int)cmd.Parameters["@batchStatus"].Value;
                        entered = (decimal)cmd.Parameters["@entered"].Value;
                        decimal bal = (decimal)cmd.Parameters["@bal"].Value;
                        decimal check = (decimal)cmd.Parameters["@checkAmount"].Value;
                        completePer = Statement.CalculateCompletePercent(check,bal,entered).ToString("N", new CultureInfo("en-US")); */
                    }
                }
            }
            catch (Exception ex)
            {
                ActionLogger.Logger.WriteLog(DateTime.Now.ToString() + "DeletePaymentAndIssues exception: " + ex.Message, true);
                throw ex;
            }
        }

        public static PolicyPaymentEntriesPost GetPolicyPaymentEntryDEUEntryIdWise(Guid DeuEntryId)
        {
            PolicyPaymentEntriesPost _popaenpost = null;
            try
            {
                using (DLinq.CommissionDepartmentEntities DataModel = Entity.DataModel)
                {
                    _popaenpost = (from u in DataModel.PolicyPaymentEntries
                                   where (u.DEUEntryId == DeuEntryId)
                                   select new PolicyPaymentEntriesPost
                                   {
                                       PaymentEntryID = u.PaymentEntryId,
                                       StmtID = u.StatementId ?? Guid.Empty,
                                       PolicyID = u.PolicyId ?? Guid.Empty,
                                       //IssueID = u.IssueID.Value,
                                       InvoiceDate = u.InvoiceDate.Value,
                                       PaymentRecived = u.PaymentRecived.Value,
                                       CommissionPercentage = u.CommissionPercentage.Value,
                                       NumberOfUnits = u.NumberOfUnits.Value,
                                       DollerPerUnit = u.DollerPerUnit.Value,
                                       Fee = u.Fee.Value,
                                       SplitPer = u.SplitPercentage.Value,
                                       TotalPayment = u.TotalPayment.Value,
                                       CreatedOn = u.CreatedOn.Value,
                                       CreatedBy = u.CreatedBy ?? Guid.Empty,
                                       PostStatusID = u.PostStatusID.Value,
                                       //ClientId = u.ClientId ?? Guid.Empty,
                                       Bonus = u.Bonus.Value,
                                       //  IsPaid = u.IsPaid,
                                       // PercentageOfPremium=u.PerOfPremium.Value,
                                       DEUEntryId = u.DEUEntryId ?? Guid.Empty,
                                       StmtNumber = u.Statement.StatementNumber,
                                       FollowUpVarIssueId = u.FollowUpVarIssueId,
                                       //Added code 29102013
                                       //To idenify issue is manaully closed
                                       FollowUpIssueResolveOrClosed = u.FollowUpIssueResolveOrClosed,
                                       //Check payment is link or not
                                       IsLinkPayment = u.IsLinkPayment ?? null,

                                   }
                                ).FirstOrDefault();
                }

            }
            catch (Exception ex)
            {
                ActionLogger.Logger.WriteLog("GetPolicyPaymentEntryDEUEntryIdWise ex: " + ex.Message, true);
            }
            return _popaenpost;
        }
        //   public double? PercentageOfPremium { get; set; }
        public static void UpdateExpectedPayment(decimal? expectedpayment, Guid PolicyPaymentEntryId)
        {
            try
            {
                using (DLinq.CommissionDepartmentEntities DataModel = Entity.DataModel)
                {
                    DataModel.PolicyPaymentEntries.Where(p => p.PaymentEntryId == PolicyPaymentEntryId).FirstOrDefault().ExpectedPayment = expectedpayment;
                    DataModel.SaveChanges();
                }
            }
            catch
            {
            }
        }
        /// <summary>
        /// Upadte the Payment with the Followup issue id shows Variense in payment
        /// </summary>
        /// <param name="PolicyPaymententryId"></param>
        /// <param name="FollowupIssueId"></param>
        public static void UpdateVarPaymentIssueId(Guid PolicyPaymententryId, Guid FollowupIssueId)
        {
            try
            {
                using (DLinq.CommissionDepartmentEntities DataModel = Entity.DataModel)
                {
                    DataModel.PolicyPaymentEntries.Where(p => p.PaymentEntryId == PolicyPaymententryId).FirstOrDefault().FollowUpVarIssueId = FollowupIssueId;
                    DataModel.SaveChanges();
                }
            }
            catch
            {
            }
        }

        public static void UpdateVarPaymentIssueIDValue(Guid FollowUpId1, Guid? FollowUpId2)
        {
            using (DLinq.CommissionDepartmentEntities DataModel = Entity.DataModel)
            {
                DataModel.PolicyPaymentEntries.Where(p => p.FollowUpVarIssueId == FollowUpId1).FirstOrDefault().FollowUpVarIssueId = FollowUpId2;
                DataModel.SaveChanges();
            }
        }
    }
}

