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
    public class UserResponse : JSONResponse
    {
        public UserResponse(string message, int errorCode, string exceptionMessage)
            : base(message, errorCode, exceptionMessage)
        {
        }
        [DataMember]
        public userInfo UserDetails { get; set; }
        [DataMember]
        public HouseAccountDetails HouseAccountdetails { get; set; }
        [DataMember]
        public string UserToken { get; set; }
    }

    [DataContract]
    public class LicenseesListResponse : JSONResponse
    {
        public LicenseesListResponse(string message, int errorCode, string exceptionMessage)
            : base(message, errorCode, exceptionMessage)
        {
        }
        [DataMember]
        public List< LicenseeListDetail> licenseeList{ get; set; }
    }
    [DataContract]
    public class UserObjectResponse : JSONResponse
    {
        public UserObjectResponse(string message, int errorCode, string exceptionMessage)
            : base(message, errorCode, exceptionMessage)
        {
        }

        [DataMember]
        public User UserObject { get; set; }
    }

    [DataContract]
    public class ValidUserResponse : JSONResponse
    {
        public ValidUserResponse(string message, int errorCode, string exceptionMessage)
            : base(message, errorCode, exceptionMessage)
        {
        }
        [DataMember]
        public ValidUserDetail UserObject { get; set; }
    }

    [DataContract]
    public class UsersPermissionResponse : JSONResponse
    {
        public UsersPermissionResponse(string message, int errorCode, string exceptionMessage)
            : base(message, errorCode, exceptionMessage)
        {
        }
        [DataMember]
        public List<UserPermissions> UsersList { get; set; }
    }

    [DataContract]
    public class UsersListResponse : JSONResponse
    {
        public UsersListResponse(string message, int errorCode, string exceptionMessage)
            : base(message, errorCode, exceptionMessage)
        {
        }
        [DataMember]
        public List<User> UsersList { get; set; }
    }
    [DataContract]
    public class GetPayeeListResponse : JSONResponse
    {
        public GetPayeeListResponse(string message, int errorCode, string exceptionMessage)
            : base(message, errorCode, exceptionMessage)
        {
        }
        [DataMember]
        public List<GetPayeeList> PayeeList { get; set; }
    }
   

    [DataContract]
    public class ResetPasswordResponse : JSONResponse
    {
        public ResetPasswordResponse(string message, int errorCode, string exceptionMessage)
            : base(message, errorCode, exceptionMessage)
        {
        }
        [DataMember]
        public string  UsersName { get; set; }
    }
    [DataContract]
    public class GetUsersListResponse : JSONResponse
    {
        public GetUsersListResponse(string message, int errorCode, string exceptionMessage)
            : base(message, errorCode, exceptionMessage)
        {
        }
        [DataMember]
        public List<UserListObject> UsersList { get; set; }
       
        int _agentrecordCount;
        int _userRecordCounts;
        int _dataEntryUserRecordCount;
        int _selectedUserRecordCount;
        [DataMember]
        public int AgentRecordCount
        {
            get { return _agentrecordCount; }
            set { _agentrecordCount = value; }
        }
        [DataMember]
        public int UserRecordCount
        {
            get { return _userRecordCounts; }
            set { _userRecordCounts = value; }
        }
        [DataMember]
        public int DataEntryUserRecordCount
        {
            get { return _dataEntryUserRecordCount; }
            set { _dataEntryUserRecordCount = value; }
        }
        [DataMember]
        public int SelectedUserRecordCount
        {
            get { return _selectedUserRecordCount; }
            set { _selectedUserRecordCount = value; }
        }
        [DataMember]
        public List<DataEntryUserListObject> DataEntryUsersList { get; set; }

        //int _recordCounts;
        //[DataMember]
        //public int RecordCounts
        //{
        //    get { return _recordCounts; }
        //    set { _recordCounts = value; }
        //}
    }

    [DataContract]
    public class UsersCountResponse : JSONResponse
    {
        public UsersCountResponse(string message, int errorCode, string exceptionMessage)
            : base(message, errorCode, exceptionMessage)
        {
        }
        [DataMember]
        public int UsersCount { get; set; }
    }


    [DataContract]
    public class DeleteUserResponse : JSONResponse
    {
        public DeleteUserResponse(string message, int errorCode, string exceptionMessage)
            : base(message, errorCode, exceptionMessage)
        {
        }
        [DataMember]
        public bool IsDeleted { get; set; }
        [DataMember]
        public int IsDeleteSuccess { get; set; }
    }

    [DataContract]
    public class LinkedUsersListResponse : JSONResponse
    {
        public LinkedUsersListResponse(string message, int errorCode, string exceptionMessage)
            : base(message, errorCode, exceptionMessage)
        {
        }

        [DataMember]
        public List<LinkedUser> UsersList { get; set; }
        public int _recordCount;
        [DataMember]
        public int RecordCount
        {
            get { return _recordCount; }
            set { _recordCount = value; }
        }
    }

    [DataContract]
    public class UsernameResponse : JSONResponse
    {
        public UsernameResponse(string message, int errorCode, string exceptionMessage)
            : base(message, errorCode, exceptionMessage)
        {
        }

        [DataMember]
        public string UserName { get; set; }
    }

    [DataContract]
    public class UserPasswordHintResponse : JSONResponse
    {
        public UserPasswordHintResponse(string message, int errorCode, string exceptionMessage)
            : base(message, errorCode, exceptionMessage)
        {
        }

        [DataMember]
        public string SecurityQues { get; set; }
    }
    [DataContract]
    public class UserCredentialIdResponse : JSONResponse
    {
        public UserCredentialIdResponse(string message, int errorCode, string exceptionMessage)
            : base(message, errorCode, exceptionMessage)
        {
        }

        [DataMember]
        public string usercredentialId { get; set; }
    }
    [DataContract]
    public class HasPasswordKeyExpiredResponse : JSONResponse
    {
        public HasPasswordKeyExpiredResponse(string message, int errorCode, string exceptionMessage)
            : base(message, errorCode, exceptionMessage)
        {
        }

        [DataMember]
        public string Email { get; set; }
    }

    [DataContract]
    public class LocationAutoCompleteResponse : JSONResponse
    {
        public LocationAutoCompleteResponse(string message, int errorCode, string exceptionMessage)
            : base(message, errorCode, exceptionMessage)
        {
        }

        [DataMember]
        public List<string> Locations { get; set; }
    }

}