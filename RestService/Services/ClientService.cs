using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ServiceModel;
using MyAgencyVault.BusinessLibrary;
using System.ServiceModel.Web;
using MyAgencyVault.WcfService.Library.Response;
using System.Resources;

namespace MyAgencyVault.WcfService
{
    [ServiceContract]
    interface IClientService
    {
        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]

        ClientListResponse GetSearchClientService(Guid licenseeId, Guid loggedInUserId, string searchString = "");

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]

        ClientListResponse GetSelectedClientNameService(Guid licenseeId, Guid loggedInUserId, Guid? clientId);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        JSONResponse AddUpdateClientService(Client client, bool checkDuplicateName);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        JSONResponse DeleteClientsService(Guid ClientID, bool ForceDelete);

        //  [OperationContract] - Not implemented 
        //[WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        //PolicyBoolResponse IsValidClientService(Client ClientObject);


        /// <summary>
        /// <param name="PolicyId"/>
        /// <param name="LicenseeId"/>
        /// <param name="BatchId"/>
        /// <param name="StatementId"/>
        /// </summary>
        /// condition apply : (all/ all viewable to the user/ all under the licensee/)
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        ClientListResponse GetClientListService(Guid? licenseeId, ListParams listParams, string statusId,Guid loggedInUserId);


        //[OperationContract] - Found not used 
        //[WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        //ClientListResponse GetRefreshedClientListService(Guid LicenseeId, List<Guid> ClientIds);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        ClientListResponse GetAllClientNameService(Guid licenseeId, Guid loggedInUserId, string searchString ="");

        //[OperationContract]
        //[WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        //PolicyListResponse GetClientPoliciesService(Client ClientObject);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        PolicyBoolResponse CheckClientPolicyIssueExistsService(Guid ClientId);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        ClientObjectResponse GetClientByClientNameService(string ClientName, Guid LicID);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        ClientObjectResponse GetClientDetailsService(Guid clientId, Guid licenseeId);

        //[OperationContract]
        //[WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        //PolicyPMCResponse GetAllClientCountinLicService(Guid LicID);

        //[OperationContract]
        //[WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        //PolicyPMCResponse GetAllClientCountService();

        //[OperationContract]
        //[WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        //ClientListResponse GetAllClientByLicChunckService(Guid LicenseeId, int skip, int take);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        ClientListResponse GetClientNameService(Guid licenseeId, Guid loggedInUserId, string clientId);
    }

    public partial class MavService : IClientService
    {
        #region IClient Members

        public JSONResponse AddUpdateClientService(Client client, bool checkDuplicateName)
        {
            JSONResponse jres = null;
            ActionLogger.Logger.WriteLog("AddUpdateClientService request: " + client.ToStringDump() + ", checkDuplicate: " + checkDuplicateName, true);

            if (checkDuplicateName && client.CheckDuplicateName())
            {
                //if (client.CheckDuplicateName()) // If duplicate found in DB
                //{
                jres = new JSONResponse(string.Format("Client information found duplicate"), Convert.ToInt16(ResponseCodes.IssueExistWithEntity), "");
                ActionLogger.Logger.WriteLog("AddUpdateClientService :Client information found duplicate ", true);
            }
            else
            {
                try
                {
                    client.AddUpdate();
                    jres = new JSONResponse(string.Format("Client information saved successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                    ActionLogger.Logger.WriteLog("AddUpdateClientService success ", true);
                }
                catch (Exception ex)
                {
                    jres = new JSONResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                    ActionLogger.Logger.WriteLog("AddUpdateClientService failure ", true);
                }
                //jres = new JSONResponse(string.Format("Client information not found duplicate"), Convert.ToInt16(ResponseCodes.NoIssueWithEntity), "");
                //ActionLogger.Logger.WriteLog("AddUpdateClientService : Client information not found duplicate ", true);
            }
            //}
            //else
            //{
            //    try
            //    {
            //        client.AddUpdate();
            //        jres = new JSONResponse(string.Format("Client information saved successfully"), Convert.ToInt16(ResponseCodes.Success), "");
            //        ActionLogger.Logger.WriteLog("AddUpdateClientService success ", true);
            //    }
            //    catch (Exception ex)
            //    {
            //        jres = new JSONResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
            //        ActionLogger.Logger.WriteLog("AddUpdateClientService failure ", true);
            //    }
            //}
            return jres;
        }

        /// <summary>
        /// Author: jyotisna
        /// Date: jan 31, 2019
        /// Purpose; To handle client deletion request from front end
        /// </summary>
        /// <param name="ClientID"></param>
        /// <param name="forceDelete"></param>
        public JSONResponse DeleteClientsService(Guid ClientID, bool ForceDelete)
        {
            JSONResponse jres = null;
            ActionLogger.Logger.WriteLog("DeleteClientsService request: " + ClientID + ", forceDelete: " + ForceDelete, true);
            try
            {
                if (ForceDelete)
                {
                    Client.DeleteClient(ClientID);
                    jres = new JSONResponse(_resourceManager.GetString("ClientDeleteSuccess"), Convert.ToInt16(ResponseCodes.Success), "");
                    ActionLogger.Logger.WriteLog("DeleteClientsService success ", true);
                }
                else
                {
                    if (Client.CheckClientPolicyIssueExists(ClientID))
                    {
                        jres = new JSONResponse(_resourceManager.GetString("ClientNotDeletable"), Convert.ToInt16(ResponseCodes.IssueExistWithEntity), "");
                        ActionLogger.Logger.WriteLog("DeleteClientsService - client cannot be deleted ", true);
                    }
                    else
                    {
                        jres = new JSONResponse(_resourceManager.GetString("ClientDeletable"), Convert.ToInt16(ResponseCodes.NoIssueWithEntity), "");
                        ActionLogger.Logger.WriteLog("DeleteClientsService - client deletable ", true);
                    }
                }
            }
            catch (Exception ex)
            {
                jres = new JSONResponse(_resourceManager.GetString("ClientDeleteFailure"), Convert.ToInt16(ResponseCodes.Failure), _resourceManager.GetString("ClientDeleteFailure") + " " + ex.Message);
                ActionLogger.Logger.WriteLog("DeleteClientsService cannot be deleted " + ex.Message, true);
            }
            return jres;
        }

        //public ClientListResponse GetAllClientListService()
        //{
        //    ClientListResponse jres = null;
        //    ActionLogger.Logger.WriteLog("GetAllClientListService request: ", true);
        //    try
        //    {
        //        List<Client> lst =  Client.GetClientList(Guid.Empty);
        //        if (lst != null && lst.Count > 0)
        //        {
        //            jres = new ClientListResponse(string.Format("Client list retreived successfully"), Convert.ToInt16(ResponseCodes.Success), "");
        //            jres.ClientList = lst;
        //            ActionLogger.Logger.WriteLog("GetAllClientListService success ", true);
        //        }
        //        else
        //        {
        //            jres = new ClientListResponse("No clients found!", Convert.ToInt16(ResponseCodes.RecordNotFound), "");
        //            ActionLogger.Logger.WriteLog("GetAllClientListService 404 ", true);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        jres = new ClientListResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
        //        ActionLogger.Logger.WriteLog("GetAllClientListService failure ", true);
        //    }
        //    return jres;
        //}

        //public ClientListResponse GetRefreshedClientListService(Guid LicenseeId, List<Guid> ClientIds)
        //{

        //    ClientListResponse jres = null;
        //    ActionLogger.Logger.WriteLog("GetRefreshedClientListService request: " + LicenseeId, true);
        //    try
        //    {
        //        List<Client> lst = Client.GetRefreshedClientList(LicenseeId, ClientIds);
        //        if (lst != null && lst.Count > 0)
        //        {
        //            jres = new ClientListResponse(string.Format("Client list retreived successfully"), Convert.ToInt16(ResponseCodes.Success), "");
        //            jres.ClientList = lst;
        //            ActionLogger.Logger.WriteLog("GetRefreshedClientListService success ", true);
        //        }
        //        else
        //        {
        //            jres = new ClientListResponse("No clients found!", Convert.ToInt16(ResponseCodes.RecordNotFound), "");
        //            ActionLogger.Logger.WriteLog("GetRefreshedClientListService 404 ", true);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        jres = new ClientListResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
        //        ActionLogger.Logger.WriteLog("GetRefreshedClientListService failure ", true);
        //    }
        //    return jres;
        //}


        //public PolicyBoolResponse IsValidClientService(Client Clnt)
        //{

        //    PolicyBoolResponse jres = null;
        //    ActionLogger.Logger.WriteLog("IsValidClientService request: " + Clnt.ToStringDump(), true);
        //    try
        //    {
        //        bool isValid =   Clnt.IsValid();
        //        jres = new PolicyBoolResponse(string.Format("Client information found successfully"), Convert.ToInt16(ResponseCodes.Success), "");
        //        jres.BoolFlag = isValid;
        //        ActionLogger.Logger.WriteLog("IsValidClientService success ", true);
        //    }
        //    catch (Exception ex)
        //    {
        //        jres = new PolicyBoolResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
        //        ActionLogger.Logger.WriteLog("IsValidClientService failure ", true);
        //    }
        //    return jres;

        //}

        public ClientListResponse GetClientListService(Guid? licenseeId, ListParams listParams, string statusId, Guid loggedInUserId)
        {
            ClientListResponse jres = null;
            ActionLogger.Logger.WriteLog("GetClientListService request: " + licenseeId, true);
            try
            {
                ClientCountObject count = new ClientCountObject();
                List<ClientListingObject> lst = Client.GetClientList(licenseeId, listParams, statusId, loggedInUserId, out count);
                jres = new ClientListResponse(_resourceManager.GetString("ClientListSuccessMessage"), (int)ResponseCodes.Success, "");
                jres.ClientList = lst;
                jres.ClientsCount = count;
                ActionLogger.Logger.WriteLog("GetClientListService success ", true);
            }
            catch (Exception ex)
            {
                jres = new ClientListResponse(_resourceManager.GetString("ClientListFailure"), Convert.ToInt16(ResponseCodes.Failure), "");
                ActionLogger.Logger.WriteLog("GetClientListService: Exception occurs:" + ex.Message, true);
            }
            return jres;
        }

        //public PolicyPMCResponse GetAllClientCountService()
        //{
        //    PolicyPMCResponse jres = null;
        //    ActionLogger.Logger.WriteLog("GetAllClientCountService request", true);
        //    try
        //    {
        //        int val = Client.GetAllClientCount();
        //        ActionLogger.Logger.WriteLog("GetAllClientCountService value: " + val, true);
        //        if (val != null)
        //        {
        //            jres = new PolicyPMCResponse(string.Format("Data found successfully"), Convert.ToInt16(ResponseCodes.Success), "");
        //            jres.NumberValue = (decimal)val;
        //        }
        //        else
        //        {
        //            jres = new PolicyPMCResponse("Client count not found!", Convert.ToInt16(ResponseCodes.RecordNotFound), "");
        //            ActionLogger.Logger.WriteLog("GetAllClientCountService 404 ", true);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        jres = new PolicyPMCResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
        //        ActionLogger.Logger.WriteLog("GetAllClientCountService failure ", true);
        //    }
        //    return jres;
        //}

        //public PolicyPMCResponse GetAllClientCountinLicService(Guid LicID)
        //{
        //    PolicyPMCResponse jres = null;
        //    ActionLogger.Logger.WriteLog("GetAllClientCountinLicService request: " + LicID, true);
        //    try
        //    {
        //        int val = Client.GetAllClientCountinLic(LicID);
        //        ActionLogger.Logger.WriteLog("GetAllClientCountinLicService value: " + val, true);
        //        if (val != null)
        //        {
        //            jres = new PolicyPMCResponse(string.Format("Data found successfully"), Convert.ToInt16(ResponseCodes.Success), "");
        //            jres.NumberValue = (decimal)val;
        //        }
        //        else
        //        {
        //            jres = new PolicyPMCResponse("Client count not found!", Convert.ToInt16(ResponseCodes.RecordNotFound), "");
        //            ActionLogger.Logger.WriteLog("GetAllClientCountinLicService 404 ", true);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        jres = new PolicyPMCResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
        //        ActionLogger.Logger.WriteLog("GetAllClientCountinLicService failure ", true);
        //    }
        //    return jres;
        //}
        /// <summary>
        /// Created By :Ankit khandelwal
        /// </summary>
        /// <param name="clientId"></param>
        /// <param name="licenseeId"></param>
        /// <returns></returns>
        public ClientObjectResponse GetClientDetailsService(Guid clientId, Guid licenseeId)
        {
            ClientObjectResponse jres = null;
            ActionLogger.Logger.WriteLog("GetClientByClientIDService request: " + clientId + ", licID: " + licenseeId, true);
            try
            {
                Client c = Client.GetClientByClientID(clientId, licenseeId);
                if (c != null)
                {
                    jres = new ClientObjectResponse(string.Format("Client information found successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                    jres.ClientObject = c;
                    ActionLogger.Logger.WriteLog("GetClientByClientIDService success ", true);
                }
                else
                {
                    jres = new ClientObjectResponse("No client found!", Convert.ToInt16(ResponseCodes.RecordNotFound), "");
                    ActionLogger.Logger.WriteLog("GetClientByClientIDService 404 ", true);
                }
            }
            catch (Exception ex)
            {
                jres = new ClientObjectResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                ActionLogger.Logger.WriteLog("GetClientByClientIDService failure ", true);
            }
            return jres;
        }
        /// <summary>
        /// Created By-Ankit Kahndelwal
        /// CreateOn-15-03-2019
        /// Purpose:
        /// </summary>
        /// <param name="licenseeId"></param>
        /// <returns></returns>
        public ClientListResponse GetAllClientNameService(Guid licenseeId,Guid loggedInUserId,string searchString= "")
        {
            ClientListResponse jres = null;
            ActionLogger.Logger.WriteLog("GetAllClientNameService request: " + licenseeId + ", search string: " + searchString, true);
            try
            {
                List<ClientListingObject> clientList = Client.GetPolicyClientName(licenseeId, loggedInUserId, searchString);
                if (clientList != null)
                {
                    jres = new ClientListResponse(string.Format("Client information found successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                    jres.ClientList = clientList;
                    ActionLogger.Logger.WriteLog("GetAllClientNameService success ", true);
                }
                else
                {
                    jres = new ClientListResponse("No client found!", Convert.ToInt16(ResponseCodes.RecordNotFound), "");
                    ActionLogger.Logger.WriteLog("GetAllClientNameService 404 ", true);
                }
            }
            catch (Exception ex)
            {
                jres = new ClientListResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                ActionLogger.Logger.WriteLog("GetClientByClientIDService failure "+ ex.Message, true);
            }
            return jres;
        }

        public ClientListResponse GetSearchClientService(Guid licenseeId, Guid loggedInUserId, string searchString = "")
        {
            ClientListResponse jres = null;
            ActionLogger.Logger.WriteLog("GetSearchClientService request: " + licenseeId + ", search string: " + searchString, true);
            try
            {
                List<ClientListingObject> clientList = Client.GetSearchedClientName(licenseeId, loggedInUserId, searchString);
                if (clientList != null)
                {
                    jres = new ClientListResponse(string.Format("Client information found successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                    jres.ClientList = clientList;
                    ActionLogger.Logger.WriteLog("GetSearchClientService success ", true);
                }
                else
                {
                    jres = new ClientListResponse("No client found!", Convert.ToInt16(ResponseCodes.RecordNotFound), "");
                    ActionLogger.Logger.WriteLog("GetSearchClientService 404 ", true);
                }
            }
            catch (Exception ex)
            {
                jres = new ClientListResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                ActionLogger.Logger.WriteLog("GetSearchClientService failure " + ex.Message, true);
            }
            return jres;
        }
        public ClientListResponse GetSelectedClientNameService(Guid licenseeId, Guid loggedInUserId, Guid? clientId)
        {
            ClientListResponse jres = null;
            ActionLogger.Logger.WriteLog("GetSelectedClientNameService request: " + licenseeId, true);
            try
            {
                List<ClientListingObject> clientList = Client.GetPolicyClientNameByID (licenseeId, loggedInUserId, clientId);
                if (clientList != null)
                {
                    jres = new ClientListResponse(string.Format("Client information found successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                    jres.ClientList = clientList;
                    ActionLogger.Logger.WriteLog("GetSelectedClientNameService success ", true);
                }
                else
                {
                    jres = new ClientListResponse("No client found!", Convert.ToInt16(ResponseCodes.RecordNotFound), "");
                    ActionLogger.Logger.WriteLog("GetSelectedClientNameService 404 ", true);
                }
            }
            catch (Exception ex)
            {
                jres = new ClientListResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                ActionLogger.Logger.WriteLog("GetSelectedClientNameService failure " + ex.Message, true);
            }
            return jres;
        }
        //public PolicyListResponse GetClientPoliciesService(Client Clnt)
        //{
        //    PolicyListResponse jres = null;
        //    ActionLogger.Logger.WriteLog("GetClientPoliciesService request: Clnt" + Clnt, true);
        //    try
        //    {
        //        List<PolicyDetailsData> lst = Clnt.GetPolicies();
        //        if (lst != null && lst.Count > 0)
        //        {
        //            jres = new PolicyListResponse(string.Format("Policies found successfully"), Convert.ToInt16(ResponseCodes.Success), "");
        //            jres.PolicyDetailList = lst;
        //            ActionLogger.Logger.WriteLog("GetClientPoliciesService success ", true);
        //        }
        //        else
        //        {
        //            jres = new PolicyListResponse("No data  found!", Convert.ToInt16(ResponseCodes.RecordNotFound), "");
        //            ActionLogger.Logger.WriteLog("GetClientPoliciesService 404 ", true);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        jres = new PolicyListResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
        //        ActionLogger.Logger.WriteLog("GetClientPoliciesService failure ", true);
        //    }
        //    return jres;
        //}

        //public ClientListResponse GetAllClientByLicChunckService(Guid LicenseeId, int skip, int take)
        //{
        //    ClientListResponse jres = null;
        //    ActionLogger.Logger.WriteLog("GetAllClientByLicChunckService request: " + LicenseeId, true);
        //    try
        //    {
        //        IEnumerable<Client> enClient = Client.GetAllClientByLicChunck(LicenseeId, skip, take);
        //        if (enClient.Count() != 0)
        //        {
        //            List<Client> lst = enClient.ToList();
        //            jres = new ClientListResponse(string.Format("Client list retreived successfully"), Convert.ToInt16(ResponseCodes.Success), "");
        //            //jres.ClientList = lst;
        //            ActionLogger.Logger.WriteLog("GetAllClientByLicChunckService success ", true);
        //        }
        //        else
        //        {
        //            jres = new ClientListResponse("No clients found!", Convert.ToInt16(ResponseCodes.RecordNotFound), "");
        //            ActionLogger.Logger.WriteLog("GetAllClientByLicChunckService 404 ", true);
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        jres = new ClientListResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
        //        ActionLogger.Logger.WriteLog("GetAllClientByLicChunckService failure ", true);
        //    }
        //    return jres;
        //}

        public ClientObjectResponse GetClientByClientNameService(string strClientName, Guid LicID)
        {
            ClientObjectResponse jres = null;
            ActionLogger.Logger.WriteLog("GetClientByClientNameService request: " + strClientName + ", licID: " + LicID, true);
            try
            {
                Client objClient = new Client();
                Client c = objClient.GetClientByClientName(strClientName, LicID);
                if (c != null)
                {
                    jres = new ClientObjectResponse(string.Format("Client information found successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                    jres.ClientObject = c;
                    ActionLogger.Logger.WriteLog("GetClientByClientNameService success ", true);
                }
                else
                {
                    jres = new ClientObjectResponse("No client found!", Convert.ToInt16(ResponseCodes.RecordNotFound), "");
                    ActionLogger.Logger.WriteLog("GetClientByClientNameService 404 ", true);
                }
            }
            catch (Exception ex)
            {
                jres = new ClientObjectResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                ActionLogger.Logger.WriteLog("GetClientByClientIDService failure ", true);
            }
            return jres;
        }

        public PolicyBoolResponse CheckClientPolicyIssueExistsService(Guid ClientId)
        {
            PolicyBoolResponse jres = null;
            ActionLogger.Logger.WriteLog("CheckClientPolicyIssueExistsService request: " + ClientId.ToStringDump(), true);
            try
            {
                bool isValid = Client.CheckClientPolicyIssueExists(ClientId);
                jres = new PolicyBoolResponse(string.Format("Client information deleted successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                jres.BoolFlag = isValid;
                ActionLogger.Logger.WriteLog("CheckClientPolicyIssueExistsService success ", true);
            }
            catch (Exception ex)
            {
                jres = new PolicyBoolResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                ActionLogger.Logger.WriteLog("CheckClientPolicyIssueExistsService failure ", true);
            }
            return jres;
        }

        /// <summary>
        /// Created By-Acmeminds
        /// CreateOn-24-11-2020
        /// Purpose:
        /// </summary>
        /// <param name="licenseeId"></param>
        /// <param name="loggedInUserId"></param>
        /// <param name="clientId"></param>
        /// <returns></returns>
        public ClientListResponse GetClientNameService(Guid licenseeId, Guid loggedInUserId, string clientId)
        {
            ClientListResponse jres = null;
            ActionLogger.Logger.WriteLog("GetClientNameService request: " + licenseeId, true);
            try
            {
                List<ClientListingObject> clientList = Client.GetPolicyClientNameByClientId(licenseeId, loggedInUserId, clientId);
                if (clientList != null)
                {
                    jres = new ClientListResponse(string.Format("Client information found successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                    jres.ClientList = clientList;
                    ActionLogger.Logger.WriteLog("GetClientNameService success ", true);
                }
                else
                {
                    jres = new ClientListResponse("No client found!", Convert.ToInt16(ResponseCodes.RecordNotFound), "");
                    ActionLogger.Logger.WriteLog("GetClientNameService 404 ", true);
                }
            }
            catch (Exception ex)
            {
                jres = new ClientListResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                ActionLogger.Logger.WriteLog("GetClientNameService failure " + ex.Message, true);
            }
            return jres;
        }

        #endregion
    }
}