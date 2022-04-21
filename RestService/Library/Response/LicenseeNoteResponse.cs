using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Runtime.Serialization;
using MyAgencyVault.BusinessLibrary;

namespace MyAgencyVault.WcfService.Library.Response
{
    [DataContract]
    public class LicenseeNoteResponse : JSONResponse
    {
        public LicenseeNoteResponse(string message, int errorCode, string exceptionMessage)
            : base(message, errorCode, exceptionMessage)
        {
        }
        [DataMember]
        public LicenseeNote NoteObject { get; set; }
    }

    [DataContract]
    public class LicenseeNoteListResponse : JSONResponse
    {
        public LicenseeNoteListResponse(string message, int errorCode, string exceptionMessage)
            : base(message, errorCode, exceptionMessage)
        {
        }
        [DataMember]
        public List<LicenseeNote> NotesList { get; set; }
    }

    [DataContract]
    public class JournalListResponse : JSONResponse
    {
        public JournalListResponse(string message, int errorCode, string exceptionMessage)
            : base(message, errorCode, exceptionMessage)
        {
        }
        [DataMember]
        public List<LicenseeInvoiceJournal> JournalList { get; set; }
    }
}