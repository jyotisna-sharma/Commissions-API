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
    public class PayorListResponse : JSONResponse
    {
          public PayorListResponse(string message, int errorCode, string exceptionMessage)
            : base(message, errorCode, exceptionMessage)
        {
        }
        [DataMember]
        public List<Payor> PayorList { get; set; }

        [DataMember]
        public int PayorsCount { get; set; }

    }

    [DataContract]
     public class PayorResponse : JSONResponse
     {
         public PayorResponse(string message, int errorCode, string exceptionMessage)
             : base(message, errorCode, exceptionMessage)
         {
         }
         [DataMember]
         public Payor PayorObject { get; set; }
     }

     [DataContract]
     public class DisplayPayorListResponse : JSONResponse
     {
         public DisplayPayorListResponse(string message, int errorCode, string exceptionMessage)
             : base(message, errorCode, exceptionMessage)
         {
         }
         [DataMember]
         public List<DisplayedPayor> PayorList { get; set; }
     }

    [DataContract]
     public class PayorDefaultListresponse: JSONResponse
     {
        public PayorDefaultListresponse(string message, int errorCode, string exceptionMessage)
             : base(message, errorCode, exceptionMessage)
         {
         }
         [DataMember]
         public List<PayorDefaults> PayorList { get; set; }
     }

    [DataContract]
    public class PayorDefaultResponse : JSONResponse
    {
        public PayorDefaultResponse(string message, int errorCode, string exceptionMessage)
            : base(message, errorCode, exceptionMessage)
        {
        }
        [DataMember]
        public PayorDefaults PayorObject { get; set; }
    }

    [DataContract]
    public class PayorSiteResponse : JSONResponse
    {
        public PayorSiteResponse(string message, int errorCode, string exceptionMessage)
            : base(message, errorCode, exceptionMessage)
        {
        }
        [DataMember]
        public List<PayorSiteLoginInfo> PayorSiteList { get; set; }
    }

    [DataContract]
    public class ConfigPayorListResponse : JSONResponse
    {
        public ConfigPayorListResponse(string message, int errorCode, string exceptionMessage)
            : base(message, errorCode, exceptionMessage)
        {
        }
        [DataMember]
        public List<ConfigDisplayedPayor> PayorList { get; set; }
    }

    [DataContract]
    public class SettingPayorListResponse : JSONResponse
    {
        public SettingPayorListResponse(string message, int errorCode, string exceptionMessage)
            : base(message, errorCode, exceptionMessage)
        {
        }
        [DataMember]
        public List<ConfigPayor> PayorList { get; set; }
        [DataMember]
        public int PayorsCount { get; set; }
    }
    [DataContract]
    public class PayorContactsListResponse : JSONResponse
    {
        public PayorContactsListResponse(string message, int errorCode, string exceptionMessage)
            : base(message, errorCode, exceptionMessage)
        {
        }
        [DataMember]
        public List<GlobalPayorContact> ContactList { get; set; }
        [DataMember]
        public int ContactsCount { get; set; }
    }
    [DataContract]
    public class GetPayorContactDetailsResponse : JSONResponse
    {
        public GetPayorContactDetailsResponse(string message, int errorCode, string exceptionMessage)
            : base(message, errorCode, exceptionMessage)
        {
        }
        [DataMember]
        public PayorContact PayorContactDetails { get; set; }
    }
    [DataContract]
    public class AddUpdatePayorContactResponse : JSONResponse
    {
        public AddUpdatePayorContactResponse(string message, int errorCode, string exceptionMessage)
            : base(message, errorCode, exceptionMessage)
        {
        }
        [DataMember]
        public int Status { get; set; }
    }
}