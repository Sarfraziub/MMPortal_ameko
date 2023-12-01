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
    public class EscalationMasterBL
    {
        DBInterface dbInterface;
        public EscalationMasterBL()
        {
            dbInterface = new DBInterface();
        }
  
        public List<EscalationMasterViewModel> GetEscalationMasterList()
        {
            List<EscalationMasterViewModel> escalationMasters = new List<EscalationMasterViewModel>();
            try
            {
                List<EscalationMaster> escalationMasterList = dbInterface.GetEscalationMasterList();
                if (escalationMasterList != null && escalationMasterList.Count > 0)
                {
                    foreach (EscalationMaster escalationMaster in escalationMasterList)
                    {
                        escalationMasters.Add(new EscalationMasterViewModel { EscalationId = escalationMaster.EscalationId, ProcessType = escalationMaster.ProcessType, EscalationDays = Convert.ToInt32(escalationMaster.EscalationDays) });
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return escalationMasters;
        }

        public ResponseOut AddEditEscalationMaster(EscalationMasterViewModel escalationMasterViewModel)
        {
            ResponseOut responseOut = new ResponseOut();
            try
            {
                EscalationMaster escalationMaster = new EscalationMaster
                {
                   EscalationId = escalationMasterViewModel.EscalationId,
                    ProcessType = escalationMasterViewModel.ProcessType,
                    EscalationDays = escalationMasterViewModel.EscalationDays
                };
                responseOut = dbInterface.AddEditEscalationMaster(escalationMaster);
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



        public List<EscalationMasterViewModel> GetEscalationMasterList(string processType = "")
        {
            List<EscalationMasterViewModel> escalationMasters = new List<EscalationMasterViewModel>();
            SQLDbInterface sqlDbInterface = new SQLDbInterface();
            try
            {
                DataTable dtCustomerTypes = sqlDbInterface.GetEscalationMasterList(processType);
                if (dtCustomerTypes != null && dtCustomerTypes.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtCustomerTypes.Rows)
                    {
                        escalationMasters.Add(new EscalationMasterViewModel
                        {
                            EscalationId = Convert.ToInt32(dr["EscalationId"]),
                            ProcessType = Convert.ToString(dr["ProcessType"]),
                            EscalationDays = Convert.ToInt32(dr["EscalationDays"])
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return escalationMasters;
        }

        public EscalationMasterViewModel GetEscalationMasterDetail(int escalationId = 0)
        {
            EscalationMasterViewModel escalationMaster = new EscalationMasterViewModel();
            SQLDbInterface sqlDbInterface = new SQLDbInterface();
            try
            {
                DataTable dtEscalationMasters = sqlDbInterface.GetEscalationMasterDetail(escalationId);
                if (dtEscalationMasters != null && dtEscalationMasters.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtEscalationMasters.Rows)
                    {
                        escalationMaster = new EscalationMasterViewModel
                        {
                            EscalationId = Convert.ToInt32(dr["EscalationId"]),
                            ProcessType = Convert.ToString(dr["ProcessType"]),
                            EscalationDays = Convert.ToInt32(dr["EscalationDays"])
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return escalationMaster;
        }
    }
}
