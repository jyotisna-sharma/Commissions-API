using System;
using System.Transactions;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyAgencyVault.BusinessLibrary.Base;
using MyAgencyVault.BusinessLibrary.Masters;
using System.Runtime.Serialization;
using DLinq = DataAccessLayer.LinqtoEntity;
using System.Threading;

namespace MyAgencyVault.BusinessLibrary
{
    public class CommissionDashboard
    {
        public static List<PolicyPaymentEntriesPost> GetPolicyPaymentEntry(Guid PolicyId)
        {
            List<PolicyPaymentEntriesPost> _PolicyPaymentEntriesPost = PolicyPaymentEntriesPost.GetPolicyPaymentEntryPolicyIDWise(PolicyId);
            return _PolicyPaymentEntriesPost;
        }

        public static List<PolicyOutgoingDistribution> GetPolicyOutgoingPayment(Guid PolicyPaymentEntryId)
        {
            List<PolicyOutgoingDistribution> _PolicyOutgoingDistribution = PolicyOutgoingDistribution.GetOutgoingPaymentByPoicyPaymentEntryId(PolicyPaymentEntryId);
            return _PolicyOutgoingDistribution;
        }

        public static List<DisplayFollowupIssue> GetPolicyCommissionIssues(Guid PolicyId)
        {
            List<DisplayFollowupIssue> _FollowupIssue = FollowupIssue.GetIssues(PolicyId);
            return _FollowupIssue;
        }

        public static PolicyPaymentEntriesPost GetPolicyPaymentPaymentEntryEntryIdWise(Guid PolicyEntryid)
        {
            return PolicyPaymentEntriesPost.GetPolicyPaymentEntry(PolicyEntryid);
        }

        public static PostProcessReturnStatus CommissionDashBoardPostStartClienVMWrapper(PolicyDetailsData SelectedPolicy, PolicyPaymentEntriesPost PaymentEntry, PostEntryProcess _PostEntryProcess, UserRole _UserRole, bool isInvoiceEdited = false)
        {
            //PostProcessReturnStatus status = new PostProcessReturnStatus();
            #region Process
            try
            {
               
                if (_PostEntryProcess == PostEntryProcess.FirstPost)
                {
                    Batch batch = Policy.GenerateBatch(SelectedPolicy);
                    return CommissionDashBoardPostStart(batch.BatchId, PaymentEntry, _PostEntryProcess, _UserRole, isInvoiceEdited);
                }
                else
                    return CommissionDashBoardPostStart(Guid.Empty, PaymentEntry, _PostEntryProcess, _UserRole, isInvoiceEdited);
            }
            //catch(Exception ex)
            //{

            //}
            //return status;
            finally
            {
                PolicyLocking.UnlockPolicy(SelectedPolicy.PolicyId);
            }

            #endregion
        }

        //public static PostProcessReturnStatus CommissionDashBoardPostStart(Guid BatchId, PolicyPaymentEntriesPost PaymentEntry, PostEntryProcess _PostEntryProcess, UserRole _UserRole)
        //{
        //    ActionLogger.Logger.WriteLog("CommissionDashBoardPostStart request BAtchid: " + BatchId + ", Policy - " +PaymentEntry.PolicyID + ", totalPayment: " +PaymentEntry.TotalPayment, true);
        //    ActionLogger.Logger.WriteLog("CommissionDashBoardPostStart request PostEntryProcess: " + _PostEntryProcess,true);
        //    var options = new TransactionOptions
        //    {
        //        IsolationLevel = IsolationLevel.ReadCommitted,
        //        Timeout = TimeSpan.FromMinutes(60)
        //    };

        //    using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required, options))
        //    {
        //        PostProcessReturnStatus _PostProcessReturnStatus = new PostProcessReturnStatus() { DeuEntryId = Guid.Empty, IsComplete = false, ErrorMessage = null, PostEntryStatus = _PostEntryProcess };

