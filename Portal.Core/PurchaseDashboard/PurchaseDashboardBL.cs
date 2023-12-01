using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Portal.Core.ViewModel;
using Portal.DAL;
using Portal.Common;
using System.Reflection;
using System.Data;

namespace Portal.Core
{
    public class PurchaseDashboardBL
    {
        
        public PurchaseDashboardBL()
        {
            
        }
        public List<POCountViewModel> GetPOCountList(int companyId, int userId, int reportingUserId, int reportingRoleId, int finYearId, out string pOCountList,out string pOtotalCountList)
        {
            
            List<POCountViewModel> bookBalanceList = new List<POCountViewModel>();
            SQLDbInterface sqlDbInterface = new SQLDbInterface();
            try
            {
                DataTable dtPOCount = sqlDbInterface.GetDashboard_POCount(companyId, userId, reportingUserId, reportingRoleId, finYearId);
                if (dtPOCount != null && dtPOCount.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtPOCount.Rows)
                    {
                        bookBalanceList.Add(new POCountViewModel
                        {                         
                            POCount_Head = Convert.ToString(dr["Count_Head"]),                          
                            POTotalCount = Convert.ToString(dr["TotalCount"])
                        });
                    }
                }
                var PocountHead = (from temp in bookBalanceList
                                 select temp.POCount_Head).ToList();

                var POtotal_Count = (from temp in bookBalanceList
                                    select temp.POTotalCount).ToList();

                pOCountList = "'"+ string.Join("','", PocountHead) + "'";


                pOtotalCountList = string.Join(",", POtotal_Count);
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return bookBalanceList;
        }

        public List<PICountViewModel> GetPICountList(int companyId, int userId, int reportingUserId, int reportingRoleId, int finYearId, out string pICountList, out string pItotalCountList)
        {

            List<PICountViewModel> bookBalanceList = new List<PICountViewModel>();
            SQLDbInterface sqlDbInterface = new SQLDbInterface();
            try
            {
                DataTable dtPICount = sqlDbInterface.GetDashboard_PICount(companyId, userId, reportingUserId, reportingRoleId, finYearId);
                if (dtPICount != null && dtPICount.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtPICount.Rows)
                    {
                        bookBalanceList.Add(new PICountViewModel
                        {
                            PICount_Head = Convert.ToString(dr["Count_Head"]),
                            PITotalCount = Convert.ToString(dr["TotalCount"])
                        });
                    }
                }
                var PIcountHead = (from temp in bookBalanceList
                                 select temp.PICount_Head).ToList();

                var PItotal_Count = (from temp in bookBalanceList
                                   select temp.PITotalCount).ToList();

                pICountList = "'" + string.Join("','", PIcountHead) + "'";


                pItotalCountList = string.Join(",", PItotal_Count);
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return bookBalanceList;
        }


