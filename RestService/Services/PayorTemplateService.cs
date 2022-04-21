using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyAgencyVault.BusinessLibrary;
using System.ServiceModel;
using System.ServiceModel.Web;
using MyAgencyVault.WcfService.Library.Response;


namespace MyAgencyVault.WcfService
{
    //[ServiceContract]
    //interface IPayorTemplateService
    //{
        //Found not in use
        //[OperationContract]
        //[WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        //JSONResponse AddUpdateTemplateService(PayorTemplate template);

        //Found not in use
        //[OperationContract]
        //[WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        //JSONResponse DeleteTemplateService(Guid PayorId);

         //Found not in use
        //  [OperationContract]
        //[WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        //JSONResponse DuplicateSelectedPaymentDataService(ImportToolSeletedPaymentData objImportToolSeletedPaymentData);


        //Blank implememtation found 
        //[OperationContract]
        //[WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        //PayorTemplateMainListResponse GetPayorTemplateListService();

        //[OperationContract]
        //[WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        //PayorTemplateResponse GetPayorTemplateService(Guid PayorId);

        //[OperationContract]
        //[WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        //PolicyBoolResponse AddUpdateImportToolPayorTemplateService(Guid SelectedPayorID, Guid SelectedtempID, string strTemName, bool isDeleted, bool isForceImport, string strCommandType);

        //[OperationContract]
        //[WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        //PolicyBoolResponse ValidateTemplateNameService(Guid SelectedPayorID, Guid SelectedTempID, string strTempName, bool isDeleted, bool isForceImport);

        //[OperationContract]
        //[WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        //PayorTemplateListResponse GetImportToolPayorTemplateService(Guid SelectedPayorID);

        //[OperationContract]
        //[WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        //PolicyBoolResponse DeleteImportToolPayorTemplateService(Tempalate SelectedPayortempalate);

        //[OperationContract]
        //[WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        //PolicyBoolResponse AddUpdateImportToolPayorPhraseService(ImportToolPayorPhrase objImportToolPayorPhrase);

        //[OperationContract]
        //[WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        //ImportToolPhraseListResponse CheckAvailabilityService(string strPhrase);

        //[OperationContract]
        //[WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        //PolicyBoolResponse SaveImportToolStmtSettingsSrvc(ImportToolStatementDataSettings objImportToolStatementDataSettings);

        //[OperationContract]
        //[WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        //ImportToolStmtListResponse GetALoadllImportToolStatementDataSettingsSrvc(Guid PayorID, Guid templateID);

        //[OperationContract]
        //[WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        //PolicyStringResponse ValidatePhraseAvailbilityService(string strPhrase);

        //[OperationContract]
        //[WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        //MaskFieldListResponse AllMaskTypeService();

        ////Add to fetch translator type
        //[OperationContract]
        //[WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        //TranslatorListResponse AllTranslatorTypeService();

        //[OperationContract]
        //[WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        //JSONResponse AddUpdatePaymentDataFieldsSettingSrvc(ImportToolPaymentDataFieldsSettings objImportToolPaymentDataFieldsSettings);

        //[OperationContract]
        //[WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        //ImportToolStmtListResponse LoadImportToolStatementDataSettingSrvc();

        //[OperationContract]
        //[WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        //JSONResponse AddUpdatePaymentDataService(ImportToolSeletedPaymentData objImportToolSeletedPaymentData);

        //[OperationContract]
        //[WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        //JSONResponse DeletePaymentDataService(ImportToolSeletedPaymentData objImportToolSeletedPaymentData);

        //[OperationContract]
        //[WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        //ImportToolPaymentListResponse LoadImportToolSeletedPaymentDataSrvc(Guid PayorID, Guid TemplateID);

        //[OperationContract]
        //[WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        //ImportToolPaymentStngsListResponse LoadPaymentDataFieldsSettingSrvc(Guid PayorID, Guid TemplateID);

      
        ////[OperationContract]
        ////[WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        ////ImportToolPhraseListResponse GetAllTemplatePhraseOnTemplateSrvc();

        //[OperationContract]
        //[WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        //PayorTemplateListResponse GetAllPayorTemplateService(Guid? SelectedPayorID);

        //[OperationContract]
        //[WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        //JSONResponse DeletePhraseService(int intID);

        //[OperationContract]
        //[WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        //JSONResponse UpdatePhraseService(int intID, string Phrase);


    //}

