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
  public   class SiteConsumptionBL
    {
        DBInterface dbInterface;
        public SiteConsumptionBL()
        {
            dbInterface = new DBInterface();
        }


        public List<SiteConsumptionProductViewModel> GetSiteConsumptionProductList(int siteConsumptionId)
        {
            List<SiteConsumptionProductViewModel> siteConsumptionProducts = new List<SiteConsumptionProductViewModel>();
            SQLDbInterface sqlDbInterface = new SQLDbInterface();
            try
            {
                DataTable dtSiteConsumption = sqlDbInterface.GetSiteConsumptionProductList(siteConsumptionId);
                if (dtSiteConsumption != null && dtSiteConsumption.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtSiteConsumption.Rows)
                    {
                        siteConsumptionProducts.Add(new SiteConsumptionProductViewModel
                        {
                            SiteConsumptionDetailId = Convert.ToInt32(dr["SiteConsumptionDetailId"]),
                            SequenceNo = Convert.ToInt32(dr["SequenceNo"]),
                            ProductId = Convert.ToInt32(dr["ProductId"]),
                            ProductName = Convert.ToString(dr["ProductName"]),
                            ProductCode = Convert.ToString(dr["ProductCode"]),
                            ProductMainGroupId = Convert.ToInt32(dr["ProductMainGroupId"]),
                            ProductMainGroupName = Convert.ToString(dr["ProductMainGroupName"]),
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
            return siteConsumptionProducts;
        }

        public ResponseOut AddEditSiteConsumption(SiteConsumptionViewModel siteConsumptionViewModel, List<SiteConsumptionProductViewModel>siteConsumptionProducts)
        {
            ResponseOut responseOut = new ResponseOut();
            SQLDbInterface sqlDbInterface = new SQLDbInterface();
            try
            {
                SiteConsumption siteCosumptionList = new SiteConsumption
                {
                    SiteConsumptionNo=siteConsumptionViewModel.SiteConsumptionNo,
                    SiteConsumptionId=siteConsumptionViewModel.SiteConsumptionId,
                    SiteConsumptionDate= Convert.ToDateTime(siteConsumptionViewModel.SiteConsumptionDate),
                    CustomerId=siteConsumptionViewModel.CustomerId,
                    CustomerBranchId=siteConsumptionViewModel.CustomerBranchId,
                    CompanyId=siteConsumptionViewModel.CompanyId,
                    CompanyBranchId=siteConsumptionViewModel.CompanyBranchId,
                    Remarks1=siteConsumptionViewModel.Remarks1,
                    Remarks2=siteConsumptionViewModel.Remarks2,                 
                    CreatedBy = siteConsumptionViewModel.CreatedBy,
                    ModifiedBy = siteConsumptionViewModel.ModifiedBy,
                    ConsumedByUser=siteConsumptionViewModel.ConsumedByUser,
                    ConsumptionStatus=siteConsumptionViewModel.ConsumptionStatus,
                    FinYearId = siteConsumptionViewModel.FinYearId
                };
                List<SiteConsumptionDetail> siteConsumptionProductList = new List<SiteConsumptionDetail>();
                if (siteConsumptionProducts != null && siteConsumptionProducts.Count > 0)
                {
                    foreach (SiteConsumptionProductViewModel item in siteConsumptionProducts)
                    {
                        siteConsumptionProductList.Add(new SiteConsumptionDetail
                        {
                            ProductId = item.ProductId,
                            Quantity = item.Quantity,
                            
                        });
                    }
                }
                responseOut = sqlDbInterface.AddEditSiteConsumption(siteCosumptionList, siteConsumptionProductList);

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
        public List<SiteConsumptionProductViewModel> GetPackingListProductList(int sizeConsumptionId)
        {
            List<SiteConsumptionProductViewModel> siteConsumptionProducts = new List<SiteConsumptionProductViewModel>();
            SQLDbInterface sqlDbInterface = new SQLDbInterface();
            try
            {
                DataTable dtPackingList = sqlDbInterface.GetSiteConsumptionProductList(sizeConsumptionId);
                if (dtPackingList != null && dtPackingList.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtPackingList.Rows)
                    {
                        siteConsumptionProducts.Add(new SiteConsumptionProductViewModel
                        {
                            SiteConsumptionDetailId = Convert.ToInt32(dr["PackingListDetailedID"]),
                            SiteConsumptionId = Convert.ToInt32(dr["SiteConsumptionId"]),
                            SequenceNo = Convert.ToInt32(dr["SequenceNo"]),
                            ProductId = Convert.ToInt32(dr["ProductId"]),
                            ProductName = Convert.ToString(dr["ProductName"]),
                            ProductCode = Convert.ToString(dr["ProductCode"]),
                            ProductShortDesc = Convert.ToString(dr["ProductShortDesc"]),
                            UOMName = Convert.ToString(dr["UOMName"]),
                            Quantity = Convert.ToDecimal(dr["Quantity"]),
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return siteConsumptionProducts;
        }

        public List<SiteConsumptionViewModel> GetSiteConsumptionList(string siteConsumptionNo, int companyBranchId, string fromDate, string toDate, int companyId, string consumptionStatus = "")
        {
            List<SiteConsumptionViewModel> siteConsumptionViewModel = new List<SiteConsumptionViewModel>();
            SQLDbInterface sqlDbInterface = new SQLDbInterface();
            try
            {
                DataTable dtsiteConsumption = sqlDbInterface.GetSiteConsumptionList(siteConsumptionNo, companyBranchId, Convert.ToDateTime(fromDate), Convert.ToDateTime(toDate), companyId, consumptionStatus);
                if (dtsiteConsumption != null && dtsiteConsumption.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtsiteConsumption.Rows)
                    {
                        siteConsumptionViewModel.Add(new SiteConsumptionViewModel
                        {
                            SiteConsumptionId = Convert.ToInt32(dr["SiteConsumptionId"]),
                            SiteConsumptionNo = Convert.ToString(dr["SiteConsumptionNo"]),
                           // WorkOrderId = Convert.ToInt32(dr["WorkOrderId"]),
                          //  WorkOrderNo = Convert.ToString(dr["WorkOrderNo"]),
                            SiteConsumptionDate = Convert.ToString(dr["SiteConsumptionDate"]),
                            CompanyBranchId = Convert.ToInt32(dr["CompanyBranchId"]),
                            CompanyBranchName = Convert.ToString(dr["CompanyBranchName"]),
                            ConsumptionStatus = Convert.ToString(dr["ConsumptionStatus"]),
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
            return siteConsumptionViewModel;
        }
        public SiteConsumptionViewModel GetSiteConsumptionDetail(long siteConsumptionId = 0)
        {
            SiteConsumptionViewModel siteConsumptionViewModel = new SiteConsumptionViewModel();
            SQLDbInterface sqlDbInterface = new SQLDbInterface();
            try
            {
                DataTable dtSiteConsumption = sqlDbInterface.GetSiteConsumptionDetail(siteConsumptionId);
                if (dtSiteConsumption != null && dtSiteConsumption.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtSiteConsumption.Rows)
                    {
                        siteConsumptionViewModel = new SiteConsumptionViewModel
                        {
                            SiteConsumptionId = Convert.ToInt32(dr["SiteConsumptionId"]),
                            SiteConsumptionNo = Convert.ToString(dr["SiteConsumptionNo"]),
                            CustomerId = Convert.ToInt32(dr["CustomerId"]),
                            CustomerBranchId = Convert.ToInt32(dr["CustomerBranchId"]),
                            ConsumedByUser = Convert.ToString(dr["ConsumedByUser"]),
                            CustomerName =Convert.ToString(dr["CustomerName"]),
                            CustomerBranchName = Convert.ToString(dr["CustomerBranchName"]),
                            CustomerCode = Convert.ToString(dr["CustomerCode"]),
                            SiteConsumptionDate = Convert.ToString(dr["SiteConsumptionDate"]),
                            CompanyBranchId = Convert.ToInt32(dr["CompanyBranchId"]),
                            ConsumptionStatus = Convert.ToString(dr["ConsumptionStatus"]),
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
            return siteConsumptionViewModel;
        }

        public DataTable GetProductSiteConsumptionListReport(int customerId, int customerBranchId, int productId = 0, int companyBranchId=0, string fromDate="", string toDate="", int companyId=0)
        {

            SQLDbInterface sqlDbInterface = new SQLDbInterface();
            DataTable dtSiteConsumptionReports = new DataTable();
            try
            {
                dtSiteConsumptionReports = sqlDbInterface.GetProductSiteConsumptionListReport(customerId, customerBranchId, productId, companyBranchId, fromDate, toDate, companyId);
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dtSiteConsumptionReports;
        }
    }
}