        public POCountViewModel GetDashboard_TodayPOSumAmount(int companyId, int userId, int reportingUserId, int reportingRoleId)
        {
            POCountViewModel pOCountViewModel = new POCountViewModel();
            SQLDbInterface sqldbinterface = new SQLDbInterface();
            try
            {
                DataTable dtcustomerList = sqldbinterface.GetDashboard_TodayPOSumAmount(companyId, userId, reportingUserId, reportingRoleId);

                if (dtcustomerList != null && dtcustomerList.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtcustomerList.Rows)
                    {
                        pOCountViewModel = new POCountViewModel
                        {
                            TodayPOSumAmount = Convert.ToString(dr["TodayPOSum"])
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return pOCountViewModel;
        }

        public PICountViewModel GetDashboard_TodayPISumAmount(int companyId, int userId, int reportingUserId, int reportingRoleId)
        {
            PICountViewModel pICountViewModel = new PICountViewModel();
            SQLDbInterface sqldbinterface = new SQLDbInterface();
            try
            {
                DataTable dtPIList = sqldbinterface.GetDashboard_TodayPISumAmount(companyId, userId, reportingUserId, reportingRoleId);

                if (dtPIList != null && dtPIList.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtPIList.Rows)
                    {
                        pICountViewModel = new PICountViewModel
                        {
                            TodayPISumAmount = Convert.ToString(dr["TodayPISum"])
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return pICountViewModel;
        }

        public List<StoreRequisitionViewModel> GetDashboardPendingStoreRequisition(int companyId)
        {
            List<StoreRequisitionViewModel> storeRequisitionList = new List<StoreRequisitionViewModel>();
            SQLDbInterface sqlDbInterface = new SQLDbInterface();
            try
            {
                DataTable dtCount = sqlDbInterface.GetDashboardPendingStoreRequisition(companyId);
                if (dtCount != null && dtCount.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtCount.Rows)
                    {
                        storeRequisitionList.Add(new StoreRequisitionViewModel
                        {
                            RequisitionId = Convert.ToInt32(dr["RequisitionId"]),
                            RequisitionNo = Convert.ToString(dr["RequisitionNo"]),
                            RequisitionDate = Convert.ToString(dr["RequisitionDate"]),
                            RequisitionStatus = "Pending"
                        });
                    }
                }

            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return storeRequisitionList;
        }

        public List<StoreRequisitionViewModel> GetDashboardRejectedStoreRequisition(int companyId)
        {
            List<StoreRequisitionViewModel> storeRequisitionList = new List<StoreRequisitionViewModel>();
            SQLDbInterface sqlDbInterface = new SQLDbInterface();
            try
            {
                DataTable dtCount = sqlDbInterface.GetDashboardRejectedStoreRequisition(companyId);
                if (dtCount != null && dtCount.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtCount.Rows)
                    {
                        storeRequisitionList.Add(new StoreRequisitionViewModel
                        {
                            RequisitionId = Convert.ToInt32(dr["RequisitionId"]),
                            RequisitionNo = Convert.ToString(dr["RequisitionNo"]),
                            RequisitionDate = Convert.ToString(dr["RequisitionDate"]),
                            ApprovalStatus = Convert.ToString(dr["ApprovalStatus"])
                        });
                    }
                }

            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return storeRequisitionList;
        }

        public List<POViewModel> GetPOPendingForMDApprovals(int companyId)
        {
            List<POViewModel> poList = new List<POViewModel>();
            SQLDbInterface sqlDbInterface = new SQLDbInterface();
            try
            {
                DataTable dtCount = sqlDbInterface.GetPOPendingForMDApprovals(companyId);
                if (dtCount != null && dtCount.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtCount.Rows)
                    {
                        poList.Add(new POViewModel
                        {
                            POId= Convert.ToInt32(dr["POId"]),
                            PONo = Convert.ToString(dr["PONo"]),
                            PODate = Convert.ToString(dr["PODate"]),
                            ApprovalStatus = Convert.ToString(dr["ApprovalStatus"])
                        });
                    }
                }

            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return poList;
        }

        public List<POViewModel> GetPORejectedByMD(int companyId)
        {
            List<POViewModel> poList = new List<POViewModel>();
            SQLDbInterface sqlDbInterface = new SQLDbInterface();
            try
            {
                DataTable dtCount = sqlDbInterface.GetPORejectedByMD(companyId);
                if (dtCount != null && dtCount.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtCount.Rows)
                    {
                        poList.Add(new POViewModel
                        {
                            POId = Convert.ToInt32(dr["POId"]),
                            PONo = Convert.ToString(dr["PONo"]),
                            PODate = Convert.ToString(dr["PODate"]),
                            ApprovalStatus = Convert.ToString(dr["ApprovalStatus"])
                        });
                    }
                }

            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return poList;
        }

        #region "POApproval Pending"
        public string GetPendingPOCountList(int companyId, int userId, int reportingUserId, int reportingRoleId, int finYearId)
        {
            POCountViewModel PO = new POCountViewModel();
            SQLDbInterface sqldbinterface = new SQLDbInterface();
            string TotalPendingPOCount = "";
            try
            {
                DataTable dtPOList = sqldbinterface.GetPendingPOCountList(companyId,userId, reportingUserId, reportingRoleId,finYearId);

                if (dtPOList != null && dtPOList.Rows.Count > 0)
                {
                    TotalPendingPOCount = Convert.ToString(dtPOList.Rows[0]["PendingApprovalCount"]);
                    //foreach (DataRow dr in dtPOList.Rows)
                    //{
                    //    //PO = new POCountViewModel
                    //    //{
                    //    if (dr["Count_Head"].ToString() == "PendingApprovalCount")
                    //    {
                    //        TotalPendingPOCount = Convert.ToString(dr["TotalCount"]);
                    //    }
                    //    //};
                    //}
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return TotalPendingPOCount;
        }

        public string GetPendingPOLessThan25KCountList(int companyId, int userId, int reportingUserId, int reportingRoleId, int finYearId)
        {
            POCountViewModel PO = new POCountViewModel();
            SQLDbInterface sqldbinterface = new SQLDbInterface();
            string TotalPendingPOCount = "";
            try
            {
                DataTable dtPOList = sqldbinterface.GetPendingPOLessThan25KCountList(companyId, userId, reportingUserId, reportingRoleId, finYearId);

                if (dtPOList != null && dtPOList.Rows.Count > 0)
                {
                    TotalPendingPOCount = Convert.ToString(dtPOList.Rows[0]["PendingPOCount"]);
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return TotalPendingPOCount;
        }


        #endregion
    }
}








