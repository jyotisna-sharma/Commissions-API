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
    interface ILicenseeNoteService
    {
        #region Notes 
        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        LicenseeNoteResponse AddUpdateLicenseeNoteService(LicenseeNote LNote);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        JSONResponse DeleteLicenseeNoteService(LicenseeNote LNote);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        PolicyBoolResponse IsValidLicenseeNoteService(LicenseeNote LNote);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        LicenseeNoteResponse GetLicenseeNoteOfIDService(Guid Id);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        LicenseeNoteListResponse GetLicenseeNotesService(Guid licenseeID);

        #endregion

        #region Journal methods

        [OperationContract()]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        JournalListResponse GetJournalEntriesByLicenseeService(Guid LicenseeID);

        [OperationContract()]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        JournalListResponse GetJournalEntriesByInvoiceIDService(long InvoiceId);

        [OperationContract()]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        JournalListResponse GetJournalEntriesByLicenseeIDInvocieIDService(Guid LicenseeID, long InvoiceId);

        [OperationContract()]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        JournalListResponse GetAllJournalEntriesService();

        [OperationContract()]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        PolicyBoolResponse DeleteJournalEntryService(LicenseeInvoiceJournal journalEntry);

        [OperationContract()]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        PolicyPMCResponse InsertJournalEntryService(LicenseeInvoiceJournal journalEntry);

        [OperationContract()]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        PolicyBoolResponse UpdateJournalEntryService(LicenseeInvoiceJournal journalEntry);

        #endregion
    }

    public partial class MavService : ILicenseeNoteService
    {
        #region ILicenseeNote Members

        public LicenseeNoteResponse AddUpdateLicenseeNoteService(LicenseeNote LNote)
        {
            LicenseeNoteResponse jres = null;
            ActionLogger.Logger.WriteLog("AddUpdateLicenseeNoteService request: " + LNote.ToStringDump(), true);
            try
            {
                LicenseeNote note =  LNote.AddUpdate();
                if (note != null)
                {
                    jres = new LicenseeNoteResponse(string.Format("Note saved successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                    jres.NoteObject = note;
                }
                else
                {
                    jres = new LicenseeNoteResponse("", Convert.ToInt16(ResponseCodes.Failure), "Note could not be saved");
                }
                ActionLogger.Logger.WriteLog("AddUpdateLicenseeNoteService success ", true);
            }
            catch (Exception ex)
            {
                jres = new LicenseeNoteResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                ActionLogger.Logger.WriteLog("AddUpdateLicenseeNoteService failure ", true);
            }
            return jres;
        }

        public JSONResponse DeleteLicenseeNoteService(LicenseeNote LNote)
        {
             JSONResponse jres = null;
             ActionLogger.Logger.WriteLog("DeleteLicenseeNoteService request: " + LNote.ToStringDump(), true);
            try
            {
                LNote.Delete();
                jres = new JSONResponse(string.Format("Note deleted  successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                ActionLogger.Logger.WriteLog("DeleteLicenseeNoteService success ", true);
            }
            catch (Exception ex)
            {
                jres = new JSONResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                ActionLogger.Logger.WriteLog("DeleteLicenseeNoteService failure ", true);
            }
            return jres;
        }

        public PolicyBoolResponse IsValidLicenseeNoteService(LicenseeNote LNote)
        {
            PolicyBoolResponse jres = null;
            ActionLogger.Logger.WriteLog("IsValidLicenseeNoteService request: " + LNote.ToStringDump(), true);
            try
            {
                bool res = LNote.IsValid();
                jres = new PolicyBoolResponse(string.Format("Notes validity found  successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                jres.BoolFlag = res;
                ActionLogger.Logger.WriteLog("IsValidLicenseeNoteService success ", true);
            }
            catch (Exception ex)
            {
                jres = new PolicyBoolResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                ActionLogger.Logger.WriteLog("IsValidLicenseeNoteService failure ", true);
            }
            return jres;

        }

        public LicenseeNoteResponse GetLicenseeNoteOfIDService(Guid Id)
        {
            LicenseeNoteResponse jres = null;
            ActionLogger.Logger.WriteLog("GetLicenseeNoteOfIDService request: " + Id, true);
            try
            {
                LicenseeNote note = LicenseeNote.GetOfID(Id);
                if (note != null)
                {
                    jres = new LicenseeNoteResponse(string.Format("Note found successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                    jres.NoteObject = note;
                }
                else
                {
                    jres = new LicenseeNoteResponse("Note not found", Convert.ToInt16(ResponseCodes.RecordNotFound), "Note could not be found");
                }
                ActionLogger.Logger.WriteLog("GetLicenseeNoteOfIDService success ", true);
            }
            catch (Exception ex)
            {
                jres = new LicenseeNoteResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                ActionLogger.Logger.WriteLog("GetLicenseeNoteOfIDService failure ", true);
            }
            return jres;
        }

        public LicenseeNoteListResponse GetLicenseeNotesService(Guid licenseeID)
        {
            LicenseeNoteListResponse jres = null;
            ActionLogger.Logger.WriteLog("GetLicenseeNotesService request: " + licenseeID, true);
            try
            {
                List<LicenseeNote> lst = LicenseeNote.GetLicenseeNotes(licenseeID);
           
                if (lst != null && lst.Count > 0)
                {
                    jres = new LicenseeNoteListResponse(string.Format("Licensee notes list found successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                    jres.NotesList = lst;
                    ActionLogger.Logger.WriteLog("GetLicenseeNotesService success ", true);
                }
                else
                {
                    jres = new LicenseeNoteListResponse(string.Format("No licensee note found "), Convert.ToInt16(ResponseCodes.RecordNotFound), "");
                    ActionLogger.Logger.WriteLog("GetLicenseeNotesService 404 ", true);
                }
            }
            catch (Exception ex)
            {
                jres = new LicenseeNoteListResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                ActionLogger.Logger.WriteLog("GetLicenseeNotesService failure ", true);
            }
            return jres;
        }
        #endregion

        #region Journal methods

        public JournalListResponse GetJournalEntriesByLicenseeService(Guid LicenseeID)
        {
            JournalListResponse jres = null;
            ActionLogger.Logger.WriteLog("GetJournalEntriesByLicenseeService request: " + LicenseeID, true);
            try
            {
                JournalHelper journalHelper = new JournalHelper();
                List<LicenseeInvoiceJournal> lst = journalHelper.getJournalEntries(LicenseeID);

                if (lst != null && lst.Count > 0)
                {
                    jres = new JournalListResponse(string.Format("Licensee journal list found successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                    jres.JournalList = lst;
                    ActionLogger.Logger.WriteLog("GetJournalEntriesByLicenseeService success ", true);
                }
                else
                {
                    jres = new JournalListResponse(string.Format("No licensee journal found "), Convert.ToInt16(ResponseCodes.RecordNotFound), "");
                    ActionLogger.Logger.WriteLog("GetJournalEntriesByLicenseeService 404 ", true);
                }
            }
            catch (Exception ex)
            {
                jres = new JournalListResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                ActionLogger.Logger.WriteLog("GetJournalEntriesByLicenseeService failure ", true);
            }
            return jres;
        }

        public JournalListResponse GetJournalEntriesByInvoiceIDService(long InvoiceId)
        {
            JournalListResponse jres = null;
            ActionLogger.Logger.WriteLog("GetJournalEntriesByInvoiceIDService request: " + InvoiceId, true);
            try
            {
                JournalHelper journalHelper = new JournalHelper();
                List < LicenseeInvoiceJournal > lst = journalHelper.getJournalEntries(InvoiceId);

                if (lst != null && lst.Count > 0)
                {
                    jres = new JournalListResponse(string.Format("Licensee journal list found successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                    jres.JournalList = lst;
                    ActionLogger.Logger.WriteLog("GetJournalEntriesByInvoiceIDService success ", true);
                }
                else
                {
                    jres = new JournalListResponse(string.Format("No licensee journal found "), Convert.ToInt16(ResponseCodes.RecordNotFound), "");
                    ActionLogger.Logger.WriteLog("GetJournalEntriesByInvoiceIDService 404 ", true);
                }
            }
            catch (Exception ex)
            {
                jres = new JournalListResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                ActionLogger.Logger.WriteLog("GetJournalEntriesByLicenseeService failure ", true);
            }
            return jres;
        }

        public JournalListResponse GetJournalEntriesByLicenseeIDInvocieIDService(Guid LicenseeID, long InvoiceId)
        {
            JournalListResponse jres = null;
            ActionLogger.Logger.WriteLog("GetJournalEntriesByLicenseeIDInvocieIDService request: " + InvoiceId + ", licenseeID:  "+ LicenseeID, true);
            try
            {
                JournalHelper journalHelper = new JournalHelper();
                List<LicenseeInvoiceJournal> lst = journalHelper.getJournalEntries(LicenseeID, InvoiceId);
                
                if (lst != null && lst.Count > 0)
                {
                    jres = new JournalListResponse(string.Format("Licensee journal list found successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                    jres.JournalList = lst;
                    ActionLogger.Logger.WriteLog("GetJournalEntriesByLicenseeIDInvocieIDService success ", true);
                }
                else
                {
                    jres = new JournalListResponse(string.Format("No licensee journal found "), Convert.ToInt16(ResponseCodes.RecordNotFound), "");
                    ActionLogger.Logger.WriteLog("GetJournalEntriesByLicenseeIDInvocieIDService 404 ", true);
                }
            }
            catch (Exception ex)
            {
                jres = new JournalListResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                ActionLogger.Logger.WriteLog("GetJournalEntriesByLicenseeIDInvocieIDService failure ", true);
            }
            return jres;
        }

        public JournalListResponse GetAllJournalEntriesService()
        {
            JournalListResponse jres = null;
            ActionLogger.Logger.WriteLog("GetAllJournalEntriesService request ", true);
            try
            {
                JournalHelper journalHelper = new JournalHelper();
                List<LicenseeInvoiceJournal> lst = journalHelper.getAllJournalEntries();

                if (lst != null && lst.Count > 0)
                {
                    jres = new JournalListResponse(string.Format("Licensee journal list found successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                    jres.JournalList = lst;
                    ActionLogger.Logger.WriteLog("GetAllJournalEntriesService success ", true);
                }
                else
                {
                    jres = new JournalListResponse(string.Format("No licensee journal found "), Convert.ToInt16(ResponseCodes.RecordNotFound), "");
                    ActionLogger.Logger.WriteLog("GetAllJournalEntriesService 404 ", true);
                }
            }
            catch (Exception ex)
            {
                jres = new JournalListResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                ActionLogger.Logger.WriteLog("GetAllJournalEntriesService failure ", true);
            }
            return jres;
        }

        public PolicyBoolResponse DeleteJournalEntryService(LicenseeInvoiceJournal journalEntry)
        {
            PolicyBoolResponse jres = null;
            ActionLogger.Logger.WriteLog("DeleteJournalEntryService request: " + journalEntry.ToStringDump(), true);
            try
            {
                JournalHelper journalHelper = new JournalHelper();
                bool res =  journalHelper.DeleteJournalEntry(journalEntry);

                jres = new PolicyBoolResponse(string.Format("Journal entry deleted successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                jres.BoolFlag = res;
                ActionLogger.Logger.WriteLog("DeleteJournalEntryService success ", true);
            }
            catch (Exception ex)
            {
                jres = new PolicyBoolResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                ActionLogger.Logger.WriteLog("DeleteJournalEntryService failure ", true);
            }
            return jres;
        }

        public PolicyPMCResponse InsertJournalEntryService(LicenseeInvoiceJournal journalEntry)
        {
           
            PolicyPMCResponse jres = null;
            ActionLogger.Logger.WriteLog("InsertJournalEntryService request: " + journalEntry.ToStringDump(), true);
            try
            {
                 JournalHelper journalHelper = new JournalHelper();
                long num = journalHelper.InsertJournalEntry(journalEntry);
    
                if (num > 0)
                {
                    jres = new PolicyPMCResponse(string.Format("Journal entry added successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                    jres.NumberValue = num;
                }
                else
                {
                    jres = new PolicyPMCResponse("Journal entry could not be added ", Convert.ToInt16(ResponseCodes.Failure), "Journal entry could not be added");
                }
                ActionLogger.Logger.WriteLog("InsertJournalEntryService success ", true);
            }
            catch (Exception ex)
            {
                jres = new PolicyPMCResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                ActionLogger.Logger.WriteLog("InsertJournalEntryService failure ", true);
            }
            return jres;
        }

        public PolicyBoolResponse UpdateJournalEntryService(LicenseeInvoiceJournal journalEntry)
        {
            PolicyBoolResponse jres = null;
            ActionLogger.Logger.WriteLog("UpdateJournalEntryService request: " + journalEntry.ToStringDump(), true);
            try
            {
                JournalHelper journalHelper = new JournalHelper();
                bool res = journalHelper.UpdateJournalEntry(journalEntry);
                if (res)
                {
                    jres = new PolicyBoolResponse(string.Format("Journal entry updated successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                }
                else
                {
                    jres = new PolicyBoolResponse("Journal entry could not be updated ", Convert.ToInt16(ResponseCodes.Failure), "Journal entry could not be updated");
                }
                jres.BoolFlag = res;
                ActionLogger.Logger.WriteLog("UpdateJournalEntryService success ", true);
            }
            catch (Exception ex)
            {
                jres = new PolicyBoolResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                ActionLogger.Logger.WriteLog("UpdateJournalEntryService failure ", true);
            }
            return jres;
        }

        #endregion
    }
}