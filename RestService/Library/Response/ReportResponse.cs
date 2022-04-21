using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
using MyAgencyVault.BusinessLibrary;

namespace MyAgencyVault.WcfService.Library.Response
{
    [DataContract]
    public class ReportListResponse : JSONResponse
    {
        public ReportListResponse(string message, int errorCode, string exceptionMessage)
         : base(message, errorCode, exceptionMessage)
        {
        }
        [DataMember]
        public List<Report> ReportsList
        {
            get; set;
        }

            [DataMember]
            public int Count { get; set; }
    }

    [DataContract]
    public class PrintReportResponse : JSONResponse
    {
        public PrintReportResponse(string message, int errorCode, string exceptionMessage)
            : base(message, errorCode, exceptionMessage)
        {
        }
        [DataMember]
        public PrintReportOutput ReportObject { get; set; }
    }
}