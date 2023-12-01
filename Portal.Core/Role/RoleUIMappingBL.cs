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
    public class RoleUIMappingBL
    {
        DBInterface dbInterface;
        public RoleUIMappingBL()
        {
            dbInterface = new DBInterface();
        }
        public ResponseOut AddEditRoleUIMapping(List<RoleUIMappingViewModel> roleUIMappingList)
        {
            ResponseOut responseOut = new ResponseOut();
            try
            {
                foreach (var item in roleUIMappingList)
                { 
                    RoleUIActionMapping roleUIMapping = new RoleUIActionMapping
                    {
                        RoleId = item.RoleId,
                        InterfaceId= item.InterfaceId,
                        AddAccess=item.AddAccess,
                        EditAccess=item.EditAccess,
                        ViewAccess=item.ViewAccess,
                        Status = true,
                    };
                    responseOut = dbInterface.AddEditRoleUIMapping(roleUIMapping);
                }
                
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

        public List<RoleUIMappingViewModel> GetRoleUIActionMappingDetail(string interfaceType = "", string interfaceSubType= "", int roleId=0)
        {
            List<RoleUIMappingViewModel> roles = new List<RoleUIMappingViewModel>();
            SQLDbInterface sqlDbInterface = new SQLDbInterface();
            try
            {
                DataTable dtRoles = sqlDbInterface.GetRoleUIActionMappingDetail(interfaceType, interfaceSubType, roleId);
                if (dtRoles != null && dtRoles.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtRoles.Rows)
                    {
                        roles.Add(new RoleUIMappingViewModel
                        {
                            InterfaceId = Convert.ToInt32(dr["INTERFACEID"]),
                            InterfaceName = Convert.ToString(dr["INTERFACENAME"]),
                            AddAccess = Convert.ToBoolean(dr["AddAccess"]), 
                            EditAccess = Convert.ToBoolean(dr["EditAccess"]),
                            ViewAccess = Convert.ToBoolean(dr["ViewAccess"]),

                        });
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return roles;
        }

        public bool CheckMasterPermission(int roleId, int interfaceId, int accessMode)
        {
            bool isAuthorized = false;
            try
            {
                isAuthorized = dbInterface.AuthorizeUser(roleId, interfaceId, accessMode);
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                isAuthorized = false;
                throw ex;
            }
            return isAuthorized;

        }



    }
}
