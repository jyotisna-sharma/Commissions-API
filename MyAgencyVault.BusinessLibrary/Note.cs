using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyAgencyVault.BusinessLibrary.Base;
using System.Runtime.Serialization;

namespace MyAgencyVault.BusinessLibrary
{
    [DataContract]
    public abstract class Note : IEditable<Note>
    {
        #region IEditable<Note> Members
        public abstract void AddUpdate();
        public abstract void Delete();
        public Note GetOfID()
        {
            throw new NotImplementedException();
        }
        //public abstract bool IsValid();        
        #endregion
        #region "data members aka - public properties."
        [DataMember]
        public Guid NoteID { get; set; }
        [DataMember]
        public Guid PolicyId { get; set; }
        [DataMember]
        public string Content { get; set; }
        [DataMember]
        public string CreatedBy { get; set; }
        [DataMember]
        public string ModifiedBy { get; set; }

   
        DateTime _CreatedDate;
        [DataMember]
        public DateTime CreatedDate
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
                    CreatedDateString = value.ToString("MM/dd/yyyy");
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
        //[DataMember]
        //public DateTime LastModified { get; set; }

        DateTime? _LastModifiedDate;
        [DataMember]
        public DateTime? LastModified
        {
            get
            {
                return _LastModifiedDate;
            }
            set
            {
                _LastModifiedDate = value;
                if (value != null && string.IsNullOrEmpty(LastModifiedDateString))
                {
                    LastModifiedDateString = Convert.ToDateTime(value).ToString("MM/dd/yyyy");
                }
            }
        }
        string _LastModifiedDateString;
        [DataMember]
        public string LastModifiedDateString
        {
            get
            {
                return _LastModifiedDateString;
            }
            set
            {
                _LastModifiedDateString = value;
                if (LastModified == null && !string.IsNullOrEmpty(_LastModifiedDateString))
                {
                    DateTime dt;
                    DateTime.TryParse(_LastModifiedDateString, out dt);
                    LastModified = dt;
                }
            }
        }
        #endregion 
    }
    [DataContract]
    public class UploadedFile
    {
        [DataMember]
        public string FileName { get; set; }
        [DataMember]
        public string UploadedBy { get; set; }
        [DataMember]
        public string UploadedOn { get; set; }
        [DataMember]
        public Guid PolicyId { get; set; }
        [DataMember]
        public Guid PolicyNoteId { get; set; }
        [DataMember]
        public string UploadedFileName { get; set; }
    }
}
