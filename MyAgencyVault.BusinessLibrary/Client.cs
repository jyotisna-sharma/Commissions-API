using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyAgencyVault.BusinessLibrary.Base;
using System.Runtime.Serialization;
using DLinq = DataAccessLayer.LinqtoEntity;
using MyAgencyVault.BusinessLibrary.Masters;
using System.Data.SqlClient;
using System.Data;
using System.Data.EntityClient;

namespace MyAgencyVault.BusinessLibrary
{
    [DataContract]
    public class ClientListingObject
    {
        [DataMember]
        public Guid ClientId { get; set; }

        [DataMember]
        public string CreatedDate { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Email { get; set; }
        [DataMember]
        public string Address { get; set; }
        [DataMember]
        public string Action //To display action button in the column in front end 
        {
            get
            {
                return "action";
            }
            set { }
        }

        DateTime? _dob;
        [DataMember]
        public DateTime? DOB
        {
            get
            {
                return _dob;
            }
            set
            {
                _dob = value;
                if (value != null && string.IsNullOrEmpty(DOBString))
                {
                    DOBString = Convert.ToDateTime(value).ToString("MMM dd, yyyy");
                }
            }
        }

        string _dobString;
        [DataMember]
        public string DOBString
        {
            get
            {
                return _dobString;
            }
            set
            {
                _dobString = value;
                if (DOB == null && !string.IsNullOrEmpty(_dobString))
                {
                    DateTime dt;
                    DateTime.TryParse(_dobString, out dt);
                    DOB = dt;
                }
            }
        }

        [DataMember]
        public string SSN { get; set; }
        [DataMember]
        public string Phone { get; set; }
    }

    [DataContract]
    public class Client : IEditable<Client>
    {
        public Client()
        {
            this.Phone = new ContactDetails();
        }

        #region IEditable<Client> Members


