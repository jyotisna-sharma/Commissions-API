using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace MyAgencyVault.BusinessLibrary.BusinessObjects
{
    [DataContract]
    public class BathcListObject
    {
        #region "Data Members aka - |Public properties"
        [DataMember]
        public Guid BatchId { get; set; }
        [DataMember]
        public int BatchNumber { get; set; }

        DateTime? _CreatedDate;
        [DataMember]
        public DateTime? CreatedDate
        {
            get
            {
                return _CreatedDate;
            }
            set
            {
                _CreatedDate = value;
                if (value != null && string.IsNullOrEmpty(CreatedDateString))
                {
                    CreatedDateString = Convert.ToDateTime(value).ToString("MMM dd, yyyy");
                }
            }
        }
        string _CreatedDateString;
        [DataMember]
        public string CreatedDateString
        {
            get
            {
                return _CreatedDateString;
            }
            set
            {
                _CreatedDateString = value;
                if (CreatedDate == null && !string.IsNullOrEmpty(_CreatedDateString))
                {
                    DateTime dt;
                    DateTime.TryParse(_CreatedDateString, out dt);
                    CreatedDate = dt;
                }
            }
        }
        [DataMember]
        public UploadStatus UploadStatus { get; set; }
        [DataMember]
        public EntryStatus EntryStatus { get; set; }
        [DataMember]
        public string  BatchUploadStatus { get; set; }
        [DataMember]
        public string BatchEntryStatus { get; set; }
        [DataMember]
        public List<Statement> BatchStatements { get; set; }

        [DataMember]
        public string BatchNote { get; set; }
        [DataMember]
        public string FileType { get; set; }
        [DataMember]
        public string FileName { get; set; }
        [DataMember]
        public string  Balance { get; set; }
    }
    public class MonthsListData
    {
        public string  MonthName {get;set;}
        public int ? Month { get; set; }
    }
    #endregion
}
