using System;
using System.Collections.Generic;
using System.Linq;
using MyAgencyVault.BusinessLibrary.Base;
using System.Runtime.Serialization;
using DataAccessLayer.LinqtoEntity;
using DLinq = DataAccessLayer.LinqtoEntity;
using MyAgencyVault.BusinessLibrary.Masters;
using System.Collections.ObjectModel;
using System.Data.EntityClient;
using System.Data.SqlClient;
using System.Data;

namespace MyAgencyVault.BusinessLibrary
{
    [DataContract]
    public class ConfigSegment
    {
        [DataMember]
        public Guid? SegmentId { get; set; }
        [DataMember]
        public string SegmentName { get; set; }
        [DataMember]
        public Guid LicenseeId { get; set; }
        public static List<ConfigSegment> GetSegmentsForPolicies(Guid LicenseeId)
        {
            ActionLogger.Logger.WriteLog("GetSegmentsForPolicies start:", true);

            List<ConfigSegment> lstsegments = new List<ConfigSegment>();
            try
            {
                using (SqlConnection con = new SqlConnection(DBConnection.GetConnectionString()))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_GetSegmentsForPolicies", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandTimeout = 0;
                        cmd.Parameters.AddWithValue("@LicenseeId", LicenseeId);
                        if (con.State == ConnectionState.Closed)
                        {
                            con.Open();
                        }

                        SqlDataReader dr = cmd.ExecuteReader();
                        while (dr.Read())
                        {
                            ConfigSegment segment = new ConfigSegment();
                            segment.SegmentName = Convert.ToString(dr["SegmentName"]);
                            segment.SegmentId = (Guid)dr["SegmentId"];
                            segment.LicenseeId = LicenseeId;
                            lstsegments.Add(segment);
                        }
                        dr.Close();
                        con.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                ActionLogger.Logger.WriteLog("GetSegmentsForPolicies exception:" + ex.Message, true);
                throw ex;
            }
            return lstsegments;
        }

