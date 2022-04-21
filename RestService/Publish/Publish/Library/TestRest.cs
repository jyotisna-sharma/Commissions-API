using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyAgencyVault.BusinessLibrary;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.ServiceModel.Dispatcher;

namespace MyAgencyVault.WcfService
{
    [ServiceContract]
    interface ITestRest
    {
        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, UriTemplate = "GetDataRest", BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        string GetDataRest(string s);
    }
    public partial class MavService : ITestRest, IErrorHandler
    {
        public string GetDataRest(string s)
        {
            return string.Format("You entered: {0}", s);
        }
    }
}