using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyAgencyVault.BusinessLibrary.Base;
using System.Runtime.Serialization;
using DLinq = DataAccessLayer.LinqtoEntity;
using MyAgencyVault.BusinessLibrary.Masters;
using System.Data.EntityClient;
using System.Data.SqlClient;
using System.Data;
using System.Transactions;
using System.IO;
using System.Threading;
using System.Net;
using Acme.Services.DTO;
using Acme.Services.GoogleLocationSearch;

namespace MyAgencyVault.BusinessLibrary
{
    [DataContract]
    public class User : UserDetail
    {
        #region  "Data members i.e. public properties"
        [DataMember]
        public String UserName { get; set; }


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
        ////[DataMember]
        public bool IsDeleted { get; set; }
        [DataMember]
        public bool? IsLicenseDeleted { get; set; }
        [DataMember]
        public Guid? LicenseeId { get; set; }
        [DataMember]
        public Guid? CreatedBy { get; set; }
        [DataMember]
        public DateTime? CreatedOn { get; set; }
        [DataMember]
        public Guid? ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        [DataMember]
        public bool? IsUserActiveOnWeb { get; set; }

        [DataMember]
        public bool? IsAdmin { get; set; }

        [DataMember]
        public bool? HasAssociatedPolicies { get; set; }

        //acme - Comented as no reference found

        //public DateTime CreateOn { get; set; }
        DateTime _CreateOn;
        [DataMember(IsRequired = false, EmitDefaultValue = false)]
        public DateTime CreateOn
        {
            get
            {
                return _CreateOn;
            }
            set
            {
                _CreateOn = value;
                if (value != null && string.IsNullOrEmpty(CreateOnstring))
                {
                    CreateOnstring = value.ToString();
                }
            }
        }
        string _CreateOnString;
        [DataMember]
        public string CreateOnstring
        {
            get
            {
                return _CreateOnString;
            }
            set
            {
                _CreateOnString = value;
                if ((CreateOn == null || CreateOn == DateTime.MinValue) && !string.IsNullOrEmpty(_CreateOnString))
                {
                    DateTime dt;
                    DateTime.TryParse(_CreateOnString, out dt);
                    CreateOn = dt;
                }
            }
        }

        //DateTime _ModifiedOn;
        //[DataMember(IsRequired = false, EmitDefaultValue = false)]
        //public DateTime ModifiedOn
        //{
        //    get
        //    {
        //        return _ModifiedOn;
        //    }
        //    set
        //    {
        //        _ModifiedOn = value;
        //        if (value != null && string.IsNullOrEmpty(ModifiedOnstring))
        //        {
        //            ModifiedOnstring = value.ToString();
        //        }
        //    }
        //}
        //string _ModifiedOnString;
        //public string ModifiedOnstring
        //{
        //    get
        //    {
        //        return _ModifiedOnString;
        //    }
        //    set
        //    {
        //        _ModifiedOnString = value;
        //        if ((ModifiedOn == null || ModifiedOn == DateTime.MinValue) && !string.IsNullOrEmpty(_ModifiedOnString))
        //        {
        //            DateTime dt;
        //            DateTime.TryParse(_ModifiedOnString, out dt);
        //            ModifiedOn = dt;
        //        }
        //    }
        //}

        private UserLicenseeDetail _userlicenseeDetail;
        [DataMember]
        public UserLicenseeDetail UserlicenseeDetails { get; set; }

