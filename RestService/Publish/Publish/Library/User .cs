using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyAgencyVault.BusinessLibrary;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.ServiceModel.Activation;
using System.Runtime.Serialization;

namespace MyAgencyVault.WcfService
{
    [ServiceContract]
    interface IUser
    {
        [OperationContract]
        void AddUpdateUser(User Usr);

        [OperationContract]
        void AddUpdateUserPermissionAndOtherData(User Usr);

        [OperationContract]
        bool DeleteUserInfo(User Usr);

        [OperationContract(Name = "UsersWithRole")]
        List<User> GetUsers(UserRole RoleId);

        [OperationContract]
        List<User> GetHouseUsers(Guid LincessID, int intRoleID, bool IsHouseAccount);

        [OperationContract]
        List<User> GetAllUsers();

        [OperationContract]
        IEnumerable<User> GetUsersByLicensee(Guid LicenseeId);

        [OperationContract]
        IEnumerable<User> GetUsersForReports(Guid LicenseeId);


        [OperationContract(Name = "UsersWithLicenseeId")]
        IEnumerable<User> GetUsers(Guid? LicenseeId, UserRole RoleIdToView);

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

        [OperationContract]
        User GetUserIdWise(Guid UserCredId);

        [OperationContract]
        string getUserEmail(Guid UserID);

        [OperationContract]
        void TurnOnNewsToFlashBit();

        [OperationContract]
        void TurnOffNewsToFlashBit(Guid userId);

        [OperationContract]
        bool IsUserNameExist(Guid userId, string userName);

        [OperationContract]
        List<LinkedUser> GetLinkedUser(Guid UserCredentialId, UserRole RoleId, bool isHouseValue);

        [OperationContract]
        List<User> GetAllPayee();

        [OperationContract]
        List<User> GetAllPayeeByLicencessID(Guid licID);

        [OperationContract]
        bool CheckAccoutExec(Guid userCredencialID);

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

        [OperationContract]
        List<LinkedUser> GetAllLinkedUser(List<User> objUserList, Guid GuidLicID, Guid UserCredentialId, int intRole, bool boolHouseAccount);
        
        //[OperationContract]
        //List<LinkedUser> GetSeletedLinkedUser(Guid UserCredentialId, int RoleId, bool isHouseValue);
        [OperationContract]
        void UpdateAccountExec(Guid userCredencialID, bool bvalue);

        [OperationContract]
        int GetAccountExecCount(Guid licensssID);
        
        [OperationContract]
        int GetAllAccountExecCount();

        [OperationContract]
        void AddLoginLogoutTime(Guid UserID, string AppVersion, string Activity);

        //[OperationContract]
        //void DeleteTempFolderContents();
    }
    
    public partial class MavService : IUser
    {
        #region IUser Members

