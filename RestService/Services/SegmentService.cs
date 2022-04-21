using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ServiceModel;
using MyAgencyVault.BusinessLibrary;
using System.ServiceModel.Web;
using MyAgencyVault.WcfService.Library.Response;
using System.Data.SqlClient;
using System.Data;

namespace MyAgencyVault.WcfService
{
    [ServiceContract]
    interface ISegmentService
    {
        #region Config Segment 
        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        SettingSegmentListResponse GetSegmentsList(ListParams listParams, Guid LicenseeId);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        SettingSegmentListResponse GetSegmentsForPolicies(Guid LicenseeId, bool? isBlankRequired = false);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        SettingSegmentListResponse GetProductsWithoutSegments(Guid LicenseeId);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        SettingSegmentListResponse GetProductsSegmentsForUpdate(Guid segmentId, Guid LicenseeId);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        ReturnStatusResponse AddUpdateDeleteSegmentService(Segment segmentObject, Operation operationType, Guid LicenseeId);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        SettingSegmentListResponse GetSegmentsOnCoverageId(Guid? CoverageId, Guid? PolicyId, Guid LicenseeId);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        ReturnStatusResponse CheckProductSegmentAssociation(Segment segmentObject);
        #endregion
    }

    public partial class MavService : ISegmentService
    {

