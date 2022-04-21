using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyAgencyVault.BusinessLibrary.Base;
using System.Runtime.Serialization;
using DLinq = DataAccessLayer.LinqtoEntity;


namespace MyAgencyVault.BusinessLibrary
{
    public class OutGoingPayment
    {
        #region "Data members aka - public properties"
        [DataMember]
        public Guid OutgoingScheduleId { get; set; }
        [DataMember]
        public Guid PolicyId { get; set; }
        [DataMember]
        public string Payor { get; set; }
        [DataMember]
        public Guid PayeeUserCredentialId { get; set; }
        [DataMember]
        public double? FirstYearPercentage { get; set; }
        [DataMember]
        public double? RenewalPercentage { get; set; }
        [DataMember]
        public bool IsPrimaryAgent { get; set; }

        [DataMember]
        public bool IsEditDisable { get; set; }


        [DataMember]
        public int ScheduleTypeId { get; set; }
        [DataMember]
        public DateTime? CreatedOn { get; set; }

        //Acme- new parameters for  custom split feature 
        //[DataMember]
        //public DateTime? CustomStartDate { get; set; }
        //[DataMember]
        //public DateTime? CustomEndDate { get; set; }
        //[DataMember]
        //public string CustomDateType { get; set; }
        [DataMember]
        public double? SplitPercent { get; set; }

        [DataMember]
        public double? splitpercentage { get; set; }

        DateTime? _customStartDate;
        [DataMember]
        public DateTime? CustomStartDate
        {
            get
            {
                return _customStartDate;
            }
            set
            {
                _customStartDate = value;
                if (value != null && string.IsNullOrEmpty(CustomStartDateString))
                {
                    CustomStartDateString = value.ToString();
                }
            }
        }
        string _customStartDateString;
        [DataMember]
        public string CustomStartDateString
        {
            get
            {
                return _customStartDateString;
            }
            set
            {
                _customStartDateString = value;
                if (CustomStartDate == null && !string.IsNullOrEmpty(_customStartDateString))
                {
                    DateTime dt;
                    DateTime.TryParse(_customStartDateString, out dt);
                    CustomStartDate = dt;
                }
            }
        }

        //[DataMember]
        //public DateTime? EffectiveToDate { get; set; }
        DateTime? _customEndDate;
        [DataMember]
        public DateTime? CustomEndDate
        {
            get
            {
                return _customEndDate;
            }
            set
            {
                _customEndDate = value;
                if (value != null && string.IsNullOrEmpty(CustomEndDateString))
                {
                    CustomEndDateString = value.ToString();
                }
            }
        }
        string customEndDateString;
        [DataMember]
        public string CustomEndDateString
        {
            get
            {
                return customEndDateString;
            }
            set
            {
                customEndDateString = value;
                if (CustomEndDate == null && !string.IsNullOrEmpty(customEndDateString))
                {
                    DateTime dt;
                    DateTime.TryParse(customEndDateString, out dt);
                    CustomEndDate = dt;
                }
            }
        }

        [DataMember]
        public int? TierNumber { get; set; }
        #endregion

        #region IEditable<OutgoingPayment> Members
        public void AddUpdate()
        {
            throw new NotImplementedException();
        }

