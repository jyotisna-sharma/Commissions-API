using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DLinq = DataAccessLayer.LinqtoEntity;
using System.Data.SqlClient;
using System.Data.EntityClient;
using System.Data;
using ActionLogger;
using System.Reflection;

namespace MyAgencyVault.BusinessLibrary
{
    public static class DBConnection
    {
        public static string GetConnectionString()
        {
            DLinq.CommissionDepartmentEntities ctx = new DLinq.CommissionDepartmentEntities(); //create your entity object here
            EntityConnection ec = (EntityConnection)ctx.Connection;
            SqlConnection sc = (SqlConnection)ec.StoreConnection; //get the SQLConnection that your entity object would use
            return sc.ConnectionString.ToString();
        }

        public static SqlConnection GetConnection()
        {
            DLinq.CommissionDepartmentEntities ctx = new DLinq.CommissionDepartmentEntities(); //create your entity object here
            EntityConnection ec = (EntityConnection)ctx.Connection;
            SqlConnection sc = (SqlConnection)ec.StoreConnection; //get the SQLConnection that your entity object would use
            return sc;
        }
        public static DataSet ExecuteQuery(string procName, SqlParameter[] param, string className)
        {
            Logger.WriteLog("ExecuteQuery: procName - " + procName + ", class - " + className, true);
            DataSet ds = new DataSet();

            try
            {
                using (System.Data.SqlClient.SqlConnection scn = DBConnection.GetConnection())
                {
                    Logger.WriteLog("Authenticate: got connection", true);
                    if (scn != null)
                    {
                        SqlCommand scm = new SqlCommand
                        {
                            CommandText = procName,
                            CommandType = CommandType.StoredProcedure,
                            CommandTimeout = 360,
                            Connection = scn
                        };
                        if (param != null && param.Length > 0)
                        {
                            scm.Parameters.AddRange(param);
                        }
                        SqlDataAdapter ad = new SqlDataAdapter(scm);
                        ad.Fill(ds);
                        Logger.WriteLog("dataset found: " + ds.Tables.Count.ToString(), true);
                        scn.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog("Exception in ExecuteQuery: " + ex.Message, true);
            }
            return ds;
        }
        public static IList<T> DatatableToClass<T>(DataTable Table) where T : class, new()
        {
            //if (!Helper.IsValidDatatable(Table))
            //    return new List<T>();

            Type classType = typeof(T);
            IList<PropertyInfo> propertyList = classType.GetProperties();

            // Parameter class has no public properties.
            if (propertyList.Count == 0)
                return new List<T>();

            List<string> columnNames = Table.Columns.Cast<DataColumn>().Select(column => column.ColumnName).ToList();

            List<T> result = new List<T>();
            try
            {
                foreach (DataRow row in Table.Rows)
                {
                    T classObject = new T();
                    foreach (PropertyInfo property in propertyList)
                    {
                        if (property != null && property.CanWrite && property.PropertyType != typeof(object))   // Make sure property isn't read only
                        {
                            if (columnNames.Contains(property.Name))  // If property is a column name
                            {
                                if (row[property.Name] != System.DBNull.Value)   // Don't copy over DBNull
                                {
                                    object propertyValue = System.Convert.ChangeType(
                                            row[property.Name],
                                            property.PropertyType
                                        );
                                    property.SetValue(classObject, propertyValue, null);
                                }
                            }
                        }
                    }
                    result.Add(classObject);
                }
                return result;
            }
            catch (Exception ex)
            {
                Logger.WriteLog("Error while converting data table in class: " + ex.ToStringDump(), true);
                return new List<T>();
            }
        }

        /// <summary>
        /// Convert DataTable to class object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Table"></param>
        /// <returns></returns>
        public static T DatatableToClassObject<T>(DataTable Table) where T : class, new()
        {
            //if (!Helper.IsValidDatatable(Table))
            //    return new List<T>();

            Type classType = typeof(T);
            IList<PropertyInfo> propertyList = classType.GetProperties();

            // Parameter class has no public properties.
            if (propertyList.Count == 0)
                return new T();

            List<string> columnNames = Table.Columns.Cast<DataColumn>().Select(column => column.ColumnName).ToList();

            List<T> result = new List<T>();
            T classObject = new T();

            try
            {
                foreach (DataRow row in Table.Rows)
                {
                    foreach (PropertyInfo property in propertyList)
                    {
                        if (property != null && property.CanWrite && property.PropertyType != typeof(System.Object))   // Make sure property isn't read only
                        {
                            if (columnNames.Contains(property.Name))  // If property is a column name
                            {
                                if (row[property.Name] != System.DBNull.Value)   // Don't copy over DBNull
                                {
                                    object propertyValue = System.Convert.ChangeType(
                                            row[property.Name],
                                            property.PropertyType
                                        );
                                    property.SetValue(classObject, propertyValue, null);
                                }
                            }
                        }
                    }
                }
                return classObject;
            }
            catch (Exception ex)
            {
                Logger.WriteLog("Exception in convert referral settings object:" + ex.Message, true);
                return new T();
            }
        }


    }
}