        /// <summary>
        /// Author; Jyotisna
        /// Date: Jan 31, 2019
        /// Purpose; Check if client present with same name
        /// </summary>
        public bool CheckDuplicateName()
        {
            using (DLinq.CommissionDepartmentEntities DataModel = Entity.DataModel)
            {
                var _clients = (from s in DataModel.Clients where (s.ClientId == this.ClientId) select s).FirstOrDefault();

                if (_clients == null) //new client request
                {
                    _clients = (from s in DataModel.Clients where (!string.IsNullOrEmpty(s.Name) && (s.Name.Trim().ToLower() == this.Name.Trim().ToLower()) && s.LicenseeId == this.LicenseeId && s.IsDeleted == false) select s).FirstOrDefault();
                }
                else //edit request
                {
                    _clients = (from s in DataModel.Clients where (!string.IsNullOrEmpty(s.Name) && (s.Name.Trim().ToLower() == this.Name.Trim().ToLower() && s.LicenseeId == this.LicenseeId && s.ClientId != this.ClientId && s.IsDeleted == false)) select s).FirstOrDefault();
                }
                if (_clients == null)
                {
                    ActionLogger.Logger.WriteLog(DateTime.Now.ToString() + " CheckDuplicateName clientName not found:" + this.Name, true);
                    return false;
                }
                ActionLogger.Logger.WriteLog(DateTime.Now.ToString() + " CheckDuplicateName clientName found existing:" + this.Name, true);
                return true;
            }

        }
        public void AddUpdate()
        {
            try
            {
                using (DLinq.CommissionDepartmentEntities DataModel = Entity.DataModel)
                {
                    var _clients = (from s in DataModel.Clients where (s.ClientId == this.ClientId) select s).FirstOrDefault();
                    if (_clients == null)
                    {
                        _clients = new DLinq.Client
                        {
                            ClientId = this.ClientId,
                            Address = this.Address,
                            Zip = this.Zip,
                            State = this.State,
                            Name = this.Name,
                            IsDeleted = false,
                            City = this.City,
                            Email = this.Email,
                            CreatedOn = DateTime.Now,
                            SSN = this.SSN,
                            DOB = this.DOB,
                            MaritalStatus = this.MaritalStatus,
                            Gender = this.Gender,
                            FlatNo = this.FlatNo,
                            Phone = (this.Phone != null) ? this.Phone.PhoneNumber : "",
                            DialCode = (this.Phone != null) ? this.Phone.DialCode : "",
                            CountryCode = (this.Phone != null) ? this.Phone.CountryCode : ""
                        };
                        DLinq.Licensee _license = ReferenceMaster.GetReferencedLicensee(this.LicenseeId, DataModel);
                        _clients.Licensee = _license;
                        DataModel.AddToClients(_clients);
                    }
                    else
                    {
                        _clients.Address = this.Address;
                        _clients.Zip = this.Zip;
                        _clients.State = this.State;
                        _clients.Name = this.Name;
                        _clients.City = this.City;
                        _clients.Email = this.Email;
                        _clients.SSN = this.SSN;
                        _clients.DOB = this.DOB;
                        _clients.MaritalStatus = this.MaritalStatus;
                        _clients.Gender = this.Gender;
                        _clients.FlatNo = this.FlatNo;
                        _clients.Phone = (this.Phone != null) ? this.Phone.PhoneNumber : "";
                        _clients.DialCode = (this.Phone != null) ? this.Phone.DialCode : "";
                        _clients.CountryCode = (this.Phone != null) ? this.Phone.CountryCode : "";
                        _clients.ModifiedOn = DateTime.Now;

                        if (_clients.IsDeleted == true)
                            _clients.IsDeleted = false;
                    }
                    DataModel.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                ActionLogger.Logger.WriteLog("AddUpdate client:" + ex.Message.ToString(), true);
                if (ex.InnerException != null)
                {
                    ActionLogger.Logger.WriteLog("AddUpdateClient inner ex:" + ex.InnerException.ToStringDump(), true);
                }
            }
        }

        public static void DeleteCascadeClient(Guid PolicyId, Guid ClientId, Guid LicenseId)
        {
            ActionLogger.Logger.WriteLog(DateTime.Now.ToString() + " DeleteCascadeClient started - policyid: " + PolicyId + ", clientid: " + ClientId + ", licenseeid: " + LicenseId, true);
            Policy.DeletePolicyCascadeFromDBById(PolicyId);
            PostUtill.RemoveClient(ClientId, LicenseId);
            ActionLogger.Logger.WriteLog(DateTime.Now.ToString() + " DeleteCascadeClient ended - policyid: " + PolicyId + ", clientid: " + ClientId + ", licenseeid: " + LicenseId, true);
        }

        /// <summary>
        /// Author: jyotisna
        /// Date: jan 31, 2019
        /// Purpose; To handle client deletion request from front end
        /// </summary>
        /// <param name="ClientID"></param>
        public static void DeleteClient(Guid ClientID)
        {
            try
            {
                ActionLogger.Logger.WriteLog(DateTime.Now.ToString() + " Delete started - as forceDelete is true in client.cs - clientID: " + ClientID, true);
                using (DLinq.CommissionDepartmentEntities DataModel = Entity.DataModel)
                {
                    var client = (from s in DataModel.Clients where (s.ClientId == ClientID) select s).FirstOrDefault();

                    Dictionary<string, object> parameters = new Dictionary<string, object>();
                    if (client != null)
                    {
                        if (client.LicenseeId != null)
                        {
                            parameters.Add("PolicyLicenseeId", client.LicenseeId);
                        }
                        parameters.Add("PolicyClientId", client.ClientId);
                        parameters.Add("IsDeleted", false);
                        List<Guid> policyIds = Policy.GetPolicyData(parameters).Select(s => s.PolicyId).ToList();

                        foreach (Guid policyId in policyIds)
                        {
                            Policy.MarkPolicyDeletedById(policyId);
                        }

                        //if (client != null)
                        client.IsDeleted = true;

                        DataModel.SaveChanges();
                    }
                    ActionLogger.Logger.WriteLog(DateTime.Now.ToString() + " Delete ended in client.cs - clientID: " + ClientID, true);
                }
            }
            catch (Exception ex)
            {
                ActionLogger.Logger.WriteLog("exception deleting client: " + ex.Message, true);
                throw ex;
            }
        }

        public void Delete()
        {
            ActionLogger.Logger.WriteLog(DateTime.Now.ToString() + " Delete started in client.cs - clientID: " + this.ClientId, true);
            using (DLinq.CommissionDepartmentEntities DataModel = Entity.DataModel)
            {
                try
                {
                    var client = (from s in DataModel.Clients where (s.ClientId == this.ClientId) select s).FirstOrDefault();

                    Dictionary<string, object> parameters = new Dictionary<string, object>();
                    if (client != null)
                    {
                        if (client.LicenseeId != null)
                        {
                            parameters.Add("PolicyLicenseeId", client.LicenseeId);
                        }
                        parameters.Add("PolicyClientId", client.ClientId);
                        parameters.Add("IsDeleted", false);
                        List<Guid> policyIds = Policy.GetPolicyData(parameters).Select(s => s.PolicyId).ToList();

                        foreach (Guid policyId in policyIds)
                        {
                            Policy.MarkPolicyDeletedById(policyId);
                        }

                        if (client != null)
                            client.IsDeleted = true;

                        DataModel.SaveChanges();
                    }
                    ActionLogger.Logger.WriteLog(DateTime.Now.ToString() + " Delete ended in client.cs - clientID: " + this.ClientId, true);
                }
                catch (Exception ex)
                {
                    ActionLogger.Logger.WriteLog(DateTime.Now.ToString() + " Delete exception in client.cs - clientID: " + this.ClientId + ", ex: " + ex.Message, true);
                }
            }
        }
        /// <summary>
        /// Physically delete the client
        /// for delete we need only ClientID in Instance
        /// </summary>
        /// <param name="_Client"></param>
        public static void DeleteClient(Client _Client)
        {
            ActionLogger.Logger.WriteLog(DateTime.Now.ToString() + " DeleteClient started in client.cs - clientID: " + _Client.ClientId, true);
            using (DLinq.CommissionDepartmentEntities DataModel = Entity.DataModel)
            {
                try
                {
                    var _clients = (from s in DataModel.Clients where (s.ClientId == _Client.ClientId) select s).FirstOrDefault();
                    if (_clients != null)
                    {
                        DataModel.DeleteObject(_clients);
                        DataModel.SaveChanges();
                    }
                    ActionLogger.Logger.WriteLog(DateTime.Now.ToString() + " DeleteClient ended in client.cs - clientID: " + _Client.ClientId, true);
                }
                catch (Exception ex)
                {
                    ActionLogger.Logger.WriteLog(DateTime.Now.ToString() + " DeleteClient exception in client.cs - clientID: " + _Client.ClientId + ", ex: " + ex.Message, true);
                    if (ex.InnerException != null)
                    {
                        ActionLogger.Logger.WriteLog(DateTime.Now.ToString() + " DeleteClient inner exception : " + ex.InnerException.Message, true);
                    }
                }
            }
        }
        public Client GetOfID()
        {
            throw new NotImplementedException();
        }

        public bool IsValid()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region "Data members aka =- public properties"
        [DataMember]
        public Guid ClientId { get; set; }

        [DataMember]
        public string ModifiedDate { get; set; }

        DateTime? _dob;
        [DataMember]
        public DateTime? DOB
        {
            get
            {
                return _dob;
            }
            set
            {
                _dob = value;
                if (value != null && string.IsNullOrEmpty(DOBString))
                {
                    DOBString = Convert.ToDateTime(value).ToString("MM/dd/yyyy");
                }
            }
        }

        string _dobString;
        [DataMember]
        public string DOBString
        {
            get
            {
                return _dobString;
            }
            set
            {
                _dobString = value;
                if (DOB == null && !string.IsNullOrEmpty(_dobString))
                {
                    DateTime dt;
                    DateTime.TryParse(_dobString, out dt);
                    DOB = dt;
                }
            }
        }

        [DataMember]
        public string CreatedDate { get; set; }
        [DataMember]
        public Guid? LicenseeId { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string InsuredName { get; set; }//Insured Name May be different from Client
        [DataMember]
        public string Address { get; set; }
        [DataMember]
        public string Zip { get; set; }
        [DataMember]
        public string City { get; set; }
        [DataMember]
        public string State { get; set; }
        [DataMember]
        public string Email { get; set; }
        [DataMember]
        public bool? IsDeleted { get; set; }
        [DataMember]
        public string SSN { get; set; }
        [DataMember]
        public string Gender { get; set; }
        [DataMember]
        public string MaritalStatus { get; set; }
        [DataMember]
        public string DialCode { get; set; }
        [DataMember]
        public string PhoneNumber { get; set; }
        [DataMember]
        public string CountryCode { get; set; }
        [DataMember]
        public string FlatNo { get; set; }
        [DataMember]
        public ContactDetails Phone { get; set; }
        [DataMember] //--don't know if it is required or not.
        public Guid LogInUserId { get; set; }
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
        #endregion

        #region
        /// <summary>
        /// <param name="PolicyId"/>
        /// <param name="LicenseeId"/>
        /// <param name="BatchId"/>
        /// <param name="StatementId"/>
        /// </summary>
        /// condition apply : (all/ all viewable to the user/ all under the licensee/)
        /// GetClients()(all/all in a licensee/all a user can see/ one of a given id/ one associated with a policy id/ )
        /// overloaded function is possible to implement different filter criteria.
        /// <returns></returns>
        /// 
        public static IEnumerable<Client> GetAllClientByLicChunck(Guid LicenseeId, int skip, int take)
        {
            IEnumerable<Client> clientLst = null;
            using (DLinq.CommissionDepartmentEntities DataModel = Entity.DataModel)
            {
                try
                {
                    clientLst = (from s in DataModel.Clients
                                 where s.IsDeleted == false && s.LicenseeId == LicenseeId
                                 select new Client
                                 {
                                     ClientId = s.ClientId,
                                     Address = s.Address,
                                     City = s.City,
                                     Email = s.Email,
                                     LicenseeId = s.Licensee.LicenseeId,
                                     IsDeleted = s.IsDeleted,
                                     Name = s.Name,
                                     State = s.State,
                                     Zip = s.Zip
                                 }).ToList();

                }
                catch (Exception ex)
                {
                    ActionLogger.Logger.WriteLog(ex.Message, true);
                }
                return clientLst.OrderBy(d => d.Name).Skip(skip).Take(take).ToList();
            }
        }


        /// <summary>
        /// Created By: Ankit khandelwal
        /// Created on: Jan 28, 2019
        /// Purpose: To get the List of clients based on statusId,pagination,searching,sorting
        /// </summary>
        /// <param name="licenseeId"></param>
        /// <param name="listParams"></param>
        /// <param name="statusId"></param>
        /// <param name="allListCounts"></param>
        /// <returns></returns>
        public static List<ClientListingObject> GetClientList(Guid? licenseeId, ListParams listParams, string statusId, Guid loggedInUserId, out ClientCountObject allListCounts)
        {
            using (DLinq.CommissionDepartmentEntities DataModel = Entity.DataModel)
            {
                List<ClientListingObject> clientList = new List<ClientListingObject>();
                try
                {

                    int rowStart = (listParams.pageSize == 0 && listParams.pageIndex == 0) ? 0 : (listParams.pageSize * (listParams.pageIndex)) + 1;
                    int rowEnd = (listParams.pageSize == 0 && listParams.pageIndex == 0) ? 0 : (listParams.pageIndex + 1) * listParams.pageSize;
                    using (SqlConnection con = new SqlConnection(DBConnection.GetConnectionString()))
                    {
                        ActionLogger.Logger.WriteLog("GetClientList processing begins " + "StatusId: " + statusId + ", licenseeId: " + licenseeId + "listParams:" + listParams, true);
                        using (SqlCommand cmd = new SqlCommand("usp_GetClientListing", con))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.CommandTimeout = 0;
                            cmd.Parameters.AddWithValue("@licenseeId", licenseeId);
                            cmd.Parameters.AddWithValue("@loggedInUserId", loggedInUserId);
                            cmd.Parameters.AddWithValue("@statusId", Convert.ToInt32(statusId));
                            cmd.Parameters.AddWithValue("@rowStart", rowStart);
                            cmd.Parameters.AddWithValue("@rowEnd", rowEnd);
                            cmd.Parameters.AddWithValue("@filterBy", listParams.filterBy);
                            cmd.Parameters.AddWithValue("@sortType", listParams.sortType);
                            cmd.Parameters.AddWithValue("@sortBy", listParams.sortBy);
                            cmd.Parameters.Add("@totalClientCount", SqlDbType.Int);
                            cmd.Parameters.Add("@activePolicyClientCount", SqlDbType.Int);
                            cmd.Parameters.Add("@pendingPolicyClientCount", SqlDbType.Int);
                            cmd.Parameters.Add("@TerminatePolicyClientCount", SqlDbType.Int);
                            cmd.Parameters.Add("@withoutPolicyClientCount", SqlDbType.Int);
                            cmd.Parameters.Add("@recordLength", SqlDbType.Int);
                            cmd.Parameters["@totalClientCount"].Direction = ParameterDirection.Output;
                            cmd.Parameters["@activePolicyClientCount"].Direction = ParameterDirection.Output;
                            cmd.Parameters["@pendingPolicyClientCount"].Direction = ParameterDirection.Output;
                            cmd.Parameters["@TerminatePolicyClientCount"].Direction = ParameterDirection.Output;
                            cmd.Parameters["@withoutPolicyClientCount"].Direction = ParameterDirection.Output;
                            cmd.Parameters["@recordLength"].Direction = ParameterDirection.Output;
                            con.Open();
                            ActionLogger.Logger.WriteLog("GetClientList: before start reading a records", true);
                            SqlDataReader reader = cmd.ExecuteReader();

                            ActionLogger.Logger.WriteLog("GetClientList:start reading a records", true);
                            while (reader.Read())
                            {
                                ClientListingObject clientListRecords = new ClientListingObject();
                                if (System.DBNull.Value != reader["ClientId"])
                                {
                                    clientListRecords.ClientId = (Guid)(reader["ClientId"]);
                                }
                                clientListRecords.Name = Convert.ToString(reader["Name"]);
                                clientListRecords.CreatedDate = "";
                                clientListRecords.Email = string.IsNullOrEmpty(Convert.ToString(reader["Email"])) ? "" : Convert.ToString(reader["Email"]);
                                if (System.DBNull.Value != reader["CreatedDate"])
                                {
                                    clientListRecords.CreatedDate = Convert.ToString(reader["CreatedDate"]);
                                }

                                string SSN = Convert.ToString(reader["SSN"]);
                                if (SSN.Length > 3 && SSN.Length <= 5)
                                    SSN = SSN.Insert(3, "-");
                                else if (SSN.Length > 5)
                                    SSN = SSN.Insert(5, "-").Insert(3, "-");
                                clientListRecords.SSN = SSN;
                                clientListRecords.DOB = reader.IsDBNull("DOB") ? (DateTime?)null : Convert.ToDateTime(reader["DOB"]);
                                clientListRecords.Phone = Convert.ToString(reader["Phone"]);

                                clientList.Add(clientListRecords);
                            }
                            ActionLogger.Logger.WriteLog("GetClientList: reading a records success+'clientList'", true);
                            reader.Close();
                            ClientCountObject clientCount = new ClientCountObject();
                            int.TryParse(Convert.ToString(cmd.Parameters["@activePolicyClientCount"].Value), out int ActiveCount);
                            clientCount.ActivePolicyClientCount = ActiveCount;
                            int.TryParse(Convert.ToString(cmd.Parameters["@totalClientCount"].Value), out int TotalClientCount);
                            clientCount.TotalClientCount = TotalClientCount;
                            int.TryParse(Convert.ToString(cmd.Parameters["@pendingPolicyClientCount"].Value), out int PendingPolicyClientCount);
                            clientCount.PendingPolicyClientCount = PendingPolicyClientCount;
                            int.TryParse(Convert.ToString(cmd.Parameters["@TerminatePolicyClientCount"].Value), out int TerminatePolicyClientCount);
                            clientCount.TerminatePolicyClientCount = TerminatePolicyClientCount;
                            int.TryParse(Convert.ToString(cmd.Parameters["@withoutPolicyClientCount"].Value), out int WithoutPolicyClientCount);
                            clientCount.WithoutPolicyClientCount = WithoutPolicyClientCount;
                            int.TryParse(Convert.ToString(cmd.Parameters["@recordLength"].Value), out int SelectedClientCount);
                            clientCount.SelectedClientCount = SelectedClientCount;
                            allListCounts = clientCount;
                            ActionLogger.Logger.WriteLog("GetClientList: all process done task completed'", true);
                        }
                    }
                    return clientList;
                }
                catch (Exception ex)
                {
                    ActionLogger.Logger.WriteLog(" exception while fetching the GDataEntryUsersList: " + "statusId:" + statusId + "ex:" + ex.Message, true);
                    throw ex;
                }
            }
        }


        public static List<ClientListingObject> GetSearchedClientName(Guid? licenseeId, Guid loggedInUserId, string searchString = "")
        {
            using (DLinq.CommissionDepartmentEntities DataModel = Entity.DataModel)
            {
                List<ClientListingObject> clientList = new List<ClientListingObject>();
                try
                {
                    using (SqlConnection con = new SqlConnection(DBConnection.GetConnectionString()))
                    {
                        ActionLogger.Logger.WriteLog("GetSearchedClientName processing begins licenseeId " + licenseeId, true);
                        using (SqlCommand cmd = new SqlCommand("Usp_getpolicyclientlistForSearch", con))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.CommandTimeout = 0;
                            cmd.Parameters.AddWithValue("@licenseeId", licenseeId);
                            cmd.Parameters.AddWithValue("@loggedInUserId", loggedInUserId);
                            cmd.Parameters.AddWithValue("@searchString", searchString);
                            con.Open();
                            SqlDataReader reader = cmd.ExecuteReader();
                            while (reader.Read())
                            {
                                ClientListingObject clientListRecords = new ClientListingObject();
                                if (System.DBNull.Value != reader["ClientId"])
                                {
                                    clientListRecords.ClientId = (Guid)(reader["ClientId"]);
                                }
                                clientListRecords.Name = Convert.ToString(reader["ClientName"]);
                                clientList.Add(clientListRecords);
                            }
                            reader.Close();
                        }
                    }
                    return clientList;
                }
                catch (Exception ex)
                {
                    ActionLogger.Logger.WriteLog(" exception GetSearchedClientName: " + "licenseeId" + licenseeId + "ex:" + ex.Message, true);
                    if (ex.InnerException != null)
                    {
                        ActionLogger.Logger.WriteLog(" Inner exception GetSearchedClientName: " + ex.InnerException.Message, true);
                    }
                    throw ex;
                }
            }
        }

