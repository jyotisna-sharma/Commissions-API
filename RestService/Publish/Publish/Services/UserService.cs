using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyAgencyVault.BusinessLibrary;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.ServiceModel.Activation;
using System.Runtime.Serialization;
using MyAgencyVault.WcfService.Library.Response;
using System.ServiceModel.Dispatcher;

namespace MyAgencyVault.WcfService
{
    [ServiceContract]
    interface IUserService
    {
        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        JSONResponse AddUpdateUserService(User user);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        JSONResponse AddUpdateUserPermissionAndOtherDataService(User Usr);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        DeleteUserResponse DeleteUserInfoService(User Usr);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        UsersListResponse GetUsersOnRole(UserRole RoleId);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        UsersListResponse GetHouseUsersService(Guid LincessID, int intRoleID, bool IsHouseAccount);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        UsersListResponse GetAllUsersService();

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        UsersListResponse GetUsersByLicenseeService(Guid LicenseeId);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        UsersListResponse GetUsersForReportsService(Guid LicenseeId);


        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        UsersListResponse GetUsersService(Guid? LicenseeId, UserRole RoleIdToView);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        UserResponse GetValidIdentityService(string UserName, string Password);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        UserResponse GetValidIdentityUsingNameService(string UserName);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        UsersPermissionResponse GetCurrentPermissionService(Guid UserCredentialId);

        //[OperationContract]
        //[WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        //Guid GetLicenseeUserCredentialIdService(Guid licId);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        JSONResponse HouseAccoutTransferProcessService(User user);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        UserResponse GetUserIdWiseService(Guid UserCredId);

        //[OperationContract]
        //[WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        //string getUserEmailService(Guid UserID);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        JSONResponse  TurnOnNewsToFlashBitService();

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        JSONResponse TurnOffNewsToFlashBitService(Guid UserId);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        DeleteUserResponse IsUserNameExistService(Guid UserID, string UserName);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        LinkedUsersListResponse GetLinkedUserService(Guid UserCredentialId, UserRole RoleId, bool isHouseValue);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        UsersListResponse GetAllPayeeService(); 
       
        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        UsersListResponse GetAllPayeeByLicencessIDService(Guid licID);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        DeleteUserResponse CheckAccoutExecService(Guid userCredencialID);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        JSONResponse ImportHouseUsersService(System.Data.DataTable TempTable, Guid LincessID);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        UsersListResponse GetAccountExecByLicencessIDService(Guid licensssID);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        UsersCountResponse GetPayeeCountService(Guid licenseeID);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        UsersCountResponse GetAllPayeeCountService();

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        UsersListResponse GetAllUsersByLicChunckService(Guid LicenseeId, int skip, int take);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        UsersListResponse GetAllUsersByChunckService(int skip, int take);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        UserResponse GetUserWithinLicenseeService(Guid userCredencialID, Guid LicenseeID);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        LinkedUsersListResponse GetAllLinkedUserService(List<User> objUserList, Guid GuidLicID, Guid UserCredentialId, int intRole, bool boolHouseAccount);

        //[OperationContract]
        //List<LinkedUser> GetSeletedLinkedUser(Guid UserCredentialId, int RoleId, bool isHouseValue);
        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        JSONResponse UpdateAccountExecService(Guid userCredencialID, bool bvalue);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        UsersCountResponse GetAccountExecCountService(Guid licenseeID);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        UsersCountResponse GetAllAccountExecCountService();

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        JSONResponse AddLoginLogoutTimeService(Guid UserID, string AppVersion, string Activity);

        //[OperationContract]
        //void DeleteTempFolderContents();
    }

    public partial class MavService : IUserService, IErrorHandler
    {
        #region IUser Members

