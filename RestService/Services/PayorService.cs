using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ServiceModel;
using MyAgencyVault.BusinessLibrary;
using System.ServiceModel.Web;
using MyAgencyVault.WcfService.Library.Response;
using System.Data.SqlClient;
using System.Data;

namespace MyAgencyVault.WcfService
{
    [ServiceContract]
    interface IPayorService
    {
        #region Payor
        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        ReturnStatusResponse AddUpdateDeletePayorService(Payor payorObject, Operation operationType, bool forceDelete=false);

        //[OperationContract]
        //[WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        //ReturnStatusResponse DeletePayorService(Guid PayorId);

        //[OperationContract]
        //int GetPayorCount(Guid? LicenseeId, PayorFillInfo PayerfillInfo);

        //[OperationContract]
        //List<Payor> GetPayorsInChunk(Guid? LicenseeId, PayorFillInfo PayerfillInfo, int skip, int take);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        PayorResponse GetPayorByIDService(Guid PayorId);

        //[OperationContract]
        //Payor GetPayorIDbyNickName(string nickName);

        //[OperationContract]
        //[WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        //ReturnStatusResponse ValdateLocalPayorService(Payor pObj, string PayorName, string NickName);
        #endregion

        #region Display Payor
        //[OperationContract]
        //[WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        //PolicyPMCResponse GetDisplayPayorCountService(Guid? LicenseeId, PayorFillInfo PayerfillInfo);

        //[OperationContract]
        //[WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        //DisplayPayorListResponse GetDisplayPayorsInChunkService(Guid? LicenseeId, PayorFillInfo PayerfillInfo, int skip, int take);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        PayorListResponse  GetPayorsService(Guid? licenseeId, PayorFillInfo payerfillInfo);
       
        //[OperationContract]
        //[WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        //PayorSourceResponse GetPayorSourceService(PayorSource source);



        #endregion

        #region Payor defaults
        //[OperationContract]
        //[WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        //PayorDefaultListresponse GetPayorDefaultsService();

        //[OperationContract]
        //[WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        //JSONResponse AddUpdatePayorDefaultsService(PayorDefaults PyrToolDafults);

        //[OperationContract]
        //[WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        //PayorDefaultResponse GetPayorDefaultByIDService(Guid globalPayerId);
        #endregion

        #region Config Payor 
        /*[OperationContract]
         [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
         ConfigPayorListResponse GetConfigDisplayPayorsService(Guid? LicenseeId, PayorFillInfo PayerfillInfo);

          Not in use in app
         [OperationContract]
         [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
         PolicyPMCResponse GetConfigDisplayPayorCountService(Guid? LicenseeId, PayorFillInfo PayerfillInfo);

         [OperationContract]
         [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
         ConfigPayorListResponse GetConfigDisplayPayorsInChunkService(Guid? LicenseeId, PayorFillInfo PayerfillInfo, int skip, int take);

         #endregion

         #region SettingPayor*/
        /*OperationContract]
       [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
       SettingPayorListResponse GetSettingDisplayPayorsService(Guid? LicenseeId, PayorFillInfo PayerfillInfo);

       [OperationContract]
       [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
       PolicyPMCResponse GetSettingDisplayPayorCountService(Guid? LicenseeId, PayorFillInfo PayerfillInfo);

       [OperationContract]
       [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
       SettingPayorListResponse GetSettingDisplayPayorsInChunkService(Guid? LicenseeId, PayorFillInfo PayerfillInfo, int skip, int take);*/
        #endregion

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        SettingPayorListResponse GetPayorsList(ListParams listParams);
    }

    public partial class MavService : IPayorService
    {
        #region Payor

