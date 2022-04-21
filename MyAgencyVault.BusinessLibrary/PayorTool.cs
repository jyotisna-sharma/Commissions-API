using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyAgencyVault.BusinessLibrary.Base;
using System.Runtime.Serialization;
using DLinq = DataAccessLayer.LinqtoEntity;
using DataAccessLayer.LinqtoEntity;
using MyAgencyVault.BusinessLibrary.Masters;
using System.Collections.ObjectModel;
using System.IO;
using System.Data.SqlClient;
using System.Data;
using MyAgencyVault.BusinessLibrary.BusinessObjects;
using System.Text.RegularExpressions;

namespace MyAgencyVault.BusinessLibrary
{

    #region"add template"
    public class Tempalate
    {
        [DataMember]
        public int? ID { get; set; }
        [DataMember]
        public Guid? TemplateID { get; set; }
        [DataMember]
        public string TemplateName { get; set; }
        [DataMember]
        public bool IsDeleted { get; set; }
        [DataMember]
        public bool IsForceImport { get; set; }

    }
    #endregion

    [DataContract]
    public class PayorTool
    {
        #region IEditable<PayorTool> Members

        #region "duplicate"
        //duplicate payor tools



        //duplicate payor tools
        public static bool IsAvailablePayorTempalate(Guid SourcePayorID, Guid? SourceTempID, Guid DestinationPayorID, Guid? DestiTempID)
        {
            using (DLinq.CommissionDepartmentEntities DataModel = Entity.DataModel)
            {
                DLinq.PayorTool ObjPayorTemplate = null;

                if (DestiTempID == null)
                {
                    ObjPayorTemplate = (from p in DataModel.PayorTools
                                        where p.PayorId == DestinationPayorID && p.TemplateID == null
                                        select p).FirstOrDefault();
                }
                else
                {
                    ObjPayorTemplate = (from p in DataModel.PayorTools
                                        where p.PayorId == DestinationPayorID && p.TemplateID == DestiTempID
                                        select p).FirstOrDefault();
                }

                if (ObjPayorTemplate != null)
                {
                    return true;
                }
                else
                    return false;


            }
        }

        #endregion


        public static bool DeletePayorToolTemplate(PayorTool payorTool, Guid? tempID)
        {
            ActionLogger.Logger.WriteLog("DeletePayorToolTemplate:Processing begins with templateId" + tempID, true);
            bool bValue = true;
            try
            {
                Delete(payorTool, tempID);
            }
            catch (Exception ex)
            {
                ActionLogger.Logger.WriteLog("DeletePayorToolTemplate:Exception occurs  with templateId" + tempID + ex.Message, true);
                bValue = false;
                throw ex;
            }
            return bValue;

        }
        public static void Delete(PayorTool payorTool, Guid? tempID)
        {
            ActionLogger.Logger.WriteLog("Delete:processing begins with templateId" + tempID, true);
            try
            {
                //Delete all payor toll fieds
                DeletePayorToolField(payorTool);
                using (DLinq.CommissionDepartmentEntities DataModel = Entity.DataModel)
                {

                    DLinq.PayorTool ObjPayorTool = null;
                    if (tempID == null)
                    {
                        ObjPayorTool = (from p in DataModel.PayorTools
                                        where p.PayorToolId == payorTool.PayorToolId && p.TemplateID == null
                                        select p).FirstOrDefault();
                    }
                    else
                    {
                        ObjPayorTool = (from p in DataModel.PayorTools
                                        where p.PayorToolId == payorTool.PayorToolId && p.TemplateID == tempID
                                        select p).FirstOrDefault();
                    }

                    if (ObjPayorTool != null)
                    {
                        //Delete Payor tool
                        try
                        {
                            DataModel.DeleteObject(ObjPayorTool);
                            DataModel.SaveChanges();
                        }
                        catch
                        {
                            ObjPayorTool.IsDeleted = true;
                            DataModel.SaveChanges();
                            //delete payor template
                            deleteTemplate(tempID);
                        }

                    }

                    //delete payor template
                    deleteTemplate(tempID);

                }
            }
            catch (Exception ex)
            {
                ActionLogger.Logger.WriteLog("DeletePayorToolTemplate:Exception occurs  with templateId" + tempID + ex.Message, true);
                throw ex;
            }
        }

        private static void DeletePayorToolField(PayorTool payorTool)
        {
            ActionLogger.Logger.WriteLog("DeletePayorToolField:Exception occurs  with payorTool" + payorTool.ToStringDump(), true);
            try
            {
                if (payorTool.ToolFields != null)
                {
                    foreach (PayorToolField Field in payorTool.ToolFields)
                    {
                        PayorToolField.Delete(Field);
                    }
                }
            }
            catch (Exception ex)
            {
                ActionLogger.Logger.WriteLog("DeletePayorToolField:Exception occurs  with payorTool" + payorTool.ToStringDump() + ex.Message, true);
                throw ex;
            }
        }