        public static void AddUpdate(List<OutGoingPayment> GlobalOutPayment, bool IsCustomSchedule = false, bool IsTierSchedule = false)
        {
            using (DLinq.CommissionDepartmentEntities DataModel = Entity.DataModel)
            {
                DLinq.PolicyOutgoingSchedule gcs = null;

                if (GlobalOutPayment != null)
                {

                    foreach (OutGoingPayment _globalCoveragesShedule in GlobalOutPayment)
                    {
                        if (_globalCoveragesShedule.splitpercentage != null || _globalCoveragesShedule.splitpercentage != 0.00)
                        {
                            _globalCoveragesShedule.SplitPercent = _globalCoveragesShedule.splitpercentage;
                        }
                        gcs = (from e in DataModel.PolicyOutgoingSchedules
                               where e.OutgoingScheduleId == _globalCoveragesShedule.OutgoingScheduleId
                               select e).FirstOrDefault();
                        if (gcs == null)
                        {
                            gcs = new DLinq.PolicyOutgoingSchedule
                            {
                                OutgoingScheduleId = _globalCoveragesShedule.OutgoingScheduleId,
                                FirstYearPercentage = _globalCoveragesShedule.FirstYearPercentage,
                                RenewalPercentage = _globalCoveragesShedule.RenewalPercentage,
                                //      IsPrimaryAgent = _globalCoveragesShedule.IsPrimaryAgent,
                                ScheduleTypeId = _globalCoveragesShedule.ScheduleTypeId,
                                CreatedOn = DateTime.Today,
                                CustomStartDate = _globalCoveragesShedule.CustomStartDate,
                                CustomEndDate = _globalCoveragesShedule.CustomEndDate,
                                SplitPercent = _globalCoveragesShedule.SplitPercent,
                                TierNumber = IsTierSchedule == true ? _globalCoveragesShedule.TierNumber : null

                            };

                            gcs.PolicyReference.Value = (from f in DataModel.Policies where f.PolicyId == _globalCoveragesShedule.PolicyId select f).FirstOrDefault();
                            //gcs.UserDetailReference.Value = _objPayee;
                            gcs.UserCredentialReference.Value = (from f in DataModel.UserCredentials where f.UserCredentialId == _globalCoveragesShedule.PayeeUserCredentialId select f).FirstOrDefault();
                            DataModel.AddToPolicyOutgoingSchedules(gcs);

                        }
                        else
                        {
                            gcs.FirstYearPercentage = (IsCustomSchedule) ? 0 : _globalCoveragesShedule.FirstYearPercentage;
                            gcs.RenewalPercentage = (IsCustomSchedule) ? 0 : _globalCoveragesShedule.RenewalPercentage;
                            // gcs.IsPrimaryAgent = _globalCoveragesShedule.IsPrimaryAgent;
                            gcs.ScheduleTypeId = _globalCoveragesShedule.ScheduleTypeId;
                            gcs.CustomStartDate = (IsCustomSchedule) ? _globalCoveragesShedule.CustomStartDate : null;
                            gcs.CustomEndDate = (IsCustomSchedule) ? _globalCoveragesShedule.CustomEndDate : null;
                            gcs.SplitPercent = (IsCustomSchedule) ? _globalCoveragesShedule.SplitPercent : 0;
                            gcs.TierNumber = IsTierSchedule == true ? _globalCoveragesShedule.TierNumber : null;
                        }
                        DataModel.SaveChanges();
                    }
                }
            }
        }

        public void Delete()
        {
            ActionLogger.Logger.WriteLog(DateTime.Now.ToString() + " Delete started in outgoingPayment.cs - OutgoingScheduleId: " + this.OutgoingScheduleId, true);
            using (DLinq.CommissionDepartmentEntities DataModel = Entity.DataModel)
            {
                DLinq.PolicyOutgoingSchedule _policyOutgoingSchedule = (from n in DataModel.PolicyOutgoingSchedules
                                                                        where (n.OutgoingScheduleId == this.OutgoingScheduleId)
                                                                        select n).FirstOrDefault();

                if (_policyOutgoingSchedule != null)
                {
                    DataModel.DeleteObject(_policyOutgoingSchedule);
                    DataModel.SaveChanges();
                }
            }
            ActionLogger.Logger.WriteLog(DateTime.Now.ToString() + " Delete ended in outgoingPayment.cs - OutgoingScheduleId: " + this.OutgoingScheduleId, true);
        }

        #endregion

        public static List<OutGoingPayment> GetOutgoingShedule()
        {
            using (DLinq.CommissionDepartmentEntities DataModel = Entity.DataModel)
            {
                return (from gc in DataModel.PolicyOutgoingSchedules
                        select new OutGoingPayment
                        {
                            RenewalPercentage = gc.RenewalPercentage,
                            FirstYearPercentage = gc.FirstYearPercentage,
                            //   IsPrimaryAgent = gc.IsPrimaryAgent,
                            OutgoingScheduleId = gc.OutgoingScheduleId,
                            PolicyId = gc.Policy.PolicyId,
                            PayeeUserCredentialId = gc.UserCredential.UserCredentialId,
                            ScheduleTypeId = gc.ScheduleTypeId.Value,
                            CreatedOn = gc.CreatedOn,
                            Payor = gc.UserCredential.UserDetail.NickName,
                        }).ToList();
            }
        }

        public static int getIncomingScheduleTypeId(Guid PolicyId)
        {
            ActionLogger.Logger.WriteLog(DateTime.Now.ToString() + " getIncomingScheduleTypeId start", true);

            var incomingScheduleTypeId = 1;
            try
            {
                using (DLinq.CommissionDepartmentEntities DataModel = Entity.DataModel)
                {
                    incomingScheduleTypeId = (int)DataModel.PolicyIncomingSchedules.Where(a => a.PolicyId == PolicyId).FirstOrDefault().ScheduleTypeId;
                }
                ActionLogger.Logger.WriteLog(DateTime.Now.ToString() + " getIncomingScheduleTypeId incomingScheduleTypeId " + incomingScheduleTypeId, true);
            }
            catch (Exception ex)
            {
                ActionLogger.Logger.WriteLog(DateTime.Now.ToString() + " Exception getIncomingScheduleTypeId " + ex.Message, true);
            }
            return incomingScheduleTypeId;
        }

