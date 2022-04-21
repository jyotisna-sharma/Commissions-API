using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyAgencyVault.BusinessLibrary;
using System.ServiceModel.Web;
using MyAgencyVault.WcfService.Library.Response;
using System.ServiceModel;
using MyAgencyVault.EmailFax;

namespace MyAgencyVault.WcfService
{
    [ServiceContract]
    interface IBrokerCodeService
    {
        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        JSONResponse AddBrokerCodeService(DisplayBrokerCode objDisplayBrokerCode);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        JSONResponse UpdateBrokerCodeService(DisplayBrokerCode objDisplayBrokerCode);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        PolicyBoolResponse ValidateBrokerCodeService(string strBrokerCode);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        DisplayBrokerListResponse LoadBrokerCodeService(Guid? licID);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        PolicyBoolResponse DeleteBrokerCodeService(DisplayBrokerCode objDisplayBrokerCode);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        JSONResponse NotifyMailService(MailData _MailData, string strSubject, string strMailBody);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        JSONResponse AddImportToolBrokerSettingsService(ImportToolBrokerSetting objImportToolBrokerSetting);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        BrokerListResponse LoadImportToolBrokerSettingService();

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        MasterStatementListResponse LoadImportToolMasterStatementDataService();
    }

    public partial class MavService : IBrokerCodeService
    {
        public JSONResponse AddBrokerCodeService(DisplayBrokerCode objDisplayBrokerCode)
        {
            ActionLogger.Logger.WriteLog("AddBrokerCodeService request: objDisplayBrokerCode" + objDisplayBrokerCode.ToStringDump(), true);
            JSONResponse jres = null;
            try
            {
                Brokercode objBrokercode = new Brokercode();
                objBrokercode.AddBrokerCode(objDisplayBrokerCode);
          
                jres = new JSONResponse("Broker details saved successfully", (int)ResponseCodes.Success, "");
                ActionLogger.Logger.WriteLog("AddBrokerCodeService success", true);
            }
            catch (Exception ex)
            {
                jres = new JSONResponse("", (int)ResponseCodes.Failure, ex.Message);
                ActionLogger.Logger.WriteLog("AddBrokerCodeService failure", true);
            }
            return jres;
        }

        public JSONResponse UpdateBrokerCodeService(DisplayBrokerCode objDisplayBrokerCode)
        {
            ActionLogger.Logger.WriteLog("UpdateBrokerCodeService request: objDisplayBrokerCode" + objDisplayBrokerCode.ToStringDump(), true);
            JSONResponse jres = null;
            try
            {
                Brokercode objBrokercode = new Brokercode();
                objBrokercode.UpdateBrokerCode(objDisplayBrokerCode);
                jres = new JSONResponse("Broker details saved successfully", (int)ResponseCodes.Success, "");
                ActionLogger.Logger.WriteLog("UpdateBrokerCodeService success", true);
            }
            catch (Exception ex)
            {
                jres = new JSONResponse("", (int)ResponseCodes.Failure, ex.Message);
                ActionLogger.Logger.WriteLog("UpdateBrokerCodeService failure", true);
            }
            return jres;
        }

        public PolicyBoolResponse ValidateBrokerCodeService(string strBrokerCode)
        {
            PolicyBoolResponse jres = null;
            ActionLogger.Logger.WriteLog("ValidateBrokerCodeService request: strBrokerCode -" + strBrokerCode, true);
            try
            {
                Brokercode objBrokercode = new Brokercode();
                bool res = objBrokercode.ValidateBrokerCode(strBrokerCode);

                jres = (!res) ? new PolicyBoolResponse(string.Format("Broker code validated successfully"), Convert.ToInt16(ResponseCodes.Success), "") :
                          new PolicyBoolResponse("", Convert.ToInt16(ResponseCodes.Failure), "Broker code validation failed ");

                jres.BoolFlag = res;
                ActionLogger.Logger.WriteLog("ValidateBrokerCodeService success ", true);
            }
            catch (Exception ex)
            {
                jres = new PolicyBoolResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                ActionLogger.Logger.WriteLog("ValidateBrokerCodeService failure ", true);
            }
            return jres;
        }

        public DisplayBrokerListResponse LoadBrokerCodeService(Guid? licID)
        {
            ActionLogger.Logger.WriteLog("LoadBrokerCodeService request: licID " + licID, true);
            DisplayBrokerListResponse jres = null;
            try
            {
                Brokercode objBrokercode = new Brokercode();
                List<DisplayBrokerCode> lst = objBrokercode.LoadBrokerCode(licID);
                if (lst != null && lst.Count > 0)
                {
                    jres = new DisplayBrokerListResponse("Broker list found successfully", (int)ResponseCodes.Success, "");
                    jres.DisplayBrokerList = lst;
                    ActionLogger.Logger.WriteLog("LoadBrokerCodeService success", true);
                }
                else
                {
                    jres = new DisplayBrokerListResponse("No broker record found", (int)ResponseCodes.RecordNotFound, "No broker record found");
                    ActionLogger.Logger.WriteLog("LoadBrokerCodeService 404", true);
                }
            }
            catch (Exception ex)
            {
                jres = new DisplayBrokerListResponse("", (int)ResponseCodes.Failure, ex.Message);
                ActionLogger.Logger.WriteLog("LoadBrokerCodeService failure", true);
            }
            return jres;
        }

