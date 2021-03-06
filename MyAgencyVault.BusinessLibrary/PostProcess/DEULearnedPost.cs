using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyAgencyVault.BusinessLibrary.Base;
using MyAgencyVault.BusinessLibrary.Masters;
using System.Runtime.Serialization;
using DLinq = DataAccessLayer.LinqtoEntity;
using System.Data.SqlClient;
using System.Data;

namespace MyAgencyVault.BusinessLibrary
{
    [DataContract]
    class DEULearnedPost //: PolicyLearnedField
    {

        public static Guid AddDataDeuToLearnedPost(DEU _DEU)
        {
            Guid policyID = Guid.Empty;
            try
            {
                using (SqlConnection con = new SqlConnection(DBConnection.GetConnectionString()))
                {
                    string sql = "sp_SaveLearnedFieldsFromDEU";
                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 0;
                    cmd.Parameters.AddWithValue("@PolicyID", _DEU.PolicyId);
                    cmd.Parameters.AddWithValue("@ClientID", _DEU.ClientID);
                    cmd.Parameters.AddWithValue("@Insured", _DEU.Insured);
                    cmd.Parameters.AddWithValue("@PolicyNumber", _DEU.PolicyNumber);
                    cmd.Parameters.AddWithValue("@OriginalEffectiveDate", _DEU.OriginalEffectiveDate);
                    cmd.Parameters.AddWithValue("@Renewal", _DEU.Renewal);
                    cmd.Parameters.AddWithValue("@CoverageID", _DEU.CoverageID);
                    cmd.Parameters.AddWithValue("@CarrierID", _DEU.CarrierID);
                    cmd.Parameters.AddWithValue("@Coverage", _DEU.CoverageNickName);
                    cmd.Parameters.AddWithValue("@Carrier", _DEU.CarrierName);
                    cmd.Parameters.AddWithValue("@Policymode", _DEU.PolicyMode);
                    cmd.Parameters.AddWithValue("@UserID", _DEU.CreatedBy);
                    cmd.Parameters.AddWithValue("@Enrolled", _DEU.Enrolled);
                    cmd.Parameters.AddWithValue("@Eligible", _DEU.Eligible);
                    cmd.Parameters.AddWithValue("@Link1", _DEU.Link1);
                    cmd.Parameters.AddWithValue("@SplitPercent", _DEU.SplitPer);
                    cmd.Parameters.AddWithValue("@CompTypeID", _DEU.CompTypeID);
                    cmd.Parameters.AddWithValue("@CompScheduleType", _DEU.CompScheduleType);
                    cmd.Parameters.AddWithValue("@PayorSysID", _DEU.PayorSysID);
                    con.Open();
                    int count = cmd.ExecuteNonQuery();

                    // record saved successfully 
                    if (count > 0)
                        policyID = _DEU.PolicyId;

                }
               // Policy.UpdatePendingPolicy(_DEU);
            }
            catch (Exception ex)
            {
                ActionLogger.Logger.WriteLog(DateTime.Now.ToString() + " Exception AddDataDeuToLearnedPost " + ex.Message, true);
            }
            return policyID;
        }

