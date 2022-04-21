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
using System.Resources;
using System.Net;
using MyAgencyVault.BusinessLibrary.BusinessObjects;

namespace MyAgencyVault.WcfService
{
    [ServiceContract]
    interface IUserService
    {
        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        JSONResponse AddUpdateUserDetails(User userDetails);
        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        JSONResponse AddUpdateUserPermissionsService(User userDetails, Guid userCredentialId);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        JSONResponse AddUpdateAdminStatusService(Guid userCredentialId, bool adminStatus);
        //[OperationContract]
        //[WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        //JSONResponse AddUpdateUserPermissionAndOtherDataService(User Usr);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        DeleteUserResponse DeleteUserInfoService(Guid userCredentialId, bool forceToDelete);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        UsersListResponse GetUsersOnRole(UserRole RoleId);

        //[OperationContract] - Found unused in app
        //[WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        //UsersListResponse GetHouseUsersService(Guid LincessID, int intRoleID, bool IsHouseAccount);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        UsersListResponse GetAllUsersService();

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        UsersListResponse GetUsersByLicenseeService(Guid licenseeId, UserRole roleIdToView);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        UsersListResponse GetUsersForReportsService(Guid LicenseeId);






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
        UserObjectResponse GetUserDetailsService(Guid userCredentialId);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        PolicyStringResponse getUserEmailService(Guid UserID);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        JSONResponse TurnOnNewsToFlashBitService();

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        JSONResponse TurnOffNewsToFlashBitService(Guid UserId);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        DeleteUserResponse IsUserNameExistService(Guid UserID, string UserName);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        JSONResponse SetHouseAccountActive(Guid userCredentialId, Guid licenseeId);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        UsersListResponse GetAllPayeeService();

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        UsersListResponse GetAllPayeeByLicencessIDService(Guid licID);

        //[OperationContract]
        //[WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        //DeleteUserResponse CheckAccoutExecService(Guid userCredencialID);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        JSONResponse ImportHouseUsersService(System.Data.DataTable TempTable, Guid LincessID);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        UsersListResponse GetAccountExecByLicencessIDService(Guid licensssID);

        //[OperationContract]
        //[WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        //UsersCountResponse GetPayeeCountService(Guid licenseeID);

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
        UserObjectResponse GetUserWithinLicenseeService(Guid userCredencialID, Guid LicenseeID);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        LinkedUsersListResponse GetAllLinkedUserService(Guid userCredentialId, ListParams listParams);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        JSONResponse SavedLinkedUserService(List<LinkedUser> linkedUserList, Guid loggedInUserId);

        //[OperationContract]
        //List<LinkedUser> GetSeletedLinkedUser(Guid UserCredentialId, int RoleId, bool isHouseValue);
        //[OperationContract]
        //[WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        //JSONResponse UpdateAccountExecService(Guid userCredencialID, bool bvalue);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        UsersCountResponse GetAccountExecCountService(Guid licenseeID);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        UsersCountResponse GetAllAccountExecCountService();







        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        UserCredentialIdResponse GetUserCredentialId(string userName, Guid licenseeId);



        //[OperationContract]
        //[WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        //JSONResponse GetLicenseeService( int roleId);
        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        LicenseesListResponse GetAllLicenseesService();

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        LocationAutoCompleteResponse GetGoogleLocations(string query, string defaultLocation = "0,0");

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        PolicyStringResponse GetAuthTokenService(string username);

        #region
        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        JSONResponse ForgotUsernameService(string email, string securityQuestion, string securityAnswer);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        UserPasswordHintResponse GetSecurityQuesService(string email);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        JSONResponse AddLoginLogoutTimeService(UserLoginParams loginParams);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        ResetPasswordResponse ResetPasswordService(string password, string passwordKey = "");

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        JSONResponse UpdatePasswordService(Guid userId, string password, string email);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        UserResponse LoginService(string userName, string password);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        JSONResponse RegisterEmailService(string userName, string emailId);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        ValidUserResponse ForgotPasswordService(string userName);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        HasPasswordKeyExpiredResponse HasPasswordKeyExpiredService(string key);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        GetUsersListResponse GetUsersService(Guid? licenseeId, UserRole roleIdToView, ListParams listParams, Guid? loggedInUser);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        GetUsersListResponse GetDataEntryUsersService(UserRole roleIdToView, ListParams listParams);

        #endregion

    }

    public partial class MavService : IUserService, IErrorHandler
    {
        #region IUser Members

        static ResourceManager _resourceManager = new ResourceManager("MyAgencyVault.WcfService.Strings", System.Reflection.Assembly.GetExecutingAssembly());


        #region Forgot username 
        /// <summary>
        /// Author: Jyotisna
        /// Date: Aug 31, 2018
        /// Purpose: to retreive username from the system based on user's emailand security ques/ans
        /// added at the time of registration.
        /// </summary>
        /// <param name="email"></param>
        /// <param name="ques"></param>
        /// <param name="ans"></param>
        /// <returns></returns>
        public JSONResponse ForgotUsernameService(string email, string securityQuestion, string securityAnswer)
        {
            ActionLogger.Logger.WriteLog("ForgotUsernameService request: " + email, true);
            JSONResponse jres = null;
            try
            {
                string username = User.GetUsername(email, securityQuestion, securityAnswer, out string name);
                if (!string.IsNullOrEmpty(username))
                {
                    //send email
                    string emailbody = GetForgotUsernameEmailBody(username, name, email);
                    bool isSent = MailServerDetail.sendMailtodev(email, _resourceManager.GetString("ForgotUsernameMailSubject"), emailbody);
                    if (isSent)
                    {
                        jres = new JSONResponse(_resourceManager.GetString("ForgotUsernameSuccess"), (int)ResponseCodes.Success, "");
                        ActionLogger.Logger.WriteLog("ForgotUsernameService success: username" + username, true);
                    }
                    else
                    {
                        jres = new JSONResponse(_resourceManager.GetString("ForgotPasswordMailSendFail"), (int)ResponseCodes.Failure, "");
                        ActionLogger.Logger.WriteLog("ForgotUsernameService mail sending failed: username" + username, true);
                    }
                }
                else
                {
                    jres = new JSONResponse(_resourceManager.GetString("ForgotUsernameNotFound"), (int)ResponseCodes.RecordNotFound, "");
                    ActionLogger.Logger.WriteLog("ForgotUsernameService not found: username" + username, true);
                }
            }
            catch (Exception ex)
            {
                jres = new JSONResponse(_resourceManager.GetString("ForgotUsernameFailure"), (int)ResponseCodes.Failure, ex.Message);
                ActionLogger.Logger.WriteLog("ForgotUsernameService failure: email" + email + ",  ex: " + ex.Message, true);
            }
            return jres;

        }

