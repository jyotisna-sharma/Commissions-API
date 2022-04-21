using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ServiceModel;
using MyAgencyVault.BusinessLibrary;
using System.ServiceModel.Web;
using MyAgencyVault.WcfService.Library.Response;

namespace MyAgencyVault.WcfService.Services
{
    [ServiceContract]
    interface IStatementDatesService
    {
        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        JSONResponse AddUpdateStatementDatesService(List<StatementDates> StatementDate);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        JSONResponse DeleteStatementDatesService(List<StatementDates> StatementDate);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        StatementDatesListResponse GetStatementDatesService();

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        StatementDatesListResponse GetActiveStatementDatesService();

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        JSONResponse MarkAsBatchGeneratedService(List<StatementDates> Dates);
    }

    public partial class MavService : IStatementDatesService
    {
        public JSONResponse AddUpdateStatementDatesService(List<StatementDates> StatementDate)
        {
            ActionLogger.Logger.WriteLog("AddUpdateStatementDatesService request: StatementDate" + StatementDate.ToStringDump(), true);
            JSONResponse jres = null;
            try
            {
                StatementDates.AddUpdate(StatementDate);
                jres = new JSONResponse("Statement dates saved successfully", (int)ResponseCodes.Success, "");
                ActionLogger.Logger.WriteLog("AddUpdateStatementDatesService success", true);
            }
            catch (Exception ex)
            {
                jres = new JSONResponse("", (int)ResponseCodes.Failure, ex.Message);
                ActionLogger.Logger.WriteLog("AddUpdateStatementDatesService failure", true);
            }
            return jres;
        }

        public JSONResponse DeleteStatementDatesService(List<StatementDates> StatementDate)
        {
            ActionLogger.Logger.WriteLog("DeleteStatementDatesService request: StatementDate" + StatementDate.ToStringDump(), true);
            JSONResponse jres = null;
            try
            {
                StatementDates.Delete(StatementDate);
                jres = new JSONResponse("Statement dates deleted successfully", (int)ResponseCodes.Success, "");
                ActionLogger.Logger.WriteLog("DeleteStatementDatesService success", true);
            }
            catch (Exception ex)
            {
                jres = new JSONResponse("", (int)ResponseCodes.Failure, ex.Message);
                ActionLogger.Logger.WriteLog("DeleteStatementDatesService failure", true);
            }
            return jres;
        }

        public StatementDatesListResponse GetStatementDatesService()
        {
            ActionLogger.Logger.WriteLog("GetStatementDatesService request: ", true);
             StatementDatesListResponse jres = null;
            try
            {
                List<StatementDates> lst = StatementDates.GetStatementDate();
        
                if (lst != null && lst.Count > 0)
                {
                    jres = new StatementDatesListResponse("Statement dates found successfully", (int)ResponseCodes.Success, "");
                    jres.StatementDatesList = lst;
                    ActionLogger.Logger.WriteLog("GetStatementDatesService success: ", true);
                }
                else
                {
                    jres = new StatementDatesListResponse("No statement dates found", (int)ResponseCodes.RecordNotFound, "");
                    ActionLogger.Logger.WriteLog("GetStatementDatesService 404: ", true);
                }
               
            }
            catch (Exception ex)
            {
                jres = new StatementDatesListResponse("", (int)ResponseCodes.Failure,  ex.Message);
                ActionLogger.Logger.WriteLog("GetStatementDatesService failure: ", true);
            }
            return jres;
        }

        public StatementDatesListResponse GetActiveStatementDatesService()
        {
            ActionLogger.Logger.WriteLog("GetActiveStatementDatesService request: ", true);
            StatementDatesListResponse jres = null;
            try
            {
                List<StatementDates> lst = StatementDates.GetActiveStatementDates();
           
                if (lst != null && lst.Count > 0)
                {
                    jres = new StatementDatesListResponse("Active statement dates found successfully", (int)ResponseCodes.Success, "");
                    jres.StatementDatesList = lst;
                    ActionLogger.Logger.WriteLog("GetActiveStatementDatesService success: ", true);
                }
                else
                {
                    jres = new StatementDatesListResponse("No active statement dates found", (int)ResponseCodes.RecordNotFound, "");
                    ActionLogger.Logger.WriteLog("GetActiveStatementDatesService 404: ", true);
                }
             
            }
            catch (Exception ex)
            {
                jres = new StatementDatesListResponse("", (int)ResponseCodes.Failure, "Error getting users list" + ex.Message);
                ActionLogger.Logger.WriteLog("GetStatementDatesService failure: ", true);
            }
            return jres;
        }

        public JSONResponse MarkAsBatchGeneratedService(List<StatementDates> Dates)
        {
            ActionLogger.Logger.WriteLog("MarkAsBatchGeneratedService request: StatementDate" + Dates.ToStringDump(), true);
            JSONResponse jres = null;
            try
            {
                StatementDates.MarkAsBatchGenerated(Dates);
                jres = new JSONResponse("Status updated successfully", (int)ResponseCodes.Success, "");
                ActionLogger.Logger.WriteLog("MarkAsBatchGeneratedService success", true);
            }
            catch (Exception ex)
            {
                jres = new JSONResponse("", (int)ResponseCodes.Failure, ex.Message);
                ActionLogger.Logger.WriteLog("MarkAsBatchGeneratedService failure", true);
            }
            return jres;
        }
    }
}