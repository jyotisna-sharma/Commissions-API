using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyAgencyVault.BusinessLibrary;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.ServiceModel.Dispatcher;
using System.Resources;

namespace MyAgencyVault.WcfService
{
    [ServiceContract]
    interface ITestRest
    {
        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, UriTemplate = "GetDataRest", BodyStyle = WebMessageBodyStyle.WrappedRequest,Method="Post")]
        string GetDataRest(string s);
    }
    public partial class MavService : ITestRest, IErrorHandler
    {
        public string GetDataRest(string s)
        {
            ResourceManager rm = new ResourceManager("MyAgencyVault.WcfService.Strings", System.Reflection.Assembly.GetExecutingAssembly());
            return string.Format("You entered: {0}", rm.GetString("Message"));
        }
    }
}