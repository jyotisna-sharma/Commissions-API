using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ServiceModel;
using System.ServiceModel.Web;
using MyAgencyVault.BusinessLibrary;
using MyAgencyVault.WcfService.Library.Response;
using System.Globalization;

namespace MyAgencyVault.WcfService
{
    [ServiceContract]
    interface IDashboardService
    {
        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        DashboardResponse GetCountForMainDashboard(Guid LicenseeID);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        DashboardRevenueResponse GetRevenueForGraph(Guid LicenseeID, Guid UserCredentialID, List<int> Years, int StartMonth, int EndMonth);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        DashboardAgentsResponse GetAgentsData(Guid LicenseeID, string StartDate, string EndDate, string Filter = "Agent", bool isFirstTimeList = false);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        DashboardLOCRevenuResponse GetRevenueByLOCData(Guid LicenseeID, Guid UserCredentialID, string StartDate, string EndDate, bool isFirstTimeList);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        DashboardNewPolicyListResponse GetNewPolicyListService(Guid licenseeId);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        DashboardRenewalsListResponse GetRenewableListService(Guid licenseeId);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        DashboardReceivaleListResponse GetReceivablesListService(Guid licenseeId, string numberOfDays);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        DashboardClientsResponse GetClientsWithMultipleProductsService(Guid LicenseeID, Guid UserCredentialID, bool isFirstTimeList);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        DashboardAgentDetailExportResponse GetAgentDetailforExport(Guid LicenseeID, string StartDate, string EndDate, string Filter = "Agent");

    }

    public partial class MavService : IDashboardService
    {

