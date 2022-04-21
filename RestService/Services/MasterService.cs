using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyAgencyVault.BusinessLibrary.Masters;
using System.ServiceModel;
using MyAgencyVault.BusinessLibrary;
using System.Runtime.Serialization;
using System.ServiceModel.Web;
using MyAgencyVault.WcfService.Library.Response;

namespace MyAgencyVault.WcfService
{
    [ServiceContract]
    interface IMasterService
    {
        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        ExportDateResponse GetExportDateService();

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        FileTypeListResponse GetSupportedFileTypeListService();

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        BatchDownloadListResponse GetBatchDownloadStatusService();

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        LicenseeStatusResponse GetLicenseeStatusListService();

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        PayorIncomingListResponse GetPayorToolIncomingFieldListService();

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        PayorLearnedListResponse GetPayorToolLearnedFieldListService();

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        PolicyIncomingPaymentResponse GetPolicyIncomingPaymentTypeListService();

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        PolicyIncomingScheduleListResponse GetPolicyIncomingScheduleTypeListService();

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        PolicyModeResponse GetPolicyModeListService();

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        PolicyModeResponse GetPolicyModeListWithBlankAddedService();

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        PolicyModeResponse GetPolicyModeByIDService(int ModeID);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        PolicyOutgoingScheduleTypeResponse GetPolicyOutgoingScheduleTypeListService();

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        PolicyStatusListResponse GetPolicyStatusListService();

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        PolicyTerminationReasonListResponse GetTerminationReasonListService();

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        PolicyTerminationReasonListResponse GetTerminationReasonListWithBlankAddedService();

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        QuestionListResponse GetQuestionsService();

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        RegionListResponse GetRegionListService();

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        SystemConstantListResponse GetSystemConstantsService();

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        ZipListResponse GetZipListService();

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        ZipObjectResponse GetZipService(string zipcode);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        PayorToolMaskedFieldTypeListResponse GetPayorToolMaskedFieldListService();

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        PolicyStringResponse GetMaskNameService(int id);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        PolicyStringResponse GetSystemConstantKeyValueService(string key);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        PolicyDetailMasterDataListResponse GetPolicyDetailMasterDataService();

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        JSONResponse AddLogService(string message);

        //[OperationContract]
        //TempFolderDetails GetTempFolderSetting();

        #region Follow up issues listing 
        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        IssueReasonResponse GetReasonsService(int ReasonsID);
        // List<IssueReasons> GetReasons(int ReasonsID);
        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        IssueReasonListResponse GetAllReasonService();
        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        IssueStatusResponse GetStatusService(int StatusID);
        // List<IssueStatus> GetStatus(int StatusID);
        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        IssueStatusListResponse GetAllStatusService();
        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        IssueCategoryListResponse GetAllCategoryService();

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        IssueResultListResponse GetAllResultService();
        #endregion
    }


