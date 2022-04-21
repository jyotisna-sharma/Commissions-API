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
    interface IPolicyCommDashboardService
    {
        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        PolicyBoolResponse IsEntryMarkPaidService(Guid PaymentEntryId);
        
        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        PolicyOutgoingResponse GetOutgoingByPaymentEntryIdService(Guid EntryId);
        
        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        JSONResponse DeleteByPolicyPaymentIdService(Guid PaymentEntryId);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        JSONResponse AddUpdateOutgoingPaymentService(PolicyOutgoingDistribution _PolicyOutgoingDistribution);
        
        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        JSONResponse AddUpdateMultipleOutGoingPaymentService(List<PolicyOutgoingDistribution> _PolicyOutgoingDistribution);
        
        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        JSONResponse DeleteOutGoingPaymentViaIdService(Guid OutgoingPaymentid);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        JSONResponse DeleteOutgoingScheduleService(OutGoingPayment OutGoingPymnt);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        PolicyOutgoingPaymentResponse GetOutgoingScheduleService();

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        PolicyOutgoingPaymentResponse GetOutgoingSheduleForPolicyService(Guid PolicyId);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        PolicyBoolResponse CheckIsPaymentFromDEUForOutgoingPaymentIDSrvc(Guid OutgoingPaymentid);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        PolicyOutgoingDistributionResponse GetOutgoingPaymentByIDService(Guid OutgoingPaymentid);
    }

    public partial class MavService : IPolicyCommDashboardService
    {
        public PolicyBoolResponse IsEntryMarkPaidService(Guid PaymentEntryId)
        {
            PolicyBoolResponse jres = null;
            ActionLogger.Logger.WriteLog("IsEntryMarkPaid request: " + PaymentEntryId, true);
            try
            {
                bool isPaid = PolicyOutgoingDistribution.IsEntryMarkPaid(PaymentEntryId);
                jres = new PolicyBoolResponse(string.Format("Data found successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                jres.BoolFlag = isPaid;
                ActionLogger.Logger.WriteLog("IsEntryMarkPaid success ", true);
            }
            catch (Exception ex)
            {
                jres = new PolicyBoolResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                ActionLogger.Logger.WriteLog("IsEntryMarkPaid failure " + ex.Message, true);
            }
            return jres;
        }

        public PolicyOutgoingResponse GetOutgoingByPaymentEntryIdService(Guid EntryId)
        {
            PolicyOutgoingResponse jres = null;
            ActionLogger.Logger.WriteLog("GetOutgoingByPaymentEntryIdService request: " + EntryId, true);
            try
            {
                List<PolicyOutgoingDistribution> lst = PolicyOutgoingDistribution.GetOutgoingPaymentByPoicyPaymentEntryId(EntryId);
                jres = new PolicyOutgoingResponse(string.Format("Data found successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                jres.Policylist = lst;
                ActionLogger.Logger.WriteLog("GetOutgoingByPaymentEntryIdService success ", true);
            }
            catch (Exception ex)
            {
                jres = new PolicyOutgoingResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                ActionLogger.Logger.WriteLog("GetOutgoingByPaymentEntryIdService failure " + ex.Message, true);
            }
            return jres;
        }

        public JSONResponse DeleteByPolicyPaymentIdService(Guid PaymentEntryId)
        {
            JSONResponse jres = null;
            ActionLogger.Logger.WriteLog("DeleteByPolicyPaymentIdService request: " + PaymentEntryId, true);
            try
            {
                PolicyOutgoingDistribution.DeleteByPolicyIncomingPaymentId(PaymentEntryId);
                jres = new JSONResponse(string.Format("Data deleted successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                ActionLogger.Logger.WriteLog("DeleteByPolicyPaymentIdService success ", true);
            }
            catch (Exception ex)
            {
                jres = new JSONResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                ActionLogger.Logger.WriteLog("DeleteByPolicyPaymentIdService failure " + ex.Message, true);
            }
            return jres;
        }

        public JSONResponse AddUpdateOutgoingPaymentService(PolicyOutgoingDistribution _PolicyOutgoingDistribution)
        {
           JSONResponse jres = null;
           ActionLogger.Logger.WriteLog("AddUpdateOutgoingPaymentService request: " + _PolicyOutgoingDistribution.ToStringDump(), true);
            try
            {
                PolicyOutgoingDistribution.AddUpdateOutgoingPaymentEntry(_PolicyOutgoingDistribution);
                jres = new JSONResponse(string.Format("Data saved successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                ActionLogger.Logger.WriteLog("AddUpdateOutgoingPaymentService success ", true);
            }
            catch (Exception ex)
            {
                jres = new JSONResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                ActionLogger.Logger.WriteLog("AddUpdateOutgoingPaymentService failure " + ex.Message, true);
            }
            return jres;
        }

        public JSONResponse AddUpdateMultipleOutGoingPaymentService(List<PolicyOutgoingDistribution> _PolicyOutgoingDistribution)
        {
            JSONResponse jres = null;
            ActionLogger.Logger.WriteLog("AddUpdateMultipleOutGoingPaymentService request: " + _PolicyOutgoingDistribution.ToStringDump(), true);
            try
            {
                foreach (PolicyOutgoingDistribution _PolicyOutg in _PolicyOutgoingDistribution)
                {
                    PolicyOutgoingDistribution.AddUpdateOutgoingPaymentEntry(_PolicyOutg);
                }
                jres = new JSONResponse(string.Format("Data saved successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                ActionLogger.Logger.WriteLog("AddUpdateMultipleOutGoingPaymentService success ", true);
            }
            catch (Exception ex)
            {
                jres = new JSONResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                ActionLogger.Logger.WriteLog("AddUpdateMultipleOutGoingPaymentService failure " + ex.Message, true);
            }
            return jres;
        }

        public JSONResponse DeleteOutGoingPaymentViaIdService(Guid OutgoingPaymentid)
        {
            JSONResponse jres = null;
            ActionLogger.Logger.WriteLog("DeleteOutGoingPaymentViaIdService request: " + OutgoingPaymentid.ToStringDump(), true);
            try
            {
                string body = PolicyOutgoingDistribution.GetOutgoingDeleteEmailContent(OutgoingPaymentid);
                PolicyOutgoingDistribution.DeleteById(OutgoingPaymentid);
                if (!string.IsNullOrEmpty(body))
                {
                    MailServerDetail.sendMail("deudev@acmeminds.com", "Commissions Alert: Outgoing payment manually deleted from the system", body);
                }
                jres = new JSONResponse(string.Format("Data deleted successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                ActionLogger.Logger.WriteLog("DeleteOutGoingPaymentViaIdService success ", true);
            }
            catch (Exception ex)
            {
                jres = new JSONResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                ActionLogger.Logger.WriteLog("DeleteOutGoingPaymentViaIdService failure " + ex.Message, true);
            }
            return jres;
        }

        public JSONResponse DeleteOutgoingScheduleService(OutGoingPayment OutGoingPymnt)
        {
            JSONResponse jres = null;
            ActionLogger.Logger.WriteLog("DeleteOutgoingScheduleService request: " + OutGoingPymnt.ToStringDump(), true);
            try
            {
                OutGoingPymnt.Delete();
                jres = new JSONResponse(string.Format("Data deleted successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                ActionLogger.Logger.WriteLog("DeleteOutgoingScheduleService success ", true);
            }
            catch (Exception ex)
            {
                jres = new JSONResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                ActionLogger.Logger.WriteLog("DeleteOutgoingScheduleService failure " + ex.Message, true);
            }
            return jres;
        }

        public PolicyOutgoingPaymentResponse GetOutgoingScheduleService()
        {
            PolicyOutgoingPaymentResponse jres = null;
            ActionLogger.Logger.WriteLog("GetOutgoingScheduleService request: ", true);
            try
            {
                List<OutGoingPayment> lst = OutGoingPayment.GetOutgoingShedule();
                jres = new PolicyOutgoingPaymentResponse(string.Format("Data found successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                jres.PolicyOutgoingScheduleList = lst;
                ActionLogger.Logger.WriteLog("GetOutgoingScheduleService success ", true);
            }
            catch (Exception ex)
            {
                jres = new PolicyOutgoingPaymentResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                ActionLogger.Logger.WriteLog("GetOutgoingScheduleService failure " + ex.Message, true);
            }
            return jres;
        }

        public PolicyOutgoingPaymentResponse GetOutgoingSheduleForPolicyService(Guid PolicyId)
        {
            PolicyOutgoingPaymentResponse jres = null;
            ActionLogger.Logger.WriteLog("GetOutgoingSheduleForPolicyService request: ", true);
            try
            {
                List<OutGoingPayment> lst = OutGoingPayment.GetOutgoingSheduleForPolicy(PolicyId);
                jres = new PolicyOutgoingPaymentResponse(string.Format("Data found successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                jres.PolicyOutgoingScheduleList = lst;
                ActionLogger.Logger.WriteLog("GetOutgoingSheduleForPolicyService success ", true);
            }
            catch (Exception ex)
            {
                jres = new PolicyOutgoingPaymentResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                ActionLogger.Logger.WriteLog("GetOutgoingSheduleForPolicyService failure " + ex.Message, true);
            }
            return jres;
        }

        public PolicyBoolResponse CheckIsPaymentFromDEUForOutgoingPaymentIDSrvc(Guid OutgoingPaymentid)
        {
            PolicyBoolResponse jres = null;
            ActionLogger.Logger.WriteLog("CheckIsPaymentFromDEUForOutgoingPaymentIDSrvc request: " + OutgoingPaymentid, true);
            try
            {
                bool isPaid = PolicyOutgoingDistribution.CheckIsPaymentFromDEU(OutgoingPaymentid);
                jres = new PolicyBoolResponse(string.Format("Data found successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                jres.BoolFlag = isPaid;
                ActionLogger.Logger.WriteLog("CheckIsPaymentFromDEUForOutgoingPaymentIDSrvc success ", true);
            }
            catch (Exception ex)
            {
                jres = new PolicyBoolResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                ActionLogger.Logger.WriteLog("CheckIsPaymentFromDEUForOutgoingPaymentIDSrvc failure " + ex.Message, true);
            }
            return jres;
        }

        public PolicyOutgoingDistributionResponse GetOutgoingPaymentByIDService(Guid OutgoingPaymentid)
        {
            PolicyOutgoingDistributionResponse jres = null;
            ActionLogger.Logger.WriteLog("GetOutgoingPaymentByIDService request: " + OutgoingPaymentid, true);
            try
            {
                 PolicyOutgoingDistribution obj = PolicyOutgoingDistribution.GetOutgoingPaymentById(OutgoingPaymentid);
                jres = new PolicyOutgoingDistributionResponse(string.Format("Data found successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                jres.OutgoingDistribution = obj;
                ActionLogger.Logger.WriteLog("GetOutgoingPaymentByIDService success ", true);
            }
            catch (Exception ex)
            {
                jres = new PolicyOutgoingDistributionResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                ActionLogger.Logger.WriteLog("GetOutgoingPaymentByIDService failure " + ex.Message, true);
            }
            return jres;
        }
    }
}