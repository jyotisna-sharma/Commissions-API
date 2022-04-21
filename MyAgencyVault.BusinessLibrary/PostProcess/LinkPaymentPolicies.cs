////////////////////////////////////////////////////////////////////////////////////////
//   Description : It is a temp collection for LinkPayment Pending Policy
//   
//
//

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyAgencyVault.BusinessLibrary.Base;
using System.Runtime.Serialization;
using System.Transactions;
using DLinq = DataAccessLayer.LinqtoEntity;
using System.Linq.Expressions;
using System.Threading;
using System.Data.SqlClient;
using System.Data;

namespace MyAgencyVault.BusinessLibrary
{
    [DataContract]
    public class LinkPaymentPolicies
    {
        [DataMember]
        public Guid PolicyId { get; set; }

        [DataMember]
        public Guid ClientId { get; set; }
        [DataMember]
        public string ClientName { get; set; }
        [DataMember]
        public string Insured { get; set; }
        [DataMember]
        public Guid PayorId { get; set; }
        [DataMember]
        public string PayorName { get; set; }
        [DataMember]
        public Guid CarrierId { get; set; }
        [DataMember]
        public string CarrierName { get; set; }
        [DataMember]
        public Guid ProductId { get; set; }
        [DataMember]
        public string ProductName { get; set; }
        [DataMember]
        public int? CompTypeId { get; set; }
        [DataMember]
        public string CompTypeName { get; set; }
        [DataMember]
        public int? CompScheduleTypeId { get; set; }
        [DataMember]
        public string CompScheduleTypeName { get; set; }
        [DataMember]
        public string ProductType { get; set; } //Acme Added: Dec 06, 2016

        //[DataMember]
        //public double FirstYear { get; set; }
        //[DataMember]
        //public double Renewal { get; set; }
        [DataMember]
        public List<LinkPaymentReciptRecords> Entries { get; set; }
        [DataMember]
        public LinkPaymentReciptRecords SelectedEntries { get; set; }
        [DataMember]
        public string PolicyNumber { get; set; }
        [DataMember]
        public int? StatusId { get; set; }
        [DataMember]
        public string StatusName { get; set; }

