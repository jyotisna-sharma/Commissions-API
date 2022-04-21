using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using MyAgencyVault.BusinessLibrary.Base;
using System.Runtime.Serialization;
using DLinq = DataAccessLayer.LinqtoEntity;
using MyAgencyVault.BusinessLibrary.Masters;
using DataAccessLayer.LinqtoEntity;
using System.Web;
using System.IO;
using System.Threading;
using System.Net;
using MyAgencyVault.BusinessLibrary.ReportingService;
using MyAgencyVault.BusinessLibrary.BusinessObjects;
using System.Runtime.InteropServices;
using System.Security.Principal;

namespace MyAgencyVault.BusinessLibrary
{
    [DataContract]
    public class Report
    {
        /// <summary>
        /// Modified By:Ankit Kahndelwal
        /// ModifiedOn:03-05-2019
        /// Purpose:geettin glist of Reports Name
        /// </summary>
        /// <param name="reportGroupName"></param>
        /// <returns></returns>
        public static List<Report> GetReports(string reportGroupName)
        {
            ActionLogger.Logger.WriteLog("GetReports: Processing begins with reportGroupname" + " " + reportGroupName, true);
            List<Report> reports = new List<Report>();
            try
            {
                using (DLinq.CommissionDepartmentEntities DataModel = Entity.DataModel)
                {
                    List<DLinq.MasterReportList> reportList = DataModel.MasterReportLists.Where(p => p.ReportGroupName == reportGroupName).ToList();
                    foreach (DLinq.MasterReportList report in reportList)
                    {
                        bool isVisible = true;
                        bool.TryParse(Convert.ToString(report.IsVisible), out isVisible);
                        if (isVisible)
                        {
                            Report rpt = new Report();
                            rpt.Id = report.ReportId;
                            rpt.Code = report.ReportCode;
                            rpt.Description = report.ReportDescription;
                            rpt.GroupName = report.ReportGroupName;
                            rpt.Name = report.ReportName;

                            reports.Add(rpt);
                        }
                    }
                    reports = reports.OrderBy(s => s.GroupName).OrderBy(s => s.Name).ToList();
                    return reports;
                }
            }
            catch (Exception ex)
            {
                ActionLogger.Logger.WriteLog("GetReports: Exception occurs with reportGroupname" + " " + ex.Message, true);
                throw ex;
            }
            return reports;
        }
        public static List<GetPayeeList> GetPayeeList(Guid licenseeId)
        {
            List<GetPayeeList> payeelist = null;
            try
            {
                using (DLinq.CommissionDepartmentEntities DataModel = Entity.DataModel)
                {
                    payeelist = (from userCredential in DataModel.UserCredentials
                                 join
userDetail in DataModel.UserDetails on userCredential.UserCredentialId equals userDetail.UserCredentialId
                                 where (userCredential.LicenseeId == licenseeId && userCredential.IsDeleted == false && userCredential.RoleId == 3)
                                 select new GetPayeeList()
                                 {
                                     PayeeName = userDetail.NickName,
                                     UserCredentialId = userCredential.UserCredentialId
                                 }
                                ).ToList();
                }
                return payeelist.OrderBy(p => p.PayeeName).ToList();

            }
            catch (Exception ex)
            {
                ActionLogger.Logger.WriteLog("GetUserWithinLicensee exception: " + ex.Message, true);
                throw ex;
            }

            // return payeelist;
        }
        /// <summary>
        /// Used by:Ankit Khandelwal
        /// Used for:getting list of agents based on licenseeId
        /// Used from:11-06-2019
        /// </summary>
        /// <param name="licenseeId"></param>
        /// <returns></returns>
        public static List<User> GetAgentList(Guid licenseeId)
        {
            List<User> payeelist = null;
            try
            {
                using (DLinq.CommissionDepartmentEntities DataModel = Entity.DataModel)
                {
                    payeelist = (from userCredential in DataModel.UserCredentials
                                 join userDetail in DataModel.UserDetails
                                 on userCredential.UserCredentialId equals userDetail.UserCredentialId
                                 where (userCredential.LicenseeId == licenseeId && userCredential.IsDeleted == false && userCredential.RoleId == 3)
                                 select new User()
                                 {
                                     NickName = userDetail.NickName,
                                     UserCredentialID = userCredential.UserCredentialId
                                 }
                                ).ToList();
                }
                return payeelist.OrderBy(p => p.NickName).ToList();

            }
            catch (Exception ex)
            {
                ActionLogger.Logger.WriteLog("GetUserWithinLicensee exception: " + ex.Message, true);
                throw ex;
            }

            // return payeelist;
        }
        public static PrintReportOutput SaveAndPrintPayeeStatement(PayeeStatementReport report, Guid userId)
        {
            try
            {
                //PrintReportOutput result = new PrintReportOutput();

                using (DLinq.CommissionDepartmentEntities DataModel = Entity.DataModel)
                {
                    DLinq.PayeeStatementReport payeeReportData = new DLinq.PayeeStatementReport();
                    payeeReportData.Batch = report.BatcheIds;
                    payeeReportData.LicenceID = report.LicenseeId;
                    payeeReportData.Payee = report.AgentIds;
                    payeeReportData.Reports = report.ReportNames;
                    payeeReportData.ReportId = report.ReportId;
                    payeeReportData.Segments = report.SegmentIds;
                    payeeReportData.ReportOn = DateTime.Now;
                    //Added
                    payeeReportData.Zero = report.IsZero;
                    payeeReportData.IsSubTotal = report.IsSubTotal;
                    if (report.PaymentType == "Unpaid")
                        payeeReportData.PaymentType = false;
                    else if (report.PaymentType == "Paid")
                        payeeReportData.PaymentType = true;
                    else
                        payeeReportData.PaymentType = null;


                    DataModel.PayeeStatementReports.AddObject(payeeReportData);
                    DataModel.SaveChanges();
                }


                /*if (report.LicenseeId.ToString().ToLower() == "ba547e62-3812-493f-b58d-800b212a728c")
                {
                    bool isSent = PrintReportAndSendMail(report.ReportId, report.ReportNames, userId, report.Email, "payee");
                    ActionLogger.Logger.WriteLog("isSent: " + isSent, true);
                }
                else
                {
                    result = PrintReport(report.ReportId, report.ReportNames, report.ReportType);
                    ActionLogger.Logger.WriteLog("result: " + result.ToStringDump(), true);
                }*/

                PrintReportOutput result = PrintReport(report.ReportId, report.ReportNames, report.ReportType);
                ActionLogger.Logger.WriteLog("result: " + result.ToStringDump(), true);
                return result;
            }
            catch (Exception ex)
            {
                ActionLogger.Logger.WriteLog("SavePayeeStatementReport exception: " + ex.Message, true);
                throw ex;
            }
        }

