using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ServiceModel;
using MyAgencyVault.BusinessLibrary;
using MyAgencyVault.WcfService.Library.Response;
using System.ServiceModel.Web;
using MyAgencyVault.BusinessLibrary.BusinessObjects;

namespace MyAgencyVault.WcfService
{
    [ServiceContract]
    interface ILinkPaymentPolicyService
    {
        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        LinkPaymentsConditionsResponse GetConditionsToLinkService(Guid licenseeId, Guid paymentEntryId, Guid activePolicyId);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        LinkPaymentsPolicyResponse GetPendingPoliciesForLinkedPolicyService(Guid licenseeId, ListParams listParams);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        LinkPaymentResponse GetPaymentsForLinkedPolicy(Guid policyId);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        LinkPaymentsPolicyResponse GetAllPoliciesForLinkedPolicyService(Guid licenseeId, ListParams listParams, string ClientID, string PayorID);

        //[OperationContract] - Found not in use 
        //[WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        //JSONResponse AddUpdateLinkPaymentService(LinkPaymentPolicies _LinkPaymentPolicies);

        //[OperationContract]
        //[WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        //JSONResponse DoLinkPolicyService(Guid licenseeId, bool isReverse, bool isLinkWithExistingPolicy, Guid pendingPolicyId,
        //    Guid clientId, Guid activePolicyId, Guid policyPaymentEntryId, Guid currentUser,/* Guid pendingPayorId,
        //    Guid activePayorId,*/ bool isAgencyVersion, bool isPaidMarked, bool isScheduleMatches, UserRole userRole);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        JSONResponse MakePolicyActiveService(Guid PolicyId, Guid ClientId,Guid loggedInUserId);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        PolicyBoolResponse ScheduleMatchesService(Guid EntryId, Guid ActivePolicyId);
        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        LinkValidationResponse ValidatePaymentsService(Guid licenseeId, Guid pendingPolicyId, Guid activePolicyId, List<Guid> paymentsList);


        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        JSONResponse DoLinkPolicyService(Guid licenseeId, Guid pendingPolicyId, Guid clientId, Guid activePolicyId, Guid currentUser, List<Guid> paymentsList, bool isReverse = false);
    }
    public partial class MavService : ILinkPaymentPolicyService
    {

