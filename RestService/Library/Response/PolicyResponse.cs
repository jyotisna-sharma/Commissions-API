using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyAgencyVault.BusinessLibrary;
using System.Runtime.Serialization;
using MyAgencyVault.BusinessLibrary.BusinessObjects;

namespace MyAgencyVault.WcfService.Library.Response
{
    #region New Code For web UI
    [DataContract]    public class LinkValidationResponse : JSONResponse    {        public LinkValidationResponse(string message, int errorCode, string exceptionMessage)            : base(message, errorCode, exceptionMessage)        {        }        [DataMember]        public string StringValue { get; set; }        [DataMember]        public bool IsResponseNeededOnFalse { get; set; }    }
    [DataContract]
    public class PolicyImportResponse : JSONResponse
    {
        public PolicyImportResponse(string message, int errorCode, string exceptionMessage)
            : base(message, errorCode, exceptionMessage)
        {
        }
        [DataMember]
        public PolicyImportStatus  ImportStatus { get; set; }
    }

    [DataContract]
    public class PolicyOutgoingResponse : JSONResponse
    {
        public PolicyOutgoingResponse(string message, int errorCode, string exceptionMessage)
            : base(message, errorCode, exceptionMessage)
        {
        }
        [DataMember]
        public List<PolicyOutgoingDistribution> OutgoingList { get; set; }
    }
    [DataContract]
    public class PolicyOutgoingPaymentListResponse : JSONResponse
    {
        public PolicyOutgoingPaymentListResponse(string message, int errorCode, string exceptionMessage)
            : base(message, errorCode, exceptionMessage)
        {
        }
        [DataMember]
        public List<PolicyOutgoingPaymentObject> OutgoingList { get; set; }
    }

    [DataContract]
    public class PolicyOutgoingScheduleResponse : JSONResponse
    {
        public PolicyOutgoingScheduleResponse(string message, int errorCode, string exceptionMessage)
            : base(message, errorCode, exceptionMessage)
        {
        }
        [DataMember]
        public PolicyOutgoingSchedule PolicySchedule { get; set; }
    }
    [DataContract]
    public class PolicyOutgoingScheduleListResponse : JSONResponse
    {
        public PolicyOutgoingScheduleListResponse(string message, int errorCode, string exceptionMessage)
            : base(message, errorCode, exceptionMessage)
        {
        }
        [DataMember]
        public IList<PolicyOutgoingScheduleList> OutgoingSchedulelist { get; set; }
    }

    [DataContract]
    public class PolicyIncomingScheduleResponse : JSONResponse
    {
        public PolicyIncomingScheduleResponse(string message, int errorCode, string exceptionMessage)
            : base(message, errorCode, exceptionMessage)
        {
        }
        [DataMember]
        public List<PolicyToolIncommingShedule> Policylist { get; set; }
        [DataMember]
        public PolicyToolIncommingShedule PolicyIncomingScheduleObject { get; set; }
    }

    [DataContract]
    public class PolicyBoolResponse : JSONResponse
    {
        public PolicyBoolResponse(string message, int errorCode, string exceptionMessage)
            : base(message, errorCode, exceptionMessage)
        {
        }
        [DataMember]
        public bool BoolFlag { get; set; }
    }

    [DataContract]
    public class PolicyStringResponse : JSONResponse
    {
        public PolicyStringResponse(string message, int errorCode, string exceptionMessage)
            : base(message, errorCode, exceptionMessage)
        {
        }
        [DataMember]
        public string StringValue { get; set; }
    }

    [DataContract]
    public class PolicyListDetailResponse : JSONResponse
    {
        public PolicyListDetailResponse(string message, int errorCode, string exceptionMessage)
            : base(message, errorCode, exceptionMessage)
        {
        }
        [DataMember]
        public List<PolicyListObject> PolicyList { get; set; }

        [DataMember]
        public PoliciesCount PoliciesCount { get; set; }
        [DataMember]
        public string RecordCount { get; set; }


    }
    

    [DataContract]
    public class checkNamedScheduleExistResponse : JSONResponse
    {
        public checkNamedScheduleExistResponse(string message, int errorCode, string exceptionMessage)
            : base(message, errorCode, exceptionMessage)
        {
        }
        [DataMember]
        public bool isExist { get; set; }
    }

    [DataContract]
    public class PolicyPMCResponse : JSONResponse
    {
        public PolicyPMCResponse(string message, int errorCode, string exceptionMessage)
            : base(message, errorCode, exceptionMessage)
        {
        }
        [DataMember]
        public decimal NumberValue { get; set; }
    }

