using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyAgencyVault.BusinessLibrary.Base;
using System.Runtime.Serialization;
using DLinq = DataAccessLayer.LinqtoEntity;
using Masters = MyAgencyVault.BusinessLibrary.Masters;
using DataAccessLayer.LinqtoEntity;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Data;
using System.Data.EntityClient;
using MyAgencyVault.BusinessLibrary.BusinessObjects;

namespace MyAgencyVault.BusinessLibrary
{
    [DataContract]
    public class GlobalPayorContact : IEditable<GlobalPayorContact>
    {
        #region "DataMember aka - public properties "
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public Guid PayorContactId { get; set; }
        [DataMember]
        public Guid GlobalPayorId { get; set; }
        [DataMember]
        public string FirstName { get; set; }
        [DataMember]
        public string LastName { get; set; }
        [DataMember]
        public string Zip { get; set; }
        [DataMember]
        public string City { get; set; }
        [DataMember]
        public string State { get; set; }
        [DataMember]
        public string Email { get; set; }
        [DataMember]
        public string OfficePhone { get; set; }
        [DataMember]
        public string Fax { get; set; }
        [DataMember]
        public int? Priority { get; set; }
        [DataMember]
        public string ContactPref { get; set; }
        [DataMember]
        public bool IsDeleted { get; set; }
        #endregion 
        #region IEditable<GlobalPayorContact> Members

