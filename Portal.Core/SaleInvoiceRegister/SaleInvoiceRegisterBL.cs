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
    public class SaleInvoiceRegisterBL
    {
        DBInterface dbInterface;
        public SaleInvoiceRegisterBL()
        {
            dbInterface = new DBInterface();
        } 
  
        public List<SaleInvoiceViewModel> GetSaleInvoiceRegisterList(int customerId, int stateId, int shippingstateId, string fromDate, string toDate, int companyId, int createdBy, string sortBy, string sortOrder)
        {
            List<SaleInvoiceViewModel> saleinvoices = new List<SaleInvoiceViewModel>();
            SQLDbInterface sqlDbInterface = new SQLDbInterface();
            try
            {
                DataTable dtSaleInvoices = sqlDbInterface.GetSaleInvoiceRegisterList(customerId, stateId, shippingstateId, Convert.ToDateTime(fromDate), Convert.ToDateTime(toDate), companyId, createdBy, sortBy, sortOrder);
                if (dtSaleInvoices != null && dtSaleInvoices.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtSaleInvoices.Rows)
                    {
                        saleinvoices.Add(new SaleInvoiceViewModel
                        {
                            InvoiceId = Convert.ToInt32(dr["InvoiceId"]),
                            InvoiceNo = Convert.ToString(dr["InvoiceNo"]),
                            InvoiceDate = Convert.ToString(dr["InvoiceDate"]),
                            SONo = Convert.ToString(dr["SONo"]),
                            SODate = Convert.ToString(dr["SODate"]),
                            CustomerId = Convert.ToInt32(dr["CustomerId"]),
                            CustomerCode = Convert.ToString(dr["CustomerCode"]),
                            CustomerName = Convert.ToString(dr["CustomerName"]), 
                            ContactPerson  = Convert.ToString(dr["ContactPerson"]),
                            BillingAddress = Convert.ToString(dr["BillingAddress"]),
                            ShippingBillingAddress = Convert.ToString(dr["ShippingBillingAddress"]),
                            ShippingCity = Convert.ToString(dr["ShippingCity"]),
                            ShippingStateName = Convert.ToString(dr["ShippingStateName"]),
                            ShippingPinCode = Convert.ToString(dr["ShippingPinCode"]),
                            ShippingTINNo = Convert.ToString(dr["ShippingTINNo"]),
                            PinCode = Convert.ToString(dr["PinCode"]),
                            CSTNo = Convert.ToString(dr["CSTNo"]),
                            TINNo = Convert.ToString(dr["TINNo"]),  
                            PayToBookBranch = Convert.ToString(dr["PayToBookBranch"]),
                            PayToBookName = Convert.ToString(dr["PayToBookName"]), 
                            FreightValue = Convert.ToDecimal(dr["FreightValue"].ToString()==""?"0" : dr["FreightValue"].ToString()),
                            LoadingValue = Convert.ToDecimal(dr["LoadingValue"].ToString() == "" ? "0" : dr["LoadingValue"].ToString()), 
                            City = Convert.ToString(dr["City"]),
                            StateName = Convert.ToString(dr["StateName"]),
                            RefNo = Convert.ToString(dr["RefNo"]),
                            RefDate = Convert.ToString(dr["RefDate"]),
                            BasicValue = Convert.ToDecimal(dr["BasicValue"]),
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
            return saleinvoices;
        }
        public List<SaleSummaryRegisterViewModel> GetSaleSummaryRegister(int customerId, int userId, int stateId, int companyId, DateTime fromDate, DateTime toDate)
        {
            List<SaleSummaryRegisterViewModel> saleInvoices = new List<SaleSummaryRegisterViewModel>();
            SQLDbInterface sqlDbInterface = new SQLDbInterface();
            try
            {
                DataTable dtSaleInvoices = sqlDbInterface.GetSaleSummaryRegister(customerId,userId, stateId,companyId,fromDate,toDate);
                if (dtSaleInvoices != null && dtSaleInvoices.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtSaleInvoices.Rows)
                    {
                        saleInvoices.Add(new SaleSummaryRegisterViewModel
                        {
                            InvoiceId = Convert.ToInt32(dr["InvoiceId"]),
                            InvoiceNo = Convert.ToString(dr["InvoiceNo"]),
                            InvoiceDate = Convert.ToString(dr["InvoiceDate"]),
                            InvoiceType = Convert.ToString(dr["InvoiceType"]),
                            CustomerId = Convert.ToInt32(dr["CustomerId"]),
                            CustomerName = Convert.ToString(dr["CustomerName"]),
                            StateId = Convert.ToInt32(dr["StateId"]),
                            StateName = Convert.ToString(dr["StateName"]),
                            BasicValue = Convert.ToDecimal(dr["BasicValue"]),
                            FreightValue = Convert.ToDecimal(dr["FreightValue"]),
                            LoadingValue = Convert.ToDecimal(dr["LoadingValue"]),
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
            return saleInvoices;
        }

        public DataTable GenerateSaleInvoiceRegisterReports(int customerId, int stateId, int shippingstateId, string fromDate, string toDate,  int companyId, int createdBy, string sortBy, string sortOrder)
        {
            SQLDbInterface sqlDbInterface = new SQLDbInterface();
            DataTable dtVoucher = new DataTable();
            try
            {
                dtVoucher = sqlDbInterface.GenerateSaleInvoiceRegisterReports(customerId, stateId, shippingstateId, Convert.ToDateTime(fromDate), Convert.ToDateTime(toDate), companyId, createdBy, sortBy, sortOrder);
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dtVoucher;
        }

        public DataTable GenerateSaleInvoiceSummaryReports(int customerId, int userId, int stateId, int companyId, DateTime fromDate, DateTime toDate)
        {
            SQLDbInterface sqlDbInterface = new SQLDbInterface();
            DataTable dtVoucher = new DataTable();
            try
            {
                dtVoucher = sqlDbInterface.GenerateSaleInvoiceSummaryReports(customerId, userId, stateId, companyId, fromDate, toDate);
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
