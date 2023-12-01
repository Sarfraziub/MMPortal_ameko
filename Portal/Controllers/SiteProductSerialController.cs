﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Portal.Core;
using Portal.Core.ViewModel;
using Portal.Common;
using Portal.DAL;
using System.Reflection;
using System.IO;
using System.Data;
using System.Text;

namespace Portal.Controllers
{
    [CheckSessionBeforeControllerExecuteAttribute(Order = 1)]
    public class SiteProductSerialController : BaseController
    {
        //
        // GET: /Company/
        [ValidateRequest(true, UserInterfaceHelper.Add_SiteProductSerial, (int)AccessMode.ViewAccess, (int)RequestMode.GetPost)]
        public ActionResult AddEditSiteProductSerial(int siteProductSerialId = 0, int accessMode = 3)
        {
            SiteProductSerialViewModel siteProductSerialViewModel = new SiteProductSerialViewModel();

            try
            {
                if (siteProductSerialId != 0)
                {
                    ViewData["siteProductSerialId"] = siteProductSerialId;
                    ViewData["accessMode"] = accessMode;
                }
                else
                {
                    ViewData["siteProductSerialId"] = 0;
                    ViewData["accessMode"] = 0;
                }

            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return View(siteProductSerialViewModel);
        }



        [HttpPost]
        public ActionResult ImportSiteProductSerial(SiteProductSerialViewModel SerialViewModel)
        {
            SiteProductSerialBL siteProductSerialBL = new SiteProductSerialBL();
            try
            {
                if (ModelState.IsValid)
                {
                    if (SerialViewModel.ProductId != null && SerialViewModel.ProductId != 0)
                    {
                        if (Request.Files["FileUpload1"].ContentLength > 0)
                        {
                            string extension = System.IO.Path.GetExtension(Request.Files["FileUpload1"].FileName).ToLower();
                            string query = null;
                            string connString = "";
                            string[] validFileTypes = { ".xls", ".xlsx", ".csv" };

                            string path1 = string.Format("{0}/{1}", Server.MapPath("~/Content/Uploads"), Request.Files["FileUpload1"].FileName);
                            if (!Directory.Exists(path1))
                            {
                                Directory.CreateDirectory(Server.MapPath("~/Content/Uploads"));
                            }
                            if (validFileTypes.Contains(extension))
                            {
                                DataTable dt = new DataTable();
                                if (System.IO.File.Exists(path1))
                                { System.IO.File.Delete(path1); }
                                Request.Files["FileUpload1"].SaveAs(path1);
                                if (extension == ".csv")
                                {
                                    dt = CommonHelper.ConvertCSVtoDataTable(path1);
                                    ViewBag.Data = dt;
                                }

                                else if (extension.Trim() == ".xls")
                                {
                                    connString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + path1 + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\"";
                                    dt = CommonHelper.ConvertXSLXtoDataTable(path1, connString);
                                    ViewBag.Data = dt;
                                }
                                else if (extension.Trim() == ".xlsx")
                                {
                                    connString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path1 + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
                                    dt = CommonHelper.ConvertXSLXtoDataTable(path1, connString);
                                    ViewBag.Data = dt;
                                }


                                StringBuilder strErrMsg = new StringBuilder(" ");
                                if (dt.Rows.Count > 0)
                                {

                                    int rowCounter = 1;
                                    SiteProductSerialViewModel siteProductSerialViewModel;
                                    dt.Columns.Add("UploadStatus", typeof(bool));
                                    bool rowVerifyStatus = true;
                                    int sequenceNo = 0;
                                    int serialCount = 0;
                                    foreach (DataRow dr in dt.Rows)
                                    {
                                        serialCount = 0;
                                        siteProductSerialViewModel = new SiteProductSerialViewModel();
                                        if (string.IsNullOrEmpty(Convert.ToString(dr["Serial1"])))
                                        {
                                            strErrMsg.Append("Serial1 Name Column has not proper data in Row #" + rowCounter.ToString() + Environment.NewLine);
                                            rowVerifyStatus = false;
                                        }
                                        object serials = dt.Compute("COUNT(Serial1)", "Serial1='" + Convert.ToString(dr["Serial1"]) + "'");
                                        if (serials != null)
                                        {
                                            serialCount = (int)serials;
                                            if (serialCount > 1)
                                            {
                                                strErrMsg.Append("Serial1 exist more than once in Row #" + rowCounter.ToString() + Environment.NewLine);
                                                rowVerifyStatus = false;

                                            }
                                        }
                                        else
                                        {
                                            strErrMsg.Append("Serial1 data not found in Row #" + rowCounter.ToString() + Environment.NewLine);
                                            rowVerifyStatus = false;
                                        }
                                        if (rowVerifyStatus == true)
                                        {
                                            siteProductSerialViewModel.ProductId = SerialViewModel.ProductId;
                                            siteProductSerialViewModel.ProductSerialNo = Convert.ToString(dr["Serial1"]);
                                            siteProductSerialViewModel.Serial1 = Convert.ToString(dr["Serial1"]);
                                            siteProductSerialViewModel.Serial2 = Convert.ToString(dr["Serial2"]);
                                            siteProductSerialViewModel.Serial3 = Convert.ToString(dr["Serial3"]);
                                            siteProductSerialViewModel.ProductSerialStatus = "OPN";
                                            ResponseOut responseOut = siteProductSerialBL.ImportSiteProductSerial(siteProductSerialViewModel);
                                            if (responseOut.status == ActionStatus.Fail)
                                            {
                                                strErrMsg.Append(responseOut.message + ": Error in Inserting in Row #" + rowCounter.ToString() + Environment.NewLine);
                                                dr["UploadStatus"] = false;
                                            }
                                            else
                                            {
                                                dr["UploadStatus"] = true;
                                            }
                                        }
                                        else
                                        {
                                            dr["UploadStatus"] = false;
                                        }
                                        rowCounter += 1;

                                    }
                                    dt.AcceptChanges();
                                }
                                else
                                {
                                    strErrMsg.Append("Import not found");
                                }

                                ViewBag.Error = strErrMsg.ToString();


                            }
                            else
                            {
                                ViewBag.Error = "Please Upload Files in .xls, .xlsx or .csv format";
                            }
                        }
                    }
                    else
                    {
                        ViewBag.Error = "Please Select Product Name";
                    }
                }

            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
            }
            return View("AddEditSiteProductSerial");
        }

    }
}
