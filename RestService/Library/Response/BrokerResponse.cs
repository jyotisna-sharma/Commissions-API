using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
using MyAgencyVault.BusinessLibrary.Masters;
using MyAgencyVault.BusinessLibrary;


namespace MyAgencyVault.WcfService.Library.Response
{
    [DataContract]
    public class BrokerListResponse : JSONResponse
    {
        public BrokerListResponse(string message, int errorCode, string exceptionMessage)
            : base(message, errorCode, exceptionMessage)
        {
        }
        [DataMember]
        public List<ImportToolBrokerSetting> BrokerList { get; set; }
    }

    [DataContract]
    public class MasterStatementListResponse : JSONResponse
    {
        public MasterStatementListResponse(string message, int errorCode, string exceptionMessage)
            : base(message, errorCode, exceptionMessage)
        {
        }
        [DataMember]
        public List<ImportToolMasterStatementData>  StatementDataList { get; set; }
    }

    [DataContract]
    public class DisplayBrokerListResponse : JSONResponse
    {
        public DisplayBrokerListResponse(string message, int errorCode, string exceptionMessage)
            : base(message, errorCode, exceptionMessage)
        {
        }
        [DataMember]
        public List<DisplayBrokerCode> DisplayBrokerList { get; set; }
    }
}