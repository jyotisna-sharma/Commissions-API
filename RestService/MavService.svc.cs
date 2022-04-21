using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.ServiceModel.Dispatcher;
using System.ServiceModel.Channels;
using System.ServiceModel.Activation;
using System.ServiceModel.Configuration;
using System.ServiceModel.Description;
using System.Xml;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.IO;
using MyAgencyVault.WcfService.Library.Response;
using MyAgencyVault.BusinessLibrary;
using System.Net;

namespace MyAgencyVault.WcfService
{

    //[ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession, ConcurrencyMode = ConcurrencyMode.Multiple,IncludeExceptionDetailInFaults=true)]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall, ConcurrencyMode = ConcurrencyMode.Multiple, IncludeExceptionDetailInFaults = true, AddressFilterMode = AddressFilterMode.Any)]
    //[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public partial class MavService : IMavService, IErrorHandler, ITestRest
    {


        public string GetData()
        {
            return string.Format("You entered: {0}", "Hi");
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }




        #region IErrorHandler Members

        public bool HandleError(Exception error)
        {
            return true;
        }

        public void ProvideFault(Exception error, System.ServiceModel.Channels.MessageVersion version, ref System.ServiceModel.Channels.Message fault)
        {
            System.Diagnostics.Debugger.Break();
            if (error is FaultException)
                return;
            var faultException = new FaultException("A General Error Occured");
            MessageFault messageFault = faultException.CreateMessageFault();
            fault = Message.CreateMessage(version, messageFault, null);
        }

        #endregion
    }

    /*namespace WCFHeaderBehavior.Behavior
    {*/
    public class UsernameHeader : MessageHeader
    {
        #region Constants

        /// <summary>
        /// The name of the attribute to which the value of UserName is written
        /// </summary>
        public const string UserAttribute = "userName";

        /// <summary>
        /// The name of the custom message header.
        /// </summary>
        public const string MessageHeaderName = "UserName";

        /// <summary>
        /// The namespace of the custom message header.
        /// </summary>
        public const string MessageHeaderNamespace = "http://wcfheaderbehavior.com/services/username";

        #endregion

        #region Properties

        /// <summary>
        /// Gets the logon name of the user from which the request originated
        /// </summary>
        public string UserName { get; private set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance
        /// </summary>
        /// <param name="userName">The logon name of the user from which the request originated</param>
        public UsernameHeader(string userName)
        {
            if (String.IsNullOrEmpty(userName))
                throw new ArgumentNullException("userName");
            UserName = userName;
        }

        #endregion

        /// <summary>
        /// Gets the name of the message header.
        /// </summary>
        public override string Name
        {
            get { return MessageHeaderName; }
        }

        /// <summary>
        /// Gets the namespace of the message header.
        /// </summary>
        public override string Namespace
        {
            get { return MessageHeaderNamespace; }
        }

        /// <summary>
        /// Called when the header content is serialized using the specified XML writer.
        /// </summary>
        /// <param name="writer">An XmlDictionaryWriter.</param>
        /// <param name="messageVersion">Contains information related to the version of SOAP associated with a message and its exchange.</param>
        protected override void OnWriteHeaderContents(XmlDictionaryWriter writer, MessageVersion messageVersion)
        {
            writer.WriteAttributeString(UserAttribute, UserName);
        }
    }

    public class UsernameServiceBehavior : BehaviorExtensionElement, IServiceBehavior, IDispatchMessageInspector
    {
        /// <summary>
        /// Gets the UsernameHeader
        /// </summary>
        public UsernameHeader UsernameHeader { get; private set; }

        string tokenStatus = string.Empty;
        

                
        //bool isValid = true;
        /// <summary>
        /// Creates a behavior extension based on the current configuration settings.
        /// </summary>
        /// <returns>
        /// The behavior extension.
        /// </returns>
        protected override object CreateBehavior()
        {
            return this;
        }

        /// <summary>
        /// Gets the type of behavior.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Type"/>.
        /// </returns>
        public override Type BehaviorType
        {
            get { return GetType(); }
        }

