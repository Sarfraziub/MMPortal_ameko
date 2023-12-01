using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Portal.Core;
using Portal.Core.ViewModel;
using Portal.Common;
using System.Reflection;

namespace Portal.Controllers
{
    [CheckSessionBeforeControllerExecuteAttribute(Order = 1)]
    public class SiteProductOpeningStockController : BaseController
    {
        //
        // GET: /ProductOpeningStock/

        [ValidateRequest(true, UserInterfaceHelper.Add_Edit_CustomerSiteProduct_Opening_Stock, (int)AccessMode.ViewAccess, (int)RequestMode.GetPost)]
        public ActionResult AddEditSiteProductOpening(int siteOpeningTrnId = 0, int accessMode = 3)
        {

            try
            {
                if (siteOpeningTrnId != 0)
                {
                    ViewData["siteOpeningTrnId"] = siteOpeningTrnId;
                    ViewData["accessMode"] = accessMode;
                }
                else
                {
                    ViewData["siteOpeningTrnId"] = 0;
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
        [ValidateRequest(true, UserInterfaceHelper.Add_Edit_CustomerSiteProduct_Opening_Stock, (int)AccessMode.AddAccess, (int)RequestMode.Ajax)]
        public ActionResult AddEditSiteProductOpening(SiteProductOpeningViewModel siteProductOpeningViewModel)
        {
            ResponseOut responseOut = new ResponseOut();
            SiteProductOpeningBL siteProductOpeningBL = new SiteProductOpeningBL();
            try
            {
                if (siteProductOpeningViewModel != null)
                {
                    siteProductOpeningViewModel.CompanyId = ContextUser.CompanyId;
                    siteProductOpeningViewModel.CreatedBy = ContextUser.UserId;
                    responseOut = siteProductOpeningBL.AddEditSiteProductOpening(siteProductOpeningViewModel);
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

        [ValidateRequest(true, UserInterfaceHelper.Add_Edit_CustomerSiteProduct_Opening_Stock, (int)AccessMode.ViewAccess, (int)RequestMode.GetPost)]
        public ActionResult ListSiteProductOpening()
        {
            try
            {

            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return View();
        }

        [HttpGet]
        public PartialViewResult GetSiteProductOpeningList(string productName, int finYearId, int companyBranchId)
        {
            List<SiteProductOpeningViewModel> productOpenings = new List<SiteProductOpeningViewModel>();
            SiteProductOpeningBL siteProductOpeningBL = new SiteProductOpeningBL();
            try
            {

                productOpenings = siteProductOpeningBL.GetSiteProductOpeningList(productName, ContextUser.CompanyId, finYearId, companyBranchId);
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return PartialView(productOpenings);
        }

        [HttpGet]
        public JsonResult GetSiteProductOpeningDetail(long siteOpeningTrnId)
        {
            SiteProductOpeningBL productOpeningBL = new SiteProductOpeningBL();
            SiteProductOpeningViewModel productOpening = new SiteProductOpeningViewModel();
            try
            {
                productOpening = productOpeningBL.GetSiteProductOpeningDetail(siteOpeningTrnId);
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return Json(productOpening, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCompanyBranchList()
        {
            CompanyBL companyBL = new CompanyBL();
          
            List<CompanyBranchViewModel> companyViewModellist = new List<CompanyBranchViewModel>();
            try
            {
                companyViewModellist = companyBL.GetCompanyBranchList(ContextUser.CompanyId);
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return Json(companyViewModellist, JsonRequestBehavior.AllowGet);
        }

    }
}