    //public partial class MavService : IPayorTemplateService
    //{
        //public ImportToolStmtListResponse LoadImportToolStatementDataSettingSrvc()
        //{
        //    ImportToolStmtListResponse jres = null;
        //    ActionLogger.Logger.WriteLog("LoadImportToolStatementDataSettingSrvc  request: ", true);
        //    try
        //    {
        //        PayorTemplate objPayortemplate = new PayorTemplate();
        //        List<ImportToolStatementDataSettings> lst = objPayortemplate.LoadImportToolStatementDataSetting();
           
        //        if (lst != null && lst.Count > 0)
        //        {
        //            jres = new ImportToolStmtListResponse(string.Format(" Import tool statement data settings  found successfully"), Convert.ToInt16(ResponseCodes.Success), "");
        //            ActionLogger.Logger.WriteLog("LoadImportToolStatementDataSettingSrvc success ", true);
        //            jres.StatementSettingsList = lst;
        //        }
        //        else
        //        {
        //            jres = new ImportToolStmtListResponse(string.Format("No import tool statement data settings  found "), Convert.ToInt16(ResponseCodes.RecordNotFound), "");
        //            ActionLogger.Logger.WriteLog("LoadImportToolStatementDataSettingSrvc 404 ", true);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        jres = new ImportToolStmtListResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
        //        ActionLogger.Logger.WriteLog("LoadImportToolStatementDataSettingSrvc failure ", true);
        //    }
        //    return jres;
        //}

        /* Not in use 
         * public JSONResponse AddUpdateTemplateService(PayorTemplate template)
         {
             JSONResponse jres = null;
             ActionLogger.Logger.WriteLog("AddUpdateTemplateService request: " + template.ToStringDump(), true);
             try
             {
                 template.AddUpdatePayorTemplate();
                 jres = new JSONResponse(string.Format("Payor template saved successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                 ActionLogger.Logger.WriteLog("AddUpdateTemplateService success ", true);
             }
             catch (Exception ex)
             {
                 jres = new JSONResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                 ActionLogger.Logger.WriteLog("AddUpdateTemplateService failure ", true);
             }
             return jres;
         }

         public JSONResponse DeleteTemplateService(Guid PayorId)
         {
             JSONResponse jres = null;
             ActionLogger.Logger.WriteLog("DeleteTemplateService request: " + PayorId, true);
             try
             {
                 PayorTemplate.DaletePayorTemplate(PayorId);
                 jres = new JSONResponse(string.Format("Payor template deleted successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                 ActionLogger.Logger.WriteLog("DeleteTemplateService success ", true);
             }
             catch (Exception ex)
             {
                 jres = new JSONResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                 ActionLogger.Logger.WriteLog("DeleteTemplateService failure ", true);
             }
             return jres;
         }

        public PayorTemplateMainListResponse GetPayorTemplateListService()
        {
            PayorTemplateMainListResponse jres = null;
            ActionLogger.Logger.WriteLog("GetPayorTemplatesListService  request: ", true);
            try
            {
                PayorTemplate objPayortemplate = new PayorTemplate();
                List<PayorTemplate> lst = PayorTemplate.getPayorTemplates();

                if (lst != null && lst.Count > 0)
                {
                    jres = new PayorTemplateMainListResponse(string.Format("Payor templates found successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                    ActionLogger.Logger.WriteLog("GetPayorTemplatesListService success ", true);
                    jres.PayorTemplateList = lst;
                }
                else
                {
                    jres = new PayorTemplateMainListResponse(string.Format("No payor templates found"), Convert.ToInt16(ResponseCodes.RecordNotFound), "");
                    ActionLogger.Logger.WriteLog("GetPayorTemplatesListService 404 ", true);
                }
            }
            catch (Exception ex)
            {
                jres = new PayorTemplateMainListResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                ActionLogger.Logger.WriteLog("GetPayorTemplatesListService failure ", true);
            }
            return jres;
        }*/

        //public PayorTemplateResponse GetPayorTemplateService(Guid PayorId)
        //{
        //    PayorTemplateResponse jres = null;
        //    ActionLogger.Logger.WriteLog("GetPayorTemplateService request: " + PayorId, true);
        //    try
        //    {
        //        PayorTemplate obj = PayorTemplate.getPayorTemplate(PayorId);
        //        if (obj != null)
        //        {
        //            jres = new PayorTemplateResponse(string.Format("Payor template found successfully"), Convert.ToInt16(ResponseCodes.Success), "");
        //            jres.PayorTemplateObject = obj;
        //            ActionLogger.Logger.WriteLog("GetPayorTemplateService success ", true);
        //        }
        //        else
        //        {
        //            jres = new PayorTemplateResponse(string.Format("Payor template not found"), Convert.ToInt16(ResponseCodes.RecordNotFound), "");
        //            ActionLogger.Logger.WriteLog("GetPayorTemplateService 404 ", true);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        jres = new PayorTemplateResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
        //        ActionLogger.Logger.WriteLog("GetPayorTemplateService failure ", true);
        //    }
        //    return jres;
        //}

