using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyAgencyVault.BusinessLibrary.Masters;
using System.Runtime.Serialization;
using MyAgencyVault.BusinessLibrary;

namespace MyAgencyVault.WcfService.Library.Response
{
    [DataContract]
    public class ExportDateResponse : JSONResponse
    {
        public ExportDateResponse(string message, int errorCode, string exceptionMessage)
            : base(message, errorCode, exceptionMessage)
        {
        }
        [DataMember]
        public ExportDate ExportDateObject { get; set; }
    }

    [DataContract]
    public class FileTypeListResponse : JSONResponse
    {
        public FileTypeListResponse(string message, int errorCode, string exceptionMessage)
            : base(message, errorCode, exceptionMessage)
        {
        }
        [DataMember]
        public List<FileType> FileTypeList { get; set; }
    }

    [DataContract]
    public class BatchDownloadListResponse : JSONResponse
    {
        public BatchDownloadListResponse(string message, int errorCode, string exceptionMessage)
            : base(message, errorCode, exceptionMessage)
        {
        }
        [DataMember]
        public List<BatchDownloadStatus> BatchDownloadList { get; set; }
    }

    [DataContract]
    public class LicenseeStatusResponse : JSONResponse
    {
        public LicenseeStatusResponse(string message, int errorCode, string exceptionMessage)
            : base(message, errorCode, exceptionMessage)
        {
        }
        [DataMember]
        public List<LicenseeStatus> LicenseeStatusList { get; set; }
    }

    [DataContract]
    public class PayorIncomingListResponse : JSONResponse
    {
        public PayorIncomingListResponse(string message, int errorCode, string exceptionMessage)
            : base(message, errorCode, exceptionMessage)
        {
        }
        [DataMember]
        public List<PayorToolIncomingFieldType> PayorIncomingList{ get; set; }
    }

    [DataContract]
    public class PayorLearnedListResponse : JSONResponse
    {
        public PayorLearnedListResponse(string message, int errorCode, string exceptionMessage)
            : base(message, errorCode, exceptionMessage)
        {
        }
        [DataMember]
        public List<PayorToolLearnedlFieldType> PayorLearnedList { get; set; }
    }

    [DataContract]
    public class PolicyIncomingScheduleListResponse : JSONResponse
    {
        public PolicyIncomingScheduleListResponse(string message, int errorCode, string exceptionMessage)
            : base(message, errorCode, exceptionMessage)
        {
        }
        [DataMember]
        public List<PolicyIncomingScheduleType> PolicyIncomingScheduleList { get; set; }
    }

    [DataContract]
    public class PolicyIncomingPaymentResponse: JSONResponse
    {
        public PolicyIncomingPaymentResponse(string message, int errorCode, string exceptionMessage)
            : base(message, errorCode, exceptionMessage)
        {
        }
        [DataMember]
        public List<PolicyIncomingPaymentType> PolicyIncomingPaymentList { get; set; }
    }

    [DataContract]
    public class PolicyModeResponse : JSONResponse
    {
        public PolicyModeResponse(string message, int errorCode, string exceptionMessage)
            : base(message, errorCode, exceptionMessage)
        {
        }
        [DataMember]
        public List<PolicyMode> PolicyModeList { get; set; }
    }

    [DataContract]
    public class PolicyOutgoingScheduleTypeResponse : JSONResponse
    {
        public PolicyOutgoingScheduleTypeResponse(string message, int errorCode, string exceptionMessage)
            : base(message, errorCode, exceptionMessage)
        {
        }
        [DataMember]
        public List<PolicyOutgoingScheduleType> PolicyOutgoingScheduleTypeList { get; set; }
    }

    [DataContract]
    public class PolicyStatusListResponse : JSONResponse
    {
        public PolicyStatusListResponse(string message, int errorCode, string exceptionMessage)
            : base(message, errorCode, exceptionMessage)
        {
        }
        [DataMember]
        public List<PolicyStatus> PolicyStatusList { get; set; }
    }

    [DataContract]
    public class PolicyTerminationReasonListResponse : JSONResponse
    {
        public PolicyTerminationReasonListResponse(string message, int errorCode, string exceptionMessage)
            : base(message, errorCode, exceptionMessage)
        {
        }
        [DataMember]
        public List<PolicyTerminationReason> PolicyTerminationReasonList { get; set; }
    }

