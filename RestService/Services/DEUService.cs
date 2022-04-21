using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ServiceModel;
using System.ServiceModel.Web;
using MyAgencyVault.BusinessLibrary;
using MyAgencyVault.WcfService.Library.Response;
using MyAgencyVault.BusinessLibrary.BusinessObjects;
using MyAgencyVault.BusinessLibrary.PostProcess;

namespace MyAgencyVault.WcfService
{
    [ServiceContract]
    interface IDEUService
    {


        //[OperationContract]
        //[WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        //ExposedDEUListResponse GetBatchDataToExport(Guid BatchId);

        ////[OperationContract] - Found not in use 
        ////[WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        ////ModifiyableBatchStatementData AddUpdateDEUService(DEUFields deuFields);

        ////[OperationContract]- Found not in use 
        ////[WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        ////JSONResponse AddupdateDeuEntryService(DEU DeuEntry);

        //[OperationContract]
        //[WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        //DEUResponse GetDeuEntryidWiseService(Guid DeuEntryID);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        DEUListResponse GetDeuFieldsDetailsService(Guid deuEntryId, Guid payorToolId);

        //[OperationContract]
        //[WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        //PolicyBoolResponse IsPaymentFromCommissionDashBoardByPaymentEntryIdSrvc(Guid PolicPaymentId);

        //[OperationContract]
        //[WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        //PolicyBoolResponse IsPaymentFromCommissionDashBoardByDEUEntryIdSrvc(Guid DeuEntryId);

        //[OperationContract]
        //[WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        //JSONResponse DeleteDeuEntryByIDService(Guid DeuEntryId);

        //[OperationContract]
        //[WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        //JSONResponse DeleteDeuEntryAndPaymentEntryByDeuIDSrvc(Guid DeuEntryId);
        #region
        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        JsonBooleanResponse DEUStatementOpenService(Guid statementID);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        JsonBooleanResponse OpenBatchService(int batchNumber);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        DEUBatchListResponse GetDEUBatchLitService();

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        DEUStatementListResponse GetDEUStatementListService(Guid batchId);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        JsonBooleanResponse DEUEBatchClosedService(int batchNumber);
        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        JsonBooleanResponse DEUStatementClosedService(int statementNumber, string netAmount);
        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        StatementDetailsResponse FindStatementDetailsService(int statementNumber);
        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        BatchtDetailsResponse DEUFindBatchService(int batchNumber);
        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        GetUniqueIdentifierResponse GetDEUPoliciesUniqueIdentifier(Guid payorId, Guid templateId);
        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        PayorListResponse GetDEUPayorsService(Guid? licenseeId);

        //[OperationContract]
        //[WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        //PayorTemplateListResponse GetTemplateListService(Guid payorId);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        JsonStringResponse AddUpdateStatementService(Statement statement);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        DEUPayorToolResponse GetTemplateDataService(Guid payorId, Guid? templateId);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        StatementUpdateResponse UpdateCheckAmountService(Guid statementId, decimal checkAmount, decimal adjustment, string statementDateString);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        PostStatusDEUResponse DeuPostStartWrapperService(PostEntryProcess _PostEntryProcess, DEUFields deuFields, Guid deuEntryId, /*Guid userId, UserRole userRole,*/ bool isNewStatementCreate);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        DEUPolicyListResponse GetPoliciesListOnUniqueIdentifiers(List<UniqueIdenitfier> uniqueIdentifiers, Guid licenseeId, Guid payorId);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        PostedPaymentistResponse GetDeuPostedPaymentsService(Guid StatementId, ListParams listParams);
        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        PostedPaymentistResponse GetDeuFailedPaymentsService(Guid StatementId, ListParams listParams);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        ValidateDateFieldResponse ValidateDateFieldService(string date, string format);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        StatementCheckAmountListsResponse GetStatementCheckAmountService(Guid? batchId, Guid? payorId, string checkAmount, Guid currentStatementId);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        JSONResponse UpdateStatementPagesService(Guid statementId, string fromPage, string toPage);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        DEUEntryResponse PostDEUEntryService(DEUFields deuFields, Guid deuEntryId, bool isNewStatementRequired);
        #endregion

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        PostStatusDEUResponse ProcessPaymentService(PostEntryProcess _PostEntryProcess, DEUFields deuFields, Guid deuEntryId /*Guid userId, UserRole userRole, bool isNewStatementCreate*/);
        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        PayorTemplateListResponse GetDEUTemplateListService(Guid payorId);

    }

