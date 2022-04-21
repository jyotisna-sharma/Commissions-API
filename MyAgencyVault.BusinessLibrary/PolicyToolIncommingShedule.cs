using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using MyAgencyVault.BusinessLibrary.Base;
using DLinq = DataAccessLayer.LinqtoEntity;
using System.Data.SqlClient;

namespace MyAgencyVault.BusinessLibrary
{
    [DataContract]
    public class PolicyToolIncommingShedule : PayorIncomingSchedule, IEditable<PolicyToolIncommingShedule>
    {

        #region "data members aka - public properties"
        [DataMember]
        public Guid IncomingScheduleId { get; set; }
        [DataMember]
        public Guid PolicyId { get; set; }
        // [DataMember]
        //public double? FirstYearPercentage { get; set; }
        //[DataMember]
        //public double? RenewalPercentage { get; set; }
        ////[DataMember]
        ////public double? SplitPercentage { get; set; }
        //[DataMember]
        //public int ScheduleTypeId { get; set; }

        //[DataMember]
        //public List<Graded> GradedSchedule { get; set; }

        //CustomMode _mode = CustomMode.Graded;
        //[DataMember]
        //public CustomMode CustomType
        //{
        //    get { return _mode; }
        //    set { _mode = value; }
        //}
        //[DataMember]
        //public Mode Mode { get; set; }


        //[DataMember]
        //public List<NonGraded> NonGradedSchedule { get; set; }


        //[DataMember]
        //public bool? IsIncomingBasicSchedule { get; set; }
        //[DataMember]

        //public double? SplitPercentage { get; set; }//Recently added
        //[DataMember]
        //public Int32? Advance { get; set; }//Recently 27/08/2013 added       
        #endregion
        #region IEditable<IncomingSchedule> Members

        public void AddUpdate(PolicyScheduleDetails policyScheduleDetails)
        {
            using (DLinq.CommissionDepartmentEntities DataModel = Entity.DataModel)
            {

                DLinq.PolicyIncomingSchedule PolicyIncomingScheduleDetails = (from e in DataModel.PolicyIncomingSchedules
                                                                              where e.IncomingScheduleId == policyScheduleDetails.policyIncomingSchedule.IncomingScheduleId
                                                                              select e).FirstOrDefault();

                if (PolicyIncomingScheduleDetails == null)
                {
                    PolicyIncomingScheduleDetails = new DLinq.PolicyIncomingSchedule
                    {
                        FirstYearPercentage = policyScheduleDetails.policyIncomingSchedule.FirstYearPercentage,
                        RenewalPercentage = policyScheduleDetails.policyIncomingSchedule.RenewalPercentage,
                        IncomingScheduleId = policyScheduleDetails.policyIncomingSchedule.IncomingScheduleId
                    };
                    PolicyIncomingScheduleDetails.PolicyReference.Value = (from inc in DataModel.Policies where inc.PolicyId == policyScheduleDetails.policyIncomingSchedule.PolicyId select inc).FirstOrDefault();
                    PolicyIncomingScheduleDetails.MasterBasicIncomingScheduleReference.Value = (from inc in DataModel.MasterBasicIncomingSchedules where inc.ScheduleId == policyScheduleDetails.policyIncomingSchedule.ScheduleTypeId select inc).FirstOrDefault();
                    DataModel.AddToPolicyIncomingSchedules(PolicyIncomingScheduleDetails);


                }
                else
                {
                    PolicyIncomingScheduleDetails.FirstYearPercentage = policyScheduleDetails.policyIncomingSchedule.FirstYearPercentage;
                    PolicyIncomingScheduleDetails.RenewalPercentage = policyScheduleDetails.policyIncomingSchedule.RenewalPercentage;
                    PolicyIncomingScheduleDetails.MasterBasicIncomingScheduleReference.Value = (from inc in DataModel.MasterBasicIncomingSchedules where inc.ScheduleId == policyScheduleDetails.policyIncomingSchedule.ScheduleTypeId select inc).FirstOrDefault();
                }

                DataModel.SaveChanges();
            }
        }