        //public PolicyBoolResponse AddUpdateImportToolPayorTemplateService(Guid SelectedPayorID, Guid SelectedtempID, string strTemName, bool isDeleted, bool isForceImport, string strCommandType)
        //{
        //    PolicyBoolResponse jres = null;
        //    ActionLogger.Logger.WriteLog("AddUpdateImportToolPayorTemplateService request: " + SelectedPayorID + ", templateID: " + SelectedtempID, true);
        //    try
        //    {
        //        PayorTemplate objPayorTemplate = new PayorTemplate();
        //        bool res = objPayorTemplate.AddUpdateImportToolPayorTemplate(SelectedPayorID, SelectedtempID, strTemName, isDeleted, isForceImport, strCommandType);

        //        jres = new PolicyBoolResponse(string.Format("Payor template saved successfully"), Convert.ToInt16(ResponseCodes.Success), "");
        //        jres.BoolFlag = res;
        //        ActionLogger.Logger.WriteLog("AddUpdateImportToolPayorTemplateService success ", true);
        //    }
        //    catch (Exception ex)
        //    {
        //        jres = new PolicyBoolResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
        //        ActionLogger.Logger.WriteLog("AddUpdateImportToolPayorTemplateService failure ", true);
        //    }
        //    return jres;
        //}

        //public PolicyBoolResponse ValidateTemplateNameService(Guid SelectedPayorID, Guid SelectedTempID, string strTempName, bool isDeleted, bool isForceImport)
        //{
        //    PolicyBoolResponse jres = null;
        //    ActionLogger.Logger.WriteLog("AddUpdateImportToolPayorTemplateService request: " + SelectedPayorID + ", templateID: " + SelectedTempID, true);
        //    try
        //    {
        //        PayorTemplate objPayorTemplate = new PayorTemplate();
        //        bool res = objPayorTemplate.ValidateTemplateName(SelectedPayorID, SelectedTempID, strTempName, isDeleted, isForceImport);
        //        jres = new PolicyBoolResponse(string.Format("Template name validated successfully"), Convert.ToInt16(ResponseCodes.Success), "");
        //        jres.BoolFlag = res;
        //        ActionLogger.Logger.WriteLog("AddUpdateImportToolPayorTemplateService success ", true);
        //    }
        //    catch (Exception ex)
        //    {
        //        jres = new PolicyBoolResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
        //        ActionLogger.Logger.WriteLog("AddUpdateImportToolPayorTemplateService failure ", true);
        //    }
        //    return jres;
        //}

        //public PayorTemplateListResponse GetImportToolPayorTemplateService(Guid SelectedPayorID)
        //{
        //    PayorTemplateListResponse jres = null;
        //    ActionLogger.Logger.WriteLog("GetImportToolPayorTemplateService  request: " + SelectedPayorID, true);
        //    try
        //    {
        //        PayorTemplate objPayortemplate = new PayorTemplate();
        //        List<Tempalate> lst = objPayortemplate.GetImportToolPayorTemplate(SelectedPayorID);

        //        if (lst != null && lst.Count > 0)
        //        {
        //            jres = new PayorTemplateListResponse(string.Format("Payor templates found successfully"), Convert.ToInt16(ResponseCodes.Success), "");
        //            ActionLogger.Logger.WriteLog("GetImportToolPayorTemplateService success ", true);
        //            jres.TotalRecords = lst;
        //        }
        //        else
        //        {
        //            jres = new PayorTemplateListResponse(string.Format("No payor templates found"), Convert.ToInt16(ResponseCodes.RecordNotFound), "");
        //            ActionLogger.Logger.WriteLog("GetImportToolPayorTemplateService 404 ", true);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        jres = new PayorTemplateListResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
        //        ActionLogger.Logger.WriteLog("GetImportToolPayorTemplateService failure ", true);
        //    }
        //    return jres;
        //}

