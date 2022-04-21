using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyAgencyVault.BusinessLibrary;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.ServiceModel.Activation;
using System.Runtime.Serialization;
using DLinq = DataAccessLayer.LinqtoEntity;
using System.Data.Objects.SqlClient;
using System.Globalization;

namespace MyAgencyVault.WcfService
{
    [ServiceContract]
    interface IUser
    {
        //[OperationContract]
        //void AddUpdateUser(User Usr);

        //[OperationContract]
        //void AddUpdateUserPermissionAndOtherData(User Usr);

        //[OperationContract]
        //bool DeleteUserInfo(User Usr);

        [OperationContract(Name = "UsersWithRole")]
        List<User> GetUsers(UserRole RoleId);

        [OperationContract]
        List<User> GetHouseUsers(Guid LincessID, int intRoleID, bool IsHouseAccount);

        [OperationContract]
        List<User> GetAllUsers();

        //[OperationContract]
        //IEnumerable<User> GetUsersByLicensee(Guid LicenseeId);

        //[OperationContract]
        //IEnumerable<User> GetUsersForReports(Guid LicenseeId);


        //[OperationContract(Name = "UsersWithLicenseeId")]
        //List<UserListObject> GetUsers( Guid? licenseeId, UserRole roleIdToView, int pageSize, int pageIndex, string filterBy, string sortType, string sortBy);

        [OperationContract]
        userInfo GetValidIdentity(string UserName, string Password);

        [OperationContract]
        User GetValidIdentityUsingName(string UserName);

        [OperationContract]
        List<UserPermissions> GetCurrentPermission(Guid UserCredentialId);

        [OperationContract]
        Guid GetLicenseeUserCredentialId(Guid licId);

        [OperationContract]
        void HouseAccoutTransferProcess(User user);

        // [OperationContract]
        //User GetUserIdWise(Guid UserCredId);

        [OperationContract]
        string getUserEmail(Guid UserID);

        [OperationContract]
        void TurnOnNewsToFlashBit();

        [OperationContract]
        void TurnOffNewsToFlashBit(Guid userId);

        [OperationContract]
        bool IsUserNameExist(Guid userId, string userName);

        //[OperationContract]
        //List<LinkedUser> GetLinkedUser(Guid UserCredentialId, UserRole RoleId, bool isHouseValue);

        [OperationContract]
        List<User> GetAllPayee();

        [OperationContract]
        List<User> GetAllPayeeByLicencessID(Guid licID);

        //[OperationContract]
        //bool CheckAccoutExec(Guid userCredencialID);

        [OperationContract]
        void ImportHouseUsers(System.Data.DataTable TempTable, Guid LincessID);

        [OperationContract]
        List<User> GetAccountExecByLicencessID(Guid licensssID);

        [OperationContract]
        int GetPayeeCount(Guid licensssID);

        [OperationContract]
        int GetAllPayeeCount();

        [OperationContract]
        IEnumerable<User> GetAllUsersByLicChunck(Guid LicenseeId, int skip, int take);

        [OperationContract]
        IEnumerable<User> GetAllUsersByChunck(int skip, int take);

        [OperationContract]
        User GetUserWithinLicensee(Guid userCredencialID, Guid LicenseeID);
        //[OperationContract]
        //List<LinkedUser> GetAllLinkedUser( Guid GuidLicID, Guid UserCredentialId, int intRole, bool boolHouseAccount);

        //[OperationContract]
        //List<LinkedUser> GetSeletedLinkedUser(Guid UserCredentialId, int RoleId, bool isHouseValue);
        //[OperationContract]
        //void UpdateAccountExec(Guid userCredencialID, bool bvalue);

        [OperationContract]
        int GetAccountExecCount(Guid licensssID);

        [OperationContract]
        int GetAllAccountExecCount();

        [OperationContract]
        void AddLoginLogoutTime(UserLoginParams loginParams);

        //[OperationContract]
        //void DeleteTempFolderContents();
    }

    public partial class MavService : IUser
    {
        #region IUser Members

        public void AddLoginLogoutTime(UserLoginParams loginParams)
        {
            ActionLogger.Logger.WriteLog("AddLoginLogoutTime request: " + loginParams.ToStringDump(), true);
            User.AddLoginLogoutTime(loginParams);
        }

        public List<User> GetAccountExecByLicencessID(Guid licensssID)
        {
            return User.GetAccountExecByLicencessID(licensssID);
        }

        public int GetAccountExecCount(Guid licensssID)
        {
            return User.GetAccountExecCount(licensssID);
        }