        public JSONResponse AddLoginLogoutTimeService(Guid UserID, string AppVersion, string Activity)
        {
            ActionLogger.Logger.WriteLog("AddLoginLogoutTimeService request: UserID" + UserID + ",  AppVersion: " + AppVersion, true);
            JSONResponse jres = null;
            try
            {
                User.AddLoginLogoutTime(UserID, AppVersion, Activity);
                jres = new JSONResponse("User's login activity saved successfully", (int)ResponseCodes.Success, "");
                ActionLogger.Logger.WriteLog("AddLoginLogoutTimeService success: UserID" + UserID + ",  AppVersion: " + AppVersion, true);
            }
            catch (Exception ex)
            {
                jres = new JSONResponse("", (int)ResponseCodes.Failure, ex.Message);
                ActionLogger.Logger.WriteLog("AddLoginLogoutTimeService failure: UserID" + UserID + ",  ex: " + ex.Message, true);
            }
            return jres;
        }

        public UsersListResponse GetAccountExecByLicencessIDService(Guid licensssID)
        {
            ActionLogger.Logger.WriteLog("GetAccountExecByLicencessIDService request: " + licensssID, true);
            UsersListResponse jres = null;
            try
            {
                List<User> lst = User.GetAccountExecByLicencessID(licensssID);
                jres = new UsersListResponse("Users list found successfully", (int)ResponseCodes.Success,"");
                jres.UsersList = lst;
                ActionLogger.Logger.WriteLog("GetAccountExecByLicencessIDService success: " + licensssID, true);
            }
            catch (Exception ex)
            {
                jres = new UsersListResponse("", (int)ResponseCodes.Failure, "Error getting users list" + ex.Message    );
                ActionLogger.Logger.WriteLog("GetAccountExecByLicencessIDService failure: " + licensssID, true);
            }
            return jres;
        }

        public UsersCountResponse GetAccountExecCountService(Guid licensssID)
        {
            ActionLogger.Logger.WriteLog("GetAllAccountExecCountService request: " + licensssID, true);
            UsersCountResponse jres = null;
            try
            {
                jres.UsersCount = User.GetAccountExecCount(licensssID);
                jres = new UsersCountResponse("Users list found successfully", (int)ResponseCodes.Success,"");
                ActionLogger.Logger.WriteLog("GetAllAccountExecCountService success: ", true);
            }
            catch (Exception ex)
            {
                jres = new UsersCountResponse("", (int)ResponseCodes.Failure, "Error getting users count: " + ex.Message    );
                ActionLogger.Logger.WriteLog("GetAllAccountExecCountService failure: ", true);
            }
            return jres;
        }

        public UsersCountResponse GetAllAccountExecCountService()
        {
            ActionLogger.Logger.WriteLog("GetAllAccountExecCountService request: ", true);
            UsersCountResponse jres = null;
            try
            {
                jres.UsersCount = User.GetAllAccountExecCount();
                jres = new UsersCountResponse("Users list found successfully", (int)ResponseCodes.Success,"");
                ActionLogger.Logger.WriteLog("GetAllAccountExecCountService success: ", true);
            }
            catch (Exception ex)
            {
                jres = new UsersCountResponse("", (int)ResponseCodes.Failure, "Error getting users count: " + ex.Message    );
                ActionLogger.Logger.WriteLog("GetAllAccountExecCountService failure: ", true);
            }
            return jres;
        }

        //public Guid GetLicenseeUserCredentialIdService(Guid licId)
        //{
        //    return User.GetLicenseeUserCredentialId(licId);
        //}

        public JSONResponse AddUpdateUserService(User Usr)
        {
            ActionLogger.Logger.WriteLog("AddUpdateUserService request: " + Usr.UserName, true);
            JSONResponse jres = null;
            try
            {
                Usr.AddUpdateUser();
                jres = new JSONResponse("User saved successfully", (int)ResponseCodes.Success, "");
                ActionLogger.Logger.WriteLog("AddUpdateUserService success: " + Usr.UserName, true);
            }
            catch (Exception ex)
            {
                jres = new JSONResponse("", (int)ResponseCodes.Failure, "Error saving user");
                ActionLogger.Logger.WriteLog("AddUpdateUserService failure: " + Usr.UserName, true);
            }
            return jres;
        }

