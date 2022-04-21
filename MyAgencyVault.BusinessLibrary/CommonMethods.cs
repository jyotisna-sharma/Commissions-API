using MyAgencyVault.BusinessLibrary.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace MyAgencyVault.BusinessLibrary
{
    public class CommonMethods
    {
        //public static List<T> ConvertListToExcelFormat<T>(string[] keysName, List<T> list)
        //{
        //    List<T> dataList = new List<T>();


        //    foreach (var record in list)
        //    {
        //        string data = Newtonsoft.Json.JsonConvert.SerializeObject(record);
        //       var  data1 = Newtonsoft.Json.JsonConvert.DeserializeObject<List<PolicyIncomingPaymentObject>>(data);


        //        foreach (var key in keysName)
        //        {
        //            string abcd = record.GetPropertyValue(key).ToString();
        //            decimal decval;
        //            decimal.TryParse(abcd, NumberStyles.Currency, CultureInfo.CurrentCulture.NumberFormat, out decval);
        //            if (record.GetPropertyValue(key) != null)
        //            {
        //                data.SetPropertyValue(key, decval.ToString());
        //            }
        //        }
        //    }


        //    return dataList;
      //  }
    }
}