        public void AddUpdate()
        {
            using (DLinq.CommissionDepartmentEntities DataModel = Entity.DataModel)
            {

                DLinq.PolicyIncomingSchedule PolicyIncomingScheduleDetails = (from e in DataModel.PolicyIncomingSchedules
                                                                              where e.IncomingScheduleId == this.IncomingScheduleId
                                                                              select e).FirstOrDefault();

                if (PolicyIncomingScheduleDetails == null)
                {
                    PolicyIncomingScheduleDetails = new DLinq.PolicyIncomingSchedule
                    {
                        FirstYearPercentage = this.FirstYearPercentage,
                        RenewalPercentage = this.RenewalPercentage,
                        IncomingScheduleId = this.IncomingScheduleId
                    };
                    PolicyIncomingScheduleDetails.PolicyReference.Value = (from inc in DataModel.Policies where inc.PolicyId == this.PolicyId select inc).FirstOrDefault();
                    PolicyIncomingScheduleDetails.MasterBasicIncomingScheduleReference.Value = (from inc in DataModel.MasterBasicIncomingSchedules where inc.ScheduleId == this.ScheduleTypeId select inc).FirstOrDefault();
                    DataModel.AddToPolicyIncomingSchedules(PolicyIncomingScheduleDetails);


                }
                else
                {
                    PolicyIncomingScheduleDetails.FirstYearPercentage = this.FirstYearPercentage;
                    PolicyIncomingScheduleDetails.RenewalPercentage = this.RenewalPercentage;
                    PolicyIncomingScheduleDetails.MasterBasicIncomingScheduleReference.Value = (from inc in DataModel.MasterBasicIncomingSchedules where inc.ScheduleId == this.ScheduleTypeId select inc).FirstOrDefault();
                }

                DataModel.SaveChanges();
            }
        }
        public void Delete()
        {
            using (DLinq.CommissionDepartmentEntities DataModel = Entity.DataModel)
            {
                DLinq.PolicyIncomingSchedule _PInc = (from n in DataModel.PolicyIncomingSchedules
                                                      where (n.IncomingScheduleId == this.IncomingScheduleId)
                                                      select n).FirstOrDefault();
                DataModel.DeleteObject(_PInc);
                DataModel.SaveChanges();
            }

        }
        public static void DeleteSchedule(Guid PolicyId)
        {
            try
            {
                PolicyToolIncommingShedule _PolicyIncomingSchedule = GetPolicyToolIncommingSheduleListPolicyWise(PolicyId);
                if (_PolicyIncomingSchedule == null) return;
                using (DLinq.CommissionDepartmentEntities DataModel = Entity.DataModel)
                {
                    DLinq.PolicyIncomingSchedule _PInc = (from n in DataModel.PolicyIncomingSchedules
                                                          where (n.IncomingScheduleId == _PolicyIncomingSchedule.IncomingScheduleId)
                                                          select n).FirstOrDefault();
                    if (_PInc == null) return;
                    DataModel.DeleteObject(_PInc);
                    DataModel.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                ActionLogger.Logger.WriteLog("Exception in delete schedule : " + ex.Message, true);
            }
        }

        public PolicyToolIncommingShedule GetOfID()
        {
            throw new NotImplementedException();
        }

        public static List<PolicyToolIncommingShedule> GetPolicyToolIncommingSheduleList(Guid? PolicyId)
        {
            using (DLinq.CommissionDepartmentEntities DataModel = Entity.DataModel)
            {

                List<PolicyToolIncommingShedule> _PolicyToolIncommingShedule;
                _PolicyToolIncommingShedule = (from hd in DataModel.PolicyIncomingSchedules
                                               join policies in DataModel.Policies on hd.PolicyId equals policies.PolicyId
                                               where hd.PolicyId == PolicyId
                                               select new PolicyToolIncommingShedule
                                               {
                                                   FirstYearPercentage = hd.FirstYearPercentage,
                                                   RenewalPercentage = hd.RenewalPercentage,
                                                   PolicyId = hd.Policy.PolicyId,
                                                   ScheduleTypeId = hd.MasterBasicIncomingSchedule.ScheduleId,
                                                   IncomingScheduleId = hd.IncomingScheduleId,
                                                   //SplitPercentage = policies.SplitPercentage,
                                                   //Advance = policies.Advance,
                                                   //IsIncomingBasicSchedule = policies.IsIncomingBasicSchedule
                                               }).ToList();
                return _PolicyToolIncommingShedule;
            }
        }
        public static List<PolicyToolIncommingShedule> GetPolicyToolIncommingSheduleList()
        {
            using (DLinq.CommissionDepartmentEntities DataModel = Entity.DataModel)
            {
                List<PolicyToolIncommingShedule> _PolicyToolIncommingShedule;
                _PolicyToolIncommingShedule = (from hd in DataModel.PolicyIncomingSchedules
                                               select new PolicyToolIncommingShedule
                                               {
                                                   FirstYearPercentage = hd.FirstYearPercentage,
                                                   RenewalPercentage = hd.RenewalPercentage,
                                                   PolicyId = hd.Policy.PolicyId,
                                                   ScheduleTypeId = hd.MasterBasicIncomingSchedule.ScheduleId,
                                                   IncomingScheduleId = hd.IncomingScheduleId,
                                               }).ToList();
                return _PolicyToolIncommingShedule;
            }
        }

        public static PolicyToolIncommingShedule GetPolicyToolIncommingSheduleListPolicyWise(Guid PolicyId)
        {
            PolicyToolIncommingShedule PolicyToolIncommingSheduleLst = GetPolicyToolIncommingSheduleList(PolicyId).FirstOrDefault();
            return PolicyToolIncommingSheduleLst;
        }
        public bool IsValid()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Created By:Ankit Khandelwal
        /// CreatedOn:11-19-2019
        /// Purpose:Save incoming Schedule from Policy module
        /// </summary>
        /// <param name="policyIncomingSchedule"></param>
        /// <returns></returns>
        public static void SavePolicyIncomingSchedule(PolicyToolIncommingShedule policyIncomingSchedule)
        {
            PolicyToolIncommingShedule policyData = new PolicyToolIncommingShedule();
            try
            {
                 DeletePolicySchedule(policyIncomingSchedule.PolicyId);
                ActionLogger.Logger.WriteLog("SavePolicyIncomingSchedule:Processing begins with IncomingScheduleId" + policyIncomingSchedule.IncomingScheduleID, true);

                if (policyIncomingSchedule.Mode == Mode.Custom) 
                {
                    policyIncomingSchedule.FirstYearPercentage = 0;
                    policyIncomingSchedule.RenewalPercentage = 0;

                }
                using (SqlConnection con = new SqlConnection(DBConnection.GetConnectionString()))
                {
                    using (SqlCommand cmd = new SqlCommand("usp_SavePolicyIncomingScheule", con))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@IncomingScheduleID", policyIncomingSchedule.IncomingScheduleID);
                        cmd.Parameters.AddWithValue("@PolicyId", policyIncomingSchedule.PolicyId);
                        cmd.Parameters.AddWithValue("@FirstYearPercentage", policyIncomingSchedule.FirstYearPercentage);
                        cmd.Parameters.AddWithValue("@RenewalPercentage", policyIncomingSchedule.RenewalPercentage);
                        cmd.Parameters.AddWithValue("@ScheduleTypeId", policyIncomingSchedule.ScheduleTypeId);
                        cmd.Parameters.AddWithValue("@Mode", policyIncomingSchedule.Mode);
                        cmd.Parameters.AddWithValue("@CustomType", policyIncomingSchedule.CustomType);
                        con.Open();
                        cmd.ExecuteNonQuery();
                    }
                    con.Close();
                }
                if (policyIncomingSchedule.Mode == Mode.Custom)
                {
                    if (policyIncomingSchedule.CustomType == CustomMode.Graded)
                    {
                        SaveGradedSchedule(policyIncomingSchedule, policyIncomingSchedule.PolicyId);
                    }
                    else
                    {
                        SaveNonGradedSchedule(policyIncomingSchedule, policyIncomingSchedule.PolicyId);
                    }
                }
            }
            catch (Exception ex)
            {
                ActionLogger.Logger.WriteLog("SavePolicyIncomingSchedule:Exception occurs while processing with IncomingScheduleId" + policyIncomingSchedule.IncomingScheduleID + " " + ex.Message, true);
                throw ex;
            }
        }