        public JSONResponse AddUpdateUserPermissionAndOtherDataService(User Usr)
        {
            ActionLogger.Logger.WriteLog("AddUpdateUserPermissionAndOtherDataService request: " + Usr.UserName, true);
            JSONResponse jres = null;
            try
            {
                Usr.AddUpdateUserPermission();
                jres = new JSONResponse("Data saved successfully", (int)ResponseCodes.Success, "");
                ActionLogger.Logger.WriteLog("AddUpdateUserPermissionAndOtherDataService success: " + Usr.UserName, true);
            }
            catch (Exception ex)
            {
                jres = new JSONResponse("", (int)ResponseCodes.Failure, "Error saving data");
                ActionLogger.Logger.WriteLog("AddUpdateUserPermissionAndOtherDataService failure: " + Usr.UserName, true);
            }
            return jres;
        }

        public DeleteUserResponse  DeleteUserInfoService(User Usr)
        {
            ActionLogger.Logger.WriteLog("DeleteUserInfoService request: " + Usr.UserName, true);
            DeleteUserResponse  jres = null;
            try
            {
                 bool isDeleted = Usr.Delete();
                jres = new DeleteUserResponse("User deleted successfully", (int)ResponseCodes.Success,"");
                jres.IsDeleted = isDeleted;
                ActionLogger.Logger.WriteLog("DeleteUserInfoService success: " + Usr.UserName, true);
            }
            catch (Exception ex)
            {
                jres = new DeleteUserResponse("", (int)ResponseCodes.Failure, "Error deleting user" + ex.Message);
                ActionLogger.Logger.WriteLog("DeleteUserInfoService failure: " + Usr.UserName, true);
            }
            return jres;
        }

        public UsersListResponse GetUsersOnRole(UserRole RoleId)
        {
            ActionLogger.Logger.WriteLog("GetUsersOnRole request: " + RoleId, true);
             UsersListResponse jres = null;
            try
            {
                List<User> lst = User.GetUsers(RoleId);
                jres = new UsersListResponse("Users list found successfully", (int)ResponseCodes.Success,"");
                jres.UsersList = lst;
                ActionLogger.Logger.WriteLog("GetUsersOnRole success: " + RoleId, true);
            }
            catch (Exception ex)
            {
                jres = new UsersListResponse("", (int)ResponseCodes.Failure, "Error getting users list" + ex.Message    );
                ActionLogger.Logger.WriteLog("GetUsersOnRole failure: " + RoleId, true);
            }
            return jres;
        }

        public UsersListResponse GetHouseUsersService(Guid LincessID, int intRoleID, bool IsHouseAccount)
        {
            ActionLogger.Logger.WriteLog("GetHouseUsersService request: " + intRoleID + ", licID: " + LincessID + ", isHouse: " + IsHouseAccount, true);
             UsersListResponse jres = null;
            try
            {
                
                List<User> lst = (new User()).GetHouseUsers(LincessID, intRoleID, IsHouseAccount);
                jres = new UsersListResponse("Users list found successfully", (int)ResponseCodes.Success,"");
                jres.UsersList = lst;
                ActionLogger.Logger.WriteLog("GetUsersOnRole success: ", true);
            }
            catch (Exception ex)
            {
                jres = new UsersListResponse("", (int)ResponseCodes.Failure, "Error getting users list" + ex.Message    );
                ActionLogger.Logger.WriteLog("GetUsersOnRole failure: ", true);
            }
            return jres;
        }