        //public PolicyBoolResponse DeleteImportToolPayorTemplateService(Tempalate SelectedPayortempalate)
        //{
        //    PolicyBoolResponse jres = null;
        //    ActionLogger.Logger.WriteLog("DeleteImportToolPayorTemplateService request: " + SelectedPayortempalate.ToStringDump(), true);
        //    try
        //    {
        //        PayorTemplate objPayorTemplate = new PayorTemplate();
        //        bool res = objPayorTemplate.deleteImportToolPayorTemplate(SelectedPayortempalate);
        //        jres = new PolicyBoolResponse(string.Format("Template deleted successfully"), Convert.ToInt16(ResponseCodes.Success), "");
        //        jres.BoolFlag = res;
        //        ActionLogger.Logger.WriteLog("DeleteImportToolPayorTemplateService success ", true);
        //    }
        //    catch (Exception ex)
        //    {
        //        jres = new PolicyBoolResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
        //        ActionLogger.Logger.WriteLog("DeleteImportToolPayorTemplateService failure ", true);
        //    }
        //    return jres;
        //}

        //public PolicyBoolResponse AddUpdateImportToolPayorPhraseService(ImportToolPayorPhrase objImportToolPayorPhrase)
        //{
        //    PolicyBoolResponse jres = null;
        //    ActionLogger.Logger.WriteLog("AddUpdateImportToolPayorPhraseService request: " + objImportToolPayorPhrase.ToStringDump(), true);
        //    try
        //    {
        //        PayorTemplate objPayorTemplate = new PayorTemplate();
        //        bool res = objPayorTemplate.AddUpdateImportToolPayorPhrase(objImportToolPayorPhrase);
        //        jres = new PolicyBoolResponse(string.Format("Payor phrase saved successfully"), Convert.ToInt16(ResponseCodes.Success), "");
        //        jres.BoolFlag = res;
        //        ActionLogger.Logger.WriteLog("AddUpdateImportToolPayorPhraseService success ", true);
        //    }
        //    catch (Exception ex)
        //    {
        //        jres = new PolicyBoolResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
        //        ActionLogger.Logger.WriteLog("AddUpdateImportToolPayorPhraseService failure ", true);
        //    }
        //    return jres;
        //}

        //public ImportToolPhraseListResponse CheckAvailabilityService(string strPhrase)
        //{
        //    ImportToolPhraseListResponse jres = null;
        //    ActionLogger.Logger.WriteLog("CheckAvailabilityService  request: " + strPhrase, true);
        //    try
        //    {
        //        PayorTemplate objPayortemplate = new PayorTemplate();
        //        List<ImportToolPayorPhrase> lst = objPayortemplate.CheckAvailability(strPhrase);

        //        if (lst != null && lst.Count > 0)
        //        {
        //            jres = new ImportToolPhraseListResponse(string.Format("Payor template phrases found successfully"), Convert.ToInt16(ResponseCodes.Success), "");
        //            ActionLogger.Logger.WriteLog("CheckAvailabilityService success ", true);
        //            jres.TotalRecords = lst;
        //        }
        //        else
        //        {
        //            jres = new ImportToolPhraseListResponse(string.Format("No payor template phrases found"), Convert.ToInt16(ResponseCodes.RecordNotFound), "");
        //            ActionLogger.Logger.WriteLog("CheckAvailabilityService 404 ", true);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        jres = new ImportToolPhraseListResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
        //        ActionLogger.Logger.WriteLog("CheckAvailabilityService failure ", true);
        //    }
        //    return jres;

        //}

        //public PolicyBoolResponse SaveImportToolStmtSettingsSrvc(ImportToolStatementDataSettings objImportToolStatementDataSettings)
        //{
        //    PolicyBoolResponse jres = null;
        //    ActionLogger.Logger.WriteLog("SaveImportToolStmtSettingsSrvc request: " + objImportToolStatementDataSettings.ToStringDump(), true);
        //    try
        //    {
        //        PayorTemplate objPayorTemplate = new PayorTemplate();
        //        bool res = objPayorTemplate.AddUpdateImportToolStatementDataSettings(objImportToolStatementDataSettings);
        //        if (res)
        //        {
        //            jres = new PolicyBoolResponse(string.Format("Statement settings saved successfully"), Convert.ToInt16(ResponseCodes.Success), "");
        //        }
        //        else
        //        {
        //            jres = new PolicyBoolResponse(string.Format("Statement settings could not be saved"), Convert.ToInt16(ResponseCodes.Failure), "");
        //        }
        //        jres.BoolFlag = res;
        //        ActionLogger.Logger.WriteLog("SaveImportToolStmtSettingsSrvc success ", true);
        //    }
        //    catch (Exception ex)
        //    {
        //        jres = new PolicyBoolResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
        //        ActionLogger.Logger.WriteLog("SaveImportToolStmtSettingsSrvc failure ", true);
        //    }
        //    return jres;
        //}

