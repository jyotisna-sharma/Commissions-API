using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyAgencyVault.BusinessLibrary.Base;
using System.Runtime.Serialization;
using DLinq = DataAccessLayer.LinqtoEntity;
using MyAgencyVault.BusinessLibrary.Masters;
using System.Data.EntityClient;
using System.Data.SqlClient;
using System.Data;

namespace MyAgencyVault.BusinessLibrary
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public enum Operation
    {
        [EnumMember]
        Add,
        [EnumMember]
        Upadte,
        [EnumMember]
        Delete,
        [EnumMember]
        None
    }

    [DataContract]
    public class OperationSet
    {
        [DataMember]
        public Operation MainOperation { get; set; }
        [DataMember]
        public Operation NickNameOperation { get; set; }
        [DataMember]
        public Guid PreviousCarrierId { get; set; }
        [DataMember]
        public Guid PreviousCoverageId { get; set; }
        [DataMember]
        public string previousCovarageNickName { get; set; }
    }

    [DataContract]
    public class Carrier
    {
        #region "public properties"

        [DataMember]
        public Guid CarrierId { get; set; }
        [DataMember]
        public Guid PayorId { get; set; }
        [DataMember]
        public string CarrierName { get; set; }
        [DataMember]
        public string NickName { get; set; }

        [DataMember]
        public string TrackIncomingPercentage { get; set; }
        [DataMember]
        public string TrackMissingMonth { get; set; }
        [DataMember]
        public bool IsTrackIncomingPercentage { get; set; }
        [DataMember]
        public bool IsDeleted { get; set; }
        [DataMember]
        public bool IsGlobal { get; set; }
        [DataMember]
        public bool IsTrackMissingMonth { get; set; }
        [DataMember]
        public Guid? CreatedBy { get; set; }

        [DataMember]
        public Guid? ModifiedBy { get; set; }
        [DataMember]
        public Guid? UserID { get; set; }
        [DataMember]
        public Guid? LicenseeId { get; set; }
        [DataMember]
        public List<Coverage> Coverages { get; set; }
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
        DateTime? _LastModifiedDate;
        [DataMember]
        public DateTime? LastModifiedDate
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
                    LastModifiedDateString = value.ToString();
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
                if (LastModifiedDate == null && !string.IsNullOrEmpty(_LastModifiedDateString))
                {
                    DateTime dt;
                    DateTime.TryParse(_LastModifiedDateString, out dt);
                    LastModifiedDate = dt;
                }
            }
        }
        #endregion

        #region IEditable<Carrier> Members

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ReturnStatus AddUpdateDelete(OperationSet operationType)
        {
            using (DLinq.CommissionDepartmentEntities DataModel = Entity.DataModel)
            {
                ReturnStatus status = null;
                status = ValidateCarrier(DataModel, operationType);
                if (!status.IsError)
                {
                    if (operationType.MainOperation == Operation.Add)
                    {
                        DLinq.Carrier carrier = new DLinq.Carrier
                        {
                            CarrierId = this.CarrierId,
                            CarrierName = this.CarrierName,
                            IsDeleted = this.IsDeleted,
                            IsGlobal = this.IsGlobal,
                            LicenseeId = (this.LicenseeId == Guid.Empty ? null : this.LicenseeId),
                            CreatedBy = this.UserID
                        };

                        DataModel.AddToCarriers(carrier);
                        DataModel.SaveChanges();
                    }

                    if (operationType.NickNameOperation == Operation.Add)
                    {
                        DLinq.CarrierNickName carrierNickName = DataModel.CarrierNickNames.FirstOrDefault(s => s.PayorId == this.PayorId && s.CarrierId == this.CarrierId);

                        if (carrierNickName != null)
                        {
                            carrierNickName.IsDeleted = false;
                            carrierNickName.NickName = this.NickName;
                            carrierNickName.IsTrackIncomingPercentage = this.IsTrackIncomingPercentage;
                            carrierNickName.IsTrackMissingMonth = this.IsTrackMissingMonth;
                            carrierNickName.CreatedBy = this.UserID;
                            carrierNickName.ModifiedBy = this.UserID;
                            carrierNickName.ModifiedOn = DateTime.Now;
                        }
                        else
                        {
                            carrierNickName = new DLinq.CarrierNickName
                            {
                                PayorId = this.PayorId,
                                CarrierId = this.CarrierId,
                                NickName = this.NickName,
                                IsTrackIncomingPercentage = this.IsTrackIncomingPercentage,
                                IsTrackMissingMonth = this.IsTrackMissingMonth,
                                CreatedBy = this.UserID,
                                ModifiedBy = this.UserID,
                                IsDeleted = false,
                                ModifiedOn = DateTime.Now
                            };
                            DataModel.AddToCarrierNickNames(carrierNickName);
                        }
                    }
                    else if (operationType.NickNameOperation == Operation.Upadte)
                    {
                        DLinq.CarrierNickName carrierNickName = DataModel.CarrierNickNames.FirstOrDefault(s => s.PayorId == this.PayorId && s.CarrierId == operationType.PreviousCarrierId);

                        if (carrierNickName != null)
                        {
                            DataModel.CarrierNickNames.DeleteObject(carrierNickName);

                            carrierNickName = new DLinq.CarrierNickName
                            {
                                PayorId = this.PayorId,
                                CarrierId = this.CarrierId,
                                NickName = this.NickName,
                                IsTrackIncomingPercentage = this.IsTrackIncomingPercentage,
                                IsTrackMissingMonth = this.IsTrackMissingMonth,
                                CreatedBy = this.UserID,
                                ModifiedBy = this.UserID,
                                IsDeleted = false,
                                ModifiedOn = DateTime.Now
                            };
                            DataModel.AddToCarrierNickNames(carrierNickName);
                        }
                    }
                    else if (operationType.NickNameOperation == Operation.Delete)
                    {
                        DLinq.CarrierNickName carrierNickName = (from c in DataModel.CarrierNickNames
                                                                 where (c.CarrierId == this.CarrierId && c.PayorId == this.PayorId && c.IsDeleted == false)
                                                                 select c).FirstOrDefault();

                        if (carrierNickName != null && carrierNickName.PayorId != null)
                            carrierNickName.IsDeleted = true;

                        //if last reference of carrier is deleted from the payor then
                        //we also delete the carrier row

                        //int carrierNickNameExist = (from c in DataModel.CarrierNickNames
                        //                            where (c.CarrierId == this.CarrierId && c.IsDeleted == false)
                        //                            select c).Count();
                        //if (carrierNickNameExist == 0)
                        //{
                        //    DLinq.Carrier carrier = DataModel.Carriers.FirstOrDefault(s => s.CarrierId == this.CarrierId);
                        //    carrier.IsDeleted = true;
                        //    status.IsCarrierOrCoverageRemoved = true;
                        //}
                    }
                    DataModel.SaveChanges();
                }
                return status;
            }
        }

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <returns></returns>
        //public ReturnStatus AddUpdateDelete(OperationSet operationType)
        //{
        //    using (DLinq.CommissionDepartmentEntities DataModel = Entity.DataModel)
        //    {
        //        ReturnStatus status = null;
        //        if (operationType.MainOperation == Operation.Add)
        //        {
        //            status = ValidateCarrier(DataModel, operationType);
        //            if (!status.IsError)
        //            {
        //                DLinq.Carrier carrier = new DLinq.Carrier
        //                    {
        //                        CarrierId = this.CarrierId,
        //                        CarrierName = this.CarrierName,
        //                        IsDeleted = this.IsDeleted,
        //                        IsGlobal = this.IsGlobal,
        //                        LicenseeId = (this.LicenseeId == Guid.Empty ? null : this.LicenseeId),
        //                        CreatedBy = this.UserID
        //                    };

        //                DataModel.AddToCarriers(carrier);

        //                DLinq.CarrierNickName carrierNickName = DataModel.CarrierNickNames.FirstOrDefault(s => s.PayorId == this.PayorId && s.CarrierId == this.CarrierId);

        //                if (carrierNickName != null)
        //                {
        //                    carrierNickName.IsDeleted = false;
        //                    carrierNickName.NickName = this.NickName;
        //                    carrierNickName.IsTrackIncomingPercentage = this.IsTrackIncomingPercentage;
        //                    carrierNickName.IsTrackMissingMonth = this.IsTrackMissingMonth;
        //                    carrierNickName.CreatedBy = this.UserID;
        //                    carrierNickName.ModifiedBy = this.UserID;
        //                    carrierNickName.ModifiedOn = DateTime.Now;
        //                }
        //                else
        //                {
        //                    carrierNickName = new DLinq.CarrierNickName
        //                    {
        //                        PayorId = this.PayorId,
        //                        CarrierId = this.CarrierId,
        //                        NickName = this.NickName,
        //                        IsTrackIncomingPercentage = this.IsTrackIncomingPercentage,
        //                        IsTrackMissingMonth = this.IsTrackMissingMonth,
        //                        CreatedBy = this.UserID,
        //                        ModifiedBy = this.UserID,
        //                        IsDeleted = false,
        //                        ModifiedOn = DateTime.Now
        //                    };
        //                    DataModel.AddToCarrierNickNames(carrierNickName);
        //                }

        //                DataModel.SaveChanges();
        //                int carrierCount = DataModel.CarrierNickNames.Where(s => s.PayorId == this.PayorId && s.IsDeleted == false).ToList().Count;
        //                if (carrierCount > 1)
        //                {
        //                    DLinq.Payor payor = DataModel.Payors.FirstOrDefault(s => s.PayorId == this.PayorId && s.IsDeleted == false);
        //                    payor.PayorTypeId = 1;
        //                }
        //                DataModel.SaveChanges();
        //            }
        //        }
        //        else if (operationType.MainOperation == Operation.Upadte)
        //        {
        //            status = ValidateCarrier(DataModel, operationType);
        //            if (!status.IsError)
        //            {
        //                DLinq.Carrier carrier = DataModel.Carriers.FirstOrDefault(s => s.CarrierId == this.CarrierId);
        //                carrier.CarrierName = this.CarrierName;

        //                DLinq.CarrierNickName carrierNickname = DataModel.CarrierNickNames.FirstOrDefault(s => s.PayorId == this.PayorId && s.CarrierId == this.CarrierId);
        //                if (carrierNickname != null)
        //                {
        //                    carrierNickname.IsDeleted = false;
        //                    carrierNickname.NickName = this.NickName;
        //                    carrierNickname.IsTrackIncomingPercentage = this.IsTrackIncomingPercentage;
        //                    carrierNickname.IsTrackMissingMonth = this.IsTrackMissingMonth;
        //                    carrierNickname.CreatedBy = this.UserID;
        //                    carrierNickname.ModifiedBy = this.UserID;
        //                    carrierNickname.ModifiedOn = DateTime.Now;
        //                }
        //                else
        //                {
        //                    carrierNickname = new DLinq.CarrierNickName
        //                    {
        //                        PayorId = this.PayorId,
        //                        CarrierId = this.CarrierId,
        //                        NickName = this.NickName,
        //                        IsTrackIncomingPercentage = this.IsTrackIncomingPercentage,
        //                        IsTrackMissingMonth = this.IsTrackMissingMonth,
        //                        CreatedBy = this.UserID,
        //                        ModifiedBy = this.UserID,
        //                        IsDeleted = false,
        //                        ModifiedOn = DateTime.Now
        //                    };
        //                    DataModel.AddToCarrierNickNames(carrierNickname);
        //                }
        //                DataModel.SaveChanges();
        //            }
        //        }
        //        else if (operationType.MainOperation == Operation.Delete)
        //        {
        //            status = ValidateCarrier(DataModel, operationType);
        //            if (!status.IsError)
        //            {
        //                DLinq.CarrierNickName carrierNickName = (from c in DataModel.CarrierNickNames
        //                                                         where (c.CarrierId == this.CarrierId && c.PayorId == this.PayorId && c.IsDeleted == false)
        //                                                         select c).FirstOrDefault();

        //                if (carrierNickName != null && carrierNickName.PayorId != null)
        //                {
        //                    carrierNickName.IsDeleted = true;
        //                    DataModel.SaveChanges();
        //                }

        //                int carrierNickNameExist = (from c in DataModel.CarrierNickNames
        //                                            where (c.CarrierId == this.CarrierId && c.IsDeleted == false)
        //                                            select c).Count();
        //                if (carrierNickNameExist == 0)
        //                {
        //                    DLinq.Carrier carrier = DataModel.Carriers.FirstOrDefault(s => s.CarrierId == this.CarrierId);
        //                    carrier.IsDeleted = true;
        //                    status.IsCarrierOrCoverageRemoved = true;
        //                    DataModel.SaveChanges();
        //                }
        //            }
        //        }
        //        else
        //        {
        //            status = new ReturnStatus { ErrorMessage = "Undefine payor operation.", IsError = true };
        //        }

        //        return status;
        //    }
        //}

        #endregion
        /// <summary>
        /// 
        /// </summary>
        /// <param name="?"></param>
        ///GetCarriers(all/all of a licensee/all viewable to a user/all of a given search criteria/)
        public static List<DisplayedCarrier> GetCarriers(Guid licenseeId)
        {
            using (DLinq.CommissionDepartmentEntities DataModel = Entity.DataModel)
            {
                List<DisplayedCarrier> carriers;
                if (licenseeId != Guid.Empty)
                {
                    carriers = (from c in DataModel.Carriers
                                where (c.IsDeleted == false) && ((c.LicenseeId == licenseeId) || (c.IsGlobal == true))
                                orderby c.CarrierName
                                select new DisplayedCarrier
                                {
                                    CarrierId = c.CarrierId,
                                    CarrierName = c.CarrierName,
                                    IsGlobal = c.IsGlobal
                                }).ToList();

                }
                else
                {
                    carriers = (from c in DataModel.Carriers
                                where (c.IsDeleted == false) && (c.IsGlobal == true)
                                orderby c.CarrierName
                                select new DisplayedCarrier
                                {
                                    CarrierId = c.CarrierId,
                                    CarrierName = c.CarrierName,
                                    IsGlobal = c.IsGlobal
                                }).ToList();
                }
                return carriers;
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="?"></param>
        ///GetCarriers(all/all of a licensee/all viewable to a user/all of a given search criteria/)
        public static List<Carrier> GetCarriers(Guid LicenseeId, bool isCoveragesRequired)
        {
            using (DLinq.CommissionDepartmentEntities DataModel = Entity.DataModel)
            {
                List<Carrier> carriers;
                if (LicenseeId != Guid.Empty)
                {
                    carriers = (from c in DataModel.Carriers
                                where (c.IsDeleted == false) && ((c.LicenseeId == LicenseeId) || (c.IsGlobal == true))
                                orderby c.CarrierName
                                select new Carrier
                                {
                                    CarrierId = c.CarrierId,
                                    CarrierName = c.CarrierName,
                                    IsGlobal = c.IsGlobal,
                                    LicenseeId = c.LicenseeId ?? Guid.Empty,
                                    UserID = c.CreatedBy.Value
                                }).ToList();

                }
                else
                {
                    carriers = (from c in DataModel.Carriers
                                where (c.IsDeleted == false) && (c.IsGlobal == true)
                                orderby c.CarrierName
                                select new Carrier
                                {
                                    CarrierId = c.CarrierId,
                                    CarrierName = c.CarrierName,
                                    IsGlobal = c.IsGlobal,
                                    LicenseeId = c.LicenseeId ?? Guid.Empty,
                                    UserID = c.CreatedBy.Value
                                }).ToList();
                }


                foreach (Carrier carrier in carriers)
                {
                    if (isCoveragesRequired)
                        carrier.Coverages = Coverage.GetCarrierCoverages(carrier.CarrierId);
                    else
                        carrier.Coverages = null;
                }

                return carriers;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="?"></param>
        ///GetCarriers(all/all of a licensee/all viewable to a user/all of a given search criteria/)
        public static List<DisplayedCarrier> GetDispalyedCarriers(Guid LicenseeId, bool isCoveragesRequired)
        {
            using (DLinq.CommissionDepartmentEntities DataModel = Entity.DataModel)
            {
                List<DisplayedCarrier> carriers;
                if (LicenseeId != Guid.Empty)
                {
                    carriers = (from c in DataModel.Carriers
                                where (c.IsDeleted == false) && ((c.LicenseeId == LicenseeId) || (c.IsGlobal == true))
                                orderby c.CarrierName
                                select new DisplayedCarrier
                                {
                                    CarrierId = c.CarrierId,
                                    CarrierName = c.CarrierName,
                                    IsGlobal = c.IsGlobal
                                }).ToList();

                }
                else
                {
                    carriers = (from c in DataModel.Carriers
                                where (c.IsDeleted == false) && (c.IsGlobal == true)
                                orderby c.CarrierName
                                select new DisplayedCarrier
                                {
                                    CarrierId = c.CarrierId,
                                    CarrierName = c.CarrierName,
                                    IsGlobal = c.IsGlobal
                                }).ToList();
                }


                foreach (DisplayedCarrier carrier in carriers)
                {
                    if (isCoveragesRequired)
                        carrier.Coverages = Coverage.GetCarrierCoverages(carrier.CarrierId);
                    else
                        carrier.Coverages = null;
                }

                return carriers;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="PayorId"></param>
        /// <returns></returns>
        public static List<Carrier> GetPayorCarriers(Guid PayorId)
        {
            using (DLinq.CommissionDepartmentEntities DataModel = Entity.DataModel)
            {
                List<Carrier> CarrierLst = (from c in DataModel.CarrierNickNames
                                            where (c.IsDeleted == false) && (c.PayorId == PayorId)
                                            orderby c.Carrier.CarrierName
                                            select new Carrier
                                            {
                                                CarrierId = c.CarrierId,
                                                PayorId = c.PayorId,
                                                CarrierName = c.Carrier.CarrierName,
                                                NickName = c.NickName,
                                                IsTrackMissingMonth = c.IsTrackMissingMonth,
                                                IsTrackIncomingPercentage = c.IsTrackIncomingPercentage,
                                                IsDeleted = c.IsDeleted.Value,
                                                IsGlobal = c.Carrier.IsGlobal,
                                                LicenseeId = c.Carrier.LicenseeId ?? Guid.Empty,
                                                UserID = c.CreatedBy,
                                                // Coverages = null
                                            }).ToList();

                return CarrierLst;
            }
        }

        /// <summary>
        /// Authior Jyotisna
        /// Created: Feb 08, 2019
        /// Purpose: Get data without passign payor list
        /// </summary>
        /// <param name="PayorList"></param>
        /// <returns></returns>
        public static List<Payor> PayorCarrierGlobal(Guid licenseeID, PayorFillInfo payorfillInfo)
        {
            ActionLogger.Logger.WriteLog("PayorCarrierGlobal:Processing Begins with LicenseeId "+ licenseeID, true);
            //List<Payor> payors = Payor.GetPayors(licenseeID, payorfillInfo);
            //List<Guid> PayorList = payors.Select(x => x.PayorID).ToList<Guid>();
            //List<Guid> PayorIdList = new List<Guid>();
            //if (PayorList != null && PayorList.Count > 0)
            //{
            //    using (DLinq.CommissionDepartmentEntities DataModel = Entity.DataModel)
            //    {

            //        var globalPayorList = (from carriernickname in DataModel.CarrierNickNames
            //                               where carriernickname.IsDeleted == false && PayorList.Contains(carriernickname.PayorId)
            //                               group carriernickname by new { carriernickname.PayorId } into g
            //                               where g.Count(p => p.CarrierId != null) > 1
            //                               select new { g.Key.PayorId }).ToList();
            //        foreach (var listitem in globalPayorList)
            //        {
            //            PayorIdList.Add(listitem.PayorId);
            //        }

            //    }
            //}
            List<Payor> PayorIdList = new List<Payor>();
            try
            {
                using (SqlConnection con = new SqlConnection(DBConnection.GetConnectionString()))
                {
                    using (SqlCommand cmd = new SqlCommand("usp_PayorCarrierGlobal", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@licenseeId", licenseeID);
                        cmd.Parameters.AddWithValue("@IsCarriersRequired", payorfillInfo.IsCarriersRequired);
                        cmd.Parameters.AddWithValue("@IsCoveragesRequired", payorfillInfo.IsCoveragesRequired);
                        cmd.Parameters.AddWithValue("@IsWebsiteLoginsRequired", payorfillInfo.IsWebsiteLoginsRequired);
                        cmd.Parameters.AddWithValue("@IsContactsRequired", payorfillInfo.IsContactsRequired);
                        cmd.Parameters.AddWithValue("@PayorStatus", payorfillInfo.PayorStatus);
                        con.Open();

                        using (SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd))
                        {
                            DataSet ds = new DataSet();
                            dataAdapter.Fill(ds);
                            DataTable pa = ds.Tables[0];
                            // DataTable ca = ds.Tables[1];
                            for (int i = 0; i < pa.Rows.Count; i++)
                            {
                                Payor payordata = new Payor();
                                payordata.PayorName = pa.Rows[i]["PayorName"] != null ?  Convert.ToString( pa.Rows[i]["PayorName"]):"";
                                Guid PayorId = (Guid)(pa.Rows[i]["PayorId"]);
                                PayorIdList.Add(payordata);
                            }
                        }
                    }
                }
                ActionLogger.Logger.WriteLog("PayorCarrierGlobal:Processing Completed with PayorIdList " +PayorIdList.Count, true);
            }
            catch (Exception ex)
            {
                ActionLogger.Logger.WriteLog("PayorCarrierGlobal:Exception occur while fetching submittedThrough List" + ex.Message, true);
                throw ex;
            }

            return PayorIdList;
        }
        /// <summary>
        /// Create By: Ankit Khandelwal
        /// Created On:12-03-2018
        /// Purpose:Getting list of carriers based on PayorId
        /// </summary>
        /// <param name="payorId"></param>
        /// <returns></returns>
        public static List<Carrier> GetCarrierList(Guid? payorId)
        {
            List<Carrier> lstCarriers = new List<Carrier>();
            try
            {
                DLinq.CommissionDepartmentEntities ctx = new DLinq.CommissionDepartmentEntities(); //create your entity object here
                EntityConnection ec = (EntityConnection)ctx.Connection;
                SqlConnection sc = (SqlConnection)ec.StoreConnection;
                string adoconStr = sc.ConnectionString;

                using (SqlConnection con = new SqlConnection(adoconStr))
                {
                    using (SqlCommand cmd = new SqlCommand("usp_getCarrierList", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@PayorId", payorId);
                        con.Open();
                        SqlDataReader reader = cmd.ExecuteReader();
                        // Call Read before accessing data.
                        while (reader.Read())
                        {
                            Carrier carrierdata = new Carrier();
                            carrierdata.CarrierId = (Guid)(reader["CarrierId"]);
                            carrierdata.PayorId = (Guid)(reader["PayorId"]);
                            carrierdata.CarrierName = Convert.ToString(reader["CarrierName"]);
                            carrierdata.NickName = Convert.ToString(reader["NickName"]);
                            carrierdata.IsTrackIncomingPercentage = reader.IsDBNull("IsTrackMissingMonth")? true :Convert.ToBoolean(reader["IsTrackMissingMonth"]);
                            carrierdata.IsTrackMissingMonth = reader.IsDBNull("IsTrackMissingMonth") ? true:Convert.ToBoolean(reader["IsTrackIncomingPercentage"]);
                            lstCarriers.Add(carrierdata);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ActionLogger.Logger.WriteLog("Exception saving policy : " + ex.Message, true);
                throw ex;
            }
            return lstCarriers;
        }

        /// <summary>
        /// Create By: Ankit Khandelwal
        /// Created On:12-03-2018
        /// Purpose:Getting list of carriers based on PayorId
        /// </summary>
        /// <param name="payorId"></param>
        /// <param name="listparams"></param>
        /// <returns></returns>
        public static List<Carrier> GetCarrierListing(Guid? payorId, ListParams listParams, out string totalCount)
        {
            List<Carrier> lstCarriers = new List<Carrier>();
            try
            {
                DLinq.CommissionDepartmentEntities ctx = new DLinq.CommissionDepartmentEntities(); //create your entity object here
                EntityConnection ec = (EntityConnection)ctx.Connection;
                SqlConnection sc = (SqlConnection)ec.StoreConnection;
                string adoconStr = sc.ConnectionString;
                int rowStart = (listParams.pageSize == 0 && listParams.pageIndex == 0) ? 0 : (listParams.pageSize * (listParams.pageIndex)) + 1;
                int rowEnd = (listParams.pageSize == 0 && listParams.pageIndex == 0) ? 0 : (listParams.pageIndex + 1) * listParams.pageSize;
                using (SqlConnection con = new SqlConnection(adoconStr))
                {

                    using (SqlCommand cmd = new SqlCommand("usp_getCarriersListing", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@PayorId", payorId);
                        cmd.Parameters.AddWithValue("@rowStart", rowStart);
                        cmd.Parameters.AddWithValue("@rowEnd", rowEnd);
                        cmd.Parameters.AddWithValue("@filterBy", listParams.filterBy);
                        cmd.Parameters.AddWithValue("@sortType", listParams.sortType);
                        cmd.Parameters.AddWithValue("@sortBy", listParams.sortBy);
                        cmd.Parameters.Add("@totalCount", SqlDbType.Int);
                        cmd.Parameters["@totalCount"].Direction = ParameterDirection.Output;
                        con.Open();
                        SqlDataReader reader = cmd.ExecuteReader();
                        // Call Read before accessing data.
                        while (reader.Read())
                        {
                            Carrier carrierdata = new Carrier();
                            carrierdata.CarrierId = (Guid)(reader["CarrierId"]);
                            carrierdata.CarrierName = !reader.IsDBNull("Name") ? Convert.ToString(reader["Name"]) : null;
                            carrierdata.NickName = !reader.IsDBNull("NickName") ? Convert.ToString(reader["NickName"]) : null;
                            carrierdata.TrackIncomingPercentage = Convert.ToString(reader["TrackIncomingPercentage"]);
                            carrierdata.TrackMissingMonth = Convert.ToString(reader["TrackMissingMonth"]);
                            carrierdata.IsTrackIncomingPercentage = (bool)reader["IsTrackIncomingPercentage"];
                            carrierdata.IsTrackMissingMonth = (bool)reader["IsTrackMissingMonth"];
                            carrierdata.PayorId = (Guid)reader["PayorId"];
                            lstCarriers.Add(carrierdata);
                        }
                        reader.Close();
                        totalCount = Convert.ToString(cmd.Parameters["@totalCount"].Value);
                    }

                }
            }
            catch (Exception ex)
            {
                ActionLogger.Logger.WriteLog("Exception saving policy : " + ex.Message, true);
                throw ex;
            }
            return lstCarriers;
        }

        /// <summary>
        /// CreaterBy :Ankit khandelwal
        /// CreatedOn:07-08-2019
        /// Purpose:Update the Carrier details
        /// </summary>
        /// <param name="PayorList"></param>
        /// <returns></returns>

        public static int AddUpdateCarrierDetails(Carrier CarrierDetails)
        {
            ActionLogger.Logger.WriteLog("AddUpdateCarrierDetails: Processing starts with CarrierDetails:" + CarrierDetails.CarrierId, true);
            int recordStatus = 0;
            try
            {
                DLinq.CommissionDepartmentEntities ctx = new DLinq.CommissionDepartmentEntities(); //create your entity object here
                EntityConnection ec = (EntityConnection)ctx.Connection;
                SqlConnection sc = (SqlConnection)ec.StoreConnection;
                string adoconStr = sc.ConnectionString;

                using (SqlConnection con = new SqlConnection(adoconStr))
                {
                    using (SqlCommand cmd = new SqlCommand("usp_AddUpdateCarrierDetails", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@CarrierId", CarrierDetails.CarrierId);
                        cmd.Parameters.AddWithValue("@CarrierName", CarrierDetails.CarrierName);
                        cmd.Parameters.AddWithValue("@NickName", CarrierDetails.NickName);
                        cmd.Parameters.AddWithValue("@IsTrackIncomingPercentage", CarrierDetails.IsTrackIncomingPercentage);
                        cmd.Parameters.AddWithValue("@IsTrackMissingMonth", CarrierDetails.IsTrackMissingMonth);
                        cmd.Parameters.AddWithValue("@CreatedBy", CarrierDetails.CreatedBy);
                        cmd.Parameters.AddWithValue("@PayorId", CarrierDetails.PayorId);
                        cmd.Parameters.AddWithValue("@ModifiedBy", CarrierDetails.ModifiedBy);
                        cmd.Parameters.Add("@status", SqlDbType.Int);
                        cmd.Parameters["@status"].Direction = ParameterDirection.Output;
                        con.Open();
                        cmd.ExecuteScalar();
                        // Call Read before accessing data.
                        recordStatus = (int)cmd.Parameters["@status"].Value;
                    }
                }
            }
            catch (Exception ex)
            {
                ActionLogger.Logger.WriteLog("AddUpdateCarrierDetails: Exception occurs CarrierDetails:" + CarrierDetails.CarrierId + "Exception:" + ex.Message, true);
                throw ex;
            }
            return recordStatus;
        }

        /// <summary>
        /// Created by:Ankit khandelwal
        /// CreatedOn:07-08-2019
        /// Purpose:Delete a carrier based on carrierId and payorId
        /// </summary>
        /// <param name="carrierId"></param>
        /// <param name="payorId"></param>
        /// <param name="deleteFlag"></param>
        /// <returns></returns>
        public static bool DeleteCarrier(Guid carrierId,Guid payorId,bool deleteFlag=false)
        {
            bool isDeleteAllowed = false;
            Carrier details = new Carrier();
            try
            {
                DLinq.CommissionDepartmentEntities ctx = new DLinq.CommissionDepartmentEntities(); //create your entity object here
                EntityConnection ec = (EntityConnection)ctx.Connection;
                SqlConnection sc = (SqlConnection)ec.StoreConnection;
                string adoconStr = sc.ConnectionString;

                using (SqlConnection con = new SqlConnection(adoconStr))
                {
                    using (SqlCommand cmd = new SqlCommand("usp_DeleteCarrier", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@CarrierId", carrierId);
                        cmd.Parameters.AddWithValue("@PayorId", payorId);
                        cmd.Parameters.AddWithValue("@DeleteFlag", deleteFlag);
                        cmd.Parameters.Add("@IsDeleteAllowed", SqlDbType.Bit);
                        cmd.Parameters["@IsDeleteAllowed"].Direction = ParameterDirection.Output;
                        con.Open();
                        cmd.ExecuteScalar();
                        isDeleteAllowed = (bool)cmd.Parameters["@IsDeleteAllowed"].Value;
                    }
                }
            }
            catch(Exception ex)
            {
                ActionLogger.Logger.WriteLog("DeleteCarrier: Exception occurs carrierId:" + carrierId + "Exception:" + ex.Message, true);
                throw ex;
            }
            return isDeleteAllowed;
        }
        public static List<Guid> PayorCarrierGlobal(List<Guid> PayorList)
        {
            using (DLinq.CommissionDepartmentEntities DataModel = Entity.DataModel)
            {
                List<Guid> PayorIdList = new List<Guid>();
                var globalPayorList = (from carriernickname in DataModel.CarrierNickNames
                                       where carriernickname.IsDeleted == false && PayorList.Contains(carriernickname.PayorId)
                                       group carriernickname by new { carriernickname.PayorId } into g
                                       where g.Count(p => p.CarrierId != null) > 1
                                       select new { g.Key.PayorId }).ToList();
                foreach (var listitem in globalPayorList)
                {
                    PayorIdList.Add(listitem.PayorId);
                }
                return PayorIdList;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="PayorId"></param>
        /// <returns></returns>
        public static List<Carrier> GetPayorCarriers(Guid PayorId, bool isCoveragesRequired)
        {
            using (DLinq.CommissionDepartmentEntities DataModel = Entity.DataModel)
            {
                List<Carrier> carriers = (from c in DataModel.CarrierNickNames
                                          where (c.IsDeleted == false) && (c.PayorId == PayorId)
                                          orderby c.Carrier.CarrierName
                                          select new Carrier
                                          {
                                              CarrierId = c.CarrierId,
                                              PayorId = c.PayorId,
                                              CarrierName = c.Carrier.CarrierName,
                                              NickName = c.NickName,
                                              IsTrackMissingMonth = c.IsTrackMissingMonth,
                                              IsTrackIncomingPercentage = c.IsTrackIncomingPercentage,
                                              IsDeleted = c.IsDeleted.Value,
                                              IsGlobal = c.Carrier.IsGlobal,
                                              LicenseeId = c.Carrier.LicenseeId ?? Guid.Empty,
                                              UserID = c.CreatedBy
                                          }).ToList();



                foreach (Carrier car in carriers)
                {
                    if (isCoveragesRequired)
                        car.Coverages = Coverage.GetCarrierCoverages(PayorId, car.CarrierId);
                    else
                        car.Coverages = null;
                }

                return carriers;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="PayorId"></param>
        /// <param name="CarrierId"></param>
        /// <returns></returns>
        public static Carrier GetPayorCarrier(Guid PayorId, Guid CarrierId)
        {
            if (PayorId == Guid.Empty || CarrierId == Guid.Empty)
                return null;

            using (DLinq.CommissionDepartmentEntities DataModel = Entity.DataModel)
            {
                return (from c in DataModel.CarrierNickNames
                        where (c.CarrierId == CarrierId) && (c.PayorId == PayorId) && (c.IsDeleted == false)
                        select new Carrier
                        {
                            CarrierId = c.CarrierId,
                            PayorId = c.PayorId,
                            CarrierName = c.Carrier.CarrierName,
                            NickName = c.NickName,
                            IsDeleted = c.IsDeleted.Value,
                            IsGlobal = c.Carrier.IsGlobal,
                            LicenseeId = c.Carrier.LicenseeId,
                            IsTrackMissingMonth = c.IsTrackMissingMonth,
                            IsTrackIncomingPercentage = c.IsTrackIncomingPercentage,
                            UserID = c.CreatedBy
                        }).ToList().FirstOrDefault();
            }
        }


        public static bool IsValidCarrier(string carrierNickName, Guid payorId)
        {
            using (DLinq.CommissionDepartmentEntities DataModel = Entity.DataModel)
            {
                DLinq.CarrierNickName carrier = DataModel.CarrierNickNames.FirstOrDefault(s => s.NickName.ToUpper() == carrierNickName.ToUpper() && s.PayorId == payorId && s.IsDeleted == false);

                if (carrier != null)
                    return true;
                else
                    return false;
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="PayorId"></param>
        /// <param name="CarrierId"></param>
        /// <returns></returns>
        public static string GetCarrierNickName(Guid PayorId, Guid CarrierId)
        {
            if (PayorId == Guid.Empty || CarrierId == Guid.Empty)
                return string.Empty;

            using (DLinq.CommissionDepartmentEntities DataModel = Entity.DataModel)
            {
                string nickName = string.Empty;
                DLinq.CarrierNickName payorcarrier = DataModel.CarrierNickNames.FirstOrDefault(s => s.PayorId == PayorId && s.CarrierId == CarrierId && s.IsDeleted == false);
                if (payorcarrier != null)
                {
                    nickName = payorcarrier.NickName;
                }
                return nickName;
            }

        }

        public static string GetSingleCarrier(Guid PayorId)
        {
            if (PayorId == Guid.Empty)
                return string.Empty;

            using (DLinq.CommissionDepartmentEntities DataModel = Entity.DataModel)
            {
                string nickName = string.Empty;
                DLinq.CarrierNickName payorcarrier = DataModel.CarrierNickNames.FirstOrDefault(s => s.PayorId == PayorId && s.IsDeleted == false);
                if (payorcarrier != null)
                {


                }
                return nickName;
            }

        }

        /// <summary>
        /// This function is used to validate the carrier for system.
        /// </summary>
        /// <returns></returns>
        private ReturnStatus ValidateCarrier(DLinq.CommissionDepartmentEntities DataModel, OperationSet operation)
        {
            ReturnStatus retStatus = new ReturnStatus();

            if (operation.MainOperation == Operation.Add)
            {
                List<DLinq.CarrierNickName> payorCarriers = DataModel.CarrierNickNames.Where(s => s.PayorId == this.PayorId && s.IsDeleted == false).ToList();

                if (payorCarriers == null && payorCarriers.Count == 0)
                    return retStatus;
                else
                {
                    DLinq.CarrierNickName payorCarrier = payorCarriers.FirstOrDefault(s => s.Carrier.CarrierName == this.CarrierName && s.IsDeleted == false);
                    if (payorCarrier != null)
                    {
                        retStatus.IsError = true;
                        retStatus.ErrorMessage = "This carrier is already present. Please select other.";
                    }
                }
            }

            if (operation.NickNameOperation == Operation.Add)
            {
                List<DLinq.CarrierNickName> payorCarriers = DataModel.CarrierNickNames.Where(s => s.PayorId == this.PayorId && s.IsDeleted == false).ToList();
                //List<DLinq.CarrierNickName> payorCarriers = DataModel.CarrierNickNames.Where(s=>s.IsDeleted == false).ToList();

                if (payorCarriers == null && payorCarriers.Count == 0)
                    return retStatus;
                else
                {
                    DLinq.CarrierNickName payorCarrier = payorCarriers.FirstOrDefault(s => s.NickName == this.NickName && s.IsDeleted == false);
                    if (payorCarrier != null)
                    {
                        retStatus.IsError = true;
                        retStatus.ErrorMessage = "This carrier nickname is already present. Please select other.";
                    }
                }
            }
            else if (operation.NickNameOperation == Operation.Upadte)
            {
                int count = DataModel.CarrierNickNames.Where(s => s.NickName == this.NickName && s.CarrierId != operation.PreviousCarrierId && s.PayorId == this.PayorId && s.IsDeleted == false).ToList().Count;
                //int count = DataModel.CarrierNickNames.Where(s => s.NickName == this.NickName && s.IsDeleted == false).ToList().Count;
                if (count != 0)
                {
                    retStatus.IsError = true;
                    retStatus.ErrorMessage = "This carrier nickname is already present. Please select other.";
                }
            }
            else if (operation.NickNameOperation == Operation.Delete)
            {
                DLinq.Policy policy = (from po in DataModel.Policies
                                       where (po.CarrierId == this.CarrierId
                                       && po.PayorId == this.PayorId
                                       && po.IsDeleted == false)
                                       select po).FirstOrDefault();
                if (policy != null)
                {
                    retStatus.IsError = true;
                    retStatus.ErrorMessage = "Some policy refer this carrier. You can not delete carrier without deleting all the policies that refer this carrier.";
                }
            }

            return retStatus;
        }

    }

    [DataContract]
    public class DisplayedCarrier
    {
        [DataMember]
        public Guid CarrierId { get; set; }
        [DataMember]
        public string CarrierName { get; set; }
        [DataMember]
        public bool IsGlobal { get; set; }
        [DataMember]
        public List<Coverage> Coverages { get; set; }
    }

    [DataContract]
    public class ReturnStatus
    {
        [DataMember]
        public bool IsError { get; set; }
        [DataMember]
        public string ErrorMessage { get; set; }
        [DataMember]
        public bool IsCarrierOrCoverageRemoved { get; set; }
    }
}
