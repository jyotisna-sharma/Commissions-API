using MyAgencyVault.BusinessLibrary;
using MyAgencyVault.BusinessLibrary.BusinessObjects;
using MyAgencyVault.BusinessLibrary.Masters;
using MyAgencyVault.WcfService.Library.Response;
using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace MyAgencyVault.WcfService
{
    [ServiceContract]
    interface IPayorToolService
    {
        #region 
        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        PayorTemplateListResponse GetPayorToolTemplateService(Guid payorId, ListParams listParams, string selectedTabName);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        PayorToolObjectResponse GetPayorTemplateDataService(Guid payorId, Guid? templateId);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        PayorToolAvailablelFieldTypeListResponse GetFieldListService();

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        PolicyPMCResponse AddUpdatePayorToolAvailablelFieldTypeService(PayorToolAvailablelFieldType PyrToolAvalableFields);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        PolicyBoolResponse DeletePayorToolAvailablelFieldTypeService(PayorToolAvailablelFieldType PyrToolAvalableFields);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        AddUpdatePayorToolFieldListResponse AddUpdatePayorToolService(PayorTool payorTool);


        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        PolicyBoolResponse AddTemplateService(Guid templateId, string templateName, Guid payorId, bool isForceImport, bool isPayorImportTemplate);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        JSONResponse DeleteTemplateService(Guid payorId, Guid? templateId);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        PolicyBoolResponse DulicatePayorTemplateService(Guid sourcePayorId, Guid? sourceTemplateId, Guid destinationPayorId, Guid? destinationTemplateId);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        PolicyBoolResponse IfPayorTemplateHasValue(Guid sourcePayorId, Guid? sourceTemplateId);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        PayorTestFormulaResponse GetTestFormulaResult(string expression);
        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        JSONResponse DeleteImportPayorTemplateService(Guid templateId);
        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        JSONResponse DulicateImportPayorTemplateService(Guid sourcePayorId, Guid? sourceTemplateId, Guid destinationPayorId, Guid? destinationTemplateId);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        ImportToolPhraseListResponse GetImportPayorTemplatePhraseService(Guid payorId, Guid templateId, ListParams listParams);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        JSONResponse SaveImportPayorPhraseService(ImportToolPayorPhrase objImportToolPayorPhrase, bool isForceToAdd);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        JSONResponse DeleteImportPayorPhraseService(int phraseId);


        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        ImportToolStatementListResponse GetImportToolStatementService(Guid payorID, Guid? templateID);


        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        JSONResponse SaveImportToolStatementService(List<ImportToolStatementDataSettings> data);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        JSONResponse SaveImportToolTemplateDetailsService(Guid payorId, Guid templateId, ImportToolTemplateDetails templateData);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        PayorToolAvailablelFieldTypeListResponse GetImportToolAvilableFieldService();

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        AddUpdateImportToolFieldListResponse AddUpdateImportPaymentDataService(List<ImportToolPaymentDataFieldsSettings> importToolPaymentDataList);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        ImportToolPaymentStngsListResponse GetImportPaymentDataDetailsSrvice(Guid payorId, Guid templateId);

        #endregion
    }
    public partial class MavService : IPayorToolService
    {
        public AddUpdatePayorToolFieldListResponse AddUpdatePayorToolService(PayorTool payorTool)
        {
            ActionLogger.Logger.WriteLog("AddUpdatePayorToolService request: PyorTool" + payorTool.ToStringDump(), true);
            AddUpdatePayorToolFieldListResponse jres = null;
            try
            {
                if (payorTool.TemplateID == Guid.Empty)
                {
                    payorTool.TemplateID = null;
                }
                List<ImportToolInValidFieldList> inValidFormulaExpressionList = PayorTool.ValidateFormulaExpression(null, payorTool.ToolFields);
                if (inValidFormulaExpressionList.Count == 0)
                {
                    PayorTool.AddUpdate(payorTool);
                }
                jres = new AddUpdatePayorToolFieldListResponse("Payor tool data saved successfully", (int)ResponseCodes.Success, "");
                jres.InValidFormulaExpressionList = inValidFormulaExpressionList;
                ActionLogger.Logger.WriteLog("AddUpdatePayorToolMgrService success", true);
            }
            catch (Exception ex)
            {
                jres = new AddUpdatePayorToolFieldListResponse("", (int)ResponseCodes.Failure, ex.Message);
                ActionLogger.Logger.WriteLog("AddUpdatePayorToolMgrService failure" + payorTool.PayorID + ex.Message, true);
            }
            return jres;
        }
        // ***********************************************************************************************************************************
        /// <summary>
        /// CreatedBy:Acmeminds
        /// CreatedOn:31-Jan-2019
        /// Purpose:Getting Template list based on Payor Selection
        /// </summary>
        /// <param name="payorId"></param>
        /// <returns></returns>
        public PayorTemplateListResponse GetPayorToolTemplateService(Guid payorId, ListParams listParams, string selectedTabName)
        {
            ActionLogger.Logger.WriteLog("GetPayorToolTemplateService: Processing begins with payorId: " + payorId, true);
            PayorTemplateListResponse jres = null;
            try
            {
                List<Tempalate> lst = PayorTool.GetPayorToolTemplate(payorId, listParams, selectedTabName, out int totalRecord, out string payorName);
                if (lst != null && lst.Count > 0)
                {
                    jres = new PayorTemplateListResponse("Payors template list found successfully", (int)ResponseCodes.Success, "");
                    jres.TotalRecords = lst;
                    jres.TotalLength = totalRecord;
                    jres.PayorName = payorName;
                }
                else
                {
                    jres = new PayorTemplateListResponse("No payor templates found", (int)ResponseCodes.RecordNotFound, "");
                }
                ActionLogger.Logger.WriteLog("GetPayorToolTemplateService success:", true);
            }
            catch (Exception ex)
            {
                jres = new PayorTemplateListResponse("", (int)ResponseCodes.Failure, "Error getting templates list" + ex.Message);
                ActionLogger.Logger.WriteLog("GetPayorToolTemplateService:Exception occurs while fetching details payorId" + payorId + " " + ex.Message, true);
            }
            return jres;
        }

        /// <summary>
        /// CreatedOn:31-Jan-2019
        /// CreatedBy:Acmeminds
        /// Purpose:Getting details of payortemplate
        /// </summary>
        /// <param name="payorId"></param>
        /// <param name="templateId"></param>
        /// <returns></returns>
        public PayorToolObjectResponse GetPayorTemplateDataService(Guid payorId, Guid? templateId)
        {
            ActionLogger.Logger.WriteLog("GetPayorTemplateDataService: processing begins with payorId" + payorId + " " + "templateId" + templateId, true);
            PayorToolObjectResponse jres = null;
            try
            {
                if (templateId == Guid.Empty)
                {
                    templateId = null;
                }
                PayorTool obj = PayorTool.GetPayorTemplateDetails(payorId, templateId);
                jres = new PayorToolObjectResponse("Payor tool data found successfully", (int)ResponseCodes.Success, "");
                jres.PayorToolObject = obj;
                ActionLogger.Logger.WriteLog("GetPayorTemplateDataService: data found successfully", true);
            }
            catch (Exception ex)
            {
                jres = new PayorToolObjectResponse("", (int)ResponseCodes.Failure, ex.Message);
                ActionLogger.Logger.WriteLog("GetPayorTemplateDataService:Exception occurs while fetching data with payorId" + payorId + " " + ex.Message, true);
            }
            return jres;
        }

        /// <summary>
        /// CreatedBy:Acmeminds
        /// CreatedOn:04-Feb-2019
        /// Purpose:Getting a List of Fields
        /// </summary
        /// <returns></returns>
        public PayorToolAvailablelFieldTypeListResponse GetFieldListService()
        {
            PayorToolAvailablelFieldTypeListResponse jres = null;
            ActionLogger.Logger.WriteLog("GetFieldListService:processing begins", true);
            try
            {
                List<PayorToolAvailablelFieldType> lst = PayorToolAvailablelFieldType.GetFieldList();
                jres = new PayorToolAvailablelFieldTypeListResponse(string.Format("Data found successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                jres.PayorToolFieldTypeList = lst;
                ActionLogger.Logger.WriteLog("GetFieldListService success ", true);

            }
            catch (Exception ex)
            {
                jres = new PayorToolAvailablelFieldTypeListResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                ActionLogger.Logger.WriteLog("GetFieldListService:Exception occurs while fetching list from server" + ex.Message, true);
            }
            return jres;
        }
        /// <summary>
        /// CreatedBy:Ankit Khandelwal
        /// CreatedOn:12-Feb-2020
        /// Purpose:Add the payor tool template
        /// </summary>
        /// <param name="PyrToolAvalableFields"></param>
        /// <returns></returns>
        public PolicyBoolResponse AddTemplateService(Guid templateId, string templateName, Guid payorId, bool isForceImport, bool isPayorImportTemplate)
        {
            ActionLogger.Logger.WriteLog("AddUpdatePayorToolTemplateService: Processing begins with payorId" + payorId, true);
            PolicyBoolResponse jres = null;
            try
            {
                if (isPayorImportTemplate == true)
                {
                    PayorTool.AddUpdateImportToolPayorTemplate(templateId, templateName, payorId, isForceImport, out bool isImportPayorTemplateExist);
                    if (!isImportPayorTemplateExist)
                    {
                        jres = new PolicyBoolResponse("Payor tool template saved successfully", (int)ResponseCodes.Success, "");
                    }
                    else
                    {
                        jres = new PolicyBoolResponse("Template already exist with same name", (int)ResponseCodes.RecordAlreadyExist, "");
                    }
                }
                else
                {
                    PayorTool.AddUpdatePayorToolTemplate(templateId, templateName, payorId, out bool isTemplateExist);
                    if (!isTemplateExist)
                    {
                        jres = new PolicyBoolResponse("Payor tool template saved successfully", (int)ResponseCodes.Success, "");
                    }
                    else
                    {
                        jres = new PolicyBoolResponse("Template already exist with same name", (int)ResponseCodes.RecordAlreadyExist, "");
                    }
                }


            }
            catch (Exception ex)
            {
                jres = new PolicyBoolResponse(ex.Message, (int)ResponseCodes.Failure, ex.Message);
                ActionLogger.Logger.WriteLog("AddUpdatePayorToolTemplateService:Exception occurs while processing" + "payorId" + payorId + ex.Message, true);
            }
            return jres;
        }
        /// <summary>
        /// CreatedBy:Acmeminds
        /// CreatedOn:
        /// Purpose:Add update payor tool fields
        /// </summary>
        /// <param name="PyrToolAvalableFields"></param>
        /// <returns></returns>
        public PolicyPMCResponse AddUpdatePayorToolAvailablelFieldTypeService(PayorToolAvailablelFieldType PyrToolAvalableFields)
        {
            PolicyPMCResponse jres = null;
            ActionLogger.Logger.WriteLog("AddUpdatePayorToolAvailablelFieldTypeService:Processing begins with PayortoolDetails" + PyrToolAvalableFields.ToStringDump(), true);
            try
            {
                int result = PyrToolAvalableFields.AddUpdate();
                if (result == -1)
                {
                    jres = new PolicyPMCResponse(string.Format("Field name'" + PyrToolAvalableFields.FieldName + "' already exists"), Convert.ToInt16(ResponseCodes.RecordAlreadyExist), "");
                }
                else
                {
                    jres = new PolicyPMCResponse(string.Format("Data saved successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                    ActionLogger.Logger.WriteLog("AddUpdatePayorToolAvailablelFieldTypeService success ", true);
                }
                jres.NumberValue = result;

            }
            catch (Exception ex)
            {
                jres = new PolicyPMCResponse(ex.Message, Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                ActionLogger.Logger.WriteLog("AddUpdatePayorToolAvailablelFieldTypeService:" + ex.Message, true);
            }
            return jres;
        }
        /// <summary>
        /// CreatedBy:Acmeminds
        /// CreatedOn:
        /// Purpose:Delete Payor tool fields
        /// </summary>
        /// <param name="PyrToolAvalableFields"></param>
        /// <returns></returns>
        public PolicyBoolResponse DeletePayorToolAvailablelFieldTypeService(PayorToolAvailablelFieldType PyrToolAvalableFields)
        {
            PolicyBoolResponse jres = null;
            ActionLogger.Logger.WriteLog("DeletePayorToolAvailablelFieldTypeService request: object- " + PyrToolAvalableFields.ToStringDump(), true);
            try
            {
                bool result = PyrToolAvalableFields.Delete();
                jres = new PolicyBoolResponse(string.Format("Data deleted successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                jres.BoolFlag = result;
                ActionLogger.Logger.WriteLog("DeletePayorToolAvailablelFieldTypeService success ", true);
            }
            catch (Exception ex)
            {
                jres = new PolicyBoolResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                ActionLogger.Logger.WriteLog("DeletePayorToolAvailablelFieldTypeService failure" + ex.Message, true);
            }
            return jres;
        }
        /// <summary>
        /// CreatedBy:Ankit Kahndelwal
        /// CreatedOn:Feb-19-2020
        /// Purpose: Delete a payor template 
        /// </summary>
        /// <param name="payorId"></param>
        /// <param name="templateId"></param>
        /// <returns></returns>
        public JSONResponse DeleteTemplateService(Guid payorId, Guid? templateId)
        {
            ActionLogger.Logger.WriteLog("DeleteTemplateService: Processing begins with payorId" + payorId + ", TemplateID: " + templateId, true);
            JSONResponse jres = null;
            try
            {
                PayorTool.DeleteTemplate(payorId, templateId);
                jres = new JSONResponse("Payor tool data deleted successfully", (int)ResponseCodes.Success, "");
                ActionLogger.Logger.WriteLog("DeletePayotTemplateService success", true);
            }
            catch (Exception ex)
            {
                jres = new JSONResponse("", (int)ResponseCodes.Failure, ex.Message);
                ActionLogger.Logger.WriteLog("DeleteTemplateService:Exception occurs while  delete template templateId" + templateId + ex.Message, true);
            }
            return jres;

        }
        /// <summary>
        /// Created by:Jyotisna
        /// </summary>
        /// <param name="sourcePayorId"></param>
        /// <param name="sourceTemplateId"></param>
        /// <returns></returns>
        public PolicyBoolResponse IfPayorTemplateHasValue(Guid sourcePayorId, Guid? sourceTemplateId)
        {
            ActionLogger.Logger.WriteLog("IfPayorTemplateHasValue:Processing begins with sourcePayorId" + sourcePayorId + "sourceTemplateId:" + sourceTemplateId, true);
            PolicyBoolResponse jres = null;
            try
            {
                bool flag = PayorTool.CheckIfTemplateHasFields(sourcePayorId, sourceTemplateId);
                ActionLogger.Logger.WriteLog("IfPayorTemplateHasValue: success - " + flag, true);
                jres = new PolicyBoolResponse("Payor template checked successfully", (int)ResponseCodes.Success, "");
                jres.BoolFlag = flag;
            }
            catch (Exception ex)
            {
                jres = new PolicyBoolResponse("", (int)ResponseCodes.Failure, ex.Message);
                ActionLogger.Logger.WriteLog("IfPayorTemplateHasValue:Exception occurs while processing with:" + sourcePayorId + " " + ex.Message, true);
            }
            return jres;
        }
        /// <summary>
        ///Createdby:Jyotisna
        /// Puprpose:Getting result of test Formula
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public PayorTestFormulaResponse GetTestFormulaResult(string expression)
        {
            ActionLogger.Logger.WriteLog("GetTestFormulaResult:Processing begins with expression" + expression, true);
            PayorTestFormulaResponse jres = null;
            try
            {
                string result = PayorTool.GetTestFormulaResult(expression, out bool isFormulaExpressionValid);
                ActionLogger.Logger.WriteLog("GetTestFormulaResult: success - " + result, true);

                jres = new PayorTestFormulaResponse("Formula result found successfully", (int)ResponseCodes.Success, "");
                jres.Result = result;
                jres.IsResultValid = isFormulaExpressionValid;
            }
            catch (Exception ex)
            {
                jres = new PayorTestFormulaResponse("", (int)ResponseCodes.Failure, ex.Message);
                ActionLogger.Logger.WriteLog("GetTestFormulaResult:Exception ex: " + ex.Message, true);
            }
            return jres;
        }
        //######################################################################################################################################

        /// <summary>
        /// Createdby:Jyotisna
        /// CreatedFor:Getting list of statement data 
        /// </summary>
        /// <param name="payorID"></param>
        /// <param name="templateID"></param>
        /// <returns></returns>
        public ImportToolStatementListResponse GetImportToolStatementService(Guid payorID, Guid? templateID)
        {
            ActionLogger.Logger.WriteLog("GetImportToolStatementService:Request-" + payorID + ", templateID - " + templateID, true);
            ImportToolStatementListResponse jres = null;
            try
            {
                List<ImportToolStatementDataSettings> result = PayorTemplate.GetStatementDataList(payorID, templateID);
                ActionLogger.Logger.WriteLog("GetImportToolStatementService: success - ", true);
                if (result != null && result.Count > 0)
                {
                    jres = new ImportToolStatementListResponse("Statements data found successfully", (int)ResponseCodes.Success, "");
                    jres.TotalRecords = result;
                    jres.TotalLength = result.Count;
                    jres.EndDataList = PayorToolAvailablelFieldType.GetEndDataList();
                }
                else
                {
                    jres = new ImportToolStatementListResponse("Statements data not found", (int)ResponseCodes.RecordNotFound, "");
                }
            }
            catch (Exception ex)
            {
                jres = new ImportToolStatementListResponse("", (int)ResponseCodes.Failure, ex.Message);
                ActionLogger.Logger.WriteLog("GetImportToolStatementService:Exception ex: " + ex.Message, true);
            }
            return jres;
        }
        /// <summary>
        /// Createdby:Jyotisna 
        /// CreatedFor:Save the Import tool statement Data
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public JSONResponse SaveImportToolStatementService(List<ImportToolStatementDataSettings> data)
        {
            ActionLogger.Logger.WriteLog("SaveImportToolStatementService:Request-" + data.ToString(), true);
            JSONResponse jres = null;
            try
            {
                PayorTemplate.SaveImportToolStatementData(data);
                ActionLogger.Logger.WriteLog("SaveImportToolStatementService: success - ", true);
                jres = new JSONResponse("Statements data saved successfully", (int)ResponseCodes.Success, "");
            }
            catch (Exception ex)
            {
                jres = new JSONResponse("", (int)ResponseCodes.Failure, ex.Message);
                ActionLogger.Logger.WriteLog("SaveImportToolStatementService:Exception ex: " + ex.Message, true);
            }
            return jres;
        }
        /// <summary>
        /// Createdby:Ankit khandelwal
        /// CreatedFor :
        /// </summary>
        /// <param name="payorId"></param>
        /// <param name="templateId"></param>
        /// <param name="listParams"></param>
        /// <returns></returns>
        public ImportToolPhraseListResponse GetImportPayorTemplatePhraseService(Guid payorId, Guid templateId, ListParams listParams)
        {
            ImportToolPhraseListResponse jres = null;
            ActionLogger.Logger.WriteLog("GetImportPayorTemplatePhraseService:processing start with payorId:" + payorId, true);
            try
            {
                List<ImportToolPayorPhrase> lst = PayorTool.GetImportPayorTemplatePhrase(payorId, templateId, listParams, out int totalRecords, out ImportToolTemplateDetails templatedetails);
                jres = new ImportToolPhraseListResponse(string.Format("Payor template phrases found successfully"), Convert.ToInt16(ResponseCodes.Success), "");

                ActionLogger.Logger.WriteLog("GetImportPayorTemplatePhraseService:success", true);
                jres.TotalRecords = lst;
                jres.TotalLength = totalRecords;
                jres.ImportPayorTemplateDetails = templatedetails;
            }
            catch (Exception ex)
            {
                jres = new ImportToolPhraseListResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                ActionLogger.Logger.WriteLog("GetImportPayorTemplatePhraseService:Exception occurs while processing templateId" + templateId + ex.Message, true);
            }
            return jres;
        }
        /// <summary>
        /// CreatedBy:Acmeminds
        /// CreatedOn:31-Jan-2019
        /// Purpose:Getting Template list based on Payor Selection
        /// </summary>
        /// <param name="payorId"></param>
        /// <returns></returns>SaveImportPayorPhraseService
        public JSONResponse SaveImportToolTemplateDetailsService(Guid payorId, Guid templateId, ImportToolTemplateDetails templateData)
        {
            ActionLogger.Logger.WriteLog("SaveImportToolTemplateDetailsService: Processing begins with payorId: " + payorId, true);
            JSONResponse jres = null;
            try
            {
                PayorTool.SaveImportToolTemplateDetails(payorId, templateId, templateData);
                jres = new JSONResponse("Payors template list found successfully", (int)ResponseCodes.Success, "");
            }
            catch (Exception ex)
            {
                jres = new JSONResponse("", (int)ResponseCodes.Failure, "Error getting templates list" + ex.Message);
                ActionLogger.Logger.WriteLog("SaveImportToolTemplateDetailsService:Exception occurs while fetching details payorId" + payorId + " " + ex.Message, true);
            }
            return jres;
        }

        /// <summary>
        /// Createdby:Ankit Khandelwal
        /// CreatedOn:March23,2020
        /// Purpose:delete Phrase from phrase list
        /// </summary>
        /// <param name="intId"></param>
        /// <returns></returns>
        public JSONResponse DeleteImportPayorPhraseService(int phraseId)
        {
            JSONResponse jres = null;
            ActionLogger.Logger.WriteLog("DeleteImportPayorPhraseService:processing begins with phraseId" + phraseId, true);
            try
            {
                PayorTool.DeleteImportTemplatePhrase(phraseId);
                jres = new JSONResponse(string.Format("Phrase data deleted successfully"), Convert.ToInt16(ResponseCodes.Success), "");
            }
            catch (Exception ex)
            {
                jres = new JSONResponse("Exception occurs while deleting", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                ActionLogger.Logger.WriteLog("DeleteImportPayorPhraseService:Exception occurs with phraseId" + phraseId + " " + ex.Message, true);
            }
            return jres;
        }

        /// <summary>
        /// CreatedBy:Ankit Khandelwal
        /// Createdon:23-March-2020
        /// Purpose:Save the payorPhrase details
        /// </summary>
        /// <param name="objImportToolPayorPhrase"></param>
        /// <param name="isForceToAdd"></param>
        /// <returns></returns>
        public JSONResponse SaveImportPayorPhraseService(ImportToolPayorPhrase objImportToolPayorPhrase, bool isForceToAdd)
        {
            JSONResponse jres = null;
            ActionLogger.Logger.WriteLog("SaveImportPayorPhraseService: processing starts with" + objImportToolPayorPhrase.ToStringDump(), true);
            try
            {
                bool isPhraseExist = PayorTool.AddUpdateImportToolPayorPhrase(objImportToolPayorPhrase, isForceToAdd);

                if (isPhraseExist == true)
                {
                    jres = new JSONResponse(string.Format("Payor phrase already exists"), Convert.ToInt16(ResponseCodes.RecordAlreadyExist), "");
                }
                else
                {
                    jres = new JSONResponse(string.Format("Payor phrase saved successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                }

            }
            catch (Exception ex)
            {
                jres = new JSONResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                ActionLogger.Logger.WriteLog("SaveImportPayorPhraseService:Exception occurs while fetching details:" + objImportToolPayorPhrase.PayorPhrases +ex.Message, true);
            }
            return jres;
        }

        /// <summary>
        /// CreatedBy:Ankit Kahndelwal
        /// CreatedOn:Feb-19-2020
        /// Purpose: Copy a duplicate template name
        /// </summary>
        /// <param name="sourcePayorId"></param>
        /// <param name="sourceTemplateId"></param>
        /// <param name="destinationPayorId"></param>
        /// <param name="destinationTemplateId"></param>
        /// <returns></returns>
        public PolicyBoolResponse DulicatePayorTemplateService(Guid sourcePayorId, Guid? sourceTemplateId, Guid destinationPayorId, Guid? destinationTemplateId)
        {
            ActionLogger.Logger.WriteLog("DulicatePayorTemplateService:Processing begins with sourcePayorId" + sourcePayorId + "sourceTemplateId:" + sourceTemplateId + "destinationPayorId:" + destinationPayorId + "destinationTemplateId:" + destinationTemplateId, true);
            PolicyBoolResponse jres = null;
            try
            {
                bool flag = PayorTool.UpdateDulicatePayorTool(sourcePayorId, sourceTemplateId, destinationPayorId, destinationTemplateId);
                if (flag)
                {
                    jres = new PolicyBoolResponse("Payor tool details saved successfully", (int)ResponseCodes.Success, "");
                }
                else
                {
                    jres = new PolicyBoolResponse("", (int)ResponseCodes.Failure, "Payor tool details could not be saved.");
                }
                jres.BoolFlag = flag;
                ActionLogger.Logger.WriteLog("DulicatePayorTemplateService:processing success", true);
            }
            catch (Exception ex)
            {
                jres = new PolicyBoolResponse("", (int)ResponseCodes.Failure, ex.Message);
                ActionLogger.Logger.WriteLog("DulicatePayorTemplateService:Exception occurs while fetching details sourcePayorId:" + sourcePayorId + "sourceTemplateId:" + sourceTemplateId + "destinationPayorId:" + destinationPayorId + "destinationTemplateId:" + destinationTemplateId + " " + ex.Message, true);
            }
            return jres;
        }

        /// <summary>
        /// CreatedBy:Ankit Khandelwal
        /// CreatedOn:Feb-19-2020
        /// Purpose: Copy a duplicate Import payor template 
        /// </summary>
        /// <param name="sourcePayorId"></param>
        /// <param name="sourceTemplateId"></param>
        /// <param name="destinationPayorId"></param>
        /// <param name="destinationTemplateId"></param>
        /// <returns></returns>
        public JSONResponse DulicateImportPayorTemplateService(Guid sourcePayorId, Guid? sourceTemplateId, Guid destinationPayorId, Guid? destinationTemplateId)
        {
            ActionLogger.Logger.WriteLog("DulicateImportPayorTemplateService:Processing begins with sourcePayorId" + sourcePayorId + "sourceTemplateId:" + sourceTemplateId + "destinationPayorId:" + destinationPayorId + "destinationTemplateId:" + destinationTemplateId, true);
            JSONResponse jres = null;
            try
            {
                PayorTool.DulicateImportPayorToolTemplate(sourcePayorId, sourceTemplateId, destinationPayorId, destinationTemplateId);
                jres = new JSONResponse("Payor tool details saved successfully", (int)ResponseCodes.Success, "");
            }
            catch (Exception ex)
            {
                jres = new JSONResponse("", (int)ResponseCodes.Failure, ex.Message);
                ActionLogger.Logger.WriteLog("DulicateImportPayorTemplateService:Exception occurs while fetching details sourcePayorId:" + sourcePayorId + "sourceTemplateId:" + sourceTemplateId + "destinationPayorId:" + destinationPayorId + "destinationTemplateId:" + destinationTemplateId + " " + ex.Message, true);
            }
            return jres;
        }


        /// <summary>
        /// CreatedBy:AnkitKhandelwal
        /// CreatedOn:18-march-2020
        /// Purpose:To delete a ImportPayorTemplate
        /// </summary>
        /// <param name="templateId"></param>
        /// <returns></returns>
        public JSONResponse DeleteImportPayorTemplateService(Guid templateId)
        {
            JSONResponse jres = null;
            ActionLogger.Logger.WriteLog("DeleteImportPayorTemplateService: processing begins:" + templateId, true);
            try
            {
                PayorTool.DeleteImportPayorTemplate(templateId);
                jres = new JSONResponse(string.Format("Template deleted successfully"), Convert.ToInt16(ResponseCodes.Success), "");

            }
            catch (Exception ex)
            {
                jres = new JSONResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                ActionLogger.Logger.WriteLog("DeleteImportPayorTemplateService:Exception occurs while fetch data" + templateId + " Exception:" + ex.Message, true);
            }
            return jres;
        }
        /// <summary>
        /// CreatedBy:Ankit Khandelwal
        /// CreatedOn:27-03-2020
        /// Purpose:Getting import tool payment data Available field list
        /// </summary>
        /// <returns></returns>
        public PayorToolAvailablelFieldTypeListResponse GetImportToolAvilableFieldService()
        {
            PayorToolAvailablelFieldTypeListResponse jres = null;
            ActionLogger.Logger.WriteLog("GetImportToolAvilableFieldService processing begins:", true);
            try
            {
                List<PayorToolAvailablelFieldType> lst = PayorTool.GetImportToolAvilableFieldList();
                jres = new PayorToolAvailablelFieldTypeListResponse(string.Format("Import tool available field list found successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                jres.PayorToolFieldTypeList = lst;
            }
            catch (Exception ex)
            {
                jres = new PayorToolAvailablelFieldTypeListResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                ActionLogger.Logger.WriteLog("GetImportToolAvilableFieldService:Exception occurs while fetching data" + ex.Message, true);

            }
            return jres;
        }
        ///// <summary>
        ///// ModifiedBy:Ankit Khandelwal
        ///// ModifiedOn:April02,2020
        ///// Purpose:Add update import tool Payment Data
        ///// </summary>
        ////  <param name="importToolPaymentDataList"></param>
        ///// <returns> </returns>
        public AddUpdateImportToolFieldListResponse AddUpdateImportPaymentDataService(List<ImportToolPaymentDataFieldsSettings> importToolPaymentDataList)
        {
            ActionLogger.Logger.WriteLog("AddUpdateImportPaymentDataService: processing begins with templateId:" + importToolPaymentDataList[0].TemplateID, true);
            AddUpdateImportToolFieldListResponse jres = null;
            try
            {
                List<ImportToolInValidFieldList> inValidFormulaExpressionList = PayorTool.ValidateFormulaExpression(importToolPaymentDataList, null);
                if (inValidFormulaExpressionList.Count == 0)
                {
                    PayorTool.AddUpdatePaymentDataFieldsSetting(importToolPaymentDataList);
                }
                jres = new AddUpdateImportToolFieldListResponse(string.Format("Payment data field settings saved successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                jres.InValidFormulaExpressionList = inValidFormulaExpressionList;
                ActionLogger.Logger.WriteLog("AddUpdateImportPaymentDataService: success ", true);
            }
            catch (Exception ex)
            {
                jres = new AddUpdateImportToolFieldListResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                ActionLogger.Logger.WriteLog("AddUpdateImportPaymentDataService: Exception occurs while processing with TemplateID" + importToolPaymentDataList[0].TemplateID + ex.Message, true);
            }
            return jres;
        }
        ///// <summary>
        ///// ModifiedBy:Ankit Khandelwal
        ///// ModifiedOn:April02,2020
        ///// Purpose:Get details of Payment Data Field List
        ///// </summary>
        ///// <param name="payorId"></param>
        ///// <param name="templateId"></param>
        ///// <returns> </returns>
        public ImportToolPaymentStngsListResponse GetImportPaymentDataDetailsSrvice(Guid payorId, Guid templateId)
        {
            ImportToolPaymentStngsListResponse jres = null;
            ActionLogger.Logger.WriteLog("GetImportPaymentDataDetailsSrvice:processing begins with:" + payorId + ", templateId: " + templateId, true);
            try
            {
                List<ImportToolPaymentDataFieldsSettings> lst = PayorTool.GetImportPaymentDataFieldDetails(payorId, templateId);

                jres = new ImportToolPaymentStngsListResponse(string.Format("Payment fields settings found successfully"), Convert.ToInt16(ResponseCodes.Success), "");

                jres.ImportToolPaymentSettingList = lst;
            }
            catch (Exception ex)
            {
                jres = new ImportToolPaymentStngsListResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                ActionLogger.Logger.WriteLog("GetImportPaymentDataDetailsSrvice:Exception occurs while processing" + ex.Message, true);
            }
            return jres;
        }
    }

}