        /// <summary>
        /// Gets schedule based on invoice/entered date
        ///Fetch only those schedules where invoice/entered date falls in one of the date ranges 
        /// </summary>
        /// 
        /// <param name="PolicyId"></param>
        /// <param name="DateType"></param>
        /// <param name="Invoice"></param>
        /// <param name="Entered"></param>
        /// <returns></returns>
        public static List<OutGoingPayment> GetCustomScheduleForPolicy(Guid PolicyId, string DateType, DateTime? Invoice, DateTime? Entered)
        {
            ActionLogger.Logger.WriteLog("GetCustomScheduleForPolicy: start processing,PolicyId: " + PolicyId + " returning :", true);
            if (!string.IsNullOrEmpty(DateType))
            {
                using (DLinq.CommissionDepartmentEntities DataModel = Entity.DataModel)
                {
                    ActionLogger.Logger.WriteLog("GetCustomScheduleForPolicy: DateType is null,PolicyId: " + PolicyId + " returning :" + "DateType:" + DateType, true);
                    if (DateType.ToLower() == "invoice")
                    {
                        ActionLogger.Logger.WriteLog("GetCustomScheduleForPolicy: invoice ,PolicyId: " + PolicyId + " returning :", true);
                        return (from gc in DataModel.PolicyOutgoingSchedules
                                where (gc.PolicyId == PolicyId &&
                                           ((Invoice >= gc.CustomStartDate && Invoice <= gc.CustomEndDate) ||
                                             (Invoice >= gc.CustomStartDate && gc.CustomEndDate == null)
                                            )
                                  )
                                select new OutGoingPayment
                                {
                                    RenewalPercentage = gc.RenewalPercentage,
                                    FirstYearPercentage = gc.FirstYearPercentage,
                                    //    IsPrimaryAgent = gc.IsPrimaryAgent,
                                    OutgoingScheduleId = gc.OutgoingScheduleId,
                                    PolicyId = gc.Policy.PolicyId,
                                    PayeeUserCredentialId = gc.UserCredential.UserCredentialId,
                                    ScheduleTypeId = gc.ScheduleTypeId.Value,
                                    CreatedOn = gc.CreatedOn,
                                    Payor = gc.UserCredential.UserDetail.NickName,
                                    CustomStartDate = gc.CustomStartDate,
                                    CustomEndDate = gc.CustomEndDate,
                                    SplitPercent = (gc.SplitPercent == null) ? 0 : (double)(gc.SplitPercent),
                                    TierNumber = gc.TierNumber
                                }).ToList();
                    }
                    else
                    {
                        ActionLogger.Logger.WriteLog("GetCustomScheduleForPolicy:  no datetype is invoice ,PolicyId: " + PolicyId + " returning :", true);
                        return (from gc in DataModel.PolicyOutgoingSchedules
                                where (gc.PolicyId == PolicyId &&
                                           ((Entered >= gc.CustomStartDate && Entered <= gc.CustomEndDate) ||
                                             (Entered >= gc.CustomStartDate && gc.CustomEndDate == null)
                                            ))
                                select new OutGoingPayment
                                {
                                    RenewalPercentage = gc.RenewalPercentage,
                                    FirstYearPercentage = gc.FirstYearPercentage,
                                    // IsPrimaryAgent = gc.IsPrimaryAgent,
                                    OutgoingScheduleId = gc.OutgoingScheduleId,
                                    PolicyId = gc.Policy.PolicyId,
                                    PayeeUserCredentialId = gc.UserCredential.UserCredentialId,
                                    ScheduleTypeId = gc.ScheduleTypeId.Value,
                                    CreatedOn = gc.CreatedOn,
                                    Payor = gc.UserCredential.UserDetail.NickName,
                                    CustomStartDate = gc.CustomStartDate,
                                    CustomEndDate = gc.CustomEndDate,
                                    SplitPercent = (gc.SplitPercent == null) ? 0 : (double)(gc.SplitPercent),
                                    TierNumber = gc.TierNumber
                                }).ToList();

                    }



                }
            }
            else
            {
                ActionLogger.Logger.WriteLog("GetCustomScheduleForPolicy: DateType is null,PolicyId: " + PolicyId + " returning :", true);
                return null;
            }

        }
        /// <summary>
        /// Acme - changes to include custom split 
        /// </summary>
        /// <param name="PolicyId"></param>
        /// <returns></returns>
        public static List<OutGoingPayment> GetOutgoingSheduleForPolicy(Guid PolicyId)
        {
            using (DLinq.CommissionDepartmentEntities DataModel = Entity.DataModel)
            {
                return (from gc in DataModel.PolicyOutgoingSchedules
                        where (gc.PolicyId == PolicyId)
                        select new OutGoingPayment
                        {
                            RenewalPercentage = gc.RenewalPercentage,
                            FirstYearPercentage = gc.FirstYearPercentage,
                            //    IsPrimaryAgent = gc.IsPrimaryAgent,
                            OutgoingScheduleId = gc.OutgoingScheduleId,
                            PolicyId = gc.Policy.PolicyId,
                            PayeeUserCredentialId = gc.UserCredential.UserCredentialId,
                            ScheduleTypeId = gc.ScheduleTypeId.Value,
                            CreatedOn = gc.CreatedOn,
                            Payor = gc.UserCredential.UserDetail.NickName,
                            CustomStartDate = gc.CustomStartDate,
                            CustomEndDate = gc.CustomEndDate,
                            SplitPercent = (gc.SplitPercent == null) ? 0 : (double)(gc.SplitPercent),
                            TierNumber = gc.TierNumber
                        }).ToList();
            }
        }

