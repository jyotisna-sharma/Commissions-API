using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
using MyAgencyVault.BusinessLibrary;

namespace MyAgencyVault.WcfService.Library.Response
{
    [DataContract]
    public class CarrierListResponse : JSONResponse
    {
        public CarrierListResponse(string message, int errorCode, string exceptionMessage)
            : base(message, errorCode, exceptionMessage)
        {
        }
        [DataMember]
        public List<Carrier> CarrierList { get; set; }
        [DataMember]
        public string TotalCount  { get; set; }
    }
    [DataContract]
    public class ReportCarrierListResponse : JSONResponse
    {
        public ReportCarrierListResponse(string message, int errorCode, string exceptionMessage)
            : base(message, errorCode, exceptionMessage)
        {
        }
        [DataMember]
        public List<DisplayedCarrier> CarrierList { get; set; }
    }

    [DataContract]
    public class CarrierResponse : JSONResponse
    {
        public CarrierResponse(string message, int errorCode, string exceptionMessage)
            : base(message, errorCode, exceptionMessage)
        {

        }
        [DataMember]
        public  Carrier CarrierObject { get; set; }
    }

    [DataContract]
    public class DisplayCarrierListResponse : JSONResponse
    {
        public DisplayCarrierListResponse(string message, int errorCode, string exceptionMessage)
            : base(message, errorCode, exceptionMessage)
        { }

        [DataMember]
        public List<DisplayedCarrier> DisplayCarrierList { get; set; }
    }
    
    [DataContract]
    public class IDListResponse : JSONResponse
    {
        public IDListResponse(string message, int errorCode, string exceptionMessage)
            : base(message, errorCode, exceptionMessage)
        {
        }
        [DataMember]
        public List<Payor> SubmittedThroughList { get; set; }
    }
}