using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using DLinq = DataAccessLayer.LinqtoEntity;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using MyAgencyVault.BusinessLibrary.Masters;

namespace MyAgencyVault.BusinessLibrary
{


    [DataContract]
    public class PolicyNotes : Note
    {

        #region policyNotes
        /// <summary>
        /// Author:Ankit 
        /// CreatedOn:Sept 19,2018
        /// Purpose:delete policyNotes based on policyId And NoteId
        /// </summary>
        /// <param name="policyId"></param>
        /// <param name="noteId"></param>
        /// <returns></returns>
        /// 
        public static int DeleteNoteByPolicyId(Guid policyId, Guid noteId)
        {
            ActionLogger.Logger.WriteLog("DeleteNoteByPolicyId:processing Starts with noteId:" + noteId + " " + "policyId" + policyId, true);
            int getDeleteStatus = 0;
            using (DLinq.CommissionDepartmentEntities DataModel = Entity.DataModel)
            {
                try
                {
                    var _policyNote = (from po in DataModel.PolicyNotes where po.PolicyId == policyId && po.PolicyNoteId == noteId select po).FirstOrDefault();
                    ActionLogger.Logger.WriteLog("DeleteNoteByPolicyId:processing is InProgress with" + "policyId:" + _policyNote.PolicyId, true);
                    if (_policyNote != null)
                    {

                        _policyNote.Isdeleted = true;
                        DataModel.SaveChanges();
                        getDeleteStatus = 1;
                        ActionLogger.Logger.WriteLog("DeleteNoteByPolicyId:PolicyId found  policy Deleted successfully" + "policyId:" + _policyNote.PolicyId, true);
                        return getDeleteStatus;
                    }
                    else
                    {
                        ActionLogger.Logger.WriteLog("DeleteNoteByPolicyId:PolicyId dosn't found with this policyId " + "policyId:" + _policyNote.PolicyId, true);
                        return getDeleteStatus;
                    }
                }
                catch (Exception ex)
                {
                    ActionLogger.Logger.WriteLog("DeleteNoteByPolicyId:PolicyId dosn't found with this policyId " + "policyId:" + policyId + " " + "ExceptionMessage:" + ex.Message, true);
                    throw ex;
                }
            }

        }

        /// <summary>
        /// CreatedBy:Acmeminds
        /// CreatedOn:28-10-2020
        /// Purpose:Delete policy note file
        /// </summary>
        /// <param name="policyId"></param>
        /// <param name="noteId"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static int DeletPolicyNoteFile(Guid policyId, Guid noteId, string fileName,bool isNoteMarkDeleted)
        {
            ActionLogger.Logger.WriteLog("DeletPolicyNoteFile:processing Starts with noteId:" + noteId + " " + "policyId" + policyId + " " + " fileName" + fileName, true);
            int isfileDeleted = 0;
            using (DLinq.CommissionDepartmentEntities DataModel = Entity.DataModel)
            {
                try
                {
                    string sourcePath = System.Configuration.ConfigurationSettings.AppSettings["PolicyNoteFilePath"];
                    String path = sourcePath + fileName;
                    ActionLogger.Logger.WriteLog("DeletPolicyNoteFile:file path for delete file:" + path, true);

                    DirectoryInfo directory = new DirectoryInfo(sourcePath);
                    foreach (FileInfo file in directory.GetFiles())
                    {
                        if (file.Name == fileName)
                        {
                            file.Delete();
                            ActionLogger.Logger.WriteLog("DeletPolicyNoteFile:file deleted successfully", true);
                            isfileDeleted = 1;
                        }
                    }

                   
                    if (isNoteMarkDeleted)
                    {
                        DeleteNoteByPolicyId(policyId, noteId);
                    }
                }
                catch (Exception ex)
                {
                    ActionLogger.Logger.WriteLog("DeletPolicyNoteFile:Exception occurs while delete uploaded file" + "policyId:" + policyId + " " + "ExceptionMessage:" + ex.Message, true);
                    throw ex;
                }
                return isfileDeleted;
            }

        }


