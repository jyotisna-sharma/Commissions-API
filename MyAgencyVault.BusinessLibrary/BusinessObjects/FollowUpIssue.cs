using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace MyAgencyVault.BusinessLibrary.BusinessObjects
{
    public class FollowUpIssue
    {
        DateTime? _lastruns;
        [DataMember]
        public DateTime? LastFollowUpRuns
        {
            get
            {
                return _lastruns;
            }
            set
            {
                _lastruns = value;
                if (value != null && string.IsNullOrEmpty(LastFollowupRunString))
                {
                    LastFollowupRunString = Convert.ToDateTime(value).ToString("MM/dd/yyyy");
                }
            }
        }
        string _lastRunString;
        [DataMember]
        public string LastFollowupRunString
        {
            get
            {
                return _lastRunString;
            }
            set
            {
                _lastRunString = value;
                if (_lastruns == null && !string.IsNullOrEmpty(_lastRunString))
                {
                    DateTime dt;
                    DateTime.TryParse(_lastRunString, out dt);
                    LastFollowUpRuns = dt;
                }
            }
        }
        [DataMember]
        public bool IsTrackPayment { get; set; }
    }
}
