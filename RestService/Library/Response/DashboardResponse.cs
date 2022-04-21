using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
using MyAgencyVault.BusinessLibrary;
using MyAgencyVault.BusinessLibrary.BusinessObjects;

namespace MyAgencyVault.WcfService.Library.Response
{
    [DataContract]
    public class DashboardResponse:JSONResponse
    {
        public DashboardResponse(string message, int errorCode, string exceptionMessage)
           : base(message, errorCode, exceptionMessage)
        {
        }

        [DataMember]
        public int AgentCount { get; set; }

        [DataMember]
        public int ClientCount { get; set; }

    }

    [DataContract]
    public class DashboardAgentsResponse : JSONResponse
    {
        public DashboardAgentsResponse(string message, int errorCode, string exceptionMessage)
           : base(message, errorCode, exceptionMessage)
        {
        }

        [DataMember]
        public List<AgentData> TotalRecords { get; set; }
        [DataMember]
        public int TotalLength { get; set; }
        [DataMember]
        public List<AgentData> ExportDataList { get; set; }



    }


    [DataContract]
    public class DashboardAgentDetailExportResponse : JSONResponse
    {
        public DashboardAgentDetailExportResponse(string message, int errorCode, string exceptionMessage)
           : base(message, errorCode, exceptionMessage)
        {
        }

        [DataMember]
        public List<AgentExportDetail> ExportDataList { get; set; }



    }

    [DataContract]
    public class DashboardLOCRevenuResponse : JSONResponse
    {
        public DashboardLOCRevenuResponse(string message, int errorCode, string exceptionMessage)
           : base(message, errorCode, exceptionMessage)
        {
        }

        [DataMember]
        public List<RevenueLOCData> TotalRecords { get; set; }

        [DataMember]
        public int TotalLength { get; set; }
        [DataMember]
        public List<RevenueLOCData> ExportDataList { get; set; }
    }




    [DataContract]
    public class DashboardRevenueResponse : JSONResponse
    {
        public DashboardRevenueResponse(string message, int errorCode, string exceptionMessage)
           : base(message, errorCode, exceptionMessage)
        {
        }

        [DataMember]
        public List<LineGraphData> GrossRevenueData { get; set; }

        [DataMember]
        public List<LineGraphData> NetRevenueData { get; set; }

        [DataMember]
        public List<string> MonthsRange { get; set; }

        [DataMember]
        public AllRevenueData GrossRevenueNumbers { get; set; }
        [DataMember]
        public AllRevenueData NetRevenueNumbers { get; set; }

        [DataMember]
        public Guid DefaultClient { get; set; }

        [DataMember]
        public string RefreshTime { get; set; }

    }
    [DataContract]
    public class DashboardNewPolicyListResponse : JSONResponse
    {
        public DashboardNewPolicyListResponse(string message, int errorCode, string exceptionMessage)
           : base(message, errorCode, exceptionMessage)
        {
        }

        [DataMember]
        public List<NewPolicyList> TotalRecords { get; set; }

        [DataMember]
        public int TotalLength { get; set; }
        [DataMember]
        public List<NewPolicyList> ExportDataList { get; set; }

    }

    [DataContract]
    public class DashboardRenewalsListResponse : JSONResponse
    {
        public DashboardRenewalsListResponse(string message, int errorCode, string exceptionMessage)
           : base(message, errorCode, exceptionMessage)
        {
        }

        [DataMember]
        public List<RenewalsList> TotalRecords { get; set; }

        [DataMember]
        public int TotalLength { get; set; }
        [DataMember]
        public List<RenewalsList> ExportDataList { get; set; }

    }

    [DataContract]
    public class DashboardReceivaleListResponse : JSONResponse
    {
        public DashboardReceivaleListResponse(string message, int errorCode, string exceptionMessage)
           : base(message, errorCode, exceptionMessage)
        {
        }

        [DataMember]
        public List<ReceivablesList> TotalRecords { get; set; }

        [DataMember]
        public int TotalLength { get; set; }

    }
    [DataContract]
    public class DashboardClientsResponse : JSONResponse
    {
        public DashboardClientsResponse(string message, int errorCode, string exceptionMessage)
           : base(message, errorCode, exceptionMessage)
        {
        }

        [DataMember]
        public List<ClientName> TotalRecords { get; set; }

        [DataMember]
        public int TotalLength { get; set; }

    }
}