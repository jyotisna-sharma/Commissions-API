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
    public class SettingSegmentListResponse : JSONResponse
    {
        public SettingSegmentListResponse(string message, int errorCode, string exceptionMessage)
            : base(message, errorCode, exceptionMessage)
        {
        }
        [DataMember]
        public List<ConfigSegment> SegmentList { get; set; }
        [DataMember]
        public List<ConfigProductWithoutSegment> ProductListWithoutSegment { get; set; }
        [DataMember]
        public int RecordCount { get; set; }
        [DataMember]
        public int SegmentsCount { get; set; }

        [DataMember]
        public Guid? SegmentId { get; set; }
    }
}