    [DataContract]
    public class PolicyLearnedResponse : JSONResponse
    {
        public PolicyLearnedResponse(string message, int errorCode, string exceptionMessage)
            : base(message, errorCode, exceptionMessage)
        {
        }
        [DataMember]
        public PolicyLearnedFieldData PolicyLearned { get; set; }
    }
    [DataContract]
    public class PolicyDateResponse : JSONResponse
    {
        public PolicyDateResponse(string message, int errorCode, string exceptionMessage)
            : base(message, errorCode, exceptionMessage)
        {
        }
        [DataMember]
        public string PolicyDate { get; set; }
    }
    [DataContract]
    public class PolicyTrackResponse : JSONResponse
    {
        public PolicyTrackResponse(string message, int errorCode, string exceptionMessage) : base(message, errorCode, exceptionMessage)
        {
        }
        [DataMember]
        public bool IsTrackEnabled { get; set; }
    }

    [DataContract]
    public class PolicyOutgoingDistributionResponse : JSONResponse
    {
        public PolicyOutgoingDistributionResponse(string message, int errorCode, string exceptionMessage)
            : base(message, errorCode, exceptionMessage)
        {
        }
        [DataMember]
        public PolicyOutgoingDistribution OutgoingDistribution { get; set; }
    }

    [DataContract]
    public class PolicyOutgoingPaymentResponse : JSONResponse
    {
        public PolicyOutgoingPaymentResponse(string message, int errorCode, string exceptionMessage)
            : base(message, errorCode, exceptionMessage)
        {
        }
        [DataMember]
        public List<OutGoingPayment> PolicyOutgoingScheduleList { get; set; }
    }

    [DataContract]
    public class LinkPaymentsPolicyResponse : JSONResponse
    {
        public LinkPaymentsPolicyResponse(string message, int errorCode, string exceptionMessage)
            : base(message, errorCode, exceptionMessage)
        {
        }
        [DataMember]
        public List<LinkPaymentPolicies> LinkPaymentsList { get; set; }
        [DataMember]
        public int RecordCount {get;set;}
    }
    [DataContract]
    public class LinkPaymentReciptRecordsList : JSONResponse
    {
        public LinkPaymentReciptRecordsList(string message, int errorCode, string exceptionMessage)
            : base(message, errorCode, exceptionMessage)
        {
        }
        [DataMember]
        public List<LinkPaymentReciptRecords> LinkPaymentsList { get; set; }
        [DataMember]
        public int RecordCount { get; set; }
    }

    [DataContract]
    public class LinkPaymentResponse : JSONResponse
    {
        public LinkPaymentResponse(string message, int errorCode, string exceptionMessage)
            : base(message, errorCode, exceptionMessage)
        {
        }
        [DataMember]
        public List<LinkPayment> LinkPaymentsList { get; set; }
        [DataMember]
        public int RecordCount { get; set; }
        [DataMember]
        public List<LinkPendingPolicy> PolicyDetail { get; set; }
    }



    [DataContract]
    public class LinkPaymentsConditionsResponse : JSONResponse
    {
        public LinkPaymentsConditionsResponse(string message, int errorCode, string exceptionMessage)
            : base(message, errorCode, exceptionMessage)
        {
        }
        [DataMember]
        public bool IsAgencyVersion { get; set; }
        [DataMember]
        public bool IsMarkedPaid { get; set; }
        [DataMember]
        public bool ScheduleMatches { get; set; }
    }

    [DataContract]
    public class PolicyDetailsResponse : JSONResponse
    {
        public PolicyDetailsResponse(string message, int errorCode, string exceptionMessage)
            : base(message, errorCode, exceptionMessage)
        {
        }
        [DataMember]
        public PolicyDetailsData PolicyObject { get; set; }
    }


