using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Portal.Core.ViewModel;
using Portal.DAL;
using Portal.Common;
using System.Reflection;
using System.Data;
using System.Transactions;
namespace Portal.Core
{
    public class PORegisterBL
    {
        DBInterface dbInterface;
        public PORegisterBL()
        {
            dbInterface = new DBInterface();
        }
        public List<POViewModel> GetPORegisterList(string vendorId, int stateId, string fromDate, string toDate, int companyId, int createdBy, string sortBy, string sortOrder)
        {
            List<POViewModel> pos = new List<POViewModel>();
            SQLDbInterface sqlDbInterface = new SQLDbInterface();
            try
            {
                DataTable dtPOs = sqlDbInterface.GetPORegisterList(vendorId, stateId, Convert.ToDateTime(fromDate), Convert.ToDateTime(toDate), companyId, createdBy, sortBy, sortOrder);
                if (dtPOs != null && dtPOs.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtPOs.Rows)
                    {
                        pos.Add(new POViewModel
                        {
                            POId = Convert.ToInt32(dr["POId"]),
                            PONo = Convert.ToString(dr["PONo"]),
                            PODate = Convert.ToString(dr["PODate"]),
                            VendorName = Convert.ToString(dr["VendorName"]),
                            VendorId = Convert.ToInt32(dr["VendorId"]),
                            VendorCode = Convert.ToString(dr["VendorCode"]),
                            City = Convert.ToString(dr["City"]),
                            BillingAddress = Convert.ToString(dr["BillingAddress"]),
                            ShippingAddress = Convert.ToString(dr["ShippingAddress"]),
                            StateName = Convert.ToString(dr["StateName"]),
                            PinCode = Convert.ToString(dr["PinCode"]),
                            CSTNo = Convert.ToString(dr["CSTNo"]),
                            TINNo = Convert.ToString(dr["TINNo"]),
                            RefNo = Convert.ToString(dr["RefNo"]),
                            RefDate = Convert.ToString(dr["RefDate"]),
                            BasicValue = Convert.ToDecimal(dr["BasicValue"]),
                            TotalValue = Convert.ToDecimal(dr["TotalValue"]),
                            FreightValue = Convert.ToDecimal(dr["FreightValue"].ToString() == "" ? "0" : dr["FreightValue"].ToString()),
                            LoadingValue = Convert.ToDecimal(dr["LoadingValue"].ToString() == "" ? "0" : dr["LoadingValue"].ToString()), 
                            Remarks1 = Convert.ToString(dr["Remarks1"]),
                            Remarks2 = Convert.ToString(dr["Remarks2"]),
                            CreatedByUserName = Convert.ToString(dr["CreatedByName"]),
                            CreatedDate = Convert.ToString(dr["CreatedDate"]),
                            ModifiedByUserName = Convert.ToString(dr["ModifiedByName"]),
                            ModifiedDate = Convert.ToString(dr["ModifiedDate"])
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return pos;
        }
    }
}
