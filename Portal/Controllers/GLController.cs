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
    public class GLController : BaseController
    {
        //
        // GET: /GL/
        [ValidateRequest(true, UserInterfaceHelper.AddEdit_GL, (int)AccessMode.ViewAccess, (int)RequestMode.GetPost)]
        public ActionResult AddEditGL(int GLId = 0, int accessMode = 3)
        {
            try
            {
                if (GLId != 0)
                {
                    ViewData["GLId"] = GLId;
                    ViewData["accessMode"] = accessMode;
                }
                else
                {
                    ViewData["GLId"] = 0;
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
        [ValidateRequest(true, UserInterfaceHelper.AddEdit_GL, (int)AccessMode.AddAccess, (int)RequestMode.Ajax)]
        public ActionResult AddEditGL(GLViewModel glViewModel,GLDetailViewModel glDetailViewModel)
        {
            ResponseOut responseOut = new ResponseOut();
            GLBL glBL = new GLBL();
            try
            {
                if (glViewModel != null && glDetailViewModel!=null)
                {
                    //FinYearViewModel finYear = Session[SessionKey.CurrentFinYear] != null ? (FinYearViewModel)Session[SessionKey.CurrentFinYear] : new FinYearViewModel();
                    //ViewData["fromDate"] = finYear.StartDate;
                    //ViewData["toDate"] = finYear.EndDate;
                    //ViewData["currentDate"] = DateTime.Now.ToString("dd-MMM-yyyy");

                    glDetailViewModel.FinYearId = Session[SessionKey.CurrentFinYear] != null ? ((FinYearViewModel)Session[SessionKey.CurrentFinYear]).FinYearId : DateTime.Now.Year;
                    glViewModel.CreatedBy = ContextUser.UserId;
                    glViewModel.CompanyId = ContextUser.CompanyId;
                    responseOut = glBL.AddEditGL(glViewModel,glDetailViewModel);
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
        [HttpGet]

        [ValidateRequest(true, UserInterfaceHelper.AddEdit_GL, (int)AccessMode.ViewAccess, (int)RequestMode.GetPost)]
        public ActionResult ListGL()
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
        public PartialViewResult GetGLList(string GLHead = "", string GLCode = "", string GLType = "", int GLMainGroupId = 0, int GLSubGroupId = 0, int SLTypeId = 0)
        {
            List<GLViewModel> gls = new List<GLViewModel>();
            GLBL glBL = new GLBL();

            try
            {
                int FinYearId = Session[SessionKey.CurrentFinYear] != null ? ((FinYearViewModel)Session[SessionKey.CurrentFinYear]).FinYearId : DateTime.Now.Year;
                gls = glBL.GetGLList(GLHead ,GLCode, GLType, GLMainGroupId, GLSubGroupId, SLTypeId, ContextUser.CompanyId, FinYearId);
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return PartialView(gls);
        }
        public JsonResult GetGLDetail(int GLId)
        {
            GLBL glBL = new GLBL();
            GLViewModel gLViewModel = new GLViewModel();
            try
            {
                int finYearId = Session[SessionKey.CurrentFinYear] != null ? ((FinYearViewModel)Session[SessionKey.CurrentFinYear]).FinYearId : DateTime.Now.Year;

                gLViewModel = glBL.GetGLDetail(GLId, finYearId);
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return Json(gLViewModel, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetGLSubGroupList(int MainGroupId)
        {
            GLBL gLBL = new GLBL();
            List<GLSubGroupViewModel> GLSubGroupList = new List<GLSubGroupViewModel>();
            try
            {
                GLSubGroupList = gLBL.GetGLSubGroupList(MainGroupId);
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return Json(GLSubGroupList, JsonRequestBehavior.AllowGet);
        }

     


    }
}
