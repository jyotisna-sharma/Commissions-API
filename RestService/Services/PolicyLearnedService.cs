using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ServiceModel;
using MyAgencyVault.WcfService.Library.Response;
using MyAgencyVault.BusinessLibrary;
using System.ServiceModel.Web;
using PGK.Extensions;

namespace MyAgencyVault.WcfService
{
    [ServiceContract]
    interface IPolicyLearnedService
    {

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        JSONResponse AddLearnedToPolicyService(Guid PolicyID);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        JSONResponse AddUpdatPolicyToLearnService(Guid PolicyID);    

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        JSONResponse AddUpdatePolicyLearnedFieldService(PolicyLearnedFieldData PolicyLernField, string strProductType);
   
        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        JSONResponse DeletePolicyLearnedFieldService(PolicyLearnedFieldData PolicyLernField);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        PolicyBoolResponse IsValidPolicyLearnedFieldService(PolicyLearnedFieldData PolicyLernField);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        PolicyLearnedResponse GetPolicyLearnedFieldsPolicyWiseService(Guid PolicyID);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        JSONResponse AddUpdateHistoryLearnedService(Guid PolicyId);
        
        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        PolicyLearnedResponse GetPolicyLearnedFieldsHistoryPolicyWiseService(Guid PolicyID);
        
        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        JSONResponse DeleteLearnedHistoryService(Guid PolicyID);
   
        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        PolicyDateResponse GetPolicyLearnedFieldAutoTerminationDateService(Guid PolicyID);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        PolicyStringResponse GetPolicyLearnedCoverageNickNameService(Guid PolicyID, Guid PayorID, Guid CarrierID, Guid CoverageID);
    }


     public partial class MavService : IPolicyLearnedService
    {
         public JSONResponse AddLearnedToPolicyService(Guid PolicyID)
         {
            JSONResponse jres = null;
            ActionLogger.Logger.WriteLog("AddLearnedToPolicy request: " + PolicyID, true);
             try
             {
                 LearnedToPolicyPost.AddUpdateLearnedToPolicy(PolicyID);
                 ActionLogger.Logger.WriteLog("AddLearnedToPolicy success ", true);
                 jres = new JSONResponse(string.Format("Policy details saved successfully"), Convert.ToInt16(ResponseCodes.Success), "");
             }
             catch (Exception ex)
             {
                 jres = new JSONResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                 ActionLogger.Logger.WriteLog("AddLearnedToPolicy failure: " + ex.Message, true);
             }
             return jres;
         }

         public JSONResponse AddUpdatPolicyToLearnService(Guid PolicyID)
         {
             JSONResponse jres = null;
             ActionLogger.Logger.WriteLog("AddUpdatPolicyToLearn request: " + PolicyID, true);
             try
             {
                 PolicyToLearnPost.AddUpdatPolicyToLearn(PolicyID);
                 ActionLogger.Logger.WriteLog("AddUpdatPolicyToLearn success ", true);
                 jres = new JSONResponse(string.Format("Policy learned fields saved successfully"), Convert.ToInt16(ResponseCodes.Success), "");
             }
             catch (Exception ex)
             {
                 jres = new JSONResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                 ActionLogger.Logger.WriteLog("AddUpdatPolicyToLearn failure: " + ex.Message, true);
             }
             return jres;
         }

        public JSONResponse AddUpdatePolicyLearnedFieldService(PolicyLearnedFieldData PolicyLernField, string strProductType)
        {
            JSONResponse jres = null;
            ActionLogger.Logger.WriteLog("AddUpdatePolicyLearnedField request: " + PolicyLernField.ToStringDump(), true);
            try
            {
                PolicyLearnedField.AddUpdateLearned(PolicyLernField, strProductType);
                ActionLogger.Logger.WriteLog("AddUpdatePolicyLearnedField success ", true);
                jres = new JSONResponse(string.Format("Policy learned fields saved successfully"), Convert.ToInt16(ResponseCodes.Success), "");
            }
            catch (Exception ex)
            {
                jres = new JSONResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                ActionLogger.Logger.WriteLog("AddUpdatePolicyLearnedField failure: " + ex.Message, true);
            }
            return jres;
        }

        public JSONResponse DeletePolicyLearnedFieldService(PolicyLearnedFieldData PolicyLernField)
        {
           JSONResponse jres = null;
           ActionLogger.Logger.WriteLog("DeletePolicyLearnedFieldService request: " + PolicyLernField.ToStringDump(), true);
            try
            {
                PolicyLearnedField.Delete(PolicyLernField);
                ActionLogger.Logger.WriteLog("DeletePolicyLearnedFieldService success ", true);
                jres = new JSONResponse(string.Format("Policy learned fields deleted successfully"), Convert.ToInt16(ResponseCodes.Success), "");
            }
            catch (Exception ex)
            {
                jres = new JSONResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                ActionLogger.Logger.WriteLog("DeletePolicyLearnedFieldService failure: " + ex.Message, true);
            }
            return jres;
        }

