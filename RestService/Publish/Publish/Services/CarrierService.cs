using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ServiceModel;
using MyAgencyVault.BusinessLibrary;
using MyAgencyVault.WcfService.Library.Response;
using System.ServiceModel.Web;

namespace MyAgencyVault.WcfService
{
    [ServiceContract]
    interface ICarrierService
    {
        //[OperationContract]
        //ReturnStatus AddUpdateDeleteCarrier(Carrier Carr, OperationSet operationType);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        CarrierListResponse GetCarriersOnlyService(Guid LicenseeId);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        CarrierListResponse GetCarriersWithService(Guid LicenseeId, bool isCoveragesRequired);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        DisplayCarrierListResponse GetDispalyedCarriersWithService(Guid LicenseeId, bool isCoveragesRequired);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        CarrierListResponse GetPayorCarriersOnlyService(Guid PayorId);

        //[OperationContract]
        //List<Carrier> GetPayorCarriersWith(Guid PayorId, bool isCoveragesRequired);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        IDListResponse PayorCarrierGlobalService(List<Guid> PayorList);

        //[OperationContract]
        //Carrier GetPayorCarrier(Guid PayorId, Guid CarrierId);

        //[OperationContract]
        //List<Carrier> GetPayorCarriers(Guid PayorId);

        //[OperationContract]
        //bool IsValidCarrier(string carrierNickName, Guid payorId);
    }

    public partial class MavService : ICarrierService
    {
        //public ReturnStatus AddUpdateDeleteCarrier(Carrier Carr, OperationSet operationType)
        //{
        //    return Carr.AddUpdateDelete(operationType);
        //}

        public CarrierListResponse GetCarriersOnlyService(Guid LicenseeId)
        {
            CarrierListResponse jres = null;
            ActionLogger.Logger.WriteLog("GetCarriersOnly request: ", true);
            try
            {
                List<Carrier> lst = Carrier.GetCarriers(LicenseeId);
                if (lst != null && lst.Count > 0)
                {
                    jres = new CarrierListResponse(string.Format("Carrier list retreived successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                    jres.CarrierList = lst;
                    ActionLogger.Logger.WriteLog("GetCarriersOnly success ", true);
                }
                else
                {
                    jres = new CarrierListResponse("No carriers found!", Convert.ToInt16(ResponseCodes.RecordNotFound), "");
                    ActionLogger.Logger.WriteLog("GetCarriersOnly 404 ", true);
                }
            }
            catch (Exception ex)
            {
                jres = new CarrierListResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                ActionLogger.Logger.WriteLog("GetCarriersOnly failure ", true);
            }
            return jres;
        }

        public DisplayCarrierListResponse GetDispalyedCarriersWithService(Guid LicenseeId, bool isCoveragesRequired)
        {
//return 
            DisplayCarrierListResponse jres = null;
            ActionLogger.Logger.WriteLog("GetDispalyedCarriersWith request: ", true);
            try
            {
                List<DisplayedCarrier> lst = Carrier.GetDispalyedCarriers(LicenseeId, isCoveragesRequired);
                if (lst != null && lst.Count > 0)
                {
                    jres = new DisplayCarrierListResponse(string.Format("Carrier list retreived successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                    jres.DisplayCarrierList = lst;
                    ActionLogger.Logger.WriteLog("GetDispalyedCarriersWith success ", true);
                }
                else
                {
                    jres = new DisplayCarrierListResponse("No carriers found!", Convert.ToInt16(ResponseCodes.RecordNotFound), "");
                    ActionLogger.Logger.WriteLog("GetDispalyedCarriersWith 404 ", true);
                }
            }
            catch (Exception ex)
            {
                jres = new DisplayCarrierListResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                ActionLogger.Logger.WriteLog("GetDispalyedCarriersWith failure ", true);
            }
            return jres;
        }

        public CarrierListResponse GetCarriersWithService(Guid LicenseeId, bool isCoveragesRequired)
        {
            CarrierListResponse jres = null;
            ActionLogger.Logger.WriteLog("GetCarriersWith request: ", true);
            try
            {
                List<Carrier> lst = Carrier.GetCarriers(LicenseeId, isCoveragesRequired);
                if (lst != null && lst.Count > 0)
                {
                    jres = new CarrierListResponse(string.Format("Carrier list retreived successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                    jres.CarrierList = lst;
                    ActionLogger.Logger.WriteLog("GetCarriersWith success ", true);
                }
                else
                {
                    jres = new CarrierListResponse("No carriers found!", Convert.ToInt16(ResponseCodes.RecordNotFound), "");
                    ActionLogger.Logger.WriteLog("GetCarriersWith 404 ", true);
                }
            }
            catch (Exception ex)
            {
                jres = new CarrierListResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                ActionLogger.Logger.WriteLog("GetCarriersWith failure ", true);
            }
            return jres;
        }

        public CarrierListResponse GetPayorCarriersOnlyService(Guid PayorId)
        {
            CarrierListResponse jres = null;
            ActionLogger.Logger.WriteLog("GetPayorCarriersOnly request: ", true);
            try
            {
                List<Carrier> lst = Carrier.GetPayorCarriers(PayorId);
                if (lst != null && lst.Count > 0)
                {
                    jres = new CarrierListResponse(string.Format("Carrier list retreived successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                    jres.CarrierList = lst;
                    ActionLogger.Logger.WriteLog("GetPayorCarriersOnly success ", true);
                }
                else
                {
                    jres = new CarrierListResponse("No carriers found!", Convert.ToInt16(ResponseCodes.RecordNotFound), "");
                    ActionLogger.Logger.WriteLog("GetPayorCarriersOnly 404 ", true);
                }
            }
            catch (Exception ex)
            {
                jres = new CarrierListResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                ActionLogger.Logger.WriteLog("GetPayorCarriersOnly failure ", true);
            }
            return jres;
        }

        //Duplicate code as above method
        //public List<Carrier> GetPayorCarriersWith(Guid PayorId, bool isCoveragesRequired)
        //{
        //    return Carrier.GetPayorCarriers(PayorId);
        //}

        public IDListResponse PayorCarrierGlobalService(List<Guid> PayorList)
        {
            IDListResponse jres = null;
            ActionLogger.Logger.WriteLog("PayorCarrierGlobalService request: ", true);
            try
            {
                List<Guid> lst = Carrier.PayorCarrierGlobal(PayorList);
                if (lst != null && lst.Count > 0)
                {
                    jres = new IDListResponse(string.Format("Carrier list retreived successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                    jres.IDList = lst;
                    ActionLogger.Logger.WriteLog("PayorCarrierGlobalService success ", true);
                }
                else
                {
                    jres = new IDListResponse("No carriers found!", Convert.ToInt16(ResponseCodes.RecordNotFound), "");
                    ActionLogger.Logger.WriteLog("PayorCarrierGlobalService 404 ", true);
                }
            }
            catch (Exception ex)
            {
                jres = new IDListResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                ActionLogger.Logger.WriteLog("PayorCarrierGlobalService failure ", true);
            }
            return jres;
        }

      /*  public Carrier GetPayorCarrier(Guid PayorId, Guid CarrierId)
        {
            return Carrier.GetPayorCarrier(PayorId, CarrierId);
        }

        public List<Carrier> GetPayorCarriers(Guid PayorId)
        {
            return Carrier.GetPayorCarriers(PayorId);
        }

        public bool IsValidCarrier(string carrierNickName, Guid payorId)
        {
            return Carrier.IsValidCarrier(carrierNickName, payorId);
        }*/
    }
}