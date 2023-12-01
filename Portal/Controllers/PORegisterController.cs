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


namespace Portal.Controllers
{
    [CheckSessionBeforeControllerExecuteAttribute(Order = 1)]
    public class PORegisterController : BaseController
    {
        //
        // GET: /PORegister/ 


        [ValidateRequest(true, UserInterfaceHelper.Add_PORegister, (int)AccessMode.ViewAccess, (int)RequestMode.GetPost)]
        public ActionResult ListPORegister()
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
        public PartialViewResult GetPORegisterList( string vendorId = "", int stateId=0, string fromDate = "", string toDate = "", int createdBy = 0, string sortBy = "", string sortOrder = "")
        {
            List<POViewModel> pos = new List<POViewModel>();

            PORegisterBL poregisterBL = new PORegisterBL();
            try
            {
                pos = poregisterBL.GetPORegisterList( vendorId, stateId, fromDate, toDate, ContextUser.CompanyId, createdBy, sortBy, sortOrder);
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return PartialView(pos);
        }



         

    }
}
