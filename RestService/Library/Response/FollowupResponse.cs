using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
using MyAgencyVault.BusinessLibrary;

namespace MyAgencyVault.WcfService.Library.Response
{
    [DataContract]
    public class FollowupObjectResponse : JSONResponse
    {
        public FollowupObjectResponse(string message, int errorCode, string exceptionMessage)
            : base(message, errorCode, exceptionMessage)
        {
        }
        [DataMember]
        public DisplayFollowupIssue FollowupIssueObject { get; set; }
    }

    [DataContract]
    public class FollowupListResponse : JSONResponse
    {
        public FollowupListResponse(string message, int errorCode, string exceptionMessage)
            : base(message, errorCode, exceptionMessage)
        {
        }
        [DataMember]
        public List<DisplayFollowupIssue> TotalRecords { get; set; }
        [DataMember]
        public int TotalLength { get; set; }
    }

    [DataContract]
    public class FollowupPolicyResponse : JSONResponse
    {
        public FollowupPolicyResponse(string message, int errorCode, string exceptionMessage)
            : base(message, errorCode, exceptionMessage)
        {
        }
        [DataMember]
        public List<FollowupIssuePolicyListObject> FollowupIssueList { get; set; }
        [DataMember]
        public int TotalLength { get; set; }
    }

    [DataContract]
    public class FollowupManagerListResponse : JSONResponse
    {
        public FollowupManagerListResponse(string message, int errorCode, string exceptionMessage)
            : base(message, errorCode, exceptionMessage)
        {
        }
        [DataMember]
        public List<FollowupIssueListObject> TotalRecords { get; set; }
        [DataMember]
        public int TotalLength { get; set; }
    }

    [DataContract]
    public class FollowupManagerNotesResponse : JSONResponse
    {
        public FollowupManagerNotesResponse(string message, int errorCode, string exceptionMessage)
            : base(message, errorCode, exceptionMessage)
        {
        }
        [DataMember]
        public List<LicenseeNote> TotalRecords { get; set; }
        [DataMember]
        public string IssueNotes { get; set; } [DataMember]
        public int  TotalLength { get; set; }
        [DataMember]
        public string PayorNotes { get; set; }
        [DataMember]
        public string AgencyPayorNotes { get; set; }
    }

    [DataContract]
    public class IssueDetailResponse : JSONResponse
    {
        public IssueDetailResponse(string message, int errorCode, string exceptionMessage)
            : base(message, errorCode, exceptionMessage)
        {
        }
        [DataMember]
        public List<IssuePolicyDetail> FollowupIssueList { get; set; }
    }

    [DataContract]
    public class IssueIDResponse : JSONResponse
    {
        public IssueIDResponse(string message, int errorCode, string exceptionMessage)
            : base(message, errorCode, exceptionMessage)
        {
        }
        [DataMember]
        public Guid? IssueID { get; set; }
    }

    [DataContract]
    public class FollowupInPaymentResponse : JSONResponse
    {
        public FollowupInPaymentResponse(string message, int errorCode, string exceptionMessage)
            : base(message, errorCode, exceptionMessage)
        {
        }

        [DataMember]
        public List<FollowupIncomingPament>  FollowupInPaymentList { get; set; }
        [DataMember]
        public int Count { get; set; }
    }

    [DataContract]
    public class FollowupPayorContactsResponse : JSONResponse
    {
        public FollowupPayorContactsResponse(string message, int errorCode, string exceptionMessage)
            : base(message, errorCode, exceptionMessage)
        {
        }
        [DataMember]
        public List<FollowUPPayorContacts> FollowupPayorContactsList { get; set; }
    }
    [DataContract]
    public class FollowupIssueDetailResponse : JSONResponse
    {
        public FollowupIssueDetailResponse(string message, int errorCode, string exceptionMessage)
            : base(message, errorCode, exceptionMessage)
        {
        }
        [DataMember]
        public FollowupIssueDetail FollowupDetails { get; set; }
    }

}