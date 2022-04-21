using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Runtime.Serialization;

namespace MyAgencyVault.BusinessLibrary
{

    public class AllRevenueData
    {
        public string Title { get; set; }
        public string YTDCurrentLabel { get; set; }
        public string YTDCurrentdata { get; set; }
        public string YTDLastLabel { get; set; }
        public string YTDLastData { get; set; }
        public string YTDGainLoss { get; set; }
        public string YTDGainLossPercent { get; set; }
        public string YTDGainLossLabel { get; set; }


        public string MTDCurrentLabel { get; set; }
        public string MTDCurrentdata { get; set; }
        public string MTDLastLabel { get; set; }
        public string MTDLastData { get; set; }
        public string MTDGainLoss { get; set; }
        public string MTDGainLossPercent { get; set; }
        public string MTDGainLossLabel { get; set; }


        public string YearCurrentLabel { get; set; }
        public string YearCurrentdata { get; set; }
        public string YearLastLabel { get; set; }
        public string YearLastData { get; set; }
        public string YearGainLoss { get; set; }
        public string YearGainLossPercent { get; set; }
        public string YearGainLossLabel { get; set; }

    }

    public class AgentData
    {
        public string NickName { get; set; }
        public string NetRevenue { get; set; }

        public string GrossRevenue { get; set; }
    }

    public class AgentExportDetail
    {
        public string Client { get; set; }
        public string PolicyNumber { get; set; }
        public string PayorName { get; set; }
        public string CarrierName { get; set; }
        public string ProductName { get; set; }
        public string ProductType { get; set; }
        public string CompType { get; set; }
        public string NickName { get; set; }
        public string NetRevenue { get; set; }
        public string GrossRevenue { get; set; }
    }

    public class RevenueLOCData
    {
        public string Product { get; set; }
        public string GrossRevenue { get; set; }

        public string NetRevenue { get; set; }
    }
    public class NewPolicyList
    {
        public string ClientName { get; set; }
        public string PAC { get; set; }

    }

    public class RenewalsList
    {
        public string Months { get; set; }
        public string AnnualizedRevenue { get; set; }
        public string TotalPercentage { get; set; }
    }

    public class ReceivablesList
    {
        public string PolicyNumber { get; set; }
        public string ClientName { get; set; }
        public string Payor { get; set; }
        public string Carrier { get; set; }
        public string Effective { get; set; }
        public string product { get; set; }
        public string RevenueDue { get; set; }
    }




    public class LineGraphData
    {
        public string type { get; set; }
        public string label { get; set; }

        public string color { get; set; }

        public int lineTension { get; set; }
        public List<double> data { get; set; }
        public bool fill { get; set; }


    }
    public class ClientName
    {
        public string Client { get; set; }
    }
    public class Dashboard
    {

