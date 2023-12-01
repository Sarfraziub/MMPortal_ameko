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
namespace Portal.Controllers
{
    [CheckSessionBeforeControllerExecuteAttribute(Order = 1)]
    public class PurchaseSummaryController : BaseController
    {
        #region SaleInvoiceSummary

        //
        // GET: /SaleInvoiceRegister/ 


        [ValidateRequest(true, UserInterfaceHelper.Add_PurchaseSummary, (int)AccessMode.ViewAccess, (int)RequestMode.GetPost)]
        public ActionResult ListPurchaseSummary()
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
        public PartialViewResult GetPurchaseSummaryList(int vendorId, int userId, int stateId,  string fromDate, string toDate)
        {
            List<PurchaseSummaryRegisterViewModel> purchaseInvoices = new List<PurchaseSummaryRegisterViewModel>();
            PurchaseInvoiceRegisterBL purchaseInvoiceRegisterBL = new PurchaseInvoiceRegisterBL();
            try
            {

                purchaseInvoices = purchaseInvoiceRegisterBL.GetPurchaseSummaryRegister(vendorId,userId, stateId ,ContextUser.CompanyId ,Convert.ToDateTime(fromDate),Convert.ToDateTime(toDate));
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return PartialView(purchaseInvoices);
        }

        #endregion
    }
}
