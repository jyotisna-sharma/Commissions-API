using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Web;
using MyAgencyVault.WcfService.Library.Response;
using MyAgencyVault.BusinessLibrary;

namespace MyAgencyVault.WcfService
{
    [ServiceContract]
    interface ISettingService
    {
        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        GetSettingsListResponse GetSettingsListService(Guid licenseeId, ListParams listParams);
        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        GetSettingsListResponse GetNamedScheduleListService(Guid licenseeId, ListParams listParams);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        JSONResponse SaveSchedule(PayorIncomingSchedule schedule, int overwrite = 0);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        JSONResponse SaveNamedSchedule(PayorIncomingSchedule schedule);
        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        IsScheduleExistResponse IsNamedScheduleExist(string scheduleName, Guid? incomingScheduleId, Guid licenseeId);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        JSONResponse DeleteSchedule(Guid incomingScheduleId);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        PayorScheduleDetailResponse GetCommissionScheduleDetail(Guid incomingScheduleId);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        PayorScheduleDetailResponse GetPayorScheduleDetail(ScheduleQuery parameters);
        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        ReportSettingListResponse GetReportSettingListService(Guid licenseeId, Guid reportId);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        JSONResponse SaveReportSettingListService(Guid licenseeId, Guid reportId, string fields);
    }
    public partial class MavService : ISettingService
    {
        public GetSettingsListResponse GetSettingsListService(Guid licenseeId, ListParams listParams)
        {
            GetSettingsListResponse response = null;
            try
            {
                List<PayorIncomingSchedule> settingList = PayorIncomingSchedule.GetAllSchedules(licenseeId, listParams, out double count);
                response = new GetSettingsListResponse("List found Successfully", (int)ResponseCodes.Success, "");
                response.SettingList = settingList;
                response.listCount = count;
            }
            catch (Exception ex)
            {
                ActionLogger.Logger.WriteLog(DateTime.Now.ToString() + " Exception Occurs getting setting List:licenseeId " + licenseeId + "Exception:" + ex.Message, true);
                response = new GetSettingsListResponse("Exception Occurs getting setting List: " + ex.Message, (int)ResponseCodes.Failure, ex.Message);
            }

            return response;
        }
        //public GetSettingsListResponse GetNamedScheduleListResponse(Guid licenseeId, ListParams listParams)
        //{
        //    GetSettingsListResponse response = null;
        //    try
        //    {
        //        List<PayorIncomingSchedule> namedScheuleSettingList = PayorIncomingSchedule.GetAllSchedules(licenseeId, listParams, out double count);
        //        response = new GetSettingsListResponse("List found Successfully", (int)ResponseCodes.Success, "");
        //        response.SettingList = namedScheuleSettingList;
        //        response.listCount = count;
        //    }
        //    catch (Exception ex)
        //    {
        //        ActionLogger.Logger.WriteLog(DateTime.Now.ToString() + " Exception Occurs getting setting List:licenseeId " + licenseeId + "Exception:" + ex.Message, true);
        //        response = new GetSettingsListResponse("Exception Occurs getting setting List: " + ex.Message, (int)ResponseCodes.Failure, ex.Message);
        //    }

        //    return response;
        //}

        public JSONResponse SaveSchedule(PayorIncomingSchedule schedule, int overwrite = 0)
        {
            JSONResponse resp;
            try
            {
                PayorIncomingSchedule.SaveSchedule(schedule, overwrite);
                //if (isRecordExist == false)
                //{
                ActionLogger.Logger.WriteLog(DateTime.Now.ToString() + " Incoming schedule saved successfully: IncomingScheduleID" + schedule.IncomingScheduleID, true);
                resp = new JSONResponse("Incoming schedule saved successfully", (int)ResponseCodes.Success, "");

                //}
                //else
                //{
                //    ActionLogger.Logger.WriteLog(DateTime.Now.ToString() + " Incoming schedule already exist:IncomingScheduleID" + schedule.IncomingScheduleID, true);
                //    resp = new JSONResponse("Incoming schedule already exist", (int)ResponseCodes.RecordAlreadyExist, "");
                //}
            }
            catch (Exception ex)
            {
                ActionLogger.Logger.WriteLog(DateTime.Now.ToString() + " SaveSchedule error: " + ex.Message, true);
                resp = new JSONResponse("Error saving incoming schedule: " + ex.Message, (int)ResponseCodes.Failure, ex.Message);
            }
            return resp;
        }
        #region
        public JSONResponse SaveNamedSchedule(PayorIncomingSchedule schedule)
        {
            JSONResponse resp;
            try
            {
                PayorIncomingSchedule.SaveNamedSchedule(schedule);
                ActionLogger.Logger.WriteLog(DateTime.Now.ToString() + " Incoming schedule saved successfully: IncomingScheduleID" + schedule.IncomingScheduleID, true);
                resp = new JSONResponse("Incoming schedule saved successfully", (int)ResponseCodes.Success, "");
            }
            catch (Exception ex)
            {
                ActionLogger.Logger.WriteLog(DateTime.Now.ToString() + " SaveSchedule error: " + ex.Message, true);
                resp = new JSONResponse("Error saving incoming schedule: " + ex.Message, (int)ResponseCodes.Failure, ex.Message);
            }
            return resp;
        }
        public IsScheduleExistResponse IsNamedScheduleExist(string scheduleName,Guid? incomingScheduleId,Guid licenseeId)
        {
            ActionLogger.Logger.WriteLog(DateTime.Now.ToString() + " IsNamedScheduleExist:scheduleName " + scheduleName, true);
            IsScheduleExistResponse response = null;
            try
            {
                bool isExist = Settings.IsNamedScheduleExist(scheduleName, incomingScheduleId, licenseeId);
                response = new IsScheduleExistResponse("IsNamedScheduleExist:Details are successfully fetched scheduleName:" + scheduleName, (int)ResponseCodes.Success, "");
                response.isExist = isExist;
               
            }
            catch(Exception ex)
            {
                ActionLogger.Logger.WriteLog(DateTime.Now.ToString() + " IsNamedScheduleExist:scheduleName" + scheduleName  + ex.Message, true);
                response = new IsScheduleExistResponse("IsNamedScheduleExist: Exception occurs while fetching scheduleName:" + scheduleName, (int)ResponseCodes.Success, "");
            }
            return response;
        }
        public GetSettingsListResponse GetNamedScheduleListService(Guid licenseeId, ListParams listParams)
        {
            GetSettingsListResponse response = null;
            try
            {
                List<PayorIncomingSchedule> settingList = PayorIncomingSchedule.GetNamedScheduleList(licenseeId, listParams, out double count);
                response = new GetSettingsListResponse("List found Successfully", (int)ResponseCodes.Success, "");
                response.SettingList = settingList;
                response.listCount = count;
            }
            catch (Exception ex)
            {
                ActionLogger.Logger.WriteLog(DateTime.Now.ToString() + " Exception Occurs getting setting List:licenseeId " + licenseeId + "Exception:" + ex.Message, true);
                response = new GetSettingsListResponse("Exception Occurs getting setting List: " + ex.Message, (int)ResponseCodes.Failure, ex.Message);
            }

            return response;
        }
        #endregion
        public JSONResponse DeleteSchedule(Guid incomingScheduleId)
        {
            JSONResponse resp;
            try
            {
                PayorIncomingSchedule.DeleteSchedule(incomingScheduleId);
                resp = new JSONResponse("Incoming schedule deleted successfully", (int)ResponseCodes.Success, "");
            }
            catch (Exception ex)
            {
                ActionLogger.Logger.WriteLog(DateTime.Now.ToString() + " DeleteSchedule error: " + ex.Message, true);
                resp = new JSONResponse("Error removing incoming schedule: " + ex.Message, (int)ResponseCodes.Failure, ex.Message);
            }
            return resp;
        }