        //public PayorSourceResponse GetPayorSourceService(PayorSource source)
        //{
        //    PayorSourceResponse jres = null;
        //    ActionLogger.Logger.WriteLog("GetPayorSourceService request: source -" + source.ToStringDump(), true);
        //    try
        //    {
        //        PayorSource obj = source.GetPayorSource();
        //        if (obj != null)
        //        {
        //            jres = new PayorSourceResponse(string.Format("Payor found successfully "), Convert.ToInt16(ResponseCodes.Success), "");
        //            jres.PayorSrcObject = obj;
        //            ActionLogger.Logger.WriteLog("GetPayorSourceService failure ", true);
        //        }
        //        else
        //        {
        //            jres = new PayorSourceResponse(string.Format("Payor not found "), Convert.ToInt16(ResponseCodes.RecordNotFound), "");
        //            ActionLogger.Logger.WriteLog("GetPayorSourceService 404 ", true);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        jres = new PayorSourceResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
        //        ActionLogger.Logger.WriteLog("GetPayorSourceService failure ", true);
        //    }
        //    return jres;
        //}

        public ReturnStatusResponse AddUpdateDeletePayorService(Payor payorObject, Operation operationType, bool forceDelete = false)
        {
            ReturnStatusResponse jres = null;
            ActionLogger.Logger.WriteLog("AddUpdateDeletePayorService request: pObj -" + payorObject.ToStringDump() + ", operationType : " + operationType + ", forceDelete: " + forceDelete, true);
            try
            {
                 ReturnStatus obj = payorObject.AddUpdateDelete(operationType, forceDelete);
          
                if (obj != null)
                {
                    if (obj.IsError)
                    {
                        jres = new ReturnStatusResponse(string.Format("Payor could not be processed "), Convert.ToInt16(ResponseCodes.Failure), "");
                    }
                    else
                    {
                        jres = new ReturnStatusResponse(string.Format("Payor processed successfully "), Convert.ToInt16(ResponseCodes.Success), "");
                    }
                    jres.Status = obj;
                    ActionLogger.Logger.WriteLog("AddUpdateDeletePayorService success ", true);
                }
                else
                {
                    jres = new ReturnStatusResponse(string.Format("Payor could not be processed"), Convert.ToInt16(ResponseCodes.Failure), "");
                    ActionLogger.Logger.WriteLog("AddUpdateDeletePayorService failure ", true);
                }
            }
            catch (Exception ex)
            {
                jres = new ReturnStatusResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                ActionLogger.Logger.WriteLog("AddUpdateDeletePayorService failure ", true);
            }
            return jres;
        }

        //public ReturnStatusResponse ValdateLocalPayorService(Payor pObj, string strPayorName, string strNickName)
        //{
        //    ReturnStatusResponse jres = null;
        //    ActionLogger.Logger.WriteLog("ValdateLocalPayorService request: pObj -" + pObj.ToStringDump() + ", payorName : " + strPayorName, true);
        //    try
        //    {
        //        ReturnStatus obj = pObj.ValdateLocalPayor(strPayorName, strNickName);
          
        //        if (obj != null)
        //        {
        //            if (obj.IsError)
        //            {
        //                jres = new ReturnStatusResponse(string.Format("Payor could not be processed "), Convert.ToInt16(ResponseCodes.Failure), "");
        //                 ActionLogger.Logger.WriteLog("ValdateLocalPayorService success ", true);
        //            }
        //            else
        //            {
        //                jres = new ReturnStatusResponse(string.Format("Payor processed successfully "), Convert.ToInt16(ResponseCodes.Success), "");
        //            }
        //            jres.Status = obj;
                      
        //        }
        //        else
        //        {
        //            jres = new ReturnStatusResponse(string.Format("Coverage could not be processed"), Convert.ToInt16(ResponseCodes.Failure), "");
        //            ActionLogger.Logger.WriteLog("ValdateLocalPayorService failure ", true);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        jres = new ReturnStatusResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
        //        ActionLogger.Logger.WriteLog("AddUpdateDeletePayorService failure ", true);
        //    }
        //    return jres;
             
        //}

