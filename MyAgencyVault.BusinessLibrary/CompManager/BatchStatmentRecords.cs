using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyAgencyVault.BusinessLibrary.Base;
using System.Runtime.Serialization;
using DLinq = DataAccessLayer.LinqtoEntity;
using DataAccessLayer.LinqtoEntity;
using MyAgencyVault.BusinessLibrary.Masters;
using MyAgencyVault.BusinessLibrary.BusinessObjects;
using System.Data.SqlClient;
using System.Data;

namespace MyAgencyVault.BusinessLibrary
{

    [DataContract]
    public class BatchStatmentRecords
    {
        [DataMember]
        public Guid PayorId { get; set; }
        [DataMember]
        public string PayorNickName { get; set; }

        [DataMember]
        public string PayorName { get; set; }

        [DataMember]
        public Guid StatmentId { get; set; }
        [DataMember]
        public int? StatmentNumber { get; set; }
        [DataMember]
        public decimal? CheckAmount { get; set; }
        [DataMember]
        public string CheckAmountString { get; set; }
        [DataMember]
        public decimal? House { get; set; }
        [DataMember]
        public string HouseString { get; set; }
        [DataMember]
        public decimal? Remaining { get; set; }
        [DataMember]
        public string RemainingString { get; set; }
        [DataMember]
        public double? DonePer { get; set; }
        [DataMember]
        public string DonePerString { get; set; }
        [DataMember]
        public int Entries { get; set; }
        [DataMember]
        public int? StmtStatus { get; set; }
        [DataMember]
        public string StmtStatusString { get; set; }

        [DataMember]
        public string TotalCheckAmount { get; set; }
        [DataMember]
        public decimal? BalAdj { get; set; }


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
        public decimal? EnterAmount { get; set; }
        public static decimal GetBatchTotal(Guid BatchId)
        {
            using (DLinq.CommissionDepartmentEntities DataModel = Entity.DataModel)
            {
                decimal? TotalStatementAmount = (from stValue in DataModel.Statements
                                                 where stValue.BatchId == BatchId
                                                 select stValue.EnteredAmount).Sum();
                return Convert.ToDecimal(TotalStatementAmount);
            }
        }

        private static double SumValueOperation(double? objDbPaidAmount, ref double totalValue)
        {
            double dbPaidAmount = 0;
            try
            {
                if (objDbPaidAmount != null)
                {
                    dbPaidAmount = Convert.ToDouble(objDbPaidAmount);
                }
            }
            catch (Exception)
            {
            }

            totalValue += dbPaidAmount;
            return totalValue;
        }

        //private static double SumValueOperation(double Value, ref double totalValue)
        //{
        //    return totalValue += Value;
        //}