        public static List<ClientListingObject> GetPolicyClientName(Guid? licenseeId, Guid loggedInUserId, string searchString = "")
        {
            using (DLinq.CommissionDepartmentEntities DataModel = Entity.DataModel)
            {
                List<ClientListingObject> clientList = new List<ClientListingObject>();
                try
                {
                    DLinq.CommissionDepartmentEntities ctx = new DLinq.CommissionDepartmentEntities(); //create your entity object here
                    EntityConnection ec = (EntityConnection)ctx.Connection;
                    SqlConnection sc = (SqlConnection)ec.StoreConnection; //get the SQLConnection that your entity object would use
                    string adoConnStr = sc.ConnectionString;
                    using (SqlConnection con = new SqlConnection(adoConnStr))
                    {
                        ActionLogger.Logger.WriteLog("GetClientList processing begins licenseeId " + licenseeId, true);
                        using (SqlCommand cmd = new SqlCommand("usp_GetPolicyClientList", con))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.CommandTimeout = 0;
                            cmd.Parameters.AddWithValue("@licenseeId", licenseeId);
                            cmd.Parameters.AddWithValue("@loggedInUserId", loggedInUserId);
                            cmd.Parameters.AddWithValue("@searchString", searchString);
                            con.Open();
                            SqlDataReader reader = cmd.ExecuteReader();
                            while (reader.Read())
                            {
                                ClientListingObject clientListRecords = new ClientListingObject();
                                if (System.DBNull.Value != reader["ClientId"])
                                {
                                    clientListRecords.ClientId = (Guid)(reader["ClientId"]);
                                }
                                clientListRecords.Name = Convert.ToString(reader["ClientName"]);
                                clientList.Add(clientListRecords);
                            }
                            reader.Close();
                        }
                    }
                    return clientList;
                }
                catch (Exception ex)
                {
                    ActionLogger.Logger.WriteLog(" exception while fetching the GDataEntryUsersList: " + "licenseeId" + licenseeId + "ex:" + ex.InnerException, true);
                    throw ex;
                }
            }
        }