        public PolicyBoolResponse IsValidPolicyLearnedFieldService(PolicyLearnedFieldData PolicyLernField)
        {
            PolicyBoolResponse jres = null;
            ActionLogger.Logger.WriteLog("IsValidPolicyLearnedFieldService request: " + PolicyLernField.ToStringDump(), true);
            try
            {
                bool isvalid = PolicyLearnedField.IsValid(PolicyLernField);
                ActionLogger.Logger.WriteLog("IsValidPolicyLearnedFieldService success ", true);
                jres = new PolicyBoolResponse(string.Format("Policy learned fields deleted successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                jres.BoolFlag = isvalid;
            }
            catch (Exception ex)
            {
                jres = new PolicyBoolResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                ActionLogger.Logger.WriteLog("IsValidPolicyLearnedFieldService failure: " + ex.Message, true);
            }
            return jres;
        }


        public PolicyLearnedResponse GetPolicyLearnedFieldsPolicyWiseService(Guid PolicyId)
        {
            PolicyLearnedResponse jres = null;
            ActionLogger.Logger.WriteLog("GetPolicyLearnedFieldsPolicyWiseService request: " + PolicyId, true);
            try
            {
                PolicyLearnedFieldData obj = PolicyLearnedField.GetPolicyLearnedFieldsPolicyWise(PolicyId);
                ActionLogger.Logger.WriteLog("GetPolicyLearnedFieldsPolicyWiseService success ", true);
                jres = new PolicyLearnedResponse(string.Format("Policy learned fields found successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                jres.PolicyLearned = obj;
            }
            catch (Exception ex)
            {
                jres = new PolicyLearnedResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                ActionLogger.Logger.WriteLog("GetPolicyLearnedFieldsPolicyWiseService failure: " + ex.Message, true);
            }
            return jres;
        }

        public PolicyStringResponse GetPolicyLearnedCoverageNickNameService(Guid PolicyId, Guid PayorID, Guid CarrierID, Guid CoverageID)
        {
            PolicyStringResponse jres = null;
            ActionLogger.Logger.WriteLog("GetPolicyLearnedCoverageNickNameService request: " + PolicyId, true);
            try
            {
                string s = PolicyLearnedField.GetPolicyLearnedCoverageNickName(PolicyId, PayorID, CarrierID, CoverageID);
                ActionLogger.Logger.WriteLog("GetPolicyLearnedCoverageNickNameService success ", true);
                jres = new PolicyStringResponse(string.Format("Policy learned coverage found successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                jres.StringValue = s;
            }
            catch (Exception ex)
            {
                jres = new PolicyStringResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                ActionLogger.Logger.WriteLog("GetPolicyLearnedCoverageNickNameService failure: " + ex.Message, true);
            }
            return jres;
        }

        public JSONResponse AddUpdateHistoryLearnedService(Guid PolicyId)
        {
            JSONResponse jres = null;
            ActionLogger.Logger.WriteLog("AddUpdateHistoryLearnedService request: " + PolicyId, true);
            try
            {
                PolicyLearnedField.AddUpdateHistoryLearned(PolicyId);
                ActionLogger.Logger.WriteLog("AddUpdateHistoryLearnedService success ", true);
                jres = new JSONResponse(string.Format("Policy learned fields history saved successfully"), Convert.ToInt16(ResponseCodes.Success), "");
            }
            catch (Exception ex)
            {
                jres = new JSONResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                ActionLogger.Logger.WriteLog("AddUpdateHistoryLearnedService failure: " + ex.Message, true);
            }
            return jres;
        }

        public PolicyLearnedResponse GetPolicyLearnedFieldsHistoryPolicyWiseService(Guid PolicyId)
        {
            PolicyLearnedResponse jres = null;
            ActionLogger.Logger.WriteLog("GetPolicyLearnedFieldsHistoryPolicyWiseService request: " + PolicyId, true);
            try
            {
                PolicyLearnedFieldData obj = PolicyLearnedField.GetPolicyLearnedFieldsHistoryPolicyWise(PolicyId);
                ActionLogger.Logger.WriteLog("GetPolicyLearnedFieldsHistoryPolicyWiseService success ", true);
                jres = new PolicyLearnedResponse(string.Format("Policy learned fields history found successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                jres.PolicyLearned = obj;
            }
            catch (Exception ex)
            {
                jres = new PolicyLearnedResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                ActionLogger.Logger.WriteLog("GetPolicyLearnedFieldsHistoryPolicyWiseService failure: " + ex.Message, true);
            }
            return jres;
        }

        public JSONResponse DeleteLearnedHistoryService(Guid PolicyId)
        {
            JSONResponse jres = null;
            ActionLogger.Logger.WriteLog("DeleteLearnedHistoryService request: " + PolicyId, true);
            try
            {
                PolicyLearnedField.DeleteLearnedHistory(PolicyId);
                ActionLogger.Logger.WriteLog("DeleteLearnedHistoryService success ", true);
                jres = new JSONResponse(string.Format("Policy learned fields history deleted successfully"), Convert.ToInt16(ResponseCodes.Success), "");
            }
            catch (Exception ex)
            {
                jres = new JSONResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                ActionLogger.Logger.WriteLog("DeleteLearnedHistoryService failure: " + ex.Message, true);
            }
            return jres;
        }

        public PolicyDateResponse GetPolicyLearnedFieldAutoTerminationDateService(Guid PolicyId)
        {
            PolicyDateResponse jres = null;
            ActionLogger.Logger.WriteLog("GetPolicyLearnedFieldAutoTerminationDateService request: " + PolicyId, true);
            try
            {
                DateTime? dt = PolicyLearnedField.GetPolicyLearnedFieldAutoTerminationDate(PolicyId);
                ActionLogger.Logger.WriteLog("GetPolicyLearnedFieldAutoTerminationDateService success ", true);
                jres = new PolicyDateResponse(string.Format("Policy auto termination date found successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                jres.PolicyDate = dt==null ? string.Empty :  dt.ToString();
            }
            catch (Exception ex)
            {
                jres = new PolicyDateResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                ActionLogger.Logger.WriteLog("GetPolicyLearnedFieldAutoTerminationDateService failure: " + ex.Message, true);
            }
            return jres;
        }

    }
}