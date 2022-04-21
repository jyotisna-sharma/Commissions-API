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
    interface IPayorService
    {
        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        PolicyPMCResponse GetDisplayPayorCountService(Guid? LicenseeId, PayorFillInfo PayerfillInfo);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        DisplayPayorListResponse GetDisplayPayorsInChunkService(Guid? LicenseeId, PayorFillInfo PayerfillInfo, int skip, int take);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        PayorListResponse  GetPayorsService(Guid? LicenseeId, PayorFillInfo PayerfillInfo);
    }

    public partial class MavService : IPayorService
    {
        public PolicyPMCResponse GetDisplayPayorCountService(Guid? LicenseeId, PayorFillInfo PayerfillInfo)
        {
            PolicyPMCResponse jres = null;
            ActionLogger.Logger.WriteLog("GetDisplayPayorCount request: " + LicenseeId, true);
            try
            {
                int val = DisplayedPayor.GetPayorCount(LicenseeId, PayerfillInfo);
                ActionLogger.Logger.WriteLog("GetDisplayPayorCount value: " + val, true);
                if (val != null)
                {
                    jres = new PolicyPMCResponse(string.Format("Data found successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                    jres.NumberValue = (decimal)val;
                }
                else
                {
                    jres = new PolicyPMCResponse("Payor count not found!", Convert.ToInt16(ResponseCodes.RecordNotFound), "");
                    ActionLogger.Logger.WriteLog("GetDisplayPayorCount 404 ", true);
                }
            }
            catch (Exception ex)
            {
                jres = new PolicyPMCResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                ActionLogger.Logger.WriteLog("GetDisplayPayorCount failure ", true);
            }
            return jres;
        }

        public DisplayPayorListResponse GetDisplayPayorsInChunkService(Guid? LicenseeId, PayorFillInfo PayerfillInfo, int skip, int take)
        {
         
            DisplayPayorListResponse jres = null;
            ActionLogger.Logger.WriteLog("GetDisplayPayorsInChunk request: ", true);
            try
            {
                List<DisplayedPayor> lst = DisplayedPayor.GetPayors(LicenseeId, PayerfillInfo, skip, take);
                if (lst != null && lst.Count > 0)
                {
                    jres = new DisplayPayorListResponse(string.Format("Payor list retreived successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                    jres.PayorList = lst;
                    ActionLogger.Logger.WriteLog("GetDisplayPayorsInChunk success ", true);
                }
                else
                {
                    jres = new DisplayPayorListResponse("No payors found!", Convert.ToInt16(ResponseCodes.RecordNotFound), "");
                    ActionLogger.Logger.WriteLog("GetDisplayPayorsInChunk 404 ", true);
                }
            }
            catch (Exception ex)
            {
                jres = new DisplayPayorListResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                ActionLogger.Logger.WriteLog("GetDisplayPayorsInChunk failure ", true);
            }
            return jres;
        }

        public PayorListResponse GetPayorsService(Guid? LicenseeId, PayorFillInfo PayerfillInfo)
        {
            PayorListResponse jres = null;
            ActionLogger.Logger.WriteLog("GetPayorsService request: ", true);
            try
            {
                List<Payor> lst = Payor.GetPayors(LicenseeId, PayerfillInfo);
                if (lst != null && lst.Count > 0)
                {
                    jres = new PayorListResponse(string.Format("Payor list retreived successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                    jres.PayorList = lst;
                    ActionLogger.Logger.WriteLog("GetPayorsService success ", true);
                }
                else
                {
                    jres = new PayorListResponse("No payors found!", Convert.ToInt16(ResponseCodes.RecordNotFound), "");
                    ActionLogger.Logger.WriteLog("GetPayorsService 404 ", true);
                }
            }
            catch (Exception ex)
            {
                jres = new PayorListResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                ActionLogger.Logger.WriteLog("GetPayorsService failure ", true);
            }
            return jres;
        }
    }
  
}