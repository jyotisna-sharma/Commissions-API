using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyAgencyVault.BusinessLibrary;
using System.Runtime.Serialization;

namespace MyAgencyVault.WcfService.Library.Response
{
     [DataContract]
    public class PolicyTrackResponse : JSONResponse
    {
        public PolicyTrackResponse(string message, int errorCode, string exceptionMessage)  : base(message, errorCode, exceptionMessage)
        {
        }
         [DataMember]
        public bool IsTrackEnabled { get; set; }
    }

    [DataContract]
    public class PolicyListResponse : JSONResponse
    {
        public PolicyListResponse(string message, int errorCode, string exceptionMessage)
            : base(message, errorCode, exceptionMessage)
        {
        }
        [DataMember]
        public List<PolicyDetailsData> PolicyList { get; set; }
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
        public List<PolicyOutgoingDistribution> Policylist { get; set; }
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
    public class  PolicyOutgoingDistributionResponse: JSONResponse
    {
        public PolicyOutgoingDistributionResponse(string message, int errorCode, string exceptionMessage)
            : base(message, errorCode, exceptionMessage)
        {
        }
        [DataMember]
        public PolicyOutgoingDistribution OutgoingDistribution { get; set; }
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
}