        private static BatchStatmentRecords SetStatementValues(DLinq.Statement statement, Guid HouseOwner)
        {
            BatchStatmentRecords _BSR = new BatchStatmentRecords();

            try
            {
                _BSR.PayorId = statement.PayorId ?? Guid.Empty;
                Payor payor = Payor.GetPayorByID(_BSR.PayorId);

                if (payor != null)
                    _BSR.PayorNickName = payor.NickName;
                else
                    _BSR.PayorNickName = string.Empty;

                _BSR.StatmentId = statement.StatementId;
                _BSR.StatmentNumber = statement.StatementNumber;
                _BSR.CheckAmount = statement.CheckAmount;
                _BSR.StatementDate = statement.StatementDate;
                _BSR.CheckAmountString = "$" + Math.Round((decimal)_BSR.CheckAmount,2);


                //_BSR.CheckAmount = ((statement.CheckAmount ?? 0) + (statement.BalAdj ?? 0));

                double HouseValue = 0;
                double totalValue = 0;

                //statement.PolicyPaymentEntries.ToList().ForEach(pStatement => pStatement.PolicyOutgoingPayments.ToList().ForEach(pOutGoing => SumValueOperation(pOutGoing.PaidAmount.Value, ref totalValue)));
                //statement.PolicyPaymentEntries.ToList().ForEach(pStatement => pStatement.PolicyOutgoingPayments.ToList().Where(pOutGoing => pOutGoing.RecipientUserCredentialId == HouseOwner).ToList().ForEach(pOutGoing1 => SumValueOperation(pOutGoing1.PaidAmount.Value, ref HouseValue)));

                statement.PolicyPaymentEntries.ToList().ForEach(pStatement => pStatement.PolicyOutgoingPayments.ToList().ForEach(pOutGoing => SumValueOperation(pOutGoing.PaidAmount, ref totalValue)));
                statement.PolicyPaymentEntries.ToList().ForEach(pStatement => pStatement.PolicyOutgoingPayments.ToList().Where(pOutGoing => pOutGoing.RecipientUserCredentialId == HouseOwner).ToList().ForEach(pOutGoing1 => SumValueOperation(pOutGoing1.PaidAmount, ref HouseValue)));

                decimal? totaldismon = (decimal?)totalValue;
                _BSR.House = (decimal?)HouseValue;// GetHouseOwnerDistributedMoneyStmtWise(_BSR.StatmentId, BatchId);

                //_BSR.Remaining = _BSR.CheckAmount - totaldismon;
                //Added after suggestion of kevin and Eric
                //TotalDonePercent = (((NetCheck – RemainingAmt) / Net Check)) 
                //Remaining = Net Check – Entered Amount
                //Net check fpormula
                //net check = CheckAmount- BalAdj

                decimal dcNetCheck = Convert.ToDecimal(_BSR.CheckAmount - statement.BalAdj);
                //Update formula
                _BSR.Remaining = dcNetCheck - totaldismon;

                _BSR.DonePer = 0;

                //if (_BSR.CheckAmount.HasValue)
                //{
                //    if (_BSR.CheckAmount.Value != 0 && totaldismon != 0)
                //        _BSR.DonePer = (((double)(totaldismon ?? 0)) * 100) / ((double)_BSR.CheckAmount.Value);
                //}

                if (dcNetCheck != 0 && dcNetCheck > 0)
                {
                    _BSR.DonePer = Convert.ToDouble((dcNetCheck - _BSR.Remaining) / dcNetCheck);
                    if (_BSR.DonePer > 0)
                    {
                        _BSR.DonePer = _BSR.DonePer * 100;
                    }
                }

                _BSR.Entries = (int)statement.Entries;
                _BSR.StmtStatus = statement.MasterStatementStatu.StatementStatusId;
                _BSR.StmtStatusString = statement.MasterStatementStatu.Name;

                decimal houseAmt = 0;
                decimal.TryParse(Convert.ToString(_BSR.House), out houseAmt);
                _BSR.HouseString = houseAmt.ToString("C");


                //_BSR.HouseString =  "$" + Math.Round((decimal)_BSR.House, 2);
                decimal remainAmt = 0;
                decimal.TryParse(Convert.ToString(_BSR.Remaining), out remainAmt);
                _BSR.RemainingString = remainAmt.ToString("C"); //"$" + Math.Round((decimal)_BSR.Remaining, 2);

                decimal donePer = 0;
                decimal.TryParse(Convert.ToString(_BSR.DonePer), out donePer);
                _BSR.DonePerString = Math.Round(donePer) + "%";
            }
            catch (Exception)
            {
            }
            return _BSR;
        }