        public UsersListResponse GetUsersService(Guid? LicenseeId, UserRole RoleIdToView)
        {
            ActionLogger.Logger.WriteLog("GetUsersService request RoleIdToView: " + RoleIdToView, true);
            UsersListResponse jres = null;
            try
            {
                List<User> lst = User.GetUsers(LicenseeId, RoleIdToView).ToList<User>();
                jres = new UsersListResponse("Users list found successfully", (int)ResponseCodes.Success,"");
                jres.UsersList = lst;
                ActionLogger.Logger.WriteLog("GetUsersService success: ", true);
            }
            catch (Exception ex)
            {
                jres = new UsersListResponse("", (int)ResponseCodes.Failure, "Error getting users list" + ex.Message    );
                ActionLogger.Logger.WriteLog("GetUsersService failure: ", true);
            }
            return jres;
        }

        public UserResponse GetValidIdentityService(string UserName, string Password)
        {
            ActionLogger.Logger.WriteLog("GetValidIdentityService request: " + UserName + ", Password: " + Password, true);
            UserResponse u = null;
            try
            {
                User objUser = new User();
                User usr = objUser.GetValidIdentity(UserName, Password);
                userInfo ui = new userInfo();
                ui.UserName = usr.UserName;
                ui.UserCredentialID = usr.UserCredentialID;
                ui.UserClients = usr.UserClients;

                u = new UserResponse("User found successfully", (int)ResponseCodes.Success,"");
                u.UserDetails = ui;
        //        u.UserObject = usr;
                ActionLogger.Logger.WriteLog("GetValidIdentityService request success ", true);
            }
            catch (Exception ex)
            {
                u = new UserResponse("", (int)ResponseCodes.Failure, ex.Message);
                ActionLogger.Logger.WriteLog("GetValidIdentityService request failure ", true);
            }
            return u;
        }

        public UserResponse GetValidIdentityUsingNameService(string UserName)
        {
            ActionLogger.Logger.WriteLog("GetValidIdentityUsingNameService request: " + UserName , true);
            UserResponse u = null;
            try
            {
                User usr = (new User()).GetForgetValidIdentity(UserName);
                u = new UserResponse("User found successfully", (int)ResponseCodes.Success,"");
                u.UserObject = usr;
                ActionLogger.Logger.WriteLog("GetValidIdentityUsingNameService request success ", true);
            }
            catch (Exception ex)
            {
                u = new UserResponse("", (int)ResponseCodes.Failure, ex.Message);
                ActionLogger.Logger.WriteLog("GetValidIdentityUsingNameService request failure ", true);
            }
            return u;
        }

        public LinkedUsersListResponse GetLinkedUserService(Guid UserCredentialId, UserRole RoleId, bool isHouseValue)
        {
            ActionLogger.Logger.WriteLog("GetLinkedUserService request UserCredentialId: " + UserCredentialId, true);
            LinkedUsersListResponse jres = null;
            try
            {

                User objUser = new User();
                List < LinkedUser > lst = objUser.GetLinkedUser(UserCredentialId, RoleId, isHouseValue);
                jres = new LinkedUsersListResponse("Users list found successfully", (int)ResponseCodes.Success,"");
                jres.UsersList = lst;
                ActionLogger.Logger.WriteLog("GetLinkedUserService success: ", true);
            }
            catch (Exception ex)
            {
                jres = new LinkedUsersListResponse("", (int)ResponseCodes.Failure, "Error getting users list" + ex.Message);
                ActionLogger.Logger.WriteLog("GetLinkedUserService failure: ", true);
            }
            return jres;
        }

        public UsersCountResponse GetPayeeCountService(Guid licensssID)
        {
            ActionLogger.Logger.WriteLog("GetPayeeCountService request: ", true);
            UsersCountResponse jres = null;
            try
            {
                User objUser = new User();
                jres.UsersCount = objUser.GetPayeeCount(licensssID);
                jres = new UsersCountResponse("Users list found successfully", (int)ResponseCodes.Success,"");
                ActionLogger.Logger.WriteLog("GetPayeeCountService success: ", true);
            }
            catch (Exception ex)
            {
                jres = new UsersCountResponse("", (int)ResponseCodes.Failure, "Error getting users count: " + ex.Message    );
                ActionLogger.Logger.WriteLog("GetPayeeCountService failure: ", true);
            }
            return jres;
        }

