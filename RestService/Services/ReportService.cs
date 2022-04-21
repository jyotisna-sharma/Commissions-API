using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ServiceModel;
using System.ServiceModel.Web;
using MyAgencyVault.BusinessLibrary;
using MyAgencyVault.WcfService.Library.Response;
using MyAgencyVault.BusinessLibrary.BusinessObjects;

namespace MyAgencyVault.WcfService
{
    [ServiceContract]
    interface IReportService
    {
        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        ReportListResponse GetReportNamesService(string reportGroupName);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        PrintReportResponse SavePayeeStatementReportService(PayeeStatementReport report, Guid userCredentialId);

        //[OperationContract]
        //[WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        //PrintReportResponse PrintReportService(Guid Id, string reportType, string Format);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        PrintReportResponse SaveAuditReportService(AuditReport report, Guid userCredentialId);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        PrintReportResponse SaveManagementReportService(ManagementReport report, Guid userCredentialId);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        PolicyBoolResponse PrintReportAndSendMailService(Guid Id, string reportType, Guid userId);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        GetPayeeListResponse GetPayeeListService(Guid licenseeId);
    }

    public partial class MavService : IReportService
    {

        /// <summary>
        /// Modified By:Ankit Kahndelwal
        /// ModifiedOn:03-05-2019
        /// Purpose:geettin glist of Reports Name
        /// </summary>
        /// <param name="reportGroupName"></param>
        /// <returns></returns>
        public ReportListResponse GetReportNamesService(string reportGroupName)
        {
            ReportListResponse jres = null;
            ActionLogger.Logger.WriteLog("GetReportNamesService: Processing begins with reportGroupname" + " " + reportGroupName, true);
            try
            {
                List<Report> lst = Report.GetReports(reportGroupName);
                if (lst != null && lst.Count > 0)
                {
                    jres = new ReportListResponse(string.Format("Data found successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                    jres.ReportsList = lst;
                    jres.Count = lst.Count;
                }
                else
                {
                    jres = new ReportListResponse(string.Format("No data found "), Convert.ToInt16(ResponseCodes.RecordNotFound), "");
                }

                ActionLogger.Logger.WriteLog("GetReportNamesService: Processing Success with reportGroupname" + " " + reportGroupName, true);
            }
            catch (Exception ex)
            {
                jres = new ReportListResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                ActionLogger.Logger.WriteLog("GetReportNamesService:Exception occurs with reportGroupname" + " " + reportGroupName + "Exception:" + ex.Message, true);
            }
            return jres;
        }
        /// <summary>
        /// Created By :Ankit Khandelwal
        /// CreatedOn:03-05-2019
        /// purpose:Get list of all payees
        /// </summary>
        /// <param name="licenseeId"></param>
        /// <returns></returns>
        public GetPayeeListResponse GetPayeeListService(Guid licenseeId)
        {
            ActionLogger.Logger.WriteLog("GetAllPayeeByLicencessIDService request: ", true);
            GetPayeeListResponse jres = null;
            try
            {
                List<GetPayeeList> lst = Report.GetPayeeList(licenseeId);
                jres = new GetPayeeListResponse("Users list found sucscessfully", (int)ResponseCodes.Success, "");
                jres.PayeeList = lst;
                ActionLogger.Logger.WriteLog("GetAllPayeeByLicencessIDService success: ", true);
            }
            catch (Exception ex)
            {
                jres = new GetPayeeListResponse("", (int)ResponseCodes.Failure, "Error getting users list" + ex.Message);
                ActionLogger.Logger.WriteLog("GetAllPayeeByLicencessIDService failure: " + ex.Message, true);
                throw ex;
            }
            return jres;
        }
        public PrintReportResponse SavePayeeStatementReportService(PayeeStatementReport report, Guid userCredentialId)
        {
            PrintReportResponse jres = null;
            ActionLogger.Logger.WriteLog("SavePayeeStatementReportService request: ", true);
            try
            {
                PrintReportOutput output = Report.SaveAndPrintPayeeStatement(report, userCredentialId);
                jres = new PrintReportResponse(string.Format("Payee statement report saved successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                jres.ReportObject = output;
                ActionLogger.Logger.WriteLog("SavePayeeStatementReportService success ", true);
            }
            catch (Exception ex)
            {
                jres = new PrintReportResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                ActionLogger.Logger.WriteLog("SavePayeeStatementReportService failure ", true);
            }
            return jres;
        }

        //public PrintReportResponse PrintReportService(Guid Id, string reportType, string Format)
        //{
        //    PrintReportResponse jres = null;
        //    ActionLogger.Logger.WriteLog("PrintReportService request: " + Id + ", reporttype " + reportType, true);
        //    try
        //    {
        //        PrintReportOutput obj = Report.PrintReport(Id, reportType, Format);
        //        jres = new PrintReportResponse(string.Format("Report printed successfully"), Convert.ToInt16(ResponseCodes.Success), "");
        //        jres.ReportObject = obj;
        //        ActionLogger.Logger.WriteLog("PrintReportService success ", true);
        //    }
        //    catch (Exception ex)
        //    {
        //        jres = new PrintReportResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
        //        ActionLogger.Logger.WriteLog("PrintReportService failure ", true);
        //    }
        //    return jres;
        //}

        public PrintReportResponse SaveAuditReportService(AuditReport report, Guid userCredentialId)
        {
            PrintReportResponse jres = null;
            ActionLogger.Logger.WriteLog("SaveAuditReportService request: ", true);
            try
            {
                PrintReportOutput output = Report.SaveAndPrintAuditReport(report, userCredentialId);
                jres = new PrintReportResponse(string.Format("Audit report saved successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                jres.ReportObject = output;
                ActionLogger.Logger.WriteLog("SaveAuditReportService success ", true);
            }
            catch (Exception ex)
            {
                jres = new PrintReportResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                ActionLogger.Logger.WriteLog("SaveAuditReportService failure ", true);
            }
            return jres;
        }

        public PrintReportResponse SaveManagementReportService(ManagementReport report, Guid userCredentialId)
        {
            PrintReportResponse jres = null;
            ActionLogger.Logger.WriteLog("SaveManagementReportService request: " + report.ReportId + ", user: " + userCredentialId, true);
            try
            {
                PrintReportOutput output =  Report.SaveManagementReport(report, userCredentialId);
                jres = new PrintReportResponse(string.Format("Management report saved successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                jres.ReportObject = output;
                ActionLogger.Logger.WriteLog("SaveManagementReportService success ", true);
            }
            catch (Exception ex)
            {
                jres = new PrintReportResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                ActionLogger.Logger.WriteLog("SaveManagementReportService failure ", true);
            }
            return jres;
        }

        public PolicyBoolResponse PrintReportAndSendMailService(Guid Id, string reportType, Guid userId)
        {
            PolicyBoolResponse jres = null;
            ActionLogger.Logger.WriteLog("PrintReportAndSendMailService request: Id -" + Id + ", reportType: " + reportType, true);
            try
            {

                bool res = Report.PrintReportAndSendMail(Id, reportType, userId);
                jres = new PolicyBoolResponse(string.Format("Data deleted successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                jres.BoolFlag = res;
                ActionLogger.Logger.WriteLog("PrintReportAndSendMailService success ", true);
            }
            catch (Exception ex)
            {
                jres = new PolicyBoolResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                ActionLogger.Logger.WriteLog("PrintReportAndSendMailService failure ", true);
            }
            return jres;
        }
    }
}