        public static List<BatchStatmentRecords> GetBatchStatment_old(Guid BatchId, out List<ComListObject> TotalListAmountData)
        {

            List<BatchStatmentRecords> _BatchStatmentRecords = null;
            TotalListAmountData = new List<ComListObject>();
            try
            {
                _BatchStatmentRecords = new List<BatchStatmentRecords>();
                //List<Statement> _TempStatement = Statement.GetStatementList(BatchId);

                //Added instance method
                Statement objStatement = new Statement();
                List<Statement> _TempStatement = objStatement.GetStatementList(BatchId);

                Batch objBatch = new Batch();
                //Batch batch = Batch.GetBatchViaBatchId(BatchId);
                Batch batch = objBatch.GetBatchViaBatchId(BatchId);

                Guid HouseOwner = new Guid();
                if (batch != null)
                    HouseOwner = PostUtill.GetPolicyHouseOwner(batch.LicenseeId);

                using (DLinq.CommissionDepartmentEntities DataModel = new CommissionDepartmentEntities())
                {
                    List<DLinq.Statement> Statements = (from statementRecords in DataModel.Statements
                                                        where statementRecords.BatchId == BatchId
                                                        select statementRecords).ToList();

                    Statements.ToList().ForEach(statement => _BatchStatmentRecords.Add(SetStatementValues(statement, HouseOwner)));
                    ComListObject TotalListData = new ComListObject();
                    var checkAmnt = _BatchStatmentRecords.Sum(item => item.CheckAmount);
                    decimal checkDecimal = 0;
                    decimal.TryParse(Convert.ToString(checkAmnt), out checkDecimal);
                    TotalListData.CheckAmountString = checkDecimal.ToString("C"); // "$" + Math.Round((decimal)checkAmnt,2);

                    TotalListData.Entries = _BatchStatmentRecords.Sum(item => item.Entries);

                    var House = _BatchStatmentRecords.Sum(item => item.House);
                    decimal houseAmt = 0;
                    decimal.TryParse(Convert.ToString(House), out houseAmt);
                    TotalListData.HouseString = houseAmt.ToString("C");  //"$" + Math.Round((decimal)House, 2); ;

                    var Remaining = _BatchStatmentRecords.Sum(item => item.Remaining);
                    decimal remainAmt = 0;
                    decimal.TryParse(Convert.ToString(Remaining), out remainAmt);
                    TotalListData.RemainingString = remainAmt.ToString("C");  //"$" + Math.Round((decimal)Remaining, 2); ;

                    var DonePer = _BatchStatmentRecords.Sum(item => item.DonePer);
                    decimal doneAmt = 0;
                    decimal.TryParse(Convert.ToString(DonePer), out doneAmt);
                    TotalListData.DonePerString = doneAmt.ToString("P2");  //Math.Round((decimal)DonePer, 2) +"%" ;
                    TotalListData.PayorNickName = "Total";
                    TotalListAmountData.Add(TotalListData);
                }
            }
            catch (Exception ex)
            {
                ActionLogger.Logger.WriteLog("Exception : " + ex.Message, true);
            }
            return _BatchStatmentRecords;
        }