        public static PrintReportOutput SaveAndPrintAuditReport(AuditReport report, Guid userId)
        {
            PrintReportOutput result = new PrintReportOutput();
            try
            {
                using (DLinq.CommissionDepartmentEntities DataModel = Entity.DataModel)
                {
                    DLinq.AuditReport auditReportData = (from f in DataModel.AuditReports where f.ReportId == report.ReportId select f).FirstOrDefault();
                    // DLinq.AuditReport auditReportData = new DLinq.AuditReport();
                    if (auditReportData == null)
                    {
                        auditReportData = new DLinq.AuditReport();
                        auditReportData.Payor = report.PayorIds;
                        auditReportData.LicenceID = report.LicenseeId;
                        auditReportData.Payee = report.AgentIds;
                        auditReportData.Reports = report.ReportNames;
                        auditReportData.ReportId = report.ReportId;
                        auditReportData.Segments = report.SegmentIds;   
                        auditReportData.ReportOn = DateTime.Now;
                        auditReportData.OrderBy = report.OrderBy;
                        //Newly added
                        auditReportData.FilterBy = report.FilterBy;

                        auditReportData.InvoiceFrom = report.FromInvoiceDate;
                        auditReportData.InvoiceTo = report.ToInvoiceDate;

                        DataModel.AddToAuditReports(auditReportData);
                        DataModel.SaveChanges();
                    }
                }

                if (report.LicenseeId.ToString().ToLower() == "ba547e62-3812-493f-b58d-800b212a728c")
                {
                    bool isSent = PrintReportAndSendMail(report.ReportId, report.ReportNames, userId, report.Email, "audit", report.LicenseeId);
                    ActionLogger.Logger.WriteLog("isSent: " + isSent, true);
                }
                else
                {
                    result = PrintReport(report.ReportId, report.ReportNames, report.ReportType);
                    ActionLogger.Logger.WriteLog("result: " + result.ToStringDump(), true);
                }

                //PrintReportOutput result = PrintReport(report.ReportId, report.ReportNames, report.ReportType);
                //ActionLogger.Logger.WriteLog("result: " + result.ToStringDump(), true);
                return result;
            }
            catch (Exception ex)
            {
                ActionLogger.Logger.WriteLog("SavePayeeStatementReport exception: " + ex.Message, true);
                throw ex;
            }
        }

