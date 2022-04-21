using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
using MyAgencyVault.BusinessLibrary;
using MyAgencyVault.BusinessLibrary.Masters;


namespace MyAgencyVault.WcfService.Library.Response
{
    [DataContract]
    public class BillingLineListResponse : JSONResponse
    {
        public BillingLineListResponse(string message, int errorCode, string exceptionMessage)
            : base(message, errorCode, exceptionMessage)
        {
        }
        [DataMember]
        public List<BillingLineDetail> BillingLineList { get; set; }
    }

    [DataContract]
    public class ServiceChargeListResponse : JSONResponse
    {
        public ServiceChargeListResponse(string message, int errorCode, string exceptionMessage)
            : base(message, errorCode, exceptionMessage)
        {
        }
        [DataMember]
        public List<ServiceChargeType> ServiceChargeTypeList { get; set; }
    }

    [DataContract]
    public class ServiceProductListResponse : JSONResponse
    {
        public ServiceProductListResponse(string message, int errorCode, string exceptionMessage)
            : base(message, errorCode, exceptionMessage)
        {
        }
        [DataMember]
        public List<ServiceProduct> ServiceProductList { get; set; }
    }

    [DataContract]
    public class CompTypeListResponse : JSONResponse
    {
        public CompTypeListResponse(string message, int errorCode, string exceptionMessage)
            : base(message, errorCode, exceptionMessage)
        {
        }
        [DataMember]
        public List<CompType> CompTypeList { get; set; }

        [DataMember]
        public int recordCount { get; set; }
    }

}