    public partial class MavService : IMasterService
    {
        public ExportDateResponse GetExportDateService()
        {
            ExportDateResponse jres = null;
            ActionLogger.Logger.WriteLog("GetExportDateService request: ", true);
            try
            {
                ExportDate obj = ExportDate.getExportDate();
                if (obj != null)
                {
                    jres = new ExportDateResponse(string.Format("Export date found successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                    jres.ExportDateObject = obj;
                }
                else
                {
                    jres = new ExportDateResponse(string.Format("No export date found"), Convert.ToInt16(ResponseCodes.RecordNotFound), "");
                }

                ActionLogger.Logger.WriteLog("GetExportDateService success ", true);
            }
            catch (Exception ex)
            {
                jres = new ExportDateResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                ActionLogger.Logger.WriteLog("GetExportDateService failure ", true);
            }
            return jres;
        }

        //public TempFolderDetails GetTempFolderSetting()
        //{
        //    TempFolderDetails obj = new TempFolderDetails();
        //    obj.AllowDelete = Convert.ToBoolean(System.Configuration.ConfigurationSettings.AppSettings["AllowTempFolderDelete"]);
        //    obj.FileSizeToBeDeleted = Convert.ToString(System.Configuration.ConfigurationSettings.AppSettings["TempFolderDeleteSizeInGB"]);
        //    return obj;
        //}

        public JSONResponse AddLogService(string message)
        {
            JSONResponse jres = null;
            //  ActionLogger.Logger.WriteLog("AddUpdatePayorUserWebSiteService request: " + plSiteinfo.ToStringDump(), true);
            try
            {
                ActionLogger.Logger.WriteLog(message, true);
                jres = new JSONResponse(string.Format("Payor site saved successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                //   ActionLogger.Logger.WriteLog("AddUpdatePayorUserWebSiteService success ", true);
            }
            catch (Exception ex)
            {
                jres = new JSONResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                //    ActionLogger.Logger.WriteLog("AddUpdatePayorUserWebSiteService failure ", true);
            }
            return jres;
        }

        public PolicyDetailMasterDataListResponse GetPolicyDetailMasterDataService()
        {
            PolicyDetailMasterDataListResponse jres = null;
            ActionLogger.Logger.WriteLog("GetPolicyDetailMasterDataService request: ", true);
            try
            {
                PolicyDetailMasterData obj = Policy.GetPolicyDetailMasterData();

                if (obj != null)
                {
                    List<PolicyDetailMasterData> lst = new List<PolicyDetailMasterData>();
                    lst.Add(obj);

                    jres = new PolicyDetailMasterDataListResponse(string.Format("Policy list found successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                    jres.PolicyDetailMasterDataList = lst;
                }
                else
                {
                    jres = new PolicyDetailMasterDataListResponse(string.Format("No policy master data found"), Convert.ToInt16(ResponseCodes.RecordNotFound), "");
                }

                ActionLogger.Logger.WriteLog("GetPolicyDetailMasterDataService success ", true);
            }
            catch (Exception ex)
            {
                jres = new PolicyDetailMasterDataListResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                ActionLogger.Logger.WriteLog("GetPolicyDetailMasterDataService failure ", true);
            }
            return jres;
        }

        public FileTypeListResponse GetSupportedFileTypeListService()
        {
            FileTypeListResponse jres = null;
            ActionLogger.Logger.WriteLog("GetSupportedFileTypeListService request: ", true);
            try
            {
                List<FileType> lst = FileType.GetSupportedFileTypeList();
                if (lst != null && lst.Count > 0)
                {
                    jres = new FileTypeListResponse(string.Format("File type list found successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                    jres.FileTypeList = lst;
                }
                else
                {
                    jres = new FileTypeListResponse(string.Format("No file types found"), Convert.ToInt16(ResponseCodes.RecordNotFound), "");
                }

                ActionLogger.Logger.WriteLog("GetSupportedFileTypeListService success ", true);
            }
            catch (Exception ex)
            {
                jres = new FileTypeListResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                ActionLogger.Logger.WriteLog("GetSupportedFileTypeListService failure ", true);
            }
            return jres;
        }

        public LicenseeStatusResponse GetLicenseeStatusListService()
        {
            LicenseeStatusResponse jres = null;
            ActionLogger.Logger.WriteLog("GetLicenseeStatusList request: ", true);
            try
            {
                List<LicenseeStatus> lst = LicenseeStatus.GetLicenseeStatusList();
                if (lst != null && lst.Count > 0)
                {
                    jres = new LicenseeStatusResponse(string.Format("Licensee status list found successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                    jres.LicenseeStatusList = lst;
                }
                else
                {
                    jres = new LicenseeStatusResponse(string.Format("No LicenseeStatus data found"), Convert.ToInt16(ResponseCodes.RecordNotFound), "");
                }

                ActionLogger.Logger.WriteLog("GetLicenseeStatusList success ", true);
            }
            catch (Exception ex)
            {
                jres = new LicenseeStatusResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                ActionLogger.Logger.WriteLog("GetLicenseeStatusList failure ", true);
            }
            return jres;
        }

        public PayorIncomingListResponse GetPayorToolIncomingFieldListService()
        {
            PayorIncomingListResponse jres = null;
            ActionLogger.Logger.WriteLog("GetPayorToolIncomingFieldList request: ", true);
            try
            {
                List<PayorToolIncomingFieldType> lst = PayorToolIncomingFieldType.GetFieldList();
                if (lst != null && lst.Count > 0)
                {
                    jres = new PayorIncomingListResponse(string.Format("Payor tool incoming field list found successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                    jres.PayorIncomingList = lst;
                    ActionLogger.Logger.WriteLog("GetPayorToolIncomingFieldList success ", true);
                }
                else
                {
                    jres = new PayorIncomingListResponse(string.Format("No payor tool incoming field list found"), Convert.ToInt16(ResponseCodes.RecordNotFound), "");
                    ActionLogger.Logger.WriteLog("GetPayorToolIncomingFieldList 404 ", true);
                }
            }
            catch (Exception ex)
            {
                jres = new PayorIncomingListResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                ActionLogger.Logger.WriteLog("GetPayorToolIncomingFieldList failure ", true);
            }
            return jres;
        }

        public BatchDownloadListResponse GetBatchDownloadStatusService()
        {
            BatchDownloadListResponse jres = null;
            ActionLogger.Logger.WriteLog("GetBatchDownloadStatusService request: ", true);
            try
            {
                List<BatchDownloadStatus> lst = BatchDownloadStatus.GetBatchDownloadStatus();
                if (lst != null && lst.Count > 0)
                {
                    jres = new BatchDownloadListResponse(string.Format("Batch download status list found successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                    jres.BatchDownloadList = lst;
                    ActionLogger.Logger.WriteLog("GetBatchDownloadStatusService success ", true);
                }
                else
                {
                    jres = new BatchDownloadListResponse(string.Format("No batch download status list found"), Convert.ToInt16(ResponseCodes.RecordNotFound), "");
                    ActionLogger.Logger.WriteLog("GetBatchDownloadStatusService 404 ", true);
                }
            }
            catch (Exception ex)
            {
                jres = new BatchDownloadListResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                ActionLogger.Logger.WriteLog("GetBatchDownloadStatusService failure ", true);
            }
            return jres;

        }

        public PayorLearnedListResponse GetPayorToolLearnedFieldListService()
        {
            PayorLearnedListResponse jres = null;
            ActionLogger.Logger.WriteLog("GetPayorToolLearnedFieldListService request: ", true);
            try
            {
                List<PayorToolLearnedlFieldType> lst = PayorToolLearnedlFieldType.GetFieldList();
                if (lst != null && lst.Count > 0)
                {
                    jres = new PayorLearnedListResponse(string.Format("Payortool learned fields list found successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                    jres.PayorLearnedList = lst;
                    ActionLogger.Logger.WriteLog("GetPayorToolLearnedFieldListService success ", true);
                }
                else
                {
                    jres = new PayorLearnedListResponse(string.Format("No Payortool learned fields list found"), Convert.ToInt16(ResponseCodes.RecordNotFound), "");
                    ActionLogger.Logger.WriteLog("GetPayorToolLearnedFieldListService 404 ", true);
                }
            }
            catch (Exception ex)
            {
                jres = new PayorLearnedListResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                ActionLogger.Logger.WriteLog("GetPayorToolLearnedFieldListService failure ", true);
            }
            return jres;
        }

        public PolicyIncomingPaymentResponse GetPolicyIncomingPaymentTypeListService()
        {
            PolicyIncomingPaymentResponse jres = null;
            ActionLogger.Logger.WriteLog("GetPolicyIncomingPaymentTypeListService request: ", true);
            try
            {
                List<PolicyIncomingPaymentType> lst = PolicyIncomingPaymentType.GetIncomingPaymentTypeList();
                if (lst != null && lst.Count > 0)
                {
                    jres = new PolicyIncomingPaymentResponse(string.Format("Policy incoming payment list found successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                    jres.PolicyIncomingPaymentList = lst;
                    ActionLogger.Logger.WriteLog("GetPolicyIncomingPaymentTypeListService success ", true);
                }
                else
                {
                    jres = new PolicyIncomingPaymentResponse(string.Format("No policy incoming payment fields list found"), Convert.ToInt16(ResponseCodes.RecordNotFound), "");
                    ActionLogger.Logger.WriteLog("GetPolicyIncomingPaymentTypeListService 404 ", true);
                }
            }
            catch (Exception ex)
            {
                jres = new PolicyIncomingPaymentResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                ActionLogger.Logger.WriteLog("GetPolicyIncomingPaymentTypeListService failure ", true);
            }
            return jres;
        }

        public PolicyIncomingScheduleListResponse GetPolicyIncomingScheduleTypeListService()
        {
            PolicyIncomingScheduleListResponse jres = null;
            ActionLogger.Logger.WriteLog("GetPolicyIncomingPaymentTypeListService request: ", true);
            try
            {
                List<PolicyIncomingScheduleType> lst = PolicyIncomingScheduleType.GetIncomingScheduleTypeList();
                if (lst != null && lst.Count > 0)
                {
                    jres = new PolicyIncomingScheduleListResponse(string.Format("Policy incoming schedule list found successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                    jres.PolicyIncomingScheduleList = lst;
                    ActionLogger.Logger.WriteLog("GetPolicyIncomingPaymentTypeListService success ", true);
                }
                else
                {
                    jres = new PolicyIncomingScheduleListResponse(string.Format("No policy incoming schedule list found"), Convert.ToInt16(ResponseCodes.RecordNotFound), "");
                    ActionLogger.Logger.WriteLog("GetPolicyIncomingPaymentTypeListService 404 ", true);
                }
            }
            catch (Exception ex)
            {
                jres = new PolicyIncomingScheduleListResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                ActionLogger.Logger.WriteLog("GetPolicyIncomingPaymentTypeListService failure ", true);
            }
            return jres;
        }

        public PolicyModeResponse GetPolicyModeListService()
        {
            PolicyModeResponse jres = null;
            ActionLogger.Logger.WriteLog("GetPolicyModeListService request: ", true);
            try
            {
                List<PolicyMode> lst = PolicyMode.GetPolicyModeList();
                if (lst != null && lst.Count > 0)
                {
                    jres = new PolicyModeResponse(string.Format("Policy mode list found successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                    jres.PolicyModeList = lst;
                    ActionLogger.Logger.WriteLog("GetPolicyModeListService success ", true);
                }
                else
                {
                    jres = new PolicyModeResponse(string.Format("No policy mode list found"), Convert.ToInt16(ResponseCodes.RecordNotFound), "");
                    ActionLogger.Logger.WriteLog("GetPolicyModeListService 404 ", true);
                }
            }
            catch (Exception ex)
            {
                jres = new PolicyModeResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                ActionLogger.Logger.WriteLog("GetPolicyModeListService failure ", true);
            }
            return jres;
        }

        public PolicyModeResponse GetPolicyModeListWithBlankAddedService()
        {
            PolicyModeResponse jres = null;
            ActionLogger.Logger.WriteLog("GetPolicyModeListWithBlankAddedService request: ", true);
            try
            {
                List<PolicyMode> lst = PolicyMode.GetPolicyModeListWithBlankAdded();
                if (lst != null && lst.Count > 0)
                {
                    jres = new PolicyModeResponse(string.Format("Policy mode list found successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                    jres.PolicyModeList = lst;
                    ActionLogger.Logger.WriteLog("GetPolicyModeListWithBlankAddedService success ", true);
                }
                else
                {
                    jres = new PolicyModeResponse(string.Format("No policy mode list found"), Convert.ToInt16(ResponseCodes.RecordNotFound), "");
                    ActionLogger.Logger.WriteLog("GetPolicyModeListWithBlankAddedService 404 ", true);
                }
            }
            catch (Exception ex)
            {
                jres = new PolicyModeResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                ActionLogger.Logger.WriteLog("GetPolicyModeListWithBlankAddedService failure ", true);
            }
            return jres;
        }

        public PolicyModeResponse GetPolicyModeByIDService(int ModeID)
        {
            PolicyModeResponse jres = null;
            ActionLogger.Logger.WriteLog("GetPolicyModeByIDService request: ", true);
            try
            {
                PolicyMode obj = PolicyMode.GetPolicyModeByID(ModeID);
                if (obj != null)
                {
                    List<PolicyMode> lst = new List<PolicyMode>();
                    lst.Add(obj);
                    jres = new PolicyModeResponse(string.Format("Policy mode list found successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                    jres.PolicyModeList = lst;
                    ActionLogger.Logger.WriteLog("GetPolicyModeByIDService success ", true);
                }
                else
                {
                    jres = new PolicyModeResponse(string.Format("No policy mode list found"), Convert.ToInt16(ResponseCodes.RecordNotFound), "");
                    ActionLogger.Logger.WriteLog("GetPolicyModeByIDService 404 ", true);
                }
            }
            catch (Exception ex)
            {
                jres = new PolicyModeResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                ActionLogger.Logger.WriteLog("GetPolicyModeByIDService failure ", true);
            }
            return jres;
        }

        public PolicyOutgoingScheduleTypeResponse GetPolicyOutgoingScheduleTypeListService()
        {
            PolicyOutgoingScheduleTypeResponse jres = null;
            ActionLogger.Logger.WriteLog("GetPolicyOutgoingScheduleTypeListService request: ", true);
            try
            {
                List<PolicyOutgoingScheduleType> lst = PolicyOutgoingScheduleType.GetOutgoingScheduleTypeList();
                if (lst != null && lst.Count > 0)
                {
                    jres = new PolicyOutgoingScheduleTypeResponse(string.Format("Policy mode list found successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                    jres.PolicyOutgoingScheduleTypeList = lst;
                    ActionLogger.Logger.WriteLog("GetPolicyOutgoingScheduleTypeListService success ", true);
                }
                else
                {
                    jres = new PolicyOutgoingScheduleTypeResponse(string.Format("No policy mode list found"), Convert.ToInt16(ResponseCodes.RecordNotFound), "");
                    ActionLogger.Logger.WriteLog("GetPolicyOutgoingScheduleTypeListService 404 ", true);
                }
            }
            catch (Exception ex)
            {
                jres = new PolicyOutgoingScheduleTypeResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                ActionLogger.Logger.WriteLog("GetPolicyOutgoingScheduleTypeListService failure ", true);
            }
            return jres;
        }

        public PolicyStatusListResponse GetPolicyStatusListService()
        {
            PolicyStatusListResponse jres = null;
            ActionLogger.Logger.WriteLog("GetPolicyStatusListService request: ", true);
            try
            {
                List<PolicyStatus> lst = PolicyStatus.GetPolicyStatusList();
                if (lst != null && lst.Count > 0)
                {
                    jres = new PolicyStatusListResponse(string.Format("Policy status list found successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                    jres.PolicyStatusList = lst;
                    ActionLogger.Logger.WriteLog("GetPolicyStatusListService success ", true);
                }
                else
                {
                    jres = new PolicyStatusListResponse(string.Format("No policy status list found"), Convert.ToInt16(ResponseCodes.RecordNotFound), "");
                    ActionLogger.Logger.WriteLog("GetPolicyStatusListService 404 ", true);
                }
            }
            catch (Exception ex)
            {
                jres = new PolicyStatusListResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                ActionLogger.Logger.WriteLog("GetPolicyStatusListService failure ", true);
            }
            return jres;
        }

        public PolicyTerminationReasonListResponse GetTerminationReasonListService()
        {
            PolicyTerminationReasonListResponse jres = null;
            ActionLogger.Logger.WriteLog("GetTerminationReasonListService request: ", true);
            try
            {
                List<PolicyTerminationReason> lst = PolicyTerminationReason.GetTerminationReasonList();
                if (lst != null && lst.Count > 0)
                {
                    jres = new PolicyTerminationReasonListResponse(string.Format("Policy termination list found successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                    jres.PolicyTerminationReasonList = lst;
                    ActionLogger.Logger.WriteLog("GetTerminationReasonListService success ", true);
                }
                else
                {
                    jres = new PolicyTerminationReasonListResponse(string.Format("No policy termination list found"), Convert.ToInt16(ResponseCodes.RecordNotFound), "");
                    ActionLogger.Logger.WriteLog("GetTerminationReasonListService 404 ", true);
                }
            }
            catch (Exception ex)
            {
                jres = new PolicyTerminationReasonListResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                ActionLogger.Logger.WriteLog("GetTerminationReasonListService failure ", true);
            }
            return jres;
        }

        public PolicyTerminationReasonListResponse GetTerminationReasonListWithBlankAddedService()
        {
            PolicyTerminationReasonListResponse jres = null;
            ActionLogger.Logger.WriteLog("GetTerminationReasonListWithBlankAddedService request: ", true);
            try
            {
                List<PolicyTerminationReason> lst = PolicyTerminationReason.GetTerminationReasonListWithBlankAdded();
                if (lst != null && lst.Count > 0)
                {
                    jres = new PolicyTerminationReasonListResponse(string.Format("Policy termination list found successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                    jres.PolicyTerminationReasonList = lst;
                    ActionLogger.Logger.WriteLog("GetTerminationReasonListWithBlankAddedService success ", true);
                }
                else
                {
                    jres = new PolicyTerminationReasonListResponse(string.Format("No policy termination list found"), Convert.ToInt16(ResponseCodes.RecordNotFound), "");
                    ActionLogger.Logger.WriteLog("GetTerminationReasonListWithBlankAddedService  404 ", true);
                }
            }
            catch (Exception ex)
            {
                jres = new PolicyTerminationReasonListResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                ActionLogger.Logger.WriteLog("GetTerminationReasonListWithBlankAddedService failure ", true);
            }
            return jres;
        }

        public QuestionListResponse GetQuestionsService()
        {
            QuestionListResponse jres = null;
            ActionLogger.Logger.WriteLog("GetQuestionsService request: ", true);
            try
            {
                List<Question> lst = Question.GetQuestions();
                if (lst != null && lst.Count > 0)
                {
                    jres = new QuestionListResponse(string.Format("Questions list found successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                    jres.QuestionList = lst;
                    ActionLogger.Logger.WriteLog("GetQuestionsService success ", true);
                }
                else
                {
                    jres = new QuestionListResponse(string.Format("No question list found"), Convert.ToInt16(ResponseCodes.RecordNotFound), "");
                    ActionLogger.Logger.WriteLog("GetQuestionsService  404 ", true);
                }
            }
            catch (Exception ex)
            {
                jres = new QuestionListResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                ActionLogger.Logger.WriteLog("GetQuestionsService failure ", true);
            }
            return jres;
        }

        public RegionListResponse GetRegionListService()
        {
            RegionListResponse jres = null;
            ActionLogger.Logger.WriteLog("GetRegionListService request: ", true);
            try
            {
                List<Region> lst = Region.GetRegionList();
                if (lst != null && lst.Count > 0)
                {
                    jres = new RegionListResponse(string.Format("Region list found successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                    jres.RegionList = lst;
                    ActionLogger.Logger.WriteLog("GetRegionListService success ", true);
                }
                else
                {
                    jres = new RegionListResponse(string.Format("No region list found"), Convert.ToInt16(ResponseCodes.RecordNotFound), "");
                    ActionLogger.Logger.WriteLog("GetRegionListService  404 ", true);
                }
            }
            catch (Exception ex)
            {
                jres = new RegionListResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                ActionLogger.Logger.WriteLog("GetRegionListService failure ", true);
            }
            return jres;
        }

        public SystemConstantListResponse GetSystemConstantsService()
        {
            SystemConstantListResponse jres = null;
            ActionLogger.Logger.WriteLog("GetSystemConstantsService request: ", true);
            try
            {
                List<SystemConstant> lst = SystemConstant.GetSystemConstants();
                if (lst != null && lst.Count > 0)
                {
                    jres = new SystemConstantListResponse(string.Format("System constants list found successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                    jres.SystemConstantList = lst;
                    ActionLogger.Logger.WriteLog("GetSystemConstantsService success ", true);
                }
                else
                {
                    jres = new SystemConstantListResponse(string.Format("No system constants list found"), Convert.ToInt16(ResponseCodes.RecordNotFound), "");
                    ActionLogger.Logger.WriteLog("GetSystemConstantsService  404 ", true);
                }
            }
            catch (Exception ex)
            {
                jres = new SystemConstantListResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                ActionLogger.Logger.WriteLog("GetSystemConstantsService failure ", true);
            }
            return jres;
        }

        public ZipListResponse GetZipListService()
        {
            ZipListResponse jres = null;
            ActionLogger.Logger.WriteLog("GetZipListService request: ", true);
            try
            {
                List<Zip> lst = Zip.GetZipList();
                if (lst != null && lst.Count > 0)
                {
                    jres = new ZipListResponse(string.Format("Zip list found successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                    jres.ZipList = lst;
                    ActionLogger.Logger.WriteLog("GetZipListService success ", true);
                }
                else
                {
                    jres = new ZipListResponse(string.Format("No zip list found"), Convert.ToInt16(ResponseCodes.RecordNotFound), "");
                    ActionLogger.Logger.WriteLog("GetZipListService  404 ", true);
                }
            }
            catch (Exception ex)
            {
                jres = new ZipListResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                ActionLogger.Logger.WriteLog("GetZipListService failure ", true);
            }
            return jres;
        }

        public PayorToolMaskedFieldTypeListResponse GetPayorToolMaskedFieldListService()
        {
            PayorToolMaskedFieldTypeListResponse jres = null;
            ActionLogger.Logger.WriteLog("GetPayorToolMaskedFieldListService request: ", true);
            try
            {
                List<PayorToolMaskedFieldType> lst = PayorToolMaskedFieldType.GetFieldList();
                if (lst != null && lst.Count > 0)
                {
                    jres = new PayorToolMaskedFieldTypeListResponse(string.Format("Payor tool masked field type list found successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                    jres.PayorToolMaskedFieldTypeList = lst;
                    ActionLogger.Logger.WriteLog("GetPayorToolMaskedFieldListService success ", true);
                }
                else
                {
                    jres = new PayorToolMaskedFieldTypeListResponse(string.Format("No payor tool masked field type list found"), Convert.ToInt16(ResponseCodes.RecordNotFound), "");
                    ActionLogger.Logger.WriteLog("GetPayorToolMaskedFieldListService  404 ", true);
                }
            }
            catch (Exception ex)
            {
                jres = new PayorToolMaskedFieldTypeListResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                ActionLogger.Logger.WriteLog("GetPayorToolMaskedFieldListService failure ", true);
            }
            return jres;
        }

        public PolicyStringResponse GetMaskNameService(int id)
        {
            PolicyStringResponse jres = null;
            ActionLogger.Logger.WriteLog("GetMaskNameService request: " + id, true);
            try
            {
                string s = PayorToolMaskedFieldType.GetMaskName(id);
                if (!string.IsNullOrEmpty(s))
                {
                    jres = new PolicyStringResponse(string.Format("Mask name found successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                    jres.StringValue = s;
                    ActionLogger.Logger.WriteLog("GetMaskNameService success ", true);
                }
                else
                {
                    jres = new PolicyStringResponse(string.Format("Mask name not found"), Convert.ToInt16(ResponseCodes.RecordNotFound), "");
                    ActionLogger.Logger.WriteLog("GetMaskNameService  404 ", true);
                }
            }
            catch (Exception ex)
            {
                jres = new PolicyStringResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                ActionLogger.Logger.WriteLog("GetMaskNameService failure ", true);
            }
            return jres;
        }
        /// <summary>
        /// Used by:Ankit khandelwal
        /// Purpose:Getting details of follow up setting
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public PolicyStringResponse GetSystemConstantKeyValueService(string key)
        {
            PolicyStringResponse jres = null;
            ActionLogger.Logger.WriteLog("GetSystemConstantKeyValueService request: " + key, true);
            try
            {
                string s = SystemConstant.GetKeyValue(key);
                if (!string.IsNullOrEmpty(s))
                {
                    jres = new PolicyStringResponse(string.Format("System constant key found successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                    jres.StringValue = s;
                    ActionLogger.Logger.WriteLog("GetSystemConstantKeyValueService success ", true);
                }
                else
                {
                    jres = new PolicyStringResponse(string.Format("System constant key not found"), Convert.ToInt16(ResponseCodes.RecordNotFound), "");
                    ActionLogger.Logger.WriteLog("GetSystemConstantKeyValueService  404 ", true);
                }
            }
            catch (Exception ex)
            {
                jres = new PolicyStringResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                ActionLogger.Logger.WriteLog("GetSystemConstantKeyValueService failure ", true);
            }
            return jres;
        }

        #region IMasters Members


        public ZipObjectResponse GetZipService(string zipcode)
        {
            ZipObjectResponse jres = null;
            ActionLogger.Logger.WriteLog("GetZipService request: " + zipcode, true);
            try
            {
                Zip z = Zip.GetZip(zipcode);
                if (z != null)
                {
                    jres = new ZipObjectResponse(string.Format("Zip details found successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                    jres.ZipObject = z;
                    ActionLogger.Logger.WriteLog("GetZipService success ", true);
                }
                else
                {
                    jres = new ZipObjectResponse(string.Format("Zip details not found"), Convert.ToInt16(ResponseCodes.RecordNotFound), "");
                    ActionLogger.Logger.WriteLog("GetZipService  404 ", true);
                }
            }
            catch (Exception ex)
            {
                jres = new ZipObjectResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                ActionLogger.Logger.WriteLog("GetZipService failure ", true);
            }
            return jres;
        }

        #endregion


        #region Issue Reason/Status 
        public IssueStatusResponse GetStatusService(int StatusID)
        {
            IssueStatusResponse jres = null;
            ActionLogger.Logger.WriteLog("GetStatusService request: " + StatusID, true);
            try
            {
                IssueStatus obj = IssueStatus.GetStatus(StatusID);
                if (obj != null)
                {
                    jres = new IssueStatusResponse(string.Format("Issue status found successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                    jres.IssueStatusObject = obj;
                    ActionLogger.Logger.WriteLog("GetStatusService success ", true);
                }
                else
                {
                    jres = new IssueStatusResponse(string.Format("Issue status not found "), Convert.ToInt16(ResponseCodes.RecordNotFound), "");
                    ActionLogger.Logger.WriteLog("GetStatusService  404 ", true);
                }
            }
            catch (Exception ex)
            {
                jres = new IssueStatusResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                ActionLogger.Logger.WriteLog("GetStatusService failure ", true);
            }
            return jres;
        }


        public IssueCategoryListResponse GetAllCategoryService()
        {
            IssueCategoryListResponse jres = null;
            ActionLogger.Logger.WriteLog("GetAllCategoryService request: ", true);
            try
            {
                List<IssueCategory> lst = IssueCategory.GetAllCategory();
                if (lst != null && lst.Count > 0)
                {
                    jres = new IssueCategoryListResponse(string.Format("Issues category list found successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                    jres.IssueCategoryList = lst;
                    ActionLogger.Logger.WriteLog("GetAllCategoryService success ", true);
                }
                else
                {
                    jres = new IssueCategoryListResponse(string.Format("No category list found"), Convert.ToInt16(ResponseCodes.RecordNotFound), "");
                    ActionLogger.Logger.WriteLog("GetAllCategoryService  404 ", true);
                }
            }
            catch (Exception ex)
            {
                jres = new IssueCategoryListResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                ActionLogger.Logger.WriteLog("GetAllCategoryService failure ", true);
            }
            return jres;
        }
        public IssueResultListResponse GetAllResultService()
        {
            IssueResultListResponse jres = null;
            ActionLogger.Logger.WriteLog("GetAllCategoryService request: ", true);
            try
            {
                List<IssueResults> lst = IssueResults.GetAllResults();
                if (lst != null && lst.Count > 0)
                {
                    jres = new IssueResultListResponse(string.Format("Issues result list found successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                    jres.IssueResultList  = lst;
                    ActionLogger.Logger.WriteLog("GetAllResultService success ", true);
                }
                else
                {
                    jres = new IssueResultListResponse(string.Format("No result list found"), Convert.ToInt16(ResponseCodes.RecordNotFound), "");
                    ActionLogger.Logger.WriteLog("GetAllResultService  404 ", true);
                }
            }
            catch (Exception ex)
            {
                jres = new IssueResultListResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                ActionLogger.Logger.WriteLog("GetAllResultService failure ", true);
            }
            return jres;
        }
        public IssueStatusListResponse GetAllStatusService()
        {
            IssueStatusListResponse jres = null;
            ActionLogger.Logger.WriteLog("GetAllStatusService request: ", true);
            try
            {
                List<IssueStatus> lst = IssueStatus.GetAllStatus();
                if (lst != null && lst.Count > 0)
                {
                    jres = new IssueStatusListResponse(string.Format("Issues status list found successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                    jres.IssueStatusList = lst;
                    ActionLogger.Logger.WriteLog("GetAllStatusService success ", true);
                }
                else
                {
                    jres = new IssueStatusListResponse(string.Format("No status list found"), Convert.ToInt16(ResponseCodes.RecordNotFound), "");
                    ActionLogger.Logger.WriteLog("GetAllStatusService  404 ", true);
                }
            }
            catch (Exception ex)
            {
                jres = new IssueStatusListResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                ActionLogger.Logger.WriteLog("GetAllStatusService failure ", true);
            }
            return jres;
        }

        public IssueReasonResponse GetReasonsService(int ReasonsID)
        {
            IssueReasonResponse jres = null;
            ActionLogger.Logger.WriteLog("GetReasonsService request: " + ReasonsID, true);
            try
            {
                IssueReasons obj = IssueReasons.GetReasons(ReasonsID);
                if (obj != null)
                {
                    jres = new IssueReasonResponse(string.Format("Issue reason found successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                    jres.IssueReasonObject = obj;
                    ActionLogger.Logger.WriteLog("GetReasonsService success ", true);
                }
                else
                {
                    jres = new IssueReasonResponse(string.Format("Issue reason not found "), Convert.ToInt16(ResponseCodes.RecordNotFound), "");
                    ActionLogger.Logger.WriteLog("GetReasonsService  404 ", true);
                }
            }
            catch (Exception ex)
            {
                jres = new IssueReasonResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                ActionLogger.Logger.WriteLog("GetReasonsService failure ", true);
            }
            return jres;

        }

        public IssueReasonListResponse GetAllReasonService()
        {
            IssueReasonListResponse jres = null;
            ActionLogger.Logger.WriteLog("GetAllReasonService request: ", true);
            try
            {
                List<IssueReasons> lst = IssueReasons.GetAllReason();
                if (lst != null && lst.Count > 0)
                {
                    jres = new IssueReasonListResponse(string.Format("Issues reasons list found successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                    jres.IssueReasonList = lst;
                    ActionLogger.Logger.WriteLog("GetAllReasonService success ", true);
                }
                else
                {
                    jres = new IssueReasonListResponse(string.Format("No reason list found"), Convert.ToInt16(ResponseCodes.RecordNotFound), "");
                    ActionLogger.Logger.WriteLog("GetAllReasonService  404 ", true);
                }
            }
            catch (Exception ex)
            {
                jres = new IssueReasonListResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                ActionLogger.Logger.WriteLog("GetAllReasonService failure ", true);
            }
            return jres;
        }
        #endregion

    }
}