        public int GetAllAccountExecCount()
        {
            return User.GetAllAccountExecCount();
        }

        public Guid GetLicenseeUserCredentialId(Guid licId)
        {
            return User.GetLicenseeUserCredentialId(licId);
        }

        //public void AddUpdateUser(User Usr)
        //{
        //    Usr.AddUpdateUser();
        //}

        //public void AddUpdateUserPermissionAndOtherData(User Usr)
        //{
        //    Usr.AddUpdateUserPermission();
        //}

        //public bool DeleteUserInfo(User Usr)
        //{
        //    return Usr.Delete();
        //}

        public List<User> GetUsers(UserRole RoleId)
        {
            return User.GetUsers(RoleId);
        }


        public List<User> GetHouseUsers(Guid LincessID, int intRoleID, bool IsHouseAccount)
        {
            User objUser = new User();
            return objUser.GetHouseUsers(LincessID, intRoleID, IsHouseAccount);
        }

        //public List<UserListObject> GetUsers(Guid? licenseeId, UserRole roleIdToView, int pageSize, int pageIndex, string filterBy, string sortType, string sortBy)
        //{
        //    List<UserListObject> _usr = User.GetUsers(licenseeId, roleIdToView, pageSize, pageIndex, filterBy, sortType,sortBy);
        //    return _usr;
        //}

        public userInfo GetValidIdentity(string UserName, string Password)
        {
            User objUser = new User();
            //return  objUser.GetValidIdentity(UserName, Password);
            User u = objUser.GetValidIdentity(UserName, Password);
            //JSONResponse r = new JSONResponse();
            //r.ResponseCode = 200;
            //r.ExceptionMessage = "";
            //r.Message = "Successs";
            //r.UserInfo = new userInfo();
            //r.UserInfo.Name = u.UserName;
            userInfo ui = new userInfo();
            ui.UserName = u.UserName;
            ui.UserCredentialID = u.UserCredentialID;
            ui.UserClients = u.UserClients;
            return ui;
        }

        public User GetValidIdentityUsingName(string UserName)
        {
            User objUser = new User();
            return objUser.GetForgetValidIdentity(UserName);
        }

        //public List<LinkedUser> GetLinkedUser(Guid UserCredentialId, UserRole RoleId, bool isHouseValue)
        //{
        //    User objUser = new User();
        //    return objUser.GetLinkedUser(UserCredentialId, RoleId, isHouseValue);
        //}

        public int GetPayeeCount(Guid licensssID)
        {
            User objUser = new User();
            return objUser.GetPayeeCount(licensssID);
        }

        public int GetAllPayeeCount()
        {
            User objUser = new User();
            return objUser.GetAllPayeeCount();
        }

        public List<UserPermissions> GetCurrentPermission(Guid UserCredentialId)
        {
            return User.GetCurrentPermission(UserCredentialId);
        }

        public List<User> GetAllUsers()
        {
            return User.GetAllUsers();
        }

        public List<User> GetAllPayee()
        {
            return User.GetAllPayee();
        }

        public List<User> GetAllPayeeByLicencessID(Guid licID)
        {
            return User.GetAllPayeeByLicencessID(licID);
        }

        //public IEnumerable<User> GetUsersByLicensee(Guid LicenseeId)
        //{
        //    return User.GetUsers(LicenseeId);
        //}

        public IEnumerable<User> GetUsersForReports(Guid LicenseeId)
        {
            return User.GetUsersForReports(LicenseeId);
        }



        public IEnumerable<User> GetAllUsersByLicChunck(Guid LicenseeId, int skip, int take)
        {
            return User.GetAllUsersByLicChunck(LicenseeId, skip, take);
        }


        public IEnumerable<User> GetAllUsersByChunck(int skip, int take)
        {
            return User.GetAllUsersByChunck(skip, take);
        }

        public void HouseAccoutTransferProcess(User user)
        {
            User.HouseAccoutTransferProcess(user);
        }

        #endregion

        #region IUser Members

        public string getUserEmail(Guid UserID)
        {
            return User.getUserEmail(UserID);
        }
        //public User GetUserIdWise(Guid UserCredId)
        //{
        //    return User.GetUserIdWise(UserCredId);
        //}

        #endregion

        public void TurnOnNewsToFlashBit()
        {
            User.TurnOnNewsToFlashBit();
        }

        public void TurnOffNewsToFlashBit(Guid userId)
        {
            User.TurnOffNewsToFlashBit(userId);
        }

        public bool IsUserNameExist(Guid userId, string userName)
        {
            return User.IsUserNameExist(userId, userName);
        }