        public static List<ClientListingObject> GetPolicyClientNameByID(Guid? licenseeId, Guid loggedInUserId, Guid? clientID)
        {
            using (DLinq.CommissionDepartmentEntities DataModel = Entity.DataModel)
            {
                List<ClientListingObject> clientList = new List<ClientListingObject>();
                try
                {
                    if (clientID != null)
                    {
                        Client objClient = GetClientByClientID((Guid)clientID, (Guid)licenseeId);
                        ClientListingObject clientListRecords = new ClientListingObject();
                        clientListRecords.ClientId = objClient.ClientId;
                        clientListRecords.Name = objClient.Name;
                        clientList.Add(clientListRecords);
                    }
                    else
                    {
                        using (SqlConnection con = new SqlConnection(DBConnection.GetConnectionString()))
                        {
                            ActionLogger.Logger.WriteLog("GetClientList processing begins licenseeId " + licenseeId, true);
                            using (SqlCommand cmd = new SqlCommand("Usp_getDefaultClientName", con))
                            {
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.CommandTimeout = 0;
                                cmd.Parameters.AddWithValue("@licenseeId", licenseeId);
                                cmd.Parameters.AddWithValue("@loggedInUserId", loggedInUserId);
                                cmd.Parameters.AddWithValue("@searchString", "");
                                con.Open();

                                SqlDataReader reader = cmd.ExecuteReader();
                                if (reader.Read())
                                {
                                    ClientListingObject clientListRecords = new ClientListingObject();
                                    if (System.DBNull.Value != reader["ClientId"])
                                    {
                                        clientListRecords.ClientId = (Guid)(reader["ClientId"]);
                                    }
                                    clientListRecords.Name = Convert.ToString(reader["ClientName"]);
                                    clientList.Add(clientListRecords);
                                }
                                reader.Close();
                            }
                        }
                    }
                    return clientList;
                }
                catch (Exception ex)
                {
                    ActionLogger.Logger.WriteLog(" exception while fetching the GDataEntryUsersList: " + "licenseeId" + licenseeId + "ex:" + ex.InnerException, true);
                    throw ex;
                }
            }
        }

