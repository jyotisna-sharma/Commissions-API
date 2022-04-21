using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
using MyAgencyVault.BusinessLibrary;
using static MyAgencyVault.BusinessLibrary.DEU;
using MyAgencyVault.BusinessLibrary.BusinessObjects;

namespace MyAgencyVault.WcfService.Library.Response
{
    [DataContract]
    public class PostedPaymentistResponse : JSONResponse
    {
        public PostedPaymentistResponse(string message, int errorCode, string exceptionMessage)
            : base(message, errorCode, exceptionMessage)
        {
        }
        [DataMember]
        public List<DEUPaymentEntry> TotalRecords { get; set; }
        [DataMember]
        public int TotalLength { get; set; }
        [DataMember]
        public int ErrorCount { get; set; }
        [DataMember]
        public int EntriesCount { get; set; }
    }

    [DataContract]
    public class DEUListResponse : JSONResponse
    {
        public DEUListResponse(string message, int errorCode, string exceptionMessage)
            : base(message, errorCode, exceptionMessage)
        {
        }
        [DataMember]
        public List<DataEntryField> DEUList { get; set; }
    }

    [DataContract]
    public class DEUResponse : JSONResponse
    {
        public DEUResponse(string message, int errorCode, string exceptionMessage)
            : base(message, errorCode, exceptionMessage)
        {
        }
        [DataMember]
        public DEU DEUObject { get; set; }
    }


    [DataContract]
    public class DEUBatchListResponse : JSONResponse
    {
        public DEUBatchListResponse(string message, int errorCode, string exceptionMessage)
            : base(message, errorCode, exceptionMessage)
        {
        }
        [DataMember]
        public List<Batch> TotalRecords { get; set; }
    }
    [DataContract]
    public class DEUStatementListResponse : JSONResponse
    {
        public DEUStatementListResponse(string message, int errorCode, string exceptionMessage)
            : base(message, errorCode, exceptionMessage)
        {
        }
        [DataMember]
        public List<Statement> TotalRecords { get; set; }

    }

    [DataContract]
    public class JsonBooleanResponse : JSONResponse
    {
        public JsonBooleanResponse(string message, int errorCode, string exceptionMessage)
            : base(message, errorCode, exceptionMessage)
        {
        }
        [DataMember]
        public bool result { get; set; }
        [DataMember]
        public bool isRecordExist { get; set; }
    }
    [DataContract]
    public class JsonStringResponse : JSONResponse
    {
        public JsonStringResponse(string message, int errorCode, string exceptionMessage)
            : base(message, errorCode, exceptionMessage)
        {
        }
        [DataMember]
        public string StringResult { get; set; }

        [DataMember]
        public int? IntResult { get; set; }
    }

    [DataContract]
    public class StatementDetailsResponse : JSONResponse
    {
        public StatementDetailsResponse(string message, int errorCode, string exceptionMessage)
            : base(message, errorCode, exceptionMessage)
        {
        }
        [DataMember]
        public Statement Details { get; set; }
    }
    [DataContract]
    public class BatchtDetailsResponse : JSONResponse
    {
        public BatchtDetailsResponse(string message, int errorCode, string exceptionMessage)
            : base(message, errorCode, exceptionMessage)
        {
        }
        [DataMember]
        public Batch Details { get; set; }
        [DataMember]
        public bool IsBatchFound { get; set; }
    }
    [DataContract]
    public class StatementCheckAmountListsResponse : JSONResponse
    {
        public StatementCheckAmountListsResponse(string message, int errorCode, string exceptionMessage)
            : base(message, errorCode, exceptionMessage)
        {
        }
        [DataMember]
        public bool IsStatementFound { get; set; }
    }

    [DataContract]
    public class GetUniqueIdentifierResponse : JSONResponse
    {
        public GetUniqueIdentifierResponse(string message, int errorCode, string exceptionMessage)
            : base(message, errorCode, exceptionMessage)
        {
        }
        [DataMember]
        public List<PayorToolField> PartOfPrimaryFieldList { get; set; }

    }
    [DataContract]
    public class DEUPayorToolResponse : JSONResponse
    {
        public DEUPayorToolResponse(string message, int errorCode, string exceptionMessage)
            : base(message, errorCode, exceptionMessage)
        {
        }
        [DataMember]
        public Batch Details { get; set; }
        [DataMember]
        public DEUPayorToolObject PayorToolObject { get; set; }
        [DataMember]
        public string uniqueIDs { get; set; }
    }
}