        public static PrintReportOutput SaveManagementReport(ManagementReport report, Guid userId)
        {
            try
            {
                PrintReportOutput result = new PrintReportOutput();
                using (DLinq.CommissionDepartmentEntities DataModel = Entity.DataModel)
                {
                    DLinq.ManagementReport mgmtReportData = new DLinq.ManagementReport();
                    mgmtReportData.Payor = report.PayorIds;
                    mgmtReportData.Carrier = report.CarrierIds;
                    mgmtReportData.Product = report.ProductIds;
                    mgmtReportData.LicenceID = report.LicenseeId;
                    mgmtReportData.Payee = report.AgentIds;
                    mgmtReportData.Reports = report.ReportNames;
                    mgmtReportData.ReportId = report.ReportId;
                    mgmtReportData.ReportOn = DateTime.Now;
                    mgmtReportData.Segments = report.SegmentIds;
                    mgmtReportData.OrderBy = report.OrderBy;

                    mgmtReportData.EffectiveFrom = report.FromEffectiveDate;
                    mgmtReportData.EffectiveTo = report.ToEffectiveDate;
                    mgmtReportData.TrackFrom = report.FromTrackDate;
                    mgmtReportData.TrackTo = report.ToTrackDate;
                    mgmtReportData.TermFrom = report.FromTermDate;
                    mgmtReportData.TermTo = report.ToTermDate;

                    mgmtReportData.PremiumFrom = report.BeginPremium;
                    mgmtReportData.PremiumTo = report.EndPremium;
                    mgmtReportData.EnrolledFrom = report.BeginEnrolled;
                    mgmtReportData.EnrolledTo = report.EndEnrolled;
                    mgmtReportData.EligibleFrom = report.BeginEligible;
                    mgmtReportData.EligibleTo = report.EndEligible;
                    //Added 
                    mgmtReportData.EffectiveMonth = report.EffectiveMonth;

                    DLinq.MasterPolicyMode policyMode = DataModel.MasterPolicyModes.FirstOrDefault(s => s.Name == report.PolicyMode);
                    if (policyMode != null)
                        mgmtReportData.PolicyMode = policyMode.PolicyModeId;
                    else
                        mgmtReportData.PolicyMode = null;

                    DLinq.MasterPolicyTerminationReason policyTermReason = DataModel.MasterPolicyTerminationReasons.FirstOrDefault(s => s.Name == report.PolicyTermReason);
                    if (policyTermReason != null)
                        mgmtReportData.TermReason = policyTermReason.PTReasonId;
                    else
                        mgmtReportData.TermReason = null;

                    DLinq.MasterPolicyStatu policyStatus = DataModel.MasterPolicyStatus.FirstOrDefault(s => s.Name == report.PolicyType);

                    if (report.PolicyType == "Active")
                        mgmtReportData.PolicyType = 1;
                    else if (report.PolicyType == "Pending")
                        mgmtReportData.PolicyType = 2;
                    else if (report.PolicyType == "Active/Pending")
                        mgmtReportData.PolicyType = 3;
                    else if (report.PolicyType == "Terminated")
                        mgmtReportData.PolicyType = 4;
                    else if (report.PolicyType == "Deleted")
                        mgmtReportData.PolicyType = 5;
                    else if (report.PolicyType == "All")
                        mgmtReportData.PolicyType = 6;
                    else
                        mgmtReportData.PolicyType = null;

                    if (report.TrackPayment == "Yes")
                        mgmtReportData.TrackPayment = true;
                    else if (report.TrackPayment == "No")
                        mgmtReportData.TrackPayment = false;
                    else
                        mgmtReportData.TrackPayment = null;

                    mgmtReportData.InvoiceFrom = report.InvoiceFrom;
                    mgmtReportData.InvoiceTo = report.InvoiceTo;

                    ActionLogger.Logger.WriteLog("Mgmt ECR Report InvoiceFrom: " + report.InvoiceFrom + ", Invoice To: " + report.InvoiceTo, true);
                    DataModel.ManagementReports.AddObject(mgmtReportData);
                    DataModel.SaveChanges();
                }
                ActionLogger.Logger.WriteLog("Mgmt ECR Report reportName " + report.ReportNames, true);
                if (report.ReportNames.Contains("MRECR") || report.LicenseeId.ToString().ToLower() == "ba547e62-3812-493f-b58d-800b212a728c")
                {
                    bool isSent = PrintReportAndSendMail(report.ReportId, report.ReportNames, userId, report.Email, "mgmt", report.LicenseeId);
                    ActionLogger.Logger.WriteLog("isSent: " + isSent, true);
                }
                else
                {
                    result = PrintReport(report.ReportId, report.ReportNames, report.ReportType);
                    ActionLogger.Logger.WriteLog("result: " + result.ToStringDump(), true);
                }

                return result;
            }
            catch (Exception ex)
            {
                ActionLogger.Logger.WriteLog("SaveManagementReport exception: " + ex.Message, true);
                throw ex;
            }
        }


