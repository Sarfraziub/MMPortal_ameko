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
using System.Transactions;
namespace Portal.Core
{
    public class WorkOrderBL
    {
        DBInterface dbInterface;
        public WorkOrderBL()
        {
            dbInterface = new DBInterface();
        }
        public ResponseOut AddEditWorkOrder(WorkOrderViewModel workOrderViewModel,List<WorkOrderProductViewModel> workOrderProducts)
        {
            ResponseOut responseOut = new ResponseOut();
            SQLDbInterface sqlDbInterface = new SQLDbInterface();
            try
            {
                WorkOrder workOrder= new WorkOrder
                {
                    WorkOrderId = workOrderViewModel.WorkOrderId,
                    WorkOrderDate = Convert.ToDateTime(workOrderViewModel.WorkOrderDate),
                    TargetFromDate = Convert.ToDateTime(workOrderViewModel.TargetFromDate),
                    TargetToDate = Convert.ToDateTime(workOrderViewModel.TargetToDate),
                    CompanyId = workOrderViewModel.CompanyId,
                    CompanyBranchId = workOrderViewModel.CompanyBranchId,
                    Remarks1 = workOrderViewModel.Remarks1,
                    Remarks2 = workOrderViewModel.Remarks2,
                    CreatedBy = workOrderViewModel.CreatedBy,
                    WorkOrderStatus = workOrderViewModel.WorkOrderStatus
                };
                List<WorkOrderProductDetail> workOrderProductList = new List<WorkOrderProductDetail>();
                if(workOrderProducts != null && workOrderProducts.Count>0)
                {
                    foreach(WorkOrderProductViewModel item in workOrderProducts)
                    {
                        workOrderProductList.Add(new WorkOrderProductDetail
                        {
                            ProductId=item.ProductId,
                            Quantity=item.Quantity
                        });
                    }
                }

                responseOut = sqlDbInterface.AddEditWorkOrder(workOrder, workOrderProductList);
             

             
             
            }

            catch (Exception ex)
            {
                responseOut.status = ActionStatus.Fail;
                responseOut.message = ActionMessage.ApplicationException;
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            
            return responseOut;
        }
    
       
        public List<WorkOrderViewModel> GetWorkOrderList(string workOrderNo, int companyBranchId,string fromDate, string toDate, int companyId, string displayType = "",string approvalStatus="")
        {
            List<WorkOrderViewModel> workOrders = new List<WorkOrderViewModel>();
            SQLDbInterface sqlDbInterface = new SQLDbInterface();
            try
            {
                DataTable dtQuotations = sqlDbInterface.GetWorkOrderList(workOrderNo, companyBranchId,  Convert.ToDateTime(fromDate),Convert.ToDateTime(toDate),companyId, displayType, approvalStatus);
                if (dtQuotations != null && dtQuotations.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtQuotations.Rows)
                    {
                        workOrders.Add(new WorkOrderViewModel
                        {
                            WorkOrderId = Convert.ToInt32(dr["WorkOrderId"]),
                            WorkOrderNo = Convert.ToString(dr["WorkOrderNo"]),
                            WorkOrderDate = Convert.ToString(dr["WorkOrderDate"]),
                            TargetFromDate = Convert.ToString(dr["TargetFromDate"]),
                            TargetToDate = Convert.ToString(dr["TargetToDate"]),
                            CompanyBranchId = Convert.ToInt32(dr["CompanyBranchId"]),
                            CompanyBranchName = Convert.ToString(dr["CompanyBranchName"]),
                            WorkOrderStatus = Convert.ToString(dr["WorkOrderStatus"]),
                            CreatedByUserName = Convert.ToString(dr["CreatedByName"]),
                            CreatedDate = Convert.ToString(dr["CreatedDate"]),
                            ModifiedByUserName = Convert.ToString(dr["ModifiedByName"]),
                            ModifiedDate = Convert.ToString(dr["ModifiedDate"])
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return workOrders;
        }
        public WorkOrderViewModel GetWorkOrderDetail(long workOrderId = 0)
        {
            WorkOrderViewModel workOrder = new WorkOrderViewModel();
            SQLDbInterface sqlDbInterface = new SQLDbInterface();
            try
            {
                DataTable dtCompanies = sqlDbInterface.GetWorkOrderDetail(workOrderId);
                if (dtCompanies != null && dtCompanies.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtCompanies.Rows)
                    {
                        workOrder = new WorkOrderViewModel
                        {
                            WorkOrderId = Convert.ToInt32(dr["WorkOrderId"]),
                            WorkOrderNo = Convert.ToString(dr["WorkOrderNo"]),
                            WorkOrderDate = Convert.ToString(dr["WorkOrderDate"]),
                            TargetFromDate = Convert.ToString(dr["TargetFromDate"]),
                            TargetToDate = Convert.ToString(dr["TargetToDate"]),
                            CompanyBranchId =Convert.ToInt32(dr["CompanyBranchId"]),
                            WorkOrderStatus = Convert.ToString(dr["WorkOrderStatus"]),
                            Remarks1 = Convert.ToString(dr["Remarks1"]),
                            Remarks2 = Convert.ToString(dr["Remarks2"]),
                            CreatedByUserName = Convert.ToString(dr["CreatedByName"]),
                            CreatedDate = Convert.ToString(dr["CreatedDate"]),
                            ModifiedByUserName = Convert.ToString(dr["ModifiedByName"]),
                            ModifiedDate = Convert.ToString(dr["ModifiedDate"])
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return workOrder;
        }
    
 
        public List<WorkOrderProductViewModel> GetWorkOrderProductList(long workOrderId)
        {
            List<WorkOrderProductViewModel> workOrderProducts = new List<WorkOrderProductViewModel>();
            SQLDbInterface sqlDbInterface = new SQLDbInterface();
            try
            {
                DataTable dtCustomers = sqlDbInterface.GetWorkOrderProductList(workOrderId);
                if (dtCustomers != null && dtCustomers.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtCustomers.Rows)
                    {
                        workOrderProducts.Add(new WorkOrderProductViewModel
                        {
                            WorkOrderProductDetailId = Convert.ToInt32(dr["WorkOrderProductDetailId"]),
                            SequenceNo = Convert.ToInt32(dr["SNo"]),
                            ProductId = Convert.ToInt32(dr["ProductId"]),
                            ProductName = Convert.ToString(dr["ProductName"]),
                            ProductCode = Convert.ToString(dr["ProductCode"]),
                            ProductShortDesc = Convert.ToString(dr["ProductShortDesc"]),
                            UOMName = Convert.ToString(dr["UOMName"]),
                            Quantity = Convert.ToDecimal(dr["Quantity"])
                            
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return workOrderProducts;
        }

        public DataTable GetWorkOrderProductListDataTable(long workOrderId)
        {
            SQLDbInterface sqlDbInterface = new SQLDbInterface();
            DataTable dtProducts = new DataTable();
            try
            {
                dtProducts = sqlDbInterface.GetWorkOrderProductList(workOrderId);
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dtProducts;
        }

        public DataTable GetWorkOrderDataTable(long workOrderId = 0)
        {
            SQLDbInterface sqlDbInterface = new SQLDbInterface();
            DataTable dtworkOrder = new DataTable();
            try
            {
                dtworkOrder = sqlDbInterface.GetWorkOrderDetail(workOrderId);
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dtworkOrder;
        }



    }
}
