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
    public class SLTrialBalancePrintBL
    {
        DBInterface dbInterface;
        public SLTrialBalancePrintBL()
        {
            dbInterface = new DBInterface();
        }
        #region SL Trial Balance
        public ResponseOut GenerateSLTrialBalance(int slTypeId, int glId, int companyId, int finYearId, DateTime asOnDate, int reportUserId, string sessionId)
        {
            ResponseOut responseOut = new ResponseOut();
            SQLDbInterface sqlDbInterface = new SQLDbInterface();
            try
            {
                responseOut = sqlDbInterface.GenerateSLTrial(slTypeId, glId,companyId, finYearId, asOnDate, reportUserId, sessionId);
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
        public DataTable GetSLTrialBalanceDataTable(int reportUserId, string sessionId)
        {
            SQLDbInterface sqlDbInterface = new SQLDbInterface();
            DataTable dtTrialBalance = new DataTable();
            try
            {
                dtTrialBalance = sqlDbInterface.GetSLTrialDetail(reportUserId,sessionId);
            }
            catch (Exception ex)
            {
                Logger.SaveErrorLog(this.ToString(), MethodBase.GetCurrentMethod().Name, ex);
                throw ex;
            }
            return dtTrialBalance;
        }
        #endregion
    }
}