        /// <summary>
        /// Provides the ability to inspect the service host and the service description to confirm that the service can run successfully.
        /// </summary>
        /// <param name="serviceDescription">The service description.</param><param name="serviceHostBase">The service host that is currently being constructed.</param>
        public void Validate(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {
        }

        /// <summary>
        /// Provides the ability to pass custom data to binding elements to support the contract implementation.
        /// </summary>
        /// <param name="serviceDescription">The service description of the service.</param><param name="serviceHostBase">The host of the service.</param><param name="endpoints">The service endpoints.</param><param name="bindingParameters">Custom objects to which binding elements have access.</param>
        public void AddBindingParameters(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase, Collection<ServiceEndpoint> endpoints, BindingParameterCollection bindingParameters)
        {
        }

        /// <summary>
        /// Provides the ability to change run-time property values or insert custom extension objects such as error handlers, message or parameter interceptors, security extensions, and other custom extension objects.
        /// </summary>
        /// <param name="serviceDescription">The service description.</param><param name="serviceHostBase">The host that is currently being built.</param>
        public void ApplyDispatchBehavior(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {
            foreach (ChannelDispatcher channelDispatcher in serviceHostBase.ChannelDispatchers)
            {
                foreach (var endpointDispatcher in channelDispatcher.Endpoints)
                {
                    endpointDispatcher.DispatchRuntime.MessageInspectors.Add(this);
                }
            }
        }

        /// <summary>
        /// Called after an inbound message has been received but before the message is dispatched to the intended operation.
        /// </summary>
        /// <returns>
        /// The object used to correlate state. This object is passed back in the <see cref="M:System.ServiceModel.Dispatcher.IDispatchMessageInspector.BeforeSendReply(System.ServiceModel.Channels.Message@,System.Object)"/> method.
        /// </returns>
        /// <param name="request">The request message.</param><param name="channel">The incoming channel.</param><param name="instanceContext">The current service instance.</param>
        public object AfterReceiveRequest(ref Message request, IClientChannel channel, InstanceContext instanceContext)
        {

            //List<string> publicURl = System.Configuration.ConfigurationManager.AppSettings["publicURLs"].Split(",").ToList();


            //tokenStatus = string.Empty;
            //string action = (request.Headers.To != null) ? System.IO.Path.GetFileName(request.Headers.To.AbsolutePath).ToLower() : "";

            //IncomingWebRequestContext req = WebOperationContext.Current.IncomingRequest;
            //WebHeaderCollection headers = req.Headers;
            //string token = headers["AuthToken"];

            //if (!string.IsNullOrEmpty(action))
            //{
            //    if (!publicURl.Contains(action) &&  string.IsNullOrEmpty(token)) //When not log-in request, only then validate token
            //    {
            //        //return the request as invalid
            //        // isValid = false;
            //        tokenStatus = "empty";
            //        request = null;
            //        return null;
            //    }
            //    else if (!string.IsNullOrEmpty(token))
            //    {
            //        //Validate token
            //        string name = AuthenticationTokenService.ValidateToken(token);
            //       // isValid = true;
            //        //if (string.IsNullOrEmpty(name) || (!string.IsNullOrEmpty(name) && (name == "invalid" || name == "timeout")))
            //        if(name != "username")
            //        {
            //            tokenStatus = string.IsNullOrEmpty(name) ? "invalid" :  name; //when blank/null, then invalid, else reason
            //            //isValid = false;
            //            request = null;
            //            return null;
            //        }
            //    }

            //}
            return "";
        }

        /// <summary>
        /// Called after the operation has returned but before the reply message is sent.
        /// </summary>
        /// <param name="reply">The reply message. This value is null if the operation is one way.</param><param name="correlationState">The correlation object returned from the <see cref="M:System.ServiceModel.Dispatcher.IDispatchMessageInspector.AfterReceiveRequest(System.ServiceModel.Channels.Message@,System.ServiceModel.IClientChannel,System.ServiceModel.InstanceContext)"/> method.</param>
        public void BeforeSendReply(ref Message reply, object correlationState)
        {
            //if (!isValid)
            //{

                WebOperationContext.Current.OutgoingResponse.Headers.Add("TokenStatus", tokenStatus);
                //reply.Headers.Add(header);  
            //}
            //else
            //{
            //    WebOperationContext.Current.OutgoingResponse.Headers.Add("TokenStatus", "true");
            //}
        }

        private static UsernameHeader ParseHeader(XmlDictionaryReader reader)
        {
            // Check reader position
            if (reader.IsStartElement(UsernameHeader.MessageHeaderName, UsernameHeader.MessageHeaderNamespace))
            {
                // Parse the header
                var originatingUser = reader.GetAttribute(UsernameHeader.UserAttribute);
                if (String.IsNullOrEmpty(originatingUser))
                {
                    throw new FaultException("No originating user provided", FaultCode.CreateSenderFaultCode(new FaultCode("ParseHeader")));
                }

                // Create new header
                return new UsernameHeader(originatingUser);
            }

            return null;
        }
    }
    //}
}
