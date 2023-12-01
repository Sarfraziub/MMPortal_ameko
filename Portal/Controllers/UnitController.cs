using Portal.Common;
using Portal.Core;
using Portal.Core.Unit;
using Portal.Core.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace Portal.Controllers
{
    [CheckSessionBeforeControllerExecuteAttribute(Order = 1)]

    public class UnitController : BaseController
    {
        //
        // GET: /Unit/

        [ValidateRequest(true, UserInterfaceHelper.Add_Edit_Unit, (int)AccessMode.ViewAccess, (int)RequestMode.GetPost)]
        public ActionResult ListUnit()
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
        public PartialViewResult GetUnitList(string unitName = "", string unitShortName = "", int parentId = 0)
        {
            List<UnitViewModel> units = new List<UnitViewModel>();
            UnitBL unitBL = new UnitBL();
            try
            {
                units = unitBL.GetUnitList(unitName, unitShortName, parentId);
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return PartialView(units);
        }



        [ValidateRequest(true, UserInterfaceHelper.Add_Edit_Unit, (int)AccessMode.ViewAccess, (int)RequestMode.GetPost)]
        public ActionResult AddEditUnit(int unitId = 0, int accessMode = 3)
        {

            try
            {
                if (unitId != 0)
                {
                    ViewData["unitId"] = unitId;
                    ViewData["accessMode"] = accessMode;
                }
                else
                {
                    ViewData["unitId"] = 0;
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
        [ValidateRequest(true, UserInterfaceHelper.Add_Edit_Unit, (int)AccessMode.AddAccess, (int)RequestMode.Ajax)]
        public ActionResult AddEditUnit(UnitViewModel unitViewModel)
        {
            ResponseOut responseOut = new ResponseOut();
            UnitBL unitBL = new UnitBL();
            try
            {
                if (unitViewModel != null)
                {
                    UnitViewModel unitViewModel2sd = new UnitViewModel();
                    responseOut = unitBL.AddEditUnit(unitViewModel);
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
        public JsonResult GetUnitDetail(int unitId)
        {

            UnitBL unitBL = new UnitBL();
            UnitViewModel unit = new UnitViewModel();
            try
            {

                unit = unitBL.GetUnitDetail(unitId);
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return Json(unit, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult GetBaseUnitList() 
        {

            UnitBL unitBL = new UnitBL();
            List<UnitViewModel> unit = new List<UnitViewModel>();
            try
            {
                unit = unitBL.GetBaseUnitList();
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return Json(unit, JsonRequestBehavior.AllowGet);
        }

    }
}