    #endregion



    
    /*

     [DataContract]
     public class PolicyPMCResponse : JSONResponse
     {
         public PolicyPMCResponse(string message, int errorCode, string exceptionMessage)
             : base(message, errorCode, exceptionMessage)
         {
         }
         [DataMember]
         public decimal NumberValue { get; set; }
     }


     [DataContract]
     public class GetHouseAccountResponse : JSONResponse
     {
         public GetHouseAccountResponse(string message, int errorCode, string exceptionMessage)
             : base(message, errorCode, exceptionMessage)
         {
         }
         [DataMember]
         public HouseAccountDetails HouseAccountdetails { get; set; }
     }
     [DataContract]
     public class PolicySmartfieldDetailsResponse : JSONResponse
     {
         public PolicySmartfieldDetailsResponse(string message, int errorCode, string exceptionMessage)
             : base(message, errorCode, exceptionMessage)
         {
         }
         [DataMember]
         public PolicyLearnedFieldData policyDetails { get; set; }
     }

     [DataContract]
     public class PolicyStringResponse : JSONResponse
     {
         public PolicyStringResponse(string message, int errorCode, string exceptionMessage)
             : base(message, errorCode, exceptionMessage)
         {
         }
         [DataMember]
         public string StringValue { get; set; }
     }

     [DataContract]
     public class PolicyBoolResponse : JSONResponse
     {
         public PolicyBoolResponse(string message, int errorCode, string exceptionMessage)
             : base(message, errorCode, exceptionMessage)
         {
         }
         [DataMember]
         public bool BoolFlag { get; set; }
     }

    

     [DataContract]
     public class PolicyBatchResponse : JSONResponse
     {
         public PolicyBatchResponse(string message, int errorCode, string exceptionMessage)
             : base(message, errorCode, exceptionMessage)
         {
         }
         [DataMember]
         public Batch PolicyBatch { get; set; }
     }

     [DataContract]
     public class PolicyStmtResponse : JSONResponse
     {
         public PolicyStmtResponse(string message, int errorCode, string exceptionMessage)
             : base(message, errorCode, exceptionMessage)
         {
         }
         [DataMember]
         public Statement PolicyStmt{ get; set; }
     }

     [DataContract]
     public class PolicyLearnedResponse : JSONResponse
     {
         public PolicyLearnedResponse(string message, int errorCode, string exceptionMessage)
             : base(message, errorCode, exceptionMessage)
         {
         }
         [DataMember]
         public PolicyLearnedFieldData PolicyLearned { get; set; }
     }

     [DataContract]
     public class PolicySearchResponse : JSONResponse
     {
         public PolicySearchResponse(string message, int errorCode, string exceptionMessage)
             : base(message, errorCode, exceptionMessage)
         {
         }
         [DataMember]
         public List<PolicySearched> Policylist { get; set; }
     }





     [DataContract]
     public class PolicyOutgoingResponse : JSONResponse
     {
         public PolicyOutgoingResponse(string message, int errorCode, string exceptionMessage)
             : base(message, errorCode, exceptionMessage)
         {
         }
         [DataMember]
         public List<PolicyOutgoingDistribution> OutgoingList { get; set; }
     }

     [DataContract]
     public class PolicyOutgoingScheduleResponse : JSONResponse
     {
         public PolicyOutgoingScheduleResponse(string message, int errorCode, string exceptionMessage)
             : base(message, errorCode, exceptionMessage)
         {
         }
         [DataMember]
         public PolicyOutgoingSchedule PolicySchedule { get; set; }
     }
     [DataContract]
     public class PolicyOutgoingScheduleListResponse : JSONResponse
     {
         public PolicyOutgoingScheduleListResponse(string message, int errorCode, string exceptionMessage)
             : base(message, errorCode, exceptionMessage)
         {
         }
         [DataMember]
         public IList<PolicyOutgoingScheduleList> OutgoingSchedulelist { get; set; }
     }

     [DataContract]
     public class PolicyIncomingScheduleResponse : JSONResponse
     {
         public PolicyIncomingScheduleResponse(string message, int errorCode, string exceptionMessage)
             : base(message, errorCode, exceptionMessage)
         {
         }
         [DataMember]
         public List<PolicyToolIncommingShedule> Policylist { get; set; }
         [DataMember]
         public PolicyToolIncommingShedule PolicyIncomingScheduleObject { get; set; }
     }

     
    

     [DataContract]
     public class LastViewedPolicyResponse : JSONResponse
     {
         public LastViewedPolicyResponse(string message, int errorCode, string exceptionMessage)
             : base(message, errorCode, exceptionMessage)
         {
         }
         [DataMember]
         public List<LastViewPolicy> LastVewedPolicyList { get; set; }
     }

    
     [DataContract]
     public class GetPolicyNotesResponse : JSONResponse
     {
         public GetPolicyNotesResponse(string message, int errorCode, string exceptionMessage)
             : base(message, errorCode, exceptionMessage)
         {
         }
         [DataMember]
         public List<PolicyNotes> PolicyNotesList { get; set; }

         [DataMember]
         public int policyNotesCount { get; set; }
     }*/

}