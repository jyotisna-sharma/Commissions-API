using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
using MyAgencyVault.BusinessLibrary;

namespace MyAgencyVault.WcfService.Library.Response
{
    [DataContract]
    public class StatementDatesListResponse : JSONResponse
    {
        public StatementDatesListResponse(string message, int errorCode, string exceptionMessage)
            : base(message, errorCode, exceptionMessage)
        {
        }
        [DataMember]
        public List<StatementDates> StatementDatesList { get; set; }
    }

    [DataContract]
    public class StatementResponse : JSONResponse
    {
        public StatementResponse(string message, int errorCode, string exceptionMessage)
            : base(message, errorCode, exceptionMessage)
        {
        }
        [DataMember]
        public Statement StatementObject { get; set; }
    }
     
    [DataContract]
     public class StatementListResponse : JSONResponse
    {
        public StatementListResponse(string message, int errorCode, string exceptionMessage)
            : base(message, errorCode, exceptionMessage)
        {
        }
        [DataMember]
        public List<Statement> StatementList { get; set; }
    }

      [DataContract]
    public class ModifiedStmtResponse : JSONResponse
    {
        public ModifiedStmtResponse(string message, int errorCode, string exceptionMessage)
            : base(message, errorCode, exceptionMessage)
        {
        }
        [DataMember]
        public ModifiableStatementData StatementObject { get; set; }
    }
}