        //public  ImportToolStmtListResponse GetAllImportToolStatementDataSettingsSrvc(Guid PayorID, Guid templateID)
        //{
        //    ImportToolStmtListResponse jres = null;
        //    ActionLogger.Logger.WriteLog("GetAllImportToolStatementDataSettingsSrvc  request: " +PayorID + ", templateID: " + templateID, true);
        //    try
        //    {
        //        PayorTemplate objPayortemplate = new PayorTemplate();
        //        List<ImportToolStatementDataSettings> lst = objPayortemplate.GetAllImportToolStatementDataSettings(PayorID, templateID);

        //        if (lst != null && lst.Count > 0)
        //        {
        //            jres = new ImportToolStmtListResponse(string.Format("Statements settings found successfully"), Convert.ToInt16(ResponseCodes.Success), "");
        //            ActionLogger.Logger.WriteLog("GetAllImportToolStatementDataSettingsSrvc success ", true);
        //            jres.StatementSettingsList = lst;
        //        }
        //        else
        //        {
        //            jres = new ImportToolStmtListResponse(string.Format("No statement setting found"), Convert.ToInt16(ResponseCodes.RecordNotFound), "");
        //            ActionLogger.Logger.WriteLog("GetAllImportToolStatementDataSettingsSrvc 404 ", true);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        jres = new ImportToolStmtListResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
        //        ActionLogger.Logger.WriteLog("GetAllImportToolStatementDataSettingsSrvc failure ", true);
        //    }
        //    return jres;
        //}

        //public PolicyStringResponse ValidatePhraseAvailbilityService(string strPhrase)
        //{
        //     PolicyStringResponse jres = null;
        //     ActionLogger.Logger.WriteLog("ValidatePhraseAvailbilityService request: strPhrase " + strPhrase, true);
        //    try
        //    {
        //        PayorTemplate objPayortemplate = new PayorTemplate();
        //        string s = objPayortemplate.ValidatePhraseAvailbility(strPhrase);
         
        //        jres = new PolicyStringResponse(string.Format("Phrase availability validated successfully"), Convert.ToInt16(ResponseCodes.Success), "");
        //        jres.StringValue = s;
        //        ActionLogger.Logger.WriteLog("ValidatePhraseAvailbilityService success ", true);
        //    }
        //    catch (Exception ex)
        //    {
        //        jres = new PolicyStringResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
        //        ActionLogger.Logger.WriteLog("ValidatePhraseAvailbilityService failure ", true);
        //    }
        //    return jres;

        //}

        //public MaskFieldListResponse AllMaskTypeService()
        //{
        //    MaskFieldListResponse jres = null;
        //     ActionLogger.Logger.WriteLog("AllMaskTypeService request: ", true);
        //    try
        //    {
        //        PayorTemplate objPayortemplate = new PayorTemplate();
        //        List<MaskFieldTypes> lst = objPayortemplate.AllMaskType();

        //        if (lst != null && lst.Count > 0)
        //        {
        //            jres = new MaskFieldListResponse(string.Format("Mask types found successfully"), Convert.ToInt16(ResponseCodes.Success), "");
        //            ActionLogger.Logger.WriteLog("AllMaskTypeService success ", true);
        //            jres.MaskFieldsList = lst;
        //        }
        //        else
        //        {
        //            jres = new MaskFieldListResponse(string.Format("No mask type found"), Convert.ToInt16(ResponseCodes.RecordNotFound), "");
        //            ActionLogger.Logger.WriteLog("AllMaskTypeService 404 ", true);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        jres = new MaskFieldListResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
        //        ActionLogger.Logger.WriteLog("AllMaskTypeService failure ", true);
        //    }
        //    return jres;
        //}

        //public TranslatorListResponse AllTranslatorTypeService()
        //{
        //    TranslatorListResponse jres = null;
        //    ActionLogger.Logger.WriteLog("AllTranslatorTypeService request: ", true);
        //    try
        //    {
        //        PayorTemplate objPayortemplate = new PayorTemplate();
        //        List<TranslatorTypes> lst = objPayortemplate.AllTranslatorType();
        //        if (lst != null && lst.Count > 0)
        //        {
        //            jres = new TranslatorListResponse(string.Format("Translator types found successfully"), Convert.ToInt16(ResponseCodes.Success), "");
        //            ActionLogger.Logger.WriteLog("AllTranslatorTypeService success ", true);
        //            jres.TranslatorTypesList = lst;
        //        }
        //        else
        //        {
        //            jres = new TranslatorListResponse(string.Format("No translator type found"), Convert.ToInt16(ResponseCodes.RecordNotFound), "");
        //            ActionLogger.Logger.WriteLog("AllTranslatorTypeService 404 ", true);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        jres = new TranslatorListResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
        //        ActionLogger.Logger.WriteLog("AllTranslatorTypeService failure ", true);
        //    }
        //    return jres;
        //}

