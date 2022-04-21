using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
using MyAgencyVault.BusinessLibrary;

namespace MyAgencyVault.WcfService.Library.Response
{
    [DataContract]
    public class CoverageListResponse : JSONResponse
    {
        public CoverageListResponse(string message, int errorCode, string exceptionMessage)
            : base(message, errorCode, exceptionMessage)
        { }

        [DataMember]
        public List<Coverage> CoverageList { get; set; }
        [DataMember]
        public int recordCount { get; set; }
    }

    [DataContract]
    public class CoverageResponse : JSONResponse
    {
        public CoverageResponse(string message, int errorCode, string exceptionMessage)
                : base(message, errorCode, exceptionMessage)
        { }

        [DataMember]
        public Coverage CoverageObject { get; set; }
    }


    [DataContract]
    public class CoverageNickNameResponse : JSONResponse
    {
        public CoverageNickNameResponse(string message, int errorCode, string exceptionMessage)
            : base(message, errorCode, exceptionMessage)
        { }

        [DataMember]
        public List<CoverageNickName> CoverageNickNameList { get; set; }
        [DataMember]
        public int recordCount { get; set; }
    }

    [DataContract]
    public class DisplayCoverageListResponse : JSONResponse
    {
        public DisplayCoverageListResponse(string message, int errorCode, string exceptionMessage)
            : base(message, errorCode, exceptionMessage)
        { }

        [DataMember]
        public List<DisplayedCoverage> DisplayCoverageList { get; set; }
    }

    [DataContract]
    public class DisplayCoverageResponse : JSONResponse
    {
        public DisplayCoverageResponse(string message, int errorCode, string exceptionMessage)
            : base(message, errorCode, exceptionMessage)
        { }

        [DataMember]
        public DisplayedCoverage CoverageObject { get; set; }
    }

    [DataContract]
    public class ReturnStatusResponse : JSONResponse
    {
        public ReturnStatusResponse(string message, int errorCode, string exceptionMessage)
            : base(message, errorCode, exceptionMessage)
        { }

        [DataMember]
        public ReturnStatus Status { get; set; }
        [DataMember]
        public bool RecordStatus { get; set; }
    }
}