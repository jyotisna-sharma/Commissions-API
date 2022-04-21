using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
using MyAgencyVault.BusinessLibrary.Masters;
using MyAgencyVault.BusinessLibrary;
using MyAgencyVault.BusinessLibrary.BusinessObjects;

namespace MyAgencyVault.WcfService.Library.Response
{

    [DataContract]
    public class PayorToolAvailablelFieldTypeListResponse : JSONResponse
    {
        public PayorToolAvailablelFieldTypeListResponse(string message, int errorCode, string exceptionMessage)
          : base(message, errorCode, exceptionMessage)
        {
        }
        [DataMember]
        public List<PayorToolAvailablelFieldType> PayorToolFieldTypeList { get; set; }
    }

    [DataContract]
    public class PayorToolObjectResponse : JSONResponse
    {
        public PayorToolObjectResponse(string message, int errorCode, string exceptionMessage)
            : base(message, errorCode, exceptionMessage)
        {
        }
        [DataMember]
        public PayorTool PayorToolObject { get; set; }
    }


    [DataContract]
    public class PayorToolIDResponse : JSONResponse
    {
        public PayorToolIDResponse(string message, int errorCode, string exceptionMessage)
            : base(message, errorCode, exceptionMessage)
        {
        }
        [DataMember]
        public Guid PayorToolID { get; set; }
    }

    [DataContract]
    public class PayorSourceResponse : JSONResponse
    {
        public PayorSourceResponse(string message, int errorCode, string exceptionMessage)
            : base(message, errorCode, exceptionMessage)
        {
        }
        [DataMember]
        public PayorSource PayorSrcObject { get; set; }
    }

    [DataContract]
    public class AddUpdateImportToolFieldListResponse : JSONResponse
    {
        public AddUpdateImportToolFieldListResponse(string message, int errorCode, string exceptionMessage)
            : base(message, errorCode, exceptionMessage)
        {
        }
        [DataMember]
        public List<ImportToolInValidFieldList> InValidFormulaExpressionList { get; set; }
    }
    [DataContract]
    public class AddUpdatePayorToolFieldListResponse : JSONResponse
    {
        public AddUpdatePayorToolFieldListResponse(string message, int errorCode, string exceptionMessage)
            : base(message, errorCode, exceptionMessage)
        {
        }
        [DataMember]
        public List<ImportToolInValidFieldList> InValidFormulaExpressionList { get; set; }
    }
}