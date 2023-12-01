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
    public class FabricationBL
    {
        DBInterface dbInterface;
        public FabricationBL()
        {
            dbInterface = new DBInterface();
        }
        public ResponseOut AddEditFabrication(FabricationViewModel fabricationViewModel, List<FabricationProductViewModel> fabricationProducts)
        {
            ResponseOut responseOut = new ResponseOut();
            SQLDbInterface sqlDbInterface = new SQLDbInterface();
            try
            {
                Fabrication fabrication = new Fabrication
                {
                    FabricationId= fabricationViewModel.FabricationId,
                    FabricationDate = Convert.ToDateTime(fabricationViewModel.FabricationDate),
                    WorkOrderId = fabricationViewModel.WorkOrderId,
                    WorkOrderNo = fabricationViewModel.WorkOrderNo,                    
                    CompanyId = fabricationViewModel.CompanyId,
                    CompanyBranchId = fabricationViewModel.CompanyBranchId,
                    Remarks1 = fabricationViewModel.Remarks1,
                    Remarks2 = fabricationViewModel.Remarks2,
                    CreatedBy = fabricationViewModel.CreatedBy,
                    FabricationStatus = fabricationViewModel.FabricationStatus
                };
                List<FabricationProductDetail> fabricationProductList = new List<FabricationProductDetail>();
                if(fabricationProducts != null && fabricationProducts.Count>0)
                {
                    foreach(FabricationProductViewModel item in fabricationProducts)
                    {
                        fabricationProductList.Add(new FabricationProductDetail
                        {
                            ProductId=item.ProductId,
                            Quantity=item.Quantity
                        });
                    }
                }

                responseOut = sqlDbInterface.AddEditFabrication(fabrication, fabricationProductList);
             

             
             
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
    
       
        public List<FabricationViewModel> GetFabricationList(string fabricationNo, string workOrderNo, int companyBranchId,string fromDate, string toDate, int companyId, string fabricationStatus = "")
        {
            List<FabricationViewModel> fabricationViewModel = new List<FabricationViewModel>();
            SQLDbInterface sqlDbInterface = new SQLDbInterface();
            try
            {
                DataTable dtQuotations = sqlDbInterface.GetFabricationList(fabricationNo,workOrderNo, companyBranchId,  Convert.ToDateTime(fromDate),Convert.ToDateTime(toDate),companyId, fabricationStatus);
                if (dtQuotations != null && dtQuotations.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtQuotations.Rows)
                    {
                        fabricationViewModel.Add(new FabricationViewModel
                        {
                            FabricationId = Convert.ToInt32(dr["FabricationId"]),
                            FabricationNo = Convert.ToString(dr["FabricationNo"]),
                            WorkOrderId = Convert.ToInt32(dr["WorkOrderId"]),
                            WorkOrderNo = Convert.ToString(dr["WorkOrderNo"]),
                            FabricationDate = Convert.ToString(dr["FabricationDate"]),
                            CompanyBranchId = Convert.ToInt32(dr["CompanyBranchId"]),
                            CompanyBranchName = Convert.ToString(dr["CompanyBranchName"]),
                            FabricationStatus = Convert.ToString(dr["FabricationStatus"]),                          
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
            return fabricationViewModel;
        }
        public FabricationViewModel GetFabricationDetail(long fabricationId = 0)
        {
            FabricationViewModel fabricationViewModel = new FabricationViewModel();           
            SQLDbInterface sqlDbInterface = new SQLDbInterface();
            try
            {
                DataTable dtCompanies = sqlDbInterface.GetFabricationDetail(fabricationId);
                if (dtCompanies != null && dtCompanies.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtCompanies.Rows)
                    {
                        fabricationViewModel = new FabricationViewModel
                        {
                            FabricationId= Convert.ToInt32(dr["FabricationId"]),
                            FabricationNo= Convert.ToString(dr["FabricationNo"]),
                            WorkOrderId = Convert.ToInt32(dr["WorkOrderId"]),
                            WorkOrderNo = Convert.ToString(dr["WorkOrderNo"]),
                            FabricationDate = Convert.ToString(dr["FabricationDate"]),                           
                            CompanyBranchId =Convert.ToInt32(dr["CompanyBranchId"]),
                            FabricationStatus = Convert.ToString(dr["FabricationStatus"]),
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
            return fabricationViewModel;
        }
    
 
        public List<FabricationProductViewModel> GetFabricationProductList(long fabricationId)
        {
            List<FabricationProductViewModel> fabricationProducts = new List<FabricationProductViewModel>();
            SQLDbInterface sqlDbInterface = new SQLDbInterface();
            try
            {
                DataTable dtCustomers = sqlDbInterface.GetFabricationProductList(fabricationId);
                if (dtCustomers != null && dtCustomers.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtCustomers.Rows)
                    {
                        fabricationProducts.Add(new FabricationProductViewModel
                        {
                            FabricationDetailId = Convert.ToInt32(dr["FabricationDetailId"]),
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
            return fabricationProducts;
        }

     
    
     
     
    }
}