        public static Client GetClientByClientID(Guid clientId, Guid licenseeId)
        {
            using (DLinq.CommissionDepartmentEntities DataModel = Entity.DataModel)
            {
                Client objClient = new Client();
                try
                {
                    objClient = (from s in DataModel.Clients
                                 orderby s.Name
                                 where (s.IsDeleted == false && s.LicenseeId == licenseeId && s.ClientId == clientId && !string.IsNullOrEmpty(s.Name))
                                 select new Client
                                 {
                                     ClientId = s.ClientId,
                                     Address = s.Address,
                                     City = s.City,
                                     Email = s.Email,
                                     LicenseeId = s.Licensee.LicenseeId,
                                     IsDeleted = s.IsDeleted,
                                     Name = s.Name,
                                     State = s.State,
                                     Zip = s.Zip,
                                     SSN = s.SSN,
                                     DOB = s.DOB,
                                     Gender = string.IsNullOrEmpty(s.Gender) ? "None" : s.Gender,
                                     FlatNo = s.FlatNo,
                                     MaritalStatus = string.IsNullOrEmpty(s.MaritalStatus) ? "None" : s.MaritalStatus,
                                     PhoneNumber = s.Phone == null ? "" : s.Phone,
                                     DialCode = s.DialCode == null ? "1" : s.DialCode,
                                     CountryCode = s.CountryCode == null ? "us" : s.CountryCode
                                 }).FirstOrDefault();

                    //objClient.Phone = new ContactDetails();
                    objClient.Phone = new ContactDetails();
                    objClient.Phone.PhoneNumber = objClient.PhoneNumber;
                    objClient.Phone.DialCode = objClient.DialCode;
                    objClient.Phone.CountryCode = objClient.CountryCode;

                }
                catch (Exception ex)
                {
                    ActionLogger.Logger.WriteLog("GetClientByClientID ex " + ex.Message, true);
                    if (ex.InnerException != null)
                    {
                        ActionLogger.Logger.WriteLog("GetClientByClientID inner ex " + ex.InnerException.ToStringDump(), true);
                    }
                    throw ex;
                }

                return objClient;
            }
        }