        public static bool PrintReportAndSendMail(Guid Id, string reportType, Guid userId, string EmailAddress, string type, Guid licenseeId)
        {
            ActionLogger.Logger.WriteLog("Reports!! Execution  start  of PrintReportAndSendMail " + Id + " " + reportType + "" + userId + "" + EmailAddress + "", true);

            using (DLinq.CommissionDepartmentEntities DataModel = Entity.DataModel)
            {
                string[] rpts = reportType.Split(',');
                List<string> tmpRpts = new List<string>(rpts);
                string code = tmpRpts[0];
                DLinq.MasterReportList Report = DataModel.MasterReportLists.FirstOrDefault(s => s.ReportCode == code);
                string historyID = null;
                string deviceInfo = null;
                string format = "Excel";
                Byte[] results;
                string encoding = String.Empty;
                string mimeType = String.Empty;
                string extension = String.Empty;
                Warning[] warnings = null;
                string[] streamIDs = null;

                string KeyValue = SystemConstant.GetKeyValue("ServerWebDevPath");
                WebDevPath ObjWebDevPath = WebDevPath.GetWebDevPath(KeyValue);
                ActionLogger.Logger.WriteLog("Init ReportExecutionService report " + DateTime.Now.ToLongTimeString() + ", " + KeyValue, true);
                ReportExecutionService rsExec = new ReportExecutionService();
                ActionLogger.Logger.WriteLog("Init ReportExecutionService 1 " + ObjWebDevPath.UserName + "," + ObjWebDevPath.Password + "," + ObjWebDevPath.DomainName, true);
                rsExec.Credentials = new NetworkCredential(ObjWebDevPath.UserName, ObjWebDevPath.Password, ObjWebDevPath.DomainName);
                rsExec.Timeout = System.Threading.Timeout.Infinite;
                ActionLogger.Logger.WriteLog("Init ReportExecutionService done " + DateTime.Now.ToLongTimeString() + ", " + Report.ReportGroupName, true);
                ExecutionInfo ei = rsExec.LoadReport("/MavReport/" + Report.ReportGroupName, historyID);
                ActionLogger.Logger.WriteLog("Load ReportExecutionService report done " + DateTime.Now.ToLongTimeString(), true);

                ParameterValue[] rptParameters = new ParameterValue[2];


                rptParameters[0] = new ParameterValue();
                rptParameters[0].Name = "ReportID";
                //just in case: we don't want any SQL injection strings
                rptParameters[0].Value = Id.ToString();
                rptParameters[1] = new ParameterValue();
                rptParameters[1].Name = "ReportList";
                rptParameters[1].Value = reportType;
                //render the PDF
                rsExec.SetExecutionParameters(rptParameters, "en-us");
                results = rsExec.Render(format, deviceInfo, out extension, out encoding, out mimeType, out warnings, out streamIDs);

                String path = Path.GetTempPath() + Id.ToString() + @".xls";
                ActionLogger.Logger.WriteLog("Reports!! getting path from PrintReportAndSendMail " + path + "", true);

                System.IO.FileStream fs = new System.IO.FileStream(path, System.IO.FileMode.Create);
                fs.Write(results, 0, results.Length);
                fs.Close();

                string copyPath = System.Configuration.ConfigurationSettings.AppSettings["ReportPath"] + Id.ToString() + @".xls";
                System.IO.File.Copy(path, copyPath, true);
                string emailPath = System.Configuration.ConfigurationSettings.AppSettings["EmailReportPath"] + Id.ToString() + @".xls";

                DLinq.UserDetail userDetail = DataModel.UserDetails.FirstOrDefault(s => s.UserCredentialId == userId);
                var EmailAddresses = EmailAddress;
                if (userDetail != null || EmailAddress != null)
                {
                    ActionLogger.Logger.WriteLog("Reports!!  getting userDetail and EmailAddresses from  PrintReportAndSendMail " + EmailAddresses, true);
                    if (userDetail != null)
                    {
                        EmailAddresses = (EmailAddress + "," + userDetail.Email);
                        ActionLogger.Logger.WriteLog("Reports!!  user Details is not null then we can append email of logged in user with EmailAddress entered by user  " + EmailAddresses + userDetail + "", true);
                    }
                    if (!string.IsNullOrEmpty(EmailAddresses))
                    {
                        ActionLogger.Logger.WriteLog("Reports!!sending Email when we have list of Email Address " + EmailAddresses + path, true);

                        string name = (userDetail != null) ? " " + userDetail.NickName + "," : ",";

                        string typeValue = string.Empty;
                        string typeSubject = string.Empty;
                        if (type == "mgmt")
                        {
                            typeValue = "management";
                            typeSubject = "Management Report";
                        }
                        else if (type == "audit")
                        {
                            typeValue = "audit";
                            typeSubject = "Audit Report";
                        }
                        else if (type == "payee")
                        {
                            typeValue = "payee";
                            typeSubject = "Payee Report";
                        }
                        //string body = "<html> <p>Hi" + name + "</p><p> Please find your requested " + typeValue + " reports attached with this email.</p>" +
                        //"<p> Regards, <br/> Commissions Department </p> </html>";

                        string body = "<html> <p>Hi" + name + "</p><p>Your " + typeValue + " report is ready.</p><p>Please <a href=" + emailPath + ">click here</a> to view the report.</p>" +
                        "<p> Regards, <br/> Commissions Department </p> </html>";


                        return MailServerDetail.sendMailWithAttachment(EmailAddresses, typeSubject, body, path, licenseeId);
                    }
                    else
                    {
                        ActionLogger.Logger.WriteLog("Reports!! unsuccess!! sending Email when we have list of Email Address " + EmailAddresses, true);
                        return false;
                    }
                }
                else
                {
                    ActionLogger.Logger.WriteLog("Reports!! failure!! sending Email when we have list of Email Address " + EmailAddresses, true);
                    return false;
                }
                //return MailServerDetail.sendMailWithAttachment(EmailAddress, "Report", "Mail with Report", @"E:\Projects\commission Project\Commission\Commissions.xlsx");
            }
        }
        public static PrintReportOutput PrintReport(Guid Id, string reportType, string Format)
        {
            PrintReportOutput printOutput = new PrintReportOutput();

            try
            {
                ActionLogger.Logger.WriteLog(DateTime.Now.ToLongTimeString() + " Start report with ID: " + Id + ", Format : " + Format + ", reportType: " + reportType, true);

                using (DLinq.CommissionDepartmentEntities DataModel = Entity.DataModel)
                {
                    DataModel.CommandTimeout = Convert.ToInt32(System.Configuration.ConfigurationSettings.AppSettings["SqlCommandTimeOut"]);
                    string[] rpts = reportType.Split(',');
                    List<string> tmpRpts = new List<string>(rpts);
                    string code = tmpRpts[0];
                    DLinq.MasterReportList Report = DataModel.MasterReportLists.FirstOrDefault(s => s.ReportCode == code);
                    string historyID = null;
                    string deviceInfo = null;
                    string format = null;
                    if (Format == "")
                    {
                        format = "PDF";
                    }
                    else
                    {
                        format = Format;
                    }
                    Byte[] results;
                    string encoding = String.Empty;
                    string mimeType = String.Empty;
                    string extension = String.Empty;
                    Warning[] warnings = null;
                    string[] streamIDs = null;

                    string KeyValue = SystemConstant.GetKeyValue("WebDevPath");
                    WebDevPath ObjWebDevPath = WebDevPath.GetWebDevPath(KeyValue);

                    ActionLogger.Logger.WriteLog("Init ReportExecutionService report " + DateTime.Now.ToLongTimeString(), true);

                    ReportExecutionService rsExec = new ReportExecutionService();
                    ActionLogger.Logger.WriteLog("Init ReportExecutionService 1 ", true);

                    rsExec.Credentials = new NetworkCredential(ObjWebDevPath.UserName, ObjWebDevPath.Password, ObjWebDevPath.DomainName); //new NetworkCredential("comm-dev", "c0mm@d3v", "Comm-Live");  //
                    rsExec.Timeout = System.Threading.Timeout.Infinite;
                    //rsExec.Timeout = 1000000;                    
                    ActionLogger.Logger.WriteLog("Init ReportExecutionService done " + DateTime.Now.ToLongTimeString(), true);

                    ExecutionInfo ei = rsExec.LoadReport("/MavReport/" + Report.ReportGroupName, historyID);//TestMAVService
                    ActionLogger.Logger.WriteLog("Load ReportExecutionService report done " + DateTime.Now.ToLongTimeString(), true);

                    ParameterValue[] rptParameters = new ParameterValue[2];

                    rptParameters[0] = new ParameterValue();
                    rptParameters[0].Name = "ReportID";
                    //just in case: we don't want any SQL injection strings
                    rptParameters[0].Value = Id.ToString();
                    rptParameters[1] = new ParameterValue();
                    rptParameters[1].Name = "ReportList";
                    rptParameters[1].Value = reportType;
                    //render the PDF
                    //ActionLogger.Logger.WriteLog("setting parameter" + DateTime.Now.ToLongTimeString(), true);
                    rsExec.SetExecutionParameters(rptParameters, "en-us");
                    ////ActionLogger.Logger.WriteLog("Start rendering report" + DateTime.Now.ToLongTimeString(), true);
                    //results = rsExec.Render(format, deviceInfo, out extension, out encoding, out mimeType, out warnings, out streamIDs);
                    results = rsExec.Render(format, deviceInfo, out extension, out encoding, out mimeType, out warnings, out streamIDs);
                    ActionLogger.Logger.WriteLog("Render ReportExecutionService done " + DateTime.Now.ToLongTimeString(), true);

                    String path = null;

                    // ActionLogger.Logger.WriteLog("PDF generated from service" + DateTime.Now.ToLongTimeString(), true);
                    if (format == "PDF")
                    {
                        // ActionLogger.Logger.WriteLog("Start copy to temp folder complete" + DateTime.Now.ToLongTimeString(), true);
                        //path = Path.GetTempPath() + Id.ToString() + @".pdf";
                        // ActionLogger.Logger.WriteLog("End copy to temp folder complete" + DateTime.Now.ToLongTimeString(), true);
                        path = System.Configuration.ConfigurationSettings.AppSettings["ReportPath"] + Id.ToString() + @".pdf";
                    }
                    else if (format == "Excel")
                    {
                        //ActionLogger.Logger.WriteLog("Start copy to temp folder complete" + DateTime.Now.ToLongTimeString(), true);
                        path = System.Configuration.ConfigurationSettings.AppSettings["ReportPath"] + Id.ToString() + @".xls";
                        // ActionLogger.Logger.WriteLog("Start copy to temp folder complete" + DateTime.Now.ToLongTimeString(), true);
                    }

                    ActionLogger.Logger.WriteLog("Report creation complete path: " + path, true);
                    try
                    {
                        System.IO.FileStream fs = new System.IO.FileStream(path, System.IO.FileMode.Create);

                        fs.Write(results, 0, results.Length);
                        fs.Close();
                    }
                    catch (Exception ex)
                    {
                        ActionLogger.Logger.WriteLog("Report creation excep: " + ex.Message, true);
                    }



                    //ActionLogger.Logger.WriteLog("Report creation complete" + DateTime.Now.ToLongTimeString(), true);

                    /* FileUtility ObjUpload = FileUtility.CreateClient(ObjWebDevPath.URL, ObjWebDevPath.UserName, ObjWebDevPath.Password, ObjWebDevPath.DomainName);
                     AutoResetEvent autoResetEvent = new AutoResetEvent(false);
                     ObjUpload.UploadComplete += (i, j) =>
                     {
                         autoResetEvent.Set();
                     };
                     ObjUpload.Upload(path, @"Reports/" + Path.GetFileName(path));
                     autoResetEvent.WaitOne();
                     File.Delete(path);*/
                    //ActionLogger.Logger.WriteLog("Path " + Path.GetFileName(path) + " " + DateTime.Now.ToLongTimeString(), true);

                    if (format == "PDF")
                    {
                        printOutput.FileName = Id.ToString() + @".pdf";

                    }
                    else if (format == "Excel")
                    {
                        printOutput.FileName = Id.ToString() + @".xls";
                    }
                    if (tmpRpts.Contains("PS"))
                    {
                        DLinq.PayeeStatementReport report = DataModel.PayeeStatementReports.FirstOrDefault(s => s.ReportId == Id);

                        if (report.PaymentType != null && report.PaymentType == true)
                            printOutput.ShowPaidPopup = false;
                        else
                            printOutput.ShowPaidPopup = true;

                        string[] batches = report.Batch.Split(',');
                        List<string> tmpBatches = new List<string>(batches);
                        tmpBatches.Remove(string.Empty);
                        List<Guid> batchGuids = tmpBatches.Select(s => new Guid(s)).ToList();
                        printOutput.BatchIds = batchGuids;
                    }
                    else
                    {
                        printOutput.ShowPaidPopup = false;
                        printOutput.BatchIds = null;
                    }
                }
            }
            catch (Exception ex)
            {
                ActionLogger.Logger.WriteLog(ex.StackTrace.ToString(), true);
            }
            return printOutput;

        }