        /// <summary>
        /// Author:Ankit khandelwal
        /// Created on:19-10-18
        /// purpose:getting list of notes based on policyId
        /// </summary>
        /// <returns></returns>
        public static List<PolicyNotes> GetNotes(Guid policyId, out int isErrorOccur, out List<UploadedFile> uploadedFileList)
        {
            List<PolicyNotes> policyNotes = new List<PolicyNotes>();
            uploadedFileList = new List<UploadedFile>();
            try
            {
                ActionLogger.Logger.WriteLog("GetNotes: processing start for fetching the list of notes policyId:" + policyId, true);
                isErrorOccur = 0;
                using (SqlConnection con = new SqlConnection(DBConnection.GetConnectionString()))
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = "usp_GetNotesForPolicy";
                    cmd.Connection = con;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@PolicyID", policyId);
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        PolicyNotes note = new PolicyNotes();
                        note.Content = ConvertRTFToText(Convert.ToString(reader["Note"]));
                        note.CreatedDate = (DateTime)reader["CreatedOn"];
                        note.LastModified = (reader["LastModifiedOn"] == DBNull.Value) ? (DateTime?)null : (DateTime?)reader["LastModifiedOn"];
                        note.PolicyId = (Guid)reader["PolicyId"];
                        note.NoteID = (Guid)reader["PolicyNoteId"];
                        note.CreatedBy = Convert.ToString(reader["CreatedBy"]);
                        note.ModifiedBy = Convert.ToString(reader["ModifiedBy"]);
                        if (string.IsNullOrEmpty(Convert.ToString(reader["FileName"])))
                        {
                            policyNotes.Add(note);
                        }
                        else
                        {
                            UploadedFile uploadedFileDetails = new UploadedFile();
                            uploadedFileDetails.FileName = Convert.ToString(reader["FileName"]);
                            uploadedFileDetails.PolicyId = (Guid)reader["PolicyId"];
                            uploadedFileDetails.PolicyNoteId = (Guid)reader["PolicyNoteId"];
                            uploadedFileDetails.UploadedBy = Convert.ToString(reader["UploadedBy"]);
                            uploadedFileDetails.UploadedOn = Convert.ToString(reader["UploadedDate"]);
                            uploadedFileDetails.UploadedFileName = Convert.ToString(reader["UploadedFileName"]);
                            uploadedFileList.Add(uploadedFileDetails);
                        };


                    }
                    //DataSet dataset = new DataSet();
                    //SqlParameter[] arrParam = new SqlParameter[1];
                    //arrParam[0] = new SqlParameter
                    //{
                    //    ParameterName = "@PolicyID",
                    //    Value = policyId
                    //};
                    //dataset = DBConnection.ExecuteQuery("usp_GetNotesForPolicy", arrParam, "PolicyNotes");
                    //if (dataset.Tables.Count > 0)
                    //{
                    //    DataTable dt = dataset.Tables[0];
                    //    policyNotes = DBConnection.DatatableToClass<PolicyNotes>(dt).ToList();
                    //}
                    //return policyNotes;

                }
                return policyNotes;