        public void AddUpdate()
        {
            try
            {
                using (DLinq.CommissionDepartmentEntities DataModel = Entity.DataModel)
                {

                    DLinq.GlobalPayorContact _contact = (from c in DataModel.GlobalPayorContacts
                                                         where c.PayorContactId == this.PayorContactId
                                                         select c).FirstOrDefault();
                    if (_contact == null)
                    {
                        _contact = new DLinq.GlobalPayorContact
                        {
                            PayorContactId = this.PayorContactId,
                            FirstName = this.FirstName,
                            LastName = this.LastName,
                            ZipCode = this.Zip.CustomParseToLong(),
                            city = this.City,
                            state = this.State,
                            email = this.Email,
                            OfficePhone = this.OfficePhone,
                            Fax = this.Fax,
                            Priority = this.Priority,
                            ContactPref = this.ContactPref,
                            IsDeleted = this.IsDeleted,

                        };
                        _contact.Payor = Masters.ReferenceMaster.GetReferencedPayor(this.GlobalPayorId, DataModel);
                        DataModel.AddToGlobalPayorContacts(_contact);
                    }
                    else
                    {
                        _contact.FirstName = this.FirstName;
                        _contact.LastName = this.LastName;
                        _contact.ZipCode = this.Zip.CustomParseToLong();
                        _contact.city = this.City;
                        _contact.state = this.State;
                        _contact.email = this.Email;
                        _contact.OfficePhone = this.OfficePhone;
                        _contact.Fax = this.Fax;
                        _contact.Priority = this.Priority;
                        _contact.ContactPref = this.ContactPref;
                        _contact.IsDeleted = this.IsDeleted;

                    }
                    DataModel.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                ActionLogger.Logger.WriteLog("AddUpdate payor contact failure ex: " + ex.Message, true);
                throw ex; 
            }
        }

        public void Delete()
        {
            throw new NotImplementedException();
        }


        public void Delete(Guid contactID)
        {
            try
            {
                using (DLinq.CommissionDepartmentEntities DataModel = Entity.DataModel)
                {
                    var PayorContact = (from c in DataModel.GlobalPayorContacts
                                        where c.PayorContactId == contactID
                                        select c).FirstOrDefault();
                    PayorContact.IsDeleted = true;
                    DataModel.SaveChanges();
                }
                ActionLogger.Logger.WriteLog("DeleteGlobalPayorContact success: ", true);
            }
            catch (Exception ex)
            {
                ActionLogger.Logger.WriteLog("DeleteGlobalPayorContact failure: " + ex.Message, true);
                throw ex;
            }
        }

        public static List<GlobalPayorContact> getContacts(Guid PayorId, ListParams listParams, out int count)
        {
            List<GlobalPayorContact> contacts = new List<GlobalPayorContact>();
            count = 0;
            try
            {
                using (SqlConnection con = new SqlConnection(DBConnection.GetConnectionString()))
                {
                    using (SqlCommand cmd = new SqlCommand("usp_GetPayorContactsList", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        int rowStart = (listParams.pageSize == 0 && listParams.pageIndex == 0) ? 0 : (listParams.pageSize * (listParams.pageIndex)) + 1;
                        int rowEnd = (listParams.pageSize == 0 && listParams.pageIndex == 0) ? 0 : (listParams.pageIndex + 1) * listParams.pageSize;
                        cmd.Parameters.AddWithValue("@RowStart", rowStart);
                        cmd.Parameters.AddWithValue("@RowEnd", rowEnd);
                        cmd.Parameters.AddWithValue("@sortBy", listParams.sortBy);
                        cmd.Parameters.AddWithValue("@sortType", listParams.sortType);
                        cmd.Parameters.AddWithValue("@filterBy", listParams.filterBy);
                        cmd.Parameters.AddWithValue("@payorId", PayorId);
                        cmd.Parameters.Add("@recordLength", SqlDbType.Int);
                        cmd.Parameters["@recordLength"].Direction = ParameterDirection.Output;
                        con.Open();
                        SqlDataReader dr = cmd.ExecuteReader();
                        while (dr.Read())
                        {
                            GlobalPayorContact contactObj = new GlobalPayorContact();
                            contactObj.PayorContactId = (Guid)dr["PayorContactId"];
                            contactObj.FirstName = Convert.ToString(dr["FirstName"]);
                            contactObj.LastName = Convert.ToString(dr["LastName"]);
                            contactObj.Priority = dr.IsDBNull("Priority") ? 0 : Convert.ToInt32(dr["Priority"]);
                            contactObj.Email = Convert.ToString(dr["Email"]);
                            contactObj.OfficePhone = Convert.ToString(dr["OfficePhone"]);
                            contactObj.GlobalPayorId = (Guid)dr["PayorId"];
                            contactObj.ContactPref = Convert.ToString(dr["ContactPref"]);
                            contacts.Add(contactObj);
                        }
                        dr.Close();
                        int.TryParse(Convert.ToString(cmd.Parameters["@recordLength"].Value), out count);
                    }
                }
            }
            catch (Exception ex)
            {
                ActionLogger.Logger.WriteLog("getContacts exception " + ex.Message, true);
                throw ex;
            }
            return contacts;
        }

        /*public static List<GlobalPayorContact> getContacts(Guid PayorId, out int count)
        {
            using (DLinq.CommissionDepartmentEntities DataModel = Entity.DataModel)
            {
                List<GlobalPayorContact> contacts = (from r in DataModel.GlobalPayorContacts
                                                     where (r.Payor.PayorId == PayorId) && (r.IsDeleted == false)
                                                     select new GlobalPayorContact
                                                     {
                                                         PayorContactId = r.PayorContactId,
                                                         FirstName = r.FirstName,
                                                         LastName = r.LastName,
                                                         City = r.city,
                                                         State = r.state,
                                                         ContactPref = r.ContactPref,
                                                         Email = r.email,
                                                         Fax = r.Fax,
                                                         GlobalPayorId = r.Payor.PayorId,
                                                         IsDeleted = r.IsDeleted,
                                                         OfficePhone = r.OfficePhone,
                                                         Priority = r.Priority
                                                     }).ToList();

                count = contacts.Count;
                foreach (GlobalPayorContact contact in contacts)
                {
                    DLinq.GlobalPayorContact cnt = DataModel.GlobalPayorContacts.First(s => s.PayorContactId == contact.PayorContactId);
                    if (cnt.ZipCode != null)
                        contact.Zip = cnt.ZipCode.Value.ToString("D5");
                    else
                        contact.Zip = null;
                }
                return contacts;
            }
        }*/


        /// <summary>
        /// Created By :Ankit khandelwal
        /// Created On:08-08-2019
        /// </summary>
        /// <param name="details"></param>
        /// <returns></returns>
        public static int AddUpdatePayorContact(PayorContact details)
        {
            ActionLogger.Logger.WriteLog("AddUpdatePayorContact: process starts PayorContactId" + details.PayorContactId, true);
            int status = 0;
            DLinq.CommissionDepartmentEntities ctx = new DLinq.CommissionDepartmentEntities(); //create your entity object here
            EntityConnection ec = (EntityConnection)ctx.Connection;
            SqlConnection sc = (SqlConnection)ec.StoreConnection; //get the SQLConnection that your entity object would use
            string adoConnStr = sc.ConnectionString;
            try
            {

                if(details.OfficePhone == null)
                {
                    details.OfficePhone = new ContactDetails();
                }
                if(details.Fax == null)
                {
                    details.Fax = new ContactDetails();
                }
                using (SqlConnection con = new SqlConnection(adoConnStr))
                {
                    using (SqlCommand cmd = new SqlCommand("usp_AddUpdatePayorContactdetails", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@PayorContactId", details.PayorContactId);
                        cmd.Parameters.AddWithValue("@GlobalPayorId", details.GlobalPayorId);
                        cmd.Parameters.AddWithValue("@FirstName", details.FirstName);
                        cmd.Parameters.AddWithValue("@LastName", details.LastName);
                        cmd.Parameters.AddWithValue("@Email", details.Email);
                        cmd.Parameters.AddWithValue("@OfficePhone", details.OfficePhone.PhoneNumber ?? "");
                        cmd.Parameters.AddWithValue("@Fax", details.Fax.PhoneNumber);
                        cmd.Parameters.AddWithValue("@Priority", details.Priority);
                        cmd.Parameters.AddWithValue("@ContactPref", details.ContactPref);
                        cmd.Parameters.AddWithValue("@Address", details.FormattedAddress);
                        cmd.Parameters.AddWithValue("@OfficeDialCode", details.OfficePhone.DialCode);
                        cmd.Parameters.AddWithValue("@FaxDialCode", details.Fax.DialCode);
                        cmd.Parameters.AddWithValue("@OfficeCountryCode", details.OfficePhone.CountryCode);
                        cmd.Parameters.AddWithValue("@FaxCountryCode", details.Fax.CountryCode);
                        cmd.Parameters.Add("@StatusCode", SqlDbType.Int);
                        cmd.Parameters["@StatusCode"].Direction = ParameterDirection.Output;
                        con.Open();
                        cmd.ExecuteScalar();
                        status = (int)cmd.Parameters["@StatusCode"].Value;
                    }
                }
            }
            catch (Exception ex)
            {
                ActionLogger.Logger.WriteLog("AddUpdatePayorContact exception occurs" + details.PayorContactId + "Exception:" + ex.Message, true);
                throw ex;
            }
            return status;
        }
        public static PayorContact GetPayorContactDetails(Guid payorContactId)
        {
            try
            {
                PayorContact payorcontact = new PayorContact();
                payorcontact.OfficePhone = new ContactDetails();
                payorcontact.Fax = new ContactDetails();
                using (SqlConnection con = new SqlConnection(DBConnection.GetConnectionString()))
                {
                    using (SqlCommand cmd = new SqlCommand("usp_GetPayorContactdetails", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@PayorContactId", payorContactId);
                        con.Open();
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            payorcontact.FirstName = !reader.IsDBNull("FirstName") ? Convert.ToString(reader["FirstName"]) : null;
                            payorcontact.LastName = !reader.IsDBNull("LastName") ? Convert.ToString(reader["LastName"]) : null;
                            payorcontact.Email = !reader.IsDBNull("Email") ? Convert.ToString(reader["Email"]) : null;
                            payorcontact.FormattedAddress = !reader.IsDBNull("Address") ? Convert.ToString(reader["Address"]) : null;
                            payorcontact.Priority = !reader.IsDBNull("Priority") ? (int)reader["Priority"] : 1;
                            payorcontact.ContactPref = !reader.IsDBNull("ContactPref") ? Convert.ToString(reader["ContactPref"]) : null;
                            payorcontact.OfficePhone.PhoneNumber = !reader.IsDBNull("OfficePhone") ? Convert.ToString(reader["OfficePhone"]) : null;
                            payorcontact.Fax.PhoneNumber = !reader.IsDBNull("Fax") ? Convert.ToString(reader["Fax"]) : null;
                            payorcontact.OfficePhone.CountryCode = !reader.IsDBNull("OfficePhone_CountryCode") ? Convert.ToString(reader["OfficePhone_CountryCode"]) : "us";
                            payorcontact.OfficePhone.DialCode = !reader.IsDBNull("OfficePhone_DialCode") ? Convert.ToString(reader["OfficePhone_DialCode"]) : "+1";
                            payorcontact.Fax.CountryCode = !reader.IsDBNull("Fax_CountryCode") ? Convert.ToString(reader["Fax_CountryCode"]) : "us";
                            payorcontact.Fax.DialCode = !reader.IsDBNull("Fax_DialCode") ? Convert.ToString(reader["Fax_DialCode"]) : "+1";
                            payorcontact.PayorContactId = (Guid)reader["PayorContactId"];
                            payorcontact.GlobalPayorId = (Guid)reader["GlobalPayorId"];
                        }
                    }
                }

                return payorcontact;
            }
            catch(Exception ex)
            {
                ActionLogger.Logger.WriteLog("GetPayorContactDetails ex: " + ex.Message, true);
                throw ex;
            }

        }
        public GlobalPayorContact GetOfID()
        {
            throw new NotImplementedException();
        }

        public bool IsValid()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
