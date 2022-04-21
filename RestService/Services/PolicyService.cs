using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ServiceModel;
using MyAgencyVault.BusinessLibrary;
using System.ServiceModel.Web;
using System.Data;
using System.Collections.ObjectModel;
using MyAgencyVault.WcfService.Library.Response;
using MyAgencyVault.BusinessLibrary.BusinessObjects;
using System.ServiceModel.Activation;

namespace MyAgencyVault.WcfService
{
    [ServiceContract]
    interface IPolicyService
    {
        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        PolicyListDetailResponse GetPoliciesForWebService(Guid clientId, Guid licenseeId, Guid userID, int role, bool isHouseAccount, bool isAdmin, ListParams listParams);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        PolicyListDetailResponse GetReplacedPolicyListService(Guid clientId, Guid licenseeId, Guid loggedInUserID, Guid? policyID);
        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        checkNamedScheduleExistResponse CheckNamedscheduleExist(Guid licenseeId);
        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        PolicyPMCResponse DeletePolicyBasedOnIdService(Guid policyId, bool isForcefullyDeleted = false, bool isDeleteClient = false);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        JSONResponse SavePolicyService(PolicyDetailsData PolicyDetails, PolicyDetailsData ReplacedPolicy, string strRenewal, string strCoverageNickName, PolicyScheduleDetails policyScheduleDetails);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        PolicySmartfieldDetailsResponse GetPolicySmartFieldDetailsService(Guid policyId);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        JSONResponse AddUpdatePolicySmartFieldsService(PolicyLearnedFieldData PolicyLearnFields, Guid policyId);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        FollowUpPolicyObjectResponse GetFollowupPolicyDetailsService(Guid policyId);


        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        PolicyImportResponse ImportPolicyService(string policiesData, Guid licenseeId);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        PolicyListDetailResponse GetAllSearchedPolicies(PolicySearched searchingData, ListParams listParams);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        JSONResponse UploadPolicyNotesFile(Guid policyId, string fileName, Guid uploadedBy, string uploadedFileName);
        /*
        #region Incoming Schedule
        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        JSONResponse AddUpdatePolicyIncomingScheduleService(PolicyScheduleDetails policyScheduleDetails);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        JSONResponse DeleteIncomingScheduleService(PolicyToolIncommingShedule PInc);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        PolicyIncomingScheduleResponse GetPolicyIncomingScheduleService();

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        PolicyIncomingScheduleResponse GetPolicyIncomingSchedulePolicyWiseService(Guid PolicyId);
        #endregion


        #region Outgoing Schedule
        //[OperationContract] - Not in use 
        //[WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        //JSONResponse AddUpdateOutgoingScheduleService(PolicyOutgoingSchedule OutgoingSchedule);
        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        JSONResponse AddUpdateOutgoingScheduleService(List<OutGoingPayment> outgoingSchedule);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        JSONResponse DeleteOutgoingScheduleService(OutGoingPayment OutGoingSchedule);



        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        PolicyOutgoingScheduleListResponse GetOutgoingScheduleByLicenseeService(Guid licenseeId);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        GetHouseAccountResponse GetHouseAccountdetailsService(Guid licenseeId);




        #endregion


        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        PolicySearchResponse GetAllSearchedPoliciesService(string StrClient, string StrInsured, string Policynumber, string Carrier, string Payor, Guid UserID, UserRole Role, Guid LicenseID);


        //[OperationContract]
        //[WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json,  BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        //void UpdateRPolicyStatusService(PolicyDetailsData policy);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        JSONResponse UpdatePolicySettingService(PolicyDetailsData policy);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        JSONResponse DeletePolicyService(PolicyDetailsData policy);


        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        PolicyTrackResponse IsTrackPaymentCheckedService(Guid PolicyId);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        PolicyPMCResponse GetPolicyStatusIDService(Guid PolicyId);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        PolicyBatchResponse GenerateBatchService(PolicyDetailsData Policy);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        PolicyStmtResponse GenerateStatmentService(Guid BatchId, Guid PayorId, decimal PaymentRecived, Guid CreatedBy);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        JSONResponse AddUpdatePolicyHistoryService(Guid PolicyId);


        //[OperationContract]
        //[WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json,  BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        //PolicyDetailsData GetPolicyHistoryIdWiseService(Guid PolicyId);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        JSONResponse DeletePolicyHistoryService(PolicyDetailsData Policy);

        //[OperationContract]
        //[WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json,  BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        //void DeletePolicyHistoryPermanetByIdService(PolicyDetailsData _Policy);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        PolicyBoolResponse CheckForPolicyPaymentExistsService(Guid PolicyId);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        PolicyListResponse GetpoliciesDataService(Dictionary<string, object> parameters);


        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        JSONResponse UpdatePolicyTermDateService(Guid PolicyID, string TermDateString);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        PolicyPMCResponse CalculatePMCService(Guid PolicyID);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        PolicyPMCResponse CalculatePACService(Guid PolicyId);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        PolicyDetailsData GetPolicySettingService(Guid PolicyID);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        PolicyDateResponse GetFollowUpDateService(Guid PolicyID);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        PolicyStringResponse GetPolicyProductTypeService(Guid PolicyID, Guid PayorID, Guid CarrierID, Guid CoverageID);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        PolicyListResponse GetPolicyClientWiseService(Guid LicenseeId, Guid ClientId);
        */
        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        PolicyObjectResponse GetPolicyDetailsService(Guid licenseeId, Guid clientId, Guid policyId);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        PolicyObjectResponse GetPolicyTypeService(Guid ClientId, string CoverageId, Guid LicenseeId, string EffectiveDate, Guid PolicyId);

        /*
        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        PolicyStringResponse GetPolicyUniqueKeyNameService();

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        PolicyStringResponse CompareExcelService(DataTable dt);

       

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        PolicyListDetailResponse GetPolicyListDetailService(Guid clientId, Guid LicenseeId, ListParams listparam);

        #region Last searched policy
        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        JSONResponse AddLastViewPolicyService(LastViewPolicy lastviewpolicy);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        LastViewedPolicyResponse GetLastViewPolicyService(Guid userCredentialID);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        LastViewedPolicyResponse GetAllLastViewPolicyService();

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        JSONResponse DeleteRecordCredientialWiseService(Guid userCredentialID);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        JSONResponse SaveLastViewedClientsService(List<LastViewPolicy> lastViewedClients, Guid UserId);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        LastViewedPolicyResponse GetLastViewedClientsService(Guid UserID);


        


        #endregion*/
        //[OperationContract]
        //[WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        //PolicyOutgoingScheduleResponse GetOutgoingScheduleByPolicyService(Guid PolicyID);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        JSONResponse AddUpdatePolicyNotesService(Guid policyId, string note, Guid noteId, Guid? createdBy, Guid? modifiedBy);


        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        GetPolicyNotesResponse GetListOfPolicyNotesService(Guid policyId);


        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        JSONResponse DeletePolicyNotesService(Guid policyId, Guid noteId);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        JSONResponse DeletePolicyNotesFileService(Guid policyId, Guid noteId, string fileName);

    }


    public partial class MavService : IPolicyService
    {

        /// <summary>
        /// Author: Jyotisna
        /// Date: Feb 05, 2019
        /// Purpose: Fetch list of policies for landing screen in web
        /// </summary>
        /// <param name="clientId"></param>
        /// <param name="licenseeId"></param>
        /// <param name="listParams"></param>
        /// <returns></returns>
        public PolicyListDetailResponse GetPoliciesForWebService(Guid clientId, Guid LicenseeId, Guid UserID, int role, bool isHouseAccount, bool isAdmin, ListParams listparam)
        {
            PolicyListDetailResponse response = null;
            ActionLogger.Logger.WriteLog("GetPolicyDetailList request:", true);
            try
            {
                //List<PolicyListObject> list = Policy.GetlistofPolicies(clientId, licenseeId, listParams, out PoliciesCount recordCount);
                List<PolicyListObject> list = Policy.GetlistofPolicies(clientId, LicenseeId, UserID, role, isHouseAccount, isAdmin, listparam, out PoliciesCount recordCount);
                if (list != null && list.Count > 0)
                {
                    response = new PolicyListDetailResponse(_resourceManager.GetString("PolicyListSucess"), (int)ResponseCodes.Success, "");
                    response.PolicyList = list;
                    ActionLogger.Logger.WriteLog("GetlistofPolicies: list fetch successfully with clientId:" + clientId + ", LicenseeId:" + "" + LicenseeId, true);
                }
                else
                {
                    response = new PolicyListDetailResponse(_resourceManager.GetString("PolicyListFailure"), (int)ResponseCodes.RecordNotFound, "");
                    ActionLogger.Logger.WriteLog("GetlistofPolicies: No records found of policies  with clientId:" + clientId + ", LicenseeID: " + "LicenseeId:" + "" + LicenseeId, true);
                }
                response.PoliciesCount = recordCount;
            }
            catch (Exception ex)
            {
                ActionLogger.Logger.WriteLog("GetpolicyListDetail Failure:" + ex.Message, true);
                response = new PolicyListDetailResponse(_resourceManager.GetString("PolicyListFailure"), (int)ResponseCodes.Failure, "");
            }
            return response;
        }