        [DataMember]
        public string LicenseeName { get; set; }
        [DataMember]
        public List<Client> UserClients { get; set; }
        [DataMember]
        public UserRole Role { get; set; }
        [DataMember]
        public Guid AttachedToLicensee { get; set; }
        [IgnoreDataMember]
        private List<UserPermissions> _permissions;
        [DataMember]
        public List<UserPermissions> Permissions
        {
            get
            {
                try
                {
                    //if (this.Role == UserRole.SuperAdmin)
                    //{
                    //    _permissions = GetSuperPermissions();
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
                catch
                {
                    return new List<UserPermissions>();
                }
            }
            set
            {
                _permissions = value;
            }
        }

        /// all the users to which the current logged in user have the accessibility to see their data.
        /// </summary> 
        [IgnoreDataMember]
        private List<LinkedUser> _linkedusers;
        [DataMember]
        public List<LinkedUser> LinkedUsers { get; set; }
        [DataMember]
        public bool IsNewsToFlash { get; set; }
        [DataMember]
        public Guid? HouseOwnerId { get; set; }
        [DataMember]
        public Guid? AdminId { get; set; }
        [DataMember]
        public string WebDavPath { get; set; }
        public static object HttpContext { get; private set; }

        public HouseAccountDetails HouseAccountdetails { get; set; }



        #endregion
        ///Ankit
        #region IEditable<User> Members
        //to do: need to add a addupdate/delete function for user detail separately.
        /// <summary>   
        /// to do: need to do save for user permission included in this code itself.
        /// </summary>
        /// 
        public static bool Delete(Guid userCredentialId, bool forceTodelete, out int isdeleteSuccess)
        {
            using (DLinq.CommissionDepartmentEntities DataModel = Entity.DataModel)
            {
                try
                {
                    ActionLogger.Logger.WriteLog(DateTime.Now.ToString() + " Delete user request, userId: " + userCredentialId, true);
                    int associtedPolicyCount = DataModel.Policies.Where(s => s.CreatedBy == userCredentialId && s.IsDeleted == false).Count();
                    ActionLogger.Logger.WriteLog(DateTime.Now.ToString() + " Delete user request, associtedPolicyCount: " + associtedPolicyCount, true);
                    bool UserPresentAsPayee = OutGoingPayment.IsUserPresentAsPayee(userCredentialId) | OutgoingSchedule.IsUserPresentAsPayee(userCredentialId);
                    ActionLogger.Logger.WriteLog(DateTime.Now.ToString() + " Delete user request, UserPresentAsPayee: " + UserPresentAsPayee, true);
                    if (associtedPolicyCount != 0 || UserPresentAsPayee)
                    {
                        ActionLogger.Logger.WriteLog(DateTime.Now.ToString() + " Delete user request, cannot be deleted, returning.. ", true);
                        isdeleteSuccess = 0;
                        return false;
                    }
                    else if (forceTodelete == true)
                    {
                        var _dtUsers = (from u in DataModel.UserCredentials
                                        where (u.UserCredentialId == userCredentialId)
                                        select u).FirstOrDefault();
                        if (_dtUsers != null)
                        {
                            _dtUsers.IsDeleted = true;
                            isdeleteSuccess = 1;
                            DataModel.SaveChanges();
                            ActionLogger.Logger.WriteLog(DateTime.Now.ToString() + " Delete user request, user succesfully deleted: ", true);
                            return true;
                        }
                        else
                        {
                            ActionLogger.Logger.WriteLog(DateTime.Now.ToString() + " Delete user request, user not found: ", true);
                            isdeleteSuccess = 2;
                            return false;

                        }
                    }
                    else
                    {
                        ActionLogger.Logger.WriteLog(DateTime.Now.ToString() + " Delete user request, not forcetodelete: ", true);
                        isdeleteSuccess = 3;
                        return false;
                    }

                }
                catch (Exception ex)
                {
                    isdeleteSuccess = 4;
                    return false;
                }
            }
        }




        public bool CheckAccoutExec(Guid userCredencialID, bool bvalue)
        {
            bool bValue = false;
            try
            {
                using (DLinq.CommissionDepartmentEntities DataModel = Entity.DataModel)
                {
                    DLinq.Policy objPolicy = DataModel.Policies.FirstOrDefault(s => s.UserCredentialId == userCredencialID);

                    if (objPolicy != null)
                    {
                        bValue = true;
                    }
                }
            }
            catch
            {
            }

            return bValue;
        }



        #endregion

        public User GetUserWithinLicensee(Guid userCredencialID, Guid LicenseeID)
        {
            User objPolicy = new User();
            try
            {
                using (DLinq.CommissionDepartmentEntities DataModel = Entity.DataModel)
                {
                    objPolicy = (from uc in DataModel.UserCredentials
                                 where (uc.UserCredentialId == userCredencialID) && (uc.IsDeleted == false) && (uc.LicenseeId == LicenseeID)
                                 select new User
                                 {
                                     UserCredentialID = uc.UserCredentialId

                                 }
                              ).FirstOrDefault();
                }

            }
            catch (Exception ex)
            {
                ActionLogger.Logger.WriteLog("GetUserWithinLicensee exception: " + ex.Message, true);
            }

            return objPolicy;
        }
        public static string GetUserCredentialId(string userName, Guid licenseeId)
        {
            string UsercredentialId = "";
            try
            {
                DLinq.CommissionDepartmentEntities ctx = new DLinq.CommissionDepartmentEntities(); //create your entity object here
                EntityConnection ec = (EntityConnection)ctx.Connection;
                SqlConnection sc = (SqlConnection)ec.StoreConnection; //get the SQLConnection that your entity object would use
                string adoConnStr = sc.ConnectionString;
                using (SqlConnection con = new SqlConnection(adoConnStr))
                {
                    using (SqlCommand cmd = new SqlCommand("usp_getUsercredentialId", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@userName", userName);
                        cmd.Parameters.AddWithValue("@licenseeId", licenseeId);
                        con.Open();
                        UsercredentialId = Convert.ToString((cmd.ExecuteScalar()));
                    }
                }

            }
            catch (Exception ex)
            {
                ActionLogger.Logger.WriteLog("GetUserWithinLicensee exception: " + ex.Message, true);
            }

            return UsercredentialId;
        }

        //public bool CheckAccoutExec(Guid userCredencialID)
        //{
        //    ActionLogger.Logger.WriteLog(" CheckAccoutExec , user:  " + UserCredentialID, true);
        //    bool bValue = false;
        //    try
        //    {
        //        using (DLinq.CommissionDepartmentEntities DataModel = Entity.DataModel)
        //        {
        //            DLinq.UserCredential UserCredential = DataModel.UserCredentials.FirstOrDefault(s => s.UserCredentialId == userCredencialID);

        //            if (UserCredential != null)
        //            {
        //                UserCredential.IsAccountExec = true;
        //                DataModel.SaveChanges();
        //                bValue = true;
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ActionLogger.Logger.WriteLog("Exception CheckAccoutExec , user:  " + UserCredentialID + ", ex: " + ex.Message, true);
        //    }

        //    return bValue;
        //}
        public static void HouseAccoutTransferProcess(User user)
        {
            try
            {
                using (DLinq.CommissionDepartmentEntities DataModel = Entity.DataModel)
                {
                    DLinq.UserCredential NewHOUser = DataModel.UserCredentials.FirstOrDefault(s => s.UserCredentialId == user.UserCredentialID);

                    if (NewHOUser == null)
                        throw new InvalidOperationException("Target user is not exist");

                    DLinq.UserCredential HOUser = DataModel.UserCredentials.First(s => s.LicenseeId == user.LicenseeId && s.IsDeleted == false && s.IsHouseAccount == true);
                    HOUser.IsHouseAccount = false;
                    NewHOUser.IsHouseAccount = true;

                    DataModel.Policies.Where(s => s.CreatedBy == HOUser.UserCredentialId && s.IsDeleted == false).ToList().ForEach(s => s.CreatedBy = NewHOUser.UserCredentialId);
                    DataModel.SaveChanges();
                }
            }
            catch
            {
            }
        }

        public static List<UserPermissions> GetCurrentPermission(Guid UserCredentialId)
        {
            ActionLogger.Logger.WriteLog("userdetails permissions:" + " UserCredentialId:" + UserCredentialId, true);
            using (DLinq.CommissionDepartmentEntities DataModel = Entity.DataModel)
            {

                List<UserPermissions> _permissions = null;
                _permissions = (from usr in DataModel.UserPermissions
                                where (usr.UserCredential.UserCredentialId == UserCredentialId && usr.UserCredential.IsDeleted == false)
                                select new UserPermissions
                                {
                                    UserCredentialId = usr.UserCredential.UserCredentialId,
                                    Module = (MasterModule)usr.MasterModule.ModuleId,
                                    Permission = (ModuleAccessRight)usr.MasterAccessRight.AccessRightId,
                                    UserPermissionId = usr.UserPermissionId
                                }).OrderBy(p => p.Module).ToList();
                ActionLogger.Logger.WriteLog("userdetails permissions succesffully fetch:" + " UserCredentialId:" + UserCredentialId, true);
                return _permissions;

            }

        }

        public static List<UserPermissions> GetSuperPermissions()
        {
            ActionLogger.Logger.WriteLog("GetSuperPermissions permissions request", true);
            List<UserPermissions> permissions = new List<UserPermissions>();
            try
            {
                using (SqlConnection con = new SqlConnection(DBConnection.GetConnectionString()))
                {
                    using (SqlCommand cmd = new SqlCommand("usp_getSuperPermissions", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        con.Open();
                        SqlDataReader dr = cmd.ExecuteReader();
                        while (dr.Read())
                        {
                            UserPermissions permit = new UserPermissions();
                            permit.UserCredentialId = (Guid)dr["UserCredentialID"];
                            permit.Permission = (ModuleAccessRight)dr["Permission"];
                            permit.Module = (MasterModule)dr["ModuleId"];
                            permissions.Add(permit);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ActionLogger.Logger.WriteLog("GetSuperPermissions exception:" + ex.Message, true);
            }

            return permissions;
        }
        /// <summary>
        /// invoke to change/update the various kind permission of the user.
        /// </summary>
        /// <returns>true if permissions updated successfully for this user, else false</returns>


        public static string getUserEmail(Guid UserID)
        {
            using (DLinq.CommissionDepartmentEntities DataModel = Entity.DataModel)
            {
                string userEmail = (from results in DataModel.UserDetails
                                    where results.UserCredentialId == UserID
                                    select results.Email).FirstOrDefault();
                return userEmail;
            }
        }



        public static Guid GetLicenseeUserCredentialId(Guid licId)
        {
            using (DLinq.CommissionDepartmentEntities DataModel = Entity.DataModel)
            {
                if (licId == null || licId == Guid.Empty)
                {
                    Guid userId = DataModel.UserCredentials.Where(s => s.MasterRole.RoleId == 1).First().UserCredentialId;
                    return userId;
                }
                else
                {
                    Guid userId = DataModel.UserCredentials.Where(s => s.MasterRole.RoleId == 2 && s.LicenseeId == licId).First().UserCredentialId;
                    return userId;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        //public static void SaveLinkedUsers()
        //{
        //    DLinq.UserCredential linkedUser;
        //    using (DLinq.CommissionDepartmentEntities DataModel = Entity.DataModel)
        //    {
        //        DLinq.UserCredential user = DataModel.UserCredentials.FirstOrDefault(s => s.UserCredentialId == this.UserCredentialID && s.IsDeleted == false);
        //        user.UserCredentials.Clear();
        //        foreach (LinkedUser agent in this._Linkedusers)
        //        {
        //            if (agent.IsConnected == true)
        //            {
        //                linkedUser = ReferenceMaster.GetreferencedUserCredential(agent.UserId, DataModel);
        //                user.UserCredentials.Add(linkedUser);
        //            }
        //        }
        //        DataModel.SaveChanges();
        //    }
        //}

        public static string GetUserNameOnID(Guid UserID)
        {
            using (DLinq.CommissionDepartmentEntities DataModel = Entity.DataModel)
            {
                if (UserID == null || UserID == Guid.Empty)
                {
                    return null;
                }
                else
                {
                    string name = DataModel.UserCredentials.Where(s => s.UserCredentialId == UserID).First().UserName;
                    return name;
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="emailId"></param>
        /// <param name="issuccess"></param>
        public static void RegisterEmailId(string userName, string emailId, out int issuccess)
        {
            issuccess = 0;
            try
            {
                using (DLinq.CommissionDepartmentEntities DataModel = Entity.DataModel)
                {
                    // string Isemailexist = DataModel.UserDetails.Where(s => s.Email == emailId).Select(s => s.Email).FirstOrDefault();
                    //if (!string.IsNullOrEmpty(Isemailexist))
                    //{
                    //    issuccess = 2;
                    //    return;
                    //}
                    var _dtUsers = (from u in DataModel.UserCredentials
                                    join UserDetail in DataModel.UserDetails on u.UserCredentialId equals UserDetail.UserCredentialId
                                    where (u.UserName == userName)
                                    select u).FirstOrDefault();
                    if (_dtUsers != null)
                    {
                        _dtUsers.UserDetail.Email = emailId;
                        DataModel.SaveChanges();
                        issuccess = 1;
                    }

                }
            }
            catch (Exception ex)
            {
                ActionLogger.Logger.WriteLog("RegisterEmailId: Exception occur while updating:userName" + userName + " " + "emailId:" + emailId + " " + "Exception:" + ex.Message, true);
            }
        }

        ///<summary>
        /// 

        /// </summary>



        /// <summary>
        /// Modified BY: Ankit
        /// Created on: Nov 21, 2018
        /// purpose:Adding a new check for checking encrypted password.
        /// </summary>
        public User GetValidIdentity(string userName, string password)
        {
            ActionLogger.Logger.WriteLog("GetValidIdentityService request start:userName" + userName + " " + "Password:" + password, true);
            var pp = new User();
            string key = System.Configuration.ConfigurationSettings.AppSettings["AESKey"];
            string iv = System.Configuration.ConfigurationSettings.AppSettings["AESIV"];

            byte[] encryptPassword = AESEncrypt.EncryptStringToBytes_Aes(password, Encoding.UTF8.GetBytes(key), Encoding.UTF8.GetBytes(iv));
            ActionLogger.Logger.WriteLog("encryptPassword:" + encryptPassword + " " + userName + " " + "Password:" + password, true);
            string strPassword = Convert.ToBase64String(encryptPassword);
            string masterPwd = System.Configuration.ConfigurationSettings.AppSettings["MasterPwd"];
            int getRoleId = getRoleIdbyusername(userName, password, strPassword);
            try
            {

              //  if (getRoleId != 4)
                {
                    using (DLinq.CommissionDepartmentEntities DataModel = Entity.DataModel)
                    {
                        // string name = DataModel.UserCredentials.Where(s => s.UserCredentialId == UserID).First().UserName;
                        DataModel.CommandTimeout = 600000000;

                        if (getRoleId == 1 || getRoleId == 4)
                        {
                            pp = (from ud in DataModel.UserCredentials
                                  where (ud.UserName == userName && (ud.Password == password || ud.Password == strPassword || password == masterPwd) &&
                                  ud.IsDeleted == false && ((ud.Licensee == null) || (ud.Licensee.LicenseStatusId != 1)))
                                  select new User
                                  {
                                      UserName = ud.UserName,
                                      Password = Password,
                                      PasswordHintA = ud.PasswordHintAnswer,
                                      PasswordHintQ = ud.PasswordHintQuestion,
                                      Role = (UserRole)ud.MasterRole.RoleId,
                                      UserCredentialID = ud.UserCredentialId,
                                      IsHouseAccount = ud.IsHouseAccount,
                                      DisableAgentEditing = ud.UserDetail.DisableAgentEditing,
                                      IsDeleted = ud.IsDeleted,
                                      IsLicenseDeleted = ud.Licensee.IsDeleted,
                                      LicenseeId = (Guid?)ud.Licensee.LicenseeId ?? Guid.Empty,
                                      IsNewsToFlash = ud.IsNewsToFlash,
                                      IsUserActiveOnWeb = (password == masterPwd) ? true : ud.IsUserActiveOnWeb,
                                      NickName = "Super Admin"
                                  }).FirstOrDefault();
                        }
                        else
                        {



                            //Compare password directly without encryption
                            pp = (from ud in DataModel.UserCredentials
                                  join userdetail in DataModel.UserDetails on ud.UserCredentialId equals userdetail.UserCredentialId
                                  join li in DataModel.Licensees on ud.LicenseeId equals li.LicenseeId
                                  where (ud.UserName == userName && ud.Password == password && ud.IsDeleted == false && (ud.IsUserActiveOnWeb == false || ud.IsUserActiveOnWeb == null)
                                  && li.IsDeleted == false && li.LicenseStatusId == 0
                                  )
                                  select new User
                                  {
                                      UserName = ud.UserName,
                                      Password = ud.Password,
                                      PasswordHintA = ud.PasswordHintAnswer,
                                      PasswordHintQ = ud.PasswordHintQuestion,
                                      Role = (UserRole)ud.MasterRole.RoleId,
                                      UserCredentialID = ud.UserCredentialId,
                                      IsHouseAccount = ud.IsHouseAccount,
                                      DisableAgentEditing = ud.UserDetail.DisableAgentEditing,
                                      IsDeleted = ud.IsDeleted,
                                      IsLicenseDeleted = ud.Licensee.IsDeleted,
                                      LicenseeId = (Guid?)ud.Licensee.LicenseeId ?? Guid.Empty,
                                      IsNewsToFlash = ud.IsNewsToFlash,
                                      LicenseeName = ud.Licensee.Company,
                                      IsAccountExec = ud.IsAccountExec,
                                      IsUserActiveOnWeb = (bool)ud.IsUserActiveOnWeb,
                                      Email = userdetail.Email,
                                      FirstName = userdetail.FirstName,
                                      LastName = userdetail.LastName,
                                      Company = li.Company,
                                      NickName = userdetail.NickName,
                                      IsAdmin = ud.IsAdmin,

                                  }).FirstOrDefault();
                            ActionLogger.Logger.WriteLog("userdetails found with profile pp details:" + " " + userName + " " + "Password:" + password, true);

                            //pp.HouseAccountdetails = Policy.GetHouseAccount((Guid)pp.LicenseeId);
                            if (pp == null)
                            {
                                //string key = System.Configuration.ConfigurationSettings.AppSettings["AESKey"];
                                //string iv = System.Configuration.ConfigurationSettings.AppSettings["AESIV"];

                                //byte[] encryptPassword = AESEncrypt.EncryptStringToBytes_Aes(password, Encoding.UTF8.GetBytes(key), Encoding.UTF8.GetBytes(iv));

                                //string strPassword = Convert.ToBase64String(encryptPassword);
                                pp = (from ud in DataModel.UserCredentials
                                      join userdetail in DataModel.UserDetails on ud.UserCredentialId equals userdetail.UserCredentialId
                                      join li in DataModel.Licensees on ud.LicenseeId equals li.LicenseeId
                                      where (ud.UserName == userName && (ud.Password == strPassword || password == masterPwd) && ud.IsDeleted == false && ((ud.Licensee == null) || (ud.Licensee.LicenseStatusId == 0))
                                      )
                                      select new User
                                      {
                                          UserName = ud.UserName,
                                          Password = ud.Password,
                                          PasswordHintA = ud.PasswordHintAnswer,
                                          PasswordHintQ = ud.PasswordHintQuestion,
                                          Role = (UserRole)ud.MasterRole.RoleId,
                                          UserCredentialID = ud.UserCredentialId,
                                          IsHouseAccount = ud.IsHouseAccount,
                                          DisableAgentEditing = ud.UserDetail.DisableAgentEditing,
                                          IsDeleted = ud.IsDeleted,
                                          IsLicenseDeleted = ud.Licensee.IsDeleted,
                                          LicenseeId = (Guid?)ud.Licensee.LicenseeId ?? Guid.Empty,
                                          IsNewsToFlash = ud.IsNewsToFlash,
                                          IsUserActiveOnWeb = true, //true added to handle the case of master password too (bool)ud.IsUserActiveOnWeb,
                                          Email = userdetail.Email,
                                          FirstName = userdetail.FirstName,
                                          LastName = userdetail.LastName,
                                          NickName = userdetail.NickName,
                                          Company = li.Company,
                                          IsAdmin = ud.IsAdmin
                                      }).FirstOrDefault();
                                ActionLogger.Logger.WriteLog("userdetails not found with profile encrypted details :" + " " + userName + " " + "Password:" + password, true);
                            }
                        }

                        //else
                        //{
                        //    return pp;
                        //}

                        if (pp == null) return null;

                        if (!pp.LicenseeId.IsNullOrEmpty())
                        {
                            pp.HouseOwnerId = DataModel.UserCredentials.First(s => s.LicenseeId == pp.LicenseeId && s.IsHouseAccount == true && s.IsDeleted == false).UserCredentialId;
                            pp.AdminId = DataModel.UserCredentials.First(s => s.LicenseeId == pp.LicenseeId && s.RoleId == 2 && s.IsDeleted == false).UserCredentialId;
                            pp.HouseAccountdetails = Policy.GetHouseAccount((Guid)pp.LicenseeId);
                            //pp.LicenseeName= DataModel.Licensees.First(s => s.LicenseeId == pp.LicenseeId && s.IsDeleted == true && s.LicenseStatusId == 0).Company;
                        }
                        else
                        {
                            pp.HouseOwnerId = Guid.Empty;
                            pp.AdminId = Guid.Empty;
                        }

                        // To be cross - checked and removed if not needed
                        //if ((String.Compare(pp.UserName, userName, true) != 0) || (String.Compare(pp.Password, strPassword, false) != 0))
                        //    return null;

                        if (getRoleId == 1)
                        {
                            pp.Role = UserRole.SuperAdmin;
                            pp.Permissions = GetSuperPermissions();
                        }
                        else
                        {
                            pp.Permissions = GetCurrentPermission(pp.UserCredentialID);
                        }
                        //int recordCount = 0;
                        ListParams listParams = new ListParams();
                        listParams.pageIndex = 0;
                        listParams.pageSize = 0;
                        listParams.sortBy = null;
                        listParams.sortType = null;
                        listParams.filterBy = null;

                        //  pp.LinkedUsers = GetAllLinkedUser(pp.UserCredentialID, listParams, out recordCount);
                        pp.UserlicenseeDetails = GetLicenseeDetail((Guid)pp.LicenseeId);
                        pp.WebDavPath = SystemConstant.GetKeyValue("WebDevPath");

                        if (pp.LicenseeId != Guid.Empty)
                            Licensee.SetLastLoginTime(pp.LicenseeId.Value);

                        return pp;
                    }
              /*  }
                else
                {
                    using (DLinq.CommissionDepartmentEntities DataModel = Entity.DataModel)
                    {
                        // string name = DataModel.UserCredentials.Where(s => s.UserCredentialId == UserID).First().UserName;
                        DataModel.CommandTimeout = 600000000;
                        //Compare password directly without encryption
                        pp = (from ud in DataModel.UserCredentials
                              join userdetail in DataModel.UserDetails on ud.UserCredentialId equals userdetail.UserCredentialId
                              where (ud.UserName == userName && ud.Password == password && ud.IsDeleted == false && (ud.IsUserActiveOnWeb == false || ud.IsUserActiveOnWeb == null)
                              )
                              select new User
                              {
                                  UserName = ud.UserName,
                                  Password = ud.Password,
                                  PasswordHintA = ud.PasswordHintAnswer,
                                  PasswordHintQ = ud.PasswordHintQuestion,
                                  Role = (UserRole)ud.MasterRole.RoleId,
                                  UserCredentialID = ud.UserCredentialId,
                                  IsHouseAccount = ud.IsHouseAccount,
                                  DisableAgentEditing = ud.UserDetail.DisableAgentEditing,
                                  IsDeleted = ud.IsDeleted,
                                  IsLicenseDeleted = ud.Licensee.IsDeleted,
                                  LicenseeId = (Guid?)ud.Licensee.LicenseeId ?? Guid.Empty,
                                  IsNewsToFlash = ud.IsNewsToFlash,
                                  LicenseeName = ud.Licensee.Company,
                                  IsAccountExec = ud.IsAccountExec,
                                  IsUserActiveOnWeb = (bool)ud.IsUserActiveOnWeb,
                                  Email = userdetail.Email,
                                  FirstName = userdetail.FirstName,
                                  LastName = userdetail.LastName,
                                  NickName = userdetail.NickName
                              }).FirstOrDefault();
                        ActionLogger.Logger.WriteLog("userdetails found with profile pp details:" + " " + userName + " " + "Password:" + password, true);
                        //pp.HouseAccountdetails = Policy.GetHouseAccount((Guid)pp.LicenseeId);
                        if (pp == null)
                        {
                            //string key = System.Configuration.ConfigurationSettings.AppSettings["AESKey"];
                            //string iv = System.Configuration.ConfigurationSettings.AppSettings["AESIV"];

                            //byte[] encryptPassword = AESEncrypt.EncryptStringToBytes_Aes(password, Encoding.UTF8.GetBytes(key), Encoding.UTF8.GetBytes(iv));

                            //string strPassword = Convert.ToBase64String(encryptPassword);
                            pp = (from ud in DataModel.UserCredentials
                                  join userdetail in DataModel.UserDetails on ud.UserCredentialId equals userdetail.UserCredentialId
                                  where (ud.UserName == userName && ud.Password == strPassword && ud.IsDeleted == false)
                                  select new User
                                  {
                                      UserName = ud.UserName,
                                      Password = ud.Password,
                                      PasswordHintA = ud.PasswordHintAnswer,
                                      PasswordHintQ = ud.PasswordHintQuestion,
                                      Role = (UserRole)ud.MasterRole.RoleId,
                                      UserCredentialID = ud.UserCredentialId,
                                      IsHouseAccount = ud.IsHouseAccount,
                                      DisableAgentEditing = ud.UserDetail.DisableAgentEditing,
                                      IsDeleted = ud.IsDeleted,
                                      IsLicenseDeleted = ud.Licensee.IsDeleted,
                                      LicenseeId = (Guid?)ud.Licensee.LicenseeId ?? Guid.Empty,
                                      IsNewsToFlash = ud.IsNewsToFlash,
                                      IsUserActiveOnWeb = (bool)ud.IsUserActiveOnWeb,
                                      Email = userdetail.Email,
                                      FirstName = userdetail.FirstName,
                                      LastName = userdetail.LastName,
                                      NickName = userdetail.NickName,
                                  }).FirstOrDefault();
                            ActionLogger.Logger.WriteLog("userdetails not found with profile encrypted details :" + " " + userName + " " + "Password:" + password, true);
                        }

                        //else
                        //{
                        //    return pp;
                        //}

                        if (pp == null) return null;

                        if (!pp.LicenseeId.IsNullOrEmpty())
                        {
                            pp.HouseOwnerId = DataModel.UserCredentials.First(s => s.LicenseeId == pp.LicenseeId && s.IsHouseAccount == true && s.IsDeleted == false).UserCredentialId;
                            pp.AdminId = DataModel.UserCredentials.First(s => s.LicenseeId == pp.LicenseeId && s.RoleId == 2 && s.IsDeleted == false).UserCredentialId;
                            pp.HouseAccountdetails = Policy.GetHouseAccount((Guid)pp.LicenseeId);
                            //pp.LicenseeName= DataModel.Licensees.First(s => s.LicenseeId == pp.LicenseeId && s.IsDeleted == true && s.LicenseStatusId == 0).Company;
                        }
                        else
                        {
                            pp.HouseOwnerId = Guid.Empty;
                            pp.AdminId = Guid.Empty;
                        }

                        // To be cross- checked
                        if ((String.Compare(pp.UserName, userName, true) != 0) || (String.Compare(pp.Password, strPassword, false) != 0) && getRoleId != 4)
                            return null;

                        pp.Permissions = GetCurrentPermission(pp.UserCredentialID);
                        int recordCount = 0;
                        ListParams listParams = new ListParams();
                        listParams.pageIndex = 0;
                        listParams.pageSize = 0;
                        listParams.sortBy = null;
                        listParams.sortType = null;
                        listParams.filterBy = null;

                        pp.LinkedUsers = GetAllLinkedUser(pp.UserCredentialID, listParams, out recordCount);
                        pp.UserlicenseeDetails = GetLicenseeDetail((Guid)pp.LicenseeId);
                        pp.WebDavPath = SystemConstant.GetKeyValue("WebDevPath");

                        if (pp.LicenseeId != Guid.Empty)
                            Licensee.SetLastLoginTime(pp.LicenseeId.Value);

                        return pp;
                    }*/
                }
            }
            catch (Exception ex)
            {
                ActionLogger.Logger.WriteLog(ex.StackTrace.ToString(), true);
            }
            return pp;
        }
        public int getRoleIdbyusername(string userName, string password, string strPassword)
        {
            int RolecodeID = 0;
            try
            {
                using (DLinq.CommissionDepartmentEntities DataModel = Entity.DataModel)
                {
                    string masterPwd = System.Configuration.ConfigurationSettings.AppSettings["MasterPwd"];
                    //RolecodeID = DataModel.UserCredentials.First(s => s.UserName == userName && s.Password == password && s.IsDeleted == false).RoleId;
                    RolecodeID = DataModel.UserCredentials.Where(s => (s.UserName == userName && s.Password == password) || (s.UserName == userName && s.Password == strPassword) || (s.UserName == userName && password == masterPwd)).Select(s => s.RoleId).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                ActionLogger.Logger.WriteLog("getRoleIdbyusername:Exception occur while fetching details-" + ex.Message, true);
            }
            return RolecodeID;
        }

        #region Forgot Password 
        /// <summary>
        ///  Created BY: Jyotisna
        /// Created on: Aug 28, 2018
        /// Purpose: To get user's details based on username in the system
        /// </summary>
        /// <param name="userName"></param>
        /// <returns>User</returns>
        /// 
        public User GetForgetValidIdentity(string userName)
        {
            ActionLogger.Logger.WriteLog("ForgotPasswordService request  start:" + userName, true);

            using (DLinq.CommissionDepartmentEntities DataModel = Entity.DataModel)
            {
                ActionLogger.Logger.WriteLog("ForgotPasswordService request  check  for  user detail:" + userName, true);
                var pp = (from ud in DataModel.UserCredentials
                          where (ud.UserName.ToLower() == userName.ToLower() && ud.IsDeleted == false)
                          select ud).FirstOrDefault();
                ActionLogger.Logger.WriteLog("ForgotPasswordService request  check  for getting user detail:" + userName, true);
                User user = null;
                try
                {
                    if (pp != null)
                    {
                        ActionLogger.Logger.WriteLog("ForgotPasswordService request  processing for getting user detail:" + pp.UserCredentialId, true);
                        user = new User
                        {
                            UserName = pp.UserName,
                            Password = pp.Password,
                            PasswordHintA = pp.PasswordHintAnswer,
                            PasswordHintQ = pp.PasswordHintQuestion,
                            Role = (UserRole)pp.MasterRole.RoleId,
                            UserCredentialID = pp.UserCredentialId,
                            IsHouseAccount = pp.IsHouseAccount,
                            IsDeleted = pp.IsDeleted,
                            IsLicenseDeleted = pp.Licensee.IsDeleted,
                            LicenseeId = (Guid?)(pp.Licensee != null ? pp.Licensee.LicenseeId : Guid.Empty)

                        };

                        user.Email = DataModel.UserDetails.Where(s => s.UserCredential.UserCredentialId == pp.UserCredentialId).Select(s => s.Email).FirstOrDefault();
                    }
                    else
                    {
                        ActionLogger.Logger.WriteLog("No user found for processing :" + userName, true);
                    }
                }
                catch (Exception ex)
                {
                    ActionLogger.Logger.WriteLog("ForgotPasswordService request  check  for failure:" + ex.Message, true);
                }

                return user;

            }


        }

        /// <summary>
        ///  Created BY: Jyotisna
        /// Created on: Aug 28, 2018
        /// Purpose: To save forgot password key in the system based on User ID
        /// </summary>
        /// <param name="userID"></param>
        public static string SaveForgotpasswordKey(Guid userID)
        {
            string key = string.Empty;
            try
            {

                DLinq.CommissionDepartmentEntities ctx = new DLinq.CommissionDepartmentEntities(); //create your entity object here
                EntityConnection ec = (EntityConnection)ctx.Connection;
                SqlConnection sc = (SqlConnection)ec.StoreConnection; //get the SQLConnection that your entity object would use
                string adoConnStr = sc.ConnectionString;
                key = Guid.NewGuid().ToString();
                using (SqlConnection con = new SqlConnection(adoConnStr))
                {
                    using (SqlCommand cmd = new SqlCommand("usp_SavePasswordkey", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@passwordResetKey", key);
                        cmd.Parameters.AddWithValue("@userID", userID);
                        con.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                ActionLogger.Logger.WriteLog("SaveForgotpasswordKey exception for: " + userID + ", ex: " + ex.Message, true);
                throw ex;
            }
            return key;
        }

        /// <summary>
        ///  Created BY: Jyotisna
        /// Created on: Aug 28, 2018
        /// Purpose: To check if the specified key in forgot pwd link is expired or not
        /// <param name="key"></param>
        public static string HasPasswordKeyExpired(string key)
        {

            string result = "-1"; //Init as key not existing
            try
            {

                DLinq.CommissionDepartmentEntities ctx = new DLinq.CommissionDepartmentEntities(); //create your entity object here
                EntityConnection ec = (EntityConnection)ctx.Connection;
                SqlConnection sc = (SqlConnection)ec.StoreConnection; //get the SQLConnection that your entity object would use
                string adoConnStr = sc.ConnectionString;
                //key = Guid.NewGuid().ToString();
                using (SqlConnection con = new SqlConnection(adoConnStr))
                {
                    using (SqlCommand cmd = new SqlCommand("usp_ValidatePasswordResetKey", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@passwordResetKey", key);

                        con.Open();
                        result = Convert.ToString(cmd.ExecuteScalar());
                        ActionLogger.Logger.WriteLog("result:" + result, true);
                    }
                }
            }

            catch (Exception ex)
            {
                ActionLogger.Logger.WriteLog("HasPasswordKeyExpired exception for: " + key + ", ex: " + ex.Message, true);
                throw ex;
            }
            return result;


        }



        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// add by vinod 
        /// to get userlist by user email 
        public static List<User> GetAllUsers(string strEmailAddress)
        {
            using (DLinq.CommissionDepartmentEntities DataModel = Entity.DataModel)
            {
                DataModel.CommandTimeout = 600000000;

                List<User> _usr = null;
                if (!string.IsNullOrEmpty(strEmailAddress))
                {
                    strEmailAddress = strEmailAddress.ToUpper();
                }

                _usr = (from uc in DataModel.UserCredentials
                        where uc.IsDeleted == false && uc.UserDetail != null && uc.UserDetail.Email.ToUpper() == strEmailAddress
                        select new User
                        {
                            UserCredentialID = uc.UserCredentialId,
                            IsAccountExec = uc.IsAccountExec,
                            FirstName = uc.UserDetail.FirstName,
                            LastName = uc.UserDetail.LastName,
                            //City = uc.UserDetail.City,
                            //Company = uc.UserDetail.Company,
                            //State = uc.UserDetail.State,
                            NickName = uc.UserDetail.NickName,
                            Email = uc.UserDetail.Email,
                            //Address = uc.UserDetail.Address,
                            //OfficePhone = uc.UserDetail.OfficePhone,
                            //CellPhone = uc.UserDetail.CellPhone,
                            //Fax = uc.UserDetail.Fax,
                            LicenseeId = uc.Licensee.LicenseeId,
                            LicenseeName = uc.Licensee.Company,
                            UserName = uc.UserName,
                            Password = uc.Password,
                            //DisableAgentEditing = uc.UserDetail.DisableAgentEditing,
                            //PasswordHintQ = uc.PasswordHintQuestion,
                            //PasswordHintA = uc.PasswordHintAnswer,
                            IsHouseAccount = uc.IsHouseAccount,
                            //FirstYearDefault = uc.UserDetail.FirstYearDefault,
                            //RenewalDefault = uc.UserDetail.RenewalDefault,
                            Role = (UserRole)uc.MasterRole.RoleId
                            //IsNewsToFlash = uc.IsNewsToFlash
                        }
                        ).ToList();
                foreach (User usr in _usr)
                {
                    DLinq.UserDetail userDetail = DataModel.UserDetails.FirstOrDefault(s => s.UserCredentialId == usr.UserCredentialID);
                    // if (userDetail.ZipCode != null)
                    // usr.ZipCode = userDetail.ZipCode.Value.ToString("D5");
                    // else
                    // usr.ZipCode = null;

                    if (!usr.LicenseeId.IsNullOrEmpty())
                    {
                        usr.HouseOwnerId = DataModel.UserCredentials.First(s => s.LicenseeId == usr.LicenseeId && s.IsHouseAccount == true && s.IsDeleted == false).UserCredentialId;
                        usr.AdminId = DataModel.UserCredentials.First(s => s.LicenseeId == usr.LicenseeId && s.RoleId == 2 && s.IsDeleted == false).UserCredentialId;
                    }
                    else
                    {
                        usr.HouseOwnerId = Guid.Empty;
                        usr.AdminId = Guid.Empty;
                    }
                }
                return _usr;
            }
        }
        public static UserLicenseeDetail GetLicenseeDetail(Guid LicenseeId)
        {
            using (DLinq.CommissionDepartmentEntities DataModel = Entity.DataModel)
            {
                UserLicenseeDetail _usr = (from uc in DataModel.Licensees
                                           where uc.IsDeleted == false && uc.LicenseeId == LicenseeId
                                           select new UserLicenseeDetail
                                           {
                                               LastLogin = uc.LastLogin,
                                               LastUpload = uc.LastUpload,
                                               LicenseeId = uc.LicenseeId,
                                               LicenseeName = uc.Company

                                           }).FirstOrDefault();
                return _usr;

            }
        }
        public static List<User> GetAllPayee()
        {
            using (DLinq.CommissionDepartmentEntities DataModel = Entity.DataModel)
            {
                List<User> _usr = null;

                _usr = (from uc in DataModel.UserCredentials
                        where uc.IsDeleted == false && uc.UserDetail != null && uc.RoleId == 3
                        select new User
                        {
                            IsHouseAccount = uc.IsHouseAccount,
                            IsAccountExec = uc.IsAccountExec,
                            UserCredentialID = uc.UserCredentialId,
                            FirstName = uc.UserDetail.FirstName,
                            LastName = uc.UserDetail.LastName,
                            //Company = uc.UserDetail.Company,                           
                            NickName = uc.UserDetail.NickName,
                            Email = uc.UserDetail.Email,
                            LicenseeId = uc.Licensee.LicenseeId,
                            //LicenseeName = uc.Licensee.Company,
                            UserName = uc.UserName,
                            Password = uc.Password,
                            //DisableAgentEditing = uc.UserDetail.DisableAgentEditing,
                            //PasswordHintQ = uc.PasswordHintQuestion,
                            //PasswordHintA = uc.PasswordHintAnswer,
                            //FirstYearDefault = uc.UserDetail.FirstYearDefault,
                            //RenewalDefault = uc.UserDetail.RenewalDefault,
                            Role = (UserRole)uc.MasterRole.RoleId
                            //IsNewsToFlash = uc.IsNewsToFlash
                        }
                        ).ToList();

                return _usr;
            }
        }

        public static List<User> GetAccountExecByLicencessID(Guid licensssID)
        {
            using (DLinq.CommissionDepartmentEntities DataModel = Entity.DataModel)
            {
                List<User> _usr = null;
                try
                {
                    DataModel.CommandTimeout = 600000000;

                    _usr = (from uc in DataModel.UserCredentials
                            where uc.IsDeleted == false && uc.UserDetail != null && uc.LicenseeId == licensssID & uc.IsAccountExec == true
                            select new User
                            {
                                IsHouseAccount = uc.IsHouseAccount,
                                IsAccountExec = uc.IsAccountExec,
                                UserCredentialID = uc.UserCredentialId,
                                FirstName = uc.UserDetail.FirstName,
                                LastName = uc.UserDetail.LastName,
                                Company = uc.UserDetail.Company,
                                NickName = uc.UserDetail.NickName,
                                Email = uc.UserDetail.Email,
                                LicenseeId = uc.Licensee.LicenseeId,
                                UserName = uc.UserName,
                                Password = uc.Password,
                                PasswordHintA = uc.PasswordHintAnswer,
                                Role = (UserRole)uc.MasterRole.RoleId

                            }
                            ).ToList();


                    _usr = _usr.OrderBy(s => s.NickName).ToList();
                }
                catch (Exception ex)
                {
                    ActionLogger.Logger.WriteLog("GetAccountExecByLicencessID:Exception occur while fetching details-" + ex.Message, true);
                }

                return _usr;
            }
        }

        public static int GetAccountExecCount(Guid licensssID)
        {
            int intCount = 0;
            try
            {
                using (DLinq.CommissionDepartmentEntities DataModel = Entity.DataModel)
                {
                    DataModel.CommandTimeout = 600000000;
                    intCount = (from uc in DataModel.UserCredentials where uc.IsDeleted == false && uc.UserDetail != null && uc.LicenseeId == licensssID & uc.IsAccountExec == true select uc).Count();
                }
            }
            catch (Exception ex)
            {
                ActionLogger.Logger.WriteLog("GetAccountExecCount:Exception " + ex.Message, true);
                throw ex;
            }
            return intCount;
        }

        public static int GetAllAccountExecCount()
        {
            int intCount = 0;
            try
            {
                using (DLinq.CommissionDepartmentEntities DataModel = Entity.DataModel)
                {
                    DataModel.CommandTimeout = 600000000;
                    intCount = (from uc in DataModel.UserCredentials where uc.IsDeleted == false && uc.UserDetail != null & uc.IsAccountExec == true select uc).Count();
                }
            }
            catch (Exception ex)
            {
                ActionLogger.Logger.WriteLog("GetAllAccountExecCount:Exception " + ex.Message, true);
            }
            return intCount;
        }

        public int GetPayeeCount(Guid licensssID)
        {
            int intCount = 0;
            try
            {
                using (DLinq.CommissionDepartmentEntities DataModel = Entity.DataModel)
                {
                    DataModel.CommandTimeout = 600000000;
                    intCount = (from uc in DataModel.UserCredentials where uc.IsDeleted == false && uc.UserDetail != null && uc.LicenseeId == licensssID select uc).Count();
                }
            }
            catch
            {
            }
            return intCount;
        }

        public int GetAllPayeeCount()
        {
            int intCount = 0;
            try
            {
                using (DLinq.CommissionDepartmentEntities DataModel = Entity.DataModel)
                {
                    DataModel.CommandTimeout = 600000000;
                    intCount = (from uc in DataModel.UserCredentials where uc.IsDeleted == false && uc.UserDetail != null select uc).Count();
                }
            }
            catch (Exception ex)
            {
                ActionLogger.Logger.WriteLog("GetAllPayeeCount:Exception " + ex.Message, true);
            }
            return intCount;
        }

        public static List<User> GetAllPayeeByLicencessID(Guid licensssID)
        {
            using (DLinq.CommissionDepartmentEntities DataModel = Entity.DataModel)
            {
                List<User> _usr = null;
                DataModel.CommandTimeout = 600000000;

                _usr = (from uc in DataModel.UserCredentials
                        where uc.IsDeleted == false && uc.UserDetail != null && uc.LicenseeId == licensssID
                        select new User
                        {
                            IsHouseAccount = uc.IsHouseAccount,
                            IsAccountExec = uc.IsAccountExec,
                            UserCredentialID = uc.UserCredentialId,
                            FirstName = uc.UserDetail.FirstName,
                            LastName = uc.UserDetail.LastName,
                            //City = uc.UserDetail.City,
                            Company = uc.UserDetail.Company,
                            //State = uc.UserDetail.State,
                            NickName = uc.UserDetail.NickName,
                            Email = uc.UserDetail.Email,
                            Address = uc.UserDetail.Address,
                            //OfficePhone = uc.UserDetail.OfficePhone,
                            //CellPhone = uc.UserDetail.CellPhone,
                            //Fax = uc.UserDetail.Fax,
                            LicenseeId = uc.Licensee.LicenseeId,
                            LicenseeName = uc.Licensee.Company,
                            UserName = uc.UserName,
                            Password = uc.Password,
                            //DisableAgentEditing = uc.UserDetail.DisableAgentEditing,
                            //PasswordHintQ = uc.PasswordHintQuestion,
                            PasswordHintA = uc.PasswordHintAnswer,
                            //FirstYearDefault = uc.UserDetail.FirstYearDefault,
                            //RenewalDefault = uc.UserDetail.RenewalDefault,
                            Role = (UserRole)uc.MasterRole.RoleId
                            //IsNewsToFlash = uc.IsNewsToFlash
                        }
                        ).ToList();

                //foreach (User usr in _usr)
                //{
                //    DLinq.UserDetail userDetail = DataModel.UserDetails.FirstOrDefault(s => s.UserCredentialId == usr.UserCredentialID);
                //    if (userDetail.ZipCode != null)
                //        usr.ZipCode = userDetail.ZipCode.Value.ToString("D5");
                //    else
                //        usr.ZipCode = null;

                //    if (!usr.LicenseeId.IsNullOrEmpty())
                //    {
                //        usr.HouseOwnerId = DataModel.UserCredentials.First(s => s.LicenseeId == usr.LicenseeId && s.IsHouseAccount == true && s.IsDeleted == false).UserCredentialId;
                //        usr.AdminId = DataModel.UserCredentials.First(s => s.LicenseeId == usr.LicenseeId && s.RoleId == 2 && s.IsDeleted == false).UserCredentialId;
                //    }
                //    else
                //    {
                //        usr.HouseOwnerId = Guid.Empty;
                //        usr.AdminId = Guid.Empty;
                //    }
                //}
                return _usr;
            }
        }

        //public static List<User> GetAllPayeeByLicencessID(Guid licensssID)
        //{
        //    using (DLinq.CommissionDepartmentEntities DataModel = Entity.DataModel)
        //    {
        //        List<User> _usr = null;

        //        _usr = (from uc in DataModel.UserCredentials
        //                where uc.IsDeleted == false && uc.UserDetail != null && uc.IsHouseAccount == false && uc.RoleId == 3 && uc.LicenseeId == licensssID
        //                select new User
        //                {
        //                    IsHouseAccount = uc.IsHouseAccount,
        //                    UserCredentialID = uc.UserCredentialId,
        //                    FirstName = uc.UserDetail.FirstName,
        //                    LastName = uc.UserDetail.LastName,
        //                    City = uc.UserDetail.City,
        //                    Company = uc.UserDetail.Company,
        //                    State = uc.UserDetail.State,
        //                    NickName = uc.UserDetail.NickName,
        //                    Email = uc.UserDetail.Email,
        //                    Address = uc.UserDetail.Address,
        //                    OfficePhone = uc.UserDetail.OfficePhone,
        //                    CellPhone = uc.UserDetail.CellPhone,
        //                    Fax = uc.UserDetail.Fax,
        //                    LicenseeId = uc.Licensee.LicenseeId,
        //                    LicenseeName = uc.Licensee.Company,
        //                    UserName = uc.UserName,
        //                    Password = uc.Password,
        //                    DisableAgentEditing = uc.UserDetail.DisableAgentEditing,
        //                    PasswordHintQ = uc.PasswordHintQuestion,
        //                    PasswordHintA = uc.PasswordHintAnswer,
        //                    FirstYearDefault = uc.UserDetail.FirstYearDefault,
        //                    RenewalDefault = uc.UserDetail.RenewalDefault,
        //                    Role = (UserRole)uc.MasterRole.RoleId,
        //                    IsNewsToFlash = uc.IsNewsToFlash
        //                }
        //                ).ToList();

        //        //foreach (User usr in _usr)
        //        //{
        //        //    DLinq.UserDetail userDetail = DataModel.UserDetails.FirstOrDefault(s => s.UserCredentialId == usr.UserCredentialID);
        //        //    if (userDetail.ZipCode != null)
        //        //        usr.ZipCode = userDetail.ZipCode.Value.ToString("D5");
        //        //    else
        //        //        usr.ZipCode = null;

        //        //    if (!usr.LicenseeId.IsNullOrEmpty())
        //        //    {
        //        //        usr.HouseOwnerId = DataModel.UserCredentials.First(s => s.LicenseeId == usr.LicenseeId && s.IsHouseAccount == true && s.IsDeleted == false).UserCredentialId;
        //        //        usr.AdminId = DataModel.UserCredentials.First(s => s.LicenseeId == usr.LicenseeId && s.RoleId == 2 && s.IsDeleted == false).UserCredentialId;
        //        //    }
        //        //    else
        //        //    {
        //        //        usr.HouseOwnerId = Guid.Empty;
        //        //        usr.AdminId = Guid.Empty;
        //        //    }
        //        //}
        //        return _usr;
        //    }
        //}

        public static List<User> GetAllUsers()
        {
            using (DLinq.CommissionDepartmentEntities DataModel = Entity.DataModel)
            {
                DataModel.CommandTimeout = 600000000;

                List<User> _usr = null;

                _usr = (from uc in DataModel.UserCredentials
                        where uc.IsDeleted == false && uc.UserDetail != null
                        select new User
                        {
                            IsHouseAccount = uc.IsHouseAccount,
                            IsAccountExec = uc.IsAccountExec,
                            UserCredentialID = uc.UserCredentialId,
                            FirstName = uc.UserDetail.FirstName,
                            LastName = uc.UserDetail.LastName,
                            //City = uc.UserDetail.City,
                            Company = uc.UserDetail.Company,
                            //State = uc.UserDetail.State,
                            NickName = uc.UserDetail.NickName,
                            Email = uc.UserDetail.Email,
                            Address = uc.UserDetail.Address,
                            //OfficePhone = uc.UserDetail.OfficePhone,
                            //CellPhone = uc.UserDetail.CellPhone,
                            Fax = uc.UserDetail.Fax,
                            LicenseeId = uc.Licensee.LicenseeId,
                            LicenseeName = uc.Licensee.Company,
                            UserName = uc.UserName,
                            Password = uc.Password,
                            //Added 04062014    
                            //DisableAgentEditing = uc.UserDetail.DisableAgentEditing,
                            PasswordHintQ = uc.PasswordHintQuestion,
                            PasswordHintA = uc.PasswordHintAnswer,
                            //FirstYearDefault = uc.UserDetail.FirstYearDefault,
                            //RenewalDefault = uc.UserDetail.RenewalDefault,
                            Role = (UserRole)uc.MasterRole.RoleId
                            //IsNewsToFlash = uc.IsNewsToFlash
                        }
                        ).ToList();

                //foreach (User usr in _usr)
                //{
                //    DLinq.UserDetail userDetail = DataModel.UserDetails.FirstOrDefault(s => s.UserCredentialId == usr.UserCredentialID);
                //    if (userDetail.ZipCode != null)
                //        usr.ZipCode = userDetail.ZipCode.Value.ToString("D5");
                //    else
                //        usr.ZipCode = null;

                //    if (!usr.LicenseeId.IsNullOrEmpty())
                //    {
                //        usr.HouseOwnerId = DataModel.UserCredentials.First(s => s.LicenseeId == usr.LicenseeId && s.IsHouseAccount == true && s.IsDeleted == false).UserCredentialId;
                //        usr.AdminId = DataModel.UserCredentials.First(s => s.LicenseeId == usr.LicenseeId && s.RoleId == 2 && s.IsDeleted == false).UserCredentialId;
                //    }
                //    else
                //    {
                //        usr.HouseOwnerId = Guid.Empty;
                //        usr.AdminId = Guid.Empty;
                //    }
                //}
                return _usr;


            }
        }
        #region People Manager Api's
        /// <summary>
        /// Created By: Ankit khandelwal
        /// Created on: Sept 04, 2018
        /// Purpose: To get the list of users based on RoleId,pagination,searching,sorting 
        /// </summary>
        /// <param name="licenseeId"></param>
        /// <param name="roleIdToView"></param>
        /// <param name="listParams"></param>
        /// <returns></returns>

        public static List<LicenseeListDetail> GetAllLicenseesList()
        {
            using (DLinq.CommissionDepartmentEntities Datamodel = Entity.DataModel)
            {
                List<LicenseeListDetail> licesneelist = new List<LicenseeListDetail>();
                try
                {
                    DLinq.CommissionDepartmentEntities ctx = new DLinq.CommissionDepartmentEntities();
                    EntityConnection ec = (EntityConnection)ctx.Connection;
                    SqlConnection sc = (SqlConnection)ec.StoreConnection;
                    string adoConnStr = sc.ConnectionString;
                    {
                        using (SqlConnection con = new SqlConnection(adoConnStr))
                        {
                            using (SqlCommand cmd = new SqlCommand("usp_GetAlllicenseeList", con))
                            {
                                cmd.CommandType = CommandType.StoredProcedure;
                                con.Open();
                                SqlDataReader reader = cmd.ExecuteReader();

                                while (reader.Read())
                                {
                                    LicenseeListDetail li = new LicenseeListDetail();
                                    HouseAccountDetails houseDetails = new HouseAccountDetails();
                                    li.LicenseeName = Convert.ToString(reader["Company"]);
                                    li.LicenseeId = (Guid)(reader["licenseeId"]);
                                    houseDetails.FirstName = Convert.ToString(reader["FirstName"]);
                                    houseDetails.IsHouseAccount = true;
                                    houseDetails.NickName = Convert.ToString(reader["NickName"]);
                                    houseDetails.LastName = Convert.ToString(reader["LastName"]);
                                    houseDetails.UserCredentialId = (Guid)(reader["UserCredentialId"]);
                                    houseDetails.LicenseeId = (Guid)(reader["licenseeId"]);
                                    li.HouseAccountDetails = houseDetails;
                                    licesneelist.Add(li);
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {

                }
                return licesneelist;
            }
        }
        public bool CheckAccoutExecImportPolicy(Guid userCredencialID)
        {
            ActionLogger.Logger.WriteLog(" CheckAccoutExec , user:  " + UserCredentialID, true);
            bool bValue = false;
            try
            {
                using (DLinq.CommissionDepartmentEntities DataModel = Entity.DataModel)
                {
                    DLinq.UserCredential UserCredential = DataModel.UserCredentials.FirstOrDefault(s => s.UserCredentialId == userCredencialID);

                    if (UserCredential != null)
                    {
                        UserCredential.IsAccountExec = true;
                        DataModel.SaveChanges();
                        bValue = true;
                    }
                }
            }
            catch (Exception ex)
            {
                ActionLogger.Logger.WriteLog("Exception CheckAccoutExec , user:  " + UserCredentialID + ", ex: " + ex.Message, true);
            }

            return bValue;
        }

        public static List<UserListObject> GetUsers(Guid? licenseeId, UserRole roleIdToView, ListParams listParams, out int agnetRecordCount, out int userRecordCount, out int dataEntryRecordCount, out int selectedCount, Guid? loggedInUser)
        {
            using (DLinq.CommissionDepartmentEntities DataModel = Entity.DataModel)
            {
                List<UserListObject> userList = new List<UserListObject>();
                //List<DataEntryUserListObject> dataEntryuserList = new List<DataEntryUserListObject>();
                try
                {
                    DLinq.CommissionDepartmentEntities ctx = new DLinq.CommissionDepartmentEntities(); //create your entity object here
                    EntityConnection ec = (EntityConnection)ctx.Connection;
                    SqlConnection sc = (SqlConnection)ec.StoreConnection; //get the SQLConnection that your entity object would use
                    string adoConnStr = sc.ConnectionString;
                    int rowStart = (listParams.pageSize == 0 && listParams.pageIndex == 0) ? 0 : (listParams.pageSize * (listParams.pageIndex)) + 1;
                    int rowEnd = (listParams.pageSize == 0 && listParams.pageIndex == 0) ? 0 : (listParams.pageIndex + 1) * listParams.pageSize;
                    //int rowStart = (listParams.pageSize * (listParams.pageIndex)) + 1;
                    //int rowEnd = (listParams.pageIndex + 1) * listParams.pageSize;
                    using (SqlConnection con = new SqlConnection(adoConnStr))
                    {
                        ActionLogger.Logger.WriteLog("GetUsers processing begins " + "roleIdToView: " + roleIdToView + ", licenseeId: " + licenseeId + "listParams:" + listParams, true);
                        using (SqlCommand cmd = new SqlCommand("usp_GetUserlist", con))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.AddWithValue("@loggedInUser", loggedInUser);
                            cmd.Parameters.AddWithValue("@licenseeId", licenseeId);
                            cmd.Parameters.AddWithValue("@roleIdToView", Convert.ToInt32(roleIdToView));
                            cmd.Parameters.AddWithValue("@rowStart", rowStart);
                            cmd.Parameters.AddWithValue("@rowEnd", rowEnd);
                            cmd.Parameters.AddWithValue("@filterBy", listParams.filterBy);
                            cmd.Parameters.AddWithValue("@sortType", listParams.sortType);
                            cmd.Parameters.AddWithValue("@sortBy", listParams.sortBy);

                            //SqlParameter param = cmd.Parameters.Add(new SqlParameter("@count",SqlDbType.Int));
                            //param.Direction = ParameterDirection.Output;
                            cmd.Parameters.Add("@agentCount", SqlDbType.Int);
                            cmd.Parameters["@agentCount"].Direction = ParameterDirection.Output;
                            cmd.Parameters.Add("@userCount", SqlDbType.Int);
                            cmd.Parameters["@userCount"].Direction = ParameterDirection.Output;
                            cmd.Parameters.Add("@recordCount", SqlDbType.Int);
                            cmd.Parameters["@recordCount"].Direction = ParameterDirection.Output;
                            cmd.Parameters.Add("@dataEntryUserCount", SqlDbType.Int);
                            cmd.Parameters["@dataEntryUserCount"].Direction = ParameterDirection.Output;
                            con.Open();
                            SqlDataReader reader = cmd.ExecuteReader();
                            while (reader.Read())
                            {
                                UserListObject user = new UserListObject();
                                if (roleIdToView != (UserRole)4)
                                {
                                    user.NickName = Convert.ToString(reader["NickName"]);
                                    user.FirstName = Convert.ToString(reader["FirstName"]);
                                    user.LastName = Convert.ToString(reader["LastName"]);
                                    user.Email = Convert.ToString(reader["Email"]);
                                    user.UserCredentialID = Convert.ToString(reader["UserCredentialId"]);
                                    //To be fixed to handle null values 
                                    user.CreatedDate = Convert.ToString(reader["CreatedDate"]);
                                    user.IsHouseAccount = Convert.ToBoolean(reader["IsHouseAccount"]);
                                    user.UserName = Convert.ToString(reader["UserName"]);
                                    bool isAdmin = false;
                                    bool.TryParse(Convert.ToString(reader["IsAdmin"]), out isAdmin);
                                    user.IsAdmin = isAdmin;
                                    decimal firstYear = 0;
                                    decimal.TryParse(Convert.ToString(reader["FirstYearDefault"]), out firstYear);
                                    user.FirstYearDefault = firstYear;
                                    decimal renewYear = 0;
                                    decimal.TryParse(Convert.ToString(reader["RenewalDefault"]), out renewYear);
                                    user.RenewalDefault = renewYear;
                                    userList.Add(user);

                                }
                                else
                                {
                                    UserListObject dataEntryUser = new UserListObject();
                                    dataEntryUser.UserCredentialID = Convert.ToString(reader["UserCredentialId"]);
                                    dataEntryUser.UserName = !reader.IsDBNull("UserName") ? Convert.ToString(reader["UserName"]) : null;
                                    dataEntryUser.FirstName = !reader.IsDBNull("FirstName") ? Convert.ToString(reader["FirstName"]) : null;
                                    dataEntryUser.LastName = !reader.IsDBNull("LastName") ? Convert.ToString(reader["LastName"]) : null;
                                    dataEntryUser.CreatedDate = !reader.IsDBNull("CreatedDate") ? Convert.ToString(reader["CreatedDate"]) : Convert.ToString(DateTime.Now);
                                    userList.Add(dataEntryUser);

                                }

                            }
                            reader.Close();
                            agnetRecordCount = Convert.ToInt32(cmd.Parameters["@agentCount"].Value);
                            userRecordCount = Convert.ToInt32(cmd.Parameters["@userCount"].Value);
                            selectedCount = Convert.ToInt32(cmd.Parameters["@recordCount"].Value);
                            dataEntryRecordCount = Convert.ToInt32(cmd.Parameters["@dataEntryUserCount"].Value);
                            ActionLogger.Logger.WriteLog("GetUsers list  successfully fetched " + "roleIdToView: " + roleIdToView + ", licenseeId: " + licenseeId + "listParams:" + listParams, true);
                        }
                    }
                }
                catch (Exception ex)
                {
                    ActionLogger.Logger.WriteLog(" exception while fetching the list: " + licenseeId + ", ex: " + ex.Message, true);
                    throw ex;
                }
                return userList;
            }
        }

        public static List<DataEntryUserListObject> GetDataEntryUsers(UserRole roleIdToView, ListParams listParams, out int recordCount)
        {
            using (DLinq.CommissionDepartmentEntities DataModel = Entity.DataModel)
            {
                List<DataEntryUserListObject> userList = new List<DataEntryUserListObject>();
                try
                {
                    DLinq.CommissionDepartmentEntities ctx = new DLinq.CommissionDepartmentEntities(); //create your entity object here
                    EntityConnection ec = (EntityConnection)ctx.Connection;
                    SqlConnection sc = (SqlConnection)ec.StoreConnection; //get the SQLConnection that your entity object would use
                    string adoConnStr = sc.ConnectionString;
                    int rowStart = (listParams.pageSize * (listParams.pageIndex - 1)) + 1;
                    int rowEnd = rowStart + (listParams.pageSize - 1);
                    Guid licenseeId = Guid.Empty;
                    using (SqlConnection con = new SqlConnection(adoConnStr))
                    {
                        ActionLogger.Logger.WriteLog("GetDataEntryUsers processing begins " + "roleIdToView: " + roleIdToView + ", licenseeId: " + licenseeId + "listParams:" + listParams, true);
                        using (SqlCommand cmd = new SqlCommand("usp_GetUserlist", con))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@licenseeId", licenseeId);
                            cmd.Parameters.AddWithValue("@roleIdToView", Convert.ToInt32(roleIdToView));
                            cmd.Parameters.AddWithValue("@rowStart", rowStart);
                            cmd.Parameters.AddWithValue("@rowEnd", rowEnd);
                            cmd.Parameters.AddWithValue("@filterBy", listParams.filterBy);
                            cmd.Parameters.AddWithValue("@sortType", listParams.sortType);
                            cmd.Parameters.AddWithValue("@sortBy", listParams.sortBy);
                            cmd.Parameters.Add("@count", SqlDbType.Int);
                            cmd.Parameters["@count"].Direction = ParameterDirection.Output;
                            con.Open();
                            SqlDataReader reader = cmd.ExecuteReader();
                            while (reader.Read())
                            {
                                DataEntryUserListObject user = new DataEntryUserListObject();
                                user.UserName = Convert.ToString(reader["UserName"]);
                                user.FirstName = Convert.ToString(reader["FirstName"]);
                                user.LastName = Convert.ToString(reader["LastName"]);
                                user.CreatedDate = Convert.ToDateTime(reader["CreatedDate"]).ToString("yyyy/MM/dd");

                                userList.Add(user);
                            }
                            reader.Close();
                            recordCount = Convert.ToInt32(cmd.Parameters["@count"].Value);
                            ActionLogger.Logger.WriteLog("GetDataEntryUsers list  successfully fetched " + "roleIdToView: " + roleIdToView + ", licenseeId: " + licenseeId + "listParams:" + listParams, true);
                        }
                    }
                }
                catch (Exception ex)
                {
                    ActionLogger.Logger.WriteLog(" exception while fetching the GDataEntryUsersList: " + "roleIdToView:" + roleIdToView + "ex:" + ex.Message, true);
                    throw ex;
                }
                return userList;
            }
        }



        /// <summary>
        /// Created By: Ankit khandelwal
        /// Created on: Sept 04, 2018
        /// Purpose: To update the HouseAccount of user
        /// </summary>
        /// <param name="userCredentialId"></param>
        /// <param name="licenseeId"></param>
        /// <returns></returns>
        ///
        public static string SetHouseAccountValue(Guid userCredentialId, Guid licenseeId)
        {
            try
            {
                using (DLinq.CommissionDepartmentEntities DataModel = Entity.DataModel)
                {

                    ActionLogger.Logger.WriteLog("SetHouseAccountValue processing begins " + "userCredentialId: " + userCredentialId + ", licenseeId: " + licenseeId, true);
                    try
                    {
                        DLinq.CommissionDepartmentEntities ctx = new DLinq.CommissionDepartmentEntities();
                        EntityConnection ec = (EntityConnection)ctx.Connection;
                        SqlConnection sc = (SqlConnection)ec.StoreConnection; //get the SQLConnection that your entity object would use
                        string adoConnStr = sc.ConnectionString;
                        using (SqlConnection con = new SqlConnection(adoConnStr))
                        {
                            using (SqlCommand cmd = new SqlCommand("usp_UpdateHouseAccount", con))
                            {
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Parameters.AddWithValue("@licenseeId", licenseeId);
                                cmd.Parameters.AddWithValue("@userCredentialId", userCredentialId);

                                con.Open();
                                string isSuccess = Convert.ToString(cmd.ExecuteScalar());
                                ActionLogger.Logger.WriteLog("HouseAccount is successfully update to UsercredentialId: " + userCredentialId, true);
                                return isSuccess;
                            }

                        }
                    }
                    catch (Exception ex)
                    {
                        ActionLogger.Logger.WriteLog(" exception while updating the HouseAccount: " + userCredentialId + ", ex: " + ex.Message, true);

                    }
                    return null;

                }

            }

            catch (Exception ex)
            {
                ActionLogger.Logger.WriteLog(" exception while updating the HouseAccount: " + "userCredentialId:" + userCredentialId + "licenseeId:" + licenseeId + ", ex: " + ex.Message, true);
                throw ex;
            }



        }

        /// <summary>
        /// Created By: Ankit khandelwal
        /// Created on: Sept 05, 2018
        /// Purpose: To get the user detail
        /// </summary>
        /// <param name="userCredentialId"></param>
        /// <returns></returns>
        public static User GetUserDetailIdWise(Guid userCredentialId)
        {
            using (DLinq.CommissionDepartmentEntities DataModel = Entity.DataModel)
            {
                try
                {
                    ActionLogger.Logger.WriteLog("GetUserDetailIdWise processing begins " + "userCredentialId: " + userCredentialId, true);
                    User _user = (from uc in DataModel.UserCredentials
                                  where (uc.UserCredentialId == userCredentialId) && (uc.IsDeleted == false)
                                  select new User
                                  {
                                      UserCredentialID = uc.UserCredentialId,
                                      FirstName = uc.UserDetail.FirstName,
                                      LastName = uc.UserDetail.LastName,
                                      NickName = uc.UserDetail.NickName,
                                      Address = uc.UserDetail.Address,
                                      OfficePhone = uc.UserDetail.OfficePhone == null ? "" : uc.UserDetail.OfficePhone,
                                      CellPhone = uc.UserDetail.CellPhone == null ? "" : uc.UserDetail.CellPhone,
                                      Fax = uc.UserDetail.Fax == null ? "" : uc.UserDetail.Fax,
                                      Email = uc.UserDetail.Email,
                                      City = uc.UserDetail.City,
                                      Company = uc.UserDetail.Company,
                                      State = uc.UserDetail.State,
                                      LicenseeId = uc.Licensee.LicenseeId,
                                      LicenseeName = uc.Licensee.Company,
                                      UserName = uc.UserName,
                                      Password = uc.Password,
                                      PasswordHintQ = uc.PasswordHintQuestion,
                                      PasswordHintA = uc.PasswordHintAnswer,
                                      IsHouseAccount = uc.IsHouseAccount,
                                      IsAccountExec = uc.IsAccountExec,
                                      DisableAgentEditing = uc.UserDetail.DisableAgentEditing,
                                      FirstYearDefault = uc.UserDetail.FirstYearDefault,
                                      RenewalDefault = uc.UserDetail.RenewalDefault,
                                      Role = (UserRole)uc.MasterRole.RoleId,
                                      IsNewsToFlash = uc.IsNewsToFlash,
                                      CreatedBy = uc.CreatedBy,
                                      CreatedOn = uc.CreatedOn,
                                      IsUserActiveOnWeb = uc.IsUserActiveOnWeb,
                                      FormattedAddress = uc.UserDetail.FormattedAddress,
                                      Place_Id = uc.UserDetail.Place_Id,
                                      CellPhone_DialCode = uc.UserDetail.CellPhone_DialCode == null ? "1" : uc.UserDetail.CellPhone_DialCode,
                                      OfficePhone_DialCode = uc.UserDetail.OfficePhone_DialCode == null ? "1" : uc.UserDetail.OfficePhone_DialCode,
                                      Fax_DialCode = uc.UserDetail.Fax_DialCode == null ? "1" : uc.UserDetail.Fax_DialCode,
                                      CellPhone_CountryCode = uc.UserDetail.CellPhone_CountryCode == null ? "us" : uc.UserDetail.CellPhone_CountryCode,
                                      OfficePhone_CountryCode = uc.UserDetail.OfficePhone_CountryCode == null ? "us" : uc.UserDetail.OfficePhone_CountryCode,
                                      Fax_CountryCode = uc.UserDetail.Fax_CountryCode == null ? "us" : uc.UserDetail.Fax_CountryCode,
                                      IsAdmin = uc.IsAdmin == null ? false : uc.IsAdmin,
                                      BGUserId = uc.BGUserId


                                  }
                             ).FirstOrDefault();

                    _user.OfficePhoneNumber = new ContactDetails();
                    _user.MobileNumber = new ContactDetails();
                    _user.FaxNumber = new ContactDetails();
                    _user.OfficePhoneNumber.DialCode = _user.OfficePhone_DialCode;
                    _user.OfficePhoneNumber.CountryCode = _user.OfficePhone_CountryCode;
                    _user.OfficePhoneNumber.PhoneNumber = _user.OfficePhone;



                    _user.MobileNumber.DialCode = _user.CellPhone_DialCode;
                    _user.MobileNumber.CountryCode = _user.CellPhone_CountryCode;
                    _user.MobileNumber.PhoneNumber = _user.CellPhone;


                    _user.FaxNumber.DialCode = _user.Fax_DialCode;
                    _user.FaxNumber.CountryCode = _user.Fax_CountryCode;
                    _user.FaxNumber.PhoneNumber = _user.Fax;


                    _user.HasAssociatedPolicies = IsAccountExecPolicyExist(userCredentialId, _user.NickName);

                    if (_user.Password != null && _user.IsUserActiveOnWeb == true || (_user.Password != null && _user.Role == (UserRole)4 && _user.IsUserActiveOnWeb == true))
                    {
                        string key = System.Configuration.ConfigurationSettings.AppSettings["AESKey"];
                        string iv = System.Configuration.ConfigurationSettings.AppSettings["AESIV"];
                        try
                        {
                            byte[] DecryptPassword = Convert.FromBase64String(_user.Password);
                            string encryptPassword = AESEncrypt.DecryptStringFromBytes_Aes(DecryptPassword, Encoding.UTF8.GetBytes(key), Encoding.UTF8.GetBytes(iv));
                            _user.Password = encryptPassword;
                        }
                        catch(Exception ex)
                        {
                            ActionLogger.Logger.WriteLog("GetUserDetailIdWise password decryption failed, so retaining original , exception: " + ex.Message, true);
                        }

                        ActionLogger.Logger.WriteLog("GetUserDetailIdWise processing Ends " + "userCredentialId: " + userCredentialId, true);
                        return _user;
                    }
                    else
                    {
                        return _user;
                    }

                }
                catch (Exception ex)
                {
                    ActionLogger.Logger.WriteLog(" exception while fetching the user details: " + "userCredentialId" + userCredentialId + ", ex: " + ex.Message, true);
                }
                return null;

            }
        }

        /// <summary>
        /// Created By:Ankit Khandelwal
        /// Created On:24-06-2019
        /// Purpose:Check Is policy exist with Accountexec
        /// </summary>
        /// <param name="usercredentialId"></param>
        /// <param name="nickName"></param>
        /// <returns></returns>
        public static Boolean IsAccountExecPolicyExist(Guid? usercredentialId, string nickName)
        {
            Boolean result = false;
            try
            {
                ActionLogger.Logger.WriteLog("IsAccountExecPolicyExist processing begins " + "nickName: " + nickName, true);
                using (SqlConnection con = new SqlConnection(DBConnection.GetConnectionString()))
                {
                    using (SqlCommand cmd = new SqlCommand("IsAccountExecPolicyExist", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@userCredentialId", usercredentialId);
                        cmd.Parameters.AddWithValue("@nickName", nickName);
                        cmd.Parameters.Add("@isPolicyExist", SqlDbType.Bit);
                        cmd.Parameters["@isPolicyExist"].Direction = ParameterDirection.Output;
                        con.Open();
                        cmd.ExecuteScalar();
                        //string strResult = Convert.ToString(cmd.ExecuteScalar());
                        result = Convert.ToBoolean(cmd.Parameters["@isPolicyExist"].Value);
                    }
                }

            }
            catch (Exception ex)
            {
                ActionLogger.Logger.WriteLog("IsAccountExecPolicyExist:Exception occurs while processing: " + usercredentialId + "Exception:" + ex.Message, true);
            }
            return result;
        }

        /// <summary>
        /// Created By: Ankit khandelwal
        /// Created on: Sept 06, 2018
        /// Purpose: To Add or update user detail and also check username exist or not 
        /// </summary>
        /// <param name="userDetails"></param>
        /// <returns></returns>

        public static string AddUpdateUser(User userDetails)
        {
            string isUserCredentialIdExist = null;
            try
            {

                // Guid guidUserID;
                bool isUserNameExist;
                bool isemailexist;
                using (DLinq.CommissionDepartmentEntities DataModel = Entity.DataModel)
                {
                    isUserNameExist = IsUserNameExist(userDetails.UserCredentialID, userDetails.UserName);
                    isemailexist = DataModel.UserDetails.Any(s => s.Email == userDetails.Email && s.UserCredential.IsDeleted == false && s.UserCredentialId != userDetails.UserCredentialID);

                    /* if (isUserNameExist == true /*&& isemailexist == true)
                     {
                         isUserCredentialIdExist = "2";
                         return isUserCredentialIdExist;
                     }
                     else */
                    if (isUserNameExist == true)
                    {
                        isUserCredentialIdExist = "4";
                        return isUserCredentialIdExist;


                    }
                    //else if (isemailexist == true)
                    //{
                    //    isUserCredentialIdExist = "3";
                    //    return isUserCredentialIdExist;
                    //}
                    ActionLogger.Logger.WriteLog("AddUpdateUser processing begins " + "userCredentialId: " + userDetails.ToStringDump(), true);
                    var _dtUsers = DataModel.UserCredentials.FirstOrDefault(s => s.UserCredentialId == userDetails.UserCredentialID);
                    try
                    {
                        if (_dtUsers == null)
                        {

                            _dtUsers = new DLinq.UserCredential
                            {
                                UserCredentialId = ((userDetails.UserCredentialID == Guid.Empty) ? Guid.NewGuid() : userDetails.UserCredentialID),
                                UserName = userDetails.UserName,
                                Password = userDetails.Password,
                                PasswordHintAnswer = userDetails.PasswordHintA,
                                PasswordHintQuestion = userDetails.PasswordHintQ,
                                RoleId = (int)userDetails.Role,
                                LicenseeId = userDetails.LicenseeId,
                                CreatedOn = DateTime.Now,
                                ModifiedOn = DateTime.Now,
                                ModifiedBy = userDetails.ModifiedBy,
                                CreatedBy = (Guid)userDetails.CreatedBy,
                                IsUserActiveOnWeb = true,
                                BGUserId = userDetails.BGUserId


                            };
                            if (!string.IsNullOrEmpty(userDetails.Password))
                            {
                                string key = System.Configuration.ConfigurationSettings.AppSettings["AESKey"];
                                string iv = System.Configuration.ConfigurationSettings.AppSettings["AESIV"];

                                byte[] encryptPassword = AESEncrypt.EncryptStringToBytes_Aes(userDetails.Password, Encoding.UTF8.GetBytes(key), Encoding.UTF8.GetBytes(iv));

                                string strPassword = Convert.ToBase64String(encryptPassword);
                                userDetails.Password = strPassword;
                                _dtUsers.Password = userDetails.Password;
                            }
                            ActionLogger.Logger.WriteLog("AddUpdateUser processing New Usercrdential Id aasigned to use is: " + "userCredentialId: " + userDetails.UserCredentialID, true);
                            isUserCredentialIdExist = "0";
                            DataModel.AddToUserCredentials(_dtUsers);
                        }
                        else
                        {
                            ActionLogger.Logger.WriteLog("User Add/update Step 1 ", true);
                            _dtUsers.UserName = userDetails.UserName;
                            _dtUsers.Password = userDetails.Password;
                            _dtUsers.PasswordHintAnswer = userDetails.PasswordHintA;
                            _dtUsers.PasswordHintQuestion = userDetails.PasswordHintQ;
                            _dtUsers.ModifiedBy = userDetails.ModifiedBy;
                            _dtUsers.ModifiedOn = DateTime.Now;
                            _dtUsers.IsUserActiveOnWeb = true;
                            _dtUsers.BGUserId = userDetails.BGUserId;

                            if (!string.IsNullOrEmpty(userDetails.Password))
                            {
                                ActionLogger.Logger.WriteLog("User Add/update Step 2 ", true);
                                string key = System.Configuration.ConfigurationSettings.AppSettings["AESKey"];
                                string iv = System.Configuration.ConfigurationSettings.AppSettings["AESIV"];
                                byte[] encryptPassword = AESEncrypt.EncryptStringToBytes_Aes(userDetails.Password, Encoding.UTF8.GetBytes(key), Encoding.UTF8.GetBytes(iv));

                                string strPassword = Convert.ToBase64String(encryptPassword);
                                _dtUsers.Password = strPassword;
                                ActionLogger.Logger.WriteLog("User Add/update Step 3 ", true);

                            }
                            isUserCredentialIdExist = "1";
                        }
                    }
                    catch (Exception ex)
                    {
                        ActionLogger.Logger.WriteLog(" exception while update user details: " + "userCredentialId" + userDetails.UserCredentialID + ", ex: " + ex.Message, true);

                        throw ex;
                    }

                    ActionLogger.Logger.WriteLog("User Add/update Step 4 ", true);

                    var _dtUserDetail = DataModel.UserDetails.FirstOrDefault(s => s.UserCredentialId == userDetails.UserCredentialID);
                    if (_dtUserDetail == null)
                    {
                        ActionLogger.Logger.WriteLog("User Add/update Step5 ", true);

                        _dtUserDetail = new DLinq.UserDetail
                        {
                            UserCredentialId = _dtUsers.UserCredentialId,
                            FirstName = userDetails.FirstName,
                            LastName = userDetails.LastName,
                            Company = userDetails.Company,
                            NickName = userDetails.NickName,
                            Address = String.IsNullOrEmpty(userDetails.FormattedAddress) ? "" : userDetails.FormattedAddress.Length > 50 ? userDetails.FormattedAddress.Substring(0, 49) : userDetails.FormattedAddress, // this line is used for saving the formatted adress upto 50 charcters
                            ZipCode = userDetails.ZipCode.CustomParseToLong(),
                            City = userDetails.City,
                            State = userDetails.State,
                            Email = userDetails.Email,
                            FirstYearDefault = userDetails.FirstYearDefault,
                            RenewalDefault = userDetails.RenewalDefault,
                            ReportForEntireAgency = userDetails.ReportForEntireAgency,
                            ReportForOwnBusiness = userDetails.ReportForOwnBusiness,
                            OfficePhone = userDetails.OfficePhoneNumber != null ? userDetails.OfficePhoneNumber.PhoneNumber : "",
                            CellPhone = userDetails.MobileNumber != null ? userDetails.MobileNumber.PhoneNumber : "",
                            Fax = userDetails.FaxNumber != null ? userDetails.FaxNumber.PhoneNumber : "",
                            FormattedAddress = userDetails.FormattedAddress,
                            Place_Id = userDetails.Place_Id,
                            CellPhone_DialCode = userDetails.MobileNumber != null ? userDetails.MobileNumber.DialCode : "",
                            OfficePhone_DialCode = userDetails.OfficePhoneNumber != null ? userDetails.OfficePhoneNumber.DialCode : "",
                            Fax_DialCode = userDetails.FaxNumber != null ? userDetails.FaxNumber.DialCode : "",
                            CellPhone_CountryCode = userDetails.MobileNumber != null ? userDetails.MobileNumber.CountryCode : "",
                            OfficePhone_CountryCode = userDetails.OfficePhoneNumber != null ? userDetails.OfficePhoneNumber.CountryCode : "",
                            Fax_CountryCode = userDetails.FaxNumber != null ? userDetails.FaxNumber.CountryCode : "",
                            AddPayeeOn = null
                        };
                        DataModel.AddToUserDetails(_dtUserDetail);
                    }
                    else
                    {
                        ActionLogger.Logger.WriteLog("User Add/update Step 6 ", true);

                        _dtUserDetail.FirstName = userDetails.FirstName;
                        _dtUserDetail.LastName = userDetails.LastName;
                        _dtUserDetail.Company = userDetails.Company;
                        _dtUserDetail.NickName = userDetails.NickName;
                        _dtUserDetail.Address = String.IsNullOrEmpty(userDetails.FormattedAddress) ? "" : userDetails.FormattedAddress.Length > 50 ? userDetails.FormattedAddress.Substring(0, 49) : userDetails.FormattedAddress;
                        _dtUserDetail.ZipCode = userDetails.ZipCode.CustomParseToLong();
                        _dtUserDetail.City = userDetails.City;
                        _dtUserDetail.State = userDetails.State;
                        _dtUserDetail.Email = userDetails.Email;
                        _dtUserDetail.OfficePhone = userDetails.OfficePhoneNumber != null ? userDetails.OfficePhoneNumber.PhoneNumber : "";
                        _dtUserDetail.CellPhone = userDetails.MobileNumber != null ? userDetails.MobileNumber.PhoneNumber : "";
                        _dtUserDetail.Fax = userDetails.FaxNumber != null ? userDetails.FaxNumber.PhoneNumber : "";
                        _dtUserDetail.FormattedAddress = userDetails.FormattedAddress;
                        _dtUserDetail.Place_Id = userDetails.Place_Id;
                        _dtUserDetail.CellPhone_DialCode = userDetails.MobileNumber != null ? userDetails.MobileNumber.DialCode : "";
                        _dtUserDetail.OfficePhone_DialCode = userDetails.OfficePhoneNumber != null ? userDetails.OfficePhoneNumber.DialCode : "";
                        _dtUserDetail.Fax_DialCode = userDetails.FaxNumber != null ? userDetails.FaxNumber.DialCode : "";
                        _dtUserDetail.CellPhone_CountryCode = userDetails.MobileNumber != null ? userDetails.MobileNumber.CountryCode : "";
                        _dtUserDetail.OfficePhone_CountryCode = userDetails.OfficePhoneNumber != null ? userDetails.OfficePhoneNumber.CountryCode : "";
                        _dtUserDetail.Fax_CountryCode = userDetails.FaxNumber != null ? userDetails.FaxNumber.CountryCode : "";
                    }

                    DataModel.SaveChanges();
                    ActionLogger.Logger.WriteLog("User Add/update Step 7 ", true);

                    if (userDetails.UserCredentialID == Guid.Empty)
                    {
                        SaveDefaultUserPermissions(_dtUserDetail.UserCredentialId, _dtUsers.RoleId);
                        ActionLogger.Logger.WriteLog("User Add/update Step 8 ", true);

                    }
                }
            }
            catch (Exception ex)
            {
                ActionLogger.Logger.WriteLog("User Add/update Step 9 ", true);

                ActionLogger.Logger.WriteLog(" Exception  occurs  while update user details: " + "userCredentialId" + userDetails.UserCredentialID + ", ex: " + ex.Message, true);
                if (ex.InnerException != null)
                {
                    ActionLogger.Logger.WriteLog(" AddupdateUserInner ex: " + ex.InnerException.Message, true);
                }
                throw ex;
            }
            return isUserCredentialIdExist;
        }
        /// <summary>
        ///Created By: Ankit khandelwal
        /// Created on: Sept 06, 2018
        /// Purpose: To save Default permission to user 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="userName"></param>
        /// <returns></returns>

        public static void SaveDefaultUserPermissions(Guid userCredentialId, int roleId)
        {
            try
            {
                DLinq.CommissionDepartmentEntities ctx = new DLinq.CommissionDepartmentEntities(); //create your entity object here
                EntityConnection ec = (EntityConnection)ctx.Connection;
                SqlConnection sc = (SqlConnection)ec.StoreConnection; //get the SQLConnection that your entity object would use
                string adoConnStr = sc.ConnectionString;
                using (SqlConnection con = new SqlConnection(adoConnStr))
                {
                    using (SqlCommand cmd = new SqlCommand("usp_AddDefaultUserPermission", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@userCredentialId", userCredentialId);
                        cmd.Parameters.AddWithValue("@roleId", roleId);
                        con.Open();
                        cmd.ExecuteNonQuery();
                    }
                }

            }
            catch (Exception ex)
            {
                ActionLogger.Logger.WriteLog("SaveDefaultUserPermissions exception: " + ex.Message, true);
                throw ex;
            }
        }
        /// <summary>
        ///Created By: Ankit khandelwal
        /// Created on: Sept 06, 2018
        /// Purpose: To check UserName exist or not 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        public static bool IsUserNameExist(Guid userId, string userName)
        {
            bool userNameExist = false;
            using (DLinq.CommissionDepartmentEntities DataModel = Entity.DataModel)
            {
                if (DataModel.UserCredentials.Any(s => s.UserCredentialId == userId))
                    userNameExist = DataModel.UserCredentials.Any(s => s.UserName.ToUpper() == userName.ToUpper() && s.IsDeleted == false && s.UserCredentialId != userId);
                else
                    userNameExist = DataModel.UserCredentials.Any(s => s.UserName.ToUpper() == userName.ToUpper() && s.IsDeleted == false);
            }
            return userNameExist;
        }
        /// <summary>
        /// Created By: Ankit khandelwal
        /// Created on: Sept 11, 2018
        /// Purpose: To Add/Update  UserPermissions
        /// </summary>
        /// <param name="userDetails"></param>
        /// <param name="userCredentialId"></param>
        /// <param name="isvalidUsercredntialId"></param>
        public static void AddUpdateUserPermission(User userDetails, Guid userCredentialId, out string isvalidUsercredntialId)
        {
            try
            {
                ActionLogger.Logger.WriteLog("AddUpdateUserPermission processing begins " + "userCredentialId: " + userCredentialId, true);
                Guid guidUserID;
                guidUserID = userCredentialId;
                if (userCredentialId == userDetails._permissions[0].UserCredentialId)
                {
                    foreach (UserPermissions userPermissions in userDetails._permissions)
                    {
                        if (userPermissions.UserCredentialId != null)
                        {
                            if (userPermissions.UserCredentialId == Guid.Empty)
                            {
                                userPermissions.UserCredentialId = guidUserID;
                            }

                            else
                            {
                                userPermissions.UserCredentialId = guidUserID;
                            }
                        }
                    }
                    using (DLinq.CommissionDepartmentEntities DataModel = Entity.DataModel)
                    {
                        DLinq.UserDetail userDetail = DataModel.UserDetails.FirstOrDefault(s => s.UserCredentialId == userCredentialId);

                        if (userDetail != null)
                        {
                            ActionLogger.Logger.WriteLog("AddUpdateUserPermission processing " + "userCredentialId: " + userCredentialId, true);
                            userDetail.DisableAgentEditing = userDetails.DisableAgentEditing;
                            userDetail.RenewalDefault = userDetails.RenewalDefault;
                            userDetail.FirstYearDefault = userDetails.FirstYearDefault;
                            userDetail.ReportForEntireAgency = userDetails.ReportForEntireAgency;
                            userDetail.ReportForOwnBusiness = userDetails.ReportForOwnBusiness;
                            DataModel.SaveChanges();
                        }
                    }
                    bool bvalue = false;
                    if (userDetails.IsAccountExec == true)
                    {
                        bvalue = true;
                    }
                    try
                    {
                        ActionLogger.Logger.WriteLog("AddUpdateUserPermission processing is inprogress " + "userCredentialId: " + userCredentialId, true);
                        UpdateAccountExec(userCredentialId, bvalue);
                        UpdateUserPermission(userDetails);
                        isvalidUsercredntialId = "1";
                    }
                    catch (Exception ex)

                    {
                        throw ex;
                    }
                }
                else
                {
                    isvalidUsercredntialId = "0";
                }

            }
            catch (Exception ex)

            {
                ActionLogger.Logger.WriteLog(" Exception  occurs  AddUpdateUserPermission: " + "userCredentialId" + userCredentialId + ", ex: " + ex.Message, true);
                throw ex;

            }
        }
        /// <summary>
        /// Created By: Ankit khandelwal
        /// Created on: Sept 11, 2018
        /// Purpose: To Add/Update  UserPermissions
        /// </summary>
        /// <param name="userCredentialId"></param>
        /// <param name="bvalue"></param>
        public static void UpdateAccountExec(Guid userCredentialId, bool isAccountExec)
        {
            try
            {
                ActionLogger.Logger.WriteLog("UpdateAccountExec processing begins " + "userCredentialId: " + userCredentialId + "isAccountExec" + isAccountExec, true);
                if (CheckAccoutExec(userCredentialId))
                {
                    return;
                }
                else
                {
                    using (DLinq.CommissionDepartmentEntities DataModel = Entity.DataModel)
                    {
                        DLinq.UserCredential userCredential = DataModel.UserCredentials.FirstOrDefault(s => s.UserCredentialId == userCredentialId);

                        if (userCredential != null)
                        {
                            userCredential.IsAccountExec = isAccountExec;
                            DataModel.SaveChanges();
                            ActionLogger.Logger.WriteLog("UpdateAccountExec success " + "userCredentialId: " + userCredentialId, true);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ActionLogger.Logger.WriteLog(" Exception  occurs  while update user details: " + "userCredentialId" + userCredentialId + ", ex: " + ex.Message, true);
                throw ex;
            }
        }
        /// <summary>
        /// Created By: Ankit khandelwal
        /// Created on: Sept 11, 2018
        /// Purpose: To Add/Update  UserPermissions
        /// </summary>
        /// <param name="userCredencialID"></param>
        /// <returns></returns>
        public static bool CheckAccoutExec(Guid userCredentialId)
        {
            bool bValue = false;
            try
            {
                ActionLogger.Logger.WriteLog("CheckAccoutExec processing begins " + "userCredentialId: " + userCredentialId, true);
                using (DLinq.CommissionDepartmentEntities DataModel = Entity.DataModel)
                {
                    DLinq.UserCredential UserCredential = DataModel.UserCredentials.FirstOrDefault(s => s.UserCredentialId == userCredentialId);

                    if (UserCredential != null)
                    {
                        UserCredential.IsAccountExec = true;
                        DataModel.SaveChanges();
                        ActionLogger.Logger.WriteLog("CheckAccoutExec success " + "userCredentialId: " + userCredentialId, true);
                    }
                }
            }
            catch (Exception ex)
            {
                ActionLogger.Logger.WriteLog(" Exception  occurs in  CheckAccoutExec: " + "userCredentialId" + userCredentialId + ", ex: " + ex.Message, true);
                throw ex;
            }

            return bValue;
        }
        /// <summary>
        /// Created By: Ankit khandelwal
        /// Created on: Sept 10, 2018
        /// Purpose: To Add/Update UserPermissions 
        /// </summary>
        /// <param name="userDetails"></param>
        public static void UpdateUserPermission(User userDetails)
        {
            try
            {
                ActionLogger.Logger.WriteLog("UpdateUserPermission processing begins " + "userCredentialId: " + userDetails.UserCredentialID, true);
                if (userDetails._permissions == null)
                    return;

                using (DLinq.CommissionDepartmentEntities DataModel = Entity.DataModel)
                {
                    DLinq.UserPermission _permision = null;

                    foreach (UserPermissions up in userDetails._permissions)
                    {
                        DLinq.MasterAccessRight _Right = ReferenceMaster.GetReferencedMasterAccessRight(up.Permission, DataModel);
                        DLinq.MasterModule _Module = ReferenceMaster.GetReferencedMasterModule(up.Module, DataModel);
                        DLinq.UserCredential _UC = ReferenceMaster.GetreferencedUserCredential(up.UserCredentialId.Value, DataModel);

                        _permision = (from p in DataModel.UserPermissions where p.UserPermissionId == up.UserPermissionId select p).FirstOrDefault();
                        if (_permision != null)
                        {
                            _permision.MasterAccessRightReference.Value = _Right;
                            _permision.UserCredentialReference.Value = _UC;
                            _permision.MasterModuleReference.Value = _Module;
                        }
                        else
                        {
                            _permision = new DLinq.UserPermission();
                            _permision.MasterAccessRightReference.Value = _Right;
                            _permision.UserCredentialReference.Value = _UC;
                            _permision.MasterModuleReference.Value = _Module;
                            _permision.UserPermissionId = up.UserPermissionId.Value;
                            DataModel.AddToUserPermissions(_permision);

                        }

                        ActionLogger.Logger.WriteLog("UpdateUserPermission success " + "userCredentialId: " + userDetails.UserCredentialID, true);
                    }
                    DataModel.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                ActionLogger.Logger.WriteLog(" Exception  occurs in  UpdateUserPermission: " + "userCredentialId" + userDetails.UserCredentialID + ", ex: " + ex.Message, true);
                throw ex;
            }
        }
        /// <summary>
        /// Created By: Ankit khandelwal
        /// Created on: Sept 10, 2018
        /// Purpose: To Add/Update User mapping list 
        /// </summary>
        /// <param name="linkedUserList"></param>
        /// <param name="loggedInUserId"></param>
        public static void SaveLinkedUsers(List<LinkedUser> linkedUserList, Guid loggedInUserId)
        {
            try
            {
                ActionLogger.Logger.WriteLog("SaveLinkedUsers processing begins " + "loggedInuserCredentialId: " + loggedInUserId, true);
                DLinq.UserCredential linkedUser;
                using (DLinq.CommissionDepartmentEntities DataModel = Entity.DataModel)
                {
                    DLinq.UserCredential user = DataModel.UserCredentials.FirstOrDefault(s => s.UserCredentialId == loggedInUserId && s.IsDeleted == false);
                    user.UserCredentials.Clear();
                    foreach (LinkedUser agent in linkedUserList)
                    {
                        if (agent.IsConnected == true)
                        {
                            ActionLogger.Logger.WriteLog("SaveLinkedUsers link to users " + "linkedUserId: " + agent.UserCredentialId, true);
                            linkedUser = ReferenceMaster.GetreferencedUserCredential(agent.UserCredentialId, DataModel);
                            user.UserCredentials.Add(linkedUser);

                        }
                    }
                    DataModel.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                ActionLogger.Logger.WriteLog(" exception in saving User list: " + "loggedInuserCredentialId:" + loggedInUserId + ", ex: " + ex.Message, true);
                throw ex;
            }
        }

        /// <summary>
        /// Created By: Ankit khandelwal
        /// Created on: Jan 01, 2019
        /// Purpose: To Add/Update AdminStatus and Rights of Admin /// </summary>
        /// <param name="userCredentialId"></param>
        /// <param name="adminStatus"></param>
        /// <returns></returns>
        public static int AddUpdateAdminStatus(Guid userCredentialId, bool adminStatus)
        {
            ActionLogger.Logger.WriteLog("AddUpdateAdminStatus processing begins " + "userCredentialId: " + userCredentialId, true);
            int success = 0;
            DLinq.CommissionDepartmentEntities ctx = new DLinq.CommissionDepartmentEntities(); //create your entity object here
            EntityConnection ec = (EntityConnection)ctx.Connection;
            SqlConnection sc = (SqlConnection)ec.StoreConnection; //get the SQLConnection that your entity object would use
            string adoConnStr = sc.ConnectionString;
            using (SqlConnection con = new SqlConnection(adoConnStr))
            {
                try
                {
                    using (SqlCommand cmd = new SqlCommand("usp_AddUpdateAdminStatus", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@userCredentialId", userCredentialId);
                        cmd.Parameters.AddWithValue("@adminStatus", adminStatus);
                        con.Open();
                        cmd.ExecuteNonQuery();
                        success = 1;
                    }
                    ActionLogger.Logger.WriteLog("AddUpdateAdminStatus success " + "userCredentialId: " + userCredentialId, true);
                }
                catch (Exception ex)
                {
                    ActionLogger.Logger.WriteLog("AddUpdateAdminStatus:Exception Occurs while Addupdate details" + "userCredentialId: " + userCredentialId + "Exception:" + ex.Message, true);
                }
            }
            return success;
        }
        /// <summary>
        /// Created By: Ankit khandelwal
        /// Created on: Sept 10, 2018 
        /// Purpose: To Get  the  linked User list based on userCredentialId
        /// </summary>
        /// <param name="userCredentialId"></param>
        /// <param name="listParams"></param>
        /// <param name="recordCount"></param>
        /// <returns></returns>
        public List<LinkedUser> GetAllLinkedUser(Guid userCredentialId, ListParams listParams, out int recordCount)
        {

            try
            {
                ActionLogger.Logger.WriteLog("GetAllLinkedUser processing begins " + "userCredentialId: " + userCredentialId, true);
                List<LinkedUser> userList = new List<LinkedUser>();
                DLinq.CommissionDepartmentEntities ctx = new DLinq.CommissionDepartmentEntities(); //create your entity object here
                EntityConnection ec = (EntityConnection)ctx.Connection;
                SqlConnection sc = (SqlConnection)ec.StoreConnection; //get the SQLConnection that your entity object would use
                string adoConnStr = sc.ConnectionString;
                using (SqlConnection con = new SqlConnection(adoConnStr))
                {
                    int rowStart = (listParams.pageSize * (listParams.pageIndex)) + 1;
                    int rowEnd = (listParams.pageIndex + 1) * listParams.pageSize;
                    ActionLogger.Logger.WriteLog("GetUsers processing begins " + ", userCredentialId: " + userCredentialId + "listParams:", true);
                    using (SqlCommand cmd = new SqlCommand("usp_GetLinkedUserList", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@userCredentialId", userCredentialId);
                        cmd.Parameters.AddWithValue("@rowStart", rowStart);
                        cmd.Parameters.AddWithValue("@rowEnd", rowEnd);
                        cmd.Parameters.AddWithValue("@filterBy", listParams.filterBy);
                        cmd.Parameters.AddWithValue("@sortType", listParams.sortType);
                        cmd.Parameters.AddWithValue("@sortBy", listParams.sortBy);
                        cmd.Parameters.Add("@count", SqlDbType.Int);
                        cmd.Parameters["@count"].Direction = ParameterDirection.Output;
                        con.Open();
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            LinkedUser user = new LinkedUser();
                            user.NickName = Convert.ToString(reader["NickName"]);
                            user.FirstName = Convert.ToString(reader["FirstName"]);
                            user.LastName = Convert.ToString(reader["LastName"]);
                            user.Email = Convert.ToString(reader["Email"]);
                            user.CreatedDate = Convert.ToString(reader["CreatedDate"]);
                            user.IsConnected = Convert.ToBoolean(reader["IsConnecteduser"]);
                            user.UserCredentialId = (Guid)reader["UserCredentialID"];
                            userList.Add(user);
                        }
                        reader.Close();
                        ActionLogger.Logger.WriteLog("GetAllLinkedUser list  fetched successfully " + "userCredentialId: " + userCredentialId, true);
                        recordCount = Convert.ToInt32(cmd.Parameters["@count"].Value);
                        ActionLogger.Logger.WriteLog("GetAllLinkedUser list record  count  fetched successfully " + "userCredentialId: " + userCredentialId + "recordCount" + recordCount, true);


                    }
                    return userList;
                }
            }
            catch (Exception ex)
            {
                ActionLogger.Logger.WriteLog(" exception in GetLinked User List : " + "userCredentialId:" + userCredentialId + ", ex: " + ex.Message, true);
                throw ex;
            }
        }

        #endregion
        public static List<User> GetUsers(UserRole RoleId)
        {
            using (DLinq.CommissionDepartmentEntities DataModel = Entity.DataModel)
            {
                List<User> _usrlist = null;

                _usrlist = (from s in DataModel.UserCredentials
                            where (s.RoleId == (int)RoleId) && (s.IsDeleted == false) && ((4 == (int)RoleId) || s.Licensee.IsDeleted == false)
                            select new User
                            {
                                UserCredentialID = s.UserCredentialId,
                                IsAccountExec = s.IsAccountExec,
                                UserName = s.UserName,
                                Password = s.Password,
                                FirstName = s.UserDetail.FirstName,
                                LastName = s.UserDetail.LastName,
                                Role = (UserRole)s.MasterRole.RoleId,
                                IsNewsToFlash = s.IsNewsToFlash,
                                IsAdmin = s.IsAdmin
                            }
                        ).ToList();

                foreach (User usr in _usrlist)
                {
                    if (!usr.LicenseeId.IsNullOrEmpty())
                    {
                        usr.HouseOwnerId = DataModel.UserCredentials.First(s => s.LicenseeId == usr.LicenseeId && s.IsHouseAccount == true && s.IsDeleted == false).UserCredentialId;
                        usr.AdminId = DataModel.UserCredentials.First(s => s.LicenseeId == usr.LicenseeId && s.RoleId == 2 && s.IsDeleted == false).UserCredentialId;
                    }
                    else
                    {
                        usr.HouseOwnerId = Guid.Empty;
                        usr.AdminId = Guid.Empty;
                    }
                }
                return _usrlist;
            }
        }

        public static List<LinkedUser> GetLinkedUser(Guid UserCredentialId, UserRole RoleId, bool isHouseValue)
        {
            List<LinkedUser> LnkUsers = new List<LinkedUser>();
            try
            {
                using (DLinq.CommissionDepartmentEntities DataModel = Entity.DataModel)
                {


                    List<DLinq.UserCredential> connectedLinkedUsers = DataModel.UserCredentials.FirstOrDefault(s => s.UserCredentialId == UserCredentialId).UserCredentials.Where(s => s.IsDeleted == false && s.UserCredentialId != UserCredentialId).ToList();

                    if (connectedLinkedUsers != null && connectedLinkedUsers.Count != 0)
                        LnkUsers = (from usr in connectedLinkedUsers
                                    select new LinkedUser
                                    {
                                        UserCredentialId = usr.UserCredentialId,
                                        LastName = usr.UserDetail.LastName,
                                        FirstName = usr.UserDetail.FirstName,
                                        NickName = usr.UserDetail.NickName,
                                        IsConnected = true,
                                        UserName = usr.UserName
                                    }).ToList();

                }

            }

            catch
            {
            }

            return LnkUsers;

        }

        public List<User> GetHouseUsers(Guid LincessID, int intRoleID, bool IsHouseAccount)
        {

            int intHouseAccount = 0;

            if (IsHouseAccount)
            {
                intHouseAccount = 1;
            }

            List<User> lstUser = new List<User>();

            try
            {

                DLinq.CommissionDepartmentEntities ctx = new DLinq.CommissionDepartmentEntities(); //create your entity object here
                EntityConnection ec = (EntityConnection)ctx.Connection;
                SqlConnection sc = (SqlConnection)ec.StoreConnection; //get the SQLConnection that your entity object would use
                string adoConnStr = sc.ConnectionString;

                using (SqlConnection con = new SqlConnection(adoConnStr))
                {
                    using (SqlCommand cmd = new SqlCommand("Usp_GetUserList", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@LicenseeId", LincessID);
                        cmd.Parameters.AddWithValue("@RoleId", intRoleID);
                        cmd.Parameters.AddWithValue("@IsHouseAccount", intHouseAccount);
                        con.Open();

                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            try
                            {
                                User objUser = new User();

                                try
                                {
                                    if (!string.IsNullOrEmpty(Convert.ToString(reader["UserCredentialID"])))
                                    {
                                        objUser.UserCredentialID = reader["UserCredentialID"] == null ? Guid.Empty : (Guid)reader["UserCredentialID"];
                                    }
                                }
                                catch
                                {
                                }

                                try
                                {
                                    if (!string.IsNullOrEmpty(Convert.ToString(reader["UserName"])))
                                    {
                                        objUser.UserName = reader["UserName"] == null ? string.Empty : Convert.ToString(reader["UserName"]);
                                    }
                                }
                                catch
                                {
                                }

                                try
                                {
                                    if (!string.IsNullOrEmpty(Convert.ToString(reader["Password"])))
                                    {
                                        objUser.Password = Convert.ToString(reader["Password"]);
                                    }
                                }
                                catch
                                {
                                }

                                try
                                {
                                    if (!string.IsNullOrEmpty(Convert.ToString(reader["FirstName"])))
                                    {
                                        objUser.FirstName = reader["FirstName"] == null ? string.Empty : Convert.ToString(reader["FirstName"]);
                                    }
                                }
                                catch
                                {
                                }

                                try
                                {
                                    if (!string.IsNullOrEmpty(Convert.ToString(reader["LastName"])))
                                    {
                                        objUser.LastName = reader["LastName"] == null ? string.Empty : Convert.ToString(reader["LastName"]);
                                    }
                                }
                                catch
                                {
                                }

                                try
                                {
                                    if (!string.IsNullOrEmpty(Convert.ToString(reader["RoleId"])))
                                    {
                                        if (IsHouseAccount)
                                        {
                                            objUser.HouseOwnerId = objUser.UserCredentialID;
                                            objUser.IsHouseAccount = true;
                                            objUser.AdminId = null;

                                        }
                                        else
                                        {
                                            objUser.HouseOwnerId = null;
                                            objUser.IsHouseAccount = false;
                                            objUser.AdminId = null;
                                        }
                                    }
                                }
                                catch
                                {
                                }

                                try
                                {
                                    if (!string.IsNullOrEmpty(Convert.ToString(reader["IsNewsToFlash"])))
                                    {
                                        objUser.IsNewsToFlash = Convert.ToBoolean(reader["IsNewsToFlash"]);
                                    }
                                }
                                catch
                                {
                                }

                                lstUser.Add(objUser);
                            }
                            catch
                            {
                            }

                        }
                        reader.Close();
                    }
                }
            }
            catch
            {
            }

            return lstUser;

        }
        public static IEnumerable<User> GetAllUsersByLicChunck(Guid LicenseeId, int skip, int take)
        {
            using (DLinq.CommissionDepartmentEntities DataModel = Entity.DataModel)
            {
                IEnumerable<User> userList = null;

                try
                {
                    DataModel.CommandTimeout = 600000000;

                    userList = (from uc in DataModel.UserCredentials
                                where uc.IsDeleted == false && (uc.LicenseeId == LicenseeId) && uc.RoleId == 3
                                select new User
                                {
                                    UserCredentialID = uc.UserCredentialId,
                                    IsAccountExec = uc.IsAccountExec,
                                    FirstName = uc.UserDetail.FirstName,
                                    LastName = uc.UserDetail.LastName,
                                    //  City = uc.UserDetail.City,
                                    Company = uc.UserDetail.Company,
                                    // State = uc.UserDetail.State,
                                    NickName = uc.UserDetail.NickName,
                                    Email = uc.UserDetail.Email,
                                    Address = uc.UserDetail.Address,
                                    OfficePhone = uc.UserDetail.OfficePhone,
                                    CellPhone = uc.UserDetail.CellPhone,
                                    Fax = uc.UserDetail.Fax,
                                    LicenseeId = uc.Licensee.LicenseeId,
                                    LicenseeName = uc.Licensee.Company,
                                    UserName = uc.UserName,
                                    Password = uc.Password,
                                    DisableAgentEditing = uc.UserDetail.DisableAgentEditing,
                                    PasswordHintQ = uc.PasswordHintQuestion,
                                    PasswordHintA = uc.PasswordHintAnswer,
                                    IsHouseAccount = uc.IsHouseAccount,
                                    FirstYearDefault = uc.UserDetail.FirstYearDefault,
                                    RenewalDefault = uc.UserDetail.RenewalDefault,
                                    Role = (UserRole)uc.MasterRole.RoleId,
                                    IsNewsToFlash = uc.IsNewsToFlash
                                }).ToList();
                }
                catch
                {
                }

                return userList.OrderBy(d => d.NickName).Skip(skip).Take(take).ToList();
            }
        }

        public static List<User> GetAllUsersByChunck(int skip, int take)
        {
            using (DLinq.CommissionDepartmentEntities DataModel = Entity.DataModel)
            {
                DataModel.CommandTimeout = 600000000;

                List<User> _usr = null;

                try
                {

                    _usr = (from uc in DataModel.UserCredentials
                            where uc.IsDeleted == false && uc.UserDetail != null && uc.RoleId == 3
                            select new User
                            {
                                IsHouseAccount = uc.IsHouseAccount,
                                IsAccountExec = uc.IsAccountExec,
                                UserCredentialID = uc.UserCredentialId,
                                FirstName = uc.UserDetail.FirstName,
                                LastName = uc.UserDetail.LastName,
                                // City = uc.UserDetail.City,
                                Company = uc.UserDetail.Company,
                                // State = uc.UserDetail.State,
                                NickName = uc.UserDetail.NickName,
                                Email = uc.UserDetail.Email,
                                Address = uc.UserDetail.Address,
                                OfficePhone = uc.UserDetail.OfficePhone,
                                CellPhone = uc.UserDetail.CellPhone,
                                Fax = uc.UserDetail.Fax,
                                LicenseeId = uc.Licensee.LicenseeId,
                                LicenseeName = uc.Licensee.Company,
                                UserName = uc.UserName,
                                Password = uc.Password,
                                DisableAgentEditing = uc.UserDetail.DisableAgentEditing,
                                PasswordHintQ = uc.PasswordHintQuestion,
                                PasswordHintA = uc.PasswordHintAnswer,
                                FirstYearDefault = uc.UserDetail.FirstYearDefault,
                                RenewalDefault = uc.UserDetail.RenewalDefault,
                                Role = (UserRole)uc.MasterRole.RoleId,
                                IsNewsToFlash = uc.IsNewsToFlash
                            }
                            ).ToList();
                }
                catch
                {
                }

                return _usr.OrderBy(d => d.NickName).Skip(skip).Take(take).ToList();

            }
        }
        /// <summary>
        /// ModifiedBy :Ankit KAhndelwal
        /// ModifiedOn:11-04-2018
        /// Purpose:getting List of Users based on RoleId for Reverse outgoing payment
        /// </summary>
        /// <param name="licenseeId"></param>
        /// <param name="roleIdToView"></param>
        /// <returns></returns>
        public static IEnumerable<User> GetUsers(Guid licenseeId, UserRole roleIdToView)
        {
            ActionLogger.Logger.WriteLog("GetUsers:getting List of users start licenseeId: " + licenseeId, true);
            using (DLinq.CommissionDepartmentEntities DataModel = Entity.DataModel)
            {
                DataModel.CommandTimeout = 600000000;
                IEnumerable<User> userList = null;
                userList = new List<User> { };
                try
                {
                    userList = (from uc in DataModel.UserCredentials
                                where uc.IsDeleted == false && (uc.LicenseeId == licenseeId) && uc.RoleId == (int)roleIdToView
                                select new User
                                {
                                    UserCredentialID = uc.UserCredentialId,
                                    IsAccountExec = uc.IsAccountExec,
                                    FirstName = uc.UserDetail.FirstName,
                                    LastName = uc.UserDetail.LastName,
                                    // City = uc.UserDetail.City,
                                    Company = uc.UserDetail.Company,
                                    // State = uc.UserDetail.State,
                                    NickName = uc.UserDetail.NickName,
                                    Email = uc.UserDetail.Email,
                                    LicenseeId = uc.Licensee.LicenseeId,
                                    LicenseeName = uc.Licensee.Company,
                                    IsHouseAccount = uc.IsHouseAccount,
                                    Role = (UserRole)uc.MasterRole.RoleId,

                                    IsAdmin = uc.IsAdmin
                                }).OrderBy(ud => ud.NickName).ToList();
                }
                catch (Exception ex)
                {
                    ActionLogger.Logger.WriteLog("GetUsers:getting List of users start licenseeId: " + licenseeId + "Exception" + ex.Message, true);
                    throw ex;
                }
                return userList;
            }
        }

        public static IEnumerable<User> GetUsersForReports(Guid LicenseeId)
        {
            using (DLinq.CommissionDepartmentEntities DataModel = Entity.DataModel)
            {
                DataModel.CommandTimeout = 600000000;

                IEnumerable<User> userList = null;
                userList = (from uc in DataModel.UserCredentials
                            where uc.IsDeleted == false && (uc.LicenseeId == LicenseeId)
                            select new User
                            {
                                UserCredentialID = uc.UserCredentialId,
                                FirstName = uc.UserDetail.FirstName,
                                LastName = uc.UserDetail.LastName,
                                NickName = uc.UserDetail.NickName,
                                LicenseeId = uc.Licensee.LicenseeId,
                                Role = (UserRole)uc.MasterRole.RoleId

                            }).ToList();

                return userList;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="LoggedInRoleId"></param>
        /// <param name="RoleIdToView"></param>
        /// <returns></returns>
        /// 
        public static void TurnOnNewsToFlashBit()
        {
            using (DLinq.CommissionDepartmentEntities DataModel = Entity.DataModel)
            {
                List<DLinq.UserCredential> userList = DataModel.UserCredentials.Where(uc => uc.IsDeleted == false && (uc.RoleId == 2 || uc.RoleId == 3) && uc.Licensee.IsDeleted == false).ToList();
                foreach (DLinq.UserCredential usr in userList)
                {
                    usr.IsNewsToFlash = true;
                }
                DataModel.SaveChanges();
            }
        }

        public static void TurnOffNewsToFlashBit(Guid userId)
        {
            using (DLinq.CommissionDepartmentEntities DataModel = Entity.DataModel)
            {
                DLinq.UserCredential userCred = DataModel.UserCredentials.FirstOrDefault(s => s.UserCredentialId == userId);
                if (userCred != null)
                    userCred.IsNewsToFlash = false;

                DataModel.SaveChanges();
            }
        }



        public void ImportHouseUsers(System.Data.DataTable TempTable, Guid LincessID)
        {
            try
            {
                string UserName = string.Empty;
                string Password = string.Empty;
                string Role = string.Empty;
                int RID = 3;
                string PasswordHintQuestion = string.Empty;
                string PasswordHintAnswer = string.Empty;
                bool IsHouseAccount = false;
                bool IsNewsToFlash = false;
                bool IsAccountExec = false;
                string FirstName = string.Empty;
                string LastName = string.Empty;
                string Company = string.Empty;
                string NickName = string.Empty;
                string Address = string.Empty;
                string ZipCode = string.Empty;
                string City = string.Empty;
                string State = string.Empty;
                string Email = string.Empty;
                string OfficePhone = string.Empty;
                string CellPhone = string.Empty;
                string Fax = string.Empty;
                string FirstYearDefault = string.Empty;
                string RenewalDefault = string.Empty;
                bool ReportForEntireAgency = false;
                bool ReportForOwnBusiness = false;
                string AddPayeeOn = string.Empty;
                bool DisableAgentEditing = false;
                string Moduletodisplay = string.Empty;
                int ModuleId = 11; //DEU
                int AccessRightId = 2;
                string AccessRight = string.Empty;

                if (TempTable != null)
                {
                    int intColIndex = TempTable.Columns.Count - 1;

                    for (int i = 0; i < TempTable.Rows.Count; i++)
                    {
                        if (intColIndex >= 0)
                        {
                            try
                            {
                                UserName = Convert.ToString(TempTable.Rows[i][0]);
                            }
                            catch
                            {
                            }
                        }

                        if (intColIndex >= 1)
                        {
                            try
                            {
                                Password = Convert.ToString(TempTable.Rows[i][1]);

                            }
                            catch
                            {
                            }
                        }

                        if (intColIndex >= 2)
                        {
                            try
                            {
                                Role = Convert.ToString(TempTable.Rows[i][2]);
                                RID = RoleID(Role);
                            }
                            catch
                            {
                            }
                        }

                        if (intColIndex >= 3)
                        {
                            try
                            {
                                PasswordHintQuestion = Convert.ToString(TempTable.Rows[i][3]);

                            }
                            catch
                            {
                            }
                        }

                        if (intColIndex >= 4)
                        {
                            try
                            {
                                PasswordHintAnswer = Convert.ToString(TempTable.Rows[i][4]);

                            }
                            catch
                            {
                            }
                        }

                        if (intColIndex >= 5)
                        {
                            try
                            {
                                IsHouseAccount = TrueOrFalse(Convert.ToString(TempTable.Rows[i][5]));
                            }
                            catch
                            {
                            }
                        }

                        if (intColIndex >= 6)
                        {
                            try
                            {
                                IsNewsToFlash = TrueOrFalse(Convert.ToString(TempTable.Rows[i][6]));

                            }
                            catch
                            {
                            }
                        }

                        if (intColIndex >= 7)
                        {
                            try
                            {
                                IsAccountExec = TrueOrFalse(Convert.ToString(TempTable.Rows[i][7]));

                            }
                            catch
                            {
                            }
                        }

                        if (intColIndex >= 8)
                        {
                            try
                            {
                                FirstName = Convert.ToString(TempTable.Rows[i][8]);

                            }
                            catch
                            {
                            }
                        }

                        if (intColIndex >= 9)
                        {
                            try
                            {
                                LastName = Convert.ToString(TempTable.Rows[i][9]);

                            }
                            catch
                            {
                            }
                        }

                        if (intColIndex >= 10)
                        {
                            try
                            {
                                Company = Convert.ToString(TempTable.Rows[i][10]);

                            }
                            catch
                            {
                            }
                        }

                        if (intColIndex >= 11)
                        {
                            try
                            {
                                NickName = Convert.ToString(TempTable.Rows[i][11]);

                            }
                            catch
                            {
                            }
                        }

                        if (intColIndex >= 12)
                        {
                            try
                            {
                                Address = Convert.ToString(TempTable.Rows[i][12]);

                            }
                            catch
                            {
                            }
                        }

                        if (intColIndex >= 13)
                        {
                            try
                            {
                                ZipCode = Convert.ToString(TempTable.Rows[i][13]);

                            }
                            catch
                            {
                            }
                        }

                        if (intColIndex >= 14)
                        {
                            try
                            {
                                City = Convert.ToString(TempTable.Rows[i][14]);

                            }
                            catch
                            {
                            }
                        }

                        if (intColIndex >= 15)
                        {
                            try
                            {
                                State = Convert.ToString(TempTable.Rows[i][15]);

                            }
                            catch
                            {
                            }
                        }

                        if (intColIndex >= 16)
                        {
                            try
                            {
                                Email = Convert.ToString(TempTable.Rows[i][16]);

                            }
                            catch
                            {
                            }
                        }

                        if (intColIndex >= 17)
                        {
                            try
                            {
                                OfficePhone = Convert.ToString(TempTable.Rows[i][17]);

                            }
                            catch
                            {
                            }
                        }

                        if (intColIndex >= 18)
                        {
                            try
                            {
                                CellPhone = Convert.ToString(TempTable.Rows[i][18]);

                            }
                            catch
                            {
                            }
                        }

                        if (intColIndex >= 19)
                        {
                            try
                            {
                                Fax = Convert.ToString(TempTable.Rows[i][19]);

                            }
                            catch
                            {
                            }
                        }

                        if (intColIndex >= 20)
                        {
                            try
                            {
                                FirstYearDefault = Convert.ToString(TempTable.Rows[i][20]);

                            }
                            catch
                            {
                            }
                        }

                        if (intColIndex >= 21)
                        {
                            try
                            {
                                RenewalDefault = Convert.ToString(TempTable.Rows[i][21]);

                            }
                            catch
                            {
                            }
                        }

                        if (intColIndex >= 22)
                        {
                            try
                            {
                                ReportForEntireAgency = TrueOrFalse(Convert.ToString(TempTable.Rows[i][22]));

                            }
                            catch
                            {
                            }
                        }

                        if (intColIndex >= 23)
                        {
                            try
                            {
                                ReportForOwnBusiness = TrueOrFalse(Convert.ToString(TempTable.Rows[i][23]));

                            }
                            catch
                            {
                            }
                        }

                        if (intColIndex >= 24)
                        {
                            try
                            {
                                AddPayeeOn = Convert.ToString(TempTable.Rows[i][24]);

                            }
                            catch
                            {
                            }
                        }

                        if (intColIndex >= 25)
                        {
                            try
                            {
                                DisableAgentEditing = TrueOrFalse(Convert.ToString(TempTable.Rows[i][25]));

                            }
                            catch
                            {
                            }
                        }

                        if (intColIndex >= 26)
                        {
                            try
                            {
                                Moduletodisplay = Convert.ToString(TempTable.Rows[i][26]);


                            }
                            catch
                            {
                            }
                        }

                        if (intColIndex >= 27)
                        {
                            try
                            {
                                AccessRight = Convert.ToString(TempTable.Rows[i][27]);

                            }
                            catch
                            {
                            }
                        }

                        //Check Nick name available or Not
                        bool bValue = IsNickNameExist(NickName);
                        if (!bValue)
                        {
                            AddUserCredentials(UserName, Password, RID, PasswordHintQuestion, PasswordHintAnswer, IsHouseAccount, IsNewsToFlash,
                            IsAccountExec, LincessID, FirstName, LastName, Company, NickName, Address, ZipCode, City, State, Email,
                            OfficePhone, CellPhone, Fax, FirstYearDefault, RenewalDefault, ReportForEntireAgency, ReportForOwnBusiness,
                            AddPayeeOn, DisableAgentEditing, Moduletodisplay, ModuleId, AccessRightId);
                        }
                    }

                }
            }
            catch
            {
            }

        }

        private int RoleID(string strRole)
        {
            int value = 3;//agent ID

            if (string.IsNullOrEmpty(strRole))
            {
                value = 3;
            }
            else if (strRole.ToLower() == "superadmin")
            {
                value = 1;
            }
            else if (strRole.ToLower() == "administrator")
            {
                value = 2;
            }
            else if (strRole.ToLower() == "agent")
            {
                value = 3;
            }
            else if (strRole.ToLower() == "dep")
            {
                value = 5;
            }
            return value;

        }

        private bool TrueOrFalse(string strValue)
        {
            bool bvalue = false;//agent ID

            if (string.IsNullOrEmpty(strValue))
            {
                bvalue = false;
            }
            else if (strValue.ToLower() == "no")
            {
                bvalue = false;
            }
            else if (strValue.ToLower() == "yes")
            {
                bvalue = true;
            }

            return bvalue;

        }

        private bool IsNickNameExist(string strNickName)
        {
            bool bValue = false;

            try
            {
                using (DLinq.CommissionDepartmentEntities DataModel = Entity.DataModel)
                {
                    User _user = (from uc in DataModel.UserDetails

                                  where (uc.NickName.ToLower() == strNickName)
                                  select new User
                                  {
                                      UserCredentialID = uc.UserCredentialId,
                                      IsDeleted = uc.UserCredential.IsDeleted
                                  }
                             ).FirstOrDefault();

                    if (_user == null)
                    {
                        bValue = false;
                    }
                    else
                    {
                        if (_user.IsDeleted != null)
                        {
                            bValue = _user.IsDeleted;

                        }

                    }
                }
            }
            catch
            {
                bValue = false;
            }
            return bValue;
        }


        public void AddUserCredentials(string UserName, string Password, int RID, string PasswordHintQuestion, string PasswordHintAnswer, bool IsHouseAccount, bool IsNewsToFlash, bool IsAccountExec, Guid LicenseeId, string FirstName,
            string LastName, string Company, string NickName, string Address, string ZipCode, string City, string State, string Email, string OfficePhone,
            string CellPhone, string Fax, string FirstYearDefault, string RenewalDefault, bool ReportForEntireAgency, bool ReportForOwnBusiness, string AddPayeeOn,
            bool DisableAgentEditing, string Moduletodisplay, int ModuleId, int AccessRightId)
        {
            //using (TransactionScope transactionScope = new TransactionScope())
            //{
            try
            {

                using (DLinq.CommissionDepartmentEntities DataModel = Entity.DataModel)
                {
                    Guid guidID = Guid.NewGuid();
                    //User credentials
                    try
                    {

                        var _dtUsers = new DLinq.UserCredential();
                        _dtUsers.UserCredentialId = guidID;
                        _dtUsers.UserName = UserName;
                        _dtUsers.Password = Password;
                        _dtUsers.PasswordHintAnswer = PasswordHintQuestion;
                        _dtUsers.PasswordHintQuestion = PasswordHintAnswer;
                        _dtUsers.RoleId = RID;
                        _dtUsers.LicenseeId = LicenseeId;
                        _dtUsers.CreatedOn = DateTime.Now;
                        DataModel.AddToUserCredentials(_dtUsers);
                    }
                    catch
                    {
                    }

                    //User details
                    try
                    {

                        var _dtUserDetail = new DLinq.UserDetail();
                        _dtUserDetail.UserCredentialId = guidID;
                        _dtUserDetail.FirstName = FirstName;
                        _dtUserDetail.LastName = LastName;
                        _dtUserDetail.Company = Company;
                        _dtUserDetail.NickName = NickName;
                        if (Address.Length > 50)
                        {
                            string strAddress = Address;
                            strAddress = strAddress.Substring(0, 49);
                            _dtUserDetail.Address = strAddress;
                        }
                        else
                        {
                            _dtUserDetail.Address = Address;
                        }
                        if (!string.IsNullOrEmpty(ZipCode))
                        {
                            _dtUserDetail.ZipCode = Convert.ToInt32(ZipCode);
                        }

                        _dtUserDetail.City = City;
                        _dtUserDetail.State = State;
                        _dtUserDetail.Email = Email;
                        _dtUserDetail.OfficePhone = OfficePhone;
                        _dtUserDetail.CellPhone = CellPhone;
                        _dtUserDetail.Fax = Fax;
                        _dtUserDetail.FirstYearDefault = Convert.ToDouble(FirstYearDefault);
                        _dtUserDetail.RenewalDefault = Convert.ToDouble(RenewalDefault);
                        _dtUserDetail.ReportForEntireAgency = ReportForEntireAgency;
                        _dtUserDetail.ReportForOwnBusiness = ReportForOwnBusiness;
                        _dtUserDetail.AddPayeeOn = DateTime.Now;
                        _dtUserDetail.DisableAgentEditing = DisableAgentEditing;
                        DataModel.AddToUserDetails(_dtUserDetail);
                    }
                    catch
                    {
                    }
                    //permission
                    try
                    {
                        var _permision = new DLinq.UserPermission();
                        _permision.UserPermissionId = Guid.NewGuid();
                        _permision.UserCredentialId = guidID;
                        _permision.ModuleId = ModuleId;
                        _permision.AccessRightId = AccessRightId;
                        DataModel.AddToUserPermissions(_permision);
                    }
                    catch
                    {
                    }

                    DataModel.SaveChanges();

                    //transactionScope.Complete();
                }
            }
            catch
            {
            }

            //}
        }

        //Acme - added to track login/logout time of the user
        public static void AddLoginLogoutTime(UserLoginParams loginParams)
        {
            try
            {
                ActionLogger.Logger.WriteLog("AddLoginLogoutTime processing begins ", true);
                //DLinq.CommissionDepartmentEntities ctx = new DLinq.CommissionDepartmentEntities(); //create your entity object here
                //EntityConnection ec = (EntityConnection)ctx.Connection;
                //SqlConnection sc = (SqlConnection)ec.StoreConnection; //get the SQLConnection that your entity object would use
                //string adoConnStr = sc.ConnectionString;

                using (SqlConnection con = new SqlConnection(DBConnection.GetConnectionString()))
                {
                    using (SqlCommand cmd = new SqlCommand("USP_AddLoginLogoutEntry", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@UserID", loginParams.UserID);
                        cmd.Parameters.AddWithValue("@AppVersion", loginParams.AppVersion);
                        cmd.Parameters.AddWithValue("@Activity", loginParams.Activity);
                        cmd.Parameters.AddWithValue("@ClientIP", loginParams.ClientIP);
                        cmd.Parameters.AddWithValue("@ClientBrowser", loginParams.ClientBrowser);
                        cmd.Parameters.AddWithValue("@ClientBrowserVersion", loginParams.ClientBrowserVersion);
                        cmd.Parameters.AddWithValue("@ClientOS", loginParams.ClientOS);
                        cmd.Parameters.AddWithValue("@ClientDeviceDetail", loginParams.ClientDeviceDetail);
                        cmd.Parameters.AddWithValue("@ClientOSVersion", loginParams.ClientOSVersion);
                        cmd.Parameters.AddWithValue("@UserAgentInfo", loginParams.UserAgentInfo);
                        con.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
                ActionLogger.Logger.WriteLog("AddLoginLogoutTime processing completed ", true);
            }
            catch (Exception ex)
            {
                ActionLogger.Logger.WriteLog("AddLoginLogoutTime exception: " + ex.Message, true);
                throw ex;
            }
        }


        /// <summary>
        /// Created BY: jyotisna
        /// Created on: Aug 27, 2018
        /// Purpose: To reset password in DB based on key.
        /// </summary>
        /// <param name="newPassword"></param>
        /// <param name="passwordKey"></param>
        public static string ResetPassword(string newPassword, string passwordKey, out string userName)
        {
            string getusername = null;
            string keySessionExpired = null;
            try
            {

                ActionLogger.Logger.WriteLog("ResetPassword processing begins " + "NewPwd: " + newPassword + ", pwdKey: " + passwordKey, true);
                string encryptedPwd = AESEncrypt.GetEncryptedString(newPassword);
                ActionLogger.Logger.WriteLog("ResetPassword encrypted string: " + encryptedPwd, true);

                DLinq.CommissionDepartmentEntities ctx = new DLinq.CommissionDepartmentEntities(); //create your entity object here
                EntityConnection ec = (EntityConnection)ctx.Connection;
                SqlConnection sc = (SqlConnection)ec.StoreConnection; //get the SQLConnection that your entity object would use
                string adoConnStr = sc.ConnectionString;

                using (SqlConnection con = new SqlConnection(adoConnStr))
                {
                    using (SqlCommand cmd = new SqlCommand("usp_ResetPassword", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@password", encryptedPwd);
                        cmd.Parameters.AddWithValue("@passwordResetKey", passwordKey);
                        cmd.Parameters.Add("@keySessionExpired", SqlDbType.Int);
                        cmd.Parameters["@keySessionExpired"].Direction = ParameterDirection.Output;
                        con.Open();
                        //cmd.ExecuteReader();
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            getusername = reader["username"].ToString();
                            keySessionExpired = reader["keySessionExpired"].ToString();
                        }
                        userName = getusername;
                        //keySessionExpired = Convert.ToString(cmd.Parameters["@keySessionExpired"].Value);
                    }
                }
                ActionLogger.Logger.WriteLog("ResetPassword processing completed ", true);
            }
            catch (Exception ex)
            {
                ActionLogger.Logger.WriteLog("ResetPassword exception: " + ex.Message, true);
                throw ex;
            }
            return keySessionExpired.ToString();
        }

        public static void UpdateEmailAndPassword(Guid userId, string newPassword, string email, out int result)
        {
            try
            {
                //var emailalreadyexist = "0";
                //using (DLinq.CommissionDepartmentEntities DataModel = Entity.DataModel)
                //{
                //    DLinq.UserDetail isEmailexist = DataModel.UserDetails.FirstOrDefault(s => s.Email == email);
                //    DLinq.UserDetail IsSameuserEmail = DataModel.UserDetails.FirstOrDefault(s => s.Email == email && s.UserCredentialId == userId);
                //    result = 2;

                //    if (isEmailexist == null)
                //    {
                //        emailalreadyexist = "1";
                //    }
                //    else if (isEmailexist != null && IsSameuserEmail != null)
                //    {
                //        emailalreadyexist = "2";
                //    }

                //}
                //if (emailalreadyexist == "1" || emailalreadyexist == "2")
                {
                    ActionLogger.Logger.WriteLog("UpdateEmailAndPassword processing begins " + "NewPwd: " + newPassword + ", userID: " + userId, true);
                    string encryptedPwd = AESEncrypt.GetEncryptedString(newPassword);
                    ActionLogger.Logger.WriteLog("UpdateEmailAndPassword encrypted string: " + encryptedPwd, true);

                    //DLinq.CommissionDepartmentEntities ctx = new DLinq.CommissionDepartmentEntities(); //create your entity object here
                    //EntityConnection ec = (EntityConnection)ctx.Connection;
                    //SqlConnection sc = (SqlConnection)ec.StoreConnection; //get the SQLConnection that your entity object would use
                    //string adoConnStr = sc.ConnectionString;
                    if (userId == Guid.Empty || userId == null || String.IsNullOrEmpty(email))
                    {
                        result = 0;
                        return;
                    }
                    using (SqlConnection con = new SqlConnection(DBConnection.GetConnectionString()))
                    {
                        using (SqlCommand cmd = new SqlCommand("usp_SavePasswordAndEmail", con))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@password", encryptedPwd);
                            cmd.Parameters.AddWithValue("@userID", userId);
                            cmd.Parameters.AddWithValue("@email", email);

                            con.Open();
                            cmd.ExecuteNonQuery();
                            //  cmd.ExecuteReader();
                        }
                        result = 1;
                    }
                    ActionLogger.Logger.WriteLog("UpdateEmailAndPassword processing completed ", true);
                }
            }
            catch (Exception ex)
            {
                ActionLogger.Logger.WriteLog("UpdateEmailAndPassword exception: " + ex.Message, true);
                throw ex;
            }
        }

        /// <summary>
        /// Author: Jyotisna
        /// Date: Aug 31, 2018
        /// Purpose: to retreive security question from the system based on user's email
        /// added at the time of registration.
        /// </summary>
        /// <param name="email"></param>
        /// <returns>security question as found from database</returns>
        public static string GetSecurityQues(string email)
        {
            string result = string.Empty;
            try
            {
                ActionLogger.Logger.WriteLog("GetSecurityQues processing begins " + "email: " + email, true);

                DLinq.CommissionDepartmentEntities ctx = new DLinq.CommissionDepartmentEntities(); //create your entity object here
                EntityConnection ec = (EntityConnection)ctx.Connection;
                SqlConnection sc = (SqlConnection)ec.StoreConnection; //get the SQLConnection that your entity object would use
                string adoConnStr = sc.ConnectionString;

                using (SqlConnection con = new SqlConnection(adoConnStr))
                {
                    using (SqlCommand cmd = new SqlCommand("usp_GetPasswordHintQues", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@email", email);
                        con.Open();
                        result = Convert.ToString(cmd.ExecuteScalar());
                    }
                }
                ActionLogger.Logger.WriteLog("GetSecurityQues result: " + result, true);
            }
            catch (Exception ex)
            {
                ActionLogger.Logger.WriteLog("GetSecurityQues exception: " + ex.Message, true);
                throw ex;
            }
            return result;
        }

        /// <summary>
        /// Author: Jyotisna
        /// Date: Aug 31, 2018
        /// Purpose: to retreive username from the system based on user's emailand security ques/ans
        /// added at the time of registration.
        /// </summary>
        /// <param name="email"></param>
        /// <param name="ques"></param>
        /// <param name="ans"></param>
        /// <returns>username as found from database</returns>
        public static string GetUsername(string email, string ques, string ans, out string name)
        {
            string result = string.Empty;
            string firstName = "";
            string lastname = "";
            try
            {

                ActionLogger.Logger.WriteLog("getUsername processing begins " + "email: " + email + ", ques: " + ques + ", ans: " + ans, true);

                DLinq.CommissionDepartmentEntities ctx = new DLinq.CommissionDepartmentEntities(); //create your entity object here
                EntityConnection ec = (EntityConnection)ctx.Connection;
                SqlConnection sc = (SqlConnection)ec.StoreConnection; //get the SQLConnection that your entity object would use
                string adoConnStr = sc.ConnectionString;

                using (SqlConnection con = new SqlConnection(adoConnStr))
                {
                    using (SqlCommand cmd = new SqlCommand("usp_GetUsername", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@email", email);
                        cmd.Parameters.AddWithValue("@ques", ques);
                        cmd.Parameters.AddWithValue("@ans", ans);
                        con.Open();
                        SqlDataReader reader = cmd.ExecuteReader();
                        var test = "";
                        while (reader.Read())
                        {
                            firstName = reader["FirstName"].ToString();
                            lastname = reader["lastName"].ToString();
                            result = reader["Username"].ToString();

                            result = test + result;
                            test = result + " <br/>";
                        }
                        name = test;
                        name = name.Remove(name.Length - 1, 1);
                        name = name.Remove(name.Length - 1, 1);
                    }
                }

                ActionLogger.Logger.WriteLog("getUsername result: " + result, true);
            }
            catch (Exception ex)
            {
                ActionLogger.Logger.WriteLog("getUsername exception: " + ex.Message, true);
                throw ex;
            }
            return result;
        }

        #region Delete temp folder 
        long getDirSize(DirectoryInfo d)
        {
            long size = 0;
            try
            {
                size = d.EnumerateFiles().Sum(file => file.Length);
                size += d.EnumerateDirectories().Sum(dir => getDirSize(dir));
                ActionLogger.Logger.WriteLog("getTempDirSize  " + size.ToString(), true);
            }
            catch (Exception ex)
            {
                ActionLogger.Logger.WriteLog("getTempDirSize exception: " + ex.Message, true);
            }
            return size;
        }

        public void DeleteTempFolderContents()
        {
            try
            {
                ActionLogger.Logger.WriteLog("DeleteTempFolderContents started: ", true);
                string tempPath = System.IO.Path.GetTempPath();
                ActionLogger.Logger.WriteLog("DeleteTempFolderContents tempPath: " + tempPath, true);
                DirectoryInfo d = new DirectoryInfo(tempPath);
                long size = getDirSize(d);
                /*    long totalSize = d.EnumerateFiles().Sum(file => file.Length);
                    totalSize += d.EnumerateDirectories().Sum(dir => DirectorySize(dir, true));
                   */
                foreach (FileInfo fi in d.GetFiles())
                {
                    try
                    {
                        fi.Delete();
                    }
                    catch (Exception ex)
                    {
                        ActionLogger.Logger.WriteLog("DeleteTempFolderContents exception deleting file: " + fi.FullName + ",  " + ex.Message, true);
                    }
                }
                foreach (DirectoryInfo dir in d.GetDirectories())
                {
                    try
                    {
                        dir.Delete(true);
                    }
                    catch (Exception ex)
                    {
                        ActionLogger.Logger.WriteLog("DeleteTempFolderContents exception deleting subdirectory: " + dir.FullName + ",  " + ex.Message, true);
                    }
                }
                ActionLogger.Logger.WriteLog("DeleteTempFolderContents completed", true);
            }
            catch (Exception ex)
            {
                ActionLogger.Logger.WriteLog("DeleteTempFolderContents exception: " + ex.Message, true);
            }
        }

        #endregion
        public static List<string> GetGoogleLocation(string query, out string status, string defaultLocation = "0,0")
        {
            List<string> locations = new List<string>();
            status = string.Empty;
            ActionLogger.Logger.WriteLog(string.Format("GetGoogleLocation request, query: {0}", query), true);
            try
            {
                if (!string.IsNullOrEmpty(query))
                {
                    Prediction[] gLocation = ServiceLocations.GetPredictions(query, out status, defaultLocation);
                    foreach (Prediction p in gLocation)
                    {
                        locations.Add(
                        p.description
                        );
                    }
                }
            }
            catch (Exception ex)
            {
                ActionLogger.Logger.WriteLog(string.Format("Error while GetGoogleLocation, query: {0}, Exception : {1}", query, ex.Message), true);
            }
            return locations;
        }



    }


    public class AsyncDemo
    {
        // The method to be executed asynchronously.
        public string TestMethod(int callDuration, out int threadId)
        {
            Console.WriteLine("Test method begins.");
            Thread.Sleep(callDuration);
            threadId = Thread.CurrentThread.ManagedThreadId;
            return String.Format("My call time was {0}.", callDuration.ToString());
        }
    }
    // The delegate must have the same signature as the method
    // it will call asynchronously.
    public delegate string AsyncMethodCaller(int callDuration, out int threadId);
}