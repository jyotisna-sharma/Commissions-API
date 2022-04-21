using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Runtime.Serialization;
using MyAgencyVault.BusinessLibrary;

namespace MyAgencyVault.WcfService.Library.Response
{
    [DataContract]
    public class LicenseeListResponse : JSONResponse
    {
        public LicenseeListResponse(string message, int errorCode, string exceptionMessage)
            : base(message, errorCode, exceptionMessage)
        {
        }
        [DataMember]
        public List<LicenseeDisplayData> LicenseeList { get; set; }
    }

    [DataContract]
    public class StringListResponse : JSONResponse
    {
        public StringListResponse(string message, int errorCode, string exceptionMessage)
            : base(message, errorCode, exceptionMessage)
        {
        }
        [DataMember]
        public List<string> List { get; set; }
    }

    [DataContract]
    public class LicenseeBalanceListResponse : JSONResponse
    {
        public LicenseeBalanceListResponse(string message, int errorCode, string exceptionMessage)
            : base(message, errorCode, exceptionMessage)
        {
        }
        [DataMember]
        public List<LicenseeBalance> LicenseeList { get; set; }
    }

    [DataContract]
    public class LicenseeDisplayDataResponse : JSONResponse
    {
        public LicenseeDisplayDataResponse(string message, int errorCode, string exceptionMessage)
            : base(message, errorCode, exceptionMessage)
        {
        }
        [DataMember]
        public LicenseeDisplayData LicenseeObject { get; set; }
    }

    [DataContract]
    public class InvoiceLineListResponse : JSONResponse
    {
        public InvoiceLineListResponse(string message, int errorCode, string exceptionMessage)
            : base(message, errorCode, exceptionMessage)
        {
        }
        [DataMember]
        public List<InvoiceLineJournalData> InvoiceList { get; set; }
    }

    [DataContract]
    public class InvoiceLineResponse : JSONResponse
    {
        public InvoiceLineResponse(string message, int errorCode, string exceptionMessage)
            : base(message, errorCode, exceptionMessage)
        {
        }
        [DataMember]
        public InvoiceLine InvoiceLineObject { get; set; }
    }

    [DataContract]
    public class LicenseeInvoiceResponse : JSONResponse
    {
        public LicenseeInvoiceResponse(string message, int errorCode, string exceptionMessage)
            : base(message, errorCode, exceptionMessage)
        {
        }
        [DataMember]
        public LicenseeInvoice InvoiceObject { get; set; }
    }

    [DataContract]
    public class InvoiceListResponse : JSONResponse
    {
        public InvoiceListResponse(string message, int errorCode, string exceptionMessage)
            : base(message, errorCode, exceptionMessage)
        {
        }
        [DataMember]
        public List<LicenseeInvoice> InvoiceList { get; set; }
    }

    [DataContract]
    public class CalculationResponse : JSONResponse
    {
        public CalculationResponse(string message, int errorCode, string exceptionMessage)
            : base(message, errorCode, exceptionMessage)
        {
        }
        [DataMember]
        public LicenseeVariableOutputDetail Result { get; set; }
    }
}