using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
using MyAgencyVault.BusinessLibrary;

namespace MyAgencyVault.WcfService.Library.Response
{
    [DataContract]
    public class JSONResponse
    {
        private string _message;
        private int _errorCode;
        private string _exceptionMessage;
      
      
        public JSONResponse()
        {
            //Empty parameter constructor;
        }
        public JSONResponse(string message, int errorCode, string exceptionMessage)
        {
            this.Message = message;
            this.ResponseCode = errorCode;
            this.ExceptionMessage = exceptionMessage;
           // this.SecondResult = secondResult;
        }
        [DataMember]
        public int ResponseCode
        {
            get { return _errorCode; }
            set { _errorCode = value; }
        }

        [DataMember]
        public string Message
        {
            get { return _message; }
            set { _message = value; }
        }
        [DataMember]
        public string ExceptionMessage
        {
            get { return _exceptionMessage; }
            set { _exceptionMessage = value; }
        }
    }
}