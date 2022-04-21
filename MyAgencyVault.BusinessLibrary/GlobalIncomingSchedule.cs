using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using MyAgencyVault.BusinessLibrary.Base;
using DLinq = DataAccessLayer.LinqtoEntity;
using System.Data.EntityClient;
using System.Data.SqlClient;
using System.Data;
using System.Data.SqlClient;
using System.Transactions;

namespace MyAgencyVault.BusinessLibrary
{
    [DataContract]
    public class IncomingScheduleEntry
    {
        [DataMember]
        public Guid CoveragesScheduleId { get; set; }
        [DataMember]
        public double? FromRange { get; set; }
        [DataMember]
        public double? ToRange { get; set; }
        [DataMember]
        public double? Rate { get; set; }
        //[DataMember]
        //public DateTime? EffectiveFromDate { get; set; }
        DateTime? _effectiveFromDate;
        [DataMember]
        public DateTime? EffectiveFromDate
        {
            get
            {
                return _effectiveFromDate;
            }
            set
            {
                _effectiveFromDate = value;
                if (value != null && string.IsNullOrEmpty(EffectiveFromDateString))
                {
                    EffectiveFromDateString = value.ToString();
                }
            }
        }
        string _effectiveFromDateString;
        [DataMember]
        public string EffectiveFromDateString
        {
            get
            {
                return _effectiveFromDateString;
            }
            set
            {
                _effectiveFromDateString = value;
                if (EffectiveFromDate == null && !string.IsNullOrEmpty(_effectiveFromDateString))
                {
                    DateTime dt;
                    DateTime.TryParse(_effectiveFromDateString, out dt);
                    EffectiveFromDate = dt;
                }
            }
        }



        //[DataMember]
        //public DateTime? EffectiveToDate { get; set; }
        DateTime? _effectiveToDate;
        [DataMember]
        public DateTime? EffectiveToDate
        {
            get
            {
                return _effectiveToDate;
            }
            set
            {
                _effectiveToDate = value;
                if (value != null && string.IsNullOrEmpty(EffectiveToDateString))
                {
                    EffectiveToDateString = value.ToString();
                }
            }
        }
        string effectiveToDateString;
        [DataMember]
        public string EffectiveToDateString
        {
            get
            {
                return effectiveToDateString;
            }
            set
            {
                effectiveToDateString = value;
                if (EffectiveToDate == null && !string.IsNullOrEmpty(effectiveToDateString))
                {
                    DateTime dt;
                    DateTime.TryParse(effectiveToDateString, out dt);
                    EffectiveToDate = dt;
                }
            }
        }

        [DataMember]
        public bool IsDeleted { get; set; }
    }
    [DataContract]
    public class PolicyOutgoingScheduleDetails
    {
        [DataMember]
        public Guid PolicyId { get; set; }
        [DataMember]
        public string ScheduleTypeName { get; set; }
        [DataMember]
        public int ScheduleTypeId { get; set; }
        [DataMember]
        public List<PolicyOutgoingSchedules> OutgoingScheduleList { get; set; }
    }

    [DataContract]
    public class OutgoingScheduleEntry
    {
        [DataMember]
        public Guid CoveragesScheduleId { get; set; }
        [DataMember]
        public double? FromRange { get; set; }
        [DataMember]
        public double? ToRange { get; set; }
        [DataMember]
        public double? Rate { get; set; }
        //[DataMember]
        //public DateTime? EffectiveFromDate { get; set; }

        DateTime? _effectiveFromDate;
        [DataMember]
        public DateTime? EffectiveFromDate
        {
            get
            {
                return _effectiveFromDate;
            }
            set
            {
                _effectiveFromDate = value;
                if (value != null && string.IsNullOrEmpty(EffectiveFromDateString))
                {
                    EffectiveFromDateString = value.ToString();
                }
            }
        }
        string _effectiveFromDateString;
        [DataMember]
        public string EffectiveFromDateString
        {
            get
            {
                return _effectiveFromDateString;
            }
            set
            {
                _effectiveFromDateString = value;
                if (EffectiveFromDate == null && !string.IsNullOrEmpty(_effectiveFromDateString))
                {
                    DateTime dt;
                    DateTime.TryParse(_effectiveFromDateString, out dt);
                    EffectiveFromDate = dt;
                }
            }
        }

        //[DataMember]
        //public DateTime? EffectiveToDate { get; set; }
        DateTime? _effectiveToDate;
        [DataMember]
        public DateTime? EffectiveToDate
        {
            get
            {
                return _effectiveToDate;
            }
            set
            {
                _effectiveToDate = value;
                if (value != null && string.IsNullOrEmpty(EffectiveToDateString))
                {
                    EffectiveToDateString = value.ToString();
                }
            }
        }
        string effectiveToDateString;
        [DataMember]
        public string EffectiveToDateString
        {
            get
            {
                return effectiveToDateString;
            }
            set
            {
                effectiveToDateString = value;
                if (EffectiveToDate == null && !string.IsNullOrEmpty(effectiveToDateString))
                {
                    DateTime dt;
                    DateTime.TryParse(effectiveToDateString, out dt);
                    EffectiveToDate = dt;
                }
            }
        }

        [DataMember]
        public Guid PayeeUserCredentialId { get; set; }
        [DataMember]
        public string PayeeName { get; set; }
        [DataMember]
        public bool IsPrimaryAgent { get; set; }
    }

    [DataContract]
    public class GlobalIncomingSchedule
    {
        [DataMember]
        public Guid CarrierId { get; set; }
        [DataMember]
        public Guid CoverageId { get; set; }
        [DataMember]
        public string ScheduleTypeName { get; set; }
        [DataMember]
        public string CarrierName { get; set; }
        [DataMember]
        public string ProductName { get; set; }
        [DataMember]
        public int ScheduleTypeId { get; set; }
        [DataMember]
        public List<IncomingScheduleEntry> IncomingScheduleList { get; set; }
        [DataMember]
        public bool IsModified { get; set; }
    }

    [DataContract]
    public class PolicyIncomingSchedule
    {
        [DataMember]
        public Guid PolicyId { get; set; }
        [DataMember]
        public string ScheduleTypeName { get; set; }
        [DataMember]
        public int ScheduleTypeId { get; set; }
        [DataMember]
        public List<IncomingScheduleEntry> IncomingScheduleList { get; set; }
        [DataMember]
        public bool IsModified { get; set; }
    }

    [DataContract]
    public class PolicyIncomingPayType
    {
        [DataMember]
        public int IncomingPaymentTypeId { get; set; }
        [DataMember]
        public string Name { get; set; }

    }

    [DataContract]
    public class PolicyOutgoingSchedule
    {
        [DataMember]
        public Guid PolicyId { get; set; }
        [DataMember]
        public string ScheduleTypeName { get; set; }
        [DataMember]
        public int ScheduleTypeId { get; set; }
        [DataMember]
        public List<OutgoingScheduleEntry> OutgoingScheduleList { get; set; }
    }

    [DataContract]
    public class IncomingSchedule
    {
        #region IEditable<IncomingSchedule> Members

        public static void AddUpdateGlobalSchedule(GlobalIncomingSchedule globalSchedule)
        {
            using (DLinq.CommissionDepartmentEntities DataModel = Entity.DataModel)
            {
                DLinq.GlobalCoveragesSchedule gcs = null;

                if (globalSchedule.IncomingScheduleList == null || globalSchedule.IncomingScheduleList.Count == 0)
                {
                    DeleteGlobalSchedule(globalSchedule.CarrierId, globalSchedule.CoverageId, DataModel);
                    return;
                }

                List<DLinq.GlobalCoveragesSchedule> schedule = (from e in DataModel.GlobalCoveragesSchedules
                                                                where e.CarrierId == globalSchedule.CarrierId && e.CoverageId == globalSchedule.CoverageId && e.IsDeleted == false
                                                                select e).ToList();

                foreach (DLinq.GlobalCoveragesSchedule entry in schedule)
                {
                    IncomingScheduleEntry tmpEntry = globalSchedule.IncomingScheduleList.FirstOrDefault(s => s.CoveragesScheduleId == entry.CoveragesScheduleId);
                    if (tmpEntry == null)
                        entry.IsDeleted = true;
                }

                foreach (IncomingScheduleEntry _globalCoveragesShedule in globalSchedule.IncomingScheduleList)
                {
                    DLinq.MasterScheduleType _objSheduleType = new DLinq.MasterScheduleType();

                    gcs = (from e in DataModel.GlobalCoveragesSchedules
                           where e.CoveragesScheduleId == _globalCoveragesShedule.CoveragesScheduleId
                           select e).FirstOrDefault();

                    if (gcs == null)
                    {
                        gcs = new DLinq.GlobalCoveragesSchedule
                        {
                            CoveragesScheduleId = _globalCoveragesShedule.CoveragesScheduleId,
                            FromRange = _globalCoveragesShedule.FromRange,
                            ToRange = _globalCoveragesShedule.ToRange,
                            EffectiveToDate = _globalCoveragesShedule.EffectiveToDate,
                            EffectiveFromDate = _globalCoveragesShedule.EffectiveFromDate,
                            Rate = _globalCoveragesShedule.Rate,
                            ScheduleTypeId = globalSchedule.ScheduleTypeId,
                            CoverageId = globalSchedule.CoverageId,
                            CarrierId = globalSchedule.CarrierId,
                            IsDeleted = false
                        };
                        DataModel.AddToGlobalCoveragesSchedules(gcs);
                    }
                    else
                    {
                        gcs.FromRange = _globalCoveragesShedule.FromRange;
                        gcs.ToRange = _globalCoveragesShedule.ToRange;
                        gcs.EffectiveToDate = _globalCoveragesShedule.EffectiveToDate;
                        gcs.EffectiveFromDate = _globalCoveragesShedule.EffectiveFromDate;
                        gcs.Rate = _globalCoveragesShedule.Rate;
                        gcs.ScheduleTypeId = globalSchedule.ScheduleTypeId;
                        gcs.CoverageId = globalSchedule.CoverageId;
                        gcs.CarrierId = globalSchedule.CarrierId;
                    }
                }

                DataModel.SaveChanges();
            }

        }

