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
    public class PurchaseInvoiceRegisterBL
    {
        DBInterface dbInterface;
        public PurchaseInvoiceRegisterBL()
        {
            dbInterface = new DBInterface();
        }
        public List<PurchaseInvoiceViewModel> GetPurchaseInvoiceRegisterList(string vendorId, int stateId, string fromDate, string toDate, int companyId, int createdBy, string sortBy, string sortOrder)
        {
           
            List<PurchaseInvoiceViewModel> purchaseInvoiceViewModel = new List<PurchaseInvoiceViewModel>();
            SQLDbInterface sqlDbInterface = new SQLDbInterface();
            try
            {
                DataTable dtPOs = sqlDbInterface.GetPurchaseInvoiceRegisterList(vendorId, stateId, Convert.ToDateTime(fromDate), Convert.ToDateTime(toDate), companyId, createdBy, sortBy, sortOrder);
                if (dtPOs != null && dtPOs.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtPOs.Rows)
                    {
                        purchaseInvoiceViewModel.Add(new PurchaseInvoiceViewModel
                        {
                            InvoiceId = Convert.ToInt32(dr["InvoiceId"]),
                            InvoiceNo=Convert.ToString(dr["InvoiceNo"]),
                            InvoiceDate = Convert.ToString(dr["InvoiceDate"]),
                            POId = Convert.ToInt32(dr["POId"]),
                            PONo = Convert.ToString(dr["PONo"]),
                            PODate = Convert.ToString(dr["PODate"]),
                            VendorName = Convert.ToString(dr["VendorName"]),
                            VendorId = Convert.ToInt32(dr["VendorId"]),
                            VendorCode = Convert.ToString(dr["VendorCode"]),
                            City = Convert.ToString(dr["City"]),
                            BillingAddress = Convert.ToString(dr["BillingAddress"]),                            
                            StateName = Convert.ToString(dr["StateName"]),
                            PinCode = Convert.ToString(dr["PinCode"]),
                            CSTNo = Convert.ToString(dr["CSTNo"]),
                            TINNo = Convert.ToString(dr["TINNo"]),
                            RefNo = Convert.ToString(dr["RefNo"]),
                            RefDate = Convert.ToString(dr["RefDate"]),
                            BasicValue = Convert.ToDecimal(dr["BasicValue"]),
                            TotalValue = Convert.ToDecimal(dr["TotalValue"]), 
                            FinYearId=Convert.ToInt32(dr["FinYearId"]),                        
                            Remarks = Convert.ToString(dr["Remarks"]),
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
            return purchaseInvoiceViewModel;
        }


        public List<PurchaseSummaryRegisterViewModel> GetPurchaseSummaryRegister(int vendorId, int userId, int stateId, int companyId, DateTime fromDate, DateTime toDate)
        {
            List<PurchaseSummaryRegisterViewModel> purchaseInvoices = new List<PurchaseSummaryRegisterViewModel>();
            SQLDbInterface sqlDbInterface = new SQLDbInterface();
            try
            {
                DataTable dtPurchaseInvoices = sqlDbInterface.GetPurchaseSummaryRegister(vendorId, userId, stateId, companyId, fromDate, toDate);
                if (dtPurchaseInvoices != null && dtPurchaseInvoices.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtPurchaseInvoices.Rows)
                    {
                        purchaseInvoices.Add(new PurchaseSummaryRegisterViewModel
                        {
                            InvoiceId = Convert.ToInt32(dr["InvoiceId"]),
                            InvoiceNo = Convert.ToString(dr["InvoiceNo"]),
                            InvoiceDate = Convert.ToString(dr["InvoiceDate"]), 
                            VendorId = Convert.ToInt32(dr["VendorId"]),
                            VendorName = Convert.ToString(dr["VendorName"]),
                            StateId = Convert.ToInt32(dr["StateId"]),
                            StateName = Convert.ToString(dr["StateName"]),
                            BasicValue = Convert.ToDecimal(dr["BasicValue"]), 
                            TotalValue = Convert.ToDecimal(dr["TotalValue"]),
                            AmountPaid = Convert.ToDecimal(dr["AmountPaid"]),
                            AmountPending = Convert.ToDecimal(dr["AmountPending"]),
                            CreatedByUserName = Convert.ToString(dr["CreatedByName"])

                        });
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return purchaseInvoices;
        }









    }
}
