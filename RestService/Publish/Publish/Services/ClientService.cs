using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ServiceModel;
using MyAgencyVault.BusinessLibrary;
using System.ServiceModel.Web;
using MyAgencyVault.WcfService.Library.Response;

namespace MyAgencyVault.WcfService
{
    [ServiceContract]
    interface IClientService
    {
       
        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        JSONResponse AddUpdateClientService(Client ClientObject);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        JSONResponse DeleteClientsService(Client ClientObject);

          [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        PolicyBoolResponse IsValidClientService(Client ClientObject);

       
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
        ClientListResponse GetClientListService(Guid? LicenseeId);


        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        ClientListResponse GetRefreshedClientListService(Guid LicenseeId, List<Guid> ClientIds);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        ClientListResponse GetAllClientListService();
        /// <summary>
        /// <param name="ClientId"/>
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        PolicyListResponse GetClientPoliciesService(Client ClientObject);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        PolicyBoolResponse CheckClientPolicyIssueExistsService(Guid ClientId);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        ClientObjectResponse GetClientByClientNameService(string ClientName, Guid LicID);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        ClientObjectResponse GetClientByClientIDService(Guid ClientID, Guid LicID);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        PolicyPMCResponse GetAllClientCountinLicService(Guid LicID);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        PolicyPMCResponse GetAllClientCountService();

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        ClientListResponse  GetAllClientByLicChunckService(Guid LicenseeId, int skip, int take);
    }

    public partial class MavService : IClientService
    {
        #region IClient Members