                /* using (DLinq.CommissionDepartmentEntities DataModel = Entity.DataModel)
                 {
                     List<PolicyNotes> policyNotes = new List<PolicyNotes>();
                     ActionLogger.Logger.WriteLog("GetNotes: processing is InProgress for fetching the list of notes policyId:" + policyId, true);
                     policyNotes = (from getnotes in DataModel.PolicyNotes
                                    join UserDetail in DataModel.UserDetails on getnotes.CreatedBy equals UserDetail.UserCredentialId into data from detail in data.DefaultIfEmpty()
                                    join UserDetail in DataModel.UserDetails on getnotes.ModifiedBy equals UserDetail.UserCredentialId into ModifiedData from Userdetail in ModifiedData.DefaultIfEmpty()
                                    where getnotes.PolicyId == policyId && getnotes.Isdeleted == null orderby  getnotes.CreatedOn descending
                                    select new PolicyNotes
                                    {
                                        Content = getnotes.Note,
                                        LastModified = (DateTime)getnotes.LastModifiedOn,
                                        CreatedDate = (DateTime)getnotes.CreatedOn,
                                        PolicyID = getnotes.Policy.PolicyId,
                                        NoteID = getnotes.PolicyNoteId,
                                        PolicyId = (Guid)getnotes.PolicyId,
                                        CreatedBy=  detail.NickName,
                                        ModifiedBy= Userdetail.NickName
                                    }
                         ).ToList();
                     foreach(Note n in policyNotes)
                     {
                         n.Content = ConvertRTFToText(n.Content);
                     }
                     return policyNotes;
                 }*/
            }
            catch (Exception ex)
            {
                isErrorOccur = 1;
                ActionLogger.Logger.WriteLog("GetNotes: exception occur while fetching the list of notes" + ex.Message, true);
                throw ex;
            }
        }
        /// <summary>
        /// CreateBy:Acmeminds
        /// CreatedOn:26-10-2020
        /// </summary>
        /// <param name="policyId"></param>
        /// <param name="fileName"></param>
        /// <param name="uploadedBy"></param>
        /// <returns></returns>
        public static void UploadPolicyNotesFile(Guid policyId, string fileName, Guid uploadedBy, string uploadedFileName)
        {
            ActionLogger.Logger.WriteLog("UploadPolicyNotesFile:Processing begins with policyId" + policyId, true);

            try
            {
                //nt isSuccess = 0;
                using (SqlConnection con = new SqlConnection(DBConnection.GetConnectionString()))
                {
                    using (SqlCommand cmd = new SqlCommand("usp_uploadPolicyNotesFile", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@PolicyId", policyId);
                        cmd.Parameters.AddWithValue("@FileName", fileName);
                        cmd.Parameters.AddWithValue("@UploadedBy", uploadedBy);
                        cmd.Parameters.AddWithValue("@uploadedFileName", uploadedFileName);
                        con.Open();
                        cmd.ExecuteNonQuery();
                    }

                }
            }
            catch (Exception ex)
            {
                ActionLogger.Logger.WriteLog("UploadPolicyNotesFile:Exception occurs while processing with policyId" + policyId + "exception" + ex.Message, true);

                throw ex;
            }
        }

        static string ConvertRTFToText(string textValue)
        {
            System.Windows.Forms.RichTextBox rtBox = new System.Windows.Forms.RichTextBox();
            try
            {
                rtBox.Rtf = textValue;
            }
            catch
            {
                rtBox.Text = textValue;
            }

            return rtBox.Text;
        }
        static string ConvertTextToRTF(string textValue)
        {
            System.Windows.Forms.RichTextBox rtBox = new System.Windows.Forms.RichTextBox();
            try
            {
                rtBox.Rtf = textValue;
            }
            catch
            {
                rtBox.Text = textValue;
            }

            return rtBox.Rtf;
        }


        /// <summary>
        /// Author:Ankit khandelwal
        /// CreatedOn:OCt 23,2018
        /// Purpose:Add update policy notes
        /// </summary>
        /// <param name="policyId"></param>
        /// <param name="note"></param>
        /// <param name="noteId"></param>
        public static void AddUpdatePolicyNotes(Guid policyId, string note, Guid noteId, out int CheckAddupdate, Guid? createdBy, Guid? modifiedBy)
        {
            CheckAddupdate = 0;
            ActionLogger.Logger.WriteLog("AddUpdatePolicyNotes: processing start with  policyId:" + policyId, true);
            try
            {
                using (DLinq.CommissionDepartmentEntities DataModel = Entity.DataModel)
                {

                    var _PolicyNote = (from pn in DataModel.PolicyNotes where (pn.PolicyNoteId == noteId && pn.PolicyId == policyId) select pn).FirstOrDefault();
                    if (_PolicyNote == null)
                    {
                        CheckAddupdate = 1;
                        Guid policyNoteId = noteId;
                        ActionLogger.Logger.WriteLog("AddUpdatePolicyNotes: Adding new noteId  with  policyId:" + policyId + " " + " noteId:" + noteId, true);

                        _PolicyNote = new DLinq.PolicyNote
                        {
                            PolicyNoteId = policyNoteId,
                            Note = ConvertTextToRTF(note),
                            PolicyId = policyId,
                            CreatedOn = DateTime.Now,
                            CreatedBy = createdBy,
                        };
                        _PolicyNote.PolicyReference.Value = (from pid in DataModel.Policies where pid.PolicyId == policyId select pid).FirstOrDefault();
                        DataModel.AddToPolicyNotes(_PolicyNote);
                        ActionLogger.Logger.WriteLog("AddUpdatePolicyNotes: Add note details successfully with new noteId and policyId:" + policyId + " " + " noteId:" + noteId, true);
                    }
                    else
                    {
                        CheckAddupdate = 2;
                        ActionLogger.Logger.WriteLog("AddUpdatePolicyNotes:  note details update  successfully with noteId and policyId:" + policyId + " " + " noteId:" + noteId, true);
                        _PolicyNote.Note = ConvertTextToRTF(note);
                        _PolicyNote.ModifiedBy = modifiedBy;
                        _PolicyNote.LastModifiedOn = DateTime.Now;
                    }
                    DataModel.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                ActionLogger.Logger.WriteLog("AddUpdatePolicyNotes:  Failure occur while add/update policynotes:" + policyId + " " + " noteId:" + noteId + " " + "Exception:" + ex.Message, true);
                throw ex;
            }
        }
        #endregion
        public override void AddUpdate()
        {
            using (DLinq.CommissionDepartmentEntities DataModel = Entity.DataModel)
            {
                var _PolicyNote = (from pn in DataModel.PolicyNotes where pn.PolicyNoteId == this.NoteID select pn).FirstOrDefault();

                if (_PolicyNote == null)
                {
                    _PolicyNote = new DLinq.PolicyNote
                    {
                        PolicyNoteId = this.NoteID,
                        Note = this.Content,
                        // LastModifiedOn = this.LastModifiedDate,
                        CreatedOn = this.CreatedDate
                    };
                    _PolicyNote.PolicyReference.Value = (from pid in DataModel.Policies where pid.PolicyId == this.PolicyID select pid).FirstOrDefault();
                    DataModel.AddToPolicyNotes(_PolicyNote);
                }

                else
                {
                    _PolicyNote.Note = this.Content;
                    _PolicyNote.LastModifiedOn = DateTime.Today;
                    // _PolicyNote.CreatedOn = this.CreatedDate;
                }
                DataModel.SaveChanges();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public override void Delete()
        {
            using (DLinq.CommissionDepartmentEntities DataModel = Entity.DataModel)
            {
                var _policyNote = (from po in DataModel.PolicyNotes where po.PolicyNoteId == this.NoteID select po).FirstOrDefault();
                if (_policyNote == null) return;
                DataModel.DeleteObject(_policyNote);
                DataModel.SaveChanges();
            }

        }


        public static List<PolicyNotes> GetNotes()
        {

            try
            {

                using (DLinq.CommissionDepartmentEntities DataModel = Entity.DataModel)
                {
                    ActionLogger.Logger.WriteLog("GetNotes: processing start for fetching the list of notes policyId:", true);
                    return (from getnotes in DataModel.PolicyNotes

                            select new PolicyNotes
                            {
                                Content = ConvertRTFToText(getnotes.Note),
                                LastModified = (DateTime)getnotes.LastModifiedOn,
                                CreatedDate = (DateTime)getnotes.CreatedOn,
                                PolicyID = getnotes.Policy.PolicyId,
                                NoteID = getnotes.PolicyNoteId,
                                PolicyId = (Guid)getnotes.PolicyId
                            }
                        ).ToList();

                }
            }
            catch (Exception ex)
            {

                ActionLogger.Logger.WriteLog("GetNotes: exception occur while fetching the list of notes" + ex.Message, true);
                throw ex;
            }
        }
        //public static List<PolicyNotes> GetNotesPolicyWise(Guid PolicyId)
        //{
        //    List<PolicyNotes> _PolicyNotes = GetNotes().Where(p => p.PolicyID == PolicyId).OrderByDescending(p=>p.CreatedDate).ToList();
        //    return _PolicyNotes;

        //}


        public static List<PolicyNotes> GetNotesPolicyWise(Guid PolicyId)
        {
            List<PolicyNotes> _PolicyNotes = new List<PolicyNotes>();
            try
            {
                using (DLinq.CommissionDepartmentEntities DataModel = Entity.DataModel)
                {
                    _PolicyNotes = (from pn in DataModel.PolicyNotes.Where(p => p.PolicyId == PolicyId)
                                    select new PolicyNotes
                                    {
                                        Content = ConvertRTFToText(pn.Note),
                                        // LastModifiedDate = pn.LastModifiedOn,
                                        PolicyID = PolicyId,
                                        //  CreatedDate = pn.CreatedOn,
                                        LastModified = (DateTime)pn.LastModifiedOn,
                                        CreatedDate = (DateTime)pn.CreatedOn,
                                        NoteID = pn.PolicyNoteId
                                    }).ToList();
                }
            }
            catch (Exception ex)
            {
                ActionLogger.Logger.WriteLog(ex.StackTrace.ToString(), true);
            }
            return _PolicyNotes;

        }


        [DataMember]
        public Guid PolicyID { get; set; }

    }
}
