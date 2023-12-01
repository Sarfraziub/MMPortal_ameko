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
    public class SiteDeliveryChallanBL
    {
        DBInterface dbInterface;
        public SiteDeliveryChallanBL()
        {
            dbInterface = new DBInterface();
        }
        public ResponseOut AddEditSiteDeliveryChallan(SiteDeliveryChallanViewModel siteChallanViewModel, List<SiteChallanProductViewModel> siteChallanProducts,  List<SiteChallanTermsViewModel> siteChallanTerms, List<SiteDeliveryChallanSupportingDocumentViewModel>siteDeliveryChallanDocuments)
        {
            ResponseOut responseOut = new ResponseOut();
            SQLDbInterface sqlDbInterface = new SQLDbInterface();
            try
            {
                SiteDeliveryChallan siteDeliverychallan = new SiteDeliveryChallan
                {
                    SiteChallanId = siteChallanViewModel.SiteChallanId,
                    SiteChallanDate = Convert.ToDateTime(siteChallanViewModel.SiteChallanDate),
                    InvoiceNo = siteChallanViewModel.InvoiceNo,
                    InvoiceId = siteChallanViewModel.InvoiceId,
                    SinNo = siteChallanViewModel.SinNo,
                    SinId = siteChallanViewModel.SinId,
                    CustomerId = siteChallanViewModel.CustomerId,
                    CustomerName = siteChallanViewModel.CustomerName,
                    ConsigneeId = siteChallanViewModel.ConsigneeId,
                    ConsigeeName = siteChallanViewModel.ConsigneeName,
                    ContactPerson = siteChallanViewModel.ContactPerson,  
                    ShippingContactPerson = siteChallanViewModel.ShippingContactPerson,
                    ShippingBillingAddress = siteChallanViewModel.ShippingBillingAddress,
                    ShippingCity = siteChallanViewModel.ShippingCity,
                    ShippingStateId = siteChallanViewModel.ShippingStateId,
                    ShippingCountryId = siteChallanViewModel.ShippingCountryId,
                    ShippingPinCode = siteChallanViewModel.ShippingPinCode,
                    ShippingTINNo = siteChallanViewModel.ShippingTINNo,
                    ShippingEmail = siteChallanViewModel.ShippingEmail,
                    ShippingMobileNo = siteChallanViewModel.ShippingMobileNo,
                    ShippingContactNo = siteChallanViewModel.ShippingContactNo,
                    ShippingFax = siteChallanViewModel.ShippingFax,
                    CompanyBranchId= siteChallanViewModel.CompanyBranchId,
                    DispatchRefNo = siteChallanViewModel.DispatchRefNo,
                    LRNo = siteChallanViewModel.LRNo, 
                    LRDate = string.IsNullOrEmpty(siteChallanViewModel.LRDate) ? Convert.ToDateTime("01-01-1900") : Convert.ToDateTime(siteChallanViewModel.LRDate),
                    TransportVia = siteChallanViewModel.TransportVia,
                    NoOfPackets = siteChallanViewModel.NoOfPackets, 
                    DispatchRefDate = string.IsNullOrEmpty(siteChallanViewModel.DispatchRefDate) ? Convert.ToDateTime("01-01-1900") : Convert.ToDateTime(siteChallanViewModel.DispatchRefDate),
                    BasicValue = siteChallanViewModel.BasicValue,
                    TotalValue = siteChallanViewModel.TotalValue,
                    FreightValue = siteChallanViewModel.FreightValue,
                    FreightCGST_Perc = siteChallanViewModel.FreightCGST_Perc,
                    FreightCGST_Amt = siteChallanViewModel.FreightCGST_Amt,
                    FreightSGST_Perc = siteChallanViewModel.FreightSGST_Perc,
                    FreightSGST_Amt = siteChallanViewModel.FreightSGST_Amt,
                    FreightIGST_Perc = siteChallanViewModel.FreightIGST_Perc,
                    FreightIGST_Amt = siteChallanViewModel.FreightIGST_Amt,

                    LoadingValue = siteChallanViewModel.LoadingValue,
                    LoadingCGST_Perc = siteChallanViewModel.LoadingCGST_Perc,
                    LoadingCGST_Amt = siteChallanViewModel.LoadingCGST_Amt,
                    LoadingSGST_Perc = siteChallanViewModel.LoadingSGST_Perc,
                    LoadingSGST_Amt = siteChallanViewModel.LoadingSGST_Amt,
                    LoadingIGST_Perc = siteChallanViewModel.LoadingIGST_Perc,
                    LoadingIGST_Amt = siteChallanViewModel.LoadingIGST_Amt,
                    InsuranceValue = siteChallanViewModel.InsuranceValue,
                    InsuranceCGST_Perc = siteChallanViewModel.InsuranceCGST_Perc,
                    InsuranceCGST_Amt = siteChallanViewModel.InsuranceCGST_Amt,
                    InsuranceSGST_Perc = siteChallanViewModel.InsuranceSGST_Perc,
                    InsuranceSGST_Amt = siteChallanViewModel.InsuranceSGST_Amt,
                    InsuranceIGST_Perc = siteChallanViewModel.InsuranceIGST_Perc,
                    InsuranceIGST_Amt = siteChallanViewModel.InsuranceIGST_Amt,
                    Remarks1 = siteChallanViewModel.Remarks1,
                    Remarks2 = siteChallanViewModel.Remarks2,
                    ApprovalStatus= siteChallanViewModel.ApprovalStatus,
                    FinYearId = siteChallanViewModel.FinYearId,
                    CompanyId = siteChallanViewModel.CompanyId,
                    CreatedBy = siteChallanViewModel.CreatedBy,
                    ReverseChargeApplicable = siteChallanViewModel.ReverseChargeApplicable,
                    ReverseChargeAmount = siteChallanViewModel.ReverseChargeAmount,
                    CustomerBranchId= siteChallanViewModel.CustomerBranchId

                };
                List<SiteChallanProductDetail> siteChallanProductList = new List<SiteChallanProductDetail>();
                if (siteChallanProducts != null && siteChallanProducts.Count > 0)
                {
                    foreach (SiteChallanProductViewModel item in siteChallanProducts)
                    {
                        siteChallanProductList.Add(new SiteChallanProductDetail
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

                //List<ChallanTaxDetail> challanTaxList = new List<ChallanTaxDetail>();
                //if (challanTaxes != null && challanTaxes.Count > 0)
                //{
                //    foreach (ChallanTaxViewModel item in challanTaxes)
                //    {
                //        challanTaxList.Add(new ChallanTaxDetail
                //        {
                //            TaxId = item.TaxId,
                //            TaxName = item.TaxName,
                //            TaxPercentage = item.TaxPercentage,
                //            TaxAmount = item.TaxAmount
                //        });
                //    }
                //}
                List<SiteChallanTermsDetail> siteChallanTermList = new List<SiteChallanTermsDetail>();
                if (siteChallanTermList != null && siteChallanTermList.Count > 0)
                {
                    foreach (SiteChallanTermsViewModel item in siteChallanTerms)
                    {
                        siteChallanTermList.Add(new SiteChallanTermsDetail
                        {
                            TermDesc = item.TermDesc,
                            TermSequence = item.TermSequence
                        });
                    }
                }


                List<SiteDeliveryChallanSupportingDocument> siteDeliveryChallanDocumentList = new List<SiteDeliveryChallanSupportingDocument>();
                if (siteDeliveryChallanDocuments != null && siteDeliveryChallanDocuments.Count > 0)
                {
                    foreach (SiteDeliveryChallanSupportingDocumentViewModel item in siteDeliveryChallanDocuments)
                    {
                        siteDeliveryChallanDocumentList.Add(new SiteDeliveryChallanSupportingDocument
                        {
                            DocumentTypeId = item.DocumentTypeId,
                            DocumentName = item.DocumentName,
                            DocumentPath = item.DocumentPath
                        });
                    }
                }

                responseOut = sqlDbInterface.AddEditSiteDeliveryChallan(siteDeliverychallan, siteChallanProductList, siteChallanTermList, siteDeliveryChallanDocumentList); 

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
        
        public List<SiteDeliveryChallanViewModel> GetSiteChallanList(string siteDeliverychallanNo, string customerName, string dispatchrefNo, string fromDate, string toDate, string approvalStatus, int companyId)
        {
            List<SiteDeliveryChallanViewModel> siteDeliverychallans = new List<SiteDeliveryChallanViewModel>();
            SQLDbInterface sqlDbInterface = new SQLDbInterface();
            try
            {
                DataTable dtDeliveryChallans = sqlDbInterface.GetSiteChallanList(siteDeliverychallanNo, customerName, dispatchrefNo, Convert.ToDateTime(fromDate), Convert.ToDateTime(toDate), approvalStatus, companyId);
                if (dtDeliveryChallans != null && dtDeliveryChallans.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtDeliveryChallans.Rows)
                    {
                        siteDeliverychallans.Add(new SiteDeliveryChallanViewModel
                        {
                            SiteChallanId = Convert.ToInt32(dr["SiteChallanId"]),
                            SiteChallanNo = Convert.ToString(dr["SiteChallanNo"]),
                            SiteChallanDate = Convert.ToString(dr["SiteChallanDate"]),
                            CustomerId = Convert.ToInt32(dr["CustomerId"]),
                            CustomerCode = Convert.ToString(dr["CustomerCode"]),
                            CustomerName = Convert.ToString(dr["CustomerName"]),
                            ShippingCity = Convert.ToString(dr["ShippingCity"]),
                            ShippingStateName = Convert.ToString(dr["StateName"]),
                            DispatchRefNo = Convert.ToString(dr["DispatchRefNo"]),
                            DispatchRefDate = Convert.ToString(dr["DispatchRefDate"]),
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
            return siteDeliverychallans;
        }
        public SiteDeliveryChallanViewModel GetSiteChallanDetail(long siteChallanId = 0)
        {
            SiteDeliveryChallanViewModel siteDeliverychallan = new SiteDeliveryChallanViewModel();
            SQLDbInterface sqlDbInterface = new SQLDbInterface();
            try
            {
                DataTable dtCompanies = sqlDbInterface.GetSiteChallanDetail(siteChallanId);
                if (dtCompanies != null && dtCompanies.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtCompanies.Rows)
                    {
                        siteDeliverychallan = new SiteDeliveryChallanViewModel
                        {
                            SiteChallanId = Convert.ToInt32(dr["SiteChallanId"]),
                            SiteChallanNo = Convert.ToString(dr["SiteChallanNo"]),
                            SiteChallanDate = Convert.ToString(dr["SiteChallanDate"]),
                            InvoiceId = Convert.ToInt32(dr["InvoiceId"]),
                            InvoiceNo = Convert.ToString(dr["InvoiceNo"]),
                            InvoiceDate = Convert.ToString(dr["InvoiceDate"]),
                            SinId = Convert.ToInt32(dr["SinId"]),
                            SinNo = Convert.ToString(dr["SinNo"]),
                            CustomerId = Convert.ToInt32(dr["CustomerId"]),
                            CustomerBranchId = Convert.ToInt32(dr["CustomerBranchId"]),
                            CustomerCode = Convert.ToString(dr["CustomerCode"]),
                            CustomerName = Convert.ToString(dr["CustomerName"]),
                            ConsigneeId = Convert.ToInt32(dr["ConsigneeId"]),
                            ConsigneeCode = Convert.ToString(dr["ConsigneeCode"]),
                            ConsigneeName = Convert.ToString(dr["ConsigneeName"]),

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

                            CompanyBranchId = Convert.ToInt32(string.IsNullOrEmpty(dr["CompanyBranchId"].ToString())? "0": dr["CompanyBranchId"]),
                            CompanyBranchName = Convert.ToString(dr["CompanyBranchName"]),
                            CompanyBranchAddress = Convert.ToString(dr["CompanyBranchAddress"]),
                            CompanyBranchCity = Convert.ToString(dr["CompanyBranchCity"]),
                            CompanyBranchStateName = Convert.ToString(dr["CompanyBranchStateName"]),
                            CompanyBranchPinCode = Convert.ToString(dr["CompanyBranchPinCode"]),
                            CompanyBranchCSTNo = Convert.ToString(dr["CompanyBranchCSTNo"]),
                            CompanyBranchTINNo = Convert.ToString(dr["CompanyBranchTINNo"]),
                            DispatchRefNo = Convert.ToString(dr["DispatchRefNo"]),
                            DispatchRefDate = Convert.ToString(dr["DispatchRefDate"]),
                             
                            LRNo = Convert.ToString(dr["LRNo"]),
                            LRDate = Convert.ToString(dr["LRDate"]), 

                            TransportVia = Convert.ToString(dr["TransportVia"]),
                            NoOfPackets = Convert.ToInt32(dr["NoOfPackets"]), 

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

                            ReverseChargeApplicable = Convert.ToBoolean(dr["ReverseChargeApplicable"]),
                            ReverseChargeAmount = Convert.ToDecimal(dr["ReverseChargeAmount"]),
                            Remarks1 = Convert.ToString(dr["Remarks1"]),
                            Remarks2 = Convert.ToString(dr["Remarks2"]),

                            ApprovalStatus = Convert.ToString(dr["ApprovalStatus"]),
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
            return siteDeliverychallan;
        }
    
       

  
        public List<SiteChallanProductViewModel> GetSiteChallanProductList(long siteChallanId)
        {
            List<SiteChallanProductViewModel> siteChallanProducts = new List<SiteChallanProductViewModel>();
            SQLDbInterface sqlDbInterface = new SQLDbInterface();
            try
            {
                DataTable dtsiteChallan = sqlDbInterface.GetSiteChallanProductList(siteChallanId);
                if (dtsiteChallan != null && dtsiteChallan.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtsiteChallan.Rows)
                    {
                        siteChallanProducts.Add(new SiteChallanProductViewModel
                        {
                            SiteChallanProductDetailId = Convert.ToInt32(dr["SiteChallanProductDetailId"]),
                            SequenceNo=Convert.ToInt32(dr["SNo"]),
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
            return siteChallanProducts;
        }

        //public List<ChallanTaxViewModel> GetChallanTaxList(long challanId)
        //{
        //    List<ChallanTaxViewModel> challanTaxes = new List<ChallanTaxViewModel>();
        //    SQLDbInterface sqlDbInterface = new SQLDbInterface();
        //    try
        //    {
        //        DataTable dtCustomers = sqlDbInterface.GetChallanTaxList(challanId);
        //        if (dtCustomers != null && dtCustomers.Rows.Count > 0)
        //        {
        //            foreach (DataRow dr in dtCustomers.Rows)
        //            {
        //                challanTaxes.Add(new ChallanTaxViewModel
        //                {
        //                    ChallanTaxDetailId = Convert.ToInt32(dr["ChallanTaxDetailId"]),
        //                    TaxSequenceNo = Convert.ToInt32(dr["SNo"]),
        //                    TaxId = Convert.ToInt32(dr["TaxId"]),
        //                    TaxName = Convert.ToString(dr["TaxName"]),
        //                    TaxPercentage = Convert.ToDecimal(dr["TaxPercentage"]),
        //                    TaxAmount = Convert.ToDecimal(dr["TaxAmount"])
        //                });
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
        //        throw ex;
        //    }
        //    return challanTaxes;
        //}
        public List<SiteChallanTermsViewModel> GetSiteChallanTermList(long siteChallanId)
        {
            List<SiteChallanTermsViewModel> siteChallanTerms = new List<SiteChallanTermsViewModel>();
            SQLDbInterface sqlDbInterface = new SQLDbInterface();
            try
            {
                DataTable dtSiteChallanTerms = sqlDbInterface.GetSiteChallanTermList(siteChallanId);
                if (dtSiteChallanTerms != null && dtSiteChallanTerms.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtSiteChallanTerms.Rows)
                    {
                        siteChallanTerms.Add(new SiteChallanTermsViewModel
                        {
                            SiteChallanTermDetailId = Convert.ToInt32(dr["SiteChallanTermDetailId"]),
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
            return siteChallanTerms;
        }

        
        public DataTable GetSiteChallanDetailTable(long siteChallanId=0)
        {
            SQLDbInterface sqlDbInterface = new SQLDbInterface();
            DataTable dtChallan = new DataTable();
            try
            {
                dtChallan = sqlDbInterface.GetSiteChallanDetail(siteChallanId);
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dtChallan;
        }
        public DataTable GetSiteChallanProductListDataTable(long siteChallanId)
        {

            SQLDbInterface sqlDbInterface = new SQLDbInterface();
            DataTable dtProducts = new DataTable();
            try
            {
                dtProducts = sqlDbInterface.GetSiteChallanProductList(siteChallanId);
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dtProducts;
        }
        //public DataTable GetChallanTaxListDataTable(long challanId)
        //{

        //    SQLDbInterface sqlDbInterface = new SQLDbInterface();
        //    DataTable dtTerms = new DataTable();
        //    try
        //    {
        //        dtTerms = sqlDbInterface.GetChallanTaxList(challanId);
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
        //        throw ex;
        //    }
        //    return dtTerms;
        //}
        public DataTable GetSiteChallanTermListDataTable(long siteChallanId)
        {

            SQLDbInterface sqlDbInterface = new SQLDbInterface();
            DataTable dtTerms = new DataTable();
            try
            {
                dtTerms = sqlDbInterface.GetSiteChallanTermList(siteChallanId);
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dtTerms;
        }

        public List<SiteDeliveryChallanSupportingDocumentViewModel> GetSiteDeliveryChallanSupportingDocumentList(long siteChallanId)
        {
            List<SiteDeliveryChallanSupportingDocumentViewModel> siteDeliveryChallanDocuments = new List<SiteDeliveryChallanSupportingDocumentViewModel>();
            SQLDbInterface sqlDbInterface = new SQLDbInterface();
            try
            {
                DataTable dtDocument = sqlDbInterface.GetSiteDeliveryChallanSupportingDocumentList(siteChallanId);
                if (dtDocument != null && dtDocument.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtDocument.Rows)
                    {
                        siteDeliveryChallanDocuments.Add(new SiteDeliveryChallanSupportingDocumentViewModel
                        {
                            SiteDeliveryChallanDocId = Convert.ToInt32(dr["SiteDeliveryChallanDocId"]),
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
            return siteDeliveryChallanDocuments;
        }

        public List<SINViewModel> GetDeliveryChallanSINList(string sinNo, string requisitionNo, string jobno, int companyBranchId, string fromDate, string toDate, int companyId, string sINStatus)
        {
            List<SINViewModel> stns = new List<SINViewModel>();
            SQLDbInterface sqlDbInterface = new SQLDbInterface();
            try
            {
                DataTable dtSTNs = sqlDbInterface.GetDeliveryChallanSINList(sinNo, requisitionNo, jobno, companyBranchId, Convert.ToDateTime(fromDate), Convert.ToDateTime(toDate), companyId, sINStatus);


                if (dtSTNs != null && dtSTNs.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtSTNs.Rows)
                    {

                        stns.Add(new SINViewModel
                        {
                            SINId = Convert.ToInt32(dr["SINId"]),
                            SINNo = Convert.ToString(dr["SINNo"]),
                            SINDate = Convert.ToString(dr["SINDate"]),
                            RequisitionNo = Convert.ToString(dr["RequisitionNo"]),
                            JobNo = Convert.ToString(dr["JobNo"]),
                            CustomerId = Convert.ToInt32(dr["CustomerId"]),
                            CustomerName = Convert.ToString(dr["CustomerName"]),
                            CustomerAddress = Convert.ToString(dr["CustomerAddress"]),
                            CustomerGSTNo = Convert.ToString(dr["CustomerGSTNo"]),
                            CustomerCode = Convert.ToString(dr["CustomerCode"]),
                            CustomerBranchId = Convert.ToInt32(dr["CustomerBranchId"]),
                            CustomerBranchName = Convert.ToString(dr["CustomerBranchName"]),
                            CustomerBranchAddress = Convert.ToString(dr["CustomerBranchAddress"]),
                            CustomerBranchGSTNo = Convert.ToString(dr["CustomerBranchGSTNo"]),
                            BranchAddress = Convert.ToString(dr["PrimaryAddress"]),
                            CompanyBranchId = Convert.ToInt32(dr["CompanyBranchId"]),
                            BranchName = Convert.ToString(dr["CompanyBranchName"]),
                            EmployeeName = Convert.ToString(dr["EmployeeName"]),
                            FromLocationName = Convert.ToString(dr["FromLocationName"]),
                            ToLocationName = Convert.ToString(dr["ToLocationName"]),
                            CountryId = Convert.ToInt32(dr["CountryId"]),
                            StateId = Convert.ToInt32(dr["StateId"]),
                            City = Convert.ToString(dr["City"]),
                            RefNo = Convert.ToString(dr["RefNo"]),
                            SINStatus = Convert.ToString(dr["SINStatus"]),
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
            return stns;
        }
    }
}