        /// <summary>
        /// Created By:Ankit Khandelwal
        /// CreatedOn:11-19-2019
        /// Purpose:Save incoming Schedule from Policy module
        /// </summary>
        /// <param name="policyIncomingSchedule"></param>
        /// <returns></returns>
        public static PolicyToolIncommingShedule GettingPolicyIncomingSchedule(Guid policyId)
        {
            PolicyToolIncommingShedule policyData = new PolicyToolIncommingShedule();
            try
            {
                ActionLogger.Logger.WriteLog("GettingPolicyIncomingSchedule:Processing begins with policyId" + policyId, true);
                using (SqlConnection con = new SqlConnection(DBConnection.GetConnectionString()))
                {
                    using (SqlCommand cmd = new SqlCommand("Usp_gettingincomingschedule", con))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@policyId", policyId);
                        con.Open();
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            policyData.IncomingScheduleID = (Guid)reader["IncomingScheduleId"];
                            policyData.PolicyId = (Guid)reader["PolicyId"];
                            policyData.FirstYearPercentage = reader.IsDBNull("FirstYearPercentage") ? 0.00 : Convert.ToDouble(reader["FirstYearPercentage"]);
                            policyData.RenewalPercentage = reader.IsDBNull("RenewalPercentage") ? 0.00 : Convert.ToDouble(reader["RenewalPercentage"]);
                            policyData.ScheduleTypeId = reader.IsDBNull("ScheduleTypeId") ? 1 : (int)(reader["ScheduleTypeId"]);
                            string mod = Convert.ToString(reader["Mode"]);
                            int scheduleMode = 0;
                            int.TryParse(mod, out scheduleMode);
                            policyData.Mode = (Mode)scheduleMode;

                            string strType = Convert.ToString(reader["CustomType"]);
                            int intType = 0;
                            int.TryParse(strType, out intType);
                            policyData.CustomType = (CustomMode)intType;
                           

                        }
                    }
                    con.Close();
                }
                if (policyData.Mode == Mode.Custom)
                {
                    if (policyData.CustomType == CustomMode.Graded)
                    {
                        policyData.GradedSchedule= GradedScheduleList(policyData.IncomingScheduleID,policyData.PolicyId);
                    }
                    else
                    {
                        policyData.NonGradedSchedule = NonGradedScheduleList(policyData.IncomingScheduleID, policyData.PolicyId);
                    }
                }
            }
            catch (Exception ex)
            {
                ActionLogger.Logger.WriteLog("GettingPolicyIncomingSchedule:Exception occurs while processing with policyId" + policyId + " " + ex.Message, true);
                throw ex;
            }
            return policyData;
        }

        #endregion       

    }

}
