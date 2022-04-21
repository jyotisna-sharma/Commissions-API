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
using System.Threading;
using System.Globalization;

namespace MyAgencyVault.BusinessLibrary
{
    [DataContract]
    public class ExportDate
    {
        //[DataMember]
        //public DateTime? CardPayeeExportDate { get; set; }
        DateTime? _CardPayeeExportDate;
        [DataMember]
        public DateTime? CardPayeeExportDate
        {
            get
            {
                return _CardPayeeExportDate;
            }
            set
            {
                _CardPayeeExportDate = value;
                if (value != null && string.IsNullOrEmpty(CardPayeeExportDatestring))
                {
                    CardPayeeExportDatestring = value.ToString();
                }
            }
        }
        string _CardPayeeExportDateString;
        [DataMember]
        public string CardPayeeExportDatestring
        {
            get
            {
                return _CardPayeeExportDateString;
            }
            set
            {
                _CardPayeeExportDateString = value;
                if (CardPayeeExportDate == null && !string.IsNullOrEmpty(_CardPayeeExportDateString))
                {
                    DateTime dt;
                    DateTime.TryParse(_CardPayeeExportDateString, out dt);
                    CardPayeeExportDate = dt;
                }
            }
        }


        //[DataMember]
        //public DateTime? CheckPayeeExportDate { get; set; }
        DateTime? _CheckPayeeExportDate;
        [DataMember]
        public DateTime? CheckPayeeExportDate
        {
            get
            {
                return _CheckPayeeExportDate;
            }
            set
            {
                _CheckPayeeExportDate = value;
                if (value != null && string.IsNullOrEmpty(CheckPayeeExportDatestring))
                {
                    CheckPayeeExportDatestring = value.ToString();
                }
            }
        }
        string _CheckPayeeExportDateString;
        [DataMember]
        public string CheckPayeeExportDatestring
        {
            get
            {
                return _CheckPayeeExportDateString;
            }
            set
            {
                _CheckPayeeExportDateString = value;
                if (CheckPayeeExportDate == null && !string.IsNullOrEmpty(_CheckPayeeExportDateString))
                {
                    DateTime dt;
                    DateTime.TryParse(_CheckPayeeExportDateString, out dt);
                    CheckPayeeExportDate = dt;
                }
            }
        }

        public static ExportDate getExportDate()
        {
            using (DLinq.CommissionDepartmentEntities DataModel = Entity.DataModel)
            {
                var CardDate = (from se in DataModel.MasterSystemConstants
                                where se.Name == "CardPayeeDate"
                                select se.Value).FirstOrDefault();

                var ChequeDate = (from se in DataModel.MasterSystemConstants
                                  where se.Name == "CheckPayeeDate"
                                  select se.Value).FirstOrDefault();


                ExportDate exportDate = new ExportDate();

                if(!string.IsNullOrEmpty(CardDate))
                    exportDate.CardPayeeExportDate = DateTime.ParseExact(CardDate, "MMM-yyyy", DateTimeFormatInfo.InvariantInfo);

                if (!string.IsNullOrEmpty(ChequeDate))
                    exportDate.CheckPayeeExportDate = DateTime.ParseExact(ChequeDate, "MMM-yyyy", DateTimeFormatInfo.InvariantInfo);

                return exportDate;
            }
        }

        public static void setCardPayeeExportDate(DateTime? exportDate)
        {
            using (DLinq.CommissionDepartmentEntities DataModel = Entity.DataModel)
            {
                var CardDate = (from se in DataModel.MasterSystemConstants
                                where se.Name == "CardPayeeDate"
                                select se).FirstOrDefault();

                if (exportDate != null)
                    CardDate.Value = exportDate.Value.ToString("MMM-yyyy");
                else
                    CardDate.Value = null;

                DataModel.SaveChanges();
            }
        }

        public static void setCheckPayeeExportDate(DateTime? exportDate)
        {
            using (DLinq.CommissionDepartmentEntities DataModel = Entity.DataModel)
            {
                var ChequeDate = (from se in DataModel.MasterSystemConstants
                                  where se.Name == "CheckPayeeDate"
                                  select se).FirstOrDefault();

                DLinq.MasterSystemConstant chceckDateValue = new MasterSystemConstant();

                if (exportDate != null)
                    //ChequeDate.Value = exportDate.Value.ToString("MMM-yyyy");  
                    chceckDateValue.Value = exportDate.Value.ToString("MMM-yyyy");
                else
                    //ChequeDate.Value = null;
                    chceckDateValue.Value = null;


                DataModel.SaveChanges();
            }
        }
    }
}
