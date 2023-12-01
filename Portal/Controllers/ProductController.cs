using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Portal.Core;
using Portal.Core.ViewModel;
using Portal.Common;
using System.Reflection;
using System.IO;
using Portal.Core.Unit;

namespace Portal.Controllers
{
    [CheckSessionBeforeControllerExecuteAttribute(Order = 1)]
    public class ProductController : BaseController
    {
        //
        // GET: /Product/
        //
        // GET: /User/
        
        [ValidateRequest(true, UserInterfaceHelper.Add_Edit_Product, (int)AccessMode.ViewAccess, (int)RequestMode.GetPost)]
        public ActionResult AddEditProduct(int productId = 0, int accessMode = 3)
        {

            try
            {
                if (productId != 0)
                {
                    ViewData["productId"] = productId;
                    ViewData["accessMode"] = accessMode;
                }
                else
                {
                    ViewData["productId"] = 0;
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
        [ValidateRequest(true, UserInterfaceHelper.Add_Edit_Product, (int)AccessMode.AddAccess, (int)RequestMode.Ajax)]
        public ActionResult AddEditProduct(ProductViewModel productViewModel)
        {
            ResponseOut responseOut = new ResponseOut();
            ProductBL productBL = new ProductBL();
            try
            {
                if (productViewModel != null)
                {
                    productViewModel.CompanyId = ContextUser.CompanyId;
                    productViewModel.CreatedBy = ContextUser.UserId;
                    responseOut = productBL.AddEditProduct(productViewModel);

                    if (productViewModel.UnitId > 0)
                    {
                        UnitBL unitBL = new UnitBL();
                        var unitViewModel = unitBL.GetUnitDetail(productViewModel.UnitId);
                        unitViewModel.UnitValue = productViewModel.UnitValue;
                        //unitViewModel.IsMultipleUnit = true;
                        //if (string.IsNullOrEmpty(productViewModel.UnitValue))
                        //{
                        //    unitViewModel.IsMultipleUnit = false;
                        //}
                        unitBL.AddEditUnit(unitViewModel);
                    }

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
        public JsonResult GetSizeAutoCompleteList(string term, int productMainGroupId = 0, int productSubGroupId = 0)
        {
            SizeBL sizeBL = new SizeBL();
            List<SizeViewModel> sizeList = new List<SizeViewModel>();
            try
            {
                sizeList = sizeBL.GetSizeAutoCompleteList(term, productMainGroupId, productSubGroupId);
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return Json(sizeList, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult GetManufacturerAutoCompleteList(string term)
        {
            ManufacturerBL manufacturerBL = new ManufacturerBL();
            List<ManufacturerViewModel> manufacturerList = new List<ManufacturerViewModel>();
            try
            {
                manufacturerList = manufacturerBL.GetManufacturerAutoCompleteList(term);
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return Json(manufacturerList, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult UpdateProductPic()
        {
            ResponseOut responseOut = new ResponseOut();
            ProductBL productBL = new ProductBL();
            HttpFileCollectionBase files = Request.Files;
            ProductViewModel productViewModel = new ProductViewModel();
            try
            {
                productViewModel.Productid = Convert.ToInt32(Request["productId"]);
                //  Get all files from Request object  
                if (files != null && files.Count > 0 && Request.Files[0] != null && Request.Files[0].ContentLength > 0)
                {
                    HttpPostedFileBase file = files[0];
                    string fname;
                    // Checking for Internet Explorer  
                    if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                    {
                        string[] testfiles = file.FileName.Split(new char[] { '\\' });
                        fname = testfiles[testfiles.Length - 1];
                    }
                    else
                    {
                        fname = file.FileName;
                    }

                    if (file != null && file.ContentLength > 0)
                    {
                        var fileName = Path.GetFileName(file.FileName);
                        productViewModel.ProductPicName = productViewModel.Productid.ToString() + "_" + fileName;
                        var path = Path.Combine(Server.MapPath("~/Images/ProductImages"), productViewModel.ProductPicName);
                        file.SaveAs(path);
                        productViewModel.ProductPicPath = path;

                        //queryDetail.QueryAttachment = new byte[file.ContentLength];
                        //file.InputStream.Read(queryDetail.QueryAttachment, 0, file.ContentLength);
                    }
                }

                if (productViewModel != null && !string.IsNullOrEmpty(productViewModel.ProductPicPath))
                {
                    responseOut = productBL.UpdateProductPic(productViewModel);
                }
                else
                {
                    responseOut.message = "";
                    responseOut.status = ActionStatus.Success;
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
        [ValidateRequest(true, UserInterfaceHelper.Add_Edit_Product, (int)AccessMode.ViewAccess, (int)RequestMode.GetPost)]
        public ActionResult ListProduct()
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
        public PartialViewResult GetProductList(string productName, string productCode, string productShortDesc, string productFullDesc, int productTypeId, int productMainGroupId, int productSubGroupId = 0, string assemblyType = "MA", string brandName = "")
        {
            List<ProductViewModel> products = new List<ProductViewModel>();
            ProductBL productBL = new ProductBL();
            try
            {

                products = productBL.GetProductList(productName, ContextUser.CompanyId, productCode, productShortDesc, productFullDesc, productTypeId, productMainGroupId, productSubGroupId, assemblyType, brandName);
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return PartialView(products);
        }

        [HttpGet]
        public JsonResult GetProductDetail(long productid)
        {
            ProductBL productBL = new ProductBL();
            ProductViewModel product = new ProductViewModel();
            try
            {
                UnitBL unitBL = new UnitBL();
                product = productBL.GetProductDetail(productid);
                if (product.UnitId > 0)
                {
                    var unitViewModel = unitBL.GetUnitDetail(product.UnitId);
                    product.UnitValue = unitViewModel.UnitValue;
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return Json(product, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetProductAutoCompleteList(string term)
        {
            ProductBL productBL = new ProductBL();
            List<ProductViewModel> productList = new List<ProductViewModel>();
            try
            {
                productList = productBL.GetProductAutoCompleteList(term,ContextUser.CompanyId);
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return Json(productList, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetProductAutoCompleteListByMainGroup(string term, int productMainGroupId)
        {
            ProductBL productBL = new ProductBL();
            List<ProductViewModel> productList = new List<ProductViewModel>();
            try
            {
                productList = productBL.GetProductAutoCompleteListByMainGroup(term, productMainGroupId, ContextUser.CompanyId);
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return Json(productList, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetProductTypeList()
        {
            ProductTypeBL productTypeBL = new ProductTypeBL();
            List<ProductTypeViewModel> productTypeList = new List<ProductTypeViewModel>();
            try
            {
                productTypeList = productTypeBL.GetProductTypeList();
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return Json(productTypeList, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult GetProductMainGroupList()
        {
            ProductMainGroupBL productMainGroupBL = new ProductMainGroupBL();
            List<ProductMainGroupViewModel> productMainGroupList = new List<ProductMainGroupViewModel>();
            try
            {
                productMainGroupList = productMainGroupBL.GetProductMainGroupList();
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return Json(productMainGroupList, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult GetProductSubGroupList(int productMainGroupId)
        {
            ProductSubGroupBL productSubGroupBL = new ProductSubGroupBL();
            List<ProductSubGroupViewModel> productSubGroupList = new List<ProductSubGroupViewModel>();
            try
            {
                productSubGroupList = productSubGroupBL.GetProductSubGroupList(productMainGroupId);
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return Json(productSubGroupList, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult GetUOMList()
        {
            UOMBL uomBL = new UOMBL();
            List<UOMViewModel> uomList = new List<UOMViewModel>();
            try
            {
                uomList = uomBL.GetUOMList();
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return Json(uomList, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetFinYearList()
        {
            FinYearBL finYearBL = new FinYearBL();
            List<FinYearViewModel> finYearList = new List<FinYearViewModel>();
            try
            {
                finYearList = finYearBL.GetFinancialYearList();
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return Json(finYearList, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetProductAvailableStock(long productid, int companyBranchId, int trnId, string trnType)
        {
            StockLedgerBL stockBL = new StockLedgerBL();
            decimal availableStock = 0;
            try
            {
                int companyId = ContextUser.CompanyId;
                int finYearId = Session[SessionKey.CurrentFinYear] != null ? ((FinYearViewModel)Session[SessionKey.CurrentFinYear]).FinYearId : DateTime.Now.Year;

                availableStock = stockBL.GetProductAvailableStock(productid, finYearId,companyId,companyBranchId,trnId,trnType);
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return Json(availableStock, JsonRequestBehavior.AllowGet);
        }

        [ValidateRequest(true, UserInterfaceHelper.ProductReorderRegister, (int)AccessMode.ViewAccess, (int)RequestMode.GetPost)]
        public ActionResult ListProductReorderQuantity()
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
        [ValidateRequest(true, UserInterfaceHelper.ProductReorderRegister, (int)AccessMode.ViewAccess, (int)RequestMode.GetPost)]
        public PartialViewResult GetProductReorderQuantityList(string productName, string productCode, string productShortDesc, string productFullDesc)
        {
            List<ReorderPointProductCountViewModel> products = new List<ReorderPointProductCountViewModel>();
            ProductBL productBL = new ProductBL();
            try
            {
                
                 int finYearId = Session[SessionKey.CurrentFinYear] != null ? ((FinYearViewModel)Session[SessionKey.CurrentFinYear]).FinYearId : DateTime.Now.Year;
                products = productBL.GetReorderPointProductCountList(productName, productCode, productShortDesc, productFullDesc, ContextUser.CompanyId,finYearId);
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return PartialView(products);
        }

        [HttpPost]
        [ValidateRequest(true, UserInterfaceHelper.Add_Edit_Product, (int)AccessMode.EditAccess, (int)RequestMode.Ajax)]
        public JsonResult RemoveImage(long productId)
        {
            ResponseOut responseOut = new ResponseOut();
            ProductBL productBL = new ProductBL();
            try
            {
                if (productId != 0)
                { 
                    responseOut = productBL.RemoveImage(productId);
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
        public JsonResult GetSiteProductAvailableStock(long productid, int customerId, int customerBranchId)
        {
            StockLedgerBL stockBL = new StockLedgerBL();
            decimal availableStock = 0;
            try
            {
                int companyId = ContextUser.CompanyId;
                int finYearId = Session[SessionKey.CurrentFinYear] != null ? ((FinYearViewModel)Session[SessionKey.CurrentFinYear]).FinYearId : DateTime.Now.Year;

                availableStock = stockBL.GetSiteProductAvailableStock(productid, finYearId, customerId, customerBranchId);
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return Json(availableStock, JsonRequestBehavior.AllowGet);
        }
    }
}
