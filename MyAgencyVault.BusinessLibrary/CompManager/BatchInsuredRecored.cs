using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyAgencyVault.BusinessLibrary.Base;
using System.Runtime.Serialization;
using DLinq = DataAccessLayer.LinqtoEntity;
using DataAccessLayer.LinqtoEntity;
using MyAgencyVault.BusinessLibrary.Masters;
using System.Data.SqlClient;
using System.Data;

namespace MyAgencyVault.BusinessLibrary
{
    [DataContract]
    public class InsuredPayment
    {
        [DataMember]
        public string ClientName { get; set; }
        [DataMember]
        public decimal? PaymentRecived { get; set; }
        [DataMember]
        public string PaymentString { get; set; }
    }

    public class BatchInsuredRecored
    {
        [DataMember]
        public Guid PaymentEntryId { get; set; }
        [DataMember]
        public Guid PolicyId { get; set; }
        [DataMember]
        public Guid ClientId { get; set; }
        [DataMember]
        public string ClientName { get; set; }
        [DataMember]
        public string Insured { get; set; }
        [DataMember]
        public decimal? PaymentRecived { get; set; }

        public static List<BatchInsuredRecored> GetBatchInsuredRecored(Guid stmtId)
        {
            List<BatchInsuredRecored> _BatchInsuredRecored = new List<BatchInsuredRecored>();
            List<PolicyPaymentEntriesPost> _PolicyPaymentEntriesPost = PolicyPaymentEntriesPost.GetPolicyPaymentEntryStatementWise(stmtId);
            foreach (PolicyPaymentEntriesPost ppep in _PolicyPaymentEntriesPost)
            {
                PolicyDetailsData _Policy = PostUtill.GetPolicy(ppep.PolicyID);
                BatchInsuredRecored BIR = new BatchInsuredRecored();
                BIR.PaymentEntryId = ppep.PaymentEntryID;
                BIR.PolicyId = ppep.PolicyID;
                BIR.ClientId = _Policy.ClientId ?? Guid.Empty;
                Client clt = Client.GetClient(BIR.ClientId);
                BIR.ClientName = (clt == null ? "" : clt.Name);
                BIR.Insured = (clt == null ? "" : clt.InsuredName);
                BIR.PaymentRecived = ppep.TotalPayment;
                _BatchInsuredRecored.Add(BIR);
            }
            return _BatchInsuredRecored;
        }

        /// <summary>
        /// Modified By:Ankit Kahndelwal
        /// Modified on:18-06-2019
        /// </summary>
        /// <param name="statementId"></param>
        /// <returns></returns>
        public static List<InsuredPayment> GetInsuredPayments_old(Guid statementId)
        {
            ActionLogger.Logger.WriteLog("GetInsuredPayments process starts with stmtId: " + statementId, true);
            List<InsuredPayment> _BatchInsuredRecored = new List<InsuredPayment>();
            try
            {
                List<PolicyPaymentEntriesPost> _PolicyPaymentEntriesPost = PolicyPaymentEntriesPost.GetPolicyPaymentEntryStatementWise(statementId);
                foreach (PolicyPaymentEntriesPost ppep in _PolicyPaymentEntriesPost)
                {
                    PolicyDetailsData _Policy = PostUtill.GetPolicy(ppep.PolicyID);
                    if (_Policy == null)
                    {
                        return _BatchInsuredRecored;
                    }
                    InsuredPayment BIR = new InsuredPayment();

                    Client clt = Client.GetClient(_Policy.ClientId ?? Guid.Empty);
                    BIR.ClientName = (clt == null ? "" : clt.Name);
                    BIR.PaymentRecived = ppep.TotalPayment;
                    BIR.PaymentString = ppep.TotalPayment.ToString("C");
                    _BatchInsuredRecored.Add(BIR);
                }

            }
            catch (Exception ex)
            {
                ActionLogger.Logger.WriteLog("GetInsuredPayments:Exception occurs while processing-StatementId" + statementId + "Exception:" + ex.Message, true);
                ActionLogger.Logger.WriteLog("GetInsuredPayments:Exception occurs while processing-StatementId" + statementId + "Inner Exception:" + ex.InnerException, true);
            }
            var groupQuery = from income in _BatchInsuredRecored
                             group income by income.ClientName into result
                             select new InsuredPayment
                             {
                                 ClientName = result.Key,
                                 PaymentString = "$" + Convert.ToDecimal(result.Sum(i => i.PaymentRecived)).ToString("0.00")
                             };

            return groupQuery.OrderBy(data => data.ClientName).ToList();
        }

        public static List<InsuredPayment> GetInsuredPayments(Guid statementId)
        {
            ActionLogger.Logger.WriteLog("GetInsuredPayments process starts with stmtId: " + statementId, true);
            List<InsuredPayment> listPayments = new List<InsuredPayment>();
            try
            {
                using (SqlConnection con = new SqlConnection(DBConnection.GetConnectionString()))
                {
                    using (SqlCommand cmd = new SqlCommand("usp_GetPaymentsByInsured", con))
                    {
                        con.Open();
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandTimeout = 0;
                        cmd.Parameters.AddWithValue("@statementID", statementId);
                        SqlDataReader dr = cmd.ExecuteReader();
                        while (dr.Read())
                        {
                            InsuredPayment obj = new InsuredPayment();
                            try
                            {
                                obj.ClientName = Convert.ToString(dr["client"]);
                                obj.PaymentRecived = dr.IsDBNull("amount") ? 0 : Convert.ToDecimal(dr["amount"]);
                                obj.PaymentString = Convert.ToDecimal(obj.PaymentRecived).ToString("C");
                                listPayments.Add(obj);
                            }
                            catch(Exception ex)
                            {
                                ActionLogger.Logger.WriteLog("GetInsuredPayments:Exception adding reocrd for client" + obj.ClientName + "Exception:" + ex.Message, true);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ActionLogger.Logger.WriteLog("GetInsuredPayments:Exception occurs while processing-StatementId" + statementId + "Exception:" + ex.Message, true);
                if(ex.InnerException!= null)
                {
                    ActionLogger.Logger.WriteLog("GetInsuredPayments:Exception occurs while processing-StatementId" + statementId + "Inner Exception:" + ex.InnerException, true);
                }
                throw ex;
            }
            return listPayments;
        }


        public static List<InsuredPayment> GetInsuredName(Guid stmtId)
        {
            List<InsuredPayment> _BatchInsuredRecored = new List<InsuredPayment>();
            List<PolicyPaymentEntriesPost> _PolicyPaymentEntriesPost = PolicyPaymentEntriesPost.GetPolicyPaymentEntryStatementWise(stmtId);
            foreach (PolicyPaymentEntriesPost ppep in _PolicyPaymentEntriesPost)
            {
                PolicyDetailsData _Policy = PostUtill.GetPolicy(ppep.PolicyID);
                InsuredPayment BIR = new InsuredPayment();
                Client clt = Client.GetClient(_Policy.ClientId ?? Guid.Empty);
                BIR.ClientName = (clt == null ? "" : clt.Name);
                _BatchInsuredRecored.Add(BIR);
            }

            var groupQuery = from income in _BatchInsuredRecored
                             group income by income.ClientName into result
                             select new InsuredPayment
                             {
                                 ClientName = result.Key,

                             };

            return groupQuery.ToList();
        }
    }
}
