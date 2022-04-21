using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
using MyAgencyVault.BusinessLibrary;

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
}