        #region Configuration segments
        public SettingSegmentListResponse GetSegmentsForPolicies(Guid LicenseeId, bool? isBlankRequired = false)
        {
            SettingSegmentListResponse res = null;
            try
            {
                List<ConfigSegment> lst = ConfigSegment.GetSegmentsForPolicies(LicenseeId);

                if (lst != null && lst.Count > 0)
                {
                    res = new SettingSegmentListResponse(string.Format("Segments list retreived successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                    if (isBlankRequired == true)
                    {
                        lst.Add(new ConfigSegment { SegmentId = new Guid("00000000-0000-0000-0000-000000000000"), SegmentName = "Blank" });
                    }
                    res.SegmentList = lst;
                    res.SegmentsCount = lst.Count();
                    res.RecordCount = lst.Count();
                    ActionLogger.Logger.WriteLog("GetSegmentsForPolicies success ", true);
                }
                else
                {
                    if (isBlankRequired == true && lst.Count == 0)
                    { 
                            List<ConfigSegment> lstsegments = new List<ConfigSegment>();
                            res = new SettingSegmentListResponse(string.Format("Segments list retreived successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                            lstsegments.Add(new ConfigSegment { SegmentId = new Guid("00000000-0000-0000-0000-000000000000"), SegmentName = "Blank" });
                            res.SegmentList = lstsegments;
                            res.SegmentsCount = lstsegments.Count();
                            res.RecordCount = lstsegments.Count();
                       
                        ActionLogger.Logger.WriteLog("GetSegmentsForPolicies success ", true);
                    }
                    else
                    {
                        res = new SettingSegmentListResponse("No segments found!", Convert.ToInt16(ResponseCodes.RecordNotFound), "");
                        ActionLogger.Logger.WriteLog("GetSegmentsForPolicies 404 ", true);
                    }
                   
                }
            }
            catch (Exception ex)
            {
                ActionLogger.Logger.WriteLog("GetSegmentsForPolicies Exception: " + ex.Message, true);
                res = new SettingSegmentListResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
            }
            return res;
        }

        public SettingSegmentListResponse GetSegmentsList(ListParams listParams,Guid LicenseeId)
        {
            ActionLogger.Logger.WriteLog("GetSegmentsList request:  " + listParams.ToStringDump(), true);
            SettingSegmentListResponse res = null;
            int count = 0;
            try
            {
                List<ConfigSegment> lst = ConfigSegment.GetSegmentsForConfiguration(listParams, out count,  LicenseeId);

                if (lst != null && lst.Count > 0)
                {
                    res = new SettingSegmentListResponse(string.Format("Segments list retreived successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                    res.SegmentList = lst;
                    res.SegmentsCount = count;
                    ActionLogger.Logger.WriteLog("GetSegmentsList success ", true);
                }
                else
                {
                    res = new SettingSegmentListResponse("No segments found!", Convert.ToInt16(ResponseCodes.RecordNotFound), "");
                    ActionLogger.Logger.WriteLog("GetSegmentsList 404 ", true);
                }
            }
            catch (Exception ex)
            {
                ActionLogger.Logger.WriteLog("GetSegmentsList Exception: " + ex.Message, true);
                res = new SettingSegmentListResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
            }
            return res;
        }

        public SettingSegmentListResponse GetSegmentsOnCoverageId(Guid? CoverageId, Guid? PolicyId, Guid LicenseeId)
        {
            ActionLogger.Logger.WriteLog("GetSegmentsOnCoverageId start:", true);
            SettingSegmentListResponse res = null;
            Guid segmentId = Guid.Empty;
            try
            {
                segmentId = ConfigSegment.GetSegmentsOnCoverageId(CoverageId, PolicyId, LicenseeId);

                if (segmentId == Guid.Empty)
                {
                    res = new SettingSegmentListResponse("No Segments Id found!", Convert.ToInt16(ResponseCodes.RecordNotFound), "");
                    ActionLogger.Logger.WriteLog("GetSegmentsOnCoverageId 404 ", true);
                }
                else
                {
                    res = new SettingSegmentListResponse(string.Format("Segments Id retreived successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                    ActionLogger.Logger.WriteLog("GetSegmentsOnCoverageId success ", true);
                }
                res.SegmentId = segmentId;
            }
            catch (Exception ex)
            {
                ActionLogger.Logger.WriteLog("GetSegmentsOnCoverageId Exception: " + ex.Message, true);
                res = new SettingSegmentListResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
            }
            return res;
        }
        #endregion

        public SettingSegmentListResponse GetProductsWithoutSegments(Guid LicenseeId)
        {
            SettingSegmentListResponse response = null;
            try
            {
                List<ConfigProductWithoutSegment> list = ConfigProductWithoutSegment.GetProductsWithoutSegments(LicenseeId);

                if (list != null && list.Count > 0)
                {
                    response = new SettingSegmentListResponse("", (int)ResponseCodes.Success, "");
                    response.ProductListWithoutSegment = list;
                    response.RecordCount = list.Count;
                    response.SegmentsCount = list.Count;
                    ActionLogger.Logger.WriteLog("GetProductsWithoutSegments success ", true);
                }
                else
                {
                    response = new SettingSegmentListResponse("", Convert.ToInt16(ResponseCodes.RecordNotFound), "");
                    ActionLogger.Logger.WriteLog("GetProductsWithoutSegments 404 ", true);
                }
            }
            catch (Exception ex)
            {
                ActionLogger.Logger.WriteLog("GetProductsWithoutSegments Exception: " + ex.Message, true);
                response = new SettingSegmentListResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
            }
            return response;
        }

        public SettingSegmentListResponse GetProductsSegmentsForUpdate(Guid segmentId, Guid LicenseeId)
        {
            SettingSegmentListResponse response = null;
            try
            {
               
                List<ConfigProductWithoutSegment> list = ConfigProductWithoutSegment.GetProductsSegmentsForUpdate(segmentId, LicenseeId);
                
                if (list != null && list.Count > 0)
                {
                    response = new SettingSegmentListResponse("", (int)ResponseCodes.Success, "");
                    response.ProductListWithoutSegment = list;
                    response.RecordCount = list.Count;
                    response.SegmentsCount = list.Count;
                    ActionLogger.Logger.WriteLog("GetProductsSegmentsForUpdate success ", true);
                }
                else
                {
                    response = new SettingSegmentListResponse("", Convert.ToInt16(ResponseCodes.RecordNotFound), "");
                    ActionLogger.Logger.WriteLog("GetProductsSegmentsForUpdate 404 ", true);
                }
            }
            catch (Exception ex)
            {
                ActionLogger.Logger.WriteLog("GetProductsSegmentsForUpdate Exception: " + ex.Message, true);
                response = new SettingSegmentListResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
            }
            return response;
        }

        public ReturnStatusResponse AddUpdateDeleteSegmentService(Segment segmentObject, Operation operationType, Guid LicenseeId)
        {
            ReturnStatusResponse jres = null;
            ActionLogger.Logger.WriteLog("AddUpdateDeleteSegmentService request: sObj -" + segmentObject.ToStringDump() + ", operationType : " + operationType, true);
            try
            {
                ReturnStatus obj = segmentObject.AddUpdateDelete(segmentObject, operationType, LicenseeId);

                if (obj != null)
                {
                    if (obj.IsError)
                    {
                        jres = new ReturnStatusResponse(string.Format("Segment could not be processed"), Convert.ToInt16(ResponseCodes.Failure), "");
                    }
                    else
                    {
                        if (operationType == Operation.Delete)
                        {
                            jres = new ReturnStatusResponse(string.Format("Segment processed successfully"), Convert.ToInt16(ResponseCodes.Success), obj.ErrorMessage);
                        }
                        else
                        {
                            jres = new ReturnStatusResponse(string.Format("Segment processed successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                        }
                    }
                    jres.Status = obj;
                    ActionLogger.Logger.WriteLog("AddUpdateDeleteSegmentService success ", true);
                }
                else
                {
                    jres = new ReturnStatusResponse(string.Format("Segment could not be processed"), Convert.ToInt16(ResponseCodes.Failure), "");
                    ActionLogger.Logger.WriteLog("AddUpdateDeleteSegmentService failure ", true);
                }
            }
            catch (Exception ex)
            {
                jres = new ReturnStatusResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                ActionLogger.Logger.WriteLog("AddUpdateDeleteSegmentService exception: " + ex.Message, true);
            }
            return jres;
        }

        public ReturnStatusResponse CheckProductSegmentAssociation(Segment segmentObject)
        {
            ReturnStatusResponse jres = null;
            
            ActionLogger.Logger.WriteLog("checkProductSegmentAssociation request: Segment -" + segmentObject.ToStringDump() + ", CoverageId : " + segmentObject.CoverageId, true);
            try
            {

                ReturnStatus obj = segmentObject.chkProductSegmentAss(segmentObject);

                if (obj != null)
                {
                    if (obj.IsError)
                    {
                        jres = new ReturnStatusResponse(string.Format("Segment could not be processed"), Convert.ToInt16(ResponseCodes.Failure), "");
                    }
                    else
                    {
                        jres = new ReturnStatusResponse(string.Format("Segment Product association fetched successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                    }
                    jres.RecordStatus = Convert.ToBoolean(obj.ErrorMessage);
                    jres.Status = obj;
                    ActionLogger.Logger.WriteLog("checkProductSegmentAssociation success ", true);
                }
                else
                {
                    jres = new ReturnStatusResponse(string.Format("Segment could not be processed"), Convert.ToInt16(ResponseCodes.Failure), "");
                    ActionLogger.Logger.WriteLog("checkProductSegmentAssociation failure ", true);
                }
            }
            catch (Exception ex)
            {
                jres = new ReturnStatusResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                ActionLogger.Logger.WriteLog("checkProductSegmentAssociation Service exception: " + ex.Message, true);
            }
            return jres;
        }
    }

    

}