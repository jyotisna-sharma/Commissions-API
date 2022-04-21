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
        //[WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        //ReturnStatusResponse AddUpdateDeleteCarrierService(Carrier Carr, OperationSet operationType);

        //[OperationContract]
        //[WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        //CarrierListResponse GetCarriersWithService(Guid LicenseeId, bool isCoveragesRequired);

        //[OperationContract]
        //[WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        //DisplayCarrierListResponse GetDispalyedCarriersWithService(Guid LicenseeId, bool isCoveragesRequired);

        //[OperationContract]
        //[WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        //CarrierListResponse GetPayorCarriersOnlyService(Guid PayorId);

        ////[OperationContract]
        ////List<Carrier> GetPayorCarriersWith(Guid PayorId, bool isCoveragesRequired);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        IDListResponse PayorCarrierGlobalService(Guid licenseeID, PayorFillInfo payorfillInfo);

        //[OperationContract]
        //[WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        //CarrierResponse GetPayorCarrierService(Guid PayorId, Guid CarrierId);

        //[OperationContract]
        //[WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        //CarrierListResponse GetPayorCarriersService(Guid PayorId);

        //[OperationContract]
        //[WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        //PolicyBoolResponse IsValidCarrierService(string carrierNickName, Guid payorId);
        //[OperationContract]
        //[WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        //CarrierResponse GetCarrierDetailService(Guid carrierId);


        #region
        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        ReportCarrierListResponse GetCarriersOnlyService(Guid licenseeId);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        CarrierListResponse GetCarrierListService(Guid payorId);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        CarrierListResponse GetCarrierListingService(Guid payorId, ListParams listParams);

        [OperationContract] 
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        JSONResponse AddUpdateCarrierService(Carrier CarrierDetails);



        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        ReturnStatusResponse DeleteCarrierService(Guid carrierId, Guid payorId, bool deleteFlag);
        #endregion
    }

    public partial class MavService : ICarrierService
    {

        /// <summary>
        /// Modified by:Ankit khandelwal
        /// Modified on:16-5-2019
        /// purpose:to get the carrier list based on LicenseeId
        /// </summary>
        /// <param name="LicenseeId"></param>
        /// <returns></returns>
        public ReportCarrierListResponse GetCarriersOnlyService(Guid licenseeId)
        {
            ReportCarrierListResponse jres = null;
            ActionLogger.Logger.WriteLog("GetCarriersOnlyService request: licenseeId - " + licenseeId, true);
            try
            {
                List<DisplayedCarrier> lst = Carrier.GetCarriers(licenseeId);
                if (lst != null && lst.Count > 0)
                {
                    jres = new ReportCarrierListResponse(string.Format("Carrier list retreived successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                    jres.CarrierList = lst;
                    ActionLogger.Logger.WriteLog("GetCarriersOnly success ", true);
                }
                else
                {
                    jres = new ReportCarrierListResponse("No carriers found!", Convert.ToInt16(ResponseCodes.RecordNotFound), "");
                    ActionLogger.Logger.WriteLog("GetCarriersOnly 404 ", true);
                }
            }
            catch (Exception ex)
            {
                jres = new ReportCarrierListResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                ActionLogger.Logger.WriteLog("GetCarriersOnly failure ", true);
            }
            return jres;
        }
        /// <summary>
        /// Create by :Ankit Khandelwal
        /// Created On: 12-03-2019
        /// Purpose:Getting List of Carriers
        /// </summary>
        /// <param name="payorId"></param>
        /// <returns></returns>
        public CarrierListResponse GetCarrierListService(Guid payorId)
        {
            CarrierListResponse jres = null;
            ActionLogger.Logger.WriteLog("GetPayorCarriersOnly request:  payorId - " + payorId, true);
            try
            {
                List<Carrier> lst = Carrier.GetCarrierList(payorId);
                    jres = new CarrierListResponse(string.Format("Carrier list retreived successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                    jres.CarrierList = lst;
                    ActionLogger.Logger.WriteLog("GetPayorCarriersOnly success ", true);
            }
            catch (Exception ex)
            {
                jres = new CarrierListResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                ActionLogger.Logger.WriteLog("GetPayorCarriersOnly failure ", true);
            }
            return jres;
        }
        public CarrierListResponse GetCarrierListingService(Guid payorId, ListParams listParams)
        {
            CarrierListResponse response = null;
            ActionLogger.Logger.WriteLog("GetCarrierListingService request: payor - " + payorId + ", listParams: " + listParams.ToStringDump(), true);
            try
            {
                List<Carrier> lst = Carrier.GetCarrierListing(payorId, listParams, out string totalCount);
                response = new CarrierListResponse(string.Format("Carrier list retreived successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                response.CarrierList = lst;
                response.TotalCount = totalCount;
                ActionLogger.Logger.WriteLog("GetCarrierListingService success ", true);
            }
            catch (Exception ex)
            {
                response = new CarrierListResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                ActionLogger.Logger.WriteLog("GetPayorCarriersOnly failure ", true);
            }
            return response;
        }
        /// <summary>
        /// Created By :Ankit khandelwal
        /// Created on:07-08-2019
        /// Purpose:Add update details of carriers
        /// </summary>
        /// <param name="CarrierDetails"></param>
        /// <returns></returns>
        public JSONResponse AddUpdateCarrierService(Carrier CarrierDetails)
        {
            JSONResponse jres = null;
            ActionLogger.Logger.WriteLog("AddUpdateCarrierService request: Carrier - " + CarrierDetails.ToStringDump(), true);
            try
            {
                int status = Carrier.AddUpdateCarrierDetails(CarrierDetails);
                if (status == 1)
                {
                    jres = new JSONResponse(_resourceManager.GetString("CarrierSuccessfullyAdded"), (int)ResponseCodes.Success, "");
                    ActionLogger.Logger.WriteLog("AddUpdateCarrierService: Carrier added successfully in the system CarrierDetails:" + CarrierDetails.ToStringDump(), true);
                }
                else if (status == 2)
                {
                    jres = new JSONResponse(_resourceManager.GetString("CarrierSuccessfullyUpdate"), (int)ResponseCodes.Success, "");
                    ActionLogger.Logger.WriteLog("AddUpdateCarrierService: Carrier update successfully in the system CarrierDetails:" + CarrierDetails.ToStringDump(), true);
                }
                else if (status == 0)
                {
                    jres = new JSONResponse(_resourceManager.GetString("CarrierNickNameExist"), (int)ResponseCodes.UserAlreadyExist, "");
                    ActionLogger.Logger.WriteLog("AddUpdateCarrierService: Carrier nickname already exist in the system CarrierDetails:" + CarrierDetails.ToStringDump(), true);
                }
                else if (status == 3)
                {
                    jres = new JSONResponse(_resourceManager.GetString("CarrierNameNickNameExist"), (int)ResponseCodes.UserAlreadyExist, "");
                    ActionLogger.Logger.WriteLog("AddUpdateCarrierService: Carrier Name and nicknamealready exist in the system CarrierDetails:" + CarrierDetails.ToStringDump(), true);
                }
                else if(status==4)
                {
                    jres = new JSONResponse(_resourceManager.GetString("CarrierAlreadyExist"), (int)ResponseCodes.UserAlreadyExist, "");
                    ActionLogger.Logger.WriteLog("AddUpdateCarrierService: Carrier already exist in the system CarrierDetails:" + CarrierDetails.ToStringDump(), true);
                }
            }
            catch (Exception ex)
            {
                jres = new JSONResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                ActionLogger.Logger.WriteLog("AddUpdateCarrierService failure:Exception:" + ex.Message, true);
            }
            return jres;
        }

        /// <summary>
        /// Created by:Ankit khandelwal
        /// CreatedOn:07-08-2019
        /// Purpose:Delete a carrier based on carrierId and payorId
        /// </summary>
        /// <param name="carrierId"></param>
        /// <param name="payorId"></param>
        /// <param name="deleteFlag"></param>
        /// <returns></returns>
        public ReturnStatusResponse DeleteCarrierService(Guid carrierId, Guid payorId, bool deleteFlag = false)
        {
            ActionLogger.Logger.WriteLog("DeleteCarrierService request: CarrierID - " + carrierId + ", payorID: " + payorId + ", deleteFlag - "  + deleteFlag, true);
            ReturnStatusResponse response = null;
            try
            {
                bool isDeleteallowed = Carrier.DeleteCarrier(carrierId, payorId, deleteFlag);
                response = new ReturnStatusResponse("Delete request successfull", Convert.ToInt16(ResponseCodes.Success), "");
                response.RecordStatus = isDeleteallowed;
                ActionLogger.Logger.WriteLog("DeleteCarrierService success", true);
            }
            catch (Exception ex)
            {
                response = new ReturnStatusResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                ActionLogger.Logger.WriteLog("DeleteCarrierService failure Exception occurs:" + ex.Message, true);
            }

            return response;
        }
        /// <summary>
        /// Purpose:Used for getting list of submitted through
        /// MOdifiedBy:Ankit khandelwal
        /// </summary>
        /// <param name="licenseeID"></param>
        /// <param name="payorfillInfo"></param>
        /// <returns></returns>
        public IDListResponse PayorCarrierGlobalService(Guid licenseeID, PayorFillInfo payorfillInfo)
        {
            IDListResponse jres = null;
            ActionLogger.Logger.WriteLog("PayorCarrierGlobalService request: ", true);
            try
            {
                List<Payor> lst = Carrier.PayorCarrierGlobal(licenseeID, payorfillInfo);
                if (lst != null && lst.Count > 0)
                {
                    jres = new IDListResponse(string.Format("Carrier list retreived successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                    jres.SubmittedThroughList = lst;
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

        //public CarrierResponse GetCarrierDetailService(Guid carrierId)
        //{
        //    CarrierResponse response = null;
        //    ActionLogger.Logger.WriteLog("AddUpdateCarrierService request: ", true);
        //    try
        //    {
        //        Carrier data = Carrier.GetCarrierDetail(carrierId);
        //        response = new CarrierResponse(_resourceManager.GetString("CarrierAlreadyExist"), (int)ResponseCodes.Success, "");
        //        response.CarrierObject = data;
        //        ActionLogger.Logger.WriteLog("AddUpdateCarrierService: Carrier already exist in the system CarrierDetails:" + carrierId, true);
        //    }
        //    catch (Exception ex)
        //    {
        //        response = new CarrierResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
        //        ActionLogger.Logger.WriteLog("AddUpdateDeleteCarrierService failure ", true);
        //    }
        //    return response;
        //}

        //public ReturnStatusResponse AddUpdateDeleteCarrierService(Carrier Carr, OperationSet operationType)
        //{
        //    ReturnStatusResponse jres = null;
        //    ActionLogger.Logger.WriteLog("AddUpdateDeleteCarrierService request: ", true);
        //    try
        //    {
        //        ReturnStatus r = Carr.AddUpdateDelete(operationType);
        //        jres = new ReturnStatusResponse(string.Format("Carrier details processed successfully"), Convert.ToInt16(ResponseCodes.Success), "");
        //        jres.Status = r;
        //        ActionLogger.Logger.WriteLog("AddUpdateDeleteCarrierService success ", true);
        //    }
        //    catch (Exception ex)
        //    {
        //        jres = new ReturnStatusResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
        //        ActionLogger.Logger.WriteLog("AddUpdateDeleteCarrierService failure ", true);
        //    }
        //    return jres;
        //}

        //public DisplayCarrierListResponse GetDispalyedCarriersWithService(Guid LicenseeId, bool isCoveragesRequired)
        //{
        //    //return 
        //    DisplayCarrierListResponse jres = null;
        //    ActionLogger.Logger.WriteLog("GetDispalyedCarriersWith request: ", true);
        //    try
        //    {
        //        List<DisplayedCarrier> lst = Carrier.GetDispalyedCarriers(LicenseeId, isCoveragesRequired);
        //        if (lst != null && lst.Count > 0)
        //        {
        //            jres = new DisplayCarrierListResponse(string.Format("Carrier list retreived successfully"), Convert.ToInt16(ResponseCodes.Success), "");
        //            jres.DisplayCarrierList = lst;
        //            ActionLogger.Logger.WriteLog("GetDispalyedCarriersWith success ", true);
        //        }
        //        else
        //        {
        //            jres = new DisplayCarrierListResponse("No carriers found!", Convert.ToInt16(ResponseCodes.RecordNotFound), "");
        //            ActionLogger.Logger.WriteLog("GetDispalyedCarriersWith 404 ", true);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        jres = new DisplayCarrierListResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
        //        ActionLogger.Logger.WriteLog("GetDispalyedCarriersWith failure ", true);
        //    }
        //    return jres;
        //}

        //public CarrierListResponse GetCarriersWithService(Guid LicenseeId, bool isCoveragesRequired)
        //{
        //    CarrierListResponse jres = null;
        //    ActionLogger.Logger.WriteLog("GetCarriersWith request: ", true);
        //    try
        //    {
        //        List<Carrier> lst = Carrier.GetCarriers(LicenseeId, isCoveragesRequired);
        //        if (lst != null && lst.Count > 0)
        //        {
        //            jres = new CarrierListResponse(string.Format("Carrier list retreived successfully"), Convert.ToInt16(ResponseCodes.Success), "");
        //            jres.CarrierList = lst;
        //            ActionLogger.Logger.WriteLog("GetCarriersWith success ", true);
        //        }
        //        else
        //        {
        //            jres = new CarrierListResponse("No carriers found!", Convert.ToInt16(ResponseCodes.RecordNotFound), "");
        //            ActionLogger.Logger.WriteLog("GetCarriersWith 404 ", true);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        jres = new CarrierListResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
        //        ActionLogger.Logger.WriteLog("GetCarriersWith failure ", true);
        //    }
        //    return jres;
        //}

        //public CarrierListResponse GetPayorCarriersOnlyService(Guid PayorId)
        //{
        //    CarrierListResponse jres = null;
        //    ActionLogger.Logger.WriteLog("GetPayorCarriersOnly request: ", true);
        //    try
        //    {
        //        List<Carrier> lst = Carrier.GetPayorCarriers(PayorId);
        //        if (lst != null && lst.Count > 0)
        //        {
        //            jres = new CarrierListResponse(string.Format("Carrier list retreived successfully"), Convert.ToInt16(ResponseCodes.Success), "");
        //            jres.CarrierList = lst;
        //            ActionLogger.Logger.WriteLog("GetPayorCarriersOnly success ", true);
        //        }
        //        else
        //        {
        //            jres = new CarrierListResponse("No carriers found!", Convert.ToInt16(ResponseCodes.RecordNotFound), "");
        //            ActionLogger.Logger.WriteLog("GetPayorCarriersOnly 404 ", true);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        jres = new CarrierListResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
        //        ActionLogger.Logger.WriteLog("GetPayorCarriersOnly failure ", true);
        //    }
        //    return jres;
        //}

        //Duplicate code as above method
        //public List<Carrier> GetPayorCarriersWith(Guid PayorId, bool isCoveragesRequired)
        //{
        //    return Carrier.GetPayorCarriers(PayorId);
        //}

        //public IDListResponse PayorCarrierGlobalService(Guid licenseeID, PayorFillInfo payorfillInfo)
        //{
        //    IDListResponse jres = null;
        //    ActionLogger.Logger.WriteLog("PayorCarrierGlobalService request: ", true);
        //    try
        //    {
        //        List<Guid> lst = Carrier.PayorCarrierGlobal(licenseeID, payorfillInfo);
        //        if (lst != null && lst.Count > 0)
        //        {
        //            jres = new IDListResponse(string.Format("Carrier list retreived successfully"), Convert.ToInt16(ResponseCodes.Success), "");
        //            jres.IDList = lst;
        //            ActionLogger.Logger.WriteLog("PayorCarrierGlobalService success ", true);
        //        }
        //        else
        //        {
        //            jres = new IDListResponse("No carriers found!", Convert.ToInt16(ResponseCodes.RecordNotFound), "");
        //            ActionLogger.Logger.WriteLog("PayorCarrierGlobalService 404 ", true);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        jres = new IDListResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
        //        ActionLogger.Logger.WriteLog("PayorCarrierGlobalService failure ", true);
        //    }
        //    return jres;
        //}

        //public CarrierResponse GetPayorCarrierService(Guid PayorId, Guid CarrierId)
        //{
        //    CarrierResponse jres = null;
        //    ActionLogger.Logger.WriteLog("GetPayorCarrierService request: " + PayorId + ", Carrierid: " + CarrierId, true);
        //    try
        //    {
        //        Carrier obj = Carrier.GetPayorCarrier(PayorId, CarrierId);

        //        if (obj != null)
        //        {
        //            jres = new CarrierResponse(string.Format("Carrier details found successfully"), Convert.ToInt16(ResponseCodes.Success), "");
        //            ActionLogger.Logger.WriteLog("GetPayorCarrierService success ", true);
        //            jres.CarrierObject = obj;
        //        }
        //        else
        //        {
        //            jres = new CarrierResponse(string.Format("Policy details could not be found"), Convert.ToInt16(ResponseCodes.RecordNotFound), "");
        //            ActionLogger.Logger.WriteLog("GetPayorCarrierService 404 ", true);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        jres = new CarrierResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
        //        ActionLogger.Logger.WriteLog("GetPayorCarrierService failure ", true);
        //    }
        //    return jres;
        //}

        //public CarrierListResponse GetPayorCarriersService(Guid PayorId)
        //{
        //    CarrierListResponse jres = null;
        //    ActionLogger.Logger.WriteLog("GetPayorCarriersService request " + PayorId, true);
        //    try
        //    {
        //        List<Carrier> lst = Carrier.GetPayorCarriers(PayorId);
        //        if (lst != null && lst.Count > 0)
        //        {
        //            jres = new CarrierListResponse(string.Format("Data found successfully"), Convert.ToInt16(ResponseCodes.Success), "");
        //            jres.CarrierList = lst;
        //            ActionLogger.Logger.WriteLog("GetPayorCarriersService success ", true);
        //        }
        //        else
        //        {
        //            jres = new CarrierListResponse(string.Format("No data found "), Convert.ToInt16(ResponseCodes.RecordNotFound), "");
        //            ActionLogger.Logger.WriteLog("GetPayorCarriersService 404 ", true);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        jres = new CarrierListResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
        //        ActionLogger.Logger.WriteLog("GetPayorCarriersService failure ", true);
        //    }
        //    return jres;
        //}

        //public PolicyBoolResponse IsValidCarrierService(string carrierNickName, Guid payorId)
        //{
        //    PolicyBoolResponse jres = null;
        //    ActionLogger.Logger.WriteLog("IsValidCarrierService request: carrierNickName -" + carrierNickName + ", payorId: " + payorId, true);
        //    try
        //    {
        //        bool res = Carrier.IsValidCarrier(carrierNickName, payorId);
        //        jres = new PolicyBoolResponse(string.Format("Carrier validity found successfully"), Convert.ToInt16(ResponseCodes.Success), "");
        //        jres.BoolFlag = res;
        //        ActionLogger.Logger.WriteLog("IsValidCarrierService success ", true);
        //    }
        //    catch (Exception ex)
        //    {
        //        jres = new PolicyBoolResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
        //        ActionLogger.Logger.WriteLog("IsValidCarrierService failure ", true);
        //    }
        //    return jres;
        //}
    }
}