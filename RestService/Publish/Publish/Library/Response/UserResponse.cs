using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
using MyAgencyVault.BusinessLibrary;

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
        public User  UserObject { get; set; }
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
    }
}