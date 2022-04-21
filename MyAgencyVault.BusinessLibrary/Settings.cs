using MyAgencyVault.BusinessLibrary.Base;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace MyAgencyVault.BusinessLibrary
{
    [DataContract]
    public class Settings : IEditable<Settings>
    {

        public static void AddUpdate(Guid licenseeId, Guid reportID, string fields)
        {
            try
            {
                ActionLogger.Logger.WriteLog("AddUpdate:AddUpdate reports settings for licenseeId" + licenseeId, true);
                SqlConnection con = null;
                using (con = new SqlConnection(DBConnection.GetConnectionString()))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_SaveLicenseeReportFields", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@LicenseeId", licenseeId);
                        cmd.Parameters.AddWithValue("@ReportID", reportID);
                        cmd.Parameters.AddWithValue("@Fields", fields);
                        con.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                ActionLogger.Logger.WriteLog("Exception occur while saving your settings:licenseeId" + licenseeId + "Excepiton:" + ex.InnerException, true);
                throw ex;  
            }
        }

        public static Boolean IsNamedScheduleExist(string scheduleName, Guid?incomingScheduleId,Guid licenseeId)
        {
            bool isExist = true;
            try
            {
                ActionLogger.Logger.WriteLog("IsNamedScheduleExist:" + scheduleName, true);
                using (SqlConnection con = new SqlConnection(DBConnection.GetConnectionString()))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_IsNamedScheduleExist", con))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Title", scheduleName);
                        cmd.Parameters.AddWithValue("@IncomingScheduleId", incomingScheduleId);
                        cmd.Parameters.AddWithValue("@licenseeId", licenseeId);
                        cmd.Parameters.Add("@IsExist", SqlDbType.Bit);
                        cmd.Parameters["@IsExist"].Direction = ParameterDirection.Output;
                        con.Open();
                        cmd.ExecuteScalar();
                        isExist = (bool)cmd.Parameters["@IsExist"].Value;
                    }
                    con.Close();
                }

            }
            catch (Exception ex)
            {
                ActionLogger.Logger.WriteLog("SaveSchedule exception: " + ex.Message, true);
                throw ex;
            }
            return isExist;
        }
       
        public void AddUpdate()
        {
            throw new NotImplementedException();
        }
        public Settings GetOfID()
        {
            throw new NotImplementedException();
        }
        public static List<ReportCustomFieldSettings> ReportSettingListData(Guid licenseeId , Guid reportID)
        {
            SqlConnection con = null;
            List<ReportCustomFieldSettings> lstSettings = new List<ReportCustomFieldSettings>();
            try
            {
                using (con = new SqlConnection(DBConnection.GetConnectionString()))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_getLicenseeReportFields", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@LicenseeId", licenseeId);
                        cmd.Parameters.AddWithValue("@ReportID", reportID);
                        con.Open();

                        SqlDataReader reader = cmd.ExecuteReader();
                        // Call Read before accessing data. 
                        while (reader.Read())
                        {
                            ReportCustomFieldSettings obj = new ReportCustomFieldSettings();
                            obj.FieldName = Convert.ToString(reader["field"]);
                            obj.DisplayFieldName = Convert.ToString(reader["DisplayFieldName"]);

                            string strIsSelected = Convert.ToString(reader["IsSelected"]);
                            bool isSelected = false;
                            bool.TryParse(strIsSelected, out isSelected);
                            obj.IsSelected = isSelected;
                            obj.Checked = isSelected;

                            bool isModifiable = false;
                            string strIsModifiable = Convert.ToString(reader["IsModifiable"]);
                            bool.TryParse(strIsModifiable, out isModifiable);
                            obj.IsModifiable = isModifiable;
                            obj.IsReadOnly = !isModifiable;

                            lstSettings.Add(obj);
                        }
                        reader.Close();
                    }
                }

            }
            catch (Exception ex)
            {
                ActionLogger.Logger.WriteLog("Exception while getData in custom field settings:" + ex.Message, true);
            }
           return lstSettings;
        }

        public void Delete()
        {
            throw new NotImplementedException();
        }


    }
    [DataContract]
    public class ReportCustomFieldSettings
    {
        [DataMember]
        public string FieldName { get; set; }
        [DataMember]
        public string DisplayFieldName { get; set; }
        [DataMember]
        public bool IsModifiable { get; set; }
        [DataMember]
        public bool IsSelected { get; set; }
        [DataMember]
        public bool Checked { get; set; }
        [DataMember]
        public bool IsReadOnly { get; set; } //kept to bind grid property
    }
}
