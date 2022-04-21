using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace MyAgencyVault.BusinessLibrary
{
    [DataContract]
    public class PolicyListObject
    {
        [DataMember]
        public string PolicyNumber { get; set; }
        [DataMember]
        public string Insured { get; set; }
        [DataMember]
        public string ClientName { get; set; }
        [DataMember]
        public string Payor { get; set; }
        [DataMember]
        public string Carrier { get; set; }
        [DataMember]
        public string Effective { get; set; }
        [DataMember]
        public string product { get; set; }
        [DataMember]
        public string Type { get; set; }
        [DataMember]
        public string PAC { get; set; }
        [DataMember]
        public string PayType { get; set; }
        [DataMember]
        public string Status { get; set; }
        [DataMember]
        public Guid PolicyId { get; set; }
        [DataMember]
        public Guid ClientId { get; set; }
        [DataMember]
        public String ImportPolicyID { get; set; }
        [DataMember]
        public string CompTypeName { get; set; }
        [DataMember]        public string Enrolled { get; set; }        [DataMember]        public string Eligible { get; set; }

        [DataMember]
        public bool? IsOutGoingBasicSchedule { get; set; }
        [DataMember]
        public bool? IsCustomBasicSchedule { get; set; }
        [DataMember]
        public bool? IsTieredSchedule { get; set; }        [DataMember]
        public string CustomDateType { get; set; }        [DataMember]        public IList<PolicyOutgoingSchedules> schedule { get; set; }

        [DataMember]
        public Guid? CreatedBy { get; set; }

    }

    public class PolicyOutgoingSchedules
    {
        [DataMember]
        public Guid OutgoingScheduleId { get; set; }
        [DataMember]
        public Guid PolicyId { get; set; }
        [DataMember]
        public double? FirstYearPercentage { get; set; }
        [DataMember]
        public double? RenewalPercentage { get; set; }
        [DataMember]
        public double? splitpercentage { get; set; }
        //[DataMember]
        //public DateTime? EffectiveFromDate { get; set; }

        DateTime? _customStartDate;
        [DataMember]
        public DateTime? CustomStartDate
        {
            get
            {
                return  _customStartDate;
            }
            set
            {
                _customStartDate = value;
                if (value != null && string.IsNullOrEmpty(CustomStartDateString))
                {
                    CustomStartDateString = Convert.ToDateTime(value).ToString("MM/dd/yyyy");
                }
            }
        }
        string _customStartDateString;
        [DataMember]
        public string CustomStartDateString
        {
            get
            {
                return _customStartDateString;
            }
            set
            {
                _customStartDateString = value;
                if (CustomStartDate == null && !string.IsNullOrEmpty(_customStartDateString))
                {
                    DateTime dt;
                    DateTime.TryParse(_customStartDateString, out dt);
                    CustomStartDate = dt;
                }
            }
        }

        //[DataMember]
        //public DateTime? EffectiveToDate { get; set; }
        DateTime? _customEndDate;
        [DataMember]
        public DateTime? CustomEndDate
        {
            get
            {
                return _customEndDate;
            }
            set
            {
                _customEndDate = value;
                if (value != null && string.IsNullOrEmpty(CustomEndDateString))
                {
                    CustomEndDateString = value.ToString();
                }
            }
        }
        string customEndDateString;
        [DataMember]
        public string CustomEndDateString
        {
            get
            {
                return customEndDateString;
            }
            set
            {
                customEndDateString = value;
                if (CustomEndDate == null && !string.IsNullOrEmpty(customEndDateString))
                {
                    DateTime dt;
                    DateTime.TryParse(customEndDateString, out dt);
                    CustomEndDate = dt;
                }
            }
        }

        [DataMember]
        public Guid PayeeUserCredentialId { get; set; }
        [DataMember]
        public string PayeeName { get; set; }
        [DataMember]
        public bool IsPrimaryAgent { get; set; }
        [DataMember]
        public int ScheduleTypeId { get; set; }

        [DataMember]
        public int? TierNumber { get; set; }

    }
    public class PolicyOutgoingScheduleList
    {
        [DataMember]
        public Guid UserCredentialId { get; set; }
        [DataMember]
        public string FirstName { get; set; }
        [DataMember]
        public string LastName { get; set; }
        [DataMember]
        public string NickName { get; set; }
        
    }

    public class HouseAccountDetails
    {
        [DataMember]
        public Guid UserCredentialId { get; set; }
        [DataMember]
        public string FirstName { get; set; }
        [DataMember]
        public string LastName { get; set; }
        [DataMember]
        public string NickName { get; set; }

        public bool IsHouseAccount { get; set; }

        public Guid LicenseeId { get; set; }

    }



}