        public JSONResponse AddUpdateClientService(Client Clnt)
        {
            JSONResponse jres = null;
            ActionLogger.Logger.WriteLog("AddUpdateClientService request: " + Clnt.ToStringDump(), true);
            try
            {
                Clnt.AddUpdate();
                jres = new JSONResponse(string.Format("Client information saved successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                ActionLogger.Logger.WriteLog("AddUpdateClientService success ", true);
            }
            catch (Exception ex)
            {
                jres = new JSONResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                ActionLogger.Logger.WriteLog("AddUpdateClientService failure ", true);
            }
            return jres;
        }

        public JSONResponse DeleteClientsService(Client Clnt)
        {
            JSONResponse jres = null;
            ActionLogger.Logger.WriteLog("DeleteClientsService request: " + Clnt.ToStringDump(), true);
            try
            {
                Clnt.Delete();
                jres = new JSONResponse(string.Format("Client information deleted successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                ActionLogger.Logger.WriteLog("DeleteClientsService success ", true);
            }
            catch (Exception ex)
            {
                jres = new JSONResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                ActionLogger.Logger.WriteLog("DeleteClientsService failure ", true);
            }
            return jres;
        }

        public ClientListResponse GetAllClientListService()
        {
            ClientListResponse jres = null;
            ActionLogger.Logger.WriteLog("GetAllClientListService request: ", true);
            try
            {
                List<Client> lst =  Client.GetClientList(Guid.Empty);
                if (lst != null && lst.Count > 0)
                {
                    jres = new ClientListResponse(string.Format("Client list retreived successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                    jres.ClientList = lst;
                    ActionLogger.Logger.WriteLog("GetAllClientListService success ", true);
                }
                else
                {
                    jres = new ClientListResponse("No clients found!", Convert.ToInt16(ResponseCodes.RecordNotFound), "");
                    ActionLogger.Logger.WriteLog("GetAllClientListService 404 ", true);
                }
            }
            catch (Exception ex)
            {
                jres = new ClientListResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                ActionLogger.Logger.WriteLog("GetAllClientListService failure ", true);
            }
            return jres;
        }

        public ClientListResponse GetRefreshedClientListService(Guid LicenseeId, List<Guid> ClientIds)
        {
            
            ClientListResponse jres = null;
            ActionLogger.Logger.WriteLog("GetRefreshedClientListService request: " + LicenseeId, true);
            try
            {
                List<Client> lst = Client.GetRefreshedClientList(LicenseeId, ClientIds);
                if (lst != null && lst.Count > 0)
                {
                    jres = new ClientListResponse(string.Format("Client list retreived successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                    jres.ClientList = lst;
                    ActionLogger.Logger.WriteLog("GetRefreshedClientListService success ", true);
                }
                else
                {
                    jres = new ClientListResponse("No clients found!", Convert.ToInt16(ResponseCodes.RecordNotFound), "");
                    ActionLogger.Logger.WriteLog("GetRefreshedClientListService 404 ", true);
                }
            }
            catch (Exception ex)
            {
                jres = new ClientListResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                ActionLogger.Logger.WriteLog("GetRefreshedClientListService failure ", true);
            }
            return jres;
        }


        public PolicyBoolResponse IsValidClientService(Client Clnt)
        {
         
            PolicyBoolResponse jres = null;
            ActionLogger.Logger.WriteLog("IsValidClientService request: " + Clnt.ToStringDump(), true);
            try
            {
                bool isValid =   Clnt.IsValid();
                jres = new PolicyBoolResponse(string.Format("Client information found successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                jres.BoolFlag = isValid;
                ActionLogger.Logger.WriteLog("IsValidClientService success ", true);
            }
            catch (Exception ex)
            {
                jres = new PolicyBoolResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                ActionLogger.Logger.WriteLog("IsValidClientService failure ", true);
            }
            return jres;

        }

        public ClientListResponse GetClientListService(Guid? LicenseeId)
        {
            ClientListResponse jres = null;
            ActionLogger.Logger.WriteLog("GetClientListService request: " + LicenseeId, true);
            try
            {
               List<Client> lst = Client.GetClientList(LicenseeId);
               if (lst != null && lst.Count > 0)
               {
                   jres = new ClientListResponse(string.Format("Client list retreived successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                   jres.ClientList = lst;
                   ActionLogger.Logger.WriteLog("GetClientListService success ", true);
               }
               else
               {
                   jres = new ClientListResponse("No clients found!", Convert.ToInt16(ResponseCodes.RecordNotFound),"");
                   ActionLogger.Logger.WriteLog("GetClientListService 404 ", true);
               }
            }
            catch (Exception ex)
            {
                jres = new ClientListResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                ActionLogger.Logger.WriteLog("GetClientListService failure ", true);
            }
            return jres;
        }

        public PolicyPMCResponse GetAllClientCountService()
        {
            PolicyPMCResponse jres = null;
            ActionLogger.Logger.WriteLog("GetAllClientCountService request", true);
            try
            {
                int val = Client.GetAllClientCount();
                ActionLogger.Logger.WriteLog("GetAllClientCountService value: " + val, true);
                if (val != null)
                {
                    jres = new PolicyPMCResponse(string.Format("Data found successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                    jres.NumberValue = (decimal)val;
                }
                else
                {
                    jres = new PolicyPMCResponse("Client count not found!", Convert.ToInt16(ResponseCodes.RecordNotFound), "");
                    ActionLogger.Logger.WriteLog("GetAllClientCountService 404 ", true);
                }
            }
            catch (Exception ex)
            {
                jres = new PolicyPMCResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                ActionLogger.Logger.WriteLog("GetAllClientCountService failure ", true);
            }
            return jres;
        }

        public PolicyPMCResponse GetAllClientCountinLicService(Guid LicID)
        {
            PolicyPMCResponse jres = null;
            ActionLogger.Logger.WriteLog("GetAllClientCountinLicService request: " + LicID, true);
            try
            {
                int val = Client.GetAllClientCountinLic(LicID);
                ActionLogger.Logger.WriteLog("GetAllClientCountinLicService value: " + val, true);
                if (val != null)
                {
                    jres = new PolicyPMCResponse(string.Format("Data found successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                    jres.NumberValue = (decimal)val;
                }
                else
                {
                    jres = new PolicyPMCResponse("Client count not found!", Convert.ToInt16(ResponseCodes.RecordNotFound), "");
                    ActionLogger.Logger.WriteLog("GetAllClientCountinLicService 404 ", true);
                }
            }
            catch (Exception ex)
            {
                jres = new PolicyPMCResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                ActionLogger.Logger.WriteLog("GetAllClientCountinLicService failure ", true);
            }
            return jres;
        }

        public ClientObjectResponse GetClientByClientIDService(Guid ClientID, Guid LicID)
        {
            ClientObjectResponse jres = null;
            ActionLogger.Logger.WriteLog("GetClientByClientIDService request: " + ClientID + ", licID: " +LicID, true);
            try
            {
                Client c =  Client.GetClientByClientID(ClientID, LicID);
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

        public PolicyListResponse GetClientPoliciesService(Client Clnt)
        {
            PolicyListResponse jres = null;
            ActionLogger.Logger.WriteLog("GetClientPoliciesService request: Clnt" + Clnt, true);
            try
            {
                List<PolicyDetailsData> lst = Clnt.GetPolicies();
                if (lst != null && lst.Count > 0)
                {
                    jres = new PolicyListResponse(string.Format("Policies found successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                    jres.PolicyList = lst;
                    ActionLogger.Logger.WriteLog("GetClientPoliciesService success ", true);
                }
                else
                {
                    jres = new PolicyListResponse("No data  found!", Convert.ToInt16(ResponseCodes.RecordNotFound), "");
                    ActionLogger.Logger.WriteLog("GetClientPoliciesService 404 ", true);
                }
            }
            catch (Exception ex)
            {
                jres = new PolicyListResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                ActionLogger.Logger.WriteLog("GetClientPoliciesService failure ", true);
            }
            return jres;
        }

        public ClientListResponse GetAllClientByLicChunckService(Guid LicenseeId, int skip, int take)
        {
            ClientListResponse jres = null;
            ActionLogger.Logger.WriteLog("GetAllClientByLicChunckService request: " + LicenseeId, true);
            try
            {
                IEnumerable<Client> enClient = Client.GetAllClientByLicChunck(LicenseeId, skip, take);
                if(enClient.Count() != 0)
                {
                    List<Client> lst = enClient.ToList<Client>();
                    jres = new ClientListResponse(string.Format("Client list retreived successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                    jres.ClientList = lst;
                    ActionLogger.Logger.WriteLog("GetAllClientByLicChunckService success ", true);
                }
                else
                {
                    jres = new ClientListResponse("No clients found!", Convert.ToInt16(ResponseCodes.RecordNotFound), "");
                    ActionLogger.Logger.WriteLog("GetAllClientByLicChunckService 404 ", true);
                }
            }
            catch (Exception ex)
            {
                jres = new ClientListResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                ActionLogger.Logger.WriteLog("GetAllClientByLicChunckService failure ", true);
            }
            return jres;
        }

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

        #endregion
    }
}