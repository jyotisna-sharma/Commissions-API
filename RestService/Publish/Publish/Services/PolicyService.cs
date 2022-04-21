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

namespace MyAgencyVault.WcfService
{
    [ServiceContract]
    interface IPolicyService
    {
        #region Incoming Schedule
        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        JSONResponse AddUpdatePolicyIncomingScheduleService(PolicyToolIncommingShedule PInc);

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
        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        JSONResponse AddUpdateOutgoingScheduleService(PolicyOutgoingSchedule OutgoingSchedule);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        PolicyOutgoingScheduleResponse GetOutgoingScheduleByPolicyService(Guid PolicyID);
        #endregion


        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        PolicySearchResponse GetAllSearchedPoliciesService(string StrClient, string StrInsured, string Policynumber, string Carrier, string Payor, Guid UserID, UserRole Role, Guid LicenseID);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json,  BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        JSONResponse SavePolicyService(PolicyDetailsData Policy, PolicyDetailsData ReplacedPolicy, string strRenewal, string strCoverageNickName);

        //[OperationContract]
        //[WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json,  BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        //void UpdateRPolicyStatusService(PolicyDetailsData policy);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json,  BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        JSONResponse UpdatePolicySettingService(PolicyDetailsData policy);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json,  BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        JSONResponse DeletePolicyService(PolicyDetailsData policy);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json,  BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        PolicyTrackResponse IsTrackPaymentCheckedService(Guid PolicyId);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json,  BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        PolicyPMCResponse GetPolicyStatusIDService(Guid PolicyId);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json,  BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        PolicyBatchResponse GenerateBatchService(PolicyDetailsData Policy);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json,  BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        PolicyStmtResponse GenerateStatmentService(Guid BatchId, Guid PayorId, decimal PaymentRecived, Guid CreatedBy);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json,  BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        JSONResponse AddUpdatePolicyHistoryService(Guid PolicyId);


        //[OperationContract]
        //[WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json,  BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        //PolicyDetailsData GetPolicyHistoryIdWiseService(Guid PolicyId);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json,  BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        JSONResponse DeletePolicyHistoryService(PolicyDetailsData Policy);

        //[OperationContract]
        //[WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json,  BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        //void DeletePolicyHistoryPermanetByIdService(PolicyDetailsData _Policy);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json,  BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        PolicyBoolResponse CheckForPolicyPaymentExistsService(Guid Policyid);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json,  BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        PolicyListResponse GetPolicydataService(Dictionary<string, object> parameters);

      
        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json,  BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        JSONResponse UpdatePolicyTermDateService(Guid policyID, DateTime? TermDate);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json,  BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        PolicyPMCResponse CalculatePMCService(Guid PolicyId);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json,  BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        PolicyPMCResponse CalculatePACService(Guid PolicyId);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json,  BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        PolicyDetailsData GetPolicySettingService(Guid PolicyID);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json,  BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        PolicyDateResponse GetFollowUpDateService(Guid PolicyID);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json,  BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        PolicyStringResponse GetPolicyProductTypeService(Guid PolicyID, Guid PayorID, Guid CarrierID, Guid CoverageID);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json,  BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        PolicyListResponse GetPolicyClientWiseService(Guid LicenseeId, Guid ClientId);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json,  BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        PolicyStringResponse GetPolicyUniqueKeyNameService();

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json,  BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        PolicyStringResponse  CompareExcelService(DataTable dt);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json,  BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        PolicyImportStatus ImportPolicyService(DataTable tbExcel, ObservableCollection<User> GlobalAgentList, Guid LicenseeID, ObservableCollection<CompType> CompTypeList);

        #region Payee service
        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        JSONResponse AddUpdatePayeeService(Payee Pyee);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        JSONResponse DeletePayeeService(Payee Pyee);
        
        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        PolicyBoolResponse IsValidPayeeService(Payee Pyee);

        #endregion

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

        #endregion
    }

