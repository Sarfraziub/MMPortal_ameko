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
    public class MasterDashboardController : BaseController
    {
        //
        // GET: /ModuleDashboard/

        public ActionResult MDDashboard()
        {
            try
            {
                UserBL userBL = new UserBL();
                SOBL SO = new SOBL();
                InventoryDashboardBL inventoryDashboardBL = new InventoryDashboardBL();
                FinYearViewModel currentFinYear = (FinYearViewModel)Session[SessionKey.CurrentFinYear];
                ViewData["currentFinyearId"] = currentFinYear.FinYearId;
                ViewData["TotalUsersCount"] = userBL.GetTotalUsersCount(ContextUser.CompanyId);
                ViewData["totalProductCount"] = inventoryDashboardBL.GetTotalProductCount(ContextUser.CompanyId);
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return View();
        }
        public ActionResult AdminDashboard()
        {
            try
            {
                UserBL userBL = new UserBL();
                RoleBL roleBL = new RoleBL();
                ViewData["TodayUsersCount"] = userBL.GetTodayUsersCount(ContextUser.CompanyId);
                ViewData["TotalUsersCount"] = userBL.GetTotalUsersCount(ContextUser.CompanyId);
                ViewData["TotalRolesCount"] = roleBL.GetTotalRolesCount(ContextUser.CompanyId);
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return View();
        }
        public ActionResult CRMDashboard()
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
        public ActionResult CRMTeamDashboard()
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
        public ActionResult SaleDashboard()
        {
            try
            {
                CustomerBL customer = new CustomerBL();
                SOBL SO = new SOBL();
                FinYearViewModel currentFinYear = (FinYearViewModel)Session[SessionKey.CurrentFinYear];
                ViewData["currentFinyearId"] = currentFinYear.FinYearId;
                CustomerCountViewModel customerCount = customer.GetNewCustomerCount(ContextUser.CompanyId);
                ViewData["New_Customer_Count"] = customerCount.NewCustomer;
                customerCount = customer.GetTotalCustomerCount(ContextUser.CompanyId);
                ViewData["Total_Customer_Count"] = customerCount.TotalCustomer;
                SOCountViewModel SOcount = SO.GetSOCount(ContextUser.CompanyId, currentFinYear.FinYearId);
                ViewData["SO_Count"] = SOcount.sOTotalCount;
                // SICountViewModel siCount = SO.GetSITotalAmountSumByUser(ContextUser.CompanyId, currentFinYear.FinYearId,ContextUser.UserId);
                // ViewData["SITotalAmountSumByUser"] = siCount.SITotalAmountSum;
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return View();
        }

        public ActionResult SaleTeamDashboard()
        {
            try
            {
                CustomerBL customer = new CustomerBL();
                SOBL SO = new SOBL();
                FinYearViewModel currentFinYear = (FinYearViewModel)Session[SessionKey.CurrentFinYear];
                ViewData["currentFinyearId"] = currentFinYear.FinYearId;
                CustomerCountViewModel customerCount = customer.GetNewCustomerCount(ContextUser.CompanyId);
                ViewData["New_Customer_Count"] = customerCount.NewCustomer;
                customerCount = customer.GetTotalCustomerCount(ContextUser.CompanyId);
                ViewData["Total_Customer_Count"] = customerCount.TotalCustomer;
                SOCountViewModel SOcount = SO.GetSOCount(ContextUser.CompanyId, currentFinYear.FinYearId);
                ViewData["SO_Count"] = SOcount.sOTotalCount;
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return View();
        }
        [HttpPost]
        public void SetFinancialYear(int finYearId)
        {
            try
            {
                SessionWrapper session = new SessionWrapper();
                FinYearBL finYearBL = new FinYearBL();
                FinYearViewModel currentFinYear = finYearBL.GetCurrentFinancialYear(finYearId);
                session.SetInSession(SessionKey.CurrentFinYear, currentFinYear);
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return;
        }
        public ActionResult PurchaseDashboard()
        {
            try
            {
                PurchaseDashboardBL purchaseDashboardBL = new PurchaseDashboardBL();
                FinYearViewModel currentFinYear = (FinYearViewModel)Session[SessionKey.CurrentFinYear];
                ViewData["currentFinyearId"] = currentFinYear.FinYearId;
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return View();
        }

        public ActionResult PurchaseTeamDashboard()


        {
            try
            {
                PurchaseDashboardBL purchaseDashboardBL = new PurchaseDashboardBL();
                FinYearViewModel currentFinYear = (FinYearViewModel)Session[SessionKey.CurrentFinYear];
                ViewData["currentFinyearId"] = currentFinYear.FinYearId;
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return View();
        }
        public ActionResult InventoryDashboard()
        {            
            InventoryDashboardBL inventoryDashboardBL = new InventoryDashboardBL();
            try
            {
                
                FinYearViewModel currentFinYear = (FinYearViewModel)Session[SessionKey.CurrentFinYear];
                ViewData["currentFinyearId"] = currentFinYear.FinYearId;
                ViewData["totalProductCount"] = inventoryDashboardBL.GetTotalProductCount(ContextUser.CompanyId);
                ViewData["todayProductCount"] = inventoryDashboardBL.GetTodayProduct(ContextUser.CompanyId);
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return View();
        }
        public ActionResult AccountDashboard()
        {
            try
            {
                FinYearViewModel currentFinYear = (FinYearViewModel)Session[SessionKey.CurrentFinYear];
                ViewData["currentFinyearId"] = currentFinYear.FinYearId;
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return View();
        }
        public ActionResult HRDashboard()
        {
            return View();
        }
        public ActionResult PayrollDashboard()
        {
            try
            {
                FinYearViewModel currentFinYear = (FinYearViewModel)Session[SessionKey.CurrentFinYear];
                ViewData["currentFinyearId"] = currentFinYear.FinYearId;
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return View();
        }



        [HttpPost]
        public PartialViewResult GetBookBalanceList()
        {
            List<BookBalanceViewModel> bookBalanceList = new List<BookBalanceViewModel>();
            AccountDashboardBL dashboardBL = new AccountDashboardBL();
            try
            {
                string tempBookList = string.Empty;
                string tempBalanceList = string.Empty;
                int finYearId = Session[SessionKey.CurrentFinYear] != null ? ((FinYearViewModel)Session[SessionKey.CurrentFinYear]).FinYearId : DateTime.Now.Year;
                bookBalanceList = dashboardBL.GetBookBalanceList(ContextUser.CompanyId, finYearId, out tempBookList, out tempBalanceList);
                ViewBag.BookList = tempBookList.Trim();
                ViewBag.BalanceList = tempBalanceList.Trim();
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return PartialView(bookBalanceList);
        }

        [HttpPost]
        public PartialViewResult GetQuatationCountList(int userId = 0, string selfOrTeam = "SELF")
        {
            List<QuatationCountViewModel> quatationCountList = new List<QuatationCountViewModel>();
            SaleDashboardBL saledashboardBL = new SaleDashboardBL();
            try
            {
                string tempQuatationList = string.Empty;
                string tempCountList = string.Empty;
                int finYearId = Session[SessionKey.CurrentFinYear] != null ? ((FinYearViewModel)Session[SessionKey.CurrentFinYear]).FinYearId : DateTime.Now.Year;

                if (selfOrTeam == "SELF")
                {
                    quatationCountList = saledashboardBL.GetQuatationCountList(ContextUser.CompanyId, ContextUser.UserId, ContextUser.UserId, ContextUser.RoleId, finYearId, out tempQuatationList, out tempCountList);
                }
                else if (selfOrTeam == "TEAM")
                {
                    quatationCountList = saledashboardBL.GetQuatationCountList(ContextUser.CompanyId, userId, ContextUser.UserId, ContextUser.RoleId, finYearId, out tempQuatationList, out tempCountList);
                }

                ViewBag.QuatationList = tempQuatationList.Trim();
                ViewBag.CountList = tempCountList.Trim();
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return PartialView(quatationCountList);
        }

        [HttpPost]
        public PartialViewResult GetSOCountList(int userId = 0, string selfOrTeam = "SELF")
        {
            List<SOCountViewModel> sOCountList = new List<SOCountViewModel>();
            SaleDashboardBL saledashboardBL = new SaleDashboardBL();
            try
            {
                string tempSOList = string.Empty;
                string tempSOCountList = string.Empty;
                int finYearId = Session[SessionKey.CurrentFinYear] != null ? ((FinYearViewModel)Session[SessionKey.CurrentFinYear]).FinYearId : DateTime.Now.Year;
                if (selfOrTeam == "SELF")
                {
                    sOCountList = saledashboardBL.GetSOCountList(ContextUser.CompanyId, ContextUser.UserId, ContextUser.UserId, ContextUser.RoleId, finYearId, out tempSOList, out tempSOCountList);
                }
                else if (selfOrTeam == "TEAM")
                {
                    sOCountList = saledashboardBL.GetSOCountList(ContextUser.CompanyId, userId, ContextUser.UserId, ContextUser.RoleId, finYearId, out tempSOList, out tempSOCountList);
                }

                ViewBag.SOList = tempSOList.Trim();
                ViewBag.SOCountList = tempSOCountList.Trim();
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return PartialView(sOCountList);
        }

        [HttpPost]
        public PartialViewResult GetSICountList(int userId = 0, string selfOrTeam = "SELF")
        {
            List<SICountViewModel> sICountList = new List<SICountViewModel>();
            SaleDashboardBL saledashboardBL = new SaleDashboardBL();
            try
            {
                string tempSIList = string.Empty;
                string tempSICountList = string.Empty;
                int finYearId = Session[SessionKey.CurrentFinYear] != null ? ((FinYearViewModel)Session[SessionKey.CurrentFinYear]).FinYearId : DateTime.Now.Year;
                if (selfOrTeam == "SELF")
                {
                    sICountList = saledashboardBL.GetSICountList(ContextUser.CompanyId, ContextUser.UserId, ContextUser.UserId, ContextUser.RoleId, finYearId, out tempSIList, out tempSICountList);
                }
                else if (selfOrTeam == "TEAM")
                {
                    sICountList = saledashboardBL.GetSICountList(ContextUser.CompanyId, userId, ContextUser.UserId, ContextUser.RoleId, finYearId, out tempSIList, out tempSICountList);
                }

                ViewBag.SIList = tempSIList.Trim();
                ViewBag.SICountList = tempSICountList.Trim();
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return PartialView(sICountList);
        }



        [HttpPost]
        public JsonResult GetSISumByUser(int userId = 0, string selfOrTeam = "SELF")
        {
            SOBL sOBL = new SOBL();
            SICountViewModel sICountViewModel = new SICountViewModel();
            try
            {
                FinYearViewModel currentFinYear = (FinYearViewModel)Session[SessionKey.CurrentFinYear];
                if (selfOrTeam == "SELF")
                {
                    sICountViewModel = sOBL.GetSITotalAmountSumByUser(ContextUser.CompanyId, currentFinYear.FinYearId, ContextUser.UserId, ContextUser.UserId, ContextUser.RoleId);

                }
                else if (selfOrTeam == "TEAM")
                {
                    sICountViewModel = sOBL.GetSITotalAmountSumByUser(ContextUser.CompanyId, currentFinYear.FinYearId, userId, ContextUser.UserId, ContextUser.RoleId);
                }


            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return Json(sICountViewModel, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public string GetPendingPOCountList(int userId = 0, string selfOrTeam = "SELF")
        {
            PurchaseDashboardBL pOBL = new PurchaseDashboardBL();
            string PoCountPending = "";
            //POCountViewModel poCountViewModel = new POCountViewModel();
            try
            {
                FinYearViewModel currentFinYear = (FinYearViewModel)Session[SessionKey.CurrentFinYear];
                //int companyId, int userId, int reportingUserId, int reportingRoleId, int finYearId
                PoCountPending = pOBL.GetPendingPOCountList(ContextUser.CompanyId,ContextUser.UserId, ContextUser.UserId, ContextUser.RoleId, currentFinYear.FinYearId);

              }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return PoCountPending;// Json(poCountViewModel, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public PartialViewResult GetPOCountList(int userId = 0, string selfOrTeam = "SELF")
        {
            List<POCountViewModel> pOCountList = new List<POCountViewModel>();
            PurchaseDashboardBL purchaseDashboardBL = new PurchaseDashboardBL();

            try
            {
                string tempPOList = string.Empty;
                string tempPOCountList = string.Empty;
                int finYearId = Session[SessionKey.CurrentFinYear] != null ? ((FinYearViewModel)Session[SessionKey.CurrentFinYear]).FinYearId : DateTime.Now.Year;
                if (selfOrTeam == "SELF")
                {
                    pOCountList = purchaseDashboardBL.GetPOCountList(ContextUser.CompanyId, ContextUser.UserId, ContextUser.UserId, ContextUser.RoleId, finYearId, out tempPOList, out tempPOCountList);
                }
                else if (selfOrTeam == "TEAM")
                {
                    pOCountList = purchaseDashboardBL.GetPOCountList(ContextUser.CompanyId, userId, ContextUser.UserId, ContextUser.RoleId, finYearId, out tempPOList, out tempPOCountList);
                }
                ViewBag.POList = tempPOList.Trim();
                ViewBag.POCountList = tempPOCountList.Trim();
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return PartialView(pOCountList);
        }


        [HttpPost]
        public PartialViewResult GetPICountList(int userId = 0, string selfOrTeam = "SELF")
        {
            List<PICountViewModel> pICountList = new List<PICountViewModel>();
            PurchaseDashboardBL purchaseDashboardBL = new PurchaseDashboardBL();

            try
            {
                string tempPIList = string.Empty;
                string tempPICountList = string.Empty;
                int finYearId = Session[SessionKey.CurrentFinYear] != null ? ((FinYearViewModel)Session[SessionKey.CurrentFinYear]).FinYearId : DateTime.Now.Year;
                if (selfOrTeam == "SELF")
                {
                    pICountList = purchaseDashboardBL.GetPICountList(ContextUser.CompanyId, ContextUser.UserId, ContextUser.UserId, ContextUser.RoleId, finYearId, out tempPIList, out tempPICountList);
                }
                else if (selfOrTeam == "TEAM")
                {
                    pICountList = purchaseDashboardBL.GetPICountList(ContextUser.CompanyId, userId, ContextUser.UserId, ContextUser.RoleId, finYearId, out tempPIList, out tempPICountList);
                }
                ViewBag.PIList = tempPIList.Trim();
                ViewBag.PICountList = tempPICountList.Trim();
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return PartialView(pICountList);
        }



        [HttpPost]
        public PartialViewResult GetMonthWiseBankCashTransactionSummary()
        {
            List<MonthWiseCashBankTrnSummaryViewModel> monthWiseTrnList = new List<MonthWiseCashBankTrnSummaryViewModel>();
            AccountDashboardBL dashboardBL = new AccountDashboardBL();
            try
            {
                string tempMonthList = string.Empty;
                string tempBankPaymentList = string.Empty;
                string tempBankReceiptList = string.Empty;
                string tempCashPaymentList = string.Empty;
                string tempCashReceiptList = string.Empty;

                int finYearId = Session[SessionKey.CurrentFinYear] != null ? ((FinYearViewModel)Session[SessionKey.CurrentFinYear]).FinYearId : DateTime.Now.Year;
                monthWiseTrnList = dashboardBL.GetMonthWiseBankCashTrnList(ContextUser.CompanyId, finYearId, out tempMonthList, out tempBankPaymentList, out tempBankReceiptList, out tempCashPaymentList, out tempCashReceiptList);
                ViewBag.MonthList = tempMonthList.Trim();
                ViewBag.BankPaymentList = tempBankPaymentList.Trim();
                ViewBag.BankReceiptList = tempBankReceiptList.Trim();
                ViewBag.CashPaymentList = tempCashPaymentList.Trim();
                ViewBag.CashReceiptList = tempCashReceiptList.Trim();
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return PartialView(monthWiseTrnList);
        }

        [HttpPost]
        public PartialViewResult GetLeadStatusCountList(int userId = 0, string selfOrTeam = "SELF")
        {
            List<LeadStatusCountViewModel> leadStatusCountList = new List<LeadStatusCountViewModel>();
            CRMDashboardBL dashboardBL = new CRMDashboardBL();
            try
            {
                if (selfOrTeam == "SELF")
                {
                    leadStatusCountList = dashboardBL.GetLeadStatusCountList(ContextUser.CompanyId, ContextUser.UserId, ContextUser.UserId, ContextUser.RoleId);
                }
                else if (selfOrTeam == "TEAM")
                {
                    leadStatusCountList = dashboardBL.GetLeadStatusCountList(ContextUser.CompanyId, userId, ContextUser.UserId, ContextUser.RoleId);
                }

            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return PartialView(leadStatusCountList);
        }
        [HttpPost]
        public PartialViewResult GetLeadSourceCountList(int userId = 0, string selfOrTeam = "SELF")
        {
            List<LeadSourceCountViewModel> leadSourceCountList = new List<LeadSourceCountViewModel>();
            CRMDashboardBL dashboardBL = new CRMDashboardBL();
            try
            {
                if (selfOrTeam == "SELF")
                {
                    leadSourceCountList = dashboardBL.GetLeadSourceCountList(ContextUser.CompanyId, ContextUser.UserId, ContextUser.UserId, ContextUser.RoleId);
                }
                else if (selfOrTeam == "TEAM")
                {
                    leadSourceCountList = dashboardBL.GetLeadSourceCountList(ContextUser.CompanyId, userId, ContextUser.UserId, ContextUser.RoleId);
                }

            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return PartialView(leadSourceCountList);
        }
        [HttpPost]
        public PartialViewResult GetLeadFollowUpCountList(int userId = 0, string selfOrTeam = "SELF")
        {
            List<LeadFollowUpCountViewModel> leadFollowUpCountList = new List<LeadFollowUpCountViewModel>();
            CRMDashboardBL dashboardBL = new CRMDashboardBL();
            try
            {
                if (selfOrTeam == "SELF")
                {
                    leadFollowUpCountList = dashboardBL.GetLeadFollowUpCountList(ContextUser.CompanyId, ContextUser.UserId, ContextUser.UserId, ContextUser.RoleId);
                }
                else if (selfOrTeam == "TEAM")
                {
                    leadFollowUpCountList = dashboardBL.GetLeadFollowUpCountList(ContextUser.CompanyId, userId, ContextUser.UserId, ContextUser.RoleId);
                }



            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return PartialView(leadFollowUpCountList);
        }
        [HttpPost]
        public PartialViewResult GetDateWiseLeadConversionCountList(int userId = 0, string selfOrTeam = "SELF")
        {
            List<LeadConversionCountViewModel> leadConversionCountList = new List<LeadConversionCountViewModel>();
            CRMDashboardBL dashboardBL = new CRMDashboardBL();
            try
            {
                string dateList = string.Empty;
                string totalLeadCountList = string.Empty;
                string newOpportunityCountList = string.Empty;
                string quotationCountList = string.Empty;
                if (selfOrTeam == "SELF")
                {
                    leadConversionCountList = dashboardBL.GetDateWiseLeadConversionCountList(ContextUser.CompanyId, ContextUser.UserId, ContextUser.UserId, ContextUser.RoleId, out dateList, out totalLeadCountList, out newOpportunityCountList, out quotationCountList);
                }
                else if (selfOrTeam == "TEAM")
                {
                    leadConversionCountList = dashboardBL.GetDateWiseLeadConversionCountList(ContextUser.CompanyId, userId, ContextUser.UserId, ContextUser.RoleId, out dateList, out totalLeadCountList, out newOpportunityCountList, out quotationCountList);
                }

                //                leadConversionCountList = dashboardBL.GetDateWiseLeadConversionCountList(ContextUser.CompanyId, 0, out dateList, out totalLeadCountList, out newOpportunityCountList, out quotationCountList);
                ViewBag.DateList = dateList.Trim();
                ViewBag.TotalLeadCountList = totalLeadCountList.Trim();
                ViewBag.NewOpportunityCountList = newOpportunityCountList.Trim();
                ViewBag.QuotationCountList = quotationCountList.Trim();
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return PartialView(leadConversionCountList);
        }
        [HttpPost]
        public PartialViewResult GetLeadFollowUpReminderDueDateCountList(int userId = 0, string selfOrTeam = "SELF")
        {
            List<LeadFollowUpReminderDueDateCountViewModel> leadFollowUpCountList = new List<LeadFollowUpReminderDueDateCountViewModel>();
            CRMDashboardBL dashboardBL = new CRMDashboardBL();
            try
            {
                if (selfOrTeam == "SELF")
                {
                    leadFollowUpCountList = dashboardBL.GetLeadFollowUpReminderDueDateCountList(ContextUser.CompanyId, ContextUser.UserId, ContextUser.UserId, ContextUser.RoleId);
                }
                else if (selfOrTeam == "TEAM")
                {
                    leadFollowUpCountList = dashboardBL.GetLeadFollowUpReminderDueDateCountList(ContextUser.CompanyId, userId, ContextUser.UserId, ContextUser.RoleId);
                }



            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return PartialView(leadFollowUpCountList);
        }

        [HttpPost]
        public PartialViewResult GetLeadFollowUpReminderDueDateList(int userId = 0, string selfOrTeam = "SELF", int FollowUpActivityTypeId = 0)
        {
            List<LeadFollowUpReminderDueDateListViewModel> leadFollowUpList = new List<LeadFollowUpReminderDueDateListViewModel>();
            CRMDashboardBL dashboardBL = new CRMDashboardBL();
            try
            {
                if (selfOrTeam == "SELF")
                {
                    leadFollowUpList = dashboardBL.GetLeadFollowUpReminderDueDateList(ContextUser.CompanyId, ContextUser.UserId, ContextUser.UserId, ContextUser.RoleId, FollowUpActivityTypeId);
                }
                else if (selfOrTeam == "TEAM")
                {
                    leadFollowUpList = dashboardBL.GetLeadFollowUpReminderDueDateList(ContextUser.CompanyId, userId, ContextUser.UserId, ContextUser.RoleId, FollowUpActivityTypeId);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return PartialView(leadFollowUpList);
        }

        [HttpPost]
        public PartialViewResult GetReorderPointProductCountList()
        {
            List<ReorderPointProductCountViewModel> reorderProductCountList = new List<ReorderPointProductCountViewModel>();
            InventoryDashboardBL inventoryDashboardBL = new InventoryDashboardBL();
            try
            {
                int finYearId = Session[SessionKey.CurrentFinYear] != null ? ((FinYearViewModel)Session[SessionKey.CurrentFinYear]).FinYearId : DateTime.Now.Year;
                reorderProductCountList = inventoryDashboardBL.GetReorderPointProductCountList(ContextUser.CompanyId, finYearId);
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return PartialView(reorderProductCountList);
        }


        [HttpPost]
        public PartialViewResult GetInOutProductQuantityCountList()
        {
            List<ProductQuantityCountViewModel> productQuantityCountList = new List<ProductQuantityCountViewModel>();
            InventoryDashboardBL inventoryDashboardBL = new InventoryDashboardBL();
            try
            {
                int finYearId = Session[SessionKey.CurrentFinYear] != null ? ((FinYearViewModel)Session[SessionKey.CurrentFinYear]).FinYearId : DateTime.Now.Year;
                productQuantityCountList = inventoryDashboardBL.GetInOutProductQuantityCountList(ContextUser.CompanyId);
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return PartialView(productQuantityCountList);
        }

        [HttpPost]
        public PartialViewResult GetSINProductQuantityCountList()
        {
            List<SINProductQuantityCountViewModel> sINproductQuantityCountList = new List<SINProductQuantityCountViewModel>();
            InventoryDashboardBL inventoryDashboardBL = new InventoryDashboardBL();
            try
            {
                int finYearId = Session[SessionKey.CurrentFinYear] != null ? ((FinYearViewModel)Session[SessionKey.CurrentFinYear]).FinYearId : DateTime.Now.Year;
                sINproductQuantityCountList = inventoryDashboardBL.GetSINProductQuantityCountList(ContextUser.CompanyId);
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return PartialView(sINproductQuantityCountList);
        }

        public ActionResult StoreDashboard()
        {
            try
            {
                FinYearViewModel currentFinYear = (FinYearViewModel)Session[SessionKey.CurrentFinYear];
                ViewData["currentFinyearId"] = currentFinYear.FinYearId;
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return View();

        }

        [HttpGet]
        public ActionResult EssDashboard()
        {
            ViewData["currentDate"] = DateTime.Now.ToString("dd-MMM-yyyy");
            ViewData["essEmployeeId"] = Session[SessionKey.EmployeeId];
            return View();
        }
        [HttpGet]
        public JsonResult GetESSEmployeeDetail()
        {
            EmployeeBL employeeBL = new EmployeeBL();
            EmployeeViewModel employee = new EmployeeViewModel();
            try
            {
                employee = employeeBL.GetESSEmployeeDetail(ContextUser.UserId, ContextUser.CompanyId);
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return Json(employee, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetUpComingHolidayDetail()
        {
            HolidayCalenderBL holidayCalenderBL = new HolidayCalenderBL();
            HolidayCalenderViewModel holiday = new HolidayCalenderViewModel();
            try
            {
                holiday = holidayCalenderBL.GetUpComingHolidayDetail();
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return Json(holiday, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetUpComingActivityDetail()
        {
            ActivityCalenderBL activityCalenderBL = new ActivityCalenderBL();
            ActivityCalenderViewModel activity = new ActivityCalenderViewModel();
            try
            {
                activity = activityCalenderBL.GetUpcomingActivityDetail();
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return Json(activity, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public PartialViewResult GetESSEmployeeRoasterList(int employeeId = 0)
        {
            List<HR_RoasterViewModel> roasters = new List<HR_RoasterViewModel>();
            RoasterBL roasterBL = new RoasterBL();
            try
            {
                roasters = roasterBL.GetESSEmployeeRoasterList(employeeId);
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return PartialView(roasters);
        }

        [HttpGet]
        public PartialViewResult GetDashboardThoughtList()
        {
            List<ThoughtViewModel> thoughts = new List<ThoughtViewModel>();
            ThoughtBL thoughtBL = new ThoughtBL();
            try
            {
                thoughts = thoughtBL.GetDashboardThoughtDetail();
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return PartialView(thoughts);
        }

        [HttpGet]
        public JsonResult GetStickyNotesDetails()
        {
            StickyNotesBL stickyNotesBL = new StickyNotesBL();
            StickyNotesViewModel stickyNotes = new StickyNotesViewModel();
            try
            {
                int UserId = ContextUser.UserId;
                stickyNotes = stickyNotesBL.GetStickyNotesDetail(UserId);
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return Json(stickyNotes, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateRequest(true, UserInterfaceHelper.Add_Edit_Thought, (int)AccessMode.AddAccess, (int)RequestMode.Ajax)]
        public ActionResult AddEditStickyNotes(StickyNotesViewModel stickyNotesViewModel)
        {
            ResponseOut responseOut = new ResponseOut();
            StickyNotesBL stickyNotesBL = new StickyNotesBL();
            try
            {
                if (stickyNotesViewModel != null)
                {
                    stickyNotesViewModel.UserId = ContextUser.UserId;
                    responseOut = stickyNotesBL.AddEditStickyNotes(stickyNotesViewModel);
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
        public JsonResult GetHolidayandActivityDetails()
        {
            ActivityCalenderBL activityCalenderBL = new ActivityCalenderBL();
            List<HolidayActivityCalenderViewModel> holidayActivityCalenders = new List<HolidayActivityCalenderViewModel>();
            try
            {
                int UserId = ContextUser.UserId;
                holidayActivityCalenders = activityCalenderBL.GetHolidayandActivityDetails();
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return Json(holidayActivityCalenders, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult GetTeamDetailList()
        {
            LeadBL leadBL = new LeadBL();
            List<UserViewModel> users = new List<UserViewModel>();
            try
            {
                users = leadBL.GetTeamDetailList(ContextUser.UserId);
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return Json(users, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public PartialViewResult GetTodayNewCustomerList()
        {
            List<CustomerViewModel> customers = new List<CustomerViewModel>();
            CustomerBL customerBL = new CustomerBL();
            try
            {
                customers = customerBL.GetTodayNewCustomerList(ContextUser.CompanyId);
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return PartialView(customers);
        }

        [HttpPost]
        public JsonResult GetTodayPOSumAmount(int userId = 0, string selfOrTeam = "SELF")
        {
            POCountViewModel pOCountList = new POCountViewModel();
            PurchaseDashboardBL purchaseDashboardBL = new PurchaseDashboardBL();
            try
            {
                if (selfOrTeam == "SELF")
                {
                    pOCountList = purchaseDashboardBL.GetDashboard_TodayPOSumAmount(ContextUser.CompanyId, ContextUser.UserId, ContextUser.UserId, ContextUser.RoleId);
                }
                else if (selfOrTeam == "TEAM")
                {
                    pOCountList = purchaseDashboardBL.GetDashboard_TodayPOSumAmount(ContextUser.CompanyId, userId, ContextUser.UserId, ContextUser.RoleId);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return Json(pOCountList, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetTodayPISumAmount(int userId = 0, string selfOrTeam = "SELF")
        {
            PICountViewModel pOCountList = new PICountViewModel();
            PurchaseDashboardBL purchaseDashboardBL = new PurchaseDashboardBL();
            try
            {
                if (selfOrTeam == "SELF")
                {
                    pOCountList = purchaseDashboardBL.GetDashboard_TodayPISumAmount(ContextUser.CompanyId, ContextUser.UserId, ContextUser.UserId, ContextUser.RoleId);
                }
                else if (selfOrTeam == "TEAM")
                {
                    pOCountList = purchaseDashboardBL.GetDashboard_TodayPISumAmount(ContextUser.CompanyId, userId, ContextUser.UserId, ContextUser.RoleId);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return Json(pOCountList, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public PartialViewResult GetESSEmployeeAdvanceApplicationList(int userId = 0, string selfOrTeam = "SELF")
        {
            List<EmployeeAdvanceApplicationViewModel> advanceApplicationList = new List<EmployeeAdvanceApplicationViewModel>();
            AdvanceTypeBL advanceTypeBL = new AdvanceTypeBL();
            try
            {
                if (selfOrTeam == "SELF")
                {
                    advanceApplicationList = advanceTypeBL.GetEmployeeAdvanceApplicationDetails(ContextUser.CompanyId, ContextUser.UserId, ContextUser.UserId, ContextUser.RoleId);
                }
                else if (selfOrTeam == "TEAM")
                {
                    advanceApplicationList = advanceTypeBL.GetEmployeeAdvanceApplicationDetails(ContextUser.CompanyId, userId, ContextUser.UserId, ContextUser.RoleId);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return PartialView(advanceApplicationList);
        }

        [HttpPost]
        public PartialViewResult GetESSEmployeeLeaveApplicationList(int userId = 0, string selfOrTeam = "SELF")
        {
            List<EmployeeLeaveApplicationViewModel> leaveApplicationList = new List<EmployeeLeaveApplicationViewModel>();
            LeaveTypeBL leaveTypeBL = new LeaveTypeBL();
            try
            {
                if (selfOrTeam == "SELF")
                {
                    leaveApplicationList = leaveTypeBL.GetEmployeeLeaveApplicationDetails(ContextUser.CompanyId, ContextUser.UserId, ContextUser.UserId, ContextUser.RoleId);
                }
                else if (selfOrTeam == "TEAM")
                {
                    leaveApplicationList = leaveTypeBL.GetEmployeeLeaveApplicationDetails(ContextUser.CompanyId, userId, ContextUser.UserId, ContextUser.RoleId);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return PartialView(leaveApplicationList);
        }

        [HttpPost]
        public PartialViewResult GetESSEmployeeAssetApplicationList(int userId = 0, string selfOrTeam = "SELF")
        {
            List<EmployeeAssetApplicationViewModel> assetApplicationList = new List<EmployeeAssetApplicationViewModel>();
            AssetTypeBL assetTypeBL = new AssetTypeBL();
            try
            {
                if (selfOrTeam == "SELF")
                {
                    assetApplicationList = assetTypeBL.GetEmployeeAssetApplicationDetails(ContextUser.CompanyId, ContextUser.UserId, ContextUser.UserId, ContextUser.RoleId);
                }
                else if (selfOrTeam == "TEAM")
                {
                    assetApplicationList = assetTypeBL.GetEmployeeAssetApplicationDetails(ContextUser.CompanyId, userId, ContextUser.UserId, ContextUser.RoleId);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return PartialView(assetApplicationList);
        }

        [HttpPost]
        public PartialViewResult GetESSEmployeeClaimApplicationList(int userId = 0, string selfOrTeam = "SELF")
        {
            List<EmployeeClaimApplicationViewModel> claimApplicationList = new List<EmployeeClaimApplicationViewModel>();
            ClaimTypeBL claimTypeBL = new ClaimTypeBL();
            try
            {
                if (selfOrTeam == "SELF")
                {
                    claimApplicationList = claimTypeBL.GetEmployeeClaimApplicationDetails(ContextUser.CompanyId, ContextUser.UserId, ContextUser.UserId, ContextUser.RoleId);
                }
                else if (selfOrTeam == "TEAM")
                {
                    claimApplicationList = claimTypeBL.GetEmployeeClaimApplicationDetails(ContextUser.CompanyId, userId, ContextUser.UserId, ContextUser.RoleId);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return PartialView(claimApplicationList);
        }

        [HttpPost]
        public PartialViewResult GetESSEmployeeLoanApplicationList(int userId = 0, string selfOrTeam = "SELF")
        {
            List<EmployeeLoanApplicationViewModel> loanApplicationList = new List<EmployeeLoanApplicationViewModel>();
            LoanTypeBL loanTypeBL = new LoanTypeBL();
            try
            {
                if (selfOrTeam == "SELF")
                {
                    loanApplicationList = loanTypeBL.GetEmployeeLoanApplicationDetails(ContextUser.CompanyId, ContextUser.UserId, ContextUser.UserId, ContextUser.RoleId);
                }
                else if (selfOrTeam == "TEAM")
                {
                    loanApplicationList = loanTypeBL.GetEmployeeLoanApplicationDetails(ContextUser.CompanyId, userId, ContextUser.UserId, ContextUser.RoleId);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return PartialView(loanApplicationList);
        }
        [HttpPost]
        public PartialViewResult GetESSEmployeeTravelApplicationList(int userId = 0, string selfOrTeam = "SELF")
        {
            List<HR_EmployeeTravelApplicationViewModel> travelApplicationList = new List<HR_EmployeeTravelApplicationViewModel>();
            TravelTypeBL travelTypeBL = new TravelTypeBL();
            try
            {
                if (selfOrTeam == "SELF")
                {
                    travelApplicationList = travelTypeBL.GetEmployeeTravelApplicationDetails(ContextUser.CompanyId, ContextUser.UserId, ContextUser.UserId, ContextUser.RoleId);
                }
                else if (selfOrTeam == "TEAM")
                {
                    travelApplicationList = travelTypeBL.GetEmployeeTravelApplicationDetails(ContextUser.CompanyId, userId, ContextUser.UserId, ContextUser.RoleId);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return PartialView(travelApplicationList);
        }

        [HttpPost]
        public PartialViewResult GetAdminDashboardRolesList()
        {
            List<RoleViewModel> roleList = new List<RoleViewModel>();
            RoleBL roleBL= new RoleBL();
            try
            {
                roleList = roleBL.GetDashboardRolesDetails(ContextUser.CompanyId);
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return PartialView(roleList);
        }

        [HttpPost]
        public PartialViewResult GetAdminDashboardUsersList()
        {
            List<UserViewModel> usersList = new List<UserViewModel>();
            UserBL userBL = new UserBL();
            try
            {
                usersList = userBL.GetDashboardUsersDetails(ContextUser.CompanyId);
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return PartialView(usersList);
        }

        /*Start Escalation Matrix Count Data*/
        [HttpPost]
        public PartialViewResult GetEscalatedMatrix(int userId = 0, string selfOrTeam = "SELF")
        {
            List<EscalationCountViewModel> escalationCountList = new List<EscalationCountViewModel>();
            EscalationMatrixBL escalationMatrixBL = new EscalationMatrixBL();

            try
            {
                string tempEscalationList = string.Empty;
                string tempEscalationCountList = string.Empty;
                int finYearId = Session[SessionKey.CurrentFinYear] != null ? ((FinYearViewModel)Session[SessionKey.CurrentFinYear]).FinYearId : DateTime.Now.Year;
                if (selfOrTeam == "SELF")
                {
                    escalationCountList = escalationMatrixBL.GetEscalationMatrixCountList(ContextUser.CompanyId, finYearId, out tempEscalationList, out tempEscalationCountList);
                }
                else if (selfOrTeam == "TEAM")
                {
                    escalationCountList = escalationMatrixBL.GetEscalationMatrixCountList(ContextUser.CompanyId, finYearId, out tempEscalationList, out tempEscalationCountList);
                }
                ViewBag.EscalationList = tempEscalationList.Trim();
                ViewBag.EscalationCountList = tempEscalationCountList.Trim();
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return PartialView(escalationCountList);
        }

        /*End Escalation Matrix Count Data*/


        [HttpPost]
        public PartialViewResult GetDashboardApprovedRequisitionListWithoutPO()
        {
            List<StoreRequisitionViewModel> storeRequisitionList = new List<StoreRequisitionViewModel>();
            StoreRequisitionBL storeRequisitionBL = new StoreRequisitionBL();
            try
            {
                int finYearId = Session[SessionKey.CurrentFinYear] != null ? ((FinYearViewModel)Session[SessionKey.CurrentFinYear]).FinYearId : DateTime.Now.Year;
                storeRequisitionList = storeRequisitionBL.GetDashboardApprovedStoreRequisitionListWithoutPO(ContextUser.CompanyId);
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return PartialView(storeRequisitionList);
        }

        [HttpPost]
        public PartialViewResult GetDashboardRecommendationPOList()
        {
            List<POViewModel> poList = new List<POViewModel>();
            POBL storeRequisitionBL = new POBL();
            try
            {
                int finYearId = Session[SessionKey.CurrentFinYear] != null ? ((FinYearViewModel)Session[SessionKey.CurrentFinYear]).FinYearId : DateTime.Now.Year;
                poList = storeRequisitionBL.GetDashboardRecommendationPOList(ContextUser.CompanyId);
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return PartialView(poList);
        }

    }
}