        /// <summary>
        /// Author: Jyotisna
        /// Date: Aug 31, 2018
        /// Purpose: To retreive security question in "Forgot username" flow based on user's email
        /// added at the time of registration.
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public UserPasswordHintResponse GetSecurityQuesService(string email)
        {
            ActionLogger.Logger.WriteLog("GetSecurityQuesService request: " + email, true);
            UserPasswordHintResponse jres = null;
            try
            {
                string ques = User.GetSecurityQues(email);
                if (!string.IsNullOrEmpty(ques))
                {
                    jres = new UserPasswordHintResponse(_resourceManager.GetString("GetSecurityQuestionSuccess"), (int)ResponseCodes.Success, "");
                    jres.SecurityQues = ques;
                    ActionLogger.Logger.WriteLog("GetSecurityQuesService success: ques" + ques, true);
                }
                else
                {
                    jres = new UserPasswordHintResponse(_resourceManager.GetString("GetSecurityQuestionNotFound"), (int)ResponseCodes.RecordNotFound, "");
                    ActionLogger.Logger.WriteLog("GetSecurityQuesService not found: ques" + ques, true);
                }
            }
            catch (Exception ex)
            {
                jres = new UserPasswordHintResponse(_resourceManager.GetString("GetSecurityQuestionFailure"), (int)ResponseCodes.Failure, ex.Message);
                ActionLogger.Logger.WriteLog("GetSecurityQuesService failure: email" + email + ",  ex: " + ex.Message, true);
            }
            return jres;

        }
        /// <summary>
        /// Created BY: jyotisna
        /// Created on: Aug 31, 2018
        /// Purpose: To create email body to be sent on"Forgot username" request.
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public static string GetForgotUsernameEmailBody(string username, string name,string email)
        {
            string obj = "";
            try
            {

                Byte[] PageHTMLBytes;
                WebClient MyWebClient = new WebClient();

                string template = System.Configuration.ConfigurationSettings.AppSettings["commDeptForgotUnameUrl"];
                //string webSiteUrl = System.Configuration.ConfigurationSettings.AppSettings["resetPwdUrl"];
                string imageUrl = System.Configuration.ConfigurationSettings.AppSettings["ImageUrl"];
                string siteURL = System.Configuration.ConfigurationSettings.AppSettings["SiteURL"];
                template = System.Web.Hosting.HostingEnvironment.MapPath(template);
                // logger.Info("template path: " + template);

                PageHTMLBytes = MyWebClient.DownloadData(template);
                UTF8Encoding oUTF8 = new UTF8Encoding();
                obj = oUTF8.GetString(PageHTMLBytes);

                if (!string.IsNullOrEmpty(obj))
                {
                    ActionLogger.Logger.WriteLog("GetForgotUsernameEmailBody  template loaded successfully:", true);
                    obj = obj.Replace("#Name#", username);
                    obj = obj.Replace("#name#", name);
                    obj = obj.Replace("#ImageURL#", imageUrl);
                    obj = obj.Replace("#SiteURL#", siteURL);
                    obj = obj.Replace("#Email#", email);
                }
                else
                {
                    ActionLogger.Logger.WriteLog("GetForgotUsernameEmailBody  template is blank :", true);
                    obj = string.Empty;
                }
            }
            catch (Exception ex)
            {
                ActionLogger.Logger.WriteLog("GetForgotUsernameEmailBody  failure :" + ex.Message, true);
                throw ex;
            }
            return obj;
        }
        #endregion
        public UsersListResponse GetAccountExecByLicencessIDService(Guid licensssID)
        {
            ActionLogger.Logger.WriteLog("GetAccountExecByLicencessIDService request: " + licensssID, true);
            UsersListResponse jres = null;
            try
            {
                List<User> lst = User.GetAccountExecByLicencessID(licensssID);
                jres = new UsersListResponse("Users list found successfully", (int)ResponseCodes.Success, "");
                jres.UsersList = lst;
                ActionLogger.Logger.WriteLog("GetAccountExecByLicencessIDService success: " + licensssID, true);
            }
            catch (Exception ex)
            {
                jres = new UsersListResponse("", (int)ResponseCodes.Failure, "Error getting users list" + ex.Message);
                ActionLogger.Logger.WriteLog("GetAccountExecByLicencessIDService failure: " + licensssID, true);
                throw ex;
            }
            return jres;
        }

        public UsersCountResponse GetAccountExecCountService(Guid licensssID)
        {
            ActionLogger.Logger.WriteLog("GetAllAccountExecCountService request: " + licensssID, true);
            UsersCountResponse jres = null;
            try
            {
                int num = User.GetAccountExecCount(licensssID);
                jres = new UsersCountResponse("Users list found successfully", (int)ResponseCodes.Success, "");
                jres.UsersCount = num;
                ActionLogger.Logger.WriteLog("GetAllAccountExecCountService success: ", true);
            }
            catch (Exception ex)
            {
                jres = new UsersCountResponse("", (int)ResponseCodes.Failure, "Error getting users count: " + ex.Message);
                ActionLogger.Logger.WriteLog("GetAllAccountExecCountService failure: " + ex.Message, true);
                throw ex;
            }
            return jres;
        }

        public UsersCountResponse GetAllAccountExecCountService()
        {
            ActionLogger.Logger.WriteLog("GetAllAccountExecCountService request: ", true);
            UsersCountResponse jres = null;
            try
            {
                int num = User.GetAllAccountExecCount();
                jres = new UsersCountResponse("Users list found successfully", (int)ResponseCodes.Success, "");
                jres.UsersCount = num;
                ActionLogger.Logger.WriteLog("GetAllAccountExecCountService success: ", true);
            }
            catch (Exception ex)
            {
                jres = new UsersCountResponse("", (int)ResponseCodes.Failure, "Error getting users count: " + ex.Message);
                ActionLogger.Logger.WriteLog("GetAllAccountExecCountService failure: " + ex.Message, true);
                throw ex;
            }
            return jres;
        }

        //public Guid GetLicenseeUserCredentialIdService(Guid licId)
        //{
        //    return User.GetLicenseeUserCredentialId(licId);
        //}


        /// <summary>
        /// 
        /// </summary>
        /// <param name="Usr"></param>
        /// <returns></returns>
        //public JSONResponse AddUpdateUserPermissionAndOtherDataService(User Usr)
        //{
        //    ActionLogger.Logger.WriteLog("AddUpdateUserPermissionAndOtherDataService request: " + Usr.UserName, true);
        //    JSONResponse jres = null;
        //    try
        //    {
        //        Usr.AddUpdateUserPermission();
        //        jres = new JSONResponse("Data saved successfully", (int)ResponseCodes.Success, "");
        //        ActionLogger.Logger.WriteLog("AddUpdateUserPermissionAndOtherDataService success: " + Usr.UserName, true);
        //    }
        //    catch (Exception ex)
        //    {
        //        jres = new JSONResponse("", (int)ResponseCodes.Failure, "Error saving data");
        //        ActionLogger.Logger.WriteLog("AddUpdateUserPermissionAndOtherDataService failure: " + Usr.UserName, true);
        //    }
        //    return jres;
        //}



        public UsersListResponse GetUsersOnRole(UserRole RoleId)
        {
            ActionLogger.Logger.WriteLog("GetUsersOnRole request: " + RoleId, true);
            UsersListResponse jres = null;
            try
            {
                List<User> lst = User.GetUsers(RoleId);
                jres = new UsersListResponse("Users list found successfully", (int)ResponseCodes.Success, "");
                jres.UsersList = lst;
                ActionLogger.Logger.WriteLog("GetUsersOnRole success: " + RoleId, true);
            }
            catch (Exception ex)
            {
                jres = new UsersListResponse("", (int)ResponseCodes.Failure, "Error getting users list" + ex.Message);
                ActionLogger.Logger.WriteLog("GetUsersOnRole failure: " + RoleId, true);
                throw ex;
            }
            return jres;
        }
        #region Sign in and Forgot password

