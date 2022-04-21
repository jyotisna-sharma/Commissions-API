using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ServiceModel;
using System.ServiceModel.Web;
using MyAgencyVault.BusinessLibrary;
using MyAgencyVault.WcfService.Library.Response;

namespace MyAgencyVault.WcfService
{
     [ServiceContract]
    interface ILicenseeService
    {
         [OperationContract]
         [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
         CalculationResponse  StartCalculationService(LicenseeVariableInputDetail InputData);

        [OperationContract]
         [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
         InvoiceLineResponse GetInvoiceLinesService(long InvoiceId, bool IncludePolicyData);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        InvoiceLineListResponse GetInvoiceLinesForJournalService(Guid LicenseeID);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        InvoiceListResponse GetAllInvoiceService();

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        LicenseeInvoiceResponse GetInvoiceByIDService(long InvoiceID);
        
         [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        PolicyStringResponse GetExportBatchNameService(Guid? ExportedBatchId);

        [OperationContract]
         [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
         JSONResponse AddUpdateLicenseeService(LicenseeDisplayData License);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        JSONResponse DeleteLicenseeService(LicenseeDisplayData License);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        JSONResponse SaveAllNotesService(LicenseeDisplayData License);

       /* Not implemented 
        * [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        PolicyBoolResponse ExportCardPayeesService();

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        PolicyBoolResponse ExportCheckPayeesService();

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        PolicyBoolResponse ImportDataFromFileService();*/

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        LicenseeDisplayDataResponse GetLicenseeByIDService(Guid LicenseeID);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        StringListResponse GetPaymentTypesService();
        
         [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        LicenseeListResponse GetDisplayedLicenseeListService(Guid LicenseeID);
        
         [OperationContract]
         [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
         JSONResponse SetLastLoginTimeService(Guid LicenseeID);
        
         [OperationContract]
         [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
         LicenseeBalanceListResponse GetLicenseesBalanceService();

         [OperationContract]
         [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
         JSONResponse SetLastUploadTimeService(Guid LicenseeID);

         [OperationContract]
         [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
         LicenseeListResponse GetLicenseeListService(LicenseeStatusEnum status, Guid licenseeId);

         [OperationContract]
         [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
         LicenseeListResponse GetDisplayedLicenseeListPolicyMangerService(Guid LicenseeID);



    }

     public partial class MavService : ILicenseeService
     {
         public CalculationResponse StartCalculationService(LicenseeVariableInputDetail inputData)
         {
             CalculationResponse jres = null;
             ActionLogger.Logger.WriteLog("StartCalculationService inputData: " + inputData.ToStringDump(), true);
             try
             {
                 VariableCalculation VarCalc = new VariableCalculation();
                 VarCalc.LicenseeInputInfo = inputData;
                 VarCalc.LicenseeVariableInfo = new LicenseeVariableOutputDetail();
                 VarCalc.StartVariableCalculation();
                 LicenseeVariableOutputDetail obj = VarCalc.LicenseeVariableInfo;

                 jres = new CalculationResponse(string.Format("Calculation done successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                 jres.Result = obj;

                 ActionLogger.Logger.WriteLog("StartCalculationService success ", true);
             }
             catch (Exception ex)
             {
                 jres = new CalculationResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                 ActionLogger.Logger.WriteLog("StartCalculationService failure ", true);
             }
             return jres;
         }

         #region Invoice methods 
         public InvoiceListResponse GetAllInvoiceService()
         {
             InvoiceListResponse jres = null;
             ActionLogger.Logger.WriteLog("GetAllInvoiceService request: ", true);
             try
             {
                List<LicenseeInvoice> lst =   LicenseeInvoiceHelper.getAllInvoice();
                if (lst != null && lst.Count > 0)
                 {
                     jres = new InvoiceListResponse(string.Format("Invoice list found successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                     jres.InvoiceList = lst;
                     ActionLogger.Logger.WriteLog("GetAllInvoiceService success ", true);
                 }
                 else
                 {
                     jres = new InvoiceListResponse(string.Format("No invoice data found "), Convert.ToInt16(ResponseCodes.RecordNotFound), "");
                     ActionLogger.Logger.WriteLog("GetAllInvoiceService 404 ", true);
                 }
             }
             catch (Exception ex)
             {
                 jres = new InvoiceListResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                 ActionLogger.Logger.WriteLog("GetAllInvoiceService failure ", true);
             }
             return jres;
         }

         public LicenseeInvoiceResponse GetInvoiceByIDService(long InvoiceID)
         {
              LicenseeInvoiceResponse jres = null;
              ActionLogger.Logger.WriteLog("GetInvoiceByIDService request: " + InvoiceID , true);
             try
             {
                 LicenseeInvoice obj = LicenseeInvoiceHelper.getInvoiceByID(InvoiceID);
                 if (obj != null)
                 {
                     jres = new LicenseeInvoiceResponse(string.Format("Invoice details found successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                     jres.InvoiceObject = obj;
                     ActionLogger.Logger.WriteLog("GetInvoiceByIDService success ", true);
                 }
                 else
                 {
                     jres = new LicenseeInvoiceResponse(string.Format("No invoice data found "), Convert.ToInt16(ResponseCodes.RecordNotFound), "");
                     ActionLogger.Logger.WriteLog("GetInvoiceByIDService 404 ", true);
                 }
             }
             catch (Exception ex)
             {
                 jres = new LicenseeInvoiceResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                 ActionLogger.Logger.WriteLog("GetInvoiceByIDService failure ", true);
             }
             return jres;
         }

         public PolicyStringResponse GetExportBatchNameService(Guid? ExportedBatchId)
         {
             PolicyStringResponse jres = null;
             ActionLogger.Logger.WriteLog("GetExportBatchNameService request: " + ExportedBatchId, true);
             try
             {
                 string s = LicenseeInvoiceHelper.getExportBatchName(ExportedBatchId);
                 if (!string.IsNullOrEmpty(s))
                 {
                     jres = new PolicyStringResponse(string.Format("Batch name found successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                     jres.StringValue = s;
                     ActionLogger.Logger.WriteLog("GetExportBatchNameService success ", true);
                 }
                 else
                 {
                     jres = new PolicyStringResponse(string.Format("Batch name not found"), Convert.ToInt16(ResponseCodes.Success), "");
                     ActionLogger.Logger.WriteLog("GetExportBatchNameService 404 ", true);
                 }
             }
             catch (Exception ex)
             {
                 jres = new PolicyStringResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                 ActionLogger.Logger.WriteLog("GetExportBatchNameService failure ", true);
             }
             return jres;
         }

         public InvoiceLineResponse GetInvoiceLinesService(long invoiceId, bool includePolicyData)
         {
             InvoiceLineResponse jres = null;
             ActionLogger.Logger.WriteLog("GetInvoiceLinesService request: " + invoiceId + ", includePolicyData : " + includePolicyData, true);
             try
             {
                 InvoiceLineHelper invoiceLineHelper = new InvoiceLineHelper();
                 InvoiceLine obj = invoiceLineHelper.getInvoiceLines(invoiceId, includePolicyData);

                 if (obj != null)
                 {
                     jres = new InvoiceLineResponse(string.Format("Invoice data found successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                     jres.InvoiceLineObject = obj;
                     ActionLogger.Logger.WriteLog("GetInvoiceLinesService success ", true);
                 }
                 else
                 {
                     jres = new InvoiceLineResponse(string.Format("No invoice data found "), Convert.ToInt16(ResponseCodes.RecordNotFound), "");
                     ActionLogger.Logger.WriteLog("GetInvoiceLinesService 404 ", true);
                 }
             }
             catch (Exception ex)
             {
                 jres = new InvoiceLineResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                 ActionLogger.Logger.WriteLog("GetInvoiceLinesService failure ", true);
             }
             return jres;
         }

         public InvoiceLineListResponse GetInvoiceLinesForJournalService(Guid licenseeId)
         {
             InvoiceLineListResponse jres = null;
             ActionLogger.Logger.WriteLog("GetInvoiceLinesForJournalService request: " + licenseeId, true);
             try
             {
                 InvoiceLineHelper invoiceLineHelper = new InvoiceLineHelper();
                 List<InvoiceLineJournalData> lst = invoiceLineHelper.getInvoiceLinesForJournal(licenseeId);
                 if (lst != null && lst.Count > 0)
                 {
                     jres = new InvoiceLineListResponse(string.Format("Invoice list found successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                     jres.InvoiceList = lst;
                     ActionLogger.Logger.WriteLog("GetInvoiceLinesForJournalService success ", true);
                 }
                 else
                 {
                     jres = new InvoiceLineListResponse(string.Format("No invoice list found "), Convert.ToInt16(ResponseCodes.RecordNotFound), "");
                     ActionLogger.Logger.WriteLog("GetInvoiceLinesForJournalService 404 ", true);
                 }
             }
             catch (Exception ex)
             {
                 jres = new InvoiceLineListResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                 ActionLogger.Logger.WriteLog("GetInvoiceLinesForJournalService failure ", true);
             }
             return jres;
         }

        #endregion

         public LicenseeListResponse GetDisplayedLicenseeListPolicyMangerService(Guid LicenseeID)
         {
             LicenseeListResponse jres = null;
             ActionLogger.Logger.WriteLog("GetDisplayedLicenseeListPolicyMangerService request: " + LicenseeID, true);
             try
             {
                 List<LicenseeDisplayData> lst = LicenseeDisplayData.GetDisplayedLicenseeListPolicyManger(LicenseeID);
                 if (lst != null && lst.Count > 0)
                 {
                     jres = new LicenseeListResponse(string.Format("Licensee list found successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                     jres.LicenseeList = lst;
                     ActionLogger.Logger.WriteLog("GetDisplayedLicenseeListPolicyMangerService success ", true);
                 }
                 else
                 {
                     jres = new LicenseeListResponse(string.Format("No licensee data found "), Convert.ToInt16(ResponseCodes.RecordNotFound), "");
                     ActionLogger.Logger.WriteLog("GetDisplayedLicenseeListPolicyMangerService 404 ", true);
                 }
             }
             catch (Exception ex)
             {
                 jres = new LicenseeListResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                 ActionLogger.Logger.WriteLog("GetDisplayedLicenseeListPolicyMangerService failure ", true);
             }
             return jres;
         }

         public LicenseeListResponse GetLicenseeListService(LicenseeStatusEnum status, Guid licenseeId)
         {
             LicenseeListResponse jres = null;
             ActionLogger.Logger.WriteLog("GetLicenseeListService request: " + status, true);
             try
             {
                 List<LicenseeDisplayData> lst = Licensee.GetLicenseeList(status, licenseeId);
                 if (lst != null && lst.Count > 0)
                 {
                     jres = new LicenseeListResponse(string.Format("Licensee list found successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                     jres.LicenseeList = lst;
                     ActionLogger.Logger.WriteLog("GetLicenseeListService success ", true);
                 }
                 else
                 {
                     jres = new LicenseeListResponse(string.Format("No licensee data found "), Convert.ToInt16(ResponseCodes.RecordNotFound), "");
                     ActionLogger.Logger.WriteLog("GetLicenseeListService 404 ", true);
                 }
             }
             catch (Exception ex)
             {
                 jres = new LicenseeListResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                 ActionLogger.Logger.WriteLog("GetLicenseeListService failure ", true);
             }
             return jres;
         }

         public JSONResponse SetLastUploadTimeService(Guid LicenseeId)
         {
            
             JSONResponse jres = null;
             ActionLogger.Logger.WriteLog("SetLastUploadTimeService request: " + LicenseeId, true);
             try
             {
                 Licensee.SetLastUploadTime(LicenseeId);
                 jres = new JSONResponse(string.Format("Last upload time saved successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                 ActionLogger.Logger.WriteLog("SetLastUploadTimeService success ", true);
             }
             catch (Exception ex)
             {
                 jres = new JSONResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                 ActionLogger.Logger.WriteLog("SetLastUploadTimeService failure ", true);
             }
             return jres;
         }

         #region ILicensee Members

         public JSONResponse AddUpdateLicenseeService(LicenseeDisplayData licensee)
         {
             JSONResponse jres = null;
             ActionLogger.Logger.WriteLog("AddUpdateLicenseeService request: " + licensee.ToStringDump(), true);
             try
             {
                 Licensee.AddUpdate(licensee);
                 jres = new JSONResponse(string.Format("Licensee details saved successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                 ActionLogger.Logger.WriteLog("AddUpdateLicenseeService success ", true);
             }
             catch (Exception ex)
             {
                 jres = new JSONResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                 ActionLogger.Logger.WriteLog("AddUpdateLicenseeService failure ", true);
             }
             return jres;
         }

         public JSONResponse DeleteLicenseeService(LicenseeDisplayData licensee)
         {
              JSONResponse jres = null;
              ActionLogger.Logger.WriteLog("DeleteLicenseeService request: " + licensee.ToStringDump(), true);
             try
             {
                 Licensee.Delete(licensee);
                 jres = new JSONResponse(string.Format("Licensee deleted successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                 ActionLogger.Logger.WriteLog("DeleteLicenseeService success ", true);
             }
             catch (Exception ex)
             {
                 jres = new JSONResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                 ActionLogger.Logger.WriteLog("DeleteLicenseeService failure ", true);
             }
             return jres;
         }


         public JSONResponse SaveAllNotesService(LicenseeDisplayData licensee)
         {
              JSONResponse jres = null;
              ActionLogger.Logger.WriteLog("SaveAllNotesService request: " + licensee.ToStringDump(), true);
             try
             {
                 Licensee.SaveAllNotes(licensee);
                 jres = new JSONResponse(string.Format("Notes saved successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                 ActionLogger.Logger.WriteLog("SaveAllNotesService success ", true);
             }
             catch (Exception ex)
             {
                 jres = new JSONResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                 ActionLogger.Logger.WriteLog("SaveAllNotesService failure ", true);
             }
             return jres;
         }

         /*
          * Not implrmeneted 
         public PolicyBoolResponse ExportCardPayeesService()
         {
             PolicyBoolResponse jres = null;
             ActionLogger.Logger.WriteLog("ExportCardPayeeService request: " , true);
             try
             {
                 bool res = Licensee.ExportCardPayees();
                 jres = new PolicyBoolResponse(string.Format("Data saved successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                 jres.BoolFlag = res;
                 ActionLogger.Logger.WriteLog("ExportCardPayeeService success ", true);
             }
             catch (Exception ex)
             {
                 jres = new PolicyBoolResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                 ActionLogger.Logger.WriteLog("ExportCardPayeeService failure ", true);
             }
             return jres;
         }

         public PolicyBoolResponse ExportCheckPayeesService()
         {
             PolicyBoolResponse jres = null;
             ActionLogger.Logger.WriteLog("ExportCheckPayeeService request: ", true);
             try
             {
                 bool res =   Licensee.ExportCheckPayees();
                 jres = new PolicyBoolResponse(string.Format("Data saved successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                 jres.BoolFlag = res;
                 ActionLogger.Logger.WriteLog("ExportCheckPayeeService success ", true);
             }
             catch (Exception ex)
             {
                 jres = new PolicyBoolResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                 ActionLogger.Logger.WriteLog("ExportCheckPayeeService failure ", true);
             }
             return jres;
         }

         public PolicyBoolResponse ImportDataFromFileService()
         {
             PolicyBoolResponse jres = null;
             ActionLogger.Logger.WriteLog("ImportDataFromFileService request: ", true);
             try
             {
                 bool res = Licensee.ImportDataFromFile();
                 jres = new PolicyBoolResponse(string.Format("Data imported successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                 jres.BoolFlag = res;
                 ActionLogger.Logger.WriteLog("ImportDataFromFileService success ", true);
             }
             catch (Exception ex)
             {
                 jres = new PolicyBoolResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                 ActionLogger.Logger.WriteLog("ImportDataFromFileService failure ", true);
             }
             return jres;
         }*/

         public LicenseeDisplayDataResponse GetLicenseeByIDService(Guid id)
         {

             LicenseeDisplayDataResponse jres = null;
             ActionLogger.Logger.WriteLog("GetLicenseeByIDService request: ", true);
             try
             {
                 LicenseeDisplayData obj = Licensee.GetLicenseeByID(id);
                 if (obj != null )
                 {
                     jres = new LicenseeDisplayDataResponse(string.Format("Licensee data found successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                     jres.LicenseeObject = obj;
                 }
                 else
                 {
                     jres = new LicenseeDisplayDataResponse(string.Format("Licensee data not found"), Convert.ToInt16(ResponseCodes.RecordNotFound), "");
                 }
                 ActionLogger.Logger.WriteLog("GetLicenseeByIDService success ", true);
             }
             catch (Exception ex)
             {
                 jres = new LicenseeDisplayDataResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                 ActionLogger.Logger.WriteLog("GetLicenseeByIDService failure ", true);
             }
             return jres;
         }

         public StringListResponse GetPaymentTypesService()
         {
             StringListResponse jres = null;
             ActionLogger.Logger.WriteLog("ImportDataFromFileService request: ", true);
             try
             {
                 List<string> lst = Licensee.getPaymentTypes();
                 if (lst != null && lst.Count > 0)
                 {
                     jres = new StringListResponse(string.Format("Payment types found successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                     jres.List = lst;
                 }
                 else
                 {
                     jres = new StringListResponse(string.Format("Payment types not found"), Convert.ToInt16(ResponseCodes.RecordNotFound), "");
                 }
                 ActionLogger.Logger.WriteLog("ImportDataFromFileService success ", true);
             }
             catch (Exception ex)
             {
                 jres = new StringListResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                 ActionLogger.Logger.WriteLog("ImportDataFromFileService failure ", true);
             }
             return jres;
         }

         public LicenseeListResponse GetDisplayedLicenseeListService(Guid id)
         {
             LicenseeListResponse jres = null;
             ActionLogger.Logger.WriteLog("GetDisplayedLicenseeListService request: ", true);
             try
             {
                 List<LicenseeDisplayData> lst = LicenseeDisplayData.GetDisplayedLicenseeList(id);
                 if (lst != null && lst.Count > 0)
                 {
                     jres = new LicenseeListResponse(string.Format("Licensee list found successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                     jres.LicenseeList = lst;
                     ActionLogger.Logger.WriteLog("GetDisplayedLicenseeListService success ", true);
                 }
                 else
                 {
                     jres = new LicenseeListResponse(string.Format("Licensee list not found"), Convert.ToInt16(ResponseCodes.RecordNotFound), "");
                     ActionLogger.Logger.WriteLog("GetDisplayedLicenseeListService 404 ", true);
                 }
             }
             catch (Exception ex)
             {
                 jres = new LicenseeListResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                 ActionLogger.Logger.WriteLog("GetDisplayedLicenseeListService failure ", true);
             }
             return jres;
         }


         public JSONResponse SetLastLoginTimeService(Guid LicenseeId)
         {
              JSONResponse jres = null;
              ActionLogger.Logger.WriteLog("SetLastLoginTimeService request: " + LicenseeId, true);
             try
             {
                 Licensee.SetLastLoginTime(LicenseeId);
                 jres = new JSONResponse(string.Format("Last login time saved successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                 ActionLogger.Logger.WriteLog("SetLastLoginTimeService success ", true);
             }
             catch (Exception ex)
             {
                 jres = new JSONResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                 ActionLogger.Logger.WriteLog("SetLastLoginTimeService failure ", true);
             }
             return jres;
         }

       

         public LicenseeBalanceListResponse GetLicenseesBalanceService()
         {
             LicenseeBalanceListResponse jres = null;
             ActionLogger.Logger.WriteLog("GetLicenseesBalanceService request: ", true);
             try
             {
                 List<LicenseeBalance> lst = Licensee.getLicenseesBalance();
                 if (lst != null && lst.Count > 0)
                 {
                     jres = new LicenseeBalanceListResponse(string.Format("Licensees balance list found successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                     jres.LicenseeList = lst;
                     ActionLogger.Logger.WriteLog("GetLicenseesBalanceService success ", true);
                 }
                 else
                 {
                     jres = new LicenseeBalanceListResponse(string.Format("Licensees balance list not found"), Convert.ToInt16(ResponseCodes.RecordNotFound), "");
                     ActionLogger.Logger.WriteLog("GetLicenseesBalanceService 404 ", true);
                 }
             }
             catch (Exception ex)
             {
                 jres = new LicenseeBalanceListResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                 ActionLogger.Logger.WriteLog("GetLicenseesBalanceService failure ", true);
             }
             return jres;
         }

         #endregion
     }
}