using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Portal.Core;
using Portal.Core.ViewModel;
using Portal.Common;
using System.Reflection;
using System.Data;
using Microsoft.Reporting.WebForms;
using System.IO;

namespace Portal.Controllers
{
    [CheckSessionBeforeControllerExecuteAttribute(Order = 1)]
    public class CustomerSiteMRNController : BaseController
    {
        //
        // GET: /MRN/
        #region MRN
        [ValidateRequest(true, UserInterfaceHelper.Add_Edit_CustomerSiteMRN, (int)AccessMode.ViewAccess, (int)RequestMode.GetPost)]
        public ActionResult AddEditCustomerSiteMRN(int customerSiteMrnId = 0, int accessMode = 3)
        {
            try
            {
                FinYearViewModel finYear = Session[SessionKey.CurrentFinYear] != null ? (FinYearViewModel)Session[SessionKey.CurrentFinYear] : new FinYearViewModel();
                ViewData["fromDate"] = finYear.StartDate;
                ViewData["toDate"] = finYear.EndDate;
                ViewData["currentDate"] = DateTime.Now.ToString("dd-MMM-yyyy");
                if (customerSiteMrnId != 0)
                {
                    ViewData["customerSiteMrnId"] = customerSiteMrnId;
                    ViewData["accessMode"] = accessMode;
                }
                else
                {
                    ViewData["customerSiteMrnId"] = 0;
                    ViewData["accessMode"] = 0;
                }

            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return View();
        }

        [HttpPost]
        [ValidateRequest(true, UserInterfaceHelper.Add_Edit_CustomerSiteMRN, (int)AccessMode.AddAccess, (int)RequestMode.Ajax)]
        public ActionResult AddEditCustomerSiteMRN(CustomerSiteMRNViewModel customerSiteMrnViewModel, List<CustomerSiteMRNProductDetailViewModel> customerSiteMrnProducts, List<CustomerSiteMRNSupportingDocumentViewModel> customerSiteMrnDocuments)
        {
            ResponseOut responseOut = new ResponseOut();
            CustomerSiteMRNBL customerSitemrnBL = new CustomerSiteMRNBL();
            try
            {
                if (customerSiteMrnViewModel != null)
                {
                    customerSiteMrnViewModel.CreatedBy = ContextUser.UserId;
                    customerSiteMrnViewModel.CompanyId = ContextUser.CompanyId;
                    customerSiteMrnViewModel.FinYearId = Session[SessionKey.CurrentFinYear] != null ? ((FinYearViewModel)Session[SessionKey.CurrentFinYear]).FinYearId : DateTime.Now.Year;
                    responseOut = customerSitemrnBL.AddEditCustomerSiteMRN(customerSiteMrnViewModel, customerSiteMrnProducts, customerSiteMrnDocuments);
                }
                else
                {
                    responseOut.message = ActionMessage.ProbleminData;
                    responseOut.status = ActionStatus.Fail;
                    responseOut.trnId = 0;
                }

            }
            catch (Exception ex)
            {
                responseOut.message = ActionMessage.ApplicationException;
                responseOut.status = ActionStatus.Fail;
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return Json(responseOut, JsonRequestBehavior.AllowGet);
        }
        [ValidateRequest(true, UserInterfaceHelper.Add_Edit_CustomerSiteMRN, (int)AccessMode.ViewAccess, (int)RequestMode.GetPost)]
        public ActionResult ListCustomerSiteMRN()
        {
            try
            {
                FinYearViewModel finYear = Session[SessionKey.CurrentFinYear] != null ? (FinYearViewModel)Session[SessionKey.CurrentFinYear] : new FinYearViewModel();

                ViewData["fromDate"] = finYear.StartDate;
                ViewData["toDate"] = finYear.EndDate;

            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return View();
        }


        [HttpGet]
        public PartialViewResult GetCustomerSiteMRNList(string customerSiteMrnNo = "", string vendorName = "", string dispatchrefNo = "", string fromDate = "", string toDate = "", string approvalStatus = "",string isLocal="")
        {
            List<CustomerSiteMRNViewModel> customerSiteMrns = new List<CustomerSiteMRNViewModel>();
            CustomerSiteMRNBL customerSiteMrnBL = new CustomerSiteMRNBL();
            try
            {
                customerSiteMrns = customerSiteMrnBL.GetCustomerSiteMRNList(customerSiteMrnNo, vendorName, dispatchrefNo, fromDate, toDate, ContextUser.CompanyId, approvalStatus, isLocal);
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return PartialView(customerSiteMrns);
        }
        [HttpGet]
        public PartialViewResult GetPurchaseInvoiceList(string piNo="", string vendorName="", string refNo="", string fromDate="", string toDate="", string approvalStatus = "", string displayType = "")
        {
            List<PurchaseInvoiceViewModel> invoices = new List<PurchaseInvoiceViewModel>();
            
            PurchaseInvoiceBL purchaseInvoiceBL = new PurchaseInvoiceBL();
            try
            {
                
               invoices = purchaseInvoiceBL.GetPIList(piNo,vendorName, refNo, fromDate, toDate, ContextUser.CompanyId,approvalStatus, displayType);
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return PartialView(invoices);
        }

        [HttpPost]
        public PartialViewResult GetCustomerSiteMRNProductList(List<CustomerSiteMRNProductDetailViewModel> customerSiteMrnProducts, long customerSiteMrnId)
        {
            CustomerSiteMRNBL customerSiteMrnBL = new CustomerSiteMRNBL();
            try
            {
                if (customerSiteMrnProducts == null)
                {
                    customerSiteMrnProducts = customerSiteMrnBL.GetCustomerSiteMRNProductList(customerSiteMrnId);
                }

            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return PartialView(customerSiteMrnProducts);
        }

        [HttpGet]
        public JsonResult GetCustomerSiteMRNDetail(long customerSiteMrnId)
        {
            CustomerSiteMRNBL customerSiteMrnBL = new CustomerSiteMRNBL();
            CustomerSiteMRNViewModel customerSiteMrn = new CustomerSiteMRNViewModel();
            try
            {
                customerSiteMrn = customerSiteMrnBL.GetCustomerSiteMRNDetail(customerSiteMrnId);
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return Json(customerSiteMrn, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Report(long customerSiteMrnId, string reportType = "PDF")
        {
            LocalReport lr = new LocalReport();
            CustomerSiteMRNBL customerSiteMrnBL = new CustomerSiteMRNBL();
            PurchaseInvoiceBL purchaseInvoiceBL = new PurchaseInvoiceBL();
            string path = Path.Combine(Server.MapPath("~/RDLC"), "MRNPrint.rdlc");
            if (System.IO.File.Exists(path))
            {
                lr.ReportPath = path;
            }
            else
            {
                return View("Index");
            }

            DataTable dt = new DataTable();
            dt = customerSiteMrnBL.GetCustomerSiteMRNDetailDataTable(customerSiteMrnId);

            //decimal totalInvoiceAmount = 0;
            //totalInvoiceAmount = Convert.ToDecimal(dt.Rows[0]["TotalValue"].ToString());
            //string totalWords = CommonHelper.changeToWords(totalInvoiceAmount.ToString());

            ReportDataSource rd = new ReportDataSource("DataSet1", dt);
            ReportDataSource rdProduct = new ReportDataSource("DataSetProduct",customerSiteMrnBL.GetCustomerSiteMRNProductListDataTable(customerSiteMrnId));
            
            lr.DataSources.Add(rd);
            lr.DataSources.Add(rdProduct);
            

            //ReportParameter rp1 = new ReportParameter("AmountInWords", totalWords);
            //ReportParameter rp2 = new ReportParameter("ReportOption", reportOption);
            //lr.SetParameters(rp1);
            //lr.SetParameters(rp2);

            string mimeType;
            string encoding;
            string fileNameExtension;



            string deviceInfo = "<DeviceInfo>" +
                        "  <OutputFormat>" + reportType + "</OutputFormat>" +
                        "  <PageWidth>8.5in</PageWidth>" +
                        "  <PageHeight>11in</PageHeight>" +
                        "  <MarginTop>0.50in</MarginTop>" +
                        "  <MarginLeft>.1in</MarginLeft>" +
                        "  <MarginRight>.1in</MarginRight>" +
                        "  <MarginBottom>0.5in</MarginBottom>" +
                        "</DeviceInfo>";

            Warning[] warnings;
            string[] streams;
            byte[] renderedBytes;

            renderedBytes = lr.Render(
                reportType,
                deviceInfo,
                out mimeType,
                out encoding,
                out fileNameExtension,
                out streams,
                out warnings);


            return File(renderedBytes, mimeType);
        }

        [HttpPost]
        public ActionResult SaveSupportingDocument()
        {
            ResponseOut responseOut = new ResponseOut();
            HttpFileCollectionBase files = Request.Files;
            Random rnd = new Random();
            try
            {
                //  Get all files from Request object  
                if (files != null && files.Count > 0 && Request.Files[0] != null && Request.Files[0].ContentLength > 0)
                {
                    HttpPostedFileBase file = files[0];
                    string fname;
                    // Checking for Internet Explorer  
                    if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                    {
                        string[] testfiles = file.FileName.Split(new char[] { '\\' });
                        fname = testfiles[testfiles.Length - 1];
                    }
                    else
                    {
                        fname = file.FileName;
                    }

                    if (file != null && file.ContentLength > 0)
                    {
                        string newFileName = "";
                        var fileName = Path.GetFileName(file.FileName);
                        newFileName = Convert.ToString(rnd.Next(10000, 99999)) + "_" + fileName;
                        var path = Path.Combine(Server.MapPath("~/Images/MRNDocument"), newFileName);
                        file.SaveAs(path);
                        responseOut.status = ActionStatus.Success;
                        responseOut.message = newFileName;
                    }
                    else
                    {
                        responseOut.status = ActionStatus.Fail;
                        responseOut.message = "";
                    }
                }
                else
                {
                    responseOut.status = ActionStatus.Fail;
                    responseOut.message = "";
                }


            }
            catch (Exception ex)
            {
                responseOut.message = "";
                responseOut.status = ActionStatus.Fail;
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return Json(responseOut, JsonRequestBehavior.AllowGet);
        }
        public FileResult DocumentDownload(string fileName)
        {
            var path = Path.Combine(Server.MapPath("~/Images/MRNDocument"), fileName);
            byte[] fileBytes = System.IO.File.ReadAllBytes(path);

            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
        }

        [HttpPost]
        public PartialViewResult GetCustomerSiteMRNSupportingDocumentList(List<CustomerSiteMRNSupportingDocumentViewModel> customerSiteMrnDocuments, long customerSiteMrnId)
        {

          
            CustomerSiteMRNBL customerSiteMRNBL = new CustomerSiteMRNBL();
            try
            {
                if (customerSiteMrnDocuments == null)
                {
                    customerSiteMrnDocuments = customerSiteMRNBL.GetCustomerSiteMRNSupportingDocumentList(customerSiteMrnId);
                }

            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return PartialView(customerSiteMrnDocuments);
        }

        [HttpPost]
        public PartialViewResult GetStoreRequisitionProductList(List<StoreRequisitionProductDetailViewModel> storeRequisitionProducts, long requisitionId)
        {
            StoreRequisitionBL storeRequisitionBL = new StoreRequisitionBL();
            try
            {
                if (storeRequisitionProducts == null)
                {
                    storeRequisitionProducts = storeRequisitionBL.GetStoreRequisitionProductList(requisitionId);
                }

            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return PartialView(storeRequisitionProducts);
        }

        [HttpGet]
        public PartialViewResult GetStoreRequisitionList(string requisitionNo = "", string workOrderNo = "", string requisitionType = "", int companyBranchId = 0, string fromDate = "", string toDate = "", string displayType = "", string approvalStatus = "")
        {
            List<StoreRequisitionViewModel> requisitions = new List<StoreRequisitionViewModel>();
            StoreRequisitionBL storeRequisitionBL = new StoreRequisitionBL();
            CustomerSiteMRNBL customerSiteMRNBL = new CustomerSiteMRNBL();
            try
            {
                requisitions = customerSiteMRNBL.GetCustomerSiteMRNRequisitionList(requisitionNo, workOrderNo, requisitionType, companyBranchId, Convert.ToDateTime(fromDate), Convert.ToDateTime(toDate), ContextUser.CompanyId, displayType, approvalStatus);
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return PartialView(requisitions);
        }

        [HttpPost]
        [ValidateRequest(true, UserInterfaceHelper.Add_Edit_CustomerSiteMRN, (int)AccessMode.AddAccess, (int)RequestMode.Ajax)]
        public ActionResult AddEditVendorMaster(VendorViewModel vendorViewModel)
        {
            ResponseOut responseOut = new ResponseOut();
            VendorBL vendorBL = new VendorBL();
            try
            {
                if (vendorViewModel != null)
                {
                    vendorViewModel.CreatedBy = ContextUser.UserId;
                    vendorViewModel.CompanyId = ContextUser.CompanyId;
                    responseOut = vendorBL.AddEditVendorMaster(vendorViewModel);
                }
                else
                {
                    responseOut.message = ActionMessage.ProbleminData;
                    responseOut.status = ActionStatus.Fail;
                }

            }
            catch (Exception ex)
            {
                responseOut.message = ActionMessage.ApplicationException;
                responseOut.status = ActionStatus.Fail;
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return Json(responseOut, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public JsonResult GetProductPurchasePrice(int productId)
        {
            CustomerSiteMRNBL dailyExpenseBL = new CustomerSiteMRNBL();
            decimal PurchasePrice = 0;
            try
            {
                PurchasePrice = dailyExpenseBL.GetProductPurchasePrice(productId);
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return Json(PurchasePrice, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}