        //public static PrintReportOutput PrintReport(Guid Id, string reportType)
        //{
        //    using (DLinq.CommissionDepartmentEntities DataModel = Entity.DataModel)
        //    {
        //        string[] rpts = reportType.Split(',');
        //        List<string> tmpRpts = new List<string>(rpts);
        //        string code = tmpRpts[0];
        //        DLinq.MasterReportList Report = DataModel.MasterReportLists.FirstOrDefault(s => s.ReportCode == code);
        //        string historyID = null;
        //        string deviceInfo = null;
        //        string format = "PDF";
        //        Byte[] results;
        //        string encoding = String.Empty;
        //        string mimeType = String.Empty;
        //        string extension = String.Empty;
        //        Warning[] warnings = null;
        //        string[] streamIDs = null;

        //        string KeyValue = SystemConstant.GetKeyValue("ServerWebDevPath");
        //        WebDevPath ObjWebDevPath = WebDevPath.GetWebDevPath(KeyValue);

        //        ReportExecutionService rsExec = new ReportExecutionService();
        //        rsExec.Credentials = new NetworkCredential(ObjWebDevPath.UserName, ObjWebDevPath.Password, ObjWebDevPath.DomainName);
        //        ExecutionInfo ei = rsExec.LoadReport("/MAVReport/" + Report.ReportGroupName, historyID);
        //        ParameterValue[] rptParameters = new ParameterValue[2];