        /// <summary>
        /// it is called after creating pending policy means getting the policy to process beacuse it is using that
        /// </summary>
        /// <param name="_deuFields"></param>
        /// <param name="PolicyId"></param>
        public static Guid AddDataDeuToLearnedPost_old(DEU _DEU)
        {
            Guid policyID = Guid.Empty;
            try
            {
                using (DLinq.CommissionDepartmentEntities DataModel = Entity.DataModel)
                {
                    var _policyLearned = (from p in DataModel.PolicyLearnedFields where (p.PolicyId == _DEU.PolicyId) select p).FirstOrDefault();

                    if (_policyLearned == null)
                    {
                        return Guid.Empty;
                    }
                    else
                    {
                        if (!String.IsNullOrEmpty(_DEU.Insured))
                            _policyLearned.Insured = _DEU.Insured;

                        if (!String.IsNullOrEmpty(_DEU.PolicyNumber))
                            _policyLearned.PolicyNumber = _DEU.PolicyNumber;

                        if (_DEU.OriginalEffectiveDate != null)
                            _policyLearned.Effective = _DEU.OriginalEffectiveDate;


                        //Need to change code after this build
                        //ankita

                        Dictionary<string, object> parametervalues = new Dictionary<string, object>();
                        parametervalues.Add("PolicyId", _DEU.PolicyId);
                        PolicyDetailsData _policy = Policy.GetPolicyData(parametervalues).FirstOrDefault();
                        _policyLearned.TrackFrom = PostUtill.CalculateTrackFromDate(_policy);

                        if (!String.IsNullOrEmpty(_DEU.Renewal))
                            _policyLearned.Renewal = _DEU.Renewal;

                        if (_DEU.CarrierID != null || _DEU.CarrierID != Guid.Empty)
                            _policyLearned.CarrierReference.Value = (from s in DataModel.Carriers where s.CarrierId == _DEU.CarrierID select s).FirstOrDefault();

                        if (_DEU.CoverageID != null || _DEU.CoverageID != Guid.Empty)
                            _policyLearned.CoverageReference.Value = (from s in DataModel.Coverages where s.CoverageId == _DEU.CoverageID select s).FirstOrDefault();

                        //Save carrier nick Name                        
                        _policyLearned.CarrierNickName = _DEU.CarrierName;
                        //Save Coverage nick Name
                        _policyLearned.CoverageNickName = _DEU.ProductName;

                        if (_DEU.PolicyMode != null)
                            _policyLearned.PolicyModeId = _DEU.PolicyMode;

                        if (!String.IsNullOrEmpty(_DEU.Enrolled))
                            _policyLearned.Enrolled = _DEU.Enrolled;

                        if (!String.IsNullOrEmpty(_DEU.Eligible))
                            _policyLearned.Eligible = _DEU.Eligible;

                        if (!String.IsNullOrEmpty(_DEU.Link1))
                            _policyLearned.Link1 = _DEU.Link1;

                        if (_DEU.SplitPer != null)
                            _policyLearned.SplitPercentage = Convert.ToDecimal(_DEU.SplitPer);


                        if (_DEU.ClientID != null || _DEU.ClientID != Guid.Empty)
                            _policyLearned.ClientID = _DEU.ClientID;

                        //_policyLearned.PolicyReference.Value = (from s in DataModel.Policies where s.PolicyId == _policylearnedfield.PolicyId select s).FirstOrDefault();//It is already there n this information need not to override
                        if (_DEU.CompTypeID != null)
                            _policyLearned.CompTypeID = _DEU.CompTypeID;

                        if (_DEU.CompScheduleType != null)
                            _policyLearned.CompScheduleType = _DEU.CompScheduleType;

                        if (!String.IsNullOrEmpty(_DEU.PayorSysID))
                            _policyLearned.PayorSysID = _DEU.PayorSysID;

                        if (_DEU.CreatedBy != null || _DEU.CreatedBy != Guid.Empty)
                            _policyLearned.LastModifiedUserCredentialid = _DEU.CreatedBy;


                        _policyLearned.LastModifiedOn = DateTime.Today;

                        _policyLearned.PMC = PostUtill.CalculatePMC(_DEU.PolicyId).ToString();
                        _policyLearned.PAC = PostUtill.CalculatePAC(_DEU.PolicyId).ToString();

                        Policy.UpdatePendingPolicy(_DEU);

                    }
                    //int? t = DataModel.Connection.ConnectionTimeout;
                    DataModel.SaveChanges();
                    policyID = _policyLearned.PolicyId;
                }
            }
            catch (Exception ex)
            {
                ActionLogger.Logger.WriteLog("AddDataDeuToLearnedPost ex.InnerException:" + ex.InnerException.ToString(), true);
            }
            return policyID;//      return _policyLearned;            

        }

    }
}
