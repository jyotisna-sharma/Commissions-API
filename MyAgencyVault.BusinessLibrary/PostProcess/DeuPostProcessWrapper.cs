using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using System.Threading;
using MyAgencyVault.BusinessLibrary.BusinessObjects;
using System.Globalization;

namespace MyAgencyVault.BusinessLibrary.PostProcess
{
    public class DeuPostProcessWrapper
    {

        #region New methods for web
        public static DEUPaymentEntry AddUpdateDEUEntry(DEUFields deuFields, Guid deuEntryId, bool isNewStatementCreate, out int? newStatementNumber, out decimal? entered)
        {
            newStatementNumber = 0;
            ActionLogger.Logger.WriteLog("AddUpdateDEUEntry  request starts : " + deuFields.ToStringDump(), true);
            if (deuFields == null)
            {
                ActionLogger.Logger.WriteLog("DeuPostStartWrapper  request fields object is null : ", true);
                throw new Exception("DEU fields cannot be null");
            }

            //Check if edited entry is paid, then return 
            if(deuEntryId != Guid.Empty)
            {
                bool result = PolicyOutgoingDistribution.CheckIsDEUEntryMarkPaid(deuEntryId);
                ActionLogger.Logger.WriteLog("DeuPostStartWrapper edited entry is paid flag : " + result, true);
                if (result)
                {
                    throw new Exception("Payment cannot be posted from DEU as it is paid/semi-paid.");
                }
            }

            DEUPaymentEntry entryDetails = new DEUPaymentEntry();
         //   if (isNewStatementCreate == true)
           // {
              //  ActionLogger.Logger.WriteLog("AddUpdateDEUEntry  new stmt case", true);
                Statement details = new Statement();
                details.StatementID =  deuFields.StatementId;      
                details.BatchId = deuFields.BatchId;
                details.PayorId = deuFields.PayorId;
                details.TemplateID = deuFields.TemplateID == Guid.Empty ? null : deuFields.TemplateID ;
                details.CreatedBy = deuFields.CurrentUser; //will be modified for new stmt only inside method 
                newStatementNumber = DEU.AddUpdateStatement(details);
                deuFields.StatementId = DEU.GetStatementId(newStatementNumber);
             //   ActionLogger.Logger.WriteLog("AddUpdateDEUEntry  new stmt created : " + deuFields.StatementId, true);
            //}

            if (deuFields != null && deuFields.DeuFieldDataCollection.Count > 0)
            {
                foreach (var fieldData in deuFields.DeuFieldDataCollection)
                {
                    if (fieldData.DeuFieldType == 1)
                    {
                        fieldData.DeuFieldValue = DEU.GetFormatedDate(fieldData.DeuFieldValue, "MM/dd/yyyy");
                    }
                }

            }
            try
            {
                DEU objDeu = new DEU();
                // ModifiyableBatchStatementData data = objDeu.AddUpdate_old(deuFields, deuEntryId);
                Guid Id = objDeu.AddUpdate(deuFields, deuEntryId);
                entryDetails = DEU.GetDeuEntryOnID(Id);
                entryDetails.isSuccess = true; //set assuming success , will be reset in case any failure occurs later in 2nd event.
                entryDetails.isProcessing = true;
                entered = DEU.GetStatementEnteredAmount(deuFields.StatementId);
            }
            catch (Exception ex)
            {
                entryDetails.isSuccess = false;
                entryDetails.isProcessing = false;
                ActionLogger.Logger.WriteLog("AddUpdateDEUEntry exception : " + ex.Message, true);
                if(ex.InnerException != null)
                {
                    ActionLogger.Logger.WriteLog("AddUpdateDEUEntry exception : " + ex.InnerException.ToStringDump(), true);

                }
                throw ex;
            }

            return entryDetails;

        }


