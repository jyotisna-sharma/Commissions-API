using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyAgencyVault.BusinessLibrary;
using System.ServiceModel;
using System.ServiceModel.Web;
using MyAgencyVault.WcfService.Library.Response;

namespace MyAgencyVault.WcfService
{
    [ServiceContract]
    interface IPayorSiteService
    {
        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        JSONResponse AddUpdatePayorUserWebSiteService(PayorSiteLoginInfo plSiteinfo);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        JSONResponse DeletePayorUserWebSiteService(PayorSiteLoginInfo plSiteinfo);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        PayorSiteResponse GetPayorUsersService(Guid licId, Guid payorId);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        PayorSiteResponse  GetLicenseeUsersService(Guid licId);
    }

    public partial class MavService : IPayorSiteService
    {
        #region IPayorUserWebSite Members

        public PayorSiteResponse GetPayorUsersService(Guid licId, Guid payorId)
        {
            PayorSiteResponse jres = null;
            ActionLogger.Logger.WriteLog("GetPayorUsersService request: " + licId + ", payorid: " + payorId, true);
            try
            {
                List<PayorSiteLoginInfo> lst = PayorSiteLoginInfo.GetPayorUserWebSite(licId, payorId);
                if (lst != null && lst.Count > 0)
                {
                    jres = new PayorSiteResponse(string.Format("Payor users found successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                    jres.PayorSiteList = lst;
                }
                else
                {
                    jres = new PayorSiteResponse(string.Format("No payor users found"), Convert.ToInt16(ResponseCodes.RecordNotFound), "");
                }

                ActionLogger.Logger.WriteLog("GetPayorUsersService success ", true);
            }
            catch (Exception ex)
            {
                jres = new PayorSiteResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                ActionLogger.Logger.WriteLog("GetPayorUsersService failure ", true);
            }
            return jres;
        }

        public PayorSiteResponse GetLicenseeUsersService(Guid licId)
        {
            PayorSiteResponse jres = null;
            ActionLogger.Logger.WriteLog("GetLicenseeUsersService request: " + licId, true);
            try
            {
                List<PayorSiteLoginInfo> lst = PayorSiteLoginInfo.GetLicenseeUsers(licId);
                if (lst != null && lst.Count > 0)
                {
                    jres = new PayorSiteResponse(string.Format("Licensee users found successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                    jres.PayorSiteList = lst;
                }
                else
                {
                    jres = new PayorSiteResponse(string.Format("No licensee users found"), Convert.ToInt16(ResponseCodes.RecordNotFound), "");
                }

                ActionLogger.Logger.WriteLog("GetLicenseeUsersService success ", true);
            }
            catch (Exception ex)
            {
                jres = new PayorSiteResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                ActionLogger.Logger.WriteLog("GetLicenseeUsersService failure ", true);
            }
            return jres;
        }

        public JSONResponse AddUpdatePayorUserWebSiteService(PayorSiteLoginInfo plSiteinfo)
        {
            JSONResponse jres = null;
            ActionLogger.Logger.WriteLog("AddUpdatePayorUserWebSiteService request: " + plSiteinfo.ToStringDump(), true);
            try
            {
                plSiteinfo.AddUpdate();
                jres = new JSONResponse(string.Format("Payor site saved successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                ActionLogger.Logger.WriteLog("AddUpdatePayorUserWebSiteService success ", true);
            }
            catch (Exception ex)
            {
                jres = new JSONResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                ActionLogger.Logger.WriteLog("AddUpdatePayorUserWebSiteService failure ", true);
            }
            return jres;
        }

        public JSONResponse DeletePayorUserWebSiteService(PayorSiteLoginInfo plSiteinfo)
        {
             JSONResponse jres = null;
             ActionLogger.Logger.WriteLog("DeletePayorUserWebSiteService request: " + plSiteinfo.ToStringDump(), true);
            try
            {
                plSiteinfo.Delete();
                jres = new JSONResponse(string.Format("Payor site deleted successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                ActionLogger.Logger.WriteLog("DeletePayorUserWebSiteService success ", true);
            }
            catch (Exception ex)
            {
                jres = new JSONResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                ActionLogger.Logger.WriteLog("DeletePayorUserWebSiteService failure ", true);
            }
            return jres;
        }

        #endregion
    }
}