        public static int GetAllClientCountinLic(Guid? licenseeId)
        {
            int intCount = 0;
            using (DLinq.CommissionDepartmentEntities DataModel = Entity.DataModel)
            {

                try
                {
                    DataModel.CommandTimeout = 600000000;
                    intCount = (from uc in DataModel.Clients where uc.IsDeleted == false && uc.LicenseeId == licenseeId.Value && !string.IsNullOrEmpty(uc.Name) select uc).Count();

                }
                catch
                {
                }

                return intCount;

            }
        }

        public static int GetAllClientCount()
        {
            int intCount = 0;
            using (DLinq.CommissionDepartmentEntities DataModel = Entity.DataModel)
            {

                try
                {
                    DataModel.CommandTimeout = 600000000;
                    intCount = (from uc in DataModel.Clients where uc.IsDeleted == false && !string.IsNullOrEmpty(uc.Name) select uc).Count();

                }
                catch
                {
                }

                return intCount;

            }
        }

        //public static List<Client> GetClientList(Guid? licenseeId)
        //{
        //    List<Client> lstClient = new List<Client>();
        //    SqlConnection con = null;

        //    try
        //    {

        //        using (con = new SqlConnection(DBConnection.GetConnectionString()))
        //        {
        //            using (SqlCommand cmd = new SqlCommand("Usp_GetClientList", con))
        //            {
        //                cmd.CommandType = CommandType.StoredProcedure;
        //                cmd.Parameters.AddWithValue("@LicenseeId", licenseeId);
        //                con.Open();