        //public JSONResponse AddUpdatePaymentDataFieldsSettingSrvc(ImportToolPaymentDataFieldsSettings objImportToolPaymentDataFieldsSettings)
        //{
        //    JSONResponse jres = null;
        //    ActionLogger.Logger.WriteLog("AddUpdatePaymentDataFieldsSettingSrvc request: " + objImportToolPaymentDataFieldsSettings.ToStringDump(), true);
        //    try
        //    {
        //        PayorTemplate objPayortemplate = new PayorTemplate();
        //        objPayortemplate.AddUpdatePaymentDataFieldsSetting(objImportToolPaymentDataFieldsSettings);

        //        jres = new JSONResponse(string.Format("Payment data field settings saved successfully"), Convert.ToInt16(ResponseCodes.Success), "");
        //        ActionLogger.Logger.WriteLog("AddUpdatePaymentDataFieldsSettingSrvc success ", true);
        //    }
        //    catch (Exception ex)
        //    {
        //        jres = new JSONResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
        //        ActionLogger.Logger.WriteLog("AddUpdatePaymentDataFieldsSettingSrvc failure ", true);
        //    }
        //    return jres;
        //}

        //public ImportToolStmtListResponse LoadImportToolStatementDataSettingService()
        //{
        //    ImportToolStmtListResponse jres = null;
        //    ActionLogger.Logger.WriteLog("LoadImportToolStatementDataSettingService  request: ", true);
        //    try
        //    {
        //        PayorTemplate objPayortemplate = new PayorTemplate();
        //        List<ImportToolStatementDataSettings> lst = objPayortemplate.LoadImportToolStatementDataSetting();

        //        if (lst != null && lst.Count > 0)
        //        {
        //            jres = new ImportToolStmtListResponse(string.Format("Statements settings found successfully"), Convert.ToInt16(ResponseCodes.Success), "");
        //            ActionLogger.Logger.WriteLog("LoadImportToolStatementDataSettingService success ", true);
        //            jres.StatementSettingsList = lst;
        //        }
        //        else
        //        {
        //            jres = new ImportToolStmtListResponse(string.Format("No statement setting found"), Convert.ToInt16(ResponseCodes.RecordNotFound), "");
        //            ActionLogger.Logger.WriteLog("LoadImportToolStatementDataSettingService 404 ", true);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        jres = new ImportToolStmtListResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
        //        ActionLogger.Logger.WriteLog("LoadImportToolStatementDataSettingService failure ", true);
        //    }
        //    return jres;
        //}

        //public JSONResponse AddUpdatePaymentDataService(ImportToolSeletedPaymentData objImportToolSeletedPaymentData)
        //{
        //    JSONResponse jres = null;
        //    ActionLogger.Logger.WriteLog("AddUpdatePaymentDataService request: " + objImportToolSeletedPaymentData.ToStringDump(), true);
        //    try
        //    {
        //        PayorTemplate objPayortemplate = new PayorTemplate();
        //        objPayortemplate.AddUpdatePaymentData(objImportToolSeletedPaymentData);

       
        //        jres = new JSONResponse(string.Format("Payment data saved successfully"), Convert.ToInt16(ResponseCodes.Success), "");
        //        ActionLogger.Logger.WriteLog("AddUpdatePaymentDataService success ", true);
        //    }
        //    catch (Exception ex)
        //    {
        //        jres = new JSONResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
        //        ActionLogger.Logger.WriteLog("AddUpdatePaymentDataService failure ", true);
        //    }
        //    return jres;
        //}

        //public JSONResponse DeletePaymentDataService(ImportToolSeletedPaymentData objImportToolSeletedPaymentData)
        //{
        //    JSONResponse jres = null;
        //    ActionLogger.Logger.WriteLog("DeletePaymentDataService request: " + objImportToolSeletedPaymentData.ToStringDump(), true);
        //    try
        //    {
        //        PayorTemplate objPayortemplate = new PayorTemplate();
        //        objPayortemplate.DeletePaymentData(objImportToolSeletedPaymentData);

