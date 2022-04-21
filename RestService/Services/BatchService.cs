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
    interface IBatchService
    {
        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        BatchNoResponse AddUpdateIBatchService(Batch batchData);

        //        [OperationContract]
        //        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        //        BatchAddOutputResponse AddUpdateBatchWithBatchOutputService(Batch batch);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        PolicyBoolResponse DeleteBatchService(Guid batchId, UserRole userRole);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        JSONResponse AddUpdateBatchNoteService(Int32 batchNumber, string batchNote);

        //        [OperationContract]
        //        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        //        BatchListResponse GetBatchesForReportManagerService();

        //        //Found duplicate functionality in GetBatchesForReportManagerService
        //        //[OperationContract]

        //        //[WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        //        //BatchListResponse GetAllBatchForReportManagerForAllLicenseeService();

        //[OperationContract]
        //[WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        //ReportsBatchListResponse GetReportManagerBatchList(Guid licenseeId);
        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        ReportsBatchListResponse GetReportManagerBatchList(Guid licenseeId, string filter, string monthFilter);

        //        [OperationContract]
        //        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        //        BatchListResponse GetBatchesForDeuEntriesService();

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        BatchListResponse GetCurrentBatchService(Guid licenseeId, string createdOn, ListParams listParams, string selectedMonths);

        //        //[OperationContract] - fOUND NOT USED 
        //        //[WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        //        //BatchListResponse GetBatchesForReportManagerByLicIDService(Guid licenceID);

        //        //[OperationContract]
        //        //JSONResponse ClearDownloadStatusService();

        //        //[OperationContract]
        //        //JSONResponse LaunchWebSiteService();

        //        /// <summary>
        //        /// make the batch status to be closed.
        //        /// </summary>
      
       

      

        //        [OperationContract]
        //        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        //        PolicyStringResponse BatchNameByIdService(Guid BatchID);
        //        /// <summary>
        //        /// To Do:  it might be removed to be implemented on the UI code behind. 
        //        /// don't know. future implementor of this function can better recognize the conditions.
        //        /// purpose : faciltate to open up the pdf/excel file on the client machine to view the content in the file.
        //        /// </summary>
        //        //[OperationContract]
        //        //JSONResponse ViewBatchFileService(String FilePath, String FileType);

        //        /// <summary>
        //        /// compile and compose a file of all statement and entries for the licensee, and upload to the server specified folder.
        //        /// Giving specific system generated name to the file. ---
        //        /// </summary>
        //        //[OperationContract]
        //        //JSONResponse UploadBatchOfLocalPayorService();


        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        BatchListExportResponse ExportBatchService(Guid batchId, string batchNumber, string payorName);

        //        [OperationContract]
        //        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        //        PolicyBoolResponse GetBatchPaidStatusService(Guid BatchId);

        //        //[OperationContract]
        //        //bool SetBatchesAsPaid(List<Guid> BatchIds, List<Guid> payeeID, List<string> lstFilter);

        //        //[OperationContract]
        //        //[WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        //        //PolicyBoolResponse SetBatchesAsPaidService(List<Guid> BatchIds);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        PolicyBoolResponse SetBatchToUnPaidStatusService(int batchNumber);

        //        //[OperationContract]
        //        //[WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        //        //PolicyBoolResponse SetBatchesToPaidService(List<Guid> BatchIds, string strFilter, List<Guid> lstPayee);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        PolicyStringResponse SetBatchesToPaidInReportsService(string batchIds, string strFilter, string lstPayee, string lstSegments);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        JSONResponse UpdateBatchFileNameService(int batchNumber, string fileName);

        //        [OperationContract]
        //        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        //        BatchInsuredResponse GetBatchInsuredRecordService(Guid StatementID);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        InsuredPaymentsResponse GetInsuredPaymentsService(Guid statementId);

        //        [OperationContract]
        //        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        //        InsuredPaymentsResponse GetInsuredNameService(Guid StatementID);

        //        #region Download Batch methods

        //        [OperationContract]
        //        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        //        DownloadBatchResponse GetDownloadBatchListService();

        //        [OperationContract]
        //        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        //        PolicyBoolResponse IsBatchPartiallyOrFullyPaidService(DownloadBatch batch);

        //        [OperationContract]
        //        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        //        JSONResponse DeleteDownloadBatchService(DownloadBatch batch, UserRole _UserRole);

        //        [OperationContract]
        //        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        //        ImportFileResponse ImportBatchFileService(DownloadBatch batch, UserRole _UserRole);

        //        [OperationContract]
        //        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        //        PolicyDateResponse UpdateEntryStatusService(DownloadBatch batch);

        //        [OperationContract]
        //        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        //        JSONResponse ClearDownloadBatchService(DownloadBatch batch, UserRole _UserRole);
        //        #endregion

        //        #region Batch Files 
        //        [OperationContract]
        //        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        //        BatchFileListResponse FillBatchFilesDataService();

        //        [OperationContract]
        //        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        //        PolicyBoolResponse DeleteBatchFileService(BatchFiles batchFile);
        //        #endregion
    }

    public partial class MavService : IBatchService
    {
        //        #region Batch Files

        //        public BatchFileListResponse FillBatchFilesDataService()
        //        {
        //            BatchFileListResponse jres = null;
        //            ActionLogger.Logger.WriteLog("FillBatchFilesDataService request", true);
        //            try
        //            {
        //                List<BatchFiles> lst = BatchFiles.fillBatchFilesData();
        //                if (lst != null && lst.Count > 0)
        //                {
        //                    jres = new BatchFileListResponse(string.Format("Batch files saved successfully"), Convert.ToInt16(ResponseCodes.Success), "");
        //                    jres.BatchFileList = lst;
        //                }
        //                else
        //                {
        //                    jres = new BatchFileListResponse(string.Format("Batch files could not be saved "), Convert.ToInt16(ResponseCodes.RecordNotFound), "");

        //                }
        //                ActionLogger.Logger.WriteLog("FillBatchFilesDataService success ", true);
        //            }
        //            catch (Exception ex)
        //            {
        //                jres = new BatchFileListResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
        //                ActionLogger.Logger.WriteLog("FillBatchFilesDataService failure ", true);
        //            }
        //            return jres;
        //        }

        //        public PolicyBoolResponse DeleteBatchFileService(BatchFiles batchFile)
        //        {
        //            PolicyBoolResponse jres = null;
        //            ActionLogger.Logger.WriteLog("DeleteBatchFileService request", true);
        //            try
        //            {
        //                bool res = BatchFiles.DeleteBatchFile(batchFile);
        //                jres = new PolicyBoolResponse(string.Format("Batch file deleted successfully"), Convert.ToInt16(ResponseCodes.Success), "");
        //                jres.BoolFlag = res;
        //                ActionLogger.Logger.WriteLog("DeleteBatchFileService success ", true);
        //            }
        //            catch (Exception ex)
        //            {
        //                jres = new PolicyBoolResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
        //                ActionLogger.Logger.WriteLog("DeleteBatchFileService failure ", true);
        //            }
        //            return jres;
        //        }

        //        #endregion

        //        #region Batch Insured Members
        //        public BatchInsuredResponse GetBatchInsuredRecordService(Guid stmtId)
        //        {
        //            BatchInsuredResponse jres = null;
        //            ActionLogger.Logger.WriteLog("GetBatchInsuredRecordService request: " + stmtId, true);
        //            try
        //            {
        //                List<BatchInsuredRecored> lst = BatchInsuredRecored.GetBatchInsuredRecored(stmtId);
        //                if (lst != null && lst.Count > 0)
        //                {
        //                    jres = new BatchInsuredResponse(string.Format("Records found successfully"), Convert.ToInt16(ResponseCodes.Success), "");
        //                    jres.StatementList = lst;
        //                }
        //                else
        //                {
        //                    jres = new BatchInsuredResponse(string.Format("No records found"), Convert.ToInt16(ResponseCodes.RecordNotFound), "");
        //                }
        //                ActionLogger.Logger.WriteLog("GetBatchInsuredRecordService success ", true);
        //            }
        //            catch (Exception ex)
        //            {
        //                jres = new BatchInsuredResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
        //                ActionLogger.Logger.WriteLog("GetBatchInsuredRecordService failure ", true);
        //            }
        //            return jres;
        //        }
        /// <summary>
        /// Used By:Ankit Kahndelwal
        /// Userd on:18-06-2019
        /// </summary>
        /// <param name="statementId"></param>
        /// <returns></returns>
        public InsuredPaymentsResponse GetInsuredPaymentsService(Guid statementId)
        {
            InsuredPaymentsResponse jres = null;
            ActionLogger.Logger.WriteLog("GetInsuredPaymentsService request: " + statementId, true);
            try
            {
                List<InsuredPayment> lst = BatchInsuredRecored.GetInsuredPayments(statementId);
                if (lst != null && lst.Count > 0)
                {
                    jres = new InsuredPaymentsResponse(string.Format("Records found successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                    jres.InsuredPaymentsList = lst;
                }
                else
                {
                    jres = new InsuredPaymentsResponse(string.Format("No records found"), Convert.ToInt16(ResponseCodes.RecordNotFound), "");
                }
                jres.InsuredPaymentsList = lst;
                ActionLogger.Logger.WriteLog("GetInsuredPaymentsService success ", true);
            }
            catch (Exception ex)
            {
                jres = new InsuredPaymentsResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                ActionLogger.Logger.WriteLog("GetInsuredPaymentsService failure ", true);
            }
            return jres;
        }

        //        public InsuredPaymentsResponse GetInsuredNameService(Guid stmtId)
        //        {
        //            InsuredPaymentsResponse jres = null;
        //            ActionLogger.Logger.WriteLog("GetInsuredNameService request: " + stmtId, true);
        //            try
        //            {
        //                List<InsuredPayment> lst = BatchInsuredRecored.GetInsuredName(stmtId);
        //                if (lst != null && lst.Count > 0)
        //                {
        //                    jres = new InsuredPaymentsResponse(string.Format("Records found successfully"), Convert.ToInt16(ResponseCodes.Success), "");
        //                    jres.InsuredPaymentsList = lst;
        //                }
        //                else
        //                {
        //                    jres = new InsuredPaymentsResponse(string.Format("No records found"), Convert.ToInt16(ResponseCodes.RecordNotFound), "");
        //                }
        //                jres.InsuredPaymentsList = lst;
        //                ActionLogger.Logger.WriteLog("GetInsuredNameService success ", true);
        //            }
        //            catch (Exception ex)
        //            {
        //                jres = new InsuredPaymentsResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
        //                ActionLogger.Logger.WriteLog("GetInsuredNameService failure ", true);
        //            }
        //            return jres;
        //        }
        //        #endregion

        //        #region IBatch Members

        /// <summary>
        /// Modified By:Ankit khandelwal
        /// Modified On:23-05-2019
        /// Purpose:AddupdateBatchNotes
        /// </summary>
        /// <param name="BatchNumber"></param>
        /// <param name="BatchNote"></param>
        /// <returns></returns>
        public JSONResponse AddUpdateBatchNoteService(Int32 batchNumber, string batchNote)
        {
            JSONResponse jres = null;
            ActionLogger.Logger.WriteLog("AddUpdateBatchNote request: " + batchNumber + ",BatchNote  " + batchNote, true);
            try
            {
                Batch objBatch = new Batch();
                objBatch.AddUpdateBatchNote(batchNumber, batchNote);
                jres = new JSONResponse(string.Format("Batch note saved successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                ActionLogger.Logger.WriteLog("AddUpdateBatchNote success ", true);
            }
            catch (Exception ex)
            {
                jres = new JSONResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                ActionLogger.Logger.WriteLog("AddUpdateBatchNote failure ", true);
            }
            return jres;
        }
        /// <summary>
        /// Modified By :Ankit Khandelwal
        /// ModifiedOn:29-05-2019
        /// Purpose:get the batch numbe rfor Upload a file from comp manager
        /// 
        /// </summary>
        /// <param name="batchData"></param>
        /// <returns></returns>
        public BatchNoResponse AddUpdateIBatchService(Batch batchData)
        {
            BatchNoResponse jres = null;
            ActionLogger.Logger.WriteLog("AddUpdateIBatch request", true);
            try
            {
                int val = batchData.AddUpdate();
                ActionLogger.Logger.WriteLog("AddUpdateIBatch value: " + val, true);
                if (val != null)
                {
                    jres = new BatchNoResponse(string.Format("Data saved successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                    jres.BatchNumber = val;
                }
                else
                {
                    jres = new BatchNoResponse("Batch number not found!", Convert.ToInt16(ResponseCodes.RecordNotFound), "");
                    ActionLogger.Logger.WriteLog("AddUpdateIBatch 404 ", true);
                }
            }
            catch (Exception ex)
            {
                jres = new BatchNoResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                ActionLogger.Logger.WriteLog("AddUpdateIBatch failure ", true);
            }
            return jres;
        }

        //        public BatchAddOutputResponse AddUpdateBatchWithBatchOutputService(Batch batch)
        //        {
        //            BatchAddOutputResponse jres = null;
        //            ActionLogger.Logger.WriteLog("AddUpdateBatchWithBatchOutputService request: Batch-" + batch.ToStringDump(), true);
        //            try
        //            {
        //                BatchAddOutput res = batch.AddUpdateBatch();
        //                jres = new BatchAddOutputResponse(string.Format("Batch saved successfully"), Convert.ToInt16(ResponseCodes.Success), "");
        //                jres.BatchOutput = res;
        //                ActionLogger.Logger.WriteLog("AddUpdateBatchWithBatchOutputService success ", true);
        //            }
        //            catch (Exception ex)
        //            {
        //                jres = new BatchAddOutputResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
        //                ActionLogger.Logger.WriteLog("AddUpdateBatchWithBatchOutputService failure ", true);
        //            }
        //            return jres;
        //        }

        public PolicyBoolResponse DeleteBatchService(Guid batchId, UserRole userRole)
        {
            PolicyBoolResponse jres = null;
            ActionLogger.Logger.WriteLog("DeleteBatch request: BatchId -" + batchId + ", role: " + userRole, true);
            try
            {
                Batch objBatch = new Batch();
                bool res = objBatch.DeleteBatch(batchId, userRole);
                jres = new PolicyBoolResponse(string.Format("Data deleted successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                jres.BoolFlag = res;
                ActionLogger.Logger.WriteLog("DeleteBatch success ", true);
            }
            catch (Exception ex)
            {
                jres = new PolicyBoolResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                ActionLogger.Logger.WriteLog("DeleteBatch failure ", true);
            }
            return jres;
        }

        //        public BatchListResponse GetBatchesForReportManagerService()
        //        {
        //            BatchListResponse jres = null;
        //            ActionLogger.Logger.WriteLog("GetBatchesForReportManager request ", true);
        //            try
        //            {
        //                Batch objBatch = new Batch();
        //                List<Batch> lst = objBatch.GetBatchListForReportManager();
        //                if (lst != null && lst.Count > 0)
        //                {
        //                    jres = new BatchListResponse(string.Format("Data found successfully"), Convert.ToInt16(ResponseCodes.Success), "");
        //                    jres.BatchList = lst;
        //                }
        //                else
        //                {
        //                    jres = new BatchListResponse(string.Format("No data found "), Convert.ToInt16(ResponseCodes.RecordNotFound), "");
        //                }

        //                ActionLogger.Logger.WriteLog("GetBatchesForReportManager success ", true);
        //            }
        //            catch (Exception ex)
        //            {
        //                jres = new BatchListResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
        //                ActionLogger.Logger.WriteLog("GetBatchesForReportManager failure ", true);
        //            }
        //            return jres;

        //        }

        //        //public BatchListResponse GetAllBatchForReportManagerForAllLicenseeService()
        //        //{

        //        //    BatchListResponse jres = null;
        //        //    ActionLogger.Logger.WriteLog("GetAllBatchForReportManagerForAllLicensee request ", true);
        //        //    try
        //        //    {
        //        //        Batch objBatch = new Batch();
        //        //        List<Batch> lst = objBatch.GetAllBatchForReportManagerForAllLicensee();

        //        //        jres = new BatchListResponse(string.Format("Data found successfully"), Convert.ToInt16(ResponseCodes.Success), "");
        //        //        jres.BatchList = lst;
        //        //        ActionLogger.Logger.WriteLog("GetAllBatchForReportManagerForAllLicensee success ", true);
        //        //    }
        //        //    catch (Exception ex)
        //        //    {
        //        //        jres = new BatchListResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
        //        //        ActionLogger.Logger.WriteLog("GetAllBatchForReportManagerForAllLicensee failure ", true);
        //        //    }
        //        //    return jres;
        //        //}
        //        /// <summary>

        /// ModifiedBy:Ankit Khandelwal
        /// ModifiedOn:03-05-2018
        /// Purpose:getting list of batches based on licenseeId fro Reports
        /// </summary>
        /// <param name="licenseeId"></param>
        /// <returns></returns>
        public ReportsBatchListResponse GetReportManagerBatchList(Guid licenseeId, string filter, string monthFilter)
        {
            ReportsBatchListResponse jres = null;
            ActionLogger.Logger.WriteLog("GetBatchForReportManagerByLicenssIDService request : " + licenseeId, true);
            try
            {
                Batch objBatch = new Batch();
                List<ReportBatchDetails> lst = objBatch.GetReportManagerBatchList(licenseeId, filter, monthFilter);
                if (lst != null && lst.Count > 0)
                {
                    jres = new ReportsBatchListResponse(string.Format("Data found successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                }
                else
                {
                    jres = new ReportsBatchListResponse(string.Format("No data found"), Convert.ToInt16(ResponseCodes.RecordNotFound), "");
                }

                jres.TotalRecords = lst;
                jres.TotalLength = lst.Count;

                ActionLogger.Logger.WriteLog("GetBatchForReportManagerByLicenssIDService success ", true);
            }
            catch (Exception ex)
            {
                jres = new ReportsBatchListResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                ActionLogger.Logger.WriteLog("GetBatchForReportManagerByLicenssIDService failure " + ex.Message, true);
            }
            return jres;
        }

        //        public BatchListResponse GetBatchesForDeuEntriesService()
        //        {
        //            BatchListResponse jres = null;
        //            ActionLogger.Logger.WriteLog("GetBatchesForDeuEntriesService request : ", true);
        //            try
        //            {
        //                Batch objBatch = new Batch();
        //                List<Batch> lst = objBatch.GetBatchesForDeuEntries();
        //                if (lst != null && lst.Count > 0)
        //                {
        //                    jres = new BatchListResponse(string.Format("Data found successfully"), Convert.ToInt16(ResponseCodes.Success), "");
        //                    jres.BatchList = lst;
        //                }
        //                else
        //                {
        //                    jres = new BatchListResponse(string.Format("No data found "), Convert.ToInt16(ResponseCodes.RecordNotFound), "");
        //                }

        //                ActionLogger.Logger.WriteLog("GetBatchesForDeuEntriesService success ", true);
        //            }
        //            catch (Exception ex)
        //            {
        //                jres = new BatchListResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
        //                ActionLogger.Logger.WriteLog("GetBatchesForDeuEntriesService failure ", true);
        //            }
        //            return jres;
        //        }
        /// <summary>
        /// MOdified by:Ankit khandelwal
        /// ModifiedOn:22-05-2019
        /// purpose:Get list of batches for comp manager
        /// </summary>
        /// <param name="licenseeId"></param>
        /// <param name="createdOn"></param>
        /// <returns></returns>
        public BatchListResponse GetCurrentBatchService(Guid licenseeId, string createdOn,ListParams listParams,string selectedMonths)
        {
            BatchListResponse jres = null;
            ActionLogger.Logger.WriteLog("GetCurrentBatchService request : licenceID - " + licenseeId + ", CreatedOn - " + createdOn, true);
            try
            {
                int yearFilter = Convert.ToInt32(createdOn);
                List<BathcListObject> lst = Batch.GetCurrentBatch(licenseeId, yearFilter, listParams, selectedMonths, out List<MonthsListData> monthList, out int totalRecord);
                if (lst != null && lst.Count > 0)
                {
                    jres = new BatchListResponse(string.Format("Data found successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                    jres.TotalRecords = lst;
                    jres.TotalLength = totalRecord;
                    jres.MonthList = monthList;
                }
                else
                {
                    jres = new BatchListResponse(string.Format("No data found "), Convert.ToInt16(ResponseCodes.RecordNotFound), "");
                }
                ActionLogger.Logger.WriteLog("GetCurrentBatchService success ", true);
            }
            catch (Exception ex)
            {
                jres = new BatchListResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                ActionLogger.Logger.WriteLog("GetCurrentBatchService failure ", true);
            }
            return jres;
        }


        //        //public BatchListResponse GetBatchesForReportManagerByLicIDService(Guid licenceID)
        //        //{
        //        //    BatchListResponse jres = null;
        //        //    ActionLogger.Logger.WriteLog("GetBatchesForReportManagerByLicIDService request : licenceID - " + licenceID , true);
        //        //    try
        //        //    {
        //        //        Batch objBatch = new Batch();
        //        //        List<Batch> lst = objBatch.GetCurrentBatchForReportBYLicID(licenceID);

        //        //        if (lst != null && lst.Count > 0)
        //        //        {
        //        //            jres = new BatchListResponse(string.Format("Data found successfully"), Convert.ToInt16(ResponseCodes.Success), "");
        //        //            jres.BatchList = lst;
        //        //        }
        //        //        else
        //        //        {
        //        //            jres = new BatchListResponse(string.Format("No data found "), Convert.ToInt16(ResponseCodes.RecordNotFound), "");
        //        //        }
        //        //        ActionLogger.Logger.WriteLog("GetBatchesForReportManagerByLicIDService success ", true);
        //        //    }
        //        //    catch (Exception ex)
        //        //    {
        //        //        jres = new BatchListResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
        //        //        ActionLogger.Logger.WriteLog("GetBatchesForReportManagerByLicIDService failure ", true);
        //        //    }
        //        //    return jres;
        //        //}

        //        //public JSONResponse ClearDownloadStatus()
        //        //{
        //        //    throw new NotImplementedException();
        //        //}

        //        //public void LaunchWebSite()
        //        //{
        //        //    throw new NotImplementedException();
        //        //}



        

      
        //        public PolicyStringResponse BatchNameByIdService(Guid BatchID)
        //        {
        //            PolicyStringResponse jres = null;
        //            ActionLogger.Logger.WriteLog("BatchNameByIdService request: BatchID " + BatchID, true);
        //            try
        //            {
        //                Batch objBatch = new Batch();
        //                string s = objBatch.BatchNameById(BatchID);
        //                jres = new PolicyStringResponse(string.Format("Batch name found successfully by ID"), Convert.ToInt16(ResponseCodes.Success), "");
        //                jres.StringValue = s;
        //                ActionLogger.Logger.WriteLog("BatchNameByIdService success ", true);
        //            }
        //            catch (Exception ex)
        //            {
        //                jres = new PolicyStringResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
        //                ActionLogger.Logger.WriteLog("BatchNameByIdService failure ", true);
        //            }
        //            return jres;
        //        }

        //        //public void ViewBatchFile(string FilePath, string FileType)
        //        //{
        //        //    throw new NotImplementedException();
        //        //}

        //        //public void UploadBatchOfLocalPayor()
        //        //{
        //        //    throw new NotImplementedException();
        //        //}


        public BatchListExportResponse ExportBatchService(Guid batchId, string batchNumber, string payorName)
        {
            BatchListExportResponse jres = null;
            ActionLogger.Logger.WriteLog("ExportBatchService request: ", true);
            try
            {
                List<BatchEntry> lstBatches = (new Batch()).ExportBatch(batchId, batchNumber);
                if (lstBatches != null && lstBatches.Count > 0)
                {
                    jres = new BatchListExportResponse(string.Format("Batch data found successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                    jres.BatchList = lstBatches;//new List<BatchEntry>();//lstBatches;
                    ActionLogger.Logger.WriteLog("ExportBatchService success ", true);
                }
                else
                {
                    jres = new BatchListExportResponse(string.Format("Batch data not found"), Convert.ToInt16(ResponseCodes.RecordNotFound), "");
                    ActionLogger.Logger.WriteLog("ExportBatchService not found ", true);
                }
            }
            catch (Exception ex)
            {
                jres = new BatchListExportResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                ActionLogger.Logger.WriteLog("ExportBatchService failure ", true);
            }
            return jres;
        }

        //        public PolicyBoolResponse GetBatchPaidStatusService(Guid BatchId)
        //        {
        //            PolicyBoolResponse jres = null;
        //            ActionLogger.Logger.WriteLog("GetBatchPaidStatusService request: BatchId " + BatchId, true);
        //            try
        //            {
        //                Batch objBatch = new Batch();
        //                bool res = objBatch.GetBatchPaidStatus(BatchId);
        //                jres = new PolicyBoolResponse(string.Format("Batch status found successfully"), Convert.ToInt16(ResponseCodes.Success), "");
        //                jres.BoolFlag = res;
        //                ActionLogger.Logger.WriteLog("GetBatchPaidStatusService success ", true);
        //            }
        //            catch (Exception ex)
        //            {
        //                jres = new PolicyBoolResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
        //                ActionLogger.Logger.WriteLog("GetBatchPaidStatusService failure ", true);
        //            }
        //            return jres;
        //        }

        //        //public PolicyBoolResponse SetBatchesAsPaidService(List<Guid> BatchIds)
        //        //{
        //        //    PolicyBoolResponse jres = null;
        //        //    ActionLogger.Logger.WriteLog("SetBatchesAsPaidService request: BatchId " + BatchIds.ToStringDump(), true);
        //        //    try
        //        //    {
        //        //        Batch objBatch = new Batch();
        //        //        bool res = objBatch.SetBatchesAsPaid(BatchIds);
        //        //        jres = new PolicyBoolResponse(string.Format("Batch set to paid successfully"), Convert.ToInt16(ResponseCodes.Success), "");
        //        //        jres.BoolFlag = res;
        //        //        ActionLogger.Logger.WriteLog("SetBatchesAsPaidService success ", true);
        //        //    }
        //        //    catch (Exception ex)
        //        //    {
        //        //        jres = new PolicyBoolResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
        //        //        ActionLogger.Logger.WriteLog("SetBatchesAsPaidService failure ", true);
        //        //    }
        //        //    return jres;
        //        //}
        //        /// <summary>
        /// ModifiedBy:Ankit Khandelwal
        /// ModifiedOn:12-04-2018
        /// Purpose:Change the status of Batch
        /// </summary>
        /// <param name="BatchNumber"></param>
        /// <returns></returns>
        public PolicyBoolResponse SetBatchToUnPaidStatusService(int batchNumber)
        {
            PolicyBoolResponse jres = null;
            ActionLogger.Logger.WriteLog("SetBatchToUnPaidStatusService request: BatchNumber " + batchNumber, true);
            try
            {
                Batch objBatch = new Batch();
                bool res = objBatch.SetBatchToUnPaidStatus(batchNumber);
                jres = new PolicyBoolResponse(string.Format("Batch set to unpaid successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                jres.BoolFlag = res;
                ActionLogger.Logger.WriteLog("SetBatchToUnPaidStatusService success ", true);
            }
            catch (Exception ex)
            {
                jres = new PolicyBoolResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                ActionLogger.Logger.WriteLog("SetBatchToUnPaidStatusService failure ", true);
            }
            return jres;
        }

        //        //public PolicyBoolResponse SetBatchesToPaidService(List<Guid> BatchIds, string strFilter, List<Guid> lstPayee)
        //        //{
        //        //    PolicyBoolResponse jres = null;
        //        //    ActionLogger.Logger.WriteLog("SetBatchesToPaidService request: BatchIds " + BatchIds.ToStringDump(), true);
        //        //    try
        //        //    {
        //        //        Batch objBatch = new Batch();
        //        //        bool res = objBatch.SetBatchesToPaid(BatchIds, strFilter, lstPayee);
        //        //        jres = new PolicyBoolResponse(string.Format("Batches set to paid successfully"), Convert.ToInt16(ResponseCodes.Success), "");
        //        //        jres.BoolFlag = res;
        //        //        ActionLogger.Logger.WriteLog("SetBatchesToPaidService success ", true);
        //        //    }
        //        //    catch (Exception ex)
        //        //    {
        //        //        jres = new PolicyBoolResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
        //        //        ActionLogger.Logger.WriteLog("SetBatchesToPaidService failure ", true);
        //        //    }
        //        //    return jres;
        //        //}
        //        /// <summary>
        /// Modified By:Ankit khandelwal
        /// Modifiedon:10-05-2018
        /// </summary>
        /// <param name="batchIds"></param>
        /// <param name="strFilter"></param>
        /// <param name="lstPayee"></param>
        /// <returns></returns>
        public PolicyStringResponse SetBatchesToPaidInReportsService(string batchIds, string strFilter, string lstPayee, string lstSegments)
        {
            PolicyStringResponse jres = null;
            ActionLogger.Logger.WriteLog("SetBatchesToPaidInReportsService:Processing begins with batchIds"+batchIds, true);
            try
            {
                Batch objBatch = new Batch();
                string s = objBatch.SetBatchesToPaidInReports(batchIds, lstPayee, strFilter, lstSegments);
                jres = new PolicyStringResponse(string.Format("Batches set to paid successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                jres.StringValue = s;
                ActionLogger.Logger.WriteLog("SetBatchesToPaidInReportsService success ", true);
            }
            catch (Exception ex)
            {
                jres = new PolicyStringResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                ActionLogger.Logger.WriteLog("SetBatchesToPaidInReportsService:Exception occurs while processing"+ex.Message, true);
            }
            return jres;
        }
        /// <summary>
        /// Modified by :Ankit Khandelwal
        /// Modified On:29-05-2019
        /// Purpose:Update the FileName of Batch
        /// </summary>
        /// <param name="intBatchNumber"></param>
        /// <param name="strFileName"></param>
        /// <returns></returns>
        public JSONResponse UpdateBatchFileNameService(int batchNumber, string fileName)
        {
            JSONResponse jres = null;
            ActionLogger.Logger.WriteLog("UpdateBatchFileName request: intBatchNumber - " + batchNumber + ", strFileName: " + fileName, true);
            try
            {
                Batch objBatch = new Batch();
                objBatch.UpdateBatchFileName(batchNumber, fileName);
                jres = new JSONResponse(string.Format("Batch file name updated successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                ActionLogger.Logger.WriteLog("UpdateBatchFileName success ", true);
            }
            catch (Exception ex)
            {
                jres = new JSONResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                ActionLogger.Logger.WriteLog("UpdateBatchFileName failure ", true);
            }
            return jres;
        }

        //        #endregion

        //        #region Download Batch methods

        //        public DownloadBatchResponse GetDownloadBatchListService()
        //        {
        //            DownloadBatchResponse jres = null;
        //            ActionLogger.Logger.WriteLog("GetDownloadBatchListService request", true);
        //            try
        //            {
        //                DownloadBatch objDownloadBatch = new DownloadBatch();
        //                List<DownloadBatch> lst = objDownloadBatch.GetDownloadBatchList();
        //                if (lst != null && lst.Count > 0)
        //                {
        //                    jres = new DownloadBatchResponse(string.Format("Batch list found successfully"), Convert.ToInt16(ResponseCodes.Success), "");
        //                    jres.BatchList = lst;
        //                }
        //                else
        //                {
        //                    jres = new DownloadBatchResponse(string.Format("No records found"), Convert.ToInt16(ResponseCodes.RecordNotFound), "");
        //                }
        //                ActionLogger.Logger.WriteLog("GetDownloadBatchListService success ", true);
        //            }
        //            catch (Exception ex)
        //            {
        //                jres = new DownloadBatchResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
        //                ActionLogger.Logger.WriteLog("GetDownloadBatchListService failure ", true);
        //            }
        //            return jres;
        //        }

        //        public PolicyBoolResponse IsBatchPartiallyOrFullyPaidService(DownloadBatch batch)
        //        {
        //            PolicyBoolResponse jres = null;
        //            ActionLogger.Logger.WriteLog("IsBatchPartiallyOrFullyPaidService request: batch -" + batch.ToStringDump(), true);
        //            try
        //            {
        //                bool res = batch.isBatchPartiallyOrFullyPaid();
        //                jres = new PolicyBoolResponse(string.Format("Batch status found successfully"), Convert.ToInt16(ResponseCodes.Success), "");
        //                jres.BoolFlag = res;
        //                ActionLogger.Logger.WriteLog("IsBatchPartiallyOrFullyPaidService success ", true);
        //            }
        //            catch (Exception ex)
        //            {
        //                jres = new PolicyBoolResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
        //                ActionLogger.Logger.WriteLog("IsBatchPartiallyOrFullyPaidService failure ", true);
        //            }
        //            return jres;
        //        }

        //        public JSONResponse DeleteDownloadBatchService(DownloadBatch batch, UserRole _UserRole)
        //        {
        //            JSONResponse jres = null;
        //            ActionLogger.Logger.WriteLog("DeleteDownloadBatchService request: batch" + batch.ToStringDump(), true);
        //            try
        //            {
        //                batch.DeleteDownloadBatch(_UserRole);
        //                jres = new JSONResponse(string.Format("Batch deleted successfully"), Convert.ToInt16(ResponseCodes.Success), "");
        //                ActionLogger.Logger.WriteLog("DeleteDownloadBatchService success ", true);
        //            }
        //            catch (Exception ex)
        //            {
        //                jres = new JSONResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
        //                ActionLogger.Logger.WriteLog("DeleteDownloadBatchService failure ", true);
        //            }
        //            return jres;
        //        }

        //        public ImportFileResponse ImportBatchFileService(DownloadBatch batch, UserRole _UserRole)
        //        {
        //            ImportFileResponse jres = null;
        //            ActionLogger.Logger.WriteLog("ImportBatchFileService request: batch" + batch.ToStringDump(), true);
        //            try
        //            {
        //                ImportFileData obj = batch.ImportBatchFile(_UserRole);

        //                if (obj != null)
        //                {
        //                    jres = new ImportFileResponse(string.Format("Batch file imported successfully"), Convert.ToInt16(ResponseCodes.Success), "");
        //                    jres.ImportFile = obj;
        //                }
        //                else
        //                {
        //                    jres = new ImportFileResponse(string.Format("Batch file could not be imported"), Convert.ToInt16(ResponseCodes.Failure), "Batch file could not be imported");
        //                }
        //                ActionLogger.Logger.WriteLog("ImportBatchFileService success ", true);
        //            }
        //            catch (Exception ex)
        //            {
        //                jres = new ImportFileResponse("Batch file could not be imported", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
        //                ActionLogger.Logger.WriteLog("ImportBatchFileService failure ", true);
        //            }
        //            return jres;
        //        }

        //        public PolicyDateResponse UpdateEntryStatusService(DownloadBatch batch)
        //        {
        //            PolicyDateResponse jres = null;
        //            ActionLogger.Logger.WriteLog("UpdateEntryStatusService request: batch" + batch.ToStringDump(), true);
        //            try
        //            {
        //                DateTime? dt = batch.UpdateEntryStatus();
        //                jres = new PolicyDateResponse(string.Format("Entry status updated successfully"), Convert.ToInt16(ResponseCodes.Success), "");
        //                jres.PolicyDate = (dt == null) ? string.Empty : dt.ToString();
        //                ActionLogger.Logger.WriteLog("UpdateEntryStatusService success ", true);
        //            }
        //            catch (Exception ex)
        //            {
        //                jres = new PolicyDateResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
        //                ActionLogger.Logger.WriteLog("UpdateEntryStatusService failure ", true);
        //            }
        //            return jres;
        //        }

        //        public JSONResponse ClearDownloadBatchService(DownloadBatch batch, UserRole _UserRole)
        //        {
        //            JSONResponse jres = null;
        //            ActionLogger.Logger.WriteLog("ClearDownloadBatchService request: batch" + batch.ToStringDump(), true);
        //            try
        //            {
        //                batch.ClearDownloadBatch(_UserRole);
        //                jres = new JSONResponse(string.Format("Batch cleared successfully"), Convert.ToInt16(ResponseCodes.Success), "");
        //                ActionLogger.Logger.WriteLog("ClearDownloadBatchService success ", true);
        //            }
        //            catch (Exception ex)
        //            {
        //                jres = new JSONResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
        //                ActionLogger.Logger.WriteLog("ClearDownloadBatchService failure ", true);
        //            }
        //            return jres;
        //        }
        //        #endregion
    }
}