        public static List<ConfigSegment> GetSegmentsForConfiguration(ListParams listParams, out int count,Guid LicenseeId)
        {
            ActionLogger.Logger.WriteLog("GetSegmentsForConfiguration request:" + listParams.ToStringDump(), true);
            //String LinseeId = "29C25140-F373-4947-849D-BC010B021412";
            List<ConfigSegment> lstsegments = new List<ConfigSegment>();
            int recordCount = 0;
            try
            {
               // listParams.pageSize = 10;

                int rowStart = (listParams.pageSize == 0 && listParams.pageIndex == 0) ? 0 : (listParams.pageSize * (listParams.pageIndex)) + 1;
                int rowEnd = (listParams.pageSize == 0 && listParams.pageIndex == 0) ? 0 : (listParams.pageIndex + 1) * listParams.pageSize;
                using (SqlConnection con = new SqlConnection(DBConnection.GetConnectionString()))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_GetSegmentsListing", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandTimeout = 0;
                        cmd.Parameters.AddWithValue("@RowStart", rowStart);
                        cmd.Parameters.AddWithValue("@RowEnd", rowEnd);
                        cmd.Parameters.AddWithValue("@sortBy", listParams.sortBy);
                        cmd.Parameters.AddWithValue("@sortType", listParams.sortType);
                        cmd.Parameters.AddWithValue("@filterBy", listParams.filterBy);
                        cmd.Parameters.AddWithValue("@LicenseeId", LicenseeId);
                        cmd.Parameters.Add("@recordLength", SqlDbType.Int);
                        cmd.Parameters["@recordLength"].Direction = ParameterDirection.Output;
                        if (con.State == ConnectionState.Closed)                      
                            con.Open();                    

                        SqlDataReader dr = cmd.ExecuteReader();
                        while (dr.Read())
                        {
                            ConfigSegment segment = new ConfigSegment();
                            segment.SegmentName = Convert.ToString(dr["SegmentName"]);
                            segment.SegmentId = (Guid)dr["SegmentId"];
                            segment.LicenseeId = LicenseeId;
                            lstsegments.Add(segment);
                        }
                        dr.Close();
                        con.Close();
                        int.TryParse(Convert.ToString(cmd.Parameters["@recordLength"].Value), out recordCount);
                    }
                }
            }
            catch (Exception ex)
            {
                ActionLogger.Logger.WriteLog("GetSegmentsForConfiguration exception:" + ex.Message, true);
                throw ex;
            }
            count = recordCount;
            return lstsegments;
        }

        public static Guid GetSegmentsOnCoverageId(Guid? CoverageId, Guid? PolicyId, Guid LicenseeId)
        {
            ActionLogger.Logger.WriteLog("GetSegmentsOnCoverageId start:", true);
            Guid segmentId = Guid.Empty;
            try
            {
                using (SqlConnection con = new SqlConnection(DBConnection.GetConnectionString()))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_GetSegmentsOnCoverageIdOrPolicyId", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandTimeout = 0;
                        cmd.Parameters.AddWithValue("@CoverageId", CoverageId);
                        cmd.Parameters.AddWithValue("@PolicyId", PolicyId);
                        cmd.Parameters.AddWithValue("@LicenseeId", LicenseeId);
                        cmd.Parameters.Add("@SegmentId", SqlDbType.UniqueIdentifier);
                        cmd.Parameters["@SegmentId"].Direction = ParameterDirection.Output;
                        if (con.State == ConnectionState.Closed)
                        {
                            con.Open();
                        }

                        //string segId = Convert.ToString(cmd.ExecuteScalar());
                        cmd.ExecuteNonQuery();
                        string segId = Convert.ToString(cmd.Parameters["@SegmentId"].Value);
                        if (!string.IsNullOrEmpty(segId))
                        {
                            segmentId = Guid.Parse(segId);
                        }
                        con.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                ActionLogger.Logger.WriteLog("GetSegmentsOnCoverageId exception:" + ex.Message, true);
                throw ex;
            }
            return segmentId;
        }

    }

    [DataContract]
    public class ConfigProductWithoutSegment
    {
        [DataMember]
        public Guid CoverageId { get; set; }
        [DataMember]
        public string ProductName { get; set; }
        [DataMember]
        public bool IsModifiable { get; set; }
        [DataMember]
        public Guid? SegmentId { get; set; }
        [DataMember]
        public bool Checked { get; set; }
        [DataMember]
        public Guid LicenseeId { get; set; }
        //[DataMember]
        //public bool IsDisabled { get; set; }
        //[DataMember]
        //public bool toggleSelect { get; set; }

        public static List<ConfigProductWithoutSegment> GetProductsWithoutSegments(Guid LicenseeId)
        {
            ActionLogger.Logger.WriteLog("GetProductsWithoutSegments start", true);

            List<ConfigProductWithoutSegment> lstsegments = new List<ConfigProductWithoutSegment>();
            try
            {
                using (SqlConnection con = new SqlConnection(DBConnection.GetConnectionString()))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_GetProductsWithoutSegmentId", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandTimeout = 0;
                        cmd.Parameters.AddWithValue("@LicenseeId", LicenseeId);
                        if (con.State == ConnectionState.Closed)
                        {
                            con.Open();
                        }

                        SqlDataReader dr = cmd.ExecuteReader();
                        while (dr.Read())
                        {
                            ConfigProductWithoutSegment segment = new ConfigProductWithoutSegment();
                            segment.ProductName = Convert.ToString(dr["ProductName"]);
                            segment.CoverageId = (Guid)dr["CoverageId"];
                            segment.IsModifiable = true;
                            //segment.toggleSelect = false;
                            segment.LicenseeId = LicenseeId;
                            lstsegments.Add(segment);
                        }
                        dr.Close();
                        con.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                ActionLogger.Logger.WriteLog("GetProductsWithoutSegments exception:" + ex.Message, true);
                throw ex;
            }
            return lstsegments;
        }

        public static List<ConfigProductWithoutSegment> GetProductsSegmentsForUpdate(Guid segmentId, Guid LicenseeId)
        {
            ActionLogger.Logger.WriteLog("GetProductsSegmentsForUpdate start", true);
            //String LinseeId = "29C25140-F373-4947-849D-BC010B021412";
            List<ConfigProductWithoutSegment> lstsegments = new List<ConfigProductWithoutSegment>();

            try
            {
                using (SqlConnection con = new SqlConnection(DBConnection.GetConnectionString()))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_GetDataForSegmentsUpdate", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandTimeout = 0;
                        if (con.State == ConnectionState.Closed)
                        {
                            con.Open();
                        }
                        cmd.Parameters.Add("@SegmentId", SqlDbType.UniqueIdentifier).Value = segmentId;
                        cmd.Parameters.Add("@LicenseeId", SqlDbType.UniqueIdentifier).Value = LicenseeId;
                        SqlDataReader dr = cmd.ExecuteReader();
                        while (dr.Read())
                        {
                            ConfigProductWithoutSegment segment = new ConfigProductWithoutSegment();
                            segment.ProductName = Convert.ToString(dr["ProductName"]);
                            segment.CoverageId = (Guid)dr["CoverageId"];
                            //segment.IsModifiable = true;
                            string ss = Convert.ToString(dr["SegmentId"]);
                            if (Convert.ToString(dr["SegmentId"]) == "")
                            {
                                segment.SegmentId = null;
                                //segment.IsDisabled = false;
                                segment.IsModifiable = true;
                                //segment.toggleSelect = false;
                                segment.Checked = false;
                                segment.LicenseeId = LicenseeId;
                                segment.SegmentId = segmentId;
                            }
                            else
                            {
                                segment.SegmentId = (Guid)dr["SegmentId"];
                                //segment.IsDisabled = true;
                                segment.IsModifiable = false;
                                //segment.toggleSelect = true;
                                segment.Checked = true;
                                segment.LicenseeId = LicenseeId;
                                segment.SegmentId = segmentId;
                            }

                            //segment.SegmentId = (Guid)dr["SegmentId"];

                            //if (segment.SegmentId == null)
                            //{
                            //    segment.IsSelected = false;
                            //}
                            //else
                            //{
                            //    segment.IsSelected = true;//disabled also
                            //}

                            lstsegments.Add(segment);
                        }
                        dr.Close();
                        con.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                ActionLogger.Logger.WriteLog("GetProductsSegmentsForUpdate exception:" + ex.Message, true);
                throw ex;
            }
            return lstsegments;
        }
    }

    [DataContract]
    public class Segment
    {
        #region "Public Properties"
        [DataMember]
        public Guid SegmentId { get; set; }
        [DataMember]
        public string SegmentName { get; set; }
        [DataMember]
        public string CoverageId { get; set; }
        [DataMember]
        public Guid LicenseeId { get; set; }
        #endregion

        public ReturnStatus AddUpdateDelete(Segment segmentObject, Operation operationType, Guid LicenseeId)
        {
            ReturnStatus status = null;
            try
            {
                //DLinq.CommissionDepartmentEntities ctx = new DLinq.CommissionDepartmentEntities(); //create your entity object here
                //EntityConnection ec = (EntityConnection)ctx.Connection;
                //SqlConnection con = (SqlConnection)ec.StoreConnection; //get the SQLConnection that your entity object would use
                //string conStr = con.ConnectionString;
                SqlCommand cmd;

                using (SqlConnection con = new SqlConnection(DBConnection.GetConnectionString()))
                {
                    cmd = new SqlCommand();
                    if (operationType == Operation.Add)
                    {
                        cmd.Connection = con;
                        cmd.CommandText = "sp_InsertSegment";
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;

                        if (con.State == ConnectionState.Closed)
                        {
                            con.Open();
                        }
                        //cmd.Parameters.Add("@SegmentId", SqlDbType.UniqueIdentifier).Value = segmentObject.SegmentId;
                        cmd.Parameters.Add("@SegmentName", SqlDbType.VarChar, 100).Value = segmentObject.SegmentName;
                        cmd.Parameters.Add("@CoverageId", SqlDbType.VarChar, -1).Value = segmentObject.CoverageId;
                        cmd.Parameters.Add("@LicenseeId", SqlDbType.UniqueIdentifier).Value = LicenseeId;
                        cmd.Parameters.Add("@returnMessage", SqlDbType.VarChar, 100);
                        cmd.Parameters["@returnMessage"].Direction = ParameterDirection.Output;
                        cmd.ExecuteNonQuery();
                        String retunvalue = Convert.ToString(cmd.Parameters["@returnMessage"].Value);
                        if (con.State == ConnectionState.Open)
                            con.Close();
                        if (retunvalue == "There is error while inserting segment" || retunvalue == "Segment Name already exist")
                        {
                            status = new ReturnStatus { ErrorMessage = retunvalue, IsError = true };
                        }
                        else
                        {
                            status = new ReturnStatus { ErrorMessage = retunvalue, IsError = false };
                        }
                    }
                    else if (operationType == Operation.Upadte)
                    {
                        cmd.Connection = con;
                        cmd.CommandText = "sp_UpdateSegment";
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;

                        if (con.State == ConnectionState.Closed)
                        {
                            con.Open();
                        }
                        cmd.Parameters.Add("@SegmentId", SqlDbType.UniqueIdentifier).Value = segmentObject.SegmentId;
                        cmd.Parameters.Add("@SegmentName", SqlDbType.VarChar, 100).Value = segmentObject.SegmentName;
                        cmd.Parameters.Add("@CoverageId", SqlDbType.VarChar, -1).Value = segmentObject.CoverageId;
                        cmd.Parameters.Add("@LicenseeId", SqlDbType.UniqueIdentifier).Value = LicenseeId;
                        cmd.Parameters.Add("@returnMessage", SqlDbType.VarChar, 100);
                        cmd.Parameters["@returnMessage"].Direction = ParameterDirection.Output;
                        cmd.ExecuteNonQuery();
                        string retunvalue = Convert.ToString(cmd.Parameters["@returnMessage"].Value);
                        con.Close();
                        if (retunvalue == "There is error while updating segment" || retunvalue == "Segment Name already exist")
                        {
                            status = new ReturnStatus { ErrorMessage = retunvalue, IsError = true };
                        }
                        else
                        {
                            status = new ReturnStatus { ErrorMessage = retunvalue, IsError = false };
                        }
                    }
                    else if (operationType == Operation.Delete)
                    {
                        cmd.Connection = con;
                        cmd.CommandText = "sp_DeleteSegment";
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;

                        if (con.State == ConnectionState.Closed)
                        {
                            con.Open();
                        }
                        cmd.Parameters.Add("@SegmentId", SqlDbType.UniqueIdentifier).Value = segmentObject.SegmentId;
                        cmd.Parameters.Add("@LicenseeId", SqlDbType.UniqueIdentifier).Value = LicenseeId;

                        cmd.Parameters.Add("@returnMessage", SqlDbType.VarChar, 100);
                        cmd.Parameters["@returnMessage"].Direction = ParameterDirection.Output;
                        cmd.ExecuteNonQuery();
                        string retunvalue = Convert.ToString(cmd.Parameters["@returnMessage"].Value);
                        con.Close();
                        if (retunvalue == "There is error while deleting segment")
                        {
                            status = new ReturnStatus { ErrorMessage = retunvalue, IsError = true };
                        }
                        else
                        {
                            status = new ReturnStatus { ErrorMessage = retunvalue, IsError = false };
                        }
                    }
                    else
                    {
                        status = new ReturnStatus { ErrorMessage = "Undefine segment operation.", IsError = true };
                    }
                }
                //}
            }
            catch (Exception ex)
            {
                status = new ReturnStatus { ErrorMessage = ex.Message, IsError = true };
                ActionLogger.Logger.WriteLog("AddUpdateDelete segment ex: " + ex.Message, true);
            }
            return status;
        }

        /// <summary>
        /// This function is used to validate the segment for system.
        /// </summary>
        /// <returns></returns>
        /// 
        public ReturnStatus chkProductSegmentAss(Segment segmentObject)
        {
            ReturnStatus status = null;
            try
            {
                //DLinq.CommissionDepartmentEntities ctx = new DLinq.CommissionDepartmentEntities(); //create your entity object here
                //EntityConnection ec = (EntityConnection)ctx.Connection;
                //SqlConnection con = (SqlConnection)ec.StoreConnection; //get the SQLConnection that your entity object would use
                //string conStr = con.ConnectionString;
                SqlCommand cmd;

                using (SqlConnection con = new SqlConnection(DBConnection.GetConnectionString()))
                {
                    cmd = new SqlCommand();
                  
                        cmd.Connection = con;
                        cmd.CommandText = "sp_checkProductSegmentAssociation";
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;

                        if (con.State == ConnectionState.Closed)
                            con.Open();                       
                        cmd.Parameters.Add("@SegmentId", SqlDbType.UniqueIdentifier).Value = segmentObject.SegmentId;                        
                        cmd.Parameters.Add("@CoverageId", SqlDbType.UniqueIdentifier).Value = segmentObject.CoverageId.ToGuid();
                        cmd.Parameters.Add("@LicenseeId", SqlDbType.UniqueIdentifier).Value = segmentObject.LicenseeId;
                        cmd.Parameters.Add("@res", SqlDbType.Bit);
                        cmd.Parameters["@res"].Direction = ParameterDirection.Output;
                        cmd.ExecuteNonQuery();
                        Boolean retunvalue = Convert.ToBoolean(cmd.Parameters["@res"].Value);
                        if (con.State == ConnectionState.Open)
                            con.Close();
                        status = new ReturnStatus { ErrorMessage = retunvalue.ToString(), IsError = false };
                   
                }
                
            }
            catch (Exception ex)
            {
                status = new ReturnStatus { ErrorMessage = ex.Message, IsError = true };
                ActionLogger.Logger.WriteLog("chkProductSegmentAss segment ex: " + ex.Message, true);
            }
            return status;
        }

        //private ReturnStatus ValidateSegment(DLinq.CommissionDepartmentEntities DataModel, Operation operation)
        //{
        //    try
        //    {
        //        ReturnStatus retStatus = new ReturnStatus();


        //        if (operation == Operation.Add)
        //        {
        //            //DLinq.Payor payor = null;

        //            //if (this.PayorName != null || this.NickName != null)
        //            //{
        //            //    payor = DataModel.Payors.FirstOrDefault(s => s.PayorName.ToUpper() == this.PayorName.ToUpper() && s.NickName.ToUpper() == this.NickName.ToUpper() && s.IsGlobal == true && s.IsDeleted == false);
        //            //    if (payor != null)
        //            //    {
        //            //        retStatus.IsError = true;
        //            //        retStatus.ErrorMessage = "Entered payor name and nickname already exists. Please try with different name.";
        //            //        return retStatus;
        //            //    }

        //            //}

        //            //if (this.PayorLicensee == Guid.Empty) //Check Global payor which is available in all lincense 
        //            //    payor = DataModel.Payors.FirstOrDefault(s => s.PayorName.ToUpper() == this.PayorName.ToUpper() && s.IsGlobal == true && s.IsDeleted == false);
        //            //else //Check Local payor which is available in lincense 
        //            //    payor = DataModel.Payors.FirstOrDefault(s => s.PayorName.ToUpper() == this.PayorName.ToUpper() && s.IsDeleted == false && (s.IsGlobal || s.LicenseeId == this.PayorLicensee));


        //            //if (payor != null)
        //            //{
        //            //    retStatus.IsError = true;
        //            //    retStatus.ErrorMessage = "Entered payor name already exists. Please try with different name.";
        //            //}
        //            //else
        //            //{
        //            //    if (this.PayorLicensee == Guid.Empty) //Check Global payor which is available in all lincense 
        //            //        payor = DataModel.Payors.FirstOrDefault(s => s.NickName == this.NickName && s.IsGlobal == true && s.IsDeleted == false);
        //            //    else //Check Local payor which is available in lincense 
        //            //        payor = DataModel.Payors.FirstOrDefault(s => s.NickName.ToUpper() == this.NickName.ToUpper() && s.IsDeleted == false && (s.IsGlobal || s.LicenseeId == this.PayorLicensee));
        //            //    if (payor != null)
        //            //    {
        //            //        retStatus.IsError = true;
        //            //        retStatus.ErrorMessage = "Entered nickname already exists. Please try with different name.";
        //            //    }
        //            //}
        //        }
        //        //else if (operation == Operation.Upadte)
        //        //{
        //        //    int count = 0;
        //        //    if (this.PayorLicensee == Guid.Empty) //Check Global payor which is available in all lincense 
        //        //        count = count = DataModel.Payors.Where(s => s.NickName.ToUpper() == this.NickName.ToUpper() && s.PayorId != this.PayorID && s.IsGlobal == true && s.IsDeleted == false).ToList().Count;
        //        //    else //Check Local payor which is available in lincense 
        //        //        count = DataModel.Payors.Where(s => s.NickName.ToUpper() == this.NickName.ToUpper() && s.PayorId != this.PayorID && s.IsDeleted == false && (s.IsGlobal || s.LicenseeId == this.PayorLicensee)).ToList().Count;
        //        //    if (count > 0)
        //        //    {
        //        //        retStatus.IsError = true;
        //        //        retStatus.ErrorMessage = "Payor with this nickname already exists. Please enter another nickname";
        //        //    }
        //        //    //count = DataModel.Payors.Where(s => s.PayorName == this.PayorName && s.IsDeleted == false).ToList().Count;
        //        //    if (this.PayorLicensee == Guid.Empty) //Check Global payor which is available in all lincense 
        //        //        count = count = DataModel.Payors.Where(s => s.PayorName.ToUpper() == this.PayorName.ToUpper() && s.PayorId != this.PayorID && s.IsGlobal == true && s.IsDeleted == false).ToList().Count;
        //        //    else //Check Local payor which is available in lincense 
        //        //        count = DataModel.Payors.Where(s => s.PayorName.ToUpper() == this.PayorName.ToUpper() && s.PayorId != this.PayorID && s.IsDeleted == false && (s.IsGlobal || s.LicenseeId == this.PayorLicensee)).ToList().Count;
        //        //    if (count > 0)
        //        //    {
        //        //        retStatus.IsError = true;
        //        //        retStatus.ErrorMessage = "Payor with this name already exists. Please enter another name";
        //        //    }
        //        //}
        //        //else if (operation == Operation.Delete)
        //        //{
        //        //    DLinq.Policy policy = (from po in DataModel.Policies
        //        //                           where (po.PayorId == this.PayorID
        //        //                           && po.IsDeleted == false)
        //        //                           select po).FirstOrDefault();
        //        //    if (policy != null)
        //        //    {
        //        //        retStatus.IsError = true;
        //        //        retStatus.ErrorMessage = "Payor cannot be deleted as it has associated policies.";
        //        //    }

        //        //    DLinq.PayorTool payorTool = (from po in DataModel.PayorTools
        //        //                                 where (po.PayorId == this.PayorID
        //        //                                 && po.IsDeleted == false)
        //        //                                 select po).FirstOrDefault();

        //        //    if (payorTool != null)
        //        //    {
        //        //        retStatus.IsError = true;
        //        //        retStatus.ErrorMessage = "Payor cannot be deleted without deleting its payor tool settings.";
        //        //    }
        //        //}

        //        return retStatus;
        //    }
        //    catch (Exception ex)
        //    {
        //        ActionLogger.Logger.WriteLog("ValidatePayor ex: " + ex.Message, true);
        //        throw ex;
        //    }
        //}

    }
}