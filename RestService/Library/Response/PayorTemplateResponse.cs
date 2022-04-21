using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
using MyAgencyVault.BusinessLibrary;
using MyAgencyVault.BusinessLibrary.Masters;
using MyAgencyVault.BusinessLibrary.BusinessObjects;

namespace MyAgencyVault.WcfService.Library.Response
{

    [DataContract]
    public class PayorTemplateListResponse : JSONResponse
    {
        public PayorTemplateListResponse(string message, int errorCode, string exceptionMessage)
            : base(message, errorCode, exceptionMessage)
        {
        }
        [DataMember]
        public List<Tempalate> TotalRecords { get; set; }
        [DataMember]
        public int TotalLength { get; set; }

        [DataMember]
        public string PayorName { get; set; }
    }

    [DataContract]
    public class PayorTemplateMainListResponse : JSONResponse
    {
        public PayorTemplateMainListResponse(string message, int errorCode, string exceptionMessage)
            : base(message, errorCode, exceptionMessage)
        {
        }
        [DataMember]
        public List<PayorTemplate> PayorTemplateList { get; set; }
    }

    [DataContract]
    public class PayorTemplateResponse : JSONResponse
    {
        public PayorTemplateResponse(string message, int errorCode, string exceptionMessage)
            : base(message, errorCode, exceptionMessage)
        {
        }
        [DataMember]
        public PayorTemplate PayorTemplateObject { get; set; }
    }
    [DataContract]
    public class ImportrTemplateDetailResponse : JSONResponse
    {
        public ImportrTemplateDetailResponse(string message, int errorCode, string exceptionMessage)
            : base(message, errorCode, exceptionMessage)
        {
        }
        [DataMember]
        public ImportToolTemplateDetails ImportPayorTemplateDetails { get; set; }
    }

    [DataContract]
    public class PayorTestFormulaResponse : JSONResponse
    {
        public PayorTestFormulaResponse(string message, int errorCode, string exceptionMessage)
            : base(message, errorCode, exceptionMessage)
        {
        }
        [DataMember]
        public string Result { get; set; }
        [DataMember]
        public bool IsResultValid { get; set; }
    }

    [DataContract]
    public class ImportToolPhraseListResponse : JSONResponse
    {
        public ImportToolPhraseListResponse(string message, int errorCode, string exceptionMessage)
            : base(message, errorCode, exceptionMessage)
        {
        }
        [DataMember]
        public List<ImportToolPayorPhrase> TotalRecords { get; set; }

        [DataMember]
        public int TotalLength { get; set; }
        [DataMember]
        public ImportToolTemplateDetails ImportPayorTemplateDetails { get; set; }
    }

    //[DataContract]
    //public class ImportToolStmtListResponse : JSONResponse
    //{
    //    public ImportToolStmtListResponse(string message, int errorCode, string exceptionMessage)
    //        : base(message, errorCode, exceptionMessage)
    //    {
    //    }
    //    [DataMember]
    //    public List<ImportToolStatementDataSettings> StatementSettingsList { get; set; }
    //}

    [DataContract]
    public class MaskFieldListResponse : JSONResponse
    {
        public MaskFieldListResponse(string message, int errorCode, string exceptionMessage)
            : base(message, errorCode, exceptionMessage)
        {
        }
        [DataMember]
        public List<MaskFieldTypes> MaskFieldsList { get; set; }
    }

    [DataContract]
    public class TranslatorListResponse : JSONResponse
    {
        public TranslatorListResponse(string message, int errorCode, string exceptionMessage)
            : base(message, errorCode, exceptionMessage)
        {
        }
        [DataMember]
        public List<TranslatorTypes> TranslatorTypesList { get; set; }
    }

    [DataContract]
    public class ImportToolPaymentListResponse : JSONResponse
    {
        public ImportToolPaymentListResponse(string message, int errorCode, string exceptionMessage)
            : base(message, errorCode, exceptionMessage)
        {
        }
        [DataMember]
        public List<ImportToolSeletedPaymentData> ImportToolPaymentDataList { get; set; }
    }

    [DataContract]
    public class ImportToolPaymentStngsListResponse : JSONResponse
    {
        public ImportToolPaymentStngsListResponse(string message, int errorCode, string exceptionMessage)
            : base(message, errorCode, exceptionMessage)
        {
        }
        [DataMember]
        public List<ImportToolPaymentDataFieldsSettings> ImportToolPaymentSettingList { get; set; }
    }

    [DataContract]
    public class ImportToolStatementListResponse : JSONResponse
    {
        public ImportToolStatementListResponse(string message, int errorCode, string exceptionMessage)
            : base(message, errorCode, exceptionMessage)
        {
        }
        [DataMember]
        public List<ImportToolStatementDataSettings> TotalRecords { get; set; }

        [DataMember]
        public int TotalLength { get; set; }

        [DataMember]
        public List<string> EndDataList { get; set; }
    }
}