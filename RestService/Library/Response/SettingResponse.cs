using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using MyAgencyVault.BusinessLibrary;
namespace MyAgencyVault.WcfService.Library.Response
{
    [DataContract]
    public class GetSettingsListResponse : JSONResponse
    {
        public GetSettingsListResponse(string message, int errorCode, string exceptionMessage)
            : base(message, errorCode, exceptionMessage)
        {
        }
        [DataMember]
        public List<PayorIncomingSchedule> SettingList { get; set; }
        [DataMember]
        public double listCount { get; set; }
    }

    [DataContract]
    public class PayorScheduleDetailResponse : JSONResponse
    {
        public PayorScheduleDetailResponse(string message, int errorCode, string exceptionMessage)
            : base(message, errorCode, exceptionMessage)
        {
        }
        [DataMember]
        public PayorIncomingSchedule ScheduleDetails { get; set; }
    }
    [DataContract]
    public class IsScheduleExistResponse : JSONResponse
    {
        public IsScheduleExistResponse(string message, int errorCode, string exceptionMessage)
            : base(message, errorCode, exceptionMessage)
        {
        }
        [DataMember]
        public Boolean isExist { get; set; }
    }

    [DataContract]
    public class ReportSettingListResponse : JSONResponse
    {
        public ReportSettingListResponse(string message, int errorCode, string exceptionMessage)
            : base(message, errorCode, exceptionMessage)
        {
        }
        [DataMember]
        public List<ReportCustomFieldSettings> ReportSettingList { get; set; }
        [DataMember]
        public int RecordCount { get; set; }
    }
}
