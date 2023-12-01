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
    public class FabricationController : BaseController
    {
        #region Fabrication

        [ValidateRequest(true, UserInterfaceHelper.Add_Edit_Fabrication, (int)AccessMode.ViewAccess, (int)RequestMode.GetPost)]
        public ActionResult AddEditFabrication(int fabricationId = 0, int accessMode = 3)
        {
            try
            {
                FinYearViewModel finYear = Session[SessionKey.CurrentFinYear] != null ? (FinYearViewModel)Session[SessionKey.CurrentFinYear] : new FinYearViewModel();
                ViewData["currentDate"] = DateTime.Now.ToString("dd-MMM-yyyy");
                ViewData["fromDate"] = finYear.StartDate;
                ViewData["toDate"] = finYear.EndDate;

                if (fabricationId != 0)
                {
                    ViewData["fabricationId"] = fabricationId;
                    ViewData["accessMode"] = accessMode;

                }
                else
                {
                    ViewData["fabricationId"] = 0;
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
        [ValidateRequest(true, UserInterfaceHelper.Add_Edit_Fabrication, (int)AccessMode.AddAccess, (int)RequestMode.Ajax)]
        public ActionResult AddEditFabrication(FabricationViewModel fabricationViewModel, List<FabricationProductViewModel> fabricationProducts)
        {
            ResponseOut responseOut = new ResponseOut();
          
            FabricationBL fabricationBL = new FabricationBL();
            try
            {                            
                if (fabricationViewModel != null)
                {
                    fabricationViewModel.CreatedBy = ContextUser.UserId;
                    fabricationViewModel.CompanyId = ContextUser.CompanyId;
                    responseOut = fabricationBL.AddEditFabrication(fabricationViewModel, fabricationProducts);

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

        [ValidateRequest(true, UserInterfaceHelper.Add_Edit_Fabrication, (int)AccessMode.ViewAccess, (int)RequestMode.GetPost)]
        public ActionResult ListFabrication()
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
        public PartialViewResult GetFabricationList(string fabricationNo = "", string workOrderNo = "", int companyBranchId = 0, string fromDate = "", string toDate = "",string fabricationStatus="")
        {
            List<FabricationViewModel> fabrications = new List<FabricationViewModel>();
            FabricationBL fabricationBL = new FabricationBL();
            try
            {
                fabrications = fabricationBL.GetFabricationList(fabricationNo,workOrderNo, companyBranchId, fromDate, toDate, ContextUser.CompanyId, fabricationStatus);
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return PartialView(fabrications);
        }

        [HttpGet]
        public JsonResult GetFabricationDetail(long fabricationId)
        {
            FabricationBL fabricationBL = new FabricationBL();
            FabricationViewModel fabricationViewModel = new FabricationViewModel();
            try
            {
                fabricationViewModel = fabricationBL.GetFabricationDetail(fabricationId);
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return Json(fabricationViewModel, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public PartialViewResult GetFabricationProductList(List<FabricationProductViewModel> fabricationProducts, long fabricationId)
        {          
            FabricationBL fabricationBL = new FabricationBL();
            try
            {
                if (fabricationProducts == null)
                {
                    fabricationProducts = fabricationBL.GetFabricationProductList(fabricationId);
                }

            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return PartialView(fabricationProducts);
        }

        [HttpGet]
        public PartialViewResult GetFabricationWorkOrderList(string workOrderNo, int companyBranchId, string fromDate, string toDate, string displayType = "", string approvalStatus = "")
        {
            List<WorkOrderViewModel> workOrders = new List<WorkOrderViewModel>();
            StoreRequisitionBL storeRequisitionBL = new StoreRequisitionBL();
            try
            {
                workOrders = storeRequisitionBL.GetRequisitionWorkOrderList(workOrderNo, companyBranchId, fromDate, toDate, ContextUser.CompanyId, displayType, approvalStatus);
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return PartialView(workOrders);
        }

        [HttpPost]
        public PartialViewResult GetWorkOrderProductList(List<WorkOrderProductViewModel> fabricationProducts, long workOrderId)
        {
            WorkOrderBL workOrderBL = new WorkOrderBL();         
            try
            {
                if (fabricationProducts == null)
                {
                    fabricationProducts = workOrderBL.GetWorkOrderProductList(workOrderId);
                }

            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return PartialView(fabricationProducts);
        }
        public JsonResult GetBranchLocationList(int companyBranchID)
        {
            LocationBL locationBL = new LocationBL();
            List<LocationViewModel> locationViewModel = new List<LocationViewModel>();
            try
            {
                locationViewModel = locationBL.GetFromLocationList(companyBranchID);

            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return Json(locationViewModel, JsonRequestBehavior.AllowGet);
        }
        #endregion

    }
}