        public UsersCountResponse GetAllPayeeCountService()
        {
            ActionLogger.Logger.WriteLog("GetAllPayeeCountService request: ", true);
            UsersCountResponse jres = null;
            try
            {
                User objUser = new User();
                jres.UsersCount = objUser.GetAllPayeeCount();
                jres = new UsersCountResponse("Users list found successfully", (int)ResponseCodes.Success,"");
                ActionLogger.Logger.WriteLog("GetAllPayeeCountService success: ", true);
            }
            catch (Exception ex)
            {
                jres = new UsersCountResponse("", (int)ResponseCodes.Failure, "Error getting users count: " + ex.Message    );
                ActionLogger.Logger.WriteLog("GetAllPayeeCountService failure: ", true);
            }
            return jres;
        }

        public UsersPermissionResponse GetCurrentPermissionService(Guid UserCredentialId)
        {
            ActionLogger.Logger.WriteLog("GetCurrentPermissionService request: " + UserCredentialId, true);
            UsersPermissionResponse jres = null;
            try
            {
                List<UserPermissions> lst = User.GetCurrentPermission(UserCredentialId);
                jres = new UsersPermissionResponse("Users list found successfully", (int)ResponseCodes.Success,"");
                jres.UsersList = lst;
                ActionLogger.Logger.WriteLog("GetCurrentPermissionService success: ", true);
            }
            catch (Exception ex)
            {
                jres = new UsersPermissionResponse("", (int)ResponseCodes.Failure, "Error getting users list" + ex.Message    );
                ActionLogger.Logger.WriteLog("GetCurrentPermissionService failure: ", true);
            }
            return jres;

        }

        public UsersListResponse GetAllUsersService()
        {
            ActionLogger.Logger.WriteLog("GetAllUsersService request: ", true);
            UsersListResponse jres = null;
            try
            {
                List<User> lst =  User.GetAllUsers();
                jres = new UsersListResponse("Users list found successfully", (int)ResponseCodes.Success,"");
                jres.UsersList = lst;
                ActionLogger.Logger.WriteLog("GetAllUsersService success: ", true);
            }
            catch (Exception ex)
            {
                jres = new UsersListResponse("", (int)ResponseCodes.Failure, "Error getting users list" + ex.Message    );
                ActionLogger.Logger.WriteLog("GetAllUsersService failure: ", true);
            }
            return jres;

        }

        public UsersListResponse GetAllPayeeService()
             {
                 ActionLogger.Logger.WriteLog("GetAllPayeeService request: ", true);
                 UsersListResponse jres = null;
                 try
                 {
                     List<User> lst = User.GetAllPayee();
                     jres = new UsersListResponse("Users list found successfully", (int)ResponseCodes.Success,"");
                     jres.UsersList = lst;
                     ActionLogger.Logger.WriteLog("GetAllPayeeService success: ", true);
                 }
                 catch (Exception ex)
                 {
                     jres = new UsersListResponse("", (int)ResponseCodes.Failure, "Error getting users list" + ex.Message    );
                     ActionLogger.Logger.WriteLog("GetAllPayeeService failure: ", true);
                 }
                 return jres;
             }
           


        
        public UsersListResponse GetAllPayeeByLicencessIDService(Guid licID)
        {
            ActionLogger.Logger.WriteLog("GetAllPayeeByLicencessIDService request: ", true);
            UsersListResponse jres = null;
            try
            {
                List<User> lst =  User.GetAllPayeeByLicencessID(licID);
                jres = new UsersListResponse("Users list found sucscessfully", (int)ResponseCodes.Success,"");
                jres.UsersList = lst;
                ActionLogger.Logger.WriteLog("GetAllPayeeByLicencessIDService success: ", true);
            }
            catch (Exception ex)
            {
                jres = new UsersListResponse("", (int)ResponseCodes.Failure, "Error getting users list" + ex.Message    );
                ActionLogger.Logger.WriteLog("GetAllPayeeByLicencessIDService failure: ", true);
            }
            return jres;
        }
     