        //public bool CheckAccoutExec(Guid userCredencialID)
        //{
        //    User objUser = new User();
        //    return objUser.CheckAccoutExec(userCredencialID);
        //}

        public User GetUserWithinLicensee(Guid userCredencialID, Guid LicenseeID)
        {
            User objUser = new User();
            return objUser.GetUserWithinLicensee(userCredencialID, LicenseeID);
        }

        //public List<LinkedUser> GetAllLinkedUser( Guid GuidLicID, Guid UserCredentialId, int intRole, bool boolHouseAccount)
        //{
        //    User objUser = new User();
        //    return objUser.GetAllLinkedUser(GuidLicID, UserCredentialId, intRole, boolHouseAccount);
        //}

        public void ImportHouseUsers(System.Data.DataTable TempTable, Guid LincessID)
        {
            User objUser = new User();
            objUser.ImportHouseUsers(TempTable, LincessID);
        }

        //public void UpdateAccountExec(Guid userCredencialID, bool bvalue)
        //{
        //    User objUser = new User();
        //    objUser.UpdateAccountExec(userCredencialID, bvalue);
        //}

        ////public void DeleteTempFolderContents()
        ////{
        ////      User objUser = new User();
        ////      objUser.DeleteTempFolderContents();
        ////}
    }

    [DataContract]
    public class userInfo
    {
        #region  "Data members i.e. public properties"
        [DataMember]
        public String UserName { get; set; }

        [DataMember]
        public static String testName { get; set; }
        [DataMember]

        public static String IsUserActiveOnWeb { get; set; }
        [DataMember]
        public string LoginName { get; set; }

        //[DataMember]
        //public String RembStatus { get; set; }
        //[DataMember]
        //public String SStatus { get; set; }
        [DataMember]
        public string Password { get; set; }
        [DataMember]
        public string PasswordHintQ { get; set; }
        [DataMember]
        public string PasswordHintA { get; set; }
        [DataMember]
        public Guid UserCredentialID { get; set; }
        [DataMember]
        public bool IsHouseAccount { get; set; }
        //Added 13012016
        [DataMember]
        public bool? IsAccountExec { get; set; }
        //Added 04062014
        [DataMember]
        public bool? DisableAgentEditing { get; set; }
        [DataMember]
        public bool? IsAdmin { get; set; }
        [DataMember]
        public bool IsDeleted { get; set; }
        [DataMember]
        public bool? IsUserActiveOnweb { get; set; }
        [DataMember]
        public Guid? LicenseeId { get; set; }
        //[DataMember]
        //public DateTime CreateOn { get; set; }
        [DataMember]
        public string LicenseeName { get; set; }
        [DataMember]
        public string Email { get; set; }
        [DataMember]
        public List<Client> UserClients { get; set; }
        [DataMember]
        public UserRole Role { get; set; }
        //[DataMember]
        //public Guid AttachedToLicensee { get; set; }
        [IgnoreDataMember]
        private List<UserPermissions> _permissions;
        [DataMember]
        public List<UserPermissions> Permissions
        {
            get
            {
                //if (this.Role == UserRole.SuperAdmin)
                //{
                //    _permissions = User.GetSuperPermissions();
                //}
                //else
                //{
                //    using (DLinq.CommissionDepartmentEntities DataModel = Entity.DataModel)
                //    {
                //        List<UserPermissions> _permissions = null;
                //        _permissions = (from usr in DataModel.UserPermissions
                //                        where (usr.UserCredential.UserCredentialId == this.UserCredentialID && usr.UserCredential.IsDeleted == false)
                //                        select new UserPermissions
                //                        {
                //                            UserCredentialId = usr.UserCredential.UserCredentialId,
                //                            Module = (MasterModule)usr.MasterModule.ModuleId,
                //                            Permission = (ModuleAccessRight)usr.MasterAccessRight.AccessRightId,
                //                            UserPermissionId = usr.UserPermissionId
                //                        }).OrderBy(p => p.Module).ToList();
                //    }
                //}

                return _permissions ?? new List<UserPermissions>();
            }
            set
            {
                _permissions = value;
            }
        }

        private UserLicenseeDetail _userLicenseedetails;
        [DataMember]
        public UserLicenseeDetail UserLicenseedetails { get; set; }
        /// <summary>
        /// all the users to which the current logged in user have the accessibility to see their data.
        /// </summary> 

        public List<LinkedUser> _linkedusers;
        [DataMember]
        public List<LinkedUser> LinkedUsers { get; set; }
        [DataMember]
        public HouseAccountDetails HouseAccountDetails { get; set; }

        #endregion
    }

}
