using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyAgencyVault.BusinessLibrary.Base;
using MyAgencyVault.BusinessLibrary.Masters;
using System.Runtime.Serialization;
using DLinq = DataAccessLayer.LinqtoEntity;
using System.Globalization;
using System.Xml.Serialization;
using System.IO;
using System.Data.EntityClient;
using System.Data.SqlClient;
using System.Data;
using MyAgencyVault.BusinessLibrary.BusinessObjects;

namespace MyAgencyVault.BusinessLibrary
{
    [DataContract]
    public enum ExposedDeuField
    {
        [EnumMember]
        PolicyNumber,
        [EnumMember]
        Insured,
        [EnumMember]
        Carrier,
        [EnumMember]
        Product,
        [EnumMember]
        ModelAvgPremium,
        [EnumMember]
        PolicyMode,
        [EnumMember]
        Enrolled,
        [EnumMember]
        SplitPercentage,
        [EnumMember]
        Client,
        [EnumMember]
        CompType,
        [EnumMember]
        PayorSysId,
        [EnumMember]
        Renewal,
        [EnumMember]
        CompScheduleType,
        [EnumMember]
        InvoiceDate,
        [EnumMember]
        PaymentReceived,
        [EnumMember]
        CommissionPercentage,
        [EnumMember]
        NumberOfUnits,
        [EnumMember]
        DollerPerUnit,
        [EnumMember]
        Fee,
        [EnumMember]
        Bonus,
        [EnumMember]
        CommissionTotal,
        [EnumMember]
        OtherData,
        [EnumMember]
        CarrierName,
        [EnumMember]
        ProductName,
        [EnumMember]
        EntryDate,
    }

    [DataContract]
    public class ExposedDEU
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
        public decimal? PaymentRecived { get; set; }
        [DataMember]
        public int? Units { get; set; }
        [DataMember]
        public decimal? CommissionTotal { get; set; }
        [DataMember]
        public decimal? Fee { get; set; }
        [DataMember]
        public double? SplitPercentage { get; set; }
        [DataMember]
        public double? CommissionPercentage { get; set; }

        [DataMember]
        public DateTime? CreatedDate { get; set; }
        [DataMember]
        public PostCompleteStatusEnum PostStatus { get; set; }
        [DataMember]
        public string CarrierName { get; set; }
        [DataMember]
        public string ProductName { get; set; }
        [DataMember]
        public DateTime? EntryDate { get; set; }
        [DataMember]
        public string UnlinkClientName { get; set; }
        [DataMember]
        public Guid? GuidCarrierID { get; set; }
        //[DataMember]
        //public DateTime? InvoiceDate { get; set; }