        public UsersListResponse GetUsersByLicenseeService(Guid LicenseeId)
        {
            ActionLogger.Logger.WriteLog("GetUsersByLicenseeService request: LicID: " + LicenseeId, true);
            UsersListResponse jres = null;
            try
            {
                IEnumerable<User> lst = User.GetUsers(LicenseeId); ;
                jres = new UsersListResponse("Users list found successfully", (int)ResponseCodes.Success,"");
                jres.UsersList = lst.ToList<User>();
                ActionLogger.Logger.WriteLog("GetUsersByLicenseeService success: ", true);
            }
            catch (Exception ex)
            {
                jres = new UsersListResponse("", (int)ResponseCodes.Failure, "Error getting users list" + ex.Message    );
                ActionLogger.Logger.WriteLog("GetUsersByLicenseeService failure: ", true);
            }
            return jres;
        }

        public UsersListResponse GetUsersForReportsService(Guid LicenseeId)
        {
            ActionLogger.Logger.WriteLog("GetUsersForReportsService request: LicID: " + LicenseeId, true);
            UsersListResponse jres = null;
            try
            {
                IEnumerable<User> lst = User.GetUsersForReports(LicenseeId);
                jres = new UsersListResponse("Users list found successfully", (int)ResponseCodes.Success,"");
                jres.UsersList = lst.ToList<User>();
                ActionLogger.Logger.WriteLog("GetUsersForReportsService success: ", true);
            }
            catch (Exception ex)
            {
                jres = new UsersListResponse("", (int)ResponseCodes.Failure, "Error getting users list" + ex.Message    );
                ActionLogger.Logger.WriteLog("GetUsersForReportsService failure: ", true);
            }
            return jres;
        }



        public UsersListResponse GetAllUsersByLicChunckService(Guid LicenseeId, int skip, int take)
        {
            ActionLogger.Logger.WriteLog("GetAllUsersByChunckService request: skip " + skip + ", take: " + take + " LicID: " + LicenseeId, true);
            UsersListResponse jres = null;
            try
            {
                IEnumerable<User> lst = User.GetAllUsersByLicChunck(LicenseeId, skip, take);
                jres = new UsersListResponse("Users list found successfully", (int)ResponseCodes.Success,"");
                jres.UsersList = lst.ToList<User>();
                ActionLogger.Logger.WriteLog("GetAllUsersByChunckService success: ", true);
            }
            catch (Exception ex)
            {
                jres = new UsersListResponse("", (int)ResponseCodes.Failure, "Error getting users list" + ex.Message    );
                ActionLogger.Logger.WriteLog("GetAllUsersByChunckService failure: ", true);
            }
            return jres;
        }


        public UsersListResponse GetAllUsersByChunckService(int skip, int take)
        {
            ActionLogger.Logger.WriteLog("GetAllUsersByChunckService request: skip" + skip + ", take: " +take, true);
            UsersListResponse jres = null;
            try
            {
                IEnumerable<User> lst = User.GetAllUsersByChunck(skip, take);
                jres = new UsersListResponse("Users list found successfully", (int)ResponseCodes.Success,"");
                jres.UsersList = lst.ToList<User>();
                ActionLogger.Logger.WriteLog("GetAllUsersByChunckService success: ", true);
            }
            catch (Exception ex)
            {
                jres = new UsersListResponse("", (int)ResponseCodes.Failure, "Error getting users list" + ex.Message    );
                ActionLogger.Logger.WriteLog("GetAllUsersByChunckService failure: ", true);
            }
            return jres;
        }

