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
    public class BatchListExportResponse : JSONResponse
    {
        public BatchListExportResponse(string message, int errorCode, string exceptionMessage)
            : base(message, errorCode, exceptionMessage)
        {
        }
        [DataMember]
        public List<BatchEntry> BatchList { get; set; }
    }

    [DataContract]
    public class BatchListResponse : JSONResponse
    {
        public BatchListResponse(string message, int errorCode, string exceptionMessage)
            : base(message, errorCode, exceptionMessage)
        {
        }
        [DataMember]
        public List<BathcListObject> TotalRecords { get; set; }
        [DataMember]
        public  int TotalLength { get; set; }
        [DataMember]
        public List<MonthsListData> MonthList { get; set; }
    }
    [DataContract]
    public class ReportsBatchListResponse : JSONResponse
    {
        public ReportsBatchListResponse(string message, int errorCode, string exceptionMessage)
            : base(message, errorCode, exceptionMessage)
        {
        }
        [DataMember]
        public List<ReportBatchDetails> TotalRecords { get; set; }
        [DataMember]
        public int TotalLength { get; set; }
    }
    [DataContract]
    public class BatchNoResponse : JSONResponse
    {
        public BatchNoResponse(string message, int errorCode, string exceptionMessage)
            : base(message, errorCode, exceptionMessage)
        {
        }
        [DataMember]
        public int BatchNumber { get; set; }
    }
   
    [DataContract]
    public class  BatchAddOutputResponse : JSONResponse
    {
        public BatchAddOutputResponse(string message, int errorCode, string exceptionMessage)
            : base(message, errorCode, exceptionMessage)
        {
        }
        [DataMember]
        public  BatchAddOutput BatchOutput { get; set; }
    }

    [DataContract]
    public class BatchStatementResponse : JSONResponse
    {
        public BatchStatementResponse(string message, int errorCode, string exceptionMessage)
            : base(message, errorCode, exceptionMessage)
        {
        }
        [DataMember]
        public List<BatchStatmentRecords> TotalRecords { get; set; }
        [DataMember]
        public int  TotalLength { get; set; }
        [DataMember]
        public List<ComListObject> footerData { get; set; }
    }

    [DataContract]
    public class BatchInsuredResponse : JSONResponse
    {
        public BatchInsuredResponse(string message, int errorCode, string exceptionMessage)
            : base(message, errorCode, exceptionMessage)
        {
        }
        [DataMember]
        public List<BatchInsuredRecored> StatementList { get; set; }
    }

    [DataContract]
    public class InsuredPaymentsResponse : JSONResponse
    {
        public InsuredPaymentsResponse(string message, int errorCode, string exceptionMessage)
            : base(message, errorCode, exceptionMessage)
        {
        }
        [DataMember]
        public List<InsuredPayment> InsuredPaymentsList { get; set; }
    }

    [DataContract]
    public class DownloadBatchResponse : JSONResponse
    {
        public DownloadBatchResponse(string message, int errorCode, string exceptionMessage)
            : base(message, errorCode, exceptionMessage)
        {
        }
        [DataMember]
        public List<DownloadBatch> BatchList { get; set; }
    }

    [DataContract]
    public class ImportFileResponse : JSONResponse
    {
        public ImportFileResponse(string message, int errorCode, string exceptionMessage)
            : base(message, errorCode, exceptionMessage)
        {
        }
        [DataMember]
        public ImportFileData ImportFile { get; set; }
    }

    [DataContract]
    public class BatchFileListResponse : JSONResponse
    {
        public BatchFileListResponse(string message, int errorCode, string exceptionMessage)
            : base(message, errorCode, exceptionMessage)
        {
        }
        [DataMember]
        public List<BatchFiles> BatchFileList { get; set; }
    }
}