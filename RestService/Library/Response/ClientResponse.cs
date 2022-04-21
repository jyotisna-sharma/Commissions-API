using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
using MyAgencyVault.BusinessLibrary;

namespace MyAgencyVault.WcfService.Library.Response
{
    [DataContract]
    public class ClientObjectResponse : JSONResponse
    {
        public ClientObjectResponse(string message, int errorCode, string exceptionMessage)
            : base(message, errorCode, exceptionMessage)
        {
        }
        [DataMember]
        public Client ClientObject { get; set; }
    }

    [DataContract]
    public class ClientListResponse : JSONResponse
    {
        public ClientListResponse(string message, int errorCode, string exceptionMessage)
            : base(message, errorCode, exceptionMessage)
        {
        }
        [DataMember]
        public List<ClientListingObject> ClientList { get; set; }

        [DataMember]
        public ClientCountObject ClientsCount { get; set; }
    }
}