        public DashboardResponse GetCountForMainDashboard(Guid LicenseeID)
        {
            DashboardResponse jres = null;
            ActionLogger.Logger.WriteLog("GetCountForMainDashboard request: " + LicenseeID, true);
            try
            {
                int agent = 0; int client = 0;
                Dashboard.GetDashboardCount(LicenseeID, out agent, out client);
                jres = new DashboardResponse(string.Format("dashboard count found successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                jres.AgentCount = agent;
                jres.ClientCount = client;
                ActionLogger.Logger.WriteLog("GetCountForMainDashboard success ", true);
            }
            catch (Exception ex)
            {
                jres = new DashboardResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                ActionLogger.Logger.WriteLog("GetCountForMainDashboard:failure occur with LicenseeId" + LicenseeID + "exception:" + ex.Message, true);
            }
            return jres;
        }


        /// <summary>
        /// Create by :Ankit khandelwal
        /// CreatedOn:31-10-2019
        /// Purpose:Getting List of New Policies
        /// </summary>
        /// <param name="licenseeId"></param>
        /// <returns></returns>
        public DashboardNewPolicyListResponse GetNewPolicyListService(Guid licenseeId)
        {
            ActionLogger.Logger.WriteLog("GetNewPolicyListService:Processing start with licenseeId: " + licenseeId, true);
            DashboardNewPolicyListResponse response = null;
            try
            {
                List<NewPolicyList> listData = Dashboard.GetNewPolicyList(licenseeId, out List<NewPolicyList> exportDataList);
                response = new DashboardNewPolicyListResponse("", Convert.ToInt16(ResponseCodes.Success), "");
                response.TotalRecords = listData;
                response.TotalLength = listData.Count;
                response.ExportDataList = exportDataList;
            }
            catch (Exception ex)
            {
                ActionLogger.Logger.WriteLog("GetNewPolicyListService:Exception occurs with licenseeId: " + licenseeId + " " + "Exception" + ex.Message, true);
                response = new DashboardNewPolicyListResponse("Exception occurs", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
            }
            return response;
        }

        public DashboardReceivaleListResponse GetReceivablesListService(Guid licenseeId, string numberOfDays)
        {
            ActionLogger.Logger.WriteLog("GetNewPolicyListService:Processing start with licenseeId: " + licenseeId, true);
            DashboardReceivaleListResponse response = null;
            try
            {
                List<ReceivablesList> listData = Dashboard.GetNReceivablesList(licenseeId, numberOfDays);
                response = new DashboardReceivaleListResponse("", Convert.ToInt16(ResponseCodes.Success), "");
                response.TotalRecords = listData;
                response.TotalLength = listData.Count;
            }
            catch (Exception ex)
            {
                ActionLogger.Logger.WriteLog("GetNewPolicyListService:Exception occurs with licenseeId: " + licenseeId + " " + "Exception" + ex.Message, true);
                response = new DashboardReceivaleListResponse("Exception occurs", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
            }
            return response;
        }




        /// <summary>
        /// Create by :Ankit khandelwal
        /// CreatedOn:31-10-2019
        /// Purpose:Getting List of Renewables
        /// </summary>
        /// <param name="licenseeId"></param>
        /// <returns></returns>
        public DashboardRenewalsListResponse GetRenewableListService(Guid licenseeId)
        {
            ActionLogger.Logger.WriteLog("GetRenewableListService:Processing start with licenseeId: " + licenseeId, true);
            DashboardRenewalsListResponse response = null;
            try
            {
                List<RenewalsList> listData = Dashboard.GetRenewalsList(licenseeId, out List<RenewalsList> exportDataList);
                response = new DashboardRenewalsListResponse("", Convert.ToInt16(ResponseCodes.Success), "");
                response.TotalRecords = listData;
                response.TotalLength = listData.Count;
                response.ExportDataList = exportDataList;
            }
            catch (Exception ex)
            {
                ActionLogger.Logger.WriteLog("GetRenewableListService:Exception occurs with licenseeId: " + licenseeId + " " + "Exception" + ex.Message, true);
                response = new DashboardRenewalsListResponse("Exception occurs", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
            }
            return response;
        }

        public DashboardRevenueResponse GetRevenueForGraph(Guid LicenseeID, Guid UserCredentialID, List<int> lstYears, int startMonth, int endMonth)

        {
            DashboardRevenueResponse jres = null;
            ActionLogger.Logger.WriteLog("GetRevenueForGraph: processing begins with LicenseeId: " + LicenseeID, true);
            try
            {
                List<string> months = new List<string>();
                for (int i = startMonth; i <= endMonth; i++)
                {
                    months.Add(CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(i));
                }
                List<LineGraphData> netRevenueList = new List<LineGraphData>();
                AllRevenueData grossNumbers = new AllRevenueData();
                AllRevenueData netNumbers = new AllRevenueData();
                Guid clientID = Guid.Empty;
                List<LineGraphData> data = Dashboard.GetDashboardRevenueList(LicenseeID, UserCredentialID, startMonth, endMonth, lstYears, out netRevenueList, out grossNumbers, out netNumbers, out clientID, out string refreshTime);
                jres = new DashboardRevenueResponse(string.Format("dashboard count found successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                jres.DefaultClient = clientID;
                jres.RefreshTime = refreshTime;
                jres.MonthsRange = months;
                jres.GrossRevenueData = data;
                jres.NetRevenueData = netRevenueList;
                jres.GrossRevenueNumbers = grossNumbers;
                jres.NetRevenueNumbers = netNumbers;
                ActionLogger.Logger.WriteLog("GetRevenueForGraph: list found successfully", true);
            }
            catch (Exception ex)
            {
                jres = new DashboardRevenueResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                ActionLogger.Logger.WriteLog("GetRevenueForGraph:Failure occurs-" + ex.Message, true);
            }
            return jres;
        }

        public DashboardAgentsResponse GetAgentsData(Guid LicenseeID, string StartDate, string EndDate, string Filter = "Agent", bool isFirstTimeList = false)
        {
            DashboardAgentsResponse jres = null;
            ActionLogger.Logger.WriteLog("GetAgentsData request: " + LicenseeID + ", Start: " + StartDate + ", EndDate : " + EndDate + ", filter: " + Filter, true);
            try
            {
                DateTime dtStart = DateTime.MinValue;
                DateTime dtEnd = DateTime.MinValue;
                DateTime.TryParse(StartDate, out dtStart);
                DateTime.TryParse(EndDate, out dtEnd);

                List<AgentData> lstAgents = Dashboard.GetDashboardAgents(LicenseeID, dtStart, dtEnd, out List<AgentData> exportDataList, Filter, isFirstTimeList);
                jres = new DashboardAgentsResponse(string.Format("Agents data found successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                jres.TotalRecords = lstAgents;
                jres.TotalLength = lstAgents.Count;
                jres.ExportDataList = exportDataList;
                ActionLogger.Logger.WriteLog("GetAgentsData: List fetch successfully: ", true);
            }
            catch (Exception ex)
            {
                jres = new DashboardAgentsResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                ActionLogger.Logger.WriteLog("GetAgentsData:exception occurs" + ex.Message, true);
            }
            return jres;
        }

        public DashboardLOCRevenuResponse GetRevenueByLOCData(Guid LicenseeID, Guid UserCredentialID, string StartDate, string EndDate, bool isFirstTimeList)
        {
            DashboardLOCRevenuResponse jres = null;
            ActionLogger.Logger.WriteLog("GetRevenueByLOCData request: " + LicenseeID + ", Start: " + StartDate + ", EndDate : " + EndDate, true);
            try
            {
                DateTime dtStart = DateTime.MinValue;
                DateTime dtEnd = DateTime.MinValue;
                DateTime.TryParse(StartDate, out dtStart);
                DateTime.TryParse(EndDate, out dtEnd);

                List<RevenueLOCData> lstRevenueByLOC = Dashboard.GetRevenuByLOC(LicenseeID, UserCredentialID, dtStart, dtEnd, isFirstTimeList, out List<RevenueLOCData> exportDataList);
                jres = new DashboardLOCRevenuResponse(string.Format("Agents data found successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                jres.TotalRecords = lstRevenueByLOC;
                jres.TotalLength = lstRevenueByLOC.Count;
                jres.ExportDataList = exportDataList;
                ActionLogger.Logger.WriteLog("GetRevenueByLOCData:List fetch successfully ", true);
            }
            catch (Exception ex)
            {
                jres = new DashboardLOCRevenuResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                ActionLogger.Logger.WriteLog("GetRevenueByLOCData:Exception occurs while fetchi9ng list-" + ex.Message, true);
            }
            return jres;
        }

        public DashboardClientsResponse GetClientsWithMultipleProductsService(Guid LicenseeID, Guid UserCredentialID, bool isFirstTimeList)
        {
            DashboardClientsResponse jres = null;
            ActionLogger.Logger.WriteLog("GetClientsWithMultipleProducts request: " + LicenseeID + ", UserCredentialID: " + UserCredentialID + ", isFirstTimeList : " + isFirstTimeList, true);
            try
            {
                List<ClientName> lstClients = Dashboard.GetClientsWithMultipleProducts(LicenseeID, UserCredentialID, isFirstTimeList);
                jres = new DashboardClientsResponse(string.Format("Clients data found successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                jres.TotalRecords = lstClients;
                jres.TotalLength = lstClients.Count;
                ActionLogger.Logger.WriteLog("GetClientsWithMultipleProducts:List fetch successfully ", true);
            }
            catch (Exception ex)
            {
                jres = new DashboardClientsResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                ActionLogger.Logger.WriteLog("GetClientsWithMultipleProducts: Exception occurs while fetching list-" + ex.Message, true);
            }
            return jres;
        }


        public DashboardAgentDetailExportResponse GetAgentDetailforExport(Guid LicenseeID, string StartDate, string EndDate, string Filter = "Agent")
        {
            DashboardAgentDetailExportResponse jres = null;
            ActionLogger.Logger.WriteLog("GetAgentsData request: " + LicenseeID + ", Start: " + StartDate + ", EndDate : " + EndDate + ", filter: " + Filter, true);
            try
            {
                DateTime dtStart = DateTime.MinValue;
                DateTime dtEnd = DateTime.MinValue;
                DateTime.TryParse(StartDate, out dtStart);
                DateTime.TryParse(EndDate, out dtEnd);

                List<AgentExportDetail> exportDataList = Dashboard.GetDashboardAgentExportDetails(LicenseeID, dtStart, dtEnd, Filter);
                jres = new DashboardAgentDetailExportResponse(string.Format("Agents data found successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                jres.ExportDataList = exportDataList;
                ActionLogger.Logger.WriteLog("GetAgentsData: List fetch successfully: ", true);
            }
            catch (Exception ex)
            {
                jres = new DashboardAgentDetailExportResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                ActionLogger.Logger.WriteLog("GetAgentsData:exception occurs" + ex.Message, true);
            }
            return jres;
        }
    }
}