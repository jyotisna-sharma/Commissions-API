using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyAgencyVault.BusinessLibrary;
using System.Runtime.Serialization;
using MyAgencyVault.BusinessLibrary.BusinessObjects;

namespace MyAgencyVault.WcfService.Library.Response
{
    [DataContract]
    public class PolicyObjectResponse : JSONResponse
    {
        public PolicyObjectResponse(string message, int errorCode, string exceptionMessage)
            : base(message, errorCode, exceptionMessage)
        {
        }
        [DataMember]
        public PolicyDetailsData PolicyObject { get; set; }
        [DataMember]
        public PolicyToolIncommingShedule PolicyIncomingSchedule { get; set; }
        [DataMember]
        public IList<PolicyOutgoingSchedules> PolicyOutgoingSchedules { get; set; }

        [DataMember]
        public bool isScheduleExist { get; set; }
        [DataMember]
        public string PolicyType { get; set; }

    }
    [DataContract]
    public class FollowUpPolicyObjectResponse : JSONResponse
    {
        public FollowUpPolicyObjectResponse(string message, int errorCode, string exceptionMessage)
            : base(message, errorCode, exceptionMessage)
        {
        }
        [DataMember]
        public FollowUpIssue PolicyObject { get; set; }
    }

    [DataContract]
    public class DEUPolicyListResponse : JSONResponse
    {
        public DEUPolicyListResponse(string message, int errorCode, string exceptionMessage)
            : base(message, errorCode, exceptionMessage)
        {
        }
        [DataMember]
        public List<DeuSearchedPolicy> TotalRecords { get; set; }
        [DataMember]
        public int TotalLength { get; set; }
    }

    [DataContract]
    public class ValidateDateFieldResponse : JSONResponse
    {
        public ValidateDateFieldResponse(string message, int errorCode, string exceptionMessage)
            : base(message, errorCode, exceptionMessage)
        {
        }

        [DataMember]
        public bool IsDateValid { get; set; }

        [DataMember]
        public string FormattedDate { get; set; }
    }

    [DataContract]
    public class PostStatusDEUResponse : JSONResponse
    {
        public PostStatusDEUResponse(string message, int errorCode, string exceptionMessage)
            : base(message, errorCode, exceptionMessage)
        {
        }
        [DataMember]
        public PostProcessWebStatus PostStatus { get; set; }
        [DataMember]
        public int? statementNumber { get; set; }
    }

    [DataContract]
    public class DEUEntryResponse : JSONResponse
    {
        public DEUEntryResponse(string message, int errorCode, string exceptionMessage)
            : base(message, errorCode, exceptionMessage)
        {
        }
        [DataMember]
        public DEUPaymentEntry DEUEntry { get; set; }
        [DataMember]
        public int? statementNumber { get; set; }
        [DataMember]
        public decimal? enteredAmount { get; set; }
    }

    [DataContract]
    public class PostStatusResponse : JSONResponse
    {
        public PostStatusResponse(string message, int errorCode, string exceptionMessage)
            : base(message, errorCode, exceptionMessage)
        {
        }
        [DataMember]
        public PostProcessReturnStatus PostStatus { get; set; }
        //[DataMember]
        //public int ? statementNumber { get; set; }
    }

    [DataContract]
    public class GuidResponse : JSONResponse
    {
        public GuidResponse(string message, int errorCode, string exceptionMessage)
            : base(message, errorCode, exceptionMessage)
        {
        }
        [DataMember]
        public Guid GuidValue { get; set; }
    }

    [DataContract]
    public class PaymentEntryListResponse : JSONResponse
    {
        public PaymentEntryListResponse(string message, int errorCode, string exceptionMessage)
            : base(message, errorCode, exceptionMessage)
        {
        }
        [DataMember]
        public List<PolicyPaymentEntriesPost> PaymentsList { get; set; }
    }
    [DataContract]
    public class PolicyIncomingPaymentListResponse : JSONResponse
    {
        public PolicyIncomingPaymentListResponse(string message, int errorCode, string exceptionMessage)
            : base(message, errorCode, exceptionMessage)
        {
        }
        [DataMember]
        public List<PolicyIncomingPaymentObject> TotalRecords { get; set; }
        [DataMember]
        public int TotalLength { get; set; }
        [DataMember]
        public List<PolicyIncomingPaymentObject> ExportDataList { get; set; }
    }

    [DataContract]
    public class PaymentEntryObjectResponse : JSONResponse
    {
        public PaymentEntryObjectResponse(string message, int errorCode, string exceptionMessage)
            : base(message, errorCode, exceptionMessage)
        {
        }
        [DataMember]
        public PolicyPaymentEntriesPost PaymentEntry { get; set; }
    }

    [DataContract]
    public class GetPolicyNotesResponse : JSONResponse
    {
        public GetPolicyNotesResponse(string message, int errorCode, string exceptionMessage)
            : base(message, errorCode, exceptionMessage)
        {
        }

        [DataMember]
        public IList<PolicyNotes> PolicyNotesList { get; set; }
        [DataMember]
        public IList<UploadedFile> UploadedFileList { get; set; }
        [DataMember]
        public int policyNotesCount { get; set; }
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
        [DataMember]
        public int policyNotesCount { get; set; }
    }

    [DataContract]
    public class StatementUpdateResponse : JSONResponse
    {
        public StatementUpdateResponse(string message, int errorCode, string exceptionMessage)
            : base(message, errorCode, exceptionMessage)
        {
        }
       
        [DataMember]
        public string netCheck { get; set; }
        [DataMember]
        public string completePercent { get; set; }
    }
}