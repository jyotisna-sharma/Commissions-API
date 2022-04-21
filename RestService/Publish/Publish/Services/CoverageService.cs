using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ServiceModel;
using MyAgencyVault.BusinessLibrary;
using MyAgencyVault.WcfService.Library.Response;
using System.ServiceModel.Web;

namespace MyAgencyVault.WcfService
{
    [ServiceContract]
    interface ICoverageService
    {
        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        CoverageNickNameResponse GetAllNickNamesService(Guid PayorId, Guid CarrierId, Guid CoverageId);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        PolicyStringResponse GetProductTypeNickNameService(Guid PolicyID, Guid PayorID, Guid CarrierID, Guid CoverageID);

/*        [OperationContract]
        ReturnStatus AddUpdateDeleteCoverage(Coverage Covrage, OperationSet operationType);

        [OperationContract]
        Coverage GetCarrierCoverage(Guid PayorId, Guid CarrierId, Guid CoverageId);

        [OperationContract]
        List<Coverage> GetCoverages(Guid LicenseeID);

        [OperationContract]
        List<Coverage> GetCarrierCoverages(Guid CarrierId);

        [OperationContract]
        List<DisplayedCoverage> GetDisplayedCarrierCoverages(Guid LicenseeId);

        [OperationContract]
        List<Coverage> GetPayorCarrierCoverages(Guid PayorId, Guid CarrierId);

     
        [OperationContract]
        bool IsValidCoverage(string carrierNickName, string coverageNickName, Guid payorId);

        [OperationContract]
        string GetCoverageNickName(Guid PayorId, Guid CarrierId, Guid CoverageId);

        [OperationContract]
        DisplayedCoverage GetCoverageForPolicy(Guid DisplayedCoverageID);

        [OperationContract]
        ReturnStatus DeleteNickName(Guid guidPayorID, Guid guidCarrierID, Guid guidPreviousCoverageId, string strPrviousNickName);

        [OperationContract]
        ReturnStatus DeleteProductType(Guid guidPayorID, Guid guidCarrierID, Guid guidCoverageId, string strNickNames);

    */
    }

    public partial class MavService : ICoverageService
    {
        public PolicyStringResponse GetProductTypeNickNameService(Guid PolicyID, Guid PayorID, Guid CarrierID, Guid CoverageID)
        {
            PolicyStringResponse jres = null;
            ActionLogger.Logger.WriteLog("GetProductTypeNickName request: " + PolicyID + ", PayorID: " +PayorID + ", carrier: " +CarrierID + ", coverage:" + CoverageID , true);
            try
            {
                string val = DEU.GetProductTypeNickName(PolicyID, PayorID, CarrierID, CoverageID);
                ActionLogger.Logger.WriteLog("GetProductTypeNickName value: " + val, true);
                if (!string.IsNullOrWhiteSpace(val))
                {
                    jres = new PolicyStringResponse(string.Format("Product type found successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                    jres.StringValue = val;
                }
                else
                {
                    jres = new PolicyStringResponse("Product type not found!", Convert.ToInt16(ResponseCodes.RecordNotFound), "");
                    ActionLogger.Logger.WriteLog("GetProductTypeNickName 404 ", true);
                }
            }
            catch (Exception ex)
            {
                jres = new PolicyStringResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                ActionLogger.Logger.WriteLog("GetProductTypeNickName failure ", true);
            }
            return jres;
        }

        /*public ReturnStatus AddUpdateDeleteCoverage(Coverage Covrage, OperationSet operationType)
        {
            return Covrage.AddUpdateDelete(Covrage, operationType);
        }

        public Coverage GetCarrierCoverage(Guid PayorId, Guid CarrierId, Guid CoverageId)
        {
            return Coverage.GetCarrierCoverage(PayorId, CarrierId, CoverageId);
        }

        public List<Coverage> GetCoverages(Guid LicenseeID)
        {
            return Coverage.GetCoverages(LicenseeID);
        }

        public List<Coverage> GetCarrierCoverages(Guid CarrierId)
        {
            return Coverage.GetCarrierCoverages(CarrierId);
        }

        public List<DisplayedCoverage> GetDisplayedCarrierCoverages(Guid LicenseeId)
        {
            return Coverage.GetDisplayedCarrierCoverages(LicenseeId);
        }

        public List<Coverage> GetPayorCarrierCoverages(Guid PayorId, Guid CarrierId)
        {
            return Coverage.GetCarrierCoverages(PayorId, CarrierId);
        }*/

        public CoverageNickNameResponse GetAllNickNamesService(Guid PayorId, Guid CarrierId, Guid CoverageId)
        {
            CoverageNickNameResponse jres = null;
            ActionLogger.Logger.WriteLog("GetAllNickNamesService request: " + PayorId + ", CarrierId " + CarrierId + ", coverageid: " + CoverageId, true);
            try
            {
                List<CoverageNickName> lst = Coverage.GetAllNickNames(PayorId, CarrierId, CoverageId);
                if (lst != null && lst.Count > 0)
                {
                    jres = new CoverageNickNameResponse(string.Format("Coverage list retreived successfully"), Convert.ToInt16(ResponseCodes.Success), "");
                    jres.CoverageNickNameList = lst;
                    ActionLogger.Logger.WriteLog("GetAllNickNamesService success ", true);
                }
                else
                {
                    jres = new CoverageNickNameResponse("No Coverages found!", Convert.ToInt16(ResponseCodes.RecordNotFound), "");
                    ActionLogger.Logger.WriteLog("GetAllNickNamesService 404 ", true);
                }
            }
            catch (Exception ex)
            {
                jres = new CoverageNickNameResponse("", Convert.ToInt16(ResponseCodes.Failure), ex.Message);
                ActionLogger.Logger.WriteLog("GetAllNickNamesService failure ", true);
            }
            return jres;
        }

        /*public bool IsValidCoverage(string carrierNickName, string coverageNickName, Guid payorId)
        {
            return Coverage.IsValidCoverage(carrierNickName, coverageNickName, payorId);
        }

        public string GetCoverageNickName(Guid PayorId, Guid CarrierId, Guid CoverageId)
        {
            return Coverage.GetCoverageNickName(PayorId, CarrierId, CoverageId);
        }
        public DisplayedCoverage GetCoverageForPolicy(Guid DisplayedCoverageID)
        {
            return Coverage.GetCoverageForPolicy(DisplayedCoverageID);
        }

        public ReturnStatus DeleteNickName(Guid guidPayorID, Guid guidCarrierID, Guid guidPreviousCoverageId, string strPrviousNickName)
        {
            return Coverage.DeleteNickName(guidPayorID, guidCarrierID, guidPreviousCoverageId, strPrviousNickName);
        }

        public ReturnStatus DeleteProductType(Guid guidPayorID, Guid guidCarrierID, Guid guidCoverageId, string strNickNames)
        {
            return Coverage.DeleteProductType(guidPayorID, guidCarrierID, guidCoverageId, strNickNames);
        }*/
    }
}