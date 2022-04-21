using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
namespace MyAgencyVault.BusinessLibrary
{
    [DataContract]
   public  class UserLicenseeDetail
    {
        [DataMember]
        public Guid ? LicenseeId { get; set; }

        [DataMember]
        public string LicenseeName { get; set; }

        DateTime? _LastLogin;
       
        public DateTime? LastLogin
        {
            get
            {
                return _LastLogin;
            }
            set
            {
                _LastLogin = value;
                if (value != null && string.IsNullOrEmpty(LastLoginString))
                {
                    LastLoginString = value.ToString();
                }
            }
        }
        string _LastLoginString;
        [DataMember]
        public string LastLoginString
        {
            get
            {
                return _LastLoginString;
            }
            set
            {
                _LastLoginString = value;
                if (LastLogin == null && !string.IsNullOrEmpty(_LastLoginString))
                {
                    DateTime dt;
                    DateTime.TryParse(_LastLoginString, out dt);
                    LastLogin = dt;
                }
            }
        }

        //[DataMember]
        //public DateTime? LastUpload { get; set; }
        DateTime? _LastUpload;
        
        public DateTime? LastUpload
        {
            get
            {
                return _LastUpload;
            }
            set
            {
                _LastUpload = value;
                if (value != null && string.IsNullOrEmpty(LastUploadString))
                {
                    LastUploadString = value.ToString();
                }
            }
        }
        string _LastUploadString;
        [DataMember]
        public string LastUploadString
        {
            get
            {
                return _LastUploadString;
            }
            set
            {
                _LastUploadString = value;
                if (LastUpload == null && !string.IsNullOrEmpty(_LastUploadString))
                {
                    DateTime dt;
                    DateTime.TryParse(_LastUploadString, out dt);
                    LastUpload = dt;
                }
            }
        }



        DateTime? _CreatedDate;

        public DateTime? CreatedDate
        {
            get
            {
                return _CreatedDate;
            }
            set
            {
                _CreatedDate = value;
                if (value != null && string.IsNullOrEmpty(CreatedDateString))
                {
                    CreatedDateString = value.ToString();
                }
            }
        }
        string _CreatedDateString;
        [DataMember]
        public string CreatedDateString
        {
            get
            {
                return _CreatedDateString;
            }
            set
            {
                _CreatedDateString = value;
                if (CreatedDate == null && !string.IsNullOrEmpty(_CreatedDateString))
                {
                    DateTime dt;
                    DateTime.TryParse(_LastUploadString, out dt);
                    CreatedDate = dt;
                }
            }
        }

    }
    [DataContract]
    public class LicenseeListDetail
    {
        [DataMember]
        public string LicenseeName { get; set; }
        [DataMember]
        public Guid? LicenseeId { get; set; }
        [DataMember]
        public HouseAccountDetails HouseAccountDetails { get; set; }
    }
}
