using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace MyAgencyVault.BusinessLibrary.BusinessObjects
{
    [DataContract]
    public class ImportToolTemplateDetails
    {
        [DataMember]
        public string FileType { get; set; }
        [DataMember]
        public string FormatType { get; set; }
    }

    [DataContract]
    public class ImportToolInValidFieldList
    {
        [DataMember]
        public string FieldsName { get; set; }
        [DataMember]
        public bool IsFormulaValid { get; set; }
    }
}
