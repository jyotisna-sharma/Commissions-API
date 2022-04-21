using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using MyAgencyVault.BusinessLibrary;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.ServiceModel.Activation;
using System.Runtime.Serialization;
using MyAgencyVault.WcfService.Library.Response;
using System.ServiceModel.Dispatcher;

namespace MyAgencyVault.WcfService
{
    [ServiceContract]
    interface INewsService
    {
        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        JSONResponse AddUpdateNewsService(News Nws);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        JSONResponse DeleteNewsService(News Nws);

        //[OperationContract]
        //News GetNews();
        //[OperationContract] Found not implemented 
        //[WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        //PolicyBoolResponse IsValidNewsService(News Nws);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        NewsListResponse GetNewsListService();
    }
    public partial class MavService : INewsService
    {
        public JSONResponse AddUpdateNewsService(News Nws)
        {
            ActionLogger.Logger.WriteLog("AddUpdateNewsService request: Nws - " + Nws.ToStringDump(), true);
            JSONResponse jres = null;
            try
            {
                Nws.AddUpdate();
                jres = new JSONResponse("News details saved successfully", (int)ResponseCodes.Success, "");
                ActionLogger.Logger.WriteLog("AddUpdateNewsService success", true);
            }
            catch (Exception ex)
            {
                jres = new JSONResponse("", (int)ResponseCodes.Failure, ex.Message);
                ActionLogger.Logger.WriteLog("AddUpdateNewsService failure", true);
            }
            return jres;

        }

        public JSONResponse DeleteNewsService(News Nws)
        {
            ActionLogger.Logger.WriteLog("DeleteNewsService request: Nws - " + Nws.ToStringDump(), true);
            JSONResponse jres = null;
            try
            {
                Nws.Delete();
                jres = new JSONResponse("News details deleted successfully", (int)ResponseCodes.Success, "");
                ActionLogger.Logger.WriteLog("DeleteNewsService success", true);
            }
            catch (Exception ex)
            {
                jres = new JSONResponse("", (int)ResponseCodes.Failure, ex.Message);
                ActionLogger.Logger.WriteLog("DeleteNewsService failure", true);
            }
            return jres;
        }

        //public News GetNews()
        //{
        //    throw new NotImplementedException();
        //}

        //public PolicyBoolResponse IsValidNewsService(News Nws)
        //{
        //    ActionLogger.Logger.WriteLog("IsValidNewsService request: Nws - " + Nws.ToStringDump(), true);
        //    PolicyBoolResponse jres = null;
        //    try
        //    {
        //        bool result = Nws.IsValid();
        //        jres = new PolicyBoolResponse("News valid status found successfully", (int)ResponseCodes.Success, "");
        //        jres.BoolFlag = result;
        //        ActionLogger.Logger.WriteLog("IsValidNewsService success", true);
        //    }
        //    catch (Exception ex)
        //    {
        //        jres = new PolicyBoolResponse("", (int)ResponseCodes.Failure, ex.Message);
        //        ActionLogger.Logger.WriteLog("IsValidNewsService failure", true);
        //    }
        //    return jres;
        //}

        public NewsListResponse GetNewsListService()
        {
            ActionLogger.Logger.WriteLog("GetNewsListService request:  ", true);
            NewsListResponse jres = null;
            try
            {
                List<News> lst = News.GetNewsList();
                if (lst != null && lst.Count > 0)
                {
                    jres = new NewsListResponse("News listing found successfully", (int)ResponseCodes.Success, "");
                    jres.NewsList = lst;
                    ActionLogger.Logger.WriteLog("GetNewsListService success", true);
                }
                else
                {
                    jres = new NewsListResponse("No record found", (int)ResponseCodes.RecordNotFound, "");
                    ActionLogger.Logger.WriteLog("GetNewsListService 404", true);
                }
            }
            catch (Exception ex)
            {
                jres = new NewsListResponse("", (int)ResponseCodes.Failure, ex.Message);
                ActionLogger.Logger.WriteLog("GetNewsListService failure", true);
            }
            return jres;
        }
    }

}