        //public ReturnStatusResponse DeletePayorService(Guid PayorId)
        //{
        //     ReturnStatusResponse jres = null;
        //     ActionLogger.Logger.WriteLog("DeletePayorService request: PayorId -" + PayorId, true);
        //    try
        //    {
        //        ReturnStatus obj = Payor.DeletePayor(PayorId);

        //        if (obj != null)
        //        {
        //            if (obj.IsError)
        //            {
        //                jres = new ReturnStatusResponse(string.Format("Payor could not be deleted "), Convert.ToInt16(ResponseCodes.Failure), "");
        //                jres.Status = obj;
        //                ActionLogger.Logger.WriteLog("DeletePayorService failure ", true);
        //            }
        //            else
        //            {
        //                jres = new ReturnStatusResponse(string.Format("Payor deleted successfully "), Convert.ToInt16(ResponseCodes.Success), "");
        //                ActionLogger.Logger.WriteLog("DeletePayorService success ", true);
        //            }
                   
        //        }
        //        else
        //        {
        //            jres = new ReturnStatusResponse(string.Format("Payor could not be deleted"), Convert.ToInt16(ResponseCodes.Failure), "");
        //            ActionLogger.Logger.WriteLog("DeletePayorService failure ", true);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        jres = new ReturnStatusResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
        //        ActionLogger.Logger.WriteLog("DeletePayorService failure ", true);
        //    }
        //    return jres;
        //}

        //public int GetPayorCount(Guid? LicenseeId, PayorFillInfo PayerfillInfo)
        //{
        //    return Payor.GetPayorCount(LicenseeId, PayerfillInfo);
        //}

        //public List<Payor> GetPayorsInChunk(Guid? LicenseeId, PayorFillInfo PayerfillInfo, int skip, int take)
        //{
        //    return Payor.GetPayors(LicenseeId, PayerfillInfo, skip, take);
        //}

        public PayorResponse GetPayorByIDService(Guid PayorId)
        {
            PayorResponse jres = null;
             ActionLogger.Logger.WriteLog("GetPayorByIDService request: PayorId -" + PayorId, true);
             try
             {
                 Payor obj = Payor.GetPayorByID(PayorId);
                 if (obj != null)
                 {
                     jres = new PayorResponse(string.Format("Payor found successfully "), Convert.ToInt16(ResponseCodes.Success), "");
                     ActionLogger.Logger.WriteLog("GetPayorByIDService success ", true);
                     jres.PayorObject = obj;
                 }
                 else
                 {
                     jres = new PayorResponse(string.Format("Payor could not be found "), Convert.ToInt16(ResponseCodes.RecordNotFound), "");
                     ActionLogger.Logger.WriteLog("GetPayorByIDService 404 ", true);
                 }
             }
             catch (Exception ex)
             {
                 jres = new PayorResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                 ActionLogger.Logger.WriteLog("GetPayorByIDService failure ", true);
             }
            return jres;
        }

        //public Payor GetPayorIDbyNickName(string nickName)
        //{
        //    return Payor.GetPayorIDbyNickName(nickName);
        //}

        #endregion

