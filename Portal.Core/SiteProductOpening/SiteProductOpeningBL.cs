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
    public class SiteProductOpeningBL
    {
        DBInterface dbInterface;
        public SiteProductOpeningBL()
        {
            dbInterface = new DBInterface();
        }
        public ResponseOut AddEditSiteProductOpening(SiteProductOpeningViewModel siteProductOpeningViewModel)
        {
            ResponseOut responseOut = new ResponseOut();
            try
            {
                SiteProductOpeningStock siteProductOpening = new SiteProductOpeningStock
                {
                    SiteOpeningTrnId= siteProductOpeningViewModel.SiteOpeningTrnId,
                    ProductId = siteProductOpeningViewModel.ProductId,
                    FinYearId= siteProductOpeningViewModel.FinYearId,
                    CompanyBranchId = siteProductOpeningViewModel.CompanyBranchId,
                    CustomerId= siteProductOpeningViewModel.CustomerId,
                    CustomerBranchId=siteProductOpeningViewModel.CustomerBranchId,
                    OpeningQty = siteProductOpeningViewModel.OpeningQty,
                    CreatedBy = siteProductOpeningViewModel.CreatedBy,
                    CompanyId= siteProductOpeningViewModel.CompanyId
                };
                responseOut = dbInterface.AddEditSiteProductOpening(siteProductOpening);
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
        public List<SiteProductOpeningViewModel> GetSiteProductOpeningList(string productName, int companyid, int finYearId,int companyBranchId)
        {
            List<SiteProductOpeningViewModel> productOpenings = new List<SiteProductOpeningViewModel>();
            SQLDbInterface sqlDbInterface = new SQLDbInterface();
            try
            {
                DataTable dtProducts = sqlDbInterface.GetSiteProductOpeningList(productName, companyid,finYearId, companyBranchId);
                if (dtProducts != null && dtProducts.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtProducts.Rows)
                    {
                        productOpenings.Add(new SiteProductOpeningViewModel
                        {
                            SiteOpeningTrnId = Convert.ToInt32(dr["SiteOpeningTrnId"]),
                            ProductId = Convert.ToInt32(dr["ProductId"]),
                            ProductName = Convert.ToString(dr["ProductName"]),
                            ProductCode = Convert.ToString(dr["ProductCode"]),
                            ProductShortDesc = Convert.ToString(dr["ProductShortDesc"]),
                            FinYearId = Convert.ToInt16(dr["FinYearId"]),
                            CompanyBranchId = Convert.ToInt32(dr["CompanyBranchId"]),
                            CustomerId= Convert.ToInt32(dr["CustomerId"]),
                            CustomerBranchId = Convert.ToInt32(dr["CustomerBranchId"]),

                            BranchName = Convert.ToString(dr["BranchName"]),
                            FinYearDesc = Convert.ToString(dr["FinYearDesc"]),
                            OpeningQty = Convert.ToDecimal(dr["OpeningQty"])

                        });
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return productOpenings;
        }

        public SiteProductOpeningViewModel GetSiteProductOpeningDetail(long siteOpeningTrnId = 0)
        {
            SiteProductOpeningViewModel productOpening = new SiteProductOpeningViewModel();
            SQLDbInterface sqlDbInterface = new SQLDbInterface();
            try
            {
                DataTable dtProducts = sqlDbInterface.GetSiteProductOpeningDetail(siteOpeningTrnId);
                if (dtProducts != null && dtProducts.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtProducts.Rows)
                    {
                        productOpening = new SiteProductOpeningViewModel
                        {
                            SiteOpeningTrnId = Convert.ToInt32(dr["SiteOpeningTrnId"]),
                            ProductId = Convert.ToInt32(dr["ProductId"]),
                            ProductName = Convert.ToString(dr["ProductName"]),
                            ProductCode = Convert.ToString(dr["ProductCode"]),
                            ProductShortDesc = Convert.ToString(dr["ProductShortDesc"]),
                            FinYearId = Convert.ToInt32(dr["FinYearId"]),
                            FinYearDesc = Convert.ToString(dr["FinYearDesc"]),
                            CompanyBranchId = Convert.ToInt32(dr["CompanyBranchId"]),
                            BranchName = Convert.ToString(dr["BranchName"]),
                            CustomerId= Convert.ToInt32(dr["CustomerId"]),
                            CustomerBranchId= Convert.ToInt32(dr["CustomerBranchId"]),
                            CustomerName = Convert.ToString(dr["CustomerName"]),
                            CustomerBranchName = Convert.ToString(dr["CustomerBranchName"]),
                            OpeningQty = Convert.ToDecimal(dr["OpeningQty"])
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return productOpening;
        }
        public ResponseOut ImportProductOpening(ProductOpeningViewModel productOpeningViewModel)
        {
            ResponseOut responseOut = new ResponseOut();
            try
            {
                ProductOpeningStock productOpening = new ProductOpeningStock
                {
                    OpeningTrnId = productOpeningViewModel.OpeningTrnId,
                    ProductId = productOpeningViewModel.ProductId,
                    FinYearId = productOpeningViewModel.FinYearId,
                    CompanyBranchId = productOpeningViewModel.CompanyBranchId,
                    OpeningQty = productOpeningViewModel.OpeningQty,
                    CreatedBy = productOpeningViewModel.CreatedBy,
                    CompanyId = productOpeningViewModel.CompanyId
                };
                responseOut = dbInterface.AddEditProductOpening(productOpening);
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
    }
}