        public PolicyBoolResponse DeleteBrokerCodeService(DisplayBrokerCode ObjBrokerCode)
        {
            PolicyBoolResponse jres = null;
            ActionLogger.Logger.WriteLog("DeleteBrokerCodeService request: ObjBrokerCode -" + ObjBrokerCode.ToStringDump(), true);
            try
            {
                Brokercode objBrokercode = new Brokercode();
                bool res = objBrokercode.DeleteBrokerCode(ObjBrokerCode);

                jres = (res) ? new PolicyBoolResponse(string.Format("Broker code deleted successfully"), Convert.ToInt16(ResponseCodes.Success), "") :
                          new PolicyBoolResponse("", Convert.ToInt16(ResponseCodes.Failure), "Broker code could not be deleted ");

                jres.BoolFlag = res;
                ActionLogger.Logger.WriteLog("DeleteBrokerCodeService success ", true);
            }
            catch (Exception ex)
            {
                jres = new PolicyBoolResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                ActionLogger.Logger.WriteLog("DeleteBrokerCodeService failure ", true);
            }
            return jres;
        }

        public JSONResponse NotifyMailService(MailData _MailData, string strSubject, string strMailBody)
        {
            ActionLogger.Logger.WriteLog("NotifyMailService request: _MailData" + _MailData.ToStringDump(), true);
            JSONResponse jres = null;
            try
            {
                Brokercode objBrokercode = new Brokercode();
                objBrokercode.NotifyMail(_MailData, strSubject, strMailBody);
                jres = new JSONResponse("Mail sent successfully", (int)ResponseCodes.Success, "");
                ActionLogger.Logger.WriteLog("NotifyMailService success", true);
            }
            catch (Exception ex)
            {
                jres = new JSONResponse("", (int)ResponseCodes.Failure, ex.Message);
                ActionLogger.Logger.WriteLog("NotifyMailService failure", true);
            }
            return jres;
        }

        public JSONResponse AddImportToolBrokerSettingsService(ImportToolBrokerSetting objImportToolBrokerSetting)
        {
            ActionLogger.Logger.WriteLog("AddImportToolBrokerSettingsService request: objImportToolBrokerSetting" + objImportToolBrokerSetting.ToStringDump(), true);
            JSONResponse jres = null;
            try
            {
                Brokercode objBrokercode = new Brokercode();
                objBrokercode.AddImportToolBrokerSettings(objImportToolBrokerSetting);
          
                jres = new JSONResponse("Broker settings saved successfully", (int)ResponseCodes.Success, "");
                ActionLogger.Logger.WriteLog("AddImportToolBrokerSettingsService success", true);
            }
            catch (Exception ex)
            {
                jres = new JSONResponse("", (int)ResponseCodes.Failure, ex.Message);
                ActionLogger.Logger.WriteLog("AddImportToolBrokerSettingsService failure", true);
            }
            return jres;
        }

        public BrokerListResponse LoadImportToolBrokerSettingService()
        {
            ActionLogger.Logger.WriteLog("LoadBrokerCodeService request: ", true);
            BrokerListResponse jres = null;
            try
            {
                Brokercode objBrokercode = new Brokercode();
                List<ImportToolBrokerSetting> lst = objBrokercode.LoadImportToolBrokerSetting();

                if (lst != null && lst.Count > 0)
                {
                    jres = new BrokerListResponse("Broker list found successfully", (int)ResponseCodes.Success, "");
                    jres.BrokerList = lst;
                    ActionLogger.Logger.WriteLog("LoadBrokerCodeService success", true);
                }
                else
                {
                    jres = new BrokerListResponse("No broker record found", (int)ResponseCodes.RecordNotFound, "No broker record found");
                    ActionLogger.Logger.WriteLog("LoadBrokerCodeService 404", true);
                }
            }
            catch (Exception ex)
            {
                jres = new BrokerListResponse("", (int)ResponseCodes.Failure, ex.Message);
                ActionLogger.Logger.WriteLog("LoadBrokerCodeService failure", true);
            }
            return jres;
        }

        public MasterStatementListResponse LoadImportToolMasterStatementDataService()
        {
            ActionLogger.Logger.WriteLog("LoadImportToolMasterStatementDataService request: ", true);
            MasterStatementListResponse jres = null;
            try
            {
                Brokercode objBrokercode = new Brokercode();
                List<ImportToolMasterStatementData> lst = objBrokercode.LoadImportToolMasterStatementData();

                if (lst != null && lst.Count > 0)
                {
                    jres = new MasterStatementListResponse("Statement list found successfully", (int)ResponseCodes.Success, "");
                    jres.StatementDataList = lst;
                    ActionLogger.Logger.WriteLog("LoadImportToolMasterStatementDataService success", true);
                }
                else
                {
                    jres = new MasterStatementListResponse("No statement record found", (int)ResponseCodes.RecordNotFound, "No broker record found");
                    ActionLogger.Logger.WriteLog("LoadImportToolMasterStatementDataService 404", true);
                }
            }
            catch (Exception ex)
            {
                jres = new MasterStatementListResponse("", (int)ResponseCodes.Failure, ex.Message);
                ActionLogger.Logger.WriteLog("LoadImportToolMasterStatementDataService failure", true);
            }
            return jres;
        }
    }
}