        //        rptParameters[0] = new ParameterValue();
        //        rptParameters[0].Name = "ReportID";
        //        //just in case: we don't want any SQL injection strings
        //        rptParameters[0].Value = Id.ToString();
        //        rptParameters[1] = new ParameterValue();
        //        rptParameters[1].Name = "ReportList";
        //        rptParameters[1].Value = reportType;
        //        //render the PDF
        //        rsExec.SetExecutionParameters(rptParameters, "en-us");
        //        results = rsExec.Render(format, deviceInfo, out extension, out encoding, out mimeType, out warnings, out streamIDs);
        //        String path = Path.GetTempPath() + Id.ToString() + @".pdf";

        //        System.IO.FileStream fs = new System.IO.FileStream(path, System.IO.FileMode.Create);
        //        fs.Write(results, 0, results.Length);
        //        fs.Close();

        //        FileUtility ObjUpload = FileUtility.CreateClient(ObjWebDevPath.URL, ObjWebDevPath.UserName, ObjWebDevPath.Password, ObjWebDevPath.DomainName);
        //        AutoResetEvent autoResetEvent = new AutoResetEvent(false);
        //        ObjUpload.UploadComplete += (i, j) =>
        //        {
        //            autoResetEvent.Set();
        //        };
        //        ObjUpload.Upload(path, @"Reports/" + Path.GetFileName(path));
        //        autoResetEvent.WaitOne();

        //        File.Delete(path);

        //        PrintReportOutput printOutput = new PrintReportOutput();
        //        printOutput.FileName = Id.ToString() + @".pdf";

        //        if (tmpRpts.Contains("PS"))
        //        {
        //            DLinq.PayeeStatementReport report = DataModel.PayeeStatementReports.FirstOrDefault(s => s.ReportId == Id);

        //            if (report.PaymentType != null && report.PaymentType == true)
        //                printOutput.ShowPaidPopup = false;
        //            else
        //                printOutput.ShowPaidPopup = true;

        //            string[] batches = report.Batch.Split(',');
        //            List<string> tmpBatches = new List<string>(batches);
        //            tmpBatches.Remove(string.Empty);
        //            List<Guid> batchGuids = tmpBatches.Select(s => new Guid(s)).ToList();
        //            printOutput.BatchIds = batchGuids;
        //        }
        //        else
        //        {
        //            printOutput.ShowPaidPopup = false;
        //            printOutput.BatchIds = null;
        //        }
        //        return printOutput;
        //    }
        //}

        public static bool PrintReportAndSendMail(Guid Id, string reportType, Guid userId)
        {
            using (DLinq.CommissionDepartmentEntities DataModel = Entity.DataModel)
            {
                string[] rpts = reportType.Split(',');
                List<string> tmpRpts = new List<string>(rpts);
                string code = tmpRpts[0];
                DLinq.MasterReportList Report = DataModel.MasterReportLists.FirstOrDefault(s => s.ReportCode == code);
                string historyID = null;
                string deviceInfo = null;
                string format = "PDF";
                Byte[] results;
                string encoding = String.Empty;
                string mimeType = String.Empty;
                string extension = String.Empty;
                Warning[] warnings = null;
                string[] streamIDs = null;

                string KeyValue = SystemConstant.GetKeyValue("ServerWebDevPath");
                WebDevPath ObjWebDevPath = WebDevPath.GetWebDevPath(KeyValue);
                ReportExecutionService rsExec = new ReportExecutionService();
                rsExec.Credentials = new NetworkCredential(ObjWebDevPath.UserName, ObjWebDevPath.Password, ObjWebDevPath.DomainName);
                rsExec.Timeout = System.Threading.Timeout.Infinite;
                ExecutionInfo ei = rsExec.LoadReport("/MavReport/" + Report.ReportGroupName, historyID);
                ParameterValue[] rptParameters = new ParameterValue[2];

                rptParameters[0] = new ParameterValue();
                rptParameters[0].Name = "ReportID";
                //just in case: we don't want any SQL injection strings
                rptParameters[0].Value = Id.ToString();
                rptParameters[1] = new ParameterValue();
                rptParameters[1].Name = "ReportList";
                rptParameters[1].Value = reportType;
                //render the PDF
                rsExec.SetExecutionParameters(rptParameters, "en-us");
                results = rsExec.Render(format, deviceInfo, out extension, out encoding, out mimeType, out warnings, out streamIDs);
                String path = Path.GetTempPath() + Id.ToString() + @".pdf";


                System.IO.FileStream fs = new System.IO.FileStream(path, System.IO.FileMode.Create);
                fs.Write(results, 0, results.Length);
                fs.Close();

                DLinq.UserDetail userDetail = DataModel.UserDetails.FirstOrDefault(s => s.UserCredentialId == userId);
                if (userDetail != null)
                {
                    if (!string.IsNullOrEmpty(userDetail.Email))
                        return MailServerDetail.sendMailWithAttachment(userDetail.Email, "Report", "Mail with Report", path);
                    else
                        return false;
                }
                else
                    return false;

                // return MailServerDetail.sendMailWithAttachment("vinod.yadav@hanusoftware.com", "Report", "Mail with Report", path);

            }
        }

        [DataMember]
        public Guid Id { get; set; }
        [DataMember]
        public string Code { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string GroupName { get; set; }
        [DataMember]
        public string Description { get; set; }
    }


    [DataContract]
    public class PrintReportOutput
    {
        [DataMember]
        public string FileName { get; set; }
        [DataMember]
        public bool ShowPaidPopup { get; set; }
        [DataMember]
        public List<Guid> BatchIds { get; set; }
    }

