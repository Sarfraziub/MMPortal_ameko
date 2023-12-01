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
 public class CustomerSiteMRNBL
    {
        DBInterface dbInterface;
        public CustomerSiteMRNBL()
        {
            dbInterface = new DBInterface();
        }
        public List<CustomerSiteMRNProductDetailViewModel> GetCustomerSiteMRNProductList(long customerSiteMrnId)
        {
            List<CustomerSiteMRNProductDetailViewModel> customerSiteMrnProducts = new List<CustomerSiteMRNProductDetailViewModel>();
            SQLDbInterface sqlDbInterface = new SQLDbInterface();
            try
            {
                DataTable dtProducts = sqlDbInterface.GetCustomerSiteMRNProductList(customerSiteMrnId);
                if (dtProducts != null && dtProducts.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtProducts.Rows)
                    {

                        customerSiteMrnProducts.Add(new CustomerSiteMRNProductDetailViewModel
                        {
                            SequenceNo = Convert.ToInt32(dr["SNo"]),
                            CustomerSiteMRNProductDetailId = Convert.ToInt32(dr["CustomerSiteMRNProductDetailId"]),
                            ProductId = Convert.ToInt32(dr["ProductId"]),
                            ProductName = Convert.ToString(dr["ProductName"]),
                            ProductCode = Convert.ToString(dr["ProductCode"]),
                            ProductShortDesc = Convert.ToString(dr["ProductShortDesc"]),
                            Price = Convert.ToDecimal(dr["Price"]),
                            Quantity = Convert.ToDecimal(dr["Quantity"]),
                            ReceivedQuantity = Convert.ToDecimal(dr["ReceivedQuantity"]),
                            AcceptQuantity = Convert.ToDecimal(dr["AcceptQuantity"]),
                            RejectQuantity = Convert.ToDecimal(dr["RejectQuantity"]),
                            UOMName = Convert.ToString(dr["UOMName"]),
                            TotalReceivedQuantity = Convert.ToDecimal(dr["ReceivedQtyAtSite"])
,                        });
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return customerSiteMrnProducts;
        }

        public ResponseOut AddEditCustomerSiteMRN(CustomerSiteMRNViewModel customerSiteMrnViewModel, List<CustomerSiteMRNProductDetailViewModel> customerSiteMrnProducts, List<CustomerSiteMRNSupportingDocumentViewModel> customerSiteMrnDocuments)
        {
            ResponseOut responseOut = new ResponseOut();
            SQLDbInterface sqlDbInterface = new SQLDbInterface();
            try
            {
                CustomerSiteMRN customerSiteMrn = new CustomerSiteMRN
                {
                    CustomerSiteMRNId = customerSiteMrnViewModel.CustomerSiteMRNId,
                    CustomerSiteMRNDate = Convert.ToDateTime(customerSiteMrnViewModel.CustomerSiteMRNDate),
                    GRNo = customerSiteMrnViewModel.GRNo,
                    GRDate = Convert.ToDateTime(customerSiteMrnViewModel.GRDate),
                    DeliveryChallanNo = customerSiteMrnViewModel.DeliveryChallanNo,
                    PONo = customerSiteMrnViewModel.PONo,
                    POId = customerSiteMrnViewModel.POId,
                    RequisitionId = customerSiteMrnViewModel.RequisitionId,
                    VendorId = customerSiteMrnViewModel.VendorId,
                    VendorName = customerSiteMrnViewModel.VendorName,
                    ContactPerson = customerSiteMrnViewModel.ContactPerson,
                    ShippingContactPerson = customerSiteMrnViewModel.ShippingContactPerson,
                    ShippingBillingAddress = customerSiteMrnViewModel.ShippingBillingAddress,
                    ShippingCity = customerSiteMrnViewModel.ShippingCity,
                    ShippingStateId = customerSiteMrnViewModel.ShippingStateId,
                    ShippingCountryId = customerSiteMrnViewModel.ShippingCountryId,
                    ShippingPinCode = customerSiteMrnViewModel.ShippingPinCode,
                    ShippingTINNo = customerSiteMrnViewModel.ShippingTINNo,
                    ShippingEmail = customerSiteMrnViewModel.ShippingEmail,
                    ShippingMobileNo = customerSiteMrnViewModel.ShippingMobileNo,
                    ShippingContactNo = customerSiteMrnViewModel.ShippingContactNo,
                    ShippingFax = customerSiteMrnViewModel.ShippingFax,
                    CompanyBranchId = customerSiteMrnViewModel.CompanyBranchId,
                    DispatchRefNo = customerSiteMrnViewModel.DispatchRefNo,
                    DispatchRefDate = string.IsNullOrEmpty(customerSiteMrnViewModel.DispatchRefDate) ? Convert.ToDateTime("01-01-1900") : Convert.ToDateTime(customerSiteMrnViewModel.DispatchRefDate),
                    LRNo = customerSiteMrnViewModel.LRNo,
                    LRDate = string.IsNullOrEmpty(customerSiteMrnViewModel.LRDate) ? Convert.ToDateTime("01-01-1900") : Convert.ToDateTime(customerSiteMrnViewModel.LRDate),
                    TransportVia = customerSiteMrnViewModel.TransportVia,
                    NoOfPackets = customerSiteMrnViewModel.NoOfPackets,
                    Remarks1 = customerSiteMrnViewModel.Remarks1,
                    Remarks2 = customerSiteMrnViewModel.Remarks2,
                    FinYearId = customerSiteMrnViewModel.FinYearId,
                    CompanyId = customerSiteMrnViewModel.CompanyId,
                    CreatedBy = customerSiteMrnViewModel.CreatedBy,
                    ApprovalStatus = customerSiteMrnViewModel.ApprovalStatus,
                    CustomerId = customerSiteMrnViewModel.CustomerId,
                    CustomerBranchId = customerSiteMrnViewModel.CustomerBranchId,
                    IsLocal = customerSiteMrnViewModel.IsLocal
                };
                List<CustomerSiteMRNProductDetail> customerSiteMrnProductList = new List<CustomerSiteMRNProductDetail>();
                if (customerSiteMrnProducts != null && customerSiteMrnProducts.Count > 0)
                {
                    foreach (CustomerSiteMRNProductDetailViewModel item in customerSiteMrnProducts)
                    {
                        customerSiteMrnProductList.Add(new CustomerSiteMRNProductDetail
                        {
                            ProductId = item.ProductId,
                            ProductShortDesc = item.ProductShortDesc,
                            Price = item.Price,
                            Quantity = item.Quantity,
                            ReceivedQuantity=item.ReceivedQuantity,
                            AcceptQuantity = item.AcceptQuantity,
                            RejectQuantity = item.RejectQuantity,
                            RequisitionId=item.RequisitionId                            
                        });
                    }
                }

                List<CustomerSiteMRNSupportingDocument> customerSiteMrnDocumentList = new List<CustomerSiteMRNSupportingDocument>();
                if (customerSiteMrnDocuments != null && customerSiteMrnDocuments.Count > 0)
                {
                    foreach (CustomerSiteMRNSupportingDocumentViewModel item in customerSiteMrnDocuments)
                    {
                        customerSiteMrnDocumentList.Add(new CustomerSiteMRNSupportingDocument
                        {
                            DocumentTypeId = item.DocumentTypeId,
                            DocumentName = item.DocumentName,
                            DocumentPath = item.DocumentPath
                        });
                    }
                }

                responseOut = sqlDbInterface.AddEditCustomerSiteMRN(customerSiteMrn, customerSiteMrnProductList, customerSiteMrnDocumentList);

            }

            catch (Exception ex)
            {
                responseOut.status = ActionStatus.Fail;
                responseOut.message = ActionMessage.ApplicationException;
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }

            return responseOut;
        }
        public List<CustomerSiteMRNViewModel> GetCustomerSiteMRNList(string customerSiteMrnNo, string vendorName, string dispatchrefNo, string fromDate, string toDate, int companyId,string approvalStatus="",string isLocal="")
        {
            List<CustomerSiteMRNViewModel> customerSiteMrns = new List<CustomerSiteMRNViewModel>();
            SQLDbInterface sqlDbInterface = new SQLDbInterface();
            try
            {
                DataTable dtMRNs = sqlDbInterface.GetCustomerSiteMRNList(customerSiteMrnNo, vendorName, dispatchrefNo, Convert.ToDateTime(fromDate), Convert.ToDateTime(toDate), companyId, approvalStatus,isLocal);
                if (dtMRNs != null && dtMRNs.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtMRNs.Rows)
                    {

                        customerSiteMrns.Add(new CustomerSiteMRNViewModel
                        {
                            CustomerSiteMRNId = Convert.ToInt32(dr["CustomerSiteMRNId"]),
                            CustomerSiteMRNNo = Convert.ToString(dr["CustomerSiteMRNNo"]),
                            CustomerSiteMRNDate = Convert.ToString(dr["CustomerSiteMRNDate"]),
                            VendorId = Convert.ToInt32(dr["VendorId"]),
                            VendorCode = Convert.ToString(dr["VendorCode"]),
                            VendorName = Convert.ToString(dr["VendorName"]),
                            ShippingCity = Convert.ToString(dr["ShippingCity"]),
                            ShippingStateName = Convert.ToString(dr["StateName"]),
                            DispatchRefNo = Convert.ToString(dr["DispatchRefNo"]),
                            DispatchRefDate = Convert.ToString(dr["DispatchRefDate"]),
                            ApprovalStatus = Convert.ToString(dr["CustomerSiteMRNStatus"]),
                            CreatedByUserName = Convert.ToString(dr["CreatedByName"]),
                            CreatedDate = Convert.ToString(dr["CreatedDate"]),
                            ModifiedByUserName = Convert.ToString(dr["ModifiedByName"]),
                            ModifiedDate = Convert.ToString(dr["ModifiedDate"]),
                            IsLocal=Convert.ToBoolean(dr["IsLocal"])
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

        public CustomerSiteMRNViewModel GetCustomerSiteMRNDetail(long customerSiteMrnId = 0)
        {
            CustomerSiteMRNViewModel customerSiteMrn = new CustomerSiteMRNViewModel();
            SQLDbInterface sqlDbInterface = new SQLDbInterface();
            try
            {
                DataTable dtcustomerSiteMrns = sqlDbInterface.GetCustomerSiteMRNDetail(customerSiteMrnId);
                if (dtcustomerSiteMrns != null && dtcustomerSiteMrns.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtcustomerSiteMrns.Rows)
                    {
                        customerSiteMrn = new CustomerSiteMRNViewModel
                        {
                            CustomerSiteMRNId = Convert.ToInt32(dr["CustomerSiteMRNId"]),
                            CustomerSiteMRNNo = Convert.ToString(dr["CustomerSiteMRNNo"]),
                            CustomerSiteMRNDate = Convert.ToString(dr["MRNDate"]),
                            GRNo = Convert.ToString(dr["GRNo"]),
                            GRDate=Convert.ToString(dr["GRDate"]),
                            RequisitionId = Convert.ToInt32(dr["RequisitionId"]),
                            RequisitionNo = Convert.ToString(dr["RequisitionNo"]),
                            DeliveryChallanNo= Convert.ToString(dr["DeliveryChallanNo"]),
                            PONo = Convert.ToString(dr["PONo"]),
                            RequisitionType = Convert.ToString(dr["RequisitionType"]),
                            //InvoiceId = Convert.ToInt32(dr["InvoiceId"]),
                            //InvoiceNo = Convert.ToString(dr["InvoiceNo"]),
                            //InvoiceDate = Convert.ToString(dr["InvoiceDate"]),

                            VendorId = Convert.ToInt32(dr["VendorId"]),
                            VendorCode = Convert.ToString(dr["VendorCode"]),
                            VendorName = Convert.ToString(dr["VendorName"]),

                            ShippingContactPerson = Convert.ToString(dr["ShippingContactPerson"]),
                            ShippingBillingAddress = Convert.ToString(dr["ShippingBillingAddress"]),
                            ShippingCity = Convert.ToString(dr["ShippingCity"]),
                            ShippingStateId = Convert.ToInt32(dr["ShippingStateId"]),
                            ShippingCountryId = Convert.ToInt32(dr["ShippingCountryId"]),
                            ShippingPinCode = Convert.ToString(dr["ShippingPinCode"]),
                            ShippingTINNo = Convert.ToString(dr["ShippingTINNo"]),

                            ShippingEmail = Convert.ToString(dr["ShippingEmail"]),
                            ShippingMobileNo = Convert.ToString(dr["ShippingMobileNo"]),
                            ShippingContactNo = Convert.ToString(dr["ShippingContactNo"]),
                            ShippingFax = Convert.ToString(dr["ShippingFax"]),

                            CompanyBranchId = Convert.ToInt32(string.IsNullOrEmpty(dr["CompanyBranchId"].ToString()) ? "0" : dr["CompanyBranchId"]),
                            DispatchRefNo = Convert.ToString(dr["DispatchRefNo"]),
                            DispatchRefDate = Convert.ToString(dr["DispatchRefDate"]),

                            LRNo = Convert.ToString(dr["LRNo"]),
                            LRDate = Convert.ToString(dr["LRDate"]),

                            TransportVia = Convert.ToString(dr["TransportVia"]),
                            NoOfPackets = Convert.ToInt32(dr["NoOfPackets"]),
                            
                            Remarks1 = Convert.ToString(dr["Remarks1"]),
                            Remarks2 = Convert.ToString(dr["Remarks2"]),


                            ApprovalStatus = Convert.ToString(dr["CustomerSiteMRNStatus"]),

                            CreatedByUserName = Convert.ToString(dr["CreatedByName"]),
                            CreatedDate = Convert.ToString(dr["CreatedDate"]),
                            ModifiedByUserName = Convert.ToString(dr["ModifiedByName"]),
                            ModifiedDate = Convert.ToString(dr["ModifiedDate"]),
                            CustomerId = Convert.ToInt32(dr["CustomerId"]),
                             CustomerBranchId = Convert.ToInt32(dr["CustomerBranchId"]),
                             CustomerName= Convert.ToString(dr["CustomerName"]),
                           CustomerBranchName= Convert.ToString(dr["CustomerBranchName"]),
                           IsLocal=Convert.ToBoolean(dr["IsLocal"])
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return customerSiteMrn;
        }


        public DataTable GetCustomerSiteMRNDetailDataTable(long customerSiteMrnId = 0)
        {
            SQLDbInterface sqlDbInterface = new SQLDbInterface();
            DataTable dtCustomerSiteMRN = new DataTable();
            try
            {
                dtCustomerSiteMRN = sqlDbInterface.GetCustomerSiteMRNDetail(customerSiteMrnId);
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dtCustomerSiteMRN;
        }

        public DataTable GetCustomerSiteMRNProductListDataTable(long customerSiteMrnId)
        {
            SQLDbInterface sqlDbInterface = new SQLDbInterface();
            DataTable dtProducts = new DataTable();
            try
            {
                dtProducts = sqlDbInterface.GetCustomerSiteMRNProductList(customerSiteMrnId);
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dtProducts;
        }


        public List<CustomerSiteMRNSupportingDocumentViewModel> GetCustomerSiteMRNSupportingDocumentList(long customerSiteMrnId)
        {
            List<CustomerSiteMRNSupportingDocumentViewModel> customerSiteMrnDocuments = new List<CustomerSiteMRNSupportingDocumentViewModel>();
            SQLDbInterface sqlDbInterface = new SQLDbInterface();
            try
            {
                DataTable dtDocument = sqlDbInterface.GetCustomerSiteMRNSupportingDocumentList(customerSiteMrnId);
                if (dtDocument != null && dtDocument.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtDocument.Rows)
                    {
                        customerSiteMrnDocuments.Add(new CustomerSiteMRNSupportingDocumentViewModel
                        {
                            CustomerSiteMRNDocId = Convert.ToInt32(dr["CustomerSiteMRNDocId"]),
                            DocumentSequenceNo = Convert.ToInt32(dr["SNo"]),
                            DocumentTypeId = Convert.ToInt32(dr["DocumentTypeId"]),
                            DocumentTypeDesc = Convert.ToString(dr["DocumentTypeDesc"]),
                            DocumentName = Convert.ToString(dr["DocumentName"]),
                            DocumentPath = Convert.ToString(dr["DocumentPath"])
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return customerSiteMrnDocuments;
        }

        public List<StoreRequisitionViewModel> GetCustomerSiteMRNRequisitionList(string requisitionNo, string workOrderNo, string requisitionType, int companyBranchId, DateTime fromDate, DateTime toDate, int companyId, string displayType = "", string approvalStatus = "")
        {
            List<StoreRequisitionViewModel> requisitions = new List<StoreRequisitionViewModel>();
            SQLDbInterface sqlDbInterface = new SQLDbInterface();
            try
            {
                DataTable dtRequisitions = sqlDbInterface.GetCustomerSiteMRNStoreRequisitionList(requisitionNo, workOrderNo, requisitionType, companyBranchId, Convert.ToDateTime(fromDate), Convert.ToDateTime(toDate), companyId, displayType, approvalStatus);
                if (dtRequisitions != null && dtRequisitions.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtRequisitions.Rows)
                    {
                        requisitions.Add(new StoreRequisitionViewModel
                        {
                            RequisitionId = Convert.ToInt32(dr["RequisitionId"]),
                            RequisitionNo = Convert.ToString(dr["RequisitionNo"]),
                            RequisitionDate = Convert.ToString(dr["RequisitionDate"]),
                            RequisitionType = Convert.ToString(dr["RequisitionType"]),
                            CompanyBranchId = Convert.ToInt32(dr["CompanyBranchId"]),
                            BranchName = Convert.ToString(dr["BranchName"]),
                            LocationId = Convert.ToInt32(dr["LocationId"]),
                            LocationName = Convert.ToString(dr["LocationName"]),
                            WorkOrderId = Convert.ToInt32(dr["WorkOrderId"]),
                            WorkOrderNo = Convert.ToString(dr["WorkOrderNo"]),
                            WorkOrderDate = Convert.ToString(dr["WorkOrderDate"]),
                            CustomerName = Convert.ToString(dr["CustomerName"]),
                            CustomerId = Convert.ToInt32(dr["CustomerId"]),
                            CustomerBranchName = Convert.ToString(dr["CustomerBranchName"]),
                            CustomerBranchId = Convert.ToInt32(dr["CustomerBranchId"]),
                            VendorId = Convert.ToInt32(dr["VendorId"]),
                            VendorName = Convert.ToString(dr["VendorName"]),
                            VendorCode = Convert.ToString(dr["VendorCode"])
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return requisitions;
        }

        public decimal GetProductPurchasePrice(int productId)
        {
            decimal PurchasePrice = 0;
            try
            {
                PurchasePrice = dbInterface.GetProductPurchasePrice(productId);
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return PurchasePrice;
        }
    }
}
