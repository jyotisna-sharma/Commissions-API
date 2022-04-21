using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using MyAgencyVault.BusinessLibrary.Base;
using DLinq = DataAccessLayer.LinqtoEntity;
using System.Data.SqlClient;
using System.Data;

namespace MyAgencyVault.BusinessLibrary
{
    [DataContract]
    public class CompType
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public int? IncomingPaymentTypeID { get; set; }

        [DataMember]
        public string PaymentTypeName { get; set; }

        [DataMember]
        public string Names { get; set; }

        public List<CompType> GetAllComptype()
        {
            List<CompType> lstCompType = null;
            try
            {

                using (DLinq.CommissionDepartmentEntities DataModel = Entity.DataModel)
                {
                    lstCompType = (from p in DataModel.IncomingCompTypes
                                   select new CompType
                                   {
                                       Id = p.Id,
                                       IncomingPaymentTypeID = p.IncomingPaymentTypeID,
                                       PaymentTypeName = p.PaymentTypeName,
                                       Names = p.Names
                                   }).ToList();

                }
            }
            catch
            {
            }

            return lstCompType;
        }

        public void AddUpdateCompType(CompType objCompType, out bool isExist)
        {
            isExist = true;
            try
            {
                using (DLinq.CommissionDepartmentEntities DataModel = Entity.DataModel)
                {

                    DLinq.IncomingCompType isExists = (from p in DataModel.IncomingCompTypes.Where(b => b.Id != objCompType.Id && b.IncomingPaymentTypeID ==objCompType.IncomingPaymentTypeID && b.Names ==objCompType.Names) select p).FirstOrDefault();

                    if(isExists != null)
                    {
                        isExist = true;
                    }
                    else
                    {
                        isExist = false;
                        DLinq.IncomingCompType ObjValue = (from p in DataModel.IncomingCompTypes.Where(b => b.Id == objCompType.Id) select p).FirstOrDefault();
                        //insert
                        if (ObjValue == null)
                        {
                            var CompValue = new DLinq.IncomingCompType
                            {
                                IncomingPaymentTypeID = objCompType.IncomingPaymentTypeID,
                                PaymentTypeName = objCompType.PaymentTypeName,
                                Names = objCompType.Names
                            };

                            DataModel.AddToIncomingCompTypes(CompValue);
                        }
                        //update 
                        else
                        {
                            ObjValue.IncomingPaymentTypeID = objCompType.IncomingPaymentTypeID;
                            ObjValue.PaymentTypeName = objCompType.PaymentTypeName;
                            ObjValue.Names = objCompType.Names;
                        }

                        DataModel.SaveChanges();
                    }
                }
                  
            }
            catch (Exception ex)
            {
                ActionLogger.Logger.WriteLog("AddUpdateCompType ex: " + ex.Message, true);
                throw ex;
            }
        }

        public bool DeleteCompType(CompType objCompType)
        {
            bool bValue = false;
            try
            {
                using (DLinq.CommissionDepartmentEntities DataModel = Entity.DataModel)
                {
                    DLinq.IncomingCompType ObjIncomingCompType = DataModel.IncomingCompTypes.FirstOrDefault(p => p.Id == objCompType.Id);
                    if (ObjIncomingCompType != null)
                    {
                        DataModel.DeleteObject(ObjIncomingCompType);
                        DataModel.SaveChanges();
                        bValue = true;
                    }
                }
            }
            catch
            {
                bValue = false;
            }

            return bValue;
        }

        #region
        public static List<CompType> GetCompTypeListing(string incomingPaymentTypeId, string paymentTypeName, ListParams listParams, out int recordCount)
        {
            List<CompType> compTypeList = new List<CompType>();
            try
            {
                using (SqlConnection con = new SqlConnection(DBConnection.GetConnectionString()))
                {
                    using (SqlCommand cmd = new SqlCommand("usp_GetCompTypeList", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        int rowStart = (listParams.pageSize == 0 && listParams.pageIndex == 0) ? 0 : (listParams.pageSize * (listParams.pageIndex)) + 1;
                        int rowEnd = (listParams.pageSize == 0 && listParams.pageIndex == 0) ? 0 : (listParams.pageIndex + 1) * listParams.pageSize;
                        cmd.Parameters.AddWithValue("@RowStart", rowStart);
                        cmd.Parameters.AddWithValue("@RowEnd", rowEnd);
                        cmd.Parameters.AddWithValue("@sortBy", listParams.sortBy);
                        cmd.Parameters.AddWithValue("@sortType", listParams.sortType);
                        cmd.Parameters.AddWithValue("@filterBy", listParams.filterBy);
                        cmd.Parameters.AddWithValue("@IncomingPaymentTypeID", incomingPaymentTypeId);
                        cmd.Parameters.AddWithValue("@PaymentTypeName", paymentTypeName);
                        cmd.Parameters.Add("@recordLength", SqlDbType.Int);
                        cmd.Parameters["@recordLength"].Direction = ParameterDirection.Output;
                        con.Open();
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            CompType recordDetails = new CompType();
                            recordDetails.IncomingPaymentTypeID = (int)reader["incomingPaymentTypeId"];
                            recordDetails.PaymentTypeName = reader.IsDBNull("PaymentTypeName") ? null : Convert.ToString(reader["PaymentTypeName"]);
                            recordDetails.Names = reader.IsDBNull("Names") ? null : Convert.ToString(reader["Names"]);
                            recordDetails.Id = (int)reader["Id"];
                            compTypeList.Add(recordDetails);
                        }
                        reader.Close();
                        int.TryParse(Convert.ToString(cmd.Parameters["@recordLength"].Value), out recordCount);
                    }
                }
            }
            catch (Exception ex)
            {
                ActionLogger.Logger.WriteLog("getContacts exception " + ex.Message, true);
                throw ex;
            }
            return compTypeList;
        }

        #endregion
        public bool FindCompTypeName(string strName)
        {
            bool bValue = false;
            try
            {
                using (DLinq.CommissionDepartmentEntities DataModel = Entity.DataModel)
                {
                    DLinq.IncomingCompType ObjIncomingCompType = DataModel.IncomingCompTypes.FirstOrDefault(p => p.Names == strName);
                    if (ObjIncomingCompType != null)
                    {
                        bValue = true;
                    }
                }
            }
            catch
            {
                bValue = false;
            }

            return bValue;
        }

        public static int? GetCompTypeName(string strName)
        {
            int? intCompType = null;
            try
            {
                using (DLinq.CommissionDepartmentEntities DataModel = Entity.DataModel)
                {
                    DLinq.IncomingCompType ObjIncomingCompType = DataModel.IncomingCompTypes.FirstOrDefault(p => p.Names.ToLower() == strName.ToLower());
                    if (ObjIncomingCompType != null)
                    {
                        intCompType = ObjIncomingCompType.IncomingPaymentTypeID;
                    }
                }
            }
            catch
            {
            }
            return intCompType;
        }
    }

}