        public static List<AgentExportDetail> GetDashboardAgentExportDetails(Guid LicenseeID, DateTime StartDate, DateTime EndDate, string Filter = "Agent")
        {
            List<AgentExportDetail> exportDataList = new List<AgentExportDetail>();
            try
            {
                ActionLogger.Logger.WriteLog("GetDashboardAgentExportDetails request: " + LicenseeID + ", Start: " + StartDate + ", Enddate: " + EndDate, true);
                using (SqlConnection con = new SqlConnection(DBConnection.GetConnectionString()))
                {
                    using (SqlCommand cmd = new SqlCommand("usp_getDetailedExportForAgents", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandTimeout = 0;
                        cmd.Parameters.AddWithValue("@LicenseeID", LicenseeID);
                        cmd.Parameters.AddWithValue("@StartDate", StartDate);
                        cmd.Parameters.AddWithValue("@EndDate", EndDate);
                        cmd.Parameters.AddWithValue("@filter", Filter);
                        con.Open();
                        SqlDataReader dr = cmd.ExecuteReader();
                        while (dr.Read())
                        {
                            AgentExportDetail exportData = new AgentExportDetail();
                            exportData.NickName = dr.IsDBNull("Name") ? "" : Convert.ToString(dr["Name"]);
                            exportData.Client = Convert.ToString(dr["Client"]);
                            exportData.PolicyNumber = Convert.ToString(dr["Policy#"]) + "      ";
                            exportData.PayorName = Convert.ToString(dr["PayorName"]);
                            exportData.CarrierName = Convert.ToString(dr["CarrierName"]);
                            exportData.ProductName = Convert.ToString(dr["ProductName"]);
                            exportData.ProductType = Convert.ToString(dr["ProductType"]);
                            exportData.CompType = Convert.ToString(dr["CompType"]);

                            double agentTotal = 0;
                            double.TryParse(Convert.ToString(dr["Net"]), out agentTotal);
                            exportData.NetRevenue = Convert.ToString(agentTotal);

                            double grossTotal = 0;
                            double.TryParse(Convert.ToString(dr["Gross"]), out grossTotal);
                            exportData.GrossRevenue = Convert.ToString(grossTotal);
                            exportDataList.Add(exportData);
                        }
                        dr.Close();
                    }
                    ActionLogger.Logger.WriteLog("GetDashboardAgentExportDetails success: " + LicenseeID, true);
                }
            }
            catch (Exception ex)
            {
                ActionLogger.Logger.WriteLog("GetDashboardAgentExportDetails exception: " + ex.Message, true);
                throw ex;
            }
            return exportDataList;
        }
        public static List<AgentData> GetDashboardAgents(Guid LicenseeID, DateTime StartDate, DateTime EndDate, out List<AgentData> exportDataList, string Filter = "Agent", bool isFirstTimeList = false)
        {
            List<AgentData> lstAgents = new List<AgentData>();
            exportDataList = new List<AgentData>();
            try
            {
                var storedProcedureName = isFirstTimeList == true ? "usp_GetFirstTimeAgentsDataDashboard" : "usp_getAgentsDataDashboard";
                ActionLogger.Logger.WriteLog("GetDashboardAgents request: " + LicenseeID + ", Start: " + StartDate + ", Enddate: " + EndDate, true);
                using (SqlConnection con = new SqlConnection(DBConnection.GetConnectionString()))
                {
                    using (SqlCommand cmd = new SqlCommand(storedProcedureName, con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandTimeout = 0;
                        cmd.Parameters.AddWithValue("@LicenseeID", LicenseeID);
                        cmd.Parameters.AddWithValue("@StartDate", StartDate);
                        cmd.Parameters.AddWithValue("@EndDate", EndDate);
                        cmd.Parameters.AddWithValue("@filter", Filter);
                        con.Open();
                        SqlDataReader dr = cmd.ExecuteReader();
                        while (dr.Read())
                        {
                            AgentData objAgent = new AgentData();
                            AgentData exportData = new AgentData();
                            objAgent.NickName = dr.IsDBNull("NickName") ? "" : Convert.ToString(dr["NickName"]);
                            exportData.NickName = dr.IsDBNull("NickName") ? "" : Convert.ToString(dr["NickName"]);
                            double agentTotal = 0;
                            double.TryParse(Convert.ToString(dr["AgentTotal"]), out agentTotal);
                            objAgent.NetRevenue = agentTotal.ToString("C");
                            exportData.NetRevenue = Convert.ToString(agentTotal);

                            double grossTotal = 0;
                            double.TryParse(Convert.ToString(dr["GrossTotal"]), out grossTotal);
                            objAgent.GrossRevenue = grossTotal.ToString("C");
                            exportData.GrossRevenue = Convert.ToString(grossTotal);
                            lstAgents.Add(objAgent);
                            exportDataList.Add(exportData);
                        }
                        dr.Close();
                    }
                    ActionLogger.Logger.WriteLog("GetDashboardAgents success: " + LicenseeID, true);
                }
            }
            catch (Exception ex)
            {
                ActionLogger.Logger.WriteLog("GetDashboardAgents exception: " + ex.Message, true);
                throw ex;
            }
            return lstAgents;
        }

        public static void GetDashboardCount(Guid LicenseeID, out int agent, out int client)
        {
            ActionLogger.Logger.WriteLog("GetDashboardCount request: " + LicenseeID, true);
            agent = 0; client = 0;
            try
            {
                using (SqlConnection con = new SqlConnection(DBConnection.GetConnectionString()))
                {
                    using (SqlCommand cmd = new SqlCommand("usp_getDashboardDataCount", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandTimeout = 0;
                        cmd.Parameters.AddWithValue("@licenseeId", LicenseeID);
                        con.Open();
                        SqlDataReader dr = cmd.ExecuteReader();
                        while (dr.Read())
                        {
                            agent = dr.IsDBNull("Agents") ? 0 : Convert.ToInt32(dr["Agents"]);
                            client = dr.IsDBNull("Clients") ? 0 : Convert.ToInt32(dr["Clients"]);
                        }
                        dr.Close();
                    }
                    ActionLogger.Logger.WriteLog("GetDashboardCount success: " + LicenseeID, true);
                }
            }
            catch (Exception ex)
            {
                ActionLogger.Logger.WriteLog("GetDashboardCount exception: " + ex.Message, true);
                throw ex;
            }
        }

        /// <summary>
        /// Create by :Ankit khandelwal
        /// CreatedOn:31-10-2019
        /// Purpose:Getting List of New Policies
        /// </summary>
        /// <param name="licenseeId"></param>
        /// <returns></returns>
        public static List<NewPolicyList> GetNewPolicyList(Guid licenseeId, out List<NewPolicyList> exportDataList)
        {
            ActionLogger.Logger.WriteLog("GetNewPolicyList: processing begins licenseeId:" + licenseeId, true);
            List<NewPolicyList> newList = new List<NewPolicyList>();
            exportDataList = new List<NewPolicyList>();
            try
            {
                using (SqlConnection con = new SqlConnection(DBConnection.GetConnectionString()))
                {
                    using (SqlCommand cmd = new SqlCommand("usp_GetDashboardNewBusinessList ", con))
                    {
                        //  This stored procedure is used 'usp_getNewPoliciesList' for getting list
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandTimeout = 0;
                        cmd.Parameters.AddWithValue("@licenseeId", licenseeId);
                        con.Open();
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            NewPolicyList data = new NewPolicyList();
                            NewPolicyList exportData = new NewPolicyList();
                            data.ClientName = reader.IsDBNull("ClientName") ? "" : Convert.ToString(reader["ClientName"]);
                            exportData.ClientName = reader.IsDBNull("ClientName") ? "" : Convert.ToString(reader["ClientName"]);
                            double PAC = 0;
                            double.TryParse(Convert.ToString(reader["PAC"]), out PAC);
                            exportData.PAC = Convert.ToString(PAC);
                            data.PAC = PAC.ToString("C");
                            newList.Add(data);
                            exportDataList.Add(exportData);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ActionLogger.Logger.WriteLog("GetNewPolicyList: Exception occurs with licenseeId:" + licenseeId + "Exception:" + ex.InnerException, true);
                throw ex;
            }
            return newList;
        }


        public static List<ReceivablesList> GetNReceivablesList(Guid licenseeId, string numberOfDays)
        {

            if (numberOfDays == "1")
            {
                DateTime todaydate = DateTime.Now;
                var Year = todaydate.Year;
                var Month = todaydate.Month;
                todaydate = Convert.ToDateTime(Month + "/" + "01" + " /" + Year);
                DateTime calculatedDate = todaydate - TimeSpan.FromDays(30);
            }


            ActionLogger.Logger.WriteLog("GetNewPolicyList: processing begins licenseeId:" + licenseeId, true);
            List<ReceivablesList> newList = new List<ReceivablesList>();
            try
            {
                using (SqlConnection con = new SqlConnection(DBConnection.GetConnectionString()))
                {
                    using (SqlCommand cmd = new SqlCommand("usp_GetReceivableList", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandTimeout = 0;
                        cmd.Parameters.AddWithValue("@licenseeId", licenseeId);
                        con.Open();
                        SqlDataReader reader = cmd.ExecuteReader();
                        //while (reader.Read())
                        //{
                        //    NewPolicyList data = new NewPolicyList();
                        //    data.ClientName = reader.IsDBNull("ClientName") ? "" : Convert.ToString(reader["ClientName"]);
                        //    data.PAC = reader.IsDBNull("PAC") ? "$0.00" : Convert.ToString(reader["PAC"]);
                        //    newList.Add(data);
                        //}
                    }
                }
            }
            catch (Exception ex)
            {
                ActionLogger.Logger.WriteLog("GetNewPolicyList: Exception occurs with licenseeId:" + licenseeId + "Exception:" + ex.InnerException, true);
                throw ex;
            }
            return newList;
        }




        /// <summary>
        /// 
        /// </summary>
        /// <param name="licenseeId"></param>
        /// <returns></returns>
        public static List<RenewalsList> GetRenewalsList(Guid licenseeId, out List<RenewalsList> exportDataList)
        {
            ActionLogger.Logger.WriteLog("GetRenewalsList: processing begins licenseeId:" + licenseeId, true);
            List<RenewalsList> newList = new List<RenewalsList>();
            exportDataList = new List<RenewalsList>();
            try
            {
                using (SqlConnection con = new SqlConnection(DBConnection.GetConnectionString()))
                {
                    using (SqlCommand cmd = new SqlCommand("usp_GetDashboardRenewalsRevenue", con))
                    // inside this procedure for listing call usp_getrenewalsfordashboard
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandTimeout = 0;
                        cmd.Parameters.AddWithValue("@licenseeId", licenseeId);
                        con.Open();
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            RenewalsList data = new RenewalsList();
                            RenewalsList exportData = new RenewalsList();
                            data.Months = reader.IsDBNull("MonthName") ? "" : Convert.ToString(reader["MonthName"]);
                            exportData.Months = reader.IsDBNull("MonthName") ? "" : Convert.ToString(reader["MonthName"]);
                            double AnnualizedRevenue = 0;
                            double.TryParse(Convert.ToString(reader["AnnualizedRevenue"]), out AnnualizedRevenue);
                            exportData.AnnualizedRevenue = Convert.ToString(AnnualizedRevenue);
                            data.AnnualizedRevenue = AnnualizedRevenue.ToString("C");

                            data.TotalPercentage = reader.IsDBNull("% of Total") ? "0.00%" : Convert.ToString(reader["% of Total"]);
                            exportData.TotalPercentage = data.TotalPercentage;
                            newList.Add(data);
                            exportDataList.Add(exportData);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ActionLogger.Logger.WriteLog("GetRenewalsList: Exception occurs with licenseeId:" + licenseeId + "Exception:" + ex.InnerException, true);
                throw ex;
            }
            return newList;
        }


        public static List<LineGraphData> GetDashboardRevenueList(Guid LicenseeID, Guid UserCredentialID, int startMonth, int endMonth, List<int> lstyears, out List<LineGraphData> netList, out AllRevenueData grossNumbers, out AllRevenueData netNumbers, out Guid clientID, out string refreshTime)
        {
            List<LineGraphData> lstdata = new List<LineGraphData>();
            netList = new List<LineGraphData>();
            grossNumbers = new AllRevenueData();
            netNumbers = new AllRevenueData();
            clientID = Guid.Empty;
            refreshTime = Convert.ToString(DateTime.Now);
            ActionLogger.Logger.WriteLog("GetDashboardRevenuePeryear request: " + LicenseeID + ", UserCredentialID" + UserCredentialID + ", start; " + startMonth + ", end: " + endMonth, true);
            try
            {
                foreach (int year in lstyears)
                {
                    LineGraphData netRevenue = new LineGraphData();
                    LineGraphData data = GetDashboardRevenuePeryear(LicenseeID, UserCredentialID, startMonth, endMonth, year, out netRevenue, out grossNumbers, out netNumbers, out clientID, out refreshTime);
                    data.color = "";
                    netRevenue.color = "";
                    lstdata.Add(data);
                    netList.Add(netRevenue);
                }
            }
            catch (Exception ex)
            {
                ActionLogger.Logger.WriteLog("GetDashboardRevenuePeryear exception: " + ex.Message, true);
                throw ex;
            }
            return lstdata;
        }

        public static LineGraphData GetDashboardRevenuePeryear(Guid LicenseeID, Guid UserCredentialID, int startMonth, int endMonth, int year, out LineGraphData netRevenue, out AllRevenueData grossNumbers, out AllRevenueData netNumbers, out Guid clientID, out string refreshTime)
        {
            ActionLogger.Logger.WriteLog("GetDashboardRevenuePeryear request: " + LicenseeID + ", year: " + year, true);
            var rand = new Random();
            grossNumbers = new AllRevenueData();
            grossNumbers.Title = "Gross Revenue";
            netNumbers = new AllRevenueData();
            netNumbers.Title = "Net Revenue";

            clientID = Guid.Empty;
            refreshTime = Convert.ToString(DateTime.Now);
            LineGraphData obj = new LineGraphData();
            obj.label = year.ToString();

            netRevenue = new LineGraphData();
            netRevenue.label = year.ToString();

            try
            {
                List<double> lstRevenue = new List<double>();

                using (SqlConnection con = new SqlConnection(DBConnection.GetConnectionString()))
                {
                    // for logic goes to store procedure: usp_getNewPoliciesList
                    using (SqlCommand cmd = new SqlCommand("usp_getDashboardRevenueData", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandTimeout = 0;
                        cmd.Parameters.AddWithValue("@LicenseeID", LicenseeID);
                        cmd.Parameters.AddWithValue("@UserCredentialID", UserCredentialID);
                        cmd.Parameters.AddWithValue("@startMonth", startMonth);
                        cmd.Parameters.AddWithValue("@endMonth", endMonth);
                        cmd.Parameters.AddWithValue("@year", year);
                        con.Open();
                        SqlDataAdapter adap = new SqlDataAdapter();
                        adap.SelectCommand = cmd;
                        DataSet ds = new DataSet();
                        adap.Fill(ds);

                        //Table for gross revenue 
                        DataTable dt = ds.Tables[0];
                        for (int j = 0; j < dt.Rows.Count; j++)
                        {
                            string strRevenue = Convert.ToString(dt.Rows[j]["GrossRevenue"]);
                            double revenue = 0;
                            double.TryParse(strRevenue, out revenue);
                            lstRevenue.Add(revenue);
                        }
                        obj.data = lstRevenue;

                        //Table of net revenue 
                        dt = ds.Tables[0];
                        lstRevenue = new List<double>();
                        for (int j = 0; j < dt.Rows.Count; j++)
                        {
                            string strRevenue = Convert.ToString(dt.Rows[j]["NetRevenue"]);
                            double revenue = 0;
                            double.TryParse(strRevenue, out revenue);
                            lstRevenue.Add(revenue);
                        }
                        netRevenue.data = lstRevenue;


                        //All data from top labels 
                        dt = ds.Tables[1];
                        if (dt.Rows.Count > 0)
                        {
                            //Handled the January month - to represent Dec data of previous year.
                            int month = (DateTime.Now.Month - 1 == 0) ? 12 : DateTime.Now.Month - 1;
                            int curYear = (DateTime.Now.Month - 1 == 0) ? DateTime.Now.Year - 1 : DateTime.Now.Year;

                            ActionLogger.Logger.WriteLog("GetDashboardRevenuePeryear old Licensee data process: " + LicenseeID, true);
                            grossNumbers.YTDCurrentLabel = "Jan - " + CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(month) + " " + curYear;

                            double current = 0; double last = 0;
                            double.TryParse(Convert.ToString(dt.Rows[0]["CurrentYTD"]), out current);
                            grossNumbers.YTDCurrentdata = current.ToString("C0");

                            double.TryParse(Convert.ToString(dt.Rows[0]["LastYTD"]), out last);
                            grossNumbers.YTDLastData = last.ToString("C0");
                            grossNumbers.YTDLastLabel = "Jan - " + CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(month) + " " + (curYear - 1).ToString();

                            double denominator = last; // Convert.ToDouble(grossNumbers.YTDLastData.Replace("$", ""));
                            double currentGross = current - denominator; //Convert.ToDouble(grossNumbers.YTDCurrentdata.Replace("$", ""))
                            double gainPercent = (denominator == 0) ? 0 : Convert.ToInt32((currentGross / denominator) * 100);
                            grossNumbers.YTDGainLoss = (gainPercent == 0) ? "NA" : currentGross.ToString("C0"); // + "(" + string.Format("{0:0.##}%", gainPercent) + ")";  //gainPercent.ToString("#.##") + "%)";
                            grossNumbers.YTDGainLossPercent = "(" + string.Format("{0}%", gainPercent) + ")"; //string.Format("{0:0.##}%", gainPercent)
                            grossNumbers.YTDGainLossLabel = (gainPercent > 0) ? "Gain" : (gainPercent < 0) ? "Loss" : "Gain/Loss";

                            current = last = 0;
                            double.TryParse(Convert.ToString(dt.Rows[0]["CurrentMTD"]), out current);
                            grossNumbers.MTDCurrentdata = current.ToString("C0");
                            grossNumbers.MTDCurrentLabel = CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(month) + " " + curYear;
                            double.TryParse(Convert.ToString(dt.Rows[0]["LastMTD"]), out last);
                            grossNumbers.MTDLastData = last.ToString("C0");
                            grossNumbers.MTDLastLabel = CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(month) + " " + (curYear - 1).ToString();
                            denominator = last; // Convert.ToDouble(grossNumbers.MTDLastData.Replace("$", ""));
                            currentGross = current - denominator;
                            gainPercent = (denominator == 0) ? 0 : Convert.ToInt32((currentGross / denominator) * 100);
                            grossNumbers.MTDGainLoss = (gainPercent == 0) ? "NA" : currentGross.ToString("C0");// + "(" + string.Format("{0:0.##}%", gainPercent) + ")";
                            grossNumbers.MTDGainLossPercent = "(" + string.Format("{0}%", gainPercent) + ")";
                            grossNumbers.MTDGainLossLabel = (gainPercent > 0) ? "Gain" : (gainPercent < 0) ? "Loss" : "Gain/Loss";


                            current = last = 0;
                            double.TryParse(Convert.ToString(dt.Rows[0]["CurrentYear"]), out current);
                            grossNumbers.YearCurrentdata = current.ToString("C0");
                            grossNumbers.YearCurrentLabel = CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(DateTime.Now.AddMonths(-12).Month) + " " + (DateTime.Now.AddYears(-1)).ToString("yy") + " - " + CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(month) + " " + new DateTime(curYear, month, 01).ToString("yy");

                            double.TryParse(Convert.ToString(dt.Rows[0]["LastYear"]), out last);
                            grossNumbers.YearLastData = last.ToString("C0");
                            grossNumbers.YearLastLabel = "Prior 12 Months";
                            //currentGross = Convert.ToDouble(grossNumbers.YearCurrentdata.Replace("$", "")) - Convert.ToDouble(grossNumbers.YearLastData.Replace("$", ""));
                            //gainPercent = (currentGross / Convert.ToDouble(grossNumbers.YearLastData.Replace("$", ""))) * 100;
                            denominator = last; // Convert.ToDouble(grossNumbers.YearLastData.Replace("$", ""));
                            currentGross = current - denominator;
                            gainPercent = (denominator == 0) ? 0 : Convert.ToInt32((currentGross / denominator) * 100);
                            grossNumbers.YearGainLoss = (gainPercent == 0) ? "NA" : currentGross.ToString("C0");// + "(" + string.Format("{0:0.##}%", gainPercent) + ")";
                            grossNumbers.YearGainLossPercent = "(" + string.Format("{0}%", gainPercent) + ")";
                            grossNumbers.YearGainLossLabel = (gainPercent > 0) ? "Gain" : (gainPercent < 0) ? "Loss" : "Gain/Loss";

                            current = last = 0;
                            double.TryParse(Convert.ToString(dt.Rows[0]["CurrentYTDNet"]), out current);
                            netNumbers.YTDCurrentLabel = "Jan - " + CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(month) + " " + curYear;
                            netNumbers.YTDCurrentdata = current.ToString("C0"); //Convert.ToString(dt.Rows[0]["CurrentYTDNet"]);
                            double.TryParse(Convert.ToString(dt.Rows[0]["LastYTDNet"]), out last);
                            netNumbers.YTDLastData = last.ToString("C0");
                            netNumbers.YTDLastLabel = "Jan - " + CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(month) + " " + (curYear - 1).ToString();
                            //currentGross = Convert.ToDouble(netNumbers.YTDCurrentdata.Replace("$", "")) - Convert.ToDouble(netNumbers.YTDLastData.Replace("$", ""));
                            //gainPercent = (currentGross / Convert.ToDouble(netNumbers.YTDLastData.Replace("$", ""))) * 100;
                            denominator = last; // Convert.ToDouble(netNumbers.YTDLastData.Replace("$", ""));
                            currentGross = current - denominator;
                            gainPercent = (denominator == 0) ? 0 : Convert.ToInt32((currentGross / denominator) * 100);
                            netNumbers.YTDGainLoss = (gainPercent == 0) ? "NA" : currentGross.ToString("C0"); // + 
                            netNumbers.YTDGainLossPercent = "(" + string.Format("{0}%", gainPercent) + ")";

                            netNumbers.YTDGainLossLabel = (gainPercent > 0) ? "Gain" : (gainPercent < 0) ? "Loss" : "Gain/Loss";

                            current = last = 0;
                            double.TryParse(Convert.ToString(dt.Rows[0]["CurrentMTDNet"]), out current);
                            netNumbers.MTDCurrentdata = current.ToString("C0");  //Convert.ToString(dt.Rows[0]["CurrentMTDNet"]);
                            netNumbers.MTDCurrentLabel = CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(month) + " " + curYear;
                            double.TryParse(Convert.ToString(dt.Rows[0]["LastMTDNet"]), out last);
                            netNumbers.MTDLastData = last.ToString("C0"); //Convert.ToString(dt.Rows[0]["LastMTDNet"]);
                            netNumbers.MTDLastLabel = CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(month) + " " + (curYear - 1).ToString();
                            //currentGross = Convert.ToDouble(netNumbers.MTDCurrentdata.Replace("$", "")) - Convert.ToDouble(netNumbers.MTDLastData.Replace("$", ""));
                            //gainPercent = (currentGross / Convert.ToDouble(netNumbers.MTDLastData.Replace("$", ""))) * 100;
                            denominator = last; // Convert.ToDouble(netNumbers.MTDLastData.Replace("$", ""));
                            currentGross = current - denominator;
                            gainPercent = (denominator == 0) ? 0 : Convert.ToInt32((currentGross / denominator) * 100);
                            netNumbers.MTDGainLoss = (gainPercent == 0) ? "NA" : currentGross.ToString("C0");// +
                            netNumbers.MTDGainLossPercent = "(" + string.Format("{0}%", gainPercent) + ")";
                            netNumbers.MTDGainLossLabel = (gainPercent > 0) ? "Gain" : (gainPercent < 0) ? "Loss" : "Gain/Loss";

                            current = last = 0;
                            double.TryParse(Convert.ToString(dt.Rows[0]["CurrentYearNet"]), out current);

                            netNumbers.YearCurrentdata = current.ToString("C0"); // Convert.ToString(dt.Rows[0]["CurrentYearNet"]);
                            netNumbers.YearCurrentLabel = CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(DateTime.Now.AddMonths(-12).Month) + " " + (DateTime.Now.AddYears(-1)).ToString("yy") + " - " + CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(month) + " " + new DateTime(curYear, month, 01).ToString("yy");

                            double.TryParse(Convert.ToString(dt.Rows[0]["LastYearNet"]), out last);
                            netNumbers.YearLastData = last.ToString("C0");
                            netNumbers.YearLastLabel = "Prior 12 Months";

                            denominator = last; // Convert.ToDouble(netNumbers.YearLastData.Replace("$", ""));
                            currentGross = current - denominator;
                            gainPercent = (denominator == 0) ? 0 : Convert.ToInt32((currentGross / denominator) * 100);
                            netNumbers.YearGainLoss = (gainPercent == 0) ? "NA" : currentGross.ToString("C0");// + 
                            netNumbers.YearGainLossPercent = "(" + string.Format("{0}%", gainPercent) + ")";
                            netNumbers.YearGainLossLabel = (gainPercent > 0) ? "Gain" : (gainPercent < 0) ? "Loss" : "Gain/Loss";

                            clientID = (Guid)(dt.Rows[0]["DefaultClient"]);
                            refreshTime = Convert.ToString(dt.Rows[0]["CreatedOn"]);
                            DateTime date = DateTime.MinValue;
                            DateTime.TryParse(refreshTime, out date);
                            refreshTime = date.ToString("MM/dd/yyyy hh:mm tt");
                            adap.Dispose();
                        }
                        else
                        {
                            ActionLogger.Logger.WriteLog("GetDashboardRevenuePeryear new Licensee is Created: " + LicenseeID, true);
                            LicenseeWithNoRevenueData(out AllRevenueData grossNumbersData, out AllRevenueData netNumbersData, out Guid clientId, out string LastrefreshTime);
                            grossNumbers = new AllRevenueData();
                            grossNumbers = grossNumbersData;
                            netNumbers = new AllRevenueData();
                            netNumbers = netNumbersData;
                            DateTime date = DateTime.MinValue;
                            DateTime.TryParse(LastrefreshTime, out date);
                            refreshTime = date.ToString("MM/dd/yyyy hh:mm tt");
                            clientID = clientId;
                            adap.Dispose();

                        }
                    }
                    ActionLogger.Logger.WriteLog("GetDashboardRevenuePeryear success: " + LicenseeID, true);
                }
            }
            catch (Exception ex)
            {
                ActionLogger.Logger.WriteLog("GetDashboardRevenuePeryear exception: " + ex.Message, true);
                throw ex;
            }
            return obj;
        }



        public static void LicenseeWithNoRevenueData(out AllRevenueData grossNumbersData, out AllRevenueData netNumbersData, out Guid clientId, out string LastrefreshTime)
        {
            ActionLogger.Logger.WriteLog("LicenseeWithNoRevenueData:processing begins", true);
            try
            {
                grossNumbersData = new AllRevenueData();
                grossNumbersData.Title = "Gross Revenue";

                grossNumbersData.YTDCurrentLabel = "Jan - " + CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(DateTime.Now.Month - 1) + " " + DateTime.Now.Year;
                double current = 0;
                grossNumbersData.YTDCurrentdata = current.ToString("C0");

                grossNumbersData.YTDLastLabel = "Jan - " + CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(DateTime.Now.Month - 1) + " " + (DateTime.Now.Year - 1).ToString();
                double last = 0;
                grossNumbersData.YTDLastData = last.ToString("C0");

                double denominator = last; // Convert.ToDouble(grossNumbers.YTDLastData.Replace("$", ""));
                double currentGross = current - denominator; //Convert.ToDouble(grossNumbers.YTDCurrentdata.Replace("$", ""))
                double gainPercent = (denominator == 0) ? 0 : Convert.ToInt32((currentGross / denominator) * 100);
                grossNumbersData.YTDGainLoss = (gainPercent == 0) ? "NA" : currentGross.ToString("C0"); // + "(" + string.Format("{0:0.##}%", gainPercent) + ")";  //gainPercent.ToString("#.##") + "%)";
                grossNumbersData.YTDGainLossPercent = "(" + string.Format("{0}%", gainPercent) + ")"; //string.Format("{0:0.##}%", gainPercent)
                grossNumbersData.YTDGainLossLabel = (gainPercent > 0) ? "Gain" : (gainPercent < 0) ? "Loss" : "Gain/Loss";

                current = last = 0;
                grossNumbersData.MTDCurrentdata = current.ToString("C0");
                grossNumbersData.MTDCurrentLabel = CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(DateTime.Now.Month - 1) + " " + DateTime.Now.Year;

                grossNumbersData.MTDLastData = last.ToString("C0");
                grossNumbersData.MTDLastLabel = CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(DateTime.Now.Month - 1) + " " + (DateTime.Now.Year - 1).ToString();
                denominator = last;
                currentGross = current - denominator;
                gainPercent = (denominator == 0) ? 0 : Convert.ToInt32((currentGross / denominator) * 100);
                grossNumbersData.MTDGainLoss = (gainPercent == 0) ? "NA" : currentGross.ToString("C0");// + "(" + string.Format("{0:0.##}%", gainPercent) + ")";
                grossNumbersData.MTDGainLossPercent = "(" + string.Format("{0}%", gainPercent) + ")";
                grossNumbersData.MTDGainLossLabel = (gainPercent > 0) ? "Gain" : (gainPercent < 0) ? "Loss" : "Gain/Loss";


                current = last = 0;
                grossNumbersData.YearCurrentdata = current.ToString("C0");
                grossNumbersData.YearCurrentLabel = CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(DateTime.Now.AddMonths(-12).Month) + " " + (DateTime.Now.AddYears(-1)).ToString("yy") + " - " + CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(DateTime.Now.Month - 1) + " " + DateTime.Now.ToString("yy");


                grossNumbersData.YearLastData = last.ToString("C0");
                grossNumbersData.YearLastLabel = "Prior 12 Months";
                //currentGross = Convert.ToDouble(grossNumbers.YearCurrentdata.Replace("$", "")) - Convert.ToDouble(grossNumbers.YearLastData.Replace("$", ""));
                //gainPercent = (currentGross / Convert.ToDouble(grossNumbers.YearLastData.Replace("$", ""))) * 100;
                denominator = last; // Convert.ToDouble(grossNumbers.YearLastData.Replace("$", ""));
                currentGross = current - denominator;
                gainPercent = (denominator == 0) ? 0 : Convert.ToInt32((currentGross / denominator) * 100);
                grossNumbersData.YearGainLoss = (gainPercent == 0) ? "NA" : currentGross.ToString("C0");// + "(" + string.Format("{0:0.##}%", gainPercent) + ")";
                grossNumbersData.YearGainLossPercent = "(" + string.Format("{0}%", gainPercent) + ")";
                grossNumbersData.YearGainLossLabel = (gainPercent > 0) ? "Gain" : (gainPercent < 0) ? "Loss" : "Gain/Loss";

                current = last = 0;
                netNumbersData = new AllRevenueData();
                netNumbersData.Title = "Net Revenue";
                netNumbersData.YTDCurrentLabel = "Jan - " + CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(DateTime.Now.Month - 1) + " " + DateTime.Now.Year;
                netNumbersData.YTDCurrentdata = current.ToString("C0"); //Convert.ToString(dt.Rows[0]["CurrentYTDNet"]);

                netNumbersData.YTDLastData = last.ToString("C0");
                netNumbersData.YTDLastLabel = "Jan - " + CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(DateTime.Now.Month - 1) + " " + (DateTime.Now.Year - 1).ToString();
                //currentGross = Convert.ToDouble(netNumbers.YTDCurrentdata.Replace("$", "")) - Convert.ToDouble(netNumbers.YTDLastData.Replace("$", ""));
                //gainPercent = (currentGross / Convert.ToDouble(netNumbers.YTDLastData.Replace("$", ""))) * 100;
                denominator = last; // Convert.ToDouble(netNumbers.YTDLastData.Replace("$", ""));
                currentGross = current - denominator;
                gainPercent = (denominator == 0) ? 0 : Convert.ToInt32((currentGross / denominator) * 100);
                netNumbersData.YTDGainLoss = (gainPercent == 0) ? "NA" : currentGross.ToString("C0"); // + 
                netNumbersData.YTDGainLossPercent = "(" + string.Format("{0}%", gainPercent) + ")";

                netNumbersData.YTDGainLossLabel = (gainPercent > 0) ? "Gain" : (gainPercent < 0) ? "Loss" : "Gain/Loss";

                current = last = 0;

                netNumbersData.MTDCurrentdata = current.ToString("C0");  //Convert.ToString(dt.Rows[0]["CurrentMTDNet"]);
                netNumbersData.MTDCurrentLabel = CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(DateTime.Now.Month - 1) + " " + DateTime.Now.Year;

                netNumbersData.MTDLastData = last.ToString("C0"); //Convert.ToString(dt.Rows[0]["LastMTDNet"]);
                netNumbersData.MTDLastLabel = CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(DateTime.Now.Month - 1) + " " + (DateTime.Now.Year - 1).ToString();
                //currentGross = Convert.ToDouble(netNumbers.MTDCurrentdata.Replace("$", "")) - Convert.ToDouble(netNumbers.MTDLastData.Replace("$", ""));
                //gainPercent = (currentGross / Convert.ToDouble(netNumbers.MTDLastData.Replace("$", ""))) * 100;
                denominator = last; // Convert.ToDouble(netNumbers.MTDLastData.Replace("$", ""));
                currentGross = current - denominator;
                gainPercent = (denominator == 0) ? 0 : Convert.ToInt32((currentGross / denominator) * 100);
                netNumbersData.MTDGainLoss = (gainPercent == 0) ? "NA" : currentGross.ToString("C0");// +
                netNumbersData.MTDGainLossPercent = "(" + string.Format("{0}%", gainPercent) + ")";
                netNumbersData.MTDGainLossLabel = (gainPercent > 0) ? "Gain" : (gainPercent < 0) ? "Loss" : "Gain/Loss";

                current = last = 0;


                netNumbersData.YearCurrentdata = current.ToString("C0"); // Convert.ToString(dt.Rows[0]["CurrentYearNet"]);
                netNumbersData.YearCurrentLabel = CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(DateTime.Now.AddMonths(-12).Month) + " " + (DateTime.Now.AddYears(-1)).ToString("yy") + " - " + CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(DateTime.Now.Month - 1) + " " + DateTime.Now.ToString("yy");


                netNumbersData.YearLastData = last.ToString("C0");
                netNumbersData.YearLastLabel = "Prior 12 Months";

                denominator = last; // Convert.ToDouble(netNumbers.YearLastData.Replace("$", ""));
                currentGross = current - denominator;
                gainPercent = (denominator == 0) ? 0 : Convert.ToInt32((currentGross / denominator) * 100);
                netNumbersData.YearGainLoss = (gainPercent == 0) ? "NA" : currentGross.ToString("C0");// + 
                netNumbersData.YearGainLossPercent = "(" + string.Format("{0}%", gainPercent) + ")";
                netNumbersData.YearGainLossLabel = (gainPercent > 0) ? "Gain" : (gainPercent < 0) ? "Loss" : "Gain/Loss";

                clientId = Guid.Empty;
                LastrefreshTime = Convert.ToString(DateTime.Now);
                DateTime date = DateTime.MinValue;
                DateTime.TryParse(LastrefreshTime, out date);
                LastrefreshTime = date.ToString("MM/dd/yyyy hh:mm tt");
            }
            catch (Exception ex)
            {
                ActionLogger.Logger.WriteLog("LicenseeWithNoRevenueData:exception occurs" + ex.Message, true);
                throw ex;
            }
        }

        public static List<RevenueLOCData> GetRevenuByLOC(Guid LicenseeID, Guid UserCredentialID, DateTime StartDate, DateTime EndDate, bool isFirstTimeList, out List<RevenueLOCData> exportDataList)
        {
            List<RevenueLOCData> lstRevenue = new List<RevenueLOCData>();
            exportDataList = new List<RevenueLOCData>();
            ActionLogger.Logger.WriteLog("GetRevenuByLOC Processing begins:" + LicenseeID, true);
            var storedProcedureName = isFirstTimeList == true ? "usp_GetFirstTimeRevenuebyLOCList" : "usp_getRevenuebyLOC";
            try
            {
                using (SqlConnection con = new SqlConnection(DBConnection.GetConnectionString()))
                {
                    using (SqlCommand cmd = new SqlCommand(storedProcedureName, con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandTimeout = 0;
                        cmd.Parameters.AddWithValue("@LicenseeID", LicenseeID);
                        cmd.Parameters.AddWithValue("@UserCredentialID", UserCredentialID);
                        cmd.Parameters.AddWithValue("@StartDate", StartDate);
                        cmd.Parameters.AddWithValue("@EndDate", EndDate);
                        con.Open();
                        SqlDataReader dr = cmd.ExecuteReader();
                        while (dr.Read())
                        {
                            RevenueLOCData objData = new RevenueLOCData();
                            RevenueLOCData exportData = new RevenueLOCData();
                            objData.Product = dr.IsDBNull("Product") ? "" : Convert.ToString(dr["Product"]);
                            exportData.Product = dr.IsDBNull("Product") ? "" : Convert.ToString(dr["Product"]);

                            double total = 0;
                            double.TryParse(Convert.ToString(dr["Total"]), out total);
                            exportData.GrossRevenue = Convert.ToString(total);
                            objData.GrossRevenue = total.ToString("C");

                            double net = 0;
                            double.TryParse(Convert.ToString(dr["Paid"]), out net);
                            exportData.NetRevenue = Convert.ToString(net);
                            objData.NetRevenue = net.ToString("C");

                            lstRevenue.Add(objData);
                            exportDataList.Add(exportData);
                        }
                        dr.Close();
                    }
                    ActionLogger.Logger.WriteLog("GetRevenuByLOC:data successfully fetch" + LicenseeID, true);
                }
            }
            catch (Exception ex)
            {
                ActionLogger.Logger.WriteLog("GetRevenuByLOC: exception occurs:" + ex.Message, true);
                throw ex;
            }
            return lstRevenue;
        }

        public static List<ClientName> GetClientsWithMultipleProducts(Guid LicenseeID, Guid UserCredentialID, bool isFirstTimeList)
        {
            List<ClientName> lstClients = new List<ClientName>();
            ActionLogger.Logger.WriteLog("GetClientsWithMultipleProducts Processing begins:" + LicenseeID, true);

            var storedProcedureName = "usp_getClientsWithConsultingGroup";
            try
            {
                using (SqlConnection con = new SqlConnection(DBConnection.GetConnectionString()))
                {
                    using (SqlCommand cmd = new SqlCommand(storedProcedureName, con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandTimeout = 0;
                        cmd.Parameters.AddWithValue("@LicenseeID", LicenseeID);
                        con.Open();
                        SqlDataReader dr = cmd.ExecuteReader();
                        while (dr.Read())
                        {
                            string name = Convert.ToString(dr["Client"]);
                            ClientName client = new ClientName();
                            client.Client = name;
                            lstClients.Add(client);
                        }
                        dr.Close();
                    }
                    ActionLogger.Logger.WriteLog("GetClientsWithMultipleProducts:data successfully fetch" + LicenseeID, true);
                }
            }
            catch (Exception ex)
            {
                ActionLogger.Logger.WriteLog("GetClientsWithMultipleProducts: exception occurs:" + ex.Message, true);
                throw ex;
            }
            return lstClients;
        }
    }
}