using System;
using System.Runtime.Serialization;


namespace MyAgencyVault.BusinessLibrary
{
    [DataContract]
    public struct UserLoginParams
    {
        
        Guid userID;
        [DataMember]
        public Guid UserID { get => userID; set => userID = value; }

        string appVersion;
        [DataMember]
        public string AppVersion { get => appVersion; set => appVersion = value; }


        string activity;
        [DataMember]
        public string Activity { get => activity; set => activity = value; }

        string clientIP;
        [DataMember]
        public string ClientIP { get => clientIP; set => clientIP = value; }

        string clientBrowser;
        [DataMember]
        public string ClientBrowser { get => clientBrowser; set => clientBrowser = value; }

        string clientBrowserVersion;
        [DataMember]
        public string ClientBrowserVersion { get => clientBrowserVersion; set => clientBrowserVersion = value; }

        string clientOS;
        [DataMember]
        public string ClientOS { get => clientOS; set => clientOS = value; }

        string clientOSVersion;
        [DataMember]
        public string ClientOSVersion { get => clientOSVersion; set => clientOSVersion = value; }

        string clientDeviceDetail;
        [DataMember]
        public string ClientDeviceDetail { get => clientDeviceDetail; set => clientDeviceDetail = value; }

        string userAgentInfo;
        [DataMember]
        public string UserAgentInfo { get => userAgentInfo; set => userAgentInfo = value; }

    }
}