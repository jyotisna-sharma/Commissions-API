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
    interface ICoverageService
    {



        //[OperationContract]
        //[WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        //PolicyStringResponse GetProductTypeNickNameService(Guid PolicyID, Guid PayorID, Guid CarrierID, Guid CoverageID);

        //[OperationContract]
        //[WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        //ReturnStatusResponse AddUpdateDeleteCoverageService(Coverage Covrage, OperationSet operationType);

        //[OperationContract]
        //[WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        //CoverageResponse GetCarrierCoverageService(Guid PayorId, Guid CarrierId, Guid CoverageId);


        //[OperationContract]
        //[WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        //CoverageListResponse GetCarrierCoveragesService(Guid CarrierId);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        DisplayCoverageListResponse GetDisplayedCarrierCoveragesService(Guid LicenseeId);

        //[OperationContract]
        //[WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        //CoverageListResponse GetPayorCarrierCoveragesService(Guid PayorId, Guid CarrierId);


        //[OperationContract]
        //[WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        //PolicyBoolResponse IsValidCoverageService(string carrierNickName, string coverageNickName, Guid payorId);

        //[OperationContract]
        //[WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        //PolicyStringResponse GetCoverageNickNameService(Guid PayorId, Guid CarrierId, Guid CoverageId);

        //[OperationContract]
        //[WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        //DisplayCoverageResponse GetCoverageForPolicyService(Guid DisplayedCoverageID);



        //[OperationContract]
        //[WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        //ReturnStatusResponse DeleteProductTypeService(Guid guidPayorID, Guid guidCarrierID, Guid guidCoverageId, string strNickNames);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        CoverageNickNameResponse GetAllNickNamesService(Guid payorId, Guid carrierId, Guid coverageId, ListParams listParams);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        CoverageNickNameResponse GetProductTypeService(Guid PayorId, Guid CarrierId, Guid CoverageId);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        ReturnStatusResponse AddUpdateCoverageTypeService(CoverageNickName coverageDetails);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        ReturnStatusResponse AddUpdateCoverageService(Coverage coverageDetails);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        ReturnStatusResponse DeleteNickNameService(int coverageNickId, string coverageNickName, bool flag);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        CoverageListResponse GetCoveragesListingService(Guid licenseeId, ListParams listParams);
        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        ReturnStatusResponse DeleteCoverageService(Guid coverageId, bool flag);
    }

    public partial class MavService : ICoverageService
    {
        //public PolicyStringResponse GetProductTypeNickNameService(Guid PolicyID, Guid PayorID, Guid CarrierID, Guid CoverageID)
        //{
        //    PolicyStringResponse jres = null;
        //    ActionLogger.Logger.WriteLog("GetProductTypeNickName request: " + PolicyID + ", PayorID: " + PayorID + ", carrier: " + CarrierID + ", coverage:" + CoverageID, true);
        //    try
        //    {
        //        string val = DEU.GetProductTypeNickName(PolicyID, PayorID, CarrierID, CoverageID);
        //        ActionLogger.Logger.WriteLog("GetProductTypeNickName value: " + val, true);
        //        if (!string.IsNullOrWhiteSpace(val))
        //        {
        //            jres = new PolicyStringResponse(string.Format("Product type found successfully"), Convert.ToInt16(ResponseCodes.Success), "");
        //            jres.StringValue = val;
        //        }
        //        else
        //        {
        //            jres = new PolicyStringResponse("Product type not found!", Convert.ToInt16(ResponseCodes.RecordNotFound), "");
        //            ActionLogger.Logger.WriteLog("GetProductTypeNickName 404 ", true);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        jres = new PolicyStringResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
        //        ActionLogger.Logger.WriteLog("GetProductTypeNickName failure ", true);
        //    }
        //    return jres;
        //}

        //public ReturnStatusResponse AddUpdateDeleteCoverageService(Coverage Covrage, OperationSet operationType)
        //{
        //    ReturnStatusResponse jres = null;
        //    ActionLogger.Logger.WriteLog("AddUpdateDeleteCoverageService request: Covrage -" + Covrage.ToStringDump() + ", operationType : " + operationType, true);
        //    try
        //    {
        //        ReturnStatus obj = Covrage.AddUpdateDelete(Covrage, operationType);

        //        if (obj != null)
        //        {
        //            if (obj.IsError)
        //            {
        //                jres = new ReturnStatusResponse(string.Format("Coverage could not be processed "), Convert.ToInt16(ResponseCodes.Failure), "");
        //            }
        //            else
        //            {
        //                jres = new ReturnStatusResponse(string.Format("Coverage processed successfully "), Convert.ToInt16(ResponseCodes.Success), "");
        //            }
        //            jres.Status = obj;
        //            ActionLogger.Logger.WriteLog("AddUpdateDeleteCoverageService success ", true);
        //        }
        //        else
        //        {
        //            jres = new ReturnStatusResponse(string.Format("Coverage could not be processed"), Convert.ToInt16(ResponseCodes.Failure), "");
        //            ActionLogger.Logger.WriteLog("AddUpdateDeleteCoverageService failure ", true);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        jres = new ReturnStatusResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
        //        ActionLogger.Logger.WriteLog("AddUpdateDeleteCoverageService failure ", true);
        //    }
        //    return jres;
        //}

        //public CoverageResponse GetCarrierCoverageService(Guid PayorId, Guid CarrierId, Guid CoverageId)
        //{
        //    CoverageResponse jres = null;
        //    ActionLogger.Logger.WriteLog("GetCarrierCoverageService request:  PayorID: " + PayorId + ", carrier: " + CarrierId + ", coverage:" + CoverageId, true);
        //    try
        //    {
        //        Coverage obj = Coverage.GetCarrierCoverage(PayorId, CarrierId, CoverageId);
        //        if (obj != null)
        //        {
        //            jres = new CoverageResponse(string.Format("Coverage found successfully"), Convert.ToInt16(ResponseCodes.Success), "");
        //            jres.CoverageObject = obj;
        //        }
        //        else
        //        {
        //            jres = new CoverageResponse("Coverage not found!", Convert.ToInt16(ResponseCodes.RecordNotFound), "");
        //            ActionLogger.Logger.WriteLog("GetCarrierCoverageService 404 ", true);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        jres = new CoverageResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
        //        ActionLogger.Logger.WriteLog("GetCarrierCoverageService failure ", true);
        //    }
        //    return jres;
        //}



        //public CoverageListResponse GetCarrierCoveragesService(Guid CarrierId)
        //{
        //    CoverageListResponse jres = null;
        //    ActionLogger.Logger.WriteLog("GetCarrierCoveragesService request , CarrierID - " + CarrierId, true);
        //    try
        //    {
        //        List<Coverage> lst = Coverage.GetCarrierCoverages(CarrierId);

        //        if (lst != null && lst.Count > 0)
        //        {
        //            jres = new CoverageListResponse(string.Format("Data found successfully"), Convert.ToInt16(ResponseCodes.Success), "");
        //            jres.CoverageList = lst;
        //            ActionLogger.Logger.WriteLog("GetCarrierCoveragesService success ", true);
        //        }
        //        else
        //        {
        //            jres = new CoverageListResponse(string.Format("No data found "), Convert.ToInt16(ResponseCodes.RecordNotFound), "");
        //            ActionLogger.Logger.WriteLog("GetCarrierCoveragesService 404 ", true);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        jres = new CoverageListResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
        //        ActionLogger.Logger.WriteLog("GetCarrierCoveragesService failure ", true);
        //    }
        //    return jres;
        //}



        //public CoverageListResponse GetPayorCarrierCoveragesService(Guid PayorId, Guid CarrierId)
        //{
        //    CoverageListResponse jres = null;
        //    ActionLogger.Logger.WriteLog("GetPayorCarrierCoveragesService request - PayorId - " + PayorId + ", CarrierId: " + CarrierId, true);
        //    try
        //    {
        //        List<Coverage> lst = Coverage.GetCarrierCoverages(PayorId, CarrierId);

        //        if (lst != null && lst.Count > 0)
        //        {
        //            jres = new CoverageListResponse(string.Format("Data found successfully"), Convert.ToInt16(ResponseCodes.Success), "");
        //            jres.CoverageList = lst;
        //            ActionLogger.Logger.WriteLog("GetPayorCarrierCoveragesService success ", true);
        //        }
        //        else
        //        {
        //            jres = new CoverageListResponse(string.Format("No data found "), Convert.ToInt16(ResponseCodes.RecordNotFound), "");
        //            ActionLogger.Logger.WriteLog("GetPayorCarrierCoveragesService 404 ", true);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        jres = new CoverageListResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
        //        ActionLogger.Logger.WriteLog("GetPayorCarrierCoveragesService failure ", true);
        //    }
        //    return jres;
        //}
        #region


        /// <summary>
        /// ModifiedBy:Ankit kahndelwal
        /// ModifiedOn:21-08-2019
        /// Purpose: Getting list of product nickname
        /// </summary>
        /// <param name="PayorId"></param>
        /// <param name="CarrierId"></param>
        /// <param name="CoverageId"></param>
        /// <returns></returns>
        public CoverageNickNameResponse GetAllNickNamesService(Guid payorId, Guid carrierId, Guid coverageId, ListParams listParams)
        {
            CoverageNickNameResponse jres = null;
            ActionLogger.Logger.WriteLog("GetAllNickNamesService request: " + payorId + ", CarrierId " + carrierId + ", coverageid: " + coverageId, true);
            try
            {
                List<CoverageNickName> lst = Coverage.GetCoverageNickNamesList(payorId, carrierId, coverageId, listParams, out int recordCount);
                if (lst != null && lst.Count > 0)
                {
                    jres = new CoverageNickNameResponse(string.Format("Coverage list retreived successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                    jres.CoverageNickNameList = lst;
                    jres.recordCount = recordCount;
                    ActionLogger.Logger.WriteLog("GetAllNickNamesService success ", true);
                }
                else
                {
                    jres = new CoverageNickNameResponse("No Coverages found!", Convert.ToInt16(ResponseCodes.RecordNotFound), "");
                    jres.recordCount = 0;
                    ActionLogger.Logger.WriteLog("GetAllNickNamesService 404 ", true);
                }
            }
            catch (Exception ex)
            {
                jres = new CoverageNickNameResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                ActionLogger.Logger.WriteLog("GetAllNickNamesService failure ", true);
            }
            return jres;
        }


        /// <summary>
        /// Created By:Ankit kahndelwal
        /// CreatedOn:28-098-2019
        /// Purpose:Add update the details of CoverageNickName
        /// </summary>
        /// <param name="coverageDetails"></param>
        /// <returns></returns>
        public ReturnStatusResponse AddUpdateCoverageTypeService(CoverageNickName coverageDetails)
        {
            ReturnStatusResponse jres = null;
            ActionLogger.Logger.WriteLog("AddUpdateCoverageService request: Coverage details -" + coverageDetails.ToStringDump(), true);
            try
            {
                int status = Coverage.AddUpdateCoverageType(coverageDetails);

                if (status == 1)
                {
                    jres = new ReturnStatusResponse(string.Format("Coverage added successfully in the system "), Convert.ToInt16(ResponseCodes.Success), "");
                    ActionLogger.Logger.WriteLog("AddUpdateCoverageService request: Coverage added successfully in the system", true);
                }
                else if (status == 0)
                {

                    jres = new ReturnStatusResponse(string.Format("Coverage already exist in the system"), Convert.ToInt16(ResponseCodes.RecordAlreadyExist), "");
                    ActionLogger.Logger.WriteLog("AddUpdateCoverageService request: Coverage already exist in the system", true);
                }
                else if (status == 2)
                {
                    jres = new ReturnStatusResponse(string.Format("Coverage update successfully in the system "), Convert.ToInt16(ResponseCodes.Success), "");
                    ActionLogger.Logger.WriteLog("AddUpdateCoverageService request: Coverage update successfully in the system", true);
                }
            }
            catch (Exception ex)
            {
                jres = new ReturnStatusResponse(string.Format("Exception occur while processd "), Convert.ToInt16(ResponseCodes.Failure), "");
                ActionLogger.Logger.WriteLog("AddUpdateCoverageService request:Exceptin occurs while processing:CoverageID" + coverageDetails.CoverageID, true);
            }
            return jres;
        }

        /// <summary>
        /// Modified by:Ankit khandelwal
        /// Modified on:28-08-2019
        /// Purpose:Delete Coverage type
        /// </summary>
        /// <param name="CoverageNickId"></param>
        /// <param name="coverageNickName"></param>
        /// <param name="flag"></param>
        /// <returns></returns>
        public ReturnStatusResponse DeleteNickNameService(int CoverageNickId, string coverageNickName, bool flag)
        {
            ReturnStatusResponse jres = null;
            //   ActionLogger.Logger.WriteLog("DeleteNickNameService request: guidPayorID -" + guidPayorID + ", guidPreviousCoverageId : " + guidPreviousCoverageId + ", carrier: " + guidCarrierID, true);
            try
            {
                Coverage.DeleteNickName(CoverageNickId, coverageNickName, flag, out bool isPolicyExist);
                jres = new ReturnStatusResponse(string.Format("DeleteNickNameService:Coverage nick name could not be deleted"), Convert.ToInt16(ResponseCodes.Success), "");
                jres.RecordStatus = isPolicyExist;
                ActionLogger.Logger.WriteLog("DeleteNickNameService success ", true);
            }
            catch (Exception ex)
            {
                jres = new ReturnStatusResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                ActionLogger.Logger.WriteLog("DeleteNickNameService failure ", true);
            }
            return jres;
        }

        public ReturnStatusResponse AddUpdateCoverageService(Coverage coverageDetails)
        {
            ReturnStatusResponse jres = null;
            ActionLogger.Logger.WriteLog("AddUpdateCoverageService request: Coverage details -" + coverageDetails.ToStringDump(), true);
            try
            {
                int status = Coverage.AddUpdateCoverageDetails(coverageDetails);

                if (status == 1)
                {
                    jres = new ReturnStatusResponse(string.Format("Coverage added successfully in the system "), Convert.ToInt16(ResponseCodes.Success), "");
                    ActionLogger.Logger.WriteLog("AddUpdateCoverageService request: Coverage added successfully in the system", true);
                }
                else if (status == 0)
                {

                    jres = new ReturnStatusResponse(string.Format("Coverage already exist in the system"), Convert.ToInt16(ResponseCodes.RecordAlreadyExist), "");
                    ActionLogger.Logger.WriteLog("AddUpdateCoverageService request: Coverage already exist in the system", true);
                }
                else if (status == 2)
                {
                    jres = new ReturnStatusResponse(string.Format("Coverage update successfully in the system "), Convert.ToInt16(ResponseCodes.Success), "");
                    ActionLogger.Logger.WriteLog("AddUpdateCoverageService request: Coverage update successfully in the system", true);
                }
            }
            catch (Exception ex)
            {
                jres = new ReturnStatusResponse(string.Format("Exception occur while processd "), Convert.ToInt16(ResponseCodes.Failure), "");
                ActionLogger.Logger.WriteLog("AddUpdateCoverageService request:Exceptin occurs while processing:CoverageID" + coverageDetails.CoverageID, true);
            }
            return jres;
        }


        public CoverageListResponse GetCoveragesListingService(Guid licenseeId, ListParams listParams)
        {
            CoverageListResponse jres = null;
            ActionLogger.Logger.WriteLog("GetCoveragesListingService: processing Start with licenseeId:" + licenseeId, true);
            try
            {
                List<Coverage> lst = Coverage.GetCoverages(licenseeId, listParams, out int recordCount);
                jres = new CoverageListResponse(string.Format("Data found successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                jres.CoverageList = lst;
                jres.recordCount = recordCount;
                ActionLogger.Logger.WriteLog("GetCoveragesListingService:Products list fetched Successfully ", true);
            }
            catch (Exception ex)
            {
                jres = new CoverageListResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                ActionLogger.Logger.WriteLog("GetCoveragesListingService:Error occur while fetching list of Products:" + ex.Message, true);
            }
            return jres;
        }
        public DisplayCoverageListResponse GetDisplayedCarrierCoveragesService(Guid LicenseeId)
        {
            DisplayCoverageListResponse jres = null;
            ActionLogger.Logger.WriteLog("GetDisplayedCarrierCoveragesService request: " + LicenseeId, true);
            try
            {
                List<DisplayedCoverage> lst = Coverage.GetDisplayedCarrierCoverages(LicenseeId);
                if (lst != null && lst.Count > 0)
                {
                    jres = new DisplayCoverageListResponse(string.Format("Coverage list retreived successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                    jres.DisplayCoverageList = lst;
                    ActionLogger.Logger.WriteLog("GetDisplayedCarrierCoveragesService success ", true);
                }
                else
                {
                    jres = new DisplayCoverageListResponse("No Coverages found!", Convert.ToInt16(ResponseCodes.RecordNotFound), "");
                    ActionLogger.Logger.WriteLog("GetDisplayedCarrierCoveragesService 404 ", true);
                }
            }
            catch (Exception ex)
            {
                jres = new DisplayCoverageListResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                ActionLogger.Logger.WriteLog("GetDisplayedCarrierCoveragesService failure ", true);
            }
            return jres;
        }

        public ReturnStatusResponse DeleteCoverageService(Guid coverageId, bool flag)
        {
            ReturnStatusResponse jres = null;
            //   ActionLogger.Logger.WriteLog("DeleteNickNameService request: guidPayorID -" + guidPayorID + ", guidPreviousCoverageId : " + guidPreviousCoverageId + ", carrier: " + guidCarrierID, true);
            try
            {
                Coverage.DeleteCoverage(coverageId, flag, out bool isPolicyExist);
                jres = new ReturnStatusResponse(string.Format("DeleteNickNameService:Coverage nick name could not be deleted"), Convert.ToInt16(ResponseCodes.Success), "");
                jres.RecordStatus = isPolicyExist;
                ActionLogger.Logger.WriteLog("DeleteNickNameService success ", true);
            }
            catch (Exception ex)
            {
                jres = new ReturnStatusResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                ActionLogger.Logger.WriteLog("DeleteNickNameService failure ", true);
            }
            return jres;
        }


        /// <summary>
        /// Purpose: this method is used for the policy creation
        /// CreatedOn:05-05-2019
        /// </summary>
        /// <param name="PayorId"></param>
        /// <param name="CarrierId"></param>
        /// <param name="CoverageId"></param>
        /// <returns></returns>
        public CoverageNickNameResponse GetProductTypeService(Guid PayorId, Guid CarrierId, Guid CoverageId)
        {
            CoverageNickNameResponse jres = null;
            ActionLogger.Logger.WriteLog("GetAllNickNamesService request: " + PayorId + ", CarrierId " + CarrierId + ", coverageid: " + CoverageId, true);
            try
            {
                List<CoverageNickName> lst = Coverage.GetAllNickNames(PayorId, CarrierId, CoverageId);
                if (lst != null && lst.Count > 0)
                {
                    jres = new CoverageNickNameResponse(string.Format("Coverage list retreived successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                    jres.CoverageNickNameList = lst;
                    ActionLogger.Logger.WriteLog("GetAllNickNamesService success ", true);
                }
                else
                {
                    jres = new CoverageNickNameResponse("No Coverages found!", Convert.ToInt16(ResponseCodes.RecordNotFound), "");
                    ActionLogger.Logger.WriteLog("GetAllNickNamesService 404 ", true);
                }
            }
            catch (Exception ex)
            {
                jres = new CoverageNickNameResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                ActionLogger.Logger.WriteLog("GetAllNickNamesService failure ", true);
            }
            return jres;
        }

        #endregion






        //    public PolicyBoolResponse IsValidCoverageService(string carrierNickName, string coverageNickName, Guid payorId)
        //    {

        //        PolicyBoolResponse jres = null;
        //        ActionLogger.Logger.WriteLog("IsValidCoverageService request: carrierNickName -" + carrierNickName + ", payorID: " + payorId, true);
        //        try
        //        {
        //            bool res = Coverage.IsValidCoverage(carrierNickName, coverageNickName, payorId);
        //            jres = new PolicyBoolResponse(string.Format("Coverage validity found successfully"), Convert.ToInt16(ResponseCodes.Success), "");
        //            jres.BoolFlag = res;
        //            ActionLogger.Logger.WriteLog("IsValidCoverageService success ", true);
        //        }
        //        catch (Exception ex)
        //        {
        //            jres = new PolicyBoolResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
        //            ActionLogger.Logger.WriteLog("IsValidCoverageService failure ", true);
        //        }
        //        return jres;
        //    }

        //    public PolicyStringResponse GetCoverageNickNameService(Guid PayorId, Guid CarrierId, Guid CoverageId)
        //    {
        //        PolicyStringResponse jres = null;
        //        ActionLogger.Logger.WriteLog("IsValidCoverageService request: PayorId -" + PayorId + ", CarrierId: " + CarrierId + ", CoverageID: " + CoverageId, true);
        //        try
        //        {
        //            string s = Coverage.GetCoverageNickName(PayorId, CarrierId, CoverageId);
        //            if (!string.IsNullOrEmpty(s))
        //            {
        //                jres = new PolicyStringResponse(string.Format("Coverage validity found successfully"), Convert.ToInt16(ResponseCodes.Success), "");
        //                jres.StringValue = s;
        //                ActionLogger.Logger.WriteLog("IsValidCoverageService success ", true);
        //            }
        //            else
        //            {
        //                jres = new PolicyStringResponse(string.Format("Coverage validity not found"), Convert.ToInt16(ResponseCodes.RecordNotFound), "");
        //                ActionLogger.Logger.WriteLog("IsValidCoverageService 404 ", true);
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            jres = new PolicyStringResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
        //            ActionLogger.Logger.WriteLog("IsValidCoverageService failure ", true);
        //        }
        //        return jres;
        //    }

        //    public DisplayCoverageResponse GetCoverageForPolicyService(Guid DisplayedCoverageID)
        //    {
        //        DisplayCoverageResponse jres = null;
        //        ActionLogger.Logger.WriteLog("GetCoverageForPolicyService request: DisplayedCoverageID -" + DisplayedCoverageID, true);
        //        try
        //        {
        //            DisplayedCoverage obj = Coverage.GetCoverageForPolicy(DisplayedCoverageID);
        //            if (obj != null)
        //            {
        //                jres = new DisplayCoverageResponse(string.Format("Coverage found successfully"), Convert.ToInt16(ResponseCodes.Success), "");
        //                jres.CoverageObject = obj;
        //                ActionLogger.Logger.WriteLog("GetCoverageForPolicyService success ", true);
        //            }
        //            else
        //            {
        //                jres = new DisplayCoverageResponse(string.Format("Coverage not found"), Convert.ToInt16(ResponseCodes.RecordNotFound), "");
        //                ActionLogger.Logger.WriteLog("GetCoverageForPolicyService 404 ", true);
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            jres = new DisplayCoverageResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
        //            ActionLogger.Logger.WriteLog("GetCoverageForPolicyService failure ", true);
        //        }
        //        return jres;
        //    }

        //    public ReturnStatusResponse DeleteProductTypeService(Guid guidPayorID, Guid guidCarrierID, Guid guidCoverageId, string strNickNames)
        //    {
        //        ReturnStatusResponse jres = null;
        //        ActionLogger.Logger.WriteLog("DeleteProductTypeService request: guidPayorID -" + guidPayorID + ", guidPreviousCoverageId : " + guidCoverageId + ", carrier: " + guidCarrierID, true);
        //        try
        //        {
        //            ReturnStatus obj = Coverage.DeleteProductType(guidPayorID, guidCarrierID, guidCoverageId, strNickNames);

        //            if (obj != null)
        //            {
        //                if (obj.IsError)
        //                {
        //                    jres = new ReturnStatusResponse(string.Format("Product Type could not be deleted"), Convert.ToInt16(ResponseCodes.Failure), "");
        //                }
        //                else
        //                {
        //                    jres = new ReturnStatusResponse(string.Format("Product Type deleted successfully"), Convert.ToInt16(ResponseCodes.Success), "");
        //                }
        //                jres.Status = obj;
        //                ActionLogger.Logger.WriteLog("DeleteProductTypeService success ", true);
        //            }
        //            else
        //            {
        //                jres = new ReturnStatusResponse(string.Format("Product Type could not be deleted"), Convert.ToInt16(ResponseCodes.Failure), "");
        //                ActionLogger.Logger.WriteLog("DeleteProductTypeService failure ", true);
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            jres = new ReturnStatusResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
        //            ActionLogger.Logger.WriteLog("DeleteProductTypeService failure ", true);
        //        }
        //        return jres;
        //    }
    }
}