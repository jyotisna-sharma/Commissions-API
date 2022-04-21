using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace MyAgencyVault.BusinessLibrary.BusinessObjects
{
    [DataContract]
    public class DEUPayorToolFieldObject
    {
        [DataMember]
        public Guid PayorToolId { get; set; }
        [DataMember]
        public Guid PayorFieldID { get; set; }
        [DataMember]
        public string LabelOnField { get; set; }
        [DataMember]
        public string FieldStatusValue { get; set; }
        [DataMember]
        public int FieldOrder { get; set; }
        [DataMember]
        public bool IsPartOfPrimaryKey { get; set; }
        [DataMember]
        public bool IsPopulateIfLinked { get; set; }
        [DataMember]
        public bool IsTabbedToNextFieldIfLinked { get; set; }
        [DataMember]
        public bool IsCalculatedField { get; set; }
        [DataMember]
        public bool IsOverrideOfCalcAllowed { get; set; }
        [DataMember]
        public string DefaultValue { get; set; }
        [DataMember]
        public bool IsZeroorBlankAllowed { get; set; }
        [DataMember]
        public string AllignedDirection { get; set; }
        [DataMember]
        public string FormulaExpression { get; set; }
        [DataMember]
        public string HelpText { get; set; }

        [DataMember]
        public double ControlHeight { get; set; }
        [DataMember]
        public double ControlWidth { get; set; }
        [DataMember]
        public int PTAvailableFieldId { get; set; }
        [DataMember]
        public string AvailableFieldName { get; set; }
        [DataMember]
        public double ControlX { get; set; }
        [DataMember]
        public double ControlY { get; set; }

        [DataMember]
        public string EquivalentIncomingField { get; set; } //Gaurav Needs to be changed
        [DataMember]
        public string EquivalentLearnedField { get; set; } //Gaurav Needs to be changed
        [DataMember]
        public string EquivalentDeuField { get; set; } //Gaurav Needs to be changed

        [DataMember]
        public string CssProperties { get; set; } //Gaurav Needs to be changed


        [DataMember]
        public Guid? TemplateID { get; set; }

        [DataMember]
        public DEUWebMaskFieldType DEUMaskFieldType { get; set; }

    }
    [DataContract]
    public class DEUWebMaskFieldType
    {
        [DataMember]
        public int PTAMAskFieldTypeId { get; set; }
        [DataMember]
        public string MaskTypeName { get; set; }
        [DataMember]
        public string MaskFieldType { get; set; }
        [DataMember]
        public string Prefix { get; set; }
        [DataMember]
        public string Suffix { get; set; }
        [DataMember]
        public bool ShowMasking { get; set; }
        [DataMember]
        public string ThousandSeparator { get; set; }

        [DataMember]
        public byte DEUMaskTypeId { get; set; }

        [DataMember]
        public bool AllowNegativeNumber { get; set; }
    }
    [DataContract]
    public class DEUPayorToolObject
    {
        [DataMember]
        public List<DEUPayorToolFieldObject> ToolFieldList { get; set; }
        [DataMember]
        public string ChequeImageFilePath { get; set; }
        [DataMember]
        public string StatementImageFilePath { get; set; }
        [DataMember]
        public Guid? TemplateId { get; set; }
        [DataMember]
        public Guid PayorId { get; set; }

    }

    [DataContract]
    public class DEUPaymentEntry
    {
        [DataMember]
        public Guid DEUENtryID { get; set; }
        [DataMember]
        public string PolicyNumber { get; set; }
        [DataMember]
        public string ClientName { get; set; }
        [DataMember]
        public string Insured { get; set; }
        [DataMember]
        public string CarrierNickName { get; set; }
        [DataMember]
        public string CoverageNickName { get; set; }
        [DataMember]
        public string PaymentReceived { get; set; }
        [DataMember]
        public int? Units { get; set; }
        [DataMember]
        public string CommissionTotal { get; set; }
        [DataMember]
        public string Fee { get; set; }
        [DataMember]
        public string SplitPercentage { get; set; }
        [DataMember]
        public string CommissionPercentage { get; set; }

        [DataMember]
        public string CreatedDate { get; set; }
        [DataMember]
        public bool isSuccess { get; set; }
        [DataMember]
        public bool isProcessing { get; set; }
        [DataMember]
        public PostCompleteStatusEnum PostStatus { get; set; }
        [DataMember]
        public string CarrierName { get; set; }
        [DataMember]
        public string ProductName { get; set; }
        [DataMember]
        public string EntryDate { get; set; }
        [DataMember]
        public string UnlinkClientName { get; set; }
        [DataMember]
        public Guid? GuidCarrierID { get; set; }
        [DataMember]
        public string InvoiceDate { get; set; }

        [DataMember]
        public string FormattedInvoiceDate { get; set; }
    }
}