        public PayorScheduleDetailResponse GetCommissionScheduleDetail(Guid incomingScheduleId)
        {
            PayorScheduleDetailResponse resp;
            try
            {
                PayorIncomingSchedule details = PayorIncomingSchedule.GetCommissionScheduleDetails(incomingScheduleId);
                resp = new PayorScheduleDetailResponse("Payor schedule details found successfully", (int)ResponseCodes.Success, "");
                resp.ScheduleDetails = details;
            }
            catch (Exception ex)
            {
                ActionLogger.Logger.WriteLog(DateTime.Now.ToString() + " GetPayorScheduleDetail error: " + ex.Message, true);
                resp = new PayorScheduleDetailResponse("Error in getting payor schedule: " + ex.Message, (int)ResponseCodes.Failure, ex.Message);
            }
            return resp;
        }
        public PayorScheduleDetailResponse GetPayorScheduleDetail(ScheduleQuery parameters)
        {
            PayorScheduleDetailResponse resp;
            try
            {
                PayorIncomingSchedule details = PayorIncomingSchedule.GetPayorScheduleDetails(parameters);
                resp = new PayorScheduleDetailResponse("Payor schedule details found successfully", (int)ResponseCodes.Success, "");
                //resp.IsExisting = (details != null && details.IncomingScheduleID != Guid.Empty && details.IncomingScheduleID != parameters.) ? true : false;
                resp.ScheduleDetails = details;
            }
            catch (Exception ex)
            {
                ActionLogger.Logger.WriteLog(DateTime.Now.ToString() + " GetPayorScheduleDetail error: " + ex.Message, true);
                resp = new PayorScheduleDetailResponse("Error in getting payor schedule: " + ex.Message, (int)ResponseCodes.Failure, ex.Message);
            }
            return resp;
        }

        public ReportSettingListResponse GetReportSettingListService(Guid licenseeId, Guid reportId)
        {
            ReportSettingListResponse response = null;
            try
            {
                List<ReportCustomFieldSettings> list = Settings.ReportSettingListData(licenseeId, reportId);
                {
                    response = new ReportSettingListResponse("", (int)ResponseCodes.Success, "");
                    response.ReportSettingList = list;
                    response.RecordCount = list.Count;
                }
            }
            catch (Exception ex)
            {

            }
            return response;
        }

        public JSONResponse SaveReportSettingListService(Guid licenseeId, Guid reportId, string fields)
        {
            ActionLogger.Logger.WriteLog("SaveReportSettingListService:Exceution start for licenseeId" + licenseeId + " " + "fields:" + fields, true);
            JSONResponse response = null;
            try
            {
                Settings.AddUpdate(licenseeId, reportId, fields);
                response = new JSONResponse(_resourceManager.GetString("SettingUpdateSuccessfully"), (int)ResponseCodes.Success, "");
            }
            catch (Exception ex)
            {
                response = new JSONResponse(_resourceManager.GetString("SettingFailureMessage"), (int)ResponseCodes.Failure, "");
                ActionLogger.Logger.WriteLog("Exception occur while saving your settings:licenseeId" + licenseeId + "Excepiton:" + ex.Message, true);
            }
            return response;
        }
    }
}