    public partial class MavService : IPolicyService
    {
        #region Outgoing Schedule
        public JSONResponse AddUpdateOutgoingScheduleService(PolicyOutgoingSchedule outgoingSchedule)
        {
            JSONResponse jres = null;
            ActionLogger.Logger.WriteLog("AddUpdateOutgoingShedule request: " + outgoingSchedule.ToStringDump(), true);
            try
            {
                OutgoingSchedule.AddUpdatePolicyOutgoingSchedule(outgoingSchedule);
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

        public PolicyOutgoingScheduleResponse GetOutgoingScheduleByPolicyService(Guid policyId)
        {
            PolicyOutgoingScheduleResponse jres = null;
            ActionLogger.Logger.WriteLog("GetOutgoingScheduleByPolicyService request: " + policyId.ToStringDump(), true);
            try
            {
                PolicyOutgoingSchedule schedule = OutgoingSchedule.GetPolicyOutgoingSchedule(policyId);
                jres = new PolicyOutgoingScheduleResponse(string.Format("Policy outgoing schedule found successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                jres.PolicySchedule = schedule;
                ActionLogger.Logger.WriteLog("GetOutgoingScheduleByPolicyService success ", true);
            }
            catch (Exception ex)
            {
                jres = new PolicyOutgoingScheduleResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                ActionLogger.Logger.WriteLog("GetOutgoingScheduleByPolicyService failure ", true);
            }
            return jres;
        }
        #endregion

        #region Incoming Schedule
        public JSONResponse AddUpdatePolicyIncomingScheduleService(PolicyToolIncommingShedule PInc)
        {
            JSONResponse jres = null;
            ActionLogger.Logger.WriteLog("AddUpdatePolicyIncomingScheduleService request: " + PInc.ToStringDump(), true);
            try
            {
                PInc.AddUpdate();
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
        public JSONResponse SavePolicyService(PolicyDetailsData PolicySave, PolicyDetailsData ReplacedPolicy, string strRenewal, string strCoverageNickName)
        {
            JSONResponse jres = null;
            ActionLogger.Logger.WriteLog("SavePolicyService request: ", true);
            try
            {
                PolicySavedStatus status = Policy.SavePolicy(PolicySave, ReplacedPolicy, strRenewal, strCoverageNickName);
                ActionLogger.Logger.WriteLog("SavePolicyService success: IsError " + status.IsError, true);
                if (status.IsError)
                {
                    jres = new JSONResponse("", Convert.ToInt16(ResponseCodes.Failure), status.ErrorMessage);
                }
                else
                {
                    jres = new JSONResponse(string.Format("Policy saved successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                }
            }
            catch (Exception ex)
            {
                jres = new JSONResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                ActionLogger.Logger.WriteLog("SavePolicyService failure: " + ex.Message, true);
            }
            return jres;
        }

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

        public PolicyListResponse GetPolicydataService(Dictionary<string, object> parameters)
        {
            PolicyListResponse jres = null;
            ActionLogger.Logger.WriteLog("GetPolicydataService request: ", true);
            try
            {
                List<PolicyDetailsData> lst = Policy.GetPolicyData(parameters);
                jres = new PolicyListResponse(string.Format("Policy found successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                jres.PolicyList = lst;
                ActionLogger.Logger.WriteLog("GetPolicydataService success ", true);
            }
            catch (Exception ex)
            {
                jres = new PolicyListResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                ActionLogger.Logger.WriteLog("GetPolicydataService failure ", true);
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

        public JSONResponse UpdatePolicyTermDateService(Guid policyID, DateTime? TermDate)
        {
            JSONResponse jres = null;
            ActionLogger.Logger.WriteLog("UpdatePolicyTermDateService request: policyId" + policyID + " dtime: " + TermDate, true);
            try
            {
                Policy objPolicy = new Policy();
                objPolicy.UpdatePolicyTermDate(policyID, TermDate);
                jres = new JSONResponse(string.Format("Data saved successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                ActionLogger.Logger.WriteLog("UpdatePolicyTermDateService success ", true);
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
                    jres.PolicyDate = dt==null ? string.Empty : dt.ToString();
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
            ActionLogger.Logger.WriteLog("CalculatePMCService request: policyId" + PolicyId , true);
            try
            {
                decimal pmc=Policy.GetPMC(PolicyId);
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


        public PolicyBatchResponse  GenerateBatchService(PolicyDetailsData PolicyObject)
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

        public JSONResponse  DeletePolicyHistoryService(PolicyDetailsData _policyrecord)
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
            ActionLogger.Logger.WriteLog("GetPolicyProductTypeService request: policyID "+ policyID, true);
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
                jres = new PolicyBoolResponse(string.Format("Data found successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                jres.BoolFlag = Policy.CheckForPolicyPaymentExists(Policyid);
                ActionLogger.Logger.WriteLog("CheckForPolicyPaymentExistsService success ", true);
            }
            catch (Exception ex)
            {
                jres = new PolicyBoolResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                ActionLogger.Logger.WriteLog("CheckForPolicyPaymentExistsService failure ", true);
            }
            return jres;
        }

        //Added by vinod
        public PolicyListResponse GetPolicyClientWiseService(Guid LicenseeId, Guid ClientId)
        {
            PolicyListResponse jres = null;
            ActionLogger.Logger.WriteLog("GetPolicyClientWiseService request: LicenseeId" + LicenseeId + ", Client: " + ClientId, true);
            try
            {
               List<PolicyDetailsData> lst = Policy.GetPolicyClientWise(LicenseeId, ClientId);
                jres = new PolicyListResponse(string.Format("Data found successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                jres.PolicyList = lst;
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

        public PolicyImportStatus ImportPolicyService(DataTable tbExcel, ObservableCollection<User> GlobalAgentList, Guid LicenseeID, ObservableCollection<CompType> CompTypeList)
        {
            PolicyImportStatus status = Policy.ImportPolicy(tbExcel, GlobalAgentList, LicenseeID, CompTypeList);
            return status;
        }

        public PolicyStringResponse GetPolicyUniqueKeyNameService()
        {
            PolicyStringResponse jres = null;
            ActionLogger.Logger.WriteLog("GetPolicyUniqueKeyNameService request: LicenseeId", true);
            try
            {
                string s =  Policy.GetPolicyIdKeyForImport();
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
            ActionLogger.Logger.WriteLog("CompareExcelService request:" , true);
            try
            {
                string s =  Policy.CheckExcelFormat(dt);
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

        public JSONResponse AddUpdatePayeeService(Payee Pyee)
        {
            JSONResponse jres = null;
            ActionLogger.Logger.WriteLog("AddUpdatePayeeService request: ", true);
            try
            {
                Pyee.AddUpdate();
                jres = new JSONResponse(string.Format("Payee saved successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                ActionLogger.Logger.WriteLog("AddUpdatePayeeService success ", true);
            }
            catch (Exception ex)
            {
                jres = new JSONResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                ActionLogger.Logger.WriteLog("AddUpdatePayeeService failure ", true);
            }
            return jres;
        }

        public JSONResponse DeletePayeeService(Payee Pyee)
        {
             JSONResponse jres = null;
            ActionLogger.Logger.WriteLog("DeletePayeeService request: ", true);
            try
            {
                Pyee.Delete();
                jres = new JSONResponse(string.Format("Payee deleted successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                ActionLogger.Logger.WriteLog("DeletePayeeService success ", true);
            }
            catch (Exception ex)
            {
                jres = new JSONResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                ActionLogger.Logger.WriteLog("DeletePayeeService failure ", true);
            }
            return jres;
        }

        public PolicyBoolResponse IsValidPayeeService(Payee Pyee)
        {
            PolicyBoolResponse jres = null;
            ActionLogger.Logger.WriteLog("IsValidPayeeService request: ", true);
            try
            {
                bool isValid = Pyee.IsValid();
                jres = new PolicyBoolResponse(string.Format("Payee status found successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                jres.BoolFlag = isValid;
                ActionLogger.Logger.WriteLog("IsValidPayeeService success ", true);
            }
            catch (Exception ex)
            {
                jres = new PolicyBoolResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                ActionLogger.Logger.WriteLog("IsValidPayeeService failure ", true);
            }
            return jres;
        }

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
                List<LastViewPolicy> lst =LastViewPolicy.GetLastViewedClients(userID);
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
        #endregion
    }
}