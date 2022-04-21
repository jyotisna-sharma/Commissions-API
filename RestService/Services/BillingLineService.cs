using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ServiceModel;
using MyAgencyVault.BusinessLibrary;
using MyAgencyVault.WcfService.Library.Response;
using System.ServiceModel.Web;
using MyAgencyVault.BusinessLibrary.Masters;


namespace MyAgencyVault.WcfService
{
    [ServiceContract]
    interface IBillingLineService
    {
        #region IEditable<BillingLineDetail> Members
        //[OperationContract]  Found not in use 
        //JSONResponse AddUpdateBillingLineDetailService(BillingLineDetail BillLinDtl);

        //[OperationContract] Found not in use 
        //[WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        //JSONResponse DeleteBillingLineDetailService(BillingLineDetail BillLinDtl);
        
        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        BillingLineListResponse GetBillingLineDetailService();
         
        //[OperationContract] Not in use 
        //[WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        //PolicyBoolResponse IsValidBillingLineDetailService(BillingLineDetail BillLinDtl);
        #endregion

        #region ServiceProduct
        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        ServiceProductListResponse GetAllServiceProductSrvc();
       
        //[OperationContract] Not in use 
        //[WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        //JSONResponse AddServiceProductSrvc(ServiceProduct _serviceproduct);
        #endregion

        #region ServiceChargeType
        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        ServiceChargeListResponse GetAllServiceChargeTypeSrvc();
        
        //[OperationContract]
        //[WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        //JSONResponse AddServiceChargeTypeSrvc(ServiceChargeType _servicechargetype);
        #endregion
        
        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        ServiceProductListResponse GetAllServicesSrvc();
       
        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        ServiceChargeListResponse GetAllServiceChargeSrvc();
       
        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        JSONResponse AddService(List<BillingLineDetail> collection, Guid LicenseeId);
       
        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        PolicyBoolResponse IsAgencyVersionLicenseService(Guid LicenseeId);
   }

    public partial class MavService : IBillingLineService
    {
        /*  public JSONResponse AddUpdateBillingLineDetailService(BillingLineDetail BillLinDtl)
          {
              JSONResponse jres = null;
              ActionLogger.Logger.WriteLog("AddUpdateBillingLineDetailService request: " + BillLinDtl.ToStringDump(), true);
              try
              {
                  BillLinDtl.AddUpdate();
                  jres = new JSONResponse(string.Format("Biling line saved successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                  ActionLogger.Logger.WriteLog("AddUpdateBillingLineDetailService success ", true);
              }
              catch (Exception ex)
              {
                  jres = new JSONResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                  ActionLogger.Logger.WriteLog("AddUpdateBillingLineDetailService failure ", true);
              }
              return jres;
          }

          public JSONResponse DeleteBillingLineDetailService(BillingLineDetail BillLinDtl)
          {
               JSONResponse jres = null;
              ActionLogger.Logger.WriteLog("DeleteBillingLineDetailService request: " + BillLinDtl.ToStringDump(), true);
              try
              {
                  BillLinDtl.Delete();
                  jres = new JSONResponse(string.Format("Billing line saved successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                  ActionLogger.Logger.WriteLog("DeleteBillingLineDetailService success ", true);
              }
              catch (Exception ex)
              {
                  jres = new JSONResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                  ActionLogger.Logger.WriteLog("DeleteBillingLineDetailService failure ", true);
              }
              return jres;
          }*/