        //        if (_PostEntryProcess == PostEntryProcess.FirstPost)
        //        {
        //            PolicyDetailsData _Policy = PostUtill.GetPolicy(PaymentEntry.PolicyID);
        //            Guid PayorId = _Policy.PayorId ?? Guid.Empty;
        //            decimal PaymentRecived = PaymentEntry.TotalPayment;
        //            Guid CreatedBy = PaymentEntry.CreatedBy;
        //            Statement _Statement = Policy.GenerateStatment(BatchId, PayorId, PaymentRecived, CreatedBy);
        //            PaymentEntry.StmtID = _Statement.StatementID;
        //            DEU _DEU = PostUtill.GetDeuCollection(PaymentEntry, _Policy);
        //            DEU objDEU = new DEU();
        //            objDEU.AddupdateDeuEntry(_DEU);
        //            _PostProcessReturnStatus = PostUtill.PostStart(_PostEntryProcess, _DEU.DEUENtryID, Guid.Empty, Guid.Empty, _UserRole, _PostEntryProcess, string.Empty, string.Empty);
        //        }
        //        else if (_PostEntryProcess == PostEntryProcess.RePost)
        //        {
        //            PolicyDetailsData _Policy = PostUtill.GetPolicy(PaymentEntry.PolicyID);
        //            DEU _DEU = PostUtill.GetDeuCollection(PaymentEntry, _Policy);
        //            DEU objDEU = new DEU();
        //            objDEU.AddupdateDeuEntry(_DEU);
        //            _PostProcessReturnStatus = PostUtill.PostStart(_PostEntryProcess, PaymentEntry.DEUEntryId.Value, _DEU.DEUENtryID, Guid.Empty, _UserRole, _PostEntryProcess, string.Empty, string.Empty);

        //        }
        //        if (_PostProcessReturnStatus.IsComplete)
        //        {
        //            ts.Complete();
        //        }
        //        ActionLogger.Logger.WriteLog("CommissionDashBoardPostStart success BAtchid: " + BatchId + ", Policy - " + PaymentEntry.PolicyID + ", totalPayment: " + PaymentEntry.TotalPayment, true);
        //        return _PostProcessReturnStatus;
        //    }
        //}