        public JSONResponse HouseAccoutTransferProcessService(User user)
        {
            JSONResponse res = null;
            try
            {
                User.HouseAccoutTransferProcess(user);
                res = new JSONResponse("House account process success!", (int)ResponseCodes.Success, "");
                ActionLogger.Logger.WriteLog("HouseAccoutTransferProcessService success: ", true);
            }
            catch (Exception ex)
            {
                res = new JSONResponse("", (int)ResponseCodes.Failure, ex.Message);
                ActionLogger.Logger.WriteLog("HouseAccoutTransferProcessService failure: ", true);
            }
            return res;
        }

        #endregion

        #region IUser Members

        //public string getUserEmailService(Guid UserID)
        //{
        //    return User.getUserEmail(UserID);
        //}

        public UserResponse GetUserIdWiseService(Guid UserCredId)
        {
            ActionLogger.Logger.WriteLog("GetUserIdWiseService request: " + UserCredId , true);
            UserResponse u = null;
            try
            {
                User usr = User.GetUserIdWise(UserCredId);
                u = new UserResponse("User found successfully", (int)ResponseCodes.Success,"");
                u.UserObject  = usr;
                ActionLogger.Logger.WriteLog("GetUserIdWiseService request success ", true);
            }
            catch (Exception ex)
            {
                u = new UserResponse("", (int)ResponseCodes.Failure, ex.Message    );
                ActionLogger.Logger.WriteLog("GetUserIdWiseService request failure ", true);
            }
            return u;
        }

        #endregion

        public JSONResponse TurnOnNewsToFlashBitService()
        {
            ActionLogger.Logger.WriteLog("TurnOnNewsToFlashBitService request: ", true);
            JSONResponse u = null;
            try
            {
                User.TurnOnNewsToFlashBit();
                u = new JSONResponse("Update successful", (int)ResponseCodes.Success, "");
                ActionLogger.Logger.WriteLog("TurnOnNewsToFlashBitService request success ", true);
            }
            catch (Exception ex)
            {
                u = new JSONResponse("", (int)ResponseCodes.Failure, ex.Message);
                ActionLogger.Logger.WriteLog("TurnOnNewsToFlashBitService request failure ", true);
            }
            return u;
        }

        public JSONResponse TurnOffNewsToFlashBitService(Guid userId)
        {
            ActionLogger.Logger.WriteLog("TurnOffNewsToFlashBitService request: " + userId , true);
            JSONResponse u = null;
            try
            {
                User.TurnOffNewsToFlashBit(userId);
                u = new JSONResponse("Update successful", (int)ResponseCodes.Success, "");
                ActionLogger.Logger.WriteLog("TurnOffNewsToFlashBitService request success ", true);
            }
            catch (Exception ex)
            {
                u = new JSONResponse("", (int)ResponseCodes.Failure, ex.Message);
                ActionLogger.Logger.WriteLog("TurnOffNewsToFlashBitService request failure ", true);
            }
            return u;
        }

        public DeleteUserResponse IsUserNameExistService(Guid userId, string userName)
        {
            ActionLogger.Logger.WriteLog("IsUserNameExistService request: " + userName, true);
             DeleteUserResponse u = null;
            try
            {
                bool isExist = User.IsUserNameExist(userId, userName);
                u = new DeleteUserResponse("Service success", (int)ResponseCodes.Success,"");
                u.IsDeleted = isExist;
                ActionLogger.Logger.WriteLog("IsUserNameExistService request success ", true);
            }
            catch (Exception ex)
            {
                u = new DeleteUserResponse("", (int)ResponseCodes.Failure, ex.Message    );
                ActionLogger.Logger.WriteLog("IsUserNameExistService request failure ", true);
            }
            return u;
        }