        /// <summary>
        /// Created BY: jyotisna
        /// Created on: Aug 27, 2018
        /// Purpose: To recover password from the forgot password mail using password key
        /// </summary>
        /// <param name="password"></param>
        /// <param name="passwordKey"></param>
        /// <returns></returns>
        public ResetPasswordResponse ResetPasswordService(string password, string passwordKey)
        {
            ActionLogger.Logger.WriteLog("ResetPasswordService request: " + password + ", passwordKey: " + passwordKey, true);
            ResetPasswordResponse response = null;
            try
            {
                string keySessionExpired = User.ResetPassword(password, passwordKey, out string userName);
                if (keySessionExpired == "0")
                {
                    response = new ResetPasswordResponse(_resourceManager.GetString("ResetPasswordSuccess"), (int)ResponseCodes.Success, "");
                    ActionLogger.Logger.WriteLog("ResetPasswordService success for passwordKey: " + passwordKey, true);
                    response.UsersName = userName;
                }
                else if (keySessionExpired == "1")
                {
                    response = new ResetPasswordResponse(_resourceManager.GetString("PasswordKeyExpired"), (int)ResponseCodes.Success, "");
                    ActionLogger.Logger.WriteLog("ResetPasswordService found passwordKey expired: " + passwordKey, true);
                }
                else
                {
                    response = new ResetPasswordResponse(_resourceManager.GetString("ResetPasswordFailure"), (int)ResponseCodes.Success, "");
                    ActionLogger.Logger.WriteLog("ResetPasswordService - reset failed for passwordKey: " + passwordKey, true);
                }
            }
            catch (Exception ex)
            {
                response = new ResetPasswordResponse(_resourceManager.GetString("ResetPasswordFailure"), (int)ResponseCodes.Failure, ex.Message);
                ActionLogger.Logger.WriteLog("ResetPasswordService exception for passwordKey: " + passwordKey + ", ex: " + ex.Message, true);
                throw ex;
            }
            return response;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="password"></param>
        /// <param name="email"></param>r
        /// <returns></returns>
        public JSONResponse UpdatePasswordService(Guid userId, string password, string email)
        {
            ActionLogger.Logger.WriteLog("UpdatePasswordService request: " + password + ", userID: " + userId + "" + "email:" + email, true);
            JSONResponse response = null;
            try
            {
                User.UpdateEmailAndPassword(userId, password, email, out int result);
                if (result == 1)
                {
                    response = new JSONResponse(_resourceManager.GetString("ResetPasswordSuccess"), (int)ResponseCodes.Success, "");
                    ActionLogger.Logger.WriteLog("UpdatePasswordService success for userID: " + userId, true);
                }
                //else if (result == 2)
                //{
                //    response = new JSONResponse(_resourceManager.GetString("EmailAlreadyExist"), (int)ResponseCodes.EmailAlreadyExist, "");
                //    ActionLogger.Logger.WriteLog("UpdatePasswordService success for userID: " + userId, true);
                //}
                else
                {
                    response = new JSONResponse(_resourceManager.GetString("ResetPasswordParameternotfound"), (int)ResponseCodes.Failure, "");
                    ActionLogger.Logger.WriteLog("UpdatePasswordService parameter not found for userID: " + userId, true);
                }
            }

            catch (Exception ex)
            {
                response = new JSONResponse(_resourceManager.GetString("ResetPasswordFailure"), (int)ResponseCodes.Failure, ex.Message);
                ActionLogger.Logger.WriteLog("UpdatePasswordService failure for userID: " + userId + ", ex: " + ex.Message, true);
                throw ex;
            }
            return response;
        }
        /// <summary>
        /// Created BY: jyotisna
        /// Created on: Aug 27, 2018
        /// Purpose: To authenticate user
        /// </summary>
        /// <param name="userName"></param>Addlogin
        /// <param name="password"></param>
        /// <returns></returns>
        public UserResponse LoginService(string userName, string password)
        {
            ActionLogger.Logger.WriteLog("GetValidIdentityService request: " + userName + ", Password: " + password, true);
            UserResponse response = null;
            try
            {
                User objUser = new User();
                User usr = objUser.GetValidIdentity(userName, password);
                if (usr != null && usr.UserCredentialID != Guid.Empty)
                {
                    userInfo uInfo = new userInfo();
                    uInfo.UserName = usr.UserName;
                    uInfo.IsAdmin = usr.IsAdmin;
                    uInfo.UserCredentialID = usr.UserCredentialID;
                    uInfo.UserClients = usr.UserClients;
                    uInfo.PasswordHintA = usr.PasswordHintA;
                    uInfo.PasswordHintQ = usr.PasswordHintQ;
                    uInfo.Role = usr.Role;
                    uInfo.LicenseeId = usr.LicenseeId;
                    uInfo.LicenseeName = usr.LicenseeName;
                    uInfo.IsUserActiveOnweb = usr.IsUserActiveOnWeb;
                    uInfo.IsAccountExec = usr.IsAccountExec;
                    uInfo.Email = usr.Email;
                    uInfo.LoginName = usr.NickName;
                    uInfo.LicenseeName = usr.Company;
                    uInfo.HouseAccountDetails = usr.HouseAccountdetails;
                    uInfo.IsHouseAccount = usr.IsHouseAccount;
                    uInfo.Permissions = usr.Permissions;

                    response = new UserResponse(_resourceManager.GetString("LoginSuccessMessage"), (int)ResponseCodes.Success, "");
                    response.UserDetails = uInfo;
                    response.UserToken = AuthenticationTokenService.GenerateToken(uInfo.UserName);
                    ActionLogger.Logger.WriteLog("GetValidIdentityService request success ", true);
                }
                else
                {
                    response = new UserResponse(_resourceManager.GetString("LoginUserNotFoundMessage"), (int)ResponseCodes.RecordNotFound, "");
                    ActionLogger.Logger.WriteLog(_resourceManager.GetString("LoginSuccessMessage"), true);
                }
            }
            catch (Exception ex)
            {
                response = new UserResponse(_resourceManager.GetString("LoginFailureMessage"), (int)ResponseCodes.Failure, ex.Message);
                ActionLogger.Logger.WriteLog("GetValidIdentityService request failure ", true);
                throw ex;
            }
            return response;
        }

        /// <summary>
        /// Created By:Ankit khandelwal
        /// Purpose:Used for save the user loggedin details
        /// </summary>
        /// <param name="loginParams"></param>
        /// <returns></returns>
        public JSONResponse AddLoginLogoutTimeService(UserLoginParams loginParams)
        {
            ActionLogger.Logger.WriteLog("AddLoginLogoutTimeService request: " + loginParams.ToStringDump(), true);
            JSONResponse jres = null;
            try
            {
                User.AddLoginLogoutTime(loginParams);
                jres = new JSONResponse("User's login activity saved successfully", (int)ResponseCodes.Success, "");
                ActionLogger.Logger.WriteLog("AddLoginLogoutTimeService success: UserID" + loginParams.UserID, true);
            }
            catch (Exception ex)
            {
                jres = new JSONResponse("", (int)ResponseCodes.Failure, ex.Message);
                ActionLogger.Logger.WriteLog("AddLoginLogoutTimeService failure: UserID" + loginParams.UserID + ",  ex: " + ex.Message, true);
                throw ex;
            }
            return jres;
        }
        /// <summary>
        /// Created By:Ankit
        /// Createdon :22-11-2018
        /// Purpose:Register  an email with username
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="emailId"></param>
        /// <returns></returns>
        public JSONResponse RegisterEmailService(string userName, string emailId)
        {

            JSONResponse jres = null;
            User.RegisterEmailId(userName, emailId, out int isSuccess);
            if (isSuccess == 1)
            {
                jres = new JSONResponse(_resourceManager.GetString("EmailRegisteredSuccess"), (int)ResponseCodes.Success, "");
            }
            else if (isSuccess == 2)
            {
                jres = new JSONResponse(_resourceManager.GetString("EmailAlreadyExist"), (int)ResponseCodes.EmailAlreadyExist, "");
            }
            else
            {
                jres = new JSONResponse(_resourceManager.GetString("EmailRegisteredFailure"), (int)ResponseCodes.Failure, "");
            }

            return jres;
        }

        /// <summary>
        /// Created BY: jyotisna
        /// Created on: Aug 27, 2018
        /// Purpose: To retreive details based on username on "Forgot password" request
        /// </summary>
        /// <param name="UserName"></param>
        /// <returns></returns>
        public ValidUserResponse ForgotPasswordService(string userName)
        {
            ActionLogger.Logger.WriteLog("ForgotPasswordService request: " + userName, true);
            ValidUserResponse response = null;
            try
            {
                User usr = (new User()).GetForgetValidIdentity(userName);

                if (usr == null)
                {
                    response = new ValidUserResponse(_resourceManager.GetString("UsernameNotFoundMessage"), (int)ResponseCodes.RecordNotFound, "");
                }
                else
                {
                    if (!string.IsNullOrEmpty(usr.Email))
                    {
                        ValidUserDetail obj = new ValidUserDetail();
                        obj.UserName = usr.UserName;
                        obj.UserCredentialID = usr.UserCredentialID;
                        obj.Email = usr.Email;

                        string key = User.SaveForgotpasswordKey(usr.UserCredentialID);
                        ActionLogger.Logger.WriteLog("SendForgotPasswordEmail key saved in DB ", true);

                        string emailBody = GetForgotPwdEmailBody(usr.UserName, key);

                        if (string.IsNullOrEmpty(emailBody))
                        {
                            response = new ValidUserResponse(_resourceManager.GetString("ForgotPasswordMailSendFail"), (int)ResponseCodes.Failure, "");
                            ActionLogger.Logger.WriteLog("ForgotPasswordService request success on email:  " + usr.Email, true);
                            return response;
                        }

                        bool mailSent = MailServerDetail.sendMailtodev(usr.Email, _resourceManager.GetString("ForgotPasswordMailSubject"), emailBody);
                        if (mailSent)
                        {
                            response = new ValidUserResponse(_resourceManager.GetString("ForgotPasswordSuccess"), (int)ResponseCodes.Success, "");
                            ActionLogger.Logger.WriteLog("ForgotPasswordService request success on email:  " + usr.Email, true);
                        }
                        else
                        {
                            response = new ValidUserResponse(_resourceManager.GetString("ForgotPasswordMailSendFail"), (int)ResponseCodes.Failure, "");
                            ActionLogger.Logger.WriteLog("ForgotPasswordService request failure on email:  " + usr.Email, true);
                        }
                        response.UserObject = obj;
                        //obj.Role = usr.Role;)
                        //  User.SendForgotPasswordEmail(usr.UserCredentialID, usr.Email, GetForgotPwdEmailBody(usr.UserName,"",""), _resourceManager.GetString("ForgotPasswordMailSubject"));
                        //obj.Role = usr.Role;
                        //obj.Password = usr.Password;
                        //obj.PasswordHintA = usr.PasswordHintA;
                        //obj.PasswordHintQ = usr.PasswordHintQ;
                        //obj.IsHouseAccount = usr.IsHouseAccount;
                        //obj.IsDeleted = usr.IsDeleted;
                        //obj.LicenseeId = usr.LicenseeId;
                        //obj.IsLicenseDeleted = usr.IsLicenseDeleted;

                    }
                    else
                    {
                        response = new ValidUserResponse(_resourceManager.GetString("EmailNotFound"), (int)ResponseCodes.EmailNotFound, string.Empty);
                        ActionLogger.Logger.WriteLog("ForgotPasswordService: email not found  ", true);
                    }
                }
            }
            catch (Exception ex)
            {
                response = new ValidUserResponse(_resourceManager.GetString("ForgotPasswordFailure"), (int)ResponseCodes.Failure, "");
                ActionLogger.Logger.WriteLog("ForgotPasswordService request failure :" + ex.Message, true);
                throw ex;
            }
            return response;
        }
        /// <summary>
        ///  Created BY: Jyotisna
        /// Created on: Aug 28, 2018
        /// Purpose: To check if the specified key in forgot pwd link is expired or not
        /// <param name="key"></param>
        public HasPasswordKeyExpiredResponse HasPasswordKeyExpiredService(string key)
        {
            ActionLogger.Logger.WriteLog("HasPasswordKeyExpired request: " + key, true);
            HasPasswordKeyExpiredResponse response = null;
            try
            {
                string result = User.HasPasswordKeyExpired(key);
                if (result == "0")
                {
                    response = new HasPasswordKeyExpiredResponse(_resourceManager.GetString("PasswordKeyExpired"), (int)ResponseCodes.PwdKeyExpired, string.Empty);
                    ActionLogger.Logger.WriteLog("HasPasswordKeyExpired: key expired  ", true);
                }
                else if (result == "-1")
                {
                    response = new HasPasswordKeyExpiredResponse(_resourceManager.GetString("PasswordKeyNotExist"), (int)ResponseCodes.EmailNotFound, string.Empty);
                    ActionLogger.Logger.WriteLog("HasPasswordKeyExpired: key not found  ", true);
                }
                else
                {

                    response = new HasPasswordKeyExpiredResponse(_resourceManager.GetString("ForgotPasswordSuccess"), (int)ResponseCodes.Success, "");
                    response.Email = result;
                    ActionLogger.Logger.WriteLog("ForgotPasswordService request success ", true);
                }
            }
            catch (Exception ex)
            {
                response = new HasPasswordKeyExpiredResponse(_resourceManager.GetString("ForgotPasswordFailure"), (int)ResponseCodes.Failure, ex.Message);
                ActionLogger.Logger.WriteLog("HasPasswordKeyExpired request failure :" + ex.Message, true);
                throw ex;
            }
            return response;
        }
        /// <summary>
        /// Created BY: jyotisna
        /// Created on: Aug 27, 2018
        /// Purpose: To create email body to be sent on"Forgot password" request.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="passwordResetkey"></param>
        /// <returns></returns>
        public static string GetForgotPwdEmailBody(string name, string passwordResetkey)
        {
            string obj = "";
            try
            {
                Byte[] PageHTMLBytes;
                WebClient MyWebClient = new WebClient();
                string template = System.Configuration.ConfigurationSettings.AppSettings["commDeptForgotPwdUrl"];
                //string webSiteUrl = System.Configuration.ConfigurationSettings.AppSettings["resetPwdUrl"];
                string webSiteUrlPath = System.Configuration.ConfigurationSettings.AppSettings["WebsiteUrl"];
                string imageUrl = System.Configuration.ConfigurationSettings.AppSettings["ImageUrl"];
                string siteURL = System.Configuration.ConfigurationSettings.AppSettings["SiteURL"];
                template = System.Web.Hosting.HostingEnvironment.MapPath(template);
                //webSiteUrlPath = System.Web.Hosting.HostingEnvironment.MapPath(webSiteUrlPath);
                // webSiteUrl = System.Web.Hosting.HostingEnvironment.MapPath(webSiteUrlPath + webSiteUrl);
                ActionLogger.Logger.WriteLog("template path: " + template, true);
                PageHTMLBytes = MyWebClient.DownloadData(template);
                UTF8Encoding oUTF8 = new UTF8Encoding();
                obj = oUTF8.GetString(PageHTMLBytes);

                if (!string.IsNullOrEmpty(obj))
                {
                    ActionLogger.Logger.WriteLog("GetForgotPwdEmailBody  template loaded successfully:", true);
                    obj = obj.Replace("#Name#", name);
                    obj = obj.Replace("#webSiteURL#", webSiteUrlPath);
                    obj = obj.Replace("#Key#", passwordResetkey);
                    obj = obj.Replace("#ImageURL#", imageUrl);
                    obj = obj.Replace("#SiteURL#", siteURL);
                }
                else
                {
                    ActionLogger.Logger.WriteLog("GetForgotPwdEmailBody  template is blank :", true);
                    obj = string.Empty;
                }
            }
            catch (Exception ex)
            {
                ActionLogger.Logger.WriteLog("GetForgotPwdEmailBody  failure :" + ex.Message, true);
                throw ex;
            }
            return obj;
        }

        #endregion

        //<--------------------------------------------------------------------------------------------------------------------------------------------------->
        #region People manager
        //#######################################################################################################################################

        /// <summary>
        /// Created By: Ankit khandelwal
        /// Created on: Sept 04, 2018
        /// Purpose: To get the list of users based on RoleId,pagination,searching,sorting
        /// </summary>
        /// <param name="licenseeId"></param>
        /// <param name="roleIdToView"></param>
        /// <param name="listParams"></param>AddUpdateUserPermissionAndOtherDataService
        /// <returns></returns>       
        public GetUsersListResponse GetUsersService(Guid? licenseeId, UserRole roleIdToView, ListParams listParams, Guid? loggedInUser)
        {
            ActionLogger.Logger.WriteLog("GetUsersService request RoleIdToView: " + roleIdToView + "" + "licenseeId:" + licenseeId + "listParams:" + listParams.ToStringDump(), true);
            GetUsersListResponse response = null;
            try
            {
                List<UserListObject> lst = User.GetUsers(licenseeId, roleIdToView, listParams, out int agnetRecordCount, out int userRecordCount, out int dataEntryRecordCount, out int recordCount, loggedInUser).ToList<UserListObject>();
                if (!string.IsNullOrEmpty(Convert.ToString(agnetRecordCount)) && agnetRecordCount != 0)
                {
                    response = new GetUsersListResponse(_resourceManager.GetString("UserListSuccessMessage"), (int)ResponseCodes.Success, "");
                    response.UsersList = lst;

                    ActionLogger.Logger.WriteLog("GetUsersService success: ", true);
                }
                else
                {
                    response = new GetUsersListResponse(_resourceManager.GetString("userlistNotFoundMessage"), (int)ResponseCodes.RecordNotFound, "");
                    ActionLogger.Logger.WriteLog(_resourceManager.GetString("LoginSuccessMessage"), true);
                }
                response.AgentRecordCount = agnetRecordCount;
                response.UserRecordCount = userRecordCount;
                response.SelectedUserRecordCount = recordCount;
                response.DataEntryUserRecordCount = dataEntryRecordCount;
            }
            catch (Exception ex)
            {
                response = new GetUsersListResponse(_resourceManager.GetString("userlistNotFoundMessage"), (int)ResponseCodes.Failure, "Error getting users list" + "");
                ActionLogger.Logger.WriteLog("GetUsersService failure: " + ex.Message, true);
                throw ex;
            }
            return response;
        }
        public UserCredentialIdResponse GetUserCredentialId(string userName, Guid licenseeId)
        {
            UserCredentialIdResponse response = null;
            string usercredentialid = User.GetUserCredentialId(userName, licenseeId);
            if (!string.IsNullOrEmpty(usercredentialid))
            {
                response = new UserCredentialIdResponse(_resourceManager.GetString("UsercredentialIdFound"), (int)ResponseCodes.Success, "" + "");
                response.usercredentialId = usercredentialid;
            }
            else
            {
                response = new UserCredentialIdResponse(_resourceManager.GetString("UsercredentialIdNotFound"), (int)ResponseCodes.Failure, "" + "");
            }
            return response;
        }
        /// <summary>
        /// Created By: Ankit khandelwal
        /// Created on: Sept 04, 2018
        /// Purpose: To get the Date entry users list  based on RoleId,pagination,searching,sorting
        /// </summary>
        /// <param name="roleIdToView"></param>
        /// <param name="listParams"></param>
        /// <returns></returns>
        public GetUsersListResponse GetDataEntryUsersService(UserRole roleIdToView, ListParams listParams)
        {
            ActionLogger.Logger.WriteLog("GetDataEntryUsersService request RoleIdToView: " + roleIdToView + "" + "listParams:" + listParams.ToStringDump(), true);
            GetUsersListResponse response = null;
            try
            {
                List<DataEntryUserListObject> lst = User.GetDataEntryUsers(roleIdToView, listParams, out int recordCount).ToList<DataEntryUserListObject>();
                if (!string.IsNullOrEmpty(Convert.ToString(recordCount)) && recordCount != 0)
                {
                    response = new GetUsersListResponse(_resourceManager.GetString("DataEntryUserListSuccess"), (int)ResponseCodes.Success, "");
                    response.DataEntryUserRecordCount = recordCount;
                    response.DataEntryUsersList = lst;
                    ActionLogger.Logger.WriteLog("GetUsersService success: ", true);
                }
                else
                {
                    response = new GetUsersListResponse(_resourceManager.GetString("DataEntryUserslistNotFoundMessage"), (int)ResponseCodes.RecordNotFound, "");
                    ActionLogger.Logger.WriteLog(_resourceManager.GetString("LoginSuccessMessage"), true);
                }
            }
            catch (Exception ex)
            {
                response = new GetUsersListResponse(_resourceManager.GetString("DataEntryUserslistNotFoundMessage"), (int)ResponseCodes.Failure, "Error getting users list" + "");
                ActionLogger.Logger.WriteLog("GetUsersService failure: " + ex.Message, true);
                throw ex;
            }
            return response;
        }
        /// <summary>
        /// Created By: Ankit khandelwal
        /// Created on: Sept 05, 2018
        /// Purpose: To update the HouseAccount of user
        /// </summary>
        /// <param name="userCredentialId"></param>
        /// <param name="licenseeId"></param>
        /// <returns></returns>
        public JSONResponse SetHouseAccountActive(Guid userCredentialId, Guid licenseeId)
        {
            ActionLogger.Logger.WriteLog("SetHouseAccountActive request UserCredentialId: " + userCredentialId, true);
            JSONResponse response = null;
            try
            {
                string isSuccess = User.SetHouseAccountValue(userCredentialId, licenseeId);
                if (isSuccess == "True")
                {
                    response = new JSONResponse("House Account update successfully", (int)ResponseCodes.Success, "");
                    ActionLogger.Logger.WriteLog("SetHouseAccountActive success: ", true);
                }
                else
                {
                    response = new JSONResponse(_resourceManager.GetString("HouseAccountNotUpdated"), (int)ResponseCodes.RecordNotFound, "");
                    ActionLogger.Logger.WriteLog(_resourceManager.GetString("LoginSuccessMessage"), true);
                }

            }
            catch (Exception ex)
            {
                response = new JSONResponse("", (int)ResponseCodes.Failure, "Error while updating HoueseAccount" + ex.Message);
                ActionLogger.Logger.WriteLog("SetHouseAccountActive failure: " + ex.Message, true);
                throw ex;
            }
            return response;
        }

        /// <summary>
        /// Created By: Ankit khandelwal
        /// Created on: Sept 05, 2018
        /// Purpose: To Add/Update the User profile Details
        /// </summary>
        /// <param name="userDetails"></param>
        /// <returns></returns>
        public JSONResponse AddUpdateUserDetails(User userDetails)
        {
            ActionLogger.Logger.WriteLog("AddUpdateUserService request: " + userDetails.UserName, true);
            JSONResponse response = null;
            try
            {
                string isUserCredentialIdExist = User.AddUpdateUser(userDetails);
                if (isUserCredentialIdExist == "1")
                {
                    response = new JSONResponse(string.Format(_resourceManager.GetString("UserDetailsUpdateSuccessfully")), (int)ResponseCodes.Success, "");
                    ActionLogger.Logger.WriteLog("AddUpdateUserDetails success: " + userDetails.UserName, true);
                }
                //else if (isUserCredentialIdExist == "2")
                //{
                //    response = new JSONResponse(string.Format(_resourceManager.GetString("EmailUsernameAlreadyExist")), (int)ResponseCodes.UserNameAndEmailExist, "");
                //    ActionLogger.Logger.WriteLog("AddUpdateUserDetails failure: " + userDetails.UserName, true);
                //}
                else if (isUserCredentialIdExist == "0")
                {
                    response = new JSONResponse(string.Format(_resourceManager.GetString("CreateNewUserSuccessfully")), (int)ResponseCodes.Success, "");
                    ActionLogger.Logger.WriteLog("AddUpdateUserDetails success: " + userDetails.UserName, true);
                }
                else if (isUserCredentialIdExist == "4")
                {
                    response = new JSONResponse(_resourceManager.GetString("UserAlreadyExist"), (int)ResponseCodes.UserAlreadyExist, "");
                    ActionLogger.Logger.WriteLog("AddUpdateUserDetails Failure: " + userDetails.UserName, true);
                }
                //else if (isUserCredentialIdExist == "3")
                //{
                //    response = new JSONResponse(_resourceManager.GetString("EmailAlreadyExist"), (int)ResponseCodes.EmailAlreadyExist, "");
                //    ActionLogger.Logger.WriteLog("AddUpdateUserDetails Failure: " + userDetails.UserName, true);
                //}
                else
                {
                    response = new JSONResponse(_resourceManager.GetString("CreateUserFailure"), (int)ResponseCodes.Failure, "Error saving user");
                    ActionLogger.Logger.WriteLog("AddUpdateUserDetails failure: " + userDetails.UserName, true);
                }

            }
            catch (Exception ex)
            {
                response = new JSONResponse(_resourceManager.GetString("CreateUserFailure"), (int)ResponseCodes.Failure, "Error saving user");
                ActionLogger.Logger.WriteLog("AddUpdateUserDetails failure: " + ex.Message, true);
                if (ex.InnerException != null)
                {
                    ActionLogger.Logger.WriteLog(" AddupdateUserInner ex: " + ex.InnerException.ToStringDump(), true);
                }
                throw ex;
            }
            return response;
        }

        /// <summary>
        /// Created By: Ankit khandelwal
        /// Created on: Sept 05, 2018
        /// Purpose: To Add/Update the User Permissions Details
        /// </summary>
        /// <param name="permission"></param>
        /// <param name="userCredentialId"></param>
        /// <returns></returns>
        public JSONResponse AddUpdateUserPermissionsService(User userDetails, Guid userCredentialId)
        {
            ActionLogger.Logger.WriteLog("AddUpdateUserService request: " + userCredentialId, true);
            JSONResponse response = null;
            try
            {
                User.AddUpdateUserPermission(userDetails, userCredentialId, out string isvalidUsercredntialId);
                if (isvalidUsercredntialId == "1")
                {
                    response = new JSONResponse(_resourceManager.GetString("UserSettingSuccessMessage"), (int)ResponseCodes.Success, "");
                    ActionLogger.Logger.WriteLog("AddUpdateUserService success: " + userCredentialId, true);
                }
                else
                {
                    response = new JSONResponse(_resourceManager.GetString("UserSettingFailureMessage"), (int)ResponseCodes.Success, "");
                    ActionLogger.Logger.WriteLog("AddUpdateUserService fails due to usercredntial not matched: " + userCredentialId, true);
                }
            }
            catch (Exception ex)
            {
                response = new JSONResponse(_resourceManager.GetString("UserSettingFailureMessage"), (int)ResponseCodes.Failure, "");
                ActionLogger.Logger.WriteLog("AddUpdateUserService failure: " + ex.Message, true);
                throw ex;
            }
            return response;
        }
        /// <summary>
        /// Created By: Ankit khandelwal
        /// Created on: Sept 10, 2018
        /// Purpose: To Add/Update User mapping list 
        /// </summary>
        /// <param name="linkedUserList"></param>
        /// <param name="loggedInUserId"></param>
        /// <returns></returns>
        public JSONResponse SavedLinkedUserService(List<LinkedUser> linkedUserList, Guid loggedInUserId)
        {
            ActionLogger.Logger.WriteLog("SavedLinkedUserService request: " + loggedInUserId, true);
            JSONResponse response = null;
            try
            {
                User.SaveLinkedUsers(linkedUserList, loggedInUserId);
                response = new JSONResponse(_resourceManager.GetString("UserLinkedMessage"), (int)ResponseCodes.Success, "");
                ActionLogger.Logger.WriteLog("SavedLinkedUserService success: " + loggedInUserId, true);
            }
            catch (Exception ex)
            {
                response = new JSONResponse(_resourceManager.GetString("UsersLinkedFailure"), (int)ResponseCodes.Failure, ex.Message);
                ActionLogger.Logger.WriteLog("SavedLinkedUserService failure: " + ex.Message, true);
                throw ex;
            }
            return response;
        }

        /// <summary>
        /// Created By: Ankit khandelwal
        /// Created on: Sept 05, 2018
        /// Purpose: To Get  the User Details based on userCredentialId
        /// </summary>
        /// <param name="userCredentialId"></param>
        /// <returns></returns>
        public UserObjectResponse GetUserDetailsService(Guid userCredentialId)
        {
            ActionLogger.Logger.WriteLog("GetUserDetails request: " + userCredentialId, true);
            UserObjectResponse response = null;
            try
            {

                User usr = User.GetUserDetailIdWise(userCredentialId);
                if (usr != null)
                {

                    response = new UserObjectResponse("User Detail found successfully", (int)ResponseCodes.Success, "");
                    response.UserObject = usr;
                    //response.UserObject = usr;
                    ActionLogger.Logger.WriteLog("GetUserDetails request success ", true);
                }
                else
                {
                    response = new UserObjectResponse(_resourceManager.GetString("UserDetailNotFoundMessage"), (int)ResponseCodes.RecordNotFound, "");
                    ActionLogger.Logger.WriteLog(_resourceManager.GetString("LoginSuccessMessage"), true);
                }

            }
            catch (Exception ex)
            {
                response = new UserObjectResponse("", (int)ResponseCodes.Failure, ex.Message);
                ActionLogger.Logger.WriteLog("GetUserDetails request failure " + ex.Message, true);
                throw ex;
            }
            return response;
        }


        /// <summary>
        /// Created By: Ankit khandelwal
        /// Created on: Sept 10, 2018 
        /// Purpose: To Get  the linked User list based on userCredentialId
        /// </summary>
        /// <param name="userCredentialId"></param>
        /// <param name="listparam"></param>
        /// <returns></returns>
        public LinkedUsersListResponse GetAllLinkedUserService(Guid userCredentialId, ListParams listParams)
        {
            ActionLogger.Logger.WriteLog("GetAllLinkedUserService request UserCredentialId: " + userCredentialId + ", userCredentialId: " + userCredentialId, true);
            LinkedUsersListResponse response = null;
            try
            {
                User objUser = new User();
                List<LinkedUser> lst = objUser.GetAllLinkedUser(userCredentialId, listParams, out int recordCount);
                response = new LinkedUsersListResponse("Users list found successfully", (int)ResponseCodes.Success, "");
                response.UsersList = lst;
                response.RecordCount = recordCount;
                ActionLogger.Logger.WriteLog("GetAllLinkedUserService success: ", true);
            }
            catch (Exception ex)
            {
                response = new LinkedUsersListResponse("", (int)ResponseCodes.Failure, "Error getting users list" + ex.Message);
                ActionLogger.Logger.WriteLog("GetAllLinkedUserService failure: " + ex.Message, true);
                throw ex;
            }
            return response;
        }
        /// <summary>
        ///Created By: Ankit khandelwal
        /// Created on: Sept 10, 2018 
        /// Purpose: To delete User based on userCredentialId
        /// </summary>
        /// <param name="userCredentialId"></param>
        /// <returns></returns>
        public DeleteUserResponse DeleteUserInfoService(Guid userCredentialId, bool forceToDelete = false)
        {
            ActionLogger.Logger.WriteLog("DeleteUserInfoService request: " + "userCredentialId:" + userCredentialId, true);
            DeleteUserResponse jres = null;
            try
            {
                bool isDeleted = User.Delete(userCredentialId, forceToDelete, out int isdeleteSuccess);

                if (isDeleted == true && isdeleteSuccess == 1 && forceToDelete == true)
                {
                    jres = new DeleteUserResponse(_resourceManager.GetString("UserDeletedSuccessMessage"), (int)ResponseCodes.Success, "");
                    jres.IsDeleteSuccess = isdeleteSuccess;
                    jres.IsDeleted = isDeleted;
                    ActionLogger.Logger.WriteLog("DeleteUserInfoService success: " + "userCredentialId:" + userCredentialId, true);
                }
                else if (isDeleted == false && isdeleteSuccess == 2 && forceToDelete == true)
                {
                    jres = new DeleteUserResponse(_resourceManager.GetString("DeletedUserNotFound"), (int)ResponseCodes.Success, "");
                    jres.IsDeleteSuccess = isdeleteSuccess;
                    jres.IsDeleted = isDeleted;
                    ActionLogger.Logger.WriteLog("DeleteUserInfoService success: " + "userCredentialId:" + userCredentialId, true);
                }

                else if (isDeleted == false && isdeleteSuccess == 0)
                {
                    jres = new DeleteUserResponse(_resourceManager.GetString("UserHasPaymentEntries"), (int)ResponseCodes.Success, "");
                    jres.IsDeleteSuccess = isdeleteSuccess;
                    jres.IsDeleted = isDeleted;

                    ActionLogger.Logger.WriteLog("DeleteUserInfoService failureAgent cannot be deleted as there are either payment entries or issues associated to his policies in the system. : " + "userCredentialId:" + userCredentialId, true);
                }
                else if (isDeleted == false && isdeleteSuccess == 3)
                {
                    jres = new DeleteUserResponse(_resourceManager.GetString("UserHasNotPaymentEntries"), (int)ResponseCodes.Success, "");
                    jres.IsDeleted = isDeleted;
                    jres.IsDeleteSuccess = isdeleteSuccess;
                    ActionLogger.Logger.WriteLog("DeleteUserInfoService failureAgent cannot be deleted as there are either payment entries or issues associated to his policies in the system. : " + "userCredentialId:" + userCredentialId, true);
                }
                else
                {
                    jres = new DeleteUserResponse(_resourceManager.GetString("UserDeletedFailureMessage"), (int)ResponseCodes.Failure, "");
                    jres.IsDeleted = isDeleted;
                    jres.IsDeleteSuccess = isdeleteSuccess;
                    ActionLogger.Logger.WriteLog("DeleteUserInfoService success: " + "userCredentialId:" + userCredentialId, true);
                }
            }
            catch (Exception ex)
            {
                jres = new DeleteUserResponse(_resourceManager.GetString("UserDeletedFailureMessage"), (int)ResponseCodes.Failure, "Error deleting user" + ex.Message);
                ActionLogger.Logger.WriteLog("DeleteUserInfoService failure: " + "userCredentialId:" + userCredentialId + ", ex: " + ex.Message, true);
                throw ex;
            }
            return jres;
        }

        /// <summary>
        /// Created By: Ankit khandelwal
        /// Created on: Jan 01, 2019
        /// Purpose: To Add/Update AdminStatus and Rights of Admin /// </summary>
        /// <param name="userCredentialId"></param>
        /// <param name="adminStatus"></param>
        /// <returns></returns>
        public JSONResponse AddUpdateAdminStatusService(Guid userCredentialId, bool adminStatus)
        {
            ActionLogger.Logger.WriteLog("AddUpdateAdminStatusService request strat for processing:" + "userCredentialId" + userCredentialId, true);
            JSONResponse response = null;
            try
            {
                int success = User.AddUpdateAdminStatus(userCredentialId, adminStatus);
                if (success == 1)
                {
                    response = new JSONResponse(_resourceManager.GetString("AddUpdateAdminStatus"), (int)ResponseCodes.Success, " ");
                }
                else
                {
                    response = new JSONResponse(_resourceManager.GetString("AddUpdateAdminFailure"), (int)ResponseCodes.Success, " ");
                }
            }
            catch (Exception ex)
            {
                response = new JSONResponse(_resourceManager.GetString("AddUpdateAdminFailure"), (int)ResponseCodes.Failure, " ");
                ActionLogger.Logger.WriteLog("AddUpdateAdminStatusService:Exception occur while updating:" + "userCredentialId" + userCredentialId + "Exception:" + ex.Message, true);
            }
            return response;
        }
        //###############################################################################################################################
        #endregion
        //<--------------------------------------------------------------------------------------------------------------------------------------------------->



        public LicenseesListResponse GetAllLicenseesService()
        {
            ActionLogger.Logger.WriteLog("GetAllLicenseesService request: ", true);
            LicenseesListResponse response = null;
            try
            {
                List<LicenseeListDetail> lst = User.GetAllLicenseesList();
                response = new LicenseesListResponse("Users list found successfully", (int)ResponseCodes.Success, "");
                response.licenseeList = lst;
                ActionLogger.Logger.WriteLog("GetCurrentPermissionService success: ", true);
            }
            catch (Exception ex)
            {
                ActionLogger.Logger.WriteLog("GetAllLicenseesService exception: " + ex.Message, true);
                throw ex;
            }
            return response;

        }
        public UsersCountResponse GetAllPayeeCountService()
        {
            ActionLogger.Logger.WriteLog("GetAllPayeeCountService request: ", true);
            UsersCountResponse jres = null;
            try
            {
                User objUser = new User();
                int num = objUser.GetAllPayeeCount();
                jres = new UsersCountResponse("Users list found successfully", (int)ResponseCodes.Success, "");
                jres.UsersCount = num;
                ActionLogger.Logger.WriteLog("GetAllPayeeCountService success: ", true);
            }
            catch (Exception ex)
            {
                jres = new UsersCountResponse("", (int)ResponseCodes.Failure, "Error getting users count: " + ex.Message);
                ActionLogger.Logger.WriteLog("GetAllPayeeCountService failure: " + ex.Message, true);
                throw ex;
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
                jres = new UsersPermissionResponse("Users list found successfully", (int)ResponseCodes.Success, "");
                jres.UsersList = lst;
                ActionLogger.Logger.WriteLog("GetCurrentPermissionService success: ", true);
            }
            catch (Exception ex)
            {
                jres = new UsersPermissionResponse("", (int)ResponseCodes.Failure, "Error getting users list" + ex.Message);
                ActionLogger.Logger.WriteLog("GetCurrentPermissionService failure: " + ex.Message, true);
                throw ex;
            }
            return jres;

        }

        public UsersListResponse GetAllUsersService()
        {
            ActionLogger.Logger.WriteLog("GetAllUsersService request: ", true);
            UsersListResponse jres = null;
            try
            {
                List<User> lst = User.GetAllUsers();
                jres = new UsersListResponse("Users list found successfully", (int)ResponseCodes.Success, "");
                jres.UsersList = lst;
                ActionLogger.Logger.WriteLog("GetAllUsersService success: ", true);
            }
            catch (Exception ex)
            {
                jres = new UsersListResponse("", (int)ResponseCodes.Failure, "Error getting users list" + ex.Message);
                ActionLogger.Logger.WriteLog("GetAllUsersService failure: " + ex.Message, true);
                throw ex;
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
                jres = new UsersListResponse("Users list found successfully", (int)ResponseCodes.Success, "");
                jres.UsersList = lst;
                ActionLogger.Logger.WriteLog("GetAllPayeeService success: ", true);
            }
            catch (Exception ex)
            {
                jres = new UsersListResponse("", (int)ResponseCodes.Failure, "Error getting users list" + ex.Message);
                ActionLogger.Logger.WriteLog("GetAllPayeeService failure: " + ex.Message, true);
                throw ex;
            }
            return jres;
        }




        public UsersListResponse GetAllPayeeByLicencessIDService(Guid licID)
        {
            ActionLogger.Logger.WriteLog("GetAllPayeeByLicencessIDService request: ", true);
            UsersListResponse jres = null;
            try
            {
                List<User> lst = User.GetAllPayeeByLicencessID(licID);
                jres = new UsersListResponse("Users list found sucscessfully", (int)ResponseCodes.Success, "");
                jres.UsersList = lst;
                ActionLogger.Logger.WriteLog("GetAllPayeeByLicencessIDService success: ", true);
            }
            catch (Exception ex)
            {
                jres = new UsersListResponse("", (int)ResponseCodes.Failure, "Error getting users list" + ex.Message);
                ActionLogger.Logger.WriteLog("GetAllPayeeByLicencessIDService failure: " + ex.Message, true);
                throw ex;
            }
            return jres;
        }

        /// <summary>
        /// Modified By :ankit kahndelwal
        /// ModifiedOn:11-04-2019
        /// Purpose:getting list of user based on LicenseeId
        /// </summary>
        /// <param name="licenseeId"></param>
        /// <param name="roleIdToView"></param>
        /// <returns></returns>
        public UsersListResponse GetUsersByLicenseeService(Guid licenseeId, UserRole roleIdToView)
        {
            ActionLogger.Logger.WriteLog("GetUsersByLicenseeService request: LicID: " + licenseeId, true);
            UsersListResponse jres = null;
            try
            {
                IEnumerable<User> lst = User.GetUsers(licenseeId, roleIdToView);
                jres = new UsersListResponse("Users list found successfully", (int)ResponseCodes.Success, "");
                jres.UsersList = lst.ToList<User>();
                ActionLogger.Logger.WriteLog("GetUsersByLicenseeService success: ", true);
            }
            catch (Exception ex)
            {
                jres = new UsersListResponse("", (int)ResponseCodes.Failure, "Error getting users list" + ex.Message);
                ActionLogger.Logger.WriteLog("GetUsersByLicenseeService failure: " + ex.Message, true);
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
                jres = new UsersListResponse("Users list found successfully", (int)ResponseCodes.Success, "");
                jres.UsersList = lst.ToList<User>();
                ActionLogger.Logger.WriteLog("GetUsersForReportsService success: ", true);
            }
            catch (Exception ex)
            {
                jres = new UsersListResponse("", (int)ResponseCodes.Failure, "Error getting users list" + ex.Message);
                ActionLogger.Logger.WriteLog("GetUsersForReportsService failure: " + ex.Message, true);
                throw ex;
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
                jres = new UsersListResponse("Users list found successfully", (int)ResponseCodes.Success, "");
                jres.UsersList = lst.ToList<User>();
                ActionLogger.Logger.WriteLog("GetAllUsersByChunckService success: ", true);
            }
            catch (Exception ex)
            {
                jres = new UsersListResponse("", (int)ResponseCodes.Failure, "Error getting users list" + ex.Message);
                ActionLogger.Logger.WriteLog("GetAllUsersByChunckService failure: " + ex.Message, true);
                throw ex;
            }
            return jres;
        }


        public UsersListResponse GetAllUsersByChunckService(int skip, int take)
        {
            ActionLogger.Logger.WriteLog("GetAllUsersByChunckService request: skip" + skip + ", take: " + take, true);
            UsersListResponse jres = null;
            try
            {
                IEnumerable<User> lst = User.GetAllUsersByChunck(skip, take);
                jres = new UsersListResponse("Users list found successfully", (int)ResponseCodes.Success, "");
                jres.UsersList = lst.ToList<User>();
                ActionLogger.Logger.WriteLog("GetAllUsersByChunckService success: ", true);
            }
            catch (Exception ex)
            {
                jres = new UsersListResponse("", (int)ResponseCodes.Failure, "Error getting users list" + ex.Message);
                ActionLogger.Logger.WriteLog("GetAllUsersByChunckService failure: " + ex.Message, true);
                throw ex;
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
                ActionLogger.Logger.WriteLog("HouseAccoutTransferProcessService failure: " + ex.Message, true);
                throw ex;
            }
            return res;
        }

        #endregion

        #region IUser Members

        public PolicyStringResponse getUserEmailService(Guid UserID)
        {
            ActionLogger.Logger.WriteLog("getUserEmailService request: " + UserID, true);
            PolicyStringResponse u = null;
            try
            {
                string s = User.getUserEmail(UserID);
                if (!string.IsNullOrEmpty(s))
                {
                    u = new PolicyStringResponse("User email found successfully", (int)ResponseCodes.Success, "");
                    u.StringValue = s;
                    ActionLogger.Logger.WriteLog("getUserEmailService request success ", true);
                }
                else
                {
                    u = new PolicyStringResponse("User email could not be found", (int)ResponseCodes.Success, "");
                    ActionLogger.Logger.WriteLog("getUserEmailService request success ", true);
                }
            }
            catch (Exception ex)
            {
                u = new PolicyStringResponse("", (int)ResponseCodes.Failure, ex.Message);
                ActionLogger.Logger.WriteLog("getUserEmailService request failure ", true);
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
                throw ex;
            }
            return u;
        }

        public JSONResponse TurnOffNewsToFlashBitService(Guid userId)
        {
            ActionLogger.Logger.WriteLog("TurnOffNewsToFlashBitService request: " + userId, true);
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
                throw ex;
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
                u = new DeleteUserResponse("Service success", (int)ResponseCodes.Success, "");
                u.IsDeleted = isExist;
                ActionLogger.Logger.WriteLog("IsUserNameExistService request success ", true);
            }
            catch (Exception ex)
            {
                u = new DeleteUserResponse("", (int)ResponseCodes.Failure, ex.Message);
                ActionLogger.Logger.WriteLog("IsUserNameExistService request failure " + ex.Message, true);
                throw ex;
            }
            return u;
        }