        //                SqlDataReader reader = cmd.ExecuteReader();
        //                // Call Read before accessing data. 
        //                while (reader.Read())
        //                {

        //                    Client objClient = new Client();
        //                    objClient.ClientId = (Guid)reader["ClientId"];
        //                    objClient.Address = Convert.ToString(reader["Address"]);
        //                    objClient.City = Convert.ToString(reader["City"]);
        //                    objClient.Email = Convert.ToString(reader["Email"]);
        //                    objClient.LicenseeId = (Guid)reader["LicenseeId"];
        //                    objClient.IsDeleted = (bool)reader["IsDeleted"];
        //                    objClient.Name = Convert.ToString(reader["Name"]);
        //                    objClient.State = Convert.ToString(reader["State"]);
        //                    objClient.Zip = Convert.ToString(reader["Zip"]);
        //                    lstClient.Add(objClient);

        //                }

        //                // Call Close when done reading.
        //                reader.Close();
        //            }
        //        }
        //    }
        //    catch
        //    {
        //    }
        //    finally
        //    {
        //        if (con != null)
        //        {
        //            con.Close();
        //        }
        //    }

        //    return lstClient;
        //}

        public static List<Client> GetRefreshedClientList(Guid LicenseeId, List<Guid> ClientIds)
        {
            //ActionLogger.Logger.WriteLog("GetRefreshedClientList Start: " + DateTime.Now.ToLongTimeString(), true); 
            using (DLinq.CommissionDepartmentEntities DataModel = Entity.DataModel)
            {
                var clientLst = (from s in DataModel.Clients
                                 orderby s.Name
                                 where (s.IsDeleted == false) && (!ClientIds.Contains(s.ClientId)) && LicenseeId == s.LicenseeId
                                 select new Client
                                 {
                                     ClientId = s.ClientId,
                                     Address = s.Address,
                                     City = s.City,
                                     Email = s.Email,
                                     LicenseeId = s.Licensee.LicenseeId,
                                     IsDeleted = s.IsDeleted,
                                     Name = s.Name,
                                     State = s.State,
                                     Zip = s.Zip
                                 }).ToList();
                //ActionLogger.Logger.WriteLog("GetRefreshedClientList End: " + DateTime.Now.ToLongTimeString(), true); 
                return clientLst;
            }
        }

        public static Client GetClient(Guid ClientId)
        {
            Client _client = new Client();
            using (DLinq.CommissionDepartmentEntities DataModel = Entity.DataModel)
            {
                _client = (from s in DataModel.Clients
                           where (s.IsDeleted == false && s.ClientId == ClientId)
                           select new Client
                           {
                               ClientId = s.ClientId,
                               Address = s.Address,
                               City = s.City,
                               Email = s.Email,
                               LicenseeId = s.Licensee.LicenseeId,
                               IsDeleted = s.IsDeleted,
                               Name = s.Name,
                               State = s.State,
                               Zip = s.Zip
                           }).ToList().FirstOrDefault();
                return _client;
            }
        }
        /// <summary>
        /// fetch all the policyies of this client.
        /// </summary>
        /// <returns></returns>
        public List<PolicyDetailsData> GetPolicies()
        {
            Dictionary<string, object> paramaters = new Dictionary<string, object>();
            paramaters.Add("PolicyClientId", this.ClientId);
            List<PolicyDetailsData> _policylst = Policy.GetPolicyData(paramaters).ToList();
            return _policylst;
        }

        #endregion

        public Client GetClientByClientName(string strClientName, Guid LicID)
        {
            Client _client = new Client();
            using (DLinq.CommissionDepartmentEntities DataModel = Entity.DataModel)
            {
                _client = (from s in DataModel.Clients
                           where (s.IsDeleted == false && s.Name.ToLower() == strClientName.ToLower() && s.LicenseeId == LicID)
                           select new Client
                           {
                               ClientId = s.ClientId,
                               Address = s.Address,
                               City = s.City,
                               Email = s.Email,
                               LicenseeId = s.Licensee.LicenseeId,
                               IsDeleted = s.IsDeleted,
                               Name = s.Name,
                               State = s.State,
                               Zip = s.Zip
                           }).ToList().FirstOrDefault();
                return _client;
            }
        }