        public BillingLineListResponse  GetBillingLineDetailService()
        {
             BillingLineListResponse jres = null;
             ActionLogger.Logger.WriteLog("GetBillingLineDetailService request: ", true);
            try
            {
                List<BillingLineDetail> lst = BillingLineDetail.GetAllServiceLine();
                if (lst != null && lst.Count > 0)
                {
                    jres = new BillingLineListResponse(string.Format("Billing line list found successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                    jres.BillingLineList = lst;
                    ActionLogger.Logger.WriteLog("GetBillingLineDetailService success ", true);
                }
                else
                {
                    jres = new BillingLineListResponse(string.Format("No records found"), Convert.ToInt16(ResponseCodes.RecordNotFound), "");
                    ActionLogger.Logger.WriteLog("GetBillingLineDetailService success ", true);
                }
            }
            catch (Exception ex)
            {
                jres = new BillingLineListResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                ActionLogger.Logger.WriteLog("GetBillingLineDetailService failure ", true);
            }
            return jres;
        }

        public ServiceProductListResponse GetAllServicesSrvc()
        {
            ServiceProductListResponse jres = null;
            ActionLogger.Logger.WriteLog("GetAllServicesSrvc request: " , true);
            try
            {
               List<ServiceProduct> lst  = BillingLineDetail.GetAllProducts();
               if (lst != null && lst.Count > 0)
               {
                   jres = new ServiceProductListResponse(string.Format("Service products list found successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                   jres.ServiceProductList = lst;
                   ActionLogger.Logger.WriteLog("GetAllServicesSrvc success ", true);
               }
               else
               {
                   jres = new ServiceProductListResponse(string.Format("Service products list not found"), Convert.ToInt16(ResponseCodes.Success), "");
                   ActionLogger.Logger.WriteLog("GetAllServicesSrvc 404 ", true);
               }
            }
            catch (Exception ex)
            {
                jres = new ServiceProductListResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                ActionLogger.Logger.WriteLog("GetAllServicesSrvc failure ", true);
            }
            return jres;
        }

        public ServiceChargeListResponse GetAllServiceChargeSrvc()
        {
            ServiceChargeListResponse jres = null;
            ActionLogger.Logger.WriteLog("GetAllServiceChargeSrvc request: ", true);
            try
            {
                List<ServiceChargeType> lst = BillingLineDetail.GetAllProductCharge();
                if (lst != null && lst.Count > 0)
                {
                    jres = new ServiceChargeListResponse(string.Format("Service charge list found successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                    jres.ServiceChargeTypeList = lst;
                    ActionLogger.Logger.WriteLog("GetAllServiceChargeSrvc success ", true);
                }
                else
                {
                    jres = new ServiceChargeListResponse(string.Format("Service charge list not found"), Convert.ToInt16(ResponseCodes.Success), "");
                    ActionLogger.Logger.WriteLog("GetAllServiceChargeSrvc 404 ", true);
                }
            }
            catch (Exception ex)
            {
                jres = new ServiceChargeListResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                ActionLogger.Logger.WriteLog("GetAllServiceChargeSrvc failure ", true);
            }
            return jres;
        }

   /*     public PolicyBoolResponse IsValidBillingLineDetailService(BillingLineDetail BillLinDtl)
        {
            PolicyBoolResponse jres = null;
            ActionLogger.Logger.WriteLog("IsValidBillingLineDetailService request: " + BillLinDtl.ToStringDump(), true);
            try
            {
                bool res = BillLinDtl.IsValid();
                jres = new PolicyBoolResponse(string.Format("Billing line validity found successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                jres.BoolFlag = res;
                ActionLogger.Logger.WriteLog("IsValidBillingLineDetailService success ", true);
            }
            catch (Exception ex)
            {
                jres = new PolicyBoolResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                ActionLogger.Logger.WriteLog("IsValidBillingLineDetailService failure ", true);
            }
            return jres;
        }*/

        public ServiceProductListResponse GetAllServiceProductSrvc()
        {
            ServiceProductListResponse jres = null;
            ActionLogger.Logger.WriteLog("GetAllServicesSrvc request: ", true);
            try
            {
                List<ServiceProduct> lst = ServiceProduct.GetAllServiceProduct();
                if (lst != null && lst.Count > 0)
                {
                    jres = new ServiceProductListResponse(string.Format("Service products list found successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                    jres.ServiceProductList = lst;
                    ActionLogger.Logger.WriteLog("GetAllServicesSrvc success ", true);
                }
                else
                {
                    jres = new ServiceProductListResponse(string.Format("Service products list not found"), Convert.ToInt16(ResponseCodes.Success), "");
                    ActionLogger.Logger.WriteLog("GetAllServicesSrvc 404 ", true);
                }
            }
            catch (Exception ex)
            {
                jres = new ServiceProductListResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                ActionLogger.Logger.WriteLog("GetAllServicesSrvc failure ", true);
            }
            return jres;
        }
     /*   public JSONResponse AddServiceProductSrvc(ServiceProduct _serviceproduct)
        {
            JSONResponse jres = null;
            ActionLogger.Logger.WriteLog("AddServiceProductSrvc request: " + _serviceproduct.ToStringDump(), true);
            try
            {
                _serviceproduct.AddUpdate();
          
                jres = new JSONResponse(string.Format("Service product saved successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                ActionLogger.Logger.WriteLog("AddServiceProductSrvc success ", true);
            }
            catch (Exception ex)
            {
                jres = new JSONResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                ActionLogger.Logger.WriteLog("AddServiceProductSrvc failure ", true);
            }
            return jres;
        }
        */
        public ServiceChargeListResponse GetAllServiceChargeTypeSrvc()
        {
            ServiceChargeListResponse jres = null;
            ActionLogger.Logger.WriteLog("GetAllServiceChargeTypeSrvc request: ", true);
            try
            {
                List<ServiceChargeType> lst = ServiceChargeType.GetAllServiceChargeType();
                if (lst != null && lst.Count > 0)
                {
                    jres = new ServiceChargeListResponse(string.Format("Service charge type list found successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                    jres.ServiceChargeTypeList = lst;
                    ActionLogger.Logger.WriteLog("GetAllServiceChargeTypeSrvc success ", true);
                }
                else
                {
                    jres = new ServiceChargeListResponse(string.Format("Service charge type list not found"), Convert.ToInt16(ResponseCodes.Success), "");
                    ActionLogger.Logger.WriteLog("GetAllServiceChargeTypeSrvc 404 ", true);
                }
            }
            catch (Exception ex)
            {
                jres = new ServiceChargeListResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                ActionLogger.Logger.WriteLog("GetAllServiceChargeTypeSrvc failure ", true);
            }
            return jres;
        }

        public JSONResponse AddServiceChargeTypeSrvc(ServiceChargeType _servicechargetype)
        {
             JSONResponse jres = null;
             ActionLogger.Logger.WriteLog("AddServiceChargeTypeSrvc request: " + _servicechargetype.ToStringDump(), true);
            try
            {
                _servicechargetype.AddUpdate();
          
                jres = new JSONResponse(string.Format("Service charge type saved successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                ActionLogger.Logger.WriteLog("AddServiceChargeTypeSrvc success ", true);
            }
            catch (Exception ex)
            {
                jres = new JSONResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                ActionLogger.Logger.WriteLog("AddServiceChargeTypeSrvc failure ", true);
            }
            return jres;
        }
      
        public JSONResponse AddService(List<BillingLineDetail> collection, Guid LicenseeId)
        {
           JSONResponse jres = null;
           ActionLogger.Logger.WriteLog("AddService request: " + collection.ToStringDump(), true);
            try
            {
                BillingLineDetail.Add(collection, LicenseeId);
                jres = new JSONResponse(string.Format("Billing details saved successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                ActionLogger.Logger.WriteLog("AddService success ", true);
            }
            catch (Exception ex)
            {
                jres = new JSONResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                ActionLogger.Logger.WriteLog("AddService failure ", true);
            }
            return jres;
        }

        public PolicyBoolResponse IsAgencyVersionLicenseService(Guid LicenseeId)
        {
            PolicyBoolResponse jres = null;
            ActionLogger.Logger.WriteLog("IsAgencyVersionLicenseService request: " + LicenseeId, true);
            try
            {
                bool res = BillingLineDetail.IsAgencyVersionLicense(LicenseeId);
                jres = new PolicyBoolResponse(string.Format("Licensee version status found successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                jres.BoolFlag = res;
                ActionLogger.Logger.WriteLog("IsAgencyVersionLicenseService success ", true);
            }
            catch (Exception ex)
            {
                jres = new PolicyBoolResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                ActionLogger.Logger.WriteLog("IsAgencyVersionLicenseService failure ", true);
            }
            return jres;
        }

    }


}