using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyAgencyVault.BusinessLibrary.Base;
using System.Runtime.Serialization;
using System.ServiceModel;
using MyAgencyVault.BusinessLibrary;
using MyAgencyVault.WcfService.Library.Response;
using System.ServiceModel.Web;
using MyAgencyVault.BusinessLibrary.BusinessObjects;

namespace MyAgencyVault.WcfService
{
    [ServiceContract]
    interface IPayorContactService
    {
        #region IEditable<GlobalPayorContact> Members
        //[OperationContract]
        //void AddUpdateGlobalPayorContact(GlobalPayorContact gpc);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        JSONResponse DeletePayorContact(Guid contactID);

        [OperationContract]
        GlobalPayorContact GetGlobalPayorContact(Guid id);

        [OperationContract]
        bool IsValidGlobalPayorContact(GlobalPayorContact gpc);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        PayorContactsListResponse GetContactsService(Guid PayorId, ListParams listParams);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        AddUpdatePayorContactResponse AddUpdatePayorContactDetails(PayorContact PayorData);
        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        GetPayorContactDetailsResponse GetPayorContactDetailsService(Guid payorContactId);

        #endregion
    }

    public partial class MavService : IPayorContactService
    {
        #region IGlobalPayorContact Members

        //public void AddUpdateGlobalPayorContact(GlobalPayorContact gpc)
        //{
        //    gpc.AddUpdate();
        //}

        public JSONResponse DeletePayorContact(Guid contactID)
        {
            JSONResponse jres = null;
            ActionLogger.Logger.WriteLog("DeleteGlobalPayorContact request: " + contactID, true);
            try
            {
                new GlobalPayorContact().Delete(contactID);
                jres = new JSONResponse(string.Format("Payor contact deleted successfully "), Convert.ToInt16(ResponseCodes.Success), "");
            }
            catch (Exception ex)
            {
                jres = new JSONResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                ActionLogger.Logger.WriteLog("DeleteGlobalPayorContact failure " + ex.Message, true);
            }
            return jres;

        }

        //public GlobalPayorContact GetGlobalPayorContact(Guid id)
        //{
        //    throw new NotImplementedException();
        //    //return GlobalPayorContact.GetOfId(id);
        //}

        //public bool IsValidGlobalPayorContact(GlobalPayorContact gpc)
        //{
        //    return gpc.IsValid();
        //}

        public PayorContactsListResponse GetContactsService(Guid PayorId, ListParams listParams)
        {
            PayorContactsListResponse jres = null;
            ActionLogger.Logger.WriteLog("GetPayorSourceService request: source -" + PayorId, true);
            try
            {
                int count = 0;
                List<GlobalPayorContact> list = GlobalPayorContact.getContacts(PayorId, listParams, out count);
                if (list != null && list.Count > 0)
                {
                    jres = new PayorContactsListResponse(string.Format("Payor contacts found successfully "), Convert.ToInt16(ResponseCodes.Success), "");
                    jres.ContactList = list;
                    jres.ContactsCount = count;
                    ActionLogger.Logger.WriteLog("GetContactsService failure ", true);
                }
                else
                {
                    jres = new PayorContactsListResponse(string.Format("Payor contacts not found "), Convert.ToInt16(ResponseCodes.RecordNotFound), "");
                    ActionLogger.Logger.WriteLog("GetContactsService 404 ", true);
                }
            }
            catch (Exception ex)
            {
                jres = new PayorContactsListResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                ActionLogger.Logger.WriteLog("GetContactsService failure " + ex.Message, true);
            }
            return jres;
            //return GlobalPayorContact.getContacts(PayorId);
        }
        public AddUpdatePayorContactResponse AddUpdatePayorContactDetails(PayorContact PayorData)
        {
            AddUpdatePayorContactResponse jres = null;
            ActionLogger.Logger.WriteLog("AddUpdatePayorContactDetails request process :PayorContactId" + PayorData.PayorContactId, true);
            try
            {
                int status = GlobalPayorContact.AddUpdatePayorContact(PayorData);
                if (status == 0)
                {
                    jres = new AddUpdatePayorContactResponse(string.Format("Email already exist with same name"), Convert.ToInt16(ResponseCodes.EmailAlreadyExist), "");
                    ActionLogger.Logger.WriteLog("AddUpdatePayorContactDetails: Email already exist with same name", true);
                }
                else if (status == 1)
                {
                    jres = new AddUpdatePayorContactResponse(string.Format("Payor contacts add successfully "), Convert.ToInt16(ResponseCodes.Success), "");
                    ActionLogger.Logger.WriteLog("AddUpdatePayorContact: Payor contacts add successfully ", true);
                }
                else
                {
                    jres = new AddUpdatePayorContactResponse(string.Format("Payor contacts Update successfully "), Convert.ToInt16(ResponseCodes.Success), "");
                    ActionLogger.Logger.WriteLog("AddUpdatePayorContact: contacts Update successfully ", true);
                }
                jres.Status = status;
            }
            catch (Exception ex)
            {
                jres = new AddUpdatePayorContactResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                ActionLogger.Logger.WriteLog("AddUpdatePayorContact failure" + ex.Message, true);
            }
            return jres;
            //return GlobalPayorContact.getContacts(PayorId);
        }


        public GetPayorContactDetailsResponse GetPayorContactDetailsService(Guid payorContactId)
        {
            GetPayorContactDetailsResponse jres = null;
            ActionLogger.Logger.WriteLog("GetPayorContactDetails request: payorContactId:" + payorContactId, true);
            try
            {
                PayorContact PayorContactDetails = GlobalPayorContact.GetPayorContactDetails(payorContactId);
                jres = new GetPayorContactDetailsResponse(string.Format("Payor contacts details found successfully "), Convert.ToInt16(ResponseCodes.Success), "");
                jres.PayorContactDetails = PayorContactDetails;
                ActionLogger.Logger.WriteLog("GetPayorContactDetails:contacts details found successfully " + payorContactId, true);
            }
            catch (Exception ex)
            {
                jres = new GetPayorContactDetailsResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                ActionLogger.Logger.WriteLog("GetPayorContactDetails Exception occurs" + payorContactId + "Exception:" + ex.Message, true);
            }
            return jres;
            //return GlobalPayorContact.getContacts(PayorId);
        }
        #endregion       
    }
}
