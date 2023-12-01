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
    public class VendorController : BaseController
    {
        //
        // GET: /Customer/
        #region Vendor
        [ValidateRequest(true, UserInterfaceHelper.Add_Edit_Vendor_SALE, (int)AccessMode.ViewAccess, (int)RequestMode.GetPost)]
        public ActionResult AddEditVendor(int vendorId = 0, int accessMode = 3)
        {
            try
            {
                if (vendorId != 0)
                {
                    ViewData["vendorId"] = vendorId;
                    ViewData["accessMode"] = accessMode;
                }
                else
                {
                    ViewData["vendorId"] = 0;
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
        [ValidateRequest(true, UserInterfaceHelper.Add_Edit_Vendor_SALE, (int)AccessMode.AddAccess, (int)RequestMode.Ajax)]
        public ActionResult AddEditVendor(VendorViewModel vendorViewModel, List<VendorProductViewModel> vendorProducts)
        {
            ResponseOut responseOut = new ResponseOut();
            VendorBL vendorBL = new VendorBL();
            try
            {
                if (vendorViewModel != null)
                {
                    vendorViewModel.CreatedBy = ContextUser.UserId;
                    vendorViewModel.CompanyId = ContextUser.CompanyId;
                    responseOut = vendorBL.AddEditVendor(vendorViewModel, vendorProducts);
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




        [HttpPost]
        public PartialViewResult GetVendorProductList(List<VendorProductViewModel> vendorProducts, int vendorId)
        {           
            VendorBL vendorBL = new VendorBL();
            try
            {
                if (vendorProducts == null)
                {
                    vendorProducts = vendorBL.GetVendorProductList(vendorId);
                    
                }

            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return PartialView(vendorProducts);
        }


        [HttpGet]
        [ValidateRequest(true, UserInterfaceHelper.Add_Edit_Vendor_SALE, (int)AccessMode.ViewAccess, (int)RequestMode.GetPost)]
        public ActionResult ListVendor()
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
        public PartialViewResult GetVendorList(string vendorName = "", string vendorCode = "", string mobileNo = "",  string vendorStatus = "")
        {
            List<VendorViewModel> vendors = new List<VendorViewModel>();
            VendorBL vendorBL = new VendorBL();
            try
            {
                vendors = vendorBL.GetVendorList(vendorName, vendorCode, mobileNo, ContextUser.CompanyId, vendorStatus);
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return PartialView(vendors);
        }

        [HttpGet]
        public JsonResult GetVendorDetail(int vendorId)
        {
            VendorBL vendorBL = new VendorBL();
            VendorViewModel vendor = new VendorViewModel();
            try
            {
                vendor = vendorBL.GetVendorDetail(vendorId);
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return Json(vendor, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateRequest(true, UserInterfaceHelper.Add_Edit_Vendor_SALE, (int)AccessMode.AddAccess, (int)RequestMode.Ajax)]
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
        //Vendor Details By Vendor Id
        [HttpPost]
        public JsonResult GetVendorDetailsById(int vendorId)
        {
            VendorBL vendorBL = new VendorBL();
            List<VendorViewModel> vendorList = new List<VendorViewModel>();
            try
            {
                vendorList = vendorBL.GetVendorDetailsById(vendorId, ContextUser.CompanyId);
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return Json(vendorList, JsonRequestBehavior.AllowGet);
        }


        #endregion

    }
}