        public PolicyListDetailResponse GetAllSearchedPolicies(PolicySearched searchingData, ListParams listParams)
        {
            PolicyListDetailResponse response = null;
            ActionLogger.Logger.WriteLog("GetAllSearchedPolicies request: " + searchingData.ToStringDump(), true);
            try
            {
                List<PolicyListObject> policyList = Policy.GetAllSearchedPoliciesList(searchingData, listParams, out string recordCount);
                response = new PolicyListDetailResponse(_resourceManager.GetString("PolicyListSucess"), Convert.ToInt16(ResponseCodes.Success), "");
                response.PolicyList = policyList;
                response.RecordCount = recordCount;
                ActionLogger.Logger.WriteLog("GetAllSearchedPolicies:All policies are fetched successfully!!", true);
            }
            catch (Exception ex)
            {
                response = new PolicyListDetailResponse(_resourceManager.GetString("PolicyListSucess"), Convert.ToInt16(ResponseCodes.Success), "");
                ActionLogger.Logger.WriteLog("GetAllSearchedPolicies:Error occured while processing Advance searched policy finding:Exception" + ex.Message, true);
            }
            return response;
        }

        /// <summary>
        /// Author:Ankit Khandelwal
        /// Purpose:policyDeleted based on policyId and clientDeleted
        /// Date:Sep 17,2018
        /// </summary>
        /// <param name="policyId"></param>
        /// <param name="isForcefullyDeleted"></param>
        /// <returns></returns>
        public PolicyPMCResponse DeletePolicyBasedOnIdService(Guid policyId, bool isForcefullyDeleted = false, bool isDeleteClient = false)
        {
            PolicyPMCResponse jres = null;
            ActionLogger.Logger.WriteLog("DeletePolicyBasedOnIdService request: " + policyId + ", isForcefullyDeleted" + isForcefullyDeleted + ", isDeleteClient: " + isDeleteClient, true);
            try
            {
                Policy.DeletePolicyBasedOnId(policyId, isForcefullyDeleted, isDeleteClient, out int response, out int policyCount);
                if (response == 1)
                {
                    jres = new PolicyPMCResponse(_resourceManager.GetString("Policyalreadydeleted"), Convert.ToInt16(ResponseCodes.Success), "");
                    ActionLogger.Logger.WriteLog("policy already deleted PolicyId" + policyId, true);
                }
                else if (response == 0)
                {
                    jres = new PolicyPMCResponse(_resourceManager.GetString("Policynotfound"), Convert.ToInt16(ResponseCodes.Success), "");
                    ActionLogger.Logger.WriteLog("Policy not found with this policyId :" + policyId, true);
                }
                else if (response == 2)
                {
                    jres = new PolicyPMCResponse(_resourceManager.GetString("OutgoingPaymentassociates"), Convert.ToInt16(ResponseCodes.OutgoingPaymentExist), "");
                    ActionLogger.Logger.WriteLog("Policy have payments so policy Cant be deleted policyId:" + policyId, true);
                }
                else if (response == 3)
                {
                    jres = new PolicyPMCResponse(_resourceManager.GetString("PolicyDeleted"), Convert.ToInt16(ResponseCodes.Success), "");
                    ActionLogger.Logger.WriteLog("Policy deleted successfully  policyId:" + policyId, true);
                }
                else if (response == 4)
                {
                    jres = new PolicyPMCResponse(_resourceManager.GetString("PolicyDeletedwithClient"), Convert.ToInt16(ResponseCodes.NoIssueWithEntity), "");
                    ActionLogger.Logger.WriteLog("Policy can be deleted - no issue" + policyId, true);
                }
                jres.NumberValue = policyCount;
            }
            catch (Exception ex)
            {
                jres = new PolicyPMCResponse(_resourceManager.GetString("PolicyDeleteFailure"), Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                ActionLogger.Logger.WriteLog("DeletePolicyBasedOnIdService failure ", true);
            }
            return jres;
        }

        /* #region Outgoing Schedule 
         public JSONResponse AddUpdateOutgoingScheduleService(List<OutGoingPayment> outgoingSchedule)
         {
             JSONResponse jres = null;
             ActionLogger.Logger.WriteLog("AddUpdateOutgoingShedule request: " + outgoingSchedule.ToStringDump(), true);
             try
             {
                 OutGoingPayment.AddUpdate(outgoingSchedule);
                 // OutgoingSchedule.AddUpdatePolicyOutgoingSchedule(outgoingSchedule);
                 jres = new JSONResponse(string.Format("Policy outgoing schedule saved successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                 ActionLogger.Logger.WriteLog("AddUpdateOutgoingShedule success ", true);
             }
             catch (Exception ex)
             {
                 jres = new JSONResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                 ActionLogger.Logger.WriteLog("AddUpdateOutgoingShedule failure ", true);
             }
             return jres;
         }

         public JSONResponse DeleteOutgoingScheduleService(OutGoingPayment outgoingSchedule)
         {
             JSONResponse jres = null;
             ActionLogger.Logger.WriteLog("DeleteOutgoingScheduleService request: " + outgoingSchedule.ToStringDump(), true);
             try
             {
                 outgoingSchedule.Delete();
                 jres = new JSONResponse(string.Format("Policy outgoing schedule deleted successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                 ActionLogger.Logger.WriteLog("DeleteOutgoingScheduleService success ", true);
             }
             catch (Exception ex)
             {
                 jres = new JSONResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                 ActionLogger.Logger.WriteLog("DeleteOutgoingScheduleService failure ", true);
             }
             return jres;
         }
*/
        //public PolicyOutgoingScheduleResponse GetOutgoingScheduleByPolicyService(Guid PolicyID)
        //{
        //    PolicyOutgoingScheduleResponse jres = null;
        //    ActionLogger.Logger.WriteLog("GetOutgoingScheduleByPolicyService request: " + PolicyID.ToStringDump(), true);
        //    try
        //    {

        //       IList<PolicyOutgoingSchedules> schedule = OutgoingSchedule.GetPolicyOutgoingScheduleById(PolicyID, out int isrecordfound);
        //        jres = new PolicyOutgoingScheduleResponse(string.Format("Policy outgoing schedule found successfully"), Convert.ToInt16(ResponseCodes.Success), "");
        //        jres.TotalRecords = schedule;
        //       jres.TotalLength = schedule.Count();
        //        ActionLogger.Logger.WriteLog("GetOutgoingScheduleByPolicyService success ", true);
        //    }
        //    catch (Exception ex)
        //    {
        //        jres = new PolicyOutgoingScheduleResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
        //        ActionLogger.Logger.WriteLog("GetOutgoingScheduleByPolicyService failure ", true);
        //    }
        //    return jres;
        //}
        /*
         #endregion
         #region Incoming Schedule
         public JSONResponse AddUpdatePolicyIncomingScheduleService(PolicyScheduleDetails policyScheduleDetails)
         {
             JSONResponse jres = null;
             ActionLogger.Logger.WriteLog("AddUpdatePolicyIncomingScheduleService request: " + policyScheduleDetails.ToStringDump(), true);
             try
             {
                 policyScheduleDetails.policyIncomingSchedule.AddUpdate(policyScheduleDetails);
                 jres = new JSONResponse(string.Format("Policy incoming schedule saved successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                 ActionLogger.Logger.WriteLog("AddUpdatePolicyIncomingScheduleService success ", true);
             }
             catch (Exception ex)
             {
                 jres = new JSONResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                 ActionLogger.Logger.WriteLog("AddUpdatePolicyIncomingScheduleService failure ", true);
             }
             return jres;
         }

         public JSONResponse DeleteIncomingScheduleService(PolicyToolIncommingShedule PInc)
         {
             JSONResponse jres = null;
             ActionLogger.Logger.WriteLog("DeleteIncomingScheduleService request: " + PInc.ToStringDump(), true);
             try
             {
                 PInc.Delete();
                 jres = new JSONResponse(string.Format("Policy incoming schedule deleted successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                 ActionLogger.Logger.WriteLog("DeleteIncomingScheduleService success ", true);
             }
             catch (Exception ex)
             {
                 jres = new JSONResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                 ActionLogger.Logger.WriteLog("DeleteIncomingScheduleService failure ", true);
             }
             return jres;
         }


         public PolicyIncomingScheduleResponse GetPolicyIncomingScheduleService()
         {
             ActionLogger.Logger.WriteLog("GetPolicyIncomingScheduleService request: ", true);
             PolicyIncomingScheduleResponse jres = null;
             try
             {
                 List<PolicyToolIncommingShedule> obj = PolicyToolIncommingShedule.GetPolicyToolIncommingSheduleList();
                 jres = new PolicyIncomingScheduleResponse(string.Format("Policy incoming schedule found successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                 jres.Policylist = obj;
                 ActionLogger.Logger.WriteLog("GetPolicyIncomingScheduleService success ", true);
             }
             catch (Exception ex)
             {
                 jres = new PolicyIncomingScheduleResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                 ActionLogger.Logger.WriteLog("GetPolicyIncomingScheduleService failure ", true);
             }
             return jres;
         }


         public PolicyIncomingScheduleResponse GetPolicyIncomingSchedulePolicyWiseService(Guid PolicyId)
         {
             ActionLogger.Logger.WriteLog("GetPolicyIncomingSchedulePolicyWiseService request: " + PolicyId.ToString(), true);
             PolicyIncomingScheduleResponse jres = null;
             try
             {
                 PolicyToolIncommingShedule obj = PolicyToolIncommingShedule.GetPolicyToolIncommingSheduleListPolicyWise(PolicyId);
                 jres = new PolicyIncomingScheduleResponse(string.Format("Policy incoming schedule found successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                 jres.PolicyIncomingScheduleObject = obj;
                 ActionLogger.Logger.WriteLog("GetPolicyIncomingSchedulePolicyWiseService success ", true);
             }
             catch (Exception ex)
             {
                 jres = new PolicyIncomingScheduleResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                 ActionLogger.Logger.WriteLog("GetPolicyIncomingSchedulePolicyWiseService failure ", true);
             }
             return jres;
         }

         #endregion


         #region policy manager tab
         */
        /// <summary>
        /// Author:Ankit Khandelwal
        /// Purpose:Get the list of policies based on ClientId and LicenseeId
        /// Date:Sep 12,2018
        /// </summary>
        /// <param name="clientId"></param>
        /// <param name="LicenseeId"></param>
        /// <param name="listparam"></param>
        /// <returns></returns>
        public PolicyListDetailResponse GetPolicyListDetailService(Guid clientId, Guid LicenseeId, Guid UserID, int role, bool isHouseAccount, bool isAdmin, ListParams listparam)
        {
            PolicyListDetailResponse response = null;
            ActionLogger.Logger.WriteLog("GetPolicyDetailList request:", true);
            try
            {
                List<PolicyListObject> list = Policy.GetlistofPolicies(clientId, LicenseeId, UserID, role, isHouseAccount, isAdmin, listparam, out PoliciesCount recordCount);
                if (list != null && list.Count > 0)
                {

                    response = new PolicyListDetailResponse(_resourceManager.GetString("PolicyListSucess"), (int)ResponseCodes.Success, "test");
                    response.PolicyList = list;
                    response.PoliciesCount = recordCount;
                    ActionLogger.Logger.WriteLog("GetlistofPolicies: list fetch successfully with clientId:" + clientId + " " + "LicenseeId:" + "" + LicenseeId, true);
                }
                else
                {
                    response = new PolicyListDetailResponse(_resourceManager.GetString("PolicyListFailure"), (int)ResponseCodes.RecordNotFound, "test");
                    ActionLogger.Logger.WriteLog("GetlistofPolicies: Error while  getting list of policies  with clientId:" + clientId + " " + "LicenseeId:" + "" + LicenseeId, true);
                }
            }
            catch (Exception ex)
            {
                ActionLogger.Logger.WriteLog("GetpolicyListDetail Failure:" + ex.Message, true);
                response = new PolicyListDetailResponse(_resourceManager.GetString("PolicyListFailure"), (int)ResponseCodes.Failure, "");
            }
            return response;
        }
        /// <summary>
        /// Author:Ankit Khandelwal
        /// Purpose:Get the list of policies based on policyId and loggedInUserID
        /// Date:Sep 12,2018
        /// </summary>
        /// <param name="policyId"></param>
        /// <param name="loggedInUserID"></param>
        /// <returns></returns>
        public PolicyListDetailResponse GetReplacedPolicyListService(Guid clientId, Guid licenseeId, Guid loggedInUserID, Guid? policyId)
        {
            PolicyListDetailResponse response = null;
            ActionLogger.Logger.WriteLog("GetPolicyDetailList request: policyID - " + clientId + ", loggedInUser: " + loggedInUserID, true);
            try
            {
                List<PolicyListObject> list = Policy.GetRplacedPoliciesList(clientId, licenseeId, loggedInUserID, policyId);
                if (list != null && list.Count > 0)
                {

                    response = new PolicyListDetailResponse(_resourceManager.GetString("PolicyListSucess"), (int)ResponseCodes.Success, "test");
                    response.PolicyList = list;
                    /*esponse.PoliciesCount = recordCount;*/
                    ActionLogger.Logger.WriteLog("GetlistofPolicies: list fetch successfully with clientId:" + clientId + " " + "loggedInUserID:" + "" + loggedInUserID, true);
                }
                else
                {
                    response = new PolicyListDetailResponse(_resourceManager.GetString("PolicyListFailure"), (int)ResponseCodes.RecordNotFound, "test");
                    ActionLogger.Logger.WriteLog("GetlistofPolicies: Error while  getting list of policies  with clientId:" + clientId + " " + "loggedInUserID:" + "" + loggedInUserID, true);
                }
            }
            catch (Exception ex)
            {
                ActionLogger.Logger.WriteLog("GetpolicyListDetail Failure:" + ex.Message, true);
                response = new PolicyListDetailResponse(_resourceManager.GetString("PolicyListFailure"), (int)ResponseCodes.Failure, "");
            }
            return response;
        }

        public checkNamedScheduleExistResponse CheckNamedscheduleExist(Guid licenseeId)
        {
            checkNamedScheduleExistResponse response = null;
            ActionLogger.Logger.WriteLog("checkNamedscheduleExist request: licenseeId: " + licenseeId, true);
            try
            {
                bool isExist = Policy.CheckNamedscheduleExist(licenseeId);
                response = new checkNamedScheduleExistResponse(_resourceManager.GetString("PolicyListSucess"), (int)ResponseCodes.Success, "test");
                response.isExist = isExist;
                ActionLogger.Logger.WriteLog("checkNamedscheduleExist : list fetch successfully with licenseeId:" + licenseeId, true);
            }
            catch (Exception ex)
            {
                ActionLogger.Logger.WriteLog("checkNamedscheduleExist  Failure:" + ex.Message, true);
                response = new checkNamedScheduleExistResponse(_resourceManager.GetString("PolicyListFailure"), (int)ResponseCodes.Failure, "");
            }
            return response;
        }
        /// <summary>
        /// Author:Ankit Khandelwal
        /// Purpose:Add/update policies details
        /// Date:Sep 16,2018
        /// </summary>
        /// <param name="PolicyDetails"></param>
        /// <param name="ReplacedPolicy"></param>
        /// <param name="strRenewal"></param>
        /// <param name="strCoverageNickName"></param>
        /// <param name="policyScheduleDetails"></param>
        /// <returns></returns>
        public JSONResponse SavePolicyService(PolicyDetailsData PolicyDetails, PolicyDetailsData ReplacedPolicy, string strRenewal, string strCoverageNickName, PolicyScheduleDetails policyScheduleDetails)
        {
            JSONResponse jres = null;
            ActionLogger.Logger.WriteLog("SavePolicyService request: " + PolicyDetails.ToStringDump(), true);
            try
            {
                PolicySavedStatus status = Policy.SavePolicy(PolicyDetails, ReplacedPolicy, strRenewal, strCoverageNickName, policyScheduleDetails);
                Policy.ModifyPolicyLastUpdated(PolicyDetails.PolicyId, PolicyDetails.LastModifiedBy);
                ActionLogger.Logger.WriteLog("SavePolicyService success: IsError " + status.IsError, true);
                if (status.IsError)
                {
                    jres = new JSONResponse(_resourceManager.GetString("PolicyUpdateFailure"), Convert.ToInt16(ResponseCodes.Failure), status.ErrorMessage);
                }
                else
                {
                    jres = new JSONResponse(string.Format("Policy saved successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                }
            }
            catch (Exception ex)
            {
                jres = new JSONResponse(_resourceManager.GetString("PolicyUpdateFailure"), Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                ActionLogger.Logger.WriteLog("SavePolicyService failure: " + ex.Message, true);
            }
            return jres;
        }


        /// <summary>
        /// Author:Ankit Khandelwal
        /// Purpose:Get policy detail based on policy and ClientName
        /// Date:Sep 17,2018
        /// </summary>
        /// <param name="licenseeId"></param>
        /// <param name="clientId"></param>
        /// <param name="policyId"></param>
        /// <returns></returns>
        public PolicyObjectResponse GetPolicyDetailsService(Guid licenseeId, Guid clientId, Guid policyId)
        {
            PolicyObjectResponse jres = null;
            ActionLogger.Logger.WriteLog("GetPolicyDetailsService request: LicenseeId" + licenseeId + ", Client: " + clientId, true);
            try
            {
                PolicyDetailsData lst = Policy.GetPolicyDetailsClientWise(licenseeId, clientId, policyId, out PolicyToolIncommingShedule policyIncomingSchedule, out IList<PolicyOutgoingSchedules> schedule, out bool isScheduleExist);

                jres = new PolicyObjectResponse(string.Format("Data found successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                jres.PolicyObject = lst;
                jres.PolicyOutgoingSchedules = schedule;
                jres.PolicyIncomingSchedule = policyIncomingSchedule;
                jres.isScheduleExist = isScheduleExist;
                ActionLogger.Logger.WriteLog("GetPolicyDetailsService success ", true);
            }
            catch (Exception ex)
            {
                jres = new PolicyObjectResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                ActionLogger.Logger.WriteLog("GetPolicyDetailsService failure ", true);
            }
            return jres;
        }

        /// <summary>
        /// Author:Sahil
        /// Purpose:Get policy typel based on clientId,coverageId,licenseeId,effectivedate and policyId
        /// Date:Sep 16,2021
        /// </summary>
        /// <param name="ClientId"></param>
        /// <param name="CoverageId"></param>
        /// <param name="LicenseeId"></param>
        /// <param name="Effectivedate"></param>
        /// <param name="PolicyId"></param>
        /// <returns></returns>
        public PolicyObjectResponse GetPolicyTypeService(Guid ClientId, string strCoverageId, Guid LicenseeId, string Effectivedate, Guid PolicyId)
        {
            PolicyObjectResponse jres = null;
            ActionLogger.Logger.WriteLog("GetPolicyType request: ClientId" + ClientId + ", strCoverageId: " + strCoverageId + ", LicenseeId: " + LicenseeId + ", Effectivedate: " + Effectivedate + ", PolicyId: " + PolicyId, true);
            try
            {
                DateTime? dtEffectivedate = null;
                Guid? CoverageId = null;

                if (!string.IsNullOrEmpty(Effectivedate))
                {
                    dtEffectivedate = Convert.ToDateTime(Effectivedate);
                }
                ActionLogger.Logger.WriteLog("dtEffectivedate " + dtEffectivedate, true);

                if (!string.IsNullOrEmpty(strCoverageId))
                {
                    CoverageId = Guid.Parse(strCoverageId);
                }
                ActionLogger.Logger.WriteLog("CoverageId " + CoverageId, true);

                string PolicyType = Policy.calculatePolicyType(dtEffectivedate, ClientId, LicenseeId, PolicyId, CoverageId);

                jres = new PolicyObjectResponse(string.Format("Data found successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                jres.PolicyType = PolicyType;
                ActionLogger.Logger.WriteLog("GetPolicyType success ", true);
            }
            catch (Exception ex)
            {
                jres = new PolicyObjectResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                ActionLogger.Logger.WriteLog("GetPolicyType failure ", true);
            }
            return jres;
        }

        /// <summary>
        /// CreatedBy :Ankit Khandelwal
        /// CreatedOn:27-03-2019
        /// Purpose:getting details for follow up issues
        /// </summary>
        /// <param name="PolicyId"></param>
        /// <returns></returns>

        public FollowUpPolicyObjectResponse GetFollowupPolicyDetailsService(Guid policyId)
        {
            FollowUpPolicyObjectResponse jres = null;
            ActionLogger.Logger.WriteLog("GetPolicyDetailsService request: policyId" + policyId, true);
            try
            {
                FollowUpIssue lst = Policy.GetFollowupPolicyDetails(policyId);

                jres = new FollowUpPolicyObjectResponse(string.Format("Data found successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                jres.PolicyObject = lst;
                ActionLogger.Logger.WriteLog("GetPolicyDetailsService success ", true);
            }
            catch (Exception ex)
            {
                jres = new FollowUpPolicyObjectResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                ActionLogger.Logger.WriteLog("GetPolicyDetailsService failure ", true);
            }
            return jres;
        }
        #region policy Notes
        /// <summary>
        /// Author:Ankit khandelwal
        /// CreatedOn:Sep 18,2018
        /// Purpose:Getting the List of notes and record Count
        /// </summary>
        /// <param name="policyId"></param>
        /// <returns></returns>
        /// 
        public GetPolicyNotesResponse GetListOfPolicyNotesService(Guid policyId)
        {
            ActionLogger.Logger.WriteLog("GetListOfPolicyNotesService:processing Starts with policyId:" + policyId, true);
            GetPolicyNotesResponse response = null;
            try
            {
                List<PolicyNotes> policyNote = PolicyNotes.GetNotes(policyId, out int isErrorOccur, out List<UploadedFile> uploadedFileList);
                if (isErrorOccur == 0)
                {
                    response = new GetPolicyNotesResponse(_resourceManager.GetString("PolicyNotesList"), (int)ResponseCodes.Success, "");
                    ActionLogger.Logger.WriteLog("GetListOfPolicyNotesService:Policy notes list fetch successfully with policyId:" + policyId, true);
                    response.PolicyNotesList = policyNote;
                    response.UploadedFileList = uploadedFileList;
                    response.policyNotesCount = policyNote.Count();

                }
                else
                {
                    response = new GetPolicyNotesResponse(_resourceManager.GetString("PolicyListRecordNotFound"), (int)ResponseCodes.RecordNotFound, "");
                    ActionLogger.Logger.WriteLog("GetListOfPolicyNotesService:no record found with policyId:" + policyId, true);
                }
            }
            catch (Exception ex)
            {
                response = new GetPolicyNotesResponse(_resourceManager.GetString("PolicyListNotesFailure"), (int)ResponseCodes.Failure, "");
                ActionLogger.Logger.WriteLog("GetListOfPolicyNotesService:Error occur with policyId:" + policyId + " " + " Exception:" + ex.Message, true);
            }
            ActionLogger.Logger.WriteLog("GetListOfPolicyNotesService:processing Ends with policyId:" + policyId, true);
            return response;

        }
        /// <summary>
        /// Author:Ankit khandelwal
        /// CreatedOn:Oct,23,2018
        /// Purpose:Delete policy notes based on policyId and NoteId
        /// </summary>
        /// <param name="policyId"></param>
        /// <param name="noteId"></param>
        /// <returns></returns>
        public JSONResponse DeletePolicyNotesService(Guid policyId, Guid noteId)
        {
            JSONResponse response = null;
            try
            {
                ActionLogger.Logger.WriteLog("DeletePolicyNotesService:processing Starts with noteId:" + noteId + " " + "policyId" + policyId, true);
                int policyDeleteStatus = PolicyNotes.DeleteNoteByPolicyId(policyId, noteId);
                if (policyDeleteStatus == 1)
                {
                    response = new JSONResponse(_resourceManager.GetString("PolicyNotesDelete"), (int)ResponseCodes.Success, "");
                    ActionLogger.Logger.WriteLog("DeletePolicyNotesService:policy Notes deleted successfully noteId:" + noteId + " " + "policyId" + policyId, true);
                }
                else
                {
                    response = new JSONResponse(_resourceManager.GetString("PolicyNotesNOtFound"), (int)ResponseCodes.RecordNotFound, "");
                    ActionLogger.Logger.WriteLog("DeletePolicyNotesService:Policy notes doesn't due to polic details not found:" + noteId + " " + "policyId" + policyId, true);
                }
            }
            catch (Exception ex)
            {
                response = new JSONResponse(_resourceManager.GetString("PolicyNotesFailure"), (int)ResponseCodes.Success, "");
                ActionLogger.Logger.WriteLog("DeletePolicyNotesService:Policy notes failure occurs while fetching details noteId:" + noteId + " " + "policyId" + policyId + " " + ex.Message, true);
            }
            ActionLogger.Logger.WriteLog("DeletePolicyNotesService:processing ends with noteId:" + noteId + " " + "policyId" + policyId, true);
            return response;
        }
        /// <summary>
        /// Author:Ankit khandelwal
        /// CreatedOn:Oct,23,2018
        /// Purpose:Delete policy notes based on policyId and NoteId
        /// </summary>
        /// <param name="policyId"></param>
        /// <param name="noteId"></param>
        /// <returns></returns>
        public JSONResponse DeletePolicyNotesFileService(Guid policyId, Guid noteId,string fileName)
        {
            JSONResponse response = null;
            try
            {
                ActionLogger.Logger.WriteLog("DeletePolicyNotesFileService:Processing Starts for delete file with noteId:" + noteId + " " + "policyId" + policyId, true);
                int policyDeleteStatus = PolicyNotes.DeletPolicyNoteFile(policyId, noteId, fileName,true);
                if (policyDeleteStatus == 1)
                {
                    response = new JSONResponse(_resourceManager.GetString("PolicyNotesDelete"), (int)ResponseCodes.Success, "");
                    ActionLogger.Logger.WriteLog("DeletePolicyNotesFileService:policy file deleted successfully with noteId:" + noteId + " " + "policyId" + policyId, true);
                }
                else
                {
                    response = new JSONResponse(_resourceManager.GetString("PolicyNotesNOtFound"), (int)ResponseCodes.RecordNotFound, "");
                    ActionLogger.Logger.WriteLog("DeletePolicyNotesFileService:Policy file doesn't deleted due to file not found:" + noteId + " " + "policyId" + policyId, true);
                }
            }
            catch (Exception ex)
            {
                response = new JSONResponse(_resourceManager.GetString("PolicyNotesFailure"), (int)ResponseCodes.Success, "");
                ActionLogger.Logger.WriteLog("DeletePolicyNotesFileService:failure occurs while Policy file delete  with noteId:" + noteId + " " + "policyId" + policyId + " " + ex.Message, true);
            }
            ActionLogger.Logger.WriteLog("DeletePolicyNotesFileService:processing ends with noteId:" + noteId + " " + "policyId" + policyId, true);
            return response;
        }

        /// <summary>
        /// Author:Ankit khandelwal
        /// CreatedOn:Oct,25,2018
        /// Purpose:Add/update policy notes based on policyId and NoteId
        /// </summary>
        /// <param name="policyId"></param>
        /// <param name="note"></param>
        /// <param name="noteId"></param>
        /// <returns></returns>

        public JSONResponse AddUpdatePolicyNotesService(Guid policyId, string note, Guid noteId, Guid? createdBy, Guid? modifiedBy)
        {
            ActionLogger.Logger.WriteLog("AddUpdatePolicyNotesService:processing Starts with policyId:" + policyId, true);
            JSONResponse jres = null;
            try
            {
                PolicyNotes.AddUpdatePolicyNotes(policyId, note, noteId, out int CheckAddupdate, createdBy, modifiedBy);
                if (CheckAddupdate == 1)
                {
                    jres = new JSONResponse(_resourceManager.GetString("AddedPolicyfield"), (int)ResponseCodes.Success, "");
                    ActionLogger.Logger.WriteLog("AddUpdatePolicyNotesService:new notes added with policyId:" + policyId, true);
                }
                else
                {
                    jres = new JSONResponse(_resourceManager.GetString("UpdatePolicyfield"), (int)ResponseCodes.Success, "");
                    ActionLogger.Logger.WriteLog("AddUpdatePolicyNotesService: notes update with policyId:" + policyId, true);

                }
            }
            catch (Exception ex)
            {
                ActionLogger.Logger.WriteLog("AddUpdatePolicyNotesService: Error occur while Add/update  with policyId:" + policyId + " " + "Exception:" + ex.Message, true);
            }
            ActionLogger.Logger.WriteLog("AddUpdatePolicyNotesService:processing completed with policyId:" + policyId, true);
            return jres;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="policyId"></param>
        /// <param name="fileName"></param>
        /// <param name="uploadedBy"></param>
        /// <returns></returns>
        public JSONResponse UploadPolicyNotesFile(Guid policyId, string fileName, Guid uploadedBy,string uploadedFileName)
        {
            ActionLogger.Logger.WriteLog("UploadPolicyNotesFile:processing Starts with policyId:" + policyId, true);
            JSONResponse jres = null;
            try
            {
                PolicyNotes.UploadPolicyNotesFile(policyId, fileName, uploadedBy, uploadedFileName);
                jres = new JSONResponse(_resourceManager.GetString("File added successfully"), (int)ResponseCodes.Success, "");
                ActionLogger.Logger.WriteLog("UploadPolicyNotesFile:new file  uploaded with policyId:" + policyId, true);
            }
            catch (Exception ex)
            {
                ActionLogger.Logger.WriteLog("UploadPolicyNotesFile: Error occur while uploaded  with policyId:" + policyId + " " + "Exception:" + ex.Message, true);
            }
            ActionLogger.Logger.WriteLog("UploadPolicyNotesFile:processing completed with policyId:" + policyId, true);
            return jres;
        }

        #region smart fields
        /// <summary>
        /// Author:Ankit khandelwal
        /// CreatedOn:Oct,25,2018
        /// Purpose:Get the details of smart fields
        /// </summary>
        /// <param name="policyId"></param>
        /// <returns></returns>
        public PolicySmartfieldDetailsResponse GetPolicySmartFieldDetailsService(Guid policyId)
        {
            PolicySmartfieldDetailsResponse jres = null;
            try
            {
                ActionLogger.Logger.WriteLog("GetPolicySmartFieldDetailsService request policyId: " + policyId, true);
                PolicyLearnedFieldData result = PolicyLearnedField.GetPolicyLearnedFieldsOnPOlicy(policyId);
                if (result != null)
                {
                    jres = new PolicySmartfieldDetailsResponse(_resourceManager.GetString("PolicyLearnfieldsSuccess"), Convert.ToInt16(ResponseCodes.Success), "");
                    ActionLogger.Logger.WriteLog("Policy details found successfully with  policyId: " + policyId, true);
                    jres.policyDetails = result;
                }
                else
                {
                    jres = new PolicySmartfieldDetailsResponse(_resourceManager.GetString("PolicyLearnfieldsFailure"), Convert.ToInt16(ResponseCodes.RecordNotFound), "");
                    ActionLogger.Logger.WriteLog("Policy details  not found with policyId: " + policyId, true);
                }

            }
            catch (Exception ex)
            {
                jres = new PolicySmartfieldDetailsResponse(_resourceManager.GetString("PolicyLearnedExecption"), Convert.ToInt16(ResponseCodes.Failure), "");
                ActionLogger.Logger.WriteLog(DateTime.Now.ToString() + " Exception in : GetPolicySmartFieldDetailsService: " + policyId + " " + "Exception:" + ex.Message, true);
            }
            return jres;
        }

        /// <summary>
        /// Author:Ankit khandelwal
        /// CreatedOn:Oct,25,2018
        /// Purpose:Add/update the details of smart fields
        /// </summary>
        /// <param name="PolicyLearnFields"></param>
        /// <param name="policyId"></param>
        /// <returns></returns>
        public JSONResponse AddUpdatePolicySmartFieldsService(PolicyLearnedFieldData PolicyLearnFields, Guid policyId)
        {
            ActionLogger.Logger.WriteLog("AddUpdatePolicySmartFieldsService request policyId: " + policyId, true);
            JSONResponse jres = null;
            try
            {
                bool checkIsSuccessStatus = SaveLearnedFields(PolicyLearnFields, policyId);


                if (checkIsSuccessStatus == true)
                {
                    Policy.ModifyPolicyLastUpdated(PolicyLearnFields.PolicyId, PolicyLearnFields.LastModifiedUserCredentialId);
                    jres = new JSONResponse(_resourceManager.GetString("AddedPolicyfield"), (int)ResponseCodes.Success, "");
                    ActionLogger.Logger.WriteLog("AddUpdatePolicySmartFieldsService :policy details Added successfully with policyId: " + policyId, true);
                }
                else
                {
                    jres = new JSONResponse(_resourceManager.GetString("PolicyLearnfieldsFailure"), (int)ResponseCodes.Failure, "");
                    ActionLogger.Logger.WriteLog("AddUpdatePolicySmartFieldsService :policy details failure  with policyId: " + policyId, true);
                }
            }
            catch (Exception ex)
            {
                jres = new JSONResponse(_resourceManager.GetString(" PolicyLearnedExecption"), (int)ResponseCodes.Failure, "");
                ActionLogger.Logger.WriteLog("AddUpdatePolicySmartFieldsService :Error occured while Add/update details   with policyId: " + policyId + " " + "Exception:" + ex.Message, true);
            }
            return jres;
        }
        public static bool SaveLearnedFields(PolicyLearnedFieldData PolicyLearnFields, Guid policyId)
        {
            Boolean isSuccess = false;
            try
            {
                isSuccess = PolicyLearnedField.AddUpdateLearnedWeb(PolicyLearnFields, policyId/*, out int checkdetails*/);
                // Policy.AddUpdatePolicyHistory(policyId);
                //  PolicyLearnedField.AddUpdateHistoryLearned(policyId);
                LearnedToPolicyPost.AddUpdateLearnedToPolicy(PolicyLearnFields.PolicyId);
            }
            catch (Exception ex)
            {
                ActionLogger.Logger.WriteLog("SaveLearnedFields :Error occured while Add/update details   with policyId: " + policyId + " " + "Exception:" + ex.Message, true);
            }
            return isSuccess;
        }

        #endregion
        #endregion


        public PolicyImportResponse ImportPolicyService(string policiesData, Guid licenseeId)
        {
            PolicyImportResponse jres = null;
            try
            {
                string inRequest = "Incoming table: " + policiesData + ", LicenseeID: " + licenseeId;
                ActionLogger.Logger.WriteLog("********Import Policy Started*****", true);
                ActionLogger.Logger.WriteLog("Import Policy for agency: " + licenseeId, true);
                policiesData = policiesData.Replace("'", "");
                //if (!string.IsNullOrEmpty(uniqueKey) && uniqueKey.Trim() == "CommDept1973")
                {
                    List<CompType> lstComp = (new CompType()).GetAllComptype();
                    ObservableCollection<CompType> compList = new ObservableCollection<CompType>(lstComp);

                    DataTable tbExcel = (DataTable)Newtonsoft.Json.JsonConvert.DeserializeObject(policiesData, (typeof(DataTable)));
                    ObservableCollection<User> GlobalAgentList = new ObservableCollection<User>(Report.GetAgentList(licenseeId));
                    //MailServerDetail.sendMail("jyotisna@acmeminds.com", "Import process started from web app at " + DateTime.Now.ToString(), inRequest);

                    PolicyImportStatus status = Policy.ImportPolicy(tbExcel, GlobalAgentList, licenseeId, compList);
                    jres = new PolicyImportResponse(string.Format("Import process execution completed"), Convert.ToInt16(200), "");
                    jres.ImportStatus = status;
                    ActionLogger.Logger.WriteLog("********Import Process Execution completed********", true);
                }


            }
            catch (Exception ex)
            {
                jres = new PolicyImportResponse("Import process execution failed", Convert.ToInt16(210), ex.Message);
                ActionLogger.Logger.WriteLog("Import process execution  failure: " + ex.Message, true);
            }
            return jres;

        }
        /*
         /// <summary>
         /// Author:Ankit Khandelwal
         /// Purpose:policyDeleted based on policyId and clientDeleted
         /// Date:Sep 17,2018
         /// </summary>
         /// <param name="policyId"></param>
         /// <param name="isForcefullyDeleted"></param>
         /// <returns></returns>
         public JSONResponse DeletePolicyBasedOnIdService(Guid policyId, bool isForcefullyDeleted = false)
         {
             JSONResponse jres = null;
             ActionLogger.Logger.WriteLog("DeletePolicyHistoryService request: ", true);
             try
             {
                 Policy.DeletePolicyBasedOnId(policyId, out int checkCondtions, isForcefullyDeleted);
                 if (checkCondtions == 1)
                 {
                     jres = new JSONResponse(_resourceManager.GetString("Policyalreadydeleted"), Convert.ToInt16(ResponseCodes.Success), "");
                     ActionLogger.Logger.WriteLog("policy already deleted PolicyId" + policyId, true);
                 }
                 else if (checkCondtions == 0)
                 {
                     jres = new JSONResponse(_resourceManager.GetString("Policyalreadydeleted"), Convert.ToInt16(ResponseCodes.Success), "");
                     ActionLogger.Logger.WriteLog("Policy not found with this policyId :" + policyId, true);
                 }
                 else if (checkCondtions == 2)
                 {
                     jres = new JSONResponse(_resourceManager.GetString("OutgoingPaymentassociates"), Convert.ToInt16(ResponseCodes.OutgoingPaymentExist), "");
                     ActionLogger.Logger.WriteLog("Policy have payments so policy Cant be deleted policyId:" + policyId, true);
                 }
                 else if (checkCondtions == 3)
                 {
                     jres = new JSONResponse(_resourceManager.GetString("PolicyDeleted"), Convert.ToInt16(ResponseCodes.Success), "");
                     ActionLogger.Logger.WriteLog("Policy deleted successfully  policyId:" + policyId, true);
                 }
                 else if (checkCondtions == 4)
                 {
                     jres = new JSONResponse(_resourceManager.GetString("PolicyDeletedwithClient"), Convert.ToInt16(ResponseCodes.Success), "");
                     ActionLogger.Logger.WriteLog("Policy  deleted successfully with client:" + policyId, true);
                 }
                 else if (checkCondtions == 5)
                 {
                     jres = new JSONResponse(_resourceManager.GetString("PolicyCancelOperation "), Convert.ToInt16(ResponseCodes.Failure), "");
                     ActionLogger.Logger.WriteLog("Policy can't deleted perform cancel opertaion  policyId:" + policyId, true);
                 }
             }
             catch (Exception ex)
             {
                 jres = new JSONResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                 ActionLogger.Logger.WriteLog("DeletePolicyBasedOnIdService failure ", true);
             }
             return jres;
         }

         #endregion
        
        #region Outgoing Schedule

        /// <summary>
        /// Author:Ankit khandelwal
        /// CreatedOn:23-10-2018
        /// Purpose:To get the list of Payees based on licenseeId
        /// </summary>
        /// <param name="licenseeId"></param>
        /// <returns></returns>
        public PolicyOutgoingScheduleListResponse GetOutgoingScheduleByLicenseeService(Guid licenseeId)
         {
             PolicyOutgoingScheduleListResponse jres = null;
             ActionLogger.Logger.WriteLog("GetOutgoingScheduleByPolicyService request: " + licenseeId.ToStringDump(), true);
             try
             {
                 IList<PolicyOutgoingScheduleList> schedule = OutgoingSchedule.GetPolicyOutgoingScheduleById(licenseeId);
                 if (schedule.Count >= 1)
                 {
                     jres = new PolicyOutgoingScheduleListResponse(_resourceManager.GetString("RecordsFound"), Convert.ToInt16(ResponseCodes.Success), "");
                     jres.OutgoingSchedulelist = schedule;
                     ActionLogger.Logger.WriteLog("GetOutgoingScheduleByPolicyService success ", true);
                 }
                 else
                 {
                     jres = new PolicyOutgoingScheduleListResponse(_resourceManager.GetString("RecordsNotFoundOutgoingSchedule"), Convert.ToInt16(ResponseCodes.RecordNotFound), "");
                     ActionLogger.Logger.WriteLog("GetOutgoingScheduleByPolicyService success ", true);
                 }
             }
             catch (Exception ex)
             {
                 jres = new PolicyOutgoingScheduleListResponse(_resourceManager.GetString("RecordFailure"), Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                 ActionLogger.Logger.WriteLog("GetOutgoingScheduleByPolicyService failure ", true);
             }
             return jres;
         }
      
         /// <summary>
         /// Author:Ankit khandelwal
         /// CreatedOn:23-10-2018
         /// Purpose:To get the details of HouseAccount 
         /// </summary>
         /// <param name="licenseeId"></param>
         /// <returns></returns>
         public GetHouseAccountResponse GetHouseAccountdetailsService(Guid licenseeId)
         {
             GetHouseAccountResponse jres = null;
             try
             {
                 HouseAccountDetails getdetails = Policy.GetHouseAccount(licenseeId);
                 jres = new GetHouseAccountResponse("Policies found successfully", Convert.ToInt16(ResponseCodes.Success), "");
                 jres.HouseAccountdetails = getdetails;
                 return jres;
             }
             catch (Exception ex)
             {
                 ActionLogger.Logger.WriteLog("" + ex.Message, true);
             }
             return jres;
         }
         #endregion

        
        
         public PolicySearchResponse GetAllSearchedPoliciesService(string strClient, string strInsured, string policynumber, string carrier, string payor, Guid UserCridenID, UserRole Role, Guid LicenseID)
         {
             PolicySearchResponse jres = null;
             ActionLogger.Logger.WriteLog("GetAllSearchedPolicies request: ", true);
             try
             {
                 PolicySearched objPolicySerched = new PolicySearched();
                 List<PolicySearched> lst = objPolicySerched.GetLinkedPolicies(strClient, strInsured, policynumber, carrier, payor, UserCridenID, Role, LicenseID);
                 ActionLogger.Logger.WriteLog("GetAllSearchedPolicies success", true);
                 if (lst != null && lst.Count > 0)
                 {
                     jres = new PolicySearchResponse("Policies found successfully", Convert.ToInt16(ResponseCodes.Success), "");
                     jres.Policylist = lst;
                 }
                 else
                 {
                     jres = new PolicySearchResponse("No Policies found with the given search criteria", Convert.ToInt16(ResponseCodes.RecordNotFound), "");
                 }
             }
             catch (Exception ex)
             {
                 jres = new PolicySearchResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                 ActionLogger.Logger.WriteLog("GetAllSearchedPolicies failure: " + ex.Message, true);
             }
             return jres;
         }

         #region IPolicy Members


         //public void UpdateRPolicyStatusService(PolicyDetailsData policy)
         //{
         //    Policy.UpdateRPolicyStatus(policy);
         //}

         public JSONResponse UpdatePolicySettingService(PolicyDetailsData policy)
         {
             JSONResponse jres = null;
             ActionLogger.Logger.WriteLog("UpdatePolicySettingService request: ", true);
             try
             {
                 Policy.UpdatePolicySetting(policy);
                 jres = new JSONResponse(string.Format("Policy setting saved successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                 ActionLogger.Logger.WriteLog("UpdatePolicySettingService success ", true);
             }
             catch (Exception ex)
             {
                 jres = new JSONResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                 ActionLogger.Logger.WriteLog("UpdatePolicySettingService failure ", true);
             }
             return jres;
         }

         public JSONResponse DeletePolicyService(PolicyDetailsData policy)
         {
             JSONResponse jres = null;
             ActionLogger.Logger.WriteLog("UpdatePolicySettingService request: ", true);
             try
             {
                 Policy.MarkPolicyDeleted(policy);
                 jres = new JSONResponse(string.Format("Policy setting saved successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                 ActionLogger.Logger.WriteLog("UpdatePolicySettingService success ", true);
             }
             catch (Exception ex)
             {
                 jres = new JSONResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                 ActionLogger.Logger.WriteLog("UpdatePolicySettingService failure ", true);
             }
             return jres;
         }

         public PolicyListResponse GetpoliciesDataService(Dictionary<string, object> parameters)
         {
             PolicyListResponse jres = null;
             ActionLogger.Logger.WriteLog("GetpoliciesDataService request: ", true);
             try
             {
                 List<PolicyDetailsData> lst = Policy.GetpoliciesData(parameters);
                 jres = new PolicyListResponse(string.Format("Policy found successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                 jres.PolicyDetailList = lst;
                 ActionLogger.Logger.WriteLog("GetpoliciesDataService success ", true);
             }
             catch (Exception ex)
             {
                 jres = new PolicyListResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                 ActionLogger.Logger.WriteLog("GetpoliciesDataService failure ", true);
             }
             return jres;
         }

         public PolicyDetailsData GetPolicySettingService(Guid PolicyID)
         {
             Policy objPolicy = new Policy();
             return objPolicy.GetPolicyStting(PolicyID);
         }

         public PolicyTrackResponse IsTrackPaymentCheckedService(Guid PolicyId)
         {
             PolicyTrackResponse jres = null;
             try
             {
                 bool isTrack = Policy.IsTrackPaymentChecked(PolicyId);
                 jres = new PolicyTrackResponse(string.Format("Policy track status found successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                 jres.IsTrackEnabled = isTrack;
             }
             catch (Exception ex)
             {
                 jres = new PolicyTrackResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
             }
             return jres;
         }

         public PolicyPMCResponse GetPolicyStatusIDService(Guid PolicyId)
         {
             PolicyPMCResponse jres = null;
             ActionLogger.Logger.WriteLog("GetPolicyStatusIDService request: policyId" + PolicyId, true);
             try
             {
                 int? val = Policy.GetPolicyStatusID(PolicyId);
                 ActionLogger.Logger.WriteLog("GetPolicyStatusIDService value: " + val, true);
                 if (val != null)
                 {
                     jres = new PolicyPMCResponse(string.Format("Data found successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                     jres.NumberValue = (decimal)val;
                 }
                 else
                 {
                     jres = new PolicyPMCResponse("", Convert.ToInt16(ResponseCodes.RecordNotFound), "Status not found.");
                     ActionLogger.Logger.WriteLog("GetPolicyStatusIDService failure ", true);
                 }
             }
             catch (Exception ex)
             {
                 jres = new PolicyPMCResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                 ActionLogger.Logger.WriteLog("GetPolicyStatusIDService failure ", true);
             }
             return jres;
         }

         public JSONResponse UpdatePolicyTermDateService(Guid policyID, string TermDateString)
         {
             JSONResponse jres = null;
             ActionLogger.Logger.WriteLog("UpdatePolicyTermDateService request: policyId" + policyID + " dtime: " + TermDateString, true);
             try
             {
                 Policy objPolicy = new Policy();
                 if (!string.IsNullOrEmpty(TermDateString))
                 {
                     DateTime dtTerm = DateTime.MinValue;
                     DateTime.TryParse(TermDateString, out dtTerm);
                     objPolicy.UpdatePolicyTermDate(policyID, dtTerm);
                     jres = new JSONResponse(string.Format("Data saved successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                     ActionLogger.Logger.WriteLog("UpdatePolicyTermDateService success ", true);
                 }
                 else
                 {
                     jres = new JSONResponse("Term date received blank", Convert.ToInt16(ResponseCodes.Failure), "Term date received blank");
                     ActionLogger.Logger.WriteLog("UpdatePolicyTermDateService failure ", true);
                 }
             }
             catch (Exception ex)
             {
                 jres = new JSONResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                 ActionLogger.Logger.WriteLog("UpdatePolicyTermDateService failure ", true);
             }
             return jres;

         }

         public PolicyDateResponse GetFollowUpDateService(Guid PolicyID)
         {
             PolicyDateResponse jres = null;
             ActionLogger.Logger.WriteLog("GetFollowUpDateService request: policyId" + PolicyID, true);
             try
             {
                 DateTime? dt = new DateTime();
                 Policy objPolicy = new Policy();
                 dt = objPolicy.GetFollowUpDate(PolicyID);
                 ActionLogger.Logger.WriteLog("GetFollowUpDateService success dt: " + dt, true);
                 if (dt != null)
                 {
                     jres = new PolicyDateResponse(string.Format("Data found successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                     jres.PolicyDate = dt == null ? string.Empty : dt.ToString();
                 }
                 else
                 {
                     jres = new PolicyDateResponse("", Convert.ToInt16(ResponseCodes.Failure), string.Format("Data not found"));
                 }
             }
             catch (Exception ex)
             {
                 jres = new PolicyDateResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                 ActionLogger.Logger.WriteLog("GetFollowUpDateService failure ", true);
             }
             return jres;
         }

         public PolicyPMCResponse CalculatePMCService(Guid PolicyId)
         {
             PolicyPMCResponse jres = null;
             ActionLogger.Logger.WriteLog("CalculatePMCService request: policyId" + PolicyId, true);
             try
             {
                 decimal pmc = Policy.GetPMC(PolicyId);
                 jres = new PolicyPMCResponse(string.Format("Data found successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                 jres.NumberValue = pmc;
                 ActionLogger.Logger.WriteLog("CalculatePMCService success ", true);
             }
             catch (Exception ex)
             {
                 jres = new PolicyPMCResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                 ActionLogger.Logger.WriteLog("CalculatePMCService failure ", true);
             }
             return jres;
         }

         public PolicyPMCResponse CalculatePACService(Guid PolicyId)
         {
             PolicyPMCResponse jres = null;
             ActionLogger.Logger.WriteLog("CalculatePACService request: policyId" + PolicyId, true);
             try
             {
                 decimal pmc = Policy.GetPAC(PolicyId);
                 jres = new PolicyPMCResponse(string.Format("Data found successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                 jres.NumberValue = pmc;
                 ActionLogger.Logger.WriteLog("CalculatePACService success ", true);
             }
             catch (Exception ex)
             {
                 jres = new PolicyPMCResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                 ActionLogger.Logger.WriteLog("CalculatePACService failure ", true);
             }
             return jres;
         }

         #endregion

         #region IPolicy Members


         public PolicyBatchResponse GenerateBatchService(PolicyDetailsData PolicyObject)
         {
             PolicyBatchResponse jres = null;
             ActionLogger.Logger.WriteLog("GenerateBatchService request: policyId" + PolicyObject.ToStringDump(), true);
             try
             {
                 Batch b = Policy.GenerateBatch(PolicyObject);
                 jres = new PolicyBatchResponse(string.Format("Batch generated successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                 jres.PolicyBatch = b;
                 ActionLogger.Logger.WriteLog("GenerateBatchService success ", true);
             }
             catch (Exception ex)
             {
                 jres = new PolicyBatchResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                 ActionLogger.Logger.WriteLog("GenerateBatchService failure ", true);
             }
             return jres;
         }

         #endregion

         #region IPolicy Members


         public PolicyStmtResponse GenerateStatmentService(Guid BatchId, Guid PayorId, decimal PaymentRecived, Guid CreatedBy)
         {
             PolicyStmtResponse jres = null;
             ActionLogger.Logger.WriteLog("GenerateBatchService request: BatchId" + BatchId, true);
             try
             {
                 Statement s = Policy.GenerateStatment(BatchId, PayorId, PaymentRecived, CreatedBy);
                 jres = new PolicyStmtResponse(string.Format("Data found successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                 jres.PolicyStmt = s;
                 ActionLogger.Logger.WriteLog("GenerateBatchService success ", true);
             }
             catch (Exception ex)
             {
                 jres = new PolicyStmtResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                 ActionLogger.Logger.WriteLog("GenerateBatchService failure ", true);
             }
             return jres;
         }

         #endregion

         #region IPolicy Members


         public JSONResponse AddUpdatePolicyHistoryService(Guid PolicyId)
         {
             JSONResponse jres = null;
             ActionLogger.Logger.WriteLog("AddUpdatePolicyHistoryService request: ", true);
             try
             {
                 Policy.AddUpdatePolicyHistory(PolicyId);
                 jres = new JSONResponse(string.Format("Policy history saved successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                 ActionLogger.Logger.WriteLog("AddUpdatePolicyHistoryService success ", true);
             }
             catch (Exception ex)
             {
                 jres = new JSONResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                 ActionLogger.Logger.WriteLog("AddUpdatePolicyHistoryService failure ", true);
             }
             return jres;

         }

         //public PolicyDetailsData GetPolicyHistoryIdWiseService(Guid PolicyId)
         //{
         //    return Policy.GetPolicyHistoryIdWise(PolicyId);

         //}

         public JSONResponse DeletePolicyHistoryService(PolicyDetailsData _policyrecord)
         {
             JSONResponse jres = null;
             ActionLogger.Logger.WriteLog("DeletePolicyHistoryService request: ", true);
             try
             {
                 Policy.DeletePolicyHistory(_policyrecord);
                 jres = new JSONResponse(string.Format("Policy history removed successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                 ActionLogger.Logger.WriteLog("DeletePolicyHistoryService success ", true);
             }
             catch (Exception ex)
             {
                 jres = new JSONResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                 ActionLogger.Logger.WriteLog("DeletePolicyHistoryService failure ", true);
             }
             return jres;
         }




         //public void DeletePolicyHistoryPermanetByIdService(PolicyDetailsData _Policy)
         //{
         //    Policy.DeletePolicyHistoryPermanentById(_Policy);
         //}

         public PolicyStringResponse GetPolicyProductTypeService(Guid policyID, Guid PayorID, Guid CarrierID, Guid CoverageID)
         {
             PolicyStringResponse jres = null;
             ActionLogger.Logger.WriteLog("GetPolicyProductTypeService request: policyID " + policyID, true);
             try
             {
                 string s = Policy.GetPolicyProductType(policyID, PayorID, CarrierID, CoverageID);
                 jres = new PolicyStringResponse(string.Format("Data found successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                 jres.StringValue = s;
                 ActionLogger.Logger.WriteLog("GetPolicyProductTypeService success ", true);
             }
             catch (Exception ex)
             {
                 jres = new PolicyStringResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                 ActionLogger.Logger.WriteLog("GetPolicyProductTypeService failure ", true);
             }
             return jres;
         }

         #endregion

         #region IPolicy Members


         public PolicyBoolResponse CheckForPolicyPaymentExistsService(Guid Policyid)
         {
             PolicyBoolResponse jres = null;
             ActionLogger.Logger.WriteLog("CheckForPolicyPaymentExistsService request: Policyid -" + Policyid, true);
             try
             {
                 bool res = Policy.CheckForPolicyPaymentExists(Policyid);
                 jres = new PolicyBoolResponse(string.Format("Data found successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                 jres.BoolFlag = res;
                 ActionLogger.Logger.WriteLog("CheckForPolicyPaymentExistsService success ", true);
             }
             catch (Exception ex)
             {
                 jres = new PolicyBoolResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                 ActionLogger.Logger.WriteLog("CheckForPolicyPaymentExistsService failure ", true);
             }
             return jres;
         }


         public PolicyListResponse GetPolicyClientWiseService(Guid LicenseeId, Guid ClientId)
         {
             PolicyListResponse jres = null;
             ActionLogger.Logger.WriteLog("GetPolicyClientWiseService request: LicenseeId" + LicenseeId + ", Client: " + ClientId, true);
             try
             {
                 List<PolicyDetailsData> lst = Policy.GetPolicyClientWise(LicenseeId, ClientId);
                 jres = new PolicyListResponse(string.Format("Data found successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                 jres.PolicyDetailList = lst;
                 ActionLogger.Logger.WriteLog("GetPolicyClientWiseService success ", true);
             }
             catch (Exception ex)
             {
                 jres = new PolicyListResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                 ActionLogger.Logger.WriteLog("GetPolicyClientWiseService failure ", true);
             }
             return jres;
         }

         #endregion

         #region Import Policy Members

       

         public PolicyStringResponse GetPolicyUniqueKeyNameService()
         {
             PolicyStringResponse jres = null;
             ActionLogger.Logger.WriteLog("GetPolicyUniqueKeyNameService request: LicenseeId", true);
             try
             {
                 string s = Policy.GetPolicyIdKeyForImport();
                 jres = new PolicyStringResponse(string.Format("Data found successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                 jres.StringValue = s;
                 ActionLogger.Logger.WriteLog("GetPolicyUniqueKeyNameService success ", true);
             }
             catch (Exception ex)
             {
                 jres = new PolicyStringResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                 ActionLogger.Logger.WriteLog("GetPolicyUniqueKeyNameService failure ", true);
             }
             return jres;

         }

         public PolicyStringResponse CompareExcelService(DataTable dt)
         {
             PolicyStringResponse jres = null;
             ActionLogger.Logger.WriteLog("CompareExcelService request:", true);
             try
             {
                 string s = Policy.CheckExcelFormat(dt);
                 jres = new PolicyStringResponse(string.Format("Data found successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                 jres.StringValue = s;
                 ActionLogger.Logger.WriteLog("CompareExcelService success ", true);
             }
             catch (Exception ex)
             {
                 jres = new PolicyStringResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                 ActionLogger.Logger.WriteLog("CompareExcelService failure ", true);
             }
             return jres;

         }

         #endregion

         #region IPayee Members

         //public JSONResponse AddUpdatePayeeService(Payee Pyee)
         //{
         //    JSONResponse jres = null;
         //    ActionLogger.Logger.WriteLog("AddUpdatePayeeService request: ", true);
         //    try
         //    {
         //        Pyee.AddUpdate();
         //        jres = new JSONResponse(string.Format("Payee saved successfully"), Convert.ToInt16(ResponseCodes.Success), "");
         //        ActionLogger.Logger.WriteLog("AddUpdatePayeeService success ", true);
         //    }
         //    catch (Exception ex)
         //    {
         //        jres = new JSONResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
         //        ActionLogger.Logger.WriteLog("AddUpdatePayeeService failure ", true);
         //    }
         //    return jres;
         //}

         //public JSONResponse DeletePayeeService(Payee Pyee)
         //{
         //     JSONResponse jres = null;
         //    ActionLogger.Logger.WriteLog("DeletePayeeService request: ", true);
         //    try
         //    {
         //        Pyee.Delete();
         //        jres = new JSONResponse(string.Format("Payee deleted successfully"), Convert.ToInt16(ResponseCodes.Success), "");
         //        ActionLogger.Logger.WriteLog("DeletePayeeService success ", true);
         //    }
         //    catch (Exception ex)
         //    {
         //        jres = new JSONResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
         //        ActionLogger.Logger.WriteLog("DeletePayeeService failure ", true);
         //    }
         //    return jres;
         //}

         //public PolicyBoolResponse IsValidPayeeService(Payee Pyee)
         //{
         //    PolicyBoolResponse jres = null;
         //    ActionLogger.Logger.WriteLog("IsValidPayeeService request: ", true);
         //    try
         //    {
         //        bool isValid = Pyee.IsValid();
         //        jres = new PolicyBoolResponse(string.Format("Payee status found successfully"), Convert.ToInt16(ResponseCodes.Success), "");
         //        jres.BoolFlag = isValid;
         //        ActionLogger.Logger.WriteLog("IsValidPayeeService success ", true);
         //    }
         //    catch (Exception ex)
         //    {
         //        jres = new PolicyBoolResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
         //        ActionLogger.Logger.WriteLog("IsValidPayeeService failure ", true);
         //    }
         //    return jres;
         //}

         #endregion

         #region Last viewed policies
         public LastViewedPolicyResponse GetLastViewPolicyService(Guid userCredentialID)
         {
             LastViewedPolicyResponse jres = null;
             ActionLogger.Logger.WriteLog("GetLastViewPolicyService request: ", true);
             try
             {
                 List<LastViewPolicy> lst = LastViewPolicy.GetLastViewPolicy(userCredentialID);
                 jres = new LastViewedPolicyResponse(string.Format("Policy list found successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                 jres.LastVewedPolicyList = lst;
                 ActionLogger.Logger.WriteLog("GetLastViewPolicyService success ", true);
             }
             catch (Exception ex)
             {
                 jres = new LastViewedPolicyResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                 ActionLogger.Logger.WriteLog("GetLastViewPolicyService failure ", true);
             }
             return jres;

         }
         public LastViewedPolicyResponse GetAllLastViewPolicyService()
         {
             LastViewedPolicyResponse jres = null;
             ActionLogger.Logger.WriteLog("GetAllLastViewPolicy request: ", true);
             try
             {
                 List<LastViewPolicy> lst = LastViewPolicy.GetAllLastViewPolicy();
                 jres = new LastViewedPolicyResponse(string.Format("Policy list found successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                 jres.LastVewedPolicyList = lst;
                 ActionLogger.Logger.WriteLog("GetAllLastViewPolicy success ", true);
             }
             catch (Exception ex)
             {
                 jres = new LastViewedPolicyResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                 ActionLogger.Logger.WriteLog("GetAllLastViewPolicy failure ", true);
             }
             return jres;
         }

         public JSONResponse AddLastViewPolicyService(LastViewPolicy lastviewpolicy)
         {
             JSONResponse jres = null;
             ActionLogger.Logger.WriteLog("AddLastViewPolicyService request: ", true);
             try
             {
                 lastviewpolicy.AddUpdate();
                 jres = new JSONResponse(string.Format("Last viewed policy details saved successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                 ActionLogger.Logger.WriteLog("AddLastViewPolicyService success ", true);
             }
             catch (Exception ex)
             {
                 jres = new JSONResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                 ActionLogger.Logger.WriteLog("AddLastViewPolicyService failure ", true);
             }
             return jres;
         }

         public JSONResponse DeleteRecordCredientialWiseService(Guid userCredentialID)
         {
             JSONResponse jres = null;
             ActionLogger.Logger.WriteLog("DeleteRecordCredientialWiseService request: ", true);
             try
             {
                 LastViewPolicy.DeleteRecordCredientialWise(userCredentialID);
                 jres = new JSONResponse(string.Format("Records deleted successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                 ActionLogger.Logger.WriteLog("DeleteRecordCredientialWiseService success ", true);
             }
             catch (Exception ex)
             {
                 jres = new JSONResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                 ActionLogger.Logger.WriteLog("DeleteRecordCredientialWiseService failure ", true);
             }
             return jres;
         }

         public JSONResponse SaveLastViewedClientsService(List<LastViewPolicy> lastViewedClients, Guid userId)
         {
             JSONResponse jres = null;
             ActionLogger.Logger.WriteLog("SaveLastViewedClientsService request: " + lastViewedClients.ToStringDump(), true);
             try
             {
                 LastViewPolicy.SaveLastViewedClients(lastViewedClients, userId);
                 jres = new JSONResponse(string.Format("Last viewed clients saved successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                 ActionLogger.Logger.WriteLog("SaveLastViewedClientsService success ", true);
             }
             catch (Exception ex)
             {
                 jres = new JSONResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                 ActionLogger.Logger.WriteLog("SaveLastViewedClientsService failure ", true);
             }
             return jres;
         }

         public LastViewedPolicyResponse GetLastViewedClientsService(Guid userID)
         {
             LastViewedPolicyResponse jres = null;
             ActionLogger.Logger.WriteLog("GetLastViewedClients request: ", true);
             try
             {
                 List<LastViewPolicy> lst = LastViewPolicy.GetLastViewedClients(userID);
                 jres = new LastViewedPolicyResponse(string.Format("Client list found successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                 jres.LastVewedPolicyList = lst;
                 ActionLogger.Logger.WriteLog("GetLastViewedClients success ", true);
             }
             catch (Exception ex)
             {
                 jres = new LastViewedPolicyResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                 ActionLogger.Logger.WriteLog("GetLastViewedClients failure ", true);
             }
             return jres;
         }
         #endregion*/
    }
}