    public partial class MavService : IDEUService
    {

        public DEUEntryResponse PostDEUEntryService(DEUFields deuFields, Guid deuEntryId, bool isNewStmtRequired)
        {
            DEUEntryResponse jres = null;
            ActionLogger.Logger.WriteLog("PostDEUEntryService:Processing begins for getting posted entries : statementId - ", true);
            try
            {
                int? stmtNumber = 0;
                decimal? entered = 0;
                DEUPaymentEntry entry = DeuPostProcessWrapper.AddUpdateDEUEntry(deuFields, deuEntryId, isNewStmtRequired, out stmtNumber, out entered);
                jres = new DEUEntryResponse(string.Format("Data found successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                jres.statementNumber = stmtNumber;
                jres.DEUEntry = entry;
                jres.enteredAmount = entered + deuFields.DeuData.CommissionTotal; //Adding here to be reflected immediately after entry
                ActionLogger.Logger.WriteLog("PostDEUEntryService:Process completed with success!!!", true);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("paid/semi-paid"))
                {
                    jres = new DEUEntryResponse("", Convert.ToInt16(ResponseCodes.RecordAlreadyExist), ex.Message);
                }
                else {
                    jres = new DEUEntryResponse("", Convert.ToInt16(ResponseCodes.Failure), "An error occurred while posting the entry. Please check data and try again.");
                }
               
                ActionLogger.Logger.WriteLog("PostDEUEntryService:Exception occurs " + ex.Message, true);
            }
            return jres;
        }

        public PostStatusDEUResponse ProcessPaymentService(PostEntryProcess _PostEntryProcess, DEUFields deuFields, Guid deuEntryId /*Guid userId, UserRole userRole, bool isNewStatementCreate*/)
        {
            PostStatusDEUResponse jres = null;
            //int? newStatementNumber = 0;
            ActionLogger.Logger.WriteLog("PostPaymentAllDetailsService request:  deuFields:  " + deuFields.ToStringDump(), true);
            try
            {

                PostProcessWebStatus resp = DeuPostProcessWrapper.PostStart(_PostEntryProcess, deuFields, deuEntryId);
                if (resp != null)
                {
                    //if (resp.IsComplete)
                    //{
                   string res = (_PostEntryProcess == PostEntryProcess.Delete) ? "Payment deleted successfully" : "Payment posted successfully";
                    jres = new PostStatusDEUResponse(res, Convert.ToInt16(ResponseCodes.Success), "");
                    jres.PostStatus = resp;
                    ActionLogger.Logger.WriteLog("DeuPostStartWrapperService success ", true);
                    //}
                    //else
                    //{
                    //    jres = new PostStatusResponse(string.Format("Payment could not be posted"), Convert.ToInt16(ResponseCodes.Failure), "Payment could not be posted");
                    //    ActionLogger.Logger.WriteLog("DeuPostStartWrapperService faiure ", true);
                    //}
                }
                else
                {
                    jres = new PostStatusDEUResponse(string.Format("Payment could not be posted"), Convert.ToInt16(ResponseCodes.Failure), "Payment could not be posted");
                    ActionLogger.Logger.WriteLog("DeuPostStartWrapperService faiure ", true);
                }
            }
            catch (Exception ex)
            {
                jres = new PostStatusDEUResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                ActionLogger.Logger.WriteLog("DeuPostStartWrapperService failure ", true);
            }
            return jres;
        }
        public PostedPaymentistResponse GetDeuPostedPaymentsService(Guid StatementId, ListParams listParams)
        {
            PostedPaymentistResponse jres = null;
            ActionLogger.Logger.WriteLog("GetDeuPostedPaymentsService:Processing begins for getting posted entries : statementId - " + StatementId, true);
            try
            {
                Batch objBatch = new Batch();
                List<DEUPaymentEntry> lst = DEU.GetDeuEntriesforStatement(StatementId, listParams,out int totalRecordCount);
                jres = new PostedPaymentistResponse(string.Format("Data found successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                jres.TotalRecords = lst;
                jres.EntriesCount = jres.TotalLength = totalRecordCount;
                jres.ErrorCount = Statement.GetErrorCount(StatementId);
                ActionLogger.Logger.WriteLog("GetDeuPostedPaymentsService:Process completed with success!!!", true);
            }
            catch (Exception ex)
            {
                jres = new PostedPaymentistResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                ActionLogger.Logger.WriteLog("GetDeuPostedPaymentsService:Exception occurs while fetching details" + ex.Message, true);
            }
            return jres;
        }
        public PostedPaymentistResponse GetDeuFailedPaymentsService(Guid StatementId, ListParams listParams)
        {
            PostedPaymentistResponse jres = null;
            ActionLogger.Logger.WriteLog("GetDeuFailedPaymentsService:Processing begins for getting posted entries : statementId - " + StatementId, true);
            try
            {
                Batch objBatch = new Batch();
                List<DEUPaymentEntry> lst = DEU.GetDeuFailedEntriesforStatement(StatementId, listParams, out int totalRecordCount);
                jres = new PostedPaymentistResponse(string.Format("Data found successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                jres.TotalRecords = lst;
                jres.EntriesCount = Statement.GetEntriesCount(StatementId); 
                jres.ErrorCount = jres.TotalLength = totalRecordCount;  //Statement.GetErrorCount(StatementId);
                ActionLogger.Logger.WriteLog("GetDeuFailedPaymentsService:Process completed with success!!!", true);
            }
            catch (Exception ex)
            {
                jres = new PostedPaymentistResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                ActionLogger.Logger.WriteLog("GetDeuFailedPaymentsService:Exception occurs while fetching details" + ex.Message, true);
            }
            return jres;
        }

        /// <summary>
        /// /MOdifiedBy:Ankit khandelwal
        /// MOdifiedOn:14-05-2020
        /// </summary>
        /// <param name="deuEntryId"></param>
        /// <returns></returns>
        public DEUListResponse GetDeuFieldsDetailsService(Guid deuEntryId, Guid payorToolId)
        {
            DEUListResponse jres = null;
            ActionLogger.Logger.WriteLog("GetDeuFieldsDetailsService:Processing begins with deuEntryId:" + deuEntryId, true);
            try
            {
                List<DataEntryField> lst = DEU.GetDeuFieldDetails(deuEntryId, payorToolId);
                if (lst != null && lst.Count > 0)
                {
                    jres = new DEUListResponse(string.Format("Data found successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                    jres.DEUList = lst;
                }
            }
            catch (Exception ex)
            {
                jres = new DEUListResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                ActionLogger.Logger.WriteLog("GetDeuFieldsDetailsService:Exception occurs while processing with deuEntryId" + deuEntryId, true);
            }
            return jres;

        }


        #region
        /// <summary>
        /// Created by:Ankit khandelwal
        /// Createdon:16-04-2020
        /// Purpose:Getting Batch List for Datat entry unit 
        /// </summary>
        /// <returns></returns>
        public DEUBatchListResponse GetDEUBatchLitService()
        {
            DEUBatchListResponse response = null;
            ActionLogger.Logger.WriteLog("GetDEUBatchLitService: process begins for fetching batch List", true);
            try
            {
                List<Batch> batchList = DEU.GetDEUBatchList();
                response = new DEUBatchListResponse(string.Format("batch list found successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                response.TotalRecords = batchList;
            }
            catch (Exception ex)
            {
                response = new DEUBatchListResponse("Exception occurs getting list", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                ActionLogger.Logger.WriteLog("GetDEUBatchLitService: Exception occurs while processing" + ex.Message, true);
            }
            return response;

        }
        /// <summary>
        /// CreatedBy:Ankit khandelwal
        /// Createdon:16-04-2020
        /// Purpose:Get Statament list for data entry unit
        /// </summary>
        /// <param name="batchId"></param>
        /// <returns></returns>
        public DEUStatementListResponse GetDEUStatementListService(Guid batchId)
        {
            DEUStatementListResponse jres = null;
            ActionLogger.Logger.WriteLog("GetDEUStatementListService:Processing begins with batchId: " + batchId, true);
            try
            {
                List<Statement> statementList = DEU.GetDEUStatementList(batchId);
                jres = new DEUStatementListResponse(string.Format("Statement list found successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                jres.TotalRecords = statementList;
            }
            catch (Exception ex)
            {
                jres = new DEUStatementListResponse("Exception occurs getting list", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                ActionLogger.Logger.WriteLog("GetBatchStatementListService failure ", true);
            }
            return jres;
        }
        public JsonBooleanResponse DEUEBatchClosedService(int batchNumber)
        {
            JsonBooleanResponse jres = null;
            ActionLogger.Logger.WriteLog("DEUEBatchClosedService: processing begins with Batchumber:" + batchNumber, true);
            try
            {
                
                bool isClosed = DEU.DEUBatchClosed(batchNumber, out bool isBatchExist, out bool isStatementExist, out bool isAllStmtClose);

                if (isClosed)
                {
                    jres = new JsonBooleanResponse(string.Format("Entered Batch closed successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                }
                else
                {
                    if (!isBatchExist)
                    {
                        jres = new JsonBooleanResponse(string.Format("Entered Batch does not exist in the system"), Convert.ToInt16(ResponseCodes.RecordNotFound), "");
                    }
                    else if (!isStatementExist)
                    {
                        jres = new JsonBooleanResponse(string.Format("Batch can not be closed as there is no statement in the batch"), Convert.ToInt16(ResponseCodes.RecordNotFound), "");
                    }
                    else if (!isAllStmtClose)
                    {
                        jres = new JsonBooleanResponse(string.Format("All statements must be closed before closing the batch"), Convert.ToInt16(ResponseCodes.RecordNotFound), "");
                    }
                }

                //if (isBatchExist == true && batchStatus == true)
                //{
                //    jres = new JsonBooleanResponse(string.Format("Entered Batch closed successfully."), Convert.ToInt16(ResponseCodes.Success), "");
                //}
                //else if (isBatchExist == true && batchStatus == false && isStatementExist == true)
                //{
                //    jres = new JsonBooleanResponse(string.Format("All statements must be closed before closing the batch."), Convert.ToInt16(ResponseCodes.RecordNotFound), "");
                //}
                //else if (isStatementExist == false && isBatchExist == true)
                //{
                //    jres = new JsonBooleanResponse(string.Format("Batch can not be closed as there is no statement in the batch"), Convert.ToInt16(ResponseCodes.RecordNotFound), "");
                //}
                //else if (isBatchExist == false)
                //{
                //    jres = new JsonBooleanResponse(string.Format("Entered Batch does not exist in the system."), Convert.ToInt16(ResponseCodes.RecordNotFound), "");
                //}

                jres.result = isClosed;
                jres.isRecordExist = isBatchExist;
            }
            catch (Exception ex)
            {
                jres = new JsonBooleanResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                ActionLogger.Logger.WriteLog("DEUEBatchClosedService:Exception occurs while processing batch close." + ex.Message, true);
            }
            return jres;
        }
        public JsonBooleanResponse DEUStatementClosedService(int statementNumber, string netAmount)
        {
            JsonBooleanResponse response = null;
            ActionLogger.Logger.WriteLog("DEUStatementClosedService processing begins with statementNumber" + statementNumber, true);
            try
            {
                bool statementClosedStatus = DEU.CloseStatement(statementNumber, netAmount);
                response = new JsonBooleanResponse(string.Format("Statement closed successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                response.result = statementClosedStatus;
                ActionLogger.Logger.WriteLog("CloseStatementService success ", true);
            }
            catch (Exception ex)
            {
                response = new JsonBooleanResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                ActionLogger.Logger.WriteLog("DEUStatementClosedService Exception occurs while  processing with statementNumber" + statementNumber + "" + ex.Message, true);
            }
            return response;
        }
        public StatementDetailsResponse FindStatementDetailsService(int statementNumber)
        {
            StatementDetailsResponse jres = null;
            ActionLogger.Logger.WriteLog("FindStatementDetailsService:processing begins with  StatementNumber " + statementNumber, true);
            try
            {
                Statement obj = DEU.FindStatementDetails(statementNumber);
                jres = new StatementDetailsResponse(string.Format("Data found successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                jres.Details = obj;
            }
            catch (Exception ex)
            {
                jres = new StatementDetailsResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                ActionLogger.Logger.WriteLog("FindStatementDetailsService:Exception occurs while processing begins with  StatementNumber " + statementNumber + ex.Message, true);
            }
            return jres;

        }
        public BatchtDetailsResponse DEUFindBatchService(int batchNumber)
        {
            BatchtDetailsResponse jres = null;
            ActionLogger.Logger.WriteLog("DEUFindBatchService:processing begins with batchNumber " + batchNumber, true);
            try
            {
                Batch batchDetail = DEU.GetBatchDetails(batchNumber, out bool isBatchFound);
                if (isBatchFound)
                {
                    jres = new BatchtDetailsResponse(string.Format("Batch name found successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                    jres.Details = batchDetail;
                    jres.IsBatchFound = isBatchFound;
                }
                else
                {
                    jres = new BatchtDetailsResponse(string.Format("Batch name not found successfully"), Convert.ToInt16(ResponseCodes.RecordNotFound), "");
                    jres.IsBatchFound = isBatchFound;
                }

            }
            catch (Exception ex)
            {
                jres = new BatchtDetailsResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                ActionLogger.Logger.WriteLog("DEUFindBatchService:Exception occurs while processing" + ex.Message, true);
            }
            return jres;
        }
        /// <summary>
        /// Createdby:Ankit khandelwal
        /// Createdon:17-04-2020
        /// Purpose:Getting fields PoliciesUniqueIdentifier
        /// </summary>
        /// <param name="payorId"></param>
        /// <param name="templateId"></param>
        /// <returns></returns>
        public GetUniqueIdentifierResponse GetDEUPoliciesUniqueIdentifier(Guid payorId, Guid templateId)
        {
            GetUniqueIdentifierResponse jres = null;
            // ActionLogger.Logger.WriteLog("DEUFindBatchService:processing begins with batchNumber " + batchNumber, true);
            try
            {
                List<PayorToolField> uniqueIdentifiers = DEU.GetPolicyUniqueIdentifier(payorId, templateId);
                jres = new GetUniqueIdentifierResponse(string.Format("Batch name found successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                jres.PartOfPrimaryFieldList = uniqueIdentifiers;
            }
            catch (Exception ex)
            {
                jres = new GetUniqueIdentifierResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                ActionLogger.Logger.WriteLog("DEUFindBatchService:Exception occurs while processing" + ex.Message, true);
            }
            return jres;
        }
        /// <summary>
        /// Create by :Ankit Khandelwal
        /// Created On: 12-03-2019
        /// Purpose:Getting List of Payors 
        /// </summary>
        /// <param name="licenseeId"></param>
        /// <param name="payerfillInfo"></param>
        /// <returns></returns>
        public PayorListResponse GetDEUPayorsService(Guid? licenseeId)
        {
            PayorListResponse jres = null;
            ActionLogger.Logger.WriteLog("GetDEUPayorsService:processing begins with licenseeid - " + licenseeId, true);
            try
            {
                List<Payor> lst = DEU.GetDEUPayorsList(licenseeId);
                jres = new PayorListResponse(string.Format("Payor list retreived successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                jres.PayorList = lst;
                jres.PayorsCount = 0;
                ActionLogger.Logger.WriteLog("GetDEUPayorsService:success!!!!!! ", true);
            }
            catch (Exception ex)
            {
                jres = new PayorListResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                ActionLogger.Logger.WriteLog("GetDEUPayorsService:Exception occurs while fetching details:" + ex.Message, true);
            }
            return jres;
        }
        /// <summary>
        /// CreatedBy:Acmeminds
        /// CreatedOn:31-Jan-2019
        /// Purpose:Getting Template list based on Payor Selection
        /// </summary>
        /// <param name="payorId"></param>
        /// <returns></returns>
        public PayorTemplateListResponse GetDEUTemplateListService(Guid payorId)
        {
            ActionLogger.Logger.WriteLog("GetTemplateListService: Processing begins with payorId: " + payorId, true);
            PayorTemplateListResponse jres = null;
            try
            {
                List<Tempalate> lst = DEU.GetDEUTemplateList(payorId);
                jres = new PayorTemplateListResponse("Payors template list found successfully", (int)ResponseCodes.Success, "");
                jres.TotalRecords = lst;
                ActionLogger.Logger.WriteLog("GetTemplateListService success:", true);
            }
            catch (Exception ex)
            {
                jres = new PayorTemplateListResponse("", (int)ResponseCodes.Failure, "Error getting templates list" + ex.Message);
                ActionLogger.Logger.WriteLog("GetPayorToolTemplateService:Exception occurs while fetching details payorId" + payorId + " " + ex.Message, true);
            }
            return jres;
        }

        /// <summary>
        /// CreatedBy:Jyotisna 
        /// CreatedOn:24-April-2020
        /// Purpose:Open batch from DEU
        /// </summary>
        /// <param name="batchNumber"></param>
        /// <returns></returns>
        public JsonBooleanResponse OpenBatchService(int batchNumber)
        {
            ActionLogger.Logger.WriteLog("OpenBatchService: Processing begins with batchNumber: " + batchNumber, true);
            JsonBooleanResponse jres = null;
            try
            {
                bool isOpened = DEU.OpenBatch(batchNumber, out bool isBatchExist);
                jres = new JsonBooleanResponse("Batch opened successfully", (int)ResponseCodes.Success, "");
                jres.result = isOpened;
                jres.isRecordExist = isBatchExist;
                ActionLogger.Logger.WriteLog("OpenBatchService success:", true);

            }
            catch (Exception ex)
            {
                jres = new JsonBooleanResponse("", (int)ResponseCodes.Failure, "Error in re-opening batch: " + ex.Message);
                ActionLogger.Logger.WriteLog("OpenBatchService:Exception occurs while fetching details batchNumber" + batchNumber + " " + ex.Message, true);
            }
            return jres;
        }
        /// <summary>
        /// Created by:Ankit khandelwal
        /// Created on :26-04-2020
        /// puprose:Add update statement details
        /// </summary>
        /// <param name="Statement"></param>
        /// <returns></returns>
        public JsonStringResponse AddUpdateStatementService(Statement statement)
        {
            JsonStringResponse jres = null;
            ActionLogger.Logger.WriteLog("AddUpdateStatementService request: Stment " + statement.ToStringDump(), true);
            try
            {
                int? val = DEU.AddUpdateStatement(statement);
                ActionLogger.Logger.WriteLog("AddUpdateStatementService value: " + val, true);
                if (val != null)
                {
                    jres = new JsonStringResponse(string.Format("Data saved successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                    jres.IntResult = val;
                }
                else
                {
                    jres = new JsonStringResponse("", Convert.ToInt16(ResponseCodes.RecordNotFound), "Status not found.");
                    ActionLogger.Logger.WriteLog("AddUpdateStatementService failure ", true);
                }
            }
            catch (Exception ex)
            {
                jres = new JsonStringResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                ActionLogger.Logger.WriteLog("AddUpdateStatementService failure ", true);
            }
            return jres;
        }

        /// <summary>
        /// CreatedBy:Jyotisna 
        /// CreatedOn:28-April-2020
        /// Purpose:Open statement from DEU
        /// </summary>
        /// <param name="statementID"></param>
        /// <returns></returns>
        public JsonBooleanResponse DEUStatementOpenService(Guid statementID)
        {
            ActionLogger.Logger.WriteLog("OpenStatementService: Processing begins with statementID: " + statementID, true);
            JsonBooleanResponse jres = null;
            try
            {
                bool isOpened = Statement.OpenStatement(statementID);
                jres = new JsonBooleanResponse("Statement opened successfully", (int)ResponseCodes.Success, "");
                jres.result = isOpened;
                ActionLogger.Logger.WriteLog("OpenStatementService success:", true);
            }
            catch (Exception ex)
            {
                jres = new JsonBooleanResponse("", (int)ResponseCodes.Failure, "Error opening statement: " + ex.Message);
                ActionLogger.Logger.WriteLog("OpenStatementService exception: " + ex.Message, true);
            }
            return jres;
        }

        /// <summary>
        /// CreatedOn:31-Jan-2019
        /// CreatedBy:Acmeminds
        /// Purpose:Getting details of payortemplate
        /// </summary>
        /// <param name="payorId"></param>
        /// <param name="templateId"></param>
        /// <returns></returns>
        public DEUPayorToolResponse GetTemplateDataService(Guid payorId, Guid? templateId)
        {
            ActionLogger.Logger.WriteLog("GetPayorTemplateDataService: processing begins with payorId" + payorId + " " + "templateId" + templateId, true);
            DEUPayorToolResponse jres = null;
            try
            {
                //if (templateId == Guid.Empty)
                //{
                //    templateId = null;
                //}

                DEUPayorToolObject obj = DEU.GetDEUTemplateDetails(payorId, templateId, out string uniqueIDs);
                jres = new DEUPayorToolResponse("Payor tool data found successfully", (int)ResponseCodes.Success, "");
                jres.PayorToolObject = obj;
                jres.uniqueIDs = uniqueIDs;
                ActionLogger.Logger.WriteLog("GetPayorTemplateDataService: data found successfully", true);
            }
            catch (Exception ex)
            {
                jres = new DEUPayorToolResponse("", (int)ResponseCodes.Failure, ex.Message);
                ActionLogger.Logger.WriteLog("GetPayorTemplateDataService:Exception occurs while fetching data with payorId" + payorId + " " + ex.Message, true);
            }
            return jres;
        }


        public JSONResponse UpdateStatementPagesService(Guid statementId, string fromPage, string toPage)
        {
            ActionLogger.Logger.WriteLog("UpdateStatementPages: Processing begins with statementID: " + statementId, true);
            JSONResponse jres = null;
            try
            {
                Statement.UpdateStmtPages(statementId, fromPage, toPage);
                jres = new JSONResponse("Pages updated successfully", (int)ResponseCodes.Success, "");
                ActionLogger.Logger.WriteLog("UpdateStatementPages success:", true);
            }
            catch (Exception ex)
            {
                jres = new JSONResponse("", (int)ResponseCodes.Failure, "" + ex.Message);
                ActionLogger.Logger.WriteLog("UpdateStatementPages exception: " + ex.Message, true);
            }
            return jres;
        }
        public StatementUpdateResponse UpdateCheckAmountService(Guid statementId, decimal checkAmount, decimal adjustment, string statementDateString)
        {
            ActionLogger.Logger.WriteLog("UpdateCheckAmountService: Processing begins with statementID: " + statementId, true);
            StatementUpdateResponse jres = null;
            try
            {
                string completedPercent;
                string netCheck = Statement.UpdateCheckAmount(statementId, checkAmount, adjustment, statementDateString, out completedPercent);
                jres = new StatementUpdateResponse("Check amount updated successfully", (int)ResponseCodes.Success, "");
                jres.netCheck = netCheck;
                jres.completePercent = completedPercent;
                ActionLogger.Logger.WriteLog("UpdateCheckAmountService success:", true);
            }
            catch (Exception ex)
            {
                jres = new StatementUpdateResponse("", (int)ResponseCodes.Failure, "" + ex.Message);
                ActionLogger.Logger.WriteLog("UpdateCheckAmountService exception: " + ex.Message, true);
            }
            return jres;
        }
        public PostStatusDEUResponse DeuPostStartWrapperService(PostEntryProcess _PostEntryProcess, DEUFields deuFields, Guid deuEntryId, /*Guid userId, UserRole userRole,*/ bool isNewStatementCreate)
        {
            PostStatusDEUResponse jres = null;
            int? newStatementNumber = 0;
            ActionLogger.Logger.WriteLog("DeuPostStartWrapperService request:  deuFields:  " + deuFields.ToStringDump(), true);
            try
            {
                PostProcessWebStatus resp = DeuPostProcessWrapper.DeuPostStartWrapper(_PostEntryProcess, deuFields, deuEntryId, isNewStatementCreate, out newStatementNumber);
                if (resp != null)
                {
                    //if (resp.IsComplete)
                    //{
                    string res = (_PostEntryProcess == PostEntryProcess.Delete) ? "Payment deleted successfully" : "Payment posted successfully";
                    jres = new PostStatusDEUResponse(res, Convert.ToInt16(ResponseCodes.Success), "");
                    jres.PostStatus = resp;
                    ActionLogger.Logger.WriteLog("DeuPostStartWrapperService success ", true);
                    //}
                    //else
                    //{
                    //    jres = new PostStatusResponse(string.Format("Payment could not be posted"), Convert.ToInt16(ResponseCodes.Failure), "Payment could not be posted");
                    //    ActionLogger.Logger.WriteLog("DeuPostStartWrapperService faiure ", true);
                    //}
                }
                else
                {
                    jres = new PostStatusDEUResponse(string.Format("Payment could not be posted"), Convert.ToInt16(ResponseCodes.Failure), "Payment could not be posted");
                    ActionLogger.Logger.WriteLog("DeuPostStartWrapperService faiure ", true);
                }
            }
            catch (Exception ex)
            {
                jres = new PostStatusDEUResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                ActionLogger.Logger.WriteLog("DeuPostStartWrapperService failure ", true);
            }
            return jres;
        }
        /// <summary>
        /// ModifiedBy:Ankit Khandelwalba
        /// MOdifiedOn: 08-05-2020
        /// PUrpose:Get policy list based on uniqueIdentifiers on field blur
        /// </summary>
        /// <param name="uniqueIdentifiers"></param>
        /// <param name="licenseeId"></param>
        /// <param name="payorId"></param>
        /// <returns></returns>
        public DEUPolicyListResponse GetPoliciesListOnUniqueIdentifiers(List<UniqueIdenitfier> uniqueIdentifiers, Guid licenseeId, Guid payorId)
        {

            DEUPolicyListResponse jres = null;
            ActionLogger.Logger.WriteLog("GetPoliciesListOnUniqueIdentifiers:Processing begins with " + licenseeId + ", uniquIDs: " + uniqueIdentifiers.ToStringDump(), true);
            try
            {
                List<DeuSearchedPolicy> lst = PostUtill.GetPoliciesFromUniqueIdentifier(uniqueIdentifiers, licenseeId, payorId);
                jres = new DEUPolicyListResponse(string.Format("Policies found successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                ActionLogger.Logger.WriteLog("GetPoliciesListOnUniqueIdentifiers:processing success!!!! ", true);
                jres.TotalRecords = lst;
                jres.TotalLength = lst.Count;
            }
            catch (Exception ex)
            {
                jres = new DEUPolicyListResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                ActionLogger.Logger.WriteLog("GetPoliciesListOnUniqueIdentifiers:Exception occurs while processing" + ex.Message, true);
            }
            return jres;
        }


        #endregion
        public ValidateDateFieldResponse ValidateDateFieldService(string date, string format)
        {
            ValidateDateFieldResponse response = null;
            try
            {
                bool isDateValid = DEU.ValidateDate(date, format, out string formatedDate);
                response = new ValidateDateFieldResponse("validate date field", Convert.ToInt16(ResponseCodes.Success), "");
                response.IsDateValid = isDateValid;
                response.FormattedDate = formatedDate;
            }
            catch (Exception ex)
            {
                response = new ValidateDateFieldResponse(" Exception occurs while validate date field", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
            }
            return response;
        }
        /// <summary>
        /// Createdby:Ankit khandelwal
        /// Createdon:26-05-2020
        /// Purpose:getting checkamount list
        /// </summary>
        /// <param name="batchId"></param>
        /// <param name="payorId"></param>
        /// <returns></returns>
        public StatementCheckAmountListsResponse GetStatementCheckAmountService(Guid? batchId, Guid? payorId, string checkAmount, Guid currentStatementId)
        {
            StatementCheckAmountListsResponse jres = null;
            ActionLogger.Logger.WriteLog("GetCheckAmountService request: Batch " + batchId + ", Payor: " + payorId, true);
            try
            {

                bool isStatementExist = DEU.IsStatementCheckAmountExist(batchId, payorId, checkAmount, currentStatementId);

                jres = new StatementCheckAmountListsResponse(string.Format("Statements list found successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                jres.IsStatementFound = isStatementExist;
                ActionLogger.Logger.WriteLog("GetCheckAmountService success ", true);
            }
            catch (Exception ex)
            {
                jres = new StatementCheckAmountListsResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                ActionLogger.Logger.WriteLog("GetCheckAmountService failure ", true);
            }
            return jres;
        }


        //public ValidateDateFieldResponse GetDateFieldFormatService(string date, string format)
        //{
        //    ValidateDateFieldResponse response = null;
        //    try
        //    {
        //        string formatedDate = DEU.GetFormatedDate(date, format);
        //        response = new ValidateDateFieldResponse("validate date field", Convert.ToInt16(ResponseCodes.Success), "");
        //        response.FormattedDate = formatedDate;
        //    }
        //    catch (Exception ex)
        //    {
        //        response = new ValidateDateFieldResponse(" Exception occurs while validate date field", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
        //    }
        //    return response;
        //}
    }

}