    [DataContract]
    public class QuestionListResponse : JSONResponse
    {
        public QuestionListResponse(string message, int errorCode, string exceptionMessage)
            : base(message, errorCode, exceptionMessage)
        {
        }
        [DataMember]
        public List<Question> QuestionList { get; set; }
    }

    [DataContract]
    public class RegionListResponse : JSONResponse
    {
        public RegionListResponse(string message, int errorCode, string exceptionMessage)
            : base(message, errorCode, exceptionMessage)
        {
        }
        [DataMember]
        public List<Region> RegionList { get; set; }
    }

    [DataContract]
    public class SystemConstantListResponse : JSONResponse
    {
        public SystemConstantListResponse(string message, int errorCode, string exceptionMessage)
            : base(message, errorCode, exceptionMessage)
        {
        }
        [DataMember]
        public List<SystemConstant> SystemConstantList { get; set; }
    }

    [DataContract]
    public class ZipListResponse : JSONResponse
    {
        public ZipListResponse(string message, int errorCode, string exceptionMessage)
            : base(message, errorCode, exceptionMessage)
        {
        }
        [DataMember]
        public List<Zip> ZipList { get; set; }
    }

    [DataContract]
    public class ZipObjectResponse : JSONResponse
    {
        public ZipObjectResponse(string message, int errorCode, string exceptionMessage)
            : base(message, errorCode, exceptionMessage)
        {
        }
        [DataMember]
        public Zip ZipObject { get; set; }
    }

    [DataContract]
    public class PayorToolMaskedFieldTypeListResponse : JSONResponse
    {
        public PayorToolMaskedFieldTypeListResponse(string message, int errorCode, string exceptionMessage)
            : base(message, errorCode, exceptionMessage)
        {
        }
        [DataMember]
        public List<PayorToolMaskedFieldType> PayorToolMaskedFieldTypeList { get; set; }
    }

    [DataContract]
    public class PolicyDetailMasterDataListResponse : JSONResponse
    {
        public PolicyDetailMasterDataListResponse(string message, int errorCode, string exceptionMessage)
            : base(message, errorCode, exceptionMessage)
        {
        }
        [DataMember]
        public List<PolicyDetailMasterData> PolicyDetailMasterDataList { get; set; }
    }

    #region Issues List
    [DataContract]
    public class IssueReasonResponse : JSONResponse
    {
        public IssueReasonResponse(string message, int errorCode, string exceptionMessage)
            : base(message, errorCode, exceptionMessage)
        {
        }
        [DataMember]
        public IssueReasons IssueReasonObject { get; set; }
    }
    [DataContract]
    public class IssueReasonListResponse : JSONResponse
    {
        public IssueReasonListResponse(string message, int errorCode, string exceptionMessage)
            : base(message, errorCode, exceptionMessage)
        {
        }
        [DataMember]
        public List<IssueReasons> IssueReasonList { get; set; }
    }

    [DataContract]
    public class IssueStatusResponse : JSONResponse
    {
        public IssueStatusResponse(string message, int errorCode, string exceptionMessage)
            : base(message, errorCode, exceptionMessage)
        {
        }
        [DataMember]
        public IssueStatus IssueStatusObject { get; set; }
    }
    [DataContract]
    public class IssueStatusListResponse : JSONResponse
    {
        public IssueStatusListResponse(string message, int errorCode, string exceptionMessage)
            : base(message, errorCode, exceptionMessage)
        {
        }
        [DataMember]
        public List<IssueStatus> IssueStatusList { get; set; }
    }
    [DataContract]
    public class IssueCategoryListResponse : JSONResponse
    {
        public IssueCategoryListResponse(string message, int errorCode, string exceptionMessage)
            : base(message, errorCode, exceptionMessage)
        {
        }
        [DataMember]
        public List<IssueCategory> IssueCategoryList { get; set; }
    }
    [DataContract]
    public class IssueResultListResponse : JSONResponse
    {
        public IssueResultListResponse(string message, int errorCode, string exceptionMessage)
            : base(message, errorCode, exceptionMessage)
        {
        }
        [DataMember]
        public List<IssueResults> IssueResultList { get; set; }
    }
#endregion
}