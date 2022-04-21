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
        public List<DisplayFollowupIssue> FollowupIssueList { get; set; }
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
}