        //        jres = new JSONResponse(string.Format("Payment data deleted successfully"), Convert.ToInt16(ResponseCodes.Success), "");
        //        ActionLogger.Logger.WriteLog("DeletePaymentDataService success ", true);
        //    }
        //    catch (Exception ex)
        //    {
        //        jres = new JSONResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
        //        ActionLogger.Logger.WriteLog("DeletePaymentDataService failure ", true);
        //    }
        //    return jres;
        //}

        //public ImportToolPaymentListResponse LoadImportToolSeletedPaymentDataSrvc(Guid PayorID, Guid TemplateID)
        //{
        //    ImportToolPaymentListResponse jres = null;
        //    ActionLogger.Logger.WriteLog("LoadPaymentDataFieldsSettingSrvc  request: " + PayorID + ", templateID: " + TemplateID, true);
        //    try
        //    {
        //        PayorTemplate objPayortemplate = new PayorTemplate();
        //        List<ImportToolSeletedPaymentData> lst = objPayortemplate.LoadImportToolSeletedPaymentData(PayorID, TemplateID);

        //        if (lst != null && lst.Count > 0)
        //        {
        //            jres = new ImportToolPaymentListResponse(string.Format("Payment data found successfully"), Convert.ToInt16(ResponseCodes.Success), "");
        //            ActionLogger.Logger.WriteLog("LoadPaymentDataFieldsSettingSrvc success ", true);
        //            jres.ImportToolPaymentDataList = lst;
        //        }
        //        else
        //        {
        //            jres = new ImportToolPaymentListResponse(string.Format("No payment data found"), Convert.ToInt16(ResponseCodes.RecordNotFound), "");
        //            ActionLogger.Logger.WriteLog("LoadPaymentDataFieldsSettingSrvc 404 ", true);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        jres = new ImportToolPaymentListResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
        //        ActionLogger.Logger.WriteLog("LoadPaymentDataFieldsSettingSrvc failure ", true);
        //    }
        //    return jres;

        //}

        //public ImportToolPaymentStngsListResponse LoadPaymentDataFieldsSettingSrvc(Guid PayorID, Guid TemplateID)
        //{
        //    ImportToolPaymentStngsListResponse jres = null;
        //    ActionLogger.Logger.WriteLog("LoadPaymentDataFieldsSettingSrvc  request: " + PayorID + ", templateID: " + TemplateID, true);
        //    try
        //    {
        //        PayorTemplate objPayortemplate = new PayorTemplate();
        //        List<ImportToolPaymentDataFieldsSettings> lst = objPayortemplate.LoadPaymentDataFieldsSetting(PayorID, TemplateID);

        //        if (lst != null && lst.Count > 0)
        //        {
        //            jres = new ImportToolPaymentStngsListResponse(string.Format("Payment fields settings found successfully"), Convert.ToInt16(ResponseCodes.Success), "");
        //            ActionLogger.Logger.WriteLog("LoadPaymentDataFieldsSettingSrvc success ", true);
        //            jres.ImportToolPaymentSettingList = lst;
        //        }
        //        else
        //        {
        //            jres = new ImportToolPaymentStngsListResponse(string.Format("No payment field settings found"), Convert.ToInt16(ResponseCodes.RecordNotFound), "");
        //            ActionLogger.Logger.WriteLog("LoadPaymentDataFieldsSettingSrvc 404 ", true);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        jres = new ImportToolPaymentStngsListResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
        //        ActionLogger.Logger.WriteLog("LoadPaymentDataFieldsSettingSrvc failure ", true);
        //    }
        //    return jres;
        //}

        //public JSONResponse DuplicateSelectedPaymentDataService(ImportToolSeletedPaymentData objImportToolSeletedPaymentData)
        //{
        //    JSONResponse jres = null;
        //    ActionLogger.Logger.WriteLog("DuplicateSelectedPaymentDataService  request: " + objImportToolSeletedPaymentData.ToStringDump(), true);
        //    try
        //    {
        //        PayorTemplate objPayortemplate = new PayorTemplate();
        //        objPayortemplate.DuplicateSelectedPaymentData(objImportToolSeletedPaymentData);

        //        jres = new JSONResponse(string.Format("Payment data duplicated successfully"), Convert.ToInt16(ResponseCodes.Success), "");
        //        ActionLogger.Logger.WriteLog("DuplicateSelectedPaymentDataService success ", true);
        //    }
        //    catch (Exception ex)
        //    {
        //        jres = new JSONResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
        //        ActionLogger.Logger.WriteLog("DuplicateSelectedPaymentDataService failure ", true);
        //    }
        //    return jres;
        //}

