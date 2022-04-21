using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyAgencyVault.WcfService.Library.Response;
using System.ServiceModel;
using MyAgencyVault.BusinessLibrary;
using System.ServiceModel.Web;

namespace MyAgencyVault.WcfService
{
    [ServiceContract]
    interface ISendMailService
    {
        [OperationContract()]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        PolicyBoolResponse SendMailService(string toAddress, string subject, string body);

        [OperationContract()]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        PolicyStringResponse GetAlertCommissionDepartmentMailIdSrvc();
    }

    public partial class MavService : ISendMailService
    {
        public PolicyBoolResponse SendMailService(string toAddress, string subject, string body)
        {
            PolicyBoolResponse jres = null;
            ActionLogger.Logger.WriteLog("SendMailService request: toAddress -" + toAddress + ", subject: " + subject + ", body: " + body, true);
            try
            {
                bool res = MailServerDetail.sendMail(toAddress, subject, body);
                jres = new PolicyBoolResponse(string.Format("Mail sent successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                jres.BoolFlag = res;
                ActionLogger.Logger.WriteLog("SendMailService success ", true);
            }
            catch (Exception ex)
            {
                jres = new PolicyBoolResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                ActionLogger.Logger.WriteLog("SendMailService failure ", true);
            }
            return jres;
        }

        public PolicyStringResponse GetAlertCommissionDepartmentMailIdSrvc()
        {
            PolicyStringResponse jres = null;
            ActionLogger.Logger.WriteLog("GetAlertCommissionDepartmentMailIdSrvc request: ", true);
            try
            {
                string s = MailServerDetail.GetAlertCommissionDepartmentMail();
                if (!string.IsNullOrEmpty(s))
                {
                    jres = new PolicyStringResponse(string.Format("Mail details successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                    jres.StringValue = s;
                    ActionLogger.Logger.WriteLog("GetAlertCommissionDepartmentMailIdSrvc success ", true);
                }
                else
                {
                    jres = new PolicyStringResponse(string.Format("Mail details not found"), Convert.ToInt16(ResponseCodes.RecordNotFound), "");
                    ActionLogger.Logger.WriteLog("GetAlertCommissionDepartmentMailIdSrvc  404 ", true);
                }
            }
            catch (Exception ex)
            {
                jres = new PolicyStringResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                ActionLogger.Logger.WriteLog("GetAlertCommissionDepartmentMailIdSrvc failure ", true);
            }
            return jres;
        }
    }
}