        #region Payor defaults
      /*public JSONResponse AddUpdatePayorDefaultsService(PayorDefaults PyrToolDafults)
        {
            ActionLogger.Logger.WriteLog("AddUpdatePayorDefaultsService request: PyrToolDafults" + PyrToolDafults.ToStringDump(), true);
            JSONResponse jres = null;
            try
            {
                PyrToolDafults.AddUpdate();
                jres = new JSONResponse("Payor defaults saved successfully", (int)ResponseCodes.Success, "");
                ActionLogger.Logger.WriteLog("AddUpdatePayorDefaultsService success", true);
            }
            catch (Exception ex)
            {
                jres = new JSONResponse("", (int)ResponseCodes.Failure, ex.Message);
                ActionLogger.Logger.WriteLog("AddUpdatePayorDefaultsService failure", true);
            }
            return jres;
        }

        public PayorDefaultListresponse GetPayorDefaultsService()
        {
            ActionLogger.Logger.WriteLog("GetPayorDefaultsService request: ", true);
            PayorDefaultListresponse jres = null;
            try
            {
                List<PayorDefaults> lst = PayorDefaults.GetPayorDefault();
                if (lst != null && lst.Count > 0)
                {
                    jres = new PayorDefaultListresponse("Payors list found successfully", (int)ResponseCodes.Success, "");
                    jres.PayorList = lst;
                }
                else
                {
                    jres = new PayorDefaultListresponse("No payor defaults found", (int)ResponseCodes.RecordNotFound, "");
                }
                ActionLogger.Logger.WriteLog("GetPayorDefaultsService success: ", true);
            }
            catch (Exception ex)
            {
                jres = new PayorDefaultListresponse("", (int)ResponseCodes.Failure, "Error getting users list" + ex.Message);
                ActionLogger.Logger.WriteLog("GetPayorDefaultsService failure: ", true);
            }
            return jres;
        }

        public PayorDefaultResponse GetPayorDefaultByIDService(Guid globalPayerId)
        {
            ActionLogger.Logger.WriteLog("GetPayorDefaultByIDService request: ", true);
            PayorDefaultResponse jres = null;
            try
            {
                PayorDefaults obj = PayorDefaults.GetPayorDefault(globalPayerId);
                if (obj != null)
                {
                    jres = new PayorDefaultResponse("Payor object found successfully", (int)ResponseCodes.Success, "");
                    jres.PayorObject = obj;
                }
                else
                {
                    jres = new PayorDefaultResponse("No payor found with given ID", (int)ResponseCodes.RecordNotFound, "");
                }
                ActionLogger.Logger.WriteLog("GetPayorDefaultByIDService success: ", true);
            }
            catch (Exception ex)
            {
                jres = new PayorDefaultResponse("", (int)ResponseCodes.Failure, ex.Message);
                ActionLogger.Logger.WriteLog("GetPayorDefaultByIDService failure: ", true);
            }
            return jres;
        }*/
        #endregion

