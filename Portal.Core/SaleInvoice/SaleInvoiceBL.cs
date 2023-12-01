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
    public class SaleInvoiceBL
    {
        DBInterface dbInterface;
        public SaleInvoiceBL()
        {
            dbInterface = new DBInterface();
        }
        public ResponseOut AddEditSaleInvoice(SaleInvoiceViewModel saleinvoiceViewModel, List<SaleInvoiceProductViewModel> saleinvoiceProducts, List<SaleInvoiceTaxViewModel> saleinvoiceTaxes, List<SaleInvoiceTermViewModel> saleinvoiceTerms, List<SaleInvoiceProductSerialDetailViewModel> saleInvoiceProductSerialDetail, List<SISupportingDocumentViewModel> siDocuments)
        {
            ResponseOut responseOut = new ResponseOut();
            SQLDbInterface sqlDbInterface = new SQLDbInterface();
            try
            {
                SaleInvoice saleinvoice = new SaleInvoice
                {
                    InvoiceId = saleinvoiceViewModel.InvoiceId,
                    InvoiceDate = Convert.ToDateTime(saleinvoiceViewModel.InvoiceDate),
                    InvoiceType = saleinvoiceViewModel.InvoiceType,
                    SONo = saleinvoiceViewModel.SONo,
                    SOId = saleinvoiceViewModel.SOId,
                    CompanyBranchId = saleinvoiceViewModel.CompanyBranchId,
                    CurrencyCode = saleinvoiceViewModel.CurrencyCode,
                    CustomerId = saleinvoiceViewModel.CustomerId,
                    CustomerName = saleinvoiceViewModel.CustomerName,
                    ConsigneeId = saleinvoiceViewModel.ConsigneeId,
                    ConsigneeName = saleinvoiceViewModel.ConsigneeName,
                    ContactPerson = saleinvoiceViewModel.ContactPerson,
                    BillingAddress = saleinvoiceViewModel.BillingAddress,
                    City = saleinvoiceViewModel.City,
                    StateId = saleinvoiceViewModel.StateId,
                    CountryId = saleinvoiceViewModel.CountryId,
                    PinCode = saleinvoiceViewModel.PinCode,
                    CSTNo = saleinvoiceViewModel.CSTNo,
                    TINNo = saleinvoiceViewModel.TINNo,
                    PANNo = saleinvoiceViewModel.PANNo,
                    GSTNo = saleinvoiceViewModel.GSTNo,
                    ExciseNo = saleinvoiceViewModel.ExciseNo,
                    Email = saleinvoiceViewModel.Email,
                    MobileNo = saleinvoiceViewModel.MobileNo,
                    ContactNo = saleinvoiceViewModel.ContactNo,
                    Fax = saleinvoiceViewModel.Fax,
                    ApprovalStatus = saleinvoiceViewModel.ApprovalStatus,

                    ShippingContactPerson = saleinvoiceViewModel.ShippingContactPerson,
                    ShippingBillingAddress = saleinvoiceViewModel.ShippingBillingAddress,
                    ShippingCity = saleinvoiceViewModel.ShippingCity,
                    ShippingStateId = saleinvoiceViewModel.ShippingStateId,
                    ShippingCountryId = saleinvoiceViewModel.ShippingCountryId,
                    ShippingPinCode = saleinvoiceViewModel.ShippingPinCode,
                    ShippingTINNo = saleinvoiceViewModel.ShippingTINNo,
                    ShippingEmail = saleinvoiceViewModel.ShippingEmail,
                    ShippingMobileNo = saleinvoiceViewModel.ShippingMobileNo,
                    ShippingContactNo = saleinvoiceViewModel.ShippingContactNo,
                    ShippingFax = saleinvoiceViewModel.ShippingFax,
                    RefNo = saleinvoiceViewModel.RefNo,
                    RefDate = string.IsNullOrEmpty(saleinvoiceViewModel.RefDate) ? Convert.ToDateTime("01-01-1900") : Convert.ToDateTime(saleinvoiceViewModel.RefDate),
                    BasicValue = saleinvoiceViewModel.BasicValue,
                    TotalValue = saleinvoiceViewModel.TotalValue,
                    FreightValue = saleinvoiceViewModel.FreightValue,
                    FreightCGST_Perc = saleinvoiceViewModel.FreightCGST_Perc,
                    FreightCGST_Amt = saleinvoiceViewModel.FreightCGST_Amt,
                    FreightSGST_Perc = saleinvoiceViewModel.FreightSGST_Perc,
                    FreightSGST_Amt = saleinvoiceViewModel.FreightSGST_Amt,
                    FreightIGST_Perc = saleinvoiceViewModel.FreightIGST_Perc,
                    FreightIGST_Amt = saleinvoiceViewModel.FreightIGST_Amt,

                    LoadingValue = saleinvoiceViewModel.LoadingValue,
                    LoadingCGST_Perc = saleinvoiceViewModel.LoadingCGST_Perc,
                    LoadingCGST_Amt = saleinvoiceViewModel.LoadingCGST_Amt,
                    LoadingSGST_Perc = saleinvoiceViewModel.LoadingSGST_Perc,
                    LoadingSGST_Amt = saleinvoiceViewModel.LoadingSGST_Amt,
                    LoadingIGST_Perc = saleinvoiceViewModel.LoadingIGST_Perc,
                    LoadingIGST_Amt = saleinvoiceViewModel.LoadingIGST_Amt,
                    InsuranceValue = saleinvoiceViewModel.InsuranceValue,
                    InsuranceCGST_Perc = saleinvoiceViewModel.InsuranceCGST_Perc,
                    InsuranceCGST_Amt = saleinvoiceViewModel.InsuranceCGST_Amt,
                    InsuranceSGST_Perc = saleinvoiceViewModel.InsuranceSGST_Perc,
                    InsuranceSGST_Amt = saleinvoiceViewModel.InsuranceSGST_Amt,
                    InsuranceIGST_Perc = saleinvoiceViewModel.InsuranceIGST_Perc,
                    InsuranceIGST_Amt = saleinvoiceViewModel.InsuranceIGST_Amt,
                    PayToBookId = saleinvoiceViewModel.PayToBookId,
                    Remarks = saleinvoiceViewModel.Remarks,
                    FinYearId = saleinvoiceViewModel.FinYearId,
                    CompanyId = saleinvoiceViewModel.CompanyId,
                    CreatedBy = saleinvoiceViewModel.CreatedBy,
                    ReverseChargeApplicable = saleinvoiceViewModel.ReverseChargeApplicable,
                    ReverseChargeAmount = saleinvoiceViewModel.ReverseChargeAmount

                };
                List<SaleInvoiceProductDetail> saleinvoiceProductList = new List<SaleInvoiceProductDetail>();
                if (saleinvoiceProducts != null && saleinvoiceProducts.Count > 0)
                {
                    foreach (SaleInvoiceProductViewModel item in saleinvoiceProducts)
                    {
                        saleinvoiceProductList.Add(new SaleInvoiceProductDetail
                        {
                            ProductId = item.ProductId,
                            ProductShortDesc = item.ProductShortDesc,
                            Price = item.Price,
                            Quantity = item.Quantity,
                            DiscountPercentage = item.DiscountPercentage,
                            DiscountAmount = item.DiscountAmount,
                            TaxId = item.TaxId,
                            TaxName = item.TaxName,
                            TaxPercentage = item.TaxPercentage,
                            TaxAmount = item.TaxAmount,
                            TotalPrice = item.TotalPrice,
                            SurchargeName_1 = item.SurchargeName_1,
                            SurchargePercentage_1 = item.SurchargePercentage_1,
                            SurchargeAmount_1 = item.SurchargeAmount_1,
                            SurchargeName_2 = item.SurchargeName_2,
                            SurchargePercentage_2 = item.SurchargePercentage_2,
                            SurchargeAmount_2 = item.SurchargeAmount_2,
                            SurchargeName_3 = item.SurchargeName_3,
                            SurchargePercentage_3 = item.SurchargePercentage_3,
                            SurchargeAmount_3 = item.SurchargeAmount_3,
                            CGST_Perc = item.CGST_Perc,
                            CGST_Amount = item.CGST_Amount,
                            SGST_Perc = item.SGST_Perc,
                            SGST_Amount = item.SGST_Amount,
                            IGST_Perc = item.IGST_Perc,
                            IGST_Amount = item.IGST_Amount,
                            HSN_Code = item.HSN_Code
                        });
                    }
                }

                List<SaleInvoiceTaxDetail> saleinvoiceTaxList = new List<SaleInvoiceTaxDetail>();
                if (saleinvoiceTaxes != null && saleinvoiceTaxes.Count > 0)
                {
                    foreach (SaleInvoiceTaxViewModel item in saleinvoiceTaxes)
                    {
                        saleinvoiceTaxList.Add(new SaleInvoiceTaxDetail
                        {
                            TaxId = item.TaxId,
                            TaxName = item.TaxName,
                            TaxPercentage = item.TaxPercentage,
                            TaxAmount = item.TaxAmount,
                            SurchargeName_1 = item.SurchargeName_1,
                            SurchargePercentage_1 = item.SurchargePercentage_1,
                            SurchargeAmount_1 = item.SurchargeAmount_1,
                            SurchargeName_2 = item.SurchargeName_2,
                            SurchargePercentage_2 = item.SurchargePercentage_2,
                            SurchargeAmount_2 = item.SurchargeAmount_2,
                            SurchargeName_3 = item.SurchargeName_3,
                            SurchargePercentage_3 = item.SurchargePercentage_3,
                            SurchargeAmount_3 = item.SurchargeAmount_3
                        });
                    }
                }
                List<SaleInvoiceTermsDetail> saleinvoiceTermList = new List<SaleInvoiceTermsDetail>();
                if (saleinvoiceTerms != null && saleinvoiceTerms.Count > 0)
                {
                    foreach (SaleInvoiceTermViewModel item in saleinvoiceTerms)
                    {
                        saleinvoiceTermList.Add(new SaleInvoiceTermsDetail
                        {
                            TermDesc = item.TermDesc,
                            TermSequence = item.TermSequence
                        });
                    }
                }

        List<SaleInvoiceSupportingDocument> siDocumentList = new List<SaleInvoiceSupportingDocument>();
                if (siDocuments != null && siDocuments.Count > 0)
                {
                    foreach (SISupportingDocumentViewModel item in siDocuments)
                    {
                        siDocumentList.Add(new SaleInvoiceSupportingDocument
                        {
                            DocumentTypeId = item.DocumentTypeId,
                            DocumentName = item.DocumentName,
                            DocumentPath = item.DocumentPath
                        });
                    }
                }

                List<SaleInvoiceProductSerialDetail> saleInvoiceProductSerialDetailList = new List<SaleInvoiceProductSerialDetail>();
                if (saleInvoiceProductSerialDetail != null && saleInvoiceProductSerialDetail.Count > 0)
                {
                    foreach (SaleInvoiceProductSerialDetailViewModel item in saleInvoiceProductSerialDetail)
                    {
                        saleInvoiceProductSerialDetailList.Add(new SaleInvoiceProductSerialDetail
                        {
                            InvoiceId = 0,
                           
                            ProductId = item.ProductId,
                            RefSerial1 = item.RefSerial1,
                            RefSerial2 = item.RefSerial2,
                            RefSerial3 = item.RefSerial3,
                            RefSerial4 = item.RefSerial4

                        });
                    }
                }

                int prodQty = 0;
                int rowCnt = 0;
                bool flag = true;
                if (saleinvoiceProducts != null && saleinvoiceProducts.Count > 0)
                {
                    foreach (SaleInvoiceProductViewModel item in saleinvoiceProducts)
                    {
                        if (saleInvoiceProductSerialDetail != null && saleInvoiceProductSerialDetail.Count > 0)
                        {
                            foreach (SaleInvoiceProductSerialDetailViewModel itm in saleInvoiceProductSerialDetail)
                            {

                                if (item.ProductId == itm.ProductId)
                                {
                                       rowCnt++;
                                }
                            }
                            if (item.Quantity != rowCnt)
                            {
                                flag = false;
                            }

                        }
                        
                    }
                }

                if (flag == true)
                {
                    responseOut = sqlDbInterface.AddEditSaleInvoice(saleinvoice, saleinvoiceProductList, saleinvoiceTaxList, saleinvoiceTermList, saleInvoiceProductSerialDetailList,siDocumentList);
                }
                else
                {
                    responseOut.message = ActionMessage.SaleInvoiceProductQuantity;
                    responseOut.status = ActionStatus.Fail;
                }
                 
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
        public List<SaleInvoiceViewModel> GetSaleInvoiceList(string saleinvoiceNo, string customerName, string refNo, string fromDate, string toDate, int companyId,string invoiceType="",string displayType="" ,string approvalStatus="",string customerCode="", int companyBranchId = 0)
        {
            List<SaleInvoiceViewModel> saleinvoices = new List<SaleInvoiceViewModel>();
            SQLDbInterface sqlDbInterface = new SQLDbInterface();
            try
            {
                DataTable dtSaleInvoices = sqlDbInterface.GetSaleInvoiceList(saleinvoiceNo, customerName, refNo, Convert.ToDateTime(fromDate), Convert.ToDateTime(toDate), companyId,invoiceType,displayType, approvalStatus,customerCode, companyBranchId);
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
                            InvoiceType = Convert.ToString(dr["InvoiceType"]),
                            CustomerId = Convert.ToInt32(dr["CustomerId"]),
                            CustomerCode = Convert.ToString(dr["CustomerCode"]),
                            CustomerName = Convert.ToString(dr["CustomerName"]),
                            ConsigneeId = Convert.ToInt32(dr["ConsigneeId"]),
                            ConsigneeName = Convert.ToString(dr["ConsigneeName"]),
                            ConsigneeCode = Convert.ToString(dr["ConsigneeCode"]),
                            CompanyBranchName = Convert.ToString(dr["BranchName"]),
                            City = Convert.ToString(dr["City"]),
                            StateName = Convert.ToString(dr["StateName"]),
                            RefNo = Convert.ToString(dr["RefNo"]),
                            RefDate = Convert.ToString(dr["RefDate"]),
                            BasicValue = Convert.ToDecimal(dr["BasicValue"]),
                            TotalValue = Convert.ToDecimal(dr["TotalValue"]),
                            ApprovalStatus=Convert.ToString(dr["ApprovalStatus"]),
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

        public List<SaleInvoiceProductSerialDetailViewModel> GetSaleInvoiceProductSerialDetailList(int invoiceId=0)
        {
            List<SaleInvoiceProductSerialDetailViewModel> saleInvoiceProductSerialDetailList = new List<SaleInvoiceProductSerialDetailViewModel>();
            SQLDbInterface sqlDbInterface = new SQLDbInterface();
            try
            {
                DataTable dtSaleInvoiceProductSerials = sqlDbInterface.GetSaleInvoiceProductSerialDetailList(invoiceId);
                if (dtSaleInvoiceProductSerials != null && dtSaleInvoiceProductSerials.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtSaleInvoiceProductSerials.Rows)
                    {
                        saleInvoiceProductSerialDetailList.Add(new SaleInvoiceProductSerialDetailViewModel
                        {
                            InvoiceId = Convert.ToInt32(dr["InvoiceId"]),
                            ProductId  = Convert.ToInt32(dr["ProductId"]),
                            ProductName=Convert.ToString(dr["ProductName"]),
                            RefSerial1=Convert.ToString(dr["RefSerial1"]),
                            RefSerial2=Convert.ToString(dr["RefSerial2"]),
                            RefSerial3 = Convert.ToString(dr["RefSerial3"]),
                            RefSerial4 = Convert.ToString(dr["RefSerial4"])
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return saleInvoiceProductSerialDetailList;
        }

        public List<SaleInvoiceViewModel> GetJVSaleInvoiceList(string saleinvoiceNo, string refNo, string fromDate, string toDate, int companyId, string invoiceType = "", string displayType = "", string approvalStatus = "")
        {
            List<SaleInvoiceViewModel> saleinvoices = new List<SaleInvoiceViewModel>();
            SQLDbInterface sqlDbInterface = new SQLDbInterface();
            try
            {
                DataTable dtSaleInvoices = sqlDbInterface.GetJVSaleInvoiceList(saleinvoiceNo, refNo, Convert.ToDateTime(fromDate), Convert.ToDateTime(toDate), companyId, invoiceType, displayType, approvalStatus);
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
                            InvoiceType = Convert.ToString(dr["InvoiceType"]),
                            CustomerId = Convert.ToInt32(dr["CustomerId"]),
                            CustomerCode = Convert.ToString(dr["CustomerCode"]),
                            CustomerName = Convert.ToString(dr["CustomerName"]),
                            City = Convert.ToString(dr["City"]),
                            StateName = Convert.ToString(dr["StateName"]),
                            RefNo = Convert.ToString(dr["RefNo"]),
                            RefDate = Convert.ToString(dr["RefDate"]),
                            BasicValue = Convert.ToDecimal(dr["BasicValue"]),
                            TotalValue = Convert.ToDecimal(dr["TotalValue"]),
                            ApprovalStatus = Convert.ToString(dr["ApprovalStatus"]),
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

        public SaleInvoiceViewModel GetSaleInvoiceDetail(long saleinvoiceId = 0)
        {
            SaleInvoiceViewModel saleinvoice = new SaleInvoiceViewModel();
            SQLDbInterface sqlDbInterface = new SQLDbInterface();
            try
            {
                DataTable dtCompanies = sqlDbInterface.GetSaleInvoiceDetail(saleinvoiceId);
                if (dtCompanies != null && dtCompanies.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtCompanies.Rows)
                    {
                        saleinvoice = new SaleInvoiceViewModel
                        {
                            InvoiceId = Convert.ToInt32(dr["InvoiceId"]),
                            InvoiceNo = Convert.ToString(dr["InvoiceNo"]),
                            InvoiceDate = Convert.ToString(dr["InvoiceDate"]),
                            SOId = Convert.ToInt32(dr["SOId"]),
                            SONo = Convert.ToString(dr["SONo"]),
                            SODate = Convert.ToString(dr["SODate"]),
                            InvoiceType= Convert.ToString(dr["InvoiceType"]),
                            CurrencyCode = Convert.ToString(dr["CurrencyCode"]),
                            CompanyBranchId = Convert.ToInt32(dr["CompanyBranchId"]),
                            CustomerId = Convert.ToInt32(dr["CustomerId"]),
                            CustomerCode = Convert.ToString(dr["CustomerCode"]),
                            CustomerName = Convert.ToString(dr["CustomerName"]),
                            ConsigneeId = Convert.ToInt32(dr["ConsigneeId"]),
                            ConsigneeCode = Convert.ToString(dr["ConsigneeCode"]),
                            ConsigneeName = Convert.ToString(dr["ConsigneeName"]),
                            ContactPerson = Convert.ToString(dr["ContactPerson"]),
                            BillingAddress = Convert.ToString(dr["BillingAddress"]),
                            City = Convert.ToString(dr["City"]),
                            StateId = Convert.ToInt32(dr["StateId"]),
                            CountryId = Convert.ToInt32(dr["CountryId"]),
                            PinCode = Convert.ToString(dr["PinCode"]),
                            TINNo = Convert.ToString(dr["TINNo"]),
                            GSTNo = Convert.ToString(dr["GSTNo"]),
                            Email = Convert.ToString(dr["Email"]),
                            MobileNo = Convert.ToString(dr["MobileNo"]),
                            ContactNo = Convert.ToString(dr["ContactNo"]),
                            Fax = Convert.ToString(dr["Fax"]),
                            ApprovalStatus = Convert.ToString(dr["ApprovalStatus"]),

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


                            RefNo = Convert.ToString(dr["RefNo"]),
                            RefDate = Convert.ToString(dr["RefDate"]),

                            BasicValue = Convert.ToDecimal(dr["BasicValue"]),
                            TotalValue = Convert.ToDecimal(dr["TotalValue"]),

                            FreightValue = Convert.ToDecimal(dr["FreightValue"]),
                            FreightCGST_Perc = Convert.ToDecimal(dr["FreightCGST_Perc"]),
                            FreightCGST_Amt = Convert.ToDecimal(dr["FreightCGST_Amt"]),
                            FreightSGST_Perc = Convert.ToDecimal(dr["FreightSGST_Perc"]),
                            FreightSGST_Amt = Convert.ToDecimal(dr["FreightSGST_Amt"]),
                            FreightIGST_Perc = Convert.ToDecimal(dr["FreightIGST_Perc"]),
                            FreightIGST_Amt = Convert.ToDecimal(dr["FreightIGST_Amt"]),

                            LoadingValue = Convert.ToDecimal(dr["LoadingValue"]),
                            LoadingCGST_Perc = Convert.ToDecimal(dr["LoadingCGST_Perc"]),
                            LoadingCGST_Amt = Convert.ToDecimal(dr["LoadingCGST_Amt"]),
                            LoadingSGST_Perc = Convert.ToDecimal(dr["LoadingSGST_Perc"]),
                            LoadingSGST_Amt = Convert.ToDecimal(dr["LoadingSGST_Amt"]),
                            LoadingIGST_Perc = Convert.ToDecimal(dr["LoadingIGST_Perc"]),
                            LoadingIGST_Amt = Convert.ToDecimal(dr["LoadingIGST_Amt"]),

                            InsuranceValue = Convert.ToDecimal(dr["InsuranceValue"]),
                            InsuranceCGST_Perc = Convert.ToDecimal(dr["InsuranceCGST_Perc"]),
                            InsuranceCGST_Amt = Convert.ToDecimal(dr["InsuranceCGST_Amt"]),
                            InsuranceSGST_Perc = Convert.ToDecimal(dr["InsuranceSGST_Perc"]),
                            InsuranceSGST_Amt = Convert.ToDecimal(dr["InsuranceSGST_Amt"]),
                            InsuranceIGST_Perc = Convert.ToDecimal(dr["InsuranceIGST_Perc"]),
                            InsuranceIGST_Amt = Convert.ToDecimal(dr["InsuranceIGST_Amt"]),

                            PayToBookId = Convert.ToInt32(dr["PayToBookId"]),
                            PayToBookName = Convert.ToString(dr["PayToBookName"]),
                            PayToBookBranch = Convert.ToString(dr["PayToBookBranch"]),
                            ReverseChargeApplicable = Convert.ToBoolean(dr["ReverseChargeApplicable"]),
                            ReverseChargeAmount = Convert.ToDecimal(dr["ReverseChargeAmount"]),

                            Remarks = Convert.ToString(dr["Remarks"]),
                            CreatedByUserName = Convert.ToString(dr["CreatedByName"]),
                            CreatedDate = Convert.ToString(dr["CreatedDate"]),
                            ModifiedByUserName = Convert.ToString(dr["ModifiedByName"]),
                            ModifiedDate = Convert.ToString(dr["ModifiedDate"])
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return saleinvoice;
        }
    
        public List<SaleInvoiceProductViewModel> GetSaleInvoiceProductList(long saleinvoiceId)
        {
            List<SaleInvoiceProductViewModel> saleinvoiceProducts = new List<SaleInvoiceProductViewModel>();
            SQLDbInterface sqlDbInterface = new SQLDbInterface();
            try
            {
                DataTable dtCustomers = sqlDbInterface.GetSaleInvoiceProductList(saleinvoiceId);
                if (dtCustomers != null && dtCustomers.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtCustomers.Rows)
                    {
                        saleinvoiceProducts.Add(new SaleInvoiceProductViewModel
                        {
                            InvoiceProductDetailId = Convert.ToInt32(dr["InvoiceProductDetailId"]),
                            SequenceNo = Convert.ToInt32(dr["SNo"]),
                            ProductId = Convert.ToInt32(dr["ProductId"]),
                            ProductName = Convert.ToString(dr["ProductName"]),
                            ProductCode = Convert.ToString(dr["ProductCode"]),
                            ProductShortDesc = Convert.ToString(dr["ProductShortDesc"]),
                            UOMName = Convert.ToString(dr["UOMName"]),
                            Price = Convert.ToDecimal(dr["Price"]),
                            Quantity = Convert.ToDecimal(dr["Quantity"]),
                            DiscountPercentage = Convert.ToDecimal(dr["DiscountPercentage"]),
                            DiscountAmount = Convert.ToDecimal(dr["DiscountAmount"]),
                            TaxId = Convert.ToInt32(dr["TaxId"]),
                            TaxName = Convert.ToString(dr["TaxName"]),
                            TaxPercentage = Convert.ToDecimal(dr["TaxPercentage"]),
                            TaxAmount = Convert.ToDecimal(dr["TaxAmount"]),
                            TotalPrice = Convert.ToDecimal(dr["TotalPrice"]),
                            SurchargeName_1 = Convert.ToString(dr["SurchargeName_1"]),
                            SurchargePercentage_1 = Convert.ToDecimal(dr["SurchargePercentage_1"]),
                            SurchargeAmount_1 = Convert.ToDecimal(dr["SurchargeAmount_1"]),
                            SurchargeName_2 = Convert.ToString(dr["SurchargeName_2"]),
                            SurchargePercentage_2 = Convert.ToDecimal(dr["SurchargePercentage_2"]),
                            SurchargeAmount_2 = Convert.ToDecimal(dr["SurchargeAmount_2"]),
                            SurchargeName_3 = Convert.ToString(dr["SurchargeName_3"]),
                            SurchargePercentage_3 = Convert.ToDecimal(dr["SurchargePercentage_3"]),
                            SurchargeAmount_3 = Convert.ToDecimal(dr["SurchargeAmount_3"]),
                            CGST_Perc = Convert.ToDecimal(dr["CGST_Perc"]),
                            CGST_Amount = Convert.ToDecimal(dr["CGST_Amount"]),
                            SGST_Perc = Convert.ToDecimal(dr["SGST_Perc"]),
                            SGST_Amount = Convert.ToDecimal(dr["SGST_Amount"]),
                            IGST_Perc = Convert.ToDecimal(dr["IGST_Perc"]),
                            IGST_Amount = Convert.ToDecimal(dr["IGST_Amount"]),
                            HSN_Code = Convert.ToString(dr["HSN_Code"])
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return saleinvoiceProducts;
        }

        public List<SaleInvoiceTaxViewModel> GetSaleInvoiceTaxList(long saleinvoiceId)
        {
            List<SaleInvoiceTaxViewModel> saleinvoiceTaxes = new List<SaleInvoiceTaxViewModel>();
            SQLDbInterface sqlDbInterface = new SQLDbInterface();
            try
            {
                DataTable dtCustomers = sqlDbInterface.GetSaleInvoiceTaxList(saleinvoiceId);
                if (dtCustomers != null && dtCustomers.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtCustomers.Rows)
                    {
                        saleinvoiceTaxes.Add(new SaleInvoiceTaxViewModel
                        {
                            InvoiceTaxDetailId = Convert.ToInt32(dr["InvoiceTaxDetailId"]),
                            TaxSequenceNo = Convert.ToInt32(dr["SNo"]),
                            TaxId = Convert.ToInt32(dr["TaxId"]),
                            TaxName = Convert.ToString(dr["TaxName"]),
                            TaxPercentage = Convert.ToDecimal(dr["TaxPercentage"]),
                            TaxAmount = Convert.ToDecimal(dr["TaxAmount"]),
                            SurchargeName_1 = Convert.ToString(dr["SurchargeName_1"]),
                            SurchargePercentage_1 = Convert.ToDecimal(dr["SurchargePercentage_1"]),
                            SurchargeAmount_1 = Convert.ToDecimal(dr["SurchargeAmount_1"]),
                            SurchargeName_2 = Convert.ToString(dr["SurchargeName_2"]),
                            SurchargePercentage_2 = Convert.ToDecimal(dr["SurchargePercentage_2"]),
                            SurchargeAmount_2 = Convert.ToDecimal(dr["SurchargeAmount_2"]),
                            SurchargeName_3 = Convert.ToString(dr["SurchargeName_3"]),
                            SurchargePercentage_3 = Convert.ToDecimal(dr["SurchargePercentage_3"]),
                            SurchargeAmount_3 = Convert.ToDecimal(dr["SurchargeAmount_3"]),
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return saleinvoiceTaxes;
        }
        public List<SaleInvoiceTermViewModel> GetSaleInvoiceTermList(long saleinvoiceId)
        {
            List<SaleInvoiceTermViewModel> saleinvoiceTerms = new List<SaleInvoiceTermViewModel>();
            SQLDbInterface sqlDbInterface = new SQLDbInterface();
            try
            {
                DataTable dtCustomers = sqlDbInterface.GetSaleInvoiceTermList(saleinvoiceId);
                if (dtCustomers != null && dtCustomers.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtCustomers.Rows)
                    {
                        saleinvoiceTerms.Add(new SaleInvoiceTermViewModel
                        {
                            InvoiceTermDetailId = Convert.ToInt32(dr["InvoiceTermDetailId"]),
                            TermDesc = Convert.ToString(dr["TermDesc"]),
                            TermSequence = Convert.ToInt16(dr["TermSequence"])
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return saleinvoiceTerms;
        }

     
        
        public DataTable GetSaleInvoiceDetailDataTable(long siId = 0)
        {
            SQLDbInterface sqlDbInterface = new SQLDbInterface();
            DataTable dtQuotation = new DataTable();
            try
            {
                dtQuotation = sqlDbInterface.GetSaleInvoiceDetail(siId);
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dtQuotation;
        }
        public DataTable GetSaleInvoiceProductListDataTable(long siId)
        {
            SQLDbInterface sqlDbInterface = new SQLDbInterface();
            DataTable dtProducts = new DataTable();
            try
            {
                dtProducts = sqlDbInterface.GetSaleInvoiceProductList(siId);
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dtProducts;
        }
        public DataTable GetSaleInvoiceTermListDataTable(long siId)
        {

            SQLDbInterface sqlDbInterface = new SQLDbInterface();
            DataTable dtTerms = new DataTable();
            try
            {
                dtTerms = sqlDbInterface.GetSaleInvoiceTermList(siId);
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dtTerms;
        }
        public DataTable GetSaleInvoiceTaxListDataTable(long siId)
        {

            SQLDbInterface sqlDbInterface = new SQLDbInterface();
            DataTable dtTerms = new DataTable();
            try
            {
                dtTerms = sqlDbInterface.GetSaleInvoiceTaxList(siId);
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dtTerms;
        }

        public DataTable GetSaleInvoiceProductSerialDetailDataTable(long siId)
        {
            DataTable dtSaleInvoiceProductSerials;
            SQLDbInterface sqlDbInterface = new SQLDbInterface();
            try
            {
                dtSaleInvoiceProductSerials = sqlDbInterface.GetSaleInvoiceProductSerialDetailList(siId);
                
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dtSaleInvoiceProductSerials;
        }



        public ResponseOut CancelSaleInvoice(SaleInvoiceViewModel saleinvoiceViewModel)
        {
            ResponseOut responseOut = new ResponseOut();
            try
            {
                SaleInvoice saleinvoice = new SaleInvoice
                {
                    InvoiceId = saleinvoiceViewModel.InvoiceId,
                    CancelStatus = "Cancel",
                    ApprovalStatus = "Cancelled",
                    CreatedBy = saleinvoiceViewModel.CreatedBy, 
                    CancelReason=saleinvoiceViewModel.CancelReason
                };
                responseOut = dbInterface.CancelSaleInvoice(saleinvoice);
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
        public List<SISupportingDocumentViewModel> GetSISupportingDocumentList(long siId)
        {
            List<SISupportingDocumentViewModel> siDocuments = new List<SISupportingDocumentViewModel>();
            SQLDbInterface sqlDbInterface = new SQLDbInterface();
            try
            {
                DataTable dtDocument = sqlDbInterface.GetSISupportingDocumentList(siId);
                if (dtDocument != null && dtDocument.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtDocument.Rows)
                    {
                        siDocuments.Add(new SISupportingDocumentViewModel
                        {
                            SaleInvoiceDocId = Convert.ToInt32(dr["SaleInvoiceDocId"]),
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
            return siDocuments;
        }
        public List<GSTR1ViewModel> GetGSTR1B2BList(string fromDate, string toDate, int companyId)
        {
            List<GSTR1ViewModel> gSTR1s = new List<GSTR1ViewModel>();
            SQLDbInterface sqlDbInterface = new SQLDbInterface();
            try
            {
                DataTable dtGSTR1 = sqlDbInterface.GetGSTR1B2B(Convert.ToDateTime(fromDate), Convert.ToDateTime(toDate), companyId);
                if (dtGSTR1 != null && dtGSTR1.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtGSTR1.Rows)
                    {
                        gSTR1s.Add(new GSTR1ViewModel
                        {
                            RecipientCount = Convert.ToInt32(dr["RecipientCount"]),
                            InvoiceCount = Convert.ToInt32(dr["InvoiceCount"]),
                            TotalInvoiceValue = Convert.ToDecimal(dr["TotalInvoiceValue"]),
                            TotalTaxableValue = Convert.ToDecimal(dr["TotalTaxableValue"]),
                            ConsigneGSTNo = Convert.ToString(dr["ConsigneGSTNo"]),
                            InvoiceNo = Convert.ToString(dr["InvoiceNo"]),
                            InvoiceDate = Convert.ToString(dr["InvoiceDate"]),
                            InvoiceValue = Convert.ToDecimal(dr["InvoiceValue"]),
                            PlaceOfSupply = Convert.ToString(dr["PlaceOfSupply"]),
                            ReverseCharge = Convert.ToString(dr["ReverseCharge"]),
                            InvoiceType = Convert.ToString(dr["InvoiceType"]),
                            EcommerceGSTNo = Convert.ToString(dr["EcommerceGSTNo"]),
                            Rate = Convert.ToDecimal(dr["Rate"]),
                            TaxableValue = Convert.ToDecimal(dr["TaxableValue"]),
                            CessValue = Convert.ToDecimal(dr["CessValue"])
                        });

                    }
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return gSTR1s;
        }
        public DataTable GetGSTR1B2BDataTable(string fromDate, string toDate, int companyId)
        {
            DataTable dtGSTR1B2Bs = new DataTable();
            SQLDbInterface sqlDbInterface = new SQLDbInterface();
            try
            {
                dtGSTR1B2Bs = sqlDbInterface.GetGSTR1B2B(Convert.ToDateTime(fromDate), Convert.ToDateTime(toDate), companyId);

            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dtGSTR1B2Bs;
        }

        public DataTable GetGSTR1B2CLDataTable(string fromDate, string toDate, int companyId)
        {
            DataTable dtGSTR1B2Bs = new DataTable();
            SQLDbInterface sqlDbInterface = new SQLDbInterface();
            try
            {
                dtGSTR1B2Bs = sqlDbInterface.GetGSTR1B2CL(Convert.ToDateTime(fromDate), Convert.ToDateTime(toDate), companyId);

            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dtGSTR1B2Bs;
        }
        public DataTable GetGSTR1B2CSDataTable(string fromDate, string toDate, int companyId)
        {
            DataTable dtGSTR1B2Bs = new DataTable();
            SQLDbInterface sqlDbInterface = new SQLDbInterface();
            try
            {
                dtGSTR1B2Bs = sqlDbInterface.GetGSTR1B2CS(Convert.ToDateTime(fromDate), Convert.ToDateTime(toDate), companyId);

            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dtGSTR1B2Bs;
        }

        public DataTable GetGSTR1CDNRDataTable(string fromDate, string toDate, int companyId)
        {
            DataTable dtGSTR1CDNRs = new DataTable();
            SQLDbInterface sqlDbInterface = new SQLDbInterface();
            try
            {
                dtGSTR1CDNRs = sqlDbInterface.GetGSTR1CDNR(Convert.ToDateTime(fromDate), Convert.ToDateTime(toDate), companyId);

            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dtGSTR1CDNRs;
        }

        public DataTable GetGSTR1CDNURDataTable(string fromDate, string toDate, int companyId)
        {
            DataTable dtGSTR1CDNURs = new DataTable();
            SQLDbInterface sqlDbInterface = new SQLDbInterface();
            try
            {
                dtGSTR1CDNURs = sqlDbInterface.GetGSTR1CDNUR(Convert.ToDateTime(fromDate), Convert.ToDateTime(toDate), companyId);

            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dtGSTR1CDNURs;
        }
    }
}