        private static void deleteTemplate(Guid? tempID)
        {
            ActionLogger.Logger.WriteLog("deleteTemplate:processing  begins  with templateId" + tempID, true);
            try
            {
                using (DLinq.CommissionDepartmentEntities DataModel = Entity.DataModel)
                {
                    DLinq.PayorTemplate temp = DataModel.PayorTemplates.FirstOrDefault(s => s.TemplateID == tempID);

                    if (temp != null)
                    {

                        DataModel.DeleteObject(temp);
                        DataModel.SaveChanges();
                    }


                }
            }
            catch (Exception ex)
            {
                ActionLogger.Logger.WriteLog("deleteTemplate:Exception occurs with templateId" + tempID + ex.Message, true);
                throw ex;
            }
        }
        public static PayorTool GetPayorToolMgr(Guid PayorID, Guid? TemplateID)
        {
            ActionLogger.Logger.WriteLog("GetPayorToolMgr:Processing begins with payorId" + PayorID, true);
            PayorTool pTool = new PayorTool();
            try
            {
                using (DLinq.CommissionDepartmentEntities DataModel = Entity.DataModel)
                {
                    if (TemplateID == null)
                    {
                        pTool = (from p in DataModel.PayorTools
                                 where p.Payor.PayorId == PayorID && p.TemplateID == null && p.IsDeleted == false
                                 select new PayorTool
                                 {
                                     ChequeImageFilePath = string.Empty,
                                     StatementImageFilePath = string.Empty,
                                     WebDevChequeImageFilePath = p.ChequeImageFile,
                                     WebDevStatementImageFilePath = p.StatementImageFile,
                                     PayorToolId = p.PayorToolId,
                                     PayorID = p.Payor.PayorId,
                                     TemplateID = p.TemplateID,
                                 }).FirstOrDefault();
                    }
                    else
                    {
                        pTool = (from p in DataModel.PayorTools
                                 where p.Payor.PayorId == PayorID && p.TemplateID == TemplateID && p.IsDeleted == false
                                 select new PayorTool
                                 {
                                     ChequeImageFilePath = string.Empty,
                                     StatementImageFilePath = string.Empty,
                                     WebDevChequeImageFilePath = p.ChequeImageFile,
                                     WebDevStatementImageFilePath = p.StatementImageFile,
                                     PayorToolId = p.PayorToolId,
                                     PayorID = p.Payor.PayorId,
                                     TemplateID = p.TemplateID,
                                 }).FirstOrDefault();
                    }

                    if (pTool != null)
                    {
                        pTool.ToolFields =
                         (from p1 in DataModel.PayorToolFields
                          where p1.PayorTool.PayorToolId == pTool.PayorToolId && p1.IsDeleted == false
                          select new PayorToolField
                          {
                              AllignedDirection = p1.AllignedDirection ?? "Left",
                              DefaultValue = p1.DefaultNumeric,
                              EquivalentIncomingField = p1.EquivalentIncomingField,
                              EquivalentLearnedField = p1.EquivalentLearnedField,
                              EquivalentDeuField = p1.EquivalentDeuField,
                              ControlHeight = p1.FieldHeight ?? 0,
                              FieldOrder = p1.FieldOrder ?? 0,
                              ControlX = p1.FieldPositionX.Value,
                              ControlY = p1.FieldPositionY.Value,
                              FieldStatusValue = p1.FieldStatus,
                              ControlWidth = p1.FieldWidth ?? 0,
                              FormulaId = p1.FormulaId,
                              HelpText = p1.HelpText,
                              IsCalculatedField = p1.IsCalculatedField,
                              IsZeroorBlankAllowed = p1.IsOorBlankAllowed,
                              IsOverrideOfCalcAllowed = p1.IsOverrideOfCalcAllowed,
                              IsPopulateIfLinked = p1.IsPopulatedIfLinked,
                              IsPartOfPrimaryKey = p1.IsPartOfPrimary,
                              IsTabbedToNextFieldIfLinked = p1.IsTabbedToNextFieldIfLinked,
                              LabelOnField = p1.LabelOnImage,
                              MaskFieldTypeId = p1.MasterPayorToolMaskFieldType.PTMaskFieldTypeId == null ? 0 : p1.MasterPayorToolMaskFieldType.PTMaskFieldTypeId,
                              MaskFieldType = p1.MasterPayorToolMaskFieldType.Type.Value,
                              MaskText = p1.MasterPayorToolMaskFieldType.Name,
                              PTAvailableFieldId = p1.MasterPayorToolAvailableField.PTAvailableFieldId == null ? 0 : p1.MasterPayorToolAvailableField.PTAvailableFieldId,
                              AvailableFieldName = p1.MasterPayorToolAvailableField.PTAvailableFieldId == null ? "" : p1.MasterPayorToolAvailableField.Name,
                              PayorFieldID = p1.PayorToolFieldId,
                              PayorToolId = p1.PayorTool.PayorToolId


                          }).ToList();

                        foreach (PayorToolField field in pTool.ToolFields)
                        {
                            if (field.FormulaId != null)
                            {
                                DLinq.Formula formula = DataModel.Formulas.FirstOrDefault(s => s.FormulaId == field.FormulaId);
                                if (formula != null)
                                {
                                    field.CalculationFormula = new Formula();
                                    field.CalculationFormula.FormulaExpression = formula.FormulaExpression;
                                    field.CalculationFormula.FormulaTtitle = formula.FormulaTtitle;
                                    field.CalculationFormula.FormulaID = formula.FormulaId;
                                }
                            }
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                ActionLogger.Logger.WriteLog("GetPayorToolMgr:Exception occurs while processing begins with payorId" + PayorID + ex.Message, true);
            }
            return pTool;
        }

        public static PayorTool GetPayorToolMgr(Guid PayorID)
        {
            ActionLogger.Logger.WriteLog("GetPayorToolMgr:processing begins with payorId" + PayorID, true);
            try
            {
                using (DLinq.CommissionDepartmentEntities DataModel = Entity.DataModel)
                {
                    PayorTool pTool = (from p in DataModel.PayorTools
                                       where p.Payor.PayorId == PayorID && p.TemplateID == null && p.IsDeleted == false
                                       select new PayorTool
                                       {
                                           ChequeImageFilePath = string.Empty,
                                           StatementImageFilePath = string.Empty,
                                           WebDevChequeImageFilePath = p.ChequeImageFile,
                                           WebDevStatementImageFilePath = p.StatementImageFile,
                                           PayorToolId = p.PayorToolId,
                                           PayorID = p.Payor.PayorId,
                                       }).FirstOrDefault();

                    if (pTool != null)
                    {
                        pTool.ToolFields =
                         (from p1 in DataModel.PayorToolFields
                          where p1.PayorTool.PayorToolId == pTool.PayorToolId && p1.IsDeleted == false
                          select new PayorToolField
                          {
                              AllignedDirection = p1.AllignedDirection ?? "Left",
                              DefaultValue = p1.DefaultNumeric,
                              EquivalentIncomingField = p1.EquivalentIncomingField,
                              EquivalentLearnedField = p1.EquivalentLearnedField,
                              EquivalentDeuField = p1.EquivalentDeuField,
                              ControlHeight = p1.FieldHeight ?? 0,
                              FieldOrder = p1.FieldOrder ?? 0,
                              ControlX = p1.FieldPositionX.Value,
                              ControlY = p1.FieldPositionY.Value,
                              FieldStatusValue = p1.FieldStatus,
                              ControlWidth = p1.FieldWidth ?? 0,
                              FormulaId = p1.FormulaId,
                              HelpText = p1.HelpText,
                              IsCalculatedField = p1.IsCalculatedField,
                              IsZeroorBlankAllowed = p1.IsOorBlankAllowed,
                              IsOverrideOfCalcAllowed = p1.IsOverrideOfCalcAllowed,
                              IsPopulateIfLinked = p1.IsPopulatedIfLinked,
                              IsPartOfPrimaryKey = p1.IsPartOfPrimary,
                              IsTabbedToNextFieldIfLinked = p1.IsTabbedToNextFieldIfLinked,
                              LabelOnField = p1.LabelOnImage,
                              MaskFieldTypeId = p1.MasterPayorToolMaskFieldType.PTMaskFieldTypeId == null ? 0 : p1.MasterPayorToolMaskFieldType.PTMaskFieldTypeId,
                              MaskFieldType = p1.MasterPayorToolMaskFieldType.Type.Value,
                              MaskText = p1.MasterPayorToolMaskFieldType.Name,
                              PTAvailableFieldId = p1.MasterPayorToolAvailableField.PTAvailableFieldId == null ? 0 : p1.MasterPayorToolAvailableField.PTAvailableFieldId,
                              AvailableFieldName = p1.MasterPayorToolAvailableField.PTAvailableFieldId == null ? "" : p1.MasterPayorToolAvailableField.Name,
                              PayorFieldID = p1.PayorToolFieldId,
                              PayorToolId = p1.PayorTool.PayorToolId
                          }).ToList();

                        foreach (PayorToolField field in pTool.ToolFields)
                        {
                            if (field.FormulaId != null)
                            {
                                DLinq.Formula formula = DataModel.Formulas.FirstOrDefault(s => s.FormulaId == field.FormulaId);
                                if (formula != null)
                                {
                                    field.CalculationFormula = new Formula();
                                    field.CalculationFormula.FormulaExpression = formula.FormulaExpression;
                                    field.CalculationFormula.FormulaTtitle = formula.FormulaTtitle;
                                    field.CalculationFormula.FormulaID = formula.FormulaId;
                                }
                            }
                        }
                    }
                    return pTool;
                }
            }
            catch (Exception ex)
            {
                ActionLogger.Logger.WriteLog("GetPayorToolMgr:Exception occurs begins with payorId" + PayorID + ex.Message, true);
                throw ex;
            }
        }

        public static Guid GetPayorToolId(Guid PayorId)
        {
            try
            {
                ActionLogger.Logger.WriteLog("GetPayorToolId:processing begins with payorId" + PayorId, true);
                using (DLinq.CommissionDepartmentEntities DataModel = Entity.DataModel)
                {
                    DLinq.PayorTool payorTool = DataModel.PayorTools.FirstOrDefault(s => s.PayorId == PayorId && s.IsDeleted == false);
                    if (payorTool != null)
                        return payorTool.PayorToolId;
                    else
                        return Guid.NewGuid();
                }
            }
            catch (Exception ex)
            {
                ActionLogger.Logger.WriteLog("GetPayorToolId:Exception occurs begins with payorId" + PayorId + ex.Message, true);
                throw ex;
            }
        }
        /// <summary>
        /// CreatedBy:Ankit Khandelwal
        /// CreatedOn:12-Feb-2020
        /// Purpose:Add update the payor tool template
        /// </summary>
        /// <param name="PyrToolAvalableFields"></param>
        /// <returns></returns>
        public static void AddUpdatePayorToolTemplate(Guid templateId, string templateName, Guid payorId, out bool isTemplateExist)
        {
            ActionLogger.Logger.WriteLog("AddUpdatePayorToolTemplate: Processing begins with payorId" + payorId, true);
            try
            {
                isTemplateExist = false;
                using (DLinq.CommissionDepartmentEntities DataModel = Entity.DataModel)
                {
                    DLinq.PayorTemplate temp = DataModel.PayorTemplates.FirstOrDefault(s => s.TemplateName == templateName && s.PayorId == payorId && s.IsDeleted == false);
                    if (temp != null)
                    {
                        var result = String.Compare(temp.TemplateName, templateName, StringComparison.OrdinalIgnoreCase);
                        if (result == 0)
                        {
                            isTemplateExist = true;
                        }
                    }
                    else
                    {
                        temp = new DLinq.PayorTemplate();
                        temp.TemplateID = Guid.NewGuid();
                        temp.PayorId = payorId;
                        temp.TemplateName = templateName;
                        temp.IsDeleted = false;
                        DataModel.AddToPayorTemplates(temp);
                        DataModel.SaveChanges();

                    }
                }
            }
            catch (Exception ex)
            {
                ActionLogger.Logger.WriteLog("AddUpdatePayorToolTemplate: Processing begins with payorId" + payorId + ex.Message, true);
                throw ex;
            }
        }

        // ******************************************************Web Application Methods starting Point***************************************************************

        /// <summary>
        /// CreatedBY:Ankit khandelwal
        /// CreatedOn:26-march-2020
        /// Purpose:Get the Import tool payor template details
        /// </summary>
        /// <param name="selectedPayorId"></param>
        /// <param name="templateId"></param>
        /// <returns></returns>
        public static ImportToolTemplateDetails GetImportToolTemplateDetails(Guid selectedPayorId, Guid? templateId)
        {
            ActionLogger.Logger.WriteLog("GetImportToolTemplateDetails: Processing begins with payorId: " + selectedPayorId + " " + templateId, true);
            ImportToolTemplateDetails templateDetails = new ImportToolTemplateDetails();
            try
            {
                using (SqlConnection con = new SqlConnection(DBConnection.GetConnectionString()))
                {
                    using (SqlCommand cmd = new SqlCommand("usp_GetTemplateDetails", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@payorId", selectedPayorId);
                        cmd.Parameters.AddWithValue("@templateId", templateId);
                        con.Open();
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            templateDetails.FormatType = string.IsNullOrEmpty(reader["FormatType"].ToString()) ? "Tab" : Convert.ToString(reader["FormatType"]);
                            templateDetails.FileType = string.IsNullOrEmpty(reader["FileType"].ToString()) ? "xlsx" : reader["FileType"].ToString().ToLower();
                        }
                        reader.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                ActionLogger.Logger.WriteLog("GetImportToolTemplateDetails: Exception occurs while processing with templateId: " + templateId + " " + ex.Message, true);
                throw ex;
            }
            return templateDetails;
        }

        /// <summary>
        /// MOdifiedBy:Ankit Khandelwal
        /// MOdifiedOn:March-11-2020
        /// Purpose:Add/Update Import tool Payor Template
        /// </summary>
        /// <param name="selectedPayorId"></param>
        /// <param name="selectedTemplateId"></param>
        /// <param name="templateName"></param>
        /// <param name="isForceImport"></param>
        /// <returns></returns>
        public static bool SaveImportToolTemplate(Guid selectedPayorId, Guid selectedTemplateId, string templateName, bool isForceImport)
        {
            bool isTemplateExist = false;
            try
            {
                ActionLogger.Logger.WriteLog("SaveImportToolTemplate:Inner method Processing begins-" + selectedPayorId + " " + selectedTemplateId + " " + templateName + " " + isForceImport, true);
                using (DLinq.CommissionDepartmentEntities DataModel = Entity.DataModel)
                {
                    DLinq.ImportToolPayorTemplate isTemplateData = DataModel.ImportToolPayorTemplates.FirstOrDefault(s => s.PayorID == selectedPayorId && s.TemplateID != selectedTemplateId && s.TemplateName == templateName && s.IsDeleted == false);
                    DLinq.ImportToolPayorTemplate temp = DataModel.ImportToolPayorTemplates.FirstOrDefault(s => s.PayorID == selectedPayorId && s.TemplateID == selectedTemplateId && s.IsDeleted == false);
                    if (isTemplateData != null)
                    {
                        isTemplateExist = true;
                        return isTemplateExist;
                    }
                    else
                    {
                        if (temp == null)
                        {
                            //Add 
                            temp = new DLinq.ImportToolPayorTemplate();
                            temp.TemplateID = Guid.NewGuid();
                            temp.PayorID = selectedPayorId;
                            temp.TemplateName = templateName;
                            temp.IsDeleted = false;
                            temp.IsForceImport = isForceImport;
                            DataModel.AddToImportToolPayorTemplates(temp);
                        }
                        else
                        {
                            temp.TemplateName = templateName;
                            temp.IsForceImport = isForceImport;
                        }
                        DataModel.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                ActionLogger.Logger.WriteLog("SaveImportToolTemplate:Exception occurs while fetching details-" + selectedPayorId + " " + selectedTemplateId + " " + templateName + " " + isForceImport + "Exception:" + ex.Message, true);
                throw ex;
            }
            return isTemplateExist;

        }

        /// <summary>
        /// CreatedBy:Ak
        /// CreatedOn:18-March-2020
        /// Purpose:Add/update Import tool Payor Template
        /// </summary>
        /// <param name="templateId"></param>
        /// <param name="templateName"></param>
        /// <param name="payorId"></param>
        /// <param name="isForceImport"></param>
        /// <param name="isImportPayorTemplateExist"></param>
        public static void AddUpdateImportToolPayorTemplate(Guid templateId, string templateName, Guid payorId, bool isForceImport, out bool isImportPayorTemplateExist)
        {
            ActionLogger.Logger.WriteLog("AddUpdateImportToolPayorTemplate:Processing begins with templateId-" + templateId + " " + templateName + " " + payorId + " " + isForceImport, true);
            isImportPayorTemplateExist = false;
            bool isaddTemplate = true;
            try
            {
                using (DLinq.CommissionDepartmentEntities DataModel = Entity.DataModel)
                {
                    DLinq.ImportToolPayorTemplate templateData = DataModel.ImportToolPayorTemplates.FirstOrDefault(s => s.PayorID == payorId && s.TemplateName == templateName && s.TemplateID != templateId && s.IsDeleted == false);
                    if (templateData != null)
                    {
                        var result = String.Compare(templateData.TemplateName, templateName, StringComparison.OrdinalIgnoreCase);
                        if (result == 0)
                        {
                            isImportPayorTemplateExist = true;
                        }
                    }
                    else
                    {

                        DLinq.ImportToolPayorTemplate temp = DataModel.ImportToolPayorTemplates.FirstOrDefault(s => s.PayorID == payorId && s.TemplateID == templateId);
                        if (temp == null)
                        {
                            //Add 
                            temp = new DLinq.ImportToolPayorTemplate();
                            temp.TemplateID = templateId;
                            temp.PayorID = payorId;
                            temp.TemplateName = templateName;
                            temp.IsDeleted = false;
                            temp.IsForceImport = isForceImport;
                            DataModel.AddToImportToolPayorTemplates(temp);

                        }
                        else
                        {
                            isaddTemplate = false;
                            temp.TemplateName = templateName;
                            temp.IsForceImport = isForceImport;
                        }
                        DataModel.SaveChanges();
                        if (isaddTemplate == true)
                        {
                            ImportToolTemplateDetails templateDetails = new ImportToolTemplateDetails();
                            templateDetails.FormatType = "Tab";
                            templateDetails.FileType = "xlsx";
                            PayorTool.SaveImportToolTemplateDetails(payorId, templateId, templateDetails);
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                ActionLogger.Logger.WriteLog("AddUpdateImportToolPayorTemplate:Exception occurs while fetching details-" + templateId + " " + templateName + " " + payorId + " " + isForceImport + "Exception:" + ex.Message, true);
                throw ex;
            }


        }

        /// <summary>
        /// CreatedBy:Ankit Khandelwal
        /// CreatedOn:27-March2020
        /// PUrpose: Getting import tool payment data Available field list
        /// </summary>
        /// <returns></returns>
        public static List<PayorToolAvailablelFieldType> GetImportToolAvilableFieldList()
        {
            ActionLogger.Logger.WriteLog("GetImportToolAvilableFieldList:Processing begins:", true);
            List<PayorToolAvailablelFieldType> availableFields = new List<PayorToolAvailablelFieldType>();
            try
            {
                using (SqlConnection con = new SqlConnection(DBConnection.GetConnectionString()))
                {

                    using (SqlCommand cmd = new SqlCommand("usp_GeImportToolAvailableFieldList", con))
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
                        if (field.FieldName == "CommissionPercentage")
                        {
                            field.ImportToolTranslator = PayorTool.ImportToolTranslatorType();
                        }

                    }

                }
            }
            catch (Exception ex)
            {
                ActionLogger.Logger.WriteLog("GetImportToolAvilableFieldList:Exception occurs while processing:" + ex.Message, true);
                throw ex;
            }
            return availableFields;
            //    return availableFields;
            //}

        }
        /// <summary>
        /// ModifiedBy:Ankit Khandelwal
        /// ModifiedOn:April02,2020
        /// Purpose:Getting list of TranslatorType
        /// </summary>
        /// <returns></returns>
        public static List<TranslatorTypes> ImportToolTranslatorType()
        {
            ActionLogger.Logger.WriteLog("ImportToolTranslatorType:Processing begins", true);
            List<TranslatorTypes> list = new List<TranslatorTypes>();
            try
            {
                using (DLinq.CommissionDepartmentEntities DataModel = Entity.DataModel)
                {
                    list = (from p in DataModel.ImportToolCommTranslators
                            select new TranslatorTypes
                            {
                                TransID = p.TransID,
                                Name = p.Name,
                                Description = p.Description,
                                Type = (int)p.Type

                            }).ToList();
                }
            }
            catch (Exception ex)
            {
                ActionLogger.Logger.WriteLog("ImportToolTranslatorType:Exception occur while fetching details failure" + ex.Message, true);
                throw ex;
            }
            return list;
        }
        /// <summary>
        /// CreatedBy:Ankit Khandelwal
        /// CreatedOn:April 02,2020
        /// Purpose:Add update import tool payment data details
        /// </summary>
        /// <param name="importToolPaymentDataFieldsList"></param>
        public static void AddUpdatePaymentDataFieldsSetting(List<ImportToolPaymentDataFieldsSettings> importToolPaymentDataFieldsList)
        {
            ActionLogger.Logger.WriteLog("AddUpdatePaymentDataFieldsSetting:Processing begins" + importToolPaymentDataFieldsList[0].TemplateID, true);
            try
            {
                PayorTool.DeleteImportToolPaymentDataFields(importToolPaymentDataFieldsList[0].PayorID, importToolPaymentDataFieldsList[0].TemplateID);
                foreach (var data in importToolPaymentDataFieldsList)
                {
                    PayorTool.AddUpdatePaymentData(data);
                    using (DLinq.CommissionDepartmentEntities DataModel = Entity.DataModel)
                    {
                        DLinq.ImportToolPaymentDataFieldsSetting temp = DataModel.ImportToolPaymentDataFieldsSettings.FirstOrDefault(s => s.PayorID == data.PayorID && s.TemplateID == data.TemplateID && s.ID == data.ID);

                        if (temp == null)
                        {
                            temp = new DLinq.ImportToolPaymentDataFieldsSetting();
                            temp.PayorID = data.PayorID;
                            temp.TemplateID = data.TemplateID;
                            temp.PayorToolAvailableFeildsID = data.PayorToolAvailableFeildsID;
                            temp.FieldsID = data.FieldsID;
                            temp.FieldsName = data.FieldsName;
                            temp.FixedRowLocation = data.FixedRowLocation;
                            temp.FixedColLocation = data.FixedColLocation;
                            temp.HeaderSearch = data.HeaderSearch;
                            temp.RelativeRowLocation = data.RelativeRowLocation;
                            temp.RelativeColLocation = data.RelativeColLocation;
                            temp.PartOfPrimaryKey = data.PartOfPrimaryKey;
                            temp.CalculatedFields = data.CalculatedFields;
                            temp.FormulaExpression = data.FormulaExpression;
                            temp.PayorToolMaskFieldTypeId = data.PayorToolMaskFieldTypeId;
                            temp.StartColLocation = data.selectedPaymentDataStartColValue;
                            temp.StartRowLocation = data.selectedPaymentDataStartRowValue;
                            temp.EndColLocation = data.selectedPaymentDataEndColValue;
                            temp.EndRowLocation = data.selectedPaymentDataEndRowValue;
                            //Newly Added
                            temp.TransID = data.TransID;
                            temp.TransName = data.TransName;
                            temp.DefaultText = data.strDefaultText;

                            DataModel.AddToImportToolPaymentDataFieldsSettings(temp);

                        }
                        else
                        {
                            temp.ID = data.ID;
                            temp.FieldsName = data.FieldsName;
                            temp.FixedRowLocation = data.FixedRowLocation;
                            temp.FixedColLocation = data.FixedColLocation;
                            temp.HeaderSearch = data.HeaderSearch;
                            temp.RelativeRowLocation = data.RelativeRowLocation;
                            temp.RelativeColLocation = data.RelativeColLocation;
                            temp.PartOfPrimaryKey = data.PartOfPrimaryKey;
                            temp.CalculatedFields = data.CalculatedFields;
                            temp.FormulaExpression = data.FormulaExpression;
                            temp.PayorToolMaskFieldTypeId = data.PayorToolMaskFieldTypeId;
                            temp.StartColLocation = data.selectedPaymentDataStartColValue;
                            temp.StartRowLocation = data.selectedPaymentDataStartRowValue;
                            temp.EndColLocation = data.selectedPaymentDataEndColValue;
                            temp.EndRowLocation = data.selectedPaymentDataEndRowValue;
                            //Newly Added
                            temp.TransID = data.TransID;
                            temp.TransName = data.TransName;
                            //Added default text
                            temp.DefaultText = data.strDefaultText;
                        }
                        DataModel.SaveChanges();
                    }

                };
            }
            catch (Exception ex)
            {
                ActionLogger.Logger.WriteLog("AddUpdatePaymentDataFieldsSetting:Exception occur while fetching details failure" + importToolPaymentDataFieldsList[0].TemplateID + ex.Message, true);
                throw ex;
            }
        }
        /// <summary>
        /// CreatedBy:Ankit Khandelwal
        /// CreatedOn:April05,2020
        /// Purpose:Save the selectedField List in This table for shown field in desktop 
        /// </summary>
        /// <param name="objImportToolSeletedPaymentData"></param>
        public static void AddUpdatePaymentData(ImportToolPaymentDataFieldsSettings objImportToolSeletedPaymentData)
        {
            try
            {
                ActionLogger.Logger.WriteLog("AddUpdatePaymentData:processing  occur begins with details" + objImportToolSeletedPaymentData.TemplateID, true);
                using (DLinq.CommissionDepartmentEntities DataModel = Entity.DataModel)
                {
                    DLinq.ImportToolSeletedPaymentData temp = DataModel.ImportToolSeletedPaymentDatas.FirstOrDefault(s => s.PayorID == objImportToolSeletedPaymentData.PayorID && s.TemplateID == objImportToolSeletedPaymentData.TemplateID && s.FieldID == objImportToolSeletedPaymentData.FieldsID);
                    if (temp == null)
                    {
                        temp = new DLinq.ImportToolSeletedPaymentData();
                        temp.PayorID = objImportToolSeletedPaymentData.PayorID;
                        temp.TemplateID = objImportToolSeletedPaymentData.TemplateID;
                        temp.PayorToolAvailableFieldId = objImportToolSeletedPaymentData.PayorToolAvailableFeildsID;
                        temp.FieldID = objImportToolSeletedPaymentData.FieldsID;
                        temp.FieldName = objImportToolSeletedPaymentData.FieldsName;
                        DataModel.AddToImportToolSeletedPaymentDatas(temp);
                    }

                    DataModel.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                ActionLogger.Logger.WriteLog("AddUpdatePaymentData:Exception occur while fetching details" + objImportToolSeletedPaymentData.TemplateID + ex.Message, true);
                throw ex;
            }
        }
        /// <summary>
        /// Createdby:Ankit khandelwal
        /// Createdon:April 02,2020
        /// purpose:Getting details of payment data Fields
        /// </summary>
        /// <param name="payorId"></param>
        /// <param name="templateId"></param>
        /// <returns></returns>
        public static List<ImportToolPaymentDataFieldsSettings> GetImportPaymentDataFieldDetails(Guid payorId, Guid templateId)
        {
            ActionLogger.Logger.WriteLog("GetImportPaymentDataFieldDetails:processing begins with payorId" + payorId + " " + "templateId" + templateId, true);
            List<ImportToolPaymentDataFieldsSettings> fieldsDetails = new List<ImportToolPaymentDataFieldsSettings>();
            try
            {
                using (DLinq.CommissionDepartmentEntities DataModel = Entity.DataModel)
                {
                    fieldsDetails = (from p in DataModel.ImportToolPaymentDataFieldsSettings.Where(p => p.PayorID == payorId && p.TemplateID == templateId)
                                     select new ImportToolPaymentDataFieldsSettings
                                     {
                                         ID = p.ID,
                                         PayorID = (Guid)p.PayorID,
                                         TemplateID = (Guid)p.TemplateID,
                                         PayorToolAvailableFeildsID = (int)p.PayorToolAvailableFeildsID,
                                         FieldsID = (int)p.FieldsID,
                                         FieldsName = p.FieldsName,
                                         FixedColLocation = p.FixedColLocation,
                                         FixedRowLocation = p.FixedRowLocation,
                                         HeaderSearch = p.HeaderSearch,
                                         RelativeColLocation = p.RelativeColLocation,
                                         RelativeRowLocation = p.RelativeRowLocation,
                                         PartOfPrimaryKey = (bool)p.PartOfPrimaryKey,
                                         CalculatedFields = (bool)p.CalculatedFields,
                                         FormulaExpression = p.FormulaExpression,
                                         PayorToolMaskFieldTypeId = (int)p.PayorToolMaskFieldTypeId,
                                         selectedPaymentDataStartColValue = p.StartColLocation,
                                         selectedPaymentDataStartRowValue = p.StartRowLocation,
                                         selectedPaymentDataEndColValue = p.EndColLocation,
                                         selectedPaymentDataEndRowValue = p.EndRowLocation,
                                         TransID = p.TransID,
                                         TransName = p.TransName,
                                         strDefaultText = p.DefaultText,
                                         Disabled = true,
                                     }).ToList();

                    foreach (ImportToolPaymentDataFieldsSettings field in fieldsDetails)
                    {
                        field.MaskFieldType = PayorTool.GetDEUMaskFieldType(field.FieldsName);
                        field.MaskFieldList = PayorToolMaskedFieldType.GetSelectedMaskList(field.MaskFieldType);
                        if (field.FieldsName == "CommissionPercentage")
                        {
                            field.ImportToolTranslator = PayorTool.ImportToolTranslatorType();
                        }

                        if (!String.IsNullOrEmpty(field.FormulaExpression))
                        {
                            List<FormulaExpression> list = null;
                            list = GettingFormulaExpressionList(field.FormulaExpression);
                            field.CalculationFormula = new Formula();
                            field.CalculationFormula.FormulaExpression = field.FormulaExpression;
                            field.CalculationFormula.FormulaExpressionList = list;

                            // }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ActionLogger.Logger.WriteLog("GetImportPaymentDataFieldDetails:Exception occur while fetching details failure" + payorId + " " + "templateId" + templateId + ex.Message, true);
                throw ex;
            }
            return fieldsDetails;
        }
        /// <summary>
        /// CreatedBy:Ankit Kahndelwal
        /// CreateOn:April02,2020
        /// Purpose:Getting Import Tool Payment data MaskFieldType
        /// </summary>
        /// <param name="selectedPayorId"></param>
        /// <param name="templateId"></param>
        /// <param name="templateData"></param>
        public static int GetDEUMaskFieldType(string fieldName)
        {
            ActionLogger.Logger.WriteLog("GetDEUMaskFieldType: Processing begins with FieldName: " + fieldName, true);
            int value = 0;
            try
            {
                using (SqlConnection con = new SqlConnection(DBConnection.GetConnectionString()))
                {
                    using (SqlCommand cmd = new SqlCommand("usp_GetImportToolMaskFieldType", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@fieldName", fieldName);
                        cmd.Parameters.Add("@maskFieldType", SqlDbType.Int);
                        cmd.Parameters["@maskFieldType"].Direction = ParameterDirection.Output;
                        con.Open();
                        cmd.ExecuteScalar();
                        value = cmd.Parameters["@maskFieldType"].Value != null ? (int)cmd.Parameters["@maskFieldType"].Value : 0;
                    }
                }
            }
            catch (Exception ex)
            {
                ActionLogger.Logger.WriteLog("GetDEUMaskFieldType: Exception occurs while processing with fieldName: " + fieldName + " " + ex.Message, true);
                throw ex;
            }
            return value;
        }

        /// <summary>
        /// CreatedBy:Ankit Kahndelwal
        /// CreateOn:April02,2020
        /// Purpose:Getting Import Tool Payment data MaskFieldType
        /// </summary>
        /// <param name="selectedPayorId"></param>
        /// <param name="templateId"></param>
        /// <param name="templateData"></param>
        public static int DeleteImportToolPaymentDataFields(Guid payorId, Guid templateId)
        {
            ActionLogger.Logger.WriteLog("DeleteImportToolPaymentDataFields: Processing begins with payorId: " + payorId, true);
            int value = 0;
            try
            {
                using (SqlConnection con = new SqlConnection(DBConnection.GetConnectionString()))
                {
                    using (SqlCommand cmd = new SqlCommand("usp_DeleteImportToolPaymentDataFields", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@payorId", payorId);
                        cmd.Parameters.AddWithValue("@templateId", templateId);
                        con.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                ActionLogger.Logger.WriteLog("DeleteImportToolPaymentDataFields: Exception occurs while processing with payorId: " + payorId + "templateId:" + templateId + "" + ex.Message, true);
                throw ex;
            }
            return value;
        }

        /// <summary>
        /// CreatedBy:Jyotisna 
        /// Createdon:07/04/2020
        /// Puprose:Validate a formula Expression
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public static bool IsValidExpression(List<FormulaExpression> expression)
        {
            List<ExpressionToken> listTokens = new List<ExpressionToken>();
            bool isSuccess = true;
            try
            {

                ActionLogger.Logger.WriteLog("IsValidExpression:  - " + expression.ToStringDump(), true);
                //separate operater
                Regex rgVariable = new Regex(@"^[a-zA-Z]*$");
                Regex rgNumber = new Regex(@"^[0-9]*$");
                foreach (var s in expression)
                {
                    ExpressionToken token = new ExpressionToken();
                    token.TokenString = s.FormulaValue;
                    if (rgVariable.IsMatch(s.FormulaValue))
                    {
                        token.TokenType = ExpressionTokenType.Variable;
                    }
                    else if (s.FormulaValue == "(")
                    {
                        token.TokenType = ExpressionTokenType.OpenParanthesis;
                    }
                    else if (s.FormulaValue == ")")
                    {
                        token.TokenType = ExpressionTokenType.CloseParathesis;
                    }
                    else if (rgNumber.IsMatch(s.FormulaValue))
                    {
                        token.TokenType = ExpressionTokenType.Value;
                    }
                    else
                    {
                        token.TokenType = ExpressionTokenType.Operator;
                    }
                    listTokens.Add(token);
                }

                if (listTokens.Count > 2)
                {
                    for (int i = 1; i < listTokens.Count; i++)
                    {
                        if (!ExpressionValidationRule.ValidationRule(listTokens[i - 1], listTokens[i]))
                        {
                            isSuccess = false;
                            break;
                        }
                    }
                }
                else
                {
                    isSuccess = false;
                }
            }
            catch (Exception ex)
            {
                ActionLogger.Logger.WriteLog("IsValidExpression:Exception ex: " + ex.Message, true);
                throw ex;
            }
            return isSuccess;
        }

        //####################################################################################################################################
        #endregion
        public static List<FormulaExpression> GettingFormulaExpressionList(string expression)
        {
            List<FormulaExpression> expressionList = new List<FormulaExpression>();
            List<FormulaExpression> FormulaInputList = new List<FormulaExpression>();
            List<FormulaExpression> finalExp = new List<FormulaExpression>();
            try
            {

                string[] arr = expression.Split("[^A-Za-z0-9]");
                List<string> arrayList = System.Text.RegularExpressions.Regex.Split(expression, @"\s*([-+/*)(])\s*").Where(x => x != string.Empty).ToList();
                /*System.Text.RegularExpressions.Regex.Split(expression, "[a-zA-Z0-9]").Where(x => x != string.Empty).ToList();*/



                List<string> arrstrings = System.Text.RegularExpressions.Regex.Split(expression, "[^a-zA-Z0-9]").Where(x => x != string.Empty).ToList();

                string first = expression.Substring(0, 1);
                bool isBeginFromOperator = System.Text.RegularExpressions.Regex.IsMatch(first, "[^A-Za-z0-9]");

                //for (int i = 0; i < arrSymbols.Count; i++)
                //{
                //    FormulaExpression formulaData = new FormulaExpression();
                //    formulaData.OperatorName = arrSymbols[i];
                //    //formulaData.TokenName = arrstrings[i + 1];
                //    expressionList.Add(formulaData);
                //}

                for (int i = 0; i < arrstrings.Count; i++)
                {
                    FormulaExpression formulaData = new FormulaExpression();
                    formulaData.FormulaValue = arrstrings[i];
                    FormulaInputList.Add(formulaData);
                }
                foreach (var data in arrayList)
                {

                    FormulaExpression formulaData = new FormulaExpression();
                    formulaData.FormulaValue = data;
                    finalExp.Add(formulaData);

                }

                //Make final list

                //int stringIndex = 0;
                //if (isBeginFromOperator)
                //{
                //    for (int i = 0; i < arrSymbols.Count; i++)
                //    {
                //        FormulaExpression formulaData = new FormulaExpression();
                //        formulaData.FormulaValue = arrSymbols[i];
                //        finalExp.Add(formulaData);

                //        if (stringIndex < arrstrings.Count)
                //        {
                //            formulaData = new FormulaExpression();
                //            //  formulaData.FormulaValue = formulaData.TokenName = "";
                //            formulaData.FormulaValue = arrstrings[stringIndex];
                //            finalExp.Add(formulaData);
                //            stringIndex++;
                //        }
                //    }
                //}
                //else
                //{
                //    for (int i = 0; i < arrstrings.Count; i++)
                //    {
                //        FormulaExpression formulaData = new FormulaExpression();
                //        formulaData.FormulaValue = arrstrings[i];
                //        // formulaData.TokenName = formulaData.OperatorName = "";
                //        finalExp.Add(formulaData);

                //        if (stringIndex < arrSymbols.Count)
                //        {
                //            formulaData = new FormulaExpression();
                //            // formulaData.TokenName = formulaData.LableName = "";
                //            formulaData.FormulaValue = arrSymbols[stringIndex];
                //            finalExp.Add(formulaData);
                //            stringIndex++;
                //        }
                //    }
                //}
            }
            catch (Exception ex)
            {
                ActionLogger.Logger.WriteLog("GettingFormulaExpressionList:Exception ex: " + ex.Message, true);
                throw ex;
            }
            return finalExp;
        }

        //###########################################################################################################
        /// <summary>
        /// CreatedBy:Ankit Khandelwal
        /// CreatedOn:07/04/2020
        /// Purpose:Getting Invalid formula List
        /// </summary>
        /// <param name="importToolPaymentDataList"></param>
        /// <returns></returns>
        public static List<ImportToolInValidFieldList> ValidateFormulaExpression(List<ImportToolPaymentDataFieldsSettings> importToolPaymentDataList = null, List<PayorToolField> payorToolField = null)
        {
            List<ImportToolInValidFieldList> formulaInvalidList = new List<ImportToolInValidFieldList>();
            try
            {
                if (payorToolField == null)
                {
                    ActionLogger.Logger.WriteLog("ValidateFormulaExpression:processing begins for ImportToolPaymentDataFieldsSettings with:" + importToolPaymentDataList[0].PayorID + "" + importToolPaymentDataList[0].TemplateID, true);
                    foreach (var FieldData in importToolPaymentDataList)
                    {
                        if (!String.IsNullOrEmpty(FieldData.FormulaExpression))
                        {
                            List<FormulaExpression> list = null;
                            list = GettingFormulaExpressionList(FieldData.FormulaExpression);
                            bool isExpressionValid = PayorTool.IsValidExpression(list);
                            if (isExpressionValid == false)
                            {
                                ImportToolInValidFieldList selectedFieldData = new ImportToolInValidFieldList();
                                selectedFieldData.FieldsName = FieldData.FieldsName;
                                selectedFieldData.IsFormulaValid = false;
                                formulaInvalidList.Add(selectedFieldData);
                            }
                        }
                    }
                }
                else
                {
                    ActionLogger.Logger.WriteLog("ValidateFormulaExpression:processing begins with:" + payorToolField[0].TemplateID, true);
                    foreach (var FieldData in payorToolField)
                    {
                        if (FieldData.CalculationFormula != null && !String.IsNullOrEmpty(FieldData.CalculationFormula.FormulaExpression))
                        {
                            List<FormulaExpression> list = null;
                            list = GettingFormulaExpressionList(FieldData.CalculationFormula.FormulaExpression);
                            bool isExpressionValid = PayorTool.IsValidExpression(list);
                            if (isExpressionValid == false)
                            {
                                ImportToolInValidFieldList selectedFieldData = new ImportToolInValidFieldList();
                                selectedFieldData.FieldsName = FieldData.AvailableFieldName;
                                selectedFieldData.IsFormulaValid = false;
                                formulaInvalidList.Add(selectedFieldData);
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                ActionLogger.Logger.WriteLog("ValidateFormulaExpression:Exception occurs while processing:" + importToolPaymentDataList.ToStringDump() + "" + ex.Message, true);
                throw ex;
            }
            return formulaInvalidList;
        }
        /// <summary>
        /// UsedBy:Ankit Khandelwal
        /// UsedFor:Add update payorTool Details
        /// </summary>
        /// <param name="payorTool"></param>
        public static void AddUpdate(PayorTool payorTool)
        {
            try
            {
                ActionLogger.Logger.WriteLog("AddUpdate:processing begins with" + payorTool.ToStringDump(), true);

                using (DLinq.CommissionDepartmentEntities DataModel = Entity.DataModel)
                {
                    DLinq.PayorTool ObjPayor = null;

                    if (payorTool.TemplateID == null)
                    {
                        ObjPayor = (from p in DataModel.PayorTools
                                    where p.PayorId == payorTool.PayorID && p.TemplateID == null
                                    select p).FirstOrDefault();
                    }
                    else
                    {
                        ObjPayor = (from p in DataModel.PayorTools
                                    where p.PayorId == payorTool.PayorID && p.TemplateID == payorTool.TemplateID
                                    select p).FirstOrDefault();
                    }

                    if (ObjPayor != null)
                    {
                        // image path append to be removed on production, only for testing 
                        if (!string.IsNullOrEmpty(payorTool.StatementImageFilePath))
                        {
                            ObjPayor.StatementImageFile = "Images/" + payorTool.WebDevStatementImageFilePath; ; // "test_payorToolImages/" + payorTool.WebDevStatementImageFilePath;
                        }

                        if (!string.IsNullOrEmpty(payorTool.ChequeImageFilePath))
                        {
                            ObjPayor.ChequeImageFile = "Images/" + payorTool.WebDevChequeImageFilePath;  //"test_payorToolImages/" + payorTool.WebDevChequeImageFilePath;
                        }
                        // image path append to be removed on production, only for testing 

                    }
                    else
                    {
                        ObjPayor = new DLinq.PayorTool();
                        ObjPayor.StatementImageFile = payorTool.WebDevStatementImageFilePath;
                        ObjPayor.ChequeImageFile = payorTool.WebDevChequeImageFilePath;
                        ObjPayor.PayorToolId = payorTool.PayorToolId;
                        ObjPayor.PayorId = payorTool.PayorID;
                        //added New
                        ObjPayor.TemplateID = payorTool.TemplateID;
                        ObjPayor.CreatedOn = DateTime.Now;
                        DataModel.AddToPayorTools(ObjPayor);

                    }
                    ObjPayor.LastModifiedOn = DateTime.Now;
                    DataModel.SaveChanges();
                    AddUpdatePayorToolFields(payorTool);
                }
            }
            catch (Exception ex)
            {
                ActionLogger.Logger.WriteLog("AddUpdate:payorTool exception occurs while processing-" + ex.Message, true);
                throw ex;
            }
        }
        /// <summary>
        /// CreatedBy:Acmeminds
        /// CreatedOn:31-Jan-2019
        /// Purpose:Getting Template list based on Payor Selection
        /// </summary>
        /// <param name="selectedPayorId"></param>
        /// <returns></returns>
        public static List<Tempalate> GetPayorToolTemplate(Guid selectedPayorId, ListParams listParams, string selectedTabName, out int totalRecord, out string payorName)
        {
            ActionLogger.Logger.WriteLog("GetPayorToolTemplate: Processing begins with payorId: " + selectedPayorId, true);
            List<Tempalate> templateList = new List<Tempalate>();
            totalRecord = 0;
            payorName = "";
            try
            {
                using (DLinq.CommissionDepartmentEntities DataModel = Entity.DataModel)
                {
                    DLinq.Payor payorDetails = DataModel.Payors.FirstOrDefault(s => s.PayorId == selectedPayorId && s.IsDeleted == false);
                    payorName = payorDetails.PayorName;
                }
                using (SqlConnection con = new SqlConnection(DBConnection.GetConnectionString()))
                {
                    int rowStart = (listParams.pageSize * (listParams.pageIndex)) + 1;
                    int rowEnd = (listParams.pageIndex + 1) * listParams.pageSize;
                    using (SqlCommand cmd = new SqlCommand("usp_GetTemplateList", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@selectedTabName", selectedTabName);
                        cmd.Parameters.AddWithValue("@payorId", selectedPayorId);
                        cmd.Parameters.AddWithValue("@RowStart", rowStart);
                        cmd.Parameters.AddWithValue("@RowEnd", rowEnd);
                        cmd.Parameters.AddWithValue("@filterBy", listParams.filterBy);
                        cmd.Parameters.AddWithValue("@sortType", listParams.sortType);
                        cmd.Parameters.AddWithValue("@sortBy", listParams.sortBy);
                        cmd.Parameters.Add("@recordCount", SqlDbType.Int);
                        cmd.Parameters["@recordCount"].Direction = ParameterDirection.Output;
                        con.Open();
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            Tempalate data = new Tempalate();
                            data.TemplateID = reader.IsDBNull("TemplateId") ? Guid.Empty : (Guid)reader["TemplateId"];
                            data.TemplateName = reader.IsDBNull("TemplateName") ? "" : reader["TemplateName"].ToString();
                            data.ID = reader.IsDBNull("Id") ? 0 : (int)reader["Id"];
                            data.IsForceImport = reader.IsDBNull("IsForceImport") ? false : Convert.ToBoolean(reader["IsForceImport"]);
                            templateList.Add(data);
                        }
                        reader.Close();
                        totalRecord = (int)cmd.Parameters["@recordCount"].Value;
                    }

                }
            }
            catch (Exception ex)
            {
                ActionLogger.Logger.WriteLog("GetPayorToolTemplate: Exception occurs with payorId: " + selectedPayorId + " " + ex.Message, true);
                throw ex;
            }
            return templateList;
        }
        /// <summary>
        /// CreatedOn:31-Jan-2019
        /// CreatedBy:Acmeminds
        /// Purpose:Getting details of payortemplate
        /// </summary>
        /// <param name="payorId"></param>
        /// <param name="templateId"></param>
        /// <returns></returns>
        public static PayorTool GetPayorTemplateDetails(Guid payorId, Guid? templateId)
        {
            PayorTool pTool = new PayorTool();
            try
            {
                ActionLogger.Logger.WriteLog("GetPayorTemplateDetails: processing begins with payorId" + payorId + " " + "templateId" + templateId, true);
                string uniqueIDs = string.Empty;
                using (DLinq.CommissionDepartmentEntities DataModel = Entity.DataModel)
                {

                    if (templateId == null)
                    {
                        pTool = (from p in DataModel.PayorTools
                                 where p.Payor.PayorId == payorId && p.TemplateID == null && p.IsDeleted == false
                                 select new PayorTool
                                 {
                                     Disabled = false,
                                     ChequeImageFilePath = string.Empty,
                                     StatementImageFilePath = string.Empty,
                                     WebDevChequeImageFilePath = p.ChequeImageFile,
                                     WebDevStatementImageFilePath = p.StatementImageFile,
                                     PayorToolId = p.PayorToolId,
                                     PayorID = p.Payor.PayorId,
                                     TemplateID = p.TemplateID,
                                 }).FirstOrDefault();

                    }
                    else
                    {
                        pTool = (from p in DataModel.PayorTools
                                 where p.Payor.PayorId == payorId && p.TemplateID == templateId && p.IsDeleted == false
                                 select new PayorTool
                                 {
                                     Disabled = false,
                                     ChequeImageFilePath = string.Empty,
                                     StatementImageFilePath = string.Empty,
                                     WebDevChequeImageFilePath = p.ChequeImageFile,
                                     WebDevStatementImageFilePath = p.StatementImageFile,
                                     PayorToolId = p.PayorToolId,
                                     PayorID = p.Payor.PayorId,
                                     TemplateID = p.TemplateID,
                                 }).FirstOrDefault();
                    }
                    if (pTool != null)
                    {
                        if (!string.IsNullOrEmpty(pTool.WebDevStatementImageFilePath))
                        {

                            pTool.WebDevStatementImageFilePath = pTool.WebDevStatementImageFilePath.Replace("Images/", "");

                        }
                        if (!string.IsNullOrEmpty(pTool.WebDevChequeImageFilePath))
                        {

                            pTool.WebDevChequeImageFilePath = pTool.WebDevChequeImageFilePath.Replace("Images/", "");

                        }
                    }

                    if (pTool != null)
                    {
                        pTool.ToolFields =
                         (from p1 in DataModel.PayorToolFields
                          where p1.PayorTool.PayorToolId == pTool.PayorToolId && p1.IsDeleted == false
                          select new PayorToolField
                          {
                              Disabled = true,
                              AllignedDirection = p1.AllignedDirection ?? "Left",
                              DefaultValue = p1.DefaultNumeric,
                              EquivalentIncomingField = p1.EquivalentIncomingField,
                              EquivalentLearnedField = p1.EquivalentLearnedField,
                              EquivalentDeuField = p1.EquivalentDeuField,
                              ControlHeight = p1.FieldHeight ?? 0,
                              FieldOrder = p1.FieldOrder ?? 0,
                              ControlX = p1.FieldPositionX.Value,
                              ControlY = p1.FieldPositionY.Value,
                              FieldStatusValue = p1.FieldStatus,
                              IsRequired = p1.FieldStatus == "Required" ? true : false,
                              ControlWidth = p1.FieldWidth ?? 0,
                              FormulaId = p1.FormulaId,
                              HelpText = p1.HelpText,
                              IsCalculatedField = p1.IsCalculatedField,
                              IsZeroorBlankAllowed = p1.IsOorBlankAllowed,
                              IsOverrideOfCalcAllowed = p1.IsOverrideOfCalcAllowed,
                              IsPopulateIfLinked = p1.IsPopulatedIfLinked,
                              IsPartOfPrimaryKey = p1.IsPartOfPrimary,
                              IsTabbedToNextFieldIfLinked = p1.IsTabbedToNextFieldIfLinked,
                              LabelOnField = p1.LabelOnImage,
                              MaskFieldTypeId = p1.MasterPayorToolMaskFieldType.PTMaskFieldTypeId == null ? 0 : p1.MasterPayorToolMaskFieldType.PTMaskFieldTypeId,
                              MaskFieldType = p1.MasterPayorToolMaskFieldType.Type.Value,
                              MaskText = p1.MasterPayorToolMaskFieldType.Name,
                              PTAvailableFieldId = p1.MasterPayorToolAvailableField.PTAvailableFieldId == null ? 0 : p1.MasterPayorToolAvailableField.PTAvailableFieldId,
                              AvailableFieldName = p1.MasterPayorToolAvailableField.PTAvailableFieldId == null ? "" : p1.MasterPayorToolAvailableField.Name,
                              PayorFieldID = p1.PayorToolFieldId,
                              PayorToolId = p1.PayorTool.PayorToolId


                          }).OrderBy(x => x.FieldOrder).ToList();

                        foreach (PayorToolField field in pTool.ToolFields)
                        {
                            field.MaskFieldList = PayorToolMaskedFieldType.GetSelectedMaskList(field.MaskFieldType);
                            if (field.IsPartOfPrimaryKey)
                            {
                                uniqueIDs += field.AvailableFieldName + ", ";
                            }
                            if (field.FormulaId != null)
                            {
                                DLinq.Formula formula = DataModel.Formulas.FirstOrDefault(s => s.FormulaId == field.FormulaId);
                                if (formula != null)
                                {
                                    List<FormulaExpression> list = null;
                                    List<FormulaExpression> inputList = new List<FormulaExpression>();
                                    string FormulaFirstIndex = "";
                                    if (!String.IsNullOrEmpty(formula.FormulaExpression))
                                    {
                                        list = GettingFormulaExpressionList(formula.FormulaExpression);
                                        //FormulaFirstIndex = FormulaFirstIndexValue;
                                    }

                                    field.CalculationFormula = new Formula();
                                    field.CalculationFormula.FormulaExpression = formula.FormulaExpression;
                                    field.CalculationFormula.FormulaTtitle = formula.FormulaTtitle;
                                    field.CalculationFormula.FormulaID = formula.FormulaId;
                                    field.CalculationFormula.FormulaExpressionList = list;

                                }
                            }
                        }
                        if (uniqueIDs.Length > 1)
                        {
                            uniqueIDs = uniqueIDs.Substring(0, uniqueIDs.Length - 2);
                            pTool.UniqueIds = uniqueIDs;
                        }
                        ActionLogger.Logger.WriteLog("GetPayorTemplateDetails: Completed", true);

                    }
                }
            }
            catch (Exception ex)
            {
                ActionLogger.Logger.WriteLog("GetPayorTemplateDetails: Exception occurs with payorId: " + payorId + " " + ex.Message, true);
                throw ex;
            }
            return pTool;
        }
        /// <summary>
        /// createdBy:Ankit kahndelwal
        /// CreatedOn:Feb-17-2020
        /// New method with delete template functionality
        /// added to remove the need of extra objects from client
        /// </summary>
        /// <param name="payorId"></param>
        /// <param name="templateId"></param>
        public static void DeleteTemplate(Guid payorId, Guid? templateId)
        {
            ActionLogger.Logger.WriteLog("DeleteTemplate:processing begins for saving policy with payorId : " + payorId, true);
            try
            {
                using (SqlConnection con = new SqlConnection(DBConnection.GetConnectionString()))
                {
                    using (SqlCommand cmd = new SqlCommand("usp_DeletePayorTemplate", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@payorId", payorId);
                        cmd.Parameters.AddWithValue("@templateId", templateId);
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                ActionLogger.Logger.WriteLog("DeleteTemplate:Exception while saving policy:" + ex.Message, true);
                throw ex;
            }

        }
        /// <summary>
        /// Used by:Jyotisna
        /// </summary>
        /// <param name="sourcePayorId"></param>
        /// <param name="sourceTemplateId"></param>
        /// <returns></returns>
        public static bool CheckIfTemplateHasFields(Guid sourcePayorId, Guid? sourceTemplateId)
        {
            ActionLogger.Logger.WriteLog("CheckIfTemplateHasFields:Processing begins with sourcePayorId" + sourcePayorId + "sourceTemplateId:" + sourceTemplateId, true);
            try
            {
                using (DLinq.CommissionDepartmentEntities DataModel = Entity.DataModel)
                {
                    DLinq.PayorTool ObjPayorSource = null;

                    ObjPayorSource = sourceTemplateId == null ? (from p in DataModel.PayorTools
                                                                 where p.PayorId == sourcePayorId && p.TemplateID == null
                                                                 select p).FirstOrDefault() :
                                                                 (from p in DataModel.PayorTools
                                                                  where p.PayorId == sourcePayorId && p.TemplateID == sourceTemplateId
                                                                  select p).FirstOrDefault();
                    return (ObjPayorSource != null);
                }
            }
            catch (Exception ex)
            {
                ActionLogger.Logger.WriteLog("CheckIfTemplateHasFields:Exception occurs while processing" + sourcePayorId + " " + ex.Message, true);
                throw ex;
            }
        }
        /// <summary>
        /// Createdby:Jyotisna
        /// CreatedFor:Test Result fetch 
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="isFormulaExpressionValid"></param>
        /// <returns></returns>
        public static string GetTestFormulaResult(string expression, out bool isFormulaExpressionValid)
        {
            isFormulaExpressionValid = false;
            ActionLogger.Logger.WriteLog("GetTestFormulaResult:procesing begins with expression -" + expression, true);
            try
            {
                var result = new NCalc.Expression(expression).Evaluate();
                if (result.ToString().Contains("Infinity") || result.ToString().Contains("NaN"))
                    result = 0;
                isFormulaExpressionValid = true;
                return Convert.ToString(result);
            }
            catch (Exception ex)
            {
                ActionLogger.Logger.WriteLog("GetTestFormulaResult: exception occurs while processing-" + expression + " " + ex.Message, true);
                return "0";
            }

        }/// <summary>
         /// Createdby:Ankit Khandelwal
         /// CreatedFor:Getting list of payor Template Phrase
         /// </summary>
         /// <param name="payorId"></param>
         /// <param name="templateId"></param>
         /// <param name="listParams"></param>
         /// <param name="totalRecords"></param>
         /// <param name="templateDetails"></param>
         /// <returns></returns>
        public static List<ImportToolPayorPhrase> GetImportPayorTemplatePhrase(Guid payorId, Guid templateId, ListParams listParams, out int totalRecords, out ImportToolTemplateDetails templateDetails)
        {
            ActionLogger.Logger.WriteLog("GetImportPayorTemplatePhrase:processing begins with payorId-" + payorId + " " + templateId, true);
            List<ImportToolPayorPhrase> payorToolList = new List<ImportToolPayorPhrase>();
            totalRecords = 0;
            try
            {
                using (SqlConnection con = new SqlConnection(DBConnection.GetConnectionString()))
                {
                    int rowStart = (listParams.pageSize * (listParams.pageIndex)) + 1;
                    int rowEnd = (listParams.pageIndex + 1) * listParams.pageSize;
                    using (SqlCommand cmd = new SqlCommand("usp_GetTemplatePhraseList", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@PayorId", payorId);
                        cmd.Parameters.AddWithValue("@TemplateId", templateId);
                        cmd.Parameters.AddWithValue("@RowStart", rowStart);
                        cmd.Parameters.AddWithValue("@RowEnd", rowEnd);
                        cmd.Parameters.AddWithValue("@filterBy", listParams.filterBy);
                        cmd.Parameters.AddWithValue("@sortType", listParams.sortType);
                        cmd.Parameters.AddWithValue("@sortBy", listParams.sortBy);
                        cmd.Parameters.Add("@recordCount", SqlDbType.Int);
                        cmd.Parameters["@recordCount"].Direction = ParameterDirection.Output;
                        con.Open();
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            ImportToolPayorPhrase data = new ImportToolPayorPhrase();
                            data.TemplateID = reader.IsDBNull("TemplateId") ? Guid.Empty : (Guid)reader["TemplateId"];
                            data.TemplateName = reader.IsDBNull("TemplateName") ? "" : reader["TemplateName"].ToString();
                            data.PayorPhrases = reader.IsDBNull("PayorPhrases") ? "" : Convert.ToString(reader["PayorPhrases"]);
                            data.PayorName = reader.IsDBNull("PayorName") ? "" : Convert.ToString(reader["PayorName"]);
                            data.PayorID = reader.IsDBNull("PayorId") ? Guid.Empty : (Guid)reader["PayorId"];
                            data.ID = (int)(reader["Id"]);
                            payorToolList.Add(data);
                        }
                        reader.Close();
                        totalRecords = (int)cmd.Parameters["@recordCount"].Value;
                        templateDetails = PayorTool.GetImportToolTemplateDetails(payorId, templateId);
                    }
                }
            }
            catch (Exception ex)
            {
                ActionLogger.Logger.WriteLog("GetImportPayorTemplatePhrase: exception occurs while processing with templateId-" + templateId + " " + ex.Message, true);
                throw ex;
            }
            return payorToolList;
        }
        /// <summary>
        /// CreatedBY:Ankit khandelwal
        /// CreatedOn:26-march-2020
        /// Purpose:Save Import tool payor template details
        /// </summary>
        /// <param name="selectedPayorId"></param>
        /// <param name="templateId"></param>
        /// <returns></returns>
        public static void SaveImportToolTemplateDetails(Guid selectedPayorId, Guid? templateId, ImportToolTemplateDetails templateData)
        {
            ActionLogger.Logger.WriteLog("SaveImportToolTemplateDetails: Processing begins with payorId: " + selectedPayorId + " " + templateId, true);
            try
            {
                using (SqlConnection con = new SqlConnection(DBConnection.GetConnectionString()))
                {
                    using (SqlCommand cmd = new SqlCommand("usp_SaveImportPayorTemplateDetails", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@payorId", selectedPayorId);
                        cmd.Parameters.AddWithValue("@templateId", templateId);
                        cmd.Parameters.AddWithValue("@FormatType", templateData.FormatType);
                        cmd.Parameters.AddWithValue("@FileType", templateData.FileType);
                        con.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                ActionLogger.Logger.WriteLog("SaveImportToolTemplateDetails: Exception occurs while processing with templateId: " + templateId + " " + ex.Message, true);
                throw ex;
            }
        }

        /// <summary>
        /// createdby:Ankit Khandelwal
        /// CreatedOn:March23,2020
        /// Purpose:delete payorPhrase 
        /// </summary>
        /// <param name="intId"></param>
        public static void DeleteImportTemplatePhrase(int phraseId)
        {
            ActionLogger.Logger.WriteLog("DeleteImportTemplatePhrase:processing begins with Id" + phraseId, true);
            try
            {
                using (DLinq.CommissionDepartmentEntities DataModel = Entity.DataModel)
                {
                    DLinq.ImportToolPayorPhrase temp = DataModel.ImportToolPayorPhrases.FirstOrDefault(s => s.ID == phraseId);
                    if (temp != null)
                    {
                        DataModel.DeleteObject(temp);
                        DataModel.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                ActionLogger.Logger.WriteLog("DeleteImportTemplatePhrase:Exception occurs with Id" + phraseId + "Excpeption: " + ex.Message, true);
                throw ex;
            }
        }
        /// <summary>
        /// CreatedBy:Ankit Khandelwal
        /// Createdon:23-March-2020
        /// Purpose:Add update the payor Phrase details
        /// </summary>
        /// <param name="objImportToolPayorPhrase"></param>
        /// <param name="isForceToAdd"></param>
        /// <returns></returns>
        public static bool AddUpdateImportToolPayorPhrase(ImportToolPayorPhrase objImportToolPayorPhrase, bool isForceToAdd)
        {
            ActionLogger.Logger.WriteLog("AddUpdateImportToolPayorPhrase:Processing begins with objImportToolPayorPhrase" + objImportToolPayorPhrase.ToStringDump(), true);
            bool isPhraseExist = false;
            try
            {
                using (DLinq.CommissionDepartmentEntities DataModel = Entity.DataModel)
                {

                    DLinq.ImportToolPayorPhrase istemplatePhraseExist = DataModel.ImportToolPayorPhrases.FirstOrDefault(s => s.PayorPhrases == objImportToolPayorPhrase.PayorPhrases);
                    DLinq.ImportToolPayorPhrase templateData = DataModel.ImportToolPayorPhrases.FirstOrDefault(s => s.PayorID == objImportToolPayorPhrase.PayorID && s.TemplateID == objImportToolPayorPhrase.TemplateID && s.ID == objImportToolPayorPhrase.ID);
                    DLinq.Payor payorDetails = DataModel.Payors.FirstOrDefault(s => s.PayorId == objImportToolPayorPhrase.PayorID);
                    DLinq.ImportToolPayorTemplate templateDetails = DataModel.ImportToolPayorTemplates.FirstOrDefault(s => s.PayorID == objImportToolPayorPhrase.PayorID && s.TemplateID == objImportToolPayorPhrase.TemplateID);
                    if (isForceToAdd == false && istemplatePhraseExist != null)
                    {
                        isPhraseExist = true;
                    }
                    else if (((isForceToAdd == false || isForceToAdd == true) && istemplatePhraseExist == null) || (isForceToAdd == true && istemplatePhraseExist != null))
                    {

                        if (templateData == null)
                        {
                            templateData = new DLinq.ImportToolPayorPhrase();

                            templateData.TemplateID = objImportToolPayorPhrase.TemplateID;
                            templateData.TemplateName = templateDetails.TemplateName;
                            templateData.PayorID = objImportToolPayorPhrase.PayorID;
                            templateData.PayorName = payorDetails.PayorName;
                            templateData.FileType = objImportToolPayorPhrase.FileType;
                            templateData.FileFormat = objImportToolPayorPhrase.FileFormat;
                            templateData.FixedRowLocation = objImportToolPayorPhrase.FixedRowLocation;
                            templateData.FixedColLocation = objImportToolPayorPhrase.FixedColLocation;
                            templateData.RelativeSearchText = objImportToolPayorPhrase.RelativeSearchText;
                            templateData.RelativeRowLocation = objImportToolPayorPhrase.RelativeRowLocation;
                            templateData.RelativeColLocation = objImportToolPayorPhrase.RelativeColLocation;
                            templateData.PayorPhrases = objImportToolPayorPhrase.PayorPhrases;
                            DataModel.AddToImportToolPayorPhrases(templateData);
                        }
                        else
                        {
                            templateData.PayorPhrases = objImportToolPayorPhrase.PayorPhrases;
                        }

                        DataModel.SaveChanges();
                    }


                }
            }
            catch (Exception ex)
            {
                ActionLogger.Logger.WriteLog("AddUpdateImportToolPayorPhrase: Exception occurs while processing" + objImportToolPayorPhrase.PayorPhrases + " " + ex.Message, true);
                throw ex;
            }
            return isPhraseExist;

        }

        /// <summary>
        /// CreatedBy:Ankit khandelwal
        /// CreatedOn:Feb19 2020
        /// </summary>
        /// <param name="SourcePayorID"></param>
        /// <param name="SourceTempID"></param>
        /// <param name="DestinationPayorID"></param>
        /// <param name="DestiTempID"></param>
        /// <returns></returns>
        public static bool UpdateDulicatePayorTool(Guid sourcePayorId, Guid? sourceTemplateId, Guid destinationPayorId, Guid? destinationTemplateId)
        {
            ActionLogger.Logger.WriteLog("UpdateDulicatePayorTool:Processing begins with sourcePayorId" + sourcePayorId + "sourceTemplateId:" + sourceTemplateId + "destinationPayorId:" + destinationPayorId + "destinationTemplateId:" + destinationTemplateId, true);
            bool bValue = true;
            try
            {
                using (DLinq.CommissionDepartmentEntities DataModel = Entity.DataModel)
                {
                    DLinq.PayorTool ObjPayorSource = null;
                    DLinq.PayorTool ObjPayorTarget = null;

                    ObjPayorSource = sourceTemplateId == null ? (from p in DataModel.PayorTools
                                                                 where p.PayorId == sourcePayorId && p.TemplateID == null
                                                                 select p).FirstOrDefault() :
                                                                 (from p in DataModel.PayorTools
                                                                  where p.PayorId == sourcePayorId && p.TemplateID == sourceTemplateId
                                                                  select p).FirstOrDefault();

                    ObjPayorTarget = destinationTemplateId == null ? (from p in DataModel.PayorTools
                                                                      where p.PayorId == destinationPayorId && p.TemplateID == null
                                                                      select p).FirstOrDefault() :
                                                                      (from p in DataModel.PayorTools
                                                                       where p.PayorId == destinationPayorId && p.TemplateID == destinationTemplateId
                                                                       select p).FirstOrDefault();

                    if (ObjPayorTarget != null)
                    {
                        ActionLogger.Logger.WriteLog("UpdateDulicatePayorTool:ObjPayorTarget found:", true);

                        PayorTool sourcePayorTool = GetPayorTool(sourcePayorId, sourceTemplateId);
                        PayorTool targetPayorTool = GetPayorTool(destinationPayorId, destinationTemplateId);
                        //Update Image
                        ObjPayorTarget.StatementImageFile = sourcePayorTool.WebDevStatementImageFilePath;
                        ObjPayorTarget.ChequeImageFile = sourcePayorTool.WebDevChequeImageFilePath;
                        DataModel.SaveChanges();
                        UpdatePayorToolFields(sourcePayorTool, targetPayorTool);
                        ActionLogger.Logger.WriteLog("UpdateDulicatePayorTool:success: ", true);

                        return bValue;

                    }
                    else
                    {
                        ActionLogger.Logger.WriteLog("UpdateDulicatePayorTool:ObjPayorTarget NOT found: ", true);

                        PayorTool sourcePayorTool = GetPayorTool(sourcePayorId, sourceTemplateId);
                        ActionLogger.Logger.WriteLog("UpdateDulicatePayorTool:sourcePayorTool NOT found: ", true);

                        DLinq.PayorTool ObjPayor = null;
                        //Update Image
                        ObjPayor = new DLinq.PayorTool();
                        ObjPayor.StatementImageFile = sourcePayorTool.WebDevStatementImageFilePath;
                        ObjPayor.ChequeImageFile = sourcePayorTool.WebDevChequeImageFilePath;
                        ObjPayor.PayorToolId = Guid.NewGuid();
                        ObjPayor.PayorId = destinationPayorId;
                        ObjPayor.TemplateID = destinationTemplateId;
                        DataModel.AddToPayorTools(ObjPayor);
                        DataModel.SaveChanges();
                        ActionLogger.Logger.WriteLog("UpdateDulicatePayorTool:New payor tool added ", true);

                        PayorTool targetPayorTool = GetPayorTool(destinationPayorId, destinationTemplateId);
                        if (targetPayorTool == null)
                        {
                            ActionLogger.Logger.WriteLog("UpdateDulicatePayorTool:targetPayorTool is null: ", true);

                            sourcePayorTool.PayorToolId = destinationPayorId;
                            sourcePayorTool.TemplateID = destinationTemplateId;
                            AddUpdatePayorToolFields(sourcePayorTool);
                        }
                        else
                        {
                            ActionLogger.Logger.WriteLog("UpdateDulicatePayorTool:targetPayorTool is NOT null: ", true);
                            UpdatePayorToolFields(sourcePayorTool, targetPayorTool);
                        }
                        ActionLogger.Logger.WriteLog("UpdateDulicatePayorTool:success: ", true);

                    }
                }
            }
            catch (Exception ex)
            {
                ActionLogger.Logger.WriteLog("UpdateDulicatePayorTool: Exception occurs while processing " + ex.Message, true);
                bValue = false;
            }
            return bValue;
        }
        /// <summary>
        /// CreatedBy:Ankit khandelwal
        /// CreatedOn:Feb19 2020
        /// </summary>
        /// <param name="SourcePayorID"></param>
        /// <param name="SourceTempID"></param>
        /// <param name="DestinationPayorID"></param>
        /// <param name="DestiTempID"></param>
        /// <returns></returns>
        public static void DulicateImportPayorToolTemplate(Guid sourcePayorId, Guid? sourceTemplateId, Guid destinationPayorId, Guid? destinationTemplateId)
        {
            ActionLogger.Logger.WriteLog("DulicateImportPayorToolTemplate:Processing begins with sourcePayorId" + sourcePayorId + "sourceTemplateId:" + sourceTemplateId + "destinationPayorId:" + destinationPayorId + "destinationTemplateId:" + destinationTemplateId, true);
            try
            {
                using (SqlConnection con = new SqlConnection(DBConnection.GetConnectionString()))
                {
                    using (SqlCommand cmd = new SqlCommand("usp_PayorToolDuplicateTemplate", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@SourcePayorId", sourcePayorId);
                        cmd.Parameters.AddWithValue("@SourceTemplateId", sourceTemplateId);
                        cmd.Parameters.AddWithValue("@destinationPayorId", destinationPayorId);
                        cmd.Parameters.AddWithValue("@destinationTemplateId", destinationTemplateId);
                        con.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                ActionLogger.Logger.WriteLog("DulicateImportPayorToolTemplate:Exception occurs with sourcePayorId" + sourcePayorId + "sourceTemplateId:" + sourceTemplateId + "destinationPayorId:" + destinationPayorId + "destinationTemplateId:" + destinationTemplateId + "Exception: " + ex.Message, true);
                throw ex;
            }
        }
        /// <summary>
        /// CreatedBy:Ankit Khandelwal
        /// CreatedOn:18-March-2020
        /// Purpose:Delete a Payor Template
        /// </summary>
        /// <param name="SelectedPayortempalate"></param>
        /// <returns></returns>
        public static void DeleteImportPayorTemplate(Guid templateId)
        {
            ActionLogger.Logger.WriteLog("DeleteImportPayorTemplate:Processing starts with templateId:" + templateId, true);
            try
            {
                using (DLinq.CommissionDepartmentEntities DataModel = Entity.DataModel)
                {
                    DLinq.ImportToolPayorTemplate temp = DataModel.ImportToolPayorTemplates.FirstOrDefault(s => s.TemplateID == templateId);
                    if (temp != null)
                    {
                        temp.IsDeleted = true;
                        DataModel.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                ActionLogger.Logger.WriteLog("DeleteImportPayorTemplate:Processing starts with templateId:" + templateId + ex.Message, true);
                throw ex;
            }
        }
        /// <summary>
        /// ModifiedBy:Ankit khandelwal
        /// ModifiedOn:Feb19 2020
        /// Purpose:Get duplicate payor tools
        /// </summary>
        /// <param name="payorId"></param>
        /// <param name="templateId"></param>
        /// <returns></returns>
        public static PayorTool GetPayorTool(Guid payorId, Guid? templateId)
        {
            ActionLogger.Logger.WriteLog("GetPayorTool:Processing begins with payorId" + payorId + "templateId:" + templateId, true);
            using (DLinq.CommissionDepartmentEntities DataModel = Entity.DataModel)
            {
                PayorTool pTool = null;
                try
                {
                    if (templateId == null)
                    {
                        pTool = (from p in DataModel.PayorTools
                                 where p.PayorId == payorId && p.TemplateID == null && p.IsDeleted == false
                                 select new PayorTool
                                 {
                                     ChequeImageFilePath = p.ChequeImageFile,
                                     StatementImageFilePath = p.StatementImageFile,
                                     WebDevChequeImageFilePath = p.ChequeImageFile,
                                     WebDevStatementImageFilePath = p.StatementImageFile,
                                     PayorToolId = p.PayorToolId,
                                     PayorID = p.Payor.PayorId,
                                     TemplateID = p.TemplateID,
                                 }).FirstOrDefault();
                    }
                    else
                    {
                        pTool = (from p in DataModel.PayorTools
                                 where p.PayorId == payorId && p.TemplateID == templateId && p.IsDeleted == false
                                 select new PayorTool
                                 {
                                     ChequeImageFilePath = p.ChequeImageFile,
                                     StatementImageFilePath = p.StatementImageFile,
                                     WebDevChequeImageFilePath = p.ChequeImageFile,
                                     WebDevStatementImageFilePath = p.StatementImageFile,
                                     PayorToolId = p.PayorToolId,
                                     PayorID = p.Payor.PayorId,
                                     TemplateID = p.TemplateID,
                                 }).FirstOrDefault();
                    }

                    if (pTool != null)
                    {
                        ActionLogger.Logger.WriteLog("GetPayorTool:pTool != null", true);

                        pTool.ToolFields =
                         (from p1 in DataModel.PayorToolFields
                          where p1.PayorTool.PayorToolId == pTool.PayorToolId && p1.IsDeleted == false
                          select new PayorToolField
                          {
                              AllignedDirection = p1.AllignedDirection ?? "Left",
                              DefaultValue = p1.DefaultNumeric,
                              EquivalentIncomingField = p1.EquivalentIncomingField,
                              EquivalentLearnedField = p1.EquivalentLearnedField,
                              EquivalentDeuField = p1.EquivalentDeuField,
                              ControlHeight = p1.FieldHeight ?? 0,
                              FieldOrder = p1.FieldOrder ?? 0,
                              ControlX = p1.FieldPositionX.Value,
                              ControlY = p1.FieldPositionY.Value,
                              FieldStatusValue = p1.FieldStatus,
                              IsRequired = p1.FieldStatus == "Required" ? true : false,
                              ControlWidth = p1.FieldWidth ?? 0,
                              FormulaId = p1.FormulaId,
                              HelpText = p1.HelpText,
                              IsCalculatedField = p1.IsCalculatedField,
                              IsZeroorBlankAllowed = p1.IsOorBlankAllowed,
                              IsOverrideOfCalcAllowed = p1.IsOverrideOfCalcAllowed,
                              IsPopulateIfLinked = p1.IsPopulatedIfLinked,
                              IsPartOfPrimaryKey = p1.IsPartOfPrimary,
                              IsTabbedToNextFieldIfLinked = p1.IsTabbedToNextFieldIfLinked,
                              LabelOnField = p1.LabelOnImage,
                              MaskFieldTypeId = p1.MasterPayorToolMaskFieldType.PTMaskFieldTypeId == null ? 0 : p1.MasterPayorToolMaskFieldType.PTMaskFieldTypeId,
                              MaskFieldType = p1.MasterPayorToolMaskFieldType.Type.Value,
                              MaskText = p1.MasterPayorToolMaskFieldType.Name,
                              PTAvailableFieldId = p1.MasterPayorToolAvailableField.PTAvailableFieldId == null ? 0 : p1.MasterPayorToolAvailableField.PTAvailableFieldId,
                              AvailableFieldName = p1.MasterPayorToolAvailableField.PTAvailableFieldId == null ? "" : p1.MasterPayorToolAvailableField.Name,
                              PayorFieldID = p1.PayorToolFieldId,
                              PayorToolId = p1.PayorTool.PayorToolId
                          }).ToList();

                        ActionLogger.Logger.WriteLog("GetPayorTool:toolFields found", true);

                        foreach (PayorToolField field in pTool.ToolFields)
                        {
                            if (field.FormulaId != null)
                            {
                                DLinq.Formula formula = DataModel.Formulas.FirstOrDefault(s => s.FormulaId == field.FormulaId);
                                if (formula != null)
                                {
                                    field.CalculationFormula = new Formula();
                                    field.CalculationFormula.FormulaExpression = formula.FormulaExpression;
                                    field.CalculationFormula.FormulaTtitle = formula.FormulaTtitle;
                                    field.CalculationFormula.FormulaID = formula.FormulaId;
                                }
                            }
                        }
                    }
                    else
                    {
                        ActionLogger.Logger.WriteLog("GetPayorTool:pTool is null", true);
                    }
                }
                catch (Exception ex)
                {
                    ActionLogger.Logger.WriteLog("GetPayorTool:Exception occurs while processing:" + ex.Message, true);
                    throw ex;
                }

                return pTool;
            }
        }
        //duplicate payor tools
        /// <summary>
        /// Modified by:Ankit khandelwal
        /// MOdifiedOn:Feb19 2020
        /// </summary>
        /// <param name="SourcepayorTool"></param>
        /// <param name="TargetpayorTool"></param>
        private static void UpdatePayorToolFields(PayorTool sourcepayorTool, PayorTool targetpayorTool)
        {
            try
            {
                ActionLogger.Logger.WriteLog("UpdatePayorToolFields:Processing begins with payordata" + sourcepayorTool.ToStringDump() + "targetpayorTool:" + targetpayorTool.ToStringDump(), true);
                using (DLinq.CommissionDepartmentEntities DataModel = Entity.DataModel)
                {
                    List<DLinq.PayorToolField> payorToolFields = DataModel.PayorToolFields.Where(s => s.PayorToolId == targetpayorTool.PayorToolId && s.IsDeleted == false).ToList();
                    if (payorToolFields != null && payorToolFields.Count != 0)
                    {
                        foreach (DLinq.PayorToolField field in payorToolFields)
                            PayorToolField.DeletePayorToolFiledID(field.PayorToolFieldId);
                    }

                    foreach (PayorToolField Field in sourcepayorTool.ToolFields)
                    {
                        Field.PayorToolId = targetpayorTool.PayorToolId;
                        Field.TemplateID = targetpayorTool.TemplateID;
                        PayorToolField.UpdateDuplicatePayor(Field, targetpayorTool.PayorToolId, targetpayorTool.TemplateID);
                    }
                }
            }
            catch (Exception ex)
            {
                ActionLogger.Logger.WriteLog("UpdatePayorToolFields:Exception occurs while processing  with payordata" + sourcepayorTool.ToStringDump() + "targetpayorTool:" + targetpayorTool.ToStringDump() + ex.Message, true);
                throw ex;
            }
        }
        /// <summary>
        /// Modified by:Ankit khandelwal
        /// MOdifiedOn:Feb19 2020
        /// </summary>
        /// <param name="payorTool"></param>
        private static void AddUpdatePayorToolFields(PayorTool payorTool)
        {
            ActionLogger.Logger.WriteLog("AddUpdatePayorToolFields:Processing begins with payorId" + payorTool.ToStringDump(), true);
            try
            {
                using (DLinq.CommissionDepartmentEntities DataModel = Entity.DataModel)
                {
                    List<DLinq.PayorToolField> payorToolFields = DataModel.PayorToolFields.Where(s => s.PayorToolId == payorTool.PayorToolId && s.IsDeleted == false).ToList();
                    if (payorToolFields != null && payorToolFields.Count != 0)
                    {
                        payorToolFields = payorToolFields.Where(s => !payorTool.ToolFields.Exists(p => p.PayorFieldID == s.PayorToolFieldId)).ToList();
                        foreach (DLinq.PayorToolField field in payorToolFields)
                            PayorToolField.Delete(field.PayorToolFieldId);
                    }
                    foreach (PayorToolField Field in payorTool.ToolFields)
                    {
                        Field.PayorToolId = payorTool.PayorToolId;
                        Field.TemplateID = payorTool.TemplateID;
                        PayorToolField.AddUpdate(Field);
                    }
                }
            }
            catch (Exception ex)
            {
                ActionLogger.Logger.WriteLog("AddUpdatePayorToolFields:Processing begins with payorId" + payorTool.ToStringDump() + ex.Message, true);
                throw ex;
            }
        }
        #region
        [DataMember]
        public Guid PayorToolId { get; set; }
        [DataMember]
        public string ChequeImageFilePath { get; set; }
        [DataMember]
        public string StatementImageFilePath { get; set; }
        [DataMember]
        public string WebDevChequeImageFilePath { get; set; }
        [DataMember]
        public string WebDevStatementImageFilePath { get; set; }
        [DataMember]
        public List<PayorToolField> ToolFields { get; set; }
        [DataMember]
        public Guid PayorID { get; set; }

        [DataMember]
        public Guid? TemplateID { get; set; }
        [DataMember]
        public bool Disabled { get; set; }
        [DataMember]
        public string UniqueIds { get; set; }

        #endregion
    }
}