        public void DeleteSchedule(List<OutGoingPayment> DeleteOutPayment)
        {

            using (DLinq.CommissionDepartmentEntities DataModel = Entity.DataModel)
            {
                DLinq.PolicyOutgoingSchedule gcs = null;
                foreach (OutGoingPayment _out in DeleteOutPayment)
                {
                    ActionLogger.Logger.WriteLog(DateTime.Now.ToString() + " DeleteSchedule started in outgoingPayment.cs - OutgoingScheduleId: " + _out.OutgoingScheduleId, true);
                    gcs = (from e in DataModel.PolicyOutgoingSchedules
                           where e.OutgoingScheduleId == _out.OutgoingScheduleId
                           select e).FirstOrDefault();
                    if (gcs != null)
                    {
                        DataModel.DeleteObject(gcs);
                        DataModel.SaveChanges();
                    }
                    ActionLogger.Logger.WriteLog(DateTime.Now.ToString() + " DeleteSchedule ended in outgoingPayment.cs - OutgoingScheduleId: " + _out.OutgoingScheduleId, true);
                }

            }
        }

        public static void DeletePolicyOutGoingSchedulebyPolicyId(Guid PolicyId)
        {
            ActionLogger.Logger.WriteLog(DateTime.Now.ToString() + " DeletePolicyOutGoingSchedulebyPolicyId started in outgoingPayment.cs - PolicyId: " + PolicyId, true);
            using (DLinq.CommissionDepartmentEntities DataModel = Entity.DataModel)
            {
                List<DLinq.PolicyOutgoingSchedule> gcs = DataModel.PolicyOutgoingSchedules.Where(p => p.PolicyId == PolicyId).ToList<DLinq.PolicyOutgoingSchedule>();
                foreach (DLinq.PolicyOutgoingSchedule _out in gcs)
                {

                    DataModel.DeleteObject(_out);
                    DataModel.SaveChanges();
                }
            }
            ActionLogger.Logger.WriteLog(DateTime.Now.ToString() + " DeletePolicyOutGoingSchedulebyPolicyId ended in outgoingPayment.cs - PolicyId: " + PolicyId, true);
        }

        public static bool IsUserPresentAsPayee(Guid userId)
        {

            ActionLogger.Logger.WriteLog(DateTime.Now.ToString() + " IsUserPresentAsPayee userId: " + userId, true);
            using (DLinq.CommissionDepartmentEntities DataModel = Entity.DataModel)
            {
                int count = DataModel.PolicyOutgoingSchedules.Where(s => s.PayeeUserCredentialId == userId).Count();
                if (count != 0)
                {
                    ActionLogger.Logger.WriteLog(DateTime.Now.ToString() + " IsUserPresentAsPayee user found present in outgoing schedules: " + userId, true);
                    return true;
                }
                else
                {
                    ActionLogger.Logger.WriteLog(DateTime.Now.ToString() + " IsUserPresentAsPayee user not found present in outgoing schedules, checking payments: " + userId, true);

                    //Added by Jyotisna on Aug 29, 2018 to include check for outgoing payments as per Kevin
                    count = DataModel.PolicyOutgoingPayments.Where(s => s.RecipientUserCredentialId == userId).Count();
                    if (count != 0)
                    {
                        ActionLogger.Logger.WriteLog(DateTime.Now.ToString() + " IsUserPresentAsPayee user found present in outgoing payments: " + userId, true);
                        return true;
                    }
                    else
                    {
                        ActionLogger.Logger.WriteLog(DateTime.Now.ToString() + " IsUserPresentAsPayee user not found present in outgoing payments, can be deleted: " + userId, true);
                        return false;
                    }
                }
            }
        }
    }
}
