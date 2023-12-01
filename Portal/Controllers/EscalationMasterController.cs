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
    public class EscalationMasterController : BaseController
    {
        //
        // GET: /Company/
        [ValidateRequest(true, UserInterfaceHelper.Add_Edit_EscalationMaster, (int)AccessMode.ViewAccess, (int)RequestMode.GetPost)]
        public ActionResult AddEditEscalationMaster(int EscalationId = 0, int accessMode = 3)
        {

            try
            {
                if (EscalationId != 0)
                {
                    ViewData["escalationMasterId"] = EscalationId;
                    ViewData["accessMode"] = accessMode;
                }
                else
                {
                    ViewData["escalationMasterId"] = 0;
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
        [ValidateRequest(true, UserInterfaceHelper.Add_Edit_EscalationMaster, (int)AccessMode.AddAccess, (int)RequestMode.Ajax)]
        public ActionResult AddEditEscalationMaster(EscalationMasterViewModel escalationMasterViewModel)
        {
            ResponseOut responseOut = new ResponseOut();
            EscalationMasterBL escalationMasterBL = new EscalationMasterBL();
            try
            {
                if (escalationMasterViewModel != null)
                {
                    responseOut = escalationMasterBL.AddEditEscalationMaster(escalationMasterViewModel);
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


        [ValidateRequest(true, UserInterfaceHelper.Add_Edit_CustomerType_CP, (int)AccessMode.ViewAccess, (int)RequestMode.GetPost)]
        public ActionResult ListEscalationMaster()
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
        public PartialViewResult GetEscalationMasterList(string processType = "")
        {
            List<EscalationMasterViewModel> escalationMasters = new List<EscalationMasterViewModel>();
            EscalationMasterBL escalationMasterBL = new EscalationMasterBL();
            try
            {
                escalationMasters = escalationMasterBL.GetEscalationMasterList(processType);
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return PartialView(escalationMasters);
        }


        [HttpGet]
        public JsonResult GetEscalationMasterDetail(int EscalationId)
        {
            EscalationMasterBL escalationMasterBL = new EscalationMasterBL();
            EscalationMasterViewModel escalationMaster = new EscalationMasterViewModel();
            try
            {
                escalationMaster = escalationMasterBL.GetEscalationMasterDetail(EscalationId);
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return Json(escalationMaster, JsonRequestBehavior.AllowGet);
        }

    }
}