        public static List<BatchStatmentRecords> GetBatchStatment(Guid BatchId, out List<ComListObject> TotalListAmountData)
        {
            List<BatchStatmentRecords> _BatchStatmentRecords = null;
            TotalListAmountData = new List<ComListObject>();
            try
            {
                _BatchStatmentRecords = new List<BatchStatmentRecords>();
                decimal checkDecimal = 0;
                decimal housetotal = 0;
                decimal remaintotal = 0;
                decimal netCheckAmt = 0;
                using (SqlConnection con = new SqlConnection(DBConnection.GetConnectionString()))
                {
                    using (SqlCommand cmd = new SqlCommand("usp_getStatementOnBatchID", con))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@batchID", BatchId);
                        con.Open();
                        SqlDataReader dr = cmd.ExecuteReader();
                        while (dr.Read())
                        {
                            BatchStatmentRecords obj = new BatchStatmentRecords();
                            obj.PayorId = (Guid)dr["PayorID"];
                            obj.PayorName = Convert.ToString(dr["Payor"]);
                            obj.PayorNickName = Convert.ToString(dr["NickName"]);
                            obj.StatmentId = (Guid)dr["StatementId"];
                            obj.StatmentNumber = dr.IsDBNull("StatementNumber") ? 0 : (int)dr["StatementNumber"];
                            obj.StmtStatusString = Convert.ToString(dr["Status"]);
                            obj.Entries = dr.IsDBNull("Entries") ? 0 : (int)dr["Entries"];
                            
                            obj.CheckAmount = dr.IsDBNull("CheckAmount") ? 0 : Convert.ToDecimal(dr["CheckAmount"]);
                            checkDecimal += Convert.ToDecimal(obj.CheckAmount);
                            obj.CheckAmountString = Math.Round(Convert.ToDecimal(obj.CheckAmount),2).ToString("C");

                            obj.StatementDate = dr.IsDBNull("StatementDate") ? (DateTime?)null: (DateTime)dr["StatementDate"];

                            decimal houseAmt = 0;
                            decimal remainAmt = 0;
                            obj.House = dr.IsDBNull("house") ? 0 : Convert.ToDecimal(dr["house"]);
                            housetotal += Convert.ToDecimal(obj.House);

                            decimal netCheck =  dr.IsDBNull("NetCheck") ? 0 : Convert.ToDecimal(dr["NetCheck"]);
                            netCheckAmt += Convert.ToDecimal(netCheck);

                            obj.Remaining = dr.IsDBNull("Remaining") ? 0 : Convert.ToDecimal(dr["Remaining"]);
                            remaintotal += Convert.ToDecimal(obj.Remaining);

                            obj.DonePer = 0;
                            if (netCheck > 0)
                            {
                                obj.DonePer = Convert.ToDouble((netCheck - obj.Remaining) / netCheck);
                                if (obj.DonePer > 0)
                                {
                                    obj.DonePer = obj.DonePer * 100;
                                }
                            }

                            decimal.TryParse(Convert.ToString(obj.House), out houseAmt);
                            obj.HouseString = houseAmt.ToString("C");


                            //_BSR.HouseString =  "$" + Math.Round((decimal)_BSR.House, 2);
                            decimal.TryParse(Convert.ToString(obj.Remaining), out remainAmt);
                            obj.RemainingString = remainAmt.ToString("C"); //"$" + Math.Round((decimal)_BSR.Remaining, 2);

                            decimal donePer = 0;
                            decimal.TryParse(Convert.ToString(obj.DonePer), out donePer);
                            obj.DonePerString = Math.Round(donePer) + "%";

                            _BatchStatmentRecords.Add(obj);
                        }
                    }
                }
                ComListObject TotalListData = new ComListObject();
                TotalListData.Entries = _BatchStatmentRecords.Sum(item => item.Entries);
                TotalListData.CheckAmountString = checkDecimal.ToString("C");
                TotalListData.RemainingString = remaintotal.ToString("C");
                TotalListData.HouseString = housetotal.ToString("C");
                if (netCheckAmt != 0)
                {
                    TotalListData.DonePerString = Math.Round((((netCheckAmt - remaintotal) / netCheckAmt)) * 100).ToString() + "%";
                }
                else
                {
                    TotalListData.DonePerString = "0%";
                }
                TotalListData.PayorNickName = "Total";
                TotalListAmountData.Add(TotalListData);
                ActionLogger.Logger.WriteLog(" GetCurrentBatch success: count - " + _BatchStatmentRecords.Count, true);
            }
            catch (Exception ex)
            {

                
            }
            return _BatchStatmentRecords;
        }
        public static List<BatchStatmentRecords> GetBatchStatment_last(Guid BatchId, out List<ComListObject> TotalListAmountData)
        {

            List<BatchStatmentRecords> _BatchStatmentRecords = null;
            TotalListAmountData = new List<ComListObject>();
            try
            {
                _BatchStatmentRecords = new List<BatchStatmentRecords>();
                //List<Statement> _TempStatement = Statement.GetStatementList(BatchId);

                //Added instance method
                Statement objStatement = new Statement();
                //List<Statement> _TempStatement = objStatement.GetStatementList(BatchId);

                Batch objBatch = new Batch();
                //Batch batch = Batch.GetBatchViaBatchId(BatchId);
                Guid licenseeID = objBatch.GetBatchLicensee(BatchId);

                Guid HouseOwner = PostUtill.GetPolicyHouseOwner(licenseeID);
                //if (licenseeID != null)
                //    HouseOwner = PostUtill.GetPolicyHouseOwner(batch.LicenseeId);

                using (DLinq.CommissionDepartmentEntities DataModel = new CommissionDepartmentEntities())
                {
                    List<DLinq.Statement> Statements = (from statementRecords in DataModel.Statements
                                                        where statementRecords.BatchId == BatchId
                                                        select statementRecords).ToList();

                    ComListObject TotalListData = new ComListObject();
                    decimal checkDecimal = 0;
                    decimal houseAmt = 0;
                    decimal remainAmt = 0;
                    decimal netCheckAmt = 0;

                    foreach (DLinq.Statement stmt in Statements)
                    {
                        BatchStatmentRecords record = SetStatementValues(stmt, HouseOwner);
                        _BatchStatmentRecords.Add(record);
                        if (record.House != null)
                        {
                            houseAmt += record.House.Value;
                        }
                        if (record.Remaining != null)
                        {
                            remainAmt += record.Remaining.Value;
                        }
                        if (record.CheckAmount != null)
                        {
                            checkDecimal += record.CheckAmount.Value;
                            if (record.BalAdj != null)
                            {
                                netCheckAmt += Convert.ToDecimal(netCheckAmt - record.BalAdj);
                            }
                            else
                            {
                                netCheckAmt += Convert.ToDecimal(record.CheckAmount);
                            }
                        }
                    }

                    TotalListData.Entries = _BatchStatmentRecords.Sum(item => item.Entries);
                    TotalListData.CheckAmountString = checkDecimal.ToString("C");
                    TotalListData.RemainingString = remainAmt.ToString("C");
                    TotalListData.HouseString = houseAmt.ToString("C");
                    if (netCheckAmt != 0)
                    {
                        TotalListData.DonePerString = Math.Round((((netCheckAmt - remainAmt) / netCheckAmt)) * 100).ToString() + "%";
                    }
                    else
                    {
                        TotalListData.DonePerString = "0%";
                    }
                    TotalListData.PayorNickName = "Total";
                    TotalListAmountData.Add(TotalListData);

                    /*  Statements.ToList().ForEach(statement => _BatchStatmentRecords.Add(SetStatementValues(statement, HouseOwner)));

                      ComListObject TotalListData = new ComListObject();
                      var checkAmnt = _BatchStatmentRecords.Sum(item => item.CheckAmount);
                      decimal checkDecimal = 0;
                      decimal.TryParse(Convert.ToString(checkAmnt), out checkDecimal);
                      TotalListData.CheckAmountString = checkDecimal.ToString("C"); // "$" + Math.Round((decimal)checkAmnt,2);

                      TotalListData.Entries = _BatchStatmentRecords.Sum(item => item.Entries);

                      var House = _BatchStatmentRecords.Sum(item => item.House);
                      decimal houseAmt = 0;
                      decimal.TryParse(Convert.ToString(House), out houseAmt);
                      TotalListData.HouseString = houseAmt.ToString("C");  //"$" + Math.Round((decimal)House, 2); ;

                      var Remaining = _BatchStatmentRecords.Sum(item => item.Remaining);
                      decimal remainAmt = 0;
                      decimal.TryParse(Convert.ToString(Remaining), out remainAmt);
                      TotalListData.RemainingString = remainAmt.ToString("C");  //"$" + Math.Round((decimal)Remaining, 2); ;

                      var DonePer = _BatchStatmentRecords.Sum(item => item.DonePer);
                      decimal doneAmt = 0;
                      decimal.TryParse(Convert.ToString(DonePer), out doneAmt);
                      TotalListData.DonePerString = doneAmt.ToString("P2");  //Math.Round((decimal)DonePer, 2) +"%" ;
                      TotalListData.RadioButton = "Total";
                      TotalListAmountData.Add(TotalListData);*/
                }
            }
            catch (Exception ex)
            {
                ActionLogger.Logger.WriteLog("Exception : " + ex.Message, true);
            }
            return _BatchStatmentRecords.OrderBy(item => item.PayorNickName).ToList();
        }

