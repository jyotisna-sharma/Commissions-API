using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace MyAgencyVault.BusinessLibrary
{
    [DataContract]
    public class UserDetail
    {
        #region "Data Member / public properties"  
        [DataMember]
        public string FirstName { get; set; }
        [DataMember]
        public string LastName { get; set; }
        [DataMember]
        public string Company { get; set; }
        [DataMember]
        public string NickName { get; set; }
        [DataMember]
        public string Address { get; set; }
        [DataMember]
        public string ZipCode { get; set; }
        [DataMember]
        public string City { get; set; }
        [DataMember]
        public string State { get; set; }
        [DataMember]
        public string Email { get; set; }
        [DataMember]
        public string OfficePhone { get; set; }
        [DataMember]
        public string CellPhone { get; set; }
        [DataMember]
        public string OfficePhone_DialCode { get; set; }
        [DataMember]
        public string CellPhone_DialCode { get; set; }
        [DataMember]
        public string Fax_DialCode { get; set; }
        [DataMember]
        public string OfficePhone_CountryCode { get; set; }
        [DataMember]
        public string CellPhone_CountryCode { get; set; }
        [DataMember]
        public string Fax_CountryCode { get; set; }
        [DataMember]
        public string FormattedAddress { get; set; }
        [DataMember]
        public string Fax { get; set; }
        [DataMember]
        public double FirstYearDefault { get; set; }
        [DataMember]
        public double RenewalDefault { get; set; }
        [DataMember]
        public bool ReportForEntireAgency { get; set; }
        [DataMember]
        public bool ReportForOwnBusiness { get; set; }
        [DataMember]
        public bool IsDeleted { get; set; }
        [DataMember]
        public string Place_Id { get; set; }
        [DataMember]
        public string BGUserId { get; set; }

        [DataMember]
        public ContactDetails OfficePhoneNumber { get; set; }
        [DataMember]
        public ContactDetails MobileNumber { get; set; }
        [DataMember]
        public ContactDetails FaxNumber { get; set; }

        #endregion
    }

    [DataContract]
    public class ValidUserDetail
    {
        #region "Data Member / public properties"
        [DataMember]
        public string UserName { get; set; }
        [DataMember]
        public string Password { get; set; }
        [DataMember]
        public string PasswordHintA { get; set; }
        [DataMember]
        public string PasswordHintQ { get; set; }
        [DataMember]
        public UserRole Role { get; set; }
        [DataMember]
        public Guid UserCredentialID { get; set; }
        [DataMember]
        public bool IsHouseAccount { get; set; }
        [DataMember]
        public bool IsDeleted { get; set; }
        [DataMember]
        public bool? IsLicenseDeleted { get; set; }
        [DataMember]
        public Guid? LicenseeId { get; set; }
        [DataMember]
        public string Email { get; set; }

        #endregion
    }
    [DataContract]
    public class ContactDetails
    {
        [DataMember]
        public string PhoneNumber { get; set; }

        [DataMember]
        public string DialCode { get; set; }
        [DataMember]
        public string CountryCode { get; set; }
      
    }
}
