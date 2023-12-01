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
    public class ProductBOMBL
    {
        DBInterface dbInterface;
        public ProductBOMBL()
        {
            dbInterface = new DBInterface();
        }
        public ResponseOut AddEditProductBOM(ProductBOMViewModel productBOMViewModel)
        {
            ResponseOut responseOut = new ResponseOut();
            try
            {
                ProductBOM productBOM = new ProductBOM
                {
                    BOMId= productBOMViewModel.BOMId,
                    AssemblyId= productBOMViewModel.AssemblyId,
                    ProductId = productBOMViewModel.ProductId,
                    BOMQty = productBOMViewModel.BOMQty,
                    ProcessType = productBOMViewModel.ProcessType,
                    CreatedBy = productBOMViewModel.CreatedBy,
                    ScrapPercentage= productBOMViewModel.ScrapPercentage,
                    CompanyId = productBOMViewModel.CompanyId,

                    Status= productBOMViewModel.BOM_Status
                };
                responseOut = dbInterface.AddEditProductBOM(productBOM);
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
        public List<ProductBOMViewModel> GetAssemblyList(string assemblyName, string assemblyType, int companyid)
        {
            List<ProductBOMViewModel> productBOMs = new List<ProductBOMViewModel>();
            SQLDbInterface sqlDbInterface = new SQLDbInterface();
            try
            {
                DataTable dtProducts = sqlDbInterface.GetAssemblyList(assemblyName, assemblyType, companyid);
                if (dtProducts != null && dtProducts.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtProducts.Rows)
                    {
                        productBOMs.Add(new ProductBOMViewModel
                        {
                            AssemblyType = Convert.ToString(dr["AssemblyType"]),
                            AssemblyId = Convert.ToInt32(dr["AssemblyId"]),
                            AssemblyName = Convert.ToString(dr["AssemblyName"]),
                            AssemblyCode = Convert.ToString(dr["AssemblyCode"]),
                            AssemblyShortDesc = Convert.ToString(dr["AssemblyShortDesc"])
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return productBOMs;
        }

        public List<ProductBOMViewModel> GetAssemblyBOMList(long assemblyId)
        {
            List<ProductBOMViewModel> productBOMs = new List<ProductBOMViewModel>();
            SQLDbInterface sqlDbInterface = new SQLDbInterface();
            try
            {
                DataTable dtProducts = sqlDbInterface.GetAssemblyBOMList(assemblyId);
                if (dtProducts != null && dtProducts.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtProducts.Rows)
                    {
                        productBOMs.Add(new ProductBOMViewModel
                        {
                            BOMId = Convert.ToInt64(dr["BOMId"]),
                            AssemblyType = Convert.ToString(dr["AssemblyType"]),
                            ProductId = Convert.ToInt32(dr["ProductId"]),
                            ProductName = Convert.ToString(dr["ProductName"]),
                            ProductCode = Convert.ToString(dr["ProductCode"]),
                            ProductShortDesc = Convert.ToString(dr["ProductShortDesc"]),
                            BOMQty = Convert.ToDecimal(dr["BOMQty"]),
                            ScrapPercentage=Convert.ToDecimal(dr["ScrapPercentage"]),
                            BOM_Status=Convert.ToBoolean(dr["Status"])
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return productBOMs;
        }

        public ProductBOMViewModel GetProductBOMDetail(long bomId = 0)
        {
            ProductBOMViewModel productBOM = new ProductBOMViewModel();
            SQLDbInterface sqlDbInterface = new SQLDbInterface();
            try
            {
                DataTable dtProducts = sqlDbInterface.GetProductBOMDetail(bomId);
                if (dtProducts != null && dtProducts.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtProducts.Rows)
                    {
                        productBOM = new ProductBOMViewModel
                        {
                            BOMId = Convert.ToInt32(dr["BOMId"]),
                            AssemblyType = Convert.ToString(dr["AssemblyType"]),
                            AssemblyId = Convert.ToInt32(dr["AssemblyId"]),
                            AssemblyName = Convert.ToString(dr["AssemblyName"]),
                            AssemblyCode = Convert.ToString(dr["AssemblyCode"]),
                            AssemblyShortDesc = Convert.ToString(dr["AssemblyShortDesc"]),
                            ProcessType = Convert.ToString(dr["ProcessType"]),
                            ProductId = Convert.ToInt32(dr["ProductId"]),
                            ProductName = Convert.ToString(dr["ProductName"]),
                            ProductCode = Convert.ToString(dr["ProductCode"]),
                            ProductShortDesc = Convert.ToString(dr["ProductShortDesc"]),
                            BOMQty = Convert.ToDecimal(dr["BOMQty"]),
                            ScrapPercentage = Convert.ToDecimal(dr["ScrapPercentage"]),
                            BOM_Status =Convert.ToBoolean(dr["Status"])
                            
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return productBOM;
        }
        public ResponseOut CopyProductBOM(long copyFromAssemblyId, long copyToAssemblyId, int createdBy)
        {
            ResponseOut responseOut = new ResponseOut();
            try
            {
               responseOut = dbInterface.CopyProductBOM(copyFromAssemblyId, copyToAssemblyId, createdBy);
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
        public ResponseOut ImportProductBOM(ProductBOMViewModel productBOMViewModel)
        {
            ResponseOut responseOut = new ResponseOut();
            try
            {
                ProductBOM productBOM = new ProductBOM
                {
                    BOMId = productBOMViewModel.BOMId,
                    AssemblyId = productBOMViewModel.AssemblyId,
                    ProductId = productBOMViewModel.ProductId,
                    BOMQty = productBOMViewModel.BOMQty,
                    ProcessType = productBOMViewModel.ProcessType,
                    CreatedBy = productBOMViewModel.CreatedBy,
                    CompanyId = productBOMViewModel.CompanyId,
                    Status = productBOMViewModel.BOM_Status
                };
                responseOut = dbInterface.AddEditProductBOM(productBOM);
                
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

        public ProductViewModel GetProductMainGroupNameByProductID(long productId)
        {
            ProductViewModel products = new ProductViewModel();
            SQLDbInterface sqlDbInterface = new SQLDbInterface();
            try
            {
                DataTable dtProducts = sqlDbInterface.GetProductMainGroupNameByProductID(productId);
                if (dtProducts != null && dtProducts.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtProducts.Rows)
                    {
                        products = new ProductViewModel
                        {
                            ProductMainGroupName = Convert.ToString(dr["ProductMainGroupName"]),
                            UOMName = Convert.ToString(dr["UOMName"])
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return products;
        }

        public DataTable GetBOMListReport(long assemblyId, string assemblyType, int companyId)
        {

            SQLDbInterface sqlDbInterface = new SQLDbInterface();
            DataTable dtBOMReports = new DataTable();
            try
            {
                dtBOMReports = sqlDbInterface.GetBOMListReport(assemblyId, assemblyType,companyId);
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dtBOMReports;
        }

    }
}
