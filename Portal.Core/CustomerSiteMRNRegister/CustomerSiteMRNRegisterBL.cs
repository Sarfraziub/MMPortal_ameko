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
    public class CustomerSiteMRNRegisterBL
    {
        DBInterface dbInterface;
        public CustomerSiteMRNRegisterBL()
        {
            dbInterface = new DBInterface();
        }
         
     
        public List<CustomerSiteMRNViewModel> GetCustomerSiteMRNRegisterList(int vendorId, int shippingstateId, string fromDate, string toDate, int companyId, int createdBy, string sortBy, string sortOrder)
        {
            List<CustomerSiteMRNViewModel> customerSiteMrns = new List<CustomerSiteMRNViewModel>();
            SQLDbInterface sqlDbInterface = new SQLDbInterface();
            try
            {
                DataTable dtCustomerSiteMRNs = sqlDbInterface.GetCustomerSiteMRNRegisterList(vendorId, shippingstateId, Convert.ToDateTime(fromDate), Convert.ToDateTime(toDate), companyId, createdBy, sortBy, sortOrder);
                if (dtCustomerSiteMRNs != null && dtCustomerSiteMRNs.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtCustomerSiteMRNs.Rows)
                    {

                        customerSiteMrns.Add(new CustomerSiteMRNViewModel
                        {
                            CustomerSiteMRNId = Convert.ToInt32(dr["CustomerSiteMRNId"]),
                            CustomerSiteMRNNo = Convert.ToString(dr["CustomerSiteMRNNo"]),
                            CustomerSiteMRNDate = Convert.ToString(dr["CustomerSiteMRNDate"]),
                            InvoiceNo = Convert.ToString(dr["InvoiceNo"]), 
                            InvoiceDate = Convert.ToString(dr["InvoiceDate"]),
                            VendorId = Convert.ToInt32(dr["VendorId"]),
                            VendorCode = Convert.ToString(dr["VendorCode"]),
                            VendorName = Convert.ToString(dr["VendorName"]), 
                            CompanyBranchName = Convert.ToString(dr["CompanyBranchName"]),
                            CompanyBranchAddress = Convert.ToString(dr["CompanyBranchAddress"]),
                            CompanyBranchCity = Convert.ToString(dr["CompanyBranchCity"]),
                            CompanyBranchStateName = Convert.ToString(dr["CompanyBranchStateName"]),
                            CompanyBranchPinCode = Convert.ToString(dr["CompanyBranchPinCode"]),
                            CompanyBranchTINNo = Convert.ToString(dr["CompanyBranchTINNo"]),
                            CompanyBranchCSTNo = Convert.ToString(dr["CompanyBranchCSTNo"]), 
                            Remarks1 = Convert.ToString(dr["Remarks1"]),
                            Remarks2 = Convert.ToString(dr["Remarks2"]),
                            DispatchRefNo = Convert.ToString(dr["DispatchRefNo"]),
                            DispatchRefDate = Convert.ToString(dr["DispatchRefDate"]),
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
            return customerSiteMrns;
        }
         




    }
}