    [DataContract]
    public class PayeeStatementReport
    {
        [DataMember]
        public string Email { get; set; }
        [DataMember]
        public Guid ReportId { get; set; }
        [DataMember]
        public Guid LicenseeId { get; set; }
        [DataMember]
        public string BatcheIds { get; set; }
        [DataMember]
        public string AgentIds { get; set; }
        [DataMember]
        public string ReportNames { get; set; }
        [DataMember]
        public string PaymentType { get; set; }
        [DataMember]
        public string ReportType { get; set; }
        [DataMember]
        public bool IsZero { get; set; }
        [DataMember]
        public bool IsSubTotal { get; set; }
        [DataMember]
        public string SegmentIds { get; set; }
    }

    [DataContract]
    public class AuditReport
    {
        [DataMember]
        public string Email { get; set; }
        [DataMember]
        public Guid ReportId { get; set; }
        [DataMember]
        public Guid LicenseeId { get; set; }
        [DataMember]
        public string PayorIds { get; set; }
        [DataMember]
        public string AgentIds { get; set; }
        [DataMember]
        public string ReportNames { get; set; }
        [DataMember]
        public string ReportType { get; set; }
        [DataMember]
        public string SegmentIds { get; set; }

        //[DataMember]
        //public DateTime? FromInvoiceDate { get; set; }
        DateTime? _FromInvoiceDate;
        [DataMember]
        public DateTime? FromInvoiceDate
        {
            get
            {
                return _FromInvoiceDate;
            }
            set
            {
                _FromInvoiceDate = value;
                if (value != null && string.IsNullOrEmpty(FromInvoiceDateString))
                {
                    FromInvoiceDateString = value.ToString();
                }
            }
        }
        string _FromInvoiceDateString;
        [DataMember]
        public string FromInvoiceDateString
        {
            get
            {
                return _FromInvoiceDateString;
            }
            set
            {
                _FromInvoiceDateString = value;
                if (FromInvoiceDate == null && !string.IsNullOrEmpty(_FromInvoiceDateString))
                {
                    DateTime dt;
                    DateTime.TryParse(_FromInvoiceDateString, out dt);
                    FromInvoiceDate = dt;
                }
            }
        }

        //[DataMember]
        //public DateTime? ToInvoiceDate { get; set; }
        DateTime? _ToInvoiceDate;
        [DataMember]
        public DateTime? ToInvoiceDate
        {
            get
            {
                return _ToInvoiceDate;
            }
            set
            {
                _ToInvoiceDate = value;
                if (value != null && string.IsNullOrEmpty(ToInvoiceDateString))
                {
                    ToInvoiceDateString = value.ToString();
                }
            }
        }
        string _ToInvoiceDateString;
        [DataMember]
        public string ToInvoiceDateString
        {
            get
            {
                return _ToInvoiceDateString;
            }
            set
            {
                _ToInvoiceDateString = value;
                if (ToInvoiceDate == null && !string.IsNullOrEmpty(_ToInvoiceDateString))
                {
                    DateTime dt;
                    DateTime.TryParse(_ToInvoiceDateString, out dt);
                    ToInvoiceDate = dt;
                }
            }
        }

        [DataMember]
        public string OrderBy { get; set; }
        //Added filter type
        [DataMember]
        public int FilterBy { get; set; }
    }

    [DataContract]
    public class ManagementReport
    {
        [DataMember]
        public string Email { get; set; }
        [DataMember]
        public Guid ReportId { get; set; }
        [DataMember]
        public Guid LicenseeId { get; set; }
        [DataMember]
        public string PayorIds { get; set; }
        [DataMember]
        public string CarrierIds { get; set; }
        [DataMember]
        public string ProductIds { get; set; }
        [DataMember]
        public string AgentIds { get; set; }
        [DataMember]
        public string ReportNames { get; set; }
        [DataMember]
        public string PolicyType { get; set; }
        [DataMember]
        public string PolicyMode { get; set; }
        [DataMember]
        public string PolicyTermReason { get; set; }
        [DataMember]
        public string TrackPayment { get; set; }
        //add
        [DataMember]
        public string SegmentIds { get; set; }