        DateTime? _InvoiceDate;
        [DataMember]
        public DateTime? InvoiceDate
        {
            get
            {
                return _InvoiceDate;
            }
            set
            {
                _InvoiceDate = value;
                if (value != null && string.IsNullOrEmpty(InvoiceDatestring))
                {
                    InvoiceDatestring = value.ToString();
                }
            }
        }
        string _InvoiceDateString;
        [DataMember]
        public string InvoiceDatestring
        {
            get
            {
                return _InvoiceDateString;
            }
            set
            {
                _InvoiceDateString = value;
                if (InvoiceDate == null && !string.IsNullOrEmpty(_InvoiceDateString))
                {
                    DateTime dt;
                    DateTime.TryParse(_InvoiceDateString, out dt);
                    InvoiceDate = dt;
                }
            }
        }



    }

    [DataContract]
    public class ModifiyableBatchStatementData
    {
        [DataMember]
        public ModifiableBatchData BatchData { get; set; }
        [DataMember]
        public ModifiableStatementData StatementData { get; set; }
        [DataMember]
        public ExposedDEU ExposedDeu { get; set; }
        [DataMember]
        public DeuSearchedPolicy SearchedPolicy { get; set; }
        [DataMember]
        public BasicInformationForProcess BasicInformationForProcessPolicy { get; set; }
    }

    [DataContract]
    public class DEU
    {
        #region DataMember

        [DataMember]
        public Guid DEUENtryID { get; set; }
        [DataMember]
        public bool IsProcessing { get; set; }

        //[DataMember]
        //public DateTime? OriginalEffectiveDate { get; set; }


        DateTime? _OriginalEffectiveDate;
        [DataMember]
        public DateTime? OriginalEffectiveDate
        {
            get
            {
                return _OriginalEffectiveDate;
            }
            set
            {
                _OriginalEffectiveDate = value;
                if (value != null && string.IsNullOrEmpty(OriginalEffectiveDatestring))
                {
                    OriginalEffectiveDatestring = value.ToString();
                }
            }
        }
        string _OriginalEffectiveDateString;
        [DataMember]
        public string OriginalEffectiveDatestring
        {
            get
            {
                return _OriginalEffectiveDateString;
            }
            set
            {
                _OriginalEffectiveDateString = value;
                if (OriginalEffectiveDate == null && !string.IsNullOrEmpty(_OriginalEffectiveDateString))
                {
                    DateTime dt;
                    DateTime.TryParse(_OriginalEffectiveDateString, out dt);
                    OriginalEffectiveDate = dt;
                }
            }
        }

        [DataMember]
        public decimal? PaymentRecived { get; set; }//Premium
        [DataMember]
        public double? CommissionPercentage { get; set; }
        //[DataMember]
        //public decimal CommissionPaid { get; set; }
        [DataMember]
        public XmlFields XmlData { get; set; }
        [DataMember]
        public string Insured { get; set; }//Insured, GroupName
        [DataMember]
        public string PolicyNumber { get; set; }
        [DataMember]
        public string Enrolled { get; set; }
        [DataMember]
        public string Eligible { get; set; }
        [DataMember]
        public string Link1 { get; set; }
        [DataMember]
        public double? SplitPer { get; set; }
        [DataMember]
        public int? PolicyMode { get; set; }

        //[DataMember]
        //public DateTime? TrackFromDate { get; set; }

        DateTime? _TrackFromDate;
        [DataMember]
        public DateTime? TrackFromDate
        {
            get
            {
                return _TrackFromDate;
            }
            set
            {
                _TrackFromDate = value;
                if (value != null && string.IsNullOrEmpty(TrackFromDatestring))
                {
                    TrackFromDatestring = value.ToString();
                }
            }
        }
        string _TrackFromDateString;
        [DataMember]
        public string TrackFromDatestring
        {
            get
            {
                return _TrackFromDateString;
            }
            set
            {
                _TrackFromDateString = value;
                if (TrackFromDate == null && !string.IsNullOrEmpty(_TrackFromDateString))
                {
                    DateTime dt;
                    DateTime.TryParse(_TrackFromDateString, out dt);
                    TrackFromDate = dt;
                }
            }
        }

        [DataMember]
        public string PMC { get; set; }
        [DataMember]
        public string PAC { get; set; }
        [DataMember]
        public string CompScheduleType { get; set; }
        [DataMember]
        public int? CompTypeID { get; set; }
        [DataMember]
        public Guid? ClientID { get; set; }
        [DataMember]
        public Guid? StmtID { get; set; }
        [DataMember]
        public int? PostStatusID { get; set; }
        [DataMember]
        public Guid PolicyId { get; set; }

        //[DataMember]
        //public DateTime? InvoiceDate { get; set; }

        DateTime? _InvoiceDate;
        [DataMember]
        public DateTime? InvoiceDate
        {
            get
            {
                return _InvoiceDate;
            }
            set
            {
                _InvoiceDate = value;
                if (value != null && string.IsNullOrEmpty(InvoiceDatestring))
                {
                    InvoiceDatestring = value.ToString();
                }
            }
        }
        string _InvoiceDateString;
        [DataMember]
        public string InvoiceDatestring
        {
            get
            {
                return _InvoiceDateString;
            }
            set
            {
                _InvoiceDateString = value;
                if (InvoiceDate == null && !string.IsNullOrEmpty(_InvoiceDateString))
                {
                    DateTime dt;
                    DateTime.TryParse(_InvoiceDateString, out dt);
                    InvoiceDate = dt;
                }
            }
        }

        [DataMember]
        public Guid? PayorId { get; set; }
        [DataMember]
        public int? NoOfUnits { get; set; }
        [DataMember]
        public decimal? DollerPerUnit { get; set; }
        [DataMember]
        public decimal? Fee { get; set; }
        [DataMember]
        public decimal? Bonus { get; set; }
        [DataMember]
        public decimal? CommissionTotal { get; set; }
        [DataMember]
        public string PayorSysID { get; set; }
        [DataMember]
        public string Renewal { get; set; }
        [DataMember]
        public Guid? CarrierID { get; set; }
        [DataMember]
        public Guid? CoverageID { get; set; }
        [DataMember]
        public bool IsEntrybyCommissiondashBoard { get; set; }
        [DataMember]
        public string CarrierNickName { get; set; }
        [DataMember]
        public string CoverageNickName { get; set; }
        [DataMember]
        public string ClientName { get; set; }
        [DataMember]
        public Guid? CreatedBy { get; set; }
        [DataMember]
        public int? PostCompleteStatus { get; set; }
        [DataMember]
        public decimal? ModalAvgPremium { get; set; }
        [DataMember]
        public string CarrierName { get; set; }
        [DataMember]
        public string ProductName { get; set; }
        //[DataMember]
        //public DateTime? EntryDate { get; set; }

        DateTime? _EntryDate;
        [DataMember]
        public DateTime? EntryDate
        {
            get
            {
                return _EntryDate;
            }
            set
            {
                _EntryDate = value;
                if (value != null && string.IsNullOrEmpty(EntryDatestring))
                {
                    EntryDatestring = value.ToString();
                }
            }
        }
        string _EntryDateString;
        [DataMember]
        public string EntryDatestring
        {
            get
            {
                return _EntryDateString;
            }
            set
            {
                _EntryDateString = value;
                if (EntryDate == null && !string.IsNullOrEmpty(_EntryDateString))
                {
                    DateTime dt;
                    DateTime.TryParse(_EntryDateString, out dt);
                    EntryDate = dt;
                }
            }
        }

        [DataMember]
        public Guid? TemplateID { get; set; }

        [DataMember]
        public string UnlinkClientName { get; set; }

        [DataMember]
        public Guid? GuidCarrierID { get; set; }
        //Load the Policy on which followup procedure is to be done
        Guid PolicyGuidID = new Guid("249AEBFF-32A4-4A10-8563-AB7678383C64");

        [DataContract]
        [XmlRoot]
        public class XmlFields
        {
            public List<DataEntryField> FieldCollection { get; set; }

            public XmlFields()
            {
                FieldCollection = new List<DataEntryField>();
            }

            public void Add(DataEntryField field)
            {
                FieldCollection.Add(field);
            }
        }


        #endregion

        #region Methods
        public static DEU GetLatestInvoiceForDEU(Guid policyId)
        {
            DEU _DEU = null;
            try
            {
                using (SqlConnection con = new SqlConnection(DBConnection.GetConnectionString()))
                {
                    using (SqlCommand cmd = new SqlCommand("usp_GetLatestInvoiceDateRecord", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@policyID", policyId);
                        con.Open();
                        SqlDataReader dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {
                            _DEU = new DEU();
                            while (dr.Read())
                            {
                                _DEU.PolicyId = (Guid)dr["PolicyID"];
                                _DEU.ClientID = (Guid)dr["ClientID"];
                                _DEU.Insured = Convert.ToString(dr["Insured"]);
                                _DEU.PolicyNumber = Convert.ToString(dr["PolicyNumber"]);
                                _DEU.CoverageNickName = Convert.ToString(dr["CoverageNickName"]);
                                _DEU.CarrierName = Convert.ToString(dr["CarrierName"]);
                                _DEU.Enrolled = Convert.ToString(dr["Enrolled"]);
                                _DEU.Eligible = Convert.ToString(dr["Eligible"]);
                                _DEU.Renewal = Convert.ToString(dr["Renewal"]);
                                _DEU.Link1 = Convert.ToString(dr["Link1"]);
                                _DEU.CompScheduleType = Convert.ToString(dr["CompScheduleType"]);
                                _DEU.PayorSysID = Convert.ToString(dr["PayorSysID"]);
                                _DEU.OriginalEffectiveDate = dr.IsDBNull("OriginalEffectiveDate") ? (DateTime?)null : (DateTime)dr["OriginalEffectiveDate"];
                                _DEU.CoverageID = dr.IsDBNull("CoverageID") ? (Guid?)null : (Guid)dr["CoverageID"];
                                _DEU.CarrierID = dr.IsDBNull("CarrierID") ? (Guid?)null : (Guid)dr["CarrierID"];
                                _DEU.CreatedBy = dr.IsDBNull("CreatedBy") ? (Guid?)null : (Guid)dr["CreatedBy"];
                                _DEU.PolicyMode = dr.IsDBNull("PolicyModeID") ? (int?)null : (int)dr["PolicyModeID"];
                                _DEU.CompTypeID = dr.IsDBNull("CompTypeID") ? (int?)null : (int)dr["CompTypeID"];
                                _DEU.SplitPer = dr.IsDBNull("SplitPer") ? (double?)null : (double)dr["SplitPer"];
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ActionLogger.Logger.WriteLog(DateTime.Now.ToString() + "DeletePaymentAndIssues exception: " + ex.Message, true);
                throw ex;
            }
            return _DEU;
        }
        public DEU GetLatestInvoiceDateRecord(Guid PolicyId)
        {
            DEU _DEU = new DEU();
            try
            {
                using (DLinq.CommissionDepartmentEntities DataModel = Entity.DataModel)
                {
                    var _DEUEntryies = (from f in DataModel.EntriesByDEUs
                                        where (f.PolicyID == PolicyId)
                                        select f).ToList<DLinq.EntriesByDEU>();
                    if (_DEUEntryies == null || _DEUEntryies.Count == 0) return null;
                    DateTime? maxDt = _DEUEntryies.Max(p => p.InvoiceDate);   // Change the Invoice Period To Invioce Date then uncommented

                    var _DEUEntry = _DEUEntryies.Where(p => p.InvoiceDate == maxDt).FirstOrDefault();

                    _DEU.DEUENtryID = _DEUEntry.DEUEntryID;
                    _DEU.OriginalEffectiveDate = _DEUEntry.OriginalEffectiveDate;
                    _DEU.PaymentRecived = _DEUEntry.PaymentReceived;
                    _DEU.CommissionPercentage = _DEUEntry.CommissionPercentage;
                    _DEU.Insured = _DEUEntry.Insured;
                    _DEU.PolicyNumber = _DEUEntry.PolicyNumber;
                    _DEU.Enrolled = _DEUEntry.Enrolled;
                    _DEU.Eligible = _DEUEntry.Eligible;
                    _DEU.Link1 = _DEUEntry.Link1;
                    _DEU.SplitPer = _DEUEntry.SplitPer;
                    _DEU.PolicyMode = _DEUEntry.PolicyModeID;
                    _DEU.TrackFromDate = _DEUEntry.TrackFromDate;
                    _DEU.CompScheduleType = _DEUEntry.CompScheduleType;
                    _DEU.CompTypeID = _DEUEntry.CompTypeID;
                    _DEU.ClientID = _DEUEntry.ClientID;
                    _DEU.StmtID = _DEUEntry.StatementID;
                    _DEU.PostStatusID = _DEUEntry.PostStatusID;
                    _DEU.PolicyId = _DEUEntry.PolicyID ?? Guid.Empty;
                    _DEU.InvoiceDate = _DEUEntry.InvoiceDate;
                    _DEU.PayorId = _DEUEntry.PayorId;
                    _DEU.NoOfUnits = _DEUEntry.NumberOfUnits;
                    _DEU.DollerPerUnit = _DEUEntry.DollerPerUnit;
                    _DEU.Fee = _DEUEntry.Fee;
                    _DEU.Bonus = _DEUEntry.Bonus;
                    _DEU.CommissionTotal = _DEUEntry.CommissionTotal;
                    _DEU.PayorSysID = _DEUEntry.PayorSysID;
                    _DEU.Renewal = _DEUEntry.Renewal;
                    _DEU.CarrierID = _DEUEntry.CarrierId;
                    _DEU.CoverageID = _DEUEntry.CoverageId;
                    _DEU.IsEntrybyCommissiondashBoard = _DEUEntry.IsEntrybyCommissiondashBoard ?? false;
                    _DEU.CreatedBy = _DEUEntry.CreatedBy;
                    _DEU.PostCompleteStatus = _DEUEntry.PostCompleteStatus;

                    _DEU.CarrierName = _DEUEntry.CarrierName;
                    _DEU.ProductName = _DEUEntry.ProductName;
                    _DEU.EntryDate = _DEUEntry.EntryDate;
                }
            }
            catch (Exception ex)
            {
                ActionLogger.Logger.WriteLog("GetLatestInvoiceDateRecord :" + ex.StackTrace.ToString(), true);
                ActionLogger.Logger.WriteLog("GetLatestInvoiceDateRecord :" + ex.InnerException.ToString(), true);
            }

            return _DEU;

        }


        public static DEU GetDEULatestInvoiceDateRecord(Guid DeuID)
        {
            DEU _DEU = new DEU();
            try
            {
                using (DLinq.CommissionDepartmentEntities DataModel = Entity.DataModel)
                {
                    var _DEUEntryies = (from f in DataModel.EntriesByDEUs
                                        where (f.DEUEntryID == DeuID)
                                        select f).ToList<DLinq.EntriesByDEU>();
                    if (_DEUEntryies == null || _DEUEntryies.Count == 0) return null;

                    var _DEUEntry = _DEUEntryies.OrderBy(p => p.InvoiceDate).FirstOrDefault();

                    _DEU.DEUENtryID = _DEUEntry.DEUEntryID;
                    _DEU.OriginalEffectiveDate = _DEUEntry.OriginalEffectiveDate;
                    _DEU.PaymentRecived = _DEUEntry.PaymentReceived;
                    _DEU.CommissionPercentage = _DEUEntry.CommissionPercentage;
                    _DEU.Insured = _DEUEntry.Insured;
                    _DEU.PolicyNumber = _DEUEntry.PolicyNumber;
                    _DEU.Enrolled = _DEUEntry.Enrolled;
                    _DEU.Eligible = _DEUEntry.Eligible;
                    _DEU.Link1 = _DEUEntry.Link1;
                    _DEU.SplitPer = _DEUEntry.SplitPer;
                    _DEU.PolicyMode = _DEUEntry.PolicyModeID;
                    _DEU.TrackFromDate = _DEUEntry.TrackFromDate;
                    _DEU.CompScheduleType = _DEUEntry.CompScheduleType;
                    _DEU.CompTypeID = _DEUEntry.CompTypeID;
                    _DEU.ClientID = _DEUEntry.ClientID;
                    _DEU.StmtID = _DEUEntry.StatementID;
                    _DEU.PostStatusID = _DEUEntry.PostStatusID;
                    _DEU.PolicyId = _DEUEntry.PolicyID ?? Guid.Empty;
                    _DEU.InvoiceDate = _DEUEntry.InvoiceDate;
                    _DEU.PayorId = _DEUEntry.PayorId;
                    _DEU.NoOfUnits = _DEUEntry.NumberOfUnits;
                    _DEU.DollerPerUnit = _DEUEntry.DollerPerUnit;
                    _DEU.Fee = _DEUEntry.Fee;
                    _DEU.Bonus = _DEUEntry.Bonus;
                    _DEU.CommissionTotal = _DEUEntry.CommissionTotal;
                    _DEU.PayorSysID = _DEUEntry.PayorSysID;
                    _DEU.Renewal = _DEUEntry.Renewal;
                    _DEU.CarrierID = _DEUEntry.CarrierId;
                    _DEU.CoverageID = _DEUEntry.CoverageId;
                    _DEU.IsEntrybyCommissiondashBoard = _DEUEntry.IsEntrybyCommissiondashBoard ?? false;
                    _DEU.CreatedBy = _DEUEntry.CreatedBy;
                    _DEU.PostCompleteStatus = _DEUEntry.PostCompleteStatus;

                    _DEU.CarrierName = _DEUEntry.CarrierName;
                    _DEU.ProductName = _DEUEntry.ProductName;
                    _DEU.EntryDate = _DEUEntry.EntryDate;
                }
            }
            catch (Exception ex)
            {
                ActionLogger.Logger.WriteLog("GetDEULatestInvoiceDateRecord :" + ex.StackTrace.ToString(), true);
                ActionLogger.Logger.WriteLog("GetDEULatestInvoiceDateRecord :" + ex.InnerException.ToString(), true);
            }
            return _DEU;

        }

        //public static void AddupdateDeuEntry(DEU _DeuEntry)
        public void AddupdateDeuEntry(DEU _DeuEntry)
        {
            try
            {
                if (_DeuEntry == null)
                {
                    return;
                }
                using (DLinq.CommissionDepartmentEntities DataModel = Entity.DataModel)
                {
                    var _deu = (from p in DataModel.EntriesByDEUs where (p.DEUEntryID == _DeuEntry.DEUENtryID) select p).FirstOrDefault();
                    if (_deu == null)
                    {
                        _deu = new DLinq.EntriesByDEU();
                        _deu.DEUEntryID = _DeuEntry.DEUENtryID;
                        _deu.OriginalEffectiveDate = _DeuEntry.OriginalEffectiveDate;
                        _deu.PaymentReceived = _DeuEntry.PaymentRecived ?? 0;
                        _deu.CommissionPercentage = _DeuEntry.CommissionPercentage ?? 0;
                        _deu.Insured = _DeuEntry.Insured;
                        _deu.PolicyNumber = _DeuEntry.PolicyNumber;
                        _deu.Enrolled = _DeuEntry.Enrolled;
                        _deu.Eligible = _DeuEntry.Eligible;
                        _deu.Link1 = _DeuEntry.Link1;
                        _deu.SplitPer = _DeuEntry.SplitPer ?? 100;
                        _deu.PolicyModeID = _DeuEntry.PolicyMode;
                        _deu.TrackFromDate = _DeuEntry.TrackFromDate;
                        _deu.CompScheduleType = _DeuEntry.CompScheduleType;
                        _deu.CompTypeID = _DeuEntry.CompTypeID;
                        _deu.ClientValue = _DeuEntry.ClientName;
                        _deu.ClientID = _DeuEntry.ClientID;
                        _deu.StatementID = _DeuEntry.StmtID;
                        _deu.PostStatusID = _DeuEntry.PostStatusID;
                        _deu.PolicyID = _DeuEntry.PolicyId;
                        //_deu.PayorPolicyId= _DeuEntry.p
                        _deu.InvoiceDate = _DeuEntry.InvoiceDate;
                        _deu.PayorId = _DeuEntry.PayorId;
                        _deu.NumberOfUnits = _DeuEntry.NoOfUnits ?? 0;
                        _deu.DollerPerUnit = _DeuEntry.DollerPerUnit ?? 0;
                        _deu.Fee = _DeuEntry.Fee ?? 0;
                        _deu.Bonus = _DeuEntry.Bonus ?? 0;
                        _deu.CommissionTotal = _DeuEntry.CommissionTotal ?? 0;
                        _deu.PayorSysID = _DeuEntry.PayorSysID;
                        _deu.Renewal = _DeuEntry.Renewal;
                        _deu.CarrierId = _DeuEntry.CarrierID;
                        _deu.CoverageId = _DeuEntry.CoverageID;
                        _deu.IsEntrybyCommissiondashBoard = _DeuEntry.IsEntrybyCommissiondashBoard;

                        _deu.CreatedBy = _DeuEntry.CreatedBy;
                        _deu.PostCompleteStatus = _DeuEntry.PostCompleteStatus;

                        //Add  column
                        _deu.CoverageNickName = _DeuEntry.CoverageNickName;
                        _deu.CarrierName = _DeuEntry.CarrierName;
                        _deu.CarrierNickName = _DeuEntry.CarrierNickName;
                        _deu.ProductName = _DeuEntry.ProductName;
                        _deu.EntryDate = System.DateTime.Now;

                        DataModel.AddToEntriesByDEUs(_deu);
                    }
                    else
                    {
                        _deu.OriginalEffectiveDate = _DeuEntry.OriginalEffectiveDate;
                        _deu.PaymentReceived = _DeuEntry.PaymentRecived ?? 0;
                        _deu.CommissionPercentage = _DeuEntry.CommissionPercentage ?? 0;
                        _deu.Insured = _DeuEntry.Insured;
                        _deu.PolicyNumber = _DeuEntry.PolicyNumber;
                        _deu.Enrolled = _DeuEntry.Enrolled;
                        _deu.Link1 = _DeuEntry.Link1;
                        _deu.SplitPer = _DeuEntry.SplitPer ?? 100;
                        _deu.PolicyModeID = _DeuEntry.PolicyMode;
                        _deu.TrackFromDate = _DeuEntry.TrackFromDate;
                        _deu.CompScheduleType = _DeuEntry.CompScheduleType;
                        _deu.CompTypeID = _DeuEntry.CompTypeID;
                        _deu.ClientValue = _DeuEntry.ClientName;
                        _deu.ClientID = _DeuEntry.ClientID;
                        _deu.StatementID = _DeuEntry.StmtID;
                        _deu.PostStatusID = _DeuEntry.PostStatusID;
                        _deu.PolicyID = _DeuEntry.PolicyId;
                        //_deu.PayorPolicyId= _DeuEntry.p
                        _deu.NumberOfUnits = _DeuEntry.NoOfUnits ?? 0;
                        _deu.DollerPerUnit = _DeuEntry.DollerPerUnit ?? 0;
                        _deu.Fee = _DeuEntry.Fee ?? 0;
                        _deu.Bonus = _DeuEntry.Bonus ?? 0;
                        _deu.CommissionTotal = _DeuEntry.CommissionTotal ?? 0;
                        _deu.PayorSysID = _DeuEntry.PayorSysID;
                        _deu.Renewal = _DeuEntry.Renewal;
                        _deu.CarrierId = _DeuEntry.CarrierID;
                        _deu.CoverageId = _DeuEntry.CoverageID;
                        _deu.IsEntrybyCommissiondashBoard = _DeuEntry.IsEntrybyCommissiondashBoard;

                        _deu.CreatedBy = _DeuEntry.CreatedBy;

                        _deu.CoverageNickName = _DeuEntry.CoverageNickName;
                        _deu.ProductName = _DeuEntry.ProductName;
                        _deu.CarrierNickName = _DeuEntry.CarrierNickName;
                        _deu.CarrierName = _DeuEntry.CarrierName;

                        _deu.PostCompleteStatus = _DeuEntry.PostCompleteStatus;
                        _deu.IsProcessing = _DeuEntry.IsProcessing;
                    }

                    DataModel.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                ActionLogger.Logger.WriteLog("issue in AddupdateDeuEntry:" + ex.StackTrace.ToString(), true);
                ActionLogger.Logger.WriteLog("issue in AddupdateDeuEntry:" + ex.InnerException.ToString(), true);
                throw ex;
            }
        }

        //public static void AddupdateUnlinkDeuEntry(DEU _DeuEntry, string strClientName)
        public void AddupdateUnlinkDeuEntry(DEU _DeuEntry, string strClientName)
        {
            try
            {
                using (DLinq.CommissionDepartmentEntities DataModel = Entity.DataModel)
                {
                    var _deu = (from p in DataModel.EntriesByDEUs where (p.DEUEntryID == _DeuEntry.DEUENtryID) select p).FirstOrDefault();
                    if (_deu == null)
                    {
                        _deu = new DLinq.EntriesByDEU();
                        _deu.DEUEntryID = _DeuEntry.DEUENtryID;
                        _deu.OriginalEffectiveDate = _DeuEntry.OriginalEffectiveDate;
                        _deu.PaymentReceived = _DeuEntry.PaymentRecived ?? 0;
                        _deu.CommissionPercentage = _DeuEntry.CommissionPercentage ?? 0;
                        _deu.Insured = _DeuEntry.Insured;
                        _deu.PolicyNumber = _DeuEntry.PolicyNumber;
                        _deu.Enrolled = _DeuEntry.Enrolled;
                        _deu.Eligible = _DeuEntry.Eligible;
                        _deu.Link1 = _DeuEntry.Link1;
                        _deu.SplitPer = _DeuEntry.SplitPer ?? 100;
                        _deu.PolicyModeID = _DeuEntry.PolicyMode;
                        _deu.TrackFromDate = _DeuEntry.TrackFromDate;
                        _deu.CompScheduleType = _DeuEntry.CompScheduleType;
                        _deu.CompTypeID = _DeuEntry.CompTypeID;
                        _deu.ClientValue = strClientName;
                        _deu.ClientID = _DeuEntry.ClientID;
                        _deu.StatementID = _DeuEntry.StmtID;
                        _deu.PostStatusID = _DeuEntry.PostStatusID;
                        _deu.PolicyID = _DeuEntry.PolicyId;
                        //_deu.PayorPolicyId= _DeuEntry.p
                        _deu.InvoiceDate = _DeuEntry.InvoiceDate;
                        _deu.PayorId = _DeuEntry.PayorId;
                        _deu.NumberOfUnits = _DeuEntry.NoOfUnits ?? 0;
                        _deu.DollerPerUnit = _DeuEntry.DollerPerUnit ?? 0;
                        _deu.Fee = _DeuEntry.Fee ?? 0;
                        _deu.Bonus = _DeuEntry.Bonus ?? 0;
                        _deu.CommissionTotal = _DeuEntry.CommissionTotal ?? 0;
                        _deu.PayorSysID = _DeuEntry.PayorSysID;
                        _deu.Renewal = _DeuEntry.Renewal;
                        _deu.CarrierId = _DeuEntry.CarrierID;
                        _deu.CoverageId = _DeuEntry.CoverageID;
                        _deu.IsEntrybyCommissiondashBoard = _DeuEntry.IsEntrybyCommissiondashBoard;

                        _deu.CreatedBy = _DeuEntry.CreatedBy;
                        _deu.PostCompleteStatus = _DeuEntry.PostCompleteStatus;

                        //Add  column
                        _deu.CoverageNickName = _DeuEntry.CoverageNickName;
                        _deu.ProductName = _DeuEntry.ProductName;
                        _deu.CarrierNickName = _DeuEntry.CarrierNickName;
                        _deu.CarrierName = _DeuEntry.CarrierName;
                        _deu.EntryDate = System.DateTime.Now;

                        //Oldest Client Name
                        _deu.UnlinkClientName = strClientName;

                        DataModel.AddToEntriesByDEUs(_deu);
                    }
                    else
                    {
                        _deu.OriginalEffectiveDate = _DeuEntry.OriginalEffectiveDate;
                        _deu.PaymentReceived = _DeuEntry.PaymentRecived ?? 0;
                        _deu.CommissionPercentage = _DeuEntry.CommissionPercentage ?? 0;
                        _deu.Insured = _DeuEntry.Insured;
                        _deu.PolicyNumber = _DeuEntry.PolicyNumber;
                        _deu.Enrolled = _DeuEntry.Enrolled;
                        _deu.Link1 = _DeuEntry.Link1;
                        _deu.SplitPer = _DeuEntry.SplitPer ?? 100;
                        _deu.PolicyModeID = _DeuEntry.PolicyMode;
                        _deu.TrackFromDate = _DeuEntry.TrackFromDate;
                        _deu.CompScheduleType = _DeuEntry.CompScheduleType;
                        _deu.CompTypeID = _DeuEntry.CompTypeID;
                        _deu.ClientValue = _DeuEntry.ClientName;
                        _deu.ClientID = _DeuEntry.ClientID;
                        _deu.StatementID = _DeuEntry.StmtID;
                        _deu.PostStatusID = _DeuEntry.PostStatusID;
                        _deu.PolicyID = _DeuEntry.PolicyId;
                        //_deu.PayorPolicyId= _DeuEntry.p
                        _deu.NumberOfUnits = _DeuEntry.NoOfUnits ?? 0;
                        _deu.DollerPerUnit = _DeuEntry.DollerPerUnit ?? 0;
                        _deu.Fee = _DeuEntry.Fee ?? 0;
                        _deu.Bonus = _DeuEntry.Bonus ?? 0;
                        _deu.CommissionTotal = _DeuEntry.CommissionTotal ?? 0;
                        _deu.PayorSysID = _DeuEntry.PayorSysID;
                        _deu.Renewal = _DeuEntry.Renewal;
                        _deu.CarrierId = _DeuEntry.CarrierID;
                        _deu.CoverageId = _DeuEntry.CoverageID;
                        _deu.IsEntrybyCommissiondashBoard = _DeuEntry.IsEntrybyCommissiondashBoard;

                        _deu.CreatedBy = _DeuEntry.CreatedBy;

                        _deu.CoverageNickName = _DeuEntry.CoverageNickName;
                        _deu.ProductName = _DeuEntry.ProductName;
                        _deu.CarrierNickName = _DeuEntry.CarrierNickName;
                        _deu.CarrierName = _DeuEntry.CarrierName;

                        _deu.PostCompleteStatus = _DeuEntry.PostCompleteStatus;
                    }

                    DataModel.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                ActionLogger.Logger.WriteLog("AddupdateUnlinkDeuEntry:" + ex.StackTrace.ToString(), true);
                ActionLogger.Logger.WriteLog("AddupdateUnlinkDeuEntry:" + ex.InnerException.ToString(), true);
            }
        }
        /// <summary>
        /// This method is used to Add/Update the DEU data.
        /// </summary>
        /// <param name="deuFields"></param>
        /// <returns>True is data is added or false if updated</returns>
        //public static ModifiyableBatchStatementData AddUpdate(DEUFields deuFields, Guid oldDeuEntryID)
        public Guid AddUpdate(DEUFields deuFields, Guid oldDeuEntryID)
        {
            Guid Id = Guid.Empty;
            bool IsAddCase = false;
            //  ModifiyableBatchStatementData batchStatementData = new ModifiyableBatchStatementData();
            try
            {
                //JS - MAy 25, 2020 - commented the following being a hard-coded policyID check, no logic  

                //PolicyDetailsData TempPolicyDetailsData = PostUtill.GetPolicy(PolicyGuidID);
                //if (TempPolicyDetailsData != null)
                //{
                //    DateTime? dtTempTime = TempPolicyDetailsData.TrackFromDate;
                //    if (dtTempTime == null)
                //    {
                //        //return batchStatementData;
                //        throw new Exception("Track From date cannot be null");
                //    }
                //}

                using (DLinq.CommissionDepartmentEntities DataModel = Entity.DataModel)
                {
                    DataModel.CommandTimeout = 600000000;

                    DLinq.EntriesByDEU DeuEntry = null;
                    if (deuFields.DeuEntryId == Guid.Empty)
                    {
                        DeuEntry = new DLinq.EntriesByDEU();
                        DeuEntry.DEUEntryID = Guid.NewGuid();
                        DeuEntry.StatementID = deuFields.StatementId;
                        // DeuEntry.Statement = DataModel.Statements.FirstOrDefault(s => s.StatementId == DeuEntry.StatementID);
                        DeuEntry.PayorId = deuFields.PayorId;
                        // DeuEntry.Payor = DataModel.Payors.FirstOrDefault(s => s.PayorId == DeuEntry.PayorId);
                        DeuEntry.CreatedBy = deuFields.CurrentUser;
                        DeuEntry.IsCreatedFromWeb = true;

                        DEU DENew = GetDeuEntrytDateRecord(oldDeuEntryID);
                        if (DENew != null)
                        {
                            if (DENew.EntryDate == null)
                                DeuEntry.EntryDate = System.DateTime.Now;
                            else
                                DeuEntry.EntryDate = DENew.EntryDate;
                        }
                        else
                        {
                            DeuEntry.EntryDate = System.DateTime.Now;
                        }
                        IsAddCase = true;
                     //   DeuEntry.EntryDate = System.DateTime.Now;

                    }
                    else
                    {
                        //Update DeuEntry case
                        DeuEntry = DataModel.EntriesByDEUs.FirstOrDefault(s => s.DEUEntryID == oldDeuEntryID);
                    }

                    Guid? carrierId = null;
                    Guid? coverageId = null;
                    XmlFields xmlFieldCollection = null;
                    DEU deuData = new DEU();
                    DeuEntry.PaymentReceived = 0;
                    DeuEntry.CommissionPercentage = 0;
                    DeuEntry.NumberOfUnits = 0;
                    DeuEntry.DollerPerUnit = 0;
                    DeuEntry.Fee = 0;
                    DeuEntry.Bonus = 0;
                    DeuEntry.CommissionTotal = 0;
                    DeuEntry.SplitPer = 100;

                    List<Carrier> AllCarriersInPayor = new List<Carrier>();
                    if (DeuEntry != null)
                    {
                        if (DeuEntry.PayorId != null)
                        {
                            AllCarriersInPayor = Carrier.GetPayorCarriers((Guid)DeuEntry.PayorId);
                        }

                        if (AllCarriersInPayor.Count == 1)
                        {
                            string strNickName = AllCarriersInPayor.FirstOrDefault().NickName;

                            if (deuFields.PayorId != null)
                            {
                                carrierId = BLHelper.GetCarrierId(strNickName, deuFields.PayorId.Value);
                                if (carrierId != null)
                                {
                                    DeuEntry.CarrierId = carrierId;
                                    deuData.CarrierID = DeuEntry.CarrierId;
                                }
                            }
                        }
                    }

                    foreach (DataEntryField field in deuFields.DeuFieldDataCollection)
                    {
                        switch (field.DeuFieldName)
                        {
                            case "PolicyNumber":
                                //Removes all special characters and trims value to 50 chars
                                DeuEntry.PolicyNumber = BLHelper.CorrectPolicyNo(field.DeuFieldValue);
                                try
                                {
                                    string strValuePolicy = DeuEntry.PolicyNumber.Substring(0, 49);
                                    DeuEntry.PolicyNumber = strValuePolicy;
                                }
                                catch
                                {
                                }
                                deuData.PolicyNumber = DeuEntry.PolicyNumber;
                                if (string.IsNullOrEmpty(DeuEntry.ClientValue))//If no client then use policy# for client name 
                                {
                                    DeuEntry.ClientValue = DeuEntry.PolicyNumber;
                                    deuData.ClientName = DeuEntry.PolicyNumber;

                                    DeuEntry.UnlinkClientName = DeuEntry.PolicyNumber;
                                    deuData.UnlinkClientName = DeuEntry.PolicyNumber;
                                }

                                break;

                            case "ModelAvgPremium":
                                if (!string.IsNullOrEmpty(field.DeuFieldValue))
                                    DeuEntry.ModalAvgPremium = field.DeuFieldValue;

                                decimal value = 0;
                                if (decimal.TryParse(DeuEntry.ModalAvgPremium, out value))
                                    deuData.ModalAvgPremium = value;

                                break;

                            case "Insured":
                                try
                                {
                                    if (AllCapitals(field.DeuFieldValue))
                                    {
                                        DeuEntry.Insured = FirstCharIsCapital(field.DeuFieldValue);
                                        string strInsuredValue = DeuEntry.Insured.Substring(0, 49);
                                        DeuEntry.Insured = strInsuredValue;
                                    }
                                    else
                                    {

                                        DeuEntry.Insured = field.DeuFieldValue;
                                        string strInsuredValue = DeuEntry.Insured.Substring(0, 49);
                                        DeuEntry.Insured = strInsuredValue;
                                    }

                                    deuData.Insured = DeuEntry.Insured;
                                }
                                catch
                                {
                                }
                                break;

                            case "OriginalEffectiveDate":
                                try
                                {
                                    DateTime date1 = DateTime.ParseExact(field.DeuFieldValue, "MM/dd/yyyy", DateTimeFormatInfo.InvariantInfo);
                                    DeuEntry.OriginalEffectiveDate = date1;
                                    deuData.OriginalEffectiveDate = DeuEntry.OriginalEffectiveDate;
                                }
                                catch
                                {
                                    try
                                    {
                                        deuData.OriginalEffectiveDate = Convert.ToDateTime(field.DeuFieldValue);
                                    }
                                    catch
                                    {
                                    }
                                }
                                break;

                            case "InvoiceDate":
                                try
                                {
                                    DateTime date2 = DateTime.ParseExact(field.DeuFieldValue, "MM/dd/yyyy", DateTimeFormatInfo.InvariantInfo);
                                    DeuEntry.InvoiceDate = date2;
                                    deuData.InvoiceDate = DeuEntry.InvoiceDate;
                                }
                                catch (Exception ex)
                                {
                                    try
                                    {
                                        deuData.InvoiceDate = Convert.ToDateTime(field.DeuFieldValue);
                                    }
                                    catch
                                    {
                                        ActionLogger.Logger.WriteLog("Wrong InvoiceDate date format :" + ex.StackTrace.ToString(), true);
                                    }

                                }
                                break;


                            case "EffectiveDate":

                                try
                                {
                                    if (!string.IsNullOrEmpty(field.DeuFieldValue))
                                    {
                                        DateTime dateffective = DateTime.ParseExact(field.DeuFieldValue, "MM/dd/yyyy", DateTimeFormatInfo.InvariantInfo);
                                        DeuEntry.OriginalEffectiveDate = dateffective;
                                        deuData.OriginalEffectiveDate = DeuEntry.OriginalEffectiveDate;
                                    }
                                    else
                                    {
                                        DeuEntry.OriginalEffectiveDate = null;
                                        deuData.OriginalEffectiveDate = DeuEntry.OriginalEffectiveDate;
                                    }
                                }
                                catch
                                {
                                    try
                                    {
                                        deuData.OriginalEffectiveDate = Convert.ToDateTime(field.DeuFieldValue);
                                    }
                                    catch
                                    {
                                    }

                                }
                                break;

                            case "PaymentReceived":
                                if (string.IsNullOrEmpty(field.DeuFieldValue))
                                    DeuEntry.PaymentReceived = 0;
                                else
                                {
                                    string strValue = field.DeuFieldValue;
                                    strValue = strValue.Replace("(", "");
                                    strValue = strValue.Replace(")", "");
                                    //DeuEntry.PaymentReceived = decimal.Parse(field.DeuFieldValue);
                                    DeuEntry.PaymentReceived = decimal.Parse(strValue);
                                }
                                deuData.PaymentRecived = DeuEntry.PaymentReceived;
                                break;

                            case "CommissionPercentage":
                                if (string.IsNullOrEmpty(field.DeuFieldValue))
                                    DeuEntry.CommissionPercentage = 0;
                                else
                                    DeuEntry.CommissionPercentage = double.Parse(field.DeuFieldValue);
                                deuData.CommissionPercentage = DeuEntry.CommissionPercentage;
                                break;

                            case "Renewal":
                                DeuEntry.Renewal = field.DeuFieldValue;
                                deuData.Renewal = DeuEntry.Renewal;
                                break;

                            case "Enrolled":
                                DeuEntry.Enrolled = field.DeuFieldValue;
                                deuData.Enrolled = DeuEntry.Enrolled;
                                break;

                            case "Eligible":
                                DeuEntry.Eligible = field.DeuFieldValue;
                                deuData.Eligible = DeuEntry.Eligible;
                                break;

                            case "Link1":
                                DeuEntry.Link1 = field.DeuFieldValue;
                                deuData.Link1 = DeuEntry.Link1;
                                break;

                            case "SplitPercentage":
                                if (string.IsNullOrEmpty(field.DeuFieldValue))
                                    DeuEntry.SplitPer = 100;
                                else
                                    DeuEntry.SplitPer = double.Parse(field.DeuFieldValue);

                                deuData.SplitPer = DeuEntry.SplitPer;
                                break;

                            case "PolicyMode":
                                DeuEntry.PolicyModeID = BLHelper.GetPolicyMode(field.DeuFieldValue);
                                DeuEntry.PolicyModeValue = field.DeuFieldValue;
                                deuData.PolicyMode = DeuEntry.PolicyModeID;
                                break;

                            case "Carrier":
                                if (deuFields.PayorId != null)
                                {
                                    carrierId = BLHelper.GetCarrierId(field.DeuFieldValue, deuFields.PayorId.Value);

                                    if (carrierId != null)
                                    {
                                        DeuEntry.CarrierId = carrierId;
                                        DeuEntry.CarrierNickName = field.DeuFieldValue;
                                        deuData.CarrierID = DeuEntry.CarrierId;
                                    }
                                    //Carrier nick name (Entered by DEU)
                                    DeuEntry.CarrierName = field.DeuFieldValue;
                                }

                                break;

                            case "Product":

                                if (carrierId != null)

                                    coverageId = BLHelper.GetProductId(field.DeuFieldValue, deuFields.PayorId.Value, carrierId.Value);
                                if (coverageId != null)
                                {
                                    DeuEntry.CoverageId = coverageId;
                                    DeuEntry.CoverageNickName = field.DeuFieldValue;
                                    deuData.CoverageID = DeuEntry.CoverageId;
                                }
                                //Product or Coverage nick name (Entered by DEU)
                                DeuEntry.ProductName = field.DeuFieldValue;

                                break;

                            case "PayorSysId":
                                DeuEntry.PayorSysID = field.DeuFieldValue;
                                deuData.PayorSysID = DeuEntry.PayorSysID;
                                break;

                            case "CompScheduleType":
                                if (string.IsNullOrEmpty(field.DeuFieldValue))
                                    DeuEntry.CompScheduleType = null;
                                else
                                    DeuEntry.CompScheduleType = field.DeuFieldValue;
                                deuData.CompScheduleType = DeuEntry.CompScheduleType;

                                break;

                            case "CompType":
                                if (string.IsNullOrEmpty(field.DeuFieldValue))
                                    DeuEntry.CompTypeID = null;
                                else
                                    //DeuEntry.CompTypeID = BLHelper.getCompTypeId(field.DeuFieldValue);
                                    DeuEntry.CompTypeID = BLHelper.getCompTypeIdByName(field.DeuFieldValue);
                                deuData.CompTypeID = DeuEntry.CompTypeID;
                                break;

                            case "Client":
                                bool isCapital = AllCapitals(field.DeuFieldValue);

                                DeuEntry.ClientID = BLHelper.GetClientId(field.DeuFieldValue, deuFields.LicenseeId.Value);

                                if (isCapital)
                                {
                                    //DeuEntry.ClientValue = FirstCharIsCapital(field.DeuFieldValue);
                                    //DeuEntry.UnlinkClientName = DeuEntry.ClientValue;
                                    //deuData.UnlinkClientName = DeuEntry.ClientValue;
                                    try
                                    {
                                        DeuEntry.ClientValue = FirstCharIsCapital(field.DeuFieldValue);
                                        string strValue = DeuEntry.ClientValue.Substring(0, 49);
                                        DeuEntry.ClientValue = strValue;
                                        DeuEntry.UnlinkClientName = strValue;
                                        deuData.UnlinkClientName = strValue;
                                    }
                                    catch
                                    {
                                    }
                                }
                                else
                                {
                                    //DeuEntry.ClientValue = field.DeuFieldValue;
                                    //DeuEntry.UnlinkClientName = field.DeuFieldValue;
                                    //deuData.UnlinkClientName = field.DeuFieldValue;

                                    try
                                    {
                                        DeuEntry.ClientValue = field.DeuFieldValue;
                                        string strValue = DeuEntry.ClientValue.Substring(0, 49);
                                        DeuEntry.ClientValue = strValue;
                                        DeuEntry.UnlinkClientName = strValue;
                                        deuData.UnlinkClientName = strValue;
                                    }
                                    catch
                                    {
                                    }
                                }
                                //Update insured with client name
                                if (string.IsNullOrEmpty(DeuEntry.Insured))
                                {
                                    DeuEntry.Insured = DeuEntry.ClientValue;
                                }



                                deuData.ClientID = DeuEntry.ClientID;

                                break;

                            case "NumberOfUnits":
                                if (string.IsNullOrEmpty(field.DeuFieldValue))
                                    DeuEntry.NumberOfUnits = 0;
                                else
                                {
                                    decimal dCNoOfUnit = Convert.ToDecimal(field.DeuFieldValue);
                                    DeuEntry.NumberOfUnits = Convert.ToInt32(dCNoOfUnit);

                                }
                                deuData.NoOfUnits = DeuEntry.NumberOfUnits;
                                break;

                            case "DollerPerUnit":
                                if (string.IsNullOrEmpty(field.DeuFieldValue))
                                    DeuEntry.DollerPerUnit = 0;
                                else
                                    DeuEntry.DollerPerUnit = decimal.Parse(field.DeuFieldValue);
                                deuData.DollerPerUnit = DeuEntry.DollerPerUnit;
                                break;

                            case "Fee":
                                if (string.IsNullOrEmpty(field.DeuFieldValue))
                                    DeuEntry.Fee = 0;
                                else
                                    DeuEntry.Fee = decimal.Parse(field.DeuFieldValue);
                                deuData.Fee = DeuEntry.Fee;
                                break;

                            case "Bonus":
                                if (string.IsNullOrEmpty(field.DeuFieldValue))
                                    DeuEntry.Bonus = 0;
                                else
                                    DeuEntry.Bonus = decimal.Parse(field.DeuFieldValue);
                                deuData.Bonus = DeuEntry.Bonus;
                                break;

                            case "CommissionTotal":
                                if (string.IsNullOrEmpty(field.DeuFieldValue))
                                    DeuEntry.CommissionTotal = 0;
                                else
                                    DeuEntry.CommissionTotal = decimal.Parse(field.DeuFieldValue);
                                deuData.CommissionTotal = DeuEntry.CommissionTotal;
                                break;

                            default:
                                if (xmlFieldCollection == null)
                                    xmlFieldCollection = new XmlFields();

                                DataEntryField xmlField = new DataEntryField();
                                xmlField.DeuFieldName = field.DeuFieldName;
                                xmlField.DeuFieldValue = field.DeuFieldValue;
                                xmlField.DeuFieldType = field.DeuFieldType;
                                xmlFieldCollection.Add(xmlField);
                                break;
                        }
                    }

                    deuFields.DeuData = deuData;

                    if (xmlFieldCollection != null)
                    {
                        XmlSerializer serializer = new XmlSerializer(typeof(XmlFields));
                        StringWriter stringWriter = new StringWriter();
                        serializer.Serialize(stringWriter, xmlFieldCollection);
                        string xmlObject = stringWriter.ToString();
                        DeuEntry.OtherData = xmlObject;
                    }

                    if (IsAddCase)
                    {
                        DataModel.AddToEntriesByDEUs(DeuEntry);
                    }

                    Id = DeuEntry.DEUEntryID;
                    DeuEntry.IsProcessing = true;
                    DataModel.SaveChanges();
                    ActionLogger.Logger.WriteLog("DEU AddUpdate success ", true);

                    //batchStatementData.BatchData = Batch.CreateModifiableBatchData(DeuEntry.Statement.Batch);
                    /*Batch objBatch = new Batch();
                    batchStatementData.BatchData = objBatch.CreateModifiableBatchData(DeuEntry.Statement.Batch);
                    batchStatementData.StatementData = Statement.CreateModifiableStatementData(DeuEntry.Statement);
                    batchStatementData.ExposedDeu = CreateExposedDEU(DeuEntry);*/
                }
                return Id; //////  JS - May 22, 2020 -  returned entry ID 
            }

            catch (Exception ex)
            {
                ActionLogger.Logger.WriteLog("Issue in function AddUpdate StackTrace " + ex.StackTrace.ToStringDump(), true);
                ActionLogger.Logger.WriteLog("Issue in AddUpdate Message " + ex.Message, true);
                if (ex.InnerException != null)
                {
                    ActionLogger.Logger.WriteLog("Issue in AddUpdate InnerException msg: " + ex.InnerException.Message, true);
                    ActionLogger.Logger.WriteLog("Issue in AddUpdate InnerException detail: " + ex.InnerException.ToStringDump(), true);
                }
                throw ex;
            }
            // return batchStatementData;
        }

        /// This method is used to Add/Update the DEU data.
        /// </summary>
        /// <param name="deuFields"></param>
        /// <returns>True is data is added or false if updated</returns>
        //public static ModifiyableBatchStatementData AddUpdate(DEUFields deuFields, Guid oldDeuEntryID)
        public ModifiyableBatchStatementData AddUpdate_old(DEUFields deuFields, Guid oldDeuEntryID)
        {
            Guid Id = Guid.Empty;
            bool IsAddCase = false;
            ModifiyableBatchStatementData batchStatementData = new ModifiyableBatchStatementData();
            try
            {
                PolicyDetailsData TempPolicyDetailsData = PostUtill.GetPolicy(PolicyGuidID);
                if (TempPolicyDetailsData != null)
                {
                    DateTime? dtTempTime = TempPolicyDetailsData.TrackFromDate;
                    if (dtTempTime == null)
                    {
                        //return batchStatementData;
                        throw new Exception("Track From date cannot be null");
                    }
                }

                using (DLinq.CommissionDepartmentEntities DataModel = Entity.DataModel)
                {
                    DataModel.CommandTimeout = 600000000;

                    DLinq.EntriesByDEU DeuEntry = null;
                    if (deuFields.DeuEntryId == Guid.Empty)
                    {
                        DeuEntry = new DLinq.EntriesByDEU();
                        DeuEntry.DEUEntryID = Guid.NewGuid();
                        DeuEntry.StatementID = deuFields.StatementId;
                        // DeuEntry.Statement = DataModel.Statements.FirstOrDefault(s => s.StatementId == DeuEntry.StatementID);
                        DeuEntry.PayorId = deuFields.PayorId;
                        //   DeuEntry.Payor = DataModel.Payors.FirstOrDefault(s => s.PayorId == DeuEntry.PayorId);
                        DeuEntry.CreatedBy = deuFields.CurrentUser;

                        DEU DENew = GetDeuEntrytDateRecord(oldDeuEntryID);
                        if (DENew != null)
                        {
                            if (DENew.EntryDate == null)
                                DeuEntry.EntryDate = System.DateTime.Now;
                            else
                                DeuEntry.EntryDate = DENew.EntryDate;
                        }
                        else
                        {
                            DeuEntry.EntryDate = System.DateTime.Now;
                        }
                        //IsAddCase = true;

                    }
                    else
                    {
                        //Update DeuEntry case
                        DeuEntry = DataModel.EntriesByDEUs.FirstOrDefault(s => s.DEUEntryID == oldDeuEntryID);
                        IsAddCase = false;
                    }

                    Guid? carrierId = null;
                    Guid? coverageId = null;
                    XmlFields xmlFieldCollection = null;
                    DEU deuData = new DEU();
                    DeuEntry.PaymentReceived = 0;
                    DeuEntry.CommissionPercentage = 0;
                    DeuEntry.NumberOfUnits = 0;
                    DeuEntry.DollerPerUnit = 0;
                    DeuEntry.Fee = 0;
                    DeuEntry.Bonus = 0;
                    DeuEntry.CommissionTotal = 0;
                    DeuEntry.SplitPer = 100;

                    List<Carrier> AllCarriersInPayor = new List<Carrier>();
                    if (DeuEntry != null)
                    {
                        if (DeuEntry.PayorId != null)
                        {
                            AllCarriersInPayor = Carrier.GetPayorCarriers((Guid)DeuEntry.PayorId);
                        }

                        if (AllCarriersInPayor.Count == 1)
                        {
                            string strNickName = AllCarriersInPayor.FirstOrDefault().NickName;

                            if (deuFields.PayorId != null)
                            {
                                carrierId = BLHelper.GetCarrierId(strNickName, deuFields.PayorId.Value);
                                if (carrierId != null)
                                {
                                    DeuEntry.CarrierId = carrierId;
                                    deuData.CarrierID = DeuEntry.CarrierId;
                                }
                            }
                        }
                    }

                    foreach (DataEntryField field in deuFields.DeuFieldDataCollection)
                    {
                        switch (field.DeuFieldName)
                        {
                            case "PolicyNumber":
                                //Removes all special characters and trims value to 50 chars
                                DeuEntry.PolicyNumber = BLHelper.CorrectPolicyNo(field.DeuFieldValue);
                                try
                                {
                                    string strValuePolicy = DeuEntry.PolicyNumber.Substring(0, 49);
                                    DeuEntry.PolicyNumber = strValuePolicy;
                                }
                                catch
                                {
                                }
                                deuData.PolicyNumber = DeuEntry.PolicyNumber;
                                if (string.IsNullOrEmpty(DeuEntry.ClientValue))//If no client then use policy# for client name 
                                {
                                    DeuEntry.ClientValue = DeuEntry.PolicyNumber;
                                    deuData.ClientName = DeuEntry.PolicyNumber;

                                    DeuEntry.UnlinkClientName = DeuEntry.PolicyNumber;
                                    deuData.UnlinkClientName = DeuEntry.PolicyNumber;
                                }

                                break;

                            case "ModelAvgPremium":
                                if (!string.IsNullOrEmpty(field.DeuFieldValue))
                                    DeuEntry.ModalAvgPremium = field.DeuFieldValue;

                                decimal value = 0;
                                if (decimal.TryParse(DeuEntry.ModalAvgPremium, out value))
                                    deuData.ModalAvgPremium = value;

                                break;

                            case "Insured":
                                try
                                {
                                    if (AllCapitals(field.DeuFieldValue))
                                    {
                                        DeuEntry.Insured = FirstCharIsCapital(field.DeuFieldValue);
                                        string strInsuredValue = DeuEntry.Insured.Substring(0, 49);
                                        DeuEntry.Insured = strInsuredValue;
                                    }
                                    else
                                    {

                                        DeuEntry.Insured = field.DeuFieldValue;
                                        string strInsuredValue = DeuEntry.Insured.Substring(0, 49);
                                        DeuEntry.Insured = strInsuredValue;
                                    }

                                    deuData.Insured = DeuEntry.Insured;
                                }
                                catch
                                {
                                }
                                break;

                            case "OriginalEffectiveDate":
                                try
                                {
                                    DateTime date1 = DateTime.ParseExact(field.DeuFieldValue, "MM/dd/yyyy", DateTimeFormatInfo.InvariantInfo);
                                    DeuEntry.OriginalEffectiveDate = date1;
                                    deuData.OriginalEffectiveDate = DeuEntry.OriginalEffectiveDate;
                                }
                                catch
                                {
                                    try
                                    {
                                        deuData.OriginalEffectiveDate = Convert.ToDateTime(field.DeuFieldValue);
                                    }
                                    catch
                                    {
                                    }
                                }
                                break;

                            case "InvoiceDate":
                                try
                                {
                                    DateTime date2 = DateTime.ParseExact(field.DeuFieldValue, "MM/dd/yyyy", DateTimeFormatInfo.InvariantInfo);
                                    DeuEntry.InvoiceDate = date2;
                                    deuData.InvoiceDate = DeuEntry.InvoiceDate;
                                }
                                catch (Exception ex)
                                {
                                    try
                                    {
                                        deuData.InvoiceDate = Convert.ToDateTime(field.DeuFieldValue);
                                    }
                                    catch
                                    {
                                        ActionLogger.Logger.WriteLog("Wrong InvoiceDate date format :" + ex.StackTrace.ToString(), true);
                                    }

                                }
                                break;


                            case "EffectiveDate":

                                try
                                {
                                    if (!string.IsNullOrEmpty(field.DeuFieldValue))
                                    {
                                        DateTime dateffective = DateTime.ParseExact(field.DeuFieldValue, "MM/dd/yyyy", DateTimeFormatInfo.InvariantInfo);
                                        DeuEntry.OriginalEffectiveDate = dateffective;
                                        deuData.OriginalEffectiveDate = DeuEntry.OriginalEffectiveDate;
                                    }
                                    else
                                    {
                                        DeuEntry.OriginalEffectiveDate = null;
                                        deuData.OriginalEffectiveDate = DeuEntry.OriginalEffectiveDate;
                                    }
                                }
                                catch
                                {
                                    try
                                    {
                                        deuData.OriginalEffectiveDate = Convert.ToDateTime(field.DeuFieldValue);
                                    }
                                    catch
                                    {
                                    }

                                }
                                break;

                            case "PaymentReceived":
                                if (string.IsNullOrEmpty(field.DeuFieldValue))
                                    DeuEntry.PaymentReceived = 0;
                                else
                                {
                                    string strValue = field.DeuFieldValue;
                                    strValue = strValue.Replace("(", "");
                                    strValue = strValue.Replace(")", "");
                                    //DeuEntry.PaymentReceived = decimal.Parse(field.DeuFieldValue);
                                    DeuEntry.PaymentReceived = decimal.Parse(strValue);
                                }
                                deuData.PaymentRecived = DeuEntry.PaymentReceived;
                                break;

                            case "CommissionPercentage":
                                if (string.IsNullOrEmpty(field.DeuFieldValue))
                                    DeuEntry.CommissionPercentage = 0;
                                else
                                    DeuEntry.CommissionPercentage = double.Parse(field.DeuFieldValue);
                                deuData.CommissionPercentage = DeuEntry.CommissionPercentage;
                                break;

                            case "Renewal":
                                DeuEntry.Renewal = field.DeuFieldValue;
                                deuData.Renewal = DeuEntry.Renewal;
                                break;

                            case "Enrolled":
                                DeuEntry.Enrolled = field.DeuFieldValue;
                                deuData.Enrolled = DeuEntry.Enrolled;
                                break;

                            case "Eligible":
                                DeuEntry.Eligible = field.DeuFieldValue;
                                deuData.Eligible = DeuEntry.Eligible;
                                break;

                            case "Link1":
                                DeuEntry.Link1 = field.DeuFieldValue;
                                deuData.Link1 = DeuEntry.Link1;
                                break;

                            case "SplitPercentage":
                                if (string.IsNullOrEmpty(field.DeuFieldValue))
                                    DeuEntry.SplitPer = 100;
                                else
                                    DeuEntry.SplitPer = double.Parse(field.DeuFieldValue);

                                deuData.SplitPer = DeuEntry.SplitPer;
                                break;

                            case "PolicyMode":
                                DeuEntry.PolicyModeID = BLHelper.GetPolicyMode(field.DeuFieldValue);
                                DeuEntry.PolicyModeValue = field.DeuFieldValue;
                                deuData.PolicyMode = DeuEntry.PolicyModeID;
                                break;

                            case "Carrier":
                                if (deuFields.PayorId != null)
                                {
                                    carrierId = BLHelper.GetCarrierId(field.DeuFieldValue, deuFields.PayorId.Value);

                                    if (carrierId != null)
                                    {
                                        DeuEntry.CarrierId = carrierId;
                                        DeuEntry.CarrierNickName = field.DeuFieldValue;
                                        deuData.CarrierID = DeuEntry.CarrierId;
                                    }
                                    //Carrier nick name (Entered by DEU)
                                    DeuEntry.CarrierName = field.DeuFieldValue;
                                }

                                break;

                            case "Product":

                                if (carrierId != null)

                                    coverageId = BLHelper.GetProductId(field.DeuFieldValue, deuFields.PayorId.Value, carrierId.Value);
                                if (coverageId != null)
                                {
                                    DeuEntry.CoverageId = coverageId;
                                    DeuEntry.CoverageNickName = field.DeuFieldValue;
                                    deuData.CoverageID = DeuEntry.CoverageId;
                                }
                                //Product or Coverage nick name (Entered by DEU)
                                DeuEntry.ProductName = field.DeuFieldValue;

                                break;

                            case "PayorSysId":
                                DeuEntry.PayorSysID = field.DeuFieldValue;
                                deuData.PayorSysID = DeuEntry.PayorSysID;
                                break;

                            case "CompScheduleType":
                                if (string.IsNullOrEmpty(field.DeuFieldValue))
                                    DeuEntry.CompScheduleType = null;
                                else
                                    DeuEntry.CompScheduleType = field.DeuFieldValue;
                                deuData.CompScheduleType = DeuEntry.CompScheduleType;

                                break;

                            case "CompType":
                                if (string.IsNullOrEmpty(field.DeuFieldValue))
                                    DeuEntry.CompTypeID = null;
                                else
                                    //DeuEntry.CompTypeID = BLHelper.getCompTypeId(field.DeuFieldValue);
                                    DeuEntry.CompTypeID = BLHelper.getCompTypeIdByName(field.DeuFieldValue);
                                deuData.CompTypeID = DeuEntry.CompTypeID;
                                break;

                            case "Client":
                                bool isCapital = AllCapitals(field.DeuFieldValue);

                                DeuEntry.ClientID = BLHelper.GetClientId(field.DeuFieldValue, deuFields.LicenseeId.Value);

                                if (isCapital)
                                {
                                    //DeuEntry.ClientValue = FirstCharIsCapital(field.DeuFieldValue);
                                    //DeuEntry.UnlinkClientName = DeuEntry.ClientValue;
                                    //deuData.UnlinkClientName = DeuEntry.ClientValue;
                                    try
                                    {
                                        DeuEntry.ClientValue = FirstCharIsCapital(field.DeuFieldValue);
                                        string strValue = DeuEntry.ClientValue.Substring(0, 49);
                                        DeuEntry.ClientValue = strValue;
                                        DeuEntry.UnlinkClientName = strValue;
                                        deuData.UnlinkClientName = strValue;
                                    }
                                    catch
                                    {
                                    }
                                }
                                else
                                {
                                    //DeuEntry.ClientValue = field.DeuFieldValue;
                                    //DeuEntry.UnlinkClientName = field.DeuFieldValue;
                                    //deuData.UnlinkClientName = field.DeuFieldValue;

                                    try
                                    {
                                        DeuEntry.ClientValue = field.DeuFieldValue;
                                        string strValue = DeuEntry.ClientValue.Substring(0, 49);
                                        DeuEntry.ClientValue = strValue;
                                        DeuEntry.UnlinkClientName = strValue;
                                        deuData.UnlinkClientName = strValue;
                                    }
                                    catch
                                    {
                                    }
                                }
                                //Update insured with client name
                                if (string.IsNullOrEmpty(DeuEntry.Insured))
                                {
                                    DeuEntry.Insured = DeuEntry.ClientValue;
                                }



                                deuData.ClientID = DeuEntry.ClientID;

                                break;

                            case "NumberOfUnits":
                                if (string.IsNullOrEmpty(field.DeuFieldValue))
                                    DeuEntry.NumberOfUnits = 0;
                                else
                                {
                                    decimal dCNoOfUnit = Convert.ToDecimal(field.DeuFieldValue);
                                    DeuEntry.NumberOfUnits = Convert.ToInt32(dCNoOfUnit);

                                }
                                deuData.NoOfUnits = DeuEntry.NumberOfUnits;
                                break;

                            case "DollerPerUnit":
                                if (string.IsNullOrEmpty(field.DeuFieldValue))
                                    DeuEntry.DollerPerUnit = 0;
                                else
                                    DeuEntry.DollerPerUnit = decimal.Parse(field.DeuFieldValue);
                                deuData.DollerPerUnit = DeuEntry.DollerPerUnit;
                                break;

                            case "Fee":
                                if (string.IsNullOrEmpty(field.DeuFieldValue))
                                    DeuEntry.Fee = 0;
                                else
                                    DeuEntry.Fee = decimal.Parse(field.DeuFieldValue);
                                deuData.Fee = DeuEntry.Fee;
                                break;

                            case "Bonus":
                                if (string.IsNullOrEmpty(field.DeuFieldValue))
                                    DeuEntry.Bonus = 0;
                                else
                                    DeuEntry.Bonus = decimal.Parse(field.DeuFieldValue);
                                deuData.Bonus = DeuEntry.Bonus;
                                break;

                            case "CommissionTotal":
                                if (string.IsNullOrEmpty(field.DeuFieldValue))
                                    DeuEntry.CommissionTotal = 0;
                                else
                                    DeuEntry.CommissionTotal = decimal.Parse(field.DeuFieldValue);
                                deuData.CommissionTotal = DeuEntry.CommissionTotal;
                                break;

                            default:
                                if (xmlFieldCollection == null)
                                    xmlFieldCollection = new XmlFields();

                                DataEntryField xmlField = new DataEntryField();
                                xmlField.DeuFieldName = field.DeuFieldName;
                                xmlField.DeuFieldValue = field.DeuFieldValue;
                                xmlField.DeuFieldType = field.DeuFieldType;
                                xmlFieldCollection.Add(xmlField);
                                break;
                        }
                    }

                    deuFields.DeuData = deuData;

                    if (xmlFieldCollection != null)
                    {
                        XmlSerializer serializer = new XmlSerializer(typeof(XmlFields));
                        StringWriter stringWriter = new StringWriter();
                        serializer.Serialize(stringWriter, xmlFieldCollection);
                        string xmlObject = stringWriter.ToString();
                        DeuEntry.OtherData = xmlObject;
                    }

                    if (IsAddCase)
                    {
                        DataModel.AddToEntriesByDEUs(DeuEntry);
                    }

                    Id = DeuEntry.DEUEntryID;

                    DataModel.SaveChanges();
                    ActionLogger.Logger.WriteLog("DEU AddUpdate success ", true);

                    //batchStatementData.BatchData = Batch.CreateModifiableBatchData(DeuEntry.Statement.Batch);
                    Batch objBatch = new Batch();
                    //   batchStatementData.BatchData = objBatch.CreateModifiableBatchData(DeuEntry.Statement.Batch);
                    //  batchStatementData.StatementData = Statement.CreateModifiableStatementData(DeuEntry.Statement);
                    batchStatementData.ExposedDeu = CreateExposedDEU(DeuEntry);
                }
                //  return Id; //////  JS - May 22, 2020 -  returned entry ID 
            }

            catch (Exception ex)
            {
                ActionLogger.Logger.WriteLog("Issue in function AddUpdate StackTrace " + ex.StackTrace.ToStringDump(), true);
                ActionLogger.Logger.WriteLog("Issue in AddUpdate Message " + ex.Message, true);
                if (ex.InnerException != null)
                {
                    ActionLogger.Logger.WriteLog("Issue in AddUpdate InnerException " + ex.InnerException.ToStringDump(), true);
                }
                throw ex;
            }
            return batchStatementData;
        }

        #region"Font case"

        public static bool AllCapitals(string inputString)
        {
            foreach (char c in inputString)
            {
                if (char.IsLower(c))
                    return false;
            }
            return true;

        }

        public static string FirstCharIsCapital(string stringToModify)
        {

            StringBuilder sb = new StringBuilder();
            try
            {
                if (!string.IsNullOrEmpty(stringToModify))
                {
                    string[] array = stringToModify.Split(' ');

                    for (int i = 0; i < array.Length; i++)
                    {
                        if (!string.IsNullOrEmpty(array[i]))
                        {
                            string firstLetter = array[i].Substring(0, 1);
                            string secondPart = array[i].Substring(1);
                            sb.Append(firstLetter.ToUpper() + secondPart.ToLower() + " ");
                        }

                    }
                }

            }
            catch
            {
            }

            if (sb.Length > 0)
            {
                return sb.ToString().Remove(sb.Length - 1);
            }
            else
            {
                return sb.ToString();
            }

        }

        #endregion

        public static DEU GetDeuEntrytDateRecord(Guid deuEntryID)
        {
            DEU _DEU = new DEU();
            try
            {
                using (DLinq.CommissionDepartmentEntities DataModel = Entity.DataModel)
                {
                    var _DEUEntryies = (from f in DataModel.EntriesByDEUs
                                        where (f.DEUEntryID == deuEntryID)
                                        select f).FirstOrDefault(); //ToList<DLinq.EntriesByDEU>();
                    if (_DEUEntryies == null /*|| _DEUEntryies.Count == 0*/) return null;

                    _DEU.EntryDate = _DEUEntryies.EntryDate;
                    //foreach (var item in _DEUEntryies)
                    //{
                    //    _DEU.EntryDate = item.EntryDate;
                    //    break;
                    //}


                }
            }
            catch (Exception ex)
            {
                ActionLogger.Logger.WriteLog("GetDeuEntrytDateRecord " + ex.Message.ToString(), true);
            }

            return _DEU;

        }

        /// <summary>
        /// Jyotisna
        /// May 26, 2020
        /// To return total entered amount 
        /// </summary>
        /// <param name="deuStatementD"></param>
        /// <param name="DeuEntryId"></param>
        /// <returns></returns>
        public static decimal? UpdateTotalAmountAndEntry(Guid deuStatementD, Guid DeuEntryId)
        {
            decimal? totalEnteredAmount = 0;
            int intTotalEntry = 0;
            try
            {
                using (DLinq.CommissionDepartmentEntities DataModel = Entity.DataModel)
                {
                    DataModel.CommandTimeout = 600000000;

                    var _DEUEntryies = (from f in DataModel.PolicyPaymentEntries
                                        where (f.StatementId == deuStatementD)
                                        select f).ToList<DLinq.PolicyPaymentEntry>();


                    intTotalEntry = _DEUEntryies.Count;

                    if (intTotalEntry > 0)
                    {
                        foreach (var item in _DEUEntryies)
                        {
                            totalEnteredAmount = totalEnteredAmount + item.TotalPayment;
                        }

                        //Update total entry and entry amount
                        DLinq.EntriesByDEU DeuEntry = DataModel.EntriesByDEUs.FirstOrDefault(s => s.DEUEntryID == DeuEntryId);
                        if (DeuEntry != null)
                        {
                            DeuEntry.Statement.Entries = intTotalEntry;
                            DeuEntry.Statement.EnteredAmount = totalEnteredAmount;
                        }
                        DataModel.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                ActionLogger.Logger.WriteLog("UpdateTotalAmountAndEntry ex.StackTrace :" + ex.StackTrace.ToStringDump(), true);
                ActionLogger.Logger.WriteLog("UpdateTotalAmountAndEntry ex.Message:" + ex.Message, true);
            }
            return totalEnteredAmount;
        }

        //public static ModifiyableBatchStatementData UpdateBatchStatementDataOnSuccessfullDeuPost(Guid DeuEntryId, Guid UserId)
        public ModifiyableBatchStatementData UpdateBatchStatementDataOnSuccessfullDeuPost(Guid DeuEntryId, Guid? UserId, out decimal? totalenteredAmount, out int? stmtID, out int? batchStatusID, out string completePer, out int errorCount)
        {
            totalenteredAmount = 0;
            stmtID = 0;
            batchStatusID = 0; completePer = "";
            errorCount = 0;
            using (DLinq.CommissionDepartmentEntities DataModel = Entity.DataModel)
            {
                ModifiyableBatchStatementData batchStatementData = new ModifiyableBatchStatementData();

                try
                {
                    DLinq.EntriesByDEU DeuEntry = DataModel.EntriesByDEUs.FirstOrDefault(s => s.DEUEntryID == DeuEntryId);

                    if (DeuEntry != null)
                    {
                        DeuEntry.Statement.Entries = DeuEntry.Statement.Entries + 1;
                        if (DeuEntry.Statement.Entries >= 1)
                        {
                            DeuEntry.Statement.StatementStatusId = 1;
                            if (DeuEntry.Statement.Batch.Statements.Count >= 1)
                            {
                                DeuEntry.Statement.Batch.EntryStatusId = (int)EntryStatus.InDataEntry;
                                DeuEntry.Statement.Batch.AssignedUserCredentialId = (UserId != null && UserId != Guid.Empty) ? UserId : null;  //UserId - modifeid by Acme to handle null userID
                            }
                        }
                        stmtID = DeuEntry.Statement.StatementStatusId;
                        batchStatusID = DeuEntry.Statement.Batch.EntryStatusId;
                        DeuEntry.Statement.EnteredAmount += (DeuEntry.CommissionTotal ?? 0);
                        DataModel.SaveChanges();
                        //Update total entered amount and entry
                        totalenteredAmount = UpdateTotalAmountAndEntry(DeuEntry.Statement.StatementId, DeuEntryId);
                        completePer = Statement.CalculateCompletePercent(DeuEntry.Statement.CheckAmount ?? 0, DeuEntry.Statement.BalAdj ?? 0, totalenteredAmount).ToString("N", new CultureInfo("en-US"));
                        errorCount = Statement.GetErrorCount(DeuEntry.Statement.StatementId);
                        //batchStatementData.BatchData = Batch.CreateModifiableBatchData(DeuEntry.Statement.Batch);
                       
                        //Jyotisna - May 25, 2020 - Commenetd as not to be used in web version 
                        //Batch objBatch = new Batch();
                        //batchStatementData.BatchData = objBatch.CreateModifiableBatchData(DeuEntry.Statement.Batch);
                        //batchStatementData.StatementData = Statement.CreateModifiableStatementData(DeuEntry.Statement);
                        //batchStatementData.ExposedDeu = CreateExposedDEU(DeuEntry);
                    }
                }
                catch (Exception ex)
                {
                    ActionLogger.Logger.WriteLog("UpdateBatchStatementDataOnSuccessfullDeuPost :" + ex.StackTrace.ToString(), true);
                    ActionLogger.Logger.WriteLog("UpdateBatchStatementDataOnSuccessfullDeuPost :" + ex.InnerException.ToString(), true);
                }

                return batchStatementData;
            }
        }

        //public static void UpdateDeuEntryStatus(Guid entryId, bool IsCompleted)
        //{
        //    using (DLinq.CommissionDepartmentEntities DataModel = Entity.DataModel)
        //    {
        //        DLinq.EntriesByDEU deu = DataModel.EntriesByDEUs.FirstOrDefault(s => s.DEUEntryID == entryId);

        //        if (deu != null)
        //        {
        //            if (IsCompleted)
        //            {
        //                deu.PostCompleteStatus = 3;
        //            }
        //            else
        //            {
        //                DataModel.DeleteObject(deu);
        //            }
        //        }
        //        DataModel.SaveChanges();
        //    }
        //}

        public static DeuSearchedPolicy GetSearchedDeuPolicy(Guid PolicyId)
        {
            throw new NotImplementedException();
        }

        //public static ModifiyableBatchStatementData DeleteDeuEntry(Guid DeuEntryId)
        //{
        //    ModifiyableBatchStatementData batchStatementData = new ModifiyableBatchStatementData();
        //    ActionLogger.Logger.WriteLog("starting delete ModifiyableBatchStatementData DeleteDeuEntry ", true);
        //    try
        //    {
        //        using (DLinq.CommissionDepartmentEntities DataModel = Entity.DataModel)
        //        {
        //            DLinq.EntriesByDEU DeuEntry = DataModel.EntriesByDEUs.FirstOrDefault(s => s.DEUEntryID == DeuEntryId);
        //            if (DeuEntry != null)
        //            {
        //                DeuEntry.Statement.Entries = DeuEntry.Statement.Entries - 1;

        //                if (DeuEntry.Statement.Entries == 0)
        //                {
        //                    DeuEntry.Statement.StatementStatusId = 0;
        //                    DeuEntry.Statement.EnteredAmount = 0;
        //                }
        //                else
        //                {
        //                    DeuEntry.Statement.EnteredAmount -= (DeuEntry.CommissionTotal ?? 0);
        //                }

        //                bool IsBatchStarted = false;
        //                foreach (DLinq.Statement statement in DeuEntry.Statement.Batch.Statements)
        //                {
        //                    if (statement.Entries != 0)
        //                    {
        //                        IsBatchStarted = true;
        //                        break;
        //                    }
        //                }

        //                if (!IsBatchStarted)
        //                {
        //                    DeuEntry.Statement.Batch.EntryStatusId = 4;
        //                }

        //                //Update total entered amount and entry
        //                ActionLogger.Logger.WriteLog("starting UpdateTotalAmountAndEntry DeleteDeuEntry ", true);
        //                UpdateTotalAmountAndEntry(DeuEntry.Statement.StatementId, DeuEntryId);
        //                ActionLogger.Logger.WriteLog("after UpdateTotalAmountAndEntry DeleteDeuEntry ", true);
        //                ActionLogger.Logger.WriteLog("starting CreateModifiableBatchData DeleteDeuEntry ", true);
        //                Batch objBatch = new Batch();
        //                //batchStatementData.BatchData = Batch.CreateModifiableBatchData(DeuEntry.Statement.Batch);
        //                batchStatementData.BatchData = objBatch.CreateModifiableBatchData(DeuEntry.Statement.Batch);
        //                ActionLogger.Logger.WriteLog("Ending CreateModifiableBatchData DeleteDeuEntry ", true);
        //                ActionLogger.Logger.WriteLog("Starting CreateModifiableStatementData DeleteDeuEntry ", true);
        //                batchStatementData.StatementData = Statement.CreateModifiableStatementData(DeuEntry.Statement);
        //                ActionLogger.Logger.WriteLog("Ending CreateModifiableStatementData DeleteDeuEntry ", true);
        //                batchStatementData.ExposedDeu = null;

        //                DataModel.DeleteObject(DeuEntry);
        //                DataModel.SaveChanges();
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ActionLogger.Logger.WriteLog("Issue in DeleteDeuEntry ", true);
        //        ActionLogger.Logger.WriteLog(ex.Message.ToString(), true);
        //    }
        //    return batchStatementData;
        //}

        public static ModifiyableBatchStatementData DeleteDeuEntry(Guid DeuEntryId)
        {
            ModifiyableBatchStatementData batchStatementData = new ModifiyableBatchStatementData();
            try
            {
                using (DLinq.CommissionDepartmentEntities DataModel = Entity.DataModel)
                {
                    DLinq.EntriesByDEU DeuEntry = DataModel.EntriesByDEUs.FirstOrDefault(s => s.DEUEntryID == DeuEntryId);
                    if (DeuEntry != null)
                    {
                        DeuEntry.Statement.Entries = DeuEntry.Statement.Entries - 1;

                        if (DeuEntry.Statement.Entries == 0)
                        {
                            DeuEntry.Statement.StatementStatusId = 0;
                            DeuEntry.Statement.EnteredAmount = 0;
                        }
                        else
                        {
                            DeuEntry.Statement.EnteredAmount -= (DeuEntry.CommissionTotal ?? 0);
                        }

                        bool IsBatchStarted = false;
                        foreach (DLinq.Statement statement in DeuEntry.Statement.Batch.Statements)
                        {
                            if (statement.Entries != 0)
                            {
                                IsBatchStarted = true;
                                break;
                            }
                        }

                        if (!IsBatchStarted)
                        {
                            DeuEntry.Statement.Batch.EntryStatusId = 4;
                        }

                        //Update total entered amount and entry

                        decimal? totalenteredAmount = UpdateTotalAmountAndEntry(DeuEntry.Statement.StatementId, DeuEntryId);
                        Batch objBatch = new Batch();
                        //batchStatementData.BatchData = Batch.CreateModifiableBatchData(DeuEntry.Statement.Batch);
                        batchStatementData.BatchData = objBatch.CreateModifiableBatchData(DeuEntry.Statement.Batch);
                        batchStatementData.StatementData = Statement.CreateModifiableStatementData(DeuEntry.Statement);
                        batchStatementData.ExposedDeu = null;

                        DataModel.DeleteObject(DeuEntry);
                        DataModel.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                ActionLogger.Logger.WriteLog("ModifiyableBatchStatementData DeleteDeuEntry " + ex.InnerException.ToString(), true);
                ActionLogger.Logger.WriteLog("ModifiyableBatchStatementData DeleteDeuEntry " + ex.StackTrace.ToString(), true);
                ActionLogger.Logger.WriteLog("ModifiyableBatchStatementData DeleteDeuEntry " + ex.Message.ToString(), true);
            }
            return batchStatementData;
        }

        //public void DeleteDeuEntryByID(Guid _DeuEntryID)
        //{

        //    if (_DeuEntryID == Guid.Empty)
        //    {
        //        return;
        //    }

        //    try
        //    {
        //        using (DLinq.CommissionDepartmentEntities DataModel = Entity.DataModel)
        //        {
        //            DataModel.CommandTimeout = 360000;
        //            DLinq.EntriesByDEU _deu = (from c in DataModel.EntriesByDEUs
        //                                       where (c.DEUEntryID == _DeuEntryID)
        //                                       select c).FirstOrDefault();

        //            if (_deu != null)
        //            {
        //                DataModel.EntriesByDEUs.DeleteObject(_deu);
        //                DataModel.SaveChanges();                        
        //            }

        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ActionLogger.Logger.WriteLog("Issue while deleting by ID : " + ex.Message.ToString(), true);
        //        ActionLogger.Logger.WriteLog(ex.Message.ToString(), true);
        //    }
        //}


        public void DeleteDeuEntryByID(Guid _DeuEntryID)
        {
            if (_DeuEntryID == Guid.Empty)
            {
                return;
            }
            try
            {
                DLinq.CommissionDepartmentEntities ctx = new DLinq.CommissionDepartmentEntities(); //create your entity object here
                EntityConnection ec = (EntityConnection)ctx.Connection;
                SqlConnection sc = (SqlConnection)ec.StoreConnection; //get the SQLConnection that your entity object would use
                string adoConnStr = sc.ConnectionString;

                using (SqlConnection con = new SqlConnection(adoConnStr))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_deleteDeuEntry", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@DEUEntryID", _DeuEntryID);
                        con.Open();
                        int intCount = cmd.ExecuteNonQuery();
                        con.Close();

                    }
                }
            }
            catch (Exception ex)
            {
                ActionLogger.Logger.WriteLog("DeleteDeuEntryByID : " + ex.InnerException.ToString(), true);
                ActionLogger.Logger.WriteLog("DeleteDeuEntryByID : " + ex.StackTrace.ToString(), true);
            }
        }

        public void DeleteDeuEntryAndPaymentEntryByDeuID(Guid _DeuEntryID)
        {
            try
            {
                using (DLinq.CommissionDepartmentEntities DataModel = Entity.DataModel)
                {
                    DataModel.CommandTimeout = 360000;

                    DLinq.PolicyPaymentEntry _PaymentEntry = (from c in DataModel.PolicyPaymentEntries
                                                              where (c.DEUEntryId == _DeuEntryID)
                                                              select c).FirstOrDefault();

                    if (_PaymentEntry != null)
                    {

                        DataModel.PolicyPaymentEntries.DeleteObject(_PaymentEntry);
                        DataModel.SaveChanges();
                    }

                    DLinq.EntriesByDEU _deu = (from c in DataModel.EntriesByDEUs
                                               where (c.DEUEntryID == _DeuEntryID)
                                               select c).FirstOrDefault();

                    if (_deu != null)
                    {

                        DataModel.EntriesByDEUs.DeleteObject(_deu);
                        DataModel.SaveChanges();
                    }

                }
            }
            catch (Exception ex)
            {
                ActionLogger.Logger.WriteLog("DeleteDeuEntryAndPaymentEntryByDeuID : " + ex.InnerException.ToString(), true);
                ActionLogger.Logger.WriteLog("DeleteDeuEntryAndPaymentEntryByDeuID : " + ex.StackTrace.ToString(), true);
            }
        }


        public ExposedDEU CreateExposedDEU(DLinq.EntriesByDEU deu)
        {
            ExposedDEU exposedDeu = new ExposedDEU();

            try
            {
                exposedDeu.DEUENtryID = deu.DEUEntryID;
                exposedDeu.ClientName = deu.ClientValue;
                exposedDeu.UnlinkClientName = deu.ClientValue;
                exposedDeu.Insured = deu.Insured;
                exposedDeu.PaymentRecived = deu.PaymentReceived;

                PolicyDetailsData pol = (new Policy()).GetPolicyStting(deu.PolicyID ?? Guid.Empty);
                exposedDeu.PolicyNumber = (pol != null && pol.PolicyId != Guid.Empty) ? pol.PolicyNumber : deu.PolicyNumber;

                exposedDeu.Units = deu.NumberOfUnits;
                exposedDeu.InvoiceDate = deu.InvoiceDate;
                exposedDeu.InvoiceDatestring = exposedDeu.InvoiceDate != null ? exposedDeu.InvoiceDate.Value.ToString("MM/dd/yyyy") : "";
                exposedDeu.CommissionTotal = deu.CommissionTotal;
                exposedDeu.Fee = deu.Fee;
                exposedDeu.SplitPercentage = deu.SplitPer;
                exposedDeu.EntryDate = deu.EntryDate;

                exposedDeu.CarrierNickName = Carrier.GetCarrierNickName(deu.PayorId ?? Guid.Empty, deu.CarrierId ?? Guid.Empty);
                if (string.IsNullOrEmpty(exposedDeu.CarrierNickName))
                    exposedDeu.CarrierNickName = deu.CarrierName;

                exposedDeu.CoverageNickName = Coverage.GetCoverageNickName(deu.PayorId ?? Guid.Empty, deu.CarrierId ?? Guid.Empty, deu.CoverageId ?? Guid.Empty);

                if (string.IsNullOrEmpty(exposedDeu.CoverageNickName))
                    exposedDeu.CoverageNickName = deu.ProductName;

                exposedDeu.CarrierName = deu.CarrierName;
                exposedDeu.ProductName = deu.ProductName;

                exposedDeu.PostStatus = (PostCompleteStatusEnum)(deu.PostCompleteStatus ?? 0);
                exposedDeu.CommissionPercentage = deu.CommissionPercentage;
                DLinq.PolicyPaymentEntry entry = deu.PolicyPaymentEntries.FirstOrDefault();
                if (entry != null)
                {
                    exposedDeu.CreatedDate = entry.CreatedOn;
                }
            }
            catch (Exception ex)
            {
                ActionLogger.Logger.WriteLog("CreateExposedDEU " + ex.StackTrace.ToString(), true);
                ActionLogger.Logger.WriteLog("CreateExposedDEU " + ex.InnerException.ToString(), true);
            }
            return exposedDeu;
        }


        public List<DEU> GetDEUPolicyIdWise(Guid Policyid)
        {
            List<DEU> _deu = null;

            try
            {
                using (DLinq.CommissionDepartmentEntities DataModel = Entity.DataModel)
                {
                    _deu = (from f in DataModel.EntriesByDEUs
                            where (f.PolicyID == Policyid)
                            select
      new DEU

      {

          DEUENtryID = f.DEUEntryID,
          OriginalEffectiveDate = f.OriginalEffectiveDate,
          PaymentRecived = f.PaymentReceived ?? 0,
          CommissionPercentage = f.CommissionPercentage ?? 0,
          Insured = f.Insured,
          PolicyNumber = f.PolicyNumber,
          Enrolled = f.Enrolled,
          Eligible = f.Eligible,
          Link1 = f.Link1,
          SplitPer = f.SplitPer,
          PolicyMode = f.PolicyModeID,
          TrackFromDate = f.TrackFromDate,
          CompScheduleType = f.CompScheduleType,
          CompTypeID = f.CompTypeID,
          ClientID = f.ClientID ?? Guid.Empty,
          ClientName = f.Client.Name,
          StmtID = f.StatementID ?? Guid.Empty,
          PostStatusID = f.PostStatusID,
          PolicyId = f.PolicyID ?? Guid.Empty,
          InvoiceDate = f.InvoiceDate,
          PayorId = f.PayorId,
          NoOfUnits = f.NumberOfUnits,
          DollerPerUnit = f.DollerPerUnit ?? 0,
          Fee = f.Fee ?? 0,
          Bonus = f.Bonus ?? 0,
          CommissionTotal = f.CommissionTotal ?? 0,
          PayorSysID = f.PayorSysID,
          Renewal = f.Renewal,
          CarrierID = f.CarrierId,
          CoverageID = f.CoverageId,
          IsEntrybyCommissiondashBoard = f.IsEntrybyCommissiondashBoard ?? false,
          CreatedBy = f.CreatedBy ?? Guid.Empty,
          EntryDate = f.EntryDate,
          CarrierName = f.CarrierName,
          ProductName = f.ProductName,
          PostCompleteStatus = f.PostCompleteStatus ?? 0,
      }
                ).ToList();

                }
            }
            catch (Exception ex)
            {
                ActionLogger.Logger.WriteLog("GetDEUPolicyIdWise :" + ex.StackTrace.ToString(), true);
                ActionLogger.Logger.WriteLog("GetDEUPolicyIdWise :" + ex.InnerException.ToString(), true);
            }
            return _deu;

        }

        public static List<DataEntryField> GetDeuFieldDetails(Guid deuEntryId, Guid payorToolId)
        {
            ActionLogger.Logger.WriteLog("GetDeuFieldDetails:Processing begins with deuEntryId" + deuEntryId, true);
            List<DataEntryField> deuFields = new List<DataEntryField>();
            try
            {
                using (DLinq.CommissionDepartmentEntities DataModel = Entity.DataModel)
                {
                    List<string> _DeuFields = new List<string>();
                    foreach (var field in Enum.GetValues(typeof(ExposedDeuField)))
                    {
                        _DeuFields.Add(field.ToString());
                    }

                    DLinq.EntriesByDEU deuData = DataModel.EntriesByDEUs.FirstOrDefault(s => s.DEUEntryID == deuEntryId);

                    foreach (string field in _DeuFields)
                    {
                        GetDeuFieldData(deuData, field, deuFields, payorToolId);
                    }
                }
            }
            catch (Exception ex)
            {
                ActionLogger.Logger.WriteLog("GetDeuFieldDetails:Exception occurs while processing begins with deuEntryId" + deuEntryId + " " + ex.Message, true);
            }
            return deuFields;
        }

        public static void GetDeuFieldData(DLinq.EntriesByDEU deuData, string FieldName, List<DataEntryField> entries, Guid payorToolId)
        {
            DataEntryField deuField = new DataEntryField();
            try
            {
                switch (FieldName)
                {
                    case "PolicyNumber":
                        deuField.DeuFieldName = FieldName;
                        if (deuData != null)
                            deuField.DeuFieldValue = deuData.PolicyNumber;
                        deuField.DeuFieldType = 3;

                        entries.Add(deuField);

                        break;
                    case "Insured":
                        deuField.DeuFieldName = FieldName;
                        if (deuData != null)
                            deuField.DeuFieldValue = deuData.Insured;
                        deuField.DeuFieldType = 3;
                        entries.Add(deuField);
                        break;
                    case "Carrier":
                        deuField.DeuFieldName = FieldName;

                        if (deuData != null)
                            if (deuData.Carrier != null)
                                deuField.DeuFieldValue = deuData.Carrier.CarrierNickNames.FirstOrDefault(s => s.PayorId == deuData.PayorId).NickName;
                            else
                                //deuField.DeuFieldValue = string.Empty;
                                deuField.DeuFieldValue = deuData.CarrierName; ;

                        deuField.DeuFieldType = 3;
                        entries.Add(deuField);
                        break;
                    case "Product":
                        deuField.DeuFieldName = FieldName;
                        if (deuData != null)
                            if (deuData.Carrier != null && deuData.Coverage != null)
                                deuField.DeuFieldValue = deuData.Coverage.CoverageNickNames.FirstOrDefault(s => s.PayorId == deuData.PayorId && s.CarrierId == deuData.CarrierId).NickName;
                            else
                                //deuField.DeuFieldValue = string.Empty;
                                deuField.DeuFieldValue = deuData.ProductName; ;

                        deuField.DeuFieldType = 3;
                        entries.Add(deuField);
                        break;
                    case "ModelAvgPremium":
                        deuField.DeuFieldName = FieldName;
                        if (deuData != null)
                            deuField.DeuFieldValue = deuData.ModalAvgPremium;
                        deuField.DeuFieldType = 2;
                        entries.Add(deuField);
                        break;
                    case "PolicyMode":
                        deuField.DeuFieldName = FieldName;
                        if (deuData != null)
                            deuField.DeuFieldValue = deuData.PolicyModeValue;
                        deuField.DeuFieldType = 3;
                        entries.Add(deuField);
                        break;
                    case "Enrolled":
                        deuField.DeuFieldName = FieldName;
                        if (deuData != null)
                            deuField.DeuFieldValue = deuData.Enrolled;
                        deuField.DeuFieldType = 2;
                        entries.Add(deuField);
                        break;
                    case "SplitPercentage":
                        deuField.DeuFieldName = FieldName;
                        if (deuData != null)
                            deuField.DeuFieldValue = deuData.SplitPer.ToString();
                        deuField.DeuFieldType = 2;
                        entries.Add(deuField);
                        break;
                    case "Client":
                        deuField.DeuFieldName = FieldName;
                        if (deuData != null)
                            deuField.DeuFieldValue = deuData.ClientValue;
                        deuField.DeuFieldType = 3;
                        entries.Add(deuField);
                        break;
                    case "CompType":
                        deuField.DeuFieldName = FieldName;

                        if (deuData != null)
                            if (deuData.CompTypeID != null)
                                deuField.DeuFieldValue = deuData.CompTypeID.ToString();
                            else
                                deuField.DeuFieldValue = string.Empty;

                        deuField.DeuFieldType = 3;
                        entries.Add(deuField);
                        break;
                    case "PayorSysId":
                        deuField.DeuFieldName = FieldName;
                        if (deuData != null)
                            deuField.DeuFieldValue = deuData.PayorSysID;
                        deuField.DeuFieldType = 3;
                        entries.Add(deuField);
                        break;
                    case "Renewal":
                        deuField.DeuFieldName = FieldName;
                        if (deuData != null)
                            deuField.DeuFieldValue = deuData.Renewal;
                        deuField.DeuFieldType = 3;
                        entries.Add(deuField);
                        break;
                    case "CompScheduleType":
                        deuField.DeuFieldName = FieldName;

                        if (deuData != null)
                            if (deuData.CompScheduleType != null)
                                deuField.DeuFieldValue = deuData.CompScheduleType.ToString();
                            else
                                deuField.DeuFieldValue = string.Empty;

                        deuField.DeuFieldType = 3;
                        entries.Add(deuField);
                        break;
                    case "InvoiceDate":
                        deuField.DeuFieldName = FieldName;

                        if (deuData != null)
                            if (deuData.InvoiceDate != null)
                                deuField.DeuFieldValue = deuData.InvoiceDate.Value.ToString(GetDeuDateMaskType(payorToolId, "InvoiceDate"));
                            else
                                deuField.DeuFieldValue = string.Empty;

                        deuField.DeuFieldType = 1;
                        entries.Add(deuField);
                        break;
                    case "Effective":
                        deuField.DeuFieldName = FieldName;

                        if (deuData != null)
                            if (deuData.OriginalEffectiveDate != null)
                                deuField.DeuFieldValue = deuData.OriginalEffectiveDate.Value.ToString("MM/dd/yyyy");
                            else
                                deuField.DeuFieldValue = string.Empty;

                        deuField.DeuFieldType = 1;
                        entries.Add(deuField);
                        break;

                    case "PaymentReceived":
                        deuField.DeuFieldName = FieldName;

                        if (deuData != null)
                            if (deuData.PaymentReceived != null)
                                deuField.DeuFieldValue = deuData.PaymentReceived.ToString();
                            else
                                deuField.DeuFieldValue = string.Empty;

                        deuField.DeuFieldType = 2;
                        entries.Add(deuField);
                        break;
                    case "CommissionPercentage":
                        deuField.DeuFieldName = FieldName;

                        if (deuData != null)
                            if (deuData.CommissionPercentage != null)
                                deuField.DeuFieldValue = deuData.CommissionPercentage.ToString();
                            else
                                deuField.DeuFieldValue = string.Empty;

                        deuField.DeuFieldType = 2;
                        entries.Add(deuField);
                        break;
                    case "NumberOfUnits":
                        deuField.DeuFieldName = FieldName;

                        if (deuData != null)
                            if (deuData.NumberOfUnits != null)
                                deuField.DeuFieldValue = deuData.NumberOfUnits.ToString();
                            else
                                deuField.DeuFieldValue = string.Empty;

                        deuField.DeuFieldType = 2;
                        entries.Add(deuField);
                        break;
                    case "DollerPerUnit":
                        deuField.DeuFieldName = FieldName;

                        if (deuData != null)
                            if (deuData.DollerPerUnit != null)
                                deuField.DeuFieldValue = deuData.DollerPerUnit.ToString();
                            else
                                deuField.DeuFieldValue = string.Empty;

                        deuField.DeuFieldType = 2;
                        entries.Add(deuField);
                        break;
                    case "Fee":
                        deuField.DeuFieldName = FieldName;

                        if (deuData != null)
                            if (deuData.Fee != null)
                                deuField.DeuFieldValue = deuData.Fee.ToString();
                            else
                                deuField.DeuFieldValue = string.Empty;

                        deuField.DeuFieldType = 2;
                        entries.Add(deuField);
                        break;
                    case "Bonus":
                        deuField.DeuFieldName = FieldName;

                        if (deuData != null)
                            if (deuData.Bonus != null)
                                deuField.DeuFieldValue = deuData.Bonus.ToString();
                            else
                                deuField.DeuFieldValue = string.Empty;

                        deuField.DeuFieldType = 2;
                        entries.Add(deuField);
                        break;
                    case "CommissionTotal":
                        deuField.DeuFieldName = FieldName;

                        if (deuData != null)
                            if (deuData.CommissionTotal != null)
                                deuField.DeuFieldValue = deuData.CommissionTotal.ToString();
                            else
                                deuField.DeuFieldValue = string.Empty;

                        deuField.DeuFieldType = 2;
                        entries.Add(deuField);
                        break;

                    case "EntryDate":
                        deuField.DeuFieldName = FieldName;
                        break;

                    case "OtherData":
                        if (deuData != null)
                        {
                            if (deuData.OtherData != null)
                            {
                                XmlSerializer serializer = new XmlSerializer(typeof(XmlFields));
                                StringReader stringReader = new StringReader(deuData.OtherData);
                                XmlFields xmlFields = (XmlFields)serializer.Deserialize(stringReader);

                                foreach (DataEntryField field in xmlFields.FieldCollection)
                                {
                                    deuField = new DataEntryField();
                                    deuField.DeuFieldName = field.DeuFieldName;
                                    deuField.DeuFieldType = field.DeuFieldType;
                                    deuField.DeuFieldValue = field.DeuFieldValue;
                                    entries.Add(deuField);
                                }
                            }
                        }

                        break;
                }
            }
            catch (Exception ex)
            {
                ActionLogger.Logger.WriteLog("GetDeuFieldData :" + ex.StackTrace.ToString(), true);
                ActionLogger.Logger.WriteLog("GetDeuFieldData :" + ex.InnerException.ToString(), true);
            }

        }
        /// <summary>
        /// Createdby:Ankit Khandelwal
        /// CreatedOn:21-05-2020
        /// Purpose:Get deu mask type for date field
        /// </summary>
        /// <param name="payorToolId"></param>
        /// <param name="fieldName"></param>
        /// <returns></returns>
        public static string GetDeuDateMaskType(Guid payorToolId, string fieldName)
        {
            using (DLinq.CommissionDepartmentEntities DataModel = Entity.DataModel)
            {
                DLinq.PayorToolField fieldData = DataModel.PayorToolFields.FirstOrDefault(s => s.PayorToolId == payorToolId && s.EquivalentDeuField == fieldName);
                return fieldData.MasterPayorToolMaskFieldType.Name;
            }
        }
        public static DEU GetDeuEntryidWise(Guid DeuEntryID)
        {
            if (DeuEntryID == Guid.Empty)
                return null;
            DEU _deu = null;
            try
            {
                using (DLinq.CommissionDepartmentEntities DataModel = Entity.DataModel)
                {
                    DLinq.EntriesByDEU deuEntry = DataModel.EntriesByDEUs.FirstOrDefault(s => s.DEUEntryID == DeuEntryID);
                    Guid? carrierId, coverageId;
                    string strClient = string.Empty;

                    if (deuEntry != null)
                    {
                        if (deuEntry.ClientID != null)
                        {
                            Client objClient = Client.GetClient(deuEntry.ClientID.Value);

                            if (objClient != null)
                            {
                                strClient = objClient.Name;
                            }
                            else
                            {
                                if (string.IsNullOrEmpty(strClient))
                                {
                                    strClient = deuEntry.ClientValue;
                                }
                            }
                        }

                    }

                    if (deuEntry != null)
                    {
                        carrierId = deuEntry.CarrierId;
                        coverageId = deuEntry.CoverageId;
                        _deu = new DEU();
                        _deu.DEUENtryID = deuEntry.DEUEntryID;
                        _deu.OriginalEffectiveDate = deuEntry.OriginalEffectiveDate;
                        _deu.PaymentRecived = deuEntry.PaymentReceived ?? 0;
                        _deu.CommissionPercentage = deuEntry.CommissionPercentage ?? 0;
                        _deu.Insured = deuEntry.Insured;
                        _deu.PolicyNumber = deuEntry.PolicyNumber;
                        _deu.Enrolled = deuEntry.Enrolled;
                        _deu.Eligible = deuEntry.Eligible;
                        _deu.Link1 = deuEntry.Link1;
                        _deu.SplitPer = deuEntry.SplitPer;
                        _deu.PolicyMode = deuEntry.PolicyModeID;
                        _deu.TrackFromDate = deuEntry.TrackFromDate;
                        _deu.CompScheduleType = deuEntry.CompScheduleType;
                        _deu.CompTypeID = deuEntry.CompTypeID;
                        _deu.ClientID = deuEntry.ClientID ?? Guid.Empty;
                        //_deu.ClientName = deuEntry.ClientID != null ? Client.GetClient(deuEntry.ClientID.Value).Name : deuEntry.ClientValue;
                        _deu.ClientName = deuEntry.ClientID != null ? strClient : deuEntry.ClientValue;
                        _deu.StmtID = deuEntry.StatementID ?? Guid.Empty;
                        _deu.PostStatusID = deuEntry.PostStatusID;
                        _deu.PolicyId = deuEntry.PolicyID ?? Guid.Empty;
                        _deu.InvoiceDate = deuEntry.InvoiceDate;
                        _deu.PayorId = deuEntry.PayorId;
                        _deu.NoOfUnits = deuEntry.NumberOfUnits;
                        _deu.DollerPerUnit = deuEntry.DollerPerUnit ?? 0;
                        _deu.Fee = deuEntry.Fee ?? 0;
                        _deu.Bonus = deuEntry.Bonus ?? 0;
                        //_deu.CommissionPaid1 = deuEntry.CommissionPaid1 ?? 0;
                        //_deu.CommissionPaid2 = deuEntry.CommissionPaid2 ?? 0;
                        _deu.CommissionTotal = deuEntry.CommissionTotal ?? 0;
                        //_deu.AmountBilled = deuEntry.BilledAmount ?? 0;
                        _deu.PayorSysID = deuEntry.PayorSysID;
                        _deu.Renewal = deuEntry.Renewal;
                        _deu.CarrierID = deuEntry.CarrierId;
                        _deu.CoverageID = deuEntry.CoverageId;
                        _deu.IsEntrybyCommissiondashBoard = deuEntry.IsEntrybyCommissiondashBoard ?? false;
                        _deu.CreatedBy = deuEntry.CreatedBy ?? Guid.Empty;
                        _deu.PostCompleteStatus = deuEntry.PostCompleteStatus ?? 0;

                        _deu.CarrierName = deuEntry.CarrierName;
                        _deu.ProductName = deuEntry.ProductName;
                        _deu.EntryDate = deuEntry.EntryDate;

                        _deu.UnlinkClientName = deuEntry.UnlinkClientName;

                        if (deuEntry.Statement != null)
                        {
                            _deu.TemplateID = deuEntry.Statement.TemplateID;
                        }

                        if (!string.IsNullOrEmpty(deuEntry.OtherData))
                        {
                            XmlSerializer serializer = new XmlSerializer(typeof(XmlFields));
                            StringReader stringReader = new StringReader(deuEntry.OtherData);
                            XmlFields xmlFields = (XmlFields)serializer.Deserialize(stringReader);
                            _deu.XmlData = xmlFields;
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                ActionLogger.Logger.WriteLog("GetDeuEntryidWise :" + ex.StackTrace.ToString(), true);
                ActionLogger.Logger.WriteLog("GetDeuEntryidWise :" + ex.InnerException.ToString(), true);
            }
            return _deu;
        }

        public static DEU GetDeuEntryidWiseForUnlikingPolicy(Guid DeuEntryID)
        {
            if (DeuEntryID == Guid.Empty)
                return null;
            DEU _deu = null;
            try
            {
                using (DLinq.CommissionDepartmentEntities DataModel = Entity.DataModel)
                {
                    DLinq.EntriesByDEU deuEntry = DataModel.EntriesByDEUs.FirstOrDefault(s => s.DEUEntryID == DeuEntryID);
                    Guid? carrierId, coverageId;

                    if (deuEntry != null)
                    {
                        carrierId = deuEntry.CarrierId;
                        coverageId = deuEntry.CoverageId;

                        _deu = new DEU();

                        _deu.DEUENtryID = deuEntry.DEUEntryID;
                        _deu.OriginalEffectiveDate = deuEntry.OriginalEffectiveDate;
                        _deu.PaymentRecived = deuEntry.PaymentReceived ?? 0;
                        _deu.CommissionPercentage = deuEntry.CommissionPercentage ?? 0;
                        _deu.Insured = deuEntry.Insured;
                        _deu.PolicyNumber = deuEntry.PolicyNumber;
                        _deu.Enrolled = deuEntry.Enrolled;
                        _deu.Eligible = deuEntry.Eligible;
                        _deu.Link1 = deuEntry.Link1;
                        _deu.SplitPer = deuEntry.SplitPer;
                        _deu.PolicyMode = deuEntry.PolicyModeID;
                        _deu.TrackFromDate = deuEntry.TrackFromDate;
                        _deu.CompScheduleType = deuEntry.CompScheduleType;
                        _deu.CompTypeID = deuEntry.CompTypeID;
                        _deu.ClientID = deuEntry.ClientID ?? Guid.Empty;

                        _deu.ClientName = deuEntry.ClientValue != null ? deuEntry.ClientValue : Client.GetClient(deuEntry.ClientID.Value).Name;

                        _deu.StmtID = deuEntry.StatementID ?? Guid.Empty;
                        _deu.PostStatusID = deuEntry.PostStatusID;
                        _deu.PolicyId = deuEntry.PolicyID ?? Guid.Empty;
                        _deu.InvoiceDate = deuEntry.InvoiceDate;
                        _deu.PayorId = deuEntry.PayorId;
                        _deu.NoOfUnits = deuEntry.NumberOfUnits;
                        _deu.DollerPerUnit = deuEntry.DollerPerUnit ?? 0;
                        _deu.Fee = deuEntry.Fee ?? 0;
                        _deu.Bonus = deuEntry.Bonus ?? 0;
                        //_deu.CommissionPaid1 = deuEntry.CommissionPaid1 ?? 0;
                        //_deu.CommissionPaid2 = deuEntry.CommissionPaid2 ?? 0;
                        _deu.CommissionTotal = deuEntry.CommissionTotal ?? 0;
                        //_deu.AmountBilled = deuEntry.BilledAmount ?? 0;
                        _deu.PayorSysID = deuEntry.PayorSysID;
                        _deu.Renewal = deuEntry.Renewal;
                        _deu.CarrierID = deuEntry.CarrierId;
                        _deu.CoverageID = deuEntry.CoverageId;
                        _deu.IsEntrybyCommissiondashBoard = deuEntry.IsEntrybyCommissiondashBoard ?? false;
                        _deu.CreatedBy = deuEntry.CreatedBy ?? Guid.Empty;
                        _deu.PostCompleteStatus = deuEntry.PostCompleteStatus ?? 0;

                        _deu.CarrierName = deuEntry.CarrierName;
                        _deu.ProductName = deuEntry.ProductName;
                        _deu.EntryDate = deuEntry.EntryDate;
                        _deu.UnlinkClientName = deuEntry.UnlinkClientName;

                        if (!string.IsNullOrEmpty(deuEntry.OtherData))
                        {
                            XmlSerializer serializer = new XmlSerializer(typeof(XmlFields));
                            StringReader stringReader = new StringReader(deuEntry.OtherData);
                            XmlFields xmlFields = (XmlFields)serializer.Deserialize(stringReader);
                            _deu.XmlData = xmlFields;
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                ActionLogger.Logger.WriteLog("GetDeuEntryidWiseForUnlikingPolicy :" + ex.StackTrace.ToString(), true);
                ActionLogger.Logger.WriteLog("GetDeuEntryidWiseForUnlikingPolicy :" + ex.InnerException.ToString(), true);
            }
            return _deu;
        }

        public static bool IsPaymentFromCommissionDashBoardByPaymentEntryId(Guid PolicPaymentId)
        {
            using (DLinq.CommissionDepartmentEntities DataModel = Entity.DataModel)
            {
                DLinq.PolicyPaymentEntry policypaymentEntry = DataModel.PolicyPaymentEntries.Where(p => p.PaymentEntryId == PolicPaymentId).FirstOrDefault();
                if (policypaymentEntry != null)
                {
                    DLinq.EntriesByDEU entriesbyDEU = DataModel.EntriesByDEUs.Where(p => p.DEUEntryID == policypaymentEntry.DEUEntryId).FirstOrDefault();
                    if (entriesbyDEU == null) return false;
                    return entriesbyDEU.IsEntrybyCommissiondashBoard ?? false;
                }
                else
                {
                    return false;
                }
            }
        }

        public static bool IsPaymentFromCommissionDashBoardByDEUEntryId(Guid DeuEntryId)
        {
            using (DLinq.CommissionDepartmentEntities DataModel = Entity.DataModel)
            {
                DLinq.EntriesByDEU entriesbyDEU = DataModel.EntriesByDEUs.Where(p => p.DEUEntryID == DeuEntryId).FirstOrDefault();
                if (entriesbyDEU == null) return false;
                return entriesbyDEU.IsEntrybyCommissiondashBoard ?? false;
            }
        }

        public static string GetProductTypeNickName(Guid policyID, Guid PayorID, Guid CarrierID, Guid CoverageID)
        {
            string strNickName = string.Empty;

            try
            {
                using (DLinq.CommissionDepartmentEntities DataModel = Entity.DataModel)
                {
                    var VarCovergeNickName = from p in DataModel.EntriesByDEUs
                                             where (p.PolicyID == policyID && p.PayorId == PayorID && p.CarrierId == CarrierID && p.CoverageId == CoverageID)
                                             select p.ProductName;

                    foreach (var item in VarCovergeNickName)
                    {
                        if (!string.IsNullOrEmpty(item))
                        {
                            strNickName = Convert.ToString(item);
                        }

                    }

                }

            }
            catch (Exception ex)
            {
                ActionLogger.Logger.WriteLog("GetProductTypeNickName :" + ex.StackTrace.ToString(), true);
                ActionLogger.Logger.WriteLog("GetProductTypeNickName :" + ex.InnerException.ToString(), true);
            }

            return strNickName;
        }


        #endregion


        #region Methods for DEU module for Web

        /// <summary>
        /// Modified by:Ankit Khandelwal
        /// Modified On:16-04-2020
        /// Purpose:Add Exception Log and Getting List of Batches for DEU module
        /// </summary>
        /// <returns></returns>
        public static List<Batch> GetDEUBatchList()
        {
            ActionLogger.Logger.WriteLog("GetBatchListForDEU:Processing Begins for getting batch List", true);
            List<Batch> batchList = new List<Batch>();
            try
            {
                //Call Sp to delete statement 
                using (SqlConnection con = new SqlConnection(DBConnection.GetConnectionString()))
                {
                    using (SqlCommand cmd = new SqlCommand("usp_GetBatchesForDEU", con))
                    {
                        con.Open();
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandTimeout = 0;
                        SqlDataReader dr = cmd.ExecuteReader();
                        while (dr.Read())
                        {
                            Batch objBatch = new Batch();
                            objBatch.BatchId = (Guid)dr["BatchID"];
                            objBatch.BatchNumber = (int)dr["BatchNumber"];
                            objBatch.EntryStatus = (EntryStatus)dr["EntryStatusID"];
                            objBatch.EntryStatusString = Batch.GetBatchStatusString((int)objBatch.EntryStatus);  // Convert.ToString(dr["EntryStatus"]);
                            objBatch.LicenseeId = (Guid)dr["LicenseeId"];
                            objBatch.LicenseeName = Convert.ToString(dr["LicenseeName"]);
                            objBatch.FileName = Convert.ToString(dr["FileName"]);
                            // objBatch.SiteId = dv.SiteLoginID,
                            objBatch.AssignedDeuUserName = Convert.ToString(dr["AssignedDeuUserName"]);
                            // DataModel.UserCredentials.FirstOrDefault(s => s.UserCredentialId == dv.AssignedUserCredentialId).UserName,

                            objBatch.CreatedDateString = dr.IsDBNull("CreatedDate") ? "" :  Convert.ToDateTime(dr["CreatedDate"]).ToString("MM/dd/yyyy");
                            objBatch.LastModifiedDateString = dr.IsDBNull("LastModifiedDate") ? "" : Convert.ToDateTime(dr["LastModifiedDate"]).ToString("MM/dd/yyyy");
                            batchList.Add(objBatch);
                        }
                    }
                }
                /* using (DLinq.CommissionDepartmentEntities DataModel = Entity.DataModel)
                 {OpenBatchOpenBatch

                     batches = (from dv in DataModel.Batches
                                where (dv.UploadStatusId == 3 || dv.UploadStatusId == 4) && (dv.EntryStatusId == 1 || dv.EntryStatusId == 3 || dv.EntryStatusId == 4 || dv.EntryStatusId == 5) && (dv.Licensee.IsDeleted == false) && (dv.Licensee.LicenseStatusId == 0)
                                select new Batch
                                {
                                    BatchNumber = dv.BatchNumber,
                                    BatchId = dv.BatchId,
                                    UploadStatus = (UploadStatus)(dv.UploadStatusId ?? 0),
                                    EntryStatus = (EntryStatus)(dv.EntryStatusId ?? 0),
                                    //   EntryStatusString= Convert.ToString((EntryStatus)(dv.EntryStatusId ?? 0),
                                    LicenseeId = dv.LicenseeId,
                                    LicenseeName = dv.Licensee.Company,
                                    FileName = dv.FileName,
                                    SiteId = dv.SiteLoginID,
                                    AssignedDeuUserName = DataModel.UserCredentials.FirstOrDefault(s => s.UserCredentialId == dv.AssignedUserCredentialId).UserName,
                                    CreatedDate = dv.CreatedOn,
                                }).ToList();


                 }*/
            }
            catch (Exception ex)
            {
                ActionLogger.Logger.WriteLog("GetBatchListForDEU:Exception occurs while processing for getting batch List" + ex.Message, true);
                throw ex;
            }
            return batchList;
        }
        /// <summary>
        /// Createdby:Ankit khandelwal
        /// CreatedOn:16-04-2020
        /// Purpose:Getting statement listr based on BatchId
        /// </summary>
        /// <param name="batchId"></param>
        /// <returns></returns>
        public static List<Statement> GetDEUStatementList(Guid batchId)
        {
            ActionLogger.Logger.WriteLog("GetDEUStatementList:Processing Begins for getting statement List with batchId" + batchId, true);
            List<Statement> statementList = new List<Statement>();
            try
            {

                using (SqlConnection con = new SqlConnection(DBConnection.GetConnectionString()))
                {
                    using (SqlCommand cmd = new SqlCommand("GetStatementList", con))
                    {
                        con.Open();
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@BatchId", batchId);

                        cmd.CommandTimeout = 0;
                        SqlDataReader dr = cmd.ExecuteReader();
                        while (dr.Read())
                        {
                            try
                            {
                                Statement objStmt = new Statement();
                                objStmt.StatementID = (Guid)dr["StatementID"];
                                objStmt.StatementNumber = (int)dr["StatementNumber"];
                                objStmt.StatementDate = dr.IsDBNull("StatementDate") ? null : (DateTime?)dr["StatementDate"];
                                objStmt.CheckAmount = dr.IsDBNull("CheckAmount") ? null : (decimal?)dr["CheckAmount"];
                                objStmt.BalanceForOrAdjustment = dr.IsDBNull("BalAdj") ? null : (decimal?)dr["BalAdj"];
                                objStmt.BatchId = (Guid)dr["BatchID"];
                                objStmt.StatusId = dr.IsDBNull("StatementStatusId") ? null : (int?)dr["StatementStatusId"];
                                objStmt.StatusName = PostUtill.GetStmtStatusString((int)objStmt.StatusId);
                                objStmt.Entries = dr.IsDBNull("Entries") ? 0 : (int)dr["Entries"];
                                objStmt.EnteredAmount = dr.IsDBNull("EnteredAmount") ? 0 : (decimal)dr["EnteredAmount"];
                                objStmt.CreatedDate = (DateTime)dr["Createdon"];
                                objStmt.LastModified = (DateTime)dr["LastModified"];
                                objStmt.PayorId = (Guid)dr["PayorId"];
                                objStmt.CreatedBy = (Guid)dr["CreatedBy"];
                                objStmt.TemplateID = dr.IsDBNull("TemplateID") ? Guid.Empty : (Guid)dr["TemplateID"];
                                objStmt.FromPage = Convert.ToString(dr["FromPage"]);
                                objStmt.ToPage = Convert.ToString(dr["ToPage"]);
                                objStmt.CreatedDateString = objStmt.CreatedDate.ToString("MM/dd/yyyy");
                                objStmt.LastModifiedDateString = objStmt.LastModified.ToString("MM/dd/yyyy");
                                objStmt.CreatedByDEU = Convert.ToString(dr["LastName"]);
                                objStmt.NetAmount = objStmt.CheckAmount - objStmt.BalanceForOrAdjustment;
                                objStmt.CompletePercentage = (double)Statement.CalculateCompletePercent(objStmt.CheckAmount, objStmt.BalanceForOrAdjustment ?? 0, objStmt.EnteredAmount);  //(double)((objStmt.EnteredAmount) / ((objStmt.CheckAmount + objStmt.BalanceForOrAdjustment) == 0 ? int.MaxValue : (objStmt.CheckAmount + objStmt.BalanceForOrAdjustment))) * 100;
                                statementList.Add(objStmt);
                            }
                            catch (Exception ex)
                            {
                                ActionLogger.Logger.WriteLog("GetDEUStatementList:Exception with stmtID: " + (Guid)dr["StatementID"] + ", " + ex.Message, true);

                            }
                        }
                    }
                }

                /*  using (DLinq.CommissionDepartmentEntities DataModel = Entity.DataModel)
                  {


                      statementList = (from p in DataModel.Statements
                                       where p.BatchId == batchId
                                       select new Statement
                                       {
                                           StatementID = p.StatementId,
                                           StatementNumber = p.StatementNumber,
                                           StatementDate = p.StatementDate.Value,
                                           CheckAmount = p.CheckAmount,
                                           BalanceForOrAdjustment = p.BalAdj,
                                           NetAmount = p.CheckAmount ?? 0 + p.BalAdj ?? 0,
                                           CompletePercentage = (double)((p.EnteredAmount ?? 0) / ((p.CheckAmount ?? 0 + p.BalAdj ?? 0) == 0 ? int.MaxValue : (p.CheckAmount ?? 0 + p.BalAdj ?? 0))) * 100,
                                           BatchId = p.BatchId.Value,
                                           StatusId = p.StatementStatusId,
                                           StatusName = p.MasterStatementStatu.Name,
                                           Entries = p.Entries.Value,
                                           EnteredAmount = p.EnteredAmount.Value,
                                           CreatedDate = p.CreatedOn.Value,
                                           LastModified = p.LastModified.Value,
                                           PayorId = p.PayorId.Value,
                                           CreatedBy = p.CreatedBy.Value,
                                           TemplateID = p.TemplateID == null ? Guid.Empty : p.TemplateID,
                                           FromPage = p.FromPage,
                                           ToPage = p.ToPage,

                                           CreatedByDEU = DataModel.UserDetails.FirstOrDefault(dv => dv.UserCredentialId == p.CreatedBy).LastName ?? "Super"
                                       }).OrderBy(x => x.StatementNumber).ToList();

                  }*/
            }
            catch (Exception ex)
            {
                ActionLogger.Logger.WriteLog("GetDEUStatementList:Exception occur while processing for getting statement List with batchId" + batchId + " " + ex.Message, true);
                throw ex;
            }
            return statementList;
        }
        /// <summary>
        /// Createdby:Ankit khandelwal
        /// Createdon:16-04-2020
        /// Purpose:Marked batch close
        /// </summary>
        /// <param name="batchNumber"></param>
        /// <returns></returns>
        public static bool DEUBatchClosed(int batchNumber, out bool isBatchExist, out bool isStatementExist, out bool isallstatementClose)
        {
            ActionLogger.Logger.WriteLog("DEUBatchClosed:Processing begins with batchNumber: " + batchNumber, true);
            try
            {
                isBatchExist = false;
                isStatementExist = false;
                isallstatementClose = false;
                bool isClosed = false;

                using (DLinq.CommissionDepartmentEntities DataModel = Entity.DataModel)
                {

                    DLinq.Batch currentBatch = (from p in DataModel.Batches
                                                where p.BatchNumber == batchNumber
                                                select p).FirstOrDefault();
                    if (currentBatch != null)
                    {
                        isBatchExist = true;
                        if (currentBatch.Statements.Count != 0)
                        {
                            isStatementExist = true;
                            int openStatementCount = currentBatch.Statements.Count(s => s.StatementStatusId != 2);
                            if (openStatementCount == 0)
                            {
                                currentBatch.EntryStatusId = (int)EntryStatus.BatchCompleted;
                                currentBatch.MasterBatchEntryStatu = DataModel.MasterBatchEntryStatus.FirstOrDefault(s => s.BatchEntryStatusId == 6);
                                DataModel.SaveChanges();
                                isallstatementClose = true;
                                isClosed = true;
                            }
                        }

                    }
                    return isClosed;
                }
            }
            catch (Exception ex)
            {
                ActionLogger.Logger.WriteLog("DEUBatchClosed:Exception occurs while processing with batchNumber: " + batchNumber, true);
                throw ex;
            }
        }
        /// <summary>
        /// Createdby:Ankit khandelwal
        /// Createdon:16-04-2020
        /// Purpose:Marked Close statement Status
        /// </summary>
        /// <returns></returns>
        public static bool CloseStatement(int statementNumber, string netAmount)
        {
            ActionLogger.Logger.WriteLog(" CloseStatement :processing begins with -NetAmount: " + netAmount + ", statementNumber: " + statementNumber, true);
            bool StatementEntriesSuccessfull = false;
            try
            {
                using (DLinq.CommissionDepartmentEntities DataModel = Entity.DataModel)
                {
                    DLinq.Statement currentStatement = (from p in DataModel.Statements where p.StatementNumber == statementNumber select p).FirstOrDefault();
                    if (currentStatement != null)
                    {
                        ActionLogger.Logger.WriteLog(DateTime.Now.ToString() + " CloseStatement : EnteredAmount - " + currentStatement.EnteredAmount, true);
                        if (Math.Abs(Convert.ToDecimal(currentStatement.EnteredAmount) - Convert.ToDecimal(netAmount)) <= 1)
                            StatementEntriesSuccessfull = true;

                        ActionLogger.Logger.WriteLog(DateTime.Now.ToString() + " CloseStatement : StatementEntriesSuccessfull - " + StatementEntriesSuccessfull, true);
                        if (StatementEntriesSuccessfull)
                        {
                            currentStatement.StatementStatusId = 2;
                            currentStatement.MasterStatementStatu = DataModel.MasterStatementStatus.FirstOrDefault(s => s.StatementStatusId == 2);
                            currentStatement.EnteredAmount = currentStatement.EnteredAmount;
                            DataModel.SaveChanges();
                            ActionLogger.Logger.WriteLog(DateTime.Now.ToString() + " CloseStatement success", true);
                        }
                    }
                    else
                    {
                        ActionLogger.Logger.WriteLog(DateTime.Now.ToString() + " CloseStatement : current statement found null  ", true);
                    }
                }
            }
            catch (Exception ex)
            {
                ActionLogger.Logger.WriteLog(DateTime.Now.ToString() + " CloseStatement exception:  " + ex.Message, true);
                throw ex;
            }
            return StatementEntriesSuccessfull;
        }

        public static Statement FindStatementDetails(int statementNumber)
        {
            ActionLogger.Logger.WriteLog("FindStatementDetails:Processing begins with statementNumber:" + statementNumber, true);
            Statement statement = new Statement();
            try
            {
                using (DLinq.CommissionDepartmentEntities DataModel = Entity.DataModel)
                {
                    statement = (from dv in DataModel.Statements
                                 where dv.StatementNumber == statementNumber
                                 select new Statement
                                 {
                                     StatementID = dv.StatementId,
                                     StatementNumber = dv.StatementNumber,
                                     StatementDate = dv.StatementDate.Value,
                                     CheckAmount = dv.CheckAmount,
                                     BalanceForOrAdjustment = dv.BalAdj,
                                     NetAmount = dv.CheckAmount ?? 0 + dv.BalAdj ?? 0,
                                     CompletePercentage = (double)((dv.EnteredAmount ?? 0) / ((dv.CheckAmount ?? 0 + dv.BalAdj ?? 0) == 0 ? int.MaxValue : (dv.CheckAmount ?? 0 + dv.BalAdj ?? 0))) * 100,
                                     BatchId = dv.BatchId.Value,
                                     BatchNumber = dv.Batch.BatchNumber,
                                     StatusId = dv.MasterStatementStatu.StatementStatusId,
                                     Entries = dv.Entries ?? 0,
                                     EnteredAmount = dv.EnteredAmount.Value,
                                     CreatedDate = dv.CreatedOn.Value,
                                     LastModified = dv.LastModified.Value,
                                     PayorId = dv.PayorId.Value,
                                     TemplateID = dv.TemplateID,
                                     CreatedBy = dv.CreatedBy.Value,
                                     FromPage = dv.FromPage,
                                     ToPage = dv.ToPage

                                 }).FirstOrDefault();


                }
            }
            catch (Exception ex)
            {
                ActionLogger.Logger.WriteLog(" FindStatementDetails:Exception occurs while processing:" + statementNumber + " " + ex.Message, true);
                throw ex;

            }
            return statement;
        }
        public static Batch GetBatchDetails(int batchNumber, out bool isBatchFound)
        {
            ActionLogger.Logger.WriteLog("GetBatchDetails:processing begins with batchNumber:" + batchNumber, true);
            isBatchFound = false;
            Batch batchData = null;
            try
            {
                using (DLinq.CommissionDepartmentEntities DataModel = Entity.DataModel)
                {
                    batchData = (from dv in DataModel.Batches
                                 select new Batch
                                 {
                                     BatchNumber = dv.BatchNumber,
                                     BatchId = dv.BatchId,
                                     UploadStatus = (UploadStatus)(dv.UploadStatusId ?? 0),
                                     LicenseeId = dv.LicenseeId,
                                     LicenseeName = dv.Licensee.Company,
                                     CreatedDate = dv.CreatedOn,
                                     LastModifiedDate = dv.LastModifiedDate.Value,
                                     EntryStatus = (EntryStatus)(dv.EntryStatusId ?? 0),
                                     SiteId = dv.SiteLoginID,
                                     PayorId = dv.PayorId,
                                     AssignedDeuUserName = DataModel.UserCredentials.FirstOrDefault(s => s.UserCredentialId == dv.AssignedUserCredentialId).UserName,
                                     FileName = dv.FileName
                                 }).FirstOrDefault();

                    if (batchData != null)
                    {
                        isBatchFound = true;
                    }

                }
            }
            catch (Exception ex)
            {
                ActionLogger.Logger.WriteLog(" GetBatchDetails:Exception occurs while processing:" + batchNumber + " " + ex.Message, true);
                throw ex;
            }
            return batchData;
        }


        public static List<PayorToolField> GetPolicyUniqueIdentifier(Guid payorId, Guid templateId)
        {
            ActionLogger.Logger.WriteLog("GetPolicyUniqueIdentifier:processing begins with payorId:" + payorId + " templateId :" + templateId, true);
            List<PayorToolField> UniqueIdentifierList = new List<PayorToolField>();
            PayorTool payorToolDetails = PayorTool.GetPayorTemplateDetails(payorId, templateId);
            try
            {
                foreach (var fieldData in payorToolDetails.ToolFields)
                {
                    if (fieldData.IsPartOfPrimaryKey == true)
                    {
                        UniqueIdentifierList.Add(fieldData);
                    }
                }
            }
            catch (Exception ex)
            {
                ActionLogger.Logger.WriteLog(" GetPolicyUniqueIdentifier:Exception occurs while processing:with payorId:" + payorId + " templateId :" + templateId + " " + ex.Message, true);
                throw ex;
            }
            return UniqueIdentifierList;
        }
        public static List<Payor> GetDEUPayorsList(Guid? licenseeId)
        {
            List<Payor> lstPayors = new List<Payor>();
            try
            {
                using (SqlConnection con = new SqlConnection(DBConnection.GetConnectionString()))
                {
                    using (SqlCommand cmd = new SqlCommand("usp_GetDEUPayorList", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@licenseeId", licenseeId);
                        con.Open();

                        using (SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd))
                        {
                            DataSet ds = new DataSet();
                            dataAdapter.Fill(ds);
                            DataTable pa = ds.Tables[0];
                            // DataTable ca = ds.Tables[1];
                            for (int i = 0; i < pa.Rows.Count; i++)
                            {
                                // List<Carrier> lstCarriers = new List<Carrier>();
                                Payor payordata = new Payor();
                                payordata.PayorName = Convert.ToString(pa.Rows[i]["PayorName"]);
                                payordata.PayorID = (Guid)(pa.Rows[i]["PayorId"]);
                                payordata.NickName = Convert.ToString(pa.Rows[i]["NickName"]);
                                lstPayors.Add(payordata);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ActionLogger.Logger.WriteLog("GetDEUPayorsList:Exception occurs while getting PayorList : " + ex.Message, true);
                throw ex;
            }
            return lstPayors;
        }
        ///// <summary>
        ///// CreatedBy:Acmeminds
        ///// CreatedOn:31-Jan-2019
        ///// Purpose:Getting Template list based on Payor Selection
        ///// </summary>
        ///// <param name="selectedPayorId"></param>
        ///// <returns></returns>
        //public static List<Tempalate> GetDEUTemplateList(Guid selectedPayorId)
        //{
        //    ActionLogger.Logger.WriteLog("GetDEUTemplateList: Processing begins with payorId: " + selectedPayorId, true);
        //    List<Tempalate> templateList = new List<Tempalate>();
        //    try
        //    {
        //        using (SqlConnection con = new SqlConnection(DBConnection.GetConnectionString()))
        //        {

        //            using (SqlCommand cmd = new SqlCommand("usp_GetDEUTemplateList", con))
        //            {
        //                cmd.CommandType = CommandType.StoredProcedure;
        //                cmd.Parameters.AddWithValue("@payorId", selectedPayorId);
        //                con.Open();
        //                SqlDataReader reader = cmd.ExecuteReader();
        //                while (reader.Read())
        //                {
        //                    Tempalate data = new Tempalate();
        //                    data.TemplateID = reader.IsDBNull("TemplateId") ? Guid.Empty : (Guid)reader["TemplateId"];
        //                    data.TemplateName = reader.IsDBNull("TemplateName") ? "" : reader["TemplateName"].ToString().ToTitleCase();
        //                    data.ID = reader.IsDBNull("Id") ? 0 : (int)reader["Id"];
        //                    templateList.Add(data);
        //                }
        //                reader.Close();
        //            }

        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ActionLogger.Logger.WriteLog("GetDEUTemplateList: Exception occurs with payorId: " + selectedPayorId + " " + ex.Message, true);
        //        throw ex;
        //    }
        //    return templateList;
        //}

        public static bool OpenBatch(int intBatchNumber, out bool isBatchExist)
        {
            bool OpenSuccess = false;
            ActionLogger.Logger.WriteLog("OpenBatch: Processing begins with batchNumber: " + intBatchNumber, true);
            isBatchExist = false;
            try
            {
                using (DLinq.CommissionDepartmentEntities DataModel = Entity.DataModel)
                {
                    DLinq.Batch currentBatch = (from p in DataModel.Batches where p.BatchNumber == intBatchNumber select p).FirstOrDefault();
                    if (currentBatch != null)
                    {

                        if (currentBatch.Statements.Count != 0)
                        {
                            isBatchExist = true;
                            currentBatch.EntryStatusId = (int)EntryStatus.InDataEntry;
                            currentBatch.MasterBatchEntryStatu = DataModel.MasterBatchEntryStatus.FirstOrDefault(s => s.BatchEntryStatusId == 5);
                            DataModel.SaveChanges();
                            OpenSuccess = true;
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                ActionLogger.Logger.WriteLog("OpenBatch: Processing exception: " + ex.Message, true);
                throw ex;
            }
            return OpenSuccess;

        }

        public static int AddUpdateStatement(Statement Statement)
        {
            using (DLinq.CommissionDepartmentEntities DataModel = Entity.DataModel)
            {
                var _statement = (from pn in DataModel.Statements where pn.StatementId == Statement.StatementID select pn).FirstOrDefault();
                if (_statement == null)
                {
                    DLinq.Batch batch = DataModel.Batches.FirstOrDefault(s => s.BatchId == Statement.BatchId);
                    _statement = new DLinq.Statement
                    {
                        BatchId = Statement.BatchId,
                        PayorId = Statement.PayorId,
                        CreatedBy = Statement.CreatedBy,
                        CreatedOn = DateTime.Now,
                        LastModified = DateTime.Now,
                        StatementId = Guid.NewGuid(),
                        StatementDate = DateTime.Now,
                        StatementStatusId = 0,
                        TemplateID = Statement.TemplateID,
                        CheckAmount = 0,
                        EnteredAmount = 0,
                        Entries = 0,
                        BalAdj = 0,
                        FromPage = string.Empty,
                        ToPage = string.Empty,
                        IsCreatedFromWeb = true
                    };
                    DataModel.AddToStatements(_statement);

                    if (batch.Statements.Count == 1)
                        batch.AssignedUserCredentialId = Statement.CreatedBy;

                }
                else
                {
                    //_statement.BatchId = Statement.BatchId;
                    //_statement.CheckAmount = Statement.CheckAmount;
                    //_statement.EnteredAmount = Statement.EnteredAmount;//For Commission board
                    //_statement.Entries = Statement.Entries;
                    //_statement.CreatedBy = Statement.CreatedBy;
                    _statement.PayorId = Statement.PayorId;
                    _statement.LastModified = DateTime.Now;
                    //_statement.StatementDate = Statement.StatementDate;
                    //_statement.StatementStatusId = Statement.StatusId;
                    //_statement.BalAdj = Statement.BalanceForOrAdjustment;
                    //_statement.StatementStatusId = Statement.StatusId;
                    //_statement.CreatedOn = System.DateTime.Now;
                    _statement.TemplateID = Statement.TemplateID;
                    //_statement.FromPage = Statement.FromPage;
                    //_statement.ToPage = Statement.ToPage;
                }

                DataModel.SaveChanges();
                return _statement.StatementNumber;
            }
        }
        public static Guid GetStatementId(int? statementNumber)
        {
            var statementId = Guid.Empty;
            using (DLinq.CommissionDepartmentEntities DataModel = Entity.DataModel)
            {
                var _statement = (from pn in DataModel.Statements where pn.StatementNumber == statementNumber select pn).FirstOrDefault();
                return _statement.StatementId;
            }

        }

        /// <summary>
        /// By jyotisn a- for web DEU
        /// </summary>
        /// <param name="statementId"></param>
        /// <returns></returns>
        public static decimal? GetStatementEnteredAmount(Guid statementId)
        {
            using (DLinq.CommissionDepartmentEntities DataModel = Entity.DataModel)
            {
                var _statement = (from pn in DataModel.Statements where pn.StatementId == statementId select pn).FirstOrDefault();
                return _statement.EnteredAmount;
            }

        }

        public static DEUPayorToolObject GetDEUTemplateDetails(Guid payorId, Guid? templateId, out string uniqueIDs)
        {

            DEUPayorToolObject payorToolDetails = new DEUPayorToolObject();
            uniqueIDs = "";
            ActionLogger.Logger.WriteLog("GetDEUTemplateList: Processing begins with payorId: " + payorId, true);
            List<DEUPayorToolFieldObject> toolFieldList = new List<DEUPayorToolFieldObject>();
            try
            {
                using (SqlConnection con = new SqlConnection(DBConnection.GetConnectionString()))
                {

                    using (SqlCommand cmd = new SqlCommand("usp_GetDEUTemplateData", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@payorId", payorId);
                        cmd.Parameters.AddWithValue("@TemplateId", templateId);
                        con.Open();
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            DEUPayorToolFieldObject toolFieldData = new DEUPayorToolFieldObject();
                            toolFieldData.TemplateID = reader.IsDBNull("TemplateId") ? Guid.Empty : (Guid)reader["TemplateId"];
                            toolFieldData.AllignedDirection = reader.IsDBNull("AllignedDirection") ? "" : reader["AllignedDirection"].ToString();
                            toolFieldData.AvailableFieldName = reader.IsDBNull("AvailableFieldName") ? "" : reader["AvailableFieldName"].ToString();
                            toolFieldData.FormulaExpression = reader.IsDBNull("FormulaExpression") ? "" : reader["FormulaExpression"].ToString();
                            toolFieldData.ControlHeight = reader.IsDBNull("FieldHeight") ? 0 : Convert.ToDouble(reader["FieldHeight"]);
                            toolFieldData.ControlWidth = reader.IsDBNull("FieldWidth") ? 0 : Convert.ToDouble(reader["FieldWidth"]);
                            toolFieldData.ControlX = reader.IsDBNull("FieldPositionX") ? 0 : Convert.ToDouble(reader["FieldPositionX"]);
                            toolFieldData.ControlY = reader.IsDBNull("FieldPositionY") ? 0 : Convert.ToDouble(reader["FieldPositionY"]);
                            toolFieldData.DefaultValue = reader.IsDBNull("DefaultValue") ? "" : reader["DefaultValue"].ToString();
                            toolFieldData.EquivalentDeuField = reader.IsDBNull("EquivalentDeuField") ? "" : reader["EquivalentDeuField"].ToString();
                            toolFieldData.EquivalentIncomingField = reader.IsDBNull("EquivalentIncomingField") ? "" : reader["EquivalentIncomingField"].ToString();
                            toolFieldData.EquivalentLearnedField = reader.IsDBNull("EquivalentLearnedField") ? "" : reader["EquivalentLearnedField"].ToString();
                            toolFieldData.FieldOrder = reader.IsDBNull("FieldOrder") ? 1 : Convert.ToInt32(reader["FieldOrder"]);
                            toolFieldData.FieldStatusValue = reader.IsDBNull("FieldStatus") ? "" : reader["FieldStatus"].ToString();
                            toolFieldData.HelpText = reader.IsDBNull("HelpText") ? "" : reader["HelpText"].ToString();
                            toolFieldData.IsCalculatedField = reader.IsDBNull("IsCalculatedField") ? false : (bool)reader["IsCalculatedField"];
                            toolFieldData.IsOverrideOfCalcAllowed = reader.IsDBNull("IsOverrideOfCalcAllowed") ? false : (bool)reader["IsOverrideOfCalcAllowed"];
                            toolFieldData.IsPartOfPrimaryKey = reader.IsDBNull("IsPartOfPrimary") ? false : (bool)reader["IsPartOfPrimary"];
                            toolFieldData.IsPopulateIfLinked = reader.IsDBNull("IsPopulatedIfLinked") ? false : (bool)reader["IsPopulatedIfLinked"];
                            toolFieldData.IsZeroorBlankAllowed = reader.IsDBNull("IsZeroorBlankAllowed") ? false : (bool)reader["IsZeroorBlankAllowed"];
                            toolFieldData.IsTabbedToNextFieldIfLinked = reader.IsDBNull("IsTabbedToNextFieldIfLinked") ? false : (bool)reader["IsTabbedToNextFieldIfLinked"];
                            toolFieldData.LabelOnField = reader.IsDBNull("LabelOnField") ? "" : (string)reader["LabelOnField"];
                            toolFieldData.PayorToolId = reader.IsDBNull("PayorToolId") ? Guid.Empty : (Guid)reader["PayorToolId"];
                            toolFieldData.PayorFieldID = reader.IsDBNull("PayorToolFieldId") ? Guid.Empty : (Guid)reader["PayorToolFieldId"];
                            toolFieldData.PTAvailableFieldId = reader.IsDBNull("PTAvailableFieldId") ? 0 : (int)reader["PTAvailableFieldId"];

                            DEUWebMaskFieldType MaskFieldData = new DEUWebMaskFieldType();
                            // toolFieldData.DEUMaskFieldType = new toolFieldData.DEUMaskFieldType();
                            MaskFieldData.MaskTypeName = reader.IsDBNull("MaskTypeName") ? "" : reader["MaskTypeName"].ToString();
                            MaskFieldData.Suffix = reader.IsDBNull("Suffix") ? "" : reader["Suffix"].ToString();
                            MaskFieldData.Prefix = reader.IsDBNull("Prefix") ? "" : reader["Prefix"].ToString();
                            MaskFieldData.ShowMasking = reader.IsDBNull("ShowMasking") ? false : (bool)reader["ShowMasking"];
                            MaskFieldData.AllowNegativeNumber= reader.IsDBNull("AllowNegativeNumber") ? false : (bool)reader["AllowNegativeNumber"];
                            MaskFieldData.PTAMAskFieldTypeId = reader.IsDBNull("PTAMAskFieldTypeId") ? 1 : (int)reader["PTAMAskFieldTypeId"];
                            MaskFieldData.ThousandSeparator = reader.IsDBNull("ThousandSeparator") ? "" : reader["ThousandSeparator"].ToString();
                            MaskFieldData.MaskFieldType = reader.IsDBNull("MaskFieldType") ? "" : reader["MaskFieldType"].ToString();
                            MaskFieldData.DEUMaskTypeId = reader.IsDBNull("DEUMaskTypeId") ? (byte)3 : (byte)reader["DEUMaskTypeId"];
                            toolFieldData.DEUMaskFieldType = MaskFieldData;
                            if (toolFieldData.IsPartOfPrimaryKey)
                            {
                                uniqueIDs += toolFieldData.AvailableFieldName + ", ";
                            }
                            toolFieldList.Add(toolFieldData);
                        }
                        if (toolFieldList.Count > 1)
                        {
                            DEUPayorToolFieldObject toolFieldDetails = new DEUPayorToolFieldObject();
                            DEUWebMaskFieldType MaskFieldDetails = new DEUWebMaskFieldType();
                            toolFieldDetails.AvailableFieldName = "DEUEntryId";
                            toolFieldDetails.DefaultValue = Convert.ToString(Guid.Empty);
                            toolFieldDetails.DEUMaskFieldType = MaskFieldDetails;
                            toolFieldList.Add(toolFieldDetails);
                        }

                        reader.Close();
                        PayorTool pTool = new PayorTool();
                        using (DLinq.CommissionDepartmentEntities DataModel = Entity.DataModel)
                        {

                            if (templateId == Guid.Empty)
                            {
                                pTool = (from p in DataModel.PayorTools
                                         where p.Payor.PayorId == payorId && p.TemplateID == null && p.IsDeleted == false
                                         select new PayorTool
                                         {
                                             ChequeImageFilePath = p.ChequeImageFile,
                                             StatementImageFilePath = p.StatementImageFile,
                                         }).FirstOrDefault();

                            }
                            else
                            {
                                pTool = (from p in DataModel.PayorTools
                                         where p.Payor.PayorId == payorId && p.TemplateID == templateId && p.IsDeleted == false
                                         select new PayorTool
                                         {
                                             ChequeImageFilePath = p.ChequeImageFile,
                                             StatementImageFilePath = p.StatementImageFile,
                                         }).FirstOrDefault();
                            }
                        }
                        if (uniqueIDs.Length > 1)
                        {
                            uniqueIDs = uniqueIDs.Substring(0, uniqueIDs.Length - 2);
                            pTool.UniqueIds = uniqueIDs;
                        }
                        payorToolDetails.ToolFieldList = toolFieldList;
                        if (pTool != null)
                        {
                            if (!string.IsNullOrEmpty(pTool.ChequeImageFilePath))
                            {

                                payorToolDetails.ChequeImageFilePath = pTool.ChequeImageFilePath.Replace("Images/", "");

                            }
                            if (!string.IsNullOrEmpty(pTool.StatementImageFilePath))
                            {

                                payorToolDetails.StatementImageFilePath = pTool.StatementImageFilePath.Replace("Images/", "");

                            }
                            //payorToolDetails.ChequeImageFilePath = pTool.ChequeImageFilePath;
                            // payorToolDetails.StatementImageFilePath = pTool.StatementImageFilePath;
                        }

                    }

                }
            }
            catch (Exception ex)
            {
                ActionLogger.Logger.WriteLog("GetDEUTemplateList: Exception occurs with payorId: " + payorId + " " + ex.Message, true);
                throw ex;
            }
            return payorToolDetails;
        }
        public static List<DEUPaymentEntry> GetDeuEntriesforStatement(Guid StatementId, ListParams listParams, out int totalRecordCount)
        {
            List<DEUPaymentEntry> entries = new List<DEUPaymentEntry>();
            try
            {

                using (SqlConnection con = new SqlConnection(DBConnection.GetConnectionString()))
                {
                    int rowStart = (listParams.pageSize * (listParams.pageIndex)) + 1;
                    int rowEnd = (listParams.pageIndex + 1) * listParams.pageSize;
                    using (SqlCommand cmd = new SqlCommand("usp_GetDEUPaymentListPageWise", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@statementId", StatementId);
                        cmd.Parameters.AddWithValue("@pageNumber", rowStart);
                        cmd.Parameters.AddWithValue("@pageSize", rowEnd);
                        cmd.Parameters.AddWithValue("@sortType", listParams.sortType);
                        cmd.Parameters.AddWithValue("@sortBy", listParams.sortBy);
                        cmd.Parameters.Add("@totalRecordCount", SqlDbType.Int);
                        cmd.Parameters["@totalRecordCount"].Direction = ParameterDirection.Output;

                        con.Open();
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            DEUPaymentEntry entryDetails = new DEUPaymentEntry();
                            entryDetails.DEUENtryID = reader.IsDBNull("DEUEntryID") ? Guid.Empty : (Guid)reader["DEUEntryID"];
                            entryDetails.ClientName = reader.IsDBNull("ClientName") ? "" : (string)reader["ClientName"];
                            entryDetails.UnlinkClientName = reader.IsDBNull("UnlinkClientName") ? "" : (string)reader["UnlinkClientName"];
                            entryDetails.Insured = reader.IsDBNull("Insured") ? "" : (string)reader["Insured"];
                            entryDetails.PaymentReceived = reader.IsDBNull("PaymentReceived") ? "" : Convert.ToDecimal(reader["PaymentReceived"]).ToString("C");
                            entryDetails.Units = reader.IsDBNull("NumberOfUnits") ? 0 : (int)reader["NumberOfUnits"];
                            entryDetails.InvoiceDate = reader.IsDBNull("InvoiceDate") ? "" : reader["InvoiceDate"].ToString();
                            entryDetails.CommissionTotal = reader.IsDBNull("CommissionTotal") ? "" : Convert.ToDecimal(reader["CommissionTotal"]).ToString("C");
                            entryDetails.Fee = reader.IsDBNull("Fee") ? "" : Convert.ToDecimal(reader["Fee"]).ToString("C");
                            entryDetails.EntryDate = reader.IsDBNull("EntryDate") ? "" : reader["EntryDate"].ToString();
                            entryDetails.PolicyNumber = reader.IsDBNull("PolicyNumber") ? "" : reader["PolicyNumber"].ToString();
                            entryDetails.CarrierNickName = reader.IsDBNull("CarrierNickName") ? "" : reader["CarrierNickName"].ToString();
                            entryDetails.CoverageNickName = reader.IsDBNull("CoverageNickName") ? "" : reader["CoverageNickName"].ToString();
                            entryDetails.CarrierName = reader.IsDBNull("CarrierName") ? "" : reader["CarrierName"].ToString();
                            entryDetails.ProductName = reader.IsDBNull("ProductName") ? "" : reader["ProductName"].ToString();
                            entryDetails.PostStatus = reader.IsDBNull("PostStatus") ? (PostCompleteStatusEnum)2 : (PostCompleteStatusEnum)reader["PostStatus"];
                            entryDetails.CommissionPercentage = reader.IsDBNull("CommissionPercentage") ? "" : Convert.ToDecimal(reader["CommissionPercentage"]).ToString("P");
                            entryDetails.CreatedDate = reader.IsDBNull("CreatedOn") ? "" : reader["CreatedOn"].ToString();
                            entryDetails.isSuccess = reader.IsDBNull("IsSuccess") ? false : Convert.ToBoolean(reader["IsSuccess"]);
                            entryDetails.SplitPercentage = reader.IsDBNull("SplitPer") ? "0.00%" : Convert.ToDecimal(reader["SplitPer"]).ToString("P");
                            entries.Add(entryDetails);
                        }
                        con.Close();
                        totalRecordCount = (int)cmd.Parameters["@totalRecordCount"].Value;

                    }
                }
            }
            catch (Exception ex)
            {
                ActionLogger.Logger.WriteLog("Exception occurs while fetching posted payment List:" + ex.Message + StatementId, true);
                throw ex;
            }
            return entries;

        }

        public static List<DEUPaymentEntry> GetDeuFailedEntriesforStatement(Guid StatementId, ListParams listParams, out int totalRecordCount)
        {
            List<DEUPaymentEntry> entries = new List<DEUPaymentEntry>();
            try
            {

                using (SqlConnection con = new SqlConnection(DBConnection.GetConnectionString()))
                {
                    int rowStart = (listParams.pageSize * (listParams.pageIndex)) + 1;
                    int rowEnd = (listParams.pageIndex + 1) * listParams.pageSize;
                    using (SqlCommand cmd = new SqlCommand("usp_GetDEUPaymentErrorList", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@statementId", StatementId);
                        cmd.Parameters.AddWithValue("@pageNumber", rowStart);
                        cmd.Parameters.AddWithValue("@pageSize", rowEnd);
                        cmd.Parameters.AddWithValue("@sortType", listParams.sortType);
                        cmd.Parameters.AddWithValue("@sortBy", listParams.sortBy);
                        cmd.Parameters.Add("@totalRecordCount", SqlDbType.Int);
                        cmd.Parameters["@totalRecordCount"].Direction = ParameterDirection.Output;

                        con.Open();
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            DEUPaymentEntry entryDetails = new DEUPaymentEntry();
                            entryDetails.DEUENtryID = reader.IsDBNull("DEUEntryID") ? Guid.Empty : (Guid)reader["DEUEntryID"];
                            entryDetails.ClientName = reader.IsDBNull("ClientName") ? "" : (string)reader["ClientName"];
                            entryDetails.UnlinkClientName = reader.IsDBNull("UnlinkClientName") ? "" : (string)reader["UnlinkClientName"];
                            entryDetails.Insured = reader.IsDBNull("Insured") ? "" : (string)reader["Insured"];
                            entryDetails.PaymentReceived = reader.IsDBNull("PaymentReceived") ? "" : Convert.ToDecimal(reader["PaymentReceived"]).ToString("C");
                            entryDetails.Units = reader.IsDBNull("NumberOfUnits") ? 0 : (int)reader["NumberOfUnits"];
                            entryDetails.InvoiceDate = reader.IsDBNull("InvoiceDate") ? "" : reader["InvoiceDate"].ToString();
                            entryDetails.CommissionTotal = reader.IsDBNull("CommissionTotal") ? "" : Convert.ToDecimal(reader["CommissionTotal"]).ToString("C");
                            entryDetails.Fee = reader.IsDBNull("Fee") ? "" : Convert.ToDecimal(reader["Fee"]).ToString("C");
                            entryDetails.EntryDate = reader.IsDBNull("EntryDate") ? "" : reader["EntryDate"].ToString();
                            entryDetails.PolicyNumber = reader.IsDBNull("PolicyNumber") ? "" : reader["PolicyNumber"].ToString();
                            entryDetails.CarrierNickName = reader.IsDBNull("CarrierNickName") ? "" : reader["CarrierNickName"].ToString();
                            entryDetails.CoverageNickName = reader.IsDBNull("CoverageNickName") ? "" : reader["CoverageNickName"].ToString();
                            entryDetails.CarrierName = reader.IsDBNull("CarrierName") ? "" : reader["CarrierName"].ToString();
                            entryDetails.ProductName = reader.IsDBNull("ProductName") ? "" : reader["ProductName"].ToString();
                            entryDetails.PostStatus = reader.IsDBNull("PostStatus") ? (PostCompleteStatusEnum)2 : (PostCompleteStatusEnum)reader["PostStatus"];
                            entryDetails.CommissionPercentage = reader.IsDBNull("CommissionPercentage") ? "" : Convert.ToDecimal(reader["CommissionPercentage"]).ToString("P");
                            entryDetails.CreatedDate = reader.IsDBNull("CreatedOn") ? "" : reader["CreatedOn"].ToString();
                            entryDetails.isSuccess = reader.IsDBNull("IsSuccess") ? false : Convert.ToBoolean(reader["IsSuccess"]);
                            entryDetails.SplitPercentage = reader.IsDBNull("SplitPer") ? "0.00%" : Convert.ToDecimal(reader["SplitPer"]).ToString("P");
                            entries.Add(entryDetails);
                        }
                        con.Close();
                        totalRecordCount = (int)cmd.Parameters["@totalRecordCount"].Value;

                    }
                }
            }
            catch (Exception ex)
            {
                ActionLogger.Logger.WriteLog("Exception occurs while fetching posted payment List:" + ex.Message + StatementId, true);
                throw ex;
            }
            return entries;

        }
        #endregion

        public static DEUPaymentEntry GetDeuEntryOnID(Guid deuEntryID)
        {
            DEUPaymentEntry entryDetails = new DEUPaymentEntry();
            using (SqlConnection con = new SqlConnection(DBConnection.GetConnectionString()))
            {

                using (SqlCommand cmd = new SqlCommand("usp_GetDEUPaymentDetails", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@deuEntryID", deuEntryID);
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        entryDetails.DEUENtryID = reader.IsDBNull("DEUEntryID") ? Guid.Empty : (Guid)reader["DEUEntryID"];
                        entryDetails.ClientName = reader.IsDBNull("ClientName") ? "" : (string)reader["ClientName"];
                        entryDetails.UnlinkClientName = reader.IsDBNull("UnlinkClientName") ? "" : (string)reader["UnlinkClientName"];
                        entryDetails.Insured = reader.IsDBNull("Insured") ? "" : (string)reader["Insured"];
                        entryDetails.PaymentReceived = reader.IsDBNull("PaymentReceived") ? "" : Convert.ToDecimal(reader["PaymentReceived"]).ToString("C");
                        entryDetails.Units = reader.IsDBNull("NumberOfUnits") ? 0 : (int)reader["NumberOfUnits"];
                        entryDetails.InvoiceDate = reader.IsDBNull("InvoiceDate") ? "" : reader["InvoiceDate"].ToString();
                        entryDetails.CommissionTotal = reader.IsDBNull("CommissionTotal") ? "" : Convert.ToDecimal(reader["CommissionTotal"]).ToString("C");
                        entryDetails.Fee = reader.IsDBNull("Fee") ? "" : Convert.ToDecimal(reader["Fee"]).ToString("C");
                        entryDetails.EntryDate = reader.IsDBNull("EntryDate") ? "" : reader["EntryDate"].ToString();
                        entryDetails.PolicyNumber = reader.IsDBNull("PolicyNumber") ? "" : reader["PolicyNumber"].ToString();
                        entryDetails.CarrierNickName = reader.IsDBNull("CarrierNickName") ? "" : reader["CarrierNickName"].ToString();
                        entryDetails.CoverageNickName = reader.IsDBNull("CoverageNickName") ? "" : reader["CoverageNickName"].ToString();
                        entryDetails.CarrierName = reader.IsDBNull("CarrierName") ? "" : reader["CarrierName"].ToString();
                        entryDetails.ProductName = reader.IsDBNull("ProductName") ? "" : reader["ProductName"].ToString();
                        entryDetails.PostStatus = reader.IsDBNull("PostStatus") ? (PostCompleteStatusEnum)2 : (PostCompleteStatusEnum)reader["PostStatus"];
                        entryDetails.CommissionPercentage = reader.IsDBNull("CommissionPercentage") ? "" : Convert.ToDecimal(reader["CommissionPercentage"]).ToString("P");
                        entryDetails.CreatedDate = reader.IsDBNull("CreatedOn") ? "" : reader["CreatedOn"].ToString();
                        entryDetails.isSuccess = reader.IsDBNull("IsSuccess") ? false : Convert.ToBoolean(reader["IsSuccess"]);
                        entryDetails.SplitPercentage = reader.IsDBNull("SplitPer") ? "0.00%" : Convert.ToDecimal(reader["SplitPer"]).ToString("P");
                    }
                }
                return entryDetails;
            }
        }
        public static bool ValidateDate(string date, string format, out string convertedDate)
        {
            bool isFormatValid = false;
            convertedDate = "";
            format = format.Replace("*", "");
            try
            {
                //DateTime temptime;
                DateTime temptime = DateTime.ParseExact(date, format, DateTimeFormatInfo.InvariantInfo);
              //  DateTime.TryParse(date, out temptime);
                isFormatValid = true;
                convertedDate = temptime.ToString("MM/dd/yyyy");
                ActionLogger.Logger.WriteLog("ValidateDate:Date result " + convertedDate, true);

            }
            catch (Exception ex)
            {
                ActionLogger.Logger.WriteLog("ValidateDate:Date validation failed trying again: " + ex.Message, true);
                isFormatValid = false;
                //Below is not required, as it gives false date like 20-30-2019 to true
                //try
                //{
                //    DateTime temptime;
                //    DateTime.TryParse(date, out temptime);
                //    isFormatValid = true;
                //    convertedDate = temptime.ToString("MM/dd/yyyy");
                //    ActionLogger.Logger.WriteLog("ValidateDate:Date validation success in 2nd attempt: " + convertedDate, true);

                //}
                //catch (Exception e)
                //{
                //    ActionLogger.Logger.WriteLog("ValidateDate failed all :Date is invalid" + e.Message, true);
                //    isFormatValid = false;
                //}
            }
            return isFormatValid;
        }
        public static string GetFormatedDate(string date, string format = "")
        {

            format = format.Replace("*", "");
            try
            {
                DateTime dt = DateTime.MinValue;
                DateTime.TryParse(date, out dt);
                if (string.IsNullOrEmpty(format))
                {
                    return dt.ToString();
                }
                return dt.ToString(format); //Any desired format
            }
            catch (Exception ex)
            {
                ActionLogger.Logger.WriteLog("ValidateDate:Date is invalid" + ex.Message, true);
                return "";

            }

        }

        public static List<Tempalate> GetDEUTemplateList(Guid selectedPayorId)
        {
            ActionLogger.Logger.WriteLog("GetDEUTemplateList: Processing begins with payorId: " + selectedPayorId, true);
            List<Tempalate> tempPayor = new List<Tempalate>();
            List<PayorTool> tempPayor1 = new List<PayorTool>();
            try
            {
                using (DLinq.CommissionDepartmentEntities DataModel = Entity.DataModel)
                {
                    tempPayor = (from p in DataModel.PayorTemplates
                                 where p.PayorId == selectedPayorId && p.IsDeleted == false
                                 select new Tempalate
                                 {
                                     ID = p.ID,
                                     TemplateID = (Guid)p.TemplateID,
                                     TemplateName = p.TemplateName,

                                 }).ToList();

                }


                using (DLinq.CommissionDepartmentEntities DataModel = Entity.DataModel)
                {
                    tempPayor1 = (from p in DataModel.PayorTools
                                  where p.PayorId == selectedPayorId && p.TemplateID == null && p.IsDeleted == false
                                  select new PayorTool
                                  {
                                      PayorToolId = (Guid)p.PayorToolId,

                                  }).ToList();

                }


                if (tempPayor1.Count > 0)
                {
                    tempPayor = new List<Tempalate>(tempPayor.OrderBy(p => p.TemplateName));

                    Tempalate tempDefault = new Tempalate();
                    tempDefault.ID = 0;
                    tempDefault.TemplateID = Guid.Empty;
                    tempDefault.TemplateName = "Default";
                    tempPayor.Insert(0, tempDefault);
                }
            }
            catch (Exception ex)
            {
                ActionLogger.Logger.WriteLog("GetDEUTemplateList: Exception occurs with payorId: " + selectedPayorId + " " + ex.Message, true);
                throw ex;
            }

            return tempPayor;

        }


        public static bool IsStatementCheckAmountExist(Guid? batchId, Guid? payorId, string checkAmount, Guid currentStatementId)
        {
            List<Statement> BatchStatements = new List<Statement>();
            bool isStatementExist = false;
            try
            {
                //decimal CurrentcheckAmnt = Convert.ToDecimal(checkAmount);
                ActionLogger.Logger.WriteLog("GetStatementCheckAmount:Pocessing begins with batchId" + batchId + "payorId:" + payorId, true);
                using (DLinq.CommissionDepartmentEntities DataModel = Entity.DataModel)
                {
                    BatchStatements = (from p in DataModel.Statements
                                       where p.BatchId == batchId && p.PayorId == payorId
                                       && p.StatementId != currentStatementId
                                       select new Statement
                                       {
                                           StatementID = p.StatementId,
                                           StatementNumber = p.StatementNumber,
                                           StatementDate = p.StatementDate.Value,
                                           CheckAmount = p.CheckAmount

                                       }).ToList();



                }
                foreach (var value in BatchStatements)
                {
                    if (Convert.ToDouble(value.CheckAmount) == Convert.ToDouble(checkAmount))
                    {
                        isStatementExist = true;
                        break;
                    }
                }
            }

            catch (Exception ex)
            {
                ActionLogger.Logger.WriteLog("GetStatementCheckAmount:Exception occurs while pocessing begins with batchId" + batchId + "payorId:" + payorId + ex.Message, true);
                throw ex;
            }
            return isStatementExist;
        }
    }
    [DataContract]
    public class DataEntryField
    {
        [XmlAttribute]
        [DataMember]
        public string DeuFieldName { get; set; }
        [DataMember]
        [XmlElement]
        public byte DeuFieldType { get; set; }
        [XmlElement]
        [DataMember]
        public string DeuFieldValue { get; set; }

        [DataMember]
        public string DeuFieldMaskType { get; set; }
    }


}
