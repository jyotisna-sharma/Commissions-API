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
    interface IStatementService
    {
        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        BatchStatementResponse GetBatchStatmentService(Guid batchId);


        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        JSONResponse UpdateStatementDateService(Guid statementId, string statementDateString);

        //        [OperationContract]
        //        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        //        BatchStatementResponse GetBatchStatmentWithoutCalculationService(Guid BatchId);

        //        [OperationContract]
        //        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        //        JSONResponse AddUpdateBatchStatmentRecordService(BatchStatmentRecords _BatchStatmentRecord);

        //        [OperationContract]
        //        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        //        PolicyPMCResponse GetBatchTotalService(Guid BatchId);

        //        [OperationContract]
        //        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        //        JSONResponse PostStatementService(Statement Stment);

       

        //        [OperationContract]
        //        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        //        PolicyBoolResponse CloseStatementFromDeuService(Statement Stment);

        //        [OperationContract]
        //        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        //        PolicyBoolResponse OpenStatementService(Statement Stment);

     

        

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        PolicyBoolResponse DeleteStatementService(Guid statementId, UserRole userRole);

        //        [OperationContract]
        //        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        //        StatementResponse GetStatementService(Guid StatementID);

        //        [OperationContract]
        //        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        //        ModifiedStmtResponse UpdateCheckAmountService(Guid StatementID, decimal CheckAmount, decimal NetAmount, decimal Adjustment);
    }

    public partial class MavService : IStatementService
    {
        #region IBatchStatmentRecords Members
        /// <summary>
        /// Modified By:Ankit Kahndelwal
        /// Modified On:22-05-2019
        /// Purpose:Get the list of statements based on batchId
        /// </summary>
        /// <param name="batchId"></param>
        /// <returns></returns>
        public BatchStatementResponse GetBatchStatmentService(Guid batchId)
        {
            BatchStatementResponse jres = null;
            ActionLogger.Logger.WriteLog("GetBatchStatmentService request " + batchId, true);
            try
            {
                List<BatchStatmentRecords> lst = BatchStatmentRecords.GetBatchStatment(batchId, out List<ComListObject> TotalListAmountData);
                jres = new BatchStatementResponse(string.Format("Data found successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                jres.TotalRecords = lst;
                jres.TotalLength = lst.Count;
                jres.footerData = TotalListAmountData;
                ActionLogger.Logger.WriteLog("GetBatchStatmentService success ", true);
            }
            catch (Exception ex)          
            {
                jres = new BatchStatementResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                ActionLogger.Logger.WriteLog("GetBatchStatmentService failure ", true);
            }
            return jres;
        }
        /// <summary>
        /// Created By:Ankit kHandelwal
        /// Createdon:10-22-2019
        /// </summary>
        /// <param name="statementId"></param>
        /// <param name="statementDateString"></param>
        /// <returns></returns>
        public JSONResponse UpdateStatementDateService(Guid statementId, string statementDateString)
        {
            JSONResponse jres = null;
            ActionLogger.Logger.WriteLog("UpdateStatementDateService request " + statementId, true);
            try
            {
                bool isStatementDateUpdate = Statement.UpdateStatementDate(statementId, statementDateString);
                jres = new JSONResponse(string.Format("Date Upodated successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                ActionLogger.Logger.WriteLog("UpdateStatementDateService:processing success", true);
            }
            catch (Exception ex)
            {
                jres = new BatchStatementResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                ActionLogger.Logger.WriteLog("UpdateStatementDateService: Exception occurs" + ex.Message, true);
            }
            return jres;
        }


        //         public BatchStatementResponse GetBatchStatmentWithoutCalculationService(Guid BatchId)
        //         {
        //             BatchStatementResponse jres = null;
        //             ActionLogger.Logger.WriteLog("GetBatchStatmentWithoutCalculationService request " + BatchId, true);
        //             try
        //             {
        //                 List<BatchStatmentRecords> lst = BatchStatmentRecords.GetBatchStatmentWithoutCalculation(BatchId);
        //                 jres = new BatchStatementResponse(string.Format("Data found successfully"), Convert.ToInt16(ResponseCodes.Success), "");
        //                 jres.StatementList = lst;
        //                 ActionLogger.Logger.WriteLog("GetBatchStatmentWithoutCalculationService success ", true);
        //             }
        //             catch (Exception ex)
        //             {
        //                 jres = new BatchStatementResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
        //                 ActionLogger.Logger.WriteLog("GetBatchStatmentWithoutCalculationService failure ", true);
        //             }
        //             return jres;

        //         }

        //         public JSONResponse AddUpdateBatchStatmentRecordService(BatchStatmentRecords _BatchStatmentRecord)
        //         {
        //             JSONResponse jres = null;
        //             ActionLogger.Logger.WriteLog("AddUpdateBatchStatmentRecordService request: " + _BatchStatmentRecord.ToStringDump(), true);
        //             try
        //             {
        //                 _BatchStatmentRecord.AddUpdateBatchStatmentRecord(_BatchStatmentRecord);
        //                 jres = new JSONResponse(string.Format("Batch statement record saved successfully"), Convert.ToInt16(ResponseCodes.Success), "");
        //                 ActionLogger.Logger.WriteLog("AddUpdateBatchStatmentRecordService success ", true);
        //             }
        //             catch (Exception ex)
        //             {
        //                 jres = new JSONResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
        //                 ActionLogger.Logger.WriteLog("AddUpdateBatchStatmentRecordService failure ", true);
        //             }
        //             return jres;
        //         }

        //         public PolicyPMCResponse GetBatchTotalService(Guid BatchId)
        //         {
        //             PolicyPMCResponse jres = null;
        //             ActionLogger.Logger.WriteLog("GetBatchTotalService request - " + BatchId, true);
        //             try
        //             {
        //                 decimal val = BatchStatmentRecords.GetBatchTotal(BatchId);
        //                 ActionLogger.Logger.WriteLog("GetBatchTotalService value: " + val, true);
        //                 if ( val > 0)
        //                 {
        //                     jres = new PolicyPMCResponse(string.Format("Data found successfully"), Convert.ToInt16(ResponseCodes.Success), "");
        //                     jres.NumberValue = val;
        //                 }
        //                 else
        //                 {
        //                     jres = new PolicyPMCResponse("Batch total not found!", Convert.ToInt16(ResponseCodes.RecordNotFound), "");
        //                     ActionLogger.Logger.WriteLog("GetBatchTotalService 404 ", true);
        //                 }
        //             }
        //             catch (Exception ex)
        //             {
        //                 jres = new PolicyPMCResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
        //                 ActionLogger.Logger.WriteLog("GetBatchTotalService failure ", true);
        //             }
        //             return jres;
        //         }
        //         #endregion


        //         public JSONResponse PostStatementService(Statement Stment)
        //         {
        //             JSONResponse jres = null;
        //             ActionLogger.Logger.WriteLog("PostStatementService request: " + Stment.ToStringDump(), true);
        //             try
        //             {
        //                 Stment.PostStatement();
        //                 jres = new JSONResponse(string.Format("Statement posted  successfully"), Convert.ToInt16(ResponseCodes.Success), "");
        //                 ActionLogger.Logger.WriteLog("PostStatementService success ", true);
        //             }
        //             catch (Exception ex)
        //             {
        //                 jres = new JSONResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
        //                 ActionLogger.Logger.WriteLog("PostStatementService failure ", true);
        //             }
        //             return jres;
        //         }

       

        //         public PolicyBoolResponse OpenStatementService(Statement Stment)
        //         {
        //             PolicyBoolResponse jres = null;
        //             ActionLogger.Logger.WriteLog("OpenStatementService request: Stment -" + Stment.ToStringDump(), true);
        //             try
        //             {
        //                 bool res =  Stment.OpenStatement();
        //                 jres = new PolicyBoolResponse(string.Format("Statement opened successfully"), Convert.ToInt16(ResponseCodes.Success), "");
        //                 jres.BoolFlag = res;
        //                 ActionLogger.Logger.WriteLog("OpenStatementService success ", true);
        //             }
        //             catch (Exception ex)
        //             {
        //                 jres = new PolicyBoolResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
        //                 ActionLogger.Logger.WriteLog("OpenStatementService failure ", true);
        //             }
        //             return jres;
        //         }

       


        public PolicyBoolResponse DeleteStatementService(Guid statementId, UserRole userRole)
        {
            PolicyBoolResponse jres = null;
            ActionLogger.Logger.WriteLog("DeleteStatement request: Stment -" + statementId, true);
            try
            {
                bool res = Statement.DeleteStatement(statementId, userRole, string.Empty); //acme - last parameter has been removed from method signature , as has no significance further
                jres = new PolicyBoolResponse(string.Format("Statement deleted successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                jres.BoolFlag = res;
                ActionLogger.Logger.WriteLog("DeleteStatement success ", true);
            }
            catch (Exception ex)
            {
                jres = new PolicyBoolResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                ActionLogger.Logger.WriteLog("DeleteStatement failure ", true);
            }
            return jres;
        }

        //         public StatementResponse GetStatementService(Guid StatementID)
        //         {
        //             StatementResponse jres = null;
        //             ActionLogger.Logger.WriteLog("GetStatementService request: StatementNumber " + StatementID, true);
        //             try
        //             {
        //                 Statement obj = Statement.GetStatement(StatementID);
        //                 ActionLogger.Logger.WriteLog("GetStatementService value: " + obj.ToStringDump(), true);
        //                 if (obj != null)
        //                 {
        //                     jres = new StatementResponse(string.Format("Data found successfully"), Convert.ToInt16(ResponseCodes.Success), "");
        //                     jres.StatementObject = obj;
        //                 }
        //                 else
        //                 {
        //                     jres = new StatementResponse("", Convert.ToInt16(ResponseCodes.RecordNotFound), "Status not found.");
        //                     ActionLogger.Logger.WriteLog("GetStatementService failure ", true);
        //                 }
        //             }
        //             catch (Exception ex)
        //             {
        //                 jres = new StatementResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
        //                 ActionLogger.Logger.WriteLog("GetFindStatementService failure ", true);
        //             }
        //             return jres;
        //         }

        //         public ModifiedStmtResponse UpdateCheckAmountService(Guid statementId, decimal checkAmount, decimal dcNetAmount, decimal adjustment)
        //         {
        //             ModifiedStmtResponse jres = null;
        //             ActionLogger.Logger.WriteLog("UpdateCheckAmountService request: Stment -" + statementId + ", check:  " + checkAmount + ", netAmt: " +dcNetAmount + ", adj: " + adjustment , true);
        //             try
        //             {
        //                 ModifiableStatementData obj = Statement.UpdateCheckAmount(statementId, checkAmount, dcNetAmount, adjustment);
        //                 jres = new ModifiedStmtResponse(string.Format("Check amount updated successfully"), Convert.ToInt16(ResponseCodes.Success), "");
        //                 jres.StatementObject = obj;
        //                 ActionLogger.Logger.WriteLog("UpdateCheckAmountService success ", true);
        //             }
        //             catch (Exception ex)
        //             {
        //                 jres = new ModifiedStmtResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
        //                 ActionLogger.Logger.WriteLog("UpdateCheckAmountService failure ", true);
        //             }
        //             return jres;
        //         }

        //         public PolicyBoolResponse CloseStatementFromDeuService(Statement objStatement)
        //         {
        //             PolicyBoolResponse jres = null;
        //             ActionLogger.Logger.WriteLog("CloseStatementFromDeuService request: Stment -" + objStatement.ToStringDump(), true);
        //             try
        //             {
        //                 bool res = objStatement.CloseStatementFromDeu(objStatement);
        //                 jres = new PolicyBoolResponse(string.Format("Statement closed from DEU successfully"), Convert.ToInt16(ResponseCodes.Success), "");
        //                 jres.BoolFlag = res;
        //                 ActionLogger.Logger.WriteLog("CloseStatementFromDeuService success ", true);
        //             }
        //             catch (Exception ex)
        //             {
        //                 jres = new PolicyBoolResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
        //                 ActionLogger.Logger.WriteLog("CloseStatementFromDeuService failure ", true);
        //             }
        //             return jres;
        //         }
    }
    #endregion
}