        public LinkPaymentsConditionsResponse GetConditionsToLinkService(Guid licenseeId, Guid paymentEntryId, Guid activePolicyId)
        {
            LinkPaymentsConditionsResponse jres = null;
            ActionLogger.Logger.WriteLog("GetPendingPoliciesForLinkedPolicyService request: Batch " + licenseeId, true);
            try
            {
                bool isAgencyVersion = BillingLineDetail.IsAgencyVersionLicense(licenseeId);
                bool isPaymentPaid = PolicyOutgoingDistribution.CheckIsEntryMarkPaid(paymentEntryId);
                bool scheduleMatches = LinkPaymentPolicies.ScheduleMatches(paymentEntryId, activePolicyId);

                jres = new LinkPaymentsConditionsResponse(string.Format("Link conditions found successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                jres.ScheduleMatches = scheduleMatches;
                jres.IsMarkedPaid = isPaymentPaid;
                jres.IsAgencyVersion = isAgencyVersion;

                ActionLogger.Logger.WriteLog("GetPendingPoliciesForLinkedPolicyService success ", true);
            }
            catch (Exception ex)
            {
                jres = new LinkPaymentsConditionsResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                ActionLogger.Logger.WriteLog("GetPendingPoliciesForLinkedPolicyService failure " + ex.Message, true);
            }
            return jres;
        }

        /// <summary>
        /// ModifiedBy:Ankit Kahndelwal
        /// ModifiedOn:28-05-2019
        /// Purpose:Get the list of pending policies for Comp Manager
        /// </summary>
        /// <param name="licenseeId"></param>
        /// <returns></returns>
        public LinkPaymentsPolicyResponse GetPendingPoliciesForLinkedPolicyService(Guid licenseeId, ListParams listParams)
        {
            LinkPaymentsPolicyResponse jres = null;
            ActionLogger.Logger.WriteLog("GetPendingPoliciesForLinkedPolicyService request: Batch " + licenseeId, true);
            try
            {
                List<LinkPaymentPolicies> lst = LinkPaymentPolicies.GetPendingPolicies(licenseeId, listParams, out int recordCount);

                jres = new LinkPaymentsPolicyResponse(string.Format("Policies list found successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                jres.LinkPaymentsList = lst;
                jres.RecordCount = recordCount;
                ActionLogger.Logger.WriteLog("GetPendingPoliciesForLinkedPolicyService success ", true);
            }
            catch (Exception ex)
            {
                jres = new LinkPaymentsPolicyResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                ActionLogger.Logger.WriteLog("GetPendingPoliciesForLinkedPolicyService failure " + ex.Message, true);
            }
            return jres;
        }
        /// <summary>
        /// Modified By:Ankit Khandelwal
        /// ModifiedOn:30-05-2019
        /// 
        /// </summary>
        /// <param name="policyId"></param>
        /// <returns></returns>
        public LinkPaymentResponse GetPaymentsForLinkedPolicy(Guid policyId)
        {
            LinkPaymentResponse jres = null;
            ActionLogger.Logger.WriteLog("GetPendingPoliciesForLinkedPolicyService request: Batch " + policyId, true);
            try
            {
                List<LinkPayment> lst = LinkPaymentReciptRecords.GetLinkPaymentReciptRecordsByPolicyId(policyId);
                LinkPendingPolicy policy = LinkPaymentPolicies.GetPendingPolicydetail(policyId);

                //Following list is specific to the front end requirement to bind the data
                List<LinkPendingPolicy> lstPolicy = new List<LinkPendingPolicy>();
                lstPolicy.Add(policy);
                jres = new LinkPaymentResponse(string.Format("Policies list found successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                jres.PolicyDetail = lstPolicy;
                jres.LinkPaymentsList = lst;
                ActionLogger.Logger.WriteLog("GetPendingPoliciesForLinkedPolicyService success ", true);
            }
            catch (Exception ex)
            {
                jres = new LinkPaymentResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                ActionLogger.Logger.WriteLog("GetPendingPoliciesForLinkedPolicyService failure " + ex.Message, true);
            }
            return jres;
        }


        /// <summary>
        /// Modified By :Ankit Khandelwal
        /// Modified On:03-06-2019
        /// Purpose:to get the list of active policies for Linking in comp manager
        /// </summary>
        /// <param name="LicencessId"></param>
        /// <returns></returns>
        public LinkPaymentsPolicyResponse GetAllPoliciesForLinkedPolicyService(Guid licenseeId, ListParams listParams, string ClientID ,string PayorID )
        {
            LinkPaymentsPolicyResponse jres = null;
            ActionLogger.Logger.WriteLog("GetAllPoliciesForLinkedPolicy request: licenseeId " + licenseeId + ", clientID: " + ClientID + ", Payor: " + PayorID, true);
            try
            {
                List<LinkPaymentPolicies> lst = LinkPaymentPolicies.GetAllPoliciesForLinking(licenseeId, listParams, out string recordCount,ClientID, PayorID);
                jres = new LinkPaymentsPolicyResponse(string.Format("Policies list found successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                jres.LinkPaymentsList = lst;
                jres.RecordCount = Convert.ToInt32(recordCount);
                ActionLogger.Logger.WriteLog("GetAllPoliciesForLinkedPolicy success ", true);
            }
            catch (Exception ex)
            {
                jres = new LinkPaymentsPolicyResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                ActionLogger.Logger.WriteLog("GetAllPoliciesForLinkedPolicy failure " + ex.Message, true);
            }
            return jres;
        }

        //public JSONResponse AddUpdateLinkPaymentService(LinkPaymentPolicies _LinkPaymentPolicies)
        //{
        //    JSONResponse jres = null;
        //    ActionLogger.Logger.WriteLog("AddUpdateLinkPaymentService request: _LinkPaymentPolicies" + _LinkPaymentPolicies.ToStringDump(), true);
        //    try
        //    {
        //        _LinkPaymentPolicies.AddUpdate(_LinkPaymentPolicies);
        //        jres = new JSONResponse(string.Format("Payments saved successfully"), Convert.ToInt16(ResponseCodes.Success), "");
        //        ActionLogger.Logger.WriteLog("AddUpdateLinkPaymentService success ", true);
        //    }
        //    catch (Exception ex)
        //    {
        //        jres = new JSONResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
        //        ActionLogger.Logger.WriteLog("AddUpdateLinkPaymentService failure ", true);
        //    }
        //    return jres;
        //}

        public JSONResponse DoLinkPolicyService(Guid LicenseeId, bool IsReverse, bool IsLinkWithExistingPolicy, Guid PendingPolicyId,
            Guid ClientId, Guid activePolicyId, Guid PolicyPaymentEntryId, Guid CurrentUser,/* Guid PendingPayorId,
    Guid ActivePayorId,*/ bool IsAgencyVersion, bool IsPaidMarked, bool IsScheduleMatches, UserRole _UserRole)
        {
            JSONResponse jres = null;
            ActionLogger.Logger.WriteLog("DoLinkPolicyService request: LicenseeId" + LicenseeId + ", PendingPolicyId: " + PendingPolicyId + ",activePolicyId " + activePolicyId + ", PolicyPaymentEntryId " + PolicyPaymentEntryId, true);
            try
            {
                LinkPaymentPolicies.DoLinkPolicy(LicenseeId, IsReverse, IsLinkWithExistingPolicy, PendingPolicyId,
               ClientId, activePolicyId, PolicyPaymentEntryId, CurrentUser, /*PendingPayorId,
       ActivePayorId,*/ IsAgencyVersion, IsPaidMarked, IsScheduleMatches, _UserRole);

                jres = new JSONResponse(string.Format("Payment linked successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                ActionLogger.Logger.WriteLog("DoLinkPolicyService success ", true);
            }
            catch (Exception ex)
            {
                jres = new JSONResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                ActionLogger.Logger.WriteLog("DoLinkPolicyService failure " + ex.Message, true);
            }
            return jres;
        }


        public JSONResponse DoLinkPolicyService(Guid LicenseeId, Guid PendingPolicyId, Guid ClientId, Guid activePolicyId, Guid CurrentUser, List<Guid> PaymentsList, bool IsReverse = false)
        {
            JSONResponse resp = null;
            try
            {
                ActionLogger.Logger.WriteLog("DoLinkPolicyService request:", true);

               LinkPaymentPolicies.DoLinkPolicy(LicenseeId, PendingPolicyId, ClientId, activePolicyId, PaymentsList, CurrentUser, IsReverse,false);
                resp = new JSONResponse(string.Format("Payments validated successfully for linking"), Convert.ToInt16(ResponseCodes.Success), "");
            }
            catch (Exception ex)
            {
                resp = new JSONResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                ActionLogger.Logger.WriteLog("DoLinkPolicyService failure " + ex.Message, true);
            }
            return resp;
        }
        public LinkValidationResponse ValidatePaymentsService(Guid LicenseeId, Guid PendingPolicyId, Guid activePolicyId, List<Guid> PaymentsList)
        {
            LinkValidationResponse resp = null;
            try
            {
                bool isResponseReqdOnFalse = false;
                string message = LinkPaymentPolicies.ValidatePaymentsForLinking(LicenseeId, PendingPolicyId, activePolicyId, PaymentsList, out isResponseReqdOnFalse);
                resp = new LinkValidationResponse(string.Format("Payments validated successfully for linking"), Convert.ToInt16(ResponseCodes.Success), "");
                resp.StringValue = message;
                resp.IsResponseNeededOnFalse = isResponseReqdOnFalse;
            }
            catch (Exception ex)
            {
                resp = new LinkValidationResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                ActionLogger.Logger.WriteLog("ValidatePaymentsForLinking failure " + ex.Message, true);
            }
            return resp;
        }
        public JSONResponse MakePolicyActiveService(Guid PolicyId, Guid ClientId,Guid loggedInUserId)
        {

            JSONResponse jres = null;
            ActionLogger.Logger.WriteLog("MakePolicyActiveService request: PolicyId" + PolicyId + ", ClientId: " + ClientId+ ", loggedInUserId:"+loggedInUserId, true);
            try
            {
                LinkPaymentPolicies.MakePolicyActive(PolicyId, ClientId);
                Policy.ModifyPolicyLastUpdated(PolicyId, loggedInUserId);
                jres = new JSONResponse(string.Format("Policy activated successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                ActionLogger.Logger.WriteLog("MakePolicyActiveService success ", true);
            }
            catch (Exception ex)
            {
                jres = new JSONResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                ActionLogger.Logger.WriteLog("MakePolicyActiveService failure " + ex.Message, true);
            }
            return jres;
        }

        public PolicyBoolResponse ScheduleMatchesService(Guid EntryId, Guid ActivePolicyId)
        {
            PolicyBoolResponse jres = null;
            ActionLogger.Logger.WriteLog("ScheduleMatchesService request: Policyid -" + ActivePolicyId + ", entryID: " + EntryId, true);
            try
            {
                bool res = LinkPaymentPolicies.ScheduleMatches(EntryId, ActivePolicyId);
                jres = new PolicyBoolResponse(string.Format("Data found successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                jres.BoolFlag = res;
                ActionLogger.Logger.WriteLog("ScheduleMatchesService success ", true);
            }
            catch (Exception ex)
            {
                jres = new PolicyBoolResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                ActionLogger.Logger.WriteLog("ScheduleMatchesService failure " + ex.Message, true);
            }
            return jres;
        }


    }
}