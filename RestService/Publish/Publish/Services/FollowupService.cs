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
        FollowupListResponse GetFewIssueForCommissionDashBoardService(Guid PolicyId);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        JSONResponse RemoveCommisionIssueService(Guid IssueID);

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
        JSONResponse AddUpdateIssueService(DisplayFollowupIssue followupIssue);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        JSONResponse AddPaymentDataService(Guid IssueId, decimal Payment);


        /*     
         * 
         *   [OperationContract]
             [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
             List<FollowupIncomingPament> GetIncomingPayment(Guid PolicyId);

         * 
         * [OperationContract]
             [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
             string GetIssuesNote(Guid IssueID);

             [OperationContract]
             [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
             List<FollowUPPayorContacts> GetPayorContact(Guid PolicyId);


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
             void SendNotificationMail(MailData _MailData, string strSubject, string strMailBody);

             [OperationContract]
             [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
             void SendLinkedPolicyConfirmationMail(MailData _MailData, string strMailBody, string strPendingPolicy, string strActivePolicy);
         * 
         *   [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        MasterIssuesOption FillMasterIssueOptions();

             */
        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        FollowupListResponse  GetFewIssueAccordingtoModeService(int Status, Guid PayorID, Guid AgencyID, bool Followup, int intDays);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        FollowupListResponse GetFewIssueAccordingtoModeScrService(int Status, Guid PayorID, Guid AgencyID, bool Followup, int intDays);

   
      
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

        public JSONResponse RemoveCommisionIssueService(Guid IssueID)
        {
            JSONResponse jres = null;
            ActionLogger.Logger.WriteLog("RemoveCommisionIssueService request: " + IssueID, true);
            try
            {
                FollowupIssue objFollowupIssue = new FollowupIssue();
                objFollowupIssue.RemoveCommisionIssue(IssueID);
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
                List<DisplayFollowupIssue> lst =   FollowupIssue.GetIssues(policyID);

                if (lst != null && lst.Count > 0)
                {
                    jres = new FollowupListResponse(string.Format("Issues list retreived successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                    jres.FollowupIssueList = lst;
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
                    jres.FollowupIssueList = lst;
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

        public IssueDetailResponse  GetIssueDetailService(Guid PolicyId, Guid FollowUpIssueid)
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
        }


        public List<FollowupIncomingPament> GetIncomingPayment(Guid PolicyId)
        {
            return FollowupIssue.GetIncomingPayment(PolicyId);
        }

        public string GetIssuesNote(Guid IssueID)
        {
            return FollowupIssue.GetIssuesNote(IssueID);
        }

        public List<FollowUPPayorContacts> GetPayorContact(Guid PolicyId)
        {
            return FollowupIssue.GetPayorContact(PolicyId);
        }
        */
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

        public JSONResponse AddUpdateIssueService(DisplayFollowupIssue followupiss)
        {
            JSONResponse jres = null;
            ActionLogger.Logger.WriteLog("AddUpdateIssueService request: " + followupiss.ToString(), true);
            try
            {
                FollowupIssue.AddUpdate(followupiss);
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
        #region IFollowupIssue Members

/*
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

        public void SendNotificationMail(MailData _MailData, string strSubject, string strMailBody)
        {
            FollowupIssue objFollowupIssue = new FollowupIssue();
            objFollowupIssue.SendNotificationMail(_MailData, strSubject, strMailBody);
        }

        public void SendLinkedPolicyConfirmationMail(MailData _MailData, string strMailBody, string strPendingPolicy, string strActivePolicy)
        {
            FollowupIssue objFollowupIssue = new FollowupIssue();
            objFollowupIssue.SendLinkedPolicyConfirmationMail(_MailData, strMailBody, strPendingPolicy, strActivePolicy);
        }
        */
        public FollowupListResponse GetFewIssueAccordingtoModeService(int Status, Guid PayorID, Guid AgencyID, bool Followup, int intDays)
        {
            FollowupListResponse jres = null;
            ActionLogger.Logger.WriteLog("GetFewIssueAccordingtoModeService request: " + Status + ", payorID: " + PayorID, true);
            try
            {
                List<DisplayFollowupIssue> lst =  FollowupIssue.GetFewIssueAccordingtoMode(Status, PayorID, AgencyID, Followup, intDays);

                if (lst != null && lst.Count > 0)
                {
                    jres = new FollowupListResponse(string.Format("Issues list retreived successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                    jres.FollowupIssueList = lst;
                    ActionLogger.Logger.WriteLog("GetFewIssueAccordingtoModeService success ", true);
                }
                else
                {
                    jres = new FollowupListResponse("No issues found!", Convert.ToInt16(ResponseCodes.RecordNotFound), "");
                    ActionLogger.Logger.WriteLog("GetFewIssueAccordingtoModeService 404 ", true);
                }
            }
            catch (Exception ex)
            {
                jres = new FollowupListResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                ActionLogger.Logger.WriteLog("GetFewIssueAccordingtoModeService failure ", true);
            }
            return jres;
        }

        public FollowupListResponse GetFewIssueAccordingtoModeScrService(int Status, Guid PayorID, Guid AgencyID, bool Followup, int intDays)
        {
            FollowupListResponse jres = null;
            ActionLogger.Logger.WriteLog("GetFewIssueAccordingtoModeScrService request: " + Status + ", payorID: " + PayorID, true);
            try
            {
                List<DisplayFollowupIssue> lst =  FollowupIssue.GetFewIssueAccordingtoModeForFollowupScr(Status, PayorID, AgencyID, Followup, intDays);
        
                if (lst != null && lst.Count > 0)
                {
                    jres = new FollowupListResponse(string.Format("Issues list retreived successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                    jres.FollowupIssueList = lst;
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

        public FollowupListResponse GetFewIssueForCommissionDashBoardService(Guid PolicyId)
        {
            FollowupListResponse jres = null;
            ActionLogger.Logger.WriteLog("GetFewIssueForCommissionDashBoard request: " + PolicyId, true);
            try
            {
                List<DisplayFollowupIssue> lst = FollowupIssue.GetFewIssueForCommissionDashBoard(PolicyId);
                if (lst != null && lst.Count > 0)
                {
                    jres = new FollowupListResponse(string.Format("Issues list retreived successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                    jres.FollowupIssueList = lst;
                    ActionLogger.Logger.WriteLog("GetFewIssueForCommissionDashBoard success ", true);
                }
                else
                {
                    jres = new FollowupListResponse("No issues found!", Convert.ToInt16(ResponseCodes.RecordNotFound), "");
                    ActionLogger.Logger.WriteLog("GetFewIssueForCommissionDashBoard 404 ", true);
                }
            }
            catch (Exception ex)
            {
                jres = new FollowupListResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
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
            ActionLogger.Logger.WriteLog("UpdateIssuesSrcService request: " + DisplayFollowupIssue.ToStringDump() + ", prevStatus: " +PreviousStatusId, true);
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