        public DeleteUserResponse CheckAccoutExecService(Guid UserID)
        {
            ActionLogger.Logger.WriteLog("CheckAccoutExecService request: " + UserID, true);
            DeleteUserResponse u = null;
            try
            {
                User objUser = new User();
                bool isExist = objUser.CheckAccoutExec(UserID);
                u = new DeleteUserResponse("Service success", (int)ResponseCodes.Success,"");
                u.IsDeleted = isExist;
                ActionLogger.Logger.WriteLog("CheckAccoutExecService request success ", true);
            }
            catch (Exception ex)
            {
                u = new DeleteUserResponse("", (int)ResponseCodes.Failure, ex.Message    );
                ActionLogger.Logger.WriteLog("CheckAccoutExecService request failure ", true);
            }
            return u;
        }

        public UserResponse GetUserWithinLicenseeService(Guid userCredencialID, Guid LicenseeID)
        {
            ActionLogger.Logger.WriteLog("GetUserWithinLicenseeService request: " + userCredencialID, true);
            UserResponse u = null;
            try
            {
                User objUser = (new User()).GetUserWithinLicensee(userCredencialID, LicenseeID);
                u = new UserResponse("User found successfully", (int)ResponseCodes.Success,"");
                u.UserObject = objUser;
                ActionLogger.Logger.WriteLog("GetUserWithinLicenseeService request success ", true);
            }
            catch (Exception ex)
            {
                u = new UserResponse("", (int)ResponseCodes.Failure, ex.Message    );
                ActionLogger.Logger.WriteLog("GetUserWithinLicenseeService request failure ", true);
            }
            return u;
        }

        public LinkedUsersListResponse GetAllLinkedUserService(List<User> objUserList, Guid GuidLicID, Guid UserCredentialId, int intRole, bool boolHouseAccount)
        {
            ActionLogger.Logger.WriteLog("GetAllLinkedUserService request UserCredentialId: " + UserCredentialId + ", GuidLicID: " + GuidLicID, true);
            LinkedUsersListResponse jres = null;
            try
            {
                User objUser = new User();
                List<LinkedUser> lst = objUser.GetAllLinkedUser(objUserList, GuidLicID, UserCredentialId, intRole, boolHouseAccount);
                jres = new LinkedUsersListResponse("Users list found successfully", (int)ResponseCodes.Success,"");
                jres.UsersList = lst;
                ActionLogger.Logger.WriteLog("GetAllLinkedUserService success: ", true);
            }
            catch (Exception ex)
            {
                jres = new LinkedUsersListResponse("", (int)ResponseCodes.Failure, "Error getting users list" + ex.Message    );
                ActionLogger.Logger.WriteLog("GetAllLinkedUserService failure: ", true);
            }
            return jres;
        }

        public JSONResponse ImportHouseUsersService(System.Data.DataTable TempTable, Guid LincessID)
        {
            ActionLogger.Logger.WriteLog("ImportHouseUsersService request: " + LincessID, true);
            JSONResponse u = null;
            try
            {
                User objUser = new User();
                objUser.ImportHouseUsers(TempTable, LincessID);
                ActionLogger.Logger.WriteLog("ImportHouseUsersService request success ", true);
            }
            catch (Exception ex)
            {
                u = new JSONResponse("", (int)ResponseCodes.Failure, ex.Message);
                ActionLogger.Logger.WriteLog("ImportHouseUsersService request failure ", true);
            }
            return u;
        }

        public JSONResponse UpdateAccountExecService(Guid userCredencialID, bool bvalue)
        {
            ActionLogger.Logger.WriteLog("UpdateAccountExecService request: " + userCredencialID + ", bVal: " + bvalue, true);
            JSONResponse u = null;
            try
            {
                User objUser = new User();
                objUser.UpdateAccountExec(userCredencialID, bvalue);
                u = new JSONResponse("Status updated successfully", (int)ResponseCodes.Success,"");
                ActionLogger.Logger.WriteLog("GetValidIdentityUsingNameService request success ", true);
            }
            catch (Exception ex)
            {
                u = new JSONResponse("", (int)ResponseCodes.Failure, ex.Message);
                ActionLogger.Logger.WriteLog("GetValidIdentityUsingNameService request failure ", true);
            }
            return u;
        }

    }

}
