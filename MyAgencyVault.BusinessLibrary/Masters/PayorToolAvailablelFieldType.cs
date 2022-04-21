using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using DLinq = DataAccessLayer.LinqtoEntity;
using System.Data.EntityClient;
using System.Data.SqlClient;
using System.Data;

namespace MyAgencyVault.BusinessLibrary.Masters
{
    [DataContract]
    public class PayorToolAvailablelFieldType
    {
        #region "Datamembers aka -- public properties."

        [DataMember]
        public int FieldID { get; set; }
        [DataMember]
        public string FieldName { get; set; }
        [DataMember]
        public string FieldDiscription { get; set; }
        [DataMember]
        public bool IsUsed { get; set; }
        [DataMember]
        public string EquivalentIncomingField { get; set; }
        [DataMember]
        public string EquivalentLearnedField { get; set; }
        [DataMember]
        public string EquivalentDeuField { get; set; }
        [DataMember]
        public bool canDeleted { get; set; }
        [DataMember]
        public bool Disabled { get; set; }
        [DataMember]
        public int MaskFieldTypeId { get; set; }
        [DataMember]
        public int MaskFieldType { get; set; }
        [DataMember]
        public List<PayorToolMaskedFieldType> MaskFieldList { get; set; }
        [DataMember]
        public List<TranslatorTypes> ImportToolTranslator { get; set; }
        #endregion
        /// <summary>
        /// Used for :Add Update Payor Tool AvailablelFieldType
        /// Used by:Ankit 
        /// </summary>
        /// <returns></returns>
        public int AddUpdate()
        {
            try
            {
                ActionLogger.Logger.WriteLog("AddUpdate:Processing begins for Addupdate payor Tool AvailablelFieldType", true);
                using (DLinq.CommissionDepartmentEntities DataModel = Entity.DataModel)
                {
                    DLinq.MasterPayorToolAvailableField Obj = null;
                    Obj = (from p in DataModel.MasterPayorToolAvailableFields where p.Name == this.FieldName && p.IsDeleted == false select p).FirstOrDefault();

                    if (Obj == null)
                    {
                        Obj = new DLinq.MasterPayorToolAvailableField();
                        Obj.Name = this.FieldName;
                        Obj.Description = this.FieldDiscription;
                        Obj.EquivalentDeuField = this.EquivalentDeuField;
                        Obj.EquivalentIncomingField = this.EquivalentIncomingField;
                        Obj.EquivalentLearnedField = this.EquivalentLearnedField;
                        Obj.IsDeleted = false;
                        Obj.IsDeletable = true;
                        DataModel.AddToMasterPayorToolAvailableFields(Obj);
                        DataModel.SaveChanges();
                        return Obj.PTAvailableFieldId;
                    }
                    else
                    {
                        ActionLogger.Logger.WriteLog("Field already exists", true);
                        return -1;
                    }

                }
            }
            catch (Exception ex)
            {
                ActionLogger.Logger.WriteLog("AddUpdate payortoolfield ex: " + ex.Message, true);
                throw ex;
            }
        }
        /// <summary>
        /// CreatedBy:Acmeminds
        /// CreatedOn:Feb,2019
        /// Purpose:Delete a payor tool field
        /// </summary>
        /// <returns></returns>
        public bool Delete()
        {
            ActionLogger.Logger.WriteLog("Delete: processing begins to delete payor tool field", true);
            bool isDeleted = false;
            try
            {
                using (DLinq.CommissionDepartmentEntities DataModel = Entity.DataModel)
                {
                    //old
                    //DLinq.MasterPayorToolAvailableField _field = DataModel.MasterPayorToolAvailableFields.FirstOrDefault(s => s.Name == this.FieldName);
                    //new
                    //Check custom field available in any other fields
                    DLinq.PayorToolField pfield = DataModel.PayorToolFields.FirstOrDefault(s => s.PTAvailableFieldId == this.FieldID && s.IsDeleted == false);
                    if (pfield != null)
                    {
                        isDeleted = false;
                    }
                    else
                    {
                        DLinq.MasterPayorToolAvailableField _field = DataModel.MasterPayorToolAvailableFields.FirstOrDefault(s => s.Name == this.FieldName && s.IsDeleted == false);
                        _field.IsDeleted = true;
                        isDeleted = true;
                        DataModel.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                ActionLogger.Logger.WriteLog("Delete: Exception occurs to delete payor tool field" + ex.Message, true);
                throw ex;
            }
            return isDeleted;
        }

        public static List<PayorToolAvailablelFieldType> GetFieldList()
        {
            List<PayorToolAvailablelFieldType> availableFields = new List<PayorToolAvailablelFieldType>();
            using (SqlConnection con = new SqlConnection(DBConnection.GetConnectionString()))
            {

                using (SqlCommand cmd = new SqlCommand("usp_GetDraggableFieldList", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        PayorToolAvailablelFieldType data = new PayorToolAvailablelFieldType();
                        data.FieldName = reader["Name"].ToString();
                        data.FieldID = reader.IsDBNull("PTAvailableFieldId") ? 1 : (int)reader["PTAvailableFieldId"];
                        data.canDeleted = (bool)reader["IsDeletable"];
                        data.EquivalentIncomingField = reader.IsDBNull("EquivalentIncomingField") ? "" : reader["EquivalentIncomingField"].ToString();
                        data.EquivalentDeuField = reader.IsDBNull("EquivalentDeuField") ? "" : reader["EquivalentDeuField"].ToString();
                        data.EquivalentLearnedField = reader.IsDBNull("EquivalentLearnedField") ? "" : reader["EquivalentLearnedField"].ToString();
                        data.MaskFieldType = reader.IsDBNull("MaskFieldType") ? 0 : (int)reader["MaskFieldType"];
                        data.Disabled = false;
                        availableFields.Add(data);
                    }
                }

            }
            foreach (PayorToolAvailablelFieldType field in availableFields)
            {
                using (DLinq.CommissionDepartmentEntities DataModel = Entity.DataModel)
                {
                    bool isUsed = DataModel.PayorToolFields.Any(s => s.IsDeleted == false && s.PTAvailableFieldId == field.FieldID);
                    field.IsUsed = isUsed;
                    field.MaskFieldList = PayorToolMaskedFieldType.GetSelectedMaskList(field.MaskFieldType);
                }

            }
            return availableFields;
            //    return availableFields;
            //}
        }


        public static List<string> GetEndDataList()
        {
            using (DLinq.CommissionDepartmentEntities DataModel = Entity.DataModel)
            {
                List<string> availableFields = (from cf in DataModel.ImportToolAvailableFields
                                                where cf.IsDeleted == false || cf.IsDeleted == null
                                                select cf.Name).ToList();

                return availableFields;
            }
        }

    }
}