        private static void DeleteGlobalSchedule(Guid carrierId, Guid coverageId, DLinq.CommissionDepartmentEntities DataModel)
        {
            List<DLinq.GlobalCoveragesSchedule> schedule = DataModel.GlobalCoveragesSchedules.Where(s => s.CarrierId == carrierId && s.CoverageId == coverageId).ToList();
            foreach (DLinq.GlobalCoveragesSchedule entry in schedule)
                entry.IsDeleted = true;
            DataModel.SaveChanges();
        }

        public static void AddUpdatePolicySchedule(PolicyIncomingSchedule policySchedule)
        {
            using (DLinq.CommissionDepartmentEntities DataModel = Entity.DataModel)
            {
                DLinq.PolicyIncomingAdvancedSchedule gcs = null;

                if (policySchedule.IncomingScheduleList == null || policySchedule.IncomingScheduleList.Count == 0)
                {
                    DeletePolicySchedule(policySchedule.PolicyId);
                    return;
                }

                List<DLinq.PolicyIncomingAdvancedSchedule> schedule = (from e in DataModel.PolicyIncomingAdvancedSchedules
                                                                       where e.PolicyId == policySchedule.PolicyId
                                                                       select e).ToList();

                foreach (DLinq.PolicyIncomingAdvancedSchedule entry in schedule)
                {
                    IncomingScheduleEntry tmpEntry = policySchedule.IncomingScheduleList.FirstOrDefault(s => s.CoveragesScheduleId == entry.IncomingAdvancedScheduleId);
                    if (tmpEntry == null)
                        DataModel.DeleteObject(entry);
                }

                foreach (IncomingScheduleEntry _policyIncominShedule in policySchedule.IncomingScheduleList)
                {
                    DLinq.MasterScheduleType _objSheduleType = new DLinq.MasterScheduleType();

                    gcs = (from e in DataModel.PolicyIncomingAdvancedSchedules
                           where e.IncomingAdvancedScheduleId == _policyIncominShedule.CoveragesScheduleId
                           select e).FirstOrDefault();

                    if (gcs == null)
                    {
                        gcs = new DLinq.PolicyIncomingAdvancedSchedule
                        {
                            PolicyId = policySchedule.PolicyId,
                            IncomingAdvancedScheduleId = _policyIncominShedule.CoveragesScheduleId,
                            FromRange = _policyIncominShedule.FromRange,
                            ToRange = _policyIncominShedule.ToRange,
                            EffectiveToDate = _policyIncominShedule.EffectiveToDate,
                            EffectiveFromDate = _policyIncominShedule.EffectiveFromDate,
                            Rate = _policyIncominShedule.Rate,
                            ScheduleTypeId = policySchedule.ScheduleTypeId
                        };
                        DataModel.AddToPolicyIncomingAdvancedSchedules(gcs);
                    }
                    else
                    {
                        gcs.FromRange = _policyIncominShedule.FromRange;
                        gcs.ToRange = _policyIncominShedule.ToRange;
                        gcs.EffectiveToDate = _policyIncominShedule.EffectiveToDate;
                        gcs.EffectiveFromDate = _policyIncominShedule.EffectiveFromDate;
                        gcs.Rate = _policyIncominShedule.Rate;
                        gcs.ScheduleTypeId = policySchedule.ScheduleTypeId;
                    }
                }

                DataModel.SaveChanges();
            }
        }

        public static void DeletePolicySchedule(Guid PolicyId)
        {
            using (DLinq.CommissionDepartmentEntities DataModel = Entity.DataModel)
            {
                List<DLinq.PolicyIncomingAdvancedSchedule> schedule = DataModel.PolicyIncomingAdvancedSchedules.Where(s => s.PolicyId == PolicyId).ToList();
                foreach (DLinq.PolicyIncomingAdvancedSchedule entry in schedule)
                {
                    DataModel.DeleteObject(entry);
                    DataModel.SaveChanges();
                }
            }
        }

        #endregion

        #region  "Data members aka - public properties"

        [DataMember]
        public Guid CarrierId { get; set; }
        [DataMember]
        public Guid CoverageId { get; set; }
        [DataMember]
        public string ScheduleTypeName { get; set; }
        [DataMember]
        public string CarrierName { get; set; }
        [DataMember]
        public string ProductName { get; set; }
        [DataMember]
        public int ScheduleTypeId { get; set; }
        [DataMember]
        public List<IncomingScheduleEntry> IncomingScheduleList { get; set; }
        [DataMember]
        public bool IsModified { get; set; }
        #endregion

        public static GlobalIncomingSchedule GetGlobalIncomingSchedule(Guid carrierId, Guid coverageId)
        {
            using (DLinq.CommissionDepartmentEntities DataModel = Entity.DataModel)
            {
                GlobalIncomingSchedule globalIncomingSchedule = new GlobalIncomingSchedule { CarrierId = carrierId, CoverageId = coverageId };
                foreach (DLinq.GlobalCoveragesSchedule sch in DataModel.GlobalCoveragesSchedules)
                {
                    if (sch.CarrierId == carrierId && sch.CoverageId == coverageId && sch.IsDeleted == false)
                    {
                        IncomingScheduleEntry scheduleEntry = new IncomingScheduleEntry
                        {
                            CoveragesScheduleId = sch.CoveragesScheduleId,
                            FromRange = sch.FromRange,
                            ToRange = sch.ToRange,
                            EffectiveFromDate = sch.EffectiveFromDate,
                            EffectiveToDate = sch.EffectiveToDate,
                            Rate = sch.Rate,
                        };

                        if (globalIncomingSchedule.IncomingScheduleList == null)
                        {
                            globalIncomingSchedule.IncomingScheduleList = new List<IncomingScheduleEntry>();

                            globalIncomingSchedule.CoverageId = coverageId;
                            globalIncomingSchedule.CarrierId = carrierId;
                            globalIncomingSchedule.CarrierName = sch.Carrier.CarrierName;
                            globalIncomingSchedule.ProductName = sch.Coverage.ProductName;
                            globalIncomingSchedule.ScheduleTypeId = sch.ScheduleTypeId;
                            globalIncomingSchedule.ScheduleTypeName = sch.MasterScheduleType.Name;
                        }

                        globalIncomingSchedule.IncomingScheduleList.Add(scheduleEntry);
                    }
                }
                return globalIncomingSchedule;
            }
        }

        public static PolicyIncomingSchedule GetPolicyIncomingSchedule(Guid PolicyId)
        {
            using (DLinq.CommissionDepartmentEntities DataModel = Entity.DataModel)
            {
                PolicyIncomingSchedule policyIncomingSchedule = new PolicyIncomingSchedule { PolicyId = PolicyId };
                var PolicyIncomingSchedules = from PIS in DataModel.PolicyIncomingAdvancedSchedules
                                              where PIS.PolicyId == PolicyId
                                              select PIS;
                foreach (DLinq.PolicyIncomingAdvancedSchedule sch in PolicyIncomingSchedules)
                {
                    if (sch.PolicyId == PolicyId)
                    {
                        IncomingScheduleEntry scheduleEntry = new IncomingScheduleEntry
                        {
                            CoveragesScheduleId = sch.IncomingAdvancedScheduleId,
                            FromRange = sch.FromRange,
                            ToRange = sch.ToRange,
                            EffectiveFromDate = sch.EffectiveFromDate,
                            EffectiveToDate = sch.EffectiveToDate,
                            Rate = sch.Rate,
                        };

                        if (policyIncomingSchedule.IncomingScheduleList == null)
                        {
                            policyIncomingSchedule.IncomingScheduleList = new List<IncomingScheduleEntry>();

                            policyIncomingSchedule.PolicyId = PolicyId;
                            policyIncomingSchedule.ScheduleTypeId = sch.ScheduleTypeId.Value;
                            policyIncomingSchedule.ScheduleTypeName = sch.MasterScheduleType.Name;
                        }

                        policyIncomingSchedule.IncomingScheduleList.Add(scheduleEntry);
                    }
                }
                return policyIncomingSchedule;
            }
        }