        public Client GetDupliacateClient(string strClientName, DateTime? dob, string address, string city, string state, string phone, string zip, Guid LicID, string ssn)
        {
            Client _client = new Client();
            using (DLinq.CommissionDepartmentEntities DataModel = Entity.DataModel)
            {
                _client = (from s in DataModel.Clients
                           where (s.IsDeleted == false && s.Name.Equals(strClientName, StringComparison.OrdinalIgnoreCase) &&
                           ((Guid)s.LicenseeId).CompareTo(LicID) == 0
                           && s.DOB == dob && s.Address.Equals(address, StringComparison.OrdinalIgnoreCase) &&
                           s.City.Equals(city, StringComparison.OrdinalIgnoreCase) &&
                           s.State.Equals(state, StringComparison.OrdinalIgnoreCase) /*&& 
                           s.Phone == phone*/
                           && s.Zip == zip
                           && s.SSN == ssn)
                           select new Client
                           {
                               ClientId = s.ClientId,
                               Address = s.Address,
                               City = s.City,
                               Email = s.Email,
                               LicenseeId = s.Licensee.LicenseeId,
                               IsDeleted = s.IsDeleted,
                               Name = s.Name,
                               State = s.State,
                               Zip = s.Zip,
                               SSN = s.SSN
                           }).ToList().FirstOrDefault();
                return _client;
            }
        }

        public static bool CheckClientPolicyIssueExists(Guid ClientId)
        {
            ActionLogger.Logger.WriteLog("CheckClientPolicyIssueExists:execution start with clientId:" + ClientId, true);
            bool flag = false;
            try
            {
                Dictionary<string, object> paramaters = new Dictionary<string, object>();
                paramaters.Add("PolicyClientId", ClientId);
                // List<PolicyDetailsData> _policylst = Policy.GetPolicyData(paramaters).ToList();
                List<PolicyDetailsData> _policylst = Policy.GetPoliciesForClientDeletion(ClientId);
                //Get policy list which is deleted 
                // _policylst = _policylst.Where(p => p.IsDeleted == false).ToList();
                foreach (PolicyDetailsData po in _policylst)
                {
                    if (FollowupIssue.IfPolicyHasIssues(po.PolicyId)) //Modified by jyotisna to ignore fetching complete list and get count only 
                    {
                        flag = true;
                        break;
                    }
                }
                if (flag)
                    return flag;

                foreach (PolicyDetailsData po in _policylst)
                {
                    if (PolicyPaymentEntriesPost.HasPaymentEntries(po.PolicyId))
                    {
                        flag = true;
                        return flag;
                    }

                }
            }
            catch (Exception ex)
            {
                ActionLogger.Logger.WriteLog("CheckClientPolicyIssueExists:execution start with clientId:" + ClientId + " exception:" + ex.Message, true);
            }
            return flag;
        }

        public static Guid AddUpdateClient(string clientName, Guid LicID, Guid ClientID, string phone = "", DateTime? dob = null)
        {
            try
            {
                using (DLinq.CommissionDepartmentEntities DataModel = Entity.DataModel)
                {
                    var _clients = (from s in DataModel.Clients where (s.ClientId == ClientID) select s).FirstOrDefault();
                    if (_clients == null)
                    {
                        _clients = new DLinq.Client
                        {
                            ClientId = ClientID,
                            Name = clientName,
                            IsDeleted = false,
                            LicenseeId = LicID,
                            CreatedOn = DateTime.Now,
                            Phone = phone,
                            DOB = dob
                        };

                        DataModel.AddToClients(_clients);
                        DataModel.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                ActionLogger.Logger.WriteLog("Exception adding: " + ex.Message, true);
            }

            return ClientID;
        }

        public static List<ClientListingObject> GetPolicyClientNameByClientId(Guid? licenseeId, Guid loggedInUserId, string clientId)
        {
            using (DLinq.CommissionDepartmentEntities DataModel = Entity.DataModel)
            {
                List<ClientListingObject> clientList = new List<ClientListingObject>();
                try
                {
                    DLinq.CommissionDepartmentEntities ctx = new DLinq.CommissionDepartmentEntities(); //create your entity object here
                    EntityConnection ec = (EntityConnection)ctx.Connection;
                    SqlConnection sc = (SqlConnection)ec.StoreConnection; //get the SQLConnection that your entity object would use
                    string adoConnStr = sc.ConnectionString;
                    using (SqlConnection con = new SqlConnection(adoConnStr))
                    {
                        ActionLogger.Logger.WriteLog("GetPolicyClientNameByClientId processing begins licenseeId " + licenseeId, true);
                        using (SqlCommand cmd = new SqlCommand("usp_GetPolicyClientNameByClientId", con))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.CommandTimeout = 0;
                            cmd.Parameters.AddWithValue("@licenseeId", licenseeId);
                            cmd.Parameters.AddWithValue("@loggedInUserId", loggedInUserId);
                            cmd.Parameters.AddWithValue("@clientId", clientId);
                            con.Open();
                            SqlDataReader reader = cmd.ExecuteReader();
                            while (reader.Read())
                            {
                                ClientListingObject clientListRecords = new ClientListingObject();
                                if (System.DBNull.Value != reader["ClientId"])
                                {
                                    clientListRecords.ClientId = (Guid)(reader["ClientId"]);
                                }
                                clientListRecords.Name = Convert.ToString(reader["ClientName"]);
                                clientList.Add(clientListRecords);
                            }
                            reader.Close();
                        }
                    }
                    return clientList;
                }
                catch (Exception ex)
                {
                    ActionLogger.Logger.WriteLog(" exception while fetching the GetPolicyClientNameByClientId: " + "licenseeId" + licenseeId + "ex:" + ex.InnerException, true);
                    throw ex;
                }
            }
        }
    }
}