using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyAgencyVault.BusinessLibrary.Base;
using System.Runtime.Serialization;
using DLinq = DataAccessLayer.LinqtoEntity;
using MyAgencyVault.BusinessLibrary.Masters;
using System.Data.SqlClient;
using System.Data;

namespace MyAgencyVault.BusinessLibrary
{
    [DataContract]
    public class Coverage
    {

        #region  "data members aka- public properties."

        [DataMember]
        public Guid PayorID { get; set; }
        [DataMember]
        public Guid CoverageID { get; set; }
        [DataMember]
        public Guid CarrierID { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string NickName { get; set; }
        [DataMember]
        public bool IsDeleted { get; set; }
        [DataMember]
        public bool IsGlobal { get; set; }
        [DataMember]
        public Guid UserID { get; set; }
        [DataMember]
        public Guid? LicenseeId { get; set; }
        [DataMember]
        public GlobalIncomingSchedule IncomingSchedule { get; set; }

        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// 
        public ReturnStatus AddUpdateDelete(Coverage selectedCoverage, OperationSet operationType)
        {
            using (DLinq.CommissionDepartmentEntities DataModel = Entity.DataModel)
            {
                ReturnStatus status = null;
                status = ValidateProduct(DataModel, operationType);
                if (!status.IsError)
                {
                    if (operationType.MainOperation == Operation.Add)
                    {
                        DLinq.Coverage product = new DLinq.Coverage
                        {
                            CoverageId = this.CoverageID,
                            ProductName = this.Name,
                            IsDeleted = this.IsDeleted,
                            IsGlobal = this.IsGlobal,
                            LicenseeId = (this.LicenseeId == Guid.Empty ? null : this.LicenseeId),
                            CreatedBy = this.UserID
                        };

                        // Check to coverage ID and then Add to  Coverages table
                        DLinq.Coverage coverage = DataModel.Coverages.FirstOrDefault(c => c.CoverageId == operationType.PreviousCoverageId);
                        if (coverage == null)
                        {
                            DataModel.AddToCoverages(product);
                        }
                        else
                        {
                            DLinq.Carrier carrier = DataModel.Carriers.FirstOrDefault(s => s.CarrierId == this.CarrierID);
                            var count = DataModel.Coverages.Where(s => s.CoverageId == operationType.PreviousCoverageId).ToList().Count;
                            DLinq.Coverage coverageAddRemoveUpdate = DataModel.Coverages.FirstOrDefault(s => s.CoverageId == operationType.PreviousCoverageId);
                            if (count == 1)
                            {
                                if (this.Name.ToUpper().Trim() == coverageAddRemoveUpdate.ProductName.ToUpper().Trim())
                                {
                                    //carrier.Coverages.Remove(coverageAddRemoveUpdate); //Acme commented as two tables have no relation only need to delete from data model
                                    DataModel.Coverages.DeleteObject(coverageAddRemoveUpdate);
                                    DataModel.AddToCoverages(product);
                                }
                                else
                                {
                                    //carrier.Coverages.Remove(coverageAddRemoveUpdate);
                                    DataModel.AddToCoverages(product);

                                }
                            }
                            else if (count > 1)
                            {
                                if (this.Name.ToUpper().Trim() == coverageAddRemoveUpdate.ProductName.ToUpper().Trim())
                                {
                                    coverageAddRemoveUpdate.CoverageId = this.CoverageID;
                                }
                                else
                                {
                                    DataModel.AddToCoverages(product);
                                }
                            }
                        }

                        //Add to table CarrierCoverage
                        DLinq.Carrier carrier1 = DataModel.Carriers.FirstOrDefault(s => s.CarrierId == this.CarrierID);
                        //carrier1.Coverages.Add(product);
                        DataModel.SaveChanges();
                    }

                    if (operationType.NickNameOperation == Operation.Add)
                    {
                        DLinq.CoverageNickName carrierCoverageNickName = DataModel.CoverageNickNames.FirstOrDefault(s => s.PayorId == this.PayorID && s.CarrierId == this.CarrierID && s.CoverageId == this.CoverageID && s.NickName.ToLower() == this.NickName.ToLower() && s.IsDeleted == true);
                        //DLinq.CoverageNickName carrierCoverage = DataModel.CoverageNickNames.FirstOrDefault(s => s.PayorId == this.PayorID && s.CarrierId == this.CarrierID && s.CoverageId == this.CoverageID && s.IsDeleted == true);

                        if (carrierCoverageNickName != null)
                        {
                            carrierCoverageNickName.IsDeleted = false;
                            carrierCoverageNickName.NickName = this.NickName;
                            carrierCoverageNickName.CreatedBy = this.UserID;
                            carrierCoverageNickName.ModifiedBy = this.UserID;
                            carrierCoverageNickName.ModifiedOn = DateTime.Now;
                            //Insert into carrier coverage table
                            DLinq.Carrier carrier = DataModel.Carriers.FirstOrDefault(s => s.CarrierId == this.CarrierID);
                            //DLinq.Coverage coverage = carrier.Coverages.Where(r => r.CoverageId == this.CoverageID).FirstOrDefault();
                            //Get coverage ID to remove
                            // if (coverage == null)
                            {
                                DLinq.Coverage coverageToInsert = DataModel.Coverages.FirstOrDefault(s => s.CoverageId == this.CoverageID);
                                //  carrier.Coverages.Add(coverageToInsert);
                            }

                        }
                        else
                        {
                            carrierCoverageNickName = new DLinq.CoverageNickName
                            {
                                PayorId = this.PayorID,
                                CarrierId = this.CarrierID,
                                CoverageId = this.CoverageID,
                                NickName = this.NickName,
                                CreatedBy = this.UserID,
                                ModifiedBy = this.UserID,
                                ModifiedOn = DateTime.Now,
                                IsDeleted = this.IsDeleted
                            };

                            DataModel.AddToCoverageNickNames(carrierCoverageNickName);

                            DLinq.Carrier carrier = DataModel.Carriers.FirstOrDefault(s => s.CarrierId == this.CarrierID);
                            // DLinq.Coverage coverage = carrier.Coverages.Where(r => r.CoverageId == this.CoverageID).FirstOrDefault();
                            //Get coverage ID to remove
                            //  if (coverage == null)
                            {
                                DLinq.Coverage coverageToInsert = DataModel.Coverages.FirstOrDefault(s => s.CoverageId == this.CoverageID);
                                //  carrier.Coverages.Add(coverageToInsert);
                            }
                        }
                    }
                    else if (operationType.NickNameOperation == Operation.Upadte)
                    {
                        //DLinq.CoverageNickName coverageNickName = DataModel.CoverageNickNames.FirstOrDefault(s => s.PayorId == this.PayorID && s.CarrierId == this.CarrierID && s.CoverageId == operationType.PreviousCoverageId);
                        DLinq.CoverageNickName coverageNickName = DataModel.CoverageNickNames.FirstOrDefault(s => s.PayorId == this.PayorID && s.CarrierId == this.CarrierID && s.CoverageId == operationType.PreviousCoverageId && s.NickName == selectedCoverage.NickName);
                        if (coverageNickName != null)
                        {
                            DataModel.CoverageNickNames.DeleteObject(coverageNickName);

                            coverageNickName = new DLinq.CoverageNickName { PayorId = this.PayorID, CarrierId = this.CarrierID, CoverageId = this.CoverageID, NickName = this.NickName, CreatedBy = this.UserID, ModifiedBy = this.UserID, ModifiedOn = DateTime.Now, IsDeleted = this.IsDeleted };
                            coverageNickName.Carrier = DataModel.Carriers.FirstOrDefault(s => s.CarrierId == this.CarrierID);
                            coverageNickName.Payor = DataModel.Payors.FirstOrDefault(s => s.PayorId == this.PayorID);
                            coverageNickName.Coverage = DataModel.Coverages.FirstOrDefault(s => s.CoverageId == this.CoverageID);

                            DataModel.AddToCoverageNickNames(coverageNickName);

                        }
                        else
                        {
                            coverageNickName = new DLinq.CoverageNickName { PayorId = this.PayorID, CarrierId = this.CarrierID, CoverageId = this.CoverageID, NickName = this.NickName, CreatedBy = this.UserID, ModifiedBy = this.UserID, ModifiedOn = DateTime.Now, IsDeleted = this.IsDeleted };
                            coverageNickName.Carrier = DataModel.Carriers.FirstOrDefault(s => s.CarrierId == this.CarrierID);
                            coverageNickName.Payor = DataModel.Payors.FirstOrDefault(s => s.PayorId == this.PayorID);
                            coverageNickName.Coverage = DataModel.Coverages.FirstOrDefault(s => s.CoverageId == this.CoverageID);

                            DataModel.AddToCoverageNickNames(coverageNickName);
                        }
                    }
                    else if (operationType.NickNameOperation == Operation.Delete)
                    {
                        DLinq.CoverageNickName CoverageNickName = (from c in DataModel.CoverageNickNames
                                                                   where (c.CarrierId == this.CarrierID && c.PayorId == this.PayorID && c.CoverageId == this.CoverageID && c.IsDeleted == false)
                                                                   select c).FirstOrDefault();

                        if (CoverageNickName != null && CoverageNickName.CoverageId != null)
                        {
                            //Set isDelete flag is True
                            DLinq.Coverage coverage = DataModel.Coverages.FirstOrDefault(s => s.CoverageId == this.CoverageID);
                            //Set coverage deleted true
                            coverage.IsDeleted = true;
                            //Set coverage nick name is true
                            CoverageNickName.IsDeleted = true;

                            DLinq.Carrier carrier = DataModel.Carriers.FirstOrDefault(s => s.CarrierId == this.CarrierID);
                            // carrier.Coverages.Remove(DataModel.Coverages.FirstOrDefault(s => s.CoverageId == this.CoverageID));
                        }
                    }
                    DataModel.SaveChanges();
                }
                return status;
            }
        }

        #region"PreviousCode"

        //public ReturnStatus AddUpdateDelete(OperationSet operationType)
        //{
        //    using (DLinq.CommissionDepartmentEntities DataModel = Entity.DataModel)
        //    {
        //        ReturnStatus status = null;
        //        status = ValidateProduct(DataModel, operationType);
        //        if (!status.IsError)
        //        {
        //            if (operationType.MainOperation == Operation.Add)
        //            {
        //                DLinq.Coverage product = new DLinq.Coverage
        //                {
        //                    CoverageId = this.CoverageID,
        //                    ProductName = this.Name,
        //                    IsDeleted = this.IsDeleted,
        //                    IsGlobal = this.IsGlobal,
        //                    LicenseeId = (this.LicenseeId == Guid.Empty ? null : this.LicenseeId),
        //                    CreatedBy = this.UserID
        //                };

        //                // Check to coverage ID and then Add to  Coverages table

        //                DLinq.Coverage coverage = DataModel.Coverages.FirstOrDefault(c => c.CoverageId == operationType.PreviousCoverageId);
        //                if (coverage == null)
        //                {
        //                    DataModel.AddToCoverages(product);
        //                }
        //                else
        //                {
        //                    DLinq.Carrier carrier = DataModel.Carriers.FirstOrDefault(s => s.CarrierId == this.CarrierID);
        //                    var count = DataModel.Coverages.Where(s => s.CoverageId == operationType.PreviousCoverageId).ToList().Count;
        //                    DLinq.Coverage coverageAddRemoveUpdate = DataModel.Coverages.FirstOrDefault(s => s.CoverageId == operationType.PreviousCoverageId);
        //                    if (count == 1)
        //                    {
        //                        if (this.Name.ToUpper().Trim() == coverageAddRemoveUpdate.ProductName.ToUpper().Trim())
        //                        {
        //                            carrier.Coverages.Remove(coverageAddRemoveUpdate);
        //                            DataModel.Coverages.DeleteObject(coverageAddRemoveUpdate);
        //                            DataModel.AddToCoverages(product);
        //                        }
        //                        else
        //                        {
        //                            carrier.Coverages.Remove(coverageAddRemoveUpdate);
        //                            DataModel.AddToCoverages(product);

        //                        }
        //                    }
        //                    else if (count > 1)
        //                    {
        //                        if (this.Name.ToUpper().Trim() == coverageAddRemoveUpdate.ProductName.ToUpper().Trim())
        //                        {
        //                            coverageAddRemoveUpdate.CoverageId = this.CoverageID;
        //                        }
        //                        else
        //                        {
        //                            DataModel.AddToCoverages(product);
        //                        }                            
        //                    }
        //                }

        //                //Add to table CarrierCoverage
        //                DLinq.Carrier carrier1 = DataModel.Carriers.FirstOrDefault(s => s.CarrierId == this.CarrierID);
        //                carrier1.Coverages.Add(product);                      
        //                DataModel.SaveChanges();
        //            }

        //            if (operationType.NickNameOperation == Operation.Add)
        //            {
        //                DLinq.CoverageNickName carrierCoverage = DataModel.CoverageNickNames.FirstOrDefault(s => s.PayorId == this.PayorID && s.CarrierId == this.CarrierID && s.CoverageId == this.CoverageID && s.IsDeleted == true);

        //                if (carrierCoverage != null)
        //                {
        //                    carrierCoverage.IsDeleted = false;
        //                    carrierCoverage.NickName = this.NickName;
        //                    carrierCoverage.CreatedBy = this.UserID;
        //                    carrierCoverage.ModifiedBy = this.UserID;
        //                    carrierCoverage.ModifiedOn = DateTime.Now;
        //                    //Insert into carrier coverage table
        //                    DLinq.Carrier carrier = DataModel.Carriers.FirstOrDefault(s => s.CarrierId == this.CarrierID);
        //                    DLinq.Coverage coverage = carrier.Coverages.Where(r => r.CoverageId == this.CoverageID).FirstOrDefault();
        //                    //Get coverage ID to remove
        //                    if (coverage == null)
        //                    {
        //                        DLinq.Coverage coverageToInsert = DataModel.Coverages.FirstOrDefault(s => s.CoverageId == this.CoverageID);
        //                        carrier.Coverages.Add(coverageToInsert);
        //                    }

        //                }
        //                else
        //                {
        //                    carrierCoverage = new DLinq.CoverageNickName
        //                    { 
        //                        PayorId = this.PayorID, 
        //                        CarrierId = this.CarrierID, 
        //                        CoverageId = this.CoverageID, 
        //                        NickName = this.NickName, 
        //                        CreatedBy = this.UserID, 
        //                        ModifiedBy = this.UserID, 
        //                        ModifiedOn = DateTime.Now, 
        //                        IsDeleted = this.IsDeleted
        //                    };

        //                    DataModel.AddToCoverageNickNames(carrierCoverage);                          

        //                    DLinq.Carrier carrier = DataModel.Carriers.FirstOrDefault(s => s.CarrierId == this.CarrierID);
        //                    DLinq.Coverage coverage=carrier.Coverages.Where(r => r.CoverageId == this.CoverageID).FirstOrDefault();
        //                    //Get coverage ID to remove
        //                    if (coverage == null)
        //                    {
        //                        DLinq.Coverage coverageToInsert = DataModel.Coverages.FirstOrDefault(s => s.CoverageId == this.CoverageID);
        //                        carrier.Coverages.Add(coverageToInsert);
        //                    }

        //                    //DataModel.AddToCoverages(product);
        //                    //carrierCoverage.Carrier = DataModel.Carriers.FirstOrDefault(s => s.CarrierId == this.CarrierID);
        //                    //carrierCoverage.Payor = DataModel.Payors.FirstOrDefault(s => s.PayorId == this.PayorID);
        //                    //carrierCoverage.Coverage = DataModel.Coverages.FirstOrDefault(s => s.CoverageId == this.CoverageID);

        //                }
        //            }
        //            else if (operationType.NickNameOperation == Operation.Upadte)
        //            {
        //                DLinq.CoverageNickName coverageNickName = DataModel.CoverageNickNames.FirstOrDefault(s => s.PayorId == this.PayorID && s.CarrierId == this.CarrierID && s.CoverageId == operationType.PreviousCoverageId);

        //                if (coverageNickName != null)
        //                {
        //                    DataModel.CoverageNickNames.DeleteObject(coverageNickName);

        //                    coverageNickName = new DLinq.CoverageNickName { PayorId = this.PayorID, CarrierId = this.CarrierID, CoverageId = this.CoverageID, NickName = this.NickName, CreatedBy = this.UserID, ModifiedBy = this.UserID, ModifiedOn = DateTime.Now, IsDeleted = this.IsDeleted };
        //                    coverageNickName.Carrier = DataModel.Carriers.FirstOrDefault(s => s.CarrierId == this.CarrierID);
        //                    coverageNickName.Payor = DataModel.Payors.FirstOrDefault(s => s.PayorId == this.PayorID);
        //                    coverageNickName.Coverage = DataModel.Coverages.FirstOrDefault(s => s.CoverageId == this.CoverageID);

        //                    DataModel.AddToCoverageNickNames(coverageNickName);

        //                }
        //                else
        //                {
        //                    coverageNickName = new DLinq.CoverageNickName { PayorId = this.PayorID, CarrierId = this.CarrierID, CoverageId = this.CoverageID, NickName = this.NickName, CreatedBy = this.UserID, ModifiedBy = this.UserID, ModifiedOn = DateTime.Now, IsDeleted = this.IsDeleted };
        //                    coverageNickName.Carrier = DataModel.Carriers.FirstOrDefault(s => s.CarrierId == this.CarrierID);
        //                    coverageNickName.Payor = DataModel.Payors.FirstOrDefault(s => s.PayorId == this.PayorID);
        //                    coverageNickName.Coverage = DataModel.Coverages.FirstOrDefault(s => s.CoverageId == this.CoverageID);

        //                    DataModel.AddToCoverageNickNames(coverageNickName);
        //                }

        //                //DLinq.Carrier tempcarrier = DataModel.Carriers.FirstOrDefault(s => s.CarrierId == this.CarrierID);
        //                //int carrierCoverageRelationExist = tempcarrier.Coverages.Where(s => s.CoverageId == this.CoverageID).Count();
        //                //if (carrierCoverageRelationExist == 0)
        //                //    tempcarrier.Coverages.Add(DataModel.Coverages.FirstOrDefault(s => s.CoverageId == this.CoverageID));
        //            }
        //            else if (operationType.NickNameOperation == Operation.Delete)
        //            {
        //                DLinq.CoverageNickName carrierCoverage = (from c in DataModel.CoverageNickNames
        //                                                          where (c.CarrierId == this.CarrierID && c.PayorId == this.PayorID && c.CoverageId == this.CoverageID && c.IsDeleted == false)
        //                                                          select c).FirstOrDefault();

        //                if (carrierCoverage != null && carrierCoverage.CoverageId != null)
        //                {
        //                    //Set isDelete flag is True
        //                    DLinq.Coverage coverage = DataModel.Coverages.FirstOrDefault(s => s.CoverageId == this.CoverageID);                           
        //                    coverage.IsDeleted = true;

        //                    carrierCoverage.IsDeleted = true;
        //                    DLinq.Carrier carrier = DataModel.Carriers.FirstOrDefault(s => s.CarrierId == this.CarrierID);
        //                    carrier.Coverages.Remove(DataModel.Coverages.FirstOrDefault(s => s.CoverageId == this.CoverageID));
        //                }

        //                //int coverageNickNameExist = (from c in DataModel.CoverageNickNames
        //                //                             where (c.CoverageId == this.CoverageID && c.IsDeleted == false)
        //                //                             select c).Count();
        //                //if (coverageNickNameExist == 0)
        //                //{
        //                //    DLinq.Coverage coverage = DataModel.Coverages.FirstOrDefault(s => s.CoverageId == this.CoverageID);
        //                //    coverage.IsDeleted = true;
        //                //    status.IsCarrierOrCoverageRemoved = true;
        //                //    //DataModel.SaveChanges();
        //                //}
        //            }
        //            DataModel.SaveChanges();
        //        }
        //        return status;
        //    }
        //}

        #endregion

        #region"Delete nick name"

        public static void DeleteNickName(int CoverageNickId, string coverageNickName, bool flag, out bool isPolicyExist)
        {
            isPolicyExist = true;
            try
            {
                using (DLinq.CommissionDepartmentEntities DataModel = Entity.DataModel)
                {
                    DLinq.PolicyLearnedField policyLearnedFields = (from po in DataModel.PolicyLearnedFields
                                                                    where (po.CoverageNickName.ToLower() == coverageNickName.ToLower())
                                                                    select po).FirstOrDefault();
                    if (policyLearnedFields != null)
                    {
                        isPolicyExist = true;

                    }
                    else
                    {
                        isPolicyExist = false;
                        if (flag == true)
                        {
                            DLinq.CoverageNickName coverageNickname = DataModel.CoverageNickNames.FirstOrDefault(s => s.CoverageNickID == CoverageNickId && s.NickName.ToLower().Trim() == coverageNickName.ToLower().Trim());
                            if (coverageNickName != null)
                            {
                                coverageNickname.IsDeleted = true;
                                DataModel.SaveChanges();
                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            {
            }
        }


        public static void DeleteCoverage(Guid coverageId, bool flag, out bool isPolicyExist)
        {
            isPolicyExist = true;
            try
            {
                using (DLinq.CommissionDepartmentEntities DataModel = Entity.DataModel)
                {

                    DLinq.Policy policy = (from po in DataModel.Policies
                                           where (po.CoverageId == coverageId
                                           && po.IsDeleted == false)
                                           select po).FirstOrDefault();
                    isPolicyExist = policy == null ? false : true;
                    if (flag == true && isPolicyExist == false)
                    {
                        DLinq.Coverage coverage = DataModel.Coverages.FirstOrDefault(s => s.CoverageId == coverageId);
                        if (coverage != null)
                        {
                            coverage.IsDeleted = true;
                            DataModel.SaveChanges();
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        /// <summary>
        /// 
        /// </summary>
        /// <param name="PayorId"></param>
        /// <param name="CarrierId"></param>
        /// <param name="CoverageId"></param>
        /// <returns></returns>
        /// 
        #region"Delete Prodct Type name"

        public static ReturnStatus DeleteProductType(Guid guidPayorID, Guid guidCarrierID, Guid guidCoverageId, string strNickNames)
        {
            ActionLogger.Logger.WriteLog("DeleteProductType : guidPayorID - " + guidPayorID + ", guidCoverageId: " + guidCoverageId + ", guidCarrierID: " + guidCarrierID, true);
            ReturnStatus status = new ReturnStatus();
            status.IsError = false;
            try
            {
                using (DLinq.CommissionDepartmentEntities DataModel = Entity.DataModel)
                {

                    DLinq.PolicyLearnedField policyLearnedFields = (from po in DataModel.PolicyLearnedFields
                                                                    where (po.CoverageNickName.ToLower() == strNickNames.ToLower())
                                                                    select po).FirstOrDefault();
                    if (policyLearnedFields != null)
                    {
                        status.IsError = true;
                        status.ErrorMessage = "Some policy refer to " + strNickNames + " product nick name. You can not delete/update product nick without deleting all the policies that refer this product nick name.";
                        return status;
                    }
                    DLinq.CoverageNickName coverageNickName = DataModel.CoverageNickNames.FirstOrDefault(s => s.PayorId == guidPayorID && s.CarrierId == guidCarrierID && s.CoverageId == guidCoverageId && s.NickName.ToLower().Trim() == strNickNames.ToLower().Trim());
                    if (coverageNickName != null)
                    {
                        coverageNickName.IsDeleted = true;
                        DataModel.SaveChanges();
                    }

                }
            }
            catch (Exception ex)
            {
                ActionLogger.Logger.WriteLog("Exception DeleteProductType: " + ex.Message, true);
            }

            return status;
        }

        #endregion
        public static Coverage GetCarrierCoverage(Guid PayorId, Guid CarrierId, Guid CoverageId)
        {
            if (PayorId == Guid.Empty || CarrierId == Guid.Empty || CoverageId == Guid.Empty)
                return null;

            using (DLinq.CommissionDepartmentEntities DataModel = Entity.DataModel)
            {
                return (from gc in DataModel.CoverageNickNames
                        where (gc.CoverageId == CoverageId)
                        && (gc.IsDeleted == false)
                        && (gc.CarrierId == CarrierId)
                        && (gc.PayorId == PayorId)
                        orderby gc.Coverage.ProductName
                        select new Coverage
                        {
                            Name = gc.Coverage.ProductName,
                            LicenseeId = gc.Coverage.LicenseeId,
                            IsGlobal = gc.Coverage.IsGlobal,
                            NickName = gc.NickName,
                        }).ToList().FirstOrDefault();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="PayorId"></param>
        /// <param name="CarrierId"></param>
        /// <param name="CoverageId"></param>
        /// <returns></returns>
        public static string GetCoverageNickName(Guid PayorId, Guid CarrierId, Guid CoverageId)
        {
            if (PayorId == Guid.Empty || CarrierId == Guid.Empty || CoverageId == Guid.Empty)
                return string.Empty;

            using (DLinq.CommissionDepartmentEntities DataModel = Entity.DataModel)
            {
                string nickName = string.Empty;
                DLinq.CoverageNickName carriercoverage = DataModel.CoverageNickNames.FirstOrDefault(s => s.PayorId == PayorId && s.CarrierId == CarrierId && s.CoverageId == CoverageId && s.IsDeleted == false);
                if (carriercoverage != null)
                {
                    nickName = carriercoverage.NickName;
                }
                return nickName;
            }
        }
        #region
        /// <summary>
        /// Created by:Ankit khandelwal
        /// Createdon:21-08-2019
        /// Purpose:getting list of coverage nicknames
        /// </summary>
        /// <param name="payorId"></param>
        /// <param name="carrierId"></param>
        /// <param name="coverageId"></param>
        /// <param name="listParams"></param>
        /// <returns></returns>
        public static List<CoverageNickName> GetCoverageNickNamesList(Guid payorId, Guid carrierId, Guid coverageId, ListParams listParams, out int recordCount)
        {
            List<CoverageNickName> coveragelist = new List<CoverageNickName>();
            try
            {
                using (SqlConnection con = new SqlConnection(DBConnection.GetConnectionString()))
                {
                    using (SqlCommand cmd = new SqlCommand("usp_GetCoverageNickNameList", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        int rowStart = (listParams.pageSize == 0 && listParams.pageIndex == 0) ? 0 : (listParams.pageSize * (listParams.pageIndex)) + 1;
                        int rowEnd = (listParams.pageSize == 0 && listParams.pageIndex == 0) ? 0 : (listParams.pageIndex + 1) * listParams.pageSize;
                        cmd.Parameters.AddWithValue("@RowStart", rowStart);
                        cmd.Parameters.AddWithValue("@RowEnd", rowEnd);
                        cmd.Parameters.AddWithValue("@sortBy", listParams.sortBy);
                        cmd.Parameters.AddWithValue("@sortType", listParams.sortType);
                        cmd.Parameters.AddWithValue("@filterBy", listParams.filterBy);
                        cmd.Parameters.AddWithValue("@PayorId", payorId);
                        cmd.Parameters.AddWithValue("@CarrierId", carrierId);
                        cmd.Parameters.AddWithValue("@CoverageId", coverageId);
                        cmd.Parameters.Add("@recordLength", SqlDbType.Int);
                        cmd.Parameters["@recordLength"].Direction = ParameterDirection.Output;
                        con.Open();
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            CoverageNickName coverageRecord = new CoverageNickName();
                            coverageRecord.ProductName = reader.IsDBNull("ProductName") ? null : Convert.ToString(reader["ProductName"]);
                            coverageRecord.NickName = reader.IsDBNull("NickName") ? null : Convert.ToString(reader["NickName"]);
                            coverageRecord.CoverageID = (Guid)(reader["CoverageID"]);
                            coverageRecord.PayorID = (Guid)(reader["PayorID"]);
                            coverageRecord.CarrierID = (Guid)(reader["CarrierID"]);
                            coverageRecord.CoverageNickId = (int)(reader["CoverageNickID"]);
                            coveragelist.Add(coverageRecord);
                        }
                        reader.Close();
                        int.TryParse(Convert.ToString(cmd.Parameters["@recordLength"].Value), out recordCount);
                    }
                }
            }
            catch (Exception ex)
            {
                ActionLogger.Logger.WriteLog("getContacts exception " + ex.Message, true);
                throw ex;
            }
            return coveragelist;
        }

        /// <summary>
        /// Created By:Ankit khandelwal
        /// CreatedOn:28-08-2019
        /// Purpose:Add update the coverage details
        /// </summary>
        /// <param name="coverageDetails"></param>
        /// <returns></returns>
        public static int AddUpdateCoverageType(CoverageNickName coverageDetails)
        {
            ActionLogger.Logger.WriteLog("AddUpdateCoverageType:Request start for processing with coverageId " + coverageDetails.CoverageID, true);
            int recordStatus = -1;
            try
            {
                using (DLinq.CommissionDepartmentEntities DataModel = Entity.DataModel)
                {

                    DLinq.Coverage Coverage = DataModel.Coverages.FirstOrDefault(s => s.CoverageId == coverageDetails.CoverageID && s.ProductName == coverageDetails.ProductName && s.IsDeleted == false);
                    if (Coverage == null)
                    {
                        Coverage coverageData = new Coverage();
                        coverageData.CoverageID = coverageDetails.CoverageID;
                        coverageData.Name = coverageDetails.ProductName;
                        int status = AddUpdateCoverageDetails(coverageData);
                    }
                }
            }
            catch (Exception ex)
            {
                ActionLogger.Logger.WriteLog("AddUpdateCoverageType:Exception occurs while processing" + ex.Message, true);
                throw ex;
            }
            try
            {
                using (SqlConnection con = new SqlConnection(DBConnection.GetConnectionString()))
                {
                    using (SqlCommand cmd = new SqlCommand("usp_AddUpdateCoverageType", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@PayorId", coverageDetails.PayorID);
                        cmd.Parameters.AddWithValue("@CarrierId", coverageDetails.CarrierID);
                        cmd.Parameters.AddWithValue("@CoverageId", coverageDetails.CoverageID);
                        cmd.Parameters.AddWithValue("@NickName", coverageDetails.NickName);
                        cmd.Parameters.AddWithValue("@ModifiedBy", coverageDetails.ModifiedBy);
                        cmd.Parameters.AddWithValue("@CreatedBy", coverageDetails.CreatedBy);
                        cmd.Parameters.AddWithValue("@CoverageNickId", coverageDetails.CoverageNickId);
                        cmd.Parameters.Add("@recordStatus", SqlDbType.Int);
                        cmd.Parameters["@recordStatus"].Direction = ParameterDirection.Output;
                        con.Open();
                        recordStatus = (int)cmd.ExecuteScalar();

                    }
                }
            }
            catch (Exception ex)
            {
                ActionLogger.Logger.WriteLog("AddUpdateCoverageType:Exception occurs while processing" + ex.Message, true);
                throw ex;
            }
            return recordStatus;
        }


        public static int AddUpdateCoverageDetails(Coverage coverageDetails)
        {
            ActionLogger.Logger.WriteLog("AddUpdateCoverageDetails:Request start for processing with coverageId " + coverageDetails.CoverageID, true);

            int recordStatus = -1;
            try
            {
                using (SqlConnection con = new SqlConnection(DBConnection.GetConnectionString()))
                {
                    using (SqlCommand cmd = new SqlCommand("usp_AddUpdateCoverage", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@CoverageId", coverageDetails.CoverageID);
                        cmd.Parameters.AddWithValue("@CoverageName", coverageDetails.Name);
                        cmd.Parameters.Add("@recordStatus", SqlDbType.Int);
                        cmd.Parameters["@recordStatus"].Direction = ParameterDirection.Output;
                        con.Open();
                        recordStatus = (int)cmd.ExecuteScalar();

                    }
                }
            }
            catch (Exception ex)
            {
                ActionLogger.Logger.WriteLog("AddUpdateCoverageDetails:Exception occurs while processing" + ex.Message, true);
                throw ex;
            }
            return recordStatus;
        }
        public static List<CoverageNickName> GetAllNickNames(Guid PayorId, Guid CarrierId, Guid CoverageId)
        {
            List<CoverageNickName> _nickNamedcoverages = null;
            try
            {
                if (PayorId == Guid.Empty || CarrierId == Guid.Empty)
                    return null;
                using (DLinq.CommissionDepartmentEntities DataModel = Entity.DataModel)
                {
                    List<string> lstNickNames = new List<string>();
                    DLinq.Carrier carrier = DataModel.Carriers.FirstOrDefault(s => s.CarrierId == CarrierId && s.IsDeleted == false);
                    if (carrier == null) return null;
                    _nickNamedcoverages = (from cc in DataModel.CoverageNickNames
                                           where (cc.IsDeleted == false) && (cc.PayorId == PayorId) && (cc.CarrierId == CarrierId && cc.CoverageId == CoverageId)
                                           select new CoverageNickName
                                           {
                                               ID = cc.CoverageNickID,
                                               PayorID = cc.PayorId,
                                               CarrierID = cc.CarrierId,
                                               CoverageID = cc.CoverageId,
                                               NickName = cc.NickName,
                                               IsDeleted = cc.IsDeleted
                                           }).ToList();

                    //Acme added March 07, 2017 as per kevin's email
                    if (_nickNamedcoverages != null && _nickNamedcoverages.Count > 0)
                    {
                        _nickNamedcoverages = _nickNamedcoverages.OrderBy(x => x.NickName).ToList();
                    }

                }
            }
            catch (Exception ex)
            {
                ActionLogger.Logger.WriteLog("GetAllNickNames:Exception occur while fetching list of product type list", true);
                throw ex;
            }
            return _nickNamedcoverages;

        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="LicenseeID"></param>
        /// <returns></returns>
        public static List<Coverage> GetCoverages(Guid licenseeId, ListParams listParams, out int recordCount)
        {
            ActionLogger.Logger.WriteLog("GetCoverages:Processing starts with LicneseeId:" + licenseeId, true);
            List<Coverage> coverageList = new List<Coverage>();
            try
            {
                using (DLinq.CommissionDepartmentEntities DataModel = Entity.DataModel)
                {
                    using (SqlConnection con = new SqlConnection(DBConnection.GetConnectionString()))
                    {
                        int rowStart = (listParams.pageSize == 0 && listParams.pageIndex == 0) ? 0 : (listParams.pageSize * (listParams.pageIndex)) + 1;
                        int rowEnd = (listParams.pageSize == 0 && listParams.pageIndex == 0) ? 0 : (listParams.pageIndex + 1) * listParams.pageSize;
                        using (SqlCommand cmd = new SqlCommand("usp_getProductList", con))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@licenseeId", licenseeId);
                            cmd.Parameters.AddWithValue("@RowStart", rowStart);
                            cmd.Parameters.AddWithValue("@RowEnd", rowEnd);
                            cmd.Parameters.AddWithValue("@sortBy", listParams.sortBy);
                            cmd.Parameters.AddWithValue("@sortType", listParams.sortType);
                            cmd.Parameters.AddWithValue("@filterBy", listParams.filterBy);
                            cmd.Parameters.Add("@recordCount", SqlDbType.Int);
                            cmd.Parameters["@recordCount"].Direction = ParameterDirection.Output;
                            con.Open();
                            SqlDataReader reader = cmd.ExecuteReader();
                            while (reader.Read())
                            {
                                Coverage recordDetail = new Coverage();
                                recordDetail.CoverageID = (Guid)reader["CoverageId"];
                                recordDetail.Name = reader.IsDBNull("ProductName") ? null : Convert.ToString(reader["ProductName"]);
                                coverageList.Add(recordDetail);
                            }
                            reader.Close();
                            int.TryParse(Convert.ToString(cmd.Parameters["@recordCount"].Value), out recordCount);
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                ActionLogger.Logger.WriteLog("GetCoverages:Exception occur while fetching list of products", true);
                throw ex;

            }
            return coverageList;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="PayorId"></param>
        /// <param name="CarrierId"></param>
        /// <returns></returns>
        public static List<Coverage> GetCarrierCoverages(Guid CarrierId)
        {
            if (CarrierId == Guid.Empty)
                return null;

            using (DLinq.CommissionDepartmentEntities DataModel = Entity.DataModel)
            {
                List<Coverage> _cover = new List<Coverage>();

                DLinq.Carrier tmpCarrier = DataModel.Carriers.FirstOrDefault(s => s.CarrierId == CarrierId && s.IsDeleted == false);

                //_cover = (from cc in tmpCarrier.Coverages
                //          where (cc.IsDeleted == false)
                //          orderby cc.ProductName
                //          select new Coverage
                //          {
                //              CoverageID = cc.CoverageId,
                //              CarrierID = CarrierId,
                //              Name = cc.ProductName,
                //              NickName = cc.ProductName,
                //              LicenseeId = cc.LicenseeId,
                //              IsGlobal = cc.IsGlobal,
                //              UserID = cc.CreatedBy.Value
                //          }).OrderBy(p => p.Name).Distinct().ToList();
                return _cover;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="PayorId"></param>
        /// <param name="CarrierId"></param>
        /// <returns></returns>
        public static List<DisplayedCoverage> GetDisplayedCarrierCoverages(Guid LicenseeID)
        {
            List<DisplayedCoverage> _cover;
            using (DLinq.CommissionDepartmentEntities DataModel = Entity.DataModel)
            {

                if (LicenseeID != Guid.Empty)
                {
                    _cover = (from cc in DataModel.Coverages
                              where (cc.IsDeleted == false) && ((cc.LicenseeId == LicenseeID) || (cc.IsGlobal == true))
                              orderby cc.ProductName
                              select new DisplayedCoverage
                              {
                                  CoverageID = cc.CoverageId,
                                  Name = cc.ProductName,
                                  IsGlobal = cc.IsGlobal
                              }).OrderBy(p => p.Name).ToList();
                    return _cover;
                }
                else
                {
                    _cover = (from cc in DataModel.Coverages
                              where (cc.IsDeleted == false) && (cc.IsGlobal == true)
                              orderby cc.ProductName
                              select new DisplayedCoverage
                              {
                                  CoverageID = cc.CoverageId,
                                  Name = cc.ProductName,
                                  IsGlobal = cc.IsGlobal
                              }).OrderBy(k => k.Name).ToList();
                    return _cover;

                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="PayorId"></param>
        /// <param name="CarrierId"></param>
        /// <returns></returns>
        public static List<Coverage> GetCarrierCoverages(Guid PayorId, Guid CarrierId)
        {
            if (PayorId == Guid.Empty || CarrierId == Guid.Empty)
                return null;
            using (DLinq.CommissionDepartmentEntities DataModel = Entity.DataModel)
            {
                DLinq.Carrier carrier = DataModel.Carriers.FirstOrDefault(s => s.CarrierId == CarrierId && s.IsDeleted == false);
                if (carrier == null) return null;
                List<Coverage> _allCoverages = null;
                //List<Coverage> _allCoverages = carrier.Coverages.Where(s => s.IsDeleted == false).OrderBy(c => c.ProductName).Select(s => new Coverage
                //{
                //    CoverageID = s.CoverageId,
                //    Name = s.ProductName,
                //    CarrierID = CarrierId,
                //    PayorID = PayorId,
                //    LicenseeId = s.LicenseeId,
                //    UserID = s.CreatedBy.Value
                //}).ToList();


                List<Coverage> _nickNamedcoverages = (from cc in DataModel.CoverageNickNames
                                                      where (cc.IsDeleted == false) && (cc.PayorId == PayorId) && (cc.CarrierId == CarrierId)
                                                      select new Coverage
                                                      {
                                                          CoverageID = cc.CoverageId,
                                                          NickName = cc.NickName,
                                                      }).ToList();

                //foreach (Coverage nickNamedCoverages in _nickNamedcoverages)
                //{
                //    Coverage coverage = _allCoverages.FirstOrDefault(s => s.CoverageID == nickNamedCoverages.CoverageID);
                //    List<Coverage> TempMultileCoveragesNickName = _nickNamedcoverages.Where(s => s.CoverageID == nickNamedCoverages.CoverageID).ToList();
                //    string strNickName = string.Empty;

                //    if (TempMultileCoveragesNickName != null)
                //    {
                //        foreach (var Value in TempMultileCoveragesNickName)
                //        {
                //            strNickName += Value.NickName + "; ";
                //        }
                //        strNickName = strNickName.Remove(strNickName.Length - 2, 1);
                //        //check coverage null then show its nick name
                //        if (coverage != null)
                //        {
                //            coverage.NickName = strNickName;
                //        }
                //    }

                //}
                //Check payor avalable with nick name.
                if (_nickNamedcoverages.Count > 0)
                {
                    return _allCoverages;
                }
                else
                {
                    if (_allCoverages != null)
                        _allCoverages.Clear();
                }

                return _allCoverages;
            }
        }

        public static bool IsValidCoverage(string carrierNickName, string coverageNickName, Guid payorId)
        {
            using (DLinq.CommissionDepartmentEntities DataModel = Entity.DataModel)
            {

                DLinq.CarrierNickName carrier = DataModel.CarrierNickNames.FirstOrDefault(s => s.NickName.ToUpper() == carrierNickName.ToUpper() && s.PayorId == payorId && s.IsDeleted == false);
                if (carrier != null)
                {
                    DLinq.CoverageNickName coverage = DataModel.CoverageNickNames.FirstOrDefault(s => s.NickName.ToUpper() == coverageNickName.ToUpper() && s.CarrierId == carrier.CarrierId && s.PayorId == payorId && s.IsDeleted == false);
                    if (coverage != null)
                        return true;
                    else
                        return false;
                }
                else
                    return false;
            }
        }

        public static DisplayedCoverage GetCoverageForPolicy(Guid DisplayedCoverageID)
        {
            using (DLinq.CommissionDepartmentEntities DataModel = Entity.DataModel)
            {
                DisplayedCoverage newDisplayedCoverage = (from coverage in DataModel.Coverages
                                                          where coverage.CoverageId == DisplayedCoverageID
                                                          select new DisplayedCoverage
                                                          {
                                                              CoverageID = coverage.CoverageId,
                                                              IsGlobal = coverage.IsGlobal,
                                                              Name = coverage.ProductName
                                                          }).FirstOrDefault();
                return newDisplayedCoverage;
            }
        }
        /// <summary>
        /// This function is used to validate the carrier for system.
        /// </summary>
        /// <returns></returns>
        private ReturnStatus ValidateProduct(DLinq.CommissionDepartmentEntities DataModel, OperationSet operation)
        {
            ReturnStatus retStatus = new ReturnStatus();

            if (operation.MainOperation == Operation.Add)
            {
                List<DLinq.CoverageNickName> coverages = DataModel.CoverageNickNames.Where(s => s.PayorId == this.PayorID && s.CarrierId == this.CarrierID && s.IsDeleted == false).ToList();

                if (coverages == null || coverages.Count == 0)
                    return retStatus;
                else
                {
                    DLinq.CoverageNickName coverage = coverages.FirstOrDefault(s => s.Coverage.ProductName == this.Name && s.IsDeleted == false);
                    if (coverage != null)
                    {
                        retStatus.IsError = true;
                        retStatus.ErrorMessage = "This product is already present.";
                    }
                }
            }

            if (operation.NickNameOperation == Operation.Add)
            {
                //List<DLinq.CoverageNickName> coverages = DataModel.CoverageNickNames.Where(s => s.PayorId == this.PayorID && s.CarrierId == this.CarrierID && s.IsDeleted == false).ToList();
                List<DLinq.CoverageNickName> coverages = DataModel.CoverageNickNames.Where(s => s.PayorId == this.PayorID && s.CarrierId == this.CarrierID && s.NickName.ToLower() == this.NickName.ToLower() && s.IsDeleted == false).ToList();

                if (coverages == null || coverages.Count == 0)
                    return retStatus;
                else
                {
                    DLinq.CoverageNickName coverage = coverages.FirstOrDefault(s => s.NickName == this.NickName && s.IsDeleted == false);
                    if (coverage != null)
                    {
                        retStatus.IsError = true;
                        retStatus.ErrorMessage = string.Format("This product nickname is already present with Payor:{0}-Carrier:{1}. Please select other Nickname.", coverage.Payor.PayorName, coverage.Carrier.CarrierName);
                    }
                }
            }
            else if (operation.NickNameOperation == Operation.Upadte)
            {
                //int count = DataModel.CoverageNickNames.Where(s => s.NickName == this.NickName && s.PayorId == this.PayorID && s.CarrierId == this.CarrierID && s.CoverageId != this.CoverageID && s.IsDeleted == false).ToList().Count;
                int count = DataModel.CoverageNickNames.Where(s => s.NickName == this.NickName && s.PayorId == this.PayorID && s.CarrierId == this.CarrierID && s.IsDeleted == false).ToList().Count;
                if (count != 0)
                {
                    retStatus.IsError = true;
                    retStatus.ErrorMessage = "This product nickname is already present. Please select other.";
                }
            }
            else if (operation.NickNameOperation == Operation.Delete)
            {
                DLinq.Policy policy = (from po in DataModel.Policies
                                       where (po.Coverage.CoverageId == this.CoverageID
                                       && po.Carrier.CarrierId == this.CarrierID
                                       && po.IsDeleted == false)
                                       select po).FirstOrDefault();
                if (policy != null)
                {
                    retStatus.IsError = true;
                    retStatus.ErrorMessage = "Some policy refer this product. You can not delete product without deleting all the policies that refer this product.";
                }
            }

            return retStatus;
        }
    }

    [DataContract]
    public class CoverageNickName
    {
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public Guid PayorID { get; set; }
        [DataMember]
        public Guid CoverageID { get; set; }
        [DataMember]
        public Guid CarrierID { get; set; }
        [DataMember]
        public string NickName { get; set; }
        [DataMember]
        public string ProductName { get; set; }
        [DataMember]
        public bool? IsDeleted { get; set; }
        [DataMember]
        public bool IsGlobal { get; set; }
        [DataMember]
        public Guid UserID { get; set; }
        [DataMember]
        public Guid? LicenseeId { get; set; }

        [DataMember]
        public Guid? ModifiedBy { get; set; }
        [DataMember]
        public Guid? CreatedBy { get; set; }

        [DataMember]
        public int? CoverageNickId { get; set; }

    }

    [DataContract]
    public class DisplayedCoverage
    {
        [DataMember]
        public Guid CoverageID { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public bool IsGlobal { get; set; }
    }
}