        public void AddLoginLogoutTime(Guid UserID, string AppVersion, string Activity)
        {
            User.AddLoginLogoutTime(UserID, AppVersion, Activity);
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

        public void AddUpdateUser(User Usr)
        {
            Usr.AddUpdateUser();
        }

        public void AddUpdateUserPermissionAndOtherData(User Usr)
        {
            Usr.AddUpdateUserPermission();
        }

        public bool DeleteUserInfo(User Usr)
        {
            return Usr.Delete();
        }

        public List<User> GetUsers(UserRole RoleId)
        {
            return User.GetUsers(RoleId);
        }


        public List<User> GetHouseUsers(Guid LincessID, int intRoleID, bool IsHouseAccount)
        {
            User objUser = new User();
            return objUser.GetHouseUsers(LincessID, intRoleID, IsHouseAccount);
        }

        public IEnumerable<User> GetUsers(Guid? LicenseeId, UserRole RoleIdToView)
        {
            IEnumerable<User> _usr = User.GetUsers(LicenseeId, RoleIdToView);
            return _usr;
        }

        public  userInfo GetValidIdentity(string UserName, string Password)
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

        public List<LinkedUser> GetLinkedUser(Guid UserCredentialId, UserRole RoleId, bool isHouseValue)
        {
            User objUser = new User();
            return objUser.GetLinkedUser(UserCredentialId, RoleId, isHouseValue);
        }

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

        public IEnumerable<User> GetUsersByLicensee(Guid LicenseeId)
        {
            return User.GetUsers(LicenseeId);
        }

        public IEnumerable<User> GetUsersForReports(Guid LicenseeId)
        {
            return User.GetUsersForReports(LicenseeId);
        }

        

        public IEnumerable<User> GetAllUsersByLicChunck(Guid LicenseeId, int skip, int take)
        {
            return User.GetAllUsersByLicChunck(LicenseeId ,  skip,  take);
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
        public User GetUserIdWise(Guid UserCredId)
        {
            return User.GetUserIdWise(UserCredId);
        }

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

        public bool CheckAccoutExec(Guid userCredencialID)
        {
            User objUser = new User();
            return objUser.CheckAccoutExec(userCredencialID);
        }

        public User GetUserWithinLicensee(Guid userCredencialID, Guid LicenseeID)
        {
            User objUser = new User();
            return objUser.GetUserWithinLicensee(userCredencialID, LicenseeID);
        }

        public List<LinkedUser> GetAllLinkedUser(List<User> objUserList, Guid GuidLicID, Guid UserCredentialId, int intRole, bool boolHouseAccount)
        {
            User objUser = new User();
            return objUser.GetAllLinkedUser(objUserList, GuidLicID, UserCredentialId, intRole, boolHouseAccount);
        }

        public void ImportHouseUsers(System.Data.DataTable TempTable, Guid LincessID)
        {
            User objUser = new User();
            objUser.ImportHouseUsers(TempTable, LincessID);
        }

        public void UpdateAccountExec(Guid userCredencialID, bool bvalue)
        {
            User objUser = new User();
            objUser.UpdateAccountExec(userCredencialID, bvalue);
        }

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
        public String RembStatus { get; set; }
        [DataMember]
        public String SStatus { get; set; }
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
        public bool IsDeleted { get; set; }
        [DataMember]
        public bool? IsLicenseDeleted { get; set; }
        [DataMember]
        public Guid? LicenseeId { get; set; }
        //[DataMember]
        //public DateTime CreateOn { get; set; }
        [DataMember]
        public string LicenseeName { get; set; }
        [DataMember]
        public List<Client> UserClients { get; set; }
        [DataMember]
        public UserRole Role { get; set; }
        [DataMember]
        public Guid AttachedToLicensee { get; set; }
        [IgnoreDataMember]
        private List<UserPermissions> _Permissions;
        [DataMember]
        public List<UserPermissions> Permissions
        {
            get
            {
                //using (DLinq.CommissionDepartmentEntities DataModel = Entity.DataModel)
                //{
                    List<UserPermissions> _permissions = null;
                //    _permissions = (from usr in DataModel.UserPermissions
                //                    where (usr.UserCredential.UserCredentialId == this.UserCredentialID && usr.UserCredential.IsDeleted == false)
                //                    select new UserPermissions
                //                    {
                //                        UserID = usr.UserCredential.UserCredentialId,
                //                        Module = (MasterModule)usr.MasterModule.ModuleId,
                //                        Permission = (ModuleAccessRight)usr.MasterAccessRight.AccessRightId,
                //                        UserPermissionId = usr.UserPermissionId
                //                    }).OrderBy(p => p.Module).ToList();
                    return _permissions ?? new List<UserPermissions>();
                //}
            }
            set
            {
                _Permissions = value;
            }
        }
        /// <summary>
        /// all the users to which the current logged in user have the accessibility to see their data.
        /// </summary> 
        [IgnoreDataMember]
        private List<LinkedUser> _Linkedusers;
        [DataMember]
        public List<LinkedUser> LinkedUsers
        {
            get
            {
                 List<LinkedUser> LnkUsers = null;
               /* using (DLinq.CommissionDepartmentEntities DataModel = Entity.DataModel)
                {
                    List<LinkedUser> LnkUsers = null;

                    //if (this.Role == UserRole.Agent && this.IsHouseAccount == false)
                    //{
                    //    List<DLinq.UserCredential> connectedLinkedUsers = DataModel.UserCredentials.FirstOrDefault(s => s.UserCredentialId == this.UserCredentialID).UserCredentials.Where(s => s.IsDeleted == false && s.UserCredentialId != this.UserCredentialID).ToList();

                    //    if (connectedLinkedUsers != null && connectedLinkedUsers.Count != 0)
                    //        LnkUsers = (from usr in connectedLinkedUsers
                    //                    select new LinkedUser
                    //                    {
                    //                        UserId = usr.UserCredentialId,
                    //                        LastName = usr.UserDetail.LastName,
                    //                        FirstName = usr.UserDetail.FirstName,
                    //                        NickName = usr.UserDetail.NickName,
                    //                        IsConnected = true,
                    //                        UserName = usr.UserName
                    //                    }).ToList();

                    //    List<Guid> connectedUserIds = null;
                    //    if (LnkUsers == null)
                    //        connectedUserIds = new List<Guid>();
                    //    else
                    //        connectedUserIds = LnkUsers.Select(s => s.UserId).ToList();

                    //    List<LinkedUser> notConnectedLinkedUsers = (from usr in DataModel.UserCredentials
                    //                                                where (!connectedUserIds.Contains(usr.UserCredentialId)) && usr.LicenseeId == this.LicenseeId && usr.RoleId == 3 && (usr.UserCredentialId != this.UserCredentialID) && (usr.IsDeleted == false)
                    //                                                select new LinkedUser
                    //                                                {
                    //                                                    UserId = usr.UserCredentialId,
                    //                                                    LastName = usr.UserDetail.LastName,
                    //                                                    FirstName = usr.UserDetail.FirstName,
                    //                                                    NickName = usr.UserDetail.NickName,
                    //                                                    IsConnected = false,
                    //                                                    UserName = usr.UserName
                    //                                                }).ToList();

                    //    if (LnkUsers != null)
                    //        LnkUsers.AddRange(notConnectedLinkedUsers);
                    //    else if (notConnectedLinkedUsers != null)
                    //        LnkUsers = notConnectedLinkedUsers;
                    //}
                    //else
                    //{
                    //    List<DLinq.UserCredential> allUsersExceptHO = DataModel.UserCredentials.Where(s => s.LicenseeId == this.LicenseeId && s.IsDeleted == false && s.RoleId == 3 && s.IsHouseAccount == false).ToList();
                    //    LnkUsers = (from usr in allUsersExceptHO
                    //                select new LinkedUser
                    //                {
                    //                    UserId = usr.UserCredentialId,
                    //                    LastName = usr.UserDetail.LastName,
                    //                    FirstName = usr.UserDetail.FirstName,
                    //                    NickName = usr.UserDetail.NickName,
                    //                    IsConnected = true,
                    //                    UserName = usr.UserName
                    //                }).ToList();
                    //}
                */
                    return LnkUsers;
               // }
            }
            set
            {
                _Linkedusers = value;
            }
        }
        [DataMember]
        public bool IsNewsToFlash { get; set; }
        [DataMember]
        public Guid? HouseOwnerId { get; set; }
        [DataMember]
        public Guid? AdminId { get; set; }
        [DataMember]
        public string WebDavPath { get; set; }

        #endregion
    }

}