        #region Display Payor
       /*ublic PolicyPMCResponse GetDisplayPayorCountService(Guid? LicenseeId, PayorFillInfo PayerfillInfo)
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
        }*/
        /// <summary>
        /// Create by :Ankit Khandelwal
        /// Created On: 12-03-2019
        /// Purpose:Getting List of Payors 
        /// </summary>
        /// <param name="licenseeId"></param>
        /// <param name="payerfillInfo"></param>
        /// <returns></returns>
        public PayorListResponse GetPayorsService(Guid? licenseeId, PayorFillInfo payerfillInfo)
        {

            PayorListResponse jres = null;
            ActionLogger.Logger.WriteLog("GetPayorsService request: licenseeid - " + licenseeId + ", payerfillinfo - "+ payerfillInfo.ToStringDump(), true);
            try
            {
                List<Payor> lst = Payor.GetPayorsList(licenseeId, payerfillInfo);
                if (lst != null && lst.Count > 0)
                {
                    jres = new PayorListResponse(string.Format("Payor list retreived successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                    jres.PayorList = lst;
                    jres.PayorsCount = 0;
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
        #endregion

        #region Config payor
        /*   public ConfigPayorListResponse GetConfigDisplayPayorsService(Guid? LicenseeId, PayorFillInfo PayerfillInfo)
         {
             ConfigPayorListResponse jres = null;
             ActionLogger.Logger.WriteLog("GetConfigDisplayPayorsService request: ", true);
             try
             {
                 List<ConfigDisplayedPayor> lst = ConfigDisplayedPayor.GetPayors(LicenseeId, PayerfillInfo);

                 if (lst != null && lst.Count > 0)
                 {
                     jres = new ConfigPayorListResponse(string.Format("Payor list retreived successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                     jres.PayorList = lst;
                     ActionLogger.Logger.WriteLog("GetConfigDisplayPayorsService success ", true);
                 }
                 else
                 {
                     jres = new ConfigPayorListResponse("No payors found!", Convert.ToInt16(ResponseCodes.RecordNotFound), "");
                     ActionLogger.Logger.WriteLog("GetConfigDisplayPayorsService 404 ", true);
                 }
             }
             catch (Exception ex)
             {
                 jres = new ConfigPayorListResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                 ActionLogger.Logger.WriteLog("GetConfigDisplayPayorsService failure ", true);
             }
             return jres;
         }

       public PolicyPMCResponse GetConfigDisplayPayorCountService(Guid? LicenseeId, PayorFillInfo PayerfillInfo)
         {
             PolicyPMCResponse jres = null;
             ActionLogger.Logger.WriteLog("GetConfigDisplayPayorCountService request: " + LicenseeId, true);
             try
             {
                 int val = ConfigDisplayedPayor.GetPayorCount(LicenseeId, PayerfillInfo);
                 ActionLogger.Logger.WriteLog("GetConfigDisplayPayorCountService value: " + val, true);
                 if (val != null)
                 {
                     jres = new PolicyPMCResponse(string.Format("Data found successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                     jres.NumberValue = (decimal)val;
                 }
                 else
                 {
                     jres = new PolicyPMCResponse("Payor count not found!", Convert.ToInt16(ResponseCodes.RecordNotFound), "");
                     ActionLogger.Logger.WriteLog("GetConfigDisplayPayorCountService 404 ", true);
                 }
             }
             catch (Exception ex)
             {
                 jres = new PolicyPMCResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                 ActionLogger.Logger.WriteLog("GetConfigDisplayPayorCountService failure ", true);
             }
             return jres;
         }

         public ConfigPayorListResponse GetConfigDisplayPayorsInChunkService(Guid? LicenseeId, PayorFillInfo PayerfillInfo, int skip, int take)
         {
             ConfigPayorListResponse jres = null;
             ActionLogger.Logger.WriteLog("GetConfigDisplayPayorsInChunkService request: ", true);
             try
             {
                 List<ConfigDisplayedPayor> lst = ConfigDisplayedPayor.GetPayors(LicenseeId, PayerfillInfo, skip, take);

                 if (lst != null && lst.Count > 0)
                 {
                     jres = new ConfigPayorListResponse(string.Format("Payor list retreived successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                     jres.PayorList = lst;
                     ActionLogger.Logger.WriteLog("GetConfigDisplayPayorsInChunkService success ", true);
                 }
                 else
                 {
                     jres = new ConfigPayorListResponse("No payors found!", Convert.ToInt16(ResponseCodes.RecordNotFound), "");
                     ActionLogger.Logger.WriteLog("GetConfigDisplayPayorsInChunkService 404 ", true);
                 }
             }
             catch (Exception ex)
             {
                 jres = new ConfigPayorListResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                 ActionLogger.Logger.WriteLog("GetConfigDisplayPayorsInChunkService failure ", true);
             }
             return jres;
         }*/
        #endregion

        #region Configuration payor

        public SettingPayorListResponse GetPayorsList(ListParams listParams)
        {
            ActionLogger.Logger.WriteLog("GetPayorsList request:  "+ listParams.ToStringDump(), true);
            SettingPayorListResponse res = null;
            int count = 0;
            try
            {
                List<ConfigPayor> lst = ConfigPayor.GetPayorsForConfiguration(listParams, out count);

                if (lst != null && lst.Count > 0)
                {
                    res = new SettingPayorListResponse(string.Format("Payor list retreived successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                    res.PayorList = lst;
                    res.PayorsCount = count;
                    ActionLogger.Logger.WriteLog("GetPayorsList success ", true);
                }
                else
                {
                    res = new SettingPayorListResponse("No payors found!", Convert.ToInt16(ResponseCodes.RecordNotFound), "");
                    ActionLogger.Logger.WriteLog("GetPayorsList 404 ", true);
                }
            }
            catch (Exception ex)
            {
                ActionLogger.Logger.WriteLog("GetPayorsList Exception: " + ex.Message, true);
                res = new SettingPayorListResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
            }
            return res;
        }

     /* public SettingPayorListResponse GetSettingDisplayPayorsService(Guid? LicenseeId, PayorFillInfo PayerfillInfo)
        {
            SettingPayorListResponse jres = null;
            ActionLogger.Logger.WriteLog("GetSettingDisplayPayorsService request: ", true);
            try
            {
                List<SettingDisplayedPayor> lst = SettingDisplayedPayor.GetPayors(LicenseeId, PayerfillInfo);

                if (lst != null && lst.Count > 0)
                {
                    jres = new SettingPayorListResponse(string.Format("Payor list retreived successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                   // jres.PayorList = lst;
                    ActionLogger.Logger.WriteLog("GetSettingDisplayPayorsService success ", true);
                }
                else
                {
                    jres = new SettingPayorListResponse("No payors found!", Convert.ToInt16(ResponseCodes.RecordNotFound), "");
                    ActionLogger.Logger.WriteLog("GetSettingDisplayPayorsService 404 ", true);
                }
            }
            catch (Exception ex)
            {
                jres = new SettingPayorListResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                ActionLogger.Logger.WriteLog("GetSettingDisplayPayorsService failure ", true);
            }
            return jres;
        }*/

        /* - Not in use 
         * public PolicyPMCResponse GetSettingDisplayPayorCountService(Guid? LicenseeId, PayorFillInfo PayerfillInfo)
         {
             PolicyPMCResponse jres = null;
             ActionLogger.Logger.WriteLog("GetSettingDisplayPayorCountService request: " + LicenseeId, true);
             try
             {
                 int val =  SettingDisplayedPayor.GetPayorCount(LicenseeId, PayerfillInfo);
                 ActionLogger.Logger.WriteLog("GetSettingDisplayPayorCountService value: " + val, true);
                 if (val != null)
                 {
                     jres = new PolicyPMCResponse(string.Format("Data found successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                     jres.NumberValue = (decimal)val;
                 }
                 else
                 {
                     jres = new PolicyPMCResponse("Payor count not found!", Convert.ToInt16(ResponseCodes.RecordNotFound), "");
                     ActionLogger.Logger.WriteLog("GetSettingDisplayPayorCountService 404 ", true);
                 }
             }
             catch (Exception ex)
             {
                 jres = new PolicyPMCResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                 ActionLogger.Logger.WriteLog("GetSettingDisplayPayorCountService failure ", true);
             }
             return jres;
         }

         public SettingPayorListResponse GetSettingDisplayPayorsInChunkService(Guid? LicenseeId, PayorFillInfo PayerfillInfo, int skip, int take)
         {
             SettingPayorListResponse jres = null;
             ActionLogger.Logger.WriteLog("GetSettingDisplayPayorsInChunkService request: ", true);
             try
             {
                 List<SettingDisplayedPayor> lst = SettingDisplayedPayor.GetPayors(LicenseeId, PayerfillInfo, skip, take);

                 if (lst != null && lst.Count > 0)
                 {
                     jres = new SettingPayorListResponse(string.Format("Payor list retreived successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                     jres.PayorList = lst;
                     ActionLogger.Logger.WriteLog("GetSettingDisplayPayorsInChunkService success ", true);
                 }
                 else
                 {
                     jres = new SettingPayorListResponse("No payors found!", Convert.ToInt16(ResponseCodes.RecordNotFound), "");
                     ActionLogger.Logger.WriteLog("GetSettingDisplayPayorsInChunkService 404 ", true);
                 }
             }
             catch (Exception ex)
             {
                 jres = new SettingPayorListResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                 ActionLogger.Logger.WriteLog("GetSettingDisplayPayorsInChunkService failure ", true);
             }
             return jres;
         }*/
        #endregion
    }
  
}