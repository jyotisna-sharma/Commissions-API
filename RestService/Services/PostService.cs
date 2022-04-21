using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyAgencyVault.BusinessLibrary;
using System.ServiceModel;
using MyAgencyVault.BusinessLibrary.PostProcess;
using MyAgencyVault.WcfService.Library.Response;
using System.ServiceModel.Web;
using MyAgencyVault.BusinessLibrary.BusinessObjects;

namespace MyAgencyVault.WcfService
{
    [ServiceContract]
    interface IPostService
    {
      

     

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        PolicyObjectResponse GetPolicyPUService(Guid SePolicyID);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        PolicyBoolResponse CheckForOutgoingScheduleVarianceSrvc(Guid PaymentEntryId);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        GuidResponse GetPolicyHouseOwnerService(Guid PolicyLicenID);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        PaymentEntryListResponse GetPaymentEntryForCommDashboardSrvc(Guid PolicyId);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        PolicyOutgoingPaymentListResponse GetOutgoingPaymentForCommDashboardSrvc(Guid policyPaymentEntryId, ListParams listParams);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        FollowupListResponse GetCommissionIssuesForCommDashboardSrvc(Guid PolicyId);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        JSONResponse SavePolicyPaymentEntriesService(PolicyPaymentEntriesPost policypaymententriespost);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        JSONResponse SaveResolvedorClosedService(PolicyPaymentEntriesPost policypaymententriespost);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        PaymentEntryListResponse GetAllResolvedorClosedIssueService(Guid? PolicyId);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        JSONResponse UpadateResolvedOrClosedManualSrvc(Guid PaymentEntryID, int intId);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        PaymentEntryObjectResponse GetPaymentEntryIdWiseService(Guid PolicyEntryid);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        JSONResponse EnterOutGoingPaymentService(bool IsPaymentToHO, Guid PaymentEntryID, Guid PolicyId, string InvoiceDate, Guid LicenseeId);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        PostStatusResponse RemoveIncomingPaymentService(PolicyPaymentEntriesPost PaymentEntry, UserRole userRole);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        PostStatusResponse UnlinkIncomingPaymentService(PolicyPaymentEntriesPost PaymentEntry, UserRole userRole);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        PostStatusResponse CommissionDashBoardPostStartService(Guid BatchId, PolicyPaymentEntriesPost PaymentEntry, PostEntryProcess _PostEntryProcess, UserRole _UserRole);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        JSONResponse FollowUpProcedureSrvc(FollowUpRunModules _FollowUpRunModules, DEU _DEU, Guid PolicyId, bool IsTrackPayment, bool IsEntryByCommissionDashboard, UserRole _UserRole, bool? PolicyModeChange);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        PolicyIncomingPaymentListResponse GetPaymentEntryPolicyIdWiseSrvc(Guid policyId, ListParams listParams);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        PostStatusResponse CommissionDashBoardPostStartClientSrvc(PolicyDetailsData SelectedPolicy, PolicyPaymentEntriesPost PaymentEntry, PostEntryProcess _PostEntryProcess, UserRole _UserRole, bool isInvoiceEdited = false);
    }

