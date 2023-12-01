using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Portal.Core;
using Portal.Core.ViewModel;
using Portal.Common;
using System.Reflection;
using Microsoft.Reporting.WebForms;
using Microsoft.ReportingServices;
using System.IO;
using System.Data;
using System.Text;
namespace Portal.Controllers
{
    [CheckSessionBeforeControllerExecuteAttribute(Order = 1)]
    public class SaleInvoiceController : BaseController
    {
        #region SaleInvoice

        //
        // GET: /Quotation/

        [ValidateRequest(true, UserInterfaceHelper.Add_Edit_SaleInvoice, (int)AccessMode.ViewAccess, (int)RequestMode.GetPost)]
        public ActionResult AddEditSaleInvoice(int siId = 0, int accessMode = 3, int customerId = 0, string customerCode = "", string customerName = "")
        {
            try
            {
                FinYearViewModel finYear = Session[SessionKey.CurrentFinYear] != null ? (FinYearViewModel)Session[SessionKey.CurrentFinYear] : new FinYearViewModel();
                ViewData["fromDate"] = finYear.StartDate;
                ViewData["toDate"] = finYear.EndDate;
                ViewData["currentDate"] = DateTime.Now.ToString("dd-MMM-yyyy");
                ViewData["RoleId"] = ContextUser.RoleId;

                if (siId != 0)
                {
                    ViewData["siId"] = siId;
                    ViewData["accessMode"] = accessMode;
                    
                }
                else
                {
                    ViewData["siId"] = 0;
                    ViewData["accessMode"] = 3;
                    ViewData["customerId"] = customerId;
                    ViewData["customerCode"] = customerCode;
                    ViewData["customerName"] = customerName;

                }

            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return View();
        }


        [HttpPost]
        [ValidateRequest(true, UserInterfaceHelper.Add_Edit_SaleInvoice, (int)AccessMode.AddAccess, (int)RequestMode.Ajax)]
        public ActionResult AddEditSaleInvoice(SaleInvoiceViewModel saleinvoiceViewModel, List<SaleInvoiceProductViewModel> saleinvoiceProducts, List<SaleInvoiceTaxViewModel> saleinvoiceTaxes, List<SaleInvoiceTermViewModel> saleinvoiceTerms, List<SaleInvoiceProductSerialDetailViewModel> saleInvoiceProductSerialDetail, List<SISupportingDocumentViewModel> siDocuments)
        {
            ResponseOut responseOut = new ResponseOut();
            SaleInvoiceBL saleinvoiceBL = new SaleInvoiceBL();
            try
            {
                if (saleinvoiceViewModel != null)
                {
                    saleinvoiceViewModel.CreatedBy = ContextUser.UserId;
                    saleinvoiceViewModel.CompanyId = ContextUser.CompanyId;
                    saleinvoiceViewModel.FinYearId = Session[SessionKey.CurrentFinYear] != null ? ((FinYearViewModel)Session[SessionKey.CurrentFinYear]).FinYearId : DateTime.Now.Year;
                    responseOut = saleinvoiceBL.AddEditSaleInvoice(saleinvoiceViewModel, saleinvoiceProducts, saleinvoiceTaxes, saleinvoiceTerms, saleInvoiceProductSerialDetail, siDocuments);

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

        [ValidateRequest(true, UserInterfaceHelper.Add_Edit_SaleInvoice, (int)AccessMode.ViewAccess, (int)RequestMode.GetPost)]
        public ActionResult ListSaleInvoice()
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
        public PartialViewResult GetSaleInvoiceList(string saleinvoiceNo = "", string customerName = "", string refNo = "", string fromDate = "", string toDate = "",string invoiceType="",string approvalStatus="", int companyBranchId=0)
        {
            List<SaleInvoiceViewModel> saleinvoices = new List<SaleInvoiceViewModel>();
            SaleInvoiceBL saleinvoiceBL = new SaleInvoiceBL();
            try
            {
                saleinvoices = saleinvoiceBL.GetSaleInvoiceList(saleinvoiceNo, customerName, refNo, fromDate, toDate, ContextUser.CompanyId, invoiceType,"", approvalStatus,"", companyBranchId);
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return PartialView(saleinvoices);
        }

        [HttpGet]
        public JsonResult GetSaleInvoiceDetail(long saleinvoiceId)
        {
            SaleInvoiceBL saleinvoiceBL = new SaleInvoiceBL();
            SaleInvoiceViewModel saleinvoice = new SaleInvoiceViewModel();
            try
            {
                saleinvoice = saleinvoiceBL.GetSaleInvoiceDetail(saleinvoiceId);
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return Json(saleinvoice, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public PartialViewResult GetSaleInvoiceProductList(List<SaleInvoiceProductViewModel> saleinvoiceProducts, long saleinvoiceId)
        {
            SaleInvoiceBL siBL = new SaleInvoiceBL();
            try
            {
                if (saleinvoiceProducts == null)
                {
                    saleinvoiceProducts = siBL.GetSaleInvoiceProductList(saleinvoiceId);
                }

            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return PartialView(saleinvoiceProducts);
        }
        [HttpPost]
        public PartialViewResult GetSaleInvoiceTaxList(List<SaleInvoiceTaxViewModel> saleinvoiceTaxes, long saleinvoiceId)
        {
            SaleInvoiceBL saleinvoiceBL = new SaleInvoiceBL();
            try
            {
                if (saleinvoiceTaxes == null)
                {
                    saleinvoiceTaxes = saleinvoiceBL.GetSaleInvoiceTaxList(saleinvoiceId);
                }

            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return PartialView(saleinvoiceTaxes);
        }
        [HttpPost]
        public PartialViewResult GetSaleInvoiceTermList(List<SaleInvoiceTermViewModel> saleinvoiceTerms, long saleinvoiceId)
        {
            SaleInvoiceBL saleinvoiceBL = new SaleInvoiceBL();
            try
            {
                if (saleinvoiceTerms == null)
                {
                    saleinvoiceTerms = saleinvoiceBL.GetSaleInvoiceTermList(saleinvoiceId);
                }

            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return PartialView(saleinvoiceTerms);
        }
    
        [HttpPost]
        public PartialViewResult GetSaleInvoiceProductSerialDetailList(int saleinvoiceId = 0)
        {
            List<SaleInvoiceProductSerialDetailViewModel> saleinvoices = new List<SaleInvoiceProductSerialDetailViewModel>();
            SaleInvoiceBL saleinvoiceBL = new SaleInvoiceBL();
            try
            {
                saleinvoices = saleinvoiceBL.GetSaleInvoiceProductSerialDetailList(saleinvoiceId);
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return PartialView(saleinvoices);
        }

        [HttpGet]
        public JsonResult GetCustomerAutoCompleteList(string term)
        {
            CustomerBL customerBL = new CustomerBL();

            List<CustomerViewModel> customerList = new List<CustomerViewModel>();
            try
            {
                customerList = customerBL.GetCustomerAutoCompleteList(term, ContextUser.CompanyId);

            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return Json(customerList, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetTaxAutoCompleteList(string term)
        {
            TaxBL taxBL = new TaxBL();

            List<TaxViewModel> taxList = new List<TaxViewModel>();
            try
            {
                taxList = taxBL.GetTaxAutoCompleteList(term, "SALE", ContextUser.CompanyId);
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return Json(taxList, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetTermTemplateList()
        {
            TermTemplateBL termBL = new TermTemplateBL();
            List<TermTemplateViewModel> termList = new List<TermTemplateViewModel>();
            try
            {
                termList = termBL.GetTermTemplateList(ContextUser.CompanyId);
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return Json(termList, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetTermTemplateDetailList(int termTemplateId)
        {
            TermTemplateBL termBL = new TermTemplateBL();
            List<TermTemplateDetailViewModel> termList = new List<TermTemplateDetailViewModel>();
            try
            {
                termList = termBL.GetTermTemplateDetailList(termTemplateId);
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return Json(termList, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public PartialViewResult GetSaleOrderList(string soNo = "", string customerName = "", string refNo = "", string fromDate = "", string toDate = "",string approvalStatus="", string displayType = "")
        {
            List<SOViewModel> soList = new List<SOViewModel>();
            SOBL soBL = new SOBL();
            try
            {
                soList = soBL.GetSOList(soNo, customerName, refNo, fromDate, toDate, ContextUser.CompanyId, approvalStatus, displayType);
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return PartialView(soList);
        }
        public ActionResult Report(long siId, string reportType = "PDF",string reportOption="Original")
        {
            LocalReport lr = new LocalReport();
            SaleInvoiceBL siBL = new SaleInvoiceBL();
            string path = Path.Combine(Server.MapPath("~/RDLC"), "SaleInvoicePrint.rdlc");
            if (System.IO.File.Exists(path))
            {
                lr.ReportPath = path;
            }
            else
            {
                return View("Index");
            }

            DataTable dt = new DataTable();
            dt = siBL.GetSaleInvoiceDetailDataTable(siId);

            decimal totalInvoiceAmount = 0;
            totalInvoiceAmount = Convert.ToDecimal(dt.Rows[0]["TotalValue"].ToString());
            string totalWords = CommonHelper.changeToWords(totalInvoiceAmount.ToString());

            ReportDataSource rd = new ReportDataSource("DataSet1", dt);
            ReportDataSource rdProduct = new ReportDataSource("DataSetProduct", siBL.GetSaleInvoiceProductListDataTable(siId));
            ReportDataSource rdTax = new ReportDataSource("DataSetTax", siBL.GetSaleInvoiceTaxListDataTable(siId));
            ReportDataSource rdTerms = new ReportDataSource("DataSetTerms", siBL.GetSaleInvoiceTermListDataTable(siId));
            ReportDataSource rdSaleInvoiceProductSerialList = new ReportDataSource("DataSetSaleInvoiceProductSerialList", siBL.GetSaleInvoiceProductSerialDetailDataTable(siId));


            lr.DataSources.Add(rd);
            lr.DataSources.Add(rdProduct);
            lr.DataSources.Add(rdTax);
            lr.DataSources.Add(rdTerms);
            lr.DataSources.Add(rdSaleInvoiceProductSerialList);

            ReportParameter rp1 = new ReportParameter("AmountInWords", totalWords);
            ReportParameter rp2 = new ReportParameter("ReportOption", reportOption);
            lr.SetParameters(rp1);
            lr.SetParameters(rp2);

            string mimeType;
            string encoding;
            string fileNameExtension;



            string deviceInfo =

            "<DeviceInfo>" +
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
        public ActionResult SaleInvoiceMail(long siId, string reportType = "PDF", string reportOption = "Original")
        {
            ResponseOut responseOut = new ResponseOut();
            LocalReport lr = new LocalReport();
            SaleInvoiceBL siBL = new SaleInvoiceBL();
            EmailTemplateBL emailTemplateBL = new EmailTemplateBL();
            try
            { 


                string path = Path.Combine(Server.MapPath("~/RDLC"), "SaleInvoicePrint.rdlc");
                if (System.IO.File.Exists(path))
                {
                    lr.ReportPath = path;
                }
                else
                {
                    responseOut.message = "Report Path not Found!!!";
                    responseOut.status = ActionStatus.Fail;
                    return Json(responseOut, JsonRequestBehavior.AllowGet);
                }
                DataTable emaildt = new DataTable();
                emaildt = emailTemplateBL.GetEmailTemplateDetailByEmailType((int)MailSendingMode.SaleInvoice);

                DataTable dt = new DataTable();
                dt = siBL.GetSaleInvoiceDetailDataTable(siId);

                if (dt.Rows.Count > 0)
                {
                    StringBuilder mailBody = new StringBuilder(" ");
                    SendMail sendMail = new SendMail();
                    mailBody.Append("<html><head></head><body>");
                    //mailBody.Append("<img src='" + Convert.ToString(ConfigurationManager.AppSettings["Logo_Path"]) + "' alt='ICS Logo' />");
                    //mailBody.Append("<hr/><br/>");
                    mailBody.Append("<p style='font-family:Cambria;font-size:12px;margin: 0px 0px 0px 0px;'>Dear " + dt.Rows[0]["ContactPerson"].ToString() + " </p><br/>");
                    // mailBody.Append("<p style='font-family:Cambria;font-size:12px;margin: 0px 0px 0px 0px;'>Please find attached Invoice for your reference</p><br/>");
                    //  mailBody.Append("<p style='font-family:Cambria;font-size:12px;margin: 0px 0px 0px 0px;'>Regards,</p><br/>");
                    // mailBody.Append("<p style='font-family:Cambria;font-size:12px;margin: 0px 0px 0px 0px;'>Sale Team</p><br/>");
                   mailBody.Append(emaildt.Rows[0][4].ToString());
                    mailBody.Append("</body></html>");

                    decimal totalInvoiceAmount = 0;
                    totalInvoiceAmount = Convert.ToDecimal(dt.Rows[0]["TotalValue"].ToString());
                    string totalWords = CommonHelper.changeToWords(totalInvoiceAmount.ToString());



                    ReportDataSource rd = new ReportDataSource("DataSet1", dt);
                    ReportDataSource rdProduct = new ReportDataSource("DataSetProduct", siBL.GetSaleInvoiceProductListDataTable(siId));
                    ReportDataSource rdTax = new ReportDataSource("DataSetTax", siBL.GetSaleInvoiceTaxListDataTable(siId));
                    ReportDataSource rdTerms = new ReportDataSource("DataSetTerms", siBL.GetSaleInvoiceTermListDataTable(siId));
                    ReportDataSource rdSaleInvoiceProductSerialList = new ReportDataSource("DataSetSaleInvoiceProductSerialList", siBL.GetSaleInvoiceProductSerialDetailDataTable(siId));

                    lr.DataSources.Add(rd);
                    lr.DataSources.Add(rdProduct);
                    lr.DataSources.Add(rdTax);
                    lr.DataSources.Add(rdTerms);
                    lr.DataSources.Add(rdSaleInvoiceProductSerialList);

                    ReportParameter rp1 = new ReportParameter("AmountInWords", totalWords);
                    ReportParameter rp2 = new ReportParameter("ReportOption", reportOption);
                    lr.SetParameters(rp1);
                    lr.SetParameters(rp2);
                    string mimeType;
                    string encoding;
                    string fileNameExtension;



                    string deviceInfo =

                    "<DeviceInfo>" +
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

                 
                    UserEmailSettingBL userEmailSettingBL = new UserEmailSettingBL();
                    DataTable userEmailSetting = userEmailSettingBL.GetUserEmailSettingDetailDataTable(ContextUser.UserId);
                    bool sendMailStatus = false;
                    if (userEmailSetting.Rows.Count>0)
                    {
                        sendMailStatus = sendMail.SendEmail(userEmailSetting.Rows[0]["SmtpUser"].ToString(), dt.Rows[0]["Email"].ToString(), "Sale Invoice", mailBody.ToString(), renderedBytes, "SaleInvoice.pdf", userEmailSetting.Rows[0]["SmtpPass"].ToString(), userEmailSetting.Rows[0]["SmtpDisplayName"].ToString(), userEmailSetting.Rows[0]["SmtpServer"].ToString(),Convert.ToInt32(userEmailSetting.Rows[0]["SmtpPort"]),Convert.ToBoolean(userEmailSetting.Rows[0]["EnableSsl"]));
                    }
                    else
                    {
                        sendMailStatus = sendMail.SendEmail("", dt.Rows[0]["Email"].ToString(), "Sale Invoice", mailBody.ToString(), renderedBytes, "SaleInvoice.pdf");
                    }
                    //bool sendMailStatus = sendMail.SendEmail("", dt.Rows[0]["Email"].ToString(), "Invoice", mailBody.ToString(), renderedBytes, "Invoice.pdf");
                    if (sendMailStatus)
                    {
                        responseOut.message = "Mail Sent Successfully";
                        responseOut.status = ActionStatus.Success;


                    }
                    else
                    {
                        responseOut.message = "Problem in Sending Mail!!!";
                        responseOut.status = ActionStatus.Fail;

                    }
                }
                else
                {
                    responseOut.message = "Invoice Detail not found!!!";
                    responseOut.status = ActionStatus.Fail;

                }

            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return Json(responseOut, JsonRequestBehavior.AllowGet);
        }


        [ValidateRequest(true, UserInterfaceHelper.Add_Edit_SaleInvoice, (int)AccessMode.ViewAccess, (int)RequestMode.GetPost)]
        public ActionResult CancelSaleInvoice(int siId = 0, int accessMode = 3)
        {
            try
            {
                FinYearViewModel finYear = Session[SessionKey.CurrentFinYear] != null ? (FinYearViewModel)Session[SessionKey.CurrentFinYear] : new FinYearViewModel();

                ViewData["fromDate"] = finYear.StartDate;
                ViewData["toDate"] = finYear.EndDate;
                ViewData["currentDate"] = DateTime.Now.ToString("dd-MMM-yyyy");

                if (siId != 0)
                {
                    ViewData["siId"] = siId;
                    ViewData["accessMode"] = accessMode;

                }
                else
                {
                    ViewData["siId"] = 0;
                    ViewData["accessMode"] = 3;

                }

            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return View();
        }


        [HttpPost]
        [ValidateRequest(true, UserInterfaceHelper.Add_Edit_SaleInvoice, (int)AccessMode.AddAccess, (int)RequestMode.Ajax)]
        public ActionResult CancelSaleInvoice(long invoiceId,string cancelReason)
        {
            ResponseOut responseOut = new ResponseOut();
            SaleInvoiceBL saleinvoiceBL = new SaleInvoiceBL();
            SaleInvoiceViewModel saleinvoiceViewModel = new SaleInvoiceViewModel();
            try
            {
                if (saleinvoiceViewModel != null)
                {
                    saleinvoiceViewModel.InvoiceId = invoiceId;
                    saleinvoiceViewModel.CancelReason = cancelReason;
                    saleinvoiceViewModel.CreatedBy = ContextUser.UserId;
                    responseOut = saleinvoiceBL.CancelSaleInvoice(saleinvoiceViewModel);
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
                        var path = Path.Combine(Server.MapPath("~/Images/SIDocument"), newFileName);
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


        

        [HttpPost]
        public PartialViewResult SaveProductSerialDetail()
        {
            UploadUtilityBL utilityBL = new UploadUtilityBL();
            SaleInvoiceBL saleInvoiceBL = new SaleInvoiceBL();
            List<SaleInvoiceProductSerialDetailViewModel> productSerialDetail = new List<SaleInvoiceProductSerialDetailViewModel>();
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
                        string extension = System.IO.Path.GetExtension(fname).ToLower();
                        string query = null;
                        string connString = "";
                        string[] validFileTypes = { ".xls", ".xlsx", ".csv" };

                        string path1 = string.Format("{0}/{1}", Server.MapPath("~/Content/Uploads"), fname);
                        if (!Directory.Exists(path1))
                        {
                            Directory.CreateDirectory(Server.MapPath("~/Content/Uploads"));
                        }
                        if (validFileTypes.Contains(extension))
                        {
                            DataTable dt = new DataTable();
                            if (System.IO.File.Exists(path1))
                            { System.IO.File.Delete(path1); }
                            file.SaveAs(path1);
                            if (extension == ".csv")
                            {
                                dt = CommonHelper.ConvertCSVtoDataTable(path1);
                                ViewBag.Data = dt;
                            }
                            //Connection String to Excel Workbook  
                            else if (extension.Trim() == ".xls")
                            {
                                connString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + path1 + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\"";
                                dt = CommonHelper.ConvertXSLXtoDataTable(path1, connString);
                                ViewBag.Data = dt;
                            }
                            else if (extension.Trim() == ".xlsx")
                            {
                                connString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path1 + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
                                dt = CommonHelper.ConvertXSLXtoDataTable(path1, connString);
                                ViewBag.Data = dt;
                            }

                            // Code to Update ID based on Name of field
                            StringBuilder strErrMsg = new StringBuilder(" ");

                            if (dt.Rows.Count > 0)
                            {

                                Int32 productId = 0;
                                int rowCounter = 1;
                                SaleInvoiceViewModel saleInvoiceViewModel;
                                SaleInvoiceProductSerialDetailViewModel saleInvoiceProductSerialDetailViewModel;
                                dt.Columns.Add("UploadStatus", typeof(bool));
                                bool rowVerifyStatus = true;
                                //Random rnd = new Random(50000);
                                foreach (DataRow dr in dt.Rows)
                                {
                                    saleInvoiceViewModel = new SaleInvoiceViewModel();
                                    saleInvoiceProductSerialDetailViewModel = new SaleInvoiceProductSerialDetailViewModel();

                                    //code to validate data in excel//
                                    if (string.IsNullOrEmpty(Convert.ToString(dr["ProductName"])))
                                    {
                                        strErrMsg.Append("Product Name Column has not proper data in Row #" + rowCounter.ToString() + Environment.NewLine);
                                        rowVerifyStatus = false;
                                    }
                                    else if (string.IsNullOrEmpty(Convert.ToString(dr["RefSerial1"])))
                                    {
                                        strErrMsg.Append("RefSerial Column has not proper data in Row #" + rowCounter.ToString() + Environment.NewLine);
                                        rowVerifyStatus = false;
                                    }

                                    //end of code to validate data in excel//

                                    //code to get Id from Name data in excel//
                                    if (!string.IsNullOrEmpty(Convert.ToString(dr["ProductName"])))
                                    {
                                        productId = Convert.ToInt32(utilityBL.GetProductIdByProductName(Convert.ToString(dr["ProductName"])));
                                        if (productId == 0)
                                        {
                                            strErrMsg.Append("Invalid Product Name data in Row #" + rowCounter.ToString() + Environment.NewLine);
                                            rowVerifyStatus = false;
                                        }
                                    }
                                    //End of code to get Id from Name data in excel//


                                    if (rowVerifyStatus == true)
                                    {
                                        productSerialDetail.Add(new SaleInvoiceProductSerialDetailViewModel
                                        {
                                            ProductId = productId,
                                            ProductName = Convert.ToString(dr["ProductName"]),
                                            RefSerial1 = Convert.ToString(dr["RefSerial1"]),
                                            RefSerial2 = Convert.ToString(dr["RefSerial2"]),
                                            RefSerial3 = Convert.ToString(dr["RefSerial3"]),
                                            RefSerial4 = Convert.ToString(dr["RefSerial4"])
                                        });
                                        dr["UploadStatus"] = true;
                                    }
                                    else
                                    {
                                        dr["UploadStatus"] = false;
                                    
                                    }
                                    rowCounter += 1;
                                    
                                }
                                dt.AcceptChanges();
                            }
                            else
                            {
                                strErrMsg.Append("Import not found");
                               // productSerialDetail = null;
                            }

                            ViewBag.Error = strErrMsg.ToString();
                            // End of Code to Update ID based on Name of field
                           
                        }
                        else
                        {
                            ViewBag.Error = "Please Upload Files in .xls, .xlsx or .csv format";
                      
                        }
                       
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
            return PartialView("GetSaleInvoiceProductSerialDetailList", productSerialDetail);
        }
        public FileResult DocumentDownload(string fileName)
        {
            var path = Path.Combine(Server.MapPath("~/Images/SIDocument"), fileName);
            byte[] fileBytes = System.IO.File.ReadAllBytes(path);

            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
        }

        [HttpPost]
        public PartialViewResult GetSISupportingDocumentList(List<SISupportingDocumentViewModel> siDocuments, long siId)
        {            
            SaleInvoiceBL saleInvoiceBL = new SaleInvoiceBL();
            try
            {
                if (siDocuments == null)
                {
                    siDocuments = saleInvoiceBL.GetSISupportingDocumentList(siId);
                }

            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return PartialView(siDocuments);
        }
        [ValidateRequest(true, UserInterfaceHelper.Add_GETGSTR1, (int)AccessMode.ViewAccess, (int)RequestMode.GetPost)]
        public ActionResult ListGSTR1B2B()
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
        public PartialViewResult GetGSTR1List(string fromDate = "", string toDate = "")
        {
            List<GSTR1ViewModel> gSTR1invoices = new List<GSTR1ViewModel>();
            SaleInvoiceBL saleinvoiceBL = new SaleInvoiceBL();
            try
            {
                gSTR1invoices = saleinvoiceBL.GetGSTR1B2BList(fromDate, toDate, ContextUser.CompanyId);
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return PartialView(gSTR1invoices);
        }

        public ActionResult ReportGSTR1B2B(string fromDate, string toDate, string reportType = "PDF")
        {
            LocalReport lr = new LocalReport();
            SaleInvoiceBL saleInvoiceBL = new SaleInvoiceBL();
            string path = Path.Combine(Server.MapPath("~/RDLC"), "GSTR1_B2BPrint.rdlc");
            if (System.IO.File.Exists(path))
            {
                lr.ReportPath = path;
            }
            else
            {
                return View("Index");
            }
            ReportDataSource rd = new ReportDataSource("GSTDataSet", saleInvoiceBL.GetGSTR1B2BDataTable(fromDate, toDate, ContextUser.CompanyId));
            lr.DataSources.Add(rd);

            string mimeType;
            string encoding;
            string fileNameExtension;

            string deviceInfo =

             "<DeviceInfo>" +
            "  <OutputFormat>" + reportType + "</OutputFormat>" +
            "  <PageWidth>11.8in</PageWidth>" +
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

        public ActionResult ReportGSTR1B2CL(string fromDate, string toDate, string reportType = "PDF")
        {
            LocalReport lr = new LocalReport();
            SaleInvoiceBL saleInvoiceBL = new SaleInvoiceBL();
            string path = Path.Combine(Server.MapPath("~/RDLC"), "GSTR1_B2CLPrint.rdlc");
            if (System.IO.File.Exists(path))
            {
                lr.ReportPath = path;
            }
            else
            {
                return View("Index");
            }
            ReportDataSource rd = new ReportDataSource("GSTDataSet", saleInvoiceBL.GetGSTR1B2CLDataTable(fromDate, toDate, ContextUser.CompanyId));
            lr.DataSources.Add(rd);

            string mimeType;
            string encoding;
            string fileNameExtension;

            string deviceInfo =

             "<DeviceInfo>" +
            "  <OutputFormat>" + reportType + "</OutputFormat>" +
            "  <PageWidth>9.8in</PageWidth>" +
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

        public ActionResult ReportGSTR1B2CS(string fromDate, string toDate, string reportType = "PDF")
        {
            LocalReport lr = new LocalReport();
            SaleInvoiceBL saleInvoiceBL = new SaleInvoiceBL();
            string path = Path.Combine(Server.MapPath("~/RDLC"), "GSTR1_B2CSPrint.rdlc");
            if (System.IO.File.Exists(path))
            {
                lr.ReportPath = path;
            }
            else
            {
                return View("Index");
            }
            ReportDataSource rd = new ReportDataSource("GSTDataSet", saleInvoiceBL.GetGSTR1B2CSDataTable(fromDate, toDate, ContextUser.CompanyId));
            lr.DataSources.Add(rd);

            string mimeType;
            string encoding;
            string fileNameExtension;

            string deviceInfo =

             "<DeviceInfo>" +
            "  <OutputFormat>" + reportType + "</OutputFormat>" +
            "  <PageWidth>9.8in</PageWidth>" +
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

        public ActionResult ReportGSTR1CDNR(string fromDate, string toDate, string reportType = "PDF")
        {
            LocalReport lr = new LocalReport();
            SaleInvoiceBL saleInvoiceBL = new SaleInvoiceBL();
            string path = Path.Combine(Server.MapPath("~/RDLC"), "GSTR1_CDNRPrint.rdlc");
            if (System.IO.File.Exists(path))
            {
                lr.ReportPath = path;
            }
            else
            {
                return View("Index");
            }
            ReportDataSource rd = new ReportDataSource("GSTDataSet", saleInvoiceBL.GetGSTR1CDNRDataTable(fromDate, toDate, ContextUser.CompanyId));
            lr.DataSources.Add(rd);

            string mimeType;
            string encoding;
            string fileNameExtension;

            string deviceInfo =

             "<DeviceInfo>" +
            "  <OutputFormat>" + reportType + "</OutputFormat>" +
            "  <PageWidth>18in</PageWidth>" +
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

        public ActionResult ReportGSTR1CDNUR(string fromDate, string toDate, string reportType = "PDF")
        {
            LocalReport lr = new LocalReport();
            SaleInvoiceBL saleInvoiceBL = new SaleInvoiceBL();
            string path = Path.Combine(Server.MapPath("~/RDLC"), "GSTR1_CDNURPrint.rdlc");
            if (System.IO.File.Exists(path))
            {
                lr.ReportPath = path;
            }
            else
            {
                return View("Index");
            }
            ReportDataSource rd = new ReportDataSource("GSTDataSet", saleInvoiceBL.GetGSTR1CDNRDataTable(fromDate, toDate, ContextUser.CompanyId));
            lr.DataSources.Add(rd);

            string mimeType;
            string encoding;
            string fileNameExtension;

            string deviceInfo =

             "<DeviceInfo>" +
            "  <OutputFormat>" + reportType + "</OutputFormat>" +
            "  <PageWidth>18in</PageWidth>" +
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
        #endregion
    }
}
