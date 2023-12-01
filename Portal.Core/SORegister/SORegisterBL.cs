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
    public class SORegisterBL
    {
        DBInterface dbInterface;
        public SORegisterBL()
        {
            dbInterface = new DBInterface();
        } 
  
        public List<SOViewModel> GetSORegisterList(int customerId, int stateId, int shippingstateId, string fromDate, string toDate, int createdBy, int companyId, string sortBy, string sortOrder)
        {
            List<SOViewModel> sos = new List<SOViewModel>();
            SQLDbInterface sqlDbInterface = new SQLDbInterface();
            try
            {
                DataTable dtSOs = sqlDbInterface.GetSORegisterList(customerId, stateId, shippingstateId, Convert.ToDateTime(fromDate), Convert.ToDateTime(toDate), createdBy, companyId, sortBy, sortOrder);
                if (dtSOs != null && dtSOs.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtSOs.Rows)
                    {
                        sos.Add(new SOViewModel
                        {
                            SOId = Convert.ToInt32(dr["SOId"]),
                            SONo = Convert.ToString(dr["SONo"]),
                            SODate = Convert.ToString(dr["SODate"]),
                            CustomerId = Convert.ToInt32(dr["CustomerId"]),
                            CustomerCode = Convert.ToString(dr["CustomerCode"]),
                            ContactPerson = Convert.ToString(dr["ContactPerson"]), 
                            BillingAddress = Convert.ToString(dr["BillingAddress"]),
                            ShippingBillingAddress = Convert.ToString(dr["ShippingBillingAddress"]),
                            ShippingCity = Convert.ToString(dr["ShippingCity"]),
                            ShippingStateName = Convert.ToString(dr["ShippingStateName"]),
                            ShippingPinCode = Convert.ToString(dr["ShippingPinCode"]),
                            ShippingTINNo = Convert.ToString(dr["ShippingTINNo"]), 
                            PinCode = Convert.ToString(dr["PinCode"]),
                            CSTNo = Convert.ToString(dr["CSTNo"]),
                            TINNo = Convert.ToString(dr["TINNo"]),
                            CustomerName = Convert.ToString(dr["CustomerName"]),
                            City = Convert.ToString(dr["City"]),
                            StateName = Convert.ToString(dr["StateName"]),
                            PayToBookBranch = Convert.ToString(dr["PayToBookBranch"]),
                            PayToBookName = Convert.ToString(dr["PayToBookName"]),
                            RefNo = Convert.ToString(dr["RefNo"]),
                            RefDate = Convert.ToString(dr["RefDate"]),
                            BasicValue = Convert.ToDecimal(dr["BasicValue"]),
                            FreightValue = Convert.ToDecimal(dr["FreightValue"]),
                            LoadingValue = Convert.ToDecimal(dr["LoadingValue"]),
                            TotalValue = Convert.ToDecimal(dr["TotalValue"]), 
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
            return sos;
        }

        public DataTable GenerateSORegisterReports(int customerId, int stateId, int shippingstateId, string fromDate, string toDate,int createdBy, int companyId, string sortBy, string sortOrder)
        {
            SQLDbInterface sqlDbInterface = new SQLDbInterface();
            DataTable dtVoucher = new DataTable();
            try
            {
                dtVoucher = sqlDbInterface.GenerateSORegisterReports(customerId, stateId, shippingstateId, Convert.ToDateTime(fromDate), Convert.ToDateTime(toDate), createdBy, companyId, sortBy, sortOrder);
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dtVoucher;
        }
    }
}