        /// <summary>
        /// Modified By :Ankit khandelwal
        /// Modified On :09-04-2018
        /// Purpose:For adding a new incoming payment.
        /// </summary>
        /// <param name="BatchId"></param>
        /// <param name="PaymentEntry"></param>
        /// <param name="_PostEntryProcess"></param>
        /// <param name="_UserRole"></param>
        /// <param name="isInvoiceEdited"></param>
        /// <param name="_UserID"></param>
        /// <returns></returns>
        public static PostProcessReturnStatus CommissionDashBoardPostStart(Guid BatchId, PolicyPaymentEntriesPost PaymentEntry, PostEntryProcess _PostEntryProcess, UserRole _UserRole, bool isInvoiceEdited = false, Guid? _UserID = null)
        {
            try
            {
                ActionLogger.Logger.WriteLog("CommissionDashBoardPostStart request BAtchid: " + BatchId + ", Policy - " + PaymentEntry.PolicyID + ", totalPayment: " + PaymentEntry.TotalPayment + ", isInvoiceEdited: " + isInvoiceEdited, true);
                ActionLogger.Logger.WriteLog("CommissionDashBoardPostStart request PostEntryProcess: " + _PostEntryProcess + ", userid: " + _UserID, true);

                Guid UserID = (_UserID == null) ? Guid.Empty : (Guid)_UserID;
                var options = new TransactionOptions
                {
                    IsolationLevel = IsolationLevel.ReadCommitted,
                    Timeout = TimeSpan.FromMinutes(60)
                };

                //using (TransactionScope ts = new TransactionScope(TransactionScopeOption.RequiresNew))
                //{
                    PostProcessReturnStatus _PostProcessReturnStatus = new PostProcessReturnStatus() { DeuEntryId = Guid.Empty, IsComplete = false, ErrorMessage = null, PostEntryStatus = _PostEntryProcess };
                    if (isInvoiceEdited) //Jan 25, 2018: new check added after bug found in the previous method while updating invoice date only. Now, the invoice date to be updated and all else skipped.
                    {
                        PolicyPaymentEntriesPost pe = PolicyPaymentEntriesPost.GetPolicyPaymentEntry(PaymentEntry.PaymentEntryID);
                        PostUtill.newInvoice = PaymentEntry.InvoiceDate;
                        if (pe != null)
                        {
                            PostUtill.oldInvoice = pe.InvoiceDate;
                        }
                        IncomingPament.UpdateInvoiceDate(PaymentEntry.PaymentEntryID, PaymentEntry);
                        PostUtill.SaveInvoiceChangeLog(PaymentEntry.PaymentEntryID, UserID, PaymentEntry.PolicyID);
                        //       ActionLogger.Logger.WriteLog("CommissionDashBoardPostStart successfully updated invoice date , Policy - " + PaymentEntry.PolicyID + ", PaymententryID: " + PaymentEntry.PaymentEntryID + ", totalPayment: " + PaymentEntry.TotalPayment, true);
                        _PostProcessReturnStatus.IsComplete = true;
                        //ts.Complete();
                        //     ActionLogger.Logger.WriteLog("CommissionDashBoardPostStart tx committed", true);

                        return _PostProcessReturnStatus;
                    }
                    else
                    {
                        if (_PostEntryProcess == PostEntryProcess.FirstPost)
                        {
                            PolicyDetailsData _Policy = PostUtill.GetPolicy(PaymentEntry.PolicyID);
                        
                            Guid PayorId = _Policy.PayorId ?? Guid.Empty;
                            decimal PaymentRecived = PaymentEntry.TotalPayment;
                            Guid CreatedBy = PaymentEntry.CreatedBy;
                            Statement _Statement = Policy.GenerateStatment(BatchId, PayorId, PaymentRecived, CreatedBy);
                            PaymentEntry.StmtID = _Statement.StatementID;
                            DEU _DEU = PostUtill.GetDeuCollection(PaymentEntry, _Policy, isInvoiceEdited);
                            DEU objDEU = new DEU();
                            objDEU.AddupdateDeuEntry(_DEU);
                            _PostProcessReturnStatus = PostUtill.PostStart(_PostEntryProcess, _DEU.DEUENtryID, Guid.Empty, UserID, _UserRole, _PostEntryProcess, string.Empty, string.Empty);
                        }
                        else if (_PostEntryProcess == PostEntryProcess.RePost)
                        {
                            PolicyDetailsData _Policy = PostUtill.GetPolicy(PaymentEntry.PolicyID);
                            DEU _DEU = PostUtill.GetDeuCollection(PaymentEntry, _Policy, isInvoiceEdited);
                            DEU objDEU = new DEU();
                            objDEU.AddupdateDeuEntry(_DEU);
                            _PostProcessReturnStatus = PostUtill.PostStart(_PostEntryProcess, PaymentEntry.DEUEntryId.Value, _DEU.DEUENtryID, UserID, _UserRole, _PostEntryProcess, string.Empty, string.Empty);

                        }
                        //if (_PostProcessReturnStatus.IsComplete)
                        //{
                        //    ts.Complete();
                        //}
                        ActionLogger.Logger.WriteLog("CommissionDashBoardPostStart success BAtchid: " + BatchId + ", Policy - " + PaymentEntry.PolicyID + ", totalPayment: " + PaymentEntry.TotalPayment, true);
                        return _PostProcessReturnStatus;
                   // }
                }
            }
            catch(Exception ex)
            {
                ActionLogger.Logger.WriteLog("CommissionDashBoardPostStart success BAtchid: " + BatchId + ", Policy - " + PaymentEntry.PolicyID + ", Exception: " + ex.Message, true);
                throw ex;
            }
            
        }
        public static PostProcessReturnStatus RemoveCommissiondashBoardIncomingPayment(PolicyPaymentEntriesPost PolicySelectedIncomingPaymentCommissionDashBoard, UserRole _UserRole)
        {
            ActionLogger.Logger.WriteLog("RemoveCommissiondashBoardIncomingPayment request DEuEntry: " + PolicySelectedIncomingPaymentCommissionDashBoard.DEUEntryId, true);
            try
            {
                var options = new TransactionOptions
                {
                    IsolationLevel = IsolationLevel.ReadCommitted,
                    Timeout = TimeSpan.FromMinutes(60)
                };
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required, options))
                {
                    PostProcessReturnStatus _PostProcessReturnStatus = PostUtill.PostStart(PostEntryProcess.Delete,
                        PolicySelectedIncomingPaymentCommissionDashBoard.DEUEntryId.Value, Guid.Empty, Guid.Empty, _UserRole, PostEntryProcess.Delete, string.Empty, "Delete");

                    if (_PostProcessReturnStatus.IsComplete)
                    {
                        ts.Complete();
                    }
                    ActionLogger.Logger.WriteLog("RemoveCommissiondashBoardIncomingPayment success DEuEntry: " + PolicySelectedIncomingPaymentCommissionDashBoard.DEUEntryId, true);
                    return _PostProcessReturnStatus;
                }
            }
            finally
            {
                //PolicyLocking.UnlockPolicy(PolicySelectedIncomingPaymentCommissionDashBoard.PolicyID);
            }
        }

        static void UnlinkPaymentPaid(DEU _DEU, string unlinkClientName, Guid PaymentEntryID, UserRole _UserRole)
        {
            try
            {
                ActionLogger.Logger.WriteLog("UnlinkPaymentPaid  paymentEntry: " + PaymentEntryID + ",  unlinkClientName: " + unlinkClientName, true);
                //Get policy to associate with the entry
                DEUFields fields = PostUtill.FillDEUFields(_DEU.DEUENtryID);
                BasicInformationForProcess basicInfo = PostUtill.GetPolicyToProcess(fields, unlinkClientName);
                if (basicInfo != null)
                {
                    PolicyDetailsData newPolicy = Policy.GetPolicyDetailsOnPolicyID(basicInfo.PolicyId); //PostUtill.GetPolicy(basicInfo.PolicyId);
                    PolicyDetailsData oldPolicy = Policy.GetPolicyDetailsOnPolicyID(_DEU.PolicyId);  //PostUtill.GetPolicy(_DEU.PolicyId);
                    ActionLogger.Logger.WriteLog("UnlinkPaymentPaid  newPolicy: " + newPolicy.PolicyId + ",  oldpolicy: " + oldPolicy.PolicyId, true);
                    //update deu entry 

                    //update policypayment entry
                    List<Guid> payments = new List<Guid>();
                    payments.Add(PaymentEntryID);
                    LinkPaymentPolicies.DoLinkPolicy((Guid)newPolicy.PolicyLicenseeId, oldPolicy.PolicyId, (Guid)oldPolicy.ClientId, newPolicy.PolicyId, payments, Guid.Empty, true, true);
                    //manage outgoing schedules
                    //LinkPaymentPolicies.DoLinkPolicy((Guid)newPolicy.PolicyLicenseeId, true, true, _DEU.PolicyId, (Guid)oldPolicy.ClientId, basicInfo.PolicyId, PaymentEntryID,
                    //    Guid.Empty, /*(Guid)oldPolicy.PayorId, (Guid)newPolicy.PayorId,*/ true, true, true, _UserRole);
                }
                ActionLogger.Logger.WriteLog("UnlinkPaymentPaid  success ", true);
            }
            catch (Exception ex)
            {
                ActionLogger.Logger.WriteLog("UnlinkPaymentPaid exception: " + ex.Message, true);
                if (ex.InnerException != null)
                {
                    ActionLogger.Logger.WriteLog("UnlinkPaymentPaid inner exception: " + ex.InnerException.Message, true);
                }
            }
        }

        public static PostProcessReturnStatus UnlinkCommissiondashBoardIncomingPayment(PolicyPaymentEntriesPost PolicySelectedIncomingPaymentCommissionDashBoard, UserRole _UserRole)
        {
            ActionLogger.Logger.WriteLog("UnlinkCommissiondashBoardIncomingPayment request DEuEntry: " + PolicySelectedIncomingPaymentCommissionDashBoard.DEUEntryId, true);

            PostProcessReturnStatus _PostProcessReturnStatus = null;
            try
            {
                var options = new TransactionOptions
                {
                    IsolationLevel = IsolationLevel.ReadCommitted,
                    Timeout = TimeSpan.FromMinutes(60)
                };
                //using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required, options))
                //{
                    string strUnlink = string.Empty;
                    DEU _DEU = DEU.GetDeuEntryidWiseForUnlikingPolicy(PolicySelectedIncomingPaymentCommissionDashBoard.DEUEntryId ?? Guid.Empty);

                    if (_DEU != null)
                    {
                        strUnlink = Convert.ToString(_DEU.UnlinkClientName);
                    }
                    ActionLogger.Logger.WriteLog("UnlinkCommissiondashBoardIncomingPayment strUnlink- " + strUnlink, true);
                    bool flag = PolicyOutgoingDistribution.IfPaymentHasPaidEntry(PolicySelectedIncomingPaymentCommissionDashBoard.PaymentEntryID);

                    if (!flag) //When unpaid outgoing entries 
                    {
                        ActionLogger.Logger.WriteLog("UnlinkCommissiondashBoardIncomingPayment - Entries are unpaid ", true);
                   

                    PostUtill.PostStart(PostEntryProcess.Delete, _DEU.DEUENtryID, Guid.Empty, Guid.Empty, _UserRole, PostEntryProcess.FirstPost, strUnlink, string.Empty,false, true);
 
                        DEU objDEU = new DEU();
                        objDEU.AddupdateUnlinkDeuEntry(_DEU, strUnlink);

                        _PostProcessReturnStatus = PostUtill.PostStart(PostEntryProcess.FirstPost, _DEU.DEUENtryID, Guid.Empty, Guid.Empty, _UserRole, PostEntryProcess.FirstPost, strUnlink, string.Empty);
                    }
                    else
                    {
                        try
                        {
                            ActionLogger.Logger.WriteLog("UnlinkCommissiondashBoardIncomingPayment - Entries are paid ", true);
                            UnlinkPaymentPaid(_DEU, strUnlink, PolicySelectedIncomingPaymentCommissionDashBoard.PaymentEntryID, _UserRole);
                            _PostProcessReturnStatus = new PostProcessReturnStatus() { DeuEntryId = _DEU.DEUENtryID, IsComplete = true, ErrorMessage = null, PostEntryStatus = PostEntryProcess.RePost, IsClientDeleted = false };
                        }
                        catch (Exception ex)
                        {
                            _PostProcessReturnStatus = new PostProcessReturnStatus() { DeuEntryId = _DEU.DEUENtryID, IsComplete = false, ErrorMessage = null, PostEntryStatus = PostEntryProcess.RePost, IsClientDeleted = false };
                            ActionLogger.Logger.WriteLog("UnlinkCommissiondashBoardIncomingPayment exception: " + ex.Message, true);
                        }

                    }

                    if (_PostProcessReturnStatus.IsComplete)
                    {
                        //ts.Complete();
                    }
                    ActionLogger.Logger.WriteLog("UnlinkCommissiondashBoardIncomingPayment success: ", true);

                    return _PostProcessReturnStatus;
                //}
            }
            catch (Exception ex)
            {
                ActionLogger.Logger.WriteLog("UnlinkCommissiondashBoardIncomingPayment exception: " + ex.Message, true);
                if (ex.InnerException != null)
                {
                    ActionLogger.Logger.WriteLog("UnlinkCommissiondashBoardIncomingPayment inner exception: " + ex.InnerException.Message, true);
                }
                return _PostProcessReturnStatus;
            }
            finally
            {
            }
        }

    }
}
