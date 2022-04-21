using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace MyAgencyVault.BusinessLibrary
{
    [DataContract]
   public  class PolicyDetailObject
    {
        [DataMember]
        public Guid PolicyId { get; set; }
        [DataMember]
        public string PolicyNumber { get; set; }
        [DataMember]
        public int? PolicyStatusId { get; set; }
        [DataMember]
        public string PolicyStatusName { get; set; }
        [DataMember]
        public string PolicyType { get; set; }
        [DataMember]
        public Guid? PolicyLicenseeId { get; set; }
        [DataMember]
        public string Insured { get; set; }
        DateTime? _effective;
        [DataMember]
        public DateTime? OriginalEffectiveDate
        {
            get
            {
                return _effective;
            }
            set
            {
                _effective = value;
                if (value != null && string.IsNullOrEmpty(OriginDateString))
                {
                    OriginDateString = Convert.ToDateTime(value).ToString("MMM dd, yyyy");
                }
            }
        }
        string _origindateString;
        [DataMember]
        public string OriginDateString
        {
            get
            {
                return _origindateString;
            }
            set
            {
                _origindateString = value;
                if (OriginalEffectiveDate == null && !string.IsNullOrEmpty(_origindateString))
                {
                    DateTime dt;
                    DateTime.TryParse(_origindateString, out dt);
                    OriginalEffectiveDate = dt;
                }
            }
        }

        DateTime? _trackFrom;
        [DataMember]
        public DateTime? TrackFromDate
        {
            get
            {
                return _trackFrom;
            }
            set
            {
                _trackFrom = value;
                if (value != null && string.IsNullOrEmpty(TrackDateString))
                {
                    TrackDateString = value.ToString();
                }
            }
        }
        string _trackdateString;
        [DataMember]
        public string TrackDateString
        {
            get
            {
                return _trackdateString;
            }
            set
            {
                _trackdateString = value;
                if (_trackFrom == null && !string.IsNullOrEmpty(_trackdateString))
                {
                    DateTime dt;
                    DateTime.TryParse(_trackdateString, out dt);
                    TrackFromDate = dt;
                }
            }
        }

        [DataMember]
        public int? PolicyModeId { get; set; }
        [DataMember]
        public decimal? ModeAvgPremium { get; set; }
        [DataMember]
        public string SubmittedThrough { get; set; }
        [DataMember]
        public string Enrolled { get; set; }
        [DataMember]
        public string Eligible { get; set; }
        //[DataMember]
        //public DateTime? PolicyTerminationDate { get; set; }

        DateTime? _autoTerm;
        [DataMember]
        public DateTime? PolicyTerminationDate
        {
            get
            {
                return _autoTerm;
            }
            set
            {
                _autoTerm = value;
                if (value != null && string.IsNullOrEmpty(AutoTerminationDateString))
                {
                    AutoTerminationDateString = value.ToString();
                }
            }
        }

        string autoTerminationdateString;
        [DataMember]
        public string AutoTerminationDateString
        {
            get
            {
                return autoTerminationdateString;
            }
            set
            {
                autoTerminationdateString = value;
                if (PolicyTerminationDate == null && !string.IsNullOrEmpty(autoTerminationdateString))
                {
                    DateTime dt;
                    DateTime.TryParse(autoTerminationdateString, out dt);
                    PolicyTerminationDate = dt;
                }
            }
        }

        [DataMember]
        public int? TerminationReasonId { get; set; }
        [DataMember]
        public bool IsTrackMissingMonth { get; set; }
        [DataMember]
        public bool IsTrackIncomingPercentage { get; set; }
        [DataMember]
        public bool IsTrackPayment { get; set; }
        [DataMember]
        public bool IsDeleted { get; set; }
        [DataMember]
        public Guid OldPolicyId { get; set; }
        [DataMember]
        public Guid? CarrierID { get; set; }
        [DataMember]
        public string CarrierName { get; set; }
        [DataMember]
        public Guid? CoverageId { get; set; }
        [DataMember]
        public string CoverageName { get; set; }
        [DataMember]
        public Guid? ClientId { get; set; }
        [DataMember]
        public string ClientName { get; set; }
        [DataMember]
        public Guid? ReplacedBy { get; set; }
        [DataMember]
        public Guid? DuplicateFrom { get; set; }
        [DataMember]
        public bool? IsIncomingBasicSchedule { get; set; }
        [DataMember]
        public bool? IsOutGoingBasicSchedule { get; set; }
        [DataMember]
        public string PayorNickName { get; set; }//Recently Added
        [DataMember]
        public Guid? PayorId { get; set; }//Recently Added
        [DataMember]
        public string PayorName { get; set; }//Recently Added
        [DataMember]
        public double? SplitPercentage { get; set; }//Recently added

        [DataMember]
        public Int32? Advance { get; set; }//Recently 27/08/2013 added        

        [DataMember]
        public int? IncomingPaymentTypeId { get; set; }

        //[DataMember]
        //public DateTime? LastFollowUpRuns { get; set; }

        DateTime? _lastruns;
        [DataMember]
        public DateTime? LastFollowUpRuns
        {
            get
            {
                return _lastruns;
            }
            set
            {
                _lastruns = value;
                if (value != null && string.IsNullOrEmpty(LastFollowupRunString))
                {
                    LastFollowupRunString = value.ToString();
                }
            }
        }
        string _lastRunString;
        [DataMember]
        public string LastFollowupRunString
        {
            get
            {
                return _lastRunString;
            }
            set
            {
                _lastRunString = value;
                if (_lastruns == null && !string.IsNullOrEmpty(_lastRunString))
                {
                    DateTime dt;
                    DateTime.TryParse(_lastRunString, out dt);
                    LastFollowUpRuns = dt;
                }
            }
        }

        [DataMember]
        public string PolicyIncomingPayType { get; set; } //added by vinod Eric new Enhancement

        [DataMember]
        //   public DateTime? CreatedOn { get; set; }
        DateTime? _createdOn;
        [DataMember]
        public DateTime? CreatedOn
        {
            get
            {
                return _createdOn;
            }
            set
            {
                _createdOn = value;
                if (value != null && string.IsNullOrEmpty(CreatedOnstring))
                {
                    CreatedOnstring = value.ToString();
                }
            }
        }
        string _createdOnString;
        [DataMember]
        public string CreatedOnstring
        {
            get
            {
                return _createdOnString;
            }
            set
            {
                _createdOnString = value;
                if (CreatedOn == null && !string.IsNullOrEmpty(_createdOnString))
                {
                    DateTime dt;
                    DateTime.TryParse(_createdOnString, out dt);
                    CreatedOn = dt;
                }
            }
        }


        [DataMember]
        public Guid CreatedBy { get; set; }
        [DataMember]
        public bool IsSavedPolicy { get; set; }
        [DataMember]
        public int? CompType { get; set; }
        [DataMember]
        public string CompSchuduleType { get; set; }

        [DataMember]
        public PolicyDetailPreviousData PolicyPreviousData { get; set; }
        [DataMember]
        public Byte[] RowVersion { get; set; }
        [DataMember]
        public PolicyLearnedFieldData LearnedFields { get; set; }
        [DataMember]
        public string RenewalPercentage { get; set; }
        [DataMember]
        public List<PolicyPaymentEntriesPost> policyPaymentEntries { get; set; }
        [DataMember]
        public List<PolicyOutgoingSchedule> PolicyOutGoingSchedules { get; set; }
        [DataMember]
        public List<PolicyNotes> PolicyNotes { get; set; }
        [DataMember]
        public List<PolicyIncomingSchedule> PolicyIncomingSchedules { get; set; }

        //Added new property
        //Seleted product type for policy manager
        [DataMember]
        public string ProductType { get; set; }

        [DataMember]
        public Guid? UserCredentialId { get; set; }

        [DataMember]
        public string AccoutExec { get; set; }


    }

    [DataContract]
    public partial class PolicyDetailSetting
    {
        [DataMember]
        public bool IsTrackMissingMonth { get; set; }
        [DataMember]
        public bool IsTrackIncomingPercentage { get; set; }

    }

    [DataContract]
    public partial class PolicyDetailSchedule
    {
        [DataMember]
        public PolicyToolIncommingShedule policyIncomingSchedule { get; set; }
        [DataMember]
        public List<OutGoingPayment> OutgoingSchedule { get; set; }

    }
}