        //public static List<BatchStatmentRecords> GetBatchStatmentWithoutCalculation(Guid BatchId)
        //{
        //    List<BatchStatmentRecords> _BatchStatmentRecords = null;
        //    _BatchStatmentRecords = new List<BatchStatmentRecords>();

        //    try
        //    {

        //        //List<Statement> _TempStatement = Statement.GetStatementList(BatchId);
        //        //Added instance 
        //        Statement objStatement = new Statement();
        //        List<Statement> _TempStatement = objStatement.GetStatementList(BatchId);
        //        //Guid HouseOwner = PostUtill.GetPolicyHouseOwner(Batch.GetBatchViaBatchId(BatchId).LicenseeId);

        //        foreach (Statement _smt in _TempStatement)
        //        {

        //            BatchStatmentRecords _BSR = new BatchStatmentRecords();
        //            _BSR.PayorId = _smt.PayorId ?? Guid.Empty;
        //            Payor payor = Payor.GetPayorByID(_BSR.PayorId);

        //            if (payor != null)
        //            {
        //                _BSR.PayorNickName = payor.NickName;
        //                _BSR.PayorName = payor.PayorName;
        //            }
        //            else
        //            {
        //                _BSR.PayorNickName = string.Empty;
        //                _BSR.PayorName = string.Empty;
        //            }

        //            _BSR.StatmentId = _smt.StatementID;
        //            _BSR.StatmentNumber = _smt.StatementNumber;

        //            //_BSR.CheckAmount = _smt.EnteredAmount;
        //            _BSR.CheckAmount = _smt.CheckAmount;
        //            _BSR.Entries = _smt.Entries;
        //            _BSR.StmtStatus = _smt.StatusId;
        //            _BSR.BalAdj = _smt.BalanceForOrAdjustment;
        //            _BatchStatmentRecords.Add(_BSR);
        //        }
        //    }
        //    catch (Exception)
        //    {
        //    }
        //    return _BatchStatmentRecords;
        //}