        #region Date types
        //[DataMember]
        //public DateTime? FromEffectiveDate { get; set; }
        DateTime? _FromEffectiveDate;
        [DataMember]
        public DateTime? FromEffectiveDate
        {
            get
            {
                return _FromEffectiveDate;
            }
            set
            {
                _FromEffectiveDate = value;
                if (value != null && string.IsNullOrEmpty(FromEffectiveDateString))
                {
                    FromEffectiveDateString = value.ToString();
                }
            }
        }
        string _FromEffectiveDateString;
        [DataMember]
        public string FromEffectiveDateString
        {
            get
            {
                return _FromEffectiveDateString;
            }
            set
            {
                _FromEffectiveDateString = value;
                if (FromEffectiveDate == null && !string.IsNullOrEmpty(_FromEffectiveDateString))
                {
                    DateTime dt;
                    DateTime.TryParse(_FromEffectiveDateString, out dt);
                    FromEffectiveDate = dt;
                }
            }
        }
        //[DataMember]
        //public DateTime? ToEffectiveDate { get; set; }
        DateTime? _ToEffectiveDate;
        [DataMember]
        public DateTime? ToEffectiveDate
        {
            get
            {
                return _ToEffectiveDate;
            }
            set
            {
                _ToEffectiveDate = value;
                if (value != null && string.IsNullOrEmpty(ToEffectiveDateString))
                {
                    ToEffectiveDateString = value.ToString();
                }
            }
        }
        string _ToEffectiveDateString;
        [DataMember]
        public string ToEffectiveDateString
        {
            get
            {
                return _ToEffectiveDateString;
            }
            set
            {
                _ToEffectiveDateString = value;
                if (ToEffectiveDate == null && !string.IsNullOrEmpty(_ToEffectiveDateString))
                {
                    DateTime dt;
                    DateTime.TryParse(_ToEffectiveDateString, out dt);
                    ToEffectiveDate = dt;
                }
            }
        }
        //[DataMember]
        //public DateTime? FromTrackDate { get; set; }
        DateTime? _FromTrackDate;
        [DataMember]
        public DateTime? FromTrackDate
        {
            get
            {
                return _FromTrackDate;
            }
            set
            {
                _FromTrackDate = value;
                if (value != null && string.IsNullOrEmpty(FromTrackDateString))
                {
                    FromTrackDateString = value.ToString();
                }
            }
        }
        string _FromTrackDateString;
        [DataMember]
        public string FromTrackDateString
        {
            get
            {
                return _FromTrackDateString;
            }
            set
            {
                _FromTrackDateString = value;
                if (FromTrackDate == null && !string.IsNullOrEmpty(_FromTrackDateString))
                {
                    DateTime dt;
                    DateTime.TryParse(_FromTrackDateString, out dt);
                    FromTrackDate = dt;
                }
            }
        }
        //[DataMember]
        //public DateTime? ToTrackDate { get; set; }
        DateTime? _ToTrackDate;
        [DataMember]
        public DateTime? ToTrackDate
        {
            get
            {
                return _ToTrackDate;
            }
            set
            {
                _ToTrackDate = value;
                if (value != null && string.IsNullOrEmpty(ToTrackDateString))
                {
                    ToTrackDateString = value.ToString();
                }
            }
        }
        string _ToTrackDateString;
        [DataMember]
        public string ToTrackDateString
        {
            get
            {
                return _ToTrackDateString;
            }
            set
            {
                _ToTrackDateString = value;
                if (ToTrackDate == null && !string.IsNullOrEmpty(_ToTrackDateString))
                {
                    DateTime dt;
                    DateTime.TryParse(_ToTrackDateString, out dt);
                    ToTrackDate = dt;
                }
            }
        }
        //[DataMember]
        //public DateTime? FromTermDate { get; set; }
        DateTime? _FromTermDate;
        [DataMember]
        public DateTime? FromTermDate
        {
            get
            {
                return _FromTermDate;
            }
            set
            {
                _FromTermDate = value;
                if (value != null && string.IsNullOrEmpty(FromTermDateString))
                {
                    FromTermDateString = value.ToString();
                }
            }
        }
        string _FromTermDateString;
        [DataMember]
        public string FromTermDateString
        {
            get
            {
                return _FromTermDateString;
            }
            set
            {
                _FromTermDateString = value;
                if (FromTermDate == null && !string.IsNullOrEmpty(_FromTermDateString))
                {
                    DateTime dt;
                    DateTime.TryParse(_FromTermDateString, out dt);
                    FromTermDate = dt;
                }
            }
        }

        //[DataMember]
        //public DateTime? ToTermDate { get; set; }
        DateTime? _ToTermDate;
        [DataMember]
        public DateTime? ToTermDate
        {
            get
            {
                return _ToTermDate;
            }
            set
            {
                _ToTermDate = value;
                if (value != null && string.IsNullOrEmpty(ToTermDateString))
                {
                    ToTermDateString = value.ToString();
                }
            }
        }
        string _ToTermDateString;
        [DataMember]
        public string ToTermDateString
        {
            get
            {
                return _ToTermDateString;
            }
            set
            {
                _ToTermDateString = value;
                if (ToTermDate == null && !string.IsNullOrEmpty(_ToTermDateString))
                {
                    DateTime dt;
                    DateTime.TryParse(_ToTermDateString, out dt);
                    ToTermDate = dt;
                }
            }
        }
        #endregion

        [DataMember]
        public decimal? BeginPremium { get; set; }
        [DataMember]
        public decimal? EndPremium { get; set; }
        [DataMember]
        public int? BeginEnrolled { get; set; }
        [DataMember]
        public int? EndEnrolled { get; set; }
        [DataMember]
        public int? BeginEligible { get; set; }
        [DataMember]
        public int? EndEligible { get; set; }
        [DataMember]
        public string OrderBy { get; set; }
        [DataMember]
        public string ReportType { get; set; }
        //[DataMember]
        //public DateTime? InvoiceFrom { get; set; }
        DateTime? _FromInvoiceDate;
        [DataMember]
        public DateTime? InvoiceFrom
        {
            get
            {
                return _FromInvoiceDate;
            }
            set
            {
                _FromInvoiceDate = value;
                if (value != null && string.IsNullOrEmpty(InvoiceFromString))
                {
                    InvoiceFromString = value.ToString();
                }
            }
        }
        string _FromInvoiceDateString;
        [DataMember]
        public string InvoiceFromString
        {
            get
            {
                return _FromInvoiceDateString;
            }
            set
            {
                _FromInvoiceDateString = value;
                if (InvoiceFrom == null && !string.IsNullOrEmpty(_FromInvoiceDateString))
                {
                    DateTime dt;
                    DateTime.TryParse(_FromInvoiceDateString, out dt);
                    InvoiceFrom = dt;
                }
            }
        }
        //[DataMember]
        //public DateTime? InvoiceTo { get; set; }
        DateTime? _ToInvoiceDate;
        [DataMember]
        public DateTime? InvoiceTo
        {
            get
            {
                return _ToInvoiceDate;
            }
            set
            {
                _ToInvoiceDate = value;
                if (value != null && string.IsNullOrEmpty(InvoiceToString))
                {
                    InvoiceToString = value.ToString();
                }
            }
        }
        string _ToInvoiceDateString;
        [DataMember]
        public string InvoiceToString
        {
            get
            {
                return _ToInvoiceDateString;
            }
            set
            {
                _ToInvoiceDateString = value;
                if (InvoiceTo == null && !string.IsNullOrEmpty(_ToInvoiceDateString))
                {
                    DateTime dt;
                    DateTime.TryParse(_ToInvoiceDateString, out dt);
                    InvoiceTo = dt;
                }
            }
        }
        [DataMember]
        public int? EffectiveMonth { get; set; }

    }
}