        public UserObjectResponse GetUserWithinLicenseeService(Guid userCredencialID, Guid LicenseeID)
        {
            ActionLogger.Logger.WriteLog("GetUserWithinLicenseeService request: " + userCredencialID, true);
            UserObjectResponse u = null;
            try
            {
                User objUser = (new User()).GetUserWithinLicensee(userCredencialID, LicenseeID);
                u = new UserObjectResponse("User found successfully", (int)ResponseCodes.Success, "");
                u.UserObject = objUser;
                ActionLogger.Logger.WriteLog("GetUserWithinLicenseeService request success ", true);
            }
            catch (Exception ex)
            {
                u = new UserObjectResponse("", (int)ResponseCodes.Failure, ex.Message);
                ActionLogger.Logger.WriteLog("GetUserWithinLicenseeService request failure " + ex.Message, true);
                throw ex;
            }
            return u;
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
                ActionLogger.Logger.WriteLog("ImportHouseUsersService request failure " + ex.Message, true);
                throw ex;
            }
            return u;
        }

        public LocationAutoCompleteResponse GetGoogleLocations(string query, string defaultLocation = "0,0")
        {
            ActionLogger.Logger.WriteLog("GetGoogleLocations query: " + query, true);
            LocationAutoCompleteResponse response = null;
            try
            {
                string status = string.Empty;
                List<string> lstLocations = User.GetGoogleLocation(query, out status, defaultLocation);
                response = new LocationAutoCompleteResponse("Locations found successfully", (int)ResponseCodes.Success, "");
                response.Locations = lstLocations;
                ActionLogger.Logger.WriteLog("GetGoogleLocations request success ", true);
            }
            catch (Exception ex)
            {
                response = new LocationAutoCompleteResponse("", (int)ResponseCodes.Failure, ex.Message);
                ActionLogger.Logger.WriteLog("GetGoogleLocations request failure " + ex.Message, true);
            }
            return response;
        }

        public PolicyStringResponse GetAuthTokenService(string username)
        {
            ActionLogger.Logger.WriteLog("GetAuthToken username: " + username, true);
            PolicyStringResponse response = null;
            try
            {
                string token = AuthenticationTokenService.GenerateToken(username);
                response = new PolicyStringResponse("Token generated successfully", (int)ResponseCodes.Success, "");
                response.StringValue = token;
                ActionLogger.Logger.WriteLog("GetAuthToken request success ", true);
            }
            catch (Exception ex)
            {
                response = new PolicyStringResponse("", (int)ResponseCodes.Failure, ex.Message);
                ActionLogger.Logger.WriteLog("GetGoogleLocations request failure " + ex.Message, true);
                throw ex;
            }
            return response;
        }
    }
}