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
    public class SupervisorDashboardBL
    {
        
        public SupervisorDashboardBL()
        {
            
        }
        public List<ReorderPointProductCountViewModel> GetReorderPointProductCountList(int companyId,int finYearId)
        {
            
            List<ReorderPointProductCountViewModel> reorderProductCountList = new List<ReorderPointProductCountViewModel>();
            SQLDbInterface sqlDbInterface = new SQLDbInterface();
            try
            {
                DataTable dtCount= sqlDbInterface.GetDashboard_ReorderPointProductCount(companyId,finYearId);
                if (dtCount != null && dtCount.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtCount.Rows)
                    {
                        reorderProductCountList.Add(new ReorderPointProductCountViewModel
                        {
                            ProductId = Convert.ToInt32(dr["ProductId"]),
                            ProductName = Convert.ToString(dr["ProductName"]),
                            ReorderQty = Convert.ToInt32(dr["ReorderQty"]),
                            AvailableStock = Convert.ToInt32(dr["AvailableStock"])
                        });
                    }
                }
               
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return reorderProductCountList;
        }

        public List<ProductQuantityCountViewModel> GetInOutProductQuantityCountList(int companyId)
        {

            List<ProductQuantityCountViewModel> inOutProductQuantityCountList = new List<ProductQuantityCountViewModel>();
            SQLDbInterface sqlDbInterface = new SQLDbInterface();
            try
            {
                DataTable dtCount = sqlDbInterface.GetDashboard_InOutProductQuantityCount(companyId);
                if (dtCount != null && dtCount.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtCount.Rows)
                    {
                        inOutProductQuantityCountList.Add(new ProductQuantityCountViewModel
                        {
                           
                            ProductName = Convert.ToString(dr["ProductName"]),
                            ProductCode = Convert.ToString(dr["ProductCode"]),
                            OpeningQty = Convert.ToInt32(dr["OpeningQty"]),
                            PurchaseQty = Convert.ToInt32(dr["PurchaseQty"]),
                            SaleQty = Convert.ToInt32(dr["SaleQty"]),
                            STNQty = Convert.ToInt32(dr["STNQty"]) ,
                            STRQty = Convert.ToInt32(dr["STRQty"]),
                            ClosingQty = Convert.ToInt32(dr["ClosingQty"]),                            
                        });
                    }
                }

            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return inOutProductQuantityCountList;
        }


        public List<SINProductQuantityCountViewModel> GetSINProductQuantityCountList(int companyId)
        {

            List<SINProductQuantityCountViewModel> sINProductQuantityCountList = new List<SINProductQuantityCountViewModel>();
            SQLDbInterface sqlDbInterface = new SQLDbInterface();
            try
            {
                DataTable dtCount = sqlDbInterface.GetDashboard_SINProductQuantityCount(companyId);
                if (dtCount != null && dtCount.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtCount.Rows)
                    {
                        sINProductQuantityCountList.Add(new SINProductQuantityCountViewModel
                        {
                            ProductId = Convert.ToInt32(dr["ProductId"]),
                            ProductName = Convert.ToString(dr["ProductName"]),                           
                            Qty = Convert.ToInt32(dr["Quantity"]),                          
                        });
                    }
                }

            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return sINProductQuantityCountList;
        }


        public int GetTotalProductCount(int CompanyId)
        {
            int totalProductCount = 0;
            SQLDbInterface sqldbinterface = new SQLDbInterface();
            try
            {
                totalProductCount = sqldbinterface.GetTotalProductCount(CompanyId);              
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return totalProductCount;
        }

        public int GetTodayProduct(int CompanyId)
        {
            int todayProductCount = 0;
            SQLDbInterface sqldbinterface = new SQLDbInterface();
            try
            {
                todayProductCount = sqldbinterface.GetTodayProduct(CompanyId);
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return todayProductCount;
        }

        public List<StoreRequisitionViewModel> GetSupervisorDashboardRejectedStoreRequisition(int companyId)
        {
            List<StoreRequisitionViewModel> storeRequisitionList = new List<StoreRequisitionViewModel>();
            SQLDbInterface sqlDbInterface = new SQLDbInterface();
            try
            {
                DataTable dtCount = sqlDbInterface.GetSupervisorDashboardRejectedStoreRequisition(companyId);
                if (dtCount != null && dtCount.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtCount.Rows)
                    {
                        storeRequisitionList.Add(new StoreRequisitionViewModel
                        {
                            RequisitionId = Convert.ToInt32(dr["RequisitionId"]),
                            RequisitionNo = Convert.ToString(dr["RequisitionNo"]),
                            RequisitionDate = Convert.ToString(dr["RequisitionDate"]),
                            ApprovalStatus = Convert.ToString(dr["ApprovalStatus"]),
                            CustomerName = Convert.ToString(dr["CustomerName"]),
                            CustomerBranchName = Convert.ToString(dr["CustomerSite"])
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

        public List<StoreRequisitionViewModel> GetSupervisorDashboardPendingStoreRequisition(int companyId)
        {
            List<StoreRequisitionViewModel> storeRequisitionList = new List<StoreRequisitionViewModel>();
            SQLDbInterface sqlDbInterface = new SQLDbInterface();
            try
            {
                DataTable dtCount = sqlDbInterface.GetSupervisorDashboardPendingStoreRequisition(companyId);
                if (dtCount != null && dtCount.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtCount.Rows)
                    {
                        storeRequisitionList.Add(new StoreRequisitionViewModel
                        {
                            RequisitionId = Convert.ToInt32(dr["RequisitionId"]),
                            RequisitionNo = Convert.ToString(dr["RequisitionNo"]),
                            RequisitionDate = Convert.ToString(dr["RequisitionDate"]),
                            RequisitionStatus = "Pending",
                            CustomerName = Convert.ToString(dr["CustomerName"]),
                            CustomerBranchName = Convert.ToString(dr["CustomerSite"])
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

    }
}