        //public static decimal? GetDistributedMoneyinStmt(Guid StmtId)
        //{
        //    double DistributedAmt = 0;
        //    List<PolicyPaymentEntriesPost> _PolicyPaymentEntriesPost = PolicyPaymentEntriesPost.GetPolicyPaymentEntryStatementWise(StmtId);
        //    if (_PolicyPaymentEntriesPost != null)
        //    {
        //        foreach (PolicyPaymentEntriesPost ppep in _PolicyPaymentEntriesPost)
        //        {
        //            List<PolicyOutgoingDistribution> _PolicyOutgoingDistribution =
        //                PolicyOutgoingDistribution.GetOutgoingPaymentByPoicyPaymentEntryId(ppep.PaymentEntryID);
        //            if (_PolicyOutgoingDistribution != null)
        //            {
        //                foreach (PolicyOutgoingDistribution _PolOutDis in _PolicyOutgoingDistribution)
        //                {
        //                    DistributedAmt += _PolOutDis.PaidAmount ?? 0;
        //                }
        //            }
        //        }
        //    }
        //    return (decimal)DistributedAmt;

        //}

        //public static decimal? GetHouseOwnerDistributedMoneyStmtWise(Guid StmtId, Guid BatchId)
        //{
        //    double? HouseAmt = 0;
        //    List<PolicyPaymentEntriesPost> _PolicyPaymentEntriesPost = PolicyPaymentEntriesPost.GetPolicyPaymentEntryStatementWise(StmtId);
        //    if (_PolicyPaymentEntriesPost != null)
        //    {
        //        //Guid HouseOwner = PostUtill.GetPolicyHouseOwner(Batch.GetBatchViaBatchId(BatchId).LicenseeId);
        //        Batch objBatch = new Batch();
        //        Guid HouseOwner = PostUtill.GetPolicyHouseOwner(objBatch.GetBatchViaBatchId(BatchId).LicenseeId);

        //        foreach (PolicyPaymentEntriesPost ppep in _PolicyPaymentEntriesPost)
        //        {
        //            List<PolicyOutgoingDistribution> _PolicyOutgoingDistribution =
        //                PolicyOutgoingDistribution.GetOutgoingPaymentByPoicyPaymentEntryId(ppep.PaymentEntryID);
        //            if (_PolicyOutgoingDistribution != null)
        //            {
        //                PolicyOutgoingDistribution _PolicyOutgoingDis =
        //                    _PolicyOutgoingDistribution.Where(p => p.RecipientUserCredentialId == HouseOwner).FirstOrDefault();
        //                if (_PolicyOutgoingDis != null)
        //                {
        //                    HouseAmt += _PolicyOutgoingDis.PaidAmount;
        //                }

        //            }


        //        }
        //    }
        //    if (HouseAmt == null)
        //        HouseAmt = 0;

        //    return (decimal)HouseAmt;
        //}

        //public static decimal? GetHouseOwnerDistributedMoneyStmtWise(Guid StmtId, Guid BatchId, Guid HouseOwner)
        //{
        //    double? HouseAmt = 0;
        //    List<PolicyPaymentEntriesPost> _PolicyPaymentEntriesPost = PolicyPaymentEntriesPost.GetPolicyPaymentEntryStatementWise(StmtId);
        //    if (_PolicyPaymentEntriesPost != null)
        //    {
        //        foreach (PolicyPaymentEntriesPost ppep in _PolicyPaymentEntriesPost)
        //        {
        //            PolicyOutgoingDistribution _PolicyOutgoingDistribution = PolicyOutgoingDistribution.GetOutgoingPaymentByPoicyPaymentEntryId(ppep.PaymentEntryID, HouseOwner).FirstOrDefault();
        //            if (_PolicyOutgoingDistribution != null)
        //            {
        //                HouseAmt += _PolicyOutgoingDistribution.PaidAmount;
        //            }
        //        }
        //    }
        //    if (HouseAmt == null)
        //        HouseAmt = 0;

        //    return (decimal)HouseAmt;
        //}

        //public void AddUpdateBatchStatmentRecord(BatchStatmentRecords _BatchStatmentRecord)
        //{
        //}
    }

}