        //public ImportToolPhraseListResponse GetAllTemplatePhraseOnTemplateSrvc()
        //{
        //    ImportToolPhraseListResponse jres = null;
        //    ActionLogger.Logger.WriteLog("GetAllTemplatePhraseOnTemplateSrvc  request: ", true);
        //    try
        //    {
        //        PayorTemplate objPayortemplate = new PayorTemplate();
        //        List<ImportToolPayorPhrase> lst = objPayortemplate.GetAllTemplatePhraseOnTemplate();

        //        if (lst != null && lst.Count > 0)
        //        {
        //            jres = new ImportToolPhraseListResponse(string.Format("Payor template phrases found successfully"), Convert.ToInt16(ResponseCodes.Success), "");
        //            ActionLogger.Logger.WriteLog("GetAllTemplatePhraseOnTemplateSrvc success ", true);
        //            jres.PhraseList = lst;
        //        }
        //        else
        //        {
        //            jres = new ImportToolPhraseListResponse(string.Format("No payor template phrases found"), Convert.ToInt16(ResponseCodes.RecordNotFound), "");
        //            ActionLogger.Logger.WriteLog("GetAllTemplatePhraseOnTemplateSrvc 404 ", true);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        jres = new ImportToolPhraseListResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
        //        ActionLogger.Logger.WriteLog("GetAllTemplatePhraseOnTemplateSrvc failure ", true);
        //    }
        //    return jres;
        //}

        //public PayorTemplateListResponse GetAllPayorTemplateService(Guid? SelectedPayorID)
        //{
        //    PayorTemplateListResponse jres = null;
        //    ActionLogger.Logger.WriteLog("GetAllPayorTemplateService  request: " + SelectedPayorID, true);
        //    try
        //    {
        //        PayorTemplate objPayortemplate = new PayorTemplate();
        //        List<Tempalate> lst = objPayortemplate.GetAllPayorTemplate(SelectedPayorID);
        //        if (lst != null && lst.Count > 0)
        //        {
        //            jres = new PayorTemplateListResponse(string.Format("Payor templates found successfully"), Convert.ToInt16(ResponseCodes.Success), "");
        //            ActionLogger.Logger.WriteLog("GetAllPayorTemplateService success ", true);
        //            jres.TotalRecords = lst;
        //        }
        //        else
        //        {
        //            jres = new PayorTemplateListResponse(string.Format("No payor templates found"), Convert.ToInt16(ResponseCodes.RecordNotFound), "");
        //            ActionLogger.Logger.WriteLog("GetAllPayorTemplateService 404 ", true);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        jres = new PayorTemplateListResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
        //        ActionLogger.Logger.WriteLog("GetAllPayorTemplateService failure ", true);
        //    }
        //    return jres;

        //}
        //public JSONResponse DeletePhraseService(int intID)
        //{
        //    JSONResponse jres = null;
        //    ActionLogger.Logger.WriteLog("DeletePhraseService  request: " + intID, true);
        //    try
        //    {
        //        PayorTemplate objPayortemplate = new PayorTemplate();
        //        objPayortemplate.DeletePhrase(intID);

        //        jres = new JSONResponse(string.Format("Phrase data deleted successfully"), Convert.ToInt16(ResponseCodes.Success), "");
        //        ActionLogger.Logger.WriteLog("DeletePhraseService success ", true);
        //    }
        //    catch (Exception ex)
        //    {
        //        jres = new JSONResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
        //        ActionLogger.Logger.WriteLog("DeletePhraseService failure ", true);
        //    }
        //    return jres;
        //}

        //public JSONResponse UpdatePhraseService(int intID, string strPhrase)
        //{
        //    JSONResponse jres = null;
        //    ActionLogger.Logger.WriteLog("UpdatePhraseService  request: " + intID + ", phrase: " + strPhrase, true);
        //    try
        //    {
        //        PayorTemplate objPayortemplate = new PayorTemplate();
        //       // objPayortemplate.UpdatePhrase(intID, strPhrase);


        //        jres = new JSONResponse(string.Format("Phrase data updated successfully"), Convert.ToInt16(ResponseCodes.Success), "");
        //        ActionLogger.Logger.WriteLog("UpdatePhraseService success ", true);
        //    }
        //    catch (Exception ex)
        //    {
        //        jres = new JSONResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
        //        ActionLogger.Logger.WriteLog("UpdatePhraseService failure ", true);
        //    }
        //    return jres;
        //}
    //}
}