        public static void ChangeScheduleType(Guid carrierId, Guid coverageId, int scheduleType)
        {
            using (DLinq.CommissionDepartmentEntities DataModel = Entity.DataModel)
            {
                List<DLinq.GlobalCoveragesSchedule> gcs = null;

                gcs = (from e in DataModel.GlobalCoveragesSchedules
                       where e.CoverageId == coverageId && e.CarrierId == carrierId
                       select e).ToList();

                foreach (DLinq.GlobalCoveragesSchedule gc in gcs)
                {
                    gc.ScheduleTypeId = scheduleType;
                    gc.MasterScheduleType = DataModel.MasterScheduleTypes.FirstOrDefault(s => s.ScheduleTypeId == scheduleType);
                }
                DataModel.SaveChanges();
            }
        }
    }

    [DataContract]
    public class OutgoingSchedule
    {
        public static void AddUpdatePolicyOutgoingSchedule(PolicyOutgoingSchedule policySchedule)
        {
            using (DLinq.CommissionDepartmentEntities DataModel = Entity.DataModel)
            {
                DLinq.PolicyOutgoingAdvancedSchedule gcs = null;

                if (policySchedule.OutgoingScheduleList == null || policySchedule.OutgoingScheduleList.Count == 0)
                {
                    DeleteOutgoingPolicySchedule(policySchedule.PolicyId);
                    return;
                }

                List<DLinq.PolicyOutgoingAdvancedSchedule> schedule = (from e in DataModel.PolicyOutgoingAdvancedSchedules
                                                                       where e.PolicyId == policySchedule.PolicyId
                                                                       select e).ToList();

                foreach (DLinq.PolicyOutgoingAdvancedSchedule entry in schedule)
                {
                    OutgoingScheduleEntry tmpEntry = policySchedule.OutgoingScheduleList.FirstOrDefault(s => s.CoveragesScheduleId == entry.OutgoingAdvancedScheduleId);
                    if (tmpEntry == null)
                        DataModel.DeleteObject(entry);
                }

                foreach (OutgoingScheduleEntry _policyOutgoingShedule in policySchedule.OutgoingScheduleList)
                {
                    DLinq.MasterScheduleType _objSheduleType = new DLinq.MasterScheduleType();

                    gcs = (from e in DataModel.PolicyOutgoingAdvancedSchedules
                           where e.OutgoingAdvancedScheduleId == _policyOutgoingShedule.CoveragesScheduleId
                           select e).FirstOrDefault();

                    if (gcs == null)
                    {
                        gcs = new DLinq.PolicyOutgoingAdvancedSchedule
                        {
                            PolicyId = policySchedule.PolicyId,
                            IsPrimaryAgent = _policyOutgoingShedule.IsPrimaryAgent,
                            PayeeUserCredentialId = _policyOutgoingShedule.PayeeUserCredentialId,
                            PayeeName = _policyOutgoingShedule.PayeeName,
                            OutgoingAdvancedScheduleId = _policyOutgoingShedule.CoveragesScheduleId,
                            FromRange = _policyOutgoingShedule.FromRange,
                            ToRange = _policyOutgoingShedule.ToRange,
                            EffectiveToDate = _policyOutgoingShedule.EffectiveToDate,
                            EffectiveFromDate = _policyOutgoingShedule.EffectiveFromDate,
                            Rate = _policyOutgoingShedule.Rate,
                            ScheduleTypeId = policySchedule.ScheduleTypeId,
                            ModifiedOn = DateTime.Now
                        };

                        DLinq.UserDetail userDetail = DataModel.UserDetails.FirstOrDefault(s => s.UserCredentialId == _policyOutgoingShedule.PayeeUserCredentialId);
                        if (userDetail.AddPayeeOn == null)
                            userDetail.AddPayeeOn = DateTime.Now;

                        DataModel.AddToPolicyOutgoingAdvancedSchedules(gcs);
                    }
                    else
                    {
                        gcs.IsPrimaryAgent = _policyOutgoingShedule.IsPrimaryAgent;
                        gcs.PayeeUserCredentialId = _policyOutgoingShedule.PayeeUserCredentialId;
                        gcs.PayeeName = _policyOutgoingShedule.PayeeName;
                        gcs.FromRange = _policyOutgoingShedule.FromRange;
                        gcs.ToRange = _policyOutgoingShedule.ToRange;
                        gcs.EffectiveToDate = _policyOutgoingShedule.EffectiveToDate;
                        gcs.EffectiveFromDate = _policyOutgoingShedule.EffectiveFromDate;
                        gcs.Rate = _policyOutgoingShedule.Rate;
                        gcs.ScheduleTypeId = policySchedule.ScheduleTypeId;
                        gcs.ModifiedOn = DateTime.Now;
                    }
                }

                DataModel.SaveChanges();
            }
        }

        public static void DeleteOutgoingPolicySchedule(Guid PolicyId)
        {
            using (DLinq.CommissionDepartmentEntities DataModel = Entity.DataModel)
            {
                List<DLinq.PolicyIncomingAdvancedSchedule> schedule = DataModel.PolicyIncomingAdvancedSchedules.Where(s => s.PolicyId == PolicyId).ToList();
                foreach (DLinq.PolicyIncomingAdvancedSchedule entry in schedule)
                    DataModel.DeleteObject(entry);
                DataModel.SaveChanges();
            }
        }

        public static PolicyOutgoingSchedule GetPolicyOutgoingSchedule(Guid PolicyId)
        {
            using (DLinq.CommissionDepartmentEntities DataModel = Entity.DataModel)
            {
                PolicyOutgoingSchedule policyOutgoingSchedule = new PolicyOutgoingSchedule { PolicyId = PolicyId };
                foreach (DLinq.PolicyOutgoingAdvancedSchedule sch in DataModel.PolicyOutgoingAdvancedSchedules)
                {
                    if (sch.PolicyId == PolicyId)
                    {
                        OutgoingScheduleEntry scheduleEntry = new OutgoingScheduleEntry
                        {
                            IsPrimaryAgent = sch.IsPrimaryAgent,
                            PayeeUserCredentialId = sch.PayeeUserCredentialId.Value,
                            PayeeName = sch.PayeeName,
                            CoveragesScheduleId = sch.OutgoingAdvancedScheduleId,
                            FromRange = sch.FromRange,
                            ToRange = sch.ToRange,
                            EffectiveFromDate = sch.EffectiveFromDate,
                            EffectiveToDate = sch.EffectiveToDate,
                            Rate = sch.Rate,
                        };

                        if (policyOutgoingSchedule.OutgoingScheduleList == null)
                        {
                            policyOutgoingSchedule.OutgoingScheduleList = new List<OutgoingScheduleEntry>();

                            policyOutgoingSchedule.PolicyId = PolicyId;
                            policyOutgoingSchedule.ScheduleTypeId = sch.ScheduleTypeId.Value;
                            policyOutgoingSchedule.ScheduleTypeName = sch.MasterScheduleType.Name;
                        }

                        policyOutgoingSchedule.OutgoingScheduleList.Add(scheduleEntry);
                    }
                }
                return policyOutgoingSchedule;
            }
        }