        //[DataMember]
        //public DateTime? OriginalEffDate { get; set; }
        DateTime? _OriginalEffDate;
        [DataMember]
        public DateTime? OriginalEffDate
        {
            get
            {
                return _OriginalEffDate;
            }
            set
            {
                _OriginalEffDate = value;
                if (value != null && string.IsNullOrEmpty(OriginDateString))
                {
                    OriginDateString = Convert.ToDateTime(value).ToString("MMM dd, yyyy");
                }
            }
        }
        string _OriginDateString;
        [DataMember]
        public string OriginDateString
        {
            get
            {
                return _OriginDateString;
            }
            set
            {
                _OriginDateString = value;
                if (OriginalEffDate == null && !string.IsNullOrEmpty(_OriginDateString))
                {
                    DateTime dt;
                    DateTime.TryParse(_OriginDateString, out dt);
                    OriginalEffDate = dt;
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

        [DataMember]
        public Guid LicenseId { get; set; }
        public void AddUpdate(LinkPaymentPolicies _LinkPaymentPolicies)
        {
        }

        //This is called for upper Grid
        //public static List<LinkPaymentPolicies> GetPendingPoliciesForLinkedPolicy(Guid LicencessId)
        //{
        //    List<LinkPaymentPolicies> _LinkedPaymentPendingPolicies = new List<LinkPaymentPolicies>();
        //    Dictionary<string, object> parameters = new Dictionary<string, object>();
        //    parameters.Add("PolicyLicenseeId", LicencessId);
        //    parameters.Add("IsDeleted", false);
        //    parameters.Add("PolicyStatusId", (int)_PolicyStatus.Pending);
        //    Expression<Func<DLinq.Policy, bool>> expressionParameters = p => p.PolicyPaymentEntries.Count > 0;
        //    List<PolicyDetailsData> _Policies = Policy.GetPolicyData(parameters,expressionParameters);

        //    foreach (PolicyDetailsData _Policy in _Policies)
        //    {
        //        try
        //        {
        //            //List<LinkPaymentReciptRecords> _LinkPaymentReciptRecords = LinkPaymentReciptRecords.GetLinkPaymentReciptRecordsByPolicyId(_Policy.PolicyId);
        //            //if (_LinkPaymentReciptRecords != null && _LinkPaymentReciptRecords.Count > 0)
        //            //{
        //                LinkPaymentPolicies _LinkPaymentPolicies = new LinkPaymentPolicies();
        //                _LinkPaymentPolicies.PolicyId = _Policy.PolicyId;
        //                _LinkPaymentPolicies.PolicyNumber = _Policy.PolicyNumber;
        //                _LinkPaymentPolicies.ClientId = _Policy.ClientId.Value;
        //                _LinkPaymentPolicies.ClientName = _Policy.ClientName;
        //                _LinkPaymentPolicies.Insured = _Policy.Insured ?? "";
        //                _LinkPaymentPolicies.CarrierId = _Policy.CarrierID ?? Guid.Empty;
        //                _LinkPaymentPolicies.CarrierName = _Policy.CarrierName ?? "";
        //                _LinkPaymentPolicies.ProductId = _Policy.CoverageId ?? Guid.Empty;
        //                _LinkPaymentPolicies.PayorId = _Policy.PayorId ?? Guid.Empty;
        //                _LinkPaymentPolicies.PayorName = _Policy.PayorName;
        //                _LinkPaymentPolicies.ProductName = _Policy.CoverageName ?? "";
        //                _LinkPaymentPolicies.CompTypeId = _Policy.IncomingPaymentTypeId ?? 0;
        //                _LinkPaymentPolicies.OriginalEffDate = _Policy.OriginalEffectiveDate;
        //                _LinkPaymentPolicies.LicenseId = _Policy.PolicyLicenseeId ?? Guid.Empty;
        //                _LinkPaymentPolicies.CreatedOn = _Policy.CreatedOn;

        //                _LinkedPaymentPendingPolicies.Add(_LinkPaymentPolicies);
        //              //  _LinkedPaymentPendingPolicies.Where(p => p.PolicyId == _Policy.PolicyId).FirstOrDefault().Entries = _LinkPaymentReciptRecords;
        //                Payor _Payor = Payor.GetPayorByID(_Policy.PayorId ?? Guid.Empty);
        //                _LinkedPaymentPendingPolicies.Where(p => p.PolicyId == _Policy.PolicyId).FirstOrDefault().PayorId = _Payor.PayorID;
        //                _LinkedPaymentPendingPolicies.Where(p => p.PolicyId == _Policy.PolicyId).FirstOrDefault().PayorName = _Payor.PayorName;

        //            //}
        //        }
        //        catch (Exception ex)
        //        {
        //        }
        //    }

        //    return _LinkedPaymentPendingPolicies.OrderBy(p => p.CreatedOn).ToList();
        //}

        public static List<LinkPaymentPolicies> GetPendingPoliciesForLinkedPolicy(Guid LicencessId)
        {
            List<LinkPaymentPolicies> _LinkedPaymentPendingPolicies = new List<LinkPaymentPolicies>();
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("PolicyLicenseeId", LicencessId);
            parameters.Add("IsDeleted", false);
            parameters.Add("PolicyStatusId", (int)_PolicyStatus.Pending);
            Expression<Func<DLinq.Policy, bool>> expressionParameters = p => p.PolicyPaymentEntries.Count > 0;
            List<PolicyDetailsData> _Policies = Policy.GetPolicyData(parameters, expressionParameters);
            _Policies = new List<PolicyDetailsData>(_Policies.Where(p => p.ClientId != Guid.Empty).ToList());

            foreach (PolicyDetailsData _Policy in _Policies)
            {
                try
                {
                    LinkPaymentPolicies _LinkPaymentPolicies = new LinkPaymentPolicies();
                    _LinkPaymentPolicies.PolicyId = _Policy.PolicyId;
                    _LinkPaymentPolicies.PolicyNumber = _Policy.PolicyNumber;
                    _LinkPaymentPolicies.ClientId = _Policy.ClientId.Value;
                    _LinkPaymentPolicies.ClientName = _Policy.ClientName;
                    _LinkPaymentPolicies.Insured = _Policy.Insured ?? "";
                    _LinkPaymentPolicies.CarrierId = _Policy.CarrierID ?? Guid.Empty;
                    _LinkPaymentPolicies.CarrierName = _Policy.CarrierName ?? "";
                    _LinkPaymentPolicies.ProductId = _Policy.CoverageId ?? Guid.Empty;
                    _LinkPaymentPolicies.PayorId = _Policy.PayorId ?? Guid.Empty;
                    _LinkPaymentPolicies.PayorName = _Policy.PayorName;
                    _LinkPaymentPolicies.ProductName = _Policy.CoverageName ?? "";
                    _LinkPaymentPolicies.CompTypeId = _Policy.IncomingPaymentTypeId ?? 0;
                    _LinkPaymentPolicies.OriginalEffDate = _Policy.OriginalEffectiveDate;
                    _LinkPaymentPolicies.LicenseId = _Policy.PolicyLicenseeId ?? Guid.Empty;
                    _LinkPaymentPolicies.CreatedOn = _Policy.CreatedOn;
                    _LinkPaymentPolicies.CompTypeName = CompType(_Policy.CompType);
                    _LinkPaymentPolicies.CompScheduleTypeName = _Policy.CompSchuduleType;
                    _LinkPaymentPolicies.ProductType = _Policy.ProductType;

                    _LinkedPaymentPendingPolicies.Add(_LinkPaymentPolicies);

                    Payor _Payor = Payor.GetPayorByID(_Policy.PayorId ?? Guid.Empty);
                    _LinkedPaymentPendingPolicies.Where(p => p.PolicyId == _Policy.PolicyId).FirstOrDefault().PayorId = _Payor.PayorID;
                    _LinkedPaymentPendingPolicies.Where(p => p.PolicyId == _Policy.PolicyId).FirstOrDefault().PayorName = _Payor.PayorName;

                }
                catch (Exception)
                {
                }
            }

            return _LinkedPaymentPendingPolicies.OrderBy(p => p.CreatedOn).ToList();

        }
        public static List<LinkPaymentPolicies> GetPendingPolicies(Guid licenseeId, ListParams listParams, out int recordCount)
        {
            List<LinkPaymentPolicies> policiesList = new List<LinkPaymentPolicies>();

            using (SqlConnection con = new SqlConnection(DBConnection.GetConnectionString()))
            {
                try
                {
                    int rowStart = (listParams.pageSize == 0 && listParams.pageIndex == 0) ? 0 : (listParams.pageSize * (listParams.pageIndex)) + 1;
                    int rowEnd = (listParams.pageSize == 0 && listParams.pageIndex == 0) ? 0 : (listParams.pageIndex + 1) * listParams.pageSize;
                    using (SqlCommand cmd = new SqlCommand("usp_PendingPoliciesList", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@licenseeId", licenseeId);
                        cmd.Parameters.AddWithValue("@RowStart", rowStart);
                        cmd.Parameters.AddWithValue("@RowEnd", rowEnd);
                        cmd.Parameters.AddWithValue("@sortBy", listParams.sortBy);
                        cmd.Parameters.AddWithValue("@sortType", listParams.sortType);
                        cmd.Parameters.AddWithValue("@filterBy", listParams.filterBy);
                        cmd.Parameters.Add("@recordCount", SqlDbType.Int);
                        cmd.Parameters["@recordCount"].Direction = ParameterDirection.Output;
                        con.Open();
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            LinkPaymentPolicies _LinkPaymentPolicies = new LinkPaymentPolicies();
                            _LinkPaymentPolicies.PolicyId = (Guid)reader["PolicyId"];
                            _LinkPaymentPolicies.PolicyNumber = Convert.ToString(reader["PolicyNumber"]);
                            _LinkPaymentPolicies.ClientName = Convert.ToString(reader["ClientName"]);
                            _LinkPaymentPolicies.Insured = Convert.ToString(reader["Insured"]);
                            _LinkPaymentPolicies.CarrierName = Convert.ToString(reader["CarrierName"]);
                            _LinkPaymentPolicies.PayorName = Convert.ToString(reader["PayorName"]); ;
                            _LinkPaymentPolicies.ProductName = Convert.ToString(reader["ProductName"]);
                            _LinkPaymentPolicies.CompTypeName = Convert.ToString(reader["CompScheduleTypeName"]);
                            _LinkPaymentPolicies.ProductType = Convert.ToString(reader["ProductType"]);
                            policiesList.Add(_LinkPaymentPolicies);

                        }
                        reader.Close();
                        recordCount = Convert.ToInt32(cmd.Parameters["@recordCount"].Value);
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return policiesList;
        }

        public static LinkPendingPolicy GetPendingPolicydetail(Guid PolicyID)
        {
            ActionLogger.Logger.WriteLog(DateTime.Now.ToString() + " GetPendingPolicydetail request: PolicyId " + PolicyID, true);
            LinkPendingPolicy policy = new LinkPendingPolicy();
            try
            {
                PolicyDetailsData data = Policy.GetPolicyDetailsOnPolicyID(PolicyID);
                policy.PolicyNumber = data.PolicyNumber;
                policy.Insured = data.Insured;
                policy.Client = data.ClientName;
                policy.ClientID = (Guid)data.ClientId;
                policy.CompType = CompType(data.CompType);
                policy.Payor = data.PayorName;
                policy.Carrier = data.CarrierName;
                policy.Product = data.CoverageName;
                policy.ProductType = data.ProductType;
                policy.PayorID = data.PayorId;
            }
            catch (Exception ex)
            {
                ActionLogger.Logger.WriteLog(DateTime.Now.ToString() + " GetPendingPolicydetail exception: PolicyId " + PolicyID + ", ex: " + ex.Message, true);
                throw ex;
            }
            return policy;

        }
        private static string CompType(int? intVAlue)
        {
            string strReturn = string.Empty;

            switch (intVAlue)
            {
                case 0:
                    strReturn = "Other";
                    break;
                case 1:
                    strReturn = "Commission";
                    break;
                case 2:
                    strReturn = "Override";
                    break;
                case 3:
                    strReturn = "Bonus";
                    break;
                case 4:
                    strReturn = "Fee";
                    break;

                case 5:
                    strReturn = "Pending";
                    break;

                default:
                    strReturn = "Pending";
                    break;

            }
            return strReturn;
        }


        public static List<LinkPaymentPolicies> GetAllPoliciesForLinking(Guid licenseeId, ListParams listParams, out string recordCount, string ClientID = "", string PayorID = "")
        {
            List<LinkPaymentPolicies> _LinkedPaymentAllPolicies = new List<LinkPaymentPolicies>();
            try
            {

                if (string.IsNullOrEmpty(ClientID))
                {
                    ClientID = "";
                }
                if (string.IsNullOrEmpty(PayorID))
                {
                    PayorID = "";
                }
                using (SqlConnection con = new SqlConnection(DBConnection.GetConnectionString()))
                {
                    int rowStart = (listParams.pageSize * (listParams.pageIndex)) + 1;
                    int rowEnd = (listParams.pageIndex + 1) * listParams.pageSize;
                    using (System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("usp_GetpoliciesforLinking", con))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.CommandTimeout = 0;
                        cmd.Parameters.AddWithValue("@LicenseeId", licenseeId);
                        cmd.Parameters.AddWithValue("@ClientID", ClientID);
                        cmd.Parameters.AddWithValue("@PayorID", PayorID);
                        cmd.Parameters.AddWithValue("@RowStart", rowStart);
                        cmd.Parameters.AddWithValue("@RowEnd", rowEnd);
                        cmd.Parameters.AddWithValue("@sortType", listParams.sortType);
                        cmd.Parameters.AddWithValue("@sortBy", listParams.sortBy);
                        cmd.Parameters.Add("@recordsCount", SqlDbType.VarChar, 20);
                        cmd.Parameters["@recordsCount"].Direction = ParameterDirection.Output;
                        con.Open();
                        System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            LinkPaymentPolicies lpp = new LinkPaymentPolicies();
                            lpp.PolicyId = reader["PolicyId"] == null ? Guid.Empty : (Guid)reader["PolicyId"];

                            lpp.ClientId = reader["policyClientId"] == null ? Guid.Empty : (Guid)reader["policyClientId"];
                            lpp.ClientName = Convert.ToString(reader["ClientName"]);
                            lpp.Insured = Convert.ToString(reader["Insured"]);
                            lpp.PayorId = reader.IsDBNull("PayorId") ? Guid.Empty : (Guid)reader["PayorId"];
                            lpp.PayorName = Convert.ToString(reader["PayorName"]);
                            lpp.CarrierId = reader.IsDBNull("CarrierID") ? Guid.Empty : (Guid)reader["CarrierID"];
                            lpp.CarrierName = Convert.ToString(reader["CarrierName"]);
                            lpp.ProductId = reader.IsDBNull("CoverageId") ? Guid.Empty : (Guid)reader["CoverageId"];
                            lpp.ProductName = Convert.ToString(reader["ProductName"]);

                            string compId = Convert.ToString(reader["CompTypeID"]);
                            int compType = 0;
                            Int32.TryParse(compId, out compType);
                            lpp.CompTypeId = compType;


                            lpp.PolicyNumber = Convert.ToString(reader["PolicyNumber"]);

                            string strStatusType = Convert.ToString(reader["PolicyStatusId"]);
                            int statusType = 0;
                            Int32.TryParse(strStatusType, out statusType);
                            lpp.StatusId = statusType;

                            lpp.StatusName = Convert.ToString(reader["StatusName"]);

                            lpp.OriginalEffDate = reader.IsDBNull("OriginalEffectiveDate") ? null : (DateTime?)reader["OriginalEffectiveDate"];
                            lpp.CompTypeName = Convert.ToString(reader["CompTypeName"]);
                            lpp.CompScheduleTypeName = Convert.ToString(reader["CompScheduleType"]);
                            lpp.LicenseId = reader.IsDBNull("PolicyLicenseeId") ? Guid.Empty : (Guid)reader["PolicyLicenseeId"];
                            lpp.ProductType = Convert.ToString(reader["ProductType"]);
                            _LinkedPaymentAllPolicies.Add(lpp);

                        }
                        reader.Close();
                        recordCount = Convert.ToString(cmd.Parameters["@recordsCount"].Value);
                    }
                }
            }
            catch (Exception ex)
            {
                ActionLogger.Logger.WriteLog("GetAllPoliciesForLinking:Exception occurs while fetching list:" + ex.Message, true);
                throw ex;
            }
            return _LinkedPaymentAllPolicies;

        }
        /// <summary>
        /// Modified By :Ankit Khandelwal
        /// Modified On:03-06-2019
        /// Purpose:to get the list of active policies for Linking in comp manager
        /// </summary>
        /// <param name="LicencessId"></param>
        /// <returns></returns>
        public static List<LinkPaymentPolicies> GetAllPoliciesForLinkedPolicy(Guid licencessId)
        {
            List<LinkPaymentPolicies> _LinkedPaymentAllPolicies = new List<LinkPaymentPolicies>();
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("PolicyLicenseeId", licencessId);
            parameters.Add("IsDeleted", false);

            List<PolicyDetailsData> _Policies = Policy.GetPolicyData(parameters);
            _Policies = new List<PolicyDetailsData>(_Policies.Where(p => p.ClientId != Guid.Empty).ToList());
            _Policies = _Policies.Where(p => p.PolicyStatusId != (int)_PolicyStatus.Pending).ToList<PolicyDetailsData>();

            for (int idx = 0; idx < _Policies.Count; idx++)
            {
                LinkPaymentPolicies lpp = new LinkPaymentPolicies();
                lpp.PolicyId = _Policies[idx].PolicyId;
                try
                {
                    if (_Policies[idx].ClientId != null)
                    {
                        lpp.ClientId = _Policies[idx].ClientId.Value;
                    }

                }
                catch
                {
                }
                lpp.ClientName = _Policies[idx].ClientName;
                lpp.Insured = _Policies[idx].Insured;
                lpp.PayorId = _Policies[idx].PayorId ?? Guid.Empty;
                lpp.PayorName = _Policies[idx].PayorName;
                lpp.CarrierId = _Policies[idx].CarrierID ?? Guid.Empty;
                lpp.CarrierName = _Policies[idx].CarrierName;
                lpp.ProductId = _Policies[idx].CoverageId.Value;
                lpp.ProductName = _Policies[idx].CoverageName;
                lpp.CompTypeId = _Policies[idx].IncomingPaymentTypeId;


                lpp.PolicyNumber = _Policies[idx].PolicyNumber;
                lpp.StatusId = _Policies[idx].PolicyStatusId;
                lpp.StatusName = _Policies[idx].PolicyStatusName;
                lpp.OriginalEffDate = _Policies[idx].OriginalEffectiveDate;
                lpp.CompTypeName = CompType(_Policies[idx].CompType);
                lpp.CompScheduleTypeName = _Policies[idx].CompSchuduleType;
                lpp.LicenseId = _Policies[idx].PolicyLicenseeId ?? Guid.Empty;
                lpp.ProductType = _Policies[idx].ProductType; //Acme Added dec 06, 2016
                _LinkedPaymentAllPolicies.Add(lpp);
            }
            return _LinkedPaymentAllPolicies;
        }

        public static void MakePolicyActive(Guid PolicyId, Guid ClientId)
        {
            try
            {
                ActionLogger.Logger.WriteLog(DateTime.Now.ToString() + " MakePolicyActive request: PolicyId " + PolicyId + ", Client: " + ClientId, true);
                PolicyDetailsData _Policy = PostUtill.GetPolicy(PolicyId);
                _Policy.PolicyStatusId = (int?)_PolicyStatus.Active;
                Guid? Cliid = null;
                if (ClientId != Guid.Empty)
                {
                    Cliid = _Policy.ClientId;
                    _Policy.ClientId = ClientId;

                }
                Policy.AddUpdatePolicy(_Policy);
                Policy.AddUpdatePolicyHistoryNotCheckPayment(_Policy.PolicyId);
                PolicyLearnedFieldData _PolicyLearnedField = PolicyLearnedField.GetPolicyLearnedFieldsPolicyWise(_Policy.PolicyId);
                _PolicyLearnedField.ClientID = _Policy.ClientId.Value;
                _PolicyLearnedField.ClientName = _Policy.ClientName;

                PolicyLearnedField.AddUpdateLearned(_PolicyLearnedField, _PolicyLearnedField.ProductType);
                PolicyLearnedField.AddUpdateHistoryLearnedNotCheckPayment(_Policy.PolicyId);


                using (DLinq.CommissionDepartmentEntities DataModel = Entity.DataModel)
                {
                    DataModel.EntriesByDEUs.Where(P => (P.PolicyID == _Policy.PolicyId) && (P.ClientID == Cliid)).ToList().ForEach(p => p.ClientID = _Policy.ClientId);
                    DataModel.SaveChanges();
                }
                RemoveLinkPaymentToPolicy(Cliid);

                RemoveClient(Cliid.Value, _Policy.PolicyLicenseeId.Value);
            }

            catch (Exception ex)
            {
                ActionLogger.Logger.WriteLog(DateTime.Now.ToString() + " MakePolicyActive exception: " + ex.Message, true);
                throw ex;
            }
        }

        public static void RemoveLinkPaymentToPolicy(Guid? Cliid)
        {
            LastViewPolicy.DeleteLastViewRecordClientIdWise(Cliid.Value);
        }



        //public static void DoLinkPolicy(Guid LicenseeId, Guid PendingPolicyId,Guid ClientId, Guid activePolicyId, List<Guid> PaymentsList, Guid CurrentUser)
        //{
        //    ActionLogger.Logger.WriteLog(DateTime.Now.ToString() + " DoLinkPolicy request: " + "License: " + LicenseeId + ",pendingpolicy: " +PendingPolicyId + ", Client: " + ClientId + ", activePOlicy: " + activePolicyId , true);
        //    foreach (Guid PolicyPaymentEntryId in PaymentsList)
        //    {
        //        bool IsPaidMarked = PolicyOutgoingDistribution.CheckIsEntryMarkPaid(PolicyPaymentEntryId);
        //        bool isScheduleMatch = ScheduleMatches(PolicyPaymentEntryId, activePolicyId);
        //        ActionLogger.Logger.WriteLog("payment - " + PolicyPaymentEntryId + ", schedule match: " + isScheduleMatch + ", ispaidmark: " + IsPaidMarked, true);
        //        DateTime ? InvoiceDt = PolicyPaymentEntriesPost.GetPolicyPaymentEntry(PolicyPaymentEntryId).InvoiceDate;

        //        try
        //        {
        //            ChangeInPaymentIncomingEntries(PolicyPaymentEntryId, activePolicyId);
        //            ActionLogger.Logger.WriteLog(DateTime.Now.ToString() + " ChangeInPaymentIncomingEntries done ", true);
        //            if (IsPaidMarked && isScheduleMatch)
        //            {
        //                ActionLogger.Logger.WriteLog(DateTime.Now.ToString() + " IsPaidmaked and schedule matched  ", true);
        //                AddRecordWithMinusAmountInPaymentOutgoingEntries(PolicyPaymentEntryId, PendingPolicyId, activePolicyId);
        //                // DistributeAmountToPayee(PolicyPaymentEntryId);
        //                PostUtill.EntryInPolicyOutGoingPayment(false, PolicyPaymentEntryId, activePolicyId, InvoiceDt, LicenseeId);
        //                //UpdateBatchData(PolicyPaymentEntryId);
        //                UpdateBatchStatusIfPaid(PolicyPaymentEntryId);
        //                //ActionLogger.Logger.WriteLog(DateTime.Now.ToString() + " Remainig policy entires14: ", true);
        //            }
        //            else if (!IsPaidMarked && isScheduleMatch)
        //            {
        //                //Same
        //                ActionLogger.Logger.WriteLog(DateTime.Now.ToString() + " !IsPaidmaked and schedule matched  ", true);
        //                // RemoveRecordFromOutgoing(PolicyPaymentEntryId);
        //                DeleteOutgoingPayments(PolicyPaymentEntryId);

        //                //DistributeAmountToPayee(PolicyPaymentEntryId);
        //                PostUtill.EntryInPolicyOutGoingPayment(false, PolicyPaymentEntryId, activePolicyId, InvoiceDt, LicenseeId);

        //                UpdateBatchStatusIfPaid(PolicyPaymentEntryId);
        //              //  ActionLogger.Logger.WriteLog(DateTime.Now.ToString() + " Remainig policy entires16: ", true);
        //            }
        //            else
        //            {
        //                ActionLogger.Logger.WriteLog(DateTime.Now.ToString() + " No processing of outgoing payments  ", true);
        //            }

        //        }
        //        catch (Exception ex)
        //        {
        //            ActionLogger.Logger.WriteLog(DateTime.Now.ToString() + " Dolinkpolicy exception: " + ex.Message, true);
        //        }
        //    }

        //    ActionLogger.Logger.WriteLog(DateTime.Now.ToString() + "Policy processing begins ", true);
        //    RunLearnedForPolicy(activePolicyId);


        //    //List<PolicyPaymentEntriesPost> listEntries = PolicyPaymentEntriesPost.GetPolicyPaymentEntryPolicyIDWise(PendingPolicyId);
        //    bool ifMorePaymentsExist = PolicyPaymentEntriesPost.IfPolicyHasPayments(PendingPolicyId);

        //    //if (listEntries == null || (listEntries != null && listEntries.Count == 0))
        //    if(!ifMorePaymentsExist) // No more payments with the policy, delete policy 
        //    {
        //        ActionLogger.Logger.WriteLog(DateTime.Now.ToString() + " Remainig policy entries: found 0, deleteing pending policy", true);
        //        Client.DeleteCascadeClient(PendingPolicyId, ClientId, LicenseeId);
        //    }
        //    else //By Acme - execute following only if the policy is not deleted 
        //    {
        //        ActionLogger.Logger.WriteLog(DateTime.Now.ToString() + " Remainig policy entires: Not found 0, pending policy not deleted", true);
        //        using (DLinq.CommissionDepartmentEntities InnerDataModel = Entity.DataModel)
        //        {
        //            int totalUnits = (InnerDataModel.PolicyLevelBillingDetails.Where(s => s.PolicyId == PendingPolicyId).Sum(s => s.TotalUnits)) ?? 0;
        //            DLinq.PolicyLevelBillingDetail policyDetailData = InnerDataModel.PolicyLevelBillingDetails.FirstOrDefault(s => s.PolicyId == activePolicyId);
        //            if (policyDetailData != null && totalUnits > 0)
        //            {
        //                policyDetailData.TotalUnits += totalUnits;
        //                InnerDataModel.SaveChanges();
        //            }
        //        }
        //    }

        //    ActionLogger.Logger.WriteLog(DateTime.Now.ToString() + " Dolinkpolicy success in businessLibrary ", true);
        //}
        //Acme added Nov 20, 2019 - to handle both paid/unpaid outgoing entries with a payment entry
        /// <summary>
        /// CreatedBy:Acmeminds
        /// CreatedOn:03-Feb-2020
        /// Purpose:This method used for dolink process in linking 
        /// </summary>
        /// <param name="PaymentEntryId"></param>
        static void AdjustOutgoingOnLink(Guid PaymentEntryId)
        {
            ActionLogger.Logger.WriteLog("AdjustOutgoingOnLink request: " + PaymentEntryId, true);
            List<PolicyOutgoingDistribution> _PolicyOutgoingDistributionLst = PolicyOutgoingDistribution.GetOutgoingPaymentByPoicyPaymentEntryId(PaymentEntryId);
            List<PolicyOutgoingDistribution> _PaidList = _PolicyOutgoingDistributionLst.Where(x => x.IsPaid == true).ToList();
            List<PolicyOutgoingDistribution> _UnPaidList = _PolicyOutgoingDistributionLst.Where(x => x.IsPaid == false).ToList();

            //Remove entries from unpaid list
            foreach (PolicyOutgoingDistribution _PolicyOutgoingDistribution in _UnPaidList)
            {
                try
                {
                    if (_PolicyOutgoingDistribution.IsPaid == false)
                    {
                        PolicyOutgoingDistribution.DeleteById(_PolicyOutgoingDistribution.OutgoingPaymentId);
                    }
                }
                catch (Exception ex)
                {
                    ActionLogger.Logger.WriteLog("AdjustOutgoingOnLink exception: " + ex.Message, true);
                }
            }
            ActionLogger.Logger.WriteLog("AdjustOutgoingOnLink IsRevrese is true: ", true);
            foreach (PolicyOutgoingDistribution _PolicyOutgoingDistribution in _PaidList)
            {
                try
                {
                    if (_PolicyOutgoingDistribution.IsPaid == true)
                    {
                        //check if exists the negative amount already in the system for this user, then skip adding again
                        List<PolicyOutgoingDistribution> minusRecord = _PaidList.Where(x => x.PaidAmount == _PolicyOutgoingDistribution.PaidAmount * (-1) && x.RecipientUserCredentialId == _PolicyOutgoingDistribution.RecipientUserCredentialId).ToList();
                        List<PolicyOutgoingDistribution> record = _PaidList.Where(x => x.PaidAmount == _PolicyOutgoingDistribution.PaidAmount && x.RecipientUserCredentialId == _PolicyOutgoingDistribution.RecipientUserCredentialId).ToList();

                        int recordCount = (record != null && record.Count > 0) ? record.Count : 0;
                        int minusRecordCount = (minusRecord != null && minusRecord.Count > 0) ? minusRecord.Count : 0;

                        if (recordCount != minusRecordCount)
                        {
                            ActionLogger.Logger.WriteLog("AdjustOutgoingOnLink Adding minus record ", true);
                            PolicyOutgoingDistribution _NewPolicyOutgoingDistribution = new PolicyOutgoingDistribution()
                            {
                                OutgoingPaymentId = Guid.NewGuid(),
                                PaymentEntryId = _PolicyOutgoingDistribution.PaymentEntryId,
                                RecipientUserCredentialId = _PolicyOutgoingDistribution.RecipientUserCredentialId,
                                PaidAmount = _PolicyOutgoingDistribution.PaidAmount * (-1),
                                CreatedOn = DateTime.Now,
                                IsPaid = false,

                            };
                            PolicyOutgoingDistribution.AddUpdateOutgoingPaymentEntry(_NewPolicyOutgoingDistribution);
                            ActionLogger.Logger.WriteLog("AdjustOutgoingOnLink minus record success", true);
                        }
                    }

                }
                catch (Exception ex)
                {
                    ActionLogger.Logger.WriteLog("AdjustOutgoingOnLink exception: " + ex.Message, true);
                }
            }
            ActionLogger.Logger.WriteLog("AdjustOutgoingOnLink done for Paid: " + PaymentEntryId, true);
        }
        public static void DoLinkPolicy(Guid LicenseeId, Guid PendingPolicyId, Guid ClientId, Guid activePolicyId, List<Guid> PaymentsList, Guid CurrentUser, bool IsReverse, bool IsUnlinkCall=false)
        {
            ActionLogger.Logger.WriteLog(DateTime.Now.ToString() + " DoLinkPolicy request: " + "License: " + LicenseeId + ",pendingpolicy: " + PendingPolicyId + ", Client: " + ClientId + ", activePOlicy: " + activePolicyId + ", isreverse: " + IsReverse + ", IsUnlinkCall: " + IsUnlinkCall, true);
            foreach (Guid PolicyPaymentEntryId in PaymentsList)
            {
                // bool IsPaidMarked = PolicyOutgoingDistribution.CheckIsEntryMarkPaid(PolicyPaymentEntryId);
                bool isScheduleMatch = (IsUnlinkCall) ? true  : ScheduleMatches(PolicyPaymentEntryId, activePolicyId);
                ActionLogger.Logger.WriteLog("payment - " + PolicyPaymentEntryId + ", schedule match: " + isScheduleMatch, true);
                DateTime? InvoiceDt = PolicyPaymentEntriesPost.GetPolicyPaymentEntry(PolicyPaymentEntryId).InvoiceDate;

                try
                {
                    ChangeInPaymentIncomingEntries(PolicyPaymentEntryId, activePolicyId);
                    ActionLogger.Logger.WriteLog(DateTime.Now.ToString() + " ChangeInPaymentIncomingEntries done ", true);

                    if (isScheduleMatch)
                    {
                        ActionLogger.Logger.WriteLog("payment - " + PolicyPaymentEntryId + ", isScheduleMatch true enteres ", true);
                        bool ifPaidOutgoingExists = PolicyOutgoingDistribution.IfPaymentHasPaidEntry(PolicyPaymentEntryId);
                        ActionLogger.Logger.WriteLog("payment - " + PolicyPaymentEntryId + ", ifPaidOutgoingExists: " + ifPaidOutgoingExists, true);
                        if (isScheduleMatch &&
                            (!ifPaidOutgoingExists || (ifPaidOutgoingExists && IsReverse))

                            )
                        {
                            ActionLogger.Logger.WriteLog("Link Case2 true ", true);
                            // RemoveRecordFromOutgoing(PolicyPaymentEntryId);
                            AdjustOutgoingOnLink(PolicyPaymentEntryId);
                            PostUtill.EntryInPolicyOutGoingPayment(false, PolicyPaymentEntryId, activePolicyId, InvoiceDt, LicenseeId);
                            UpdateBatchStatusIfPaid(PolicyPaymentEntryId);
                        }
                        else
                        {
                            ActionLogger.Logger.WriteLog("Link all cases false ", true);
                        }
                    }
                    else
                    {
                        ActionLogger.Logger.WriteLog(DateTime.Now.ToString() + " No processing of outgoing payments as schedule mismatch ", true);
                    }

                    /* if (IsPaidMarked && isScheduleMatch)
                     {
                         ActionLogger.Logger.WriteLog(DateTime.Now.ToString() + " IsPaidmaked and schedule matched  ", true);
                         AddRecordWithMinusAmountInPaymentOutgoingEntries(PolicyPaymentEntryId, PendingPolicyId, activePolicyId);
                         // DistributeAmountToPayee(PolicyPaymentEntryId);
                         PostUtill.EntryInPolicyOutGoingPayment(false, PolicyPaymentEntryId, activePolicyId, InvoiceDt, LicenseeId);
                         //UpdateBatchData(PolicyPaymentEntryId);
                         UpdateBatchStatusIfPaid(PolicyPaymentEntryId);
                         //ActionLogger.Logger.WriteLog(DateTime.Now.ToString() + " Remainig policy entires14: ", true);
                     }
                     else if (!IsPaidMarked && isScheduleMatch)
                     {
                         //Same
                         ActionLogger.Logger.WriteLog(DateTime.Now.ToString() + " !IsPaidmaked and schedule matched  ", true);
                         // RemoveRecordFromOutgoing(PolicyPaymentEntryId);
                         DeleteOutgoingPayments(PolicyPaymentEntryId);

                         //DistributeAmountToPayee(PolicyPaymentEntryId);
                         PostUtill.EntryInPolicyOutGoingPayment(false, PolicyPaymentEntryId, activePolicyId, InvoiceDt, LicenseeId);

                         UpdateBatchStatusIfPaid(PolicyPaymentEntryId);
                       //  ActionLogger.Logger.WriteLog(DateTime.Now.ToString() + " Remainig policy entires16: ", true);
                     }
                     else
                     {
                         ActionLogger.Logger.WriteLog(DateTime.Now.ToString() + " No processing of outgoing payments  ", true);
                     }*/

                }
                catch (Exception ex)
                {
                    ActionLogger.Logger.WriteLog(DateTime.Now.ToString() + " Dolinkpolicy exception: " + ex.Message, true);
                }
            }

            ActionLogger.Logger.WriteLog(DateTime.Now.ToString() + "Policy processing begins ", true);
            RunLearnedForPolicy(activePolicyId);


            //List<PolicyPaymentEntriesPost> listEntries = PolicyPaymentEntriesPost.GetPolicyPaymentEntryPolicyIDWise(PendingPolicyId);
            bool ifMorePaymentsExist = PolicyPaymentEntriesPost.IfPolicyHasPayments(PendingPolicyId);

            //if (listEntries == null || (listEntries != null && listEntries.Count == 0))
            if (!ifMorePaymentsExist) // No more payments with the policy, delete policy 
            {
                ActionLogger.Logger.WriteLog(DateTime.Now.ToString() + " Remainig policy entries: found 0, deleteing pending policy", true);
                Client.DeleteCascadeClient(PendingPolicyId, ClientId, LicenseeId);
            }
            else //By Acme - execute following only if the policy is not deleted 
            {
                ActionLogger.Logger.WriteLog(DateTime.Now.ToString() + " Remainig policy entires: Not found 0, pending policy not deleted", true);
                using (DLinq.CommissionDepartmentEntities InnerDataModel = Entity.DataModel)
                {
                    int totalUnits = (InnerDataModel.PolicyLevelBillingDetails.Where(s => s.PolicyId == PendingPolicyId).Sum(s => s.TotalUnits)) ?? 0;
                    DLinq.PolicyLevelBillingDetail policyDetailData = InnerDataModel.PolicyLevelBillingDetails.FirstOrDefault(s => s.PolicyId == activePolicyId);
                    if (policyDetailData != null && totalUnits > 0)
                    {
                        policyDetailData.TotalUnits += totalUnits;
                        InnerDataModel.SaveChanges();
                    }
                }
            }

            ActionLogger.Logger.WriteLog(DateTime.Now.ToString() + " Dolinkpolicy success in businessLibrary ", true);
        }
        //public static string ValidatePaymentsForLinking(Guid LicenseeId, Guid PendingPolicyId, Guid activePolicyId, List<Guid> PaymentsList)
        //{
        //    string message = string.Empty;
        //    try
        //    {
        //        if (BillingLineDetail.IsAgencyVersionLicense(LicenseeId)){
        //            ActionLogger.Logger.WriteLog("ValidatePaymentsForLinking, IsAgencyVersion is true", true);
        //            foreach(Guid payment in PaymentsList)
        //            {
        //                bool isScheduleMatch = ScheduleMatches(payment, activePolicyId);
        //                bool isMarkPaid = PolicyOutgoingDistribution.CheckIsEntryMarkPaid(payment);

        //                if(isScheduleMatch && isMarkPaid)
        //                {
        //                    message = @"Payment has been linked. " +
        //                                "However, this payment was already marked as paid." +
        //                                    "   If you would like to redistribute the payment, " +
        //                                    "      you will need to reverse the payment from " +
        //                                    "         the House account and pay " +
        //                                "the policy’s payees.  Would you like the system to" +
        //                                    "   reverse and redistribute for you?";
        //                    break;
        //                }
        //                else if(!isScheduleMatch && isMarkPaid)
        //                {
        //                    message = @"The outgoing payment schedule does not equal the " +
        //                            "incoming payment and was already marked as paid.  " +
        //                            "If you would like to redistribute the payment, " +
        //                            "you will need to adjust the outgoing schedule, " +
        //                            "reverse the payment from the House account and pay the policy’s payees. " +
        //                            "It is recommended you make the necessary changes prior and link after.  " +
        //                            "Are you sure you want to continue to link?";
        //                    break;
        //                }
        //                else if (!isScheduleMatch && !isMarkPaid)
        //                {
        //                    message = @"The outgoing payment schedule does not equal the incoming payment.  " +
        //                         "If you would like to redistribute the payment, you will need to adjust " +
        //                         "the outgoing schedule  and pay the policy’s payees. It is recommended " +
        //                         "you make the necessary changes prior and link after.  Are you sure you want to continue to link?";
        //                    break;
        //                }
        //            }
        //         }
        //        else
        //        {
        //            ActionLogger.Logger.WriteLog("ValidatePaymentsForLinking, IsAgencyVersion is false", true);
        //        }
        //    }
        //    catch(Exception ex)
        //    {
        //        ActionLogger.Logger.WriteLog("ValidatePaymentsForLinking, exception: " + ex.Message, true);
        //    }
        //    ActionLogger.Logger.WriteLog("ValidatePaymentsForLinking, msg: " + message, true);
        //    return message;
        //}
        /// <summary>
        /// MOdifiedBy:Acmeminds
        /// MOdifiedOn:Feb-03-2020
        /// Purpose:
        /// </summary>
        /// <param name="LicenseeId"></param>
        /// <param name="PendingPolicyId"></param>
        /// <param name="activePolicyId"></param>
        /// <param name="PaymentsList"></param>
        /// <param name="isResponseReqdOnFalse"></param>
        /// <returns></returns>
        public static string ValidatePaymentsForLinking(Guid LicenseeId, Guid PendingPolicyId, Guid activePolicyId, List<Guid> PaymentsList, out bool isResponseReqdOnFalse)
        {
            string message = string.Empty;
            isResponseReqdOnFalse = false;
            try
            {
                if (BillingLineDetail.IsAgencyVersionLicense(LicenseeId))
                {
                    ActionLogger.Logger.WriteLog("ValidatePaymentsForLinking, IsAgencyVersion is true", true);
                    foreach (Guid payment in PaymentsList)
                    {
                        bool isScheduleMatch = ScheduleMatches(payment, activePolicyId);
                        bool isMarkPaid = PolicyOutgoingDistribution.IfPaymentHasPaidEntry(payment);

                        if (isScheduleMatch && isMarkPaid)
                        {
                            message = @"Payment has been linked. " +
                                        "However, this payment was already marked as paid." +
                                            "   If you would like to redistribute the payment, " +
                                            "      you will need to reverse the payment from " +
                                            "         the House account and pay " +
                                        "the policy’s payees.  Would you like the system to" +
                                            "   reverse and redistribute for you?";
                            isResponseReqdOnFalse = true;
                            break;
                        }
                        else if (!isScheduleMatch && isMarkPaid)
                        {
                            message = @"The outgoing payment schedule does not equal the " +
                                    "incoming payment and was already marked as paid.  " +
                                    "If you would like to redistribute the payment, " +
                                    "you will need to adjust the outgoing schedule, " +
                                    "reverse the payment from the House account and pay the policy’s payees. " +
                                    "It is recommended you make the necessary changes prior and link after.  " +
                                    "Are you sure you want to continue to link?";
                            break;
                        }
                        else if (!isScheduleMatch && !isMarkPaid)
                        {
                            message = @"The outgoing payment schedule does not equal the incoming payment.  " +
                                 "If you would like to redistribute the payment, you will need to adjust " +
                                 "the outgoing schedule  and pay the policy’s payees. It is recommended " +
                                 "you make the necessary changes prior and link after.  Are you sure you want to continue to link?";
                            break;
                        }
                    }
                }
                else
                {
                    ActionLogger.Logger.WriteLog("ValidatePaymentsForLinking, IsAgencyVersion is false", true);
                }
            }
            catch (Exception ex)
            {
                ActionLogger.Logger.WriteLog("ValidatePaymentsForLinking, exception: " + ex.Message, true);
            }
            ActionLogger.Logger.WriteLog("ValidatePaymentsForLinking, msg: " + message, true);
            return message;
        }


        public static void DoLinkPolicy(Guid LicenseeId, bool IsReverse, bool IsLinkWithExistingPolicy, Guid PendingPolicyId, Guid ClientId, Guid activePolicyId, Guid PolicyPaymentEntryId, Guid CurrentUser, /*Guid PendingPayorId,
            Guid ActivePayorId,*/ bool IsAgencyVersion, bool IsPaidMarked, bool IsScheduleMatches, UserRole _UserRole)
        {
            try
            {
                ActionLogger.Logger.WriteLog(DateTime.Now.ToString() + " Do link policy request: " + LicenseeId + ", PendingPolicyId : " + PendingPolicyId + ", ClientID: " + ClientId + ", activePOlicy: " + activePolicyId + ", paymentEntry: " + PolicyPaymentEntryId + ", User: " + CurrentUser, true);
            }
            catch (Exception ex)
            {
                ActionLogger.Logger.WriteLog(ex.Message, true);
            }
            var options = new TransactionOptions
            {
                IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted,
                Timeout = TimeSpan.FromMinutes(60)
            };
            DateTime? InvoiceDt = PolicyPaymentEntriesPost.GetPolicyPaymentEntry(PolicyPaymentEntryId).InvoiceDate;

            //using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required, options))
            //{
            try
            {
                // Acme commented, as not in use - int NonPendPolicyPaymentEntryCnt = PolicyPaymentEntriesPost.GetPolicyPaymentEntryPolicyIDWise(activePolicyId).Count;//Get record of policy via entryId
                ChangeInPaymentIncomingEntries(PolicyPaymentEntryId, activePolicyId);
                ActionLogger.Logger.WriteLog(DateTime.Now.ToString() + " Remainig policy entires12: ", true);
                if (IsAgencyVersion && IsPaidMarked && IsScheduleMatches && IsReverse && IsLinkWithExistingPolicy)
                {
                    ActionLogger.Logger.WriteLog(DateTime.Now.ToString() + " Remainig policy entires13: ", true);
                    AddRecordWithMinusAmountInPaymentOutgoingEntries(PolicyPaymentEntryId, PendingPolicyId, activePolicyId);
                    // DistributeAmountToPayee(PolicyPaymentEntryId);
                    PostUtill.EntryInPolicyOutGoingPayment(false, PolicyPaymentEntryId, activePolicyId, InvoiceDt, LicenseeId);
                    //UpdateBatchData(PolicyPaymentEntryId);
                    UpdateBatchStatusIfPaid(PolicyPaymentEntryId);
                    ActionLogger.Logger.WriteLog(DateTime.Now.ToString() + " Remainig policy entires14: ", true);
                }
                else if (IsAgencyVersion && !IsPaidMarked && IsScheduleMatches && IsLinkWithExistingPolicy)
                {
                    //Same
                    ActionLogger.Logger.WriteLog(DateTime.Now.ToString() + " Remainig policy entires15: ", true);
                    // RemoveRecordFromOutgoing(PolicyPaymentEntryId);
                    DeleteOutgoingPayments(PolicyPaymentEntryId);

                    //DistributeAmountToPayee(PolicyPaymentEntryId);
                    PostUtill.EntryInPolicyOutGoingPayment(false, PolicyPaymentEntryId, activePolicyId, InvoiceDt, LicenseeId);

                    UpdateBatchStatusIfPaid(PolicyPaymentEntryId);
                    ActionLogger.Logger.WriteLog(DateTime.Now.ToString() + " Remainig policy entires16: ", true);
                }
                ActionLogger.Logger.WriteLog(DateTime.Now.ToString() + " Remainig policy entires11: ", true);

                //#region "Updated code"

                //int PendPolicyPaymentEntryCnt = PolicyPaymentEntriesPost.GetPolicyPaymentEntryPolicyIDWise(PendingPolicyId).ToList().Count;//Get record of policy via entryId

                //if (PendPolicyPaymentEntryCnt == 1)
                //{
                //    RunLearnedForPolicy(activePolicyId);
                //    RunFollowp(activePolicyId, PolicyPaymentEntryId, _UserRole);//For Active
                //}                    

                //if (PendPolicyPaymentEntryCnt == 0)
                //{
                //    Client.DeleteCascadeClient(PendingPolicyId, ClientId, LicenseeId);
                //}
                //else//For Pending
                //{
                //    if (PendPolicyPaymentEntryCnt == 1)
                //    {
                //        FollowUpUtill.FollowUpProcedure(FollowUpRunModules.PaymentDeleted, null, PendingPolicyId, PostUtill.GetPolicy(PendingPolicyId).IsTrackPayment, true, _UserRole, null);
                //    }
                //}

                //#endregion


                //RunLearnedForPolicy(activePolicyId);
                //RunFollowp(activePolicyId, PolicyPaymentEntryId, _UserRole);//For Active policy

                //Thread  ThFollowup=new Thread(() =>
                //{
                ActionLogger.Logger.WriteLog(DateTime.Now.ToString() + " Remainig policy entires1: ", true);
                RunLearnedForPolicy(activePolicyId);
                // RunFollowp(activePolicyId, PolicyPaymentEntryId, _UserRole);//For Active policy
                //});

                //ThFollowup.IsBackground = true;
                //ThFollowup.Start();

                List<PolicyPaymentEntriesPost> listEntries = PolicyPaymentEntriesPost.GetPolicyPaymentEntryPolicyIDWise(PendingPolicyId);

                // int PendPolicyPaymentEntryCnt = (listEntries== null) ? 0 : listEntries.Count;//Get record of policy via entryId

                //ActionLogger.Logger.WriteLog(DateTime.Now.ToString() + " Remainig policy entires: " + PendPolicyPaymentEntryCnt, true);

                //if (PendPolicyPaymentEntryCnt == 0) 
                if (listEntries == null || (listEntries != null && listEntries.Count == 0))
                {
                    ActionLogger.Logger.WriteLog(DateTime.Now.ToString() + " Remainig policy entries: found 0, deleteing pending policy", true);
                    Client.DeleteCascadeClient(PendingPolicyId, ClientId, LicenseeId);
                }
                else //By Acme - execute following only if the policy is not deleted 
                {
                    ActionLogger.Logger.WriteLog(DateTime.Now.ToString() + " Remainig policy entires: Not found 0, pending policy not deleted", true);
                    using (DLinq.CommissionDepartmentEntities InnerDataModel = Entity.DataModel)
                    {
                        int totalUnits = (InnerDataModel.PolicyLevelBillingDetails.Where(s => s.PolicyId == PendingPolicyId).Sum(s => s.TotalUnits)) ?? 0;
                        DLinq.PolicyLevelBillingDetail policyDetailData = InnerDataModel.PolicyLevelBillingDetails.FirstOrDefault(s => s.PolicyId == activePolicyId);
                        if (policyDetailData != null && totalUnits > 0)
                        {
                            policyDetailData.TotalUnits += totalUnits;
                            InnerDataModel.SaveChanges();
                        }
                    }
                }
                #region Commented 
                //else//For Pending policy
                //{
                //    //Thread ThFollowup1 = new Thread(() =>
                //    //{
                //    //    FollowUpUtill.FollowUpProcedure(FollowUpRunModules.PaymentDeleted, null, PendingPolicyId, PostUtill.GetPolicy(PendingPolicyId).IsTrackPayment, true,  , null);
                //    //});

                //    //ThFollowup1.IsBackground = true;
                //    //ThFollowup1.Start();
                //    //FollowUpUtill.FollowUpProcedure(FollowUpRunModules.PaymentDeleted, null, PendingPolicyId, PostUtill.GetPolicy(PendingPolicyId).IsTrackPayment, true, _UserRole, null);
                //}



                //Update policy billing database... Shifted above by Acme
                //using (DLinq.CommissionDepartmentEntities InnerDataModel = Entity.DataModel)
                //{
                //    int totalUnits = (InnerDataModel.PolicyLevelBillingDetails.Where(s => s.PolicyId == PendingPolicyId).Sum(s => s.TotalUnits)) ?? 0;
                //    DLinq.PolicyLevelBillingDetail policyDetailData = InnerDataModel.PolicyLevelBillingDetails.FirstOrDefault(s => s.PolicyId == activePolicyId);
                //    if (policyDetailData != null && totalUnits > 0)
                //    {
                //        policyDetailData.TotalUnits += totalUnits;
                //        InnerDataModel.SaveChanges();
                //    }
                //}

                //Acme commented the followin block - as it is repeat of DeletecascadeClient
                //if (PendPolicyPaymentEntryCnt == 0)
                //{
                //    try
                //    {
                //        RemovePendingPolicy(PendingPolicyId);

                //        RemoveEntryByDEU(PendingPolicyId, PolicyPaymentEntryId);

                //        RemoveClient(ClientId, LicenseeId);
                //    }
                //    catch (Exception ex)
                //    {
                //        ActionLogger.Logger.WriteLog(DateTime.Now.ToString() + " Dolinkpolicy exception on removing entries: " + ex.Message, true);
                //    }
                //}
                //ts.Complete();
                #endregion

                ActionLogger.Logger.WriteLog(DateTime.Now.ToString() + " Dolinkpolicy success in businessLibrary ", true);
            }
            catch (Exception ex)
            {
                ActionLogger.Logger.WriteLog(DateTime.Now.ToString() + " Dolinkpolicy exception: " + ex.Message, true);
            }
            //}
        }

        private static void RemoveEntryByDEU(Guid PendingPolicyId, Guid paymentEntryID)
        {

            using (DLinq.CommissionDepartmentEntities DataModel = Entity.DataModel)
            {
                List<DLinq.PolicyPaymentEntry> PolicyPaymentEntryLst = DataModel.PolicyPaymentEntries.Where(p => p.PolicyId == PendingPolicyId && p.PaymentEntryId == paymentEntryID).ToList();
                foreach (DLinq.PolicyPaymentEntry ppe in PolicyPaymentEntryLst)
                {
                    List<DLinq.EntriesByDEU> EntriesByDEULst = DataModel.EntriesByDEUs.Where(p => p.PolicyID == PendingPolicyId && p.DEUEntryID == ppe.DEUEntryId).ToList();
                    foreach (DLinq.EntriesByDEU ebdeu in EntriesByDEULst)
                    {
                        DataModel.DeleteObject(ebdeu);

                    }
                    DataModel.SaveChanges();
                }
            }
        }

        private static void RemovePolicyPaymentEntry(Guid PendingPolicyId)
        {
            using (DLinq.CommissionDepartmentEntities DataModel = Entity.DataModel)
            {
                List<DLinq.PolicyPaymentEntry> PolicyPaymentEntryLst = DataModel.PolicyPaymentEntries.Where(p => p.PolicyId == PendingPolicyId).ToList();
                foreach (DLinq.PolicyPaymentEntry ppe in PolicyPaymentEntryLst)
                {
                    DataModel.DeleteObject(ppe);

                }
                DataModel.SaveChanges();
            }
        }

        private static void RemoveLastViewRecord(Guid PendingPolicyId)
        {
            LastViewPolicy.DeleteLastViewRecordPolicyIdWise(PendingPolicyId);
        }

        private static void UpdateDeuPolicyEntry(Guid _PolicyPaymentEntryId, Guid activePolicyId)
        {
            Guid _DeuEntryId = PolicyPaymentEntriesPost.GetPolicyPaymentEntry(_PolicyPaymentEntryId).DEUEntryId.Value;
            DEU _deu = DEU.GetDeuEntryidWise(_DeuEntryId);
            _deu.PolicyId = activePolicyId;
            //DEU.AddupdateDeuEntry(_deu);

            //Create object
            DEU objDEU = new DEU();
            objDEU.AddupdateDeuEntry(_deu);
        }

        private static void DeleteOutgoingPayments(Guid paymentEntryID)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(DBConnection.GetConnectionString()))
                {
                    using (SqlCommand cmd = new SqlCommand("Usp_DeleteOutgoingPayments", con))
                    {
                        con.Open();
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@paymentEntryID", paymentEntryID);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                ActionLogger.Logger.WriteLog("DeleteOutgoingPayments ex: " + ex.Message, true);
            }
        }
        /*private static void RemoveRecordFromOutgoing(Guid PolicyPaymentEntryId)
        {
            List<PolicyOutgoingDistribution> _PolicyOutgoingDistribution = PolicyOutgoingDistribution.GetOutgoingPaymentByPoicyPaymentEntryId(PolicyPaymentEntryId);
            foreach (PolicyOutgoingDistribution POD in _PolicyOutgoingDistribution)
            {
                PolicyOutgoingDistribution.DeleteById(POD.OutgoingPaymentId);
            }
        }*/

        private static void RunFollowp(Guid PolicyId, Guid PolicyPaymentEntryId, UserRole _UserRole)
        {
            bool IsTrackPayment = PostUtill.GetPolicy(PolicyId).IsTrackPayment;
            Guid DeuEntryId = GetDEUEntryId(PolicyPaymentEntryId);
            DEU _DeuEntry = DEU.GetDeuEntryidWise(DeuEntryId);

            FollowUpUtill.FollowUpProcedure(FollowUpRunModules.PaymentEntered, _DeuEntry, PolicyId, IsTrackPayment, true, _UserRole, null);
        }

        // private static void RunLearnedForPolicy(Guid CurrentUser, Guid LicenseeId, Guid PolicyPaymentEntryId, Guid PolicyId)
        private static void RunLearnedForPolicy(Guid PolicyId)
        {
            //DEU _LatestDEUrecord = DEU.GetLatestInvoiceDateRecord(PolicyId);
            try
            {
                //instance method
                DEU objDeu = new DEU();
                DEU _LatestDEUrecord = objDeu.GetLatestInvoiceDateRecord(PolicyId);

                if (_LatestDEUrecord != null)
                {
                    Guid PolicyIdDeuToLrn = DEULearnedPost.AddDataDeuToLearnedPost(_LatestDEUrecord);
                    if (PolicyIdDeuToLrn != null)
                    {
                        LearnedToPolicyPost.AddUpdateLearnedToPolicy(PolicyIdDeuToLrn);
                        PolicyToLearnPost.AddUpdatPolicyToLearn(PolicyIdDeuToLrn);
                    }

                }
            }
            catch (Exception ex)
            {
                ActionLogger.Logger.WriteLog(DateTime.Now.ToString() + " RunLearnedForPolicy exception: " + ex.Message, true);

            }
            //DEUFields _DEUFields = new DEUFields()
            //{
            //    CurrentUser = CurrentUser,
            //    LicenseeId = LicenseeId,
            //   DeuEntryId = GetDEUEntryId(PolicyPaymentEntryId),
            // };
            // DEU _DEU=DEU.GetDeuEntryidWise(GetDEUEntryId(PolicyPaymentEntryId));

            //DEULearnedPost.AddDataDeuToLearnedPost( _DEU);//Need to verify
            //LearnedToPolicyPost.AddUpdateLearnedToPolicy(_DEU.PolicyId);
            //PolicyToLearnPost.AddUpdatPolicyToLearn(_DEU.PolicyId);
        }

        private static Guid GetDEUEntryId(Guid PolicyPaymentEntryId)
        {
            Guid DEUEntryId = Guid.Empty;
            try
            {
                PolicyPaymentEntriesPost objPolicyPaymentEntriesPost = PolicyPaymentEntriesPost.GetPolicyPaymentEntry(PolicyPaymentEntryId);
                if (objPolicyPaymentEntriesPost != null)
                {
                    DEUEntryId = (Guid)objPolicyPaymentEntriesPost.DEUEntryId;
                }

                if (DEUEntryId == null)
                {
                    return Guid.Empty;
                }
            }
            catch (Exception ex)
            {
                ActionLogger.Logger.WriteLog(DateTime.Now.ToString() + " GetDEUEntry exception : " + ex.Message, true);
            }
            return DEUEntryId;
        }

        private static void RemoveLearnedPolicy(Guid PolicyId)
        {
            try
            {
                PolicyLearnedField.DeleteLearnedHistory(PolicyId);
                PolicyLearnedField.DeleteByPolicy(PolicyId);
            }
            catch (Exception ex)
            {
                ActionLogger.Logger.WriteLog(DateTime.Now.ToString() + " GetDEUEntry exception : " + ex.Message, true);
            }
        }

        private static void RemoveFollowUp(Guid PolicyId)
        {
            FollowupIssue.DeleteFollowupByPolicyId(PolicyId);
        }

        private static void AddRecordWithMinusAmountInPaymentOutgoingEntries(Guid PaymentEntryId, Guid PendingPolicyId, Guid ActivePolicyId)
        {
            List<PolicyOutgoingDistribution> _PolicyOutgoingDistributionLst = PolicyOutgoingDistribution.GetOutgoingPaymentByPoicyPaymentEntryId(PaymentEntryId);
            // PolicyOutgoingDistribution _PolicyOutgoingDistribution = _PolicyOutgoingDistributionLst.First();
            foreach (PolicyOutgoingDistribution _PolicyOutgoingDistribution in _PolicyOutgoingDistributionLst)
            {
                try
                {
                    PolicyOutgoingDistribution _NewPolicyOutgoingDistribution = new PolicyOutgoingDistribution()
                    {
                        OutgoingPaymentId = Guid.NewGuid(),
                        PaymentEntryId = _PolicyOutgoingDistribution.PaymentEntryId,
                        RecipientUserCredentialId = _PolicyOutgoingDistribution.RecipientUserCredentialId,
                        PaidAmount = _PolicyOutgoingDistribution.PaidAmount * (-1),
                        CreatedOn = _PolicyOutgoingDistribution.CreatedOn,
                        // ReferencedOutgoingScheduleId = _PolicyOutgoingDistribution.ReferencedOutgoingScheduleId,
                        // ReferencedOutgoingAdvancedScheduleId = _PolicyOutgoingDistribution.ReferencedOutgoingAdvancedScheduleId,
                        IsPaid = false,

                    };
                    PolicyOutgoingDistribution.AddUpdateOutgoingPaymentEntry(_NewPolicyOutgoingDistribution);
                }
                catch (Exception ex)
                {
                    ActionLogger.Logger.WriteLog("AddRecordWithMinusAmountInPaymentOutgoingEntries exception: " + ex.Message, true);
                }
            }

        }


        /// <summary>
        /// Update batch status if marked paid, to partial unpaid after linking 
        /// </summary>
        /// <param name="paymentID"></param>
        static void UpdateBatchStatusIfPaid(Guid paymentID)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(DBConnection.GetConnectionString()))
                {
                    con.Open();
                    using (System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("Usp_UpdateBatchStatusOnLink", con))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.CommandTimeout = 0;
                        cmd.Parameters.AddWithValue("@paymentEntryID", paymentID);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                ActionLogger.Logger.WriteLog("UpdateBatchStatusIdPaid exception: " + ex.Message, true);
            }
        }
        //private static void UpdateBatchData(Guid PolicyEntryId)
        //{
        //    Batch objBatch = new Batch();
        //    //Batch _Batch = Batch.GetBatchEntryViaEntryId(PolicyEntryId);
        //    Batch _Batch = objBatch.GetBatchEntryViaEntryId(PolicyEntryId);
        //    if (_Batch.EntryStatus == EntryStatus.Paid)
        //    {
        //        _Batch.EntryStatus = EntryStatus.PartialUnpaid;
        //        _Batch.AddUpdate();
        //    }
        //}

        /// <summary>
        /// Remove Client from id if it not associate from any policy--Client will never remove physically .only pne flag IsDelete is set for this
        /// </summary>
        /// <param name="ClientId"></param>
        private static void RemoveClient(Guid ClientId, Guid LicenseeId)
        {
            PostUtill.RemoveClient(ClientId, LicenseeId);
        }

        private static void RemovePendingPolicy(Guid PolicyId)
        {
            try
            {
                PolicyDetailsData _Policy = new PolicyDetailsData()
                {
                    PolicyId = PolicyId,
                };
                Policy.DeletePolicyHistoryPermanentById(_Policy);
                Policy.DeletePolicyFromDB(_Policy);
            }
            catch (Exception ex)
            {
                ActionLogger.Logger.WriteLog(DateTime.Now.ToString() + " RemovePendingPolicy exception : " + ex.Message, true);
            }
        }

        //private static void DistributeAmountToPayee(Guid PolicyPaymentEntryId)
        //{
        //    PolicyPaymentEntriesPost _PolicyPaymentEntriesPost = GetPayemntEntry(PolicyPaymentEntryId);//Get record of policy via entryId
        //    Guid PolicyId = _PolicyPaymentEntriesPost.PolicyID;
        //    DateTime? InvoiceDt = _PolicyPaymentEntriesPost.InvoiceDate;
        //    Guid LicenseeId = Guid.Empty;//No need it becoz it is use for HO n for here HO should be false
        //    PostUtill.EntryInPolicyOutGoingPayment(false, PolicyPaymentEntryId, PolicyId, InvoiceDt, LicenseeId);
        //}

        //private static void DetectedAmountFromWrongActive(Guid guid, Guid guid1)
        //{
        //    throw new NotImplementedException();
        //}

        private static PolicyPaymentEntriesPost GetPayemntEntry(Guid PolicyEntryId)
        {
            return PolicyPaymentEntriesPost.GetPolicyPaymentEntry(PolicyEntryId);
        }

        // private static void ChangeInPaymentOutGoingEntries(Guid PolicyEntryId, Guid PendingPolicyId, Guid ActivePolicyId,PolicySchedule _PolicySchedule)
        private static void ChangeInPaymentOutGoingEntries(Guid PolicyEntryId, Guid PendingPolicyId, Guid ActivePolicyId)
        {
            List<PolicyOutgoingDistribution> _PolicyOutgoingDistributionLst = PolicyOutgoingDistribution.GetOutgoingPaymentByPoicyPaymentEntryId(PolicyEntryId);
            PolicyOutgoingDistribution _PolicyOutgoingDistribution = _PolicyOutgoingDistributionLst.FirstOrDefault();

        }

        private static void ChangeInPaymentIncomingEntries_old(Guid PolicyEntryId, Guid PendingPolicyId, Guid ActivePolicyId)
        {
            try
            {
                PolicyPaymentEntriesPost _PolicyPaymentEntriesPost = PolicyPaymentEntriesPost.GetPolicyPaymentEntry(PolicyEntryId);
                PolicyDetailsData newPolicy = PostUtill.GetPolicy(ActivePolicyId); //Acme added 

                _PolicyPaymentEntriesPost.PolicyID = ActivePolicyId;
                //Set true when link the payment from comp manager
                _PolicyPaymentEntriesPost.IsLinkPayment = true;
                PolicyPaymentEntriesPost.AddUpadate(_PolicyPaymentEntriesPost);
                //Update DEU--3-5-2011
                DEU _DEU = DEU.GetDeuEntryidWise(_PolicyPaymentEntriesPost.DEUEntryId.Value);
                _DEU.PolicyId = ActivePolicyId;
                _DEU.ClientID = newPolicy.ClientId;
                _DEU.ClientName = newPolicy.ClientName;
                _DEU.PolicyNumber = newPolicy.PolicyNumber;

                //DEU.AddupdateDeuEntry(_DEU);               
                DEU objDEU = new DEU();
                objDEU.AddupdateDeuEntry(_DEU);
            }
            catch (Exception ex)
            {
                ActionLogger.Logger.WriteLog(DateTime.Now.ToString() + " ChangeInPaymentIncomingEntries exception : " + ex.Message, true);
            }
        }

        private static void ChangeInPaymentIncomingEntries(Guid PolicyEntryId, Guid ActivePolicyId)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(DBConnection.GetConnectionString()))
                {
                    using (SqlCommand cmd = new SqlCommand("usp_UpdatePaymentEntryOnLink", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@paymentEntryID", PolicyEntryId);
                        cmd.Parameters.AddWithValue("@newPolicyID", ActivePolicyId);
                        con.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                ActionLogger.Logger.WriteLog(DateTime.Now.ToString() + " ChangeInPaymentIncomingEntries exception : " + ex.Message, true);
            }
        }

        public static bool ScheduleMatches(Guid EntryId, Guid ActivePolicyId)
        {
            bool Flag = PostUtill.CheckForOutgoingScheduleVariance(EntryId, ActivePolicyId);
            return Flag;
        }

        private static bool PolicyPaidMarked()
        {
            throw new NotImplementedException();
        }

        private static bool GetAgencyVersion()
        {
            throw new NotImplementedException();
        }
    }


}
