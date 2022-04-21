using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ServiceModel;
using MyAgencyVault.WcfService.Library.Response;
using MyAgencyVault.BusinessLibrary;
using MyAgencyVault.EmailFax;
using System.ServiceModel.Web;

namespace MyAgencyVault.WcfService
{
    [ServiceContract]
    interface IFollowupService
    {
        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        JSONResponse UpdateIssuesDetailsService(DisplayFollowupIssue DisplayFollowupIssueObject, int previousStatusId, Guid modifiedBy);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        FollowupIssueDetailResponse GetFollowUpIssueDetailService(Guid issueId);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        JSONResponse SaveNotesDetail(Guid IssueID, Guid PayorID, Guid AgencyID, string IssueNotes, string PayorNotes, string AgencyPayorNotes);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        FollowupManagerListResponse GetIssueListForWeb(ListRequestObjectWeb request, ListParams listParams);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        FollowupManagerNotesResponse GetNotesDetail(Guid IssueID, Guid PayorID, Guid AgencyID);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        FollowupPolicyResponse GetFewIssueForCommissionDashBoardService(Guid PolicyId, ListParams_Issues listParams);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        JSONResponse RemoveCommisionIssueService(Guid issueId);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        FollowupListResponse GetIssuesService(Guid PolicyID);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        FollowupListResponse GetAllIssuesService(int Status, Guid PayorID, Guid AgencyID, bool Followup);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        IssueDetailResponse GetIssueDetailService(Guid PolicyId, Guid FollowUpIssueID);


        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        JSONResponse AddUpdatePolicyIssueNotesService(DisplayFollowupIssue followupIssue);

        //[OperationContract] Duplicate code of AddUpdatePolicyIssueNotes
        //[WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        //void AddUpdatePolicyIssueNotesScr(Guid ModifiedBy, Guid IssueId, string PolicyIssueNote);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        JSONResponse AddUpdatePolicyIssuePaymentService(DisplayFollowupIssue followupIssue);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        JSONResponse AddUpdateIssueService(DisplayFollowupIssue followupIssues);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        JSONResponse AddUpdateFollowUpIssuesService(DisplayFollowupIssue FollowupIssues, PolicyPaymentEntriesPost Policypaymententriespost);


        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        JSONResponse AddPaymentDataService(Guid IssueId, decimal Payment);


        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        FollowupInPaymentResponse GetIncomingPaymentService(Guid PolicyId);

        /*  Found not in use 
         * [OperationContract]
          [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
          PolicyStringResponse GetIssuesNoteService(Guid IssueID);
         * */
        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        JSONResponse SendNotificationMailService(MailData _MailData, string strSubject, string strMailBody);


        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        FollowupPayorContactsResponse GetPayorContactService(Guid PolicyId);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        JSONResponse FollowUpSettingService(string name, string value);

        /*
         * Found unused in app
                  [OperationContract]
                  [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
                  void EmailToAgencyPayor(MailData _MailData);

                  [OperationContract]
                  [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
                  void SendCloseStatemant(MailData _MailData, string strMailBody);

                  [OperationContract]
                  [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
                  void SendMailToCloseBatch(MailData _MailData, string strBatchNumber, string strMailBody);

                  [OperationContract]
                  [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
                  void SendMailOfCarrierProduct(MailData _MailData, string strMailBody);

                  [OperationContract]
                  [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
                  void SendMailToUpload(MailData _MailData, string strMailBody);

                  [OperationContract]
                  [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
                  void SendRemainderMail(MailData _MailData, string strMailBody, DateTime dtCutOfDay);

                  [OperationContract]
                  [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
                  void SendLoginLogoutMail(MailData _MailData, string strLoginLogoutType, string strMailBody);


                  [OperationContract]
                  [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
                  void SendLinkedPolicyConfirmationMail(MailData _MailData, string strMailBody, string strPendingPolicy, string strActivePolicy);
              * 
              *   [OperationContract]
             [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
             MasterIssuesOption FillMasterIssueOptions();

                  */
        //[OperationContract]
        //[WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        //FollowupListResponse GetFewIssueAccordingtoModeService(int Status, Guid PayorID, Guid AgencyID, bool Followup, int intDays);