        public static IList<PolicyOutgoingScheduleList> GetPolicyOutgoingScheduleById(Guid licenseeId)
        {
            List<PolicyOutgoingScheduleList> getOutgoingSchedulelist = new List<PolicyOutgoingScheduleList>();
            //using (DLinq.CommissionDepartmentEntities DataModel = Entity.DataModel)
            //{
            //    // var checkLicenseeIdValid = (from licensee in DataModel.Licensees where (licensee.LicenseeId == licenseeId)select licensee).FirstOrDefault();

            //    //if (checkLicenseeIdValid == null)
            //    //{
            //    //    getOutgoingSchedulelist = null;
            //    //    return getOutgoingSchedulelist;
            //    //}
            //}
            //DLinq.CommissionDepartmentEntities ctx = new DLinq.CommissionDepartmentEntities(); //create your entity object here
            //EntityConnection ec = (EntityConnection)ctx.Connection;
            //SqlConnection sc = (SqlConnection)ec.StoreConnection; //get the SQLConnection that your entity object would use
            //string adoConnStr = sc.ConnectionString;
            using (SqlConnection con = new SqlConnection(DBConnection.GetConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand("usp_GetOutgoingSchedulelist", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@licenseeId", licenseeId);
                    con.Open();

                    SqlDataReader reader = cmd.ExecuteReader();
                    // Call Read before accessing data. 
                    while (reader.Read())
                    {
                        try
                        {
                            PolicyOutgoingScheduleList policyOutgoinglist = new PolicyOutgoingScheduleList();
                            policyOutgoinglist.UserCredentialId = (Guid)reader["UserCredentialId"];
                            policyOutgoinglist.FirstName = Convert.ToString(reader["FirstName"]);
                            policyOutgoinglist.LastName = Convert.ToString(reader["LastName"]);
                            policyOutgoinglist.NickName = Convert.ToString(reader["NickName"]);
                            getOutgoingSchedulelist.Add(policyOutgoinglist);

                        }

                        catch (Exception ex)
                        {
                            throw ex;
                        }

                    }

                }
            }
            return getOutgoingSchedulelist;
        }
        /// <summary>
        /// Author:Ankit 
        /// CreatedOn:17-10-18
        /// Purpose:getting list of  outgoing schedule based on policyId
        /// </summary>
        /// <param name="PolicyId"></param>
        /// <param name="isrecordfound"></param>
        /// <returns></returns>
        public static IList<PolicyOutgoingSchedules> GetPolicyOutgoingScheduleById(Guid PolicyId, out int isrecordfound)
        {

            IList<PolicyOutgoingSchedules> getOutgoingSchedulelist = null;
            using (DLinq.CommissionDepartmentEntities DataModel = Entity.DataModel)
            {
                try
                {
                    //PolicyOutgoingScheduleDetails policyOutgoingSchedule = new PolicyOutgoingScheduleDetails { PolicyId = PolicyId };
                    if (PolicyId != null)
                    {


                        getOutgoingSchedulelist = (from PolicyOutgoingSchedules in DataModel.PolicyOutgoingSchedules
                                                   join UserDetail in DataModel.UserDetails on PolicyOutgoingSchedules.PayeeUserCredentialId equals UserDetail.UserCredentialId
                                                   where PolicyOutgoingSchedules.PolicyId == PolicyId
                                                   select new PolicyOutgoingSchedules
                                                   {
                                                       PolicyId = (Guid)PolicyOutgoingSchedules.PolicyId,
                                                       IsPrimaryAgent = PolicyOutgoingSchedules.IsPrimaryAgent,
                                                       PayeeUserCredentialId = PolicyOutgoingSchedules.PayeeUserCredentialId.Value,
                                                       OutgoingScheduleId = PolicyOutgoingSchedules.OutgoingScheduleId,
                                                       FirstYearPercentage = PolicyOutgoingSchedules.FirstYearPercentage,
                                                       RenewalPercentage = PolicyOutgoingSchedules.RenewalPercentage,
                                                       ScheduleTypeId = (int)PolicyOutgoingSchedules.ScheduleTypeId,
                                                       CustomEndDate = PolicyOutgoingSchedules.CustomEndDate,
                                                       CustomStartDate = PolicyOutgoingSchedules.CustomStartDate,
                                                       splitpercentage = PolicyOutgoingSchedules.SplitPercent,
                                                       PayeeName = UserDetail.NickName,
                                                       TierNumber = PolicyOutgoingSchedules.TierNumber

                                                   }).OrderBy(p => p.PayeeName).ToList();

                        isrecordfound = 1;
                    }
                    else
                    {
                        isrecordfound = 0;
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return getOutgoingSchedulelist;

        }


        public static List<Guid> GetAllPoliciesForUser(Guid userCredId)
        {
            using (DLinq.CommissionDepartmentEntities DataModel = Entity.DataModel)
            {
                List<DLinq.PolicyOutgoingAdvancedSchedule> schedule = (from e in DataModel.PolicyOutgoingAdvancedSchedules
                                                                       where e.PayeeUserCredentialId == userCredId
                                                                       select e).OrderBy(s => s.PolicyId).ToList();

                List<Guid> policies = new List<Guid>();
                foreach (DLinq.PolicyOutgoingAdvancedSchedule entry in schedule)
                {
                    Guid policyId = policies.FirstOrDefault(s => s == entry.PolicyId);
                    if (policyId == null)
                        policies.Add(entry.PolicyId.Value);
                }
                return policies;
            }
        }

        public static bool IsUserPresentAsPayee(Guid userId)
        {
            using (DLinq.CommissionDepartmentEntities DataModel = Entity.DataModel)
            {
                int count = DataModel.PolicyOutgoingAdvancedSchedules.Where(s => s.PayeeUserCredentialId == userId).Count();
                if (count != 0)
                    return true;
                else
                    return false;
            }
        }
    }

    [DataContract]
    public class PayorSchedules
    {
        #region Public Properties
        [DataMember]
        public Guid IncomingScheduleID { get; set; }

        [DataMember]
        public string Payor { get; set; }
        [DataMember]
        public string Carrier { get; set; }
        [DataMember]
        public string Coverage { get; set; }
        [DataMember]
        public string ProductType { get; set; }

        #endregion
    }

    [DataContract]
    public struct ScheduleQuery
    {
        [DataMember]
        public Guid PayorID { get; set; }

        [DataMember]
        public Guid CarrierID { get; set; }

        [DataMember]
        public Guid CoverageID { get; set; }

        [DataMember]
        public Guid LicenseeID { get; set; }

        [DataMember]
        public string ProductType { get; set; }

        [DataMember]
        public int IncomingPaymentTypeID { get; set; }
    }

    [DataContract]
    public enum Mode
    {
        [EnumMember]
        Standard = 0,

        [EnumMember]
        Custom = 1
    }

    [DataContract]
    public enum CustomMode
    {
        [EnumMember]
        Graded = 1,

        [EnumMember]
        NonGraded = 2
    }
    [DataContract]
    public class Graded
    {
        [DataMember]
        public double From { get; set; }
        [DataMember]
        public double To { get; set; }
        [DataMember]
        public double Percent { get; set; }
        [DataMember]
        public int CustomType { get; set; }

    }
    public class NonGraded
    {
        [DataMember]
        public int Year { get; set; }

        [DataMember]
        public double Percent { get; set; }
        [DataMember]
        public int CustomType { get; set; }

    }


    [DataContract]
    public class PayorIncomingSchedule
    {
        #region Public Properties
        [DataMember]
        public string PayorName { get; set; }
        [DataMember]
        public string CarrierName { get; set; }
        [DataMember]
        public string CoverageName { get; set; }

        [DataMember]
        public Guid IncomingScheduleID { get; set; }
        [DataMember]
        public Guid LicenseeID { get; set; }
        [DataMember]
        public Guid PayorID { get; set; }
        [DataMember]
        public Guid CarrierID { get; set; }
        [DataMember]
        public Guid CoverageID { get; set; }
        [DataMember]
        public int ScheduleTypeId { get; set; }
        [DataMember]
        public double? FirstYearPercentage { get; set; }
        [DataMember]
        public string StringRenewalPercentage { get; set; }
        [DataMember]
        public string StringFirstYearPercentage { get; set; }
        [DataMember]
        public double? RenewalPercentage { get; set; }
        [DataMember]
        public double? SplitPercentage { get; set; }
        [DataMember]
        public string StringSplitPercentage { get; set; }
        [DataMember]
        public int IncomingPaymentTypeID { get; set; }
        [DataMember]
        public int Advance { get; set; }
        [DataMember]
        public string ProductType { get; set; }
        [DataMember]
        public Guid? CreatedBy { get; set; }
        [DataMember]
        public Guid? ModifiedBy { get; set; }
        [DataMember]
        public DateTime? CreatedOn { get; set; }
        [DataMember]
        public DateTime? ModifiedOn { get; set; }
        [DataMember]
        public string IncomingPaymentTypeName { get; set; }
        [DataMember]
        public string ScheduleType { get; set; }
        [DataMember]
        public string Title { get; set; }
        [DataMember]
        public Boolean? IsNamedSchedule { get; set; }
        [DataMember]
        public List<Graded> GradedSchedule { get; set; }

        CustomMode _mode = CustomMode.Graded;        [DataMember]        public CustomMode CustomType        {            get { return _mode; }            set { _mode = value; }        }
        [DataMember]
        public Mode Mode { get; set; }
        [DataMember]
        public string StringMode { get; set; }


        [DataMember]
        public List<NonGraded> NonGradedSchedule { get; set; }


        #endregion
        //public static void SaveSchedule(PayorIncomingSchedule schedule, int overwrite = 0)
        //{

        //    try
        //    {
        //        ActionLogger.Logger.WriteLog("SaveSchedule schedule" + schedule.ToStringDump(), true);
        //        using (SqlConnection con = new SqlConnection(DBConnection.GetConnectionString()))
        //        {
        //            using (SqlCommand cmd = new SqlCommand("sp_savePayorSchedule", con))
        //            {
        //                cmd.CommandType = System.Data.CommandType.StoredProcedure;
        //                cmd.Parameters.AddWithValue("@ScheduleID", schedule.IncomingScheduleID);
        //                cmd.Parameters.AddWithValue("@LicenseeID", schedule.LicenseeID);
        //                cmd.Parameters.AddWithValue("@PayorID", schedule.PayorID);
        //                cmd.Parameters.AddWithValue("@CarrierID", schedule.CarrierID);
        //                cmd.Parameters.AddWithValue("@CoverageID", schedule.CoverageID);
        //                cmd.Parameters.AddWithValue("@ProductType", schedule.ProductType);
        //                cmd.Parameters.AddWithValue("@FirstYear", schedule.FirstYearPercentage);
        //                cmd.Parameters.AddWithValue("@Renewal", schedule.RenewalPercentage);
        //                cmd.Parameters.AddWithValue("@ScheduleTypeID", schedule.ScheduleTypeId);
        //                cmd.Parameters.AddWithValue("@IncomingPaymentTypeId", schedule.IncomingPaymentTypeID);
        //                cmd.Parameters.AddWithValue("@SplitPercentage", schedule.SplitPercentage);
        //                cmd.Parameters.AddWithValue("@Advance", schedule.Advance);
        //                cmd.Parameters.AddWithValue("@CreatedBy", schedule.CreatedBy);
        //                cmd.Parameters.AddWithValue("@ModifiedBy", schedule.ModifiedBy);
        //                cmd.Parameters.AddWithValue("@Overwrite", overwrite);
        //                con.Open();

        //                cmd.ExecuteNonQuery();
        //            }
        //        }
        //        // isRecordExist = false;
        //    }
        //    catch (Exception ex)
        //    {
        //        ActionLogger.Logger.WriteLog("SaveSchedule exception: " + ex.Message, true);
        //        throw ex;
        //    }

        //    //}
        //    //else
        //    //{
        //    //    isRecordExist = true;
        //    //}

        //}
        #region

        public static void SaveSchedule(PayorIncomingSchedule schedule, int overwrite = 0)
        {
            //using (var txscope = new TransactionScope(TransactionScopeOption.RequiresNew))
            //{
            try
            {
                if (schedule.Mode == Mode.Custom)
                {
                    schedule.FirstYearPercentage = 0;
                    schedule.RenewalPercentage = 0;

                }
                ActionLogger.Logger.WriteLog("SaveSchedule schedule" + schedule.ToStringDump(), true);

                using (SqlConnection con = new SqlConnection(DBConnection.GetConnectionString()))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_savePayorSchedule", con))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ScheduleID", schedule.IncomingScheduleID);
                        cmd.Parameters.AddWithValue("@LicenseeID", schedule.LicenseeID);
                        cmd.Parameters.AddWithValue("@PayorID", schedule.PayorID);
                        cmd.Parameters.AddWithValue("@CarrierID", schedule.CarrierID);
                        cmd.Parameters.AddWithValue("@CoverageID", schedule.CoverageID);
                        cmd.Parameters.AddWithValue("@ProductType", schedule.ProductType);
                        cmd.Parameters.AddWithValue("@FirstYear", schedule.FirstYearPercentage);
                        cmd.Parameters.AddWithValue("@Renewal", schedule.RenewalPercentage);
                        cmd.Parameters.AddWithValue("@ScheduleTypeID", schedule.ScheduleTypeId);
                        cmd.Parameters.AddWithValue("@IncomingPaymentTypeId", schedule.IncomingPaymentTypeID);
                        cmd.Parameters.AddWithValue("@SplitPercentage", schedule.SplitPercentage);
                        cmd.Parameters.AddWithValue("@Advance", schedule.Advance);
                        cmd.Parameters.AddWithValue("@CreatedBy", schedule.CreatedBy);
                        cmd.Parameters.AddWithValue("@ModifiedBy", schedule.ModifiedBy);
                        cmd.Parameters.AddWithValue("@Overwrite", overwrite);
                        cmd.Parameters.AddWithValue("@Mode", schedule.Mode);
                        cmd.Parameters.AddWithValue("@CustomType", schedule.CustomType);
                        con.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
                if (schedule.Mode == Mode.Custom)
                {
                    if (schedule.CustomType == CustomMode.Graded)
                    {
                        SaveGradedSchedule(schedule);
                    }
                    else
                    {
                        SaveNonGradedSchedule(schedule);
                    }
                    OverwriteCustomSchedule(schedule.IncomingScheduleID, overwrite);

                }
            }
            catch (Exception ex)
            {
                // txscope.Dispose();
                ActionLogger.Logger.WriteLog("SaveSchedule exception: " + ex.Message, true);
                throw ex;

            }
            // }
        }

        public static void SaveGradedSchedule(PayorIncomingSchedule schedule, Guid? policyId = null)
        {
            ActionLogger.Logger.WriteLog("SaveGradedSchedule:Processing begins with scheduleId" + schedule.IncomingScheduleID, true);
            try
            {
                DeleteCustomSchedule(schedule.IncomingScheduleID, policyId);
                foreach (var Gradedchedule in schedule.GradedSchedule)
                {
                    using (SqlConnection con = new SqlConnection(DBConnection.GetConnectionString()))
                    {
                        using (SqlCommand cmd = new SqlCommand("Usp_saveCustomSchedule", con))
                        {
                            cmd.CommandType = System.Data.CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@ScheduleID", schedule.IncomingScheduleID);
                            cmd.Parameters.AddWithValue("@GradedFrom", Gradedchedule.From);
                            cmd.Parameters.AddWithValue("@GradedTo", Gradedchedule.To);
                            cmd.Parameters.AddWithValue("@GradedPercent", Gradedchedule.Percent);
                            cmd.Parameters.AddWithValue("@PolicyID", policyId);

                            con.Open();
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ActionLogger.Logger.WriteLog("SaveGradedSchedule:Exception occurs while saving with scheduleId" + schedule.IncomingScheduleID, true);
                throw ex;
            }
        }
        public static void OverwriteCustomSchedule(Guid scheduleID, int overwrite)        {            ActionLogger.Logger.WriteLog("OverwriteCustomSchedule request - " + scheduleID + ", overWrite: " + overwrite, true);            try            {
                using (SqlConnection con = new SqlConnection(DBConnection.GetConnectionString()))                {                    using (SqlCommand cmd = new SqlCommand("Usp_OverwriteCustomSchedule", con))                    {                        cmd.CommandType = System.Data.CommandType.StoredProcedure;                        cmd.Parameters.AddWithValue("@PayorScheduleID", scheduleID);                        cmd.Parameters.AddWithValue("@Overwrite", overwrite);                        con.Open();                        cmd.ExecuteNonQuery();                    }                }            }            catch (Exception ex)            {                ActionLogger.Logger.WriteLog("OverwriteCustomSchedule ex: " + ex.Message, true);                throw ex;            }        }
        public static void SaveNonGradedSchedule(PayorIncomingSchedule schedule, Guid? policyId = null)
        {
            ActionLogger.Logger.WriteLog("SaveNonGradedSchedule:Processing begins with scheduleId" + schedule.IncomingScheduleID, true);
            try
            {
                DeleteCustomSchedule(schedule.IncomingScheduleID, policyId);
                foreach (var NonGradedchedule in schedule.NonGradedSchedule)
                {
                    using (SqlConnection con = new SqlConnection(DBConnection.GetConnectionString()))
                    {
                        using (SqlCommand cmd = new SqlCommand("usp_saveCustomSchedule", con))
                        {
                            cmd.CommandType = System.Data.CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@ScheduleID", schedule.IncomingScheduleID);
                            cmd.Parameters.AddWithValue("@NonGradedPercent", NonGradedchedule.Percent);
                            cmd.Parameters.AddWithValue("@Year", NonGradedchedule.Year);
                            cmd.Parameters.AddWithValue("@PolicyID", policyId);
                            con.Open();
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ActionLogger.Logger.WriteLog("SaveNonGradedSchedule:Exception occurs while saving with scheduleId" + schedule.IncomingScheduleID, true);
                throw ex;
            }
        }
        public static void DeleteCustomSchedule(Guid scheduleId, Guid? policyId = null)
        {
            ActionLogger.Logger.WriteLog("DeleteCustomSchedule:Processing begins with scheduleId" + scheduleId, true);
            try
            {
                using (SqlConnection con = new SqlConnection(DBConnection.GetConnectionString()))
                {
                    using (SqlCommand cmd = new SqlCommand("usp_DeleteCustomSchedule", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ScheduleId", scheduleId);
                        cmd.Parameters.AddWithValue("@PolicyID", policyId);
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                ActionLogger.Logger.WriteLog("DeleteCustomSchedule:Exception occurs while delete with scheduleId" + scheduleId + " " + ex.Message, true);
                throw ex;
            }
        }
        public static void DeletePolicySchedule(Guid policyId)
        {
            ActionLogger.Logger.WriteLog("DeletePolicySchedule:Processing begins with scheduleId" + policyId, true);
            try
            {
                using (SqlConnection con = new SqlConnection(DBConnection.GetConnectionString()))
                {
                    using (SqlCommand cmd = new SqlCommand("usp_DeletePolicySchedule", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@PolicyId", policyId);
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                ActionLogger.Logger.WriteLog("DeleteCustomSchedule:Exception occurs while delete with policyId" + policyId + " " + ex.Message, true);
                throw ex;
            }
        }
        /// <summary>
        /// Created By:Ankit Khandelwal
        /// Createdon:25-7-2019
        /// Purpose: Add update the named Schedule 
        /// </summary>
        /// <param name="schedule"></param>
        public static void SaveNamedSchedule(PayorIncomingSchedule schedule)
        {
            try
            {
                ActionLogger.Logger.WriteLog("SaveSchedule schedule" + schedule.ToStringDump(), true);
                if (schedule.Mode == Mode.Custom)
                {
                    schedule.FirstYearPercentage = 0;
                    schedule.RenewalPercentage = 0;

                }
                using (SqlConnection con = new SqlConnection(DBConnection.GetConnectionString()))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_saveNamedSchedule", con))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ScheduleID", schedule.IncomingScheduleID);
                        cmd.Parameters.AddWithValue("@LicenseeID", schedule.LicenseeID);
                        cmd.Parameters.AddWithValue("@FirstYear", schedule.FirstYearPercentage);
                        cmd.Parameters.AddWithValue("@Renewal", schedule.RenewalPercentage);
                        cmd.Parameters.AddWithValue("@ScheduleTypeID", schedule.ScheduleTypeId);
                        cmd.Parameters.AddWithValue("@IncomingPaymentTypeId", schedule.IncomingPaymentTypeID);
                        cmd.Parameters.AddWithValue("@SplitPercentage", schedule.SplitPercentage);
                        cmd.Parameters.AddWithValue("@Advance", schedule.Advance);
                        cmd.Parameters.AddWithValue("@CreatedBy", schedule.CreatedBy);
                        cmd.Parameters.AddWithValue("@ModifiedBy", schedule.ModifiedBy);
                        cmd.Parameters.AddWithValue("@Title", schedule.Title);
                        cmd.Parameters.AddWithValue("@Mode", schedule.Mode);
                        cmd.Parameters.AddWithValue("@CustomType", schedule.CustomType);

                        con.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
                if (schedule.Mode == Mode.Custom)
                {
                    if (schedule.CustomType == CustomMode.Graded)
                    {
                        SaveGradedSchedule(schedule);
                    }
                    else
                    {
                        SaveNonGradedSchedule(schedule);
                    }


                }
            }
            catch (Exception ex)
            {
                ActionLogger.Logger.WriteLog("SaveSchedule exception: " + ex.Message, true);
                throw ex;
            }
        }


        #endregion




        public static void DeleteSchedule(Guid incomingScheduleId)
        {
            try
            {
                ActionLogger.Logger.WriteLog("DeleteSchedule ScheduleID" + incomingScheduleId, true);
                using (SqlConnection con = new SqlConnection(DBConnection.GetConnectionString()))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_deletePayorSchedule", con))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ScheduleID", incomingScheduleId);
                        con.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                ActionLogger.Logger.WriteLog("DeleteSchedule exception: " + ex.Message, true);
                throw ex;
            }
        }

        public static List<PayorIncomingSchedule> GetAllSchedules(Guid LicenseeId, ListParams listParams, out double recordCount)
        {
            List<PayorIncomingSchedule> lstSchedules = new List<PayorIncomingSchedule>();
            try
            {
                ActionLogger.Logger.WriteLog("GetAllSchedules LicenseeID" + LicenseeId, true);
                using (SqlConnection con = new SqlConnection(DBConnection.GetConnectionString()))
                {

                    int rowStart = (listParams.pageSize * (listParams.pageIndex)) + 1;
                    int rowEnd = (listParams.pageIndex + 1) * listParams.pageSize;
                    using (SqlCommand cmd = new SqlCommand("sp_getAllPayorScheduledList_Web", con))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@LicenseeID", LicenseeId);
                        cmd.Parameters.AddWithValue("@RowStart", rowStart);
                        cmd.Parameters.AddWithValue("@RowEnd", rowEnd);
                        cmd.Parameters.AddWithValue("@filterBy", listParams.filterBy);
                        cmd.Parameters.AddWithValue("@sortType", listParams.sortType);
                        cmd.Parameters.AddWithValue("@sortBy", listParams.sortBy);
                        cmd.Parameters.Add("@recordCount", SqlDbType.Int);
                        cmd.Parameters["@recordCount"].Direction = ParameterDirection.Output;
                        con.Open();
                        SqlDataReader dr = cmd.ExecuteReader();
                        while (dr.Read())
                        {
                            PayorIncomingSchedule schedule = new PayorIncomingSchedule();

                            schedule.IncomingScheduleID = (Guid)dr["IncomingScheduleID"];
                            schedule.PayorID = (Guid)(dr["PayorID"]);
                            schedule.CarrierID = (Guid)(dr["CarrierID"]);
                            schedule.CoverageID = (Guid)(dr["CoverageID"]);
                            schedule.ProductType = Convert.ToString(dr["ProductType"]);
                            schedule.ScheduleTypeId = (int)(dr["ScheduleTypeId"]);
                            schedule.IncomingPaymentTypeID = (int)(dr["IncomingPaymentTypeID"]);
                            schedule.IncomingPaymentTypeName = Convert.ToString(dr["Name"]);

                            string frstYear = Convert.ToString(dr["FirstYearPercentage"]);
                            double fy = 0;
                            double.TryParse(frstYear, out fy);
                            schedule.FirstYearPercentage = fy;

                            string renewYear = Convert.ToString(dr["RenewalPercentage"]);
                            double ry = 0;
                            double.TryParse(renewYear, out ry);
                            schedule.RenewalPercentage = ry;
                            string FirstYearPercentage = Convert.ToString(dr["FirstYearPercentage"]); ;
                            double FirstYear = 0.00;
                            double.TryParse(FirstYearPercentage, out FirstYear);
                            schedule.FirstYearPercentage = FirstYear;
                            string RenwalYearPercentage = Convert.ToString(dr["RenewalPercentage"]); ;
                            double RenewalYear = 0.00;
                            double.TryParse(RenwalYearPercentage, out RenewalYear);
                            schedule.RenewalPercentage = RenewalYear;
                            string split = Convert.ToString(dr["SplitPercentage"]);
                            double splitPer = 0;
                            double.TryParse(split, out splitPer);
                            schedule.SplitPercentage = splitPer;

                            string advanc = Convert.ToString(dr["Advance"]);
                            int adv = 0;
                            int.TryParse(advanc, out adv);
                            schedule.Advance = adv;

                            schedule.PayorName = Convert.ToString(dr["PayorName"]);
                            schedule.CarrierName = Convert.ToString(dr["CarrierName"]);
                            schedule.CoverageName = Convert.ToString(dr["ProductName"]);
                            schedule.Title = Convert.ToString(dr["Title"]);
                            schedule.ScheduleTypeId = dr.IsDBNull("ScheduleTypeId") ? 1 : (int)(dr["ScheduleTypeId"]);
                            string mod = Convert.ToString(dr["Mode"]);                            int scheduleMode = 0;                            int.TryParse(mod, out scheduleMode);                            schedule.Mode = (Mode)scheduleMode;
                            schedule.StringMode = schedule.Mode == 0 ? "Standard" : "Custom";                            string strType = Convert.ToString(dr["CustomType"]);                            int intType = 0;                            int.TryParse(strType, out intType);
                            intType = (intType == 0) ? 1 : intType;                            schedule.CustomType = (CustomMode)intType;
                            if (schedule.Mode == Mode.Custom)
                            {
                                if (schedule.CustomType == CustomMode.Graded)
                                {

                                    schedule.GradedSchedule = GradedScheduleList(schedule.IncomingScheduleID, null);
                                }
                                else
                                {
                                    schedule.NonGradedSchedule = NonGradedScheduleList(schedule.IncomingScheduleID, null);
                                }
                                lstSchedules.Add(schedule);
                            }
                            else
                            {
                                lstSchedules.Add(schedule);
                            }
                        }
                        dr.Close();
                        recordCount = Convert.ToDouble(cmd.Parameters["@recordCount"].Value);
                    }
                }
            }
            catch (Exception ex)
            {
                ActionLogger.Logger.WriteLog("GetAllSchedules exception: " + ex.Message, true);
                throw ex;
            }
            return lstSchedules;
        }


        public static List<PayorIncomingSchedule> GetNamedScheduleList(Guid LicenseeId, ListParams listParams, out double recordCount)
        {
            List<PayorIncomingSchedule> lstSchedules = new List<PayorIncomingSchedule>();
            try
            {
                ActionLogger.Logger.WriteLog("GetAllSchedules LicenseeID" + LicenseeId, true);
                using (SqlConnection con = new SqlConnection(DBConnection.GetConnectionString()))
                {

                    int rowStart = (listParams.pageSize * (listParams.pageIndex)) + 1;
                    int rowEnd = (listParams.pageIndex + 1) * listParams.pageSize;
                    using (SqlCommand cmd = new SqlCommand("sp_getAllNamedScheduledList_Web", con))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@LicenseeID", LicenseeId);
                        cmd.Parameters.AddWithValue("@RowStart", rowStart);
                        cmd.Parameters.AddWithValue("@RowEnd", rowEnd);
                        cmd.Parameters.AddWithValue("@filterBy", listParams.filterBy);
                        cmd.Parameters.AddWithValue("@sortType", listParams.sortType);
                        cmd.Parameters.AddWithValue("@sortBy", listParams.sortBy);
                        cmd.Parameters.Add("@recordCount", SqlDbType.Int);
                        cmd.Parameters["@recordCount"].Direction = ParameterDirection.Output;
                        con.Open();
                        SqlDataReader dr = cmd.ExecuteReader();
                        while (dr.Read())
                        {
                            PayorIncomingSchedule schedule = new PayorIncomingSchedule();

                            schedule.IncomingScheduleID = (Guid)dr["IncomingScheduleID"];
                            schedule.ScheduleTypeId = (int)(dr["ScheduleTypeId"]);
                            schedule.StringFirstYearPercentage = Convert.ToString(dr["FirstYearPercentage"]);
                            schedule.StringRenewalPercentage = Convert.ToString(dr["RenewalPercentage"]);
                            string FirstYearPercentage = Convert.ToString(dr["FirstYearPercentage"]);
                            FirstYearPercentage = FirstYearPercentage.Replace("%", "");
                            double FirstYear = 0.00;
                            double.TryParse(FirstYearPercentage, out FirstYear);
                            schedule.FirstYearPercentage = FirstYear;
                            string RenwalYearPercentage = Convert.ToString(dr["RenewalPercentage"]);
                            RenwalYearPercentage = RenwalYearPercentage.Replace("%", "");
                            double RenewalYear = 0.00;
                            double.TryParse(RenwalYearPercentage, out RenewalYear);
                            schedule.RenewalPercentage = RenewalYear;
                            var splitper = Convert.ToString(dr["SplitPercentage"]);
                            if (!string.IsNullOrEmpty(splitper))
                            {
                                schedule.StringSplitPercentage = splitper;
                            }
                            string advanc = Convert.ToString(dr["Advance"]);
                            int adv = 0;
                            int.TryParse(advanc, out adv);
                            schedule.Advance = adv;

                            schedule.Title = Convert.ToString(dr["Title"]);
                            schedule.ScheduleType = Convert.ToString(dr["ScheduleType"]);
                            schedule.ScheduleTypeId = dr.IsDBNull("ScheduleTypeId") ? 1 : (int)(dr["ScheduleTypeId"]);
                            string mod = Convert.ToString(dr["Mode"]);                            int scheduleMode = 0;                            int.TryParse(mod, out scheduleMode);                            schedule.Mode = (Mode)scheduleMode;                            schedule.StringMode = schedule.Mode == 0 ? "Standard" : "Custom";                            string strType = Convert.ToString(dr["CustomType"]);                            int intType = 0;                            int.TryParse(strType, out intType);
                            intType = (intType == 0) ? 1 : intType;                            schedule.CustomType = (CustomMode)intType;
                            if (schedule.Mode == Mode.Custom)
                            {
                                if (schedule.CustomType == CustomMode.Graded)
                                {

                                    schedule.GradedSchedule = GradedScheduleList(schedule.IncomingScheduleID, null);
                                }
                                else
                                {
                                    schedule.NonGradedSchedule = NonGradedScheduleList(schedule.IncomingScheduleID, null);
                                }
                                lstSchedules.Add(schedule);
                            }
                            else
                            {
                                lstSchedules.Add(schedule);
                            }



                        }
                        dr.Close();
                        recordCount = Convert.ToDouble(cmd.Parameters["@recordCount"].Value);
                    }
                }
            }
            catch (Exception ex)
            {
                ActionLogger.Logger.WriteLog("GetAllSchedules exception: " + ex.Message, true);
                throw ex;
            }
            return lstSchedules;
        }


        //public static List<PayorIncomingSchedule> GetNamedScheduleSettingList(Guid LicenseeId, ListParams listParams, out double recordCount)
        //{
        //    List<PayorIncomingSchedule> NamedSchedulelst = new List<PayorIncomingSchedule>();
        //    try
        //    {
        //        ActionLogger.Logger.WriteLog("GetAllSchedules LicenseeID" + LicenseeId, true);
        //        using (SqlConnection con = new SqlConnection(DBConnection.GetConnectionString()))
        //        {

        //            int rowStart = (listParams.pageSize * (listParams.pageIndex)) + 1;
        //            int rowEnd = (listParams.pageIndex + 1) * listParams.pageSize;
        //            using (SqlCommand cmd = new SqlCommand("sp_getAllPayorScheduledList_Web", con))
        //            {
        //                cmd.CommandType = System.Data.CommandType.StoredProcedure;
        //                cmd.Parameters.AddWithValue("@LicenseeID", LicenseeId);
        //                cmd.Parameters.AddWithValue("@RowStart", rowStart);
        //                cmd.Parameters.AddWithValue("@RowEnd", rowEnd);
        //                cmd.Parameters.AddWithValue("@filterBy", listParams.filterBy);
        //                cmd.Parameters.AddWithValue("@sortType", listParams.sortType);
        //                cmd.Parameters.AddWithValue("@sortBy", listParams.sortBy);
        //                cmd.Parameters.Add("@recordCount", SqlDbType.Int);
        //                cmd.Parameters["@recordCount"].Direction = ParameterDirection.Output;
        //                con.Open();
        //                SqlDataReader dr = cmd.ExecuteReader();
        //                while (dr.Read())
        //                {
        //                    PayorIncomingSchedule schedule = new PayorIncomingSchedule();

        //                    schedule.IncomingScheduleID = (Guid)dr["IncomingScheduleID"];
        //                    schedule.PayorID = (Guid)(dr["PayorID"]);
        //                    schedule.CarrierID = (Guid)(dr["CarrierID"]);
        //                    schedule.CoverageID = (Guid)(dr["CoverageID"]);
        //                    schedule.ProductType = Convert.ToString(dr["ProductType"]);
        //                    schedule.ScheduleTypeId = (int)(dr["ScheduleTypeId"]);
        //                    schedule.IncomingPaymentTypeID = (int)(dr["IncomingPaymentTypeID"]);
        //                    schedule.IncomingPaymentTypeName = Convert.ToString(dr["Name"]);

        //                    string frstYear = Convert.ToString(dr["FirstYearPercentage"]);
        //                    double fy = 0;
        //                    double.TryParse(frstYear, out fy);
        //                    schedule.FirstYearPercentage = fy;

        //                    string renewYear = Convert.ToString(dr["RenewalPercentage"]);
        //                    double ry = 0;
        //                    double.TryParse(renewYear, out ry);
        //                    schedule.RenewalPercentage = ry;

        //                    string split = Convert.ToString(dr["SplitPercentage"]);
        //                    double splitPer = 0;
        //                    double.TryParse(split, out splitPer);
        //                    schedule.SplitPercentage = splitPer;

        //                    string advanc = Convert.ToString(dr["Advance"]);
        //                    int adv = 0;
        //                    int.TryParse(advanc, out adv);
        //                    schedule.Advance = adv;

        //                    schedule.PayorName = Convert.ToString(dr["PayorName"]);
        //                    schedule.CarrierName = Convert.ToString(dr["CarrierName"]);
        //                    schedule.CoverageName = Convert.ToString(dr["ProductName"]);
        //                    lstSchedules.Add(schedule);
        //                }
        //                dr.Close();
        //                recordCount = Convert.ToDouble(cmd.Parameters["@recordCount"].Value);
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ActionLogger.Logger.WriteLog("GetAllSchedules exception: " + ex.Message, true);
        //        throw ex;
        //    }
        //    return lstSchedules;
        //}
        public static PayorIncomingSchedule GetPayorScheduleDetails(Guid PayorID, Guid CarrierID, Guid CoverageID, Guid LicenseeID, string ProductType, int IncomingPaymentTypeID)
        {
            PayorIncomingSchedule schedule = new PayorIncomingSchedule();
            try
            {
                ActionLogger.Logger.WriteLog("GetPayorScheduleDetails schedule: " + PayorID + ", Carrier: " + CarrierID + ", coverage: " + CoverageID + ", productyType: " + ProductType, true);
                using (SqlConnection con = new SqlConnection(DBConnection.GetConnectionString()))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_getSelectedPayorSchedule", con))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@PayorID", PayorID);
                        cmd.Parameters.AddWithValue("@carrierID", CarrierID);
                        cmd.Parameters.AddWithValue("@CoverageID", CoverageID);
                        cmd.Parameters.AddWithValue("@LicenseeID", LicenseeID);
                        cmd.Parameters.AddWithValue("@ProductType", ProductType);
                        cmd.Parameters.AddWithValue("@IncomingPaymentTypeID", IncomingPaymentTypeID);
                        con.Open();
                        SqlDataReader dr = cmd.ExecuteReader();
                        while (dr.Read())
                        {
                            schedule.IncomingScheduleID = (Guid)dr["IncomingScheduleID"];
                            schedule.PayorID = (Guid)(dr["PayorID"]);
                            schedule.CarrierID = (Guid)(dr["CarrierID"]);
                            schedule.CoverageID = (Guid)(dr["CoverageID"]);
                            schedule.ProductType = Convert.ToString(dr["ProductType"]);
                            schedule.ScheduleTypeId = (int)(dr["ScheduleTypeId"]);
                            schedule.IncomingPaymentTypeID = (int)(dr["IncomingPaymentTypeID"]);

                            string frstYear = Convert.ToString(dr["FirstYearPercentage"]);
                            double fy = 0;
                            double.TryParse(frstYear, out fy);
                            schedule.FirstYearPercentage = fy;

                            string renewYear = Convert.ToString(dr["RenewalPercentage"]);
                            double ry = 0;
                            double.TryParse(renewYear, out ry);
                            schedule.RenewalPercentage = ry;

                            string split = Convert.ToString(dr["SplitPercentage"]);
                            double splitPer = 0;
                            double.TryParse(split, out splitPer);
                            schedule.SplitPercentage = splitPer;

                            string advanc = Convert.ToString(dr["Advance"]);
                            int adv = 0;
                            int.TryParse(advanc, out adv);
                            schedule.Advance = adv;


                            schedule.ScheduleTypeId = dr.IsDBNull("ScheduleTypeId") ? 1 : (int)(dr["ScheduleTypeId"]);
                            string mod = Convert.ToString(dr["Mode"]);                            int scheduleMode = 0;                            int.TryParse(mod, out scheduleMode);                            schedule.Mode = (Mode)scheduleMode;                            string strType = Convert.ToString(dr["CustomType"]);                            int intType = 0;                            int.TryParse(strType, out intType);                            schedule.CustomType = (CustomMode)intType;
                            if (schedule.Mode == Mode.Custom)
                            {
                                if (schedule.CustomType == CustomMode.Graded)
                                {

                                    schedule.GradedSchedule = GradedScheduleList(schedule.IncomingScheduleID, null);
                                }
                                else
                                {
                                    schedule.NonGradedSchedule = NonGradedScheduleList(schedule.IncomingScheduleID, null);
                                }

                            }

                        }
                        dr.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                ActionLogger.Logger.WriteLog("GetPayorScheduleDetails exception: " + ex.Message, true);
            }
            return schedule;
        }

        public static PayorIncomingSchedule GetPayorScheduleDetails(ScheduleQuery param)
        {
            PayorIncomingSchedule schedule = new PayorIncomingSchedule();
            try
            {
                ActionLogger.Logger.WriteLog("GetPayorScheduleDetails schedule: " + param.ToStringDump(), true);
                using (SqlConnection con = new SqlConnection(DBConnection.GetConnectionString()))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_getSelectedPayorSchedule", con))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@PayorID", param.PayorID);
                        cmd.Parameters.AddWithValue("@carrierID", param.CarrierID);
                        cmd.Parameters.AddWithValue("@CoverageID", param.CoverageID);
                        cmd.Parameters.AddWithValue("@LicenseeID", param.LicenseeID);
                        cmd.Parameters.AddWithValue("@ProductType", param.ProductType);
                        cmd.Parameters.AddWithValue("@IncomingPaymentTypeID", param.IncomingPaymentTypeID);
                        con.Open();
                        SqlDataReader dr = cmd.ExecuteReader();
                        while (dr.Read())
                        {
                            schedule.IncomingScheduleID = (Guid)dr["IncomingScheduleID"];
                            schedule.PayorID = (Guid)(dr["PayorID"]);
                            schedule.CarrierID = (Guid)(dr["CarrierID"]);
                            schedule.CoverageID = (Guid)(dr["CoverageID"]);
                            schedule.ProductType = Convert.ToString(dr["ProductType"]);
                            schedule.ScheduleTypeId = (int)(dr["ScheduleTypeId"]);
                            schedule.IncomingPaymentTypeID = (int)(dr["IncomingPaymentTypeID"]);

                            string frstYear = Convert.ToString(dr["FirstYearPercentage"]);
                            double fy = 0;
                            double.TryParse(frstYear, out fy);
                            schedule.FirstYearPercentage = fy;

                            string renewYear = Convert.ToString(dr["RenewalPercentage"]);
                            double ry = 0;
                            double.TryParse(renewYear, out ry);
                            schedule.RenewalPercentage = ry;

                            string split = Convert.ToString(dr["SplitPercentage"]);
                            double splitPer = 0;
                            double.TryParse(split, out splitPer);
                            schedule.SplitPercentage = splitPer;

                            string advanc = Convert.ToString(dr["Advance"]);
                            int adv = 0;
                            int.TryParse(advanc, out adv);
                            schedule.Advance = adv;
                            string mod = Convert.ToString(dr["Mode"]);                            int scheduleMode = 0;                            int.TryParse(mod, out scheduleMode);                            schedule.Mode = (Mode)scheduleMode;                            string strType = Convert.ToString(dr["CustomType"]);                            int intType = 0;                            int.TryParse(strType, out intType);                            schedule.CustomType = (CustomMode)intType;
                            if (schedule.Mode == Mode.Custom)
                            {
                                if (schedule.CustomType == CustomMode.Graded)
                                {

                                    schedule.GradedSchedule = GradedScheduleList(schedule.IncomingScheduleID, null);
                                }
                                else
                                {
                                    schedule.NonGradedSchedule = NonGradedScheduleList(schedule.IncomingScheduleID, null);
                                }

                            }
                        }

                        dr.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                ActionLogger.Logger.WriteLog("GetPayorScheduleDetails exception: " + ex.Message, true);
                throw ex;
            }
            return schedule;
        }


        public static PayorIncomingSchedule GetCommissionScheduleDetails(Guid incomingScheduleId)
        {
            PayorIncomingSchedule schedule = new PayorIncomingSchedule();
            try
            {
                ActionLogger.Logger.WriteLog("GetCommissionScheduleDetails incomingScheduleId: " + incomingScheduleId, true);
                using (SqlConnection con = new SqlConnection(DBConnection.GetConnectionString()))
                {
                    using (SqlCommand cmd = new SqlCommand("usp_CommissionScheduleDetails", con))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@incomingScheduleId", incomingScheduleId);
                        con.Open();
                        SqlDataReader dr = cmd.ExecuteReader();
                        while (dr.Read())
                        {
                            schedule.IncomingScheduleID = (Guid)dr["IncomingScheduleID"];
                            if (!dr.IsDBNull("PayorID"))
                            {
                                schedule.PayorID = (Guid)(dr["PayorID"]);
                            }
                            if (!dr.IsDBNull("CarrierID"))
                            {
                                schedule.CarrierID = (Guid)(dr["CarrierID"]);
                            }
                            if (!dr.IsDBNull("CoverageID"))
                            {
                                schedule.CoverageID = (Guid)(dr["CoverageID"]);
                            }
                            if (!dr.IsDBNull("ProductType"))
                            {
                                schedule.ProductType = Convert.ToString(dr["ProductType"]);
                            }
                            if (!dr.IsDBNull("IncomingPaymentTypeID"))
                            {
                                schedule.IncomingPaymentTypeID = (int)(dr["IncomingPaymentTypeID"]);
                            }
                            schedule.ScheduleTypeId = (int)(dr["ScheduleTypeId"]);
                            var Title = Convert.ToString(dr["Title"]);
                            if (!string.IsNullOrEmpty(Title))
                            {
                                schedule.Title = (string)(dr["Title"]);
                            }
                            //schedule.Mode = (Mode)(dr["Mode"]);
                            //schedule.CustomType = (dr.IsDBNull("CustomType") && schedule.Mode == 0) ? (CustomMode)1 : (CustomMode)dr["CustomType"];
                            string mod = Convert.ToString(dr["Mode"]);                            int scheduleMode = 0;                            int.TryParse(mod, out scheduleMode);                            schedule.Mode = (Mode)scheduleMode;                            string strType = Convert.ToString(dr["CustomType"]);                            int intType = 0;                            int.TryParse(strType, out intType);
                            intType = (intType == 0) ? 1 : intType;                            schedule.CustomType = (CustomMode)intType;

                            string frstYear = Convert.ToString(dr["FirstYearPercentage"]);
                            double fy = 0;
                            double.TryParse(frstYear, out fy);
                            schedule.FirstYearPercentage = fy;

                            string renewYear = Convert.ToString(dr["RenewalPercentage"]);
                            double ry = 0;
                            double.TryParse(renewYear, out ry);
                            schedule.RenewalPercentage = ry;

                            string split = Convert.ToString(dr["SplitPercentage"]);
                            double splitPer = 0;
                            double.TryParse(split, out splitPer);
                            schedule.SplitPercentage = splitPer;

                            string advanc = Convert.ToString(dr["Advance"]);
                            int adv = 0;
                            int.TryParse(advanc, out adv);
                            schedule.Advance = adv;
                        }
                        dr.Close();
                    }
                }
                if (schedule.Mode == Mode.Custom)
                {
                    if (schedule.CustomType == CustomMode.Graded)
                    {
                        schedule.GradedSchedule = GradedScheduleList(incomingScheduleId, null);
                    }
                    else
                    {
                        schedule.NonGradedSchedule = NonGradedScheduleList(incomingScheduleId, null);
                    }
                }
            }
            catch (Exception ex)
            {
                ActionLogger.Logger.WriteLog("GetPayorScheduleDetails exception: " + ex.Message, true);
                throw ex;
            }
            return schedule;
        }

        public static List<Graded> GradedScheduleList(Guid incomingScheduleId, Guid? policyId = null)
        {
            List<Graded> list = new List<Graded>();
            try
            {
                using (SqlConnection con = new SqlConnection(DBConnection.GetConnectionString()))
                {
                    using (SqlCommand cmd = new SqlCommand("usp_GetGradedScheduleList", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@incomingScheduleId", incomingScheduleId);
                        cmd.Parameters.AddWithValue("@PolicyID", policyId);
                        con.Open();
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            Graded data = new Graded();
                            data.From = reader.IsDBNull("GradedFrom") ? 0.0 : Convert.ToDouble(reader["GradedFrom"]);
                            data.To = reader.IsDBNull("GradedTo") ? 0.0 : Convert.ToDouble(reader["GradedTo"]);
                            data.Percent = reader.IsDBNull("GradedPercent") ? 0 : (double)(reader["GradedPercent"]);
                            list.Add(data);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return list;
        }

        public static List<NonGraded> NonGradedScheduleList(Guid incomingScheduleId, Guid? policyId = null)
        {
            List<NonGraded> list = new List<NonGraded>();
            try
            {

                using (SqlConnection con = new SqlConnection(DBConnection.GetConnectionString()))
                {
                    using (SqlCommand cmd = new SqlCommand("usp_GetGradedScheduleList", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@incomingScheduleId", incomingScheduleId);
                        cmd.Parameters.AddWithValue("@PolicyID", policyId);
                        con.Open();
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            NonGraded data = new NonGraded();
                            data.Year = reader.IsDBNull("YearNumber") ? 0 : (int)(reader["YearNumber"]);
                            data.Percent = reader.IsDBNull("NonGradedPercent") ? 0 : (double)(reader["NonGradedPercent"]);
                            list.Add(data);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return list;
        }
    }
}