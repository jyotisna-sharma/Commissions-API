using MyAgencyVault.BusinessLibrary.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Runtime.Serialization;

namespace MyAgencyVault.BusinessLibrary
{
    [DataContract]
    public class LinkPaymentReciptRecords
    {
        [DataMember]
        public Guid PaymentEntryID { get; set; }
        [DataMember]
        public Guid PolicyId { get; set; }
        [DataMember]
        public string PolicyNumber { get; set; }
        DateTime? invoiceDate;
        [DataMember]
        public DateTime? InvoiceDate
        {
            get
            {
                return invoiceDate;
            }
            set
            {
                invoiceDate = value;
                if (value != null && string.IsNullOrEmpty(InvoiceDateString))
                {
                    InvoiceDateString = Convert.ToDateTime(value).ToString("MMM dd, yyyy");
                }
            }
        }
        string invoiceDateString;
        [DataMember]
        public string InvoiceDateString
        {
            get
            {
                return invoiceDateString;
            }
            set
            {
                invoiceDateString = value;
                if (InvoiceDate == null && !string.IsNullOrEmpty(invoiceDateString))
                {
                    DateTime dt;
                    DateTime.TryParse(invoiceDateString, out dt);
                    InvoiceDate = dt;
                }
            }
        }
        [DataMember]
        public decimal? PaymentRecived { get; set; }
        [DataMember]
        public double? CommissionPercentage { get; set; }//incoming Percentage
        [DataMember]
        public int? NumberOfUnits { get; set; }
        [DataMember]
        public decimal? DollerPerUnit { get; set; }
        [DataMember]
        public decimal? Fee { get; set; }
        [DataMember]
        public double? SplitPer { get; set; }
        [DataMember]
        public decimal? TotalPayment { get; set; }

        [DataMember]
        public bool IsSelected { get; set; }


        [DataMember]
        public bool IsAllChecked { get; set; }


        /// <summary>
        /// Modified By:Ankit khandelwal
        /// Modified on:31-05-1994
        /// Purpose:getting payemnts details based on PolicyId
        /// </summary>
        /// <param name="PolicyId"></param>
        /// <returns></returns>
        public static List<LinkPayment> GetLinkPaymentReciptRecordsByPolicyId(Guid PolicyId)
        {
            List<LinkPayment> _PolicyPaymentEntriesPost = new List<LinkPayment>();
            using (SqlConnection con = new SqlConnection(DBConnection.GetConnectionString()))
            {
                using (System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("usp_GetPaymentsforLinking", con))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandTimeout = 0;
                    cmd.Parameters.AddWithValue("@PolicyID", PolicyId);
                    con.Open();
                    System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        LinkPayment obj = new LinkPayment();
                        obj.PaymentEntryID = (Guid)reader["PaymentEntryID"];
                        obj.PolicyId = (Guid)reader["PolicyID"];
                        obj.PolicyNumber = Convert.ToString(reader["PolicyNumber"]);
                        obj.InvoiceDate = reader.IsDBNull("InvoiceDate") ? null : (DateTime?)reader["InvoiceDate"];

                        string strPayment = Convert.ToString(reader["PaymentRecived"]);
                        decimal payment = 0;
                        decimal.TryParse(strPayment, out payment);
                        obj.PaymentRecived = payment.ToString("C");

                        string strComm = Convert.ToString(reader["CommissionPercentage"]);
                        decimal comm = 0;
                        decimal.TryParse(strComm, out comm);
                        obj.CommissionPercentage = comm.ToString() + "%";

                        obj.NumberOfUnits = Convert.ToString(reader["NumberOfUnits"]);
                        obj.DollerPerUnit = Convert.ToString(reader["DollerPerUnit"]);

                        string strFee = Convert.ToString(reader["Fee"]);
                        decimal fee = 0;
                        decimal.TryParse(strFee, out fee);
                        obj.Fee = fee;

                        string strSplitPer = Convert.ToString(reader["SplitPercentage"]);
                        decimal splitPer = 0;
                        decimal.TryParse(strSplitPer, out splitPer);
                        obj.SplitPer = splitPer.ToString() + "%";

                        string strTotal = Convert.ToString(reader["TotalPayment"]);
                        decimal total = 0;
                        decimal.TryParse(strTotal, out total);
                        obj.TotalPayment = total.ToString("C");

                        _PolicyPaymentEntriesPost.Add(obj);
                    }
                }
            }

                        //List<PolicyPaymentEntriesPost> _PolicyPaymentEntriesPost = PolicyPaymentEntriesPost.GetPolicyPaymentEntryPolicyIDWise(PolicyId);
                        //using (DLinq.CommissionDepartmentEntities DataModel = Entity.DataModel)
                        //{
                        //    var Result = DataModel.GetPolicyPaymentEntryPolicyIDWise(PolicyId);
                        //    List<LinkPayment> _LinkPaymentReciptRecords = (from u in Result
                        //                                                   where (u.PolicyID == PolicyId)
                        //                                                   select new LinkPayment

                        //                                                   {
                        //                                                       PaymentEntryID = u.PaymentEntryID,
                        //                                                       PolicyId = u.PolicyID.Value,
                        //                                                       PolicyNumber = PostUtill.GetPolicy(PolicyId).PolicyNumber,
                        //                                                       InvoiceDate = u.InvoiceDate,
                        //                                                       PaymentRecived = u.PaymentRecived.Value.ToString("C"),
                        //                                                       CommissionPercentage = u.CommissionPercentage.ToString() + "%",
                        //                                                       NumberOfUnits = u.NumberOfUnits.Value.ToString(),
                        //                                                       DollerPerUnit = u.DollerPerUnit.Value.ToString("C"),
                        //                                                       Fee = u.Fee.Value,
                        //                                                       SplitPer = u.SplitPercentage.Value.ToString() + "%",
                        //                                                       TotalPayment = u.TotalPayment.Value.ToString("C"),
                        //                                                   }
                        //                        ).ToList();

                        //}


                        //Policy _Policy= PostUtill.GetPolicy(PolicyId);

                        //_LinkPaymentReciptRecords.ForEach(p => p.PayorId = _Policy.PayorId);
                        //_LinkPaymentReciptRecords.ForEach(p => p.LicenseId = _Policy.PolicyLicenseeId);            

                        return _PolicyPaymentEntriesPost;
        }
        public void AddUpdateLinkPaymentReciptRecords(LinkPaymentReciptRecords _LinkPaymentReciptRecords)
        {

        }

    }
}
