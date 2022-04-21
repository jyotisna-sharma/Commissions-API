using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ServiceModel.Web;
using MyAgencyVault.WcfService.Library.Response;
using System.ServiceModel;
using MyAgencyVault.EmailFax;
using MyAgencyVault.BusinessLibrary;

namespace MyAgencyVault.WcfService
{
    [ServiceContract]
    interface ICompTypeService
    {
        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        CompTypeListResponse GetAllComptypeService();

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        JSONResponse AddUpdateCompTypeService(CompType objCompType);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        PolicyBoolResponse DeleteCompTypeService(CompType objCompType);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        PolicyBoolResponse FindCompTypeNameService(string strName);


        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        CompTypeListResponse GetCompTypeListing(string incomingPaymentTypeId, string paymentTypeName, ListParams listParams);
    }

    public partial class MavService : ICompTypeService
    {

        #region
        public CompTypeListResponse GetCompTypeListing(string incomingPaymentTypeId, string paymentTypeName, ListParams listParams)
        {
            ActionLogger.Logger.WriteLog("GetCompTypeListing: processing starts for incomingPaymentTypeId " + incomingPaymentTypeId, true);
            CompTypeListResponse jres = null;
            try
            {
                CompType objCompType = new CompType();
                List<CompType> lst = CompType.GetCompTypeListing(incomingPaymentTypeId, paymentTypeName, listParams, out int recordCount);
                if (lst != null && lst.Count > 0)
                {
                    jres = new CompTypeListResponse("GetCompTypeListing: Comp type list found successfully", (int)ResponseCodes.Success, "");
                    jres.CompTypeList = lst;
                    jres.recordCount = recordCount;
                    ActionLogger.Logger.WriteLog("GetAllComptypeService success", true);
                }
                else
                {
                    jres = new CompTypeListResponse("No record found", (int)ResponseCodes.RecordNotFound, "");
                    ActionLogger.Logger.WriteLog("GetAllComptypeService 404", true);
                }
            }
            catch (Exception ex)
            {
                jres = new CompTypeListResponse("", (int)ResponseCodes.Failure, ex.Message);
                ActionLogger.Logger.WriteLog("GetAllComptypeService failure", true);
            }
            return jres;
        }
        #endregion
        public CompTypeListResponse GetAllComptypeService()
        {
            ActionLogger.Logger.WriteLog("GetAllComptypeService request: ", true);
            CompTypeListResponse jres = null;
            try
            {
                CompType objCompType = new CompType();
                List<CompType> lst = objCompType.GetAllComptype();

                if (lst != null && lst.Count > 0)
                {
                    jres = new CompTypeListResponse("Comp type details found successfully", (int)ResponseCodes.Success, "");
                    jres.CompTypeList = lst;
                    ActionLogger.Logger.WriteLog("GetAllComptypeService success", true);
                }
                else
                {
                    jres = new CompTypeListResponse("No record found", (int)ResponseCodes.RecordNotFound, "");
                    ActionLogger.Logger.WriteLog("GetAllComptypeService 404", true);
                }
            }
            catch (Exception ex)
            {
                jres = new CompTypeListResponse("", (int)ResponseCodes.Failure, ex.Message);
                ActionLogger.Logger.WriteLog("GetAllComptypeService failure", true);
            }
            return jres;
        }
        /// <summary>
        /// Used By:Ankit khandelwal
        /// Used On:28-08-2019
        /// Purpose:For add update comp type
        /// </summary>
        /// <param name="CompType"></param>
        /// <returns></returns>
        public JSONResponse AddUpdateCompTypeService(CompType CompType)
        {
            ActionLogger.Logger.WriteLog("AddUpdateCompTypeService: request process with  CompType" + CompType.ToStringDump(), true);
            JSONResponse jres = null;
            try
            {
                CompType objCompType = new CompType();
                objCompType.AddUpdateCompType(CompType, out bool isExist);
                if (isExist == true)
                {
                    jres = new JSONResponse("Comp type details already exist in the system", (int)ResponseCodes.RecordAlreadyExist, "");
                    ActionLogger.Logger.WriteLog("AddUpdateCompTypeService :Comp type details already exist in the system", true);
                }
                else
                {
                    jres = new JSONResponse("Comp type details saved successfully", (int)ResponseCodes.Success, "");
                    ActionLogger.Logger.WriteLog("AddUpdateCompTypeService: successfully saved in the system", true);
                }

            }
            catch (Exception ex)
            {
                jres = new JSONResponse("", (int)ResponseCodes.Failure, ex.Message);
                ActionLogger.Logger.WriteLog("AddUpdateCompTypeService:Exception found:CompType" + CompType.ToStringDump() + " " + ex.Message, true);
            }
            return jres;
        }

        public PolicyBoolResponse DeleteCompTypeService(CompType CompType)
        {
            ActionLogger.Logger.WriteLog("DeleteCompTypeService request: CompType" + CompType.ToStringDump(), true);
            PolicyBoolResponse jres = null;
            try
            {
                CompType objCompType = new CompType();
                bool result = objCompType.DeleteCompType(CompType);
                if (result)
                {
                    jres = new PolicyBoolResponse("Comp type details deleted successfully", (int)ResponseCodes.Success, "");
                    ActionLogger.Logger.WriteLog("DeleteCompTypeService success", true);
                }
                else
                {
                    jres = new PolicyBoolResponse("Comp type details could not be deleted", (int)ResponseCodes.Failure, "Comp type details could not be deleted");
                    ActionLogger.Logger.WriteLog("DeleteCompTypeService failure", true);
                }
                jres.BoolFlag = result;
            }
            catch (Exception ex)
            {
                jres = new PolicyBoolResponse("", (int)ResponseCodes.Failure, ex.Message);
                ActionLogger.Logger.WriteLog("DeleteCompTypeService failure", true);
            }
            return jres;
        }

        public PolicyBoolResponse FindCompTypeNameService(string strName)
        {
            ActionLogger.Logger.WriteLog("FindCompTypeNameService request: strName" + strName, true);
            PolicyBoolResponse jres = null;
            try
            {
                CompType objCompType = new CompType();
                bool result = objCompType.FindCompTypeName(strName);
                if (result)
                {
                    jres = new PolicyBoolResponse("Comp type name found successfully", (int)ResponseCodes.Success, "");
                    ActionLogger.Logger.WriteLog("FindCompTypeNameService success", true);
                }
                else
                {
                    jres = new PolicyBoolResponse("Comp type name could not be found", (int)ResponseCodes.Failure, "Comp type name could not be found");
                    ActionLogger.Logger.WriteLog("FindCompTypeNameService failure", true);
                }
                jres.BoolFlag = result;
            }
            catch (Exception ex)
            {
                jres = new PolicyBoolResponse("", (int)ResponseCodes.Failure, ex.Message);
                ActionLogger.Logger.WriteLog("FindCompTypeNameService failure", true);
            }
            return jres;
        }
    }
}