    public partial class MavService : IPostService
    {
        public JSONResponse EnterOutGoingPaymentService(bool IsPaymentToHO, Guid PaymentEntryID, Guid PolicyId, string InvoiceDateString, Guid LicenseeId)
        {
            JSONResponse jres = null;
            ActionLogger.Logger.WriteLog("EnterOutGoingPaymentService request: IsPaymentToHO - " + IsPaymentToHO + ",PaymentEntryID  " + PaymentEntryID + ", InvoiceDate " + InvoiceDateString, true);
            try
            {
                DateTime? InvoiceDate = null;
                if (!string.IsNullOrEmpty(InvoiceDateString))
                {
                    DateTime tmpDate = DateTime.MinValue;
                    DateTime.TryParse(InvoiceDateString, out tmpDate);
                    if (tmpDate != DateTime.MinValue)
                    {
                        InvoiceDate = tmpDate;
                    }
                }
                PostUtill.EntryInPolicyOutGoingPayment(IsPaymentToHO, PaymentEntryID, PolicyId, InvoiceDate, LicenseeId);
                jres = new JSONResponse(string.Format("Outgoing payment saved successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                ActionLogger.Logger.WriteLog("EnterOutGoingPaymentService success ", true);
            }
            catch (Exception ex)
            {
                jres = new JSONResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                ActionLogger.Logger.WriteLog("EnterOutGoingPaymentService failure ", true);
            }
            return jres;

        }

        public PaymentEntryObjectResponse GetPaymentEntryIdWiseService(Guid PaymentEntryID)
        {
            PaymentEntryObjectResponse jres = null;
            ActionLogger.Logger.WriteLog("GetPaymentEntryIdWiseService request: " + PaymentEntryID, true);
            try
            {
                PolicyPaymentEntriesPost obj = CommissionDashboard.GetPolicyPaymentPaymentEntryEntryIdWise(PaymentEntryID);

                if (obj != null)
                {
                    jres = new PaymentEntryObjectResponse(string.Format("Payment entry found successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                    ActionLogger.Logger.WriteLog("GetPaymentEntryIdWiseService success ", true);
                    jres.PaymentEntry = obj;
                }
                else
                {
                    jres = new PaymentEntryObjectResponse(string.Format("Payment entry could not be found"), Convert.ToInt16(ResponseCodes.Success), "");
                    ActionLogger.Logger.WriteLog("GetPaymentEntryIdWiseService 404 ", true);
                }
            }
            catch (Exception ex)
            {
                jres = new PaymentEntryObjectResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                ActionLogger.Logger.WriteLog("GetPaymentEntryIdWiseService failure ", true);
            }
            return jres;
        }


       



        #region IPostUtil Members

        //public PostProcessReturnStatus PostStart(PostEntryProcess _PostEntryProcess, Guid DeuEntryId, Guid RepostNewDeuEntryId, UserRole _UserRole)
        //{
        //    return DeuPostProcessWrapper.DeuPostStartWrapper(_PostEntryProcess, deuFields, deuEntryId, userId, userRole);
        //}

      
        public PolicyObjectResponse GetPolicyPUService(Guid PolicyID)
        {
            PolicyObjectResponse jres = null;
            ActionLogger.Logger.WriteLog("GetPolicyPUService request: " + PolicyID, true);
            try
            {
                PolicyDetailsData obj = PostUtill.GetPolicy(PolicyID);

                if (obj != null)
                {
                    jres = new PolicyObjectResponse(string.Format("Policy details found successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                    ActionLogger.Logger.WriteLog("GetPolicyPUService success ", true);
                    jres.PolicyObject = obj;
                }
                else
                {
                    jres = new PolicyObjectResponse(string.Format("Policy details could not be found"), Convert.ToInt16(ResponseCodes.RecordNotFound), "");
                    ActionLogger.Logger.WriteLog("GetPolicyPUService 404 ", true);
                }
            }
            catch (Exception ex)
            {
                jres = new PolicyObjectResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                ActionLogger.Logger.WriteLog("GetPolicyPUService failure ", true);
            }
            return jres;
        }

        public PolicyBoolResponse CheckForOutgoingScheduleVarianceSrvc(Guid PaymentEntryId)
        {
            PolicyBoolResponse jres = null;
            ActionLogger.Logger.WriteLog("CheckForOutgoingScheduleVarianceSrvc request: BatchId -" + PaymentEntryId, true);
            try
            {
                bool res = PostUtill.CheckForOutgoingScheduleVariance(PaymentEntryId);
                jres = new PolicyBoolResponse(string.Format("Variance found successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                jres.BoolFlag = res;
                ActionLogger.Logger.WriteLog("CheckForOutgoingScheduleVarianceSrvc success ", true);
            }
            catch (Exception ex)
            {
                jres = new PolicyBoolResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                ActionLogger.Logger.WriteLog("CheckForOutgoingScheduleVarianceSrvc failure ", true);
            }
            return jres;
        }

        public GuidResponse GetPolicyHouseOwnerService(Guid PolicyLicenID)
        {
            GuidResponse jres = null;
            ActionLogger.Logger.WriteLog("GetPolicyHouseOwnerService request: " + PolicyLicenID, true);
            try
            {
                Guid val = PostUtill.GetPolicyHouseOwner(PolicyLicenID);
                if (val != null)
                {
                    jres = new GuidResponse(string.Format("Policy house owner found successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                    ActionLogger.Logger.WriteLog("GetPolicyHouseOwnerService success ", true);
                    jres.GuidValue = val;
                }
                else
                {
                    jres = new GuidResponse(string.Format("Policy house owner not found"), Convert.ToInt16(ResponseCodes.Success), "");
                    ActionLogger.Logger.WriteLog("GetPolicyHouseOwnerService 404 ", true);
                }
            }
            catch (Exception ex)
            {
                jres = new GuidResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                ActionLogger.Logger.WriteLog("GetPolicyHouseOwnerService failure ", true);
            }
            return jres;
        }
        /// <summary>
        /// Modified By :ankit
        /// ModifiedOn:25-03-2019
        /// Purpose:getting list of ImcomingPaymentList
        /// </summary>
        /// <param name="policyPaymentEntryId"></param>
        /// <returns></returns>
        public PaymentEntryListResponse GetPaymentEntryForCommDashboardSrvc(Guid PolicyId)
        {
            PaymentEntryListResponse jres = null;
            ActionLogger.Logger.WriteLog("GetPaymentEntryForCommDashboardSrvc request " + PolicyId, true);
            try
            {
                List<PolicyPaymentEntriesPost> lst = CommissionDashboard.GetPolicyPaymentEntry(PolicyId);

                if (lst != null && lst.Count > 0)
                {
                    jres = new PaymentEntryListResponse(string.Format("Data found successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                    jres.PaymentsList = lst;
                    ActionLogger.Logger.WriteLog("GetPaymentEntryForCommDashboardSrvc success ", true);
                }
                else
                {
                    jres = new PaymentEntryListResponse(string.Format("No data found "), Convert.ToInt16(ResponseCodes.RecordNotFound), "");
                    ActionLogger.Logger.WriteLog("GetPaymentEntryForCommDashboardSrvc 404 ", true);
                }
            }
            catch (Exception ex)
            {
                jres = new PaymentEntryListResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                ActionLogger.Logger.WriteLog("GetPaymentEntryForCommDashboardSrvc failure ", true);
            }
            return jres;
        }

        /// <summary>
        /// Modified By :ankit
        /// ModifiedOn:25-03-2019
        /// Purpose:getting list of OutgoingPaymentList
        /// </summary>
        /// <param name="policyPaymentEntryId"></param>
        /// <returns></returns>
        public PolicyOutgoingPaymentListResponse GetOutgoingPaymentForCommDashboardSrvc(Guid policyPaymentEntryId, ListParams listParams)
        {
            PolicyOutgoingPaymentListResponse jres = null;
            ActionLogger.Logger.WriteLog("GetOutgoingPaymentForCommDashboardSrvc request " + policyPaymentEntryId, true);
            try
            {
                List<PolicyOutgoingPaymentObject> lst = PolicyOutgoingDistribution.GetOutgoingPaymentList(policyPaymentEntryId, listParams);

                if (lst != null && lst.Count > 0)
                {
                    jres = new PolicyOutgoingPaymentListResponse(string.Format("Data found successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                    jres.OutgoingList = lst;
                    ActionLogger.Logger.WriteLog("GetOutgoingPaymentForCommDashboardSrvc success ", true);
                }
                else
                {
                    jres = new PolicyOutgoingPaymentListResponse(string.Format("No data found "), Convert.ToInt16(ResponseCodes.RecordNotFound), "");
                    ActionLogger.Logger.WriteLog("GetOutgoingPaymentForCommDashboardSrvc 404 ", true);
                }
            }
            catch (Exception ex)
            {
                jres = new PolicyOutgoingPaymentListResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                ActionLogger.Logger.WriteLog("GetOutgoingPaymentForCommDashboardSrvc failure ", true);
            }
            return jres;
        }

        public FollowupListResponse GetCommissionIssuesForCommDashboardSrvc(Guid PolicyId)
        {
            FollowupListResponse jres = null;
            ActionLogger.Logger.WriteLog("GetOutgoingPaymentForCommDashboardSrvc request " + PolicyId, true);
            try
            {
                List<DisplayFollowupIssue> lst = CommissionDashboard.GetPolicyCommissionIssues(PolicyId);

                if (lst != null && lst.Count > 0)
                {
                    jres = new FollowupListResponse(string.Format("Data found successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                    jres.TotalRecords = lst;
                    ActionLogger.Logger.WriteLog("GetOutgoingPaymentForCommDashboardSrvc success ", true);
                }
                else
                {
                    jres = new FollowupListResponse(string.Format("No data found "), Convert.ToInt16(ResponseCodes.RecordNotFound), "");
                    ActionLogger.Logger.WriteLog("GetOutgoingPaymentForCommDashboardSrvc 404 ", true);
                }
            }
            catch (Exception ex)
            {
                jres = new FollowupListResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                ActionLogger.Logger.WriteLog("GetOutgoingPaymentForCommDashboardSrvc failure ", true);
            }
            return jres;
        }


        public JSONResponse SavePolicyPaymentEntriesService(PolicyPaymentEntriesPost policypaymententriespost)
        {
            JSONResponse jres = null;
            ActionLogger.Logger.WriteLog("SavePolicyPaymentEntriesService request: " + policypaymententriespost.ToStringDump(), true);
            try
            {
                PolicyPaymentEntriesPost.AddUpadate(policypaymententriespost);
                jres = new JSONResponse(string.Format("Payment entry saved successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                ActionLogger.Logger.WriteLog("SavePolicyPaymentEntriesService success ", true);
            }
            catch (Exception ex)
            {
                jres = new JSONResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                ActionLogger.Logger.WriteLog("SavePolicyPaymentEntriesService failure ", true);
            }
            return jres;
        }

        public JSONResponse SaveResolvedorClosedService(PolicyPaymentEntriesPost policypaymententriespost)
        {
            JSONResponse jres = null;
            ActionLogger.Logger.WriteLog("SaveResolvedorClosedService request: " + policypaymententriespost.ToStringDump(), true);
            try
            {
                PolicyPaymentEntriesPost.AddUpadateResolvedorClosed(policypaymententriespost);
                jres = new JSONResponse(string.Format("Payment entry saved and issue resolved successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                ActionLogger.Logger.WriteLog("SavePolicyPaymentEntriesService success ", true);
            }
            catch (Exception ex)
            {
                jres = new JSONResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                ActionLogger.Logger.WriteLog("SavePolicyPaymentEntriesService failure ", true);
            }
            return jres;
        }


        public JSONResponse UpadateResolvedOrClosedManualSrvc(Guid PaymentEntryID, int intId)
        {
            JSONResponse jres = null;
            ActionLogger.Logger.WriteLog("UpadateResolvedOrClosedManualSrvc request: " + PaymentEntryID + ", ID: " + intId, true);
            try
            {
                PolicyPaymentEntriesPost.UpadateResolvedOrClosedbyManualy(PaymentEntryID, intId);
                jres = new JSONResponse(string.Format("Issue status updated successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                ActionLogger.Logger.WriteLog("UpadateResolvedOrClosedManualSrvc success ", true);
            }
            catch (Exception ex)
            {
                jres = new JSONResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                ActionLogger.Logger.WriteLog("UpadateResolvedOrClosedManualSrvc failure ", true);
            }
            return jres;
        }

        public PaymentEntryListResponse GetAllResolvedorClosedIssueService(Guid? GuidPolicyId)
        {
            PaymentEntryListResponse jres = null;
            ActionLogger.Logger.WriteLog("GetAllResolvedorClosedIssueService request " + GuidPolicyId, true);
            try
            {
                List<PolicyPaymentEntriesPost> lst = PolicyPaymentEntriesPost.GetAllResolvedorClosedIssueId(GuidPolicyId);
                if (lst != null && lst.Count > 0)
                {
                    jres = new PaymentEntryListResponse(string.Format("Data found successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                    jres.PaymentsList = lst;
                    ActionLogger.Logger.WriteLog("GetAllResolvedorClosedIssueService success ", true);
                }
                else
                {
                    jres = new PaymentEntryListResponse(string.Format("No data found "), Convert.ToInt16(ResponseCodes.RecordNotFound), "");
                    ActionLogger.Logger.WriteLog("GetAllResolvedorClosedIssueService 404 ", true);
                }
            }
            catch (Exception ex)
            {
                jres = new PaymentEntryListResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                ActionLogger.Logger.WriteLog("GetAllResolvedorClosedIssueService failure ", true);
            }
            return jres;
        }
        /// <summary>
        /// Modified By :Ankit Kahndelwal
        /// ModifiedOn :10-04-2019
        /// Purpose: Used for remove a payment rntry comes from Commisssion Dashbaord.
        /// </summary>
        /// <param name="PaymentEntry"></param>
        /// <param name="_UserRole"></param>
        /// <returns></returns>
        public PostStatusResponse RemoveIncomingPaymentService(PolicyPaymentEntriesPost PaymentEntry, UserRole userRole)
        {
            PostStatusResponse jres = null;
            ActionLogger.Logger.WriteLog("RemoveIncomingPaymentService request: PaymentEntry - " + PaymentEntry.ToStringDump(), true);
            try
            {
                PostProcessReturnStatus resp = CommissionDashboard.RemoveCommissiondashBoardIncomingPayment(PaymentEntry, userRole);
                if (resp != null)
                {
                    if (resp.IsComplete)
                    {
                        jres = new PostStatusResponse(string.Format("Payment removed successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                        ActionLogger.Logger.WriteLog("RemoveIncomingPaymentService success ", true);
                        jres.PostStatus = resp;
                    }
                    else
                    {
                        jres = new PostStatusResponse(string.Format("Payment could not be removed"), Convert.ToInt16(ResponseCodes.Failure), "Payment could not be removed");
                        ActionLogger.Logger.WriteLog("RemoveIncomingPaymentService failure ", true);
                    }
                }
                else
                {
                    jres = new PostStatusResponse(string.Format("Payment could not be removed"), Convert.ToInt16(ResponseCodes.Failure), "Payment could not be removed");
                    ActionLogger.Logger.WriteLog("RemoveIncomingPaymentService failure ", true);
                }
            }
            catch (Exception ex)
            {
                jres = new PostStatusResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                ActionLogger.Logger.WriteLog("RemoveIncomingPaymentService failure ", true);
            }
            return jres;
        }

        public PostStatusResponse UnlinkIncomingPaymentService(PolicyPaymentEntriesPost PaymentEntry, UserRole userRole)
        {
            PostStatusResponse jres = null;
            ActionLogger.Logger.WriteLog("UnlinkIncomingPaymentService request: PaymentEntry - " + PaymentEntry.ToStringDump(), true);
            try
            {
                PostProcessReturnStatus resp = CommissionDashboard.UnlinkCommissiondashBoardIncomingPayment(PaymentEntry, userRole);
                if (resp != null)
                {
                    if (resp.IsComplete)
                    {
                        jres = new PostStatusResponse(string.Format("Payment unlinked successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                        ActionLogger.Logger.WriteLog("UnlinkIncomingPaymentService success ", true);
                        jres.PostStatus = resp;
                    }
                    else
                    {
                        jres = new PostStatusResponse(string.Format("Payment could not be unlinked"), Convert.ToInt16(ResponseCodes.Failure), "Payment could not be unlinked");
                        ActionLogger.Logger.WriteLog("UnlinkIncomingPaymentService failure ", true);
                    }
                }
                else
                {
                    jres = new PostStatusResponse(string.Format("Payment could not be unlinked"), Convert.ToInt16(ResponseCodes.Failure), "Payment could not be unlinked");
                    ActionLogger.Logger.WriteLog("UnlinkIncomingPaymentService failure ", true);
                }
            }
            catch (Exception ex)
            {
                jres = new PostStatusResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                ActionLogger.Logger.WriteLog("UnlinkIncomingPaymentService failure ", true);
            }
            return jres;
        }


        public PostStatusResponse CommissionDashBoardPostStartService(Guid BatchId, PolicyPaymentEntriesPost PaymentEntry, PostEntryProcess _PostEntryProcess, UserRole _UserRole)
        {
            PostStatusResponse jres = null;
            ActionLogger.Logger.WriteLog("CommissionDashBoardPostStartService request: PaymentEntry - " + PaymentEntry.ToStringDump(), true);
            try
            {
                PostProcessReturnStatus resp = CommissionDashboard.CommissionDashBoardPostStart(BatchId, PaymentEntry, _PostEntryProcess, _UserRole);
                if (resp != null)
                {
                    if (resp.IsComplete)
                    {
                        jres = new PostStatusResponse(string.Format("Payment posted successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                        ActionLogger.Logger.WriteLog("CommissionDashBoardPostStartService success ", true);
                        jres.PostStatus = resp;
                    }
                    else
                    {
                        jres = new PostStatusResponse(string.Format("Payment could not be posted"), Convert.ToInt16(ResponseCodes.Failure), "Payment could not be posted");
                        ActionLogger.Logger.WriteLog("CommissionDashBoardPostStartService failure ", true);
                    }
                }
                else
                {
                    jres = new PostStatusResponse(string.Format("Payment could not be posted"), Convert.ToInt16(ResponseCodes.Failure), "Payment could not be posted");
                    ActionLogger.Logger.WriteLog("CommissionDashBoardPostStartService failure ", true);
                }
            }
            catch (Exception ex)
            {
                jres = new PostStatusResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                ActionLogger.Logger.WriteLog("CommissionDashBoardPostStartService failure ", true);
            }
            return jres;
        }



        public JSONResponse FollowUpProcedureSrvc(FollowUpRunModules _FollowUpRunModules, DEU _DEU, Guid PolicyId, bool IsTrackPayment, bool IsEntryByCommissionDashboard, UserRole _UserRole, bool? PolicyModeChange)
        {
            JSONResponse jres = null;
            ActionLogger.Logger.WriteLog("FollowUpProcedureSrvc request: PolicyId - " + PolicyId + ",_DEU  " + _DEU.ToStringDump(), true);
            try
            {
                FollowUpUtill.FollowUpProcedure(_FollowUpRunModules, _DEU, PolicyId, IsTrackPayment, IsEntryByCommissionDashboard, _UserRole, PolicyModeChange);

                jres = new JSONResponse(string.Format("Follow up procedure ran successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                ActionLogger.Logger.WriteLog("FollowUpProcedureSrvc success ", true);
            }
            catch (Exception ex)
            {
                jres = new JSONResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                ActionLogger.Logger.WriteLog("FollowUpProcedureSrvc failure ", true);
            }
            return jres;
        }

        public PolicyIncomingPaymentListResponse GetPaymentEntryPolicyIdWiseSrvc(Guid policyId, ListParams listParams)
        {
            PolicyIncomingPaymentListResponse jres = null;
            ActionLogger.Logger.WriteLog("GetPaymentEntryPolicyIdWiseSrvc request " + policyId, true);
            try
            {
                List<PolicyIncomingPaymentObject> lst = PolicyPaymentEntriesPost.GetPolicyIncomingPaymentList(policyId, listParams);

                if (lst != null && lst.Count > 0)
                {
                    jres = new PolicyIncomingPaymentListResponse(string.Format("Data found successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                    jres.TotalRecords = lst;

                    //IEnumerable<Object> data = new IEnumerable<lst>();
                    string[] keysName = new string[] { "DollerPerUnit", "Fee", "TotalPayment", "PaymentRecived" };
                    List<PolicyIncomingPaymentObject> t = new List<PolicyIncomingPaymentObject>(lst);
                    //jres.ExportDataList = CommonMethods.ConvertListToExcelFormat(  keysName,t);
                    jres.TotalLength = lst.Count();
                    ActionLogger.Logger.WriteLog("GetPaymentEntryPolicyIDWiseSrvc success ", true);
                }
                else
                {
                    jres = new PolicyIncomingPaymentListResponse(string.Format("No data found "), Convert.ToInt16(ResponseCodes.RecordNotFound), "");
                    ActionLogger.Logger.WriteLog("GetPaymentEntryPolicyIDWiseSrvc 404 ", true);
                }
            }
            catch (Exception ex)
            {
                jres = new PolicyIncomingPaymentListResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                ActionLogger.Logger.WriteLog("GetPaymentEntryPolicyIDWiseSrvc failure ", true);
            }
            return jres;

        }

        public PostStatusResponse CommissionDashBoardPostStartClientSrvc(PolicyDetailsData SelectedPolicy, PolicyPaymentEntriesPost PaymentEntry, PostEntryProcess _PostEntryProcess, UserRole _UserRole, bool isInvoiceEdited = false)
        {
            PostStatusResponse jres = null;
            ActionLogger.Logger.WriteLog("CommissionDashBoardPostStartClientSrvc request: PolicyId - " + SelectedPolicy + ",PaymentEntry  " + PaymentEntry.ToStringDump(), true);
            try
            {
                PostProcessReturnStatus resp = CommissionDashboard.CommissionDashBoardPostStartClienVMWrapper(SelectedPolicy, PaymentEntry, _PostEntryProcess, _UserRole, isInvoiceEdited);
                if (resp != null)
                {
                    if (resp.IsComplete)
                    {
                        jres = new PostStatusResponse(string.Format("Payment posted successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                        ActionLogger.Logger.WriteLog("CommissionDashBoardPostStartClientSrvc success ", true);
                        jres.PostStatus = resp;
                    }
                    else
                    {
                        jres = new PostStatusResponse(string.Format("Payment could not be posted"), Convert.ToInt16(ResponseCodes.Failure), "Payment could not be posted");
                        ActionLogger.Logger.WriteLog("CommissionDashBoardPostStartClientSrvc faiure ", true);
                    }
                }
                else
                {
                    jres = new PostStatusResponse(string.Format("Payment could not be posted"), Convert.ToInt16(ResponseCodes.Failure), "Payment could not be posted");
                    ActionLogger.Logger.WriteLog("CommissionDashBoardPostStartClientSrvc faiure ", true);
                }
            }
            catch (Exception ex)
            {
                jres = new PostStatusResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                ActionLogger.Logger.WriteLog("CommissionDashBoardPostStartClientSrvc failure ", true);
            }
            return jres;
        }

        #endregion
    }

}