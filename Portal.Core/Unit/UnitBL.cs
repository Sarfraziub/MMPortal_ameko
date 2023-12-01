using Portal.Common;
using Portal.Core.ViewModel;
using Portal.DAL;
using Portal.DAL.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;

namespace Portal.Core.Unit
{
    public class UnitBL
    {
        DBInterface dbInterface;
        public UnitBL()
        {
            dbInterface = new DBInterface();
        }
        public ResponseOut AddEditUnit(UnitViewModel unitViewModel)
        {
            ResponseOut responseOut = new ResponseOut();
            try
            {
                Portal.DAL.Unit product = new Portal.DAL.Unit
                {
                    UnitId = unitViewModel.UnitId,
                    UnitName = unitViewModel.UnitName,
                    UnitShortName = unitViewModel.UnitShortName,
                    AllowDecimal = unitViewModel.AllowDecimal,
                    IsMultipleUnit = unitViewModel.IsMultipleUnit,
                    UnitValue = unitViewModel.UnitValue,
                    ParentId = unitViewModel.ParentId,

                };
                responseOut = dbInterface.AddEditUnit(product);
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
        public List<UnitViewModel> GetUnitList(string unitName = "", string unitShortName = "", int parentId = 0)
        {
            List<UnitViewModel> products = new List<UnitViewModel>();
            SQLDbInterface sqlDbInterface = new SQLDbInterface();
            try
            {
                DataTable dtProducts = sqlDbInterface.GetUnitList(unitName, unitShortName, parentId);
                if (dtProducts != null && dtProducts.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtProducts.Rows)
                    {
                        products.Add(new UnitViewModel
                        {
                            UnitId = Convert.ToInt32(dr["UnitId"]),
                            UnitName = Convert.ToString(dr["UnitName"]),
                            UnitShortName = Convert.ToString(dr["UnitShortName"]),
                            AllowDecimal = Convert.ToInt32(dr["AllowDecimal"]),
                            IsMultipleUnit = Convert.ToBoolean(dr["IsMultipleUnit"]),
                            UnitValue = Convert.ToString(dr["UnitValue"]),
                            ParentId = Convert.ToInt32(dr["ParentId"]),

                        });
                    }
                    //products.Where(f => f.UnitName != null && f.UnitName.ToLower().Contains(unitName.ToLower())
                    //|| f.UnitShortName != null && f.UnitShortName.ToLower().Contains(unitShortName.ToLower()));
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return products;
        }
        public UnitViewModel GetUnitDetail(long unitId = 0)
        {
            UnitViewModel product = new UnitViewModel();
            SQLDbInterface sqlDbInterface = new SQLDbInterface();
            try
            {
                DataTable dtProducts = sqlDbInterface.GetUnitDetail(unitId);
                if (dtProducts != null && dtProducts.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtProducts.Rows)
                    {
                        product = new UnitViewModel
                        {
                            UnitId = Convert.ToInt32(dr["UnitId"]),
                            UnitName = Convert.ToString(dr["UnitName"]),
                            UnitShortName = Convert.ToString(dr["UnitShortName"]),
                            AllowDecimal = Convert.ToInt32(dr["AllowDecimal"]),
                            IsMultipleUnit = Convert.ToBoolean(dr["IsMultipleUnit"]),
                            UnitValue = dr["UnitValue"] != DBNull.Value ? Convert.ToString(dr["UnitValue"]) : "",
                            ParentId = dr["ParentId"] != DBNull.Value ? Convert.ToInt32(dr["ParentId"]) : 0,
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return product;
        }

        public List<UnitViewModel> GetBaseUnitList()
        {
            List<UnitViewModel> unitListVM = new List<UnitViewModel>();
            try
            {
                var unitList = dbInterface.GetBaseUnitList();

                if (unitList != null && unitList.Count > 0)
                {
                    foreach (var unit in unitList)
                    {
                        unitListVM.Add(new UnitViewModel
                        {
                            UnitId = unit.UnitId,
                            UnitName=unit.UnitName,
                            UnitShortName=unit.UnitShortName,
                            IsMultipleUnit=unit.IsMultipleUnit,
                            AllowDecimal = unit.AllowDecimal,
                            UnitValue = unit.UnitValue,
                            ParentId = unit.ParentId
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return unitListVM;
        }

    }
}