        /// <summary>
        /// Repost = deuEntryID - old entry ID of payment
        /// deuFields.DeuEntryID - new entryID to be posted 
        /// </summary>
        /// <param name="_PostEntryProcess"></param>
        /// <param name="deuFields"></param>
        /// <param name="deuEntryId"></param>
        /// <returns></returns>
        public static PostProcessWebStatus PostStart(PostEntryProcess _PostEntryProcess, DEUFields deuFields, Guid deuEntryId)
        {
            ActionLogger.Logger.WriteLog("DeuPostStartWrapper  request starts : ", true);

            if (_PostEntryProcess != PostEntryProcess.Delete)
            {
                ActionLogger.Logger.WriteLog("DeuPostStartWrapper: Action is not delete ", true);
                if (deuFields == null)
                {
                    ActionLogger.Logger.WriteLog("DeuPostStartWrapper  request fields object is null : ", true);
                    throw new Exception("DEU fields cannot be null");
                }

                if (deuFields != null && deuFields.DeuFieldDataCollection.Count > 0)
                {
                    foreach (var fieldData in deuFields.DeuFieldDataCollection)
                    {
                        if (fieldData.DeuFieldType == 1)
                        {
                            fieldData.DeuFieldValue = DEU.GetFormatedDate(fieldData.DeuFieldValue, "MM/dd/yyyy");
                        }
                    }

                }
            }

            //newStatementNumber = 0;

            //if (isNewStatementCreate == true)
            //{
            //    ActionLogger.Logger.WriteLog("DeuPostStartWrapper  new stmt case", true);
            //    Statement details = new Statement();
            //    details.BatchId = deuFields.BatchId;
            //    details.PayorId = deuFields.PayorId;
            //    details.CreatedBy = deuFields.CurrentUser;
            //    newStatementNumber = DEU.AddUpdateStatement(details);
            //    deuFields.StatementId = DEU.GetStatementId(newStatementNumber);
            //    ActionLogger.Logger.WriteLog("DeuPostStartWrapper  new stmt created : " + deuFields.StatementId, true);
            //}

            //if (deuFields != null && deuFields.DeuFieldDataCollection.Count > 0)
            //{
            //    foreach (var fieldData in deuFields.DeuFieldDataCollection)
            //    {
            //        if (fieldData.DeuFieldType == 1)
            //        {
            //            fieldData.DeuFieldValue = DEU.GetFormatedDate(fieldData.DeuFieldValue, "MM/dd/yyyy");
            //        }
            //    }

            //}


            //DEUFields tempDeuFields = null;
            //BasicInformationForProcess _BasicInformationForProcess = null;

            PostProcessWebStatus _PostProcessReturnStatus = null;
            try
            {

                //if (_PostEntryProcess == PostEntryProcess.FirstPost || _PostEntryProcess == PostEntryProcess.RePost)
                //{
                //    ActionLogger.Logger.WriteLog("DeuPostStartWrapper FirstPost or RePost  1:", true);
                //    ActionLogger.Logger.WriteLog("DeuPostStartWrapper FirstPost or RePost  1:deuEntryId: " + deuFields.DeuEntryId, true);
                //    DEU objDeu = new DEU();
                //    // ModifiyableBatchStatementData batchStatementData = new ModifiyableBatchStatementData(); //////  JS - May 22, 2020 - commented below objDeu.AddUpdate(deuFields, deuEntryId);


                //    ////  JS - May 22, 2020 - commented below 
                //    ///Check null before assingn the value
                //    //if (batchStatementData == null)
                //    //    return _PostProcessReturnStatus;
                //    ////Check null before assingn the value
                //    //if (batchStatementData.ExposedDeu == null)
                //    //    return _PostProcessReturnStatus;
                //    ////Check null before assingn the value
                //    //if (batchStatementData.ExposedDeu.DEUENtryID == null)
                //    //    return _PostProcessReturnStatus;

                //    //Add update all incoming data into entriesbyDEU 
                //    deuFields.DeuEntryId = objDeu.AddUpdate(deuFields, deuEntryId);  // JS - May 22, 2020 batchStatementData.ExposedDeu.DEUENtryID;
                //    ActionLogger.Logger.WriteLog("DeuPostStartWrapper FirstPost or RePost  1:deuFields.deuEntryId: " + deuFields.DeuEntryId, true);

                //}

                // JS - May 22, 2020 Commented below as same process happens inside post start

                //if (deuEntryId != Guid.Empty)
                //{
                //    tempDeuFields = PostUtill.FillDEUFields(deuEntryId);
                //    _BasicInformationForProcess = PostUtill.GetPolicyToProcess(tempDeuFields, string.Empty);
                //}

                //  _PostProcessReturnStatus = new PostProcessReturnStatus() { DeuEntryId = Guid.Empty, IsComplete = false, ErrorMessage = null, PostEntryStatus = _PostEntryProcess };
                _PostProcessReturnStatus = new PostProcessWebStatus(); // { DeuEntryId = Guid.Empty, ErrorMessage = null};
                _PostProcessReturnStatus.DeuEntryId = deuFields.DeuEntryId;  //(_PostEntryProcess == PostEntryProcess.Delete) ? deuEntryId : deuFields.DeuEntryId;
                _PostProcessReturnStatus.IsSuccess = true;
               // _PostProcessReturnStatus.StatementNumber = (int)newStatementNumber;

                decimal? totalEnteredAmount = 0;
                if (_PostEntryProcess == PostEntryProcess.FirstPost)
                {
                     PostUtill.PostStart_DEU(_PostEntryProcess, deuFields.DeuEntryId, deuEntryId, deuFields.CurrentUser, UserRole.DEP, _PostEntryProcess,ref _PostProcessReturnStatus);

                }
                else if (_PostEntryProcess == PostEntryProcess.RePost)
                {
                    _PostProcessReturnStatus.OldDeuEntryId = deuEntryId;
                    PostUtill.PostStart_DEU(_PostEntryProcess, deuEntryId, deuFields.DeuEntryId, deuFields.CurrentUser, UserRole.DEP, _PostEntryProcess, ref _PostProcessReturnStatus);
                }
                else
                {
                     PostUtill.PostStart_DEU(_PostEntryProcess, deuEntryId, Guid.Empty, deuFields.CurrentUser, UserRole.DEP, _PostEntryProcess, ref _PostProcessReturnStatus);
                }
                // _PostProcessReturnStatus.IsComplete = true;\
               // _PostProcessReturnStatus.EnteredAmount = totalEnteredAmount == null ?  0.ToString("C") : ((decimal)totalEnteredAmount).ToString("N", new CultureInfo("en-US")); ;
            }
            catch (Exception ex)
            {
                ActionLogger.Logger.WriteLog("DeuPostStartWrapper:" + ex.Message, true);
                if (ex.InnerException != null)
                {
                    ActionLogger.Logger.WriteLog("DeuPostStartWrapper:" + ex.InnerException.ToStringDump(), true);
                }
                _PostProcessReturnStatus.IsSuccess = false;

                throw ex;
            }
            finally
            {
                ActionLogger.Logger.WriteLog("DeuPostStartWrapper completed 'finally':", true);
            }

           // _PostProcessReturnStatus.IsPostComplete = true;
            return _PostProcessReturnStatus;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="_PostEntryProcess"></param>
        /// <param name="deuFields"></param>
        /// <param name="deuEntryId">
        /// FirstPost = Guid.Empty
        /// RePort = OldDeuEntryId
        /// DeletePost = DeuEntryId
        /// </param>
        /// <param name="userRole"></param>
        /// <returns></returns>
        public static PostProcessWebStatus DeuPostStartWrapper(PostEntryProcess _PostEntryProcess, DEUFields deuFields, Guid deuEntryId, /*Guid userId, UserRole userRole,*/ bool isNewStatementCreate , out int? newStatementNumber)
        {
            ActionLogger.Logger.WriteLog("DeuPostStartWrapper  request starts : ", true);

            if (deuFields == null)
            {
                ActionLogger.Logger.WriteLog("DeuPostStartWrapper  request fields object is null : ", true);
                throw new Exception("DEU fields cannot be null");
            }

            newStatementNumber = 0;

            if (isNewStatementCreate == true)
            {
                ActionLogger.Logger.WriteLog("DeuPostStartWrapper  new stmt case", true);
                Statement details = new Statement();
                details.BatchId = deuFields.BatchId;
                details.PayorId = deuFields.PayorId;
                details.CreatedBy = deuFields.CurrentUser;
                details.TemplateID = deuFields.TemplateID;
                newStatementNumber = DEU.AddUpdateStatement(details);
                deuFields.StatementId = DEU.GetStatementId(newStatementNumber);
                ActionLogger.Logger.WriteLog("DeuPostStartWrapper  new stmt created : " + deuFields.StatementId, true);
            }

            if (deuFields !=null &&  deuFields.DeuFieldDataCollection.Count > 0)
            {
                foreach (var fieldData in deuFields.DeuFieldDataCollection)
                {
                    if (fieldData.DeuFieldType == 1)
                    {
                        fieldData.DeuFieldValue = DEU.GetFormatedDate(fieldData.DeuFieldValue, "MM/dd/yyyy");
                    }
                }

            }


            //DEUFields tempDeuFields = null;
            //BasicInformationForProcess _BasicInformationForProcess = null;

            PostProcessWebStatus _PostProcessReturnStatus = null;
            try
            {
               
                if (_PostEntryProcess == PostEntryProcess.FirstPost || _PostEntryProcess == PostEntryProcess.RePost)
                {
                    ActionLogger.Logger.WriteLog("DeuPostStartWrapper FirstPost or RePost  1:", true);
                    ActionLogger.Logger.WriteLog("DeuPostStartWrapper FirstPost or RePost  1:deuEntryId: " + deuFields.DeuEntryId, true);
                    DEU objDeu = new DEU();
                   // ModifiyableBatchStatementData batchStatementData = new ModifiyableBatchStatementData(); //////  JS - May 22, 2020 - commented below objDeu.AddUpdate(deuFields, deuEntryId);
                    

                    ////  JS - May 22, 2020 - commented below 
                    ///Check null before assingn the value
                    //if (batchStatementData == null)
                    //    return _PostProcessReturnStatus;
                    ////Check null before assingn the value
                    //if (batchStatementData.ExposedDeu == null)
                    //    return _PostProcessReturnStatus;
                    ////Check null before assingn the value
                    //if (batchStatementData.ExposedDeu.DEUENtryID == null)
                    //    return _PostProcessReturnStatus;

                    //Add update all incoming data into entriesbyDEU 
                    deuFields.DeuEntryId = objDeu.AddUpdate(deuFields, deuEntryId);  // JS - May 22, 2020 batchStatementData.ExposedDeu.DEUENtryID;
                    ActionLogger.Logger.WriteLog("DeuPostStartWrapper FirstPost or RePost  1:deuFields.deuEntryId: " + deuFields.DeuEntryId, true);

                }

                // JS - May 22, 2020 Commented below as same process happens inside post start

                //if (deuEntryId != Guid.Empty)
                //{
                //    tempDeuFields = PostUtill.FillDEUFields(deuEntryId);
                //    _BasicInformationForProcess = PostUtill.GetPolicyToProcess(tempDeuFields, string.Empty);
                //}

                //  _PostProcessReturnStatus = new PostProcessReturnStatus() { DeuEntryId = Guid.Empty, IsComplete = false, ErrorMessage = null, PostEntryStatus = _PostEntryProcess };
                _PostProcessReturnStatus = new PostProcessWebStatus(); // { DeuEntryId = Guid.Empty, ErrorMessage = null};
                _PostProcessReturnStatus.DeuEntryId = deuFields.DeuEntryId;  //(_PostEntryProcess == PostEntryProcess.Delete) ? deuEntryId : deuFields.DeuEntryId;
                _PostProcessReturnStatus.StatementNumber = (int) newStatementNumber;

                decimal? totalEnteredAmount = 0;
                if (_PostEntryProcess == PostEntryProcess.FirstPost)
                {
                    //ActionLogger.Logger.WriteLog("DeuPostStartWrapper FirstPost 6:", true);

                    //_PostProcessReturnStatus =
                     PostUtill.PostStart_DEU(_PostEntryProcess, deuFields.DeuEntryId, deuEntryId, deuFields.CurrentUser, UserRole.DEP, _PostEntryProcess, ref _PostProcessReturnStatus); // string.Empty, string.Empty);

                    //_PostProcessReturnStatus.DeuEntryId = deuFields.DeuEntryId;
                    //_PostProcessReturnStatus.OldDeuEntryId = Guid.Empty;
                    //_PostProcessReturnStatus.ReferenceNo = deuFields.ReferenceNo;
                    //ActionLogger.Logger.WriteLog("DeuPostStartWrapper FirstPost 7:", true);

                }
                else if (_PostEntryProcess == PostEntryProcess.RePost)
                {
                    //ActionLogger.Logger.WriteLog("DeuPostStartWrapper Repost 8:", true);
                    //_PostProcessReturnStatus = 
                     PostUtill.PostStart_DEU(_PostEntryProcess, deuEntryId, deuFields.DeuEntryId, deuFields.CurrentUser, UserRole.DEP,_PostEntryProcess, ref _PostProcessReturnStatus); //string.Empty, string.Empty);
                    //_PostProcessReturnStatus.DeuEntryId = deuFields.DeuEntryId;
                    //_PostProcessReturnStatus.OldDeuEntryId = deuEntryId;
                    //_PostProcessReturnStatus.ReferenceNo = deuFields.ReferenceNo;
                    ////ActionLogger.Logger.WriteLog("DeuPostStartWrapper Repost 9:", true);
                }
                else
                {
                    //ActionLogger.Logger.WriteLog("DeuPostStartWrapper Delete 10:", true);
                    //_PostProcessReturnStatus =
                    PostUtill.PostStart_DEU(_PostEntryProcess, deuEntryId, Guid.Empty, deuFields.CurrentUser, UserRole.DEP, _PostEntryProcess, ref _PostProcessReturnStatus); //string.Empty, string.Empty);
                    //_PostProcessReturnStatus.DeuEntryId = deuEntryId;
                    //_PostProcessReturnStatus.OldDeuEntryId = Guid.Empty;
                    //ActionLogger.Logger.WriteLog("DeuPostStartWrapper Delete 11:", true);
                }
                // _PostProcessReturnStatus.IsComplete = true;\
                //_PostProcessReturnStatus.EnteredAmount = totalEnteredAmount== null ? 0.ToString("C") :  Convert.ToDecimal(totalEnteredAmount).ToString("N", new CultureInfo("en-US")); ;
            }
            catch (Exception ex)
            {
                ActionLogger.Logger.WriteLog("DeuPostStartWrapper:" + ex.Message, true);
                if(ex.InnerException != null)
                {
                    ActionLogger.Logger.WriteLog("DeuPostStartWrapper:" + ex.InnerException.ToStringDump(), true);
                }
                // _PostProcessReturnStatus.IsComplete = false;
                throw ex;
            }
            finally
            {
                ActionLogger.Logger.WriteLog("DeuPostStartWrapper completed 'finally':", true);
            }

            return _PostProcessReturnStatus;
        }

        #endregion
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_PostEntryProcess"></param>
        /// <param name="deuFields"></param>
        /// <param name="deuEntryId">
        /// FirstPost = Guid.Empty
        /// RePort = OldDeuEntryId
        /// DeletePost = DeuEntryId
        /// </param>
        /// <param name="userRole"></param>
        /// <returns></returns>
        public static PostProcessReturnStatus DeuPostStartWrapper_Old(PostEntryProcess _PostEntryProcess, DEUFields deuFields, Guid deuEntryId, Guid userId, UserRole userRole)
        {

            ActionLogger.Logger.WriteLog("DeuPostStartWrapper  request DEUEntryID: " + deuEntryId + ", user: " + userId, true);
            if (deuFields.DeuFieldDataCollection.Count > 0)
            {
                foreach (var fieldData in deuFields.DeuFieldDataCollection)
                {
                    if (fieldData.DeuFieldType == 1)
                    {
                        fieldData.DeuFieldValue = DEU.GetFormatedDate(fieldData.DeuFieldValue, "MM/dd/yyyy");
                    }
                }

            }


            //bool lockObtained = false;
            DEUFields tempDeuFields = null;
            BasicInformationForProcess _BasicInformationForProcess = null;

            //var options = new TransactionOptions
            //{
            //    IsolationLevel = IsolationLevel.ReadUncommitted,
            //    Timeout = TimeSpan.FromMinutes(60)
            //};

            PostProcessReturnStatus _PostProcessReturnStatus = null;
            try
            {
                //using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required, options))
                //{
                if (_PostEntryProcess == PostEntryProcess.FirstPost || _PostEntryProcess == PostEntryProcess.RePost)
                {
                    ActionLogger.Logger.WriteLog("DeuPostStartWrapper FirstPost or RePost  1:", true);
                    ActionLogger.Logger.WriteLog("DeuPostStartWrapper FirstPost or RePost  1:deuEntryId: " + deuEntryId, true);
                    DEU objDeu = new DEU();
                    ModifiyableBatchStatementData batchStatementData = objDeu.AddUpdate_old(deuFields, deuEntryId);

                    //Check null before assingn the value
                    if (batchStatementData == null)
                        return _PostProcessReturnStatus;
                    //Check null before assingn the value
                    if (batchStatementData.ExposedDeu == null)
                        return _PostProcessReturnStatus;
                    //Check null before assingn the value
                    if (batchStatementData.ExposedDeu.DEUENtryID == null)
                        return _PostProcessReturnStatus;
                    //Assin deuentry ID
                    deuFields.DeuEntryId = batchStatementData.ExposedDeu.DEUENtryID;
                    ActionLogger.Logger.WriteLog("DeuPostStartWrapper FirstPost or RePost  1:deuFields.deuEntryId: " + deuFields.DeuEntryId, true);
                    // ActionLogger.Logger.WriteLog("DeuPostStartWrapper FirstPost or RePost  2:", true);

                    if (deuFields != null)
                    {
                        if (deuFields.DeuEntryId != null)
                        {
                            tempDeuFields = PostUtill.FillDEUFields(deuFields.DeuEntryId);
                        }
                    }

                    if (deuEntryId != Guid.Empty)
                    {
                        tempDeuFields = PostUtill.FillDEUFields(deuEntryId);
                        _BasicInformationForProcess = PostUtill.GetPolicyToProcess(tempDeuFields, string.Empty);
                    }
                    // ActionLogger.Logger.WriteLog("DeuPostStartWrapper FirstPost or RePost 3:", true);
                }
                else
                {
                    ActionLogger.Logger.WriteLog("DeuPostStartWrapper  delete request", true);
                    //ActionLogger.Logger.WriteLog("DeuPostStartWrapper delete 4:", true);
                    try
                    {
                        tempDeuFields = PostUtill.FillDEUFields(deuEntryId);
                        _BasicInformationForProcess = PostUtill.GetPolicyToProcess(tempDeuFields, string.Empty);
                    }
                    catch (Exception ex)
                    {
                        ActionLogger.Logger.WriteLog("DeuPostStartWrapper exception: " + ex.Message, true);
                    }
                    // ActionLogger.Logger.WriteLog("DeuPostStartWrapper delete 5:", true);
                }

                _PostProcessReturnStatus = new PostProcessReturnStatus() { DeuEntryId = Guid.Empty, IsComplete = false, ErrorMessage = null, PostEntryStatus = _PostEntryProcess };

                if (_PostEntryProcess == PostEntryProcess.FirstPost)
                {
                    //ActionLogger.Logger.WriteLog("DeuPostStartWrapper FirstPost 6:", true);

                    _PostProcessReturnStatus = PostUtill.PostStart(_PostEntryProcess, deuFields.DeuEntryId, deuEntryId, userId, userRole, _PostEntryProcess, string.Empty, string.Empty);

                    _PostProcessReturnStatus.DeuEntryId = deuFields.DeuEntryId;
                    _PostProcessReturnStatus.OldDeuEntryId = Guid.Empty;
                    _PostProcessReturnStatus.ReferenceNo = deuFields.ReferenceNo;
                    //ActionLogger.Logger.WriteLog("DeuPostStartWrapper FirstPost 7:", true);

                }
                else if (_PostEntryProcess == PostEntryProcess.RePost)
                {
                    //ActionLogger.Logger.WriteLog("DeuPostStartWrapper Repost 8:", true);
                    _PostProcessReturnStatus = PostUtill.PostStart(_PostEntryProcess, deuEntryId, deuFields.DeuEntryId, userId, userRole, _PostEntryProcess, string.Empty, string.Empty);
                    _PostProcessReturnStatus.DeuEntryId = deuFields.DeuEntryId;
                    _PostProcessReturnStatus.OldDeuEntryId = deuEntryId;
                    _PostProcessReturnStatus.ReferenceNo = deuFields.ReferenceNo;
                    //ActionLogger.Logger.WriteLog("DeuPostStartWrapper Repost 9:", true);
                }
                else
                {
                    //ActionLogger.Logger.WriteLog("DeuPostStartWrapper Delete 10:", true);
                    _PostProcessReturnStatus = PostUtill.PostStart(_PostEntryProcess, deuEntryId, Guid.Empty, userId, userRole, _PostEntryProcess, string.Empty, string.Empty);
                    _PostProcessReturnStatus.DeuEntryId = deuEntryId;
                    _PostProcessReturnStatus.OldDeuEntryId = Guid.Empty;
                    //ActionLogger.Logger.WriteLog("DeuPostStartWrapper Delete 11:", true);
                }
                if (_PostProcessReturnStatus.IsComplete)
                {
                    //ts.Complete();
                    //ActionLogger.Logger.WriteLog("Sucess commited:", true);
                }
                else
                {
                    //ActionLogger.Logger.WriteLog("Failed need to rollback:", true);
                    if (deuFields != null)
                    {
                        //ActionLogger.Logger.WriteLog("Rollback id:" + deuFields.DeuEntryId.ToString(), true);
                    }
                }
                // }

            }
            catch (Exception ex)
            {
                ActionLogger.Logger.WriteLog("DeuPostStartWrapper:" + ex.Message.ToString(), true);
            }
            finally
            {
            }

            return _PostProcessReturnStatus;
        }
    }
}