        //[OperationContract]
        //[WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        //FollowupListResponse GetFewIssueAccordingtoModeScrService(int Status, Guid PayorID, Guid AgencyID, bool Followup, int intDays);



        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        JSONResponse UpdateIssuesSrcService(DisplayFollowupIssue DisplayFollowupIssueObject, int PreviousStatusId, Guid ModifiedBy);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        IssueIDResponse GetFollowUpIssueForPaymentEntryService(PolicyPaymentEntriesPost PaymentEntry);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        JSONResponse DeleteIssueService(Guid IssueID);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        PolicyBoolResponse IsFollowUpLicenseeService(Guid LicenseeId);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        JSONResponse AddUpadateResolvedorClosedService(PolicyPaymentEntriesPost PolicyPaymentEntries);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        JSONResponse UpadateResolvedOrClosedManuallyService(Guid PaymentEntryID, int intId);
    }

    public partial class MavService : IFollowupService
    {
        /// <summary>
        /// CreatedBy:Ankit khandelwal
        /// Cretedon:03-July-2020
        ///Purpose:Getting details of follow up issue
        /// </summary>
        /// <param name="issueId"></param>
        /// <returns></returns>UpdateIssuesSrcService
        public FollowupIssueDetailResponse GetFollowUpIssueDetailService(Guid issueId)
        {
            FollowupIssueDetailResponse response = null;
            try
            {
                FollowupIssueDetail details = FollowupIssue.GetFollowupIssueDetails(issueId);
                response = new FollowupIssueDetailResponse("Follow up issue details found successfully", Convert.ToInt16(ResponseCodes.Success), "");
                response.FollowupDetails = details;
            }
            catch (Exception ex)
            {
                response = new FollowupIssueDetailResponse("Exception occurs while processing", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                ActionLogger.Logger.WriteLog("GetFollowUpIssueDetailService: Exception occurs while process " + ex.Message, true);
            }
            return response;
        }
        /// <summary>
        /// CreatedBy:Ankit khandelwal
        /// CreatedOn:03-07-2020
        /// Purpose:Update  followup issue details
        /// </summary>
        /// <param name="DisplayFollowupIssue"></param>
        /// <param name="previousStatusId"></param>
        /// <param name="modifiedBy"></param>
        /// <returns></returns>
        public JSONResponse UpdateIssuesDetailsService(DisplayFollowupIssue DisplayFollowupIssue, int previousStatusId, Guid modifiedBy)
        {
            JSONResponse jres = null;
            ActionLogger.Logger.WriteLog("UpdateIssuesDetailsService: processing begins with DisplayFollowupIssue : " + DisplayFollowupIssue.ToStringDump() + ", prevStatus: " + previousStatusId, true);
            try
            {
                FollowupIssue.UpdateFollowUpIssuesDetails(DisplayFollowupIssue, previousStatusId, modifiedBy);
                jres = new JSONResponse(string.Format("Issue status updated successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                ActionLogger.Logger.WriteLog("UpdateIssuesDetailsService: processing completed", true);
            }
            catch (Exception ex)
            {
                jres = new JSONResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                ActionLogger.Logger.WriteLog("UpdateIssuesDetailsService:Exception occurs while processing" + DisplayFollowupIssue.IssueId, true);
            }
            return jres;
        }

        public JSONResponse UpadateResolvedOrClosedManuallyService(Guid PaymentEntryID, int intId)
        {
            JSONResponse jres = null;
            ActionLogger.Logger.WriteLog("UpadateResolvedOrClosedManuallyService request: " + PaymentEntryID + ", ID" + intId, true);
            try
            {
                PolicyPaymentEntriesPost.UpadateResolvedOrClosedbyManualy(PaymentEntryID, intId);
                jres = new JSONResponse(string.Format("Commission issue updated successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                ActionLogger.Logger.WriteLog("UpadateResolvedOrClosedManuallyService success ", true);
            }
            catch (Exception ex)
            {
                jres = new JSONResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                ActionLogger.Logger.WriteLog("UpadateResolvedOrClosedManuallyService failure ", true);
            }
            return jres;
        }

        public JSONResponse AddUpadateResolvedorClosedService(PolicyPaymentEntriesPost policypaymententriespost)
        {
            JSONResponse jres = null;
            ActionLogger.Logger.WriteLog("AddUpadateResolvedorClosedService request: " + policypaymententriespost.ToStringDump(), true);
            try
            {
                PolicyPaymentEntriesPost.AddUpadateResolvedorClosed(policypaymententriespost);
                jres = new JSONResponse(string.Format("Commission issue updated successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                ActionLogger.Logger.WriteLog("AddUpadateResolvedorClosedService success ", true);
            }
            catch (Exception ex)
            {
                jres = new JSONResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                ActionLogger.Logger.WriteLog("AddUpadateResolvedorClosedService failure ", true);
            }
            return jres;
        }

        public PolicyBoolResponse IsFollowUpLicenseeService(Guid LicenseeId)
        {
            PolicyBoolResponse jres = null;
            ActionLogger.Logger.WriteLog("IsFollowUpLicenseeService request: " + LicenseeId, true);
            try
            {
                bool isValid = BillingLineDetail.IsFollowUpLicensee(LicenseeId);
                jres = new PolicyBoolResponse(string.Format("Followup information found successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                jres.BoolFlag = isValid;
                ActionLogger.Logger.WriteLog("IsFollowUpLicenseeService success ", true);
            }
            catch (Exception ex)
            {
                jres = new PolicyBoolResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                ActionLogger.Logger.WriteLog("IsFollowUpLicenseeService failure ", true);
            }
            return jres;
        }


        public JSONResponse AddPaymentDataService(Guid IssueId, decimal Payment)
        {
            JSONResponse jres = null;
            ActionLogger.Logger.WriteLog("AddPaymentDataService request: " + IssueId, true);
            try
            {
                FollowupIssue.AddPaymentData(IssueId, Payment);
                jres = new JSONResponse(string.Format("Payment data added successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                ActionLogger.Logger.WriteLog("AddPaymentDataService success ", true);
            }
            catch (Exception ex)
            {
                jres = new JSONResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                ActionLogger.Logger.WriteLog("AddPaymentDataService failure ", true);
            }
            return jres;
        }
        /// <summary>
        /// Modified By:Ankit Khandelwal
        /// Modified On:02-04-2018
        /// </summary>
        /// <param name="IssueID"></param>
        /// <returns></returns>
        public JSONResponse RemoveCommisionIssueService(Guid issueId)
        {
            JSONResponse jres = null;
            ActionLogger.Logger.WriteLog("RemoveCommisionIssueService request: " + issueId, true);
            try
            {
                FollowupIssue objFollowupIssue = new FollowupIssue();
                objFollowupIssue.RemoveCommisionIssue(issueId);
                jres = new JSONResponse(string.Format("Commission issue removed successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                ActionLogger.Logger.WriteLog("RemoveCommisionIssueService success ", true);
            }
            catch (Exception ex)
            {
                jres = new JSONResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                ActionLogger.Logger.WriteLog("RemoveCommisionIssueService failure ", true);
            }
            return jres;
        }

        public FollowupListResponse GetIssuesService(Guid policyID)
        {

            FollowupListResponse jres = null;
            ActionLogger.Logger.WriteLog("GetIssuesService request: " + policyID, true);
            try
            {
                List<DisplayFollowupIssue> lst = FollowupIssue.GetIssues(policyID);

                if (lst != null && lst.Count > 0)
                {
                    jres = new FollowupListResponse(string.Format("Issues list retreived successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                    jres.TotalRecords = lst;
                    ActionLogger.Logger.WriteLog("GetIssuesService success ", true);
                }
                else
                {
                    jres = new FollowupListResponse("No issues found!", Convert.ToInt16(ResponseCodes.RecordNotFound), "");
                    ActionLogger.Logger.WriteLog("GetAllIssues 404 ", true);
                }
            }
            catch (Exception ex)
            {
                jres = new FollowupListResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                ActionLogger.Logger.WriteLog("GetIssuesService failure ", true);
            }
            return jres;
        }

        public IssueIDResponse GetFollowUpIssueForPaymentEntryService(PolicyPaymentEntriesPost PaymentEntry)
        {
            IssueIDResponse jres = null;
            ActionLogger.Logger.WriteLog("GetFollowUpIssueForPaymentEntry request: " + PaymentEntry.ToStringDump(), true);
            try
            {
                DisplayFollowupIssue issue = FollowupIssue.GetFollowupissueOfPayment(PaymentEntry);
                if (issue != null)
                {
                    Guid? id = issue.IssueId;
                    if (id != null)
                    {
                        jres = new IssueIDResponse(string.Format("Issues ID found successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                        jres.IssueID = id;
                    }
                    else
                    {
                        jres = new IssueIDResponse(string.Format("Issues ID not found"), Convert.ToInt16(ResponseCodes.RecordNotFound), "");
                    }
                }
                else
                {
                    jres = new IssueIDResponse(string.Format("Issues ID not found"), Convert.ToInt16(ResponseCodes.RecordNotFound), "");
                }
            }
            catch (Exception ex)
            {
                jres = new IssueIDResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                ActionLogger.Logger.WriteLog("GetFollowUpIssueForPaymentEntry failure ", true);
            }
            return jres;
        }

        public FollowupListResponse GetAllIssuesService(int Status, Guid PayorID, Guid AgencyID, bool Followup)
        {
            FollowupListResponse jres = null;
            ActionLogger.Logger.WriteLog("GetAllIssues request: " + Status + ", payorID: " + PayorID, true);
            try
            {
                List<DisplayFollowupIssue> lst = FollowupIssue.GetAllIssues(Status, PayorID, AgencyID, Followup, 180);

                if (lst != null && lst.Count > 0)
                {
                    jres = new FollowupListResponse(string.Format("Issues list retreived successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                    jres.TotalRecords = lst;
                    ActionLogger.Logger.WriteLog("GetAllIssues success ", true);
                }
                else
                {
                    jres = new FollowupListResponse("No issues found!", Convert.ToInt16(ResponseCodes.RecordNotFound), "");
                    ActionLogger.Logger.WriteLog("GetAllIssues 404 ", true);
                }
            }
            catch (Exception ex)
            {
                jres = new FollowupListResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                ActionLogger.Logger.WriteLog("GetAllIssues failure ", true);
            }
            return jres;
        }
        public JSONResponse FollowUpSettingService(string name, string value)
        {
            JSONResponse jres = null;
            ActionLogger.Logger.WriteLog("FollowUpSettingService:Processing starts:name " + name + ",value:" + value, true);
            try
            {
                PayorSource.UpdateFollowUpDateAndserviceStatus(name, value);
                jres = new JSONResponse("FollowUpSetting update successfully", Convert.ToInt16(ResponseCodes.Success), "");
            }
            catch (Exception ex)
            {
                jres = new JSONResponse("FollowUpSetting not updated successfully", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                ActionLogger.Logger.WriteLog("FollowUpSettingService:Exception occur while update the details name" + name + ",value" + value , true);
            }
            return jres;
        }
        public IssueDetailResponse GetIssueDetailService(Guid PolicyId, Guid FollowUpIssueid)
        {
            IssueDetailResponse jres = null;
            ActionLogger.Logger.WriteLog("GetIssueDetailService request: " + PolicyId + ", FollowUpIssueid: " + FollowUpIssueid, true);
            try
            {
                List<IssuePolicyDetail> lst = FollowupIssue.GetIssueDetail(PolicyId, FollowUpIssueid);
                if (lst != null && lst.Count > 0)
                {
                    jres = new IssueDetailResponse(string.Format("Issues list retreived successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                    jres.FollowupIssueList = lst;
                    ActionLogger.Logger.WriteLog("GetIssueDetailService success ", true);
                }
                else
                {
                    jres = new IssueDetailResponse("No issues found!", Convert.ToInt16(ResponseCodes.RecordNotFound), "");
                    ActionLogger.Logger.WriteLog("GetIssueDetailService 404 ", true);
                }
            }
            catch (Exception ex)
            {
                jres = new IssueDetailResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                ActionLogger.Logger.WriteLog("GetIssueDetailService failure ", true);
            }
            return jres;
        }

        /*  public MasterIssuesOption FillMasterIssueOptions()
          {
              return FollowupIssue.FillMasterIssueOptions();
          }*/


        public FollowupInPaymentResponse GetIncomingPaymentService(Guid PolicyId)
        {
            FollowupInPaymentResponse jres = null;
            ActionLogger.Logger.WriteLog("GetIncomingPaymentService request: " + PolicyId, true);
            try
            {
                List<FollowupIncomingPament> lst = FollowupIssue.GetIncomingPayment(PolicyId);

                if (lst != null && lst.Count > 0)
                {
                    jres = new FollowupInPaymentResponse(string.Format("Incoming payment list retreived successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                    jres.FollowupInPaymentList = lst;
                    jres.Count = lst.Count();
                    ActionLogger.Logger.WriteLog("GetIncomingPaymentService success ", true);
                }
                else
                {
                    jres = new FollowupInPaymentResponse("No issues found!", Convert.ToInt16(ResponseCodes.RecordNotFound), "");
                    ActionLogger.Logger.WriteLog("GetIncomingPaymentService 404 ", true);
                }
            }
            catch (Exception ex)
            {
                jres = new FollowupInPaymentResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                ActionLogger.Logger.WriteLog("GetIncomingPaymentService failure ", true);
            }
            return jres;
        }

        /*Found not in use 
         * public PolicyStringResponse GetIssuesNoteService(Guid IssueID)
        {
            PolicyStringResponse jres = null;
            ActionLogger.Logger.WriteLog("GetIssuesNoteService request: " + IssueID, true);
            try
            {
                string s = FollowupIssue.GetIssuesNote(IssueID);
                if (!string.IsNullOrEmpty(s))
                {
                    jres = new PolicyStringResponse(string.Format("Policy issue notes found successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                    jres.StringValue = s;
                    ActionLogger.Logger.WriteLog("GetIssuesNoteService success ", true);
                }
                else
                {
                    jres = new PolicyStringResponse(string.Format("Policy issue notes not found"), Convert.ToInt16(ResponseCodes.RecordNotFound), "");
                    ActionLogger.Logger.WriteLog("GetIssuesNoteService 404 ", true);
                }
            }
            catch (Exception ex)
            {
                jres = new PolicyStringResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                ActionLogger.Logger.WriteLog("GetIssuesNoteService failure ", true);
            }
            return jres;
        }*/

        public FollowupPayorContactsResponse GetPayorContactService(Guid PolicyId)
        {
            FollowupPayorContactsResponse jres = null;
            ActionLogger.Logger.WriteLog("GetPayorContactService request: " + PolicyId, true);
            try
            {
                List<FollowUPPayorContacts> lst = FollowupIssue.GetPayorContact(PolicyId);
                if (lst != null && lst.Count > 0)
                {
                    jres = new FollowupPayorContactsResponse(string.Format("Payor contacts found successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                    jres.FollowupPayorContactsList = lst;
                    ActionLogger.Logger.WriteLog("GetPayorContactService success ", true);
                }
                else
                {
                    jres = new FollowupPayorContactsResponse(string.Format("No payor contacts found"), Convert.ToInt16(ResponseCodes.RecordNotFound), "");
                    ActionLogger.Logger.WriteLog("GetPayorContactService 404 ", true);
                }
            }
            catch (Exception ex)
            {
                jres = new FollowupPayorContactsResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                ActionLogger.Logger.WriteLog("GetPayorContactService failure ", true);
            }
            return jres;
        }

        public JSONResponse AddUpdatePolicyIssueNotesService(DisplayFollowupIssue followupiss)
        {
            JSONResponse jres = null;
            ActionLogger.Logger.WriteLog("AddUpdatePolicyIssueNotesService request: " + followupiss.ToString(), true);
            try
            {
                FollowupIssue.AddUpdatePolicyIssueNotes(followupiss);
                jres = new JSONResponse(string.Format("Policy issue notes updated successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                ActionLogger.Logger.WriteLog("AddUpdatePolicyIssueNotesService success ", true);
            }
            catch (Exception ex)
            {
                jres = new JSONResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                ActionLogger.Logger.WriteLog("AddUpdatePolicyIssueNotesService failure ", true);
            }
            return jres;
        }

        public JSONResponse AddUpdatePolicyIssuePaymentService(DisplayFollowupIssue followupiss)
        {
            JSONResponse jres = null;
            ActionLogger.Logger.WriteLog("AddUpdatePolicyIssuePaymentService request: " + followupiss.ToString(), true);
            try
            {
                FollowupIssue.AddUpdatePolicyIssuePayment(followupiss);
                jres = new JSONResponse(string.Format("Policy issue payment saved successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                ActionLogger.Logger.WriteLog("AddUpdatePolicyIssuePaymentService success ", true);
            }
            catch (Exception ex)
            {
                jres = new JSONResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                ActionLogger.Logger.WriteLog("AddUpdatePolicyIssuePaymentService failure ", true);
            }
            return jres;
        }
        /// <summary>
        /// Modified By:Ankit Khandelwal
        /// Modified On :02-04-2018
        /// </summary>
        /// <param name="followupIssues"></param>
        /// <returns></returns>
        public JSONResponse AddUpdateIssueService(DisplayFollowupIssue followupIssues)
        {
            JSONResponse jres = null;
            ActionLogger.Logger.WriteLog("AddUpdateIssueService request: " + followupIssues.ToString(), true);
            try
            {
                FollowupIssue.AddUpdate(followupIssues);
                jres = new JSONResponse(string.Format("Issue details saved successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                ActionLogger.Logger.WriteLog("AddUpdateIssueService success ", true);
            }
            catch (Exception ex)
            {
                jres = new JSONResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                ActionLogger.Logger.WriteLog("AddUpdateIssueService failure ", true);
            }
            return jres;
        }
        public JSONResponse AddUpdateFollowUpIssuesService(DisplayFollowupIssue FollowupIssues, PolicyPaymentEntriesPost Policypaymententriespost)
        {
            JSONResponse jres = null;
            ActionLogger.Logger.WriteLog("AddUpdateIssueService request: " + FollowupIssues.ToString(), true);
            try
            {
                FollowupIssue.AddUpdateFollowUpIssues(FollowupIssues);
                // PolicyPaymentEntriesPost.AddUpadateResolvedorClosed(Policypaymententriespost);
                jres = new JSONResponse(_resourceManager.GetString("FollowUpIssuesSuccess"), Convert.ToInt16(ResponseCodes.Success), "");
                ActionLogger.Logger.WriteLog("AddUpdateIssueService success ", true);
            }
            catch (Exception ex)
            {
                jres = new JSONResponse(_resourceManager.GetString("FollowUpissuesFailure"), Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                ActionLogger.Logger.WriteLog("AddUpdateIssueService: Exception occurs while processing " + ex.Message, true);
            }
            return jres;
        }

        #region IFollowupIssue Members

        /*
         * Following found not used in app, so ignoring for now
         * 
                public void EmailToAgencyPayor(MailData _MailData)
                {
                    FollowupIssue objFollowupIssue = new FollowupIssue();
                    objFollowupIssue.EmailToAgencyPayor(_MailData);
                }

                public void SendCloseStatemant(MailData _MailData, string strMailBody)
                {
                    FollowupIssue objFollowupIssue = new FollowupIssue();
                    objFollowupIssue.SendCloseStatemant(_MailData, strMailBody);
                }

                public void SendMailToCloseBatch(MailData _MailData, string strBatchNumber, string strMailBody)
                {
                    FollowupIssue objFollowupIssue = new FollowupIssue();
                    objFollowupIssue.SendMailToCloseBatch(_MailData, strBatchNumber, strMailBody);
                }

                public void SendLoginLogoutMail(MailData _MailData, string strLoginLogoutType, string strMailBody)
                {
                    FollowupIssue objFollowupIssue = new FollowupIssue();
                    objFollowupIssue.SendLoginLogoutMail(_MailData, strLoginLogoutType, strMailBody);
                }

                public void SendMailOfCarrierProduct(MailData _MailData, string strMailBody)
                {
                    FollowupIssue objFollowupIssue = new FollowupIssue();
                    objFollowupIssue.SendMailOfCarrierProduct(_MailData, strMailBody);
                }

                public void SendMailToUpload(MailData _MailData, string strMailBody)
                {
                    FollowupIssue objFollowupIssue = new FollowupIssue();
                    objFollowupIssue.SendMailToUpload(_MailData, strMailBody);
                }

                public void SendRemainderMail(MailData _MailData, string strMailBody, DateTime dtCutOfDay)
                {
                    FollowupIssue objFollowupIssue = new FollowupIssue();
                    objFollowupIssue.SendRemainderMail(_MailData, strMailBody, dtCutOfDay);
                }


                public void SendLinkedPolicyConfirmationMail(MailData _MailData, string strMailBody, string strPendingPolicy, string strActivePolicy)
                {
                    FollowupIssue objFollowupIssue = new FollowupIssue();
                    objFollowupIssue.SendLinkedPolicyConfirmationMail(_MailData, strMailBody, strPendingPolicy, strActivePolicy);
                }
                */

        public JSONResponse SendNotificationMailService(MailData _MailData, string strSubject, string strMailBody)
        {
            JSONResponse jres = null;
            ActionLogger.Logger.WriteLog("SendNotificationMailService request: " + _MailData, true);
            try
            {
                FollowupIssue objFollowupIssue = new FollowupIssue();
                objFollowupIssue.SendNotificationMail(_MailData, strSubject, strMailBody);
                jres = new JSONResponse(string.Format("Mail sent successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                ActionLogger.Logger.WriteLog("SendNotificationMailService success ", true);
            }
            catch (Exception ex)
            {
                jres = new JSONResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                ActionLogger.Logger.WriteLog("SendNotificationMailService failure ", true);
            }
            return jres;
        }

        #region FollowupManager Web

        public JSONResponse SaveNotesDetail(Guid IssueID, Guid PayorID, Guid AgencyID, string IssueNotes, string PayorNotes, string AgencyPayorNotes)
        {
            JSONResponse jres = null;
            ActionLogger.Logger.WriteLog("SaveNotesDetail request: " + IssueID + ", payorID: " + PayorID, true);
            try
            {
                FollowupIssue.SaveNotesFromWeb(IssueID, PayorID, AgencyID, IssueNotes, PayorNotes, AgencyPayorNotes);
                jres = new JSONResponse(string.Format("Notes saved successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                ActionLogger.Logger.WriteLog("SaveNotesDetail success ", true);
            }
            catch (Exception ex)
            {
                jres = new JSONResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                ActionLogger.Logger.WriteLog("SaveNotesDetail failure ", true);
            }
            return jres;
        }
        public FollowupManagerNotesResponse GetNotesDetail(Guid IssueID, Guid PayorID, Guid AgencyID)
        {
            FollowupManagerNotesResponse jres = null;
            ActionLogger.Logger.WriteLog("GetNotesDetail request: " + IssueID + ", payorID: " + PayorID, true);
            try
            {

                List<LicenseeNote> lstNotes = LicenseeNote.GetLicenseeNotes(AgencyID);
                jres = new FollowupManagerNotesResponse(string.Format("Notes list retreived successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                jres.TotalRecords = lstNotes;
                jres.TotalLength = lstNotes.Count;
                jres.IssueNotes = FollowupIssue.GetIssuesNote(IssueID);
                PayorSource source = PayorSource.GetPayorSource(PayorID, AgencyID);
                jres.PayorNotes = source == null ? "" : source.Notes;
                jres.AgencyPayorNotes = source == null ? "" : source.ConfigNotes;

                ActionLogger.Logger.WriteLog("GetNotesDetail success ", true);

            }
            catch (Exception ex)
            {
                jres = new FollowupManagerNotesResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                ActionLogger.Logger.WriteLog("GetNotesDetail failure ", true);
            }
            return jres;
        }
        public FollowupManagerListResponse GetIssueListForWeb(ListRequestObjectWeb request, ListParams listParams)
        {
            FollowupManagerListResponse jres = null;
            ActionLogger.Logger.WriteLog("GetIssueListForWeb request: " + request.ToStringDump(), true);
            try
            {
                int total = 0;
                List<FollowupIssueListObject> lst = FollowupIssue.GetIssueListForWeb(request, listParams, out total);

                if (lst != null && lst.Count > 0)
                {
                    jres = new FollowupManagerListResponse(string.Format("Issues list retreived successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                    jres.TotalRecords = lst;
                    jres.TotalLength = total;
                    ActionLogger.Logger.WriteLog("GetIssueListForWeb success ", true);
                }
                else
                {
                    jres = new FollowupManagerListResponse("No issues found!", Convert.ToInt16(ResponseCodes.RecordNotFound), "");
                    ActionLogger.Logger.WriteLog("GetIssueListForWeb 404 ", true);
                }
            }
            catch (Exception ex)
            {
                jres = new FollowupManagerListResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                ActionLogger.Logger.WriteLog("GetIssueListForWeb failure ", true);
            }
            return jres;
        }

        #endregion

        public FollowupListResponse GetFewIssueAccordingtoModeScrService(int Status, Guid PayorID, Guid AgencyID, bool Followup, int intDays)
        {
            FollowupListResponse jres = null;
            ActionLogger.Logger.WriteLog("GetFewIssueAccordingtoModeScrService request: " + Status + ", payorID: " + PayorID, true);
            try
            {
                List<DisplayFollowupIssue> lst = FollowupIssue.GetFewIssueAccordingtoModeForFollowupScr(Status, PayorID, AgencyID, Followup, intDays);

                if (lst != null && lst.Count > 0)
                {
                    jres = new FollowupListResponse(string.Format("Issues list retreived successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                    jres.TotalRecords = lst;
                    jres.TotalLength = lst.Count;
                    ActionLogger.Logger.WriteLog("GetFewIssueAccordingtoModeScrService success ", true);
                }
                else
                {
                    jres = new FollowupListResponse("No issues found!", Convert.ToInt16(ResponseCodes.RecordNotFound), "");
                    ActionLogger.Logger.WriteLog("GetFewIssueAccordingtoModeScrService 404 ", true);
                }
            }
            catch (Exception ex)
            {
                jres = new FollowupListResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                ActionLogger.Logger.WriteLog("GetFewIssueAccordingtoModeScrService failure ", true);
            }
            return jres;

        }
        /// <summary>
        /// Modified By:Ankit Khandelwal
        /// Modified on:02-04-2018
        /// </summary>
        /// <param name="PolicyId"></param>
        /// <param name="listParams"></param>
        /// <returns></returns>
        public FollowupPolicyResponse GetFewIssueForCommissionDashBoardService(Guid PolicyId, ListParams_Issues listParams)
        {
            FollowupPolicyResponse jres = null;
            ActionLogger.Logger.WriteLog("GetFewIssueForCommissionDashBoard request: " + PolicyId, true);
            try
            {
                //List<DisplayFollowupIssue> lst = FollowupIssue.GetFewIssueForCommissionDashBoard(PolicyId, listParams);
                int totalLength = 0;
                List<FollowupIssuePolicyListObject> lst = FollowupIssue.GetPolicyIssues_Web(PolicyId, listParams, out totalLength);
                if (lst != null && lst.Count > 0)
                {
                    jres = new FollowupPolicyResponse(string.Format("Issues list retreived successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                    jres.FollowupIssueList = lst;
                    jres.TotalLength = totalLength;
                    ActionLogger.Logger.WriteLog("GetFewIssueForCommissionDashBoard success ", true);
                }
                else
                {
                    jres = new FollowupPolicyResponse("No issues found!", Convert.ToInt16(ResponseCodes.RecordNotFound), "");
                    ActionLogger.Logger.WriteLog("GetFewIssueForCommissionDashBoard 404 ", true);
                }
            }
            catch (Exception ex)
            {
                jres = new FollowupPolicyResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                ActionLogger.Logger.WriteLog("GetFewIssueForCommissionDashBoard failure ", true);
            }
            return jres;
        }

        public JSONResponse DeleteIssueService(Guid IssueId)
        {
            JSONResponse jres = null;
            ActionLogger.Logger.WriteLog("DeleteIssueService request: " + IssueId, true);
            try
            {
                FollowupIssue.DeleteIssue(IssueId);
                jres = new JSONResponse(string.Format("Issue deleted successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                ActionLogger.Logger.WriteLog("DeleteIssueService success ", true);
            }
            catch (Exception ex)
            {
                jres = new JSONResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                ActionLogger.Logger.WriteLog("DeleteIssueService failure ", true);
            }
            return jres;
        }

        #endregion

        #region IFollowupIssue Members

        //Commenting as duplicate code of AddUpdatePolicyIssueNotes
        //public void AddUpdatePolicyIssueNotesScr(Guid ModifiedBy, Guid IssueId, string PolicyIssueNote)
        //{
        //    FollowupIssue.AddUpdatePolicyIssueNotesScr(ModifiedBy, IssueId, PolicyIssueNote);
        //}

        #endregion

        #region IFollowupIssue Members
        public JSONResponse UpdateIssuesSrcService(DisplayFollowupIssue DisplayFollowupIssue, int PreviousStatusId, Guid ModifiedBy)
        {
            JSONResponse jres = null;
            ActionLogger.Logger.WriteLog("UpdateIssuesSrcService request: " + DisplayFollowupIssue.ToStringDump() + ", prevStatus: " + PreviousStatusId, true);
            try
            {
                FollowupIssue.UpdateIssuesSrc(DisplayFollowupIssue, PreviousStatusId, ModifiedBy);
                jres = new JSONResponse(string.Format("Issue status updated successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                ActionLogger.Logger.WriteLog("UpdateIssuesSrcService success ", true);
            }
            catch (Exception ex)
            {
                jres = new JSONResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                ActionLogger.Logger.WriteLog("UpdateIssuesSrcService failure ", true);
            }
            